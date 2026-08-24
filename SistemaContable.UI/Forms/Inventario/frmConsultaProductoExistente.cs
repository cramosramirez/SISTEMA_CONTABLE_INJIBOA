using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using SistemaContable.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Inventario
{
    public partial class frmConsultaProductoExistente : Form
    {
        private readonly DALBase _dal = new DALBase();
        private readonly int? _idProductoSigesta;
        private readonly string _nombreProductoSigesta;
        private bool _cargando;
        private bool _busquedaPendiente;
        private bool _formularioMostrado;

        public DataRow ProductoSeleccionado { get; private set; }
        public bool ProductoRegistrado { get; private set; }

        public frmConsultaProductoExistente(
            int? idProductoSigesta = null,
            string descripcionProductoSigesta = null)
        {
            InitializeComponent();
            _idProductoSigesta = idProductoSigesta;
            _nombreProductoSigesta =
                (descripcionProductoSigesta ?? string.Empty).Trim();
            txtFiltro.Text = _nombreProductoSigesta;
            btnRegistrarNuevo.Enabled =
                _idProductoSigesta.HasValue && _idProductoSigesta.Value > 0;
            ConfigurarGrid();
        }

        private static string ObtenerFiltroConsulta(string descripcion)
        {
            string valor = (descripcion ?? string.Empty).Trim();
            if (valor.Length == 0)
                return string.Empty;

            int separador = valor.IndexOf(' ');
            string primeraPalabra = separador > 0
                ? valor.Substring(0, separador)
                : valor;

            return primeraPalabra;
        }

        private void ConfigurarGrid()
        {
            gridViewProductos.OptionsBehavior.Editable = false;
            gridViewProductos.OptionsBehavior.ReadOnly = true;
            gridViewProductos.OptionsSelection.MultiSelect = false;
            gridViewProductos.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewProductos.OptionsView.ShowGroupPanel = false;
            gridViewProductos.OptionsView.ShowAutoFilterRow = true;
            gridViewProductos.OptionsView.ColumnAutoWidth = false;
            gridViewProductos.Appearance.FocusedRow.BackColor =
                Color.FromArgb(204, 229, 255);
            gridViewProductos.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewProductos.Appearance.HideSelectionRow.BackColor =
                Color.FromArgb(204, 229, 255);
            gridViewProductos.Appearance.HideSelectionRow.Options.UseBackColor = true;
            gridViewProductos.Appearance.Row.Font = new Font("Segoe UI", 9f);
            gridViewProductos.Appearance.Row.Options.UseFont = true;
            gridViewProductos.Appearance.HeaderPanel.Font =
                new Font("Segoe UI", 9f, FontStyle.Bold);
            gridViewProductos.Appearance.HeaderPanel.Options.UseFont = true;
        }

        private async void frmConsultaProductoExistente_Shown(object sender, EventArgs e)
        {
            _formularioMostrado = true;
            await CargarProductosAsync();
            txtFiltro.Focus();
            txtFiltro.SelectAll();
        }

        private async Task CargarProductosAsync()
        {
            string textoBusqueda = txtFiltro.Text;
            string filtro = ObtenerFiltroConsulta(textoBusqueda);
            if (string.IsNullOrWhiteSpace(filtro))
            {
                LimpiarResultados();
                return;
            }

            if (_cargando)
            {
                _busquedaPendiente = true;
                return;
            }

            _cargando = true;
            btnBuscar.Enabled = false;
            btnSeleccionar.Enabled = false;
            lblEstado.Text = "Buscando productos registrados...";
            UseWaitCursor = true;

            try
            {
                DataTable productos = await Task.Run(() => _dal.EjecutarConsulta(
                    "[EINVENTARIO].[SP_PRODUCTO]",
                    new
                    {
                        ACCION = "PRODUCTOS_REGISTRADO",
                        FILTRO = filtro
                    }));

                if (IsDisposed)
                    return;

                if (!string.Equals(
                    txtFiltro.Text,
                    textoBusqueda,
                    StringComparison.Ordinal))
                {
                    return;
                }

                gridProductos.DataSource = productos;
                gridProductos.ForceInitialize();
                gridViewProductos.PopulateColumns();
                ConfigurarColumnas();

                int cantidad = productos == null ? 0 : productos.Rows.Count;
                btnSeleccionar.Enabled = cantidad > 0;
                lblEstado.Text = cantidad == 0
                    ? "No se encontraron productos. Cambie el texto de búsqueda e intente nuevamente."
                    : $"{cantidad} producto(s) encontrado(s). Seleccione el producto local que desea relacionar.";

                if (cantidad > 0)
                    gridViewProductos.FocusedRowHandle = 0;
            }
            catch (Exception ex)
            {
                gridProductos.DataSource = null;
                lblEstado.Text = "No fue posible consultar los productos registrados.";
                XtraMessageBox.Show(
                    "No fue posible consultar los productos registrados:\n\n" + ex.Message,
                    "Productos registrados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
                {
                    UseWaitCursor = false;
                    btnBuscar.Enabled = true;
                }

                _cargando = false;

                if (_busquedaPendiente && !IsDisposed)
                {
                    _busquedaPendiente = false;
                    temporizadorBusqueda.Stop();
                    temporizadorBusqueda.Start();
                }
            }
        }

        private void LimpiarResultados()
        {
            gridProductos.DataSource = null;
            gridViewProductos.Columns.Clear();
            btnSeleccionar.Enabled = false;
            lblEstado.Text = "Ingrese una descripción para buscar productos.";
        }

        private void ConfigurarColumnas()
        {
            foreach (GridColumn column in gridViewProductos.Columns)
            {
                column.Visible = false;
                column.OptionsColumn.AllowEdit = false;
                column.OptionsColumn.ReadOnly = true;
            }

            MostrarColumna("COD_REF", "Código", 130, 0);
            MostrarColumna("DESCRIPCION", "Descripción", 390, 1);
            MostrarColumna("NOMBRE_CATEGORIA", "Categoría", 190, 2);
            MostrarColumna("NOMBRE_SUBCATEGORIA", "Subcategoría", 210, 3);
        }

        private void MostrarColumna(
            string fieldName,
            string caption,
            int width,
            int visibleIndex)
        {
            GridColumn column = gridViewProductos.Columns.ColumnByFieldName(fieldName);
            if (column == null)
                return;

            column.Caption = caption;
            column.Width = width;
            column.Visible = true;
            column.VisibleIndex = visibleIndex;
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            temporizadorBusqueda.Stop();
            _busquedaPendiente = false;
            await CargarProductosAsync();
        }

        private async void txtFiltro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.Handled = true;
            e.SuppressKeyPress = true;
            temporizadorBusqueda.Stop();
            _busquedaPendiente = false;
            await CargarProductosAsync();
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            temporizadorBusqueda.Stop();

            if (string.IsNullOrWhiteSpace(txtFiltro.Text))
            {
                _busquedaPendiente = false;
                LimpiarResultados();
                return;
            }

            if (!_formularioMostrado)
                return;

            btnSeleccionar.Enabled = false;
            lblEstado.Text = "Esperando para actualizar la búsqueda...";
            temporizadorBusqueda.Start();
        }

        private async void temporizadorBusqueda_Tick(object sender, EventArgs e)
        {
            temporizadorBusqueda.Stop();
            await CargarProductosAsync();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            SeleccionarProductoActivo();
        }

        private void btnRegistrarNuevo_Click(object sender, EventArgs e)
        {
            if (!_idProductoSigesta.HasValue || _idProductoSigesta.Value <= 0)
            {
                XtraMessageBox.Show(
                    "No se pudo identificar el código del producto SIGESTA.",
                    "Registrar producto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using (var frm = new frmProducto
            {
                IdProducto = 0,
                IdProductoSigestaInicial = _idProductoSigesta.Value,
                NombreProductoSigestaInicial = _nombreProductoSigesta,
                StartPosition = FormStartPosition.CenterParent
            })
            {
                frm.ShowDialog(this);

                if (frm.IdProducto <= 0)
                    return;
            }

            ProductoRegistrado = true;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void gridViewProductos_DoubleClick(object sender, EventArgs e)
        {
            SeleccionarProductoActivo();
        }

        private void SeleccionarProductoActivo()
        {
            DataRow producto = gridViewProductos.GetFocusedDataRow();
            if (producto == null)
            {
                XtraMessageBox.Show(
                    "Seleccione un producto de la lista.",
                    "Productos registrados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            ProductoSeleccionado = producto;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
