using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SistemaContable.DAL;
using SistemaContable.RP.Bancos.Proveedores;

namespace SistemaContable.UI.Forms.Proveedores
{
    public partial class frmConsultaCompraRemesa : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtDetalle;

        public frmConsultaCompraRemesa()
        {
            InitializeComponent();
            riVerRVacio = new RepositoryItemButtonEdit();
            riVerRVacio.Buttons.Clear(); // sin botón visible
            riVerRVacio.TextEditStyle = TextEditStyles.DisableTextEditor;
            riVerRVacio.ReadOnly = true;
            gridControl1.RepositoryItems.Add(riVerRVacio);
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
            gvDetalle.OptionsBehavior.AutoPopulateColumns = false; // solo usar las columnas definidas en el diseñador
            gvDetalle.OptionsView.ShowGroupPanel = false;   // panel de agrupación visible
            gvDetalle.OptionsView.ShowAutoFilterRow = true;
            gvDetalle.OptionsBehavior.AutoExpandAllGroups = true;
            gvDetalle.OptionsBehavior.Editable = true;   // necesario para los botones por fila
            colVER_R.Visible = false; // "Ver R" oculto a pedido de Roberto (2026-08-25)
            colVER_Q.Visible = false; // "Ver Q" oculto a pedido de Roberto (2026-08-25)

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

            // --- Agrupación por NUM_QUEDAN ---
          

            

            // --- Embedded Navigator: solo Nuevo ---
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
            _dtDetalle = _dal.EjecutarConsulta("[EPROVEEDOR].[SP_COMPRA_REMESA]",
                new { ACCION = "COMPRA_REMESA_LISTAR", ULTIMOS_3_MESES = chkUltimos3Meses.Checked });
            gridControl1.DataSource = _dtDetalle;

           
        }
        #endregion


        #region === HELPERS ===

        private int? ObtenerIdFilaActiva()
        {
            if (gvDetalle.FocusedRowHandle < 0) return null;
            object val = gvDetalle.GetRowCellValue(gvDetalle.FocusedRowHandle, "ID_COMPRA_REMESA");
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }
        
       

        private void AbrirDocumento(int idCompraExterior)
        {
            using (var frm = new frmDocumentoCompraRemesa())
            {
                frm.IdCompraRemesa = idCompraExterior;
                frm.ShowDialog(this);
            }
            CargarDatos();
        }
        #endregion

        private void riEditar_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            int? id = ObtenerIdFilaActiva();
            if (id.HasValue) AbrirDocumento(id.Value);
        }

        private void btnNuevoQuedan_Click(object sender, EventArgs e)
        {
            AbrirDocumento(0);
        }

        private void chkUltimos3Meses_CheckedChanged(object sender, EventArgs e)
        {
            CargarDatos();
        }

       

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Close();
        }

       
    }
}
