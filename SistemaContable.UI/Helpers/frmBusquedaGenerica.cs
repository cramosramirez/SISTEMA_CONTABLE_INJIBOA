using SistemaContable.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Helpers
{
    public partial class frmBusquedaGenerica : DevExpress.XtraEditors.XtraForm
    {
        #region Propiedades públicas

        public DataRow FilaSeleccionada { get; private set; }

        #endregion

        #region Campos privados

        private readonly BusquedaConfig _config;
        private readonly DALBase _dal = new DALBase();

        #endregion

        #region Constructor

        public frmBusquedaGenerica(BusquedaConfig config)
        {            
            InitializeComponent();
            _config = config;
        }

        #endregion

        #region Eventos del formulario

        private void frmBusquedaGenerica_Load(object sender, EventArgs e)
        {
            FormHelper.AplicarMayusculas(this);
            CambiarAparienciaGrid();
            ConfigurarColumnas();
            Buscar(null); // carga inicial
            txtTexto_a_buscar.Focus();

            // ESC cierra el formulario
            KeyPreview = true;
            this.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Escape)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
            };

            // Doble clic en el grid selecciona el registro
            gridView1.DoubleClick += (s, ev) => Seleccionar();

            // Flecha abajo desde el textBox baja al grid
            txtTexto_a_buscar.KeyDown += txtTexto_a_buscar_KeyDown;

            // Enter en el grid selecciona
            gridView1.KeyDown += GridView1_KeyDown;

            // Búsqueda incremental con delay 300ms
            var timer = new Timer();
            timer.Interval = 300;
            timer.Tick += (s, ev) =>
            {
                timer.Stop();
                Buscar(txtTexto_a_buscar.Text.Trim());
            };
            txtTexto_a_buscar.TextChanged += (s, ev) =>
            {
                timer.Stop();
                timer.Start();
            };
        }

        private void txtTexto_a_buscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                gridView1.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                Buscar(txtTexto_a_buscar.Text.Trim());
                if (gridView1.RowCount > 0)
                    gridView1.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void GridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Seleccionar();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Up && gridView1.FocusedRowHandle == 0)
            {
                // Subir al campo de búsqueda desde la primera fila
                txtTexto_a_buscar.Focus();
            }
        }

        #endregion

        #region Lógica

        private void ConfigurarColumnas()
        {
            if (_config.Columnas == null || _config.Columnas.Count == 0)
                return;

            gridView1.Columns.Clear();

            foreach (var col in _config.Columnas)
            {
                var gridCol = gridView1.Columns.AddField(col.Key);
                gridCol.Caption = col.Value;
                gridCol.Visible = true;
                gridCol.OptionsColumn.AllowEdit = false;

                // Aplicar ancho si está definido
                if (_config.Anchos != null && _config.Anchos.ContainsKey(col.Key))
                    gridCol.Width = _config.Anchos[col.Key];
            }


            foreach (var col in _config.ColumnasOcultas)
            {
                if (gridView1.Columns[col] != null)
                    gridView1.Columns[col].Visible = false;
            }

        }

        private void Buscar(string filtro)
        {
            try
            {
                // Construir parámetros
                var parametros = new Dictionary<string, object>
                {
                    { "ACCION", _config.Accion },
                    { "FILTRO", string.IsNullOrWhiteSpace(filtro) ? null : filtro }
                };

                // Agregar parámetros extra si los hay
                if (_config.ParametrosExtra != null)
                {
                    foreach (var prop in _config.ParametrosExtra
                                                .GetType().GetProperties())
                        parametros[prop.Name] = prop.GetValue(_config.ParametrosExtra);
                }

                var dt = _dal.EjecutarConsulta(_config.StoredProcedure, parametros);
                gridControl1.DataSource = dt;                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Seleccionar()
        {
            if (gridView1.FocusedRowHandle < 0) return;

            var dt = gridControl1.DataSource as DataTable;
            if (dt == null) return;

            int idx = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
            if (idx < 0 || idx >= dt.Rows.Count) return;

            FilaSeleccionada = dt.Rows[idx];
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CambiarAparienciaGrid()
        {
            gridView1.OptionsView.ShowGroupPanel = false;
            // Selección de fila completa
            gridView1.OptionsSelection.MultiSelect = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = true;

            // Resaltado más notorio — color azul sólido
            gridView1.Appearance.FocusedRow.BackColor = Color.FromArgb(37, 96, 160);
            gridView1.Appearance.FocusedRow.ForeColor = Color.White;
            gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            gridView1.Appearance.FocusedRow.Options.UseForeColor = true;

            // Mantener el resaltado aunque el grid no tenga el foco
            gridView1.Appearance.HideSelectionRow.BackColor = Color.FromArgb(37, 96, 160);
            gridView1.Appearance.HideSelectionRow.ForeColor = Color.White;
            gridView1.Appearance.HideSelectionRow.Options.UseBackColor = true;
            gridView1.Appearance.HideSelectionRow.Options.UseForeColor = true;

            gridView1.Appearance.Row.ForeColor = Color.Black;
            gridView1.Appearance.Row.Font = new Font("Segoe UI", 9f);
            gridView1.Appearance.Row.Options.UseFont = true;
            gridView1.Appearance.Row.Options.UseForeColor = true;

            // También para filas alternadas si las tienes activadas
            gridView1.Appearance.EvenRow.ForeColor = Color.Black;
            gridView1.Appearance.EvenRow.Font = new Font("Segoe UI", 9f);
            gridView1.Appearance.EvenRow.Options.UseFont = true;
            gridView1.Appearance.EvenRow.Options.UseForeColor = true;

            // Encabezado de columnas
            gridView1.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            gridView1.Appearance.HeaderPanel.Options.UseFont = true;
        }

        #endregion
    }
}
