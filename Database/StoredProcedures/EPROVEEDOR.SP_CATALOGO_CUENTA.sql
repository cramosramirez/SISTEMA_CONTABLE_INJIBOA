USE [PH2];
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

-- ----------------------------------------------------------------
-- SP: CATALOGO_CUENTA (Plan de Cuentas)
-- ----------------------------------------------------------------
CREATE OR ALTER PROCEDURE [EPROVEEDOR].[SP_CATALOGO_CUENTA]
    @ACCION        VARCHAR(10),
    @ID_CUENTA     INT           = NULL,
    @CUENTA        NVARCHAR(50)  = NULL,
    @NOMBRE_CUENTA NVARCHAR(100) = NULL,
    @ID_TIPO_CTA   INT           = NULL,
    @ID_CUENTA_PADRE INT         = NULL,
    @ANTERIOR      NVARCHAR(50)  = NULL,
    @ES_DETALLE    BIT           = NULL,
    @NIVEL         INT           = NULL,
    @USUARIO_CREA  NVARCHAR(100) = NULL,
    @USUARIO_ACT   NVARCHAR(100) = NULL,
    @FILTRO        NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- BUSCAR: búsqueda incremental — solo cuentas de detalle por defecto
    IF @ACCION = 'BUSCAR'
    BEGIN
        SELECT TOP 25
            CC.ID_CUENTA,
            CC.CUENTA,
            CC.NOMBRE_CUENTA,
            CC.NIVEL,
            CC.ES_DETALLE,
            TC.NOMBRE_TIPO AS TIPO_CUENTA
        FROM CATALOGO_CUENTA CC
        INNER JOIN TIPO_CUENTA TC ON TC.ID_TIPO_CTA = CC.ID_TIPO_CTA
        WHERE CC.ES_DETALLE = 1
          AND (@ID_TIPO_CTA IS NULL OR CC.ID_TIPO_CTA = @ID_TIPO_CTA)
          AND (@FILTRO IS NULL
               OR CC.CUENTA        LIKE '%' + @FILTRO + '%'
               OR CC.NOMBRE_CUENTA LIKE '%' + @FILTRO + '%')
        ORDER BY CC.CUENTA
    END

    -- BUSCAR_TODO: incluye cuentas de mayor (para armar el árbol)
    ELSE IF @ACCION = 'BUSCAR_TODO'
    BEGIN
        SELECT TOP 25
            CC.ID_CUENTA,
            CC.CUENTA,
            CC.NOMBRE_CUENTA,
            CC.NIVEL,
            CC.ES_DETALLE,
            CC.ID_CUENTA_PADRE,
            TC.NOMBRE_TIPO AS TIPO_CUENTA
        FROM CATALOGO_CUENTA CC
        INNER JOIN TIPO_CUENTA TC ON TC.ID_TIPO_CTA = CC.ID_TIPO_CTA
        WHERE (@ID_TIPO_CTA IS NULL OR CC.ID_TIPO_CTA = @ID_TIPO_CTA)
          AND (@FILTRO IS NULL
               OR CC.CUENTA        LIKE '%' + @FILTRO + '%'
               OR CC.NOMBRE_CUENTA LIKE '%' + @FILTRO + '%')
        ORDER BY CC.CUENTA
    END

    -- ARBOL: retorna el plan completo ordenado jerárquicamente con CTE recursivo
    ELSE IF @ACCION = 'ARBOL'
    BEGIN
        WITH CTE_CUENTAS AS
        (
            SELECT
                ID_CUENTA, CUENTA, NOMBRE_CUENTA, ID_TIPO_CTA,
                ID_CUENTA_PADRE, NIVEL, ES_DETALLE,
                CAST(CUENTA AS NVARCHAR(500)) AS RUTA
            FROM CATALOGO_CUENTA
            WHERE ID_CUENTA_PADRE IS NULL

            UNION ALL

            SELECT
                C.ID_CUENTA, C.CUENTA, C.NOMBRE_CUENTA, C.ID_TIPO_CTA,
                C.ID_CUENTA_PADRE, C.NIVEL, C.ES_DETALLE,
                CAST(CTE.RUTA + '|' + C.CUENTA AS NVARCHAR(500))
            FROM CATALOGO_CUENTA C
            INNER JOIN CTE_CUENTAS CTE ON CTE.ID_CUENTA = C.ID_CUENTA_PADRE
        )
        SELECT
            CTE.ID_CUENTA, CTE.CUENTA, CTE.NOMBRE_CUENTA,
            CTE.NIVEL, CTE.ES_DETALLE, CTE.ID_CUENTA_PADRE,
            TC.NOMBRE_TIPO AS TIPO_CUENTA, CTE.RUTA
        FROM CTE_CUENTAS CTE
        INNER JOIN TIPO_CUENTA TC ON TC.ID_TIPO_CTA = CTE.ID_TIPO_CTA
        ORDER BY CTE.RUTA
    END

    -- OBTENER: un registro para edición
    ELSE IF @ACCION = 'OBTENER'
    BEGIN
        SELECT
            CC.ID_CUENTA, CC.CUENTA, CC.NOMBRE_CUENTA,
            CC.ID_TIPO_CTA, CC.ID_CUENTA_PADRE, CC.ANTERIOR,
            CC.ES_DETALLE, CC.NIVEL,
            TC.NOMBRE_TIPO AS TIPO_CUENTA,
            CP.CUENTA AS CUENTA_PADRE_COD,
            CP.NOMBRE_CUENTA AS CUENTA_PADRE_NOMBRE
        FROM CATALOGO_CUENTA CC
        INNER JOIN TIPO_CUENTA TC ON TC.ID_TIPO_CTA = CC.ID_TIPO_CTA
        LEFT JOIN CATALOGO_CUENTA CP ON CP.ID_CUENTA = CC.ID_CUENTA_PADRE
        WHERE (@ID_CUENTA IS NOT NULL AND CC.ID_CUENTA = @ID_CUENTA)
       OR (@CUENTA IS NOT NULL AND CC.CUENTA = @CUENTA)
    END

    -- GUARDAR
    ELSE IF @ACCION = 'GUARDAR'
    BEGIN
        DECLARE @NUEVO_ID_CUE INT
        IF @ID_CUENTA = 0
        BEGIN
            SELECT @NUEVO_ID_CUE = ISNULL(MAX(ID_CUENTA), 0) + 1 FROM CATALOGO_CUENTA
            INSERT INTO CATALOGO_CUENTA (
                ID_CUENTA, CUENTA, NOMBRE_CUENTA, ID_TIPO_CTA,
                ID_CUENTA_PADRE, ANTERIOR, ES_DETALLE, NIVEL,
                USUARIO_CREA, FECHA_CREA
            )
            VALUES (
                @NUEVO_ID_CUE, @CUENTA, @NOMBRE_CUENTA, @ID_TIPO_CTA,
                @ID_CUENTA_PADRE, @ANTERIOR, ISNULL(@ES_DETALLE, 1), ISNULL(@NIVEL, 1),
                @USUARIO_CREA, GETDATE()
            )
            SELECT @NUEVO_ID_CUE AS ID_GENERADO
        END
        ELSE
        BEGIN
            UPDATE CATALOGO_CUENTA SET
                CUENTA          = @CUENTA,
                NOMBRE_CUENTA   = @NOMBRE_CUENTA,
                ID_TIPO_CTA     = @ID_TIPO_CTA,
                ID_CUENTA_PADRE = @ID_CUENTA_PADRE,
                ANTERIOR        = @ANTERIOR,
                ES_DETALLE      = @ES_DETALLE,
                NIVEL           = @NIVEL,
                USUARIO_ACT = @USUARIO_ACT,
                FECHA_ACT       = GETDATE()
            WHERE ID_CUENTA = @ID_CUENTA
            SELECT @ID_CUENTA AS ID_GENERADO
        END
    END

    -- ELIMINAR: solo si no tiene hijos ni movimientos
    ELSE IF @ACCION = 'ELIMINAR'
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM CATALOGO_CUENTA WHERE ID_CUENTA_PADRE = @ID_CUENTA)
            DELETE FROM CATALOGO_CUENTA WHERE ID_CUENTA = @ID_CUENTA
        ELSE
            RAISERROR('No se puede eliminar una cuenta que tiene subcuentas asociadas.', 16, 1)
    END
END
GO
