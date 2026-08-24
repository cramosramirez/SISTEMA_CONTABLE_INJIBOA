USE [PH2];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/*
    Migración recomendada para la base PH2 existente.

    - Conserva los datos, el object_id, la PK y las 13 relaciones FK actuales.
    - Traslada la tabla de dbo a EPROVEEDOR.
    - Crea un sinónimo de compatibilidad en dbo para los módulos nacionales
      que todavía consultan CREDITO_FISCAL_COMPRA sin indicar esquema.
    - No copia ni duplica registros.
*/

BEGIN TRY
    BEGIN TRANSACTION;

    IF SCHEMA_ID(N'EPROVEEDOR') IS NULL
    BEGIN
        EXEC(N'CREATE SCHEMA [EPROVEEDOR] AUTHORIZATION [dbo];');
    END;

    IF OBJECT_ID(N'[EPROVEEDOR].[CREDITO_FISCAL_COMPRA]', N'U') IS NOT NULL
       AND OBJECT_ID(N'[dbo].[CREDITO_FISCAL_COMPRA]', N'U') IS NOT NULL
    BEGIN
        THROW 50000, N'Existen dos tablas CREDITO_FISCAL_COMPRA. Revise la estructura antes de migrar.', 1;
    END;

    IF OBJECT_ID(N'[EPROVEEDOR].[CREDITO_FISCAL_COMPRA]', N'U') IS NULL
    BEGIN
        IF OBJECT_ID(N'[dbo].[CREDITO_FISCAL_COMPRA]', N'U') IS NULL
        BEGIN
            THROW 50001, N'No se encontró [dbo].[CREDITO_FISCAL_COMPRA] para trasladarla.', 1;
        END;

        ALTER SCHEMA [EPROVEEDOR]
            TRANSFER [dbo].[CREDITO_FISCAL_COMPRA];
    END;

    IF OBJECT_ID(N'[EPROVEEDOR].[CREDITO_FISCAL_COMPRA]', N'U') IS NULL
    BEGIN
        THROW 50002, N'No se pudo trasladar la tabla a EPROVEEDOR.', 1;
    END;

    IF OBJECT_ID(N'[dbo].[CREDITO_FISCAL_COMPRA]') IS NOT NULL
       AND NOT EXISTS
       (
           SELECT 1
           FROM sys.synonyms
           WHERE schema_id = SCHEMA_ID(N'dbo')
             AND name = N'CREDITO_FISCAL_COMPRA'
       )
    BEGIN
        THROW 50003, N'El nombre [dbo].[CREDITO_FISCAL_COMPRA] está ocupado por otro objeto.', 1;
    END;

    IF NOT EXISTS
    (
        SELECT 1
        FROM sys.synonyms
        WHERE schema_id = SCHEMA_ID(N'dbo')
          AND name = N'CREDITO_FISCAL_COMPRA'
    )
    BEGIN
        EXEC(N'CREATE SYNONYM [dbo].[CREDITO_FISCAL_COMPRA]
               FOR [EPROVEEDOR].[CREDITO_FISCAL_COMPRA];');
    END;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
    BEGIN
        ROLLBACK TRANSACTION;
    END;

    THROW;
END CATCH;
GO

SELECT
    SCHEMA_NAME(T.schema_id) AS ESQUEMA,
    T.name AS TABLA,
    SUM(P.rows) AS FILAS,
    CASE
        WHEN EXISTS
        (
            SELECT 1
            FROM sys.synonyms AS S
            WHERE S.schema_id = SCHEMA_ID(N'dbo')
              AND S.name = N'CREDITO_FISCAL_COMPRA'
        ) THEN N'SÍ'
        ELSE N'NO'
    END AS SINONIMO_DBO
FROM sys.tables AS T
INNER JOIN sys.partitions AS P
    ON P.object_id = T.object_id
   AND P.index_id IN (0, 1)
WHERE T.object_id = OBJECT_ID(N'[EPROVEEDOR].[CREDITO_FISCAL_COMPRA]', N'U')
GROUP BY T.schema_id, T.name;
GO
