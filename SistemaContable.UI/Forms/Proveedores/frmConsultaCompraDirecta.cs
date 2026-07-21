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
    public partial class frmConsultaCompraDirecta : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtDetalle;
        public frmConsultaCompraDirecta()
        {
            InitializeComponent();
        }

        private void frmConsultaCompraDirecta_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarDatos();
        }
        private void ConfigurarGrid()
        {
            // --- Vista general ---
            gridControl1.ForceInitialize();
            gvDetalle.OptionsView.ShowGroupPanel = false;   // panel de agrupación visible
            gvDetalle.OptionsView.ShowAutoFilterRow = true;
            gvDetalle.OptionsBehavior.AutoExpandAllGroups = true;
            gvDetalle.OptionsBehavior.Editable = true;   // necesario para los botones por fila

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
            _dtDetalle = _dal.EjecutarConsulta("SP_CREDITO_FISCAL_COMPRA",
                new { ACCION = "LISTAR_COMPRA_X_CAJA_CHICA" });
            gridControl1.DataSource = _dtDetalle;
        }

        private int? ObtenerIdFilaActiva()
        {
            if (gvDetalle.FocusedRowHandle < 0) return null;
            object val = gvDetalle.GetRowCellValue(gvDetalle.FocusedRowHandle, "ID_CCF_COMPRA");
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }

        private void AbrirDocumento(int idCcfCompra)
        {
            using (var frm = new frmDocumentoCompra())
            {
                frm.IdCcfCompra = idCcfCompra;
                frm.EsContado = true;
                frm.ShowDialog(this);
            }
            CargarDatos();
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void riEditar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int? id = ObtenerIdFilaActiva();
            if (id.HasValue) AbrirDocumento(id.Value);
       }      

        private void btnNuevoQuedan_Click(object sender, EventArgs e)
        {
            AbrirDocumento(0);
        }
    }
}
