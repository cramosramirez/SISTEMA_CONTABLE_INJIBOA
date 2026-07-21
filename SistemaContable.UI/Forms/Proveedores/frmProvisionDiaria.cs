using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Proveedores
{
    public partial class frmProvisionDiaria : Form
    {
        private readonly DALBase _dal = new DALBase();
        public frmProvisionDiaria()
        {
            InitializeComponent();
        }

        private void frmProvisionDiaria_Load(object sender, EventArgs e)
        {            
            InicializarDatos();
            InicializarGrids();
            CargarGrids();

            dateEdit1.Leave += (s, ev) => CargarGrids();
            cbxTIPO_PROVEEDOR.SelectionChangeCommitted += (s, ev) => CargarGrids();
            dateEdit1.EditValueChanged += (s, ev) => CargarGrids();
        }

        private void InicializarDatos()
        {
            dateEdit1.DateTime = DateTime.Today;
            CargarTiposProveedor();
            AsignarConcepto();
        }       

        private void AsignarConcepto()
        {           
            txtCONCEPTO_PARTIDA.Text = "PARTIDA DEL DIA " + dateEdit1.Text;           
        }

        private void CargarTiposProveedor()
        {
            var dt = new DataTable();
            dt.Columns.Add("CODIGO", typeof(string));
            dt.Columns.Add("DESCRIPCION", typeof(string));
            dt.Rows.Add("T", "[TODOS]");
            dt.Rows.Add("P", "PROVEEDORES");
            dt.Rows.Add(Configuracion.CodigoCCJIBOA, "CENTRO DE SERVICIOS JIBOA S.A. DE C.V.");
            dt.Rows.Add(Configuracion.CodigoHIBRONSA, "HIERROS Y BRONCES S.A. DE C.V.");

            cbxTIPO_PROVEEDOR.DataSource = dt;
            cbxTIPO_PROVEEDOR.DisplayMember = "DESCRIPCION";
            cbxTIPO_PROVEEDOR.ValueMember = "CODIGO";
            cbxTIPO_PROVEEDOR.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxTIPO_PROVEEDOR.SelectedIndex = 0;
        }

        private void InicializarGrids()
        {
            // gridControl1 (arriba) -> SIN provisión
            ConfigurarGrid(gridControl1, gridView1);
            gridControl1.Dock = DockStyle.Fill;

            // gridControl2 (abajo) -> CON provisión
            ConfigurarGrid(gridControl2, gridView2);
            gridControl2.Dock = DockStyle.Fill;
        }

        private void ConfigurarGrid(GridControl grid, GridView view)
        {
            // Mostrar el caption del grid como encabezado azul con texto blanco
            grid.UseEmbeddedNavigator = false;
            //grid.ViewCaptionHeight = 28;
            grid.ShowOnlyPredefinedDetails = true;            
            view.Appearance.Row.ForeColor = Color.Black;
            view.Appearance.Row.Options.UseForeColor = true;            
            view.Appearance.FocusedRow.ForeColor = Color.Black;
            view.Appearance.FocusedRow.Options.UseForeColor = true;            
            view.Appearance.HideSelectionRow.ForeColor = Color.Black;
            view.Appearance.HideSelectionRow.Options.UseForeColor = true;
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsBehavior.Editable = false;
            view.OptionsSelection.MultiSelect = false;
            view.OptionsSelection.EnableAppearanceFocusedRow = true;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;

            view.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            view.Appearance.HeaderPanel.Options.UseFont = true;
            view.Appearance.Row.Font = new Font("Segoe UI", 9f);
            view.Appearance.Row.Options.UseFont = true;
        }

        private void CargarGrids()
        {
            string tipoProveedor = cbxTIPO_PROVEEDOR.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(tipoProveedor)) return;

            if (dateEdit1.EditValue == null || dateEdit1.EditValue == DBNull.Value)
            {
                gridControl1.DataSource = null;
                gridControl2.DataSource = null;
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                // gridControl1 (arriba) -> SIN provisión
                DataTable dtSinProvision = _dal.EjecutarConsulta(
                    "SP_CREDITO_FISCAL_LISTAR_PROVISIONADOS",
                    new
                    {
                        ACCION = "LISTAR_SIN_PROVISION",
                        FECHA = dateEdit1.DateTime,
                        TIPO_PROVEEDOR = tipoProveedor,
                        CODIGO_CCJIBOA = Configuracion.CodigoCCJIBOA,
                        CODIGO_HIBRONSA = Configuracion.CodigoHIBRONSA
                    });

                gridControl1.DataSource = dtSinProvision;
                
                // gridControl2 (abajo) -> CON provisión
                DataTable dtConProvision = _dal.EjecutarConsulta(
                    "SP_CREDITO_FISCAL_LISTAR_PROVISIONADOS",
                    new
                    {
                        ACCION = "LISTAR_CON_PROVISION",
                        FECHA = dateEdit1.DateTime,
                        TIPO_PROVEEDOR = tipoProveedor,
                        CODIGO_CCJIBOA = Configuracion.CodigoCCJIBOA,
                        CODIGO_HIBRONSA = Configuracion.CodigoHIBRONSA
                    });

                gridControl2.DataSource = dtConProvision;                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los documentos:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateEdit1_Leave(object sender, EventArgs e)
        {
            AsignarConcepto(); 
        }

        private void EnterComoTab(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
               
        }
    }
}
