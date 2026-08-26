USE [PH2];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

-- ============================================================================
-- Agrega la columna ACTIVIDAD_EXT a dbo.ENTIDAD.
-- Representa la "Actividad económica en el exterior" de la entidad.
-- NOTA: esta columna ya fue aplicada manualmente en PH2 por Roberto el
-- 2026-08-25 mediante:
--   ALTER TABLE [PH2].[dbo].[ENTIDAD] ADD ACTIVIDAD_EXT NVARCHAR(200) NULL
-- Este script queda como registro versionado en el repo y es idempotente,
-- para poder re-aplicarse en otros ambientes (dev/QA) sin duplicar la columna.
-- ============================================================================

BEGIN TRY
    BEGIN TRANSACTION;

    IF OBJECT_ID(N'[dbo].[ENTIDAD]', N'U') IS NULL
        THROW 50000, N'No existe la tabla dbo.ENTIDAD.', 1;

    IF COL_LENGTH(N'[dbo].[ENTIDAD]', N'ACTIVIDAD_EXT') IS NULL
    BEGIN
        ALTER TABLE [dbo].[ENTIDAD]
            ADD [ACTIVIDAD_EXT] NVARCHAR(200) NULL;
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
    C.name AS COLUMNA,
    TY.name AS TIPO,
    C.max_length,
    C.is_nullable
FROM sys.columns AS C
INNER JOIN sys.types AS TY ON TY.user_type_id = C.user_type_id
WHERE C.object_id = OBJECT_ID(N'[dbo].[ENTIDAD]')
  AND C.name = N'ACTIVIDAD_EXT';
GO
