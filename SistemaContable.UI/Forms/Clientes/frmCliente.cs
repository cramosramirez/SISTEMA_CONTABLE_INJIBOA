using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System.Linq;
namespace SistemaContable.UI.Forms.Clientes
{
    public partial class frmCliente : Form
    {
        #region === CAMPOS PRIVADOS ===
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtRoles;
        private DataTable _dtRolesEntidad;
        private DataTable _dtAllMunicipios;
        private bool _cargando = false;
        // IDs seleccionados por búsqueda de actividad económica
        private int _idActividad1 = 0;
        private int _idActividad2 = 0;
        private int _idActividad3 = 0;
        #endregion
        public int IdEntidad { get; set; } = 0;
        public frmCliente()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        #region === CARGA INICIAL ===
        private void frmEntidad_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);
            CargarTipoPersona();
            CargarTipoContribuyente();
            CargarTipoDocIdentidad();
            CargarPaises();
            CargarDepartamentos();
            RegistrarBusquedaActividades();
            CargarOrigen();
            CargarRolesEntidad();
            InicializarGridRoles();
            if (IdEntidad == 0)
            {
                LimpiarFormulario();
                ConfigurarBotones(esNuevo: true);
                txtCODIGO_ENTIDAD.Focus();
            }
            else
            {
                CargarEntidadExistente(IdEntidad);
            }
        }
        #endregion
        #region === COMBOS ===
        private static void AgregarFilaVacia(DataTable dt, string displayMember = null)
        {
            foreach (DataColumn col in dt.Columns)
                col.AllowDBNull = true;
            DataRow fila = dt.NewRow();
            foreach (DataColumn col in dt.Columns)
            {
                try
                {
                    if (displayMember != null && col.ColumnName == displayMember)
                        fila[col] = "-- Seleccionar --";
                    else if (col.DataType == typeof(int) || col.DataType == typeof(long) ||
                             col.DataType == typeof(short) || col.DataType == typeof(byte) ||
                             col.DataType == typeof(decimal) || col.DataType == typeof(double) ||
                             col.DataType == typeof(float))
                        fila[col] = 0;
                    else if (col.DataType == typeof(bool))
                        fila[col] = false;
                    else
                        fila[col] = DBNull.Value;
                }
                catch { fila[col] = DBNull.Value; }
            }
            dt.Rows.InsertAt(fila, 0);
        }
        private void CargarTipoPersona()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_TIPO_PERSONA]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "NOMBRE");
            cbxTIPO_ENTIDAD.DataSource = dt;
            cbxTIPO_ENTIDAD.ValueMember = "ID_TIPO_PERSONA";
            cbxTIPO_ENTIDAD.DisplayMember = "NOMBRE";
            cbxTIPO_ENTIDAD.SelectedIndex = 0;
        }
        private void CargarTipoContribuyente()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_TIPO_CONTRIBUYENTE]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "NOMBRE");
            cbxTIPO_CONTRIB.DataSource = dt;
            cbxTIPO_CONTRIB.ValueMember = "ID_TIPO_CONTRIB";
            cbxTIPO_CONTRIB.DisplayMember = "NOMBRE";
            cbxTIPO_CONTRIB.SelectedIndex = 0;
        }
        private void CargarTipoDocIdentidad()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_TIPO_DOCUMENTO_IDENTIDAD]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "NOMBRE");
            cbxTIPO_DOC_IDEN.DataSource = dt;
            cbxTIPO_DOC_IDEN.ValueMember = "ID_TIPO_DOC_INDEN";
            cbxTIPO_DOC_IDEN.DisplayMember = "NOMBRE";
            cbxTIPO_DOC_IDEN.SelectedIndex = 0;
        }
        private void CargarPaises()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_PAIS]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "VALORES");
            cbxPAIS.DataSource = dt;
            cbxPAIS.ValueMember = "ID_PAIS";
            cbxPAIS.DisplayMember = "VALORES";
            cbxPAIS.SelectedIndex = 0;
        }
        private void CargarDepartamentos()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_DEPARTAMENTO]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "VALORES");
            cbxDEPTO.DataSource = dt;
            cbxDEPTO.ValueMember = "CODI_DEPTO";
            cbxDEPTO.DisplayMember = "VALORES";
            cbxDEPTO.SelectedIndex = 0;
        }
        private void CargarMunicipios(string codiDepto)
        {
            _dtAllMunicipios = _dal.EjecutarConsulta("[EMH].[SP_MUNICIPIO]",
                new { ACCION = "LISTAR", CODI_DEPTO = codiDepto });
            DataTable dtMuni = (_dtAllMunicipios != null && _dtAllMunicipios.Rows.Count > 0)
                ? _dtAllMunicipios.Copy()
                : new DataTable();
            if (!dtMuni.Columns.Contains("CODI_MUNI"))
                dtMuni.Columns.Add("CODI_MUNI", typeof(string));
            if (!dtMuni.Columns.Contains("NOMBRE_MUNICIPIO"))
                dtMuni.Columns.Add("NOMBRE_MUNICIPIO", typeof(string));
            AgregarFilaVacia(dtMuni, "NOMBRE_MUNICIPIO");
            cbxMUNI.DataSource = dtMuni;
            cbxMUNI.ValueMember = "CODI_MUNI";
            cbxMUNI.DisplayMember = "NOMBRE_MUNICIPIO";
            cbxMUNI.SelectedIndex = 0;
            cbxDIST.DataSource = null;
            cbxDIST.Items.Clear();
        }
        private void SetDistritoByMunicipio(string codiMuni)
        {
            if (_dtAllMunicipios == null || string.IsNullOrEmpty(codiMuni)) return;
            var fila = _dtAllMunicipios.AsEnumerable()
                .FirstOrDefault(r => r["CODI_MUNI"].ToString() == codiMuni);
            if (fila == null || fila["CODI_DIST"] == DBNull.Value)
            {
                cbxDIST.DataSource = null;
                cbxDIST.Items.Clear();
                return;
            }
            DataTable dtDist = new DataTable();
            dtDist.Columns.Add("CODI_DIST", typeof(string));
            dtDist.Columns.Add("NOMBRE_DISTRITO", typeof(string));
            DataRow nr = dtDist.NewRow();
            nr["CODI_DIST"] = fila["CODI_DIST"];
            nr["NOMBRE_DISTRITO"] = fila["NOMBRE_DISTRITO"] != DBNull.Value
                                    ? fila["NOMBRE_DISTRITO"]
                                    : string.Empty;
            dtDist.Rows.Add(nr);
            cbxDIST.DataSource = dtDist;
            cbxDIST.ValueMember = "CODI_DIST";
            cbxDIST.DisplayMember = "NOMBRE_DISTRITO";
            cbxDIST.SelectedIndex = 0;
        }
        private void RegistrarBusquedaActividades()
        {
            var config1 = new BusquedaConfig
            {
                StoredProcedure = "[EMH].[SP_ACTIVIDAD_ECONOMICA]",
                Accion = "BUSCAR",
                Columnas = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "ID_ACTIVIDAD", "Código" },
                    { "VALORES",      "Descripción" }
                },
                Anchos = new System.Collections.Generic.Dictionary<string, int>
                {
                    { "ID_ACTIVIDAD", 80 },
                    { "VALORES",     600 }
                }
            };
            FormHelper.RegistrarBusqueda(txtACTIVIDAD_1, config1, fila =>
            {
                _idActividad1 = Convert.ToInt32(fila["ID_ACTIVIDAD"]);
                txtACTIVIDAD_1.Text = fila["VALORES"].ToString();
            });
            var config2 = new BusquedaConfig
            {
                StoredProcedure = "[EMH].[SP_ACTIVIDAD_ECONOMICA]",
                Accion = "BUSCAR",
                Columnas = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "ID_ACTIVIDAD", "Código" },
                    { "VALORES",      "Descripción" }
                },
                Anchos = new System.Collections.Generic.Dictionary<string, int>
                {
                    { "ID_ACTIVIDAD", 80 },
                    { "VALORES",     600 }
                }
            };
            FormHelper.RegistrarBusqueda(txtACTIVIDAD_2, config2, fila =>
            {
                _idActividad2 = Convert.ToInt32(fila["ID_ACTIVIDAD"]);
                txtACTIVIDAD_2.Text = fila["VALORES"].ToString();
            });
            var config3 = new BusquedaConfig
            {
                StoredProcedure = "[EMH].[SP_ACTIVIDAD_ECONOMICA]",
                Accion = "BUSCAR",
                Columnas = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "ID_ACTIVIDAD", "Código" },
                    { "VALORES",      "Descripción" }
                },
                Anchos = new System.Collections.Generic.Dictionary<string, int>
                {
                    { "ID_ACTIVIDAD", 80 },
                    { "VALORES",     600 }
                }
            };
            FormHelper.RegistrarBusqueda(txtACTIVIDAD_3, config3, fila =>
            {
                _idActividad3 = Convert.ToInt32(fila["ID_ACTIVIDAD"]);
                txtACTIVIDAD_3.Text = fila["VALORES"].ToString();
            });
        }
        private string ObtenerDescripcionActividad(int idActividad)
        {
            if (idActividad <= 0) return "";
            try
            {
                DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_ACTIVIDAD_ECONOMICA]",
                    new { ACCION = "CONSULTAR", ID_ACTIVIDAD = idActividad });
                if (dt != null && dt.Rows.Count > 0)
                    return dt.Rows[0]["VALORES"]?.ToString() ?? "";
            }
            catch { }
            return "";
        }
        private void CargarOrigen()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_ORIGEN_ENTIDAD]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "NOMBRE_ORIGEN");
            cbxORIGEN.DataSource = dt;
            cbxORIGEN.ValueMember = "ID_ORIGEN";
            cbxORIGEN.DisplayMember = "NOMBRE_ORIGEN";
            cbxORIGEN.SelectedIndex = 0;
        }
        private void cbxDEPTO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando) return;
            string codiDepto = ObtenerCodigoCombo(cbxDEPTO);
            if (!string.IsNullOrEmpty(codiDepto))
                CargarMunicipios(codiDepto);
            else
            {
                cbxMUNI.DataSource = null;
                cbxMUNI.Items.Clear();
                cbxDIST.DataSource = null;
                cbxDIST.Items.Clear();
            }
        }
        private void cbxMUNI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando) return;
            string codiMuni = ObtenerCodigoCombo(cbxMUNI);
            SetDistritoByMunicipio(codiMuni);
        }
        #endregion
        #region === GRID ROLES ===
        private void CargarRolesEntidad()
        {
            _dtRolesEntidad = _dal.EjecutarConsulta("[EMH].[SP_TIPO_CLIENTE]",
                new { ACCION = "LISTAR" });
        }
        private void InicializarGridRoles()
        {
            _dtRoles = new DataTable();
            _dtRoles.Columns.Add("ID_ENTIDAD_TPC", typeof(int));
            _dtRoles.Columns.Add("ID_ENTIDAD", typeof(int));
            _dtRoles.Columns.Add("ID_TIPO_CLIENTE", typeof(int));
            _dtRoles.Columns.Add("ACTIVO", typeof(bool));
            _dtRoles.Columns.Add("FECHA_ASIGNACION", typeof(DateTime));
            gridRoles.DataSource = _dtRoles;
            var view = gridRoles.MainView as GridView;
            if (view == null) return;
            view.Columns.Clear();
            view.PopulateColumns();
            // Ocultar claves
            OcultarColumna(view, "ID_ENTIDAD_TPC");
            OcultarColumna(view, "ID_ENTIDAD");
            // Columna: TIPO CLIENTE (LookUpEdit)
            var colRol = view.Columns["ID_TIPO_CLIENTE"];
            if (colRol != null)
            {
                colRol.Caption = "Tipo Cliente";
                colRol.Width = 250;
                colRol.Visible = true;
                colRol.VisibleIndex = 0;
                colRol.OptionsColumn.AllowEdit = true;
                var repoRol = new RepositoryItemLookUpEdit();
                repoRol.DataSource = _dtRolesEntidad;
                repoRol.ValueMember = "ID_TIPO_CLIENTE";
                repoRol.DisplayMember = "NOMBRE_TIPO_CLIENTE";
                repoRol.NullText = "-- Seleccionar --";
                repoRol.ShowHeader = false;
                repoRol.ShowFooter = false;
                gridRoles.RepositoryItems.Add(repoRol);
                colRol.ColumnEdit = repoRol;
            }
            // Columna: Activo
            ConfigurarColumnaCheckBox(view, "ACTIVO", "Activo", 55);
            // Columna: Fecha asignación
            var colFecha = view.Columns["FECHA_ASIGNACION"];
            if (colFecha != null)
            {
                colFecha.Caption = "Fecha Asignación";
                colFecha.Width = 130;
                colFecha.Visible = true;
                colFecha.VisibleIndex = 2;
                colFecha.OptionsColumn.AllowEdit = true;
                colFecha.DisplayFormat.FormatString = "dd/MM/yyyy";
                colFecha.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                var repoFecha = new RepositoryItemDateEdit();
                repoFecha.DisplayFormat.FormatString = "dd/MM/yyyy";
                repoFecha.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridRoles.RepositoryItems.Add(repoFecha);
                colFecha.ColumnEdit = repoFecha;
            }
            // Columna: Eliminar
            var colEliminar = view.Columns.AddField("ELIMINAR");
            colEliminar.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            colEliminar.Caption = " ";
            colEliminar.Width = 36;
            colEliminar.Visible = true;
            colEliminar.VisibleIndex = 3;
            colEliminar.OptionsColumn.AllowEdit = true;
            colEliminar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            var repoEliminar = new RepositoryItemButtonEdit();
            repoEliminar.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            repoEliminar.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            repoEliminar.Buttons[0].ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.eliminarFila32x32;
            repoEliminar.Buttons[0].Caption = "";
            repoEliminar.Buttons[0].ToolTip = "Eliminar tipo de cliente";
            repoEliminar.ButtonClick += (s, ev) => EliminarRol();
            gridRoles.RepositoryItems.Add(repoEliminar);
            colEliminar.ColumnEdit = repoEliminar;
            // Apariencia
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsBehavior.Editable = true;
            view.OptionsBehavior.AutoSelectAllInEditor = true;
            view.OptionsNavigation.EnterMoveNextColumn = true;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.OptionsSelection.EnableAppearanceFocusedRow = true;
            view.Appearance.FocusedRow.BackColor = Color.FromArgb(204, 229, 255);
            view.Appearance.FocusedRow.Options.UseBackColor = true;
            view.Appearance.HideSelectionRow.BackColor = Color.FromArgb(204, 229, 255);
            view.Appearance.HideSelectionRow.Options.UseBackColor = true;
            view.Appearance.Row.ForeColor = Color.Black;
            view.Appearance.Row.Font = new Font("Segoe UI", 9f);
            view.Appearance.Row.Options.UseFont = true;
            view.Appearance.HeaderPanel.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            view.Appearance.HeaderPanel.Options.UseFont = true;
            // KeyDown: * + Enter abre búsqueda

        }
        private void ConfigurarColumnaCheckBox(GridView view, string field, string caption, int width)
        {
            var col = view.Columns.ColumnByFieldName(field);
            if (col == null) return;
            col.Caption = caption;
            col.Width = width;
            col.Visible = true;
            col.OptionsColumn.AllowEdit = true;
            var repo = new RepositoryItemCheckEdit();
            repo.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard;
            gridRoles.RepositoryItems.Add(repo);
            col.ColumnEdit = repo;
        }
        private void OcultarColumna(GridView view, string field)
        {
            var col = view.Columns.ColumnByFieldName(field);
            if (col != null) col.Visible = false;
        }
        private void CargarEntidadClienteExistente(int idEntidad)
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_ENTIDAD_CLIENTE]",
                new { ACCION = "CONSULTAR", ID_ENTIDAD = idEntidad });
            if (dt == null || dt.Rows.Count == 0) return;
            DataRow r = dt.Rows[0];
            txtDIAS_PLAZO.Text = r["DIAS_PLAZO"] == DBNull.Value ? "" : r["DIAS_PLAZO"].ToString();
            txtCUENTA_X_COBRAR.Text = r["CUENTA_X_COBRAR"] == DBNull.Value ? "" : r["CUENTA_X_COBRAR"].ToString();
        }
        private void CargarRolesExistentes(int idEntidad)
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_ENTIDAD_TIPO_CLIENTE]",
                new { ACCION = "LISTAR", ID_ENTIDAD = idEntidad });
            _dtRoles.Clear();
            if (dt == null) return;
            foreach (DataRow row in dt.Rows)
            {
                var f = _dtRoles.NewRow();
                f["ID_ENTIDAD_TPC"] = row["ID_ENTIDAD_TPC"];
                f["ID_ENTIDAD"] = row["ID_ENTIDAD"];
                f["ID_TIPO_CLIENTE"] = row["ID_TIPO_CLIENTE"] == DBNull.Value
                                     ? (object)DBNull.Value
                                     : Convert.ToInt32(row["ID_TIPO_CLIENTE"]);
                f["ACTIVO"] = row["ACTIVO"] == DBNull.Value
                                         ? true
                                         : Convert.ToBoolean(row["ACTIVO"]);
                f["FECHA_ASIGNACION"] = row["FECHA_ASIGNACION"] == DBNull.Value
                                         ? (object)DBNull.Value
                                         : Convert.ToDateTime(row["FECHA_ASIGNACION"]);
                _dtRoles.Rows.Add(f);
            }
        }
        private void AgregarRol()
        {
            var fila = _dtRoles.NewRow();
            fila["ID_ENTIDAD_TPC"] = 0;
            fila["ID_ENTIDAD"] = IdEntidad;
            fila["ID_TIPO_CLIENTE"] = DBNull.Value;
            fila["ACTIVO"] = true;
            fila["FECHA_ASIGNACION"] = DateTime.Today;
            _dtRoles.Rows.Add(fila);
        }
        private void EliminarRol()
        {
            var view = gridRoles.MainView as GridView;
            if (view == null) return;
            int fila = view.FocusedRowHandle;
            if (fila < 0) return;
            if (MessageBox.Show("¿Desea eliminar este tipo de cliente de la entidad?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            object valId = view.GetRowCellValue(fila, "ID_ENTIDAD_TPC");
            if (valId != null && valId != DBNull.Value)
            {
                int idRol = Convert.ToInt32(valId);
                if (idRol > 0)
                {
                    _dal.EjecutarSinRetorno("[EMH].[SP_ENTIDAD_TIPO_CLIENTE]", new
                    {
                        ACCION = "ELIMINAR",
                        ID_ENTIDAD_TPC = idRol
                    });
                }
            }
            view.DeleteRow(fila);
        }
        #endregion
        #region === CARGAR ENTIDAD EXISTENTE ===
        private void CargarEntidadExistente(int idEntidad)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_ENTIDAD]",
                    new { ACCION = "CONSULTAR", ID_ENTIDAD = idEntidad });
                if (dt == null || dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No se encontró la entidad solicitada.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    return;
                }
                DataRow r = dt.Rows[0];
                IdEntidad = Convert.ToInt32(r["ID_ENTIDAD"]);
                txtCODIGO_ENTIDAD.Text = AsString(r["CODIGO_ENTIDAD"]);
                txtNOMBRE.Text = AsString(r["NOMBRE"]);
                txtNOMBRE_COMERCIAL.Text = AsString(r["NOMBRE_COMERCIAL"]);
                txtNRC.Text = AsString(r["NRC"]);
                txtDUI.Text = AsString(r["DUI"]);
                txtNIT.Text = AsString(r["NIT"]);
                txtDOCUMENTO.Text = AsString(r["DOCUMENTO"]);
                txtCORREO.Text = AsString(r["CORREO"]);
                txtCORREO_CC.Text = AsString(r["CORREO_CC"]);
                txtCELULAR.Text = AsString(r["CELULAR"]);
                txtTELEFONO.Text = AsString(r["TELEFONO"]);
                txtDIAS_PLAZO.Text = r["DIAS_PLAZO"] == DBNull.Value ? "" : r["DIAS_PLAZO"].ToString();
                txtCOMPLEMENTO.Text = AsString(r["COMPLEMENTO"]);
                txtCALLE.Text = AsString(r["CALLE"]);
                txtCASA.Text = AsString(r["CASA"]);
                txtAPTO_LOCAL.Text = AsString(r["APTO_LOCAL"]);
                txtCOLONIA.Text = AsString(r["COLONIA"]);
                txtCODIPROVEEDOR.Text = AsString(r["CODIPROVEEDOR"]);
                txtCODTRANSPORT.Text = r["CODTRANSPORT"] == DBNull.Value ? "" : r["CODTRANSPORT"].ToString();
                txtID_CARGADORA.Text = r["ID_CARGADORA"] == DBNull.Value ? "" : r["ID_CARGADORA"].ToString();
                // Combos
                SetComboById(cbxTIPO_ENTIDAD, AsInt(r["ID_TIPO_ENTIDAD"]));
                SetComboById(cbxTIPO_CONTRIB, AsInt(r["ID_TIPO_CONTRIB"]));
                SetComboById(cbxTIPO_DOC_IDEN, AsInt(r["ID_TIPO_DOC_INDEN"]));
                SetComboById(cbxPAIS, AsInt(r["ID_PAIS"]));
                _idActividad1 = AsInt(r["ID_ACTIVIDAD_1"]) ?? 0;
                _idActividad2 = AsInt(r["ID_ACTIVIDAD_2"]) ?? 0;
                _idActividad3 = AsInt(r["ID_ACTIVIDAD_3"]) ?? 0;
                txtACTIVIDAD_1.Text = ObtenerDescripcionActividad(_idActividad1);
                txtACTIVIDAD_2.Text = ObtenerDescripcionActividad(_idActividad2);
                txtACTIVIDAD_3.Text = ObtenerDescripcionActividad(_idActividad3);
                SetComboById(cbxORIGEN, AsInt(r["ID_ORIGEN"]));
                // Departamento → Municipio → Distrito
                string codiDepto = AsString(r["CODI_DEPTO"]);
                string codiMuni = AsString(r["CODI_MUNI"]);
                if (!string.IsNullOrEmpty(codiDepto))
                {
                    _cargando = true;
                    cbxDEPTO.SelectedValue = codiDepto;
                    CargarMunicipios(codiDepto);
                    if (!string.IsNullOrEmpty(codiMuni))
                    {
                        cbxMUNI.SelectedValue = codiMuni;
                        SetDistritoByMunicipio(codiMuni);
                    }
                    _cargando = false;
                }
                CargarEntidadClienteExistente(idEntidad);
                CargarRolesExistentes(idEntidad);
                ConfigurarBotones(esNuevo: false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar la entidad:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion
        #region === GUARDAR ===
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;
            try
            {
                Cursor = Cursors.WaitCursor;
                var dtResult = _dal.EjecutarConsulta("[EMH].[SP_ENTIDAD]", new
                {
                    ACCION = "GUARDAR",
                    ID_ENTIDAD = IdEntidad,
                    CODIGO_ENTIDAD = NullIfEmpty(txtCODIGO_ENTIDAD.Text),
                    NOMBRE = txtNOMBRE.Text.Trim(),
                    NOMBRE_COMERCIAL = NullIfEmpty(txtNOMBRE_COMERCIAL.Text),
                    ID_TIPO_ENTIDAD = ObtenerIdCombo(cbxTIPO_ENTIDAD),
                    ID_TIPO_CONTRIB = ObtenerIdCombo(cbxTIPO_CONTRIB),
                    ID_TIPO_DOC_INDEN = ObtenerIdCombo(cbxTIPO_DOC_IDEN),
                    NRC = NullIfEmpty(txtNRC.Text),
                    DUI = NullIfEmpty(txtDUI.Text),
                    NIT = NullIfEmpty(txtNIT.Text),
                    DOCUMENTO = NullIfEmpty(txtDOCUMENTO.Text),
                    CORREO = NullIfEmpty(txtCORREO.Text),
                    CORREO_CC = NullIfEmpty(txtCORREO_CC.Text),
                    CELULAR = NullIfEmpty(txtCELULAR.Text),
                    TELEFONO = NullIfEmpty(txtTELEFONO.Text),
                    DIAS_PLAZO = ParseInt(txtDIAS_PLAZO.Text),
                    COMPLEMENTO = NullIfEmpty(txtCOMPLEMENTO.Text),
                    CALLE = NullIfEmpty(txtCALLE.Text),
                    CASA = NullIfEmpty(txtCASA.Text),
                    APTO_LOCAL = NullIfEmpty(txtAPTO_LOCAL.Text),
                    COLONIA = NullIfEmpty(txtCOLONIA.Text),
                    ID_PAIS = ObtenerIdCombo(cbxPAIS),
                    CODI_DEPTO = ObtenerCodigoCombo(cbxDEPTO),
                    CODI_MUNI = ObtenerCodigoCombo(cbxMUNI),
                    ID_ACTIVIDAD_1 = _idActividad1 > 0 ? _idActividad1 : (int?)null,
                    ID_ACTIVIDAD_2 = _idActividad2 > 0 ? _idActividad2 : (int?)null,
                    ID_ACTIVIDAD_3 = _idActividad3 > 0 ? _idActividad3 : (int?)null,
                    ID_ORIGEN = ObtenerIdCombo(cbxORIGEN),
                    CODIPROVEEDOR = NullIfEmpty(txtCODIPROVEEDOR.Text),
                    CODTRANSPORT = ParseIntNull(txtCODTRANSPORT.Text),
                    ID_CARGADORA = ParseIntNull(txtID_CARGADORA.Text),
                    USER = Configuracion.UsuarioActual
                });
                if (dtResult == null || dtResult.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Error al guardar la entidad.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                IdEntidad = Convert.ToInt32(dtResult.Rows[0]["ID_GENERADO"]);
                GuardarEntidadCliente();
                GuardarRoles();
                XtraMessageBox.Show("Entidad guardada correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConfigurarBotones(esNuevo: false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error al guardar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void GuardarEntidadCliente()
        {
            _dal.EjecutarSinRetorno("[EMH].[SP_ENTIDAD_CLIENTE]", new
            {
                ACCION = "GUARDAR",
                ID_ENTIDAD = IdEntidad,
                DIAS_PLAZO = ParseInt(txtDIAS_PLAZO.Text),
                CUENTA_X_COBRAR = NullIfEmpty(txtCUENTA_X_COBRAR.Text),
                USER = Configuracion.UsuarioActual
            });
        }
        private void GuardarRoles()
        {
            var view = gridRoles.MainView as GridView;
            view?.CloseEditor();
            view?.UpdateCurrentRow();
            foreach (DataRow fila in _dtRoles.Rows)
            {
                if (fila["ID_TIPO_CLIENTE"] == DBNull.Value) continue;
                _dal.EjecutarSinRetorno("[EMH].[SP_ENTIDAD_TIPO_CLIENTE]", new
                {
                    ACCION = "GUARDAR",
                    ID_ENTIDAD_TPC = fila["ID_ENTIDAD_TPC"] == DBNull.Value ? 0 : Convert.ToInt32(fila["ID_ENTIDAD_TPC"]),
                    ID_ENTIDAD = IdEntidad,
                    ID_TIPO_CLIENTE = Convert.ToInt32(fila["ID_TIPO_CLIENTE"]),
                    ACTIVO = fila["ACTIVO"] == DBNull.Value ? true : Convert.ToBoolean(fila["ACTIVO"]),
                    FECHA_ASIGNACION = fila["FECHA_ASIGNACION"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(fila["FECHA_ASIGNACION"]),
                    USUARIO = Configuracion.UsuarioActual
                });
            }
            CargarRolesExistentes(IdEntidad);
        }
        #endregion
        #region === ELIMINAR ENTIDAD ===
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (IdEntidad == 0)
            {
                LimpiarFormulario();
                return;
            }
            if (MessageBox.Show("¿Desea eliminar esta entidad?\nSe eliminarán también todos sus roles.",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            try
            {
                _dal.EjecutarSinRetorno("[EMH].[SP_ENTIDAD]", new
                {
                    ACCION = "ELIMINAR",
                    ID_ENTIDAD = IdEntidad
                });
                XtraMessageBox.Show("Entidad eliminada correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error al eliminar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region === REGLAS DE CONTROLES ===
        private void cbxORIGEN_SelectedIndexChanged(object sender, EventArgs e)
        {
            string origen = cbxORIGEN.Text.Trim().ToUpper();
            bool esExterior = origen == "EXTERIOR";
            cbxTIPO_DOC_IDEN.Enabled = esExterior;
            txtDOCUMENTO.Enabled = esExterior;
            if (!esExterior)
            {
                cbxTIPO_DOC_IDEN.SelectedIndex = 0;
                txtDOCUMENTO.Clear();
            }
        }
        private void cbxTIPO_CONTRIB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string contrib = cbxTIPO_CONTRIB.Text.Trim().ToUpper();
            bool esNoContribuyente = contrib.Contains("NO CONTRIBUYENTE");
            txtNRC.Enabled = !esNoContribuyente;
            if (esNoContribuyente)
                txtNRC.Clear();
        }
        #endregion
        #region === VALIDAR ===
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNOMBRE.Text))
            {
                XtraMessageBox.Show("El nombre de la entidad es obligatorio.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNOMBRE.Focus();
                return false;
            }
            // Validación por Tipo Persona
            string tipoPersona = cbxTIPO_ENTIDAD.Text.Trim().ToUpper();
            string tipoContrib = cbxTIPO_CONTRIB.Text.Trim().ToUpper();
            bool esNoContrib = tipoContrib.Contains("NO CONTRIBUYENTE");
            if (tipoPersona == "NATURAL")
            {
                if (string.IsNullOrWhiteSpace(txtDUI.Text))
                {
                    XtraMessageBox.Show("El DUI es obligatorio para personas naturales.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDUI.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtNIT.Text))
                {
                    XtraMessageBox.Show("El NIT es obligatorio para personas naturales.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNIT.Focus();
                    return false;
                }
            }
            else if (tipoPersona == "JURIDICA" && !esNoContrib)
            {
                if (string.IsNullOrWhiteSpace(txtNRC.Text))
                {
                    XtraMessageBox.Show("El NRC es obligatorio para personas jurídicas.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNRC.Focus();
                    return false;
                }
            }
            // Validación por Origen (EXTERIOR)
            string origen = cbxORIGEN.Text.Trim().ToUpper();
            if (origen == "EXTERIOR")
            {
                if (ObtenerIdCombo(cbxTIPO_DOC_IDEN) == null)
                {
                    XtraMessageBox.Show("El Tipo de Documento es obligatorio para origen EXTERIOR.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbxTIPO_DOC_IDEN.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtDOCUMENTO.Text))
                {
                    XtraMessageBox.Show("El Documento es obligatorio para origen EXTERIOR.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDOCUMENTO.Focus();
                    return false;
                }
            }
            // Validación por Tipo Contribuyente
            bool requiereNRC = !esNoContrib && (tipoContrib == "GRANDE" || tipoContrib == "MEDIANO" ||
                               tipoContrib.StartsWith("PEQUEÑO"));
            if (requiereNRC && string.IsNullOrWhiteSpace(txtNRC.Text))
            {
                XtraMessageBox.Show("El NRC es obligatorio para este tipo de contribuyente.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNRC.Focus();
                return false;
            }
            if (requiereNRC && _idActividad1 == 0)
            {
                XtraMessageBox.Show("Debe seleccionar al menos la Actividad Económica 1.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtACTIVIDAD_1.Focus();
                return false;
            }
            return true;
        }
        #endregion
        #region === LIMPIAR ===
        private void LimpiarFormulario()
        {
            IdEntidad = 0;
            txtCODIGO_ENTIDAD.Text = "";
            lblValidacionCodigo.Text = "";
            btnGuardar.Enabled = true;
            txtNOMBRE.Text = "";
            txtNOMBRE_COMERCIAL.Text = "";
            txtNRC.Text = "";
            txtDUI.Text = "";
            txtNIT.Text = "";
            txtDOCUMENTO.Text = "";
            txtCORREO.Text = "";
            txtCORREO_CC.Text = "";
            txtCELULAR.Text = "";
            txtTELEFONO.Text = "";
            txtDIAS_PLAZO.Text = "";
            txtCUENTA_X_COBRAR.Text = "";
            txtCOMPLEMENTO.Text = "";
            txtCALLE.Text = "";
            txtCASA.Text = "";
            txtAPTO_LOCAL.Text = "";
            txtCOLONIA.Text = "";
            txtCODIPROVEEDOR.Text = "";
            txtCODTRANSPORT.Text = "";
            txtID_CARGADORA.Text = "";
            cbxTIPO_ENTIDAD.SelectedIndex = 0;
            cbxTIPO_CONTRIB.SelectedIndex = 0;
            cbxTIPO_DOC_IDEN.SelectedIndex = 0;
            cbxPAIS.SelectedIndex = 0;
            cbxDEPTO.SelectedIndex = 0;
            cbxDIST.DataSource = null;
            cbxDIST.Items.Clear();
            cbxMUNI.DataSource = null;
            cbxMUNI.Items.Clear();
            _idActividad1 = 0;
            _idActividad2 = 0;
            _idActividad3 = 0;
            txtACTIVIDAD_1.Text = "";
            txtACTIVIDAD_2.Text = "";
            txtACTIVIDAD_3.Text = "";
            cbxORIGEN.SelectedIndex = 0;
            _dtRoles?.Clear();
        }
        #endregion
        #region === BOTONES ===
        private void ConfigurarBotones(bool esNuevo)
        {
            btnEliminar.Enabled = !esNuevo && IdEntidad > 0;
            btnAgregarRol.Enabled = !esNuevo && IdEntidad > 0;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            ConfigurarBotones(esNuevo: true);
            txtCODIGO_ENTIDAD.Focus();
        }
        private void txtCODIGO_ENTIDAD_Leave(object sender, EventArgs e)
        {
            ValidarCodigoExistencia();
        }
        private void btnAgregarRol_Click(object sender, EventArgs e)
        {
            AgregarRol();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #region === VALIDACIÓN CÓDIGO ===
        private void ValidarCodigoExistencia()
        {
            string codigo = txtCODIGO_ENTIDAD.Text.Trim();
            if (string.IsNullOrWhiteSpace(codigo))
            {
                lblValidacionCodigo.Text = "";
                btnGuardar.Enabled = true;
                return;
            }
            try
            {
                DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_ENTIDAD]", new
                {
                    ACCION = "BUSCAR",
                    FILTRO = codigo
                });
                bool existe = false;
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string codBD = row["CODIGO_ENTIDAD"]?.ToString()?.Trim() ?? "";
                        int idBD = row["ID_ENTIDAD"] != DBNull.Value ? Convert.ToInt32(row["ID_ENTIDAD"]) : 0;
                        if (string.Equals(codBD, codigo, StringComparison.OrdinalIgnoreCase)
                            && idBD != IdEntidad)
                        {
                            existe = true;
                            break;
                        }
                    }
                }
                if (existe)
                {
                    lblValidacionCodigo.Text = "✘";
                    lblValidacionCodigo.ForeColor = Color.Red;
                    lblValidacionCodigo.Tag = "DUPLICADO";
                    btnGuardar.Enabled = false;
                }
                else
                {
                    lblValidacionCodigo.Text = "✔";
                    lblValidacionCodigo.ForeColor = Color.Green;
                    lblValidacionCodigo.Tag = "OK";
                    btnGuardar.Enabled = true;
                }
            }
            catch
            {
                lblValidacionCodigo.Text = "";
            }
        }
        #endregion
        #region === HELPERS ===
        private static void SetComboById(System.Windows.Forms.ComboBox cbx, int? value)
        {
            if (value != null && value != 0)
                cbx.SelectedValue = value;
            else
                cbx.SelectedIndex = 0;
        }
        private static string AsString(object val)
            => val == null || val == DBNull.Value ? "" : val.ToString();
        private static int? AsInt(object val)
        {
            if (val == null || val == DBNull.Value) return null;
            int i = Convert.ToInt32(val);
            return i == 0 ? (int?)null : i;
        }
        private static decimal ParseDecimal(string texto)
            => decimal.TryParse(texto.Replace(",", ""), out decimal d) ? d : 0m;
        private static int? ParseInt(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return null;
            return int.TryParse(texto.Trim(), out int i) ? i : (int?)null;
        }
        private static int? ParseIntNull(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return null;
            return int.TryParse(texto.Trim(), out int i) ? i : (int?)null;
        }
        private static string NullIfEmpty(string texto)
            => string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
        private static string ObtenerCodigoCombo(System.Windows.Forms.ComboBox cbx)
        {
            if (cbx.SelectedValue == null || cbx.SelectedValue == DBNull.Value) return null;
            string val = cbx.SelectedValue.ToString();
            return string.IsNullOrWhiteSpace(val) ? null : val;
        }
        private static int? ObtenerIdCombo(System.Windows.Forms.ComboBox cbx)
        {
            if (cbx.SelectedValue == null || cbx.SelectedValue == DBNull.Value) return null;
            if (cbx.SelectedValue is DataRowView) return null;
            int val = Convert.ToInt32(cbx.SelectedValue);
            return val == 0 ? (int?)null : val;
        }
        #endregion
        private void lblTIPO_CONTRIB_Click(object sender, EventArgs e)
        {
        }
    }
}
