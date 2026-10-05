using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;

namespace SistemaContable.UI.Forms.Planilla
{
    // ---------------------------------------------------------------------------
    // Inconsistencias de planilla (2026-10-05).
    // Revisión PREVIA a generar en frmCreditoFiscalCorte / frmFacturaCorte, con los
    // mismos filtros (Zafra, Catorcena, Tipo Planilla) + Tipo de documento.
    // Reúne las validaciones que hoy se hacen al generar:
    //   - Cliente: código vacío, sin cliente en ENTIDAD, ENTIDAD sin CODIPROVEEDOR
    //     (cañeros) / CODTRANSPORT (transportistas), sin CUENTA_X_COBRAR,
    //     CCF sin NRC o cliente no contribuyente.
    //   - Productos: sin ítems, sin producto PH2, sin unidad de medida,
    //     producto repetido (Factura).
    //   - Montos: cuadre encabezado vs detalle, IVA 13% (CCF).
    //   - Planilla: fecha en PLANILLA_BASE, planilla no importada.
    // SP: [ECOMPROB].[SP_INCONSISTENCIAS_PLANILLA]
    // ---------------------------------------------------------------------------
    public partial class frmInconsistenciasCorte : Form
    {
        private const string SP_INCONSISTENCIAS = "[ECOMPROB].[SP_INCONSISTENCIAS_PLANILLA]";
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtResultado;
        private string _tituloReporte = string.Empty;
        private bool _revisionTransportistas;   // tipo de planilla de la última revisión (2 = transportistas)
        // (2026-10-05) Indicador de proceso mientras se revisa la planilla.
        private System.Windows.Forms.ProgressBar _progreso;
        private System.Windows.Forms.Label _lblProgreso;

        public frmInconsistenciasCorte()
        {
            InitializeComponent();
        }

        private void frmInconsistenciasCorte_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarZafra();
            CargarTipoPlanilla();
            CargarTipoDocumento();
            chkSoloErrores.CheckedChanged += (s, a) => AplicarFiltro();

            foreach (var btn in new[] { btnConsultar, btnImprimir, btnCorregir, btnSalir })
                AjustarIcono(btn, 28);

            CrearIndicadorProgreso();

            // (2026-10-05) Botón Corregir oculto por ahora (pedido de Roberto).
            btnCorregir.Visible = false;
        }

        private void CrearIndicadorProgreso()
        {
            _progreso = new System.Windows.Forms.ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 30,
                Location = new Point(420, 12),
                Size = new Size(320, 18),
                Visible = false
            };
            _lblProgreso = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 78, 121),
                Location = new Point(420, 33),
                Visible = false
            };
            panelBotones.Controls.Add(_progreso);
            panelBotones.Controls.Add(_lblProgreso);
        }

        private System.Diagnostics.Stopwatch _cronometro;
        private System.Windows.Forms.Timer _timerProgreso;

        private void MostrarProgreso(bool visible)
        {
            _progreso.Visible = visible;
            _lblProgreso.Visible = visible;
            btnConsultar.Enabled = !visible;
            btnImprimir.Enabled = !visible;
            btnCorregir.Enabled = !visible;
            grpFiltros.Enabled = !visible;
            Cursor = visible ? Cursors.WaitCursor : Cursors.Default;

            if (visible)
            {
                _cronometro = System.Diagnostics.Stopwatch.StartNew();
                _lblProgreso.Text = "Revisando la planilla...";
                if (_timerProgreso == null)
                {
                    _timerProgreso = new System.Windows.Forms.Timer { Interval = 1000 };
                    _timerProgreso.Tick += (s, a) =>
                        _lblProgreso.Text = $"Revisando la planilla... {(int)_cronometro.Elapsed.TotalSeconds} s";
                }
                _timerProgreso.Start();
                lblResumen.BackColor = Color.FromArgb(221, 235, 247);
                lblResumen.ForeColor = Color.FromArgb(31, 78, 121);
                lblResumen.Text = "Revisando la planilla, espere un momento...";
            }
            else
            {
                _timerProgreso?.Stop();
                _cronometro?.Stop();
            }
        }

        private static void AjustarIcono(SimpleButton btn, int px)
        {
            Image original = btn.ImageOptions.Image;
            if (original == null) return;
            var bmp = new Bitmap(px, px);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(original, 0, 0, px, px);
            }
            btn.ImageOptions.Image = bmp;
        }

        #region === COMBOS ===

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

        // 1 = Crédito Fiscal (contribuyentes, frmCreditoFiscalCorte)
        // 0 = Factura (no contribuyentes, frmFacturaCorte)
        private void CargarTipoDocumento()
        {
            var dt = new DataTable();
            dt.Columns.Add("ES_CONTRIBUYENTE", typeof(int));
            dt.Columns.Add("NOMBRE", typeof(string));
            dt.Rows.Add(1, "Crédito Fiscal");
            dt.Rows.Add(0, "Factura");
            cbxTIPO_DOCUMENTO.DataSource = dt;
            cbxTIPO_DOCUMENTO.ValueMember = "ES_CONTRIBUYENTE";
            cbxTIPO_DOCUMENTO.DisplayMember = "NOMBRE";
        }

        private void cbxZAFRA_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? idZafra = ValorCombo(cbxZAFRA);
            if (idZafra.HasValue)
                CargarCatorcena(idZafra.Value);
        }

        private static int? ValorCombo(System.Windows.Forms.ComboBox cbx)
        {
            object v = cbx.SelectedValue;
            if (v == null || v == DBNull.Value || v is DataRowView) return null;
            return int.TryParse(v.ToString(), out int id) ? id : (int?)null;
        }

        #endregion

        #region === GRID ===

        private void ConfigurarGrid()
        {
            var gv = gvInconsistencias;
            gv.Columns.Clear();
            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsView.ShowFooter = true;
            gv.OptionsView.ColumnAutoWidth = true;
            gv.OptionsView.RowAutoHeight = true;
            gv.OptionsBehavior.Editable = false;
            gv.OptionsFind.AlwaysVisible = true;
            gv.OptionsFind.FindNullPrompt = "Introduzca el texto a buscar...";
            gv.OptionsSelection.EnableAppearanceFocusedCell = false;
            gv.OptionsSelection.MultiSelect = true;   // para Corregir solo las filas seleccionadas
            gv.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gv.Appearance.FooterPanel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gv.OptionsPrint.PrintFooter = true;
            gv.OptionsPrint.AutoWidth = true;
            gv.OptionsPrint.EnableAppearanceEvenRow = false;

            Columna("GRAVEDAD", "Gravedad", 70);
            Columna("CODIGO", "Código", 90);
            Columna("NOMBRE", "Proveedor / Cliente", 240);
            Columna("CATEGORIA", "Tipo", 80);
            Columna("INCONSISTENCIA", "Inconsistencia", 520, multilinea: true);
            Columna("SOLUCION", "Solución", 300, multilinea: true);
            Columna("ID_COMPROB_ENCA", "Id Comprob.", 70);
            Columna("ID_ENTIDAD", "Id Cliente", 70);
            AgregarColumnaCorregir();

            gv.Columns["NOMBRE"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "NOMBRE", "{0} inconsistencia(s)");
            gv.RowStyle += Gv_RowStyle;
        }

        // (2026-10-05) Botón "Corregir" en cada registro corregible (columna Solución = "Corregible: ...").
        // Se dibuja como botón (fondo azul, borde redondeado, ícono y texto blancos) y se
        // activa con un clic sobre él.
        private Image _iconoCorregir;
        private int _filaHot = DevExpress.XtraGrid.GridControl.InvalidRowHandle;

        private void AgregarColumnaCorregir()
        {
            var col = gvInconsistencias.Columns.AddField("COL_CORREGIR");
            col.UnboundType = DevExpress.Data.UnboundColumnType.String;
            col.Caption = "Acción";
            col.Width = 115;
            col.MinWidth = 110;
            col.Visible = true;
            col.VisibleIndex = gvInconsistencias.VisibleColumns.Count;
            col.OptionsColumn.AllowEdit = false;
            col.OptionsColumn.AllowFocus = false;
            col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            col.OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;

            // Ícono CorregirRegistro32x32 con sus colores originales
            _iconoCorregir = IconoPequenio(Properties.Resources.CorregirRegistro32x32, 18);

            gvInconsistencias.CustomDrawCell += Gv_CustomDrawCellCorregir;
            gvInconsistencias.RowCellClick += Gv_RowCellClickCorregir;
            gvInconsistencias.MouseMove += (s, e) =>
            {
                var hit = gvInconsistencias.CalcHitInfo(e.Location);
                int fila = hit.InRowCell && hit.Column?.FieldName == "COL_CORREGIR" && EsCorregible(hit.RowHandle)
                    ? hit.RowHandle : DevExpress.XtraGrid.GridControl.InvalidRowHandle;
                gridControl1.Cursor = fila >= 0 ? Cursors.Hand : Cursors.Default;
                if (fila != _filaHot) { _filaHot = fila; gridControl1.Invalidate(); }
            };
            gvInconsistencias.MouseLeave += (s, e) =>
            {
                if (_filaHot >= 0) { _filaHot = DevExpress.XtraGrid.GridControl.InvalidRowHandle; gridControl1.Invalidate(); }
                gridControl1.Cursor = Cursors.Default;
            };
        }

        private static Rectangle RectBoton(Rectangle celda)
        {
            int alto = Math.Min(24, celda.Height - 6);
            int ancho = Math.Min(98, celda.Width - 8);
            return new Rectangle(celda.X + (celda.Width - ancho) / 2, celda.Y + (celda.Height - alto) / 2, ancho, alto);
        }

        private void Gv_CustomDrawCellCorregir(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName != "COL_CORREGIR") return;
            e.Appearance.FillRectangle(e.Cache, e.Bounds);   // fondo normal de la fila
            if (EsCorregible(e.RowHandle))
            {
                Rectangle r = RectBoton(e.Bounds);
                Color fondo = e.RowHandle == _filaHot ? Color.FromArgb(207, 228, 250) : Color.FromArgb(232, 242, 253);
                Graphics g = e.Cache.Graphics;
                var modo = g.SmoothingMode;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = Redondeado(r, 5))
                using (var brush = new SolidBrush(fondo))
                using (var pen = new Pen(Color.FromArgb(30, 136, 229)))
                {
                    g.FillPath(brush, path);
                    g.DrawPath(pen, path);
                }
                g.SmoothingMode = modo;

                int x = r.X + 8;
                if (_iconoCorregir != null)
                {
                    g.DrawImage(_iconoCorregir, x, r.Y + (r.Height - 18) / 2, 18, 18);
                    x += 22;
                }
                using (var font = new Font("Segoe UI", 8.5F, FontStyle.Bold))
                    TextRenderer.DrawText(g, "Corregir", font,
                        new Rectangle(x, r.Y, r.Right - x - 4, r.Height), Color.FromArgb(13, 71, 161),
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.NoPadding);
            }
            e.Handled = true;
        }

        private async void Gv_RowCellClickCorregir(object sender, RowCellClickEventArgs e)
        {
            if (e.Column?.FieldName != "COL_CORREGIR" || !EsCorregible(e.RowHandle)) return;
            var info = gvInconsistencias.GetViewInfo() as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo;
            var celda = info?.GetGridCellInfo(e.RowHandle, e.Column);
            if (celda != null && !RectBoton(celda.Bounds).Contains(e.Location)) return;

            DataRow fila = gvInconsistencias.GetDataRow(e.RowHandle);
            if (fila != null)
                await CorregirAsync(new System.Collections.Generic.List<DataRow> { fila }, soloSeleccion: true);
        }

        private static System.Drawing.Drawing2D.GraphicsPath Redondeado(Rectangle r, int radio)
        {
            int d = radio * 2;
            var p = new System.Drawing.Drawing2D.GraphicsPath();
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        // Pinta el ícono en blanco conservando la transparencia.
        private static Image IconoBlanco(Image original)
        {
            if (original == null) return null;
            var bmp = new Bitmap(original.Width, original.Height);
            var matriz = new System.Drawing.Imaging.ColorMatrix(new[]
            {
                new float[] { 0, 0, 0, 0, 0 },
                new float[] { 0, 0, 0, 0, 0 },
                new float[] { 0, 0, 0, 0, 0 },
                new float[] { 0, 0, 0, 1, 0 },
                new float[] { 1, 1, 1, 0, 1 }
            });
            using (var attr = new System.Drawing.Imaging.ImageAttributes())
            using (Graphics g = Graphics.FromImage(bmp))
            {
                attr.SetColorMatrix(matriz);
                g.DrawImage(original, new Rectangle(0, 0, bmp.Width, bmp.Height),
                    0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attr);
            }
            return bmp;
        }

        private static Image IconoPequenio(Image original, int px)
        {
            if (original == null) return null;
            var bmp = new Bitmap(px, px);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(original, 0, 0, px, px);
            }
            return bmp;
        }

        private bool EsCorregible(int rowHandle)
        {
            if (rowHandle < 0) return false;
            string tipo = Convert.ToString(gvInconsistencias.GetRowCellValue(rowHandle, "CORRECCION"));
            object id = gvInconsistencias.GetRowCellValue(rowHandle, "ID_ENTIDAD");
            return (tipo == "CXC" || tipo == "COD") && id != null && id != DBNull.Value;
        }

        private void Columna(string campo, string caption, int ancho, bool multilinea = false)
        {
            var col = new GridColumn { FieldName = campo, Caption = caption, Width = ancho, Visible = true };
            gvInconsistencias.Columns.Add(col);
            col.VisibleIndex = gvInconsistencias.VisibleColumns.Count;
            col.OptionsColumn.AllowEdit = false;
            if (multilinea)
            {
                var memo = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit { WordWrap = true };
                gridControl1.RepositoryItems.Add(memo);
                col.ColumnEdit = memo;
            }
        }

        private void Gv_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            string gravedad = Convert.ToString(gvInconsistencias.GetRowCellValue(e.RowHandle, "GRAVEDAD"));
            if (gravedad == "ERROR")
                e.Appearance.BackColor = Color.FromArgb(252, 228, 214);
            else if (gravedad == "AVISO")
                e.Appearance.BackColor = Color.FromArgb(255, 242, 204);
        }

        private void AplicarFiltro()
        {
            gvInconsistencias.ActiveFilterString = chkSoloErrores.Checked ? "[GRAVEDAD] = 'ERROR'" : string.Empty;
        }

        #endregion

        #region === BOTONES ===

        private async void btnConsultar_Click(object sender, EventArgs e)
        {
            int? zafra = ValorCombo(cbxZAFRA);
            int? catorcena = ValorCombo(cbxCATORCENA);
            int? planilla = ValorCombo(cbxTIPO_PLANILLA);
            int? esContribuyente = ValorCombo(cbxTIPO_DOCUMENTO);

            if (zafra == null || catorcena == null || planilla == null || esContribuyente == null)
            {
                XtraMessageBox.Show("Debe seleccionar Zafra, Quincena, Tipo Planilla y Documento.",
                    "Inconsistencias", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                MostrarProgreso(true);
                // La consulta corre en segundo plano para que la pantalla no se congele.
                DataTable dt = await System.Threading.Tasks.Task.Run(() =>
                    _dal.EjecutarConsulta(SP_INCONSISTENCIAS, new
                    {
                        ID_ZAFRA = zafra,
                        ID_CATORCENA = catorcena,
                        ID_TIPO_PLANILLA = planilla,
                        ES_CONTRIBUYENTE = esContribuyente
                    }));
                if (IsDisposed) return;

                _dtResultado = dt ?? new DataTable();
                _revisionTransportistas = planilla == 2;
                gridControl1.DataSource = _dtResultado;
                AplicarFiltro();
                MostrarProgreso(false);
                ActualizarResumen();
            }
            catch (Exception ex)
            {
                if (IsDisposed) return;
                MostrarProgreso(false);
                lblResumen.Text = "No se pudo revisar la planilla.";
                XtraMessageBox.Show($"No se pudo revisar la planilla:\n\n{ex.Message}",
                    "Inconsistencias", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarResumen()
        {
            int errores = 0, avisos = 0, clientes = 0;
            if (_dtResultado != null && _dtResultado.Columns.Contains("GRAVEDAD"))
            {
                errores = _dtResultado.AsEnumerable().Count(r => Convert.ToString(r["GRAVEDAD"]) == "ERROR");
                avisos = _dtResultado.AsEnumerable().Count(r => Convert.ToString(r["GRAVEDAD"]) == "AVISO");
                clientes = _dtResultado.AsEnumerable()
                    .Where(r => Convert.ToString(r["GRAVEDAD"]) == "ERROR" && r["CODIGO"] != DBNull.Value)
                    .Select(r => Convert.ToString(r["CODIGO"])).Distinct().Count();
            }

            string filtros = $"{cbxTIPO_DOCUMENTO.Text}  ·  {cbxZAFRA.Text}  ·  Quincena {cbxCATORCENA.Text}  ·  {cbxTIPO_PLANILLA.Text}";
            _tituloReporte = $"Inconsistencias de planilla — {filtros}";

            if (errores == 0 && avisos == 0)
            {
                lblResumen.BackColor = Color.FromArgb(226, 239, 218);
                lblResumen.ForeColor = Color.FromArgb(55, 86, 35);
                lblResumen.Text = $"Sin inconsistencias: se puede generar.   |   {filtros}";
            }
            else
            {
                bool hayErrores = errores > 0;
                lblResumen.BackColor = hayErrores ? Color.FromArgb(252, 228, 214) : Color.FromArgb(255, 242, 204);
                lblResumen.ForeColor = hayErrores ? Color.FromArgb(156, 0, 6) : Color.FromArgb(156, 101, 0);
                lblResumen.Text =
                    $"Errores: {errores}" + (clientes > 0 ? $" ({clientes} proveedor(es))" : string.Empty) +
                    $"   |   Avisos: {avisos}   |   {filtros}" +
                    (hayErrores ? "   —   Corrija los errores antes de generar." : string.Empty);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (_dtResultado == null)
            {
                XtraMessageBox.Show("Primero presione Revisar.", "Imprimir",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var ps = new PrintingSystem();
                var link = new PrintableComponentLink(ps)
                {
                    Component = gridControl1,
                    Landscape = true,
                    PaperKind = System.Drawing.Printing.PaperKind.Letter,
                    Margins = new System.Drawing.Printing.Margins(40, 40, 50, 50)
                };

                string titulo = string.IsNullOrEmpty(_tituloReporte) ? "Inconsistencias de planilla" : _tituloReporte;
                string resumen = lblResumen.Text;
                link.CreateReportHeaderArea += (s, a) =>
                {
                    float ancho = a.Graph.ClientPageSize.Width;
                    a.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near);
                    a.Graph.BackColor = Color.Transparent;
                    a.Graph.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                    a.Graph.DrawString(titulo, Color.Black, new RectangleF(0, 0, ancho, 24), BorderSide.None);
                    a.Graph.Font = new Font("Segoe UI", 9F);
                    a.Graph.DrawString(resumen, Color.Black, new RectangleF(0, 26, ancho, 20), BorderSide.None);
                    a.Graph.DrawString(
                        $"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}   Usuario: {Configuracion.UsuarioActual}",
                        Color.DimGray, new RectangleF(0, 46, ancho, 18), BorderSide.None);
                };
                link.PageHeaderFooter = new PageHeaderFooter(
                    new PageHeaderArea(),
                    new PageFooterArea(new[] { "", "Página [Page # of Pages #]", "" },
                        new Font("Segoe UI", 8F), BrickAlignment.Center));

                link.CreateDocument();
                link.ShowPreviewDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"No se pudo imprimir:\n\n{ex.Message}", "Imprimir",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // (2026-10-05) Corregir: por ahora la cuenta por cobrar faltante ('CXC'), tomándola de
        // ERPMH.EFACTURACION.clientes y guardándola en dbo.ENTIDAD_CLIENTE
        // ([ECOMPROB].[SP_CORREGIR_INCONSISTENCIA]). Si hay filas seleccionadas corrige
        // esas; si no, todas las corregibles de la revisión.
        private const string SP_CORREGIR_LOTE = "[ECOMPROB].[SP_CORREGIR_INCONSISTENCIA_LOTE]";

        private async void btnCorregir_Click(object sender, EventArgs e)
        {
            if (_dtResultado == null || !_dtResultado.Columns.Contains("CORRECCION"))
            {
                XtraMessageBox.Show("Primero presione Revisar.", "Corregir",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var filas = gvInconsistencias.GetSelectedRows()
                .Where(h => h >= 0)
                .Select(h => gvInconsistencias.GetDataRow(h))
                .Where(r => r != null)
                .ToList();
            bool soloSeleccion = filas.Count > 1;
            if (!soloSeleccion)
                filas = _dtResultado.AsEnumerable().ToList();

            await CorregirAsync(filas, soloSeleccion);
        }

        // Corrige las filas indicadas en una sola llamada (botón por registro o lote).
        private async System.Threading.Tasks.Task CorregirAsync(System.Collections.Generic.List<DataRow> filas, bool soloSeleccion)
        {
            var corregibles = filas
                .Where(r => r["ID_ENTIDAD"] != DBNull.Value &&
                            (Convert.ToString(r["CORRECCION"]) == "CXC" || Convert.ToString(r["CORRECCION"]) == "COD"))
                .GroupBy(r => new { Tipo = Convert.ToString(r["CORRECCION"]), Id = Convert.ToInt32(r["ID_ENTIDAD"]) })
                .Select(g => new
                {
                    Tipo = g.Key.Tipo,
                    IdEntidad = g.Key.Id,
                    Codigo = Convert.ToString(g.First()["CODIGO"]),
                    Cliente = $"{g.First()["CODIGO"]} - {g.First()["NOMBRE"]}"
                })
                .ToList();

            if (corregibles.Count == 0)
            {
                XtraMessageBox.Show(
                    "No hay inconsistencias que se puedan corregir desde aquí" +
                    (soloSeleccion ? " en las filas seleccionadas." : ".") +
                    "\n\nSe corrigen: la cuenta por cobrar faltante (desde ERPMH) y el código " +
                    "CODIPROVEEDOR / CODTRANSPORT vacío en ENTIDAD (desde la planilla).",
                    "Corregir", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string campoCodigo = _revisionTransportistas ? "CODTRANSPORT" : "CODIPROVEEDOR";
            int nCxc = corregibles.Count(x => x.Tipo == "CXC");
            int nCod = corregibles.Count(x => x.Tipo == "COD");
            string lista = string.Join("\n", corregibles.Take(15).Select(x =>
                "• " + x.Cliente + (x.Tipo == "CXC" ? "  (cuenta por cobrar)" : $"  ({campoCodigo})")));
            if (corregibles.Count > 15) lista += $"\n• ... y {corregibles.Count - 15} más";
            string que = (nCxc > 0 ? $"\n  - Cuenta por cobrar desde ERPMH: {nCxc}" : "") +
                         (nCod > 0 ? $"\n  - Asignar {campoCodigo} desde la planilla: {nCod}" : "");
            if (XtraMessageBox.Show(
                    $"Se corregirán {corregibles.Count} inconsistencia(s):{que}\n\n{lista}\n\n¿Continuar?",
                    "Corregir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // (2026-10-05) Todo en UNA sola llamada y una sola transacción (no fila por fila):
            // [ECOMPROB].[SP_CORREGIR_INCONSISTENCIA_LOTE] recibe la lista 'TIPO|ID_ENTIDAD|CODIGO;...'.
            string listaSp = string.Join(";", corregibles.Select(x =>
                $"{x.Tipo}|{x.IdEntidad}|{(x.Codigo ?? string.Empty).Replace("|", "").Replace(";", "")}"));
            bool esTrans = _revisionTransportistas;
            string usuario = Configuracion.UsuarioActual;
            DataTable resultado = null;
            Exception error = null;
            try
            {
                MostrarProgreso(true);
                _lblProgreso.Text = "Corrigiendo inconsistencias...";
                resultado = await System.Threading.Tasks.Task.Run(() =>
                    _dal.EjecutarConsulta(SP_CORREGIR_LOTE, new
                    {
                        LISTA = listaSp,
                        ES_TRANS = esTrans,
                        USUARIO = usuario
                    }));
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                if (!IsDisposed) MostrarProgreso(false);
            }
            if (IsDisposed) return;

            if (error != null)
            {
                XtraMessageBox.Show($"No se pudo corregir:\n\n{error.Message}", "Corregir",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var filasRes = resultado?.AsEnumerable().ToList() ?? new System.Collections.Generic.List<DataRow>();
            int ok = filasRes.Count(r => Convert.ToString(r["RESULTADO"]) == "OK");
            var errores = new System.Text.StringBuilder();
            foreach (var r in filasRes.Where(r => Convert.ToString(r["RESULTADO"]) != "OK").Take(25))
                errores.AppendLine($"• {r["CLIENTE"]}: {r["MENSAJE"]}");

            string resumen = $"Corregidas: {ok}\nCon error: {filasRes.Count - ok}";
            if (errores.Length > 0) resumen += "\n\n" + errores;
            XtraMessageBox.Show(resumen, "Corregir", MessageBoxButtons.OK,
                errores.Length == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            // Volver a revisar para ver cómo quedó
            btnConsultar_Click(btnConsultar, EventArgs.Empty);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
