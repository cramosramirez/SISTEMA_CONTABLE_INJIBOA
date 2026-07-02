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

namespace SistemaContable.UI.Forms.Proveedores
{
    public partial class frmConsultaProveedor : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtDetalle;
        private Dictionary<string, int> _anchosColumnas;
        public frmConsultaProveedor()
        {
            InitializeComponent();
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Close(); 
        }

        private void frmConsultaProveedor_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarDatos();
        }

        private void ConfigurarGrid()
        {
            // --- Vista general ---
            gridControl1.ForceInitialize();
            gvDetalle.OptionsBehavior.AutoPopulateColumns = false;
            gvDetalle.OptionsBehavior.Editable = true;
            gvDetalle.OptionsView.ShowGroupPanel = false;
            gvDetalle.OptionsView.ShowAutoFilterRow = true;
            gvDetalle.OptionsView.ColumnAutoWidth = false;
            gvDetalle.OptionsSelection.MultiSelect = false;
            gvDetalle.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvDetalle.OptionsSelection.EnableAppearanceFocusedRow = true;

            // --- Buscador global ---
            gvDetalle.OptionsFind.AlwaysVisible = true;
            gvDetalle.OptionsFind.FindNullPrompt = "Introduzca el texto a buscar...";
            gvDetalle.OptionsFind.ShowFindButton = true;            

            gvDetalle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gvDetalle.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvDetalle.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvDetalle.Appearance.FocusedRow.BackColor = Color.FromArgb(204, 229, 255);
            gvDetalle.Appearance.FocusedRow.Options.UseBackColor = true;
            gvDetalle.Appearance.HideSelectionRow.BackColor = Color.FromArgb(204, 229, 255);
            gvDetalle.Appearance.HideSelectionRow.Options.UseBackColor = true;
            gvDetalle.Appearance.Row.ForeColor = Color.Black;
            gvDetalle.Appearance.Row.Font = new Font("Segoe UI", 9f);
            gvDetalle.Appearance.Row.Options.UseFont = true;

            gvDetalle.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            gvDetalle.Appearance.HeaderPanel.Options.UseFont = true;
            gvDetalle.Appearance.Row.Font = new Font("Segoe UI", 9f);
            gvDetalle.Appearance.Row.Options.UseFont = true;


            // --- Embedded Navigator: solo Nuevo ---
            gridControl1.UseEmbeddedNavigator = true;
            var nav = gridControl1.EmbeddedNavigator;
            nav.Buttons.Append.Visible = false;
            nav.Buttons.Remove.Visible = false;
            nav.Buttons.Edit.Visible = false;
            nav.Buttons.EndEdit.Visible = false;
            nav.Buttons.CancelEdit.Visible = false;
        }
        private void CargarDatos()
        {            
            gridControl1.BeginUpdate();
            try
            {
                _dtDetalle = _dal.EjecutarConsulta("SP_ENTIDAD",
                    new
                    {
                        ACCION = "CONSULTA_TODOS_PROVEEDORES",
                        ROL = "PRO"
                    });

                gridControl1.DataSource = _dtDetalle;
            }
            finally
            {
                gridControl1.EndUpdate();
            }

            // Primera vez: BestFit y guardar
            // Siguientes: restaurar
            var timer = new Timer { Interval = 50 };
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                timer.Dispose();

                if (_anchosColumnas == null)
                {
                    gvDetalle.BestFitColumns();
                    GuardarAnchosColumnas();
                }
                else
                {
                    RestaurarAnchosColumnas();
                }                   
            };
            timer.Start();
        }


        private int? ObtenerIdFilaActiva()
        {
            if (gvDetalle.FocusedRowHandle < 0) return null;
            object val = gvDetalle.GetRowCellValue(gvDetalle.FocusedRowHandle, "ID_ENTIDAD");
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }

        private void AbrirDocumento(int idEntidad)
        {
            using (var frm = new frmProveedor())
            {
                frm.IdEntidad = idEntidad;                
                frm.ShowDialog(this);
            }
            CargarDatos();
        }

        private void btnNuevoQuedan_Click(object sender, EventArgs e)
        {
            AbrirDocumento(0);
        }

        private void riEditar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int? id = ObtenerIdFilaActiva();
            if (id.HasValue) AbrirDocumento(id.Value);
        }

        private void GuardarAnchosColumnas()
        {
            _anchosColumnas = new Dictionary<string, int>();
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gvDetalle.Columns)
            {
                _anchosColumnas[col.FieldName] = col.Width;
            }
        }

        private void RestaurarAnchosColumnas()
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gvDetalle.Columns)
            {
                if (_anchosColumnas.TryGetValue(col.FieldName, out int ancho))
                    col.Width = ancho;
            }
        }
    }
}
