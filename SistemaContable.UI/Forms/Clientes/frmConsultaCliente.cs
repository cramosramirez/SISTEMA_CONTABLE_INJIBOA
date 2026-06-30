using SistemaContable.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;

namespace SistemaContable.UI.Forms.Clientes
{
    public partial class frmConsultaCliente : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtEntidades;

        public frmConsultaCliente()
        {
            InitializeComponent();
        }

        #region === CARGA INICIAL ===
        private void frmConsultaCliente_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarDatos();
        }
        #endregion

        #region === CONFIGURACIÓN DEL GRID ===
        private void ConfigurarGrid()
        {
            gridControl1.ForceInitialize();
            gvEntidades.OptionsView.ShowGroupPanel = false;
            gvEntidades.OptionsView.ShowAutoFilterRow = true;
            gvEntidades.OptionsBehavior.AutoExpandAllGroups = false;
            gvEntidades.OptionsBehavior.Editable = true;
            gvEntidades.OptionsFind.AlwaysVisible = true;
            gvEntidades.OptionsFind.FindNullPrompt = "Buscar entidad...";
            gvEntidades.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gvEntidades.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvEntidades.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvEntidades.Appearance.FocusedRow.BackColor = Color.FromArgb(204, 229, 255);
            gvEntidades.Appearance.FocusedRow.Options.UseBackColor = true;
            gvEntidades.Appearance.HideSelectionRow.BackColor = Color.FromArgb(204, 229, 255);
            gvEntidades.Appearance.HideSelectionRow.Options.UseBackColor = true;
            gvEntidades.Appearance.Row.ForeColor = Color.Black;
            gvEntidades.Appearance.Row.Font = new Font("Segoe UI", 9f);
            gvEntidades.Appearance.Row.Options.UseFont = true;

            // Columna oculta (clave)
            colID_ENTIDAD.FieldName = "ID_ENTIDAD";
            colID_ENTIDAD.Visible = false;

            // Botón Editar
            colEDITAR.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
            colEDITAR.Visible = true;
            colEDITAR.VisibleIndex = 0;
            colEDITAR.Width = 41;

            // Código
            colCODIGO_ENTIDAD.FieldName = "CODIGO_ENTIDAD";
            colCODIGO_ENTIDAD.Caption = "Código";
            colCODIGO_ENTIDAD.OptionsColumn.AllowEdit = false;
            colCODIGO_ENTIDAD.Width = 110;
            colCODIGO_ENTIDAD.VisibleIndex = 1;
            colCODIGO_ENTIDAD.Visible = true;

            // Nombre
            colNOMBRE.FieldName = "NOMBRE";
            colNOMBRE.Caption = "Nombre";
            colNOMBRE.OptionsColumn.AllowEdit = false;
            colNOMBRE.Width = 300;
            colNOMBRE.VisibleIndex = 2;
            colNOMBRE.Visible = true;

            // Nombre Comercial
            colNOMBRE_COMERCIAL.FieldName = "NOMBRE_COMERCIAL";
            colNOMBRE_COMERCIAL.Caption = "Nombre Comercial";
            colNOMBRE_COMERCIAL.OptionsColumn.AllowEdit = false;
            colNOMBRE_COMERCIAL.Width = 200;
            colNOMBRE_COMERCIAL.VisibleIndex = 3;
            colNOMBRE_COMERCIAL.Visible = true;

            // Tipo Persona
            colTIPO_PERSONA.FieldName = "NOMBRE_TIPO_PERSONA";
            colTIPO_PERSONA.Caption = "Tipo Persona";
            colTIPO_PERSONA.OptionsColumn.AllowEdit = false;
            colTIPO_PERSONA.Width = 120;
            colTIPO_PERSONA.VisibleIndex = 4;
            colTIPO_PERSONA.Visible = true;

            // NRC
            colNRC.FieldName = "NRC";
            colNRC.Caption = "NRC";
            colNRC.OptionsColumn.AllowEdit = false;
            colNRC.Width = 90;
            colNRC.VisibleIndex = 5;
            colNRC.Visible = true;
            colNRC.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colNRC.AppearanceCell.Options.UseTextOptions = true;

            // NIT
            colNIT.FieldName = "NIT";
            colNIT.Caption = "NIT";
            colNIT.OptionsColumn.AllowEdit = false;
            colNIT.Width = 110;
            colNIT.VisibleIndex = 6;
            colNIT.Visible = true;

            // Correo
            colCORREO.FieldName = "CORREO";
            colCORREO.Caption = "Correo";
            colCORREO.OptionsColumn.AllowEdit = false;
            colCORREO.Width = 200;
            colCORREO.VisibleIndex = 7;
            colCORREO.Visible = true;

            // Teléfono
            colTELEFONO.FieldName = "TELEFONO";
            colTELEFONO.Caption = "Teléfono";
            colTELEFONO.OptionsColumn.AllowEdit = false;
            colTELEFONO.Width = 100;
            colTELEFONO.VisibleIndex = 8;
            colTELEFONO.Visible = true;

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
            _dtEntidades = _dal.EjecutarConsulta("[EMH].[SP_ENTIDAD]", new
            {
                ACCION = "LISTAR"
            });
            gridControl1.DataSource = _dtEntidades;
        }
        #endregion

        #region === HELPERS ===
        private void AbrirEntidad(int idEntidad)
        {
            using (var frm = new frmCliente())
            {
                frm.IdEntidad = idEntidad;
                frm.ShowDialog(this);
            }
            CargarDatos();
        }
        #endregion

        #region === EVENTOS ===
        private void riEditar_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            int rowHandle = gvEntidades.FocusedRowHandle;
            if (rowHandle < 0) return;
            object val = gvEntidades.GetRowCellValue(rowHandle, colID_ENTIDAD);
            if (val == null || val == DBNull.Value) return;
            int id = Convert.ToInt32(val);
            if (id > 0) AbrirEntidad(id);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            AbrirEntidad(idEntidad: 0);
        }
        #endregion
    }
}
