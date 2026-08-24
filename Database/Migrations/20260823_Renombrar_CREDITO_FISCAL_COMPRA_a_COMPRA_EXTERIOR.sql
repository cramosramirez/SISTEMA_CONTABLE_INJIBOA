USE [PH2];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/*
    Renombra la tabla física y su llave primaria:

        EPROVEEDOR.CREDITO_FISCAL_COMPRA.ID_CCF_COMPRA
        -> EPROVEEDOR.COMPRA_EXTERIOR.ID_COMPRA_EXTERIOR

    Para conservar el funcionamiento de los módulos nacionales, reemplaza
    el sinónimo dbo.CREDITO_FISCAL_COMPRA por una vista actualizable que
    expone ID_COMPRA_EXTERIOR con el alias histórico ID_CCF_COMPRA.
*/

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @TablaAnteriorId INT =
        OBJECT_ID(N'[EPROVEEDOR].[CREDITO_FISCAL_COMPRA]', N'U');
    DECLARE @TablaNuevaId INT =
        OBJECT_ID(N'[EPROVEEDOR].[COMPRA_EXTERIOR]', N'U');
    DECLARE @FilasAntes BIGINT;
    DECLARE @FilasDespues BIGINT;

    IF @TablaAnteriorId IS NOT NULL AND @TablaNuevaId IS NOT NULL
    BEGIN
        THROW 50000, N'Existen ambas tablas en EPROVEEDOR. Revise la estructura antes de continuar.', 1;
    END;

    IF @TablaAnteriorId IS NULL AND @TablaNuevaId IS NULL
    BEGIN
        THROW 50001, N'No se encontró la tabla que debe renombrarse.', 1;
    END;

    SELECT @FilasAntes = SUM(P.rows)
    FROM sys.partitions AS P
    WHERE P.object_id = COALESCE(@TablaAnteriorId, @TablaNuevaId)
      AND P.index_id IN (0, 1);

    IF EXISTS
    (
        SELECT 1
        FROM sys.synonyms
        WHERE schema_id = SCHEMA_ID(N'dbo')
          AND name = N'CREDITO_FISCAL_COMPRA'
    )
    BEGIN
        DROP SYNONYM [dbo].[CREDITO_FISCAL_COMPRA];
    END;

    IF @TablaAnteriorId IS NOT NULL
    BEGIN
        EXEC sys.sp_rename
            @objname = N'[EPROVEEDOR].[CREDITO_FISCAL_COMPRA]',
            @newname = N'COMPRA_EXTERIOR',
            @objtype = N'OBJECT';
    END;

    IF COL_LENGTH(N'[EPROVEEDOR].[COMPRA_EXTERIOR]', N'ID_COMPRA_EXTERIOR') IS NULL
    BEGIN
        IF COL_LENGTH(N'[EPROVEEDOR].[COMPRA_EXTERIOR]', N'ID_CCF_COMPRA') IS NULL
        BEGIN
            THROW 50002, N'No se encontró la columna ID_CCF_COMPRA para renombrarla.', 1;
        END;

        EXEC sys.sp_rename
            @objname = N'[EPROVEEDOR].[COMPRA_EXTERIOR].[ID_CCF_COMPRA]',
            @newname = N'ID_COMPRA_EXTERIOR',
            @objtype = N'COLUMN';
    END;

    IF OBJECT_ID(N'[dbo].[CREDITO_FISCAL_COMPRA]') IS NOT NULL
       AND OBJECT_ID(N'[dbo].[CREDITO_FISCAL_COMPRA]', N'V') IS NULL
    BEGIN
        THROW 50003, N'El nombre dbo.CREDITO_FISCAL_COMPRA está ocupado por un objeto incompatible.', 1;
    END;

    EXEC(N'
CREATE OR ALTER VIEW [dbo].[CREDITO_FISCAL_COMPRA]
AS
    SELECT
        [ID_COMPRA_EXTERIOR] AS [ID_CCF_COMPRA],
        [ID_QUEDAN], [NUM_QUEDAN], [ID_CHEQUE], [NUM_CHEQUE],
        [ID_TIPO_DTE], [NUM_CONTROL], [COD_GENERACION], [SELLO_RECIBIDO],
        [FECHA_EMISION], [TIPO_MONEDA], [ID_ENTIDAD], [CODIGO_ENTIDAD],
        [FECHA_RECIBIDO], [FECHA_VENCE], [ORDEN], [ID_SUCURSAL],
        [ID_TIPO_SERVI], [ID_TIPO_OPERA], [ID_CLASIFICA], [ID_SECTOR],
        [ID_TIPO_COSTO], [ID_TIPO_RENTA], [NO_SUJETA], [EXENTA], [GRAVADA],
        [IVA], [FOVIAL], [COTRANS], [TOTAL], [CARGO], [ABONO],
        [APLICABLE_RENTA], [RENTA], [IVAR], [SALDO], [ID_COMPROBANTE_RET],
        [ID_CCF_COMPRA_ASOCIADO], [OBSERVACION], [USUARIO_CREA], [FECHA_CREA],
        [USUARIO_ACT], [FECHA_ACT], [UID_ENLACE_CHEQUE], [SALFEC], [UID_DOCUMENTO]
    FROM [EPROVEEDOR].[COMPRA_EXTERIOR];
');

    SET @TablaNuevaId = OBJECT_ID(N'[EPROVEEDOR].[COMPRA_EXTERIOR]', N'U');

    SELECT @FilasDespues = SUM(P.rows)
    FROM sys.partitions AS P
    WHERE P.object_id = @TablaNuevaId
      AND P.index_id IN (0, 1);

    IF @TablaNuevaId IS NULL
       OR COL_LENGTH(N'[EPROVEEDOR].[COMPRA_EXTERIOR]', N'ID_COMPRA_EXTERIOR') IS NULL
       OR ISNULL(@FilasAntes, 0) <> ISNULL(@FilasDespues, 0)
    BEGIN
        THROW 50004, N'La validación posterior al renombrado no fue satisfactoria.', 1;
    END;

    IF
    (
        SELECT COUNT(*)
        FROM sys.foreign_keys AS FK
        WHERE FK.parent_object_id = @TablaNuevaId
           OR FK.referenced_object_id = @TablaNuevaId
    ) < 13
    BEGIN
        THROW 50005, N'La tabla no conserva las 13 relaciones esperadas.', 1;
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
    C.name AS LLAVE,
    SUM(P.rows) AS FILAS
FROM sys.tables AS T
INNER JOIN sys.indexes AS I
    ON I.object_id = T.object_id
   AND I.is_primary_key = 1
INNER JOIN sys.index_columns AS IC
    ON IC.object_id = I.object_id
   AND IC.index_id = I.index_id
INNER JOIN sys.columns AS C
    ON C.object_id = IC.object_id
   AND C.column_id = IC.column_id
INNER JOIN sys.partitions AS P
    ON P.object_id = T.object_id
   AND P.index_id = I.index_id
WHERE T.object_id = OBJECT_ID(N'[EPROVEEDOR].[COMPRA_EXTERIOR]', N'U')
GROUP BY T.schema_id, T.name, C.name;
GO
