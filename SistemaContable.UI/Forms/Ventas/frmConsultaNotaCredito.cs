using SistemaContable.DAL;
using SistemaContable.RP.Ventas;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;

namespace SistemaContable.UI.Forms.Ventas
{
    public partial class frmConsultaNotaCredito : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtDatos;

        public frmConsultaNotaCredito()
        {
            InitializeComponent();
        }

        #region === CARGA INICIAL ===
        private void frmConsultaNotaCredito_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarDatos();
        }
        #endregion

        #region === CONFIGURACIÓN DEL GRID ===
        private void ConfigurarGrid()
        {
            gridControl1.ForceInitialize();

            gvDetalle.OptionsView.ShowGroupPanel = false;
            gvDetalle.OptionsView.ShowAutoFilterRow = false;
            gvDetalle.OptionsBehavior.Editable = false;

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

            // ID oculto
            colID_NTCENC.Visible = false;

            // Botón Editar
            colEDITAR.Caption = "Editar";
            colEDITAR.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
            colEDITAR.Visible = true;
            colEDITAR.VisibleIndex = 0;
            colEDITAR.Width = 41;

            // Botón Ver reporte
            colVER_R.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
            colVER_R.Visible = true;
            colVER_R.VisibleIndex = 1;
            colVER_R.Width = 41;

            // Fecha NCR
            colFECHA.Caption = "Fecha NCR";
            colFECHA.OptionsColumn.AllowEdit = false;
            colFECHA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colFECHA.AppearanceCell.Options.UseTextOptions = true;

            // Número interno
            colNUMINTERNO.Caption = "N° Interno";
            colNUMINTERNO.OptionsColumn.AllowEdit = false;
            colNUMINTERNO.Width = 90;

            // Cliente
            colNOMBRE_ENTIDAD.Caption = "Cliente";
            colNOMBRE_ENTIDAD.OptionsColumn.AllowEdit = false;
            colNOMBRE_ENTIDAD.Width = 220;

            // N° Control NCR
            // (2026-08-26) Roberto pidió quitar esta columna del grid (ocupaba espacio
            // mostrando el N° Control de la NCR truncado; ya se ve el de origen -
            // "N° Control CCF" - que es el relevante para esta consulta).
            colNUMCONTROL.Visible = false;

            // N° Control CCF origen
            colNUMCONTROLCCF.Caption = "N° Control CCF";
            colNUMCONTROLCCF.OptionsColumn.AllowEdit = false;
            colNUMCONTROLCCF.Width = 220;

            // Motivo (OBSERVACIONES)
            colOBSERVACIONES.Caption = "Motivo";
            colOBSERVACIONES.OptionsColumn.AllowEdit = false;
            colOBSERVACIONES.Width = 200;

            // Sello de recepción
            colSELLORECEPCION.Caption = "Sello Recepción";
            colSELLORECEPCION.OptionsColumn.AllowEdit = false;
            colSELLORECEPCION.Width = 200;

            // Total venta
            colTOTALVENTA.Caption = "Total Venta";
            colTOTALVENTA.OptionsColumn.AllowEdit = false;
            colTOTALVENTA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTOTALVENTA.AppearanceCell.Options.UseTextOptions = true;
            colTOTALVENTA.Width = 100;

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
            // (2026-08-26) Se usa de nuevo el SP oficial [EDTE].[SP_NOTACREDITO_ENC]
            // ACCION="LISTAR" (ya corregido: trae NOMBRE_ENTIDAD vía JOIN a ENTIDAD, y el
            // N° Interno del CCF de origen vía JOIN a CREDITOFISCAL_ENC). Ya no se usa el
            // SP aislado SP_NOTACREDITO_LISTAR creado temporalmente antes de tener el texto
            // completo de este SP.
            _dtDatos = _dal.EjecutarConsulta("[EDTE].[SP_NOTACREDITO_ENC]", new
            {
                ACCION = "LISTAR",
                ID_EMISOR = 1
            });
            gridControl1.DataSource = _dtDatos;
            gridControl1.Refresh();
        }
        #endregion

        #region === HELPERS ===
        private int? ObtenerIdFilaActiva()
        {
            if (gvDetalle.FocusedRowHandle < 0) return null;
            object val = gvDetalle.GetRowCellValue(gvDetalle.FocusedRowHandle, "ID_NTCENC");
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }

        private void AbrirDocumento(int idNTCEnc)
        {
            using (var frm = new frmNotaCredito())
            {
                frm.IdNTCEnc = idNTCEnc;
                frm.ShowDialog(this);
            }
            CargarDatos();
        }
        #endregion

        #region === EVENTOS ===
        private void btnNuevaNcr_Click(object sender, EventArgs e)
        {
            AbrirDocumento(idNTCEnc: 0);
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDetalle_RowCellClick(object sender,
            DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == colEDITAR)
            {
                int? id = ObtenerIdFilaActiva();
                if (id.HasValue) AbrirDocumento(id.Value);
            }
            else if (e.Column == colVER_R)
            {
                int? id = ObtenerIdFilaActiva();
                if (!id.HasValue) return;
                // (2026-08-26) Mismo patrón que frmNotaCredito.btnImprimir_Click.
                try
                {
                    Cursor = Cursors.WaitCursor;
                    var reporte = new rptNotaCredito { IdNTCEnc = id.Value };
                    reporte.MostrarPreview();
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Error al mostrar el reporte:\n\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }
        #endregion
    }
}
