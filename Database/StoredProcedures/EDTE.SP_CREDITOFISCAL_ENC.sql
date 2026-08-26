USE [PH2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [EDTE].[SP_CREDITOFISCAL_ENC]
    @ACCION                   NVARCHAR(50),
    -- Identificadores
    @ID_CCFENC                INT              = NULL,
    @ID_EMISOR                INT              = NULL,
    @ID_SUCURSAL              INT              = NULL,
    @ID_ALMACEN               INT              = NULL,
    @ID_CAJA                  INT              = NULL,
    @ID_CAJERO                INT              = NULL,
    @ID_CONDPAGO              INT              = NULL,
    @ID_CLIENTE               CHAR(10)         = NULL,
    @COD_REF                  NVARCHAR(15)     = NULL,
    @ID_TIPO_DTE              INT              = NULL,
    -- Documento
    @TPDOC                    NVARCHAR(6)      = NULL,
    @FECHA                    DATE             = NULL,
    @SALFEC                   NVARCHAR(6)      = NULL,
    @NUMDOC                   NVARCHAR(15)     = NULL,
    @NUMINTERNO               NVARCHAR(15)     = NULL,
    @CODGENERACION            NVARCHAR(40)     = NULL,
    @NUMCONTROL               NVARCHAR(40)     = NULL,
    @SELLORECEPCION           NVARCHAR(40)     = NULL,
    @FHPROCESAMIENTO          NVARCHAR(40)     = NULL,
    @FECHA_VENCE              DATE             = NULL,
    @DIAS_CREDITO             INT              = NULL,
    -- Formas de pago
    @RECIB_EFECTIVO           NUMERIC(20, 2)   = NULL,
    @RECIB_REMESA             NUMERIC(20, 2)   = NULL,
    @RECIB_CHEQUE             NUMERIC(20, 2)   = NULL,
    @RECIB_NOTAABONO          NUMERIC(20, 2)   = NULL,
    @RECIB_ANTICIPO           NUMERIC(20, 2)   = NULL,
    @RECIB_EFECTIVO_CAMBIO    NUMERIC(20, 2)   = NULL,
    -- Detalle bancario de formas de pago
    @RECIB_REMESA_BANCO       NVARCHAR(200)    = NULL,
    @RECIB_REMESA_CUENTA      NVARCHAR(50)     = NULL,
    @RECIB_REMESA_MONTO       NUMERIC(20, 2)   = NULL,
    @RECIB_CHEQUE_BANCO       NVARCHAR(200)    = NULL,
    @RECIB_CHEQUE_CUENTA      NVARCHAR(50)     = NULL,
    @RECIB_CHEQUE_MONTO       NUMERIC(20, 2)   = NULL,
    @RECIB_NOTAABONO_BANCO    NVARCHAR(200)    = NULL,
    @RECIB_NOTAABONO_CUENTA   NVARCHAR(50)     = NULL,
    @RECIB_NOTAABONO_MONTO    NUMERIC(20, 2)   = NULL,
    -- Montos
    @AFECTA                   NUMERIC(20, 2)   = NULL,
    @EXCENTA                  NUMERIC(20, 2)   = NULL,
    @DESCUENTO                NUMERIC(20, 2)   = NULL,
    @DESCUENTO_VALOR          NUMERIC(20, 2)   = NULL,
    @SUBTOTAL                 NUMERIC(20, 2)   = NULL,
    @IVA                      NUMERIC(20, 2)   = NULL,
    @IVARETENIDO              NUMERIC(20, 2)   = NULL,
    @IVAPERCIBIDO             NUMERIC(20, 2)   = NULL,
    @TOTALVENTA               NUMERIC(20, 2)   = NULL,
    @TOTALLETRAS              NVARCHAR(500)    = NULL,
    -- Flags / estado
    @AP_PERCEPCION            BIT              = NULL,
    @ID_ESTADO                INT              = NULL,
    @ANULADA                  BIT              = NULL,
    -- Datos adicionales
    @OBSERVACIONES            NVARCHAR(500)    = NULL,
    @TPCONTRIBUYENTE          INT              = NULL,
    @TPCONTRIBUYENTEEMISOR    INT              = NULL,
    @NORDEN_COMPRA            NVARCHAR(10)     = NULL,
    @NIT                      NVARCHAR(17)     = NULL,
    @NRC                      NVARCHAR(15)     = NULL,
    @ID_ZAFRA                 INT              = NULL,
    @ID_CENTRO                INT              = NULL,
    @ID_SOLICITUD             INT              = NULL,
    -- Historial de integración SIGESTA (solicitud agrícola importada) - 2026-08-25
    @NUM_SOLICITUD            NVARCHAR(20)     = NULL,
    @NOMBRE_CUENTA            NVARCHAR(200)    = NULL,
    @UID_SOLIC_AGRICOLA       NVARCHAR(40)     = NULL,
    @TPDOCRECTOR              NVARCHAR(2)      = NULL,
    @NDOCRECTOR               NVARCHAR(25)     = NULL,
    @JSONCOMPLETO             BIT              = NULL,
    @USER_APSELLO             NVARCHAR(50)     = NULL,
    @codigo_empresa           CHAR(10)         = NULL,
    @codigo_emision           INT              = NULL,
    @codigo_comprobante       INT              = NULL,
    @codigo_tipo_doc          INT              = NULL,
    @numero_solicitud         INT              = NULL,
    @numero_cobro             INT              = NULL,
    -- Anulación
    @FECHA_ANULACION          NVARCHAR(40)     = NULL,
    @CODGENERACION_ANULACION  NVARCHAR(40)     = NULL,
    @SELLO_ANULACION          NVARCHAR(40)     = NULL,
    -- Auditoría
    @USUARIO                  NVARCHAR(50)     = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @IDCCF INT
    SET @IDCCF=@ID_CCFENC
    -- =============================================
    -- OBTENER  → encabezado + datos del cliente
    -- =============================================
    IF @ACCION = 'OBTENER'
    BEGIN
        SELECT
            C.ID_CCFENC,
            C.ID_EMISOR,
            C.ID_SUCURSAL,
            C.ID_ALMACEN,
            C.ID_CAJA,
            C.ID_CAJERO,
            C.ID_CONDPAGO,
            C.ID_CLIENTE,
            C.COD_REF,
            C.ID_TIPO_DTE,
            C.TPDOC,
            C.FECHA,
            C.SALFEC,
            C.NUMDOC,
            C.NUMINTERNO,
            C.CODGENERACION,
            C.NUMCONTROL,
            C.SELLORECEPCION,
            C.FHPROCESAMIENTO,
            C.FECHA_VENCE,
            C.DIAS_CREDITO,
            C.RECIB_EFECTIVO,
            C.RECIB_REMESA,
            C.RECIB_CHEQUE,
            C.RECIB_NOTAABONO,
            C.RECIB_ANTICIPO,
            C.RECIB_EFECTIVO_CAMBIO,
            C.RECIB_REMESA_BANCO,
            C.RECIB_REMESA_CUENTA,
            C.RECIB_REMESA_MONTO,
            C.RECIB_CHEQUE_BANCO,
            C.RECIB_CHEQUE_CUENTA,
            C.RECIB_CHEQUE_MONTO,
            C.RECIB_NOTAABONO_BANCO,
            C.RECIB_NOTAABONO_CUENTA,
            C.RECIB_NOTAABONO_MONTO,
            C.AFECTA,
            C.EXCENTA,
            C.DESCUENTO,
            C.DESCUENTO_VALOR,
            C.SUBTOTAL,
            C.IVA,
            C.IVARETENIDO,
            C.IVAPERCIBIDO,
            C.TOTALVENTA,
            C.TOTALLETRAS,
            C.AP_PERCEPCION,
            C.EMAIL_ENVIADO,
            C.ANULADA,
            C.ID_ESTADO,
            C.OBSERVACIONES,
            C.TPCONTRIBUYENTE,
            C.TPCONTRIBUYENTEEMISOR,
            C.NORDEN_COMPRA,
            C.NIT,
            C.NRC,
            C.ID_ZAFRA,
            C.ID_CENTRO,
            C.ID_SOLICITUD,
            C.NUM_SOLICITUD,
            C.NOMBRE_CUENTA,
            C.UID_SOLIC_AGRICOLA,
            C.TPDOCRECTOR,
            C.NDOCRECTOR,
            C.JSONCOMPLETO,
            C.USER_APSELLO,
            C.codigo_empresa,
            C.codigo_emision,
            C.codigo_comprobante,
            C.codigo_tipo_doc,
            C.numero_solicitud,
            C.numero_cobro,
            C.FECHA_ANULACION,
            C.CODGENERACION_ANULACION,
            C.SELLO_ANULACION,
            C.FECHA_CREA,
            C.USER_CREA,
            C.FECHA_ACT,
            C.USER_ACT,
            -- Datos del cliente (entidad)
            E.ID_ENTIDAD    AS ID_ENTIDAD,       -- NUEVO: id real del cliente
            E.CODIGO_ENTIDAD AS CODIGO_ENTIDAD,  -- NUEVO: código legible para mostrar en pantalla
            E.NOMBRE        AS NOMBRE_ENTIDAD,
            E.NIT           AS NIT_ENTIDAD,
            E.NRC           AS NRC_ENTIDAD,
            E.DUI,
            E.TELEFONO      AS CELULAR,
            E.CORREO,
            E.COMPLEMENTO   AS COMPLEMENTO,
            E.CODIPROVEEDOR AS CODIPROVEEDOR, -- NUEVO: para recargar el combo "Solicitud" al reabrir
            TD.NOMBRE       AS TIPO_CONTRIBUYENTE,
            AC.VALORES      AS ACTIVIDAD_PRIMARIA
        FROM [EDTE].[CREDITOFISCAL_ENC] C
        LEFT JOIN [dbo].[ENTIDAD]              E  ON C.ID_CLIENTE        = E.ID_ENTIDAD   -- FIX: ID_CLIENTE ahora guarda ID_ENTIDAD
        LEFT JOIN [dbo].[TIPO_CONTRIBUYENTE]   TD ON C.TPCONTRIBUYENTE   = TD.ID_TIPO_CONTRIB
        LEFT JOIN [dbo].[ACTIVIDAD_ECONOMICA]  AC ON E.ID_ACTIVIDAD_1    = AC.ID_ACTIVIDAD
        WHERE C.ID_CCFENC = @ID_CCFENC
          AND C.ID_EMISOR = @ID_EMISOR;
        RETURN;
    END
    -- =============================================
    -- LISTAR  → lista de CCF por emisor
    -- =============================================
    IF @ACCION = 'LISTAR'
    BEGIN
        SELECT
            C.ID_CCFENC,
            C.ID_EMISOR,
            C.NUMINTERNO,
            C.NUMDOC,
            C.CODGENERACION,
            C.NUMCONTROL,
            C.FECHA,
            C.TPDOC,
            C.COD_REF,
            E.NOMBRE        AS NOMBRE_ENTIDAD,
            C.AFECTA,
            C.EXCENTA,
            C.IVA,
            C.IVARETENIDO,
            C.IVAPERCIBIDO,
            C.TOTALVENTA,
            C.ID_ESTADO,
            C.ANULADA,
            C.SELLORECEPCION
        FROM [EDTE].[CREDITOFISCAL_ENC] C
        LEFT JOIN [dbo].[ENTIDAD] E ON C.ID_CLIENTE = E.ID_ENTIDAD   -- FIX: ID_CLIENTE ahora guarda ID_ENTIDAD
        WHERE C.ID_EMISOR = @ID_EMISOR
        ORDER BY C.FECHA DESC, C.ID_CCFENC DESC;
        RETURN;
    END
    -- =============================================
    -- GUARDAR  → INSERT o UPDATE
    -- =============================================
    IF @ACCION = 'GUARDAR'
    BEGIN
        -- ── UPDATE ──────────────────────────────
        IF ISNULL(@ID_CCFENC, 0) > 0
           AND EXISTS (SELECT 1 FROM [EDTE].[CREDITOFISCAL_ENC]
                       WHERE ID_CCFENC = @ID_CCFENC AND ID_EMISOR = @ID_EMISOR)
        BEGIN
            UPDATE [EDTE].[CREDITOFISCAL_ENC]
            SET
                ID_SUCURSAL           = @ID_SUCURSAL,
                ID_ALMACEN            = @ID_ALMACEN,
                ID_CAJA               = @ID_CAJA,
                ID_CAJERO             = @ID_CAJERO,
                ID_CONDPAGO           = @ID_CONDPAGO,
                ID_CLIENTE            = @ID_CLIENTE,
                COD_REF               = @COD_REF,
                ID_TIPO_DTE           = @ID_TIPO_DTE,
                TPDOC                 = @TPDOC,
                FECHA                 = @FECHA,
                SALFEC                = @SALFEC,
                NUMDOC                = @NUMDOC,
                NUMINTERNO            = @NUMINTERNO,
                CODGENERACION         = @CODGENERACION,
                NUMCONTROL            = @NUMCONTROL,
                SELLORECEPCION        = @SELLORECEPCION,
                FHPROCESAMIENTO       = @FHPROCESAMIENTO,
                FECHA_VENCE           = @FECHA_VENCE,
                DIAS_CREDITO          = @DIAS_CREDITO,
                RECIB_EFECTIVO        = ISNULL(@RECIB_EFECTIVO, 0),
                RECIB_REMESA          = ISNULL(@RECIB_REMESA, 0),
                RECIB_CHEQUE          = ISNULL(@RECIB_CHEQUE, 0),
                RECIB_NOTAABONO       = ISNULL(@RECIB_NOTAABONO, 0),
                RECIB_ANTICIPO        = ISNULL(@RECIB_ANTICIPO, 0),
                RECIB_EFECTIVO_CAMBIO = ISNULL(@RECIB_EFECTIVO_CAMBIO, 0),
                RECIB_REMESA_BANCO    = @RECIB_REMESA_BANCO,
                RECIB_REMESA_CUENTA   = @RECIB_REMESA_CUENTA,
                RECIB_REMESA_MONTO    = ISNULL(@RECIB_REMESA_MONTO, 0),
                RECIB_CHEQUE_BANCO    = @RECIB_CHEQUE_BANCO,
                RECIB_CHEQUE_CUENTA   = @RECIB_CHEQUE_CUENTA,
                RECIB_CHEQUE_MONTO    = ISNULL(@RECIB_CHEQUE_MONTO, 0),
                RECIB_NOTAABONO_BANCO  = @RECIB_NOTAABONO_BANCO,
                RECIB_NOTAABONO_CUENTA = @RECIB_NOTAABONO_CUENTA,
                RECIB_NOTAABONO_MONTO  = ISNULL(@RECIB_NOTAABONO_MONTO, 0),
                AFECTA                = ISNULL(@AFECTA, 0),
                EXCENTA               = ISNULL(@EXCENTA, 0),
                DESCUENTO             = ISNULL(@DESCUENTO, 0),
                DESCUENTO_VALOR       = ISNULL(@DESCUENTO_VALOR, 0),
                SUBTOTAL              = ISNULL(@SUBTOTAL, 0),
                IVA                   = ISNULL(@IVA, 0),
                IVARETENIDO           = ISNULL(@IVARETENIDO, 0),
                IVAPERCIBIDO          = ISNULL(@IVAPERCIBIDO, 0),
                TOTALVENTA            = ISNULL(@TOTALVENTA, 0),
                TOTALLETRAS           = @TOTALLETRAS,
                AP_PERCEPCION         = ISNULL(@AP_PERCEPCION, 0),
                ID_ESTADO             = ISNULL(@ID_ESTADO, 1),
                OBSERVACIONES         = @OBSERVACIONES,
                TPCONTRIBUYENTE       = @TPCONTRIBUYENTE,
                TPCONTRIBUYENTEEMISOR = @TPCONTRIBUYENTEEMISOR,
                NORDEN_COMPRA         = @NORDEN_COMPRA,
                NIT                   = @NIT,
                NRC                   = @NRC,
                ID_ZAFRA              = @ID_ZAFRA,
                ID_CENTRO             = @ID_CENTRO,
                ID_SOLICITUD          = @ID_SOLICITUD,
                NUM_SOLICITUD         = @NUM_SOLICITUD,
                NOMBRE_CUENTA         = @NOMBRE_CUENTA,
                UID_SOLIC_AGRICOLA    = @UID_SOLIC_AGRICOLA,
                TPDOCRECTOR           = @TPDOCRECTOR,
                NDOCRECTOR            = @NDOCRECTOR,
                codigo_empresa        = @codigo_empresa,
                codigo_emision        = @codigo_emision,
                codigo_comprobante    = @codigo_comprobante,
                codigo_tipo_doc       = @codigo_tipo_doc,
                numero_solicitud      = @numero_solicitud,
                numero_cobro          = @numero_cobro,
                FECHA_ACT             = GETDATE(),
                USER_ACT              = @USUARIO
            WHERE ID_CCFENC = @ID_CCFENC
              AND ID_EMISOR  = @ID_EMISOR;
            SELECT @ID_CCFENC AS ID_GENERADO,
                   NUMCONTROL AS NCONT,
                   NUMINTERNO AS INTERN
            FROM [EDTE].[CREDITOFISCAL_ENC]
            WHERE ID_CCFENC = @ID_CCFENC AND ID_EMISOR = @ID_EMISOR;
        END
        -- ── INSERT ──────────────────────────────
        ELSE
        BEGIN
            BEGIN TRY
                BEGIN TRAN
                DECLARE @NUEVO_ID_CCFENC INT,
                        @NUMCONTROL_     NVARCHAR(40),
                        @NUMINTERNO_     NVARCHAR(15),
                        @CODSUCURSAL     NVARCHAR(8);
                -- Siguiente ID con bloqueo para evitar duplicados concurrentes
                SELECT @NUEVO_ID_CCFENC = ISNULL(MAX(ID_CCFENC), 0) + 1
                FROM [EDTE].[CREDITOFISCAL_ENC] WITH (UPDLOCK, HOLDLOCK);
                -- Código de sucursal para armar el NUMCONTROL
                SELECT @CODSUCURSAL = CODSUCURSAL
                FROM [dbo].[SUCURSAL]
                WHERE ID_SUCURSAL = @ID_SUCURSAL;
                -- Generar NUMCONTROL y NUMINTERNO desde la función CCF
                SET @NUMCONTROL_ = (SELECT NUMCONTROL FROM [EMH].[F_NUMCONTROL_CCF](@CODSUCURSAL));
                SET @NUMINTERNO_ = (SELECT NUMINTERNO FROM [EMH].[F_NUMCONTROL_CCF](@CODSUCURSAL));
                INSERT INTO [EDTE].[CREDITOFISCAL_ENC]
                (
                    ID_CCFENC, ID_EMISOR, ID_SUCURSAL, ID_ALMACEN,
                    ID_CAJA, ID_CAJERO, ID_CONDPAGO,
                    ID_CLIENTE, COD_REF, ID_TIPO_DTE,
                    TPDOC, FECHA, SALFEC, NUMDOC, NUMINTERNO,
                    CODGENERACION, NUMCONTROL, SELLORECEPCION,
                    FHPROCESAMIENTO, FECHA_VENCE, DIAS_CREDITO,
                    RECIB_EFECTIVO, RECIB_REMESA, RECIB_CHEQUE,
                    RECIB_NOTAABONO, RECIB_ANTICIPO, RECIB_EFECTIVO_CAMBIO,
                    RECIB_REMESA_BANCO, RECIB_REMESA_CUENTA, RECIB_REMESA_MONTO,
                    RECIB_CHEQUE_BANCO, RECIB_CHEQUE_CUENTA, RECIB_CHEQUE_MONTO,
                    RECIB_NOTAABONO_BANCO, RECIB_NOTAABONO_CUENTA, RECIB_NOTAABONO_MONTO,
                    AFECTA, EXCENTA, DESCUENTO, DESCUENTO_VALOR,
                    SUBTOTAL, IVA, IVARETENIDO, IVAPERCIBIDO,
                    TOTALVENTA, TOTALLETRAS,
                    AP_PERCEPCION, EMAIL_ENVIADO, ANULADA, ID_ESTADO,
                    OBSERVACIONES, TPCONTRIBUYENTE, TPCONTRIBUYENTEEMISOR,
                    NORDEN_COMPRA, NIT, NRC,
                    ID_ZAFRA, ID_CENTRO, ID_SOLICITUD,
                    NUM_SOLICITUD, NOMBRE_CUENTA, UID_SOLIC_AGRICOLA,
                    TPDOCRECTOR, NDOCRECTOR,
                    codigo_empresa, codigo_emision,
                    codigo_comprobante, codigo_tipo_doc,
                    numero_solicitud, numero_cobro,
                    FECHA_CREA, USER_CREA, FECHAID, USERC_ID
                )
                VALUES
                (
                    @NUEVO_ID_CCFENC, @ID_EMISOR, @ID_SUCURSAL, @ID_ALMACEN,
                    @ID_CAJA, @ID_CAJERO, @ID_CONDPAGO,
                    @ID_CLIENTE, @COD_REF, @ID_TIPO_DTE,
                    @TPDOC, @FECHA, @SALFEC, CAST(@NUMINTERNO_ AS INT), @NUMINTERNO_,
                    CASE WHEN ISNULL(@CODGENERACION, '') = '' THEN CONVERT(NVARCHAR(40), NEWID()) ELSE @CODGENERACION END,
                    @NUMCONTROL_, @SELLORECEPCION,
                    @FHPROCESAMIENTO, @FECHA_VENCE, @DIAS_CREDITO,
                    ISNULL(@RECIB_EFECTIVO, 0), ISNULL(@RECIB_REMESA, 0), ISNULL(@RECIB_CHEQUE, 0),
                    ISNULL(@RECIB_NOTAABONO, 0), ISNULL(@RECIB_ANTICIPO, 0), ISNULL(@RECIB_EFECTIVO_CAMBIO, 0),
                    @RECIB_REMESA_BANCO, @RECIB_REMESA_CUENTA, ISNULL(@RECIB_REMESA_MONTO, 0),
                    @RECIB_CHEQUE_BANCO, @RECIB_CHEQUE_CUENTA, ISNULL(@RECIB_CHEQUE_MONTO, 0),
                    @RECIB_NOTAABONO_BANCO, @RECIB_NOTAABONO_CUENTA, ISNULL(@RECIB_NOTAABONO_MONTO, 0),
                    ISNULL(@AFECTA, 0), ISNULL(@EXCENTA, 0), ISNULL(@DESCUENTO, 0), ISNULL(@DESCUENTO_VALOR, 0),
                    ISNULL(@SUBTOTAL, 0), ISNULL(@IVA, 0), ISNULL(@IVARETENIDO, 0), ISNULL(@IVAPERCIBIDO, 0),
                    ISNULL(@TOTALVENTA, 0),
                    [EDTE].[fNumero_a_Letras](@TOTALVENTA, ' DOLARES DE ESTADOS UNIDOS DE AMERICA'),
                    ISNULL(@AP_PERCEPCION, 0), 0, 0, ISNULL(@ID_ESTADO, 1),
                    @OBSERVACIONES, @TPCONTRIBUYENTE, @TPCONTRIBUYENTEEMISOR,
                    @NORDEN_COMPRA, @NIT, @NRC,
                    @ID_ZAFRA, @ID_CENTRO, @ID_SOLICITUD,
                    @NUM_SOLICITUD, @NOMBRE_CUENTA, @UID_SOLIC_AGRICOLA,
                    @TPDOCRECTOR, @NDOCRECTOR,
                    @codigo_empresa, @codigo_emision,
                    @codigo_comprobante, @codigo_tipo_doc,
                    @numero_solicitud, @numero_cobro,
                    GETDATE(), @USUARIO, GETDATE(), @USUARIO
                );
                -- Actualizar el último número asignado en DOCUMENTO_NUMERACION para CCF
                UPDATE [dbo].[DOCUMENTO_NUMERACION]
                SET ULT_NUM_ASIGNADO = CAST(@NUMINTERNO_ AS INT)
                WHERE ESTADO = 1 AND ID_TIPO_DTE = 2;
                -- FIX: este SELECT ahora va ANTES del EXEC de abajo, para que
                -- sea el primer result set que reciba el C# (antes iba después
                -- y por eso fallaba "La columna 'ID_GENERADO' no pertenece a
                -- la tabla").
                EXEC [EDTE].[SP_CREDITOFISCAL_JSON] @ID_CCFENC = @IDCCF, @ID_EMISOR = 1
                SELECT @NUEVO_ID_CCFENC          AS ID_GENERADO,
                       @NUMCONTROL_               AS NCONT,
                       CAST(@NUMINTERNO_ AS INT)  AS INTERN;
                COMMIT TRAN
            END TRY
            BEGIN CATCH
                IF @@TRANCOUNT > 0 ROLLBACK TRAN;
                ;THROW
            END CATCH
        END
        RETURN;
    END
    -- =============================================
    -- ANULAR  → marca como anulada
    -- =============================================
    IF @ACCION = 'ANULAR'
    BEGIN
        UPDATE [EDTE].[CREDITOFISCAL_ENC]
        SET
            ANULADA                  = 1,
            ID_ESTADO                = 0,
            FECHA_ANULACION          = @FECHA_ANULACION,
            CODGENERACION_ANULACION  = @CODGENERACION_ANULACION,
            SELLO_ANULACION          = @SELLO_ANULACION,
            FECHA_ACT                = GETDATE(),
            USER_ACT                 = @USUARIO
        WHERE ID_CCFENC = @ID_CCFENC
          AND ID_EMISOR  = @ID_EMISOR;
        SELECT @@ROWCOUNT AS AFECTADOS, 'ANULADO' AS RESULTADO;
        RETURN;
    END
    -- =============================================
    -- ELIMINAR  → elimina físicamente (solo borradores)
    -- =============================================
    IF @ACCION = 'ELIMINAR'
    BEGIN
        DELETE FROM [EDTE].[CREDITOFISCAL_ENC]
        WHERE ID_CCFENC = @ID_CCFENC
          AND ID_EMISOR  = @ID_EMISOR;
        SELECT @@ROWCOUNT AS AFECTADOS, 'ELIMINADO' AS RESULTADO;
        RETURN;
    END
    -- =============================================
    -- APLICAR_SELLO  → graba sello de recepción del MH
    -- =============================================
    IF @ACCION = 'APLICAR_SELLO'
    BEGIN
        UPDATE [EDTE].[CREDITOFISCAL_ENC]
        SET
            SELLORECEPCION   = @SELLORECEPCION,
            FHPROCESAMIENTO  = @FHPROCESAMIENTO,
            ID_ESTADO        = 2,           -- validado
            FECHAID          = GETDATE(),
            USERC_ID         = @USUARIO,
            USER_APSELLO     = @USUARIO
        WHERE ID_CCFENC = @ID_CCFENC
          AND ID_EMISOR  = @ID_EMISOR;
        SELECT @@ROWCOUNT AS AFECTADOS, 'SELLO_APLICADO' AS RESULTADO;
        RETURN;
    END
END
