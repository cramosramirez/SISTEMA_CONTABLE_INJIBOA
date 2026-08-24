USE [PH2]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF COL_LENGTH(N'EINVENTARIO.PRODUCTO', N'ID_PRODUCTO_SIGESTA') IS NULL
BEGIN
    ALTER TABLE [EINVENTARIO].[PRODUCTO]
        ADD [ID_PRODUCTO_SIGESTA] INT NULL;
END;
GO

ALTER PROCEDURE [EINVENTARIO].[SP_PRODUCTO_SIGESTA]
    @ACCION VARCHAR(20),
    @FILTRO VARCHAR(200) = NULL,
    @ID_PRODUCTO INT = NULL,
    @COD_REF NVARCHAR(30) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @ACCION = UPPER(LTRIM(RTRIM(@ACCION)));

    IF @ACCION = 'LISTAR'
    BEGIN
        DECLARE @FILTRO_NORMALIZADO VARCHAR(200) =
            NULLIF(LTRIM(RTRIM(@FILTRO)), '');

        SELECT DISTINCT
            P.ID_PRODUCTO AS CODIGO,
            P.NOMBRE_PRODUCTO AS NOMBRE
        FROM [INJIBOA].[dbo].[PRODUCTO] AS P
        WHERE P.ID_PROVEE = 4
          AND P.ID_PRODUCTO IS NOT NULL
          AND P.ACTIVO = 1
          AND
          (
              @FILTRO_NORMALIZADO IS NULL
              OR CONVERT(VARCHAR(50), P.ID_PRODUCTO)
                    LIKE '%' + @FILTRO_NORMALIZADO + '%'
              OR P.NOMBRE_PRODUCTO
                    LIKE '%' + @FILTRO_NORMALIZADO + '%'
          )
        ORDER BY P.NOMBRE_PRODUCTO;

        RETURN;
    END;

    IF @ACCION = 'ACTUALIZAR_COD_REF'
    BEGIN
        IF @ID_PRODUCTO IS NULL OR @ID_PRODUCTO <= 0
            THROW 50001, 'El parámetro @ID_PRODUCTO debe ser mayor que cero.', 1;

        IF NOT EXISTS
        (
            SELECT 1
            FROM [INJIBOA].[dbo].[PRODUCTO]
            WHERE ID_PRODUCTO = @ID_PRODUCTO
        )
            THROW 50002, 'El producto indicado no existe en INJIBOA.dbo.PRODUCTO.', 1;

        SET @COD_REF = NULLIF(LTRIM(RTRIM(@COD_REF)), N'');

        IF @COD_REF IS NULL
            THROW 50003, 'El parámetro @COD_REF es obligatorio.', 1;

        IF NOT EXISTS
        (
            SELECT 1
            FROM [EINVENTARIO].[PRODUCTO]
            WHERE LTRIM(RTRIM(COD_REF)) = @COD_REF
        )
            THROW 50004, 'No existe el producto local indicado por @COD_REF.', 1;

        BEGIN TRANSACTION;

        BEGIN TRY
            UPDATE [INJIBOA].[dbo].[PRODUCTO]
            SET COD_REF = @COD_REF
            WHERE ID_PRODUCTO = @ID_PRODUCTO;

            DECLARE @FILAS_AFECTADAS INT = @@ROWCOUNT;

            UPDATE [EINVENTARIO].[PRODUCTO]
            SET ID_PRODUCTO_SIGESTA = @ID_PRODUCTO
            WHERE LTRIM(RTRIM(COD_REF)) = @COD_REF;

            DECLARE @FILAS_RELACIONADAS INT = @@ROWCOUNT;

            COMMIT TRANSACTION;

            SELECT
                @ID_PRODUCTO AS ID_PRODUCTO,
                @COD_REF AS COD_REF,
                @FILAS_AFECTADAS AS FILAS_AFECTADAS,
                @FILAS_RELACIONADAS AS FILAS_RELACIONADAS;
        END TRY
        BEGIN CATCH
            IF XACT_STATE() <> 0
                ROLLBACK TRANSACTION;

            THROW;
        END CATCH;

        RETURN;
    END;

    THROW 50000, 'La acción indicada no es válida para SP_PRODUCTO_SIGESTA.', 1;
END;
GO
