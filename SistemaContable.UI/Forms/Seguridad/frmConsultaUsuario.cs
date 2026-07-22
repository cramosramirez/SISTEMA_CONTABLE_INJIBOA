using SistemaContable.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;

namespace SistemaContable.UI.Forms.Seguridad
{
    public partial class frmConsultaUsuario : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtUsuarios;

        public frmConsultaUsuario()
        {
            InitializeComponent();
        }

        #region === CARGA INICIAL ===
        private void frmConsultaUsuario_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarDatos();
        }
        #endregion

        #region === CONFIGURACIÓN DEL GRID ===
        private void ConfigurarGrid()
        {
            gridControl1.ForceInitialize();
            gvUsuarios.OptionsView.ShowGroupPanel = false;
            gvUsuarios.OptionsView.ShowAutoFilterRow = true;
            gvUsuarios.OptionsBehavior.AutoExpandAllGroups = false;
            gvUsuarios.OptionsBehavior.Editable = true;
            gvUsuarios.OptionsFind.AlwaysVisible = true;
            gvUsuarios.OptionsFind.FindNullPrompt = "Buscar usuario...";
            gvUsuarios.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gvUsuarios.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvUsuarios.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvUsuarios.Appearance.FocusedRow.BackColor = Color.FromArgb(204, 229, 255);
            gvUsuarios.Appearance.FocusedRow.Options.UseBackColor = true;
            gvUsuarios.Appearance.HideSelectionRow.BackColor = Color.FromArgb(204, 229, 255);
            gvUsuarios.Appearance.HideSelectionRow.Options.UseBackColor = true;
            gvUsuarios.Appearance.Row.ForeColor = Color.Black;
            gvUsuarios.Appearance.Row.Font = new Font("Segoe UI", 9f);
            gvUsuarios.Appearance.Row.Options.UseFont = true;

            // Botón Editar
            colEDITAR.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
            colEDITAR.Visible = true;
            colEDITAR.VisibleIndex = 0;
            colEDITAR.Width = 41;

            // Usuario (login = clave real)
            colUSUARIO.FieldName = "USUARIO";
            colUSUARIO.Caption = "Usuario";
            colUSUARIO.OptionsColumn.AllowEdit = false;
            colUSUARIO.Width = 130;
            colUSUARIO.VisibleIndex = 1;
            colUSUARIO.Visible = true;

            // Nombre
            colNOMBRE.FieldName = "NOMBRE";
            colNOMBRE.Caption = "Nombre";
            colNOMBRE.OptionsColumn.AllowEdit = false;
            colNOMBRE.Width = 220;
            colNOMBRE.VisibleIndex = 2;
            colNOMBRE.Visible = true;

            // Email
            colEMAIL.FieldName = "EMAIL";
            colEMAIL.Caption = "Email";
            colEMAIL.OptionsColumn.AllowEdit = false;
            colEMAIL.Width = 180;
            colEMAIL.VisibleIndex = 3;
            colEMAIL.Visible = true;

            // Rol
            colNOMBRE_ROL.FieldName = "NOMBRE_ROL";
            colNOMBRE_ROL.Caption = "Rol";
            colNOMBRE_ROL.OptionsColumn.AllowEdit = false;
            colNOMBRE_ROL.Width = 140;
            colNOMBRE_ROL.VisibleIndex = 4;
            colNOMBRE_ROL.Visible = true;

            // Activo
            colACTIVO.FieldName = "ACTIVO";
            colACTIVO.Caption = "Activo";
            colACTIVO.OptionsColumn.AllowEdit = false;
            colACTIVO.Width = 60;
            colACTIVO.VisibleIndex = 5;
            colACTIVO.Visible = true;
            colACTIVO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colACTIVO.AppearanceCell.Options.UseTextOptions = true;

            // Bloqueado
            colBLOQUEADO.FieldName = "BLOQUEADO";
            colBLOQUEADO.Caption = "Bloqueado";
            colBLOQUEADO.OptionsColumn.AllowEdit = false;
            colBLOQUEADO.Width = 75;
            colBLOQUEADO.VisibleIndex = 6;
            colBLOQUEADO.Visible = true;
            colBLOQUEADO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colBLOQUEADO.AppearanceCell.Options.UseTextOptions = true;

            // Último acceso
            colFECHA_ULTACCESO.FieldName = "FECHA_ULTACCESO";
            colFECHA_ULTACCESO.Caption = "Último Acceso";
            colFECHA_ULTACCESO.OptionsColumn.AllowEdit = false;
            colFECHA_ULTACCESO.Width = 130;
            colFECHA_ULTACCESO.VisibleIndex = 7;
            colFECHA_ULTACCESO.Visible = true;
            colFECHA_ULTACCESO.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            colFECHA_ULTACCESO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;

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
            // SP_USUARIO.BUSCAR trae TOP 20; con @FILTRO y @ACTIVO en null trae
            // los primeros 20 usuarios ordenados por nombre.
            _dtUsuarios = _dal.EjecutarConsulta("[dbo].[SP_USUARIO]", new
            {
                ACCION = "BUSCAR",
                FILTRO = (string)null,
                ACTIVO = (bool?)null
            });
            gridControl1.DataSource = _dtUsuarios;
        }
        #endregion

        #region === HELPERS ===
        private void AbrirUsuario(string usuario)
        {
            using (var frm = new frmUsuario())
            {
                frm.Usuario = usuario;
                frm.ShowDialog(this);
            }
            CargarDatos();
        }
        #endregion

        #region === EVENTOS ===
        private void riEditar_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            int rowHandle = gvUsuarios.FocusedRowHandle;
            if (rowHandle < 0) return;
            object val = gvUsuarios.GetRowCellValue(rowHandle, colUSUARIO);
            if (val == null || val == DBNull.Value) return;
            string usuario = val.ToString();
            if (!string.IsNullOrEmpty(usuario)) AbrirUsuario(usuario);
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            AbrirUsuario(usuario: null);
        }
        #endregion
    }
}
