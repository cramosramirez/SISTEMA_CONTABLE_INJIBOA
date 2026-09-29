using SistemaContable.DAL;
using SistemaContable.UI.Forms.Ventas; // frmCreditoFiscal vive en Ventas
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
    // Crédito Fiscal de Planilla (cañeros).
    // Tablas de control: [ECOMPROB].[PLANILLA_CANIERO_ENCA] / [PLANILLA_CANIERO_DETA]
    // SP: [ECOMPROB].[SP_GENERAR_COMPROB_PLANILLA_CANIERO]
    //   Siempre @ES_CONTRIBUYENTE = 1 (Crédito Fiscal); ya no hay combo de tipo contribuyente
    //   CARGAR     -> botón Importar (lee la planilla y guarda ENCA/DETA)
    //   ENCABEZADO -> grid superior (maestro)
    //   DETALLE    -> grid superior (detalle al expandir cada proveedor)
    // La Factura (ID_TIPO_DTE = 1) se maneja en frmFacturaCorte.
    public partial class frmCreditoFiscalCorte : Form
    {
        private const string SP_PLANILLA = "[ECOMPROB].[SP_GENERAR_COMPROB_PLANILLA_CANIERO]";
        private const string RELACION_DETALLE = "Detalle";
        // Esta pantalla trabaja siempre con contribuyentes (Crédito Fiscal). Definido por Roberto 2026-09-29.
        private const int ES_CONTRIBUYENTE = 1;

        private readonly DALBase _dal = new DALBase();
        private DataSet _dsCandidatos;
        private DataTable _dtDocumentos;
        private GridView gvDetalle;

        public frmCreditoFiscalCorte()
        {
            InitializeComponent();
        }

        private void frmCreditoFiscalCorte_Load(object sender, EventArgs e)
        {
            ConfigurarGridCandidatos();
            ConfigurarGridDocumentos();

            CargarZafra();
            CargarTipoPlanilla();

            chkNoSellados.CheckedChanged += (s, a) => AplicarFiltroNoSellados();

            // Mismo tamaño para todos los íconos (los recursos vienen en 32 y 48 px).
            foreach (var btn in new[] { btnConsultar, btnImportar, btnReporte, btnSalir, btnNuevo })
                AjustarIcono(btn, 28);
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
                    "Crédito Fiscal de Planilla", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                XtraMessageBox.Show($"Error al consultar: {ex.Message}", "Crédito Fiscal de Planilla",
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
            string tipo = "Créditos fiscales generados";
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
            Mostrar("NUMINTERNO", "N° Interno", 90);
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
            object val = gvDocumentos.GetRowCellValue(gvDocumentos.FocusedRowHandle, "ID_DTEENC");
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
            AbrirDocumento(0, esFactura: false);
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
                    "Crédito Fiscal de Planilla", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
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

                XtraMessageBox.Show(msg, "Crédito Fiscal de Planilla",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"No se pudo importar la planilla:\n\n{ex.Message}",
                    "Crédito Fiscal de Planilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
