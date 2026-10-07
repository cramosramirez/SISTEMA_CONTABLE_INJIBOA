using SistemaContable.DAL;
using SistemaContable.UI.Forms.Proveedores;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace SistemaContable.UI.Forms.Planilla
{
    // Factura de Sujeto Excluido de planilla para cañeros y transportistas no contribuyentes.
    // La importación conserva un encabezado y exactamente un detalle como referencia del origen;
    // la emisión crea una sola fila en dbo.FACTURA_SUJETO_EXC (ID_TIPO_DTE = 10).
    public partial class frmSujectoExcCorte : Form
    {
        // Tipo Planilla = 2 (TRANSPORTISTAS) usa sus propias tablas y SPs (PLANILLA_TRAS_ENCA / _DETA);
        // el resto de planillas usa los de caneros (PLANILLA_CANIERO_ENCA / _DETA). 2026-09-30
        private const int TIPO_PLANILLA_TRANSPORTISTAS = 2;
        private const string SP_PLANILLA_CANIERO = "[ECOMPROB].[SP_GENERAR_COMPROB_PLANILLA_CANIERO_FSE]";
        private const string SP_PLANILLA_TRAS    = "[ECOMPROB].[SP_GENERAR_COMPROB_PLANILLA_TRAS_FSE]";
        private bool EsTransportistas => ValorCombo(cbxTIPO_PLANILLA) == TIPO_PLANILLA_TRANSPORTISTAS;
        private string SP_PLANILLA => EsTransportistas ? SP_PLANILLA_TRAS : SP_PLANILLA_CANIERO;
        private const string RELACION_DETALLE = "Detalle";
        // Esta pantalla trabaja siempre con proveedores no contribuyentes.
        private const int ES_CONTRIBUYENTE = 0;

        private readonly DALBase _dal = new DALBase();
        private DataSet _dsCandidatos;
        private DataTable _dtDocumentos;
        private GridView gvDetalle;

        public frmSujectoExcCorte()
        {
            InitializeComponent();
        }

        private void frmSujectoExcCorte_Load(object sender, EventArgs e)
        {
            ConfigurarGridCandidatos();
            ConfigurarGridDocumentos();

            CargarZafra();
            CargarTipoPlanilla();

            chkNoSellados.CheckedChanged += (s, a) => AplicarFiltroNoSellados();

            // Mismo tamaño para todos los íconos (los recursos vienen en 32 y 48 px).
            foreach (var btn in new[] { btnConsultar, btnImportar, btnReporte, btnSalir, btnNuevo, btnGenerar, btnEliminarPruebas })
                AjustarIcono(btn, 28);
        }

        // Las dos secciones (planilla arriba / FSE generadas abajo) del mismo alto,
        // al abrir y cada vez que se cambia el tamaño de la ventana.
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            DividirMitad();
            this.Resize += (s, a) => DividirMitad();
        }

        private void DividirMitad()
        {
            if (WindowState == FormWindowState.Minimized) return;
            int mitad = (splitMain.Height - splitMain.SplitterWidth) / 2;
            if (mitad > splitMain.Panel1MinSize && mitad < splitMain.Height - splitMain.Panel2MinSize)
                splitMain.SplitterDistance = mitad;
        }

        private static void AjustarIcono(SimpleButton btn, int px)
        {
            Image original = btn.ImageOptions.Image;
            if (original == null) return;

            var bmp = new Bitmap(px, px);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.DrawImage(original, 0, 0, px, px);
            }
            btn.ImageOptions.Image = bmp;
        }

        #region === CARGA DE COMBOS ===

        private void CargarZafra()
        {
            var dt = _dal.EjecutarConsulta("[EGENERALES].[SP_ZAFRA]", new { ACCION = "OBTENER_CB" });

            cbxZAFRA.DataSource = dt;
            cbxZAFRA.ValueMember = "ID_ZAFRA";
            cbxZAFRA.DisplayMember = "NOMBRE_ZAFRA";
        }

        private void CargarCatorcena(int idZafra)
        {
            var dt = _dal.EjecutarConsulta("[EGENERALES].[SP_CATORCENA]",
                new { ACCION = "OBTENER", ID_ZAFRA = idZafra });

            cbxCATORCENA.DataSource = dt;
            cbxCATORCENA.ValueMember = "ID_CATORCENA";
            cbxCATORCENA.DisplayMember = "CATORCENA";
        }

        private void CargarTipoPlanilla()
        {
            var dt = _dal.EjecutarConsulta("[EGENERALES].[SP_TIPO_PLANILLA]", new { ACCION = "OBTENER" });

            cbxTIPO_PLANILLA.DataSource = dt;
            cbxTIPO_PLANILLA.ValueMember = "ID_TIPO_PLANILLA";
            cbxTIPO_PLANILLA.DisplayMember = "NOMBRE_TIPO_PLANILLA";
        }

        private void cbxZAFRA_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? idZafra = ValorCombo(cbxZAFRA);
            if (idZafra.HasValue)
                CargarCatorcena(idZafra.Value);
        }

        // SelectedValue puede venir como DataRowView mientras el combo se está enlazando.
        private static int? ValorCombo(System.Windows.Forms.ComboBox cbx)
        {
            object v = cbx.SelectedValue;
            if (v == null || v == DBNull.Value || v is DataRowView) return null;
            return int.TryParse(v.ToString(), out int id) ? id : (int?)null;
        }

        #endregion

        #region === GRID SUPERIOR (tabla de control: encabezado + detalle) ===

        private void ConfigurarGridCandidatos()
        {
            gridControl1.ForceInitialize();

            // ---- Maestro: ENCABEZADO ----
            // Las columnas antiguas del Designer (SP_COMPROB_PLANILLA) ya no aplican.
            gvCandidatos.Columns.Clear();
            gvCandidatos.OptionsView.ShowGroupPanel = false;
            gvCandidatos.OptionsView.ShowAutoFilterRow = false;
            gvCandidatos.OptionsView.ShowFooter = true;
            gvCandidatos.OptionsView.ColumnAutoWidth = true;
            gvCandidatos.OptionsView.EnableAppearanceEvenRow = true;
            gvCandidatos.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gvCandidatos.Appearance.FooterPanel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gvCandidatos.OptionsBehavior.Editable = false;
            gvCandidatos.OptionsFind.AlwaysVisible = true;
            gvCandidatos.OptionsFind.FindNullPrompt = "Introduzca el texto a buscar...";
            gvCandidatos.OptionsSelection.EnableAppearanceFocusedCell = false;
            // Selección múltiple (Ctrl / Shift) para el botón Generar FSE
            gvCandidatos.OptionsSelection.MultiSelect = true;
            gvCandidatos.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvCandidatos.OptionsDetail.EnableMasterViewMode = true;
            gvCandidatos.OptionsDetail.ShowDetailTabs = false;

            AgregarColumna(gvCandidatos, "ID_COMPROB_ENCA", "Id Comprob.", 70);
            AgregarColumna(gvCandidatos, "CODIPROVEEDOR_TRANSPORTISTA", "Código", 90);
            AgregarColumna(gvCandidatos, "NOMBRE_CLIENTE", "Proveedor", 260);
            AgregarColumna(gvCandidatos, "FECHA_PLANILLA", "Fecha", 85, formatoFecha: true);
            AgregarColumna(gvCandidatos, "ITEMS", "Ítems", 50);
            AgregarColumna(gvCandidatos, "AFECTO", "Afecto", 95, numerico: true, total: true);
            AgregarColumna(gvCandidatos, "IVA", "IVA", 85, numerico: true, total: true);
            AgregarColumna(gvCandidatos, "RENTA", "Renta", 85, numerico: true, total: true);
            AgregarColumna(gvCandidatos, "RETENCION_IVA", "IVA retenido", 90, numerico: true, total: true);
            AgregarColumna(gvCandidatos, "TOTAL", "Total", 95, numerico: true, total: true);
            AgregarColumna(gvCandidatos, "SALDO", "A pagar", 95, numerico: true, total: true);
            AgregarColumna(gvCandidatos, "ESTADO", "Estado", 55);
            AgregarColumna(gvCandidatos, "ID_ENTIDAD", "Id Proveedor", 75);
            AgregarColumna(gvCandidatos, "PROVEEDOR_RELACIONADO", "Proveedor relacionado", 90, visible: false);
            AgregarColumna(gvCandidatos, "DETALLES_INVALIDOS", "Detalle inválido", 85);

            var colCount = gvCandidatos.Columns["NOMBRE_CLIENTE"];
            colCount.Summary.Add(DevExpress.Data.SummaryItemType.Count, "NOMBRE_CLIENTE", "{0} proveedor(es)");

            // Filas con problemas en rojo claro, emitidas en verde claro
            gvCandidatos.RowStyle += gvCandidatos_RowStyle;

            // ---- Detalle: DETALLE (se muestra al expandir cada proveedor) ----
            gvDetalle = new GridView(gridControl1);
            gvDetalle.OptionsView.ShowGroupPanel = false;
            gvDetalle.OptionsBehavior.Editable = false;
            gvDetalle.OptionsView.ShowFooter = true;
            gvDetalle.OptionsView.ColumnAutoWidth = true;
            gvDetalle.Appearance.HeaderPanel.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
            gvDetalle.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvDetalle.ViewCaption = "Ítems";

            AgregarColumna(gvDetalle, "ID_COMPROB_DETA", "#", 35);
            AgregarColumna(gvDetalle, "DETALLE", "Concepto", 360);
            AgregarColumna(gvDetalle, "AFECTO", "Afecto", 95, numerico: true, total: true);
            AgregarColumna(gvDetalle, "IVA", "IVA", 85, numerico: true, total: true);
            AgregarColumna(gvDetalle, "RENTA", "Renta", 85, numerico: true, total: true);
            AgregarColumna(gvDetalle, "RETENCION_IVA", "IVA retenido", 90, numerico: true, total: true);
            AgregarColumna(gvDetalle, "TOTAL", "Total", 95, numerico: true, total: true);
            AgregarColumna(gvDetalle, "SALDO", "A pagar", 95, numerico: true, total: true);
            AgregarColumna(gvDetalle, "DETALLE_VALIDO", "Detalle válido", 80, visible: false);

            gvDetalle.RowStyle += gvDetalle_RowStyle;

            gridControl1.LevelTree.Nodes.Add(RELACION_DETALLE, gvDetalle);
        }

        private static void AgregarColumna(GridView view, string campo, string caption, int ancho,
            bool numerico = false, bool total = false, bool formatoFecha = false, bool visible = true)
        {
            var col = new GridColumn
            {
                FieldName = campo,
                Caption = caption,
                Width = ancho
            };
            view.Columns.Add(col);
            col.OptionsColumn.AllowEdit = false;
            col.Visible = visible;
            if (visible) col.VisibleIndex = view.VisibleColumns.Count;

            if (numerico)
            {
                col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                col.DisplayFormat.FormatString = "N2";
            }
            if (formatoFecha)
            {
                col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                col.DisplayFormat.FormatString = "dd/MM/yyyy";
            }
            if (total)
                col.Summary.Add(DevExpress.Data.SummaryItemType.Sum, campo, "{0:N2}");
        }

        private void gvCandidatos_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            var view = (GridView)sender;

            string estado = Convert.ToString(view.GetRowCellValue(e.RowHandle, "ESTADO"));
            object sinEntidad = view.GetRowCellValue(e.RowHandle, "PROVEEDOR_RELACIONADO");
            object detalleInvalido = view.GetRowCellValue(e.RowHandle, "DETALLES_INVALIDOS");
            object items = view.GetRowCellValue(e.RowHandle, "ITEMS");

            bool proveedorOk = sinEntidad != null && sinEntidad != DBNull.Value && Convert.ToInt32(sinEntidad) == 1;
            bool detalleOk = (detalleInvalido == null || detalleInvalido == DBNull.Value || Convert.ToInt32(detalleInvalido) == 0)
                && items != null && items != DBNull.Value && Convert.ToInt32(items) == 1;

            if (estado == "1")
                e.Appearance.BackColor = Color.FromArgb(226, 239, 218);   // emitido
            else if (!proveedorOk || !detalleOk)
                e.Appearance.BackColor = Color.FromArgb(252, 228, 214);   // no se puede emitir
        }

        private void gvDetalle_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            var view = (GridView)sender;
            object rel = view.GetRowCellValue(e.RowHandle, "DETALLE_VALIDO");
            if (rel != null && rel != DBNull.Value && Convert.ToInt32(rel) == 0)
                e.Appearance.BackColor = Color.FromArgb(252, 228, 214);
        }

        private bool ValidarFiltros(bool exigirZafra)
        {
            if ((exigirZafra && ValorCombo(cbxZAFRA) == null) ||
                ValorCombo(cbxCATORCENA) == null ||
                ValorCombo(cbxTIPO_PLANILLA) == null)
            {
                XtraMessageBox.Show("Debe seleccionar Zafra, Catorcena y Tipo Planilla.",
                    "Factura de Sujeto Excluido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (!ValidarFiltros(exigirZafra: true)) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                CargarCandidatos();
                CargarGridDocumentos();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error al consultar: {ex.Message}", "Factura de Sujeto Excluido",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void CargarCandidatos()
        {
            var filtros = new
            {
                ID_ZAFRA = ValorCombo(cbxZAFRA),
                ID_CATORCENA = ValorCombo(cbxCATORCENA),
                ID_TIPO_PLANILLA = ValorCombo(cbxTIPO_PLANILLA),
                ES_CONTRIBUYENTE = ES_CONTRIBUYENTE
            };

            DataTable dtEnc = _dal.EjecutarConsulta(SP_PLANILLA, new
            {
                ACCION = "ENCABEZADO",
                filtros.ID_ZAFRA,
                filtros.ID_CATORCENA,
                filtros.ID_TIPO_PLANILLA,
                filtros.ES_CONTRIBUYENTE
            });
            DataTable dtDet = _dal.EjecutarConsulta(SP_PLANILLA, new
            {
                ACCION = "DETALLE",
                filtros.ID_ZAFRA,
                filtros.ID_CATORCENA,
                filtros.ID_TIPO_PLANILLA,
                filtros.ES_CONTRIBUYENTE
            });

            _planillaConsultada = ValorCombo(cbxTIPO_PLANILLA);
            dtEnc.TableName = "ENCABEZADO";
            dtDet.TableName = "DETALLE";

            _dsCandidatos = new DataSet();
            _dsCandidatos.Tables.Add(dtEnc);
            _dsCandidatos.Tables.Add(dtDet);
            _dsCandidatos.Relations.Add(RELACION_DETALLE,
                dtEnc.Columns["ID_COMPROB_ENCA"],
                dtDet.Columns["ID_COMPROB_ENCA"],
                false);

            gridControl1.DataSource = _dsCandidatos;
            gridControl1.DataMember = "ENCABEZADO";
            AplicarFiltroNoSellados();
            gridControl1.RefreshDataSource();
            ActualizarTituloCandidatos(dtEnc);
        }

        // Título de la sección: cuántos proveedores hay, cuántos pendientes/emitidos y cuántos con problemas.
        private void ActualizarTituloCandidatos(DataTable dtEnc)
        {
            int total = dtEnc.Rows.Count, emitidos = 0, conProblema = 0;
            foreach (DataRow r in dtEnc.Rows)
            {
                if (Convert.ToString(r["ESTADO"]) == "1") { emitidos++; continue; }
                bool proveedorOk = ValorEntero(r, "PROVEEDOR_RELACIONADO") == 1;

                // Compatibilidad temporal: los SP anteriores devuelven PRODUCTOS_SIN_RELACION;
                // los nuevos devolverán DETALLES_INVALIDOS porque una FSE no usa productos.
                int detallesInvalidos = dtEnc.Columns.Contains("DETALLES_INVALIDOS")
                    ? ValorEntero(r, "DETALLES_INVALIDOS") ?? 0
                    : ValorEntero(r, "PRODUCTOS_SIN_RELACION") ?? 0;
                bool detalleOk = detallesInvalidos == 0 && ValorEntero(r, "ITEMS") == 1;
                if (!proveedorOk || !detalleOk) conProblema++;
            }

            lblTituloCandidatos.Text =
                $"Comprobantes de la planilla  —  {total} proveedor(es)   |   Pendientes: {total - emitidos}" +
                $"   |   Emitidos: {emitidos}" +
                (conProblema > 0 ? $"   |   Con problemas: {conProblema}" : string.Empty);
        }

        private static int? ValorEntero(DataRow fila, string columna)
        {
            if (fila == null || !fila.Table.Columns.Contains(columna) || fila[columna] == DBNull.Value)
                return null;

            return int.TryParse(Convert.ToString(fila[columna]), out int valor) ? valor : (int?)null;
        }

        // "No Sellados" = solo los que aún no se han emitido (ESTADO distinto de '1').
        private void AplicarFiltroNoSellados()
        {
            gvCandidatos.ActiveFilterString = chkNoSellados.Checked ? "[ESTADO] <> '1'" : string.Empty;
        }

        #endregion

        #region === GRID INFERIOR (documentos ya generados) ===
        // FSE generadas desde la planilla (acción GENERADOS del procedimiento correspondiente).

        private void ConfigurarGridDocumentos()
        {
            gridControl2.ForceInitialize();
            gvDocumentos.OptionsView.ShowGroupPanel = false;
            gvDocumentos.OptionsView.ShowAutoFilterRow = false;
            gvDocumentos.OptionsBehavior.Editable = false;
            gvDocumentos.OptionsFind.AlwaysVisible = true;
            gvDocumentos.OptionsFind.FindNullPrompt = "Introduzca el texto a buscar...";
            gvDocumentos.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvDocumentos.OptionsView.ColumnAutoWidth = true;
            gvDocumentos.OptionsView.EnableAppearanceEvenRow = true;
            gvDocumentos.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gvDocumentos.RowCellClick += gvDocumentos_RowCellClick;
        }

        // DTE ya generados desde la planilla, con los MISMOS filtros del encabezado
        // (catorcena, tipo planilla y tipo contribuyente). SP ... 'GENERADOS'.
        private void CargarGridDocumentos()
        {
            int? zafra = ValorCombo(cbxZAFRA);
            int? catorcena = ValorCombo(cbxCATORCENA);
            int? planilla = ValorCombo(cbxTIPO_PLANILLA);

            if (zafra == null || catorcena == null || planilla == null)
            {
                gridControl2.DataSource = null;
                lblTituloDocumentos.Text = TituloDocumentos(0);
                return;
            }

            _dtDocumentos = _dal.EjecutarConsulta(SP_PLANILLA, new
            {
                ACCION = "GENERADOS_FSE",
                ID_ZAFRA = zafra,
                ID_CATORCENA = catorcena,
                ID_TIPO_PLANILLA = planilla,
                ES_CONTRIBUYENTE = ES_CONTRIBUYENTE
            });

            gridControl2.DataSource = _dtDocumentos;
            gvDocumentos.PopulateColumns();
            AjustarColumnasDocumentos();
            gridControl2.Refresh();
            lblTituloDocumentos.Text = TituloDocumentos(_dtDocumentos.Rows.Count);
        }

        private string TituloDocumentos(int cantidad)
        {
            string tipo = "FSE generadas";
            return $"{tipo}  —  {cantidad} documento(s)   |   {cbxZAFRA.Text}  ·  Catorcena {cbxCATORCENA.Text}  ·  {cbxTIPO_PLANILLA.Text}";
        }

        // Columnas del grid inferior (acción GENERADOS), en este orden.
        private void AjustarColumnasDocumentos()
        {
            foreach (GridColumn c in gvDocumentos.Columns)
                c.Visible = false;

            int orden = 0;
            void Mostrar(string campo, string caption, int ancho, bool numerico = false, bool fecha = false)
            {
                var col = gvDocumentos.Columns[campo];
                if (col == null) return;
                col.Caption = caption;
                col.Width = ancho;
                col.OptionsColumn.AllowEdit = false;
                if (numerico)
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "N2";
                }
                if (fecha)
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    col.DisplayFormat.FormatString = "dd/MM/yyyy";
                }
                col.Visible = true;
                col.VisibleIndex = orden++;
            }

            Mostrar("ID_FSE", "Id FSE", 60);
            Mostrar("CODIGO_ENTIDAD", "Código", 95);
            Mostrar("NOMBRE_ENTIDAD", "Proveedor", 230);
            Mostrar("FECHA_EMISION", "Fecha", 80, fecha: true);
            Mostrar("NUM_CONTROL", "N° Control", 220);
            Mostrar("COD_GENERACION", "Cód. generación", 220);
            Mostrar("MONTO", "Monto", 90, numerico: true);
            Mostrar("IVA", "IVA", 80, numerico: true);
            Mostrar("RENTA", "Renta", 80, numerico: true);
            Mostrar("IVAR", "IVA retenido", 90, numerico: true);
            Mostrar("TOTAL", "Total", 95, numerico: true);
            Mostrar("SALDO", "A pagar", 95, numerico: true);
            Mostrar("SELLO_RECIBIDO", "Sello recibido", 200);
            Mostrar("ANULADO", "Anulado", 60);
        }

        private int? ObtenerIdDocumentoFilaActiva()
        {
            if (gvDocumentos.FocusedRowHandle < 0) return null;
            object val = gvDocumentos.GetRowCellValue(gvDocumentos.FocusedRowHandle, "ID_FSE");
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }

        private void gvDocumentos_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks < 2) return;   // doble clic abre el documento
            int? id = ObtenerIdDocumentoFilaActiva();
            if (!id.HasValue) return;

            AbrirDocumento(id.Value);
        }

        private void AbrirDocumento(int id)
        {
            using (var frm = new frmFacturaSujetoExcluido())
            {
                frm.IdFse = id;
                frm.ShowDialog(this);
            }

            CargarGridDocumentos();
        }

        #endregion

        #region === BOTONES ===

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            AbrirDocumento(0);
        }

        // Importar = CARGAR: lee la planilla de la zafra/catorcena/tipo planilla y guarda
        // un encabezado y un único detalle por proveedor en las tablas de control.
        // No duplica: los proveedores ya cargados se omiten.
        private void btnImportar_Click(object sender, EventArgs e)
        {
            if (!ValidarFiltros(exigirZafra: true)) return;

            string resumen =
                $"Zafra: {cbxZAFRA.Text}\n" +
                $"Catorcena: {cbxCATORCENA.Text}\n" +
                $"Tipo Planilla: {cbxTIPO_PLANILLA.Text}";

            if (XtraMessageBox.Show("¿Importar la planilla?\n\n" + resumen,
                    "Factura de Sujeto Excluido", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                Cursor = Cursors.WaitCursor;

                DataTable dt = _dal.EjecutarConsulta(SP_PLANILLA, new
                {
                    ACCION = "CARGAR_FSE",
                    ID_ZAFRA = ValorCombo(cbxZAFRA),
                    ID_CATORCENA = ValorCombo(cbxCATORCENA),
                    ID_TIPO_PLANILLA = ValorCombo(cbxTIPO_PLANILLA),
                    ES_CONTRIBUYENTE = ES_CONTRIBUYENTE,
                    USUARIO = Configuracion.UsuarioActual
                });

                int encabezados = 0, items = 0;
                string fecha = string.Empty;
                if (dt != null && dt.Rows.Count > 0)
                {
                    encabezados = Convert.ToInt32(dt.Rows[0]["ENCABEZADOS_CARGADOS"]);
                    items = Convert.ToInt32(dt.Rows[0]["ITEMS_CARGADOS"]);
                    if (dt.Rows[0]["FECHA_PLANILLA"] != DBNull.Value)
                        fecha = Convert.ToDateTime(dt.Rows[0]["FECHA_PLANILLA"]).ToString("dd/MM/yyyy");
                }

                CargarCandidatos();
                CargarGridDocumentos();

                string msg = encabezados == 0
                    ? "No había proveedores nuevos por cargar (ya estaban importados)."
                    : $"Se importaron {encabezados} comprobante(s) con {items} ítem(s).\nFecha de la planilla: {fecha}";

                XtraMessageBox.Show(msg, "Factura de Sujeto Excluido",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"No se pudo importar la planilla:\n\n{ex.Message}",
                    "Factura de Sujeto Excluido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // Genera una FSE por cada fila seleccionada. Solo procesa pendientes con proveedor
        // relacionado y exactamente un detalle válido.
        // Cada FSE va en su propia transacción: si una falla, las demás continúan.
        // La emision usa el tipo de planilla con que se CONSULTO el grid (los Id son de esa tabla)
        private int? _planillaConsultada;
        private string SP_EMITIR => _planillaConsultada == TIPO_PLANILLA_TRANSPORTISTAS ? SP_PLANILLA_TRAS : SP_PLANILLA_CANIERO;

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (_dsCandidatos == null || gvCandidatos.RowCount == 0)
            {
                XtraMessageBox.Show("Primero consulte la planilla.", "Generar FSE",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_planillaConsultada != ValorCombo(cbxTIPO_PLANILLA))
            {
                XtraMessageBox.Show("Cambió el Tipo de Planilla. Presione Consultar antes de generar.", "Generar FSE",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Filas seleccionadas (Ctrl / Shift)
            var handles = new System.Collections.Generic.List<int>();
            foreach (int h in gvCandidatos.GetSelectedRows())
                if (h >= 0) handles.Add(h);

            // Si hay una sola fila (o ninguna) seleccionada, preguntar si se generan TODAS las pendientes
            if (handles.Count <= 1)
            {
                int pendientesTotal = 0;
                for (int h = 0; h < gvCandidatos.DataRowCount; h++)
                    if (Convert.ToString(gvCandidatos.GetRowCellValue(h, "ESTADO")) != "1") pendientesTotal++;

                var resp = XtraMessageBox.Show(
                    $"¿Generar TODAS las pendientes de la planilla ({pendientesTotal})?\n\n" +
                    "Sí = todas las pendientes\n" +
                    "No = solo la fila seleccionada\n" +
                    "Cancelar = no generar",
                    "Generar FSE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (resp == DialogResult.Cancel) return;

                handles.Clear();
                if (resp == DialogResult.Yes)
                {
                    for (int h = 0; h < gvCandidatos.DataRowCount; h++) handles.Add(h);
                }
                else
                {
                    int foco = gvCandidatos.FocusedRowHandle;
                    if (foco >= 0) handles.Add(foco);
                }
            }

            var pendientes = new System.Collections.Generic.List<(int Id, string Proveedor)>();
            int emitidas = 0, conProblema = 0;
            foreach (int h in handles)
            {
                string estado = Convert.ToString(gvCandidatos.GetRowCellValue(h, "ESTADO"));
                object rel = gvCandidatos.GetRowCellValue(h, "PROVEEDOR_RELACIONADO");
                object detalleInvalido = gvCandidatos.GetRowCellValue(h, "DETALLES_INVALIDOS");
                object items = gvCandidatos.GetRowCellValue(h, "ITEMS");
                bool proveedorOk = rel != null && rel != DBNull.Value && Convert.ToInt32(rel) == 1;
                bool detalleOk = (detalleInvalido == null || detalleInvalido == DBNull.Value || Convert.ToInt32(detalleInvalido) == 0)
                    && items != null && items != DBNull.Value && Convert.ToInt32(items) == 1;

                if (estado == "1") { emitidas++; continue; }
                if (!proveedorOk || !detalleOk) { conProblema++; continue; }

                pendientes.Add((Convert.ToInt32(gvCandidatos.GetRowCellValue(h, "ID_COMPROB_ENCA")),
                                Convert.ToString(gvCandidatos.GetRowCellValue(h, "NOMBRE_CLIENTE"))));
            }

            if (pendientes.Count == 0)
            {
                XtraMessageBox.Show(
                    "No hay filas pendientes para generar en la selección." +
                    (emitidas > 0 ? $"\n- Ya emitidas: {emitidas}" : string.Empty) +
                    (conProblema > 0 ? $"\n- Con proveedor o detalle inválido: {conProblema}" : string.Empty),
                    "Generar FSE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string aviso = $"¿Generar {pendientes.Count} FSE?";
            if (emitidas > 0) aviso += $"\n\nSe omiten {emitidas} ya emitida(s).";
            if (conProblema > 0) aviso += $"\nSe omiten {conProblema} con proveedor o detalle inválido.";
            if (XtraMessageBox.Show(aviso, "Generar FSE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            int ok = 0;
            var errores = new System.Text.StringBuilder();
            string tituloOriginal = lblTituloCandidatos.Text;

            try
            {
                Cursor = Cursors.WaitCursor;
                btnGenerar.Enabled = false;

                for (int i = 0; i < pendientes.Count; i++)
                {
                    var p = pendientes[i];
                    lblTituloCandidatos.Text = $"Generando FSE {i + 1} de {pendientes.Count}  —  {p.Proveedor}";
                    Application.DoEvents();

                    try
                    {
                        // El SP ya no usa INSERT-EXEC: los SP internos (ENC/DET) devuelven sus
                        // propios result sets y el resultado de la emision es el ULTIMO.
                        DataSet ds = _dal.EjecutarMultiple(SP_EMITIR, new
                        {
                            ACCION = "EMITIR_FSE",
                            ID_COMPROB_ENCA = p.Id,
                            ID_ZAFRA = ValorCombo(cbxZAFRA),
                            USUARIO = Configuracion.UsuarioActual
                        });

                        DataTable r = null;
                        if (ds != null)
                            for (int t = ds.Tables.Count - 1; t >= 0; t--)
                                if (ds.Tables[t].Columns.Contains("RESULTADO") && ds.Tables[t].Columns.Contains("MENSAJE"))
                                { r = ds.Tables[t]; break; }

                        string resultado = r != null && r.Rows.Count > 0 ? Convert.ToString(r.Rows[0]["RESULTADO"]) : "ERROR";
                        string mensaje = r != null && r.Rows.Count > 0 ? Convert.ToString(r.Rows[0]["MENSAJE"]) : "Sin respuesta del SP.";

                        if (resultado == "OK") ok++;
                        else errores.AppendLine($"• {p.Proveedor}: {mensaje}");
                    }
                    catch (Exception exFila)
                    {
                        errores.AppendLine($"• {p.Proveedor}: {exFila.Message}");
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Default;
                btnGenerar.Enabled = true;
                lblTituloCandidatos.Text = tituloOriginal;
            }

            // Refrescar ambos grids
            try
            {
                CargarCandidatos();
                CargarGridDocumentos();
            }
            catch { /* el resumen se muestra igual */ }

            int fallidos = pendientes.Count - ok;
            string resumen = $"FSE generadas: {ok}\nCon error: {fallidos}";
            if (fallidos > 0)
            {
                string detalle = errores.ToString();
                if (detalle.Length > 3000) detalle = detalle.Substring(0, 3000) + "\n...";
                resumen += "\n\n" + detalle;
            }

            XtraMessageBox.Show(resumen, "Generar FSE", MessageBoxButtons.OK,
                fallidos == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        private void btnEliminarPruebas_Click(object sender, EventArgs e)
        {
            if (!ValidarFiltros(exigirZafra: true)) return;

            string alcance =
                $"Zafra: {cbxZAFRA.Text}\n" +
                $"Catorcena: {cbxCATORCENA.Text}\n" +
                $"Tipo Planilla: {cbxTIPO_PLANILLA.Text}";

            if (XtraMessageBox.Show(
                    "Se eliminarán solamente las pruebas de Sujeto Excluido generadas desde esta planilla:\n\n" +
                    alcance +
                    "\n\nNo se eliminarán FSE manuales, facturas ni créditos fiscales.\n\n¿Desea continuar?",
                    "Eliminar pruebas de Sujeto Excluido",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            if (XtraMessageBox.Show(
                    "Esta acción eliminará las FSE de prueba y su importación de planilla.\n\n¿Confirma la eliminación?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Stop,
                    MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            try
            {
                Cursor = Cursors.WaitCursor;
                btnEliminarPruebas.Enabled = false;

                DataTable resultado = _dal.EjecutarConsulta(SP_PLANILLA, new
                {
                    ACCION = "ELIMINAR_PRUEBAS_FSE",
                    ID_ZAFRA = ValorCombo(cbxZAFRA),
                    ID_CATORCENA = ValorCombo(cbxCATORCENA),
                    ID_TIPO_PLANILLA = ValorCombo(cbxTIPO_PLANILLA),
                    USUARIO = Configuracion.UsuarioActual
                });

                int fse = 0, encabezados = 0, detalles = 0;
                if (resultado != null && resultado.Rows.Count > 0)
                {
                    DataRow fila = resultado.Rows[0];
                    fse = ValorEntero(fila, "FSE_ELIMINADAS") ?? 0;
                    encabezados = ValorEntero(fila, "ENCABEZADOS_ELIMINADOS") ?? 0;
                    detalles = ValorEntero(fila, "DETALLES_ELIMINADOS") ?? 0;
                }

                CargarCandidatos();
                CargarGridDocumentos();

                XtraMessageBox.Show(
                    $"FSE de prueba eliminadas: {fse}\n" +
                    $"Encabezados de importación eliminados: {encabezados}\n" +
                    $"Detalles de importación eliminados: {detalles}",
                    "Eliminar pruebas de Sujeto Excluido",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "No se pudieron eliminar las pruebas de Sujeto Excluido:\n\n" + ex.Message,
                    "Eliminar pruebas de Sujeto Excluido",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnEliminarPruebas.Enabled = true;
            }
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            if (gvCandidatos.RowCount == 0)
            {
                XtraMessageBox.Show("No hay datos para mostrar en la vista previa.",
                    "Reporte", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            gridControl1.ShowPrintPreview();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}

