USE [PH2];
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

-- ----------------------------------------------------------------
-- SP: TIPO_DTE
-- ----------------------------------------------------------------
CREATE OR ALTER PROCEDURE [EPROVEEDOR].[SP_TIPO_DTE]
    @ACCION       VARCHAR(100),
    @ID_TIPO_DTE  INT           = NULL,
    @TIPO_DTE     NVARCHAR(2)   = NULL,
    @VALORES      NVARCHAR(200) = NULL,
    @ABREVIATURA  NVARCHAR(6)   = NULL,
    @ABREVIATURA_E NVARCHAR(6)  = NULL,
    @DESCRIPCION  NVARCHAR(200) = NULL,
    @ES_FISCAL    BIT           = NULL,
    @ES_INTERNO   BIT           = NULL,
    @FILTRO       NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @ACCION = 'BUSCAR'
    BEGIN
        SELECT TOP 20
            ID_TIPO_DTE,
            TIPO_DTE,
            ABREVIATURA,
            ABREVIATURA_E,
            DESCRIPCION,
            ES_FISCAL,
            ES_INTERNO
        FROM TIPO_DTE
        WHERE (@FILTRO IS NULL
               OR DESCRIPCION    LIKE '%' + @FILTRO + '%'
               OR ABREVIATURA    LIKE '%' + @FILTRO + '%'
               OR ABREVIATURA_E  LIKE '%' + @FILTRO + '%'
               OR TIPO_DTE       LIKE '%' + @FILTRO + '%')
        ORDER BY DESCRIPCION
    END
	ELSE IF @ACCION = 'BUSCAR_FSE'
	BEGIN
		-- Solo DTEs sujeto excluido (para uso en documentos de compra)
		SELECT 
			ID_TIPO_DTE,
			TIPO_DTE,
			TIPO_DTE + '-' + VALORES AS ABREVIATURA,
			ABREVIATURA_E,
			DESCRIPCION
		FROM TIPO_DTE
		WHERE  ES_FISCAL = 1 AND ID_TIPO_DTE = 10
		ORDER BY DESCRIPCION
	END

    ELSE IF @ACCION = 'BUSCAR_FISCAL'
    BEGIN
        -- Solo DTEs fiscales (para uso en documentos de compra/venta)
        SELECT TOP 20
            ID_TIPO_DTE,
            TIPO_DTE,
            ABREVIATURA,
            ABREVIATURA_E,
            DESCRIPCION
        FROM TIPO_DTE
        WHERE ES_FISCAL = 1
          AND (@FILTRO IS NULL
               OR DESCRIPCION   LIKE '%' + @FILTRO + '%'
               OR ABREVIATURA   LIKE '%' + @FILTRO + '%')
        ORDER BY DESCRIPCION
    END
	
     ELSE IF @ACCION = 'BUSCAR_FISCAL_COMPRAS_CREDITO'
    BEGIN
        -- Solo DTEs fiscales (para uso en documentos de compra/venta)
        SELECT TOP 20
            ID_TIPO_DTE,
            TIPO_DTE,
            TIPO_DTE + '-' + VALORES AS ABREVIATURA,
            ABREVIATURA_E,
            DESCRIPCION
        FROM TIPO_DTE
        WHERE ES_FISCAL = 1 AND ID_TIPO_DTE IN(2,21)
          AND (@FILTRO IS NULL
               OR DESCRIPCION   LIKE '%' + @FILTRO + '%'
               OR ABREVIATURA   LIKE '%' + @FILTRO + '%')
        ORDER BY ID_TIPO_DTE
    END
     ELSE IF @ACCION = 'BUSCAR_FISCAL_NOTA_CRED_DEB'
    BEGIN
        -- Solo DTEs fiscales (para uso en documentos de compra/venta)
        SELECT TOP 20
            ID_TIPO_DTE,
            TIPO_DTE,
            TIPO_DTE + '-' + VALORES AS ABREVIATURA,
            ABREVIATURA_E,
            DESCRIPCION
        FROM TIPO_DTE
        WHERE ES_FISCAL = 1 AND ID_TIPO_DTE IN(4,22)
          AND (@FILTRO IS NULL
               OR DESCRIPCION   LIKE '%' + @FILTRO + '%'
               OR ABREVIATURA   LIKE '%' + @FILTRO + '%')
        ORDER BY ID_TIPO_DTE        
    END

    ELSE IF @ACCION = 'BUSCAR_INTERNO'
    BEGIN
        -- Solo documentos internos (quedan, cheque, etc.)
        SELECT TOP 20
            ID_TIPO_DTE,
            TIPO_DTE,
            ABREVIATURA,
            DESCRIPCION
        FROM TIPO_DTE
        WHERE ES_INTERNO = 1
          AND (@FILTRO IS NULL
               OR DESCRIPCION LIKE '%' + @FILTRO + '%')
        ORDER BY DESCRIPCION
    END

    ELSE IF @ACCION = 'OBTENER'
    BEGIN
        SELECT
            ID_TIPO_DTE, TIPO_DTE, VALORES, ABREVIATURA,
            ABREVIATURA_E, DESCRIPCION, ES_FISCAL, ES_INTERNO
        FROM TIPO_DTE
        WHERE ID_TIPO_DTE = @ID_TIPO_DTE
    END

    ELSE IF @ACCION = 'GUARDAR'
    BEGIN
        DECLARE @NUEVO_ID_DTE INT
        IF @ID_TIPO_DTE = 0
        BEGIN
            SELECT @NUEVO_ID_DTE = ISNULL(MAX(ID_TIPO_DTE), 0) + 1
            FROM TIPO_DTE

            INSERT INTO TIPO_DTE
                (ID_TIPO_DTE, TIPO_DTE, VALORES, ABREVIATURA,
                 ABREVIATURA_E, DESCRIPCION, ES_FISCAL, ES_INTERNO)
            VALUES
                (@NUEVO_ID_DTE, @TIPO_DTE, @VALORES, @ABREVIATURA,
                 @ABREVIATURA_E, @DESCRIPCION, @ES_FISCAL, @ES_INTERNO)

            SELECT @NUEVO_ID_DTE AS ID_GENERADO
        END
        ELSE
        BEGIN
            UPDATE TIPO_DTE SET
                TIPO_DTE     = @TIPO_DTE,
                VALORES      = @VALORES,
                ABREVIATURA  = @ABREVIATURA,
                ABREVIATURA_E= @ABREVIATURA_E,
                DESCRIPCION  = @DESCRIPCION,
                ES_FISCAL    = @ES_FISCAL,
                ES_INTERNO   = @ES_INTERNO
            WHERE ID_TIPO_DTE = @ID_TIPO_DTE

            SELECT @ID_TIPO_DTE AS ID_GENERADO
        END
    END

    ELSE IF @ACCION = 'ELIMINAR'
    BEGIN
        DELETE FROM TIPO_DTE WHERE ID_TIPO_DTE = @ID_TIPO_DTE
    END
END
GO
