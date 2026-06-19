using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Bancos
{
    public partial class frmChequeSeleccionQuedan : Form
    {
        private readonly DALBase _dal = new DALBase();
        // DataTables backing de cada grid
        private DataTable _dtPendientes;   // arriba
        private DataTable _dtPagar;        // abajo

        // ============================================================
        // Propiedades públicas: entrada y salida del form
        // ============================================================
        public string CodigoEntidad { get; set; }
        public string NombreEntidad { get; set; }
        public DataTable DocumentosAPagar { get; private set; }
        public decimal TotalAPagar { get; private set; }
        public frmChequeSeleccionQuedan()
        {
            InitializeComponent();
        }

        private void frmChequeSeleccionPago_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);
            InicializarGrids();
            CargarDocumentos();
            ActualizarTotal();
        }
        private void InicializarGrids()
        {
            // Las columnas vienen del Designer — no auto-poblar
            gridViewPendientePago.OptionsBehavior.AutoPopulateColumns = false;
            gridViewPagar.OptionsBehavior.AutoPopulateColumns = false;
            // Ocultar el group panel en ambos grids
            gridViewPendientePago.OptionsView.ShowGroupPanel = false;
            gridViewPagar.OptionsView.ShowGroupPanel = false;

            AplicarEstiloGrid(gridViewPendientePago);
            AplicarEstiloGrid(gridViewPagar);
        }
        private void AplicarEstiloGrid(GridView view)
        {
            view.OptionsBehavior.Editable = false;
            view.OptionsSelection.MultiSelect = false;
            // ✅ Resaltar fila completa, no la celda enfocada
            view.OptionsSelection.EnableAppearanceFocusedRow = true;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            // Forecolor negro
            view.Appearance.Row.ForeColor = Color.Black;            
            view.Appearance.Row.Options.UseForeColor = true;
            // Fila enfocada (cuando el grid tiene foco)
            view.Appearance.FocusedRow.BackColor = Color.FromArgb(204, 229, 255);
            view.Appearance.FocusedRow.ForeColor = Color.Black;
            view.Appearance.FocusedRow.Options.UseBackColor = true;
            view.Appearance.FocusedRow.Options.UseForeColor = true;
            // Fila seleccionada cuando el grid pierde foco (mismo color para consistencia)
            view.Appearance.HideSelectionRow.BackColor = Color.FromArgb(204, 229, 255);
            view.Appearance.HideSelectionRow.ForeColor = Color.Black;
            view.Appearance.HideSelectionRow.Options.UseBackColor = true;
            view.Appearance.HideSelectionRow.Options.UseForeColor = true;
            // Fila con tipo de letra y negrita
            view.Appearance.Row.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            view.Appearance.Row.Options.UseFont = true;
        }

        

        // ============================================================
        // Carga inicial: documentos pendientes desde el SP
        // ============================================================
        private void CargarDocumentos()
        {
            if (string.IsNullOrWhiteSpace(CodigoEntidad))
            {
                XtraMessageBox.Show("No se indicó el código de entidad del proveedor.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                return;
            }
            txtPROVEEDOR.Text = CodigoEntidad;
            txtNOMBRE_PROVEEDOR.Text = NombreEntidad; 

            try
            {
                Cursor = Cursors.WaitCursor;
                _dtPagar = _dal.EjecutarConsulta("SP_QUEDAN",
                    new
                    {
                        ACCION = "LISTAR_CCF_PENDIENTES_PAGO",
                        CODIGO_ENTIDAD = CodigoEntidad
                    });
                _dtPendientes = _dtPagar.Clone();
                gridControl1.DataSource = _dtPendientes;
                gridControl2.DataSource = _dtPagar;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar los documentos:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // ============================================================
        // Movimiento entre DataTables
        // ============================================================
        private void MoverFila(DataTable origen, DataTable destino, int rowHandle, GridView view)
        {
            DataRow filaOrigen = view.GetDataRow(rowHandle);
            if (filaOrigen == null) return;

            DataRow nueva = destino.NewRow();
            foreach (DataColumn col in destino.Columns)
            {
                if (origen.Columns.Contains(col.ColumnName))
                    nueva[col.ColumnName] = filaOrigen[col.ColumnName];
            }
            destino.Rows.Add(nueva);
            origen.Rows.Remove(filaOrigen);
        }

        private void MoverTodas(DataTable origen, DataTable destino)
        {
            for (int i = origen.Rows.Count - 1; i >= 0; i--)
            {
                DataRow fila = origen.Rows[i];
                DataRow nueva = destino.NewRow();
                foreach (DataColumn col in destino.Columns)
                {
                    if (origen.Columns.Contains(col.ColumnName))
                        nueva[col.ColumnName] = fila[col.ColumnName];
                }
                destino.Rows.Add(nueva);
                origen.Rows.Remove(fila);
            }
        }

        // ============================================================
        // Botones: Pendientes (arriba) -> Pagar (abajo)
        // ============================================================
        private void btnBajar_Click(object sender, EventArgs e)
        {
            if (gridViewPendientePago.FocusedRowHandle < 0 || _dtPendientes.Rows.Count == 0)
            {
                XtraMessageBox.Show("Seleccione un documento en la lista de pendientes.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int handle = gridViewPendientePago.FocusedRowHandle;
            MoverFila(_dtPendientes, _dtPagar, handle, gridViewPendientePago);
            ActualizarTotal();
        }

        private void btnBajarTodos_Click(object sender, EventArgs e)
        {
            if (_dtPendientes.Rows.Count == 0) return;
            MoverTodas(_dtPendientes, _dtPagar);
            ActualizarTotal();
        }

        // ============================================================
        // Botones: Pagar (abajo) -> Pendientes (arriba)
        // ============================================================
        private void btnSubir_Click(object sender, EventArgs e)
        {
            if (gridViewPagar.FocusedRowHandle < 0 || _dtPagar.Rows.Count == 0)
            {
                XtraMessageBox.Show("Seleccione un documento en la lista a pagar.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int handle = gridViewPagar.FocusedRowHandle;
            MoverFila(_dtPagar, _dtPendientes, handle, gridViewPagar);
            ActualizarTotal();
        }

        private void btnSubirTodos_Click(object sender, EventArgs e)
        {
            if (_dtPagar.Rows.Count == 0) return;
            MoverTodas(_dtPagar, _dtPendientes);
            ActualizarTotal();
        }

        // ============================================================
        // Total a pagar
        // ============================================================
        private void ActualizarTotal()
        {
            decimal total = 0;
            foreach (DataRow r in _dtPagar.Rows)
            {
                if (r["SALDO"] != DBNull.Value)
                    total += Convert.ToDecimal(r["SALDO"]);
            }
            txtValorAPagar.Text = total.ToString("N2");
        }

        // ============================================================
        // Retornar al padre
        // ============================================================
        private void btnRetornar_Click(object sender, EventArgs e)
        {
            if (_dtPagar.Rows.Count == 0)
            {
                XtraMessageBox.Show("Debe seleccionar al menos un documento para pagar.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Copia defensiva
            DocumentosAPagar = _dtPagar.Copy();

            TotalAPagar = 0;
            foreach (DataRow r in DocumentosAPagar.Rows)
            {
                if (r["SALDO"] != DBNull.Value)
                    TotalAPagar += Convert.ToDecimal(r["SALDO"]);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

       
    }
}
