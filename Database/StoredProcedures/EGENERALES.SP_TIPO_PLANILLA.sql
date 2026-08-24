USE [PH2]
GO
/****** Object:  StoredProcedure [EGENERALES].[SP_TIPO_PLANILLA] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ----------------------------------------------------------------
-- SP: [EGENERALES].[SP_TIPO_PLANILLA]
-- Acciones: LISTAR | OBTENER | BUSCAR
-- Tabla:    [INJIBOA].[dbo].[TIPO_PLANILLA]  (cross-database, sistema SIGESTA)
-- Nota:     Solo se exponen los tipos de planilla habilitados para este
--           sistema: ID_TIPO_PLANILLA IN (1,2,4,5,6,11) — confirmado por Roberto.
-- ----------------------------------------------------------------
CREATE OR ALTER PROCEDURE [EGENERALES].[SP_TIPO_PLANILLA]
    @ACCION           NVARCHAR(50),
    @ID_TIPO_PLANILLA INT           = NULL,
    @FILTRO           NVARCHAR(100) = NULL   -- para búsqueda genérica con "*"
AS
BEGIN
    SET NOCOUNT ON;

    -- ── LISTAR ────────────────────────────────────────────────────
    IF @ACCION = 'LISTAR'
    BEGIN
        SELECT
            ID_TIPO_PLANILLA,
            NOMBRE_TIPO_PLANILLA
        FROM [INJIBOA].[dbo].[TIPO_PLANILLA]
        WHERE ID_TIPO_PLANILLA IN (1, 2, 4, 5, 6, 11)
        ORDER BY NOMBRE_TIPO_PLANILLA;
        RETURN;
    END

    -- ── OBTENER ───────────────────────────────────────────────────
    IF @ACCION = 'OBTENER'
    BEGIN
        SELECT
            ID_TIPO_PLANILLA,
            NOMBRE_TIPO_PLANILLA
        FROM [INJIBOA].[dbo].[TIPO_PLANILLA]
        WHERE ID_TIPO_PLANILLA = @ID_TIPO_PLANILLA
          AND ID_TIPO_PLANILLA IN (1, 2, 4, 5, 6, 11);
        RETURN;
    END

    -- ── BUSCAR (usado por frmBusquedaGenerica / FormHelper.RegistrarBusqueda) ──
    IF @ACCION = 'BUSCAR'
    BEGIN
        SELECT
            ID_TIPO_PLANILLA,
            NOMBRE_TIPO_PLANILLA
        FROM [INJIBOA].[dbo].[TIPO_PLANILLA]
        WHERE ID_TIPO_PLANILLA IN (1, 2, 4, 5, 6, 11)
          AND (
                @FILTRO IS NULL
                OR NOMBRE_TIPO_PLANILLA LIKE '%' + @FILTRO + '%'
              )
        ORDER BY NOMBRE_TIPO_PLANILLA;
        RETURN;
    END
END
GO
