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
namespace SistemaContable.UI.Forms.Inventario
{
    public partial class frmProducto : Form
    {
        #region === CAMPOS PRIVADOS ===
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtRoles;
        private DataTable _dtRolesCatalogo;
        #endregion
        public int IdProducto { get; set; } = 0;
        public frmProducto()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        #region === CARGA INICIAL ===
        private void frmProducto_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);
            CargarCategorias();
            CargarPresentaciones();
            CargarTipoItem();
            CargarTributos();
            CargarUnidadMedida();
            CargarTipoOperacionVentas();
            CargarTipoIngresoVentas();
            InicializarGridRoles();
            if (IdProducto == 0)
            {
                LimpiarFormulario();
                ConfigurarBotones(esNuevo: true);
                txtCOD_REF.Focus();
            }
            else
            {
                CargarProductoExistente(IdProducto);
            }
        }
        #endregion
        #region === COMBOS ===
        // Inserta una fila vacía al inicio para que el combo arranque sin selección
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
        private void CargarCategorias()
        {
            DataTable dt = _dal.EjecutarConsulta("[EINVENTARIO].[SP_CATEGORIA]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "NOMBRE");
            cbxCATEGORIA.DataSource = dt;
            cbxCATEGORIA.ValueMember = "ID_CATEGORIA";
            cbxCATEGORIA.DisplayMember = "NOMBRE";
            cbxCATEGORIA.SelectedIndex = 0;
        }
        private void CargarSubCategorias(int idCategoria)
        {
            DataTable dt = _dal.EjecutarConsulta("[EINVENTARIO].[SP_SUB_CATEGORIA]",
                new { ACCION = "LISTAR", ID_CATEGORIA = idCategoria });
            AgregarFilaVacia(dt, "NOMBRE");
            cbxSUBCATEGORIA.DataSource = dt;
            cbxSUBCATEGORIA.ValueMember = "ID_SUBCATEGORIA";
            cbxSUBCATEGORIA.DisplayMember = "NOMBRE";
            cbxSUBCATEGORIA.SelectedIndex = 0;
        }
        private void CargarPresentaciones()
        {
            DataTable dt = _dal.EjecutarConsulta("[EINVENTARIO].[SP_PRESENTACION]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "NOMBRE");
            cbxPRESENTACION.DataSource = dt;
            cbxPRESENTACION.ValueMember = "ID_PRESENTACION";
            cbxPRESENTACION.DisplayMember = "NOMBRE";
            cbxPRESENTACION.SelectedIndex = 0;
        }
        private void CargarTipoItem()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_TIPO_DE_ITEM]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "VALORES");
            cbxTIPOITEM.DataSource = dt;
            cbxTIPOITEM.ValueMember = "CODIGO";
            cbxTIPOITEM.DisplayMember = "VALORES";
            cbxTIPOITEM.SelectedIndex = 0;
        }
        private void CargarTributos()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_TRIBUTOS_DET]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "VALORES");
            cbxCODTRIBUTO.DataSource = dt;
            cbxCODTRIBUTO.ValueMember = "CODIGO";
            cbxCODTRIBUTO.DisplayMember = "VALORES";
            cbxCODTRIBUTO.SelectedIndex = 0;
        }
        private void CargarUnidadMedida()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_UNIDAD_MEDIDA]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "VALORES");
            cbxUNIMEDIDA.DataSource = dt;
            cbxUNIMEDIDA.ValueMember = "CODIGO";
            cbxUNIMEDIDA.DisplayMember = "VALORES";
            cbxUNIMEDIDA.SelectedIndex = 0;
        }
        private void CargarTipoOperacionVentas()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_TIPO_OPERACION_VENTAS]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "NOMBRE");
            cbxTPOPERACION.DataSource = dt;
            cbxTPOPERACION.ValueMember = "ID_TPOPERACION_VENTAS";
            cbxTPOPERACION.DisplayMember = "NOMBRE";
            cbxTPOPERACION.SelectedIndex = 0;
        }
        private void CargarTipoIngresoVentas()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_TIPO_INGRESO_VENTAS]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "NOMBRE");
            cbxTPINGRESO.DataSource = dt;
            cbxTPINGRESO.ValueMember = "ID_TPINGRESO_VENTAS";
            cbxTPINGRESO.DisplayMember = "NOMBRE";
            cbxTPINGRESO.SelectedIndex = 0;
        }
        private void cbxCATEGORIA_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? idCategoria = ObtenerIdCombo(cbxCATEGORIA);
            if (idCategoria.HasValue && idCategoria.Value > 0)
                CargarSubCategorias(idCategoria.Value);
            else
            {
                cbxSUBCATEGORIA.DataSource = null;
                cbxSUBCATEGORIA.Items.Clear();
            }
        }
        #endregion
        #region === GRID ROLES ===
        private void InicializarGridRoles()
        {
            // Cargar catálogo de roles para el combo del grid
            _dtRolesCatalogo = _dal.EjecutarConsulta("[EINVENTARIO].[SP_ROL_PROD]",
                new { ACCION = "LISTAR" });
            _dtRoles = new DataTable();
            _dtRoles.Columns.Add("ID_PRODUCTO_ROL", typeof(int));
            _dtRoles.Columns.Add("ID_PRODUCTO", typeof(int));
            _dtRoles.Columns.Add("ID_ROL_PROD", typeof(int));
            _dtRoles.Columns.Add("NOMBRE_ROL_PROD", typeof(string));
            _dtRoles.Columns.Add("ACTIVO", typeof(bool));
            _dtRoles.Columns.Add("FECHA_ASIGNACION", typeof(DateTime));
            gridRoles.DataSource = _dtRoles;
            var view = gridRoles.MainView as GridView;
            if (view == null) return;
            view.Columns.Clear();
            view.PopulateColumns();
            // Ocultar columnas de clave
            OcultarColumna(view, "ID_PRODUCTO_ROL");
            OcultarColumna(view, "ID_PRODUCTO");
            OcultarColumna(view, "ID_ROL_PROD");
            // Columna: Rol (combo lookup)
            var colRol = view.Columns["NOMBRE_ROL_PROD"];
            if (colRol != null)
            {
                colRol.Caption = "Rol del Producto";
                colRol.Width = 250;
                colRol.Visible = true;
                colRol.VisibleIndex = 0;
                colRol.OptionsColumn.AllowEdit = true;
                var repoCombo = new RepositoryItemLookUpEdit();
                repoCombo.DataSource = _dtRolesCatalogo;
                repoCombo.ValueMember = "NOMBRE_ROL_PROD";
                repoCombo.DisplayMember = "NOMBRE_ROL_PROD";
                repoCombo.NullText = "-- Seleccione --";
                repoCombo.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NOMBRE_ROL_PROD", "Rol", 220));
                repoCombo.ShowHeader = false;
                repoCombo.EditValueChanged += (s, ev) =>
                {
                    // Sincronizar ID_ROL_PROD al seleccionar del combo
                    var editor = s as LookUpEdit;
                    if (editor == null) return;
                    string nombreRol = editor.EditValue?.ToString();
                    if (string.IsNullOrWhiteSpace(nombreRol)) return;
                    var filaRol = _dtRolesCatalogo.AsEnumerable()
                        .FirstOrDefault(r => r["NOMBRE_ROL_PROD"].ToString() == nombreRol);
                    if (filaRol == null) return;
                    view.SetFocusedRowCellValue("ID_ROL_PROD",
                        Convert.ToInt32(filaRol["ID_ROL_PROD"]));
                };
                gridRoles.RepositoryItems.Add(repoCombo);
                colRol.ColumnEdit = repoCombo;
            }
            // Columna: Activo
            ConfigurarColumnaCheckBox(view, "ACTIVO", "Activo", 55);
            // Columna: Fecha asignación
            var colFecha = view.Columns["FECHA_ASIGNACION"];
            if (colFecha != null)
            {
                colFecha.Caption = "Fecha Asignación";
                colFecha.Width = 120;
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
            repoEliminar.Buttons[0].ToolTip = "Eliminar rol";
            repoEliminar.ButtonClick += (s, ev) => EliminarRol();
            gridRoles.RepositoryItems.Add(repoEliminar);
            colEliminar.ColumnEdit = repoEliminar;
            // Opciones de vista
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
        private void CargarRolesExistentes(int idProducto)
        {
            DataTable dt = _dal.EjecutarConsulta("[EINVENTARIO].[SP_PRODUCTO_ROL]",
                new { ACCION = "LISTAR", ID_PRODUCTO = idProducto });
            _dtRoles.Clear();
            if (dt == null) return;
            foreach (DataRow row in dt.Rows)
            {
                var f = _dtRoles.NewRow();
                f["ID_PRODUCTO_ROL"] = row["ID_PRODUCTO_ROL"];
                f["ID_PRODUCTO"] = row["ID_PRODUCTO"];
                f["ID_ROL_PROD"] = row["ID_ROL_PROD"];
                f["NOMBRE_ROL_PROD"] = row["NOMBRE_ROL_PROD"];
                f["ACTIVO"] = row["ACTIVO"];
                f["FECHA_ASIGNACION"] = row["FECHA_ASIGNACION"] == DBNull.Value
                                        ? (object)DBNull.Value
                                        : Convert.ToDateTime(row["FECHA_ASIGNACION"]);
                _dtRoles.Rows.Add(f);
            }
        }
        private void AgregarRol()
        {
            var fila = _dtRoles.NewRow();
            fila["ID_PRODUCTO_ROL"] = 0;
            fila["ID_PRODUCTO"] = IdProducto;
            fila["ID_ROL_PROD"] = 0;
            fila["NOMBRE_ROL_PROD"] = "";
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
            if (MessageBox.Show("¿Desea eliminar este rol del producto?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            // Si ya está guardado en BD, eliminarlo
            object valId = view.GetRowCellValue(fila, "ID_PRODUCTO_ROL");
            if (valId != null && valId != DBNull.Value)
            {
                int idRol = Convert.ToInt32(valId);
                if (idRol > 0)
                {
                    _dal.EjecutarSinRetorno("[EINVENTARIO].[SP_PRODUCTO_ROL]", new
                    {
                        ACCION = "ELIMINAR",
                        ID_PRODUCTO_ROL = idRol
                    });
                }
            }
            view.DeleteRow(fila);
        }
        #endregion
        #region === CARGAR PRODUCTO EXISTENTE ===
        private void CargarProductoExistente(int idProducto)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DataTable dt = _dal.EjecutarConsulta("[EINVENTARIO].[SP_PRODUCTO]",
                    new { ACCION = "CONSULTAR", ID_PRODUCTO = idProducto });
                if (dt == null || dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No se encontró el producto solicitado.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    return;
                }
                DataRow r = dt.Rows[0];
                IdProducto = Convert.ToInt32(r["ID_PRODUCTO"]);
                txtCOD_REF.Text = AsString(r["COD_REF"]);
                txtDESCRIPCION.Text = AsString(r["DESCRIPCION"]);
                txtCCT_INVENT.Text = AsString(r["CCT_INVENT"]);
                // Combos — primero categoría (dispara carga de subcategorías)
                cbxCATEGORIA.SelectedValue = AsInt(r["ID_CATEGORIA"]);
                cbxSUBCATEGORIA.SelectedValue = AsInt(r["ID_SUBCATEGORIA"]);
                cbxPRESENTACION.SelectedValue = AsInt(r["ID_PRESENTACION"]);
                cbxTIPOITEM.SelectedValue = AsString(r["TIPOITEM"]);
                cbxCODTRIBUTO.SelectedValue = AsString(r["CODTRIBUTO"]);
                cbxUNIMEDIDA.SelectedValue = AsString(r["UNIMEDIDA"]);
                cbxTPOPERACION.SelectedValue = AsInt(r["ID_TPOPERACION_VENTAS"]);
                cbxTPINGRESO.SelectedValue = AsInt(r["ID_TPINGRESO_VENTAS"]);
                chkES_EXENTO.Checked = r["ES_EXENTO"] != DBNull.Value && Convert.ToBoolean(r["ES_EXENTO"]);
                chkES_NOSUJETA.Checked = r["ES_NOSUJETA"] != DBNull.Value && Convert.ToBoolean(r["ES_NOSUJETA"]);
                chkES_INVENTARIO.Checked = r["ES_INVENTARIO"] != DBNull.Value && Convert.ToBoolean(r["ES_INVENTARIO"]);
                txtPRECIO.Text = ToDecimal(r["PRECIO"]).ToString("N4");
                txtDESC_VENTA.Text = ToDecimal(r["DESC_VENTA"]).ToString("N4");
                txtULTIMOPRECIOCOMPRA.Text = ToDecimal(r["ULTIMOPRECIOCOMPRA"]).ToString("N6");
                chkESTADO.Checked = AsString(r["ESTADO"]) == "ACT";
                CargarRolesExistentes(idProducto);
                ConfigurarBotones(esNuevo: false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar el producto:\n\n" + ex.Message,
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
                var dtResult = _dal.EjecutarConsulta("[EINVENTARIO].[SP_PRODUCTO]", new
                {
                    ACCION = "GUARDAR",
                    ID_PRODUCTO = IdProducto,
                    COD_REF = txtCOD_REF.Text.Trim(),
                    DESCRIPCION = txtDESCRIPCION.Text.Trim(),
                    CCT_INVENT = NullIfEmpty(txtCCT_INVENT.Text),
                    ID_CATEGORIA = ObtenerIdCombo(cbxCATEGORIA),
                    ID_SUBCATEGORIA = ObtenerIdCombo(cbxSUBCATEGORIA),
                    ID_PRESENTACION = ObtenerIdCombo(cbxPRESENTACION),
                    TIPOITEM = ObtenerCodigoCombo(cbxTIPOITEM),
                    CODTRIBUTO = ObtenerCodigoCombo(cbxCODTRIBUTO),
                    UNIMEDIDA = ObtenerCodigoCombo(cbxUNIMEDIDA),
                    ID_TPOPERACION_VENTAS = ObtenerIdCombo(cbxTPOPERACION),
                    ID_TPINGRESO_VENTAS = ObtenerIdCombo(cbxTPINGRESO),
                    ES_EXENTO = chkES_EXENTO.Checked,
                    ES_NOSUJETA = chkES_NOSUJETA.Checked,
                    ES_INVENTARIO = chkES_INVENTARIO.Checked,
                    PRECIO = ParseDecimal(txtPRECIO.Text),
                    DESC_VENTA = ParseDecimal(txtDESC_VENTA.Text),
                    ULTIMOPRECIOCOMPRA = ParseDecimal(txtULTIMOPRECIOCOMPRA.Text),
                    ESTADO = chkESTADO.Checked ? "ACT" : "INA",
                    USER = Configuracion.UsuarioActual
                });
                if (dtResult == null || dtResult.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Error al guardar el producto.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                IdProducto = Convert.ToInt32(dtResult.Rows[0]["ID_GENERADO"]);
                // Guardar roles del grid
                GuardarRoles();
                XtraMessageBox.Show("Producto guardado correctamente.",
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
        private void GuardarRoles()
        {
            var view = gridRoles.MainView as GridView;
            view?.CloseEditor();
            view?.UpdateCurrentRow();
            foreach (DataRow fila in _dtRoles.Rows)
            {
                int idRolProd = fila["ID_ROL_PROD"] == DBNull.Value ? 0 : Convert.ToInt32(fila["ID_ROL_PROD"]);
                if (idRolProd <= 0) continue; // fila sin rol seleccionado
                _dal.EjecutarSinRetorno("[EINVENTARIO].[SP_PRODUCTO_ROL]", new
                {
                    ACCION = "GUARDAR",
                    ID_PRODUCTO_ROL = fila["ID_PRODUCTO_ROL"] == DBNull.Value ? 0 : Convert.ToInt32(fila["ID_PRODUCTO_ROL"]),
                    ID_PRODUCTO = IdProducto,
                    ID_ROL_PROD = idRolProd,
                    ACTIVO = fila["ACTIVO"] == DBNull.Value ? true : Convert.ToBoolean(fila["ACTIVO"]),
                    FECHA_ASIGNACION = fila["FECHA_ASIGNACION"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(fila["FECHA_ASIGNACION"]),
                    USUARIO = Configuracion.UsuarioActual
                });
            }
            // Recargar roles desde BD para reflejar IDs generados
            CargarRolesExistentes(IdProducto);
        }
        #endregion
        #region === ELIMINAR PRODUCTO ===
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (IdProducto == 0)
            {
                LimpiarFormulario();
                return;
            }
            if (MessageBox.Show("¿Desea eliminar este producto?\nSe eliminarán también todos sus roles.",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            try
            {
                _dal.EjecutarSinRetorno("[EINVENTARIO].[SP_PRODUCTO]", new
                {
                    ACCION = "ELIMINAR",
                    ID_PRODUCTO = IdProducto
                });
                XtraMessageBox.Show("Producto eliminado correctamente.",
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
        #region === VALIDAR ===
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtCOD_REF.Text))
            {
                XtraMessageBox.Show("El código de referencia es obligatorio.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCOD_REF.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDESCRIPCION.Text))
            {
                XtraMessageBox.Show("La descripción es obligatoria.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDESCRIPCION.Focus();
                return false;
            }
            if (cbxCATEGORIA.SelectedValue == null)
            {
                XtraMessageBox.Show("Seleccione una categoría.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbxSUBCATEGORIA.SelectedValue == null)
            {
                XtraMessageBox.Show("Seleccione una subcategoría.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbxPRESENTACION.SelectedValue == null)
            {
                XtraMessageBox.Show("Seleccione una presentación.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        #endregion
        #region === LIMPIAR ===
        private void LimpiarFormulario()
        {
            IdProducto = 0;
            txtCOD_REF.Text = "";
            lblValidacionCodigo.Text = "";
            btnGuardar.Enabled = true;
            txtDESCRIPCION.Text = "";
            txtCCT_INVENT.Text = "";
            txtPRECIO.Text = "0.0000";
            txtDESC_VENTA.Text = "0.0000";
            txtULTIMOPRECIOCOMPRA.Text = "0.000000";
            chkES_EXENTO.Checked = false;
            chkES_NOSUJETA.Checked = false;
            chkES_INVENTARIO.Checked = false;
            cbxCATEGORIA.SelectedIndex = 0;
            cbxSUBCATEGORIA.DataSource = null;
            cbxSUBCATEGORIA.Items.Clear();
            cbxPRESENTACION.SelectedIndex = 0;
            cbxTIPOITEM.SelectedIndex = 0;
            cbxCODTRIBUTO.SelectedIndex = 0;
            cbxUNIMEDIDA.SelectedIndex = 0;
            cbxTPOPERACION.SelectedIndex = 0;
            cbxTPINGRESO.SelectedIndex = 0;
            chkESTADO.Checked = true;
            _dtRoles?.Clear();
        }
        #endregion
        #region === BOTONES ===
        private void ConfigurarBotones(bool esNuevo)
        {
            btnEliminar.Enabled = !esNuevo && IdProducto > 0;
            btnAgregarRol.Enabled = !esNuevo && IdProducto > 0;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            ConfigurarBotones(esNuevo: true);
            txtCOD_REF.Focus();
        }
        private void btnAgregarRol_Click(object sender, EventArgs e)
        {
            AgregarRol();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtCOD_REF_Leave(object sender, EventArgs e)
        {
            ValidarCodigoExistencia();
        }
        #endregion
        #region === VALIDACIÓN CÓDIGO ===
        private void ValidarCodigoExistencia()
        {
            string codigo = txtCOD_REF.Text.Trim();
            if (string.IsNullOrWhiteSpace(codigo))
            {
                lblValidacionCodigo.Text = "";
                return;
            }
            try
            {
                DataTable dt = _dal.EjecutarConsulta("[EINVENTARIO].[SP_PRODUCTO]", new
                {
                    ACCION = "BUSCAR",
                    FILTRO = codigo
                });
                // Buscar coincidencia exacta excluyendo el producto actual (en modo edición)
                bool existe = false;
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string codBD = row["COD_REF"]?.ToString()?.Trim() ?? "";
                        int idBD = row["ID_PRODUCTO"] != DBNull.Value ? Convert.ToInt32(row["ID_PRODUCTO"]) : 0;
                        if (string.Equals(codBD, codigo, StringComparison.OrdinalIgnoreCase)
                            && idBD != IdProducto)
                        {
                            existe = true;
                            break;
                        }
                    }
                }
                if (existe)
                {
                    lblValidacionCodigo.Text = "✘";
                    lblValidacionCodigo.ForeColor = System.Drawing.Color.Red;
                    lblValidacionCodigo.Tag = "DUPLICADO";
                    btnGuardar.Enabled = false;
                }
                else
                {
                    lblValidacionCodigo.Text = "✔";
                    lblValidacionCodigo.ForeColor = System.Drawing.Color.Green;
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
        private static string AsString(object val)
            => val == null || val == DBNull.Value ? "" : val.ToString();
        private static decimal ToDecimal(object val)
            => val == null || val == DBNull.Value ? 0m : Convert.ToDecimal(val);
        private static decimal ParseDecimal(string texto)
            => decimal.TryParse(texto.Replace(",", ""), out decimal d) ? d : 0m;
        private static string NullIfEmpty(string texto)
            => string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
        private static int? AsInt(object val)
        {
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }
        private static string ObtenerCodigoCombo(System.Windows.Forms.ComboBox cbx)
        {
            if (cbx.SelectedValue == null || cbx.SelectedValue == DBNull.Value) return null;
            string val = cbx.SelectedValue.ToString();
            return string.IsNullOrWhiteSpace(val) ? null : val;
        }
        private int? ObtenerIdCombo(System.Windows.Forms.ComboBox cbx)
        {
            if (cbx.SelectedValue == null || cbx.SelectedValue == DBNull.Value) return null;
            if (cbx.SelectedValue is DataRowView) return null;
            int val = Convert.ToInt32(cbx.SelectedValue);
            return val == 0 ? (int?)null : val;
        }
        #endregion
    }
}
