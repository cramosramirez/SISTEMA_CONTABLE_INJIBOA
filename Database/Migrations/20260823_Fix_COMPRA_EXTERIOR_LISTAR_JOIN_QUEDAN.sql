-- ============================================================================
-- Fix: [EPROVEEDOR].[SP_COMPRA_EXTERIOR] - rama COMPRA_EXTERIOR_LISTAR
--
-- Sintoma: frmDocumentoCompraExterior guarda correctamente, pero el
-- documento nunca aparece en frmConsultaCompraExterior (grid vacio).
--
-- Causa raiz (tres problemas combinados):
--   1) frmConsultaCompraExterior.cs invocaba ACCION = 'QUEDAN_DETALLE_LISTAR',
--      nombre que ya no existe en el SP (fue renombrado a
--      'COMPRA_EXTERIOR_LISTAR'). Al no coincidir ninguna rama IF/ELSE IF,
--      el SP no ejecutaba ningun SELECT y regresaba un DataTable vacio.
--      -> Corregido en el codigo C# (ya aplicado en frmConsultaCompraExterior.cs).
--   2) La rama tenia:
--         INNER JOIN QUEDAN Q ON Q.ID_QUEDAN = C.ID_QUEDAN
--         WHERE ... AND C.ID_QUEDAN IS NULL
--      El INNER JOIN exige C.ID_QUEDAN NOT NULL, mientras el WHERE exige
--      C.ID_QUEDAN IS NULL: condicion imposible, 0 filas siempre.
--      Ese "AND C.ID_QUEDAN IS NULL" se copio por error desde el bloque
--      LISTAR_COMPRA_X_CAJA_CHICA (que si necesita documentos SIN Quedan).
--   3) El filtro WHERE C.ID_TIPO_DTE IN (2,21,4,22,23,24,25) tambien se
--      copio de otra rama (tipos de documento NACIONALES). Los documentos
--      reales de Compra Exterior usan sus propios TIPO_DTE (confirmado:
--      ID_TIPO_DTE = 29, ABREVIATURA = 'INV', "INVOICE DE TRANSPORTE"),
--      que no estaban en esa lista. Se reemplaza por una exclusion: como
--      [EPROVEEDOR].[COMPRA_EXTERIOR] ya es una tabla exclusiva de Compra
--      Exterior, el listado principal debe mostrar todo excepto las
--      Notas de Credito/Debito (que ya tienen su propia consulta en
--      NOTA_CRED_DEB_DETALLE_LISTAR, ID_TIPO_DTE IN (4,5,22,23)). Esto
--      evita tener que enumerar cada tipo de documento nuevo a futuro.
--
-- Verificado con datos reales (PH2):
--   ID_COMPRA_EXTERIOR 1 y 2, ID_TIPO_DTE = 29 (INV) - antes excluidos
--   por el filtro viejo, ahora incluidos.
--
-- Fecha: 2026-08-23
-- Confirmado por: Roberto
USE [PH2];
-- ============================================================================
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
-- ----------------------------------------------------------------
-- SP: [EPROVEEDOR].[SP_COMPRA_EXTERIOR]
-- ----------------------------------------------------------------
CREATE OR ALTER PROCEDURE [EPROVEEDOR].[SP_COMPRA_EXTERIOR]
    @ACCION             VARCHAR(100),
    @ID_COMPRA_EXTERIOR      INT            = NULL,
    @ID_QUEDAN_CEX      INT            = NULL,
    @NUM_QUEDAN         INT            = NULL,        
    @ID_CHEQUE          INT            = NULL,
    @NUM_CHEQUE         INT            = NULL,    
    @ID_TIPO_DTE        INT            = NULL,
    @NUM_CONTROL        NVARCHAR(60)   = NULL,
    @COD_GENERACION     NVARCHAR(60)   = NULL,
    @SELLO_RECIBIDO     NVARCHAR(60)   = NULL,
    @FECHA_EMISION      DATETIME       = NULL,
    @TIPO_MONEDA        NVARCHAR(3)    = NULL,
    @ID_ENTIDAD         INT            = NULL,
    @CODIGO_ENTIDAD     NVARCHAR(20)   = NULL,
    @FECHA_RECIBIDO     DATETIME       = NULL,
    @FECHA_VENCE        DATETIME       = NULL,
    @ORDEN              NVARCHAR(50)   = NULL,
    @ID_SUCURSAL        INT            = NULL,
    @ID_TIPO_SERVI      INT            = NULL,
    @ID_TIPO_IMPORTACION INT           = NULL,
    @ID_TIPO_OPERA      INT            = NULL,
    @ID_CLASIFICA       INT            = NULL,
    @ID_SECTOR          INT            = NULL,
    @ID_TIPO_COSTO      INT            = NULL,
    @NO_SUJETA          NUMERIC(20,2)  = 0,
    @EXENTA             NUMERIC(20,2)  = 0,
    @GRAVADA            NUMERIC(20,2)  = 0,    
    @IVA                NUMERIC(20,2)  = 0,
    @FOVIAL             NUMERIC(20,2)  = 0,
    @COTRANS            NUMERIC(20,2)  = 0,
    @TOTAL              NUMERIC(20,2)  = 0,
    @CARGO              NUMERIC(20,2)  = 0,
    @ABONO              NUMERIC(20,2)  = 0,
    @RENTA              NUMERIC(20,2)  = 0,
    @IVAR               NUMERIC(20,2)  = 0,
    @SALDO              NUMERIC(20,2)  = 0,
    @ID_TIPO_RENTA      INT             = NULL,
    @APLICABLE_RENTA    NUMERIC(20, 2)  = NULL,
    @ID_COMPROBANTE_RET INT            = NULL,
    @ID_CCF_COMPRA_ASOCIADO INT        = NULL,
    @OBSERVACION        NVARCHAR(1000) = NULL,
    @USUARIO            NVARCHAR(100) = NULL,
    @UID_ENLACE_CHEQUE  NVARCHAR(60) = ''
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NUEVO_ID_COMPRA_EXTERIOR INT
    DECLARE @ID_QUEDAN_CEX_DEST INT = ISNULL(@ID_QUEDAN_CEX,0)
    DECLARE @NUM_QUEDAN_DEST    INT = ISNULL(@NUM_QUEDAN,0)
    DECLARE @ID_TIPO_DTE_CEX    INT
    DECLARE @ID_COMP_RET        INT
    DECLARE @NUEVO_CARGO        NUMERIC(20,2)
    DECLARE @NUEVO_ABONO        NUMERIC(20,2) 
    DECLARE @AHORA              DATETIME =
                CONVERT(DATETIME, CONVERT(CHAR(19), GETDATE(), 120), 120)
    IF @ACCION = 'OBTENER'
     BEGIN
        SELECT
            C.ID_COMPRA_EXTERIOR, C.ID_QUEDAN, C.NUM_QUEDAN,            
            C.ID_TIPO_DTE, C.NUM_CONTROL, C.COD_GENERACION, C.SELLO_RECIBIDO,
            C.FECHA_EMISION, C.TIPO_MONEDA,
            C.ID_ENTIDAD, C.CODIGO_ENTIDAD,
            C.FECHA_RECIBIDO, C.FECHA_VENCE, C.ORDEN,
            C.ID_SUCURSAL, C.ID_TIPO_SERVI, C.ID_TIPO_IMPORTACION, C.ID_TIPO_OPERA,
            C.ID_CLASIFICA, C.ID_SECTOR, C.ID_TIPO_COSTO, C.ID_TIPO_RENTA,
            C.NO_SUJETA, C.EXENTA, C.GRAVADA, C.IVA,
            C.FOVIAL, C.COTRANS, C.TOTAL,
            C.CARGO, C.ABONO,
            C.APLICABLE_RENTA, C.RENTA, C.IVAR, C.SALDO,
            C.ID_COMPROBANTE_RET, C.ID_CCF_COMPRA_ASOCIADO,
            C.OBSERVACION, C.UID_ENLACE_CHEQUE,
            C.USUARIO_CREA, C.FECHA_CREA, C.USUARIO_ACT, C.FECHA_ACT,
            -- Datos para el form
            E.NOMBRE         AS NOMBRE_ENTIDAD,
            E.ID_TIPO_ENTIDAD,
            E.NRC,
            E.NIT,
            E.ID_TIPO_CONTRIB,
            E.CORREO,
            E.TELEFONO,
            E.CELULAR,
            E.COMPLEMENTO,
            TC.NOMBRE AS TIPO_CONTRIBUYENTE,
             (SELECT T.CODI_MH + ' - ' + T.VALORES FROM ACTIVIDAD_ECONOMICA T WHERE T.ID_ACTIVIDAD = E.ID_ACTIVIDAD_1) AS ACTIVIDAD_PRIMARIA,
            TD.ABREVIATURA   AS TIPO_DTE_ABREV,
            CASE 
                WHEN EXISTS(  SELECT 1
                        FROM COMPROBANTE_RETENCION X 
                        WHERE X.ID_COMPROBANTE_RET = C.ID_COMPROBANTE_RET AND ISNULL(X.SELLO_RECIBIDO,'') = '') THEN 1
                ELSE 0
            END AS VALIDAR_COMPIVAR,
            -- Datos para el form - CCF al que aplica en caso de que sea NC/ND
            (SELECT  TIPO_DTE + '-' + VALORES FROM TIPO_DTE T WHERE T.ID_TIPO_DTE = X.ID_TIPO_DTE) AS TIPO_DTE_CCF,
            (X.SELLO_RECIBIDO) AS SELLO_RECIBIDO_CCF,
            (X.COD_GENERACION) AS COD_GENERACION_CCF,
            (X.NUM_CONTROL) AS NUM_CONTROL_CCF,
            (X.FECHA_EMISION) AS FECHA_EMISION_CCF,
            (X.FECHA_RECIBIDO) AS FECHA_RECIBIDO_CCF,
            (X.FECHA_VENCE) AS FECHA_VENCE_CCF,
            (X.ORDEN) AS ORDEN_CCF,
            (SELECT T.NOMBRE FROM SUCURSAL T WHERE T.ID_SUCURSAL = X.ID_SUCURSAL) AS SUCURSAL_CCF
        FROM [EPROVEEDOR].[COMPRA_EXTERIOR] C
        LEFT JOIN ENTIDAD  E  ON E.ID_ENTIDAD   = C.ID_ENTIDAD
        LEFT JOIN TIPO_DTE TD ON TD.ID_TIPO_DTE = C.ID_TIPO_DTE
        LEFT JOIN TIPO_PERSONA TP ON E.ID_TIPO_ENTIDAD = TP.ID_TIPO_PERSONA
        LEFT JOIN TIPO_CONTRIBUYENTE TC ON E.ID_TIPO_CONTRIB = TC.ID_TIPO_CONTRIB 
        LEFT JOIN [EPROVEEDOR].[COMPRA_EXTERIOR] X ON X.ID_COMPRA_EXTERIOR = C.ID_CCF_COMPRA_ASOCIADO
        WHERE C.ID_COMPRA_EXTERIOR = @ID_COMPRA_EXTERIOR
    END
    ELSE IF @ACCION = 'COMPRA_EXTERIOR_LISTAR'
        BEGIN
            SELECT
                Q.ID_QUEDAN,
                C.ID_TIPO_DTE,
                Q.NUM_QUEDAN,
                C.ID_COMPRA_EXTERIOR,
                E.NOMBRE        AS NOMBRE_ENTIDAD,
                TD.ABREVIATURA  AS TIPO_DTE,
                C.NUM_CONTROL,
                C.COD_GENERACION,
                C.FECHA_EMISION,
                C.FECHA_RECIBIDO,
                C.FECHA_VENCE,
                C.SALDO,    
                C.TOTAL,
                C.ID_COMPROBANTE_RET
            FROM [EPROVEEDOR].[COMPRA_EXTERIOR] C
            INNER JOIN QUEDAN   Q  ON Q.ID_QUEDAN   = C.ID_QUEDAN
            INNER JOIN TIPO_DTE TD ON TD.ID_TIPO_DTE = C.ID_TIPO_DTE
            LEFT  JOIN ENTIDAD  E  ON E.ID_ENTIDAD  = C.ID_ENTIDAD      
            WHERE C.ID_TIPO_DTE NOT IN (4,5,22,23) -- excluir NC/ND, que tienen su propia consulta (ver NOTA_CRED_DEB_DETALLE_LISTAR)
            ORDER BY Q.NUM_QUEDAN DESC, C.ID_COMPRA_EXTERIOR ASC, C.FECHA_EMISION
        END   
	ELSE IF @ACCION = 'OBTENER_CHEQUE'
		BEGIN
		SELECT 
			CH.NUM_CHEQUE,
			CH.FECHA_CHEQUE
		FROM [EPROVEEDOR].[COMPRA_EXTERIOR] C, CHEQUE CH
		WHERE C.ID_COMPRA_EXTERIOR = @ID_COMPRA_EXTERIOR
			AND C.ID_CHEQUE = CH.ID_CHEQUE
		END
    ELSE IF @ACCION = 'LISTAR_COMPRA_X_CAJA_CHICA'
        BEGIN
            SELECT                
                C.ID_TIPO_DTE,                
                C.ID_COMPRA_EXTERIOR,
                E.NOMBRE        AS NOMBRE_ENTIDAD,
                TD.ABREVIATURA  AS TIPO_DTE,
                C.NUM_CONTROL,
                C.COD_GENERACION,
                C.FECHA_EMISION,
                C.FECHA_RECIBIDO,
                C.FECHA_VENCE,
                C.SALDO,    
                C.TOTAL
            FROM [EPROVEEDOR].[COMPRA_EXTERIOR] C            
            INNER JOIN TIPO_DTE TD ON TD.ID_TIPO_DTE = C.ID_TIPO_DTE
            LEFT  JOIN ENTIDAD  E  ON E.ID_ENTIDAD  = C.ID_ENTIDAD      
            /* Mostrar solo                        
                COMPROBANTE DE CREDITO FISCAL (ELECTRONICO / FISICO)
                NOTA DE CREDITO FISCAL (ELECTRONICO / FISICO)
                NOTA DE DEBITO
                DECLARACION DE MERCANCIA
                RECIBO DE IMPORTACION
            */            
            WHERE C.ID_TIPO_DTE IN (2,21,4,22,23,24,25)   
            AND C.ID_QUEDAN IS NULL            
            ORDER BY C.ID_COMPRA_EXTERIOR ASC, C.FECHA_EMISION
        END
    ELSE IF @ACCION = 'NOTA_CRED_DEB_DETALLE_LISTAR'
        BEGIN
            SELECT
                Q.ID_QUEDAN,
                Q.NUM_QUEDAN,
                C.ID_COMPRA_EXTERIOR,
                E.NOMBRE        AS NOMBRE_ENTIDAD,
                TD.ABREVIATURA  AS TIPO_DTE,               
                C.NUM_CONTROL,
                C.COD_GENERACION,
                (SELECT Y.ABREVIATURA FROM [EPROVEEDOR].[COMPRA_EXTERIOR] X, TIPO_DTE Y WHERE X.ID_COMPRA_EXTERIOR = C.ID_CCF_COMPRA_ASOCIADO AND Y.ID_TIPO_DTE = X.ID_TIPO_DTE) AS TIPO_DTE_APLICADO,
                (SELECT X.COD_GENERACION FROM [EPROVEEDOR].[COMPRA_EXTERIOR] X WHERE X.ID_COMPRA_EXTERIOR = C.ID_CCF_COMPRA_ASOCIADO) AS COD_GENERACION_APLICADO,
                C.FECHA_EMISION,
                C.FECHA_RECIBIDO,
                C.FECHA_VENCE,
                C.SALDO,    
                C.TOTAL
            FROM [EPROVEEDOR].[COMPRA_EXTERIOR] C
            INNER JOIN QUEDAN   Q  ON Q.ID_QUEDAN   = C.ID_QUEDAN
            INNER JOIN TIPO_DTE TD ON TD.ID_TIPO_DTE = C.ID_TIPO_DTE
            LEFT  JOIN ENTIDAD  E  ON E.ID_ENTIDAD  = C.ID_ENTIDAD            
            WHERE C.ID_TIPO_DTE IN(4,5,22,23) -- MOSTRAR SOLO NOTA DE CREDITO / DEBITO
            ORDER BY C.FECHA_RECIBIDO DESC            
        END
   ELSE IF @ACCION = 'GUARDAR'
    BEGIN      
 
        IF @ID_COMPRA_EXTERIOR = 0
            BEGIN
                BEGIN TRY
                    BEGIN TRAN
 
                     -- Detectar si es modo contado (viene UID y no Quedan)
                    DECLARE @ES_CONTADO BIT = 0
                    IF ISNULL(@UID_ENLACE_CHEQUE, '') <> '' AND ISNULL(@ID_QUEDAN_CEX, 0) = 0
                        SET @ES_CONTADO = 1
                    -- 1) Si NO es contado y no viene Quedan, crearlo 
                    IF @ES_CONTADO = 0 AND @ID_QUEDAN_CEX_DEST = 0
                        BEGIN
                            SELECT @ID_TIPO_DTE_CEX = ID_TIPO_DTE
                            FROM TIPO_DTE
                            WHERE ABREVIATURA = 'CEX'
 
                            IF @ID_TIPO_DTE_CEX IS NULL
                            BEGIN
                                RAISERROR('No existe el tipo de documento con ABREVIATURA = ''CEX'' en TIPO_DTE.', 16, 1)
                                RETURN
                            END
 
                            -- Captura + incremento del correlativo en una sola operación atómica
                            UPDATE DOCUMENTO_NUMERACION
                            SET    @NUM_QUEDAN_DEST = ULT_NUM_ASIGNADO + 1,
                                   ULT_NUM_ASIGNADO = ULT_NUM_ASIGNADO + 1
                            WHERE ID_TIPO_DTE = @ID_TIPO_DTE_CEX
 
                            IF @NUM_QUEDAN_DEST IS NULL OR @NUM_QUEDAN_DEST = 0
                            BEGIN
                                RAISERROR('No existe numeración activa para Compra Exterior (CEX) en DOCUMENTO_NUMERACION.', 16, 1)
                                RETURN
                            END
 
                            SELECT @ID_QUEDAN_CEX_DEST = ISNULL(MAX(ID_QUEDAN), 0) + 1 FROM QUEDAN
 
                            INSERT INTO QUEDAN (
                                ID_QUEDAN, NUM_QUEDAN, FECHA,
                                ID_ENTIDAD, CODIGO_ENTIDAD, ESTADO, OBSERVACION,
                                USUARIO_CREA, FECHA_CREA, USUARIO_ACT, FECHA_ACT
                            )
                            VALUES (
                                @ID_QUEDAN_CEX_DEST, @NUM_QUEDAN_DEST, ISNULL(@FECHA_RECIBIDO, @AHORA),
                                @ID_ENTIDAD, @CODIGO_ENTIDAD, 'ACT', NULL,
                                @USUARIO, @AHORA, @USUARIO, @AHORA
                            )
                        END
                   -- 2) Si es contado, forzar Quedan a NULL para el INSERT
                   IF @ES_CONTADO = 1
                        BEGIN
                         SET @ID_QUEDAN_CEX_DEST = NULL
                        SET @NUM_QUEDAN_DEST = NULL
                        END
 
                    -- 3) Insertar el CCF 
                    SELECT @NUEVO_ID_COMPRA_EXTERIOR = ISNULL(MAX(ID_COMPRA_EXTERIOR), 0) + 1
                    FROM [EPROVEEDOR].[COMPRA_EXTERIOR]
 
                    INSERT INTO [EPROVEEDOR].[COMPRA_EXTERIOR] (
                        ID_COMPRA_EXTERIOR, ID_QUEDAN, NUM_QUEDAN,
                        ID_CHEQUE, NUM_CHEQUE,
                        ID_TIPO_DTE, NUM_CONTROL, COD_GENERACION, SELLO_RECIBIDO,
                        FECHA_EMISION, TIPO_MONEDA, ID_ENTIDAD, CODIGO_ENTIDAD,
                        FECHA_RECIBIDO, FECHA_VENCE, ORDEN,
                        ID_SUCURSAL, ID_TIPO_SERVI, ID_TIPO_IMPORTACION, ID_TIPO_OPERA,
                        ID_CLASIFICA, ID_SECTOR, ID_TIPO_COSTO, ID_TIPO_RENTA, APLICABLE_RENTA,
                        NO_SUJETA, EXENTA, GRAVADA, IVA,
                        FOVIAL, COTRANS, TOTAL, CARGO, ABONO,
                        RENTA, IVAR, SALDO, OBSERVACION,
                        USUARIO_CREA, FECHA_CREA, USUARIO_ACT, FECHA_ACT,
                        UID_ENLACE_CHEQUE, SALFEC, UID_DOCUMENTO
                    )
                    VALUES (
                        @NUEVO_ID_COMPRA_EXTERIOR, @ID_QUEDAN_CEX_DEST, @NUM_QUEDAN_DEST,
                        @ID_CHEQUE, @NUM_CHEQUE,
                        @ID_TIPO_DTE, @NUM_CONTROL, @COD_GENERACION, @SELLO_RECIBIDO,
                        @FECHA_EMISION, ISNULL(@TIPO_MONEDA, 'USD'),
                        @ID_ENTIDAD, @CODIGO_ENTIDAD,
                        @FECHA_RECIBIDO, @FECHA_VENCE, @ORDEN,
                        @ID_SUCURSAL, @ID_TIPO_SERVI, @ID_TIPO_IMPORTACION, @ID_TIPO_OPERA,
                        @ID_CLASIFICA, @ID_SECTOR, @ID_TIPO_COSTO, @ID_TIPO_RENTA, ISNULL(@APLICABLE_RENTA, 0),
                        ISNULL(@NO_SUJETA, 0), ISNULL(@EXENTA, 0), ISNULL(@GRAVADA, 0),
                        ISNULL(@IVA, 0),
                        ISNULL(@FOVIAL, 0), ISNULL(@COTRANS, 0), ISNULL(@TOTAL, 0),
                        ISNULL(@CARGO, 0), ISNULL(@ABONO, 0),
                        ISNULL(@RENTA, 0), ISNULL(@IVAR, 0), ISNULL(@SALDO, 0),
                        @OBSERVACION, @USUARIO, @AHORA, @USUARIO, @AHORA,
                        @UID_ENLACE_CHEQUE, LEFT(CONVERT(varchar, @FECHA_RECIBIDO, 112), 6), NEWID()
                    )
                    EXEC EIVA.SP_LBCOMPRAS_INS @NUEVO_ID_COMPRA_EXTERIOR
                     -- 3) Generar Comprobante de Retención (Si aplica)
                     IF ISNULL(@IVAR, 0) > 0
                        BEGIN
                            EXEC SP_COMPROBANTE_RETENCION
                                @ACCION             = 'GENERAR_DTE',
                                @ID_COMPROBANTE_RET = @ID_COMP_RET OUT,
                                @ID_CCF_COMPRA      = @NUEVO_ID_COMPRA_EXTERIOR,
                                @USUARIO            = @USUARIO
                            UPDATE [EPROVEEDOR].[COMPRA_EXTERIOR]
                            SET    ID_COMPROBANTE_RET = @ID_COMP_RET
                            WHERE  ID_COMPRA_EXTERIOR = @NUEVO_ID_COMPRA_EXTERIOR
                            
                            
                             EXEC EIVA.SP_LBCOMPRAS_RET_INS @NUEVO_ID_COMPRA_EXTERIOR
                        END
                        -- Actualizar el libro de compras de retención
                        IF @IVAR > 0 AND EXISTS(    SELECT 1 
                                                    FROM [EPROVEEDOR].[COMPRA_EXTERIOR] 
                                                    WHERE ID_COMPRA_EXTERIOR = @ID_COMPRA_EXTERIOR AND  NOT ID_COMPROBANTE_RET IS NULL
                                                )
                        BEGIN
                            EXEC EIVA.SP_LBCOMPRAS_RET_INS @ID_COMPRA_EXTERIOR  
                        END
 
                    COMMIT TRAN
 
                    SELECT
                        @NUEVO_ID_COMPRA_EXTERIOR AS ID_GENERADO,
                        @ID_QUEDAN_CEX_DEST AS ID_QUEDAN_GENERADO,
                        @NUM_QUEDAN_DEST AS NUM_QUEDAN_GENERADO
                END TRY
                BEGIN CATCH
                    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
                    ;THROW
                END CATCH
            END
        ELSE
            BEGIN
                BEGIN TRY
                    BEGIN TRAN
                    -- UPDATE del CCF (no se toca el Quedan)
                    UPDATE [EPROVEEDOR].[COMPRA_EXTERIOR] SET
                        ID_TIPO_DTE     = @ID_TIPO_DTE,
                        NUM_CONTROL     = @NUM_CONTROL,
                        COD_GENERACION  = @COD_GENERACION,
                        SELLO_RECIBIDO  = @SELLO_RECIBIDO,
                        FECHA_EMISION   = @FECHA_EMISION,
                        TIPO_MONEDA     = ISNULL(@TIPO_MONEDA, 'USD'),
                        FECHA_RECIBIDO  = @FECHA_RECIBIDO,
                        FECHA_VENCE     = @FECHA_VENCE,
                        ORDEN           = @ORDEN,
                        ID_SUCURSAL     = @ID_SUCURSAL,
                        ID_TIPO_SERVI   = @ID_TIPO_SERVI,
                        ID_TIPO_IMPORTACION = @ID_TIPO_IMPORTACION,
                        ID_TIPO_OPERA   = @ID_TIPO_OPERA,
                        ID_CLASIFICA    = @ID_CLASIFICA,
                        ID_SECTOR       = @ID_SECTOR,
                        ID_TIPO_COSTO   = @ID_TIPO_COSTO,
                        NO_SUJETA       = ISNULL(@NO_SUJETA, 0),
                        EXENTA          = ISNULL(@EXENTA, 0),
                        GRAVADA         = ISNULL(@GRAVADA, 0),                        
                        IVA             = ISNULL(@IVA, 0),
                        FOVIAL          = ISNULL(@FOVIAL, 0),
                        COTRANS         = ISNULL(@COTRANS, 0),
                        TOTAL           = ISNULL(@TOTAL, 0),
                        CARGO           = ISNULL(@CARGO, 0),
                        ABONO           = ISNULL(@ABONO, 0),
                        RENTA           = ISNULL(@RENTA, 0),
                        IVAR            = ISNULL(@IVAR, 0),
                        SALDO           = ISNULL(@SALDO, 0),
                        OBSERVACION     = @OBSERVACION,
                        USUARIO_ACT     = @USUARIO,
                        FECHA_ACT       = @AHORA,
                        SALFEC         = LEFT(CONVERT(varchar, @FECHA_RECIBIDO, 112), 6)
                    WHERE ID_COMPRA_EXTERIOR = @ID_COMPRA_EXTERIOR
                    
                    EXEC EIVA.SP_LBCOMPRAS_INS @ID_COMPRA_EXTERIOR
                    -- Verificar si posee comprobante de Retención, sino emitirlo
                    IF @IVAR > 0 AND EXISTS(    SELECT 1 
                                                FROM [EPROVEEDOR].[COMPRA_EXTERIOR] 
                                                WHERE ID_COMPRA_EXTERIOR = @ID_COMPRA_EXTERIOR AND ID_COMPROBANTE_RET IS NULL
                                            )
                        BEGIN                            
                            EXEC SP_COMPROBANTE_RETENCION
                                    @ACCION             = 'GENERAR_DTE',
                                    @ID_COMPROBANTE_RET = @ID_COMP_RET OUT,
                                    @ID_CCF_COMPRA      = @ID_COMPRA_EXTERIOR,
                                    @USUARIO            = @USUARIO
                            UPDATE [EPROVEEDOR].[COMPRA_EXTERIOR]
                            SET    ID_COMPROBANTE_RET   = @ID_COMP_RET
                            WHERE  ID_COMPRA_EXTERIOR        = @ID_COMPRA_EXTERIOR
                            
                            EXEC EIVA.SP_LBCOMPRAS_RET_INS @ID_COMPRA_EXTERIOR  
                        END
                        -- Actualizar el libro de compras de retención
                        IF @IVAR > 0 AND EXISTS(    SELECT 1 
                                                    FROM [EPROVEEDOR].[COMPRA_EXTERIOR] 
                                                    WHERE ID_COMPRA_EXTERIOR = @ID_COMPRA_EXTERIOR AND  NOT ID_COMPROBANTE_RET IS NULL
                                                )
                        BEGIN
                            EXEC EIVA.SP_LBCOMPRAS_RET_INS @ID_COMPRA_EXTERIOR  
                        END
 
                    -- Devuelve la misma estructura para que el front maneje todos los casos uniformemente
                    SELECT
                        @ID_COMPRA_EXTERIOR AS ID_GENERADO,
                        ID_QUEDAN      AS ID_QUEDAN_GENERADO,
                        NUM_QUEDAN     AS NUM_QUEDAN_GENERADO                        
                    FROM [EPROVEEDOR].[COMPRA_EXTERIOR]
                    WHERE ID_COMPRA_EXTERIOR = @ID_COMPRA_EXTERIOR
                    COMMIT TRAN
                END TRY
                BEGIN CATCH
                    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
                    ;THROW
                END CATCH
            END
    END
    ELSE IF @ACCION = 'GUARDAR_NC_ND'
        BEGIN
            IF @ID_COMPRA_EXTERIOR = 0                
                -- CREAR
                BEGIN TRY
                        BEGIN TRAN
                        SELECT @NUEVO_ID_COMPRA_EXTERIOR = ISNULL(MAX(ID_COMPRA_EXTERIOR), 0) + 1
                        FROM [EPROVEEDOR].[COMPRA_EXTERIOR]
 
                        -- 1) Insertar la NC/ND asociado al Quedan 
                        INSERT INTO [EPROVEEDOR].[COMPRA_EXTERIOR] (
                            ID_COMPRA_EXTERIOR, ID_QUEDAN, NUM_QUEDAN,
                            ID_TIPO_DTE, NUM_CONTROL, COD_GENERACION, SELLO_RECIBIDO,
                            FECHA_EMISION, TIPO_MONEDA, ID_ENTIDAD, CODIGO_ENTIDAD,
                            FECHA_RECIBIDO, FECHA_VENCE, ORDEN,
                            ID_SUCURSAL, ID_TIPO_SERVI, ID_TIPO_IMPORTACION, ID_TIPO_OPERA,
                            ID_CLASIFICA, ID_SECTOR, ID_TIPO_COSTO, ID_TIPO_RENTA, APLICABLE_RENTA,
                            NO_SUJETA, EXENTA, GRAVADA, IVA,
                            FOVIAL, COTRANS, TOTAL, CARGO, ABONO,
                            RENTA, IVAR, SALDO, OBSERVACION,
                            USUARIO_CREA, FECHA_CREA, USUARIO_ACT, FECHA_ACT,
                            ID_CCF_COMPRA_ASOCIADO, UID_ENLACE_CHEQUE, SALFEC, UID_DOCUMENTO
                        )
                        VALUES (
                            @NUEVO_ID_COMPRA_EXTERIOR, @ID_QUEDAN_CEX_DEST, @NUM_QUEDAN_DEST,
                            @ID_TIPO_DTE, @NUM_CONTROL, @COD_GENERACION, @SELLO_RECIBIDO,
                            @FECHA_EMISION, ISNULL(@TIPO_MONEDA, 'USD'), @ID_ENTIDAD, @CODIGO_ENTIDAD,
                            @FECHA_RECIBIDO, @FECHA_VENCE, @ORDEN,
                            @ID_SUCURSAL, @ID_TIPO_SERVI, @ID_TIPO_IMPORTACION, @ID_TIPO_OPERA,
                            @ID_CLASIFICA, @ID_SECTOR, @ID_TIPO_COSTO, @ID_TIPO_RENTA, ISNULL(@APLICABLE_RENTA, 0),
                            ISNULL(@NO_SUJETA, 0), ISNULL(@EXENTA, 0), ISNULL(@GRAVADA, 0), ISNULL(@IVA, 0),                            
                            ISNULL(@FOVIAL, 0), ISNULL(@COTRANS, 0), ISNULL(@TOTAL, 0), ISNULL(@CARGO, 0), ISNULL(@ABONO, 0),
                            ISNULL(@RENTA, 0), ISNULL(@IVAR, 0), ISNULL(@SALDO, 0), @OBSERVACION, 
							@USUARIO, @AHORA, @USUARIO, @AHORA,
                            @ID_CCF_COMPRA_ASOCIADO, @UID_ENLACE_CHEQUE, LEFT(CONVERT(varchar, @FECHA_RECIBIDO, 112), 6), NEWID()
                        )
                        -- 2) Actualizar el documento asociado
                        SET @NUEVO_CARGO = ISNULL( (SELECT SUM(T.SALDO) FROM [EPROVEEDOR].[COMPRA_EXTERIOR] T WHERE T.ID_CCF_COMPRA_ASOCIADO = @ID_CCF_COMPRA_ASOCIADO AND T.ID_TIPO_DTE IN(5,23)) ,0)
                        SET @NUEVO_ABONO = ISNULL( (SELECT SUM(T.SALDO) FROM [EPROVEEDOR].[COMPRA_EXTERIOR] T WHERE T.ID_CCF_COMPRA_ASOCIADO = @ID_CCF_COMPRA_ASOCIADO AND T.ID_TIPO_DTE IN(4,22)) ,0)
                        UPDATE [EPROVEEDOR].[COMPRA_EXTERIOR]
                        SET         -- ND: AUMENTA EL SALDO DEL CCF
                            CARGO = @NUEVO_CARGO,
                                    -- NC: DISMINUYE AL SALDO DEL CCF
                            ABONO = @NUEVO_ABONO,
                                    -- ACTUALIZAR SALDO
                            SALDO = TOTAL + @NUEVO_CARGO - @NUEVO_ABONO - RENTA - IVAR
                        WHERE ID_COMPRA_EXTERIOR = @ID_CCF_COMPRA_ASOCIADO
                        
                        EXEC EIVA.SP_LBCOMPRAS_INS @NUEVO_ID_COMPRA_EXTERIOR
                        -- 3) Devuelve ID generado
                        SELECT @NUEVO_ID_COMPRA_EXTERIOR AS ID_GENERADO
 
                        COMMIT TRAN
                END TRY
                BEGIN CATCH
                    IF @@TRANCOUNT > 0 ROLLBACK TRAN
                    ;THROW
                END CATCH
            ELSE
                -- ACTUALIZAR
                BEGIN
                UPDATE [EPROVEEDOR].[COMPRA_EXTERIOR] SET
                    ID_TIPO_DTE     = @ID_TIPO_DTE,
                    NUM_CONTROL     = @NUM_CONTROL,
                    COD_GENERACION  = @COD_GENERACION,
                    SELLO_RECIBIDO  = @SELLO_RECIBIDO,
                    FECHA_EMISION   = @FECHA_EMISION,
                    TIPO_MONEDA     = ISNULL(@TIPO_MONEDA, 'USD'),
                    FECHA_RECIBIDO  = @FECHA_RECIBIDO,
                    FECHA_VENCE     = @FECHA_VENCE,
                    ORDEN           = @ORDEN,
                    ID_SUCURSAL     = @ID_SUCURSAL,
                    ID_TIPO_SERVI   = @ID_TIPO_SERVI,
                    ID_TIPO_IMPORTACION = @ID_TIPO_IMPORTACION,
                    ID_TIPO_OPERA   = @ID_TIPO_OPERA,
                    ID_CLASIFICA    = @ID_CLASIFICA,
                    ID_SECTOR       = @ID_SECTOR,
                    ID_TIPO_COSTO   = @ID_TIPO_COSTO,
                    NO_SUJETA       = ISNULL(@NO_SUJETA, 0),
                    EXENTA          = ISNULL(@EXENTA, 0),
                    GRAVADA         = ISNULL(@GRAVADA, 0),                    
                    IVA             = ISNULL(@IVA, 0),
                    FOVIAL          = ISNULL(@FOVIAL, 0),
                    COTRANS         = ISNULL(@COTRANS, 0),
                    TOTAL           = ISNULL(@TOTAL, 0),
                    CARGO           = ISNULL(@CARGO, 0),
                    ABONO           = ISNULL(@ABONO, 0),
                    RENTA           = ISNULL(@RENTA, 0),
                    IVAR            = ISNULL(@IVAR, 0),
                    SALDO           = ISNULL(@SALDO, 0),
                    OBSERVACION     = @OBSERVACION,
                    USUARIO_ACT     = @USUARIO,
                    FECHA_ACT       = @AHORA,
                    ID_CCF_COMPRA_ASOCIADO = @ID_CCF_COMPRA_ASOCIADO,
                    SALFEC         = LEFT(CONVERT(varchar, @FECHA_RECIBIDO, 112), 6)
                WHERE ID_COMPRA_EXTERIOR = @ID_COMPRA_EXTERIOR
                 -- 2) Actualizar el documento asociado
                SET @NUEVO_CARGO = ISNULL( (SELECT SUM(T.SALDO) FROM [EPROVEEDOR].[COMPRA_EXTERIOR] T WHERE T.ID_CCF_COMPRA_ASOCIADO = @ID_CCF_COMPRA_ASOCIADO AND T.ID_TIPO_DTE IN(5,23)) ,0)
                SET @NUEVO_ABONO = ISNULL( (SELECT SUM(T.SALDO) FROM [EPROVEEDOR].[COMPRA_EXTERIOR] T WHERE T.ID_CCF_COMPRA_ASOCIADO = @ID_CCF_COMPRA_ASOCIADO AND T.ID_TIPO_DTE IN(4,22)) ,0)
                UPDATE [EPROVEEDOR].[COMPRA_EXTERIOR]
                SET         -- ND: AUMENTA EL SALDO DEL CCF
                    CARGO = @NUEVO_CARGO,
                            -- NC: DISMINUYE AL SALDO DEL CCF
                    ABONO = @NUEVO_ABONO,
                            -- ACTUALIZAR SALDO
                    SALDO = TOTAL + @NUEVO_CARGO - @NUEVO_ABONO - RENTA - IVAR
                WHERE ID_COMPRA_EXTERIOR = @ID_CCF_COMPRA_ASOCIADO
                
                 EXEC EIVA.SP_LBCOMPRAS_INS @ID_COMPRA_EXTERIOR
                
                END
        END
    ELSE IF @ACCION = 'ELIMINAR'
        BEGIN
            DELETE FROM [EPROVEEDOR].[COMPRA_EXTERIOR] WHERE ID_COMPRA_EXTERIOR = @ID_COMPRA_EXTERIOR
        END
    -- ============================================================
    -- LISTAR_POR_UID
    -- Devuelve los CCFs asociados temporalmente a un cheque en
    -- construcción, identificados por UID_ENLACE_CHEQUE.
    -- Se usa en frmChequeDocumentosContado.
    -- ============================================================
    ELSE IF @ACCION = 'LISTAR_POR_UID'
        BEGIN
            SELECT
                C.ID_COMPRA_EXTERIOR,
                C.CODIGO_ENTIDAD,
                E.NOMBRE              AS PROVEEDOR,
                TD.ABREVIATURA        AS TIPO_DOC,
                C.COD_GENERACION,
                C.NUM_CONTROL,
                C.FECHA_RECIBIDO,
                ISNULL(C.GRAVADA, 0)      AS GRAVADA,
                ISNULL(C.EXENTA, 0)       AS EXENTA,
                ISNULL(C.NO_SUJETA, 0)    AS EXCLUIDO,                
                ISNULL(C.IVA, 0)          AS IVA,
                ISNULL(C.FOVIAL, 0)       AS FOVIAL,
                ISNULL(C.COTRANS, 0)      AS COTRANS,
                ISNULL(C.TOTAL, 0)        AS TOTAL,
                ISNULL(C.RENTA, 0)        AS RENTA,
                ISNULL(C.IVAR, 0)         AS IVAR,
                ISNULL(C.SALDO, 0)        AS SALDO
            FROM [EPROVEEDOR].[COMPRA_EXTERIOR] C
            INNER JOIN TIPO_DTE TD ON TD.ID_TIPO_DTE = C.ID_TIPO_DTE
            LEFT  JOIN ENTIDAD  E  ON E.ID_ENTIDAD   = C.ID_ENTIDAD
            WHERE C.UID_ENLACE_CHEQUE = @UID_ENLACE_CHEQUE
              AND ISNULL(C.UID_ENLACE_CHEQUE, '') <> ''
            ORDER BY C.ID_COMPRA_EXTERIOR
        END
    ELSE IF @ACCION = 'LISTAR_UIDS_HUERFANOS'
        BEGIN
            SELECT
                UID_ENLACE_CHEQUE,
                COUNT(*)                AS CANTIDAD_DOCUMENTOS,
                MIN(FECHA_CREA)         AS FECHA_PRIMERA_CAPTURA
            FROM [EPROVEEDOR].[COMPRA_EXTERIOR]
            WHERE ISNULL(UID_ENLACE_CHEQUE, '') <> ''
              AND ID_CHEQUE IS NULL
              AND USUARIO_CREA = @USUARIO
            GROUP BY UID_ENLACE_CHEQUE
            ORDER BY MIN(FECHA_CREA) DESC
        END
END
GO

IF OBJECT_ID(N'[EPROVEEDOR].[SP_CREDITO_FISCAL_COMPRA]', N'P') IS NOT NULL
BEGIN
    DROP PROCEDURE [EPROVEEDOR].[SP_CREDITO_FISCAL_COMPRA];
END;
GO
