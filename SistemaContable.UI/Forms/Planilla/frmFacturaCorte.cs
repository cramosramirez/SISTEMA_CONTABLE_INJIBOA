using SistemaContable.DAL;
using SistemaContable.UI.Forms.Ventas; // frmCreditoFiscal vive en Ventas
using SistemaContable.UI.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace SistemaContable.UI.Forms.Planilla
{
    // Factura de Planilla (cañeros NO contribuyentes) - copia de frmFacturaCorte (2026-09-29).
    // Tablas de control: [ECOMPROB].[PLANILLA_CANIERO_ENCA] / [PLANILLA_CANIERO_DETA]
    // SP: [ECOMPROB].[SP_GENERAR_COMPROB_PLANILLA_CANIERO]
    //   Siempre @ES_CONTRIBUYENTE = 0 (Factura, ID_TIPO_DTE = 1); definido por Roberto 2026-09-29
    //   CARGAR     -> botón Importar (lee la planilla y guarda ENCA/DETA)
    //   ENCABEZADO -> grid superior (maestro)
    //   DETALLE    -> grid superior (detalle al expandir cada proveedor)
    // El Crédito Fiscal (ID_TIPO_DTE = 2) se maneja en frmFacturaCorte.
    // Emisión: [ECOMPROB].[SP_EMITIR_DTE_CANIERO_FAC] -> EDTE.FACTURA_ENC / FACTURA_DET.
    public partial class frmFacturaCorte : Form
    {
        // Tipo Planilla = 2 (TRANSPORTISTAS) usa sus propias tablas y SPs (PLANILLA_TRAS_ENCA / _DETA);
        // el resto de planillas usa los de caneros (PLANILLA_CANIERO_ENCA / _DETA). 2026-09-30
        private const int TIPO_PLANILLA_TRANSPORTISTAS = 2;
        private const string SP_PLANILLA_CANIERO = "[ECOMPROB].[SP_GENERAR_COMPROB_PLANILLA_CANIERO]";
        private const string SP_PLANILLA_TRAS    = "[ECOMPROB].[SP_GENERAR_COMPROB_PLANILLA_TRAS]";
        private bool EsTransportistas => ValorCombo(cbxTIPO_PLANILLA) == TIPO_PLANILLA_TRANSPORTISTAS;
        private string SP_PLANILLA => EsTransportistas ? SP_PLANILLA_TRAS : SP_PLANILLA_CANIERO;
        private const string RELACION_DETALLE = "Detalle";
        // Esta pantalla trabaja siempre con NO contribuyentes (Factura). Definido por Roberto 2026-09-29.
        private const int ES_CONTRIBUYENTE = 0;

        private readonly DALBase _dal = new DALBase();
        private DataSet _dsCandidatos;
        private DataTable _dtDocumentos;
        private GridView gvDetalle;

        public frmFacturaCorte()
        {
            InitializeComponent();
        }

        private void frmFacturaCorte_Load(object sender, EventArgs e)
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

        // Las dos secciones (planilla arriba / facturas generadas abajo) del mismo alto,
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
            // Selección múltiple (Ctrl / Shift) para el botón Generar Factura
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
            AgregarColumna(gvCandidatos, "EXENTO", "Exento", 80, numerico: true, total: true);
            AgregarColumna(gvCandidatos, "IVA", "IVA", 85, numerico: true, total: true);
            AgregarColumna(gvCandidatos, "TOTAL", "Total", 95, numerico: true, total: true);
            AgregarColumna(gvCandidatos, "ESTADO", "Estado", 55);
            AgregarColumna(gvCandidatos, "ID_ENTIDAD", "Id Cliente", 70);
            AgregarColumna(gvCandidatos, "PROVEEDOR_RELACIONADO", "Cliente relacionado", 90, visible: false);
            AgregarColumna(gvCandidatos, "PRODUCTOS_SIN_RELACION", "Prod. sin relación", 90);

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
            AgregarColumna(gvDetalle, "CODIGOART", "Cód. Art.", 70);
            AgregarColumna(gvDetalle, "DETALLE", "Detalle planilla", 220);
            AgregarColumna(gvDetalle, "NOMBRE_PRODUCTO", "Producto PH2", 220);
            AgregarColumna(gvDetalle, "UNIDAD_MEDIDA", "U.M.", 70);
            AgregarColumna(gvDetalle, "CANTIDAD", "Cantidad", 70, numerico: true);
            AgregarColumna(gvDetalle, "AFECTO", "Afecto", 95, numerico: true, total: true);
            AgregarColumna(gvDetalle, "EXENTO", "Exento", 80, numerico: true, total: true);
            AgregarColumna(gvDetalle, "TOTAL", "Total", 95, numerico: true, total: true);
            AgregarColumna(gvDetalle, "PRODUCTO_RELACIONADO", "Producto relacionado", 80, visible: false);

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
            object sinProd = view.GetRowCellValue(e.RowHandle, "PRODUCTOS_SIN_RELACION");

            bool proveedorOk = sinEntidad != null && sinEntidad != DBNull.Value && Convert.ToInt32(sinEntidad) == 1;
            bool productosOk = sinProd == null || sinProd == DBNull.Value || Convert.ToInt32(sinProd) == 0;

            if (estado == "1")
                e.Appearance.BackColor = Color.FromArgb(226, 239, 218);   // emitido
            else if (!proveedorOk || !productosOk)
                e.Appearance.BackColor = Color.FromArgb(252, 228, 214);   // no se puede emitir
        }

        private void gvDetalle_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            var view = (GridView)sender;
            object rel = view.GetRowCellValue(e.RowHandle, "PRODUCTO_RELACIONADO");
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
                    "Factura de Planilla", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                XtraMessageBox.Show($"Error al consultar: {ex.Message}", "Factura de Planilla",
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
                bool proveedorOk = r["PROVEEDOR_RELACIONADO"] != DBNull.Value && Convert.ToInt32(r["PROVEEDOR_RELACIONADO"]) == 1;
                bool productosOk = r["PRODUCTOS_SIN_RELACION"] == DBNull.Value || Convert.ToInt32(r["PRODUCTOS_SIN_RELACION"]) == 0;
                if (!proveedorOk || !productosOk) conProblema++;
            }

            lblTituloCandidatos.Text =
                $"Comprobantes de la planilla  —  {total} proveedor(es)   |   Pendientes: {total - emitidos}" +
                $"   |   Emitidos: {emitidos}" +
                (conProblema > 0 ? $"   |   Con problemas: {conProblema}" : string.Empty);
        }

        // "No Sellados" = solo los que aún no se han emitido (ESTADO distinto de '1').
        private void AplicarFiltroNoSellados()
        {
            gvCandidatos.ActiveFilterString = chkNoSellados.Checked ? "[ESTADO] <> '1'" : string.Empty;
        }

        #endregion

        #region === GRID INFERIOR (documentos ya generados) ===
        // DTE generados desde la planilla (SP_GENERAR_COMPROB_PLANILLA_CANIERO 'GENERADOS').

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
                ACCION = "GENERADOS",
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
            string tipo = "Facturas generadas";
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

            Mostrar("ID_COMPROB_ENCA", "Id Comprob.", 70);
            Mostrar("CODIPROVEEDOR_TRANSPORTISTA", "Código", 90);
            Mostrar("NOMBRE_ENTIDAD", "Cliente", 240);
            Mostrar("NUMINTERNO", "N° Interno", 120);
            if (gvDocumentos.Columns["NUMINTERNO"] != null)
                gvDocumentos.Columns["NUMINTERNO"].MinWidth = 115;   // que se vea el numero completo
            Mostrar("FECHA", "Fecha", 80, fecha: true);
            Mostrar("NUMCONTROL", "N° Control", 220);
            Mostrar("AFECTA", "Afecto", 90, numerico: true);
            Mostrar("IVA", "IVA", 80, numerico: true);
            Mostrar("TOTALVENTA", "Total Venta", 95, numerico: true);
            Mostrar("SELLORECEPCION", "Sello Recepción", 200);
            Mostrar("ANULADA", "Anulada", 60);
        }

        private int? ObtenerIdDocumentoFilaActiva()
        {
            if (gvDocumentos.FocusedRowHandle < 0) return null;
            object val = gvDocumentos.GetRowCellValue(gvDocumentos.FocusedRowHandle, "ID_FACTENC");
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }

        private void gvDocumentos_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks < 2) return;   // doble clic abre el documento
            int? id = ObtenerIdDocumentoFilaActiva();
            if (!id.HasValue) return;

            object tipo = gvDocumentos.GetRowCellValue(gvDocumentos.FocusedRowHandle, "ID_TIPO_DTE");
            bool esFactura = tipo != null && tipo != DBNull.Value && Convert.ToInt32(tipo) == 1;
            AbrirDocumento(id.Value, esFactura);
        }

        // CCF -> frmCreditoFiscal ; Factura -> frmFactura
        private void AbrirDocumento(int id, bool esFactura)
        {
            if (esFactura)
            {
                using (var frm = new frmFactura())
                {
                    frm.IdFactEnc = id;
                    frm.ShowDialog(this);
                }
            }
            else
            {
                using (var frm = new frmCreditoFiscal())
                {
                    frm.IdCCFEnc = id;
                    frm.ShowDialog(this);
                }
            }

            CargarGridDocumentos();
        }

        #endregion

        #region === BOTONES ===

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            AbrirDocumento(0, esFactura: true);
        }

        // Importar = CARGAR: lee la planilla de la zafra/catorcena/tipo planilla y guarda
        // un encabezado por proveedor (con sus ítems) en la tabla de control.
        // No duplica: los proveedores ya cargados se omiten.
        private void btnImportar_Click(object sender, EventArgs e)
        {
            if (!ValidarFiltros(exigirZafra: true)) return;

            string resumen =
                $"Zafra: {cbxZAFRA.Text}\n" +
                $"Catorcena: {cbxCATORCENA.Text}\n" +
                $"Tipo Planilla: {cbxTIPO_PLANILLA.Text}";

            if (XtraMessageBox.Show("¿Importar la planilla?\n\n" + resumen,
                    "Factura de Planilla", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                Cursor = Cursors.WaitCursor;

                DataTable dt = _dal.EjecutarConsulta(SP_PLANILLA, new
                {
                    ACCION = "CARGAR",
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

                XtraMessageBox.Show(msg, "Factura de Planilla",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"No se pudo importar la planilla:\n\n{ex.Message}",
                    "Factura de Planilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // Generar Factura = SP_EMITIR_DTE_CANIERO_FAC por cada fila seleccionada arriba.
        // Solo procesa las pendientes (ESTADO <> '1') con cliente y productos relacionados.
        // Cada factura va en su propia transacción: si uno falla, los demás siguen.
        private const string SP_EMITIR_CANIERO = "[ECOMPROB].[SP_EMITIR_DTE_CANIERO_FAC]";
        private const string SP_EMITIR_TRAS    = "[ECOMPROB].[SP_EMITIR_DTE_TRAS_FAC]";
        // La emision usa el tipo de planilla con que se CONSULTO el grid (los Id son de esa tabla)
        private int? _planillaConsultada;
        private string SP_EMITIR => _planillaConsultada == TIPO_PLANILLA_TRANSPORTISTAS ? SP_EMITIR_TRAS : SP_EMITIR_CANIERO;

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (_dsCandidatos == null || gvCandidatos.RowCount == 0)
            {
                XtraMessageBox.Show("Primero consulte la planilla.", "Generar Factura",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_planillaConsultada != ValorCombo(cbxTIPO_PLANILLA))
            {
                XtraMessageBox.Show("Cambió el Tipo de Planilla. Presione Consultar antes de generar.", "Generar Factura",
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
                    "Generar Factura", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

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

            var clientesAValidar = new System.Collections.Generic.List<ClientePlanillaValidacion>();
            foreach (int h in handles)
            {
                if (Convert.ToString(gvCandidatos.GetRowCellValue(h, "ESTADO")) == "1") continue;

                object idEntidad = gvCandidatos.GetRowCellValue(h, "ID_ENTIDAD");
                clientesAValidar.Add(new ClientePlanillaValidacion
                {
                    IdComprobante = Convert.ToInt32(gvCandidatos.GetRowCellValue(h, "ID_COMPROB_ENCA")),
                    IdEntidad = idEntidad == null || idEntidad == DBNull.Value
                        ? (int?)null
                        : Convert.ToInt32(idEntidad),
                    CodigoImportado = Convert.ToString(gvCandidatos.GetRowCellValue(h, "CODIPROVEEDOR_TRANSPORTISTA")),
                    Nombre = Convert.ToString(gvCandidatos.GetRowCellValue(h, "NOMBRE_CLIENTE"))
                });
            }

            var problemasClientes = ValidadorClientesPlanilla.Validar(
                _dal, clientesAValidar, _planillaConsultada == TIPO_PLANILLA_TRANSPORTISTAS);
            if (problemasClientes.Count > 0)
            {
                string detalle = string.Join("\n", problemasClientes.Take(25).Select(x => "• " + x));
                if (problemasClientes.Count > 25)
                    detalle += $"\n• ... y {problemasClientes.Count - 25} problema(s) adicional(es).";

                XtraMessageBox.Show(
                    "No se puede generar porque existen problemas en los datos de clientes:\n\n" + detalle +
                    "\n\nCorrija los datos, vuelva a consultar y genere nuevamente.",
                    "Validación de clientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pendientes = new System.Collections.Generic.List<(int Id, string Proveedor)>();
            int emitidas = 0, conProblema = 0;
            foreach (int h in handles)
            {
                string estado = Convert.ToString(gvCandidatos.GetRowCellValue(h, "ESTADO"));
                object rel = gvCandidatos.GetRowCellValue(h, "PROVEEDOR_RELACIONADO");
                object sinProd = gvCandidatos.GetRowCellValue(h, "PRODUCTOS_SIN_RELACION");
                bool proveedorOk = rel != null && rel != DBNull.Value && Convert.ToInt32(rel) == 1;
                bool productosOk = sinProd == null || sinProd == DBNull.Value || Convert.ToInt32(sinProd) == 0;

                if (estado == "1") { emitidas++; continue; }
                if (!proveedorOk || !productosOk) { conProblema++; continue; }

                pendientes.Add((Convert.ToInt32(gvCandidatos.GetRowCellValue(h, "ID_COMPROB_ENCA")),
                                Convert.ToString(gvCandidatos.GetRowCellValue(h, "NOMBRE_CLIENTE"))));
            }

            if (pendientes.Count == 0)
            {
                XtraMessageBox.Show(
                    "No hay filas pendientes para generar en la selección." +
                    (emitidas > 0 ? $"\n- Ya emitidas: {emitidas}" : string.Empty) +
                    (conProblema > 0 ? $"\n- Con cliente o producto sin relacionar: {conProblema}" : string.Empty),
                    "Generar Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string aviso = $"¿Generar {pendientes.Count} factura(s)?";
            if (emitidas > 0) aviso += $"\n\nSe omiten {emitidas} ya emitida(s).";
            if (conProblema > 0) aviso += $"\nSe omiten {conProblema} con cliente o producto sin relacionar.";
            if (XtraMessageBox.Show(aviso, "Generar Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
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
                    lblTituloCandidatos.Text = $"Generando factura {i + 1} de {pendientes.Count}  —  {p.Proveedor}";
                    Application.DoEvents();

                    try
                    {
                        // El SP ya no usa INSERT-EXEC: los SP internos (ENC/DET) devuelven sus
                        // propios result sets y el resultado de la emision es el ULTIMO.
                        DataSet ds = _dal.EjecutarMultiple(SP_EMITIR, new
                        {
                            ID_COMPROB_ENCA = p.Id,
                            ID_ZAFRA = ValorCombo(cbxZAFRA),
                            ID_ALMACEN = Configuracion.Id_Almacen,
                            ID_CAJA = Configuracion.Id_Cajero,      // igual que frmFactura
                            ID_CAJERO = Configuracion.Id_Cajero,
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
            string resumen = $"Facturas generadas: {ok}\nCon error: {fallidos}";
            if (fallidos > 0)
            {
                string detalle = errores.ToString();
                if (detalle.Length > 3000) detalle = detalle.Substring(0, 3000) + "\n...";
                resumen += "\n\n" + detalle;
            }

            XtraMessageBox.Show(resumen, "Generar Factura", MessageBoxButtons.OK,
                fallidos == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        // Eliminar pruebas (definido por Roberto 2026-09-29): borra TODAS las facturas que vienen
        // de la planilla (FACTURA_ENC/DET con ID_COMPROB_ENCA) y la planilla importada de factura
        // (PLANILLA_CANIERO_ENCA/DETA con ID_TIPO_DTE = 1). No depende de los filtros de arriba.
        private void btnEliminarPruebas_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(
                    "Se eliminará TODO lo generado desde la planilla de cañeros:\n\n" +
                    "  • Facturas de planilla (FACTURA_ENC / _DET)\n" +
                    "  • Planilla importada de factura (PLANILLA_CANIERO_ENCA / _DETA con ID_TIPO_DTE = 1)\n\n" +
                    "No importa la zafra / catorcena seleccionada.\n" +
                    "Las facturas manuales o de solicitudes agrícolas NO se tocan.\n\n" +
                    "¿Desea continuar?",
                    "Eliminar pruebas", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            if (XtraMessageBox.Show(
                    "Esta acción no se puede deshacer.\n\n¿Confirma que desea eliminar?",
                    "Eliminar pruebas", MessageBoxButtons.YesNo, MessageBoxIcon.Stop,
                    MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            try
            {
                Cursor = Cursors.WaitCursor;

                DataTable r = _dal.EjecutarConsulta(SP_PLANILLA, new
                {
                    ACCION = "ELIMINAR_PRUEBAS",
                    ES_CONTRIBUYENTE = ES_CONTRIBUYENTE,   // 0 = solo facturas (el CCF tiene su propio botón)
                    USUARIO = Configuracion.UsuarioActual
                });

                string msg = "Listo.";
                if (r != null && r.Rows.Count > 0)
                {
                    DataRow x = r.Rows[0];
                    msg = $"Facturas eliminadas: {x["CCF_ELIMINADOS"]}  (líneas de detalle: {x["CCF_DETALLE_ELIMINADOS"]})" +
                          $"\nComprobantes de planilla eliminados: {x["COMPROBANTES_IMPORTACION_ELIMINADOS"]}  (líneas: {x["DETALLE_IMPORTACION_ELIMINADOS"]})" +
                          (r.Columns.Contains("LIBRO_VENTAS_ELIMINADOS") ? $"\nLibro de ventas (EIVA.LBVENTAFA_FAE) eliminados: {x["LIBRO_VENTAS_ELIMINADOS"]}" : "");
                    msg += x["NUMERACION_NUEVA"] == DBNull.Value
                        ? "\n\nLa numeración de facturas no se movió (hay facturas emitidas después de las pruebas)."
                        : $"\n\nNumeración de facturas regresada de {x["NUMERACION_ANTERIOR"]} a {x["NUMERACION_NUEVA"]}.";
                }

                CargarCandidatos();
                CargarGridDocumentos();

                XtraMessageBox.Show(msg, "Eliminar pruebas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"No se pudo eliminar:\n\n{ex.Message}", "Eliminar pruebas",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            // TODO: pendiente de definir.
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
