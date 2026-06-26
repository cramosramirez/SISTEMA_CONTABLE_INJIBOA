using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using SistemaContable.DAL;
namespace SistemaContable.UI.Forms.NotaRemision
{
    public partial class frmConsultaod : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtDetalle;

        public frmConsultaod()
        {
            InitializeComponent();
        }


        #region === CARGA INICIAL ===
        private void frmConsultaod_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarDatos();
        }
        #endregion


        #region === CONFIGURACIÓN DEL GRID ===
        private void ConfigurarGrid()
        {
            //// --- Vista general ---
            gridControl1.ForceInitialize();
            gvDetalle.OptionsView.ShowGroupPanel = false;   // panel de agrupación visible
            gvDetalle.OptionsView.ShowAutoFilterRow = false;
            gvDetalle.OptionsBehavior.AutoExpandAllGroups = false;
            gvDetalle.OptionsBehavior.Editable = false;   // necesario para los botones por fila
            gvDetalle.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;

            // --- Buscador global ---
            gvDetalle.OptionsFind.AlwaysVisible = true;
            gvDetalle.OptionsFind.FindNullPrompt = "Introduzca el texto a buscar...";

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



            // --- Embedded Navigator: solo Nuevo ---
            gridControl1.UseEmbeddedNavigator = true;
            var nav = gridControl1.EmbeddedNavigator;
            nav.Buttons.Append.Visible = false;
            nav.Buttons.Remove.Visible = false;
            nav.Buttons.Edit.Visible = false;
            nav.Buttons.EndEdit.Visible = false;
            nav.Buttons.CancelEdit.Visible = false;
        }

        private void GvDetalle_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            //if (e.Info is GridGroupRowInfo info)
            //{
            //    info.GroupText = $"N° Quedan: {info.EditValue}";
            //}
        }
        #endregion


        #region === CARGA DE DATOS ===
        private void CargarDatos()
        {
            _dtDetalle = _dal.EjecutarConsulta("[EORDEN_DESPACHO].SP_NOTAREMISION_ENC",
                new { ACCION = "LIST" });
            gridControl1.DataSource = _dtDetalle;
            gridControl1.Refresh();
        }
        #endregion


        #region === HELPERS ===

        private int? ObtenerIdFilaActiva()
        {
            if (gvDetalle.FocusedRowHandle < 0) return null;
            object val = gvDetalle.GetRowCellValue(gvDetalle.FocusedRowHandle, "ID_ODENC");
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }

        private void AbrirDocumento(int IdTraslado)
        {
            using (var frm = new frmTraslado())
            {
                frm.IdTraslado = IdTraslado;
                frm.ShowDialog(this);
            }
            CargarDatos();
        }

        #endregion



        //private void riEditar_ButtonClick(object sender, ButtonPressedEventArgs e)
        //{
        //    int? id = ObtenerIdFilaActiva();
        //    if (id.HasValue) AbrirDocumento(id.Value);
        //}

        private void btnNuevoNR_Click(object sender, EventArgs e)
        {
            AbrirDocumento(IdTraslado: 0);
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void gvDetalle_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

            if (e.Column == colEDITAR)
            {
                int? id = ObtenerIdFilaActiva();
                if (id.HasValue) AbrirDocumento(id.Value);
            }
            if (e.Column == colVER_Q)
            {
                int? id = ObtenerIdFilaActiva();
                XtraMessageBox.Show("Reporte ",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
