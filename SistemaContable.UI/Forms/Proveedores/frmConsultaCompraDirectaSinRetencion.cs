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
    public partial class frmConsultaCompraDirectaSinRetencion : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtDetalle;
        public frmConsultaCompraDirectaSinRetencion()
        {
            InitializeComponent();
        }

        private void frmConsultaCompraDirectaSinRetencion_Load(object sender, EventArgs e)
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
                new { ACCION = "COMPRA_DIRECTA_SIN_RETENCION_DETALLE_LISTAR" });
            gridControl1.DataSource = _dtDetalle;
        }

        private int? ObtenerIdFilaActiva()
        {
            if (gvDetalle.FocusedRowHandle < 0) return null;
            object val = gvDetalle.GetRowCellValue(gvDetalle.FocusedRowHandle, "ID_CCF_COMPRA");
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }

        private void AbrirDocumento(int idCcfCompra, bool esCCF)
        {
            using (var frm = new frmDocumentoCompra())
            {
                frm.IdCcfCompra = idCcfCompra;
                frm.ShowDialog(this);
            }
            CargarDatos();
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
