USE [PH2];
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

-- ================================================================
--  SP_TIPO_RENTA  (catálogo con banderas QUEDAN/RETENCION/COMPRAS/EXTERIOR)
--
--  La acción COMBO devuelve únicamente los tipos habilitados para exterior.
-- ================================================================
CREATE OR ALTER PROCEDURE [EPROVEEDOR].[SP_TIPO_RENTA]
    @ACCION        VARCHAR(100),
    @ID_TIPO_RENTA INT            = NULL,
    @CODIGO        NVARCHAR(2)    = NULL,
    @DESCRIPCION   NVARCHAR(350)  = NULL,
    @QUEDAN        BIT            = NULL,
    @RETENCION     BIT            = NULL,
    @COMPRAS       BIT            = NULL,
    @EXTERIOR      BIT            = NULL,
    @VALOR         NUMERIC(10,4)  = NULL,
    @VALOR_INT     INT            = NULL,
    @FILTRO        NVARCHAR(100)  = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @ACCION = 'BUSCAR'
    BEGIN
        SELECT TOP 20 ID_TIPO_RENTA, CODIGO, DESCRIPCION,
                      QUEDAN, RETENCION, COMPRAS, EXTERIOR,
                      VALOR, VALOR_INT
        FROM TIPO_RENTA
        WHERE (@FILTRO IS NULL
               OR DESCRIPCION LIKE '%' + @FILTRO + '%'
               OR CODIGO      LIKE '%' + @FILTRO + '%')
        ORDER BY DESCRIPCION
    END

    ELSE IF @ACCION = 'COMBO'
    BEGIN
        SELECT ID_TIPO_RENTA, CODIGO, DESCRIPCION,
               QUEDAN, RETENCION, COMPRAS, EXTERIOR,
               VALOR, VALOR_INT
        FROM TIPO_RENTA
        WHERE EXTERIOR = 1
        ORDER BY ID_TIPO_RENTA
    END

    ELSE IF @ACCION = 'OBTENER'
    BEGIN
        SELECT ID_TIPO_RENTA, CODIGO, DESCRIPCION,
               QUEDAN, RETENCION, COMPRAS, EXTERIOR,
               VALOR, VALOR_INT
        FROM TIPO_RENTA
        WHERE ID_TIPO_RENTA = @ID_TIPO_RENTA
    END

    ELSE IF @ACCION = 'GUARDAR'
    BEGIN
        DECLARE @NUEVO_ID_TR INT
        IF @ID_TIPO_RENTA = 0
        BEGIN
            SELECT @NUEVO_ID_TR = ISNULL(MAX(ID_TIPO_RENTA), 0) + 1 FROM TIPO_RENTA
            INSERT INTO TIPO_RENTA
                (ID_TIPO_RENTA, CODIGO, DESCRIPCION, QUEDAN, RETENCION,
                 COMPRAS, EXTERIOR, VALOR, VALOR_INT)
            VALUES
                (@NUEVO_ID_TR, @CODIGO, @DESCRIPCION, @QUEDAN, @RETENCION,
                 @COMPRAS, @EXTERIOR, @VALOR, @VALOR_INT)
            SELECT @NUEVO_ID_TR AS ID_GENERADO
        END
        ELSE
        BEGIN
            UPDATE TIPO_RENTA
            SET CODIGO      = @CODIGO,
                DESCRIPCION = @DESCRIPCION,
                QUEDAN      = @QUEDAN,
                RETENCION   = @RETENCION,
                COMPRAS     = @COMPRAS,
                EXTERIOR    = @EXTERIOR,
                VALOR       = @VALOR,
                VALOR_INT   = @VALOR_INT
            WHERE ID_TIPO_RENTA = @ID_TIPO_RENTA
            SELECT @ID_TIPO_RENTA AS ID_GENERADO
        END
    END

    ELSE IF @ACCION = 'ELIMINAR'
        DELETE FROM TIPO_RENTA WHERE ID_TIPO_RENTA = @ID_TIPO_RENTA
END
GO
