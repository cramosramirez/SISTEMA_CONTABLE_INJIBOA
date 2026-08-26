USE [PH2]
GO
/****** Object:  StoredProcedure [EMH].[SP_ENTIDAD]    Script Date: 25/8/2026 12:55:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================================
-- [EMH].[SP_ENTIDAD]
-- Acciones: LISTAR | CONSULTAR | GUARDAR | ELIMINAR | BUSCAR
-- ============================================================
ALTER   PROCEDURE [EMH].[SP_ENTIDAD]
    @ACCION            NVARCHAR(20)  = NULL,
    @ID_ENTIDAD        INT           = NULL,
    @CODIGO_ENTIDAD    NVARCHAR(20)  = NULL,
    @ID_TIPO_ENTIDAD   INT           = NULL,
    @ID_TIPO_CONTRIB   INT           = NULL,
    @ID_TIPO_DOC_INDEN INT           = NULL,
    @DOCUMENTO         NVARCHAR(20)  = NULL,
    @NRC               NVARCHAR(20)  = NULL,
    @DUI               NVARCHAR(20)  = NULL,
    @NIT               NVARCHAR(20)  = NULL,
    @NOMBRE            NVARCHAR(300) = NULL,
    @COMPLEMENTO       NVARCHAR(300) = NULL,
    @CALLE             NVARCHAR(100) = NULL,
    @CASA              NVARCHAR(100) = NULL,
    @APTO_LOCAL        NVARCHAR(100) = NULL,
    @COLONIA           NVARCHAR(100) = NULL,
    @CORREO            NVARCHAR(100) = NULL,
    @CORREO_CC         NVARCHAR(100) = NULL,
    @CELULAR           NVARCHAR(50)  = NULL,
    @TELEFONO          NVARCHAR(50)  = NULL,
    @ENCARGADO         NVARCHAR(200) = NULL,
    @DIAS_PLAZO        INT           = NULL,
    @ID_PAIS           INT           = NULL,
    @CODI_DEPTO        NVARCHAR(2)   = NULL,
    @CODI_MUNI         NVARCHAR(2)   = NULL,
    @ID_ACTIVIDAD_1    INT           = NULL,
    @ID_ACTIVIDAD_2    INT           = NULL,
    @ID_ACTIVIDAD_3    INT           = NULL,
    @CUENTA_X_PAGAR    NVARCHAR(100) = NULL,
    @NOMBRE_COMERCIAL  NVARCHAR(300) = NULL,
    @ID_ORIGEN         INT           = NULL,
    @CODIPROVEEDOR     VARCHAR(50)   = NULL,
    @CODTRANSPORT      INT           = NULL,
    @ID_CARGADORA      INT           = NULL,
    @RETENER_RENTA     BIT           = NULL,
    @PORC_RENTA        NUMERIC(20,5) = NULL,
    @USER              NVARCHAR(100) = NULL,
    @FILTRO            NVARCHAR(300) = NULL,
    @ACTIVIDAD_EXT     NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- ── LISTAR ───────────────────────────────────────────────
    IF @ACCION = 'LISTAR'
    BEGIN
        SELECT
            E.ID_ENTIDAD,
            E.CODIGO_ENTIDAD,
            E.NOMBRE,
            E.NOMBRE_COMERCIAL,
            E.NRC,
            E.NIT,
            E.DUI,
            E.DOCUMENTO,
            E.CORREO,
            E.CELULAR,
            E.TELEFONO,
            -- Ajusta el campo de descripción según tu tabla TIPO_PERSONA
            TP.NOMBRE      AS NOMBRE_TIPO_PERSONA,
            TC.NOMBRE      AS NOMBRE_TIPO_CONTRIB
        FROM  dbo.ENTIDAD           E
        LEFT JOIN dbo.TIPO_PERSONA       TP ON TP.ID_TIPO_PERSONA = E.ID_TIPO_ENTIDAD
        LEFT JOIN dbo.TIPO_CONTRIBUYENTE TC ON TC.ID_TIPO_CONTRIB = E.ID_TIPO_CONTRIB
        ORDER BY E.NOMBRE;
        RETURN;
    END
    -- ── CONSULTAR ────────────────────────────────────────────
    IF @ACCION = 'CONSULTAR'
    BEGIN
        -- SELECT * ya incluye ACTIVIDAD_EXT automáticamente (columna de dbo.ENTIDAD)
        SELECT *
        FROM  dbo.ENTIDAD
        WHERE ID_ENTIDAD = @ID_ENTIDAD;
        RETURN;
    END
    -- ── BUSCAR ───────────────────────────────────────────────
    IF @ACCION = 'BUSCAR'
    BEGIN
        SELECT ID_ENTIDAD, CODIGO_ENTIDAD, NOMBRE
        FROM  dbo.ENTIDAD
        WHERE CODIGO_ENTIDAD LIKE '%' + @FILTRO + '%'



           --OR NOMBRE          LIKE '%' + @FILTRO + '%'
           OR NOT EXISTS ( SELECT 1 FROM STRING_SPLIT(@FILTRO, ' ') AS palabra WHERE NOMBRE NOT LIKE '%' + palabra.value + '%')
           OR NRC             LIKE '%' + @FILTRO + '%'
           OR NIT             LIKE '%' + @FILTRO + '%'
        ORDER BY NOMBRE;
        RETURN;
    END
    -- ── GUARDAR (INSERT / UPDATE) ─────────────────────────────
    IF @ACCION = 'GUARDAR'
    BEGIN
        IF ISNULL(@ID_ENTIDAD, 0) = 0
        BEGIN
            -- Generar nuevo ID
            SELECT @ID_ENTIDAD = ISNULL(MAX(ID_ENTIDAD), 0) + 1
            FROM   dbo.ENTIDAD;
            INSERT INTO dbo.ENTIDAD (
                ID_ENTIDAD,    CODIGO_ENTIDAD,    ID_TIPO_ENTIDAD,   ID_TIPO_CONTRIB,
                ID_TIPO_DOC_INDEN, DOCUMENTO,     NRC,               DUI,
                NIT,           NOMBRE,            COMPLEMENTO,       CALLE,
                CASA,          APTO_LOCAL,        COLONIA,           CORREO,
                CORREO_CC,     CELULAR,           TELEFONO,          ENCARGADO,
                DIAS_PLAZO,    ID_PAIS,           CODI_DEPTO,        CODI_MUNI,
                ID_ACTIVIDAD_1,ID_ACTIVIDAD_2,    ID_ACTIVIDAD_3,    CUENTA_X_PAGAR,
                NOMBRE_COMERCIAL, ID_ORIGEN,      CODIPROVEEDOR,     CODTRANSPORT,
                ID_CARGADORA,  RETENER_RENTA,     PORC_RENTA,
                USUARIO_CREA,  FECHA_CREA,        ACTIVIDAD_EXT
            )
            VALUES (
                @ID_ENTIDAD,   @CODIGO_ENTIDAD,   @ID_TIPO_ENTIDAD,  @ID_TIPO_CONTRIB,
                @ID_TIPO_DOC_INDEN, @DOCUMENTO,   @NRC,              @DUI,
                @NIT,          @NOMBRE,           @COMPLEMENTO,      @CALLE,
                @CASA,         @APTO_LOCAL,       @COLONIA,          @CORREO,
                @CORREO_CC,    @CELULAR,          @TELEFONO,         @ENCARGADO,
                @DIAS_PLAZO,   @ID_PAIS,          @CODI_DEPTO,       @CODI_MUNI,
                @ID_ACTIVIDAD_1, @ID_ACTIVIDAD_2, @ID_ACTIVIDAD_3,   @CUENTA_X_PAGAR,
                @NOMBRE_COMERCIAL, @ID_ORIGEN,    @CODIPROVEEDOR,    @CODTRANSPORT,
                @ID_CARGADORA, @RETENER_RENTA,    @PORC_RENTA,
                @USER,         GETDATE(),         @ACTIVIDAD_EXT
            );
        END
        ELSE
        BEGIN
            UPDATE dbo.ENTIDAD SET
                CODIGO_ENTIDAD    = @CODIGO_ENTIDAD,
                ID_TIPO_ENTIDAD   = @ID_TIPO_ENTIDAD,
                ID_TIPO_CONTRIB   = @ID_TIPO_CONTRIB,
                ID_TIPO_DOC_INDEN = @ID_TIPO_DOC_INDEN,
                DOCUMENTO         = @DOCUMENTO,
                NRC               = @NRC,
                DUI               = @DUI,
                NIT               = @NIT,
                NOMBRE            = @NOMBRE,
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
                NOMBRE_COMERCIAL  = @NOMBRE_COMERCIAL,
                ID_ORIGEN         = @ID_ORIGEN,
                CODIPROVEEDOR     = @CODIPROVEEDOR,
                CODTRANSPORT      = @CODTRANSPORT,
                ID_CARGADORA      = @ID_CARGADORA,
                RETENER_RENTA     = @RETENER_RENTA,
                PORC_RENTA        = @PORC_RENTA,
                USUARIO_ACT       = @USER,
                FECHA_ACT         = GETDATE(),
                ACTIVIDAD_EXT     = @ACTIVIDAD_EXT
            WHERE ID_ENTIDAD = @ID_ENTIDAD;
        END
        SELECT @ID_ENTIDAD AS ID_GENERADO;
        RETURN;
    END
    -- ── ELIMINAR ─────────────────────────────────────────────
    IF @ACCION = 'ELIMINAR'
    BEGIN
        DELETE FROM dbo.ENTIDAD_ROL WHERE ID_ENTIDAD   = @ID_ENTIDAD;
        DELETE FROM dbo.ENTIDAD     WHERE ID_ENTIDAD   = @ID_ENTIDAD;
        RETURN;
    END
END
