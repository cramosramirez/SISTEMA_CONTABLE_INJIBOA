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
    public partial class frmConsultaCompraExterior : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtDetalle; 

        public frmConsultaCompraExterior()
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

            gvDetalle.CustomRowCellEdit += GvDetalle_CustomRowCellEdit;
        }

        private void GvDetalle_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column == colVER_R)
            {
                object val = gvDetalle.GetRowCellValue(e.RowHandle, "ID_COMPROBANTE_RET");
                bool tieneRetencion = val != null && val != DBNull.Value;

                e.RepositoryItem = tieneRetencion ? riVerR : riVerRVacio;
            }
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
            _dtDetalle = _dal.EjecutarConsulta("[EPROVEEDOR].[SP_COMPRA_EXTERIOR]",
                new { ACCION = "COMPRA_EXTERIOR_LISTAR" });
            gridControl1.DataSource = _dtDetalle;            
        }
        #endregion
                      

        #region === HELPERS ===

        private int? ObtenerIdFilaActiva()
        {
            if (gvDetalle.FocusedRowHandle < 0) return null;
            object val = gvDetalle.GetRowCellValue(gvDetalle.FocusedRowHandle, "ID_COMPRA_EXTERIOR");
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }
        private int? ObtenerIdQuedanFilaActiva()
        {
            if (gvDetalle.FocusedRowHandle < 0) return null;
            object val = gvDetalle.GetRowCellValue(gvDetalle.FocusedRowHandle, "ID_QUEDAN");
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

        private void AbrirDocumento(int idCompraExterior)
        {           
            using (var frm = new frmDocumentoCompraExterior())
            {
                frm.IdCompraExterior = idCompraExterior;                
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

        private void riVerQ_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                int? idQuedan = ObtenerIdQuedanFilaActiva();
                if (idQuedan.HasValue)
                {
                    var reporte = new rptQuedan
                    {
                        IdQuedan = idQuedan.Value,
                        NombreProcedimiento = "[EPROVEEDOR].[SP_QUEDAN_RPT]"
                    };
                    reporte.MostrarPreview();
                } 
                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al imprimir:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Close(); 
        }

        private void riVerR_ButtonClick(object sender, ButtonPressedEventArgs e)
        {            
            try
            {
                Cursor = Cursors.WaitCursor;
                int? id = ObtenerIdFilaActiva();
                if (id.HasValue)
                {
                    var reporte = new rptCompRetencion
                    {
                        IdCcfCompra = id.Value,
                        NombreProcedimiento = "[EPROVEEDOR].[SP_COMPROBANTE_RETENCION_RPT]"
                    };
                    reporte.MostrarPreview();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al imprimir:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}
