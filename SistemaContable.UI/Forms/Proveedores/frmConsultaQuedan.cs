using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SistemaContable.DAL;

namespace SistemaContable.UI.Forms.Proveedores
{
    public partial class frmConsultaQuedan : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtDetalle; 

        public frmConsultaQuedan()
        {
            InitializeComponent();
        }


        #region === CARGA INICIAL ===
        private void frmConsultaQuedan_Load(object sender, EventArgs e)
        {            
            ConfigurarGrid();
            CargarDatos();
        }
        #endregion


        #region === CONFIGURACIÓN DEL GRID ===
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

            // --- Agrupación por NUM_QUEDAN ---
            colNUM_QUEDAN.GroupIndex = 0;

            // Personaliza el texto del header del grupo: "N° Quedan: 24337"
            gvDetalle.CustomDrawGroupRow += GvDetalle_CustomDrawGroupRow;

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
            if (e.Info is GridGroupRowInfo info)
            {
                info.GroupText = $"N° Quedan: {info.EditValue}";
            }
        }
        #endregion


        #region === CARGA DE DATOS ===
        private void CargarDatos()
        {
            _dtDetalle = _dal.EjecutarConsulta("SP_CREDITO_FISCAL_COMPRA",
                new { ACCION = "QUEDAN_DETALLE_LISTAR" });
            gridControl1.DataSource = _dtDetalle;
        }
        #endregion
                      

        #region === HELPERS ===

        private int? ObtenerIdFilaActiva()
        {
            if (gvDetalle.FocusedRowHandle < 0) return null;
            object val = gvDetalle.GetRowCellValue(gvDetalle.FocusedRowHandle, "ID_CCF_COMPRA");
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }
        private int? ObtenerTipoDTEFilaActiva()
        {
            if (gvDetalle.FocusedRowHandle < 0) return null;
            object val = gvDetalle.GetRowCellValue(gvDetalle.FocusedRowHandle, "ID_TIPO_DTE");
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }

        private void AbrirDocumento(int idCcfCompra, bool esCCF)
        {
            if (esCCF)
            {
                using (var frm = new frmDocumentoCompra())
                {
                    frm.IdCcfCompra = idCcfCompra;
                    frm.ShowDialog(this);
                }
                CargarDatos();
            }
            else
            {
                using (var frm = new frmNotaDebCred())
                {
                    frm.IdCcfCompra = idCcfCompra;
                    frm.ShowDialog(this);
                }
                CargarDatos();
            }
                
        }
        #endregion
          
        private void riEditar_ButtonClick(object sender, ButtonPressedEventArgs e)
        {            
            int? id = ObtenerIdFilaActiva();
            int? idTipo_dte = ObtenerTipoDTEFilaActiva();
            bool esCCF = false;

            if (idTipo_dte == 2 || idTipo_dte == 21)            
                esCCF = true;           
                
            if (id.HasValue) AbrirDocumento(id.Value, esCCF);            
        }

        private void btnNuevoQuedan_Click(object sender, EventArgs e)
        {
            AbrirDocumento(0, true);
        }
    }
}
