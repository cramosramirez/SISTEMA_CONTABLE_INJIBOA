using SistemaContable.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
namespace SistemaContable.UI.Forms.Clientes
{
    public partial class frmConsultaCliente : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtEntidades;
        private Dictionary<string, int> _anchosColumnas;
        public frmConsultaCliente()
        {
            InitializeComponent();
        }
        #region === CARGA INICIAL ===
        private void frmConsultaCliente_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarDatos();
        }
        #endregion
        #region === CONFIGURACIÓN DEL GRID ===
        private void ConfigurarGrid()
        {
            gridControl1.ForceInitialize();
            gvEntidades.OptionsBehavior.AutoPopulateColumns = false;
            gvEntidades.OptionsBehavior.Editable = true;
            gvEntidades.OptionsView.ShowGroupPanel = false;
            gvEntidades.OptionsView.ShowAutoFilterRow = true;
            gvEntidades.OptionsView.ColumnAutoWidth = true;
            gvEntidades.OptionsSelection.MultiSelect = false;
            gvEntidades.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvEntidades.OptionsSelection.EnableAppearanceFocusedRow = true;

            // El escalado del formulario reduce los valores definidos por el diseñador.
            // Se fija el ancho efectivo después de inicializar el grid para que el
            // icono de 20x20 siempre tenga espacio suficiente para dibujarse.
            colEDITAR.MinWidth = 30;
            colEDITAR.MaxWidth = 30;
            colEDITAR.Width = 30;
            colEDITAR.OptionsColumn.AllowSize = false;
            colEDITAR.OptionsColumn.FixedWidth = true;

            gvEntidades.OptionsFind.AlwaysVisible = true;
            gvEntidades.OptionsFind.FindNullPrompt = "Introduzca el texto a buscar...";
            gvEntidades.OptionsFind.ShowFindButton = true;

            gvEntidades.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gvEntidades.Appearance.FocusedRow.BackColor = Color.FromArgb(204, 229, 255);
            gvEntidades.Appearance.FocusedRow.Options.UseBackColor = true;
            gvEntidades.Appearance.HideSelectionRow.BackColor = Color.FromArgb(204, 229, 255);
            gvEntidades.Appearance.HideSelectionRow.Options.UseBackColor = true;
            gvEntidades.Appearance.Row.ForeColor = Color.Black;
            gvEntidades.Appearance.Row.Font = new Font("Segoe UI", 9f);
            gvEntidades.Appearance.Row.Options.UseFont = true;

            gvEntidades.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            gvEntidades.Appearance.HeaderPanel.Options.UseFont = true;

            // Navigator
            gridControl1.UseEmbeddedNavigator = true;
            var nav = gridControl1.EmbeddedNavigator;
            nav.Buttons.Append.Visible = false;
            nav.Buttons.Remove.Visible = false;
            nav.Buttons.Edit.Visible = false;
            nav.Buttons.EndEdit.Visible = false;
            nav.Buttons.CancelEdit.Visible = false;
        }
        #endregion
        #region === CARGA DE DATOS ===
        private void CargarDatos()
        {
            gridControl1.BeginUpdate();
            try
            {
                _dtEntidades = _dal.EjecutarConsulta("[EDTE].[SP_ENTIDAD]", new
                {
                    ACCION = "LISTAR",
                    ID_ROL_USUARIO = Configuracion.IdRolActual
                });
                gridControl1.DataSource = _dtEntidades;
            }
            finally
            {
                gridControl1.EndUpdate();
            }

            var timer = new Timer { Interval = 50 };
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                timer.Dispose();

                if (_anchosColumnas == null)
                {
                    gvEntidades.BestFitColumns();
                    GuardarAnchosColumnas();
                }
                else
                {
                    RestaurarAnchosColumnas();
                }
            };
            timer.Start();
        }
        #endregion
        #region === HELPERS ===
        private void AbrirEntidad(int idEntidad)
        {
            using (var frm = new frmCliente())
            {
                frm.IdEntidad = idEntidad;
                frm.ShowDialog(this);
            }
            CargarDatos();
        }

        private void GuardarAnchosColumnas()
        {
            _anchosColumnas = new Dictionary<string, int>();
            foreach (DevExpress.XtraGrid.Columns.GridColumn columna in gvEntidades.Columns)
            {
                _anchosColumnas[columna.FieldName] = columna.Width;
            }
        }

        private void RestaurarAnchosColumnas()
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn columna in gvEntidades.Columns)
            {
                if (_anchosColumnas.TryGetValue(columna.FieldName, out int ancho))
                    columna.Width = ancho;
            }
        }
        #endregion
        #region === EVENTOS ===
        private void riEditar_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            int rowHandle = gvEntidades.FocusedRowHandle;
            if (rowHandle < 0) return;
            object val = gvEntidades.GetRowCellValue(rowHandle, colID_ENTIDAD);
            if (val == null || val == DBNull.Value) return;
            int id = Convert.ToInt32(val);
            if (id > 0) AbrirEntidad(id);
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            AbrirEntidad(idEntidad: 0);
        }
        #endregion
    }
}
