USE [PH2]
GO
/****** Object:  StoredProcedure [EDTE].[SP_CREDITOFISCAL_DET]    Script Date: 25/8/2026 15:41:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [EDTE].[SP_CREDITOFISCAL_DET]
    @ACCION            NVARCHAR(50),
    -- Identificadores
    @ID_CCFDET         INT              = NULL,
    @ID_CCFENC         INT              = NULL,
    @ID_EMISOR         INT              = NULL,
    @CODGENERACION     NVARCHAR(40)     = NULL,
    -- Referencia documento
    @ID_TIPO_DTE       INT              = NULL,
    @TPDOC             NVARCHAR(6)      = NULL,
    @FECHA             DATE             = NULL,
    @SALFEC            NVARCHAR(6)      = NULL,
    @NUMDOC            NVARCHAR(15)     = NULL,
    -- Producto
    @ID_PRODUCTO       INT              = NULL,
    @COD_REF           NVARCHAR(15)     = NULL,
    @DESCRIPCION       NVARCHAR(1500)   = NULL,
    @CANTIDAD          NUMERIC(20, 2)   = NULL,
    @ID_UNIDAD_MEDIDA  INT              = NULL,
    @UNIDAD_MEDIDA     NVARCHAR(100)    = NULL,
    @PRECIO            NUMERIC(20, 6)   = NULL,
    -- Descuento
    @DESCUENTO         INT              = NULL,
    @DESCUENTO_VALOR   NUMERIC(20, 2)   = NULL,
    -- Montos
    @ES_EXENTO         BIT              = NULL,
    @EXENTA            NUMERIC(20, 2)   = NULL,
    @GRAVADA           NUMERIC(20, 2)   = NULL,
    @TOTAL             NUMERIC(20, 2)   = NULL,
    -- Historial de integración SIGESTA (solicitud agrícola importada) - 2026-08-25
    @ID_SOLICITUD          INT              = NULL,
    @ID_SOLIC_AGRI_PROD    INT              = NULL,
    @UID_SOLIC_AGRI_PROD   NVARCHAR(40)     = NULL,
    -- Auditoría / estado
    @SELLOAPLICADO     BIT              = NULL,
    @USUARIO           NVARCHAR(50)     = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- =============================================
    -- OBTENER  → una línea de detalle
    -- =============================================
    IF @ACCION = 'OBTENER'
    BEGIN
        SELECT
            D.ID_CCFDET,
            D.ID_CCFENC,
            D.ID_EMISOR,
            D.CODGENERACION,
            D.ID_TIPO_DTE,
            D.TPDOC,
            D.FECHA,
            D.SALFEC,
            D.NUMDOC,
            D.ID_PRODUCTO,
            D.COD_REF,
            D.DESCRIPCION,
            D.CANTIDAD,
            D.ID_UNIDAD_MEDIDA,
            D.UNIDAD_MEDIDA,
            D.PRECIO,
            D.DESCUENTO,
            D.DESCUENTO_VALOR,
            D.ES_EXENTO,
            D.EXENTA,
            D.GRAVADA,
            D.TOTAL,
            D.ID_SOLICITUD,
            D.ID_SOLIC_AGRI_PROD,
            D.UID_SOLIC_AGRI_PROD,
            D.SELLOAPLICADO,
            D.FECHA_CREA,
            D.USER_CREA,
            D.FECHA_ACT,
            D.USER_ACT
        FROM [EDTE].[CREDITOFISCAL_DET] D
        WHERE D.ID_CCFDET = @ID_CCFDET
          AND D.ID_CCFENC  = @ID_CCFENC
          AND D.ID_EMISOR  = @ID_EMISOR;
        RETURN;
    END
    -- =============================================
    -- LISTAR  → todas las líneas de un encabezado
    -- =============================================
    IF @ACCION = 'LISTAR'
    BEGIN
        SELECT
            D.ID_CCFDET,
            D.ID_CCFENC,
            D.ID_EMISOR,
            D.CODGENERACION,
            D.ID_PRODUCTO,
            D.COD_REF,
            D.DESCRIPCION,
            D.CANTIDAD,
            D.UNIDAD_MEDIDA,
            D.PRECIO,
            D.DESCUENTO,
            D.DESCUENTO_VALOR,
            D.ES_EXENTO,
            D.EXENTA,
            D.GRAVADA,
            D.TOTAL,
            D.ID_SOLICITUD,
            D.ID_SOLIC_AGRI_PROD,
            D.UID_SOLIC_AGRI_PROD,
            D.SELLOAPLICADO
        FROM [EDTE].[CREDITOFISCAL_DET] D
        WHERE D.ID_CCFENC = @ID_CCFENC
          AND D.ID_EMISOR  = @ID_EMISOR
        ORDER BY D.ID_CCFDET;
        RETURN;
    END
    -- =============================================
    -- GUARDAR  → INSERT o UPDATE de una línea
    -- =============================================
    IF @ACCION = 'GUARDAR'
    BEGIN
        -- ── UPDATE ──────────────────────────────
        IF ISNULL(@ID_CCFDET, 0) > 0
           AND EXISTS (SELECT 1 FROM [EDTE].[CREDITOFISCAL_DET]
                       WHERE ID_CCFDET = @ID_CCFDET
                         AND ID_CCFENC = @ID_CCFENC
                         AND ID_EMISOR = @ID_EMISOR)
        BEGIN
            UPDATE [EDTE].[CREDITOFISCAL_DET]
            SET
                CODGENERACION     = @CODGENERACION,
                ID_TIPO_DTE = @ID_TIPO_DTE,
                TPDOC             = @TPDOC,
                FECHA             = @FECHA,
                SALFEC            = @SALFEC,
                NUMDOC            = @NUMDOC,
                ID_PRODUCTO       = @ID_PRODUCTO,
                COD_REF           = @COD_REF,
                DESCRIPCION       = @DESCRIPCION,
                CANTIDAD          = ISNULL(@CANTIDAD, 0),
                ID_UNIDAD_MEDIDA  = @ID_UNIDAD_MEDIDA,
                UNIDAD_MEDIDA     = @UNIDAD_MEDIDA,
                PRECIO            = ISNULL(@PRECIO, 0),
                DESCUENTO         = ISNULL(@DESCUENTO, 0),
                DESCUENTO_VALOR   = ISNULL(@DESCUENTO_VALOR, 0),
                ES_EXENTO         = ISNULL(@ES_EXENTO, 0),
                EXENTA            = ISNULL(@EXENTA, 0),
                GRAVADA           = ISNULL(@GRAVADA, 0),
                TOTAL             = ISNULL(@TOTAL, 0),
                ID_SOLICITUD          = @ID_SOLICITUD,
                ID_SOLIC_AGRI_PROD    = @ID_SOLIC_AGRI_PROD,
                UID_SOLIC_AGRI_PROD   = @UID_SOLIC_AGRI_PROD,
                FECHA_ACT         = GETDATE(),
                USER_ACT          = @USUARIO
            WHERE ID_CCFDET = @ID_CCFDET
              AND ID_CCFENC  = @ID_CCFENC
              AND ID_EMISOR  = @ID_EMISOR;
            SELECT @ID_CCFDET AS ID_GENERADO, 'ACTUALIZADO' AS RESULTADO;
        END
        -- ── INSERT ──────────────────────────────
        ELSE
        BEGIN
            DECLARE @NUEVO_ID INT;
            SELECT @NUEVO_ID = ISNULL(MAX(ID_CCFDET), 0) + 1
            FROM [EDTE].[CREDITOFISCAL_DET]
            WHERE ID_CCFENC = @ID_CCFENC
              AND ID_EMISOR  = @ID_EMISOR;
            INSERT INTO [EDTE].[CREDITOFISCAL_DET]
            (
                ID_CCFDET, ID_CCFENC, ID_EMISOR,
                CODGENERACION,
                ID_TIPO_DTE, TPDOC, FECHA, SALFEC, NUMDOC,
                ID_PRODUCTO, COD_REF, DESCRIPCION,
                CANTIDAD, ID_UNIDAD_MEDIDA, UNIDAD_MEDIDA,
                PRECIO, DESCUENTO, DESCUENTO_VALOR,
                ES_EXENTO, EXENTA, GRAVADA, TOTAL,
                ID_SOLICITUD, ID_SOLIC_AGRI_PROD, UID_SOLIC_AGRI_PROD,
                SELLOAPLICADO,
                FECHA_CREA, USER_CREA
            )
            VALUES
            (
                @NUEVO_ID, @ID_CCFENC, @ID_EMISOR,
                @CODGENERACION,
                @ID_TIPO_DTE, @TPDOC, @FECHA, @SALFEC, @NUMDOC,
                @ID_PRODUCTO, @COD_REF, @DESCRIPCION,
                ISNULL(@CANTIDAD, 0), @ID_UNIDAD_MEDIDA, @UNIDAD_MEDIDA,
                ISNULL(@PRECIO, 0), ISNULL(@DESCUENTO, 0), ISNULL(@DESCUENTO_VALOR, 0),
                ISNULL(@ES_EXENTO, 0), ISNULL(@EXENTA, 0), ISNULL(@GRAVADA, 0), ISNULL(@TOTAL, 0),
                @ID_SOLICITUD, @ID_SOLIC_AGRI_PROD, @UID_SOLIC_AGRI_PROD,
                0,
                GETDATE(), @USUARIO
            );
            SELECT @NUEVO_ID AS ID_GENERADO, 'INSERTADO' AS RESULTADO;
        END
        RETURN;
    END
    -- =============================================
    -- GUARDAR_LOTE  → reemplaza todas las líneas
    --   (llama antes de insertar línea a línea;
    --    borra las existentes y reinicia el conteo)
    -- =============================================
    IF @ACCION = 'LIMPIAR_LOTE'
    BEGIN
        DELETE FROM [EDTE].[CREDITOFISCAL_DET]
        WHERE ID_CCFENC = @ID_CCFENC
          AND ID_EMISOR  = @ID_EMISOR;
        SELECT @@ROWCOUNT AS AFECTADOS, 'LOTE_LIMPIADO' AS RESULTADO;
        RETURN;
    END
    -- =============================================
    -- ELIMINAR  → elimina una línea específica
    -- =============================================
    IF @ACCION = 'ELIMINAR'
    BEGIN
        DELETE FROM [EDTE].[CREDITOFISCAL_DET]
        WHERE ID_CCFDET = @ID_CCFDET
          AND ID_CCFENC  = @ID_CCFENC
          AND ID_EMISOR  = @ID_EMISOR;
        SELECT @@ROWCOUNT AS AFECTADOS, 'ELIMINADO' AS RESULTADO;
        RETURN;
    END
    -- =============================================
    -- APLICAR_SELLO  → marca líneas como selladas
    -- =============================================
    IF @ACCION = 'APLICAR_SELLO'
    BEGIN
        UPDATE [EDTE].[CREDITOFISCAL_DET]
        SET
            SELLOAPLICADO = 1,
            FECHA_ACT     = GETDATE(),
            USER_ACT      = @USUARIO
        WHERE ID_CCFENC = @ID_CCFENC
          AND ID_EMISOR  = @ID_EMISOR;
        SELECT @@ROWCOUNT AS AFECTADOS, 'SELLO_APLICADO' AS RESULTADO;
        RETURN;
    END
END
