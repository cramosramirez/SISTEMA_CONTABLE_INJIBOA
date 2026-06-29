using SistemaContable.DAL;
using SistemaContable.UI.Properties;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace SistemaContable.UI.Forms.Invalidacion
{
    public partial class frmConsultaInvalidacion : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtDatos;

        public frmConsultaInvalidacion()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        #region Carga Inicial

        private void frmConsultaInvalidacion_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarDatos();
        }

        #endregion

        #region Configuración del Grid

        private void ConfigurarGrid()
        {
            gridControl1.ForceInitialize();

            // ── Crear editores de botones en código (garantiza imagen) ──
            var repoEditar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            repoEditar.AutoHeight = false;
            repoEditar.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            repoEditar.UseReadOnlyAppearance = false;
            repoEditar.Buttons.Clear();
            var btnEditar = new DevExpress.XtraEditors.Controls.EditorButton(
                DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph);
            btnEditar.Image = SistemaContable.UI.Properties.Resources.editar3_32x32;
            repoEditar.Buttons.Add(btnEditar);
            gridControl1.RepositoryItems.Add(repoEditar);

            var repoVerR = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            repoVerR.AutoHeight = false;
            repoVerR.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            repoVerR.UseReadOnlyAppearance = false;
            repoVerR.Buttons.Clear();
            var btnVerR = new DevExpress.XtraEditors.Controls.EditorButton(
                DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph);
            btnVerR.Image = SistemaContable.UI.Properties.Resources.quedan2_32x32;
            repoVerR.Buttons.Add(btnVerR);
            gridControl1.RepositoryItems.Add(repoVerR);

            gvLista.OptionsView.ShowGroupPanel = false;
            gvLista.OptionsView.ShowAutoFilterRow = true;
            gvLista.OptionsBehavior.Editable = false;

            gvLista.OptionsFind.AlwaysVisible = true;
            gvLista.OptionsFind.FindNullPrompt = "Buscar invalidación...";

            gvLista.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gvLista.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvLista.Appearance.FocusedRow.BackColor = Color.FromArgb(204, 229, 255);
            gvLista.Appearance.FocusedRow.Options.UseBackColor = true;
            gvLista.Appearance.HideSelectionRow.BackColor = Color.FromArgb(204, 229, 255);
            gvLista.Appearance.HideSelectionRow.Options.UseBackColor = true;
            gvLista.Appearance.Row.ForeColor = Color.Black;
            gvLista.Appearance.Row.Font = new Font("Segoe UI", 9f);
            gvLista.Appearance.Row.Options.UseFont = true;

            // ── ID (oculto) ───────────────────────────────────────
            colID.FieldName = "ID_INVALIDACIONJson";
            colID.Visible = false;

            // ── Botón Editar ──────────────────────────────────────
            colEditar.ColumnEdit = repoEditar;
            colEditar.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
            colEditar.VisibleIndex = 0;
            colEditar.Width = 41;
            colEditar.Caption = "";

            // ── Botón Ver Reporte (junto al Editar) ──────────────
            colVerR.ColumnEdit = repoVerR;
            colVerR.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
            colVerR.VisibleIndex = 1;
            colVerR.Width = 41;
            colVerR.Caption = "";

            // ── Fecha Anulacion ───────────────────────────────────
            colFechaAnulacion.FieldName = "fhProcesamiento_anulado";
            colFechaAnulacion.Caption = "Fecha Anulacion";
            colFechaAnulacion.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            colFechaAnulacion.DisplayFormat.FormatString = "dd/MM/yyyy";
            colFechaAnulacion.OptionsColumn.AllowEdit = false;
            colFechaAnulacion.Width = 120;
            colFechaAnulacion.VisibleIndex = 2;

            // ── Fecha Documento ───────────────────────────────────
            colFechaDoc.FieldName = "IDENTIFICACION_fecAnula";
            colFechaDoc.Caption = "Fecha Documento";
            colFechaDoc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            colFechaDoc.DisplayFormat.FormatString = "dd/MM/yyyy";
            colFechaDoc.OptionsColumn.AllowEdit = false;
            colFechaDoc.Width = 120;
            colFechaDoc.VisibleIndex = 3;

            // ── Hora Anulacion ────────────────────────────────────
            colHoraAnulacion.FieldName = "IDENTIFICACION_horAnula";
            colHoraAnulacion.Caption = "Hora Anulacion";
            colHoraAnulacion.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            colHoraAnulacion.DisplayFormat.FormatString = "hh:mm:ss tt";
            colHoraAnulacion.OptionsColumn.AllowEdit = false;
            colHoraAnulacion.Width = 100;
            colHoraAnulacion.VisibleIndex = 4;

            // ── Tipo DTE (oculto) ─────────────────────────────────
            colTipoDte.FieldName = "DOCUMENTO_tipoDte";
            colTipoDte.Visible = false;

            // ── Nombre DTE ────────────────────────────────────────
            colNombreDte.FieldName = "DOCUMENTO_tipoDte_NOMBRE";
            colNombreDte.Caption = "DTE";
            colNombreDte.OptionsColumn.AllowEdit = false;
            colNombreDte.Width = 160;
            colNombreDte.VisibleIndex = 5;

            // ── Código Generación (oculto) ────────────────────────
            colCodGen.FieldName = "DOCUMENTO_codigoGeneracion";
            colCodGen.Visible = false;

            // ── Sello Recibido (oculto) ───────────────────────────
            colSello.FieldName = "DOCUMENTO_selloRecibido";
            colSello.Visible = false;

            // ── Número Control ────────────────────────────────────
            colNumControl.FieldName = "DOCUMENTO_numeroControl";
            colNumControl.Caption = "N° Control";
            colNumControl.OptionsColumn.AllowEdit = false;
            colNumControl.Width = 220;
            colNumControl.VisibleIndex = 6;

            // ── Sello Anulado ─────────────────────────────────────
            colSelloAnulado.FieldName = "selloRecibido_anulado";
            colSelloAnulado.Caption = "Sello Anulado";
            colSelloAnulado.OptionsColumn.AllowEdit = false;
            colSelloAnulado.Width = 300;
            colSelloAnulado.VisibleIndex = 7;

            // ── Usuario (oculto) ──────────────────────────────────
            colUsuario.FieldName = "USUARIO_CREA";
            colUsuario.Visible = false;

            // ── Motivo Interno (oculto) ───────────────────────────
            colMotivo.FieldName = "MOTIVO_CONTROL_INTERNO";
            colMotivo.Visible = false;

            // ── Navigator ─────────────────────────────────────────
            gridControl1.UseEmbeddedNavigator = true;
            var nav = gridControl1.EmbeddedNavigator;
            nav.Buttons.Append.Visible = false;
            nav.Buttons.Remove.Visible = false;
            nav.Buttons.Edit.Visible = false;
            nav.Buttons.EndEdit.Visible = false;
            nav.Buttons.CancelEdit.Visible = false;
        }

        #endregion

        #region Carga de Datos

        private void CargarDatos()
        {
            _dtDatos = _dal.EjecutarConsulta("[EMANTENIMIENTO].[SP_INVALIDACIONJson]", new
            {
                ACCION = "LISTAR"
            });
            gridControl1.DataSource = _dtDatos;
            gridControl1.Refresh();
        }

        #endregion

        #region Eventos

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            AbrirFormulario(0);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gvLista_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column == null) return;

            int? id = ObtenerIdFila();
            if (!id.HasValue) return;

            if (e.Column == colEditar)
            {
                AbrirFormulario(id.Value);
            }
            else if (e.Column == colVerR)
            {
                // TODO: abrir reporte de invalidación
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Reporte",
                    "Información", System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Helpers

        private int? ObtenerIdFila()
        {
            if (gvLista.FocusedRowHandle < 0) return null;
            object val = gvLista.GetRowCellValue(gvLista.FocusedRowHandle, "ID_INVALIDACIONJson");
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }

        private void AbrirFormulario(int id)
        {
            var frm = new frmInvalidacion();
            frm.IdInvalidacion = id;
            frm.ShowDialog(this);
            frm.Dispose();
            CargarDatos(); // Refresca al volver
        }

        #endregion
    }
}