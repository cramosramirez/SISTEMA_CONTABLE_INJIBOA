using SistemaContable.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;

namespace SistemaContable.UI.Forms.Seguridad
{
    public partial class frmConsultaRol : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtRoles;

        public frmConsultaRol()
        {
            InitializeComponent();
        }

        #region === CARGA INICIAL ===
        private void frmConsultaRol_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarDatos();
        }
        #endregion

        #region === CONFIGURACIÓN DEL GRID ===
        private void ConfigurarGrid()
        {
            gridControl1.ForceInitialize();
            gvRoles.OptionsView.ShowGroupPanel = false;
            gvRoles.OptionsView.ShowAutoFilterRow = true;
            gvRoles.OptionsBehavior.AutoExpandAllGroups = false;
            gvRoles.OptionsBehavior.Editable = true;
            gvRoles.OptionsFind.AlwaysVisible = true;
            gvRoles.OptionsFind.FindNullPrompt = "Buscar rol...";
            gvRoles.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gvRoles.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvRoles.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvRoles.Appearance.FocusedRow.BackColor = Color.FromArgb(204, 229, 255);
            gvRoles.Appearance.FocusedRow.Options.UseBackColor = true;
            gvRoles.Appearance.HideSelectionRow.BackColor = Color.FromArgb(204, 229, 255);
            gvRoles.Appearance.HideSelectionRow.Options.UseBackColor = true;
            gvRoles.Appearance.Row.ForeColor = Color.Black;
            gvRoles.Appearance.Row.Font = new Font("Segoe UI", 9f);
            gvRoles.Appearance.Row.Options.UseFont = true;

            // Columna oculta (clave)
            colID_ROL.FieldName = "ID_ROL";
            colID_ROL.Visible = false;

            // Botón Editar
            colEDITAR.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
            colEDITAR.Visible = true;
            colEDITAR.VisibleIndex = 0;
            colEDITAR.Width = 41;

            // Nombre
            colNOMBRE_ROL.FieldName = "NOMBRE_ROL";
            colNOMBRE_ROL.Caption = "Nombre";
            colNOMBRE_ROL.OptionsColumn.AllowEdit = false;
            colNOMBRE_ROL.Width = 350;
            colNOMBRE_ROL.VisibleIndex = 1;
            colNOMBRE_ROL.Visible = true;

            // Total Usuarios
            colTOTAL_USUARIOS.FieldName = "TOTAL_USUARIOS";
            colTOTAL_USUARIOS.Caption = "Usuarios";
            colTOTAL_USUARIOS.OptionsColumn.AllowEdit = false;
            colTOTAL_USUARIOS.Width = 90;
            colTOTAL_USUARIOS.VisibleIndex = 2;
            colTOTAL_USUARIOS.Visible = true;
            colTOTAL_USUARIOS.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colTOTAL_USUARIOS.AppearanceCell.Options.UseTextOptions = true;

            // Total Opciones
            colTOTAL_OPCIONES.FieldName = "TOTAL_OPCIONES";
            colTOTAL_OPCIONES.Caption = "Opciones";
            colTOTAL_OPCIONES.OptionsColumn.AllowEdit = false;
            colTOTAL_OPCIONES.Width = 90;
            colTOTAL_OPCIONES.VisibleIndex = 3;
            colTOTAL_OPCIONES.Visible = true;
            colTOTAL_OPCIONES.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colTOTAL_OPCIONES.AppearanceCell.Options.UseTextOptions = true;

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
            // SP_ROL.BUSCAR trae TOP 20 registros; con @FILTRO = null trae los
            // primeros 20 roles ordenados por nombre (la tabla ROL suele ser corta).
            _dtRoles = _dal.EjecutarConsulta("[ESEGURIDAD].[SP_ROL]", new
            {
                ACCION = "BUSCAR",
                FILTRO = (string)null
            });
            gridControl1.DataSource = _dtRoles;
        }
        #endregion

        #region === HELPERS ===
        private void AbrirRol(int idRol)
        {
            using (var frm = new frmRol())
            {
                frm.IdRol = idRol;
                frm.ShowDialog(this);
            }
            CargarDatos();
        }
        #endregion

        #region === EVENTOS ===
        private void riEditar_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            int rowHandle = gvRoles.FocusedRowHandle;
            if (rowHandle < 0) return;
            object val = gvRoles.GetRowCellValue(rowHandle, colID_ROL);
            if (val == null || val == DBNull.Value) return;
            int id = Convert.ToInt32(val);
            if (id > 0) AbrirRol(id);
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            AbrirRol(idRol: 0);
        }
        #endregion
    }
}
