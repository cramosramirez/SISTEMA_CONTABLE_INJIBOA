USE [PH2];
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

-- exec SP_CREDITO_FISCAL_PROVISION @ACCION='CARGAR_PROVISION', @ID_CCF_COMPRA = 1 
-- ================================================================
--  SP_CREDITO_FISCAL_PROVISION
--
--  Maneja las líneas de la partida contable asociada a un CCF
--  de compra. Cada CCF tiene N líneas (cargo/abono por cuenta).
--
--  Acciones:
--    LISTAR_POR_CCF     -> trae todas las líneas de un CCF
--    OBTENER            -> una línea por su ID
--    GUARDAR            -> INSERT (si ID=0) o UPDATE
--    ELIMINAR           -> borra una línea por su ID
--    ELIMINAR_POR_CCF   -> borra todas las líneas de un CCF (útil al regrabar)
--    VALIDAR_PARTIDA    -> verifica que suma de cargos = suma de abonos
-- ================================================================
CREATE OR ALTER PROCEDURE [EPROVEEDOR].[SP_CREDITO_FISCAL_PROVISION]
    @ACCION             VARCHAR(100),
    @ID_COMPRA_PROVI    INT             = 0,
    @ORDEN              INT             = 0,
    @ID_CCF_COMPRA      INT             = NULL,
    @CTACONTABLE        NVARCHAR(50)    = NULL,
    @DETALLE            NVARCHAR(300)   = NULL,
    @CARGO              NUMERIC(20, 2)  = NULL,
    @ABONO              NUMERIC(20, 2)  = NULL,
    @USUARIO            NVARCHAR(100)   = NULL,
    @PARTIDA_PROVISION  dbo.typeCREDITO_FISCAL_PROVISION READONLY
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE 			
            @PROVEEDOR      NVARCHAR(300),
            @NUM_CCF        NVARCHAR(100),
            @NUM_IVAR       NVARCHAR(100),
            @ORDEN_PROVI    INT,
            @VALOR_PROVISION NUMERIC(20,2),
            @VALOR_IVA      NUMERIC(20,2),
            @VALOR_IVA_RET  NUMERIC(20,2),
            @VALOR_RENTA    NUMERIC(20,2),
            @VALOR_FOVIAL   NUMERIC(20,2),
            @VALOR_COTRANS  NUMERIC(20,2),
            @VALOR_GASTO    NUMERIC(20,2),
            @CTA_PROVISION  NVARCHAR(50),
            @CTA_IVA        NVARCHAR(50),
            @CTA_IVA_RET    NVARCHAR(50),
            @CTA_RENTA      NVARCHAR(50),
            @CTA_FOVIAL     NVARCHAR(50),
            @CTA_COTRANS    NVARCHAR(50),
			@CTA_GASTO		NVARCHAR(50),
            @W_CTA          NVARCHAR(50),				
			@W_ID_TIPO_DTE	INT,			
            @W_MENSAJE      NVARCHAR(1000),
            @W_FILAS_AFECTADAS INT,
			@ORDEN_COMPRA	NVARCHAR(50)
        
    DECLARE @AHORA DATETIME = CONVERT(DATETIME, CONVERT(CHAR(19), GETDATE(), 120), 120)

    -- ============================================================
    -- LISTAR_POR_CCF: trae todas las líneas de un CCF
    -- ============================================================
    IF @ACCION = 'LISTAR_POR_CCF'
    BEGIN
        SELECT
            ID_COMPRA_PROVI,
            ID_CCF_COMPRA,
            CTACONTABLE,
            DETALLE,
            CARGO,
            ABONO,
            USUARIO_CREA, FECHA_CREA,
            USUARIO_ACT,  FECHA_ACT
        FROM CREDITO_FISCAL_PROVISION
        WHERE ID_CCF_COMPRA = @ID_CCF_COMPRA
        ORDER BY ID_COMPRA_PROVI
    END

    -- ============================================================
    -- OBTENER: una línea por ID
    -- ============================================================
    ELSE IF @ACCION = 'OBTENER'
    BEGIN
        SELECT
            ID_COMPRA_PROVI,
            ID_CCF_COMPRA,
            CTACONTABLE,
            DETALLE,
            CARGO,
            ABONO,
            USUARIO_CREA, FECHA_CREA,
            USUARIO_ACT,  FECHA_ACT
        FROM CREDITO_FISCAL_PROVISION
        WHERE ID_COMPRA_PROVI = @ID_COMPRA_PROVI
    END

    -- ============================================================
    -- GUARDAR: INSERT si ID=0, UPDATE si ID>0
    -- ============================================================
    ELSE IF @ACCION = 'GUARDAR'
        BEGIN
            BEGIN TRY
                BEGIN TRAN
                -- VALIDAR QUE LAS CUENTAS EXISTAN EN EL CATÁLOGO DE CUENTAS
                SELECT TOP 1 @W_CTA = P.CTACONTABLE
                FROM @PARTIDA_PROVISION P
                WHERE NOT EXISTS (
                        SELECT 1 FROM CATALOGO_CUENTA C
                        WHERE C.CUENTA = P.CTACONTABLE
                      )
                  AND P.CTACONTABLE IS NOT NULL
                  AND LTRIM(RTRIM(P.CTACONTABLE)) <> ''

                IF @W_CTA IS NOT NULL
                    BEGIN                   
                    SET @W_MENSAJE = 'LA CUENTA CONTABLE ' + @W_CTA + ' NO EXISTE EN EL CATALOGO'                    
                    RAISERROR(@W_MENSAJE, 16, 1);
                    END

                -- ELIMINANDO PROVISION ANTERIOR
                DELETE FROM CREDITO_FISCAL_PROVISION WHERE ID_CCF_COMPRA = @ID_CCF_COMPRA

                -- INSERTANDO NUEVA PROVISION
                INSERT CREDITO_FISCAL_PROVISION 
                    (ID_COMPRA_PROVI, ID_CCF_COMPRA, ORDEN, CTACONTABLE, DETALLE, CARGO, ABONO, 
                    USUARIO_CREA, FECHA_CREA, USUARIO_ACT, FECHA_ACT)
                SELECT             
                    ROW_NUMBER() OVER(ORDER BY ORDEN) +
                        (   SELECT ISNULL(MAX(ID_COMPRA_PROVI),0)
                            FROM CREDITO_FISCAL_PROVISION
                         )                                  AS CREDITO_FISCAL_PROVISION,
                    @ID_CCF_COMPRA                          AS ID_CCF_COMPRA,
                    ORDEN, CTACONTABLE, 
                    DETALLE, CARGO, ABONO, 
                    @USUARIO                                AS USUARIO_CREA,
                    @AHORA                                  AS FECHA_CREA,            
                    @USUARIO                                AS USUARIO_ACT,
                    @AHORA                                  AS FECHA_ACT
                FROM @PARTIDA_PROVISION
                WHERE (CARGO > 0 OR ABONO > 0) AND RTRIM(ISNULL(CTACONTABLE,'')) <> ''

                -- OBTENER FILAS AFECTADAS
                SET @W_FILAS_AFECTADAS = @@ROWCOUNT

                COMMIT TRAN

                 -- DEVOLVER FILAS AFECTADAS
                SELECT @W_FILAS_AFECTADAS AS FILAS_AFECTADAS
            END TRY
            BEGIN CATCH
                IF @@TRANCOUNT > 0 ROLLBACK TRAN;
                THROW;
            END CATCH
        END

    -- ============================================================
    -- GENERAR PROVISION
    -- ============================================================
    ELSE IF @ACCION = 'CARGAR_PROVISION'
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM CREDITO_FISCAL_PROVISION WHERE ID_CCF_COMPRA = @ID_CCF_COMPRA)
            BEGIN       

                 -- Tabla temporal de la partida
                 CREATE TABLE #PROVISION(            
                    ORDEN           INT,
                    CTACONTABLE     NVARCHAR(50),
                    DETALLE         NVARCHAR(300),
                    CARGO           NUMERIC(20,2),
                    ABONO           NUMERIC(20,2)
                 )

                 -- Estableciendo las cuentas de la partida y sus valores
                 SELECT      
					@CTA_GASTO		= ISNULL(E.CUENTA_GASTO,''),
                    @PROVEEDOR      = E.NOMBRE,
					@W_ID_TIPO_DTE	= C.ID_TIPO_DTE,
                    @NUM_CCF        = CASE C.ID_TIPO_DTE
                                        WHEN 2      THEN 'DTE#' + CAST( CAST(SUBSTRING(C.NUM_CONTROL,17,15) AS INT) AS NVARCHAR)
                                        WHEN 21     THEN 'CCF#' + C.COD_GENERACION
                                        WHEN 4      THEN 'NCC#'  + CAST( CAST(SUBSTRING(C.NUM_CONTROL,17,15) AS INT) AS NVARCHAR)
                                        WHEN 22     THEN 'NCC#'  + C.COD_GENERACION
                                        WHEN 5      THEN 'ND#'  + CAST( CAST(SUBSTRING(C.NUM_CONTROL,17,15) AS INT) AS NVARCHAR)
                                        WHEN 23     THEN 'ND#'  + C.COD_GENERACION
                                      END,
                    @NUM_IVAR       = ISNULL((SELECT T.NUM_CONTROL FROM COMPROBANTE_RETENCION T WHERE T.ID_COMPROBANTE_RET = C.ID_COMPROBANTE_RET),''),
                    @CTA_PROVISION  = ISNULL( E.CUENTA_X_PAGAR, '' ),
                    @CTA_IVA        = '1107.01.001',    @CTA_IVA_RET = '2108.02.002', @CTA_RENTA = '2103.01.002', 
                    @CTA_FOVIAL     = '1108.03.043',    @CTA_COTRANS = '1108.03.043',
                    @VALOR_PROVISION = C.SALDO + C.ABONO - C.CARGO,
                    @VALOR_IVA      = C.IVA,            @VALOR_IVA_RET  = C.IVAR,     @VALOR_RENTA    = C.RENTA,
                    @VALOR_FOVIAL   = C.FOVIAL,         @VALOR_COTRANS  = C.COTRANS,
					@ORDEN_COMPRA	= ISNULL(C.ORDEN,'')
                FROM [EPROVEEDOR].[COMPRA_EXTERIOR] C, ENTIDAD E
                WHERE C.ID_COMPRA_EXTERIOR = @ID_CCF_COMPRA 
                        AND C.ID_ENTIDAD = E.ID_ENTIDAD

        
                IF @VALOR_PROVISION > 0
                    BEGIN
                    SET @ORDEN_PROVI    = 1
                    INSERT #PROVISION VALUES(@ORDEN_PROVI,@CTA_PROVISION,'Provision de ' + @NUM_CCF, IIF(@W_ID_TIPO_DTE IN(4,22),@VALOR_PROVISION,0),IIF(@W_ID_TIPO_DTE IN(4,22),0,@VALOR_PROVISION))
                    END        
    
                IF @VALOR_RENTA > 0
                    BEGIN
                    SET @ORDEN_PROVI = @ORDEN_PROVI + 1
                    INSERT #PROVISION VALUES(@ORDEN_PROVI,@CTA_RENTA,'Ret.RENTA S/' + @NUM_CCF + ' ' + @PROVEEDOR, IIF(@W_ID_TIPO_DTE IN(4,22),@VALOR_RENTA,0),IIF(@W_ID_TIPO_DTE IN(4,22),0,@VALOR_RENTA))
                    END           
                IF @VALOR_IVA_RET > 0 AND @NUM_IVAR <> ''
                    BEGIN
                    SET @NUM_IVAR    = CAST( CAST(SUBSTRING(@NUM_IVAR,17,15) AS INT) AS NVARCHAR)
                    SET @ORDEN_PROVI = @ORDEN_PROVI + 1
                    INSERT #PROVISION VALUES(@ORDEN_PROVI,@CTA_IVA_RET,'IVA S/Ret.IVA CR#07-'+ @NUM_IVAR + ' S/' + @NUM_CCF + ' ' + @PROVEEDOR, IIF(@W_ID_TIPO_DTE IN(4,22),@VALOR_IVA_RET,0),IIF(@W_ID_TIPO_DTE IN(4,22),0,@VALOR_IVA_RET))
                    END       
                IF @VALOR_IVA > 0 
                    BEGIN
                    SET @ORDEN_PROVI = @ORDEN_PROVI + 1
                    INSERT #PROVISION VALUES(@ORDEN_PROVI,@CTA_IVA,'IVA S/' + @NUM_CCF + ' ' + @PROVEEDOR, IIF(@W_ID_TIPO_DTE IN(4,22),0,@VALOR_IVA),IIF(@W_ID_TIPO_DTE IN(4,22),@VALOR_IVA,0))
                    END
                IF @VALOR_FOVIAL > 0 
                    BEGIN
                    SET @ORDEN_PROVI = @ORDEN_PROVI + 1
                    INSERT #PROVISION VALUES(@ORDEN_PROVI,@CTA_FOVIAL,'FOVIAL S/' + @NUM_CCF + ' ' + @PROVEEDOR, IIF(@W_ID_TIPO_DTE IN(4,22),0,@VALOR_FOVIAL),IIF(@W_ID_TIPO_DTE IN(4,22),@VALOR_FOVIAL,0))
                    END
                IF @VALOR_COTRANS > 0 
                    BEGIN
                    SET @ORDEN_PROVI = @ORDEN_PROVI + 1
                    INSERT #PROVISION VALUES(@ORDEN_PROVI,@CTA_COTRANS,'COTRANS S/' + @NUM_CCF + ' ' + @PROVEEDOR, IIF(@W_ID_TIPO_DTE IN(4,22),0,@VALOR_COTRANS),IIF(@W_ID_TIPO_DTE IN(4,22),@VALOR_COTRANS,0))
                    END 

                SET @VALOR_GASTO = (SELECT ABS(SUM(CARGO) - SUM(ABONO)) FROM #PROVISION)

				IF @ORDEN_COMPRA = '' -- SIN ORDEN DE COMPRA ASIGNADA (TOMAR LA DEL PROVEEDOR)
					BEGIN
					IF @CTA_GASTO <> ''
						BEGIN
						SET @ORDEN_PROVI = @ORDEN_PROVI + 1
						INSERT #PROVISION VALUES(@ORDEN_PROVI,@CTA_GASTO, @NUM_CCF + ' ' + @PROVEEDOR, IIF(@W_ID_TIPO_DTE IN(4,22),0,@VALOR_GASTO),IIF(@W_ID_TIPO_DTE IN(4,22),@VALOR_GASTO,0))
						END
					END
				ELSE
					BEGIN
						IF SUBSTRING(@ORDEN_COMPRA,1,2) = 'FV' -- OC PLANTA FOTOVOLTAICA
							BEGIN
							SELECT @CTA_GASTO = CUENTA FROM CONTA.PARAMETROS_CUENTA WHERE DENOMINACION = 'OC_PLANTA_FOTOVOLTAICA' AND ACTIVA = 1
							END
						ELSE IF SUBSTRING(@ORDEN_COMPRA,1,1) = 'A' -- OC PLANTA GENERADORA
							BEGIN
							SELECT @CTA_GASTO = CUENTA FROM CONTA.PARAMETROS_CUENTA WHERE DENOMINACION = 'OC_PLANTA_GENERADORA' AND ACTIVA = 1
							END
						ELSE -- OC JIBOA
							BEGIN
							SELECT @CTA_GASTO = CUENTA FROM CONTA.PARAMETROS_CUENTA WHERE DENOMINACION = 'OC_JIBOA' AND ACTIVA = 1 
							END
						SET @ORDEN_PROVI = @ORDEN_PROVI + 1
						INSERT #PROVISION VALUES(@ORDEN_PROVI, @CTA_GASTO, @NUM_CCF + ' ' + @PROVEEDOR, IIF(@W_ID_TIPO_DTE IN(4,22),0,@VALOR_GASTO),IIF(@W_ID_TIPO_DTE IN(4,22),@VALOR_GASTO,0))
					END
        
                SELECT * FROM #PROVISION
                DROP TABLE #PROVISION
            END
        ELSE
            BEGIN
            SELECT ORDEN, CTACONTABLE, DETALLE, CARGO, ABONO 
            FROM CREDITO_FISCAL_PROVISION WHERE ID_CCF_COMPRA = @ID_CCF_COMPRA
            ORDER BY ORDEN
            END
    END

    -- ============================================================
    -- ELIMINAR: borra una línea por ID
    -- ============================================================
    ELSE IF @ACCION = 'ELIMINAR'
    BEGIN
        DELETE FROM CREDITO_FISCAL_PROVISION
        WHERE ID_COMPRA_PROVI = @ID_COMPRA_PROVI
    END

    -- ============================================================
    -- ELIMINAR_POR_CCF: borra todas las líneas asociadas al CCF.
    -- Útil cuando el usuario quiere regrabar la partida completa
    -- desde cero (borra y vuelve a insertar todo).
    -- ============================================================
    ELSE IF @ACCION = 'ELIMINAR_POR_CCF'
    BEGIN
        DELETE FROM CREDITO_FISCAL_PROVISION
        WHERE ID_CCF_COMPRA = @ID_CCF_COMPRA
    END

    -- ============================================================
    -- VALIDAR_PARTIDA: verifica que la partida del CCF cuadre.
    -- Devuelve TOTAL_CARGO, TOTAL_ABONO, PARTIDA_CUADRADA (bit) y
    -- DIFERENCIA. Útil antes de cerrar o validar la operación.
    -- ============================================================
    ELSE IF @ACCION = 'VALIDAR_PARTIDA'
    BEGIN
        DECLARE @TOTAL_CARGO NUMERIC(20, 2)
        DECLARE @TOTAL_ABONO NUMERIC(20, 2)

        SELECT
            @TOTAL_CARGO = ISNULL(SUM(CARGO), 0),
            @TOTAL_ABONO = ISNULL(SUM(ABONO), 0)
        FROM CREDITO_FISCAL_PROVISION
        WHERE ID_CCF_COMPRA = @ID_CCF_COMPRA

        SELECT
            @TOTAL_CARGO                              AS TOTAL_CARGO,
            @TOTAL_ABONO                              AS TOTAL_ABONO,
            (@TOTAL_CARGO - @TOTAL_ABONO)             AS DIFERENCIA,
            CASE WHEN @TOTAL_CARGO = @TOTAL_ABONO
                 THEN CAST(1 AS BIT)
                 ELSE CAST(0 AS BIT) END              AS PARTIDA_CUADRADA
    END
END
GO
