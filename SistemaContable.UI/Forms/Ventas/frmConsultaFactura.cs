using SistemaContable.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
namespace SistemaContable.UI.Forms.Ventas
{
    public partial class frmConsultaFactura : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtDetalle;

        public frmConsultaFactura()
        {
            InitializeComponent();
        }

        #region === CARGA INICIAL ===
        private void frmConsultaFactura_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarDatos();
        }
        #endregion

        #region === CONFIGURACIÓN DEL GRID ===
        private void ConfigurarGrid()
        {
            gridControl1.ForceInitialize();

            // --- Vista general ---
            gvDetalle.OptionsView.ShowGroupPanel = false;
            gvDetalle.OptionsView.ShowAutoFilterRow = false;
            gvDetalle.OptionsBehavior.Editable = false;   // RowCellClick maneja los botones

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

            // ── Sistema(Id) ────────────────────────────────────────────
            colID_FACTENC.Visible = true;

            // ── Botón Editar ───────────────────────────────────────────
            colEDITAR.Caption = "Editar";
            colEDITAR.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
            colEDITAR.Visible = true;
            colEDITAR.VisibleIndex = 0;
            colEDITAR.Width = 41;

            // ── Fecha de la factura ────────────────────────────────────
            colFECHA.Caption = "Fecha Fact.";
            colFECHA.OptionsColumn.AllowEdit = false;
            colFECHA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colFECHA.AppearanceCell.Options.UseTextOptions = true;

            // ── Número interno ─────────────────────────────────────────
            colNUMINTERNO.Caption = "N° Interno";
            colNUMINTERNO.OptionsColumn.AllowEdit = false;
            colNUMINTERNO.Width = 130;

            // ── Nombre del cliente ─────────────────────────────────────
            colNOMBRE_ENTIDAD.Caption = "Cliente";
            colNOMBRE_ENTIDAD.OptionsColumn.AllowEdit = false;
            colNOMBRE_ENTIDAD.Width = 280;

            // ── Tipo de comprobante ────────────────────────────────────
            colTIPO_DTE.Caption = "Tipo Comp.";
            colTIPO_DTE.OptionsColumn.AllowEdit = false;
            colTIPO_DTE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colTIPO_DTE.AppearanceCell.Options.UseTextOptions = true;
            colTIPO_DTE.Width = 68;

            // ── Código de generación DTE ───────────────────────────────
            colCOD_GENERACION.Caption = "Cód. Generación";
            colCOD_GENERACION.OptionsColumn.AllowEdit = false;
            colCOD_GENERACION.Width = 220;

            // ── Número de control ──────────────────────────────────────
            colFHPROCESAMIENTO.Caption = "N° Control";
            colFHPROCESAMIENTO.OptionsColumn.AllowEdit = false;
            colFHPROCESAMIENTO.Width = 220;
            colFHPROCESAMIENTO.ShowButtonMode = ShowButtonModeEnum.Default;

            // ── Total venta ────────────────────────────────────────────
            colTOTALVENTA.Caption = "Total Venta";
            colTOTALVENTA.OptionsColumn.AllowEdit = false;
            colTOTALVENTA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTOTALVENTA.AppearanceCell.Options.UseTextOptions = true;
            colTOTALVENTA.Width = 100;

            // ── Columna Ver (reporte) ──────────────────────────────────
            colVER_R.Visible = true;

            // ── Navigator: ocultar botones innecesarios ────────────────
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
            _dtDetalle = _dal.EjecutarConsulta("[EDTE].[SP_FACTURA_ENC]", new
            {
                ACCION = "LISTAR",
                ID_EMISOR = 1
            });
            gridControl1.DataSource = _dtDetalle;
            gridControl1.Refresh();
        }
        #endregion

        #region === HELPERS ===
        private int? ObtenerIdFilaActiva()
        {
            if (gvDetalle.FocusedRowHandle < 0) return null;
            object val = gvDetalle.GetRowCellValue(gvDetalle.FocusedRowHandle, "ID_FACTENC");
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }

        private void AbrirDocumento(int idFactEnc)
        {
            using (var frm = new frmFactura())
            {
                frm.IdFactEnc = idFactEnc;
                frm.ShowDialog(this);
            }
            CargarDatos();
        }
        #endregion

        #region === EVENTOS ===
        private void btnNuevoConsumidorFinal_Click(object sender, EventArgs e)
        {
            AbrirDocumento(idFactEnc: 0);
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

                // TODO: reemplazar con la llamada real al formulario de reporte
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Reporte",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }
}

