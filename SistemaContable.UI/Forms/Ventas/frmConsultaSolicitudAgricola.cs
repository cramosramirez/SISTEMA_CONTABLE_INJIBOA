using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SistemaContable.DAL;
using SistemaContable.UI.Forms.Inventario;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Ventas
{
    public partial class frmConsultaSolicitudAgricola : Form
    {
        private readonly DALBase _dal = new DALBase();
        private readonly string _codiProveedor;
        private readonly int _idZafra;
        private DataTable _solicitudes;
        private DataTable _detalle;
        private bool _datosCargados;
        private bool _abriendoProducto;
        private RepositoryItemButtonEdit _editorRegistrarProducto;
        private RepositoryItemTextEdit _editorSinAccion;

        private const string ColumnaRegistrarProducto = "REGISTRAR_PRODUCTO";

        public DataRow SolicitudSeleccionada { get; private set; }
        public DataTable DetalleSeleccionado { get; private set; }

        public frmConsultaSolicitudAgricola(string codiProveedor, int idZafra)
        {
            InitializeComponent();
            _codiProveedor = (codiProveedor ?? string.Empty).Trim();
            _idZafra = idZafra;
            ActualizarEncabezadoZafra();
            ConfigurarVistas();
        }

        private void ActualizarEncabezadoZafra()
        {
            string nombreZafra = null;
            if (_solicitudes != null &&
                _solicitudes.Rows.Count > 0 &&
                _solicitudes.Columns.Contains("NOMBRE_ZAFRA"))
            {
                nombreZafra = Convert.ToString(
                    _solicitudes.Rows[0]["NOMBRE_ZAFRA"]).Trim();
            }

            string zafraMostrada;
            if (string.IsNullOrWhiteSpace(nombreZafra))
            {
                zafraMostrada = $"Zafra {_idZafra}";
            }
            else
            {
                zafraMostrada = nombreZafra.StartsWith(
                    "Zafra",
                    StringComparison.OrdinalIgnoreCase)
                    ? nombreZafra
                    : $"Zafra {nombreZafra}";
            }

            lblSolicitudes.Text =
                $"Solicitudes del proveedor {_codiProveedor} - {zafraMostrada}";
        }

        private void ConfigurarVistas()
        {
            ConfigurarVista(gridViewSolicitudes);
            ConfigurarVista(gridViewDetalle);
            ConfigurarBuscadorSolicitudes();
            ConfigurarEditorRegistrarProducto();
            gridViewDetalle.OptionsBehavior.Editable = true;
            gridViewDetalle.OptionsBehavior.ReadOnly = false;
            gridViewDetalle.RowHeight = 34;
            gridViewSolicitudes.RowStyle += gridViewSolicitudes_RowStyle;
            gridViewDetalle.RowStyle += gridViewDetalle_RowStyle;
            gridViewDetalle.CustomColumnDisplayText += gridViewDetalle_CustomColumnDisplayText;
            gridViewDetalle.CustomRowCellEdit += gridViewDetalle_CustomRowCellEdit;
        }

        private void ConfigurarBuscadorSolicitudes()
        {
            gridViewSolicitudes.OptionsFind.AlwaysVisible = true;
            gridViewSolicitudes.OptionsFind.FindMode = FindMode.Always;
            gridViewSolicitudes.OptionsFind.FindDelay = 250;
            gridViewSolicitudes.OptionsFind.FindFilterColumns = "NUM_SOLICITUD";
            gridViewSolicitudes.OptionsFind.FindNullPrompt =
                "Buscar por N.º solicitud...";
            gridViewSolicitudes.OptionsFind.ShowFindButton = false;
            gridViewSolicitudes.OptionsFind.ShowClearButton = true;
        }

        private void ConfigurarEditorRegistrarProducto()
        {
            _editorRegistrarProducto = new RepositoryItemButtonEdit
            {
                TextEditStyle = TextEditStyles.HideTextEditor
            };
            _editorRegistrarProducto.Buttons[0].Kind = ButtonPredefines.Glyph;
            _editorRegistrarProducto.Buttons[0].ImageOptions.Image =
                global::SistemaContable.UI.Properties.Resources.AgregarProducto32x32;
            _editorRegistrarProducto.Buttons[0].ImageOptions.ImageToTextAlignment =
                ImageAlignToText.LeftCenter;
            _editorRegistrarProducto.Buttons[0].ImageOptions.ImageToTextIndent = 4;
            _editorRegistrarProducto.Buttons[0].Caption = "Registrar";
            _editorRegistrarProducto.Buttons[0].ToolTip =
                "Registrar y relacionar este producto";
            _editorRegistrarProducto.ButtonClick +=
                editorRegistrarProducto_ButtonClick;

            _editorSinAccion = new RepositoryItemTextEdit
            {
                ReadOnly = true
            };

            gridDetalle.RepositoryItems.Add(_editorRegistrarProducto);
            gridDetalle.RepositoryItems.Add(_editorSinAccion);
        }

        private static void ConfigurarVista(GridView view)
        {
            view.OptionsBehavior.Editable = false;
            view.OptionsBehavior.ReadOnly = true;
            view.OptionsSelection.MultiSelect = false;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ShowAutoFilterRow = false;
            view.OptionsView.ColumnAutoWidth = false;
            view.OptionsFind.AlwaysVisible = false;
            view.Appearance.FocusedRow.BackColor = Color.FromArgb(204, 229, 255);
            view.Appearance.FocusedRow.Options.UseBackColor = true;
            view.Appearance.HideSelectionRow.BackColor = Color.FromArgb(204, 229, 255);
            view.Appearance.HideSelectionRow.Options.UseBackColor = true;
            view.Appearance.Row.Font = new Font("Segoe UI", 9f);
            view.Appearance.Row.Options.UseFont = true;
            view.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            view.Appearance.HeaderPanel.Options.UseFont = true;
        }

        private async void frmConsultaSolicitudAgricola_Shown(object sender, EventArgs e)
        {
            AjustarTamanoInicial();
            if (_datosCargados) return;
            _datosCargados = true;
            await CargarDatosAsync();
        }

        private void AjustarTamanoInicial()
        {
            Control referencia = Owner ?? this;
            Rectangle areaTrabajo = Screen.FromControl(referencia).WorkingArea;
            int ancho = Math.Min(1450, areaTrabajo.Width - 40);
            int alto = Math.Min(760, areaTrabajo.Height - 50);

            ancho = Math.Max(1000, ancho);
            alto = Math.Max(600, alto);

            Size = new Size(ancho, alto);
            Location = new Point(
                areaTrabajo.Left + Math.Max(0, (areaTrabajo.Width - Width) / 2),
                areaTrabajo.Top + Math.Max(0, (areaTrabajo.Height - Height) / 2));
        }

        private async Task CargarDatosAsync(int? idSolicitudRestaurar = null)
        {
            btnSeleccionar.Enabled = false;
            lblEstado.Text = "Cargando solicitudes...";
            UseWaitCursor = true;

            try
            {
                var resultado = await Task.Run(() => new
                {
                    Solicitudes = _dal.EjecutarConsulta(
                        "[ESOLICITUD].[SP_SOLICITUDES_SIGESTA]",
                        new
                        {
                            ACTION = "PRODUCTOR_ENCABEZADO",
                            CODIPROVEEDOR = _codiProveedor,
                            ID_ZAFRA = _idZafra
                        }),
                    Detalle = _dal.EjecutarConsulta(
                        "[ESOLICITUD].[SP_SOLICITUDES_SIGESTA]",
                        new
                        {
                            ACTION = "PRODUCTOR_DETALLE",
                            CODIPROVEEDOR = _codiProveedor,
                            ID_ZAFRA = _idZafra
                        })
                });

                if (IsDisposed) return;

                _solicitudes = resultado.Solicitudes ?? new DataTable();
                _detalle = resultado.Detalle ?? new DataTable();
                ActualizarEncabezadoZafra();
                PrepararIndicadoresRelacion();

                gridSolicitudes.DataSource = _solicitudes;
                gridSolicitudes.ForceInitialize();
                gridViewSolicitudes.PopulateColumns();
                ConfigurarColumnasSolicitudes();
                RestaurarSolicitudActiva(idSolicitudRestaurar);
                MostrarDetalleSolicitudActiva();

                if (_solicitudes.Rows.Count == 0)
                {
                    btnSeleccionar.Enabled = false;
                    lblEstado.Text = "No se encontraron solicitudes para el cliente y la zafra seleccionados.";
                }
            }
            catch (Exception ex)
            {
                lblEstado.Text = "No fue posible cargar las solicitudes.";
                XtraMessageBox.Show(
                    "No fue posible consultar las solicitudes agrícolas:\n\n" + ex.Message,
                    "Solicitud agrícola",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
                    UseWaitCursor = false;
            }
        }

        private void ConfigurarColumnasSolicitudes()
        {
            OcultarTodasLasColumnas(gridViewSolicitudes);
            MostrarColumna(gridViewSolicitudes, "TIPO_SOLICITUD", "Tipo", 100, 0);
            MostrarColumna(gridViewSolicitudes, "NUM_SOLICITUD", "N.º solicitud", 110, 1);
            MostrarColumna(gridViewSolicitudes, "NOMBRE_PROVEEDOR", "Proveedor", 260, 2);
            MostrarColumna(gridViewSolicitudes, "NOMBRE_CUENTA", "Cuenta", 360, 3);
            MostrarColumna(gridViewSolicitudes, "SUB_TOTAL", "Subtotal", 100, 4, "n2");
            MostrarColumna(gridViewSolicitudes, "IVA", "IVA", 90, 5, "n2");
            MostrarColumna(gridViewSolicitudes, "TOTAL", "Total", 100, 6, "n2");
            MostrarColumna(gridViewSolicitudes, "PRODUCTOS_SIN_RELACIONAR", "Sin relacionar", 110, 7);
            OrdenarColumnas(gridViewSolicitudes,
                "TIPO_SOLICITUD",
                "NUM_SOLICITUD",
                "NOMBRE_PROVEEDOR",
                "NOMBRE_CUENTA",
                "SUB_TOTAL",
                "IVA",
                "TOTAL",
                "PRODUCTOS_SIN_RELACIONAR");
        }

        private void ConfigurarColumnasDetalle()
        {
            OcultarTodasLasColumnas(gridViewDetalle);
            MostrarColumna(gridViewDetalle, "COD_REF", "Código local", 150, 0);
            MostrarColumna(gridViewDetalle, "ID_PRODUCTO", "Código SIGESTA", 110, 1);
            MostrarColumna(gridViewDetalle, "NOMBRE_PRODUCTO", "Producto", 480, 2);
            MostrarColumna(gridViewDetalle, "CANTIDAD", "Cantidad", 120, 3, "n4");
            MostrarColumna(gridViewDetalle, "PRECIO_UNITARIO", "Precio unitario", 140, 4, "n4");
            MostrarColumna(gridViewDetalle, "TOTAL", "Total", 120, 5, "n4");
            ConfigurarColumnaRegistrarProducto();
            OrdenarColumnas(gridViewDetalle,
                "COD_REF",
                "ID_PRODUCTO",
                "NOMBRE_PRODUCTO",
                "CANTIDAD",
                "PRECIO_UNITARIO",
                "TOTAL",
                ColumnaRegistrarProducto);
        }

        private void ConfigurarColumnaRegistrarProducto()
        {
            GridColumn column = gridViewDetalle.Columns.ColumnByFieldName(
                ColumnaRegistrarProducto);
            if (column == null)
                column = gridViewDetalle.Columns.AddField(ColumnaRegistrarProducto);

            column.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            column.Caption = "Acción";
            column.Width = 130;
            column.Visible = true;
            column.VisibleIndex = 6;
            column.OptionsColumn.AllowEdit = true;
            column.OptionsColumn.ReadOnly = false;
            column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            column.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            column.ColumnEdit = _editorRegistrarProducto;
        }

        private void PrepararIndicadoresRelacion()
        {
            const string columnaPendientes = "PRODUCTOS_SIN_RELACIONAR";
            if (!_solicitudes.Columns.Contains(columnaPendientes))
                _solicitudes.Columns.Add(columnaPendientes, typeof(int));

            foreach (DataRow solicitud in _solicitudes.Rows)
            {
                int idSolicitud = Convert.ToInt32(solicitud["ID_SOLICITUD"]);
                solicitud[columnaPendientes] = ContarProductosSinRelacionar(idSolicitud);
            }
        }

        private static void OcultarTodasLasColumnas(GridView view)
        {
            foreach (GridColumn column in view.Columns)
            {
                column.Visible = false;
                column.OptionsColumn.AllowEdit = false;
                column.OptionsColumn.ReadOnly = true;
            }
        }

        private static void MostrarColumna(
            GridView view,
            string fieldName,
            string caption,
            int width,
            int visibleIndex,
            string formato = null)
        {
            GridColumn column = view.Columns.ColumnByFieldName(fieldName);
            if (column == null) return;

            column.Caption = caption;
            column.Width = width;
            column.Visible = true;
            column.VisibleIndex = visibleIndex;

            if (!string.IsNullOrWhiteSpace(formato))
            {
                column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                column.DisplayFormat.FormatString = formato;
            }
        }

        private static void OrdenarColumnas(GridView view, params string[] fields)
        {
            for (int index = 0; index < fields.Length; index++)
            {
                GridColumn column = view.Columns.ColumnByFieldName(fields[index]);
                if (column != null && column.Visible)
                    column.VisibleIndex = index;
            }
        }

        private void gridViewSolicitudes_FocusedRowChanged(
            object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            MostrarDetalleSolicitudActiva();
        }

        private void gridViewDetalle_CustomColumnDisplayText(
            object sender,
            DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "COD_REF" && !TieneCodigoRelacionado(e.Value))
                e.DisplayText = "SIN RELACIONAR";
        }

        private void gridViewDetalle_CustomRowCellEdit(
            object sender,
            CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle < 0 || e.Column.FieldName != ColumnaRegistrarProducto)
                return;

            object codigoLocal = gridViewDetalle.GetRowCellValue(
                e.RowHandle,
                "COD_REF");
            e.RepositoryItem = TieneCodigoRelacionado(codigoLocal)
                ? (RepositoryItem)_editorSinAccion
                : _editorRegistrarProducto;
        }

        private void gridViewDetalle_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            GridView view = sender as GridView;
            if (view == null || TieneCodigoRelacionado(
                    view.GetRowCellValue(e.RowHandle, "COD_REF")))
                return;

            e.Appearance.BackColor = Color.MistyRose;
            e.Appearance.ForeColor = Color.Firebrick;
            e.Appearance.Options.UseBackColor = true;
            e.Appearance.Options.UseForeColor = true;
        }

        private void gridViewSolicitudes_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            GridView view = sender as GridView;
            if (view == null) return;

            object value = view.GetRowCellValue(e.RowHandle, "PRODUCTOS_SIN_RELACIONAR");
            int pendientes = value == null || value == DBNull.Value
                ? 0
                : Convert.ToInt32(value);
            if (pendientes <= 0) return;

            e.Appearance.BackColor = Color.LightGoldenrodYellow;
            e.Appearance.ForeColor = Color.DarkOrange;
            e.Appearance.Options.UseBackColor = true;
            e.Appearance.Options.UseForeColor = true;
        }

        private void MostrarDetalleSolicitudActiva()
        {
            if (_detalle == null)
            {
                gridDetalle.DataSource = null;
                ActualizarEstadoSeleccion();
                return;
            }

            int? idSolicitud = ObtenerIdSolicitudActiva();
            DataView detalleFiltrado = new DataView(_detalle)
            {
                RowFilter = idSolicitud.HasValue && _detalle.Columns.Contains("ID_SOLICITUD")
                    ? $"ID_SOLICITUD = {idSolicitud.Value}"
                    : "1 = 0"
            };

            gridDetalle.DataSource = detalleFiltrado;
            gridDetalle.ForceInitialize();
            gridViewDetalle.PopulateColumns();
            ConfigurarColumnasDetalle();
            ActualizarEstadoSeleccion();
        }

        private void ActualizarEstadoSeleccion()
        {
            int? idSolicitud = ObtenerIdSolicitudActiva();
            if (!idSolicitud.HasValue)
            {
                btnSeleccionar.Enabled = false;
                lblEstado.Text = gridViewSolicitudes.DataRowCount == 0
                    ? "No se encontró una solicitud con el número indicado."
                    : "Seleccione una solicitud.";
                return;
            }

            int pendientes = ContarProductosSinRelacionar(idSolicitud.Value);
            btnSeleccionar.Enabled = pendientes == 0;
            lblEstado.Text = pendientes > 0
                ? $"La solicitud tiene {pendientes} producto(s) SIN RELACIONAR. Use el botón Registrar de la fila correspondiente."
                : $"{_solicitudes.Rows.Count} solicitud(es) encontrada(s). La solicitud seleccionada está lista para usar.";
        }

        private int ContarProductosSinRelacionar(int idSolicitud)
        {
            if (_detalle == null || !_detalle.Columns.Contains("ID_SOLICITUD"))
                return 0;

            int pendientes = 0;
            foreach (DataRow row in _detalle.Rows)
            {
                if (row["ID_SOLICITUD"] == DBNull.Value ||
                    Convert.ToInt32(row["ID_SOLICITUD"]) != idSolicitud)
                    continue;

                if (!_detalle.Columns.Contains("COD_REF") ||
                    !TieneCodigoRelacionado(row["COD_REF"]))
                    pendientes++;
            }
            return pendientes;
        }

        private static bool TieneCodigoRelacionado(object value)
        {
            return value != null && value != DBNull.Value &&
                   !string.IsNullOrWhiteSpace(Convert.ToString(value));
        }

        private int? ObtenerIdSolicitudActiva()
        {
            if (gridViewSolicitudes.FocusedRowHandle < 0) return null;
            object value = gridViewSolicitudes.GetRowCellValue(
                gridViewSolicitudes.FocusedRowHandle,
                "ID_SOLICITUD");
            return value == null || value == DBNull.Value
                ? (int?)null
                : Convert.ToInt32(value);
        }

        private void RestaurarSolicitudActiva(int? idSolicitud)
        {
            if (!idSolicitud.HasValue) return;

            for (int rowHandle = 0; rowHandle < gridViewSolicitudes.DataRowCount; rowHandle++)
            {
                object value = gridViewSolicitudes.GetRowCellValue(rowHandle, "ID_SOLICITUD");
                if (value == null || value == DBNull.Value ||
                    Convert.ToInt32(value) != idSolicitud.Value)
                    continue;

                gridViewSolicitudes.FocusedRowHandle = rowHandle;
                gridViewSolicitudes.MakeRowVisible(rowHandle);
                return;
            }
        }

        private async void editorRegistrarProducto_ButtonClick(
            object sender,
            ButtonPressedEventArgs e)
        {
            await AbrirConsultaProductoDesdeFilaAsync(gridViewDetalle.FocusedRowHandle);
        }

        private async Task AbrirConsultaProductoDesdeFilaAsync(int rowHandle)
        {
            if (rowHandle < 0 || _abriendoProducto)
                return;

            object codigoLocal = gridViewDetalle.GetRowCellValue(rowHandle, "COD_REF");
            if (TieneCodigoRelacionado(codigoLocal))
                return;

            object idProductoValue = gridViewDetalle.GetRowCellValue(rowHandle, "ID_PRODUCTO");
            int idProductoSigesta;
            if (idProductoValue == null || idProductoValue == DBNull.Value ||
                !int.TryParse(Convert.ToString(idProductoValue), out idProductoSigesta) ||
                idProductoSigesta <= 0)
            {
                XtraMessageBox.Show(
                    "No se pudo identificar el producto SIGESTA seleccionado.",
                    "Producto sin relacionar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            string nombreProducto = Convert.ToString(
                gridViewDetalle.GetRowCellValue(rowHandle, "NOMBRE_PRODUCTO"));

            await ConsultarProductoExistenteYRelacionarAsync(
                idProductoSigesta,
                nombreProducto);
        }

        private async Task ConsultarProductoExistenteYRelacionarAsync(
            int idProductoSigesta,
            string nombreProductoSigesta)
        {
            if (_abriendoProducto)
                return;

            _abriendoProducto = true;
            int? idSolicitud = ObtenerIdSolicitudActiva();

            try
            {
                string codigoLocal = null;
                bool productoRegistrado;
                using (var frm = new frmConsultaProductoExistente(
                    idProductoSigesta,
                    nombreProductoSigesta)
                {
                    StartPosition = FormStartPosition.CenterParent
                })
                {
                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;

                    productoRegistrado = frm.ProductoRegistrado;
                    if (!productoRegistrado)
                    {
                        if (frm.ProductoSeleccionado == null)
                            return;

                        codigoLocal = Convert.ToString(
                            frm.ProductoSeleccionado["COD_REF"]).Trim();
                    }
                }

                if (productoRegistrado)
                {
                    if (!IsDisposed)
                        await CargarDatosAsync(idSolicitud);

                    return;
                }

                if (string.IsNullOrWhiteSpace(codigoLocal))
                {
                    XtraMessageBox.Show(
                        "El producto local seleccionado no tiene código de referencia.",
                        "Relacionar producto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                UseWaitCursor = true;
                DataTable resultado = await Task.Run(() => _dal.EjecutarConsulta(
                    "[EINVENTARIO].[SP_PRODUCTO_SIGESTA]",
                    new
                    {
                        ACCION = "ACTUALIZAR_COD_REF",
                        ID_PRODUCTO = idProductoSigesta,
                        COD_REF = codigoLocal
                    }));

                if (resultado == null || resultado.Rows.Count == 0 ||
                    resultado.Rows[0]["FILAS_AFECTADAS"] == DBNull.Value ||
                    Convert.ToInt32(resultado.Rows[0]["FILAS_AFECTADAS"]) <= 0 ||
                    resultado.Rows[0]["FILAS_RELACIONADAS"] == DBNull.Value ||
                    Convert.ToInt32(resultado.Rows[0]["FILAS_RELACIONADAS"]) <= 0)
                {
                    throw new InvalidOperationException(
                        "No fue posible relacionar el producto local con el producto de SIGESTA.");
                }

                if (!IsDisposed)
                    await CargarDatosAsync(idSolicitud);

                XtraMessageBox.Show(
                    $"El producto SIGESTA quedó relacionado con el código local {codigoLocal}.",
                    "Producto relacionado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "No fue posible relacionar el producto seleccionado:\n\n" + ex.Message,
                    "Relacionar producto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
                    UseWaitCursor = false;

                _abriendoProducto = false;
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            SeleccionarSolicitudActiva();
        }

        private void gridViewSolicitudes_DoubleClick(object sender, EventArgs e)
        {
            SeleccionarSolicitudActiva();
        }

        private void SeleccionarSolicitudActiva()
        {
            int? idSolicitud = ObtenerIdSolicitudActiva();
            if (!idSolicitud.HasValue)
            {
                XtraMessageBox.Show(
                    "Seleccione una solicitud.",
                    "Solicitud agrícola",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            int pendientes = ContarProductosSinRelacionar(idSolicitud.Value);
            if (pendientes > 0)
            {
                XtraMessageBox.Show(
                    $"No se puede seleccionar esta solicitud porque tiene {pendientes} " +
                    "producto(s) SIN RELACIONAR.\n\nRelacione los productos de SIGESTA " +
                    "con sus códigos locales desde el catálogo de productos.",
                    "Productos sin relacionar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            SolicitudSeleccionada = gridViewSolicitudes.GetDataRow(
                gridViewSolicitudes.FocusedRowHandle);
            DetalleSeleccionado = _detalle?.Clone() ?? new DataTable();

            if (_detalle != null && _detalle.Columns.Contains("ID_SOLICITUD"))
            {
                foreach (DataRow row in _detalle.Select($"ID_SOLICITUD = {idSolicitud.Value}"))
                    DetalleSeleccionado.ImportRow(row);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
