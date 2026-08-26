USE [PH2];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

-- =====================================================================
-- Agrega columnas para conservar el historial de lo importado desde la
-- integración con SIGESTA (solicitud agrícola) al guardar una Factura.
-- Migra a frmFactura el mismo patrón ya usado en frmCreditoFiscal
-- (ver 20260825_Agregar_Historial_Solicitud_CCF.sql). A diferencia de CCF,
-- en Factura el combo "Solicitud" nunca se persistía en la base de datos,
-- así que aquí ID_SOLICITUD también es una columna nueva (no solo el
-- historial NUM_SOLICITUD/NOMBRE_CUENTA/UID_SOLIC_AGRICOLA).
--   - [EDTE].[FACTURA_ENC]: ID_SOLICITUD, NUM_SOLICITUD, NOMBRE_CUENTA,
--     UID_SOLIC_AGRICOLA (referencia a ENCA.UID_SOLIC_AGRICOLA de
--     ACTION='PRODUCTOR_ENCABEZADO' en [ESOLICITUD].[SP_SOLICITUDES_SIGESTA]).
--   - [EDTE].[FACTURA_DET]: ID_SOLICITUD, ID_SOLIC_AGRI_PROD,
--     UID_SOLIC_AGRI_PROD (trazabilidad hacia ACTION='PRODUCTOR_DETALLE').
-- Ejecutar ANTES de desplegar EDTE.SP_FACTURA_ENC / SP_FACTURA_DET
-- actualizados, ya que esos SP ya referencian estas columnas.
-- =====================================================================

BEGIN TRY
    BEGIN TRANSACTION;

    IF OBJECT_ID(N'[EDTE].[FACTURA_ENC]', N'U') IS NULL
        THROW 50000, N'No existe la tabla EDTE.FACTURA_ENC.', 1;

    IF OBJECT_ID(N'[EDTE].[FACTURA_DET]', N'U') IS NULL
        THROW 50001, N'No existe la tabla EDTE.FACTURA_DET.', 1;

    -- ---------- EDTE.FACTURA_ENC ----------
    IF COL_LENGTH(N'[EDTE].[FACTURA_ENC]', N'ID_SOLICITUD') IS NULL
    BEGIN
        ALTER TABLE [EDTE].[FACTURA_ENC]
            ADD [ID_SOLICITUD] INT NULL;
    END;

    IF COL_LENGTH(N'[EDTE].[FACTURA_ENC]', N'NUM_SOLICITUD') IS NULL
    BEGIN
        ALTER TABLE [EDTE].[FACTURA_ENC]
            ADD [NUM_SOLICITUD] NVARCHAR(20) NULL;
    END;

    IF COL_LENGTH(N'[EDTE].[FACTURA_ENC]', N'NOMBRE_CUENTA') IS NULL
    BEGIN
        ALTER TABLE [EDTE].[FACTURA_ENC]
            ADD [NOMBRE_CUENTA] NVARCHAR(200) NULL;
    END;

    IF COL_LENGTH(N'[EDTE].[FACTURA_ENC]', N'UID_SOLIC_AGRICOLA') IS NULL
    BEGIN
        ALTER TABLE [EDTE].[FACTURA_ENC]
            ADD [UID_SOLIC_AGRICOLA] NVARCHAR(40) NULL;
    END;

    -- ---------- EDTE.FACTURA_DET ----------
    IF COL_LENGTH(N'[EDTE].[FACTURA_DET]', N'ID_SOLICITUD') IS NULL
    BEGIN
        ALTER TABLE [EDTE].[FACTURA_DET]
            ADD [ID_SOLICITUD] INT NULL;
    END;

    IF COL_LENGTH(N'[EDTE].[FACTURA_DET]', N'ID_SOLIC_AGRI_PROD') IS NULL
    BEGIN
        ALTER TABLE [EDTE].[FACTURA_DET]
            ADD [ID_SOLIC_AGRI_PROD] INT NULL;
    END;

    IF COL_LENGTH(N'[EDTE].[FACTURA_DET]', N'UID_SOLIC_AGRI_PROD') IS NULL
    BEGIN
        ALTER TABLE [EDTE].[FACTURA_DET]
            ADD [UID_SOLIC_AGRI_PROD] NVARCHAR(40) NULL;
    END;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

SELECT
    TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE (TABLE_SCHEMA = N'EDTE' AND TABLE_NAME = N'FACTURA_ENC'
       AND COLUMN_NAME IN (N'ID_SOLICITUD', N'NUM_SOLICITUD', N'NOMBRE_CUENTA', N'UID_SOLIC_AGRICOLA'))
   OR (TABLE_SCHEMA = N'EDTE' AND TABLE_NAME = N'FACTURA_DET'
       AND COLUMN_NAME IN (N'ID_SOLICITUD', N'ID_SOLIC_AGRI_PROD', N'UID_SOLIC_AGRI_PROD'))
ORDER BY TABLE_NAME, COLUMN_NAME;
GO
