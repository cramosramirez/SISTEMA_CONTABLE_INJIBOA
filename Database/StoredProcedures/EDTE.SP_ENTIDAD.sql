USE [PH2]
GO
/****** Object:  StoredProcedure [EDTE].[SP_ENTIDAD]    Script Date: 20/8/2026 11:27:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER  PROCEDURE [EDTE].[SP_ENTIDAD]
    @ACCION             VARCHAR(100),
    @ID_ENTIDAD         INT             = NULL,
    @CODIGO_ENTIDAD     NVARCHAR(20)    = NULL,
    @ID_TIPO_ENTIDAD    INT             = NULL,
    @ID_TIPO_CONTRIB    INT             = NULL,
    @ID_TIPO_DOC_INDEN  INT             = NULL,
    @DOCUMENTO          NVARCHAR(20)    = NULL,
    @NRC                NVARCHAR(20)    = NULL,
    @DUI                NVARCHAR(20)    = NULL,
    @NIT                NVARCHAR(20)    = NULL,
    @NOMBRE             NVARCHAR(300)   = NULL,
    @NOMBRE_COMERCIAL   NVARCHAR(300)   = NULL,
    @COMPLEMENTO        NVARCHAR(400)   = NULL,
    @CALLE              NVARCHAR(400)   = NULL,
    @CASA               NVARCHAR(50)    = NULL,
    @APTO_LOCAL         NVARCHAR(400)   = NULL,
    @COLONIA            NVARCHAR(400)   = NULL,
    @CORREO             NVARCHAR(100)   = NULL,
    @CORREO_CC          NVARCHAR(100)   = NULL,
    @CELULAR            NVARCHAR(50)    = NULL,
    @TELEFONO           NVARCHAR(100)   = NULL,
    @ENCARGADO          NVARCHAR(200)   = NULL,
    @DIAS_PLAZO         INT             = NULL,
    @ID_PAIS            INT             = NULL,
    @CODI_DEPTO         NVARCHAR(2)     = NULL,
    @CODI_MUNI          NVARCHAR(2)     = NULL,
    @ID_ACTIVIDAD_1     INT             = NULL,
    @ID_ACTIVIDAD_2     INT             = NULL,
    @ID_ACTIVIDAD_3     INT             = NULL,
    @CUENTA_X_PAGAR     NVARCHAR(100)   = NULL,
    @ID_ORIGEN          INT             = NULL,
    @CODIPROVEEDOR      VARCHAR(50)     = NULL,
    @CODTRANSPORT       INT             = NULL,
    @ID_CARGADORA       INT             = NULL,
    @ID_PROVEEDOR_ROZA  INT             = NULL,
    @ID_PROVEE_QQ       INT             = NULL,
    @RETENER_RENTA      BIT             = NULL,
    @PORC_RENTA         NUMERIC(20, 5)  = NULL,
    @USUARIO_CREA       NVARCHAR(100)   = NULL,
    @USUARIO_ACT        NVARCHAR(100)   = NULL,
	@OTROS_DATOS		NVARCHAR(400)   = NULL,
    @ROL                CHAR(10)        = NULL,
    @FILTRO             NVARCHAR(100)   = NULL,
	@CUENTA_GASTO		NVARCHAR(100)   = NULL,
	@ACTIVIDAD_EXT		NVARCHAR(200)   = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- ============================================================
    -- BUSCAR
    -- ============================================================
    IF @ACCION = 'BUSCAR'
    BEGIN
        SELECT TOP 25
            E.ID_ENTIDAD, E.CODIGO_ENTIDAD, E.CODIPROVEEDOR, E.NRC, E.NIT, E.DUI,
            E.NOMBRE, E.TELEFONO, E.CELULAR, E.CORREO,
            E.ID_TIPO_CONTRIB, E.COMPLEMENTO,
            TP.NOMBRE AS TIPO_ENTIDAD,
            TC.NOMBRE AS TIPO_CONTRIBUYENTE,
            E.ID_TIPO_ENTIDAD,
            (SELECT T.CODI_MH + ' - ' + T.VALORES FROM ACTIVIDAD_ECONOMICA T WHERE T.ID_ACTIVIDAD = E.ID_ACTIVIDAD_1) AS ACTIVIDAD_PRIMARIA
        FROM ENTIDAD E
        INNER JOIN TIPO_PERSONA TP ON TP.ID_TIPO_PERSONA = E.ID_TIPO_ENTIDAD
        INNER JOIN TIPO_CONTRIBUYENTE TC ON TC.ID_TIPO_CONTRIB = E.ID_TIPO_CONTRIB
        LEFT JOIN ENTIDAD_ROL ER ON ER.ID_ENTIDAD = E.ID_ENTIDAD
                                AND ER.ACTIVO = 1
                                AND (@ROL IS NULL OR ER.ROL = @ROL)
        WHERE (@ROL IS NULL OR ER.ID_ENTIDAD IS NOT NULL)
          AND (@FILTRO IS NULL
               OR E.NRC            LIKE '%' + @FILTRO + '%'
               OR E.NIT            LIKE '%' + @FILTRO + '%'
               OR E.DUI            LIKE '%' + @FILTRO + '%'
               OR E.NOMBRE         LIKE '%' + @FILTRO + '%'
			   OR E.DOCUMENTO      LIKE '%' + @FILTRO + '%'
               OR E.CODIGO_ENTIDAD LIKE '%' + @FILTRO + '%')
        ORDER BY E.NOMBRE
    END

    IF @ACCION = 'BUSCAR_TC'
    BEGIN
    declare @TIPO_CLIENTE nvarchar(50) 
    SET @TIPO_CLIENTE=@ROL

        SELECT TOP 25
            E.ID_ENTIDAD, E.CODIGO_ENTIDAD, E.NRC, E.NIT, E.DUI,
            E.NOMBRE, E.TELEFONO, E.CELULAR, E.CORREO,
            E.ID_TIPO_CONTRIB, E.COMPLEMENTO,
            TP.NOMBRE AS TIPO_ENTIDAD,
            TC.NOMBRE AS TIPO_CONTRIBUYENTE,
            E.ID_TIPO_ENTIDAD,
            (SELECT T.CODI_MH + ' - ' + T.VALORES FROM ACTIVIDAD_ECONOMICA T WHERE T.ID_ACTIVIDAD = E.ID_ACTIVIDAD_1) AS ACTIVIDAD_PRIMARIA
        FROM ENTIDAD E
        INNER JOIN TIPO_PERSONA TP ON TP.ID_TIPO_PERSONA = E.ID_TIPO_ENTIDAD
        INNER JOIN TIPO_CONTRIBUYENTE TC ON TC.ID_TIPO_CONTRIB = E.ID_TIPO_CONTRIB
        LEFT JOIN [dbo].[ENTIDAD_TIPO_CLIENTE] ETC ON ETC.ID_ENTIDAD = E.ID_ENTIDAD
                                AND ETC.ACTIVO = 1
                                AND (@ROL IS NULL OR ETC.ABREV = @ROL)
        WHERE (@ROL IS NULL OR ETC.ID_ENTIDAD IS NOT NULL)
          AND (@FILTRO IS NULL
               OR E.NRC            LIKE '%' + @FILTRO + '%'
               OR E.NIT            LIKE '%' + @FILTRO + '%'
               OR E.DUI            LIKE '%' + @FILTRO + '%'
               OR E.NOMBRE         LIKE '%' + @FILTRO + '%'
			   OR E.DOCUMENTO      LIKE '%' + @FILTRO + '%'
               OR E.CODIGO_ENTIDAD LIKE '%' + @FILTRO + '%')
        ORDER BY E.NOMBRE
    END

IF @ACCION = 'BUSCAR_OD'
    BEGIN
    
    SET @TIPO_CLIENTE=@ROL

        SELECT TOP 25
            E.ID_ENTIDAD, E.CODIGO_ENTIDAD, E.NRC, E.NIT, E.DUI,
            E.NOMBRE, E.TELEFONO, E.CELULAR, E.CORREO,
            E.ID_TIPO_CONTRIB, E.COMPLEMENTO,
            TP.NOMBRE AS TIPO_ENTIDAD,
            TC.NOMBRE AS TIPO_CONTRIBUYENTE,
            E.ID_TIPO_ENTIDAD,
            (SELECT T.CODI_MH + ' - ' + T.VALORES FROM ACTIVIDAD_ECONOMICA T WHERE T.ID_ACTIVIDAD = E.ID_ACTIVIDAD_1) AS ACTIVIDAD_PRIMARIA
        FROM ENTIDAD E
        INNER JOIN TIPO_PERSONA TP ON TP.ID_TIPO_PERSONA = E.ID_TIPO_ENTIDAD
        INNER JOIN TIPO_CONTRIBUYENTE TC ON TC.ID_TIPO_CONTRIB = E.ID_TIPO_CONTRIB
        LEFT JOIN [dbo].[ENTIDAD_TIPO_CLIENTE] ETC ON ETC.ID_ENTIDAD = E.ID_ENTIDAD
                                AND ETC.ACTIVO = 1
                                AND  ETC.ABREV IN ('NR','NR_BD')
        WHERE (@ROL IS NULL OR ETC.ID_ENTIDAD IS NOT NULL)
          AND (@FILTRO IS NULL
               OR E.NRC            LIKE '%' + @FILTRO + '%'
               OR E.NIT            LIKE '%' + @FILTRO + '%'
               OR E.DUI            LIKE '%' + @FILTRO + '%'
               OR E.NOMBRE         LIKE '%' + @FILTRO + '%'
			   OR E.DOCUMENTO      LIKE '%' + @FILTRO + '%'
               OR E.CODIGO_ENTIDAD LIKE '%' + @FILTRO + '%')
        ORDER BY E.NOMBRE
    END

    --IF @ACCION = 'BUSCAR'
    --BEGIN
    --    SELECT TOP 25
    --        E.ID_ENTIDAD, E.CODIGO_ENTIDAD, E.NRC, E.NIT, E.DUI,
    --        E.NOMBRE, E.TELEFONO, E.CELULAR, E.CORREO,
    --        E.ID_TIPO_CONTRIB, E.COMPLEMENTO,
    --        TP.NOMBRE AS TIPO_ENTIDAD,
    --        TC.NOMBRE AS TIPO_CONTRIBUYENTE,
    --        E.ID_TIPO_ENTIDAD,
    --        (SELECT T.CODI_MH + ' - ' + T.VALORES FROM ACTIVIDAD_ECONOMICA T WHERE T.ID_ACTIVIDAD = E.ID_ACTIVIDAD_1) AS ACTIVIDAD_PRIMARIA
    --    FROM ENTIDAD E
    --    INNER JOIN TIPO_PERSONA TP ON TP.ID_TIPO_PERSONA = E.ID_TIPO_ENTIDAD
    --    INNER JOIN TIPO_CONTRIBUYENTE TC ON TC.ID_TIPO_CONTRIB = E.ID_TIPO_CONTRIB
    --    LEFT JOIN ENTIDAD_ROL ER ON ER.ID_ENTIDAD = E.ID_ENTIDAD
    --                            AND ER.ACTIVO = 1
    --                            AND (@ROL IS NULL OR ER.ROL = @ROL)
    --    WHERE (@ROL IS NULL OR ER.ID_ENTIDAD IS NOT NULL)
    --      AND (@FILTRO IS NULL
    --           OR E.NRC            LIKE '%' + @FILTRO + '%'
    --           OR E.NIT            LIKE '%' + @FILTRO + '%'
    --           OR E.DUI            LIKE '%' + @FILTRO + '%'
    --           OR E.NOMBRE         LIKE '%' + @FILTRO + '%'
			 --  OR E.DOCUMENTO      LIKE '%' + @FILTRO + '%'
    --           OR E.CODIGO_ENTIDAD LIKE '%' + @FILTRO + '%')
    --    ORDER BY E.NOMBRE
    --END


	-- ================================================================================================================
    -- BUSCAR_DUPLICADOS Se utilizará para validar documentos desde las diferentes pantallas de creación de entidades
    -- ================================================================================================================
    IF @ACCION = 'BUSCAR_DUPLICADOS'
    BEGIN
        SELECT 
            E.ID_ENTIDAD, E.CODIGO_ENTIDAD, E.NRC, E.NIT, E.DUI, E.DOCUMENTO,
            E.NOMBRE, E.TELEFONO, E.CELULAR, E.CORREO,
            E.ID_TIPO_CONTRIB, E.COMPLEMENTO,
            TP.NOMBRE AS TIPO_ENTIDAD,
            TC.NOMBRE AS TIPO_CONTRIBUYENTE,
            E.ID_TIPO_ENTIDAD,
            (SELECT T.CODI_MH + ' - ' + T.VALORES FROM ACTIVIDAD_ECONOMICA T WHERE T.ID_ACTIVIDAD = E.ID_ACTIVIDAD_1) AS ACTIVIDAD_PRIMARIA
        FROM ENTIDAD E
        INNER JOIN TIPO_PERSONA TP ON TP.ID_TIPO_PERSONA = E.ID_TIPO_ENTIDAD
        INNER JOIN TIPO_CONTRIBUYENTE TC ON TC.ID_TIPO_CONTRIB = E.ID_TIPO_CONTRIB       
        WHERE EXISTS(SELECT 1 FROM ENTIDAD_ROL R WHERE R.ID_ENTIDAD = E.ID_ENTIDAD AND R.ROL = @ROL)
          AND (@FILTRO IS NULL
               OR E.NRC						       LIKE '%' + @FILTRO + '%'
               OR REPLACE(E.NIT,'-','')			   LIKE '%' + REPLACE(@FILTRO,'-','') + '%'
               OR REPLACE(E.DUI,'-','')			   LIKE '%' + REPLACE(@FILTRO,'-','') + '%'               
			   OR REPLACE(E.DOCUMENTO,'-','')      LIKE '%' + @FILTRO + '%')
        ORDER BY E.NOMBRE
    END

    -- ============================================================
    -- CONSULTA_TODOS_PROVEEDORES (mantenido tal cual)
    -- ============================================================
    IF @ACCION = 'CONSULTA_TODOS_PROVEEDORES'
    BEGIN
        SELECT
            E.ID_ENTIDAD, E.CODIGO_ENTIDAD, E.NRC, E.NIT, E.DUI,
            E.NOMBRE, E.TELEFONO, E.CELULAR, E.CORREO,
            E.ID_TIPO_CONTRIB, E.COMPLEMENTO,
            TP.NOMBRE AS TIPO_ENTIDAD,
            TC.NOMBRE AS TIPO_CONTRIBUYENTE,
            E.ID_TIPO_ENTIDAD,
            (SELECT T.CODI_MH + ' - ' + T.VALORES FROM ACTIVIDAD_ECONOMICA T WHERE T.ID_ACTIVIDAD = E.ID_ACTIVIDAD_1) AS ACTIVIDAD_PRIMARIA
        FROM ENTIDAD E
        INNER JOIN TIPO_PERSONA TP ON TP.ID_TIPO_PERSONA = E.ID_TIPO_ENTIDAD
        INNER JOIN TIPO_CONTRIBUYENTE TC ON TC.ID_TIPO_CONTRIB = E.ID_TIPO_CONTRIB
        WHERE EXISTS (
            SELECT 1 FROM ENTIDAD_ROL ER
            WHERE ER.ID_ENTIDAD = E.ID_ENTIDAD
              AND ER.ACTIVO = 1
              AND (@ROL IS NULL OR ER.ROL = @ROL)
        )
        ORDER BY E.NOMBRE
    END

    -- ============================================================
    -- BUSCAR_NO_CONTRIBUYENTES (mantenido tal cual)
    -- ============================================================
IF @ACCION = 'BUSCAR_NO_CONTRIBUYENTES'
    BEGIN
        SELECT TOP 25
            E.ID_ENTIDAD, E.CODIGO_ENTIDAD, E.NRC, E.NIT, E.DUI,
            E.NOMBRE, E.TELEFONO, E.CELULAR, E.CORREO,
            E.ID_TIPO_CONTRIB, E.COMPLEMENTO,
            TP.ID_TIPO_PERSONA AS ID_TIPO_ENTIDAD,
            TP.NOMBRE AS TIPO_ENTIDAD,
            TC.NOMBRE AS TIPO_CONTRIBUYENTE,
            TC.ID_TIPO_CONTRIB,
            E.ID_TIPO_ENTIDAD,
            (SELECT T.CODI_MH + ' - ' + T.VALORES FROM ACTIVIDAD_ECONOMICA T WHERE T.ID_ACTIVIDAD = E.ID_ACTIVIDAD_1) AS ACTIVIDAD_PRIMARIA,
            TP.ID_TIPO_PERSONA
        FROM ENTIDAD E
        INNER JOIN TIPO_PERSONA TP ON TP.ID_TIPO_PERSONA = E.ID_TIPO_ENTIDAD
        INNER JOIN TIPO_CONTRIBUYENTE TC ON TC.ID_TIPO_CONTRIB = E.ID_TIPO_CONTRIB
        LEFT JOIN ENTIDAD_ROL ER ON ER.ID_ENTIDAD = E.ID_ENTIDAD
                                AND ER.ACTIVO = 1
                                AND (@ROL IS NULL OR ER.ROL = @ROL)
        WHERE E.ID_TIPO_CONTRIB = 0
          AND (@ROL IS NULL OR ER.ID_ENTIDAD IS NOT NULL)
          AND (@FILTRO IS NULL
               OR E.NRC            LIKE '%' + @FILTRO + '%'
               OR E.NIT            LIKE '%' + @FILTRO + '%'
               OR E.DUI            LIKE '%' + @FILTRO + '%'
               OR E.NOMBRE         LIKE '%' + @FILTRO + '%'
               OR E.DOCUMENTO      LIKE '%' + @FILTRO + '%'
               OR E.CODIGO_ENTIDAD LIKE '%' + @FILTRO + '%')
        ORDER BY E.NOMBRE
    END

    -- ============================================================
    -- BUSCAR_CONTRIBUYENTES (mantenido tal cual)
    -- ============================================================
   IF @ACCION = 'BUSCAR_CONTRIBUYENTES'
    BEGIN
        SELECT TOP 25
            E.ID_ENTIDAD, E.CODIGO_ENTIDAD, E.NRC, E.NIT, E.DUI,
            E.NOMBRE, E.TELEFONO, E.CELULAR, E.CORREO,
            E.ID_TIPO_CONTRIB, E.COMPLEMENTO,
            TP.ID_TIPO_PERSONA AS ID_TIPO_ENTIDAD,
            TP.NOMBRE AS TIPO_ENTIDAD,
            TC.NOMBRE AS TIPO_CONTRIBUYENTE,
            TC.ID_TIPO_CONTRIB,
            E.ID_TIPO_ENTIDAD,
            (SELECT T.CODI_MH + ' - ' + T.VALORES FROM ACTIVIDAD_ECONOMICA T WHERE T.ID_ACTIVIDAD = E.ID_ACTIVIDAD_1) AS ACTIVIDAD_PRIMARIA,
            TP.ID_TIPO_PERSONA
        FROM ENTIDAD E
        INNER JOIN TIPO_PERSONA TP ON TP.ID_TIPO_PERSONA = E.ID_TIPO_ENTIDAD
        INNER JOIN TIPO_CONTRIBUYENTE TC ON TC.ID_TIPO_CONTRIB = E.ID_TIPO_CONTRIB
        --LEFT JOIN ENTIDAD_ROL ER ON ER.ID_ENTIDAD = E.ID_ENTIDAD
        --                        AND ER.ACTIVO = 1
        --                        AND (@ROL IS NULL OR ER.ROL = @ROL)
        WHERE E.ID_TIPO_CONTRIB <> 0
          --AND (@ROL IS NULL OR ER.ID_ENTIDAD IS NOT NULL)
          AND (@FILTRO IS NULL
               OR E.NRC            LIKE '%' + @FILTRO + '%'
               OR E.NIT            LIKE '%' + @FILTRO + '%'
               OR E.DUI            LIKE '%' + @FILTRO + '%'
               OR E.NOMBRE         LIKE '%' + @FILTRO + '%'
               OR E.DOCUMENTO      LIKE '%' + @FILTRO + '%'
               OR E.CODIGO_ENTIDAD LIKE '%' + @FILTRO + '%')
        ORDER BY E.NOMBRE
    END

    -- ============================================================
    -- BUSCAR_POR_CODIGO (mantenido tal cual)
    -- ============================================================
    IF @ACCION = 'BUSCAR_POR_CODIGO'
    BEGIN
        SELECT TOP 25
            E.ID_ENTIDAD, E.CODIGO_ENTIDAD, E.NRC, E.NIT, E.DUI,
            E.NOMBRE, E.TELEFONO, E.CELULAR, E.CORREO,
            E.ID_TIPO_CONTRIB,
            TP.NOMBRE AS TIPO_ENTIDAD,
            TC.NOMBRE AS TIPO_CONTRIBUYENTE,
            E.COMPLEMENTO, E.ID_TIPO_ENTIDAD,
            (SELECT T.CODI_MH + ' - ' + T.VALORES FROM ACTIVIDAD_ECONOMICA T WHERE T.ID_ACTIVIDAD = E.ID_ACTIVIDAD_1) AS ACTIVIDAD_PRIMARIA,
            E.CUENTA_X_PAGAR,
			E.CUENTA_GASTO
        FROM ENTIDAD E
        INNER JOIN TIPO_PERSONA TP ON TP.ID_TIPO_PERSONA = E.ID_TIPO_ENTIDAD
        INNER JOIN TIPO_CONTRIBUYENTE TC ON TC.ID_TIPO_CONTRIB = E.ID_TIPO_CONTRIB
        LEFT JOIN ENTIDAD_ROL ER ON ER.ID_ENTIDAD = E.ID_ENTIDAD
                                AND ER.ACTIVO = 1
                                AND (@ROL IS NULL OR ER.ROL = @ROL)
        WHERE (@ROL IS NULL OR ER.ID_ENTIDAD IS NOT NULL)
          AND (E.CODIGO_ENTIDAD = @FILTRO)
        ORDER BY E.NOMBRE
    END

    -- ============================================================
    -- OBTENER (ampliado: NOMBRE_COMERCIAL, ID_ORIGEN, CODIPROVEEDOR,
    --          CODTRANSPORT, ID_CARGADORA, RETENER_RENTA, PORC_RENTA,
    --          NOMBRE_DISTRITO)
    -- ============================================================
    ELSE IF @ACCION = 'OBTENER'
    BEGIN
        SELECT
            E.ID_ENTIDAD, E.CODIGO_ENTIDAD, E.ID_TIPO_ENTIDAD,
            E.ID_TIPO_CONTRIB, E.ID_TIPO_DOC_INDEN, E.DOCUMENTO,
            E.NRC, E.DUI, E.NIT, E.NOMBRE, E.NOMBRE_COMERCIAL, E.COMPLEMENTO,
            E.CALLE, E.CASA, E.APTO_LOCAL, E.COLONIA,
            E.CORREO, E.CORREO_CC, E.CELULAR, E.TELEFONO,
            E.ENCARGADO, E.DIAS_PLAZO, E.ID_PAIS,
            E.CODI_DEPTO, E.CODI_MUNI,
            E.ID_ACTIVIDAD_1, E.ID_ACTIVIDAD_2, E.ID_ACTIVIDAD_3,
            E.CUENTA_X_PAGAR,
            E.ID_ORIGEN, E.CODIPROVEEDOR, E.CODTRANSPORT,
            E.ID_CARGADORA, E.ID_PROVEEDOR_ROZA, E.ID_PROVEE_QQ,
            E.RETENER_RENTA, E.PORC_RENTA,
            TP.NOMBRE AS NOMBRE_TIPO_ENTIDAD,
            TC.NOMBRE AS NOMBRE_TIPO_CONTRIB,
            M.NOMBRE_MUNICIPIO,
            M.NOMBRE_DISTRITO,
            D.VALORES AS NOMBRE_DEPTO,
			E.OTROS_DATOS,
			E.CUENTA_GASTO,
			E.ACTIVIDAD_EXT,
			(SELECT T.NOMBRE_CUENTA FROM CATALOGO_CUENTA T WHERE T.CUENTA = E.CUENTA_X_PAGAR) AS NOMBRE_CUENTA_X_PAGAR,
			(SELECT T.NOMBRE_CUENTA FROM CATALOGO_CUENTA T WHERE T.CUENTA = E.CUENTA_GASTO) AS NOMBRE_CUENTA_GASTO
        FROM ENTIDAD E
        INNER JOIN TIPO_PERSONA TP ON TP.ID_TIPO_PERSONA = E.ID_TIPO_ENTIDAD
        INNER JOIN TIPO_CONTRIBUYENTE TC ON TC.ID_TIPO_CONTRIB = E.ID_TIPO_CONTRIB
        LEFT JOIN MUNICIPIO M ON M.CODI_DEPTO = E.CODI_DEPTO AND M.CODI_MUNI = E.CODI_MUNI
        LEFT JOIN DEPARTAMENTO D ON D.CODI_DEPTO = E.CODI_DEPTO
        WHERE E.ID_ENTIDAD = @ID_ENTIDAD

        SELECT ID_ENTIDAD_ROL, ROL, ACTIVO, FECHA_ASIGNACION
        FROM ENTIDAD_ROL
        WHERE ID_ENTIDAD = @ID_ENTIDAD
    END

    -- ============================================================
    -- OBTENER_POR_NRC (sin cambios)
    -- ============================================================
    ELSE IF @ACCION = 'OBTENER_POR_NRC'
    BEGIN
        SELECT TOP 1
            E.ID_ENTIDAD, E.CODIGO_ENTIDAD, E.NRC, E.NIT, E.DUI,
            E.NOMBRE, E.TELEFONO, E.CORREO, E.CUENTA_X_PAGAR, E.CUENTA_GASTO,
            TC.NOMBRE AS TIPO_CONTRIBUYENTE, E.ID_TIPO_ENTIDAD
        FROM ENTIDAD E
        INNER JOIN TIPO_CONTRIBUYENTE TC ON TC.ID_TIPO_CONTRIB = E.ID_TIPO_CONTRIB
        WHERE E.NRC = @NRC
    END

    -- ============================================================
    -- OBTENER_INTEGRACION: códigos de Roza y Querqueo
    -- ============================================================
    ELSE IF @ACCION = 'OBTENER_INTEGRACION'
    BEGIN
        SELECT
            E.ID_PROVEEDOR_ROZA,
            E.ID_PROVEE_QQ
        FROM dbo.ENTIDAD AS E
        WHERE E.ID_ENTIDAD = @ID_ENTIDAD
    END

    -- ============================================================
    -- GUARDAR_INTEGRACION: actualiza únicamente Roza y Querqueo
    -- ============================================================
    ELSE IF @ACCION = 'GUARDAR_INTEGRACION'
    BEGIN
        UPDATE dbo.ENTIDAD
        SET ID_PROVEEDOR_ROZA = @ID_PROVEEDOR_ROZA,
            ID_PROVEE_QQ = @ID_PROVEE_QQ,
            USUARIO_ACT = @USUARIO_ACT,
            FECHA_ACT = GETDATE()
        WHERE ID_ENTIDAD = @ID_ENTIDAD

        IF @@ROWCOUNT = 0
        BEGIN
            THROW 50002, 'No se encontró la entidad para guardar Roza y Querqueo.', 1;
        END
    END

    -- ============================================================
    -- GUARDAR: INSERT o UPDATE + validaciones + manejo de rol
    -- ============================================================
    ELSE IF @ACCION = 'GUARDAR'
    BEGIN
        BEGIN TRY
            BEGIN TRAN

            -- ----------------------------------------------------
            -- Validación de duplicados
            -- ----------------------------------------------------
            DECLARE @MSG_DUP NVARCHAR(400)
            DECLARE @ID_COMPARA INT = ISNULL(@ID_ENTIDAD, 0)

            IF ISNULL(@CODIGO_ENTIDAD, '') <> '' AND EXISTS (
                SELECT 1 FROM ENTIDAD E
                WHERE CODIGO_ENTIDAD = @CODIGO_ENTIDAD
                  AND ID_ENTIDAD <> @ID_COMPARA
				  AND EXISTS(SELECT 1 FROM ENTIDAD_ROL R WHERE R.ID_ENTIDAD = E.ID_ENTIDAD AND R.ROL = @ROL)
            )			
            BEGIN
                SET @MSG_DUP = 'Ya existe otra entidad con el c' + NCHAR(243) + 'digo: ' + @CODIGO_ENTIDAD
                RAISERROR(@MSG_DUP, 16, 1)
                RETURN
            END

            IF ISNULL(@DUI, '') <> '' AND EXISTS (
                SELECT 1 FROM ENTIDAD E
                WHERE REPLACE(DUI,'-','') = REPLACE(@DUI,'-','')
                  AND ID_ENTIDAD <> @ID_COMPARA
				  AND EXISTS(SELECT 1 FROM ENTIDAD_ROL R WHERE R.ID_ENTIDAD = E.ID_ENTIDAD AND R.ROL = @ROL)
            )
            BEGIN
                SET @MSG_DUP = 'Ya existe otra entidad con el DUI: ' + @DUI
                RAISERROR(@MSG_DUP, 16, 1)
                RETURN
            END

            IF ISNULL(@NIT, '') <> '' AND EXISTS (
                SELECT 1 FROM ENTIDAD E
                WHERE REPLACE(NIT,'-','') = REPLACE(@NIT,'-','')
                  AND ID_ENTIDAD <> @ID_COMPARA
				  AND EXISTS(SELECT 1 FROM ENTIDAD_ROL R WHERE R.ID_ENTIDAD = E.ID_ENTIDAD AND R.ROL = @ROL)
            )
            BEGIN
                SET @MSG_DUP = 'Ya existe otra entidad con el NIT: ' + @NIT
                RAISERROR(@MSG_DUP, 16, 1)
                RETURN
            END

            IF ISNULL(@NRC, '') <> '' AND EXISTS (
                SELECT 1 FROM ENTIDAD E
                WHERE NRC = @NRC
                  AND ID_ENTIDAD <> @ID_COMPARA
				  AND EXISTS(SELECT 1 FROM ENTIDAD_ROL R WHERE R.ID_ENTIDAD = E.ID_ENTIDAD AND R.ROL = @ROL)
            )
            BEGIN
                SET @MSG_DUP = 'Ya existe otra entidad con el NRC: ' + @NRC
                RAISERROR(@MSG_DUP, 16, 1)
                RETURN
            END

            -- ----------------------------------------------------
            -- INSERT o UPDATE
            -- ----------------------------------------------------
            DECLARE @ID_FINAL INT

            IF ISNULL(@ID_ENTIDAD, 0) = 0
            BEGIN
                DECLARE @NUEVO_ID_E INT
                SELECT @NUEVO_ID_E = ISNULL(MAX(ID_ENTIDAD), 0) + 1 FROM ENTIDAD

                INSERT INTO ENTIDAD (
                    ID_ENTIDAD, CODIGO_ENTIDAD, ID_TIPO_ENTIDAD, ID_TIPO_CONTRIB,
                    ID_TIPO_DOC_INDEN, DOCUMENTO, NRC, DUI, NIT,
                    NOMBRE, NOMBRE_COMERCIAL, COMPLEMENTO,
                    CALLE, CASA, APTO_LOCAL, COLONIA,
                    CORREO, CORREO_CC, CELULAR, TELEFONO, ENCARGADO,
                    DIAS_PLAZO, ID_PAIS, CODI_DEPTO, CODI_MUNI,
                    ID_ACTIVIDAD_1, ID_ACTIVIDAD_2, ID_ACTIVIDAD_3,
                    CUENTA_X_PAGAR, ID_ORIGEN, CODIPROVEEDOR, CODTRANSPORT,
                    ID_CARGADORA, ID_PROVEEDOR_ROZA, ID_PROVEE_QQ,
                    RETENER_RENTA, PORC_RENTA,
                    USUARIO_CREA, FECHA_CREA, OTROS_DATOS, CUENTA_GASTO, ACTIVIDAD_EXT
                )
                VALUES (
                    @NUEVO_ID_E, @CODIGO_ENTIDAD, @ID_TIPO_ENTIDAD, @ID_TIPO_CONTRIB,
                    @ID_TIPO_DOC_INDEN, @DOCUMENTO, @NRC, @DUI, @NIT,
                    @NOMBRE, @NOMBRE_COMERCIAL, @COMPLEMENTO,
                    @CALLE, @CASA, @APTO_LOCAL, @COLONIA,
                    @CORREO, @CORREO_CC, @CELULAR, @TELEFONO, @ENCARGADO,
                    @DIAS_PLAZO, @ID_PAIS, @CODI_DEPTO, @CODI_MUNI,
                    @ID_ACTIVIDAD_1, @ID_ACTIVIDAD_2, @ID_ACTIVIDAD_3,
                    @CUENTA_X_PAGAR, @ID_ORIGEN, @CODIPROVEEDOR, @CODTRANSPORT,
                    @ID_CARGADORA, @ID_PROVEEDOR_ROZA, @ID_PROVEE_QQ,
                    @RETENER_RENTA, @PORC_RENTA,
                    @USUARIO_CREA, GETDATE(), @OTROS_DATOS, @CUENTA_GASTO, @ACTIVIDAD_EXT
                )

                SET @ID_FINAL = @NUEVO_ID_E
            END
            ELSE
            BEGIN
                UPDATE ENTIDAD SET
                    CODIGO_ENTIDAD    = @CODIGO_ENTIDAD,
                    ID_TIPO_ENTIDAD   = @ID_TIPO_ENTIDAD,
                    ID_TIPO_CONTRIB   = @ID_TIPO_CONTRIB,
                    ID_TIPO_DOC_INDEN = @ID_TIPO_DOC_INDEN,
                    DOCUMENTO         = @DOCUMENTO,
                    NRC               = @NRC,
                    DUI               = @DUI,
                    NIT               = @NIT,
                    NOMBRE            = @NOMBRE,
                    NOMBRE_COMERCIAL  = @NOMBRE_COMERCIAL,
                    COMPLEMENTO       = @COMPLEMENTO,
                    CALLE             = @CALLE,
                    CASA              = @CASA,
                    APTO_LOCAL        = @APTO_LOCAL,
                    COLONIA           = @COLONIA,
                    CORREO            = @CORREO,
                    CORREO_CC         = @CORREO_CC,
                    CELULAR           = @CELULAR,
                    TELEFONO          = @TELEFONO,
                    ENCARGADO         = @ENCARGADO,
                    DIAS_PLAZO        = @DIAS_PLAZO,
                    ID_PAIS           = @ID_PAIS,
                    CODI_DEPTO        = @CODI_DEPTO,
                    CODI_MUNI         = @CODI_MUNI,
                    ID_ACTIVIDAD_1    = @ID_ACTIVIDAD_1,
                    ID_ACTIVIDAD_2    = @ID_ACTIVIDAD_2,
                    ID_ACTIVIDAD_3    = @ID_ACTIVIDAD_3,
                    CUENTA_X_PAGAR    = @CUENTA_X_PAGAR,
                    ID_ORIGEN         = @ID_ORIGEN,
                    CODIPROVEEDOR     = @CODIPROVEEDOR,
                    CODTRANSPORT      = @CODTRANSPORT,
                    ID_CARGADORA      = @ID_CARGADORA,
                    ID_PROVEEDOR_ROZA = @ID_PROVEEDOR_ROZA,
                    ID_PROVEE_QQ      = @ID_PROVEE_QQ,
                    RETENER_RENTA     = @RETENER_RENTA,
                    PORC_RENTA        = @PORC_RENTA,
                    USUARIO_ACT       = @USUARIO_ACT,
                    FECHA_ACT         = GETDATE(),
					OTROS_DATOS		  = @OTROS_DATOS,
					CUENTA_GASTO	  = @CUENTA_GASTO,
					ACTIVIDAD_EXT	  = @ACTIVIDAD_EXT
                WHERE ID_ENTIDAD = @ID_ENTIDAD

                SET @ID_FINAL = @ID_ENTIDAD
            END

            -- ----------------------------------------------------
            -- Manejo idempotente del rol
            -- ----------------------------------------------------
            IF ISNULL(@ROL, '') <> ''
            BEGIN
                IF EXISTS (
                    SELECT 1 FROM ENTIDAD_ROL
                    WHERE ID_ENTIDAD = @ID_FINAL AND ROL = @ROL
                )
                BEGIN
                    UPDATE ENTIDAD_ROL
                    SET ACTIVO = 1
                    WHERE ID_ENTIDAD = @ID_FINAL AND ROL = @ROL
                END
                ELSE
                BEGIN
                    DECLARE @NUEVO_ID_ROL INT
                    SELECT @NUEVO_ID_ROL = ISNULL(MAX(ID_ENTIDAD_ROL), 0) + 1
                    FROM ENTIDAD_ROL

                    INSERT INTO ENTIDAD_ROL (
                        ID_ENTIDAD_ROL, ID_ENTIDAD, ROL, ACTIVO, FECHA_ASIGNACION
                    )
                    VALUES (
                        @NUEVO_ID_ROL, @ID_FINAL, @ROL, 1, GETDATE()
                    )
                END
            END

            COMMIT TRAN

            SELECT @ID_FINAL AS ID_GENERADO
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0 ROLLBACK TRAN

            DECLARE @ERR_MSG NVARCHAR(4000) = ERROR_MESSAGE()
            DECLARE @ERR_SEV INT             = ERROR_SEVERITY()
            DECLARE @ERR_ST  INT             = ERROR_STATE()
            RAISERROR(@ERR_MSG, @ERR_SEV, @ERR_ST)
            RETURN
        END CATCH
    END

    -- ============================================================
    -- ELIMINAR: baja lógica del rol
    -- ============================================================
    ELSE IF @ACCION = 'ELIMINAR'
    BEGIN
        UPDATE ENTIDAD_ROL SET ACTIVO = 0
        WHERE ID_ENTIDAD = @ID_ENTIDAD
          AND (@ROL IS NULL OR ROL = @ROL)
    END
END
