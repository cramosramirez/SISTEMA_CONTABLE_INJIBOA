USE [PH2]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [EINVENTARIO].[SP_PRODUCTO]
(
    @ACCION                NVARCHAR(30),
    @ID_PRODUCTO           INT            = NULL,
    @COD_REF               NVARCHAR(30)   = NULL,
    @DESCRIPCION           NVARCHAR(255)  = NULL,
    @CCT_INVENT            NVARCHAR(15)   = NULL,
    @ID_CATEGORIA          INT            = NULL,
    @ID_SUBCATEGORIA       INT            = NULL,
    @ID_PRESENTACION       INT            = NULL,
    @TIPOITEM              INT            = NULL,
    @CODTRIBUTO            NVARCHAR(2)    = NULL,
    @UNIMEDIDA             NVARCHAR(2)    = NULL,
    @ID_TPOPERACION_VENTAS INT            = NULL,
    @ID_TPINGRESO_VENTAS   INT            = NULL,
    @ES_EXENTO             BIT            = NULL,
    @ES_NOSUJETA           BIT            = NULL,
    @ES_INVENTARIO         BIT            = NULL,
    @PRECIO                NUMERIC(20,4)  = NULL,
    @DESC_VENTA            NUMERIC(20,4)  = NULL,
    @ULTIMOPRECIOCOMPRA    NUMERIC(20,6)  = NULL,
    @ESTADO                NVARCHAR(3)    = NULL,
    @USER                  NVARCHAR(150)  = NULL,
    @TIPOITEMEXPOR         INT            = NULL,
    @FILTRO                NVARCHAR(100)  = NULL,
    @ES_TRASLADO           NVARCHAR(10)   = NULL,
    @ROL_PROD              NVARCHAR(50)   = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @ACCION = UPPER(LTRIM(RTRIM(@ACCION)));

    -- =============================================
    -- GUARDAR (INSERT o UPDATE)
    -- =============================================
    IF @ACCION = 'GUARDAR'
    BEGIN
        BEGIN TRY
            BEGIN TRAN;

            SET @ES_EXENTO          = ISNULL(@ES_EXENTO, 0);
            SET @ES_NOSUJETA        = ISNULL(@ES_NOSUJETA, 0);
            SET @ES_INVENTARIO      = ISNULL(@ES_INVENTARIO, 0);
            SET @PRECIO             = ISNULL(@PRECIO, 0);
            SET @DESC_VENTA         = ISNULL(@DESC_VENTA, 0);
            SET @ULTIMOPRECIOCOMPRA = ISNULL(@ULTIMOPRECIOCOMPRA, 0);

            IF ISNULL(@ID_PRODUCTO, 0) = 0
            BEGIN
                DECLARE @NEW_ID INT;

                SELECT @NEW_ID = ISNULL(MAX(ID_PRODUCTO), 0) + 1
                FROM EINVENTARIO.PRODUCTO;

                INSERT INTO EINVENTARIO.PRODUCTO
                (
                    ID_PRODUCTO,
                    COD_REF, DESCRIPCION, CCT_INVENT,
                    ID_CATEGORIA, ID_SUBCATEGORIA, ID_PRESENTACION,
                    TIPOITEM, CODTRIBUTO, UNIMEDIDA,
                    ID_TPOPERACION_VENTAS, ID_TPINGRESO_VENTAS,
                    ES_EXENTO, ES_NOSUJETA, ES_INVENTARIO,
                    PRECIO, DESC_VENTA, ULTIMOPRECIOCOMPRA,
                    ESTADO, USER_CREA, FECHA_CREA, TIPOITEMEXPOR
                )
                VALUES
                (
                    @NEW_ID,
                    @COD_REF, @DESCRIPCION, @CCT_INVENT,
                    @ID_CATEGORIA, @ID_SUBCATEGORIA, @ID_PRESENTACION,
                    @TIPOITEM, @CODTRIBUTO, @UNIMEDIDA,
                    @ID_TPOPERACION_VENTAS, @ID_TPINGRESO_VENTAS,
                    @ES_EXENTO, @ES_NOSUJETA, @ES_INVENTARIO,
                    @PRECIO, @DESC_VENTA, @ULTIMOPRECIOCOMPRA,
                    @ESTADO, @USER, GETDATE(), @TIPOITEMEXPOR
                );

                SELECT @NEW_ID AS ID_GENERADO;
            END
            ELSE
            BEGIN
                IF NOT EXISTS
                (
                    SELECT 1
                    FROM EINVENTARIO.PRODUCTO
                    WHERE ID_PRODUCTO = @ID_PRODUCTO
                )
                BEGIN
                    RAISERROR('EL PRODUCTO NO EXISTE', 16, 1);
                    ROLLBACK;
                    RETURN;
                END

                UPDATE EINVENTARIO.PRODUCTO
                SET
                    COD_REF                = @COD_REF,
                    DESCRIPCION            = @DESCRIPCION,
                    CCT_INVENT             = @CCT_INVENT,
                    ID_CATEGORIA           = @ID_CATEGORIA,
                    ID_SUBCATEGORIA        = @ID_SUBCATEGORIA,
                    ID_PRESENTACION        = @ID_PRESENTACION,
                    TIPOITEM               = @TIPOITEM,
                    CODTRIBUTO             = @CODTRIBUTO,
                    UNIMEDIDA              = @UNIMEDIDA,
                    ID_TPOPERACION_VENTAS  = @ID_TPOPERACION_VENTAS,
                    ID_TPINGRESO_VENTAS    = @ID_TPINGRESO_VENTAS,
                    ES_EXENTO              = @ES_EXENTO,
                    ES_NOSUJETA            = @ES_NOSUJETA,
                    ES_INVENTARIO          = @ES_INVENTARIO,
                    PRECIO                 = @PRECIO,
                    DESC_VENTA             = @DESC_VENTA,
                    ULTIMOPRECIOCOMPRA     = @ULTIMOPRECIOCOMPRA,
                    ESTADO                 = @ESTADO,
                    USER_ACT               = @USER,
                    FECHA_ACT              = GETDATE(),
                    TIPOITEMEXPOR          = @TIPOITEMEXPOR
                WHERE ID_PRODUCTO = @ID_PRODUCTO;

                SELECT @ID_PRODUCTO AS ID_GENERADO;
            END

            COMMIT;
        END TRY
        BEGIN CATCH
            IF XACT_STATE() <> 0
                ROLLBACK;

            THROW;
        END CATCH
    END

    -- =============================================
    -- ELIMINAR
    -- =============================================
    ELSE IF @ACCION = 'ELIMINAR'
    BEGIN
        DELETE FROM EINVENTARIO.PRODUCTO
        WHERE ID_PRODUCTO = @ID_PRODUCTO;
    END

    -- =============================================
    -- CONSULTAR (con filtro opcional de ROL)
    -- =============================================
    ELSE IF @ACCION = 'CONSULTAR'
    BEGIN
        SELECT P.*
        FROM EINVENTARIO.PRODUCTO P
        LEFT JOIN EINVENTARIO.PRODUCTO_ROL R
            ON P.ID_PRODUCTO = R.ID_PRODUCTO
        LEFT JOIN EINVENTARIO.ROL_PROD ROL
            ON R.ID_ROL_PROD = ROL.ID_ROL_PROD
        WHERE P.ID_PRODUCTO = @ID_PRODUCTO
          AND (@ROL_PROD IS NULL OR ROL.NOMBRE_ROL_PROD = @ROL_PROD);
    END

    -- =============================================
    -- LISTAR (con filtro opcional de ROL)
    -- =============================================
    ELSE IF @ACCION = 'LISTAR'
    BEGIN
        SELECT DISTINCT P.*
        FROM EINVENTARIO.PRODUCTO P
        LEFT JOIN EINVENTARIO.PRODUCTO_ROL R
            ON P.ID_PRODUCTO = R.ID_PRODUCTO
        LEFT JOIN EINVENTARIO.ROL_PROD ROL
            ON R.ID_ROL_PROD = ROL.ID_ROL_PROD
        WHERE (@ROL_PROD IS NULL OR ROL.NOMBRE_ROL_PROD = @ROL_PROD)
        ORDER BY P.DESCRIPCION;
    END

    -- =============================================
    -- BUSCAR (con filtro opcional de ROL)
    -- =============================================
    ELSE IF @ACCION = 'BUSCAR'
    BEGIN
        SELECT
            P.ID_PRODUCTO,
            P.COD_REF,
            P.DESCRIPCION,
            UM.ID_UNIDAD_MEDIDA,
            UM.VALORES AS UNIMEDIDA,
            P.PRECIO,
            P.ES_EXENTO,
            P.ES_NOSUJETA
        FROM EINVENTARIO.PRODUCTO P
        INNER JOIN EMH.UNIDAD_MEDIDA UM
            ON P.UNIMEDIDA = UM.CODIGO
        LEFT JOIN EINVENTARIO.PRODUCTO_ROL R
            ON P.ID_PRODUCTO = R.ID_PRODUCTO
        LEFT JOIN EINVENTARIO.ROL_PROD ROL
            ON R.ID_ROL_PROD = ROL.ID_ROL_PROD
        WHERE P.ESTADO = 'ACT'
          AND (@ROL_PROD IS NULL OR ROL.NOMBRE_ROL_PROD = @ROL_PROD)
          AND
          (
              @FILTRO IS NULL
              OR P.COD_REF LIKE '%' + @FILTRO + '%'
              OR P.DESCRIPCION LIKE '%' + @FILTRO + '%'
          )
        ORDER BY P.COD_REF;
    END

    -- =============================================
    -- BUSCAR PRODUCTO POR CÓDIGO DE REFERENCIA
    -- =============================================
    ELSE IF @ACCION = 'BUSCAR_PRODUCTO_COD_REF'
    BEGIN
        SET @COD_REF = NULLIF(LTRIM(RTRIM(@COD_REF)), N'');

        SELECT
            P.DESCRIPCION AS NOMBRE_PRODUCTO,
            P.COD_REF,
            P.PRECIO,
            P.ID_PRODUCTO,
            UM.ID_UNIDAD_MEDIDA,
            UM.VALORES AS UNIMEDIDA,
            P.ES_EXENTO,
            P.ES_NOSUJETA
        FROM EINVENTARIO.PRODUCTO AS P
        INNER JOIN EMH.UNIDAD_MEDIDA AS UM
            ON P.UNIMEDIDA = UM.CODIGO
        WHERE P.COD_REF = @COD_REF;
    END

    -- =============================================
    -- PRODUCTOS REGISTRADOS EN EL INVENTARIO LOCAL
    -- =============================================
    ELSE IF @ACCION = 'PRODUCTOS_REGISTRADO'
    BEGIN
        SET @FILTRO = NULLIF(LTRIM(RTRIM(@FILTRO)), N'');

        SELECT
            C.NOMBRE AS NOMBRE_CATEGORIA,
            SC.NOMBRE AS NOMBRE_SUBCATEGORIA,
            P.COD_REF,
            P.DESCRIPCION
        FROM [PH2].[EINVENTARIO].[PRODUCTO] AS P
        INNER JOIN [PH2].[EINVENTARIO].[CATEGORIA] AS C
            ON P.ID_CATEGORIA = C.ID_CATEGORIA
        INNER JOIN [PH2].[EINVENTARIO].[SUB_CATEGORIA] AS SC
            ON P.ID_SUBCATEGORIA = SC.ID_SUBCATEGORIA
        WHERE @FILTRO IS NULL
           OR P.DESCRIPCION LIKE N'%' + @FILTRO + N'%'
        ORDER BY P.DESCRIPCION;
    END
END
GO
