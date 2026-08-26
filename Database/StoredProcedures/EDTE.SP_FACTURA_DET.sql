USE [PH2]
GO
/****** Object:  StoredProcedure [EDTE].[SP_FACTURA_DET]    Script Date: 25/8/2026 16:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ----------------------------------------------------------------
-- SP: SP_FACTURA_DET  (Detalle de Factura / DTE)
-- Acciones: OBTENER | LISTAR | GUARDAR | ELIMINAR | ELIMINAR_POR_FACTURA
-- ----------------------------------------------------------------
ALTER PROCEDURE [EDTE].[SP_FACTURA_DET]
    @ACCION             VARCHAR(50),
    @ID_FACTDET         INT             = NULL,
    @ID_FACTENC         INT             = NULL,
    @ID_EMISOR          INT             = NULL,
    @CODGENERACION      NVARCHAR(40)    = NULL,
    @ID_TIPO_DTE        INT             = NULL,
    @TPDOC              NVARCHAR(6)     = NULL,
    @FECHA              DATE            = NULL,
    @SALFEC             NVARCHAR(6)     = NULL,
    @NUMDOC             NVARCHAR(15)    = NULL,
    @ID_PRODUCTO        INT             = NULL,
    @COD_REF            NVARCHAR(15)    = NULL,
    @DESCRIPCION        NVARCHAR(1500)  = NULL,
    @CANTIDAD           NUMERIC(20,2)   = 0,
    @ID_UNIDAD_MEDIDA   INT             = NULL,
    @UNIDAD_MEDIDA      NVARCHAR(100)   = NULL,
    @PRECIO             NUMERIC(20,6)   = 0,
    @DESCUENTO          INT             = 0,
    @DESCUENTO_VALOR    NUMERIC(20,2)   = 0,
    @ES_EXENTO          BIT             = 0,
    @EXENTA             NUMERIC(20,2)   = 0,
    @GRAVADA            NUMERIC(20,2)   = 0,
    @TOTAL              NUMERIC(20,2)   = 0,
    @SELLOAPLICADO      BIT             = 0,
    @USUARIO            NVARCHAR(50)    = NULL,
    -- ===== Historial de integración SIGESTA (solicitud agrícola importada) - 2026-08-25 =====
    @ID_SOLICITUD          INT           = NULL,
    @ID_SOLIC_AGRI_PROD    INT           = NULL,
    @UID_SOLIC_AGRI_PROD   NVARCHAR(40)  = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NUEVO_ID INT;
    DECLARE @NUMINTERNO NVARCHAR(40)
    SET @NUMINTERNO = (
        SELECT TOP 1 EE.NUMINTERNO FROM [EDTE].FACTURA_ENC EE
        WHERE EE.ID_FACTENC = @ID_FACTENC AND EE.ID_EMISOR = @ID_EMISOR
    )
    SET @NUMDOC = @NUMINTERNO
    -- ===================== OBTENER (por ID de línea) =====================
    IF @ACCION = 'OBTENER'
    BEGIN
        SELECT D.ID_FACTDET, D.ID_FACTENC, D.ID_EMISOR, D.CODGENERACION, D.ID_TIPO_DTE, D.TPDOC,
            D.FECHA, D.SALFEC, D.NUMDOC, D.ID_PRODUCTO, D.COD_REF, D.DESCRIPCION, D.CANTIDAD,
            D.ID_UNIDAD_MEDIDA, D.UNIDAD_MEDIDA, D.PRECIO, D.DESCUENTO, D.DESCUENTO_VALOR,
            D.ES_EXENTO, D.EXENTA, D.GRAVADA, D.TOTAL, D.SELLOAPLICADO,
            D.FECHA_CREA, D.USER_CREA, D.FECHA_ACT, D.USER_ACT,
            D.ID_SOLICITUD, D.ID_SOLIC_AGRI_PROD, D.UID_SOLIC_AGRI_PROD
        FROM EDTE.FACTURA_DET D
        WHERE D.ID_FACTENC = @ID_FACTENC AND D.ID_EMISOR = @ID_EMISOR
        ORDER BY D.ID_FACTDET
    END
    -- ===================== LISTAR (detalles de una factura) =====================
    ELSE IF @ACCION = 'LISTAR'
    BEGIN
        SELECT D.ID_FACTDET, D.ID_FACTENC, D.ID_EMISOR, D.CODGENERACION, D.ID_TIPO_DTE, D.TPDOC,
            D.FECHA, D.NUMDOC, D.ID_PRODUCTO, D.COD_REF, D.DESCRIPCION, D.CANTIDAD,
            D.ID_UNIDAD_MEDIDA, D.UNIDAD_MEDIDA, D.PRECIO, D.DESCUENTO, D.DESCUENTO_VALOR,
            D.ES_EXENTO, D.EXENTA, D.GRAVADA, D.TOTAL, D.SELLOAPLICADO,
            D.ID_SOLICITUD, D.ID_SOLIC_AGRI_PROD, D.UID_SOLIC_AGRI_PROD
        FROM EDTE.FACTURA_DET D
        WHERE D.ID_FACTENC = @ID_FACTENC AND D.ID_EMISOR = @ID_EMISOR
        ORDER BY D.ID_FACTDET
    END
    -- ===================== GUARDAR (UPSERT) =====================
    ELSE IF @ACCION = 'GUARDAR'
    BEGIN
        BEGIN TRY
            IF @ID_FACTENC IS NULL OR @ID_PRODUCTO IS NULL
                THROW 50003, 'ID_FACTENC y ID_PRODUCTO son obligatorios.', 1;
            IF ISNULL(@CANTIDAD, 0) <= 0
                THROW 50004, 'La cantidad debe ser mayor a 0.', 1;
            IF ISNULL(@PRECIO, 0) < 0
                THROW 50005, 'El precio no puede ser negativo.', 1;
            IF EXISTS (
                SELECT 1 FROM EDTE.FACTURA_DET
                WHERE ID_FACTENC = @ID_FACTENC AND ID_PRODUCTO = @ID_PRODUCTO AND ID_EMISOR = @ID_EMISOR
            )
            BEGIN
                -- UPDATE
                UPDATE EDTE.FACTURA_DET SET
                    CODGENERACION = @CODGENERACION, TPDOC = @TPDOC, FECHA = @FECHA, NUMDOC = @NUMDOC,
                    COD_REF = @COD_REF, DESCRIPCION = @DESCRIPCION, CANTIDAD = ISNULL(@CANTIDAD, 0),
                    ID_UNIDAD_MEDIDA = @ID_UNIDAD_MEDIDA, UNIDAD_MEDIDA = @UNIDAD_MEDIDA, PRECIO = ISNULL(@PRECIO, 0),
                    DESCUENTO = ISNULL(@DESCUENTO, 0), DESCUENTO_VALOR = ISNULL(@DESCUENTO_VALOR, 0),
                    ES_EXENTO = ISNULL(@ES_EXENTO, 0), EXENTA = ISNULL(@EXENTA, 0), GRAVADA = ISNULL(@GRAVADA, 0),
                    TOTAL = ISNULL(@TOTAL, 0), FECHA_ACT = GETDATE(), USER_ACT = @USUARIO, ID_TIPO_DTE = @ID_TIPO_DTE,
                    ID_SOLICITUD = @ID_SOLICITUD, ID_SOLIC_AGRI_PROD = @ID_SOLIC_AGRI_PROD,
                    UID_SOLIC_AGRI_PROD = @UID_SOLIC_AGRI_PROD
                WHERE ID_FACTENC = @ID_FACTENC AND ID_PRODUCTO = @ID_PRODUCTO AND ID_EMISOR = @ID_EMISOR;
            END
            ELSE
            BEGIN
                -- INSERT
                SELECT @NUEVO_ID = ISNULL(MAX(ID_FACTDET), 0) + 1 FROM EDTE.FACTURA_DET WHERE ID_FACTENC = @ID_FACTENC;
                INSERT INTO EDTE.FACTURA_DET (
                    ID_FACTDET, ID_FACTENC, ID_EMISOR, CODGENERACION, TPDOC, FECHA, SALFEC, NUMDOC,
                    ID_PRODUCTO, COD_REF, DESCRIPCION, CANTIDAD, ID_UNIDAD_MEDIDA, UNIDAD_MEDIDA,
                    PRECIO, DESCUENTO, DESCUENTO_VALOR, ES_EXENTO, EXENTA, GRAVADA, TOTAL,
                    SELLOAPLICADO, FECHA_CREA, USER_CREA, FECHA_ACT, USER_ACT, ID_TIPO_DTE,
                    ID_SOLICITUD, ID_SOLIC_AGRI_PROD, UID_SOLIC_AGRI_PROD
                )
                VALUES (
                    @NUEVO_ID, @ID_FACTENC, @ID_EMISOR, @CODGENERACION, @TPDOC, @FECHA, @SALFEC, @NUMDOC,
                    @ID_PRODUCTO, @COD_REF, @DESCRIPCION, ISNULL(@CANTIDAD, 0), @ID_UNIDAD_MEDIDA, @UNIDAD_MEDIDA,
                    ISNULL(@PRECIO, 0), ISNULL(@DESCUENTO, 0), ISNULL(@DESCUENTO_VALOR, 0),
                    ISNULL(@ES_EXENTO, 0), ISNULL(@EXENTA, 0), ISNULL(@GRAVADA, 0), ISNULL(@TOTAL, 0), ISNULL(@SELLOAPLICADO, 0),
                    GETDATE(), @USUARIO, GETDATE(), @USUARIO, @ID_TIPO_DTE,
                    @ID_SOLICITUD, @ID_SOLIC_AGRI_PROD, @UID_SOLIC_AGRI_PROD
                );
            END
            RETURN;
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0 ROLLBACK TRAN;
            THROW;
        END CATCH
    END
    -- ===================== ELIMINAR (una línea) =====================
    ELSE IF @ACCION = 'ELIMINAR'
    BEGIN
        IF @ID_FACTDET IS NOT NULL AND @ID_FACTDET > 0
        BEGIN
            DELETE FROM EDTE.FACTURA_DET WHERE ID_FACTDET = @ID_FACTDET;
        END
        ELSE IF @ID_FACTENC IS NOT NULL AND @ID_PRODUCTO IS NOT NULL
        BEGIN
            DELETE FROM EDTE.FACTURA_DET WHERE ID_FACTENC = @ID_FACTENC AND ID_PRODUCTO = @ID_PRODUCTO AND ID_EMISOR = @ID_EMISOR;
        END
        ELSE
        BEGIN
            THROW 50006, 'Debe enviar ID_FACTDET o bien ID_FACTENC + ID_PRODUCTO.', 1;
        END
    END
    -- ===================== ELIMINAR_POR_FACTURA (todos los detalles) =====================
    ELSE IF @ACCION = 'ELIMINAR_POR_FACTURA'
    BEGIN
        BEGIN TRY
            BEGIN TRAN
            DELETE FROM EDTE.FACTURA_DET WHERE ID_FACTENC = @ID_FACTENC AND ID_EMISOR = @ID_EMISOR;
            COMMIT TRAN
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0 ROLLBACK TRAN;
            THROW;
        END CATCH
    END
END
