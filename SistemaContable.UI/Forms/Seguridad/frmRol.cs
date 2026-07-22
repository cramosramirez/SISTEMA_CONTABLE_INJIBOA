using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;

namespace SistemaContable.UI.Forms.Seguridad
{
    public partial class frmRol : Form
    {
        #region === CAMPOS PRIVADOS ===
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtTipoCliente;
        #endregion

        public int IdRol { get; set; } = 0;

        public frmRol()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        #region === CARGA INICIAL ===
        private void frmRol_Load(object sender, EventArgs e)
        {
            // No se usa FormHelper.Inicializar(this) porque también aplica
            // mayúsculas forzadas (AplicarMayusculas) a todos los campos de texto.
            // Aquí solo se quiere el atajo Enter=Tab, dejando el texto libre.
            FormHelper.AplicarEnterComoTab(this);
            InicializarGridTipoCliente();
            if (IdRol == 0)
            {
                LimpiarFormulario();
                ConfigurarBotones(esNuevo: true);
                txtNOMBRE_ROL.Focus();
            }
            else
            {
                CargarRolExistente(IdRol);
            }
        }
        #endregion

        #region === CARGAR ROL EXISTENTE ===
        private void CargarRolExistente(int idRol)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                // SP_ROL.OBTENER devuelve dos resultsets (datos del rol y opciones
                // asignadas); aquí solo se usa el primero (mantenimiento básico).
                DataTable dt = _dal.EjecutarConsulta("[ESEGURIDAD].[SP_ROL]",
                    new { ACCION = "OBTENER", ID_ROL = idRol });
                if (dt == null || dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No se encontró el rol solicitado.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    return;
                }
                DataRow r = dt.Rows[0];
                IdRol = Convert.ToInt32(r["ID_ROL"]);
                txtNOMBRE_ROL.Text = r["NOMBRE_ROL"] == DBNull.Value ? "" : r["NOMBRE_ROL"].ToString();
                CargarTipoClienteExistentes(IdRol);
                ConfigurarBotones(esNuevo: false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar el rol:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion

        #region === GRID TIPO CLIENTE ===
        private void InicializarGridTipoCliente()
        {
            _dtTipoCliente = new DataTable();
            _dtTipoCliente.Columns.Add("ID_ROL_TPC", typeof(int));
            _dtTipoCliente.Columns.Add("ID_ROL", typeof(int));
            _dtTipoCliente.Columns.Add("ID_TIPO_CLIENTE", typeof(int));
            _dtTipoCliente.Columns.Add("NOMBRE_TIPO_CLIENTE", typeof(string));
            _dtTipoCliente.Columns.Add("ACTIVO", typeof(bool));
            _dtTipoCliente.Columns.Add("FECHA_ASIGNACION", typeof(DateTime));
            gridTipoCliente.DataSource = _dtTipoCliente;

            var view = gridTipoCliente.MainView as GridView;
            if (view == null) return;
            view.Columns.Clear();
            view.PopulateColumns();

            // Ocultar claves
            OcultarColumna(view, "ID_ROL_TPC");
            OcultarColumna(view, "ID_ROL");
            OcultarColumna(view, "ID_TIPO_CLIENTE");

            // Columna: Tipo Cliente (búsqueda genérica con "*")
            var colTipo = view.Columns["NOMBRE_TIPO_CLIENTE"];
            if (colTipo != null)
            {
                colTipo.Caption = "Tipo Cliente";
                colTipo.Width = 220;
                colTipo.Visible = true;
                colTipo.VisibleIndex = 0;
                colTipo.OptionsColumn.AllowEdit = true;
                var repoTexto = new RepositoryItemTextEdit();
                repoTexto.KeyDown += (s, ev) =>
                {
                    if (ev.KeyCode != Keys.Enter) return;
                    var editor = s as TextEdit;
                    if (editor == null) return;
                    if (editor.Text?.Trim() != "*") return;
                    ev.Handled = true;
                    AbrirBusquedaTipoClienteGrid(view);
                };
                gridTipoCliente.RepositoryItems.Add(repoTexto);
                colTipo.ColumnEdit = repoTexto;
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
                gridTipoCliente.RepositoryItems.Add(repoFecha);
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
            repoEliminar.ButtonClick += (s, ev) => EliminarTipoCliente();
            gridTipoCliente.RepositoryItems.Add(repoEliminar);
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
            view.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            view.Appearance.HeaderPanel.Options.UseFont = true;
        }

        /// <summary>
        /// Abre el formulario de búsqueda genérica de Tipo de Cliente para la celda
        /// enfocada del grid. Reutiliza [EMH].[SP_TIPO_CLIENTE] ACCION=LISTAR,
        /// igual que en frmCliente.
        /// </summary>
        private void AbrirBusquedaTipoClienteGrid(GridView view)
        {
            var config = new BusquedaConfig
            {
                StoredProcedure = "[EMH].[SP_TIPO_CLIENTE]",
                Accion = "LISTAR",
                Columnas = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "NOMBRE_TIPO_CLIENTE", "TIPO DE CLIENTE" }
                },
                Anchos = new System.Collections.Generic.Dictionary<string, int>
                {
                    { "NOMBRE_TIPO_CLIENTE", 300 }
                }
            };
            using (var frm = new frmBusquedaGenerica(config))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                if (frm.ShowDialog() == DialogResult.OK && frm.FilaSeleccionada != null)
                {
                    view.SetFocusedRowCellValue("ID_TIPO_CLIENTE",
                        Convert.ToInt32(frm.FilaSeleccionada["ID_TIPO_CLIENTE"]));
                    view.SetFocusedRowCellValue("NOMBRE_TIPO_CLIENTE", frm.FilaSeleccionada["NOMBRE_TIPO_CLIENTE"].ToString());
                }
                else
                {
                    view.SetFocusedRowCellValue("NOMBRE_TIPO_CLIENTE", "");
                }
            }
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
            gridTipoCliente.RepositoryItems.Add(repo);
            col.ColumnEdit = repo;
        }

        private void OcultarColumna(GridView view, string field)
        {
            var col = view.Columns.ColumnByFieldName(field);
            if (col != null) col.Visible = false;
        }

        private void CargarTipoClienteExistentes(int idRol)
        {
            DataTable dt = _dal.EjecutarConsulta("[ESEGURIDAD].[SP_ROL_TIPO_CLIENTE]",
                new { ACCION = "LISTAR", ID_ROL = idRol });
            _dtTipoCliente.Clear();
            if (dt == null) return;
            foreach (DataRow row in dt.Rows)
            {
                var f = _dtTipoCliente.NewRow();
                f["ID_ROL_TPC"] = row["ID_ROL_TPC"];
                f["ID_ROL"] = row["ID_ROL"];
                f["ID_TIPO_CLIENTE"] = row["ID_TIPO_CLIENTE"] == DBNull.Value
                                     ? (object)DBNull.Value
                                     : Convert.ToInt32(row["ID_TIPO_CLIENTE"]);
                f["NOMBRE_TIPO_CLIENTE"] = row["NOMBRE_TIPO_CLIENTE"] == DBNull.Value
                                     ? ""
                                     : row["NOMBRE_TIPO_CLIENTE"].ToString();
                f["ACTIVO"] = row["ACTIVO"] == DBNull.Value
                                         ? true
                                         : Convert.ToBoolean(row["ACTIVO"]);
                f["FECHA_ASIGNACION"] = row["FECHA_ASIGNACION"] == DBNull.Value
                                         ? (object)DBNull.Value
                                         : Convert.ToDateTime(row["FECHA_ASIGNACION"]);
                _dtTipoCliente.Rows.Add(f);
            }
        }

        private void AgregarTipoCliente()
        {
            var fila = _dtTipoCliente.NewRow();
            fila["ID_ROL_TPC"] = 0;
            fila["ID_ROL"] = IdRol;
            fila["ID_TIPO_CLIENTE"] = DBNull.Value;
            fila["NOMBRE_TIPO_CLIENTE"] = "";
            fila["ACTIVO"] = true;
            fila["FECHA_ASIGNACION"] = DateTime.Today;
            _dtTipoCliente.Rows.Add(fila);
        }

        private void EliminarTipoCliente()
        {
            var view = gridTipoCliente.MainView as GridView;
            if (view == null) return;
            int fila = view.FocusedRowHandle;
            if (fila < 0) return;
            if (MessageBox.Show("¿Desea eliminar este tipo de cliente del rol?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            object valId = view.GetRowCellValue(fila, "ID_ROL_TPC");
            if (valId != null && valId != DBNull.Value)
            {
                int idRolTpc = Convert.ToInt32(valId);
                if (idRolTpc > 0)
                {
                    _dal.EjecutarSinRetorno("[ESEGURIDAD].[SP_ROL_TIPO_CLIENTE]", new
                    {
                        ACCION = "ELIMINAR",
                        ID_ROL_TPC = idRolTpc
                    });
                }
            }
            view.DeleteRow(fila);
        }

        private void GuardarTipoCliente()
        {
            var view = gridTipoCliente.MainView as GridView;
            view?.CloseEditor();
            view?.UpdateCurrentRow();
            foreach (DataRow fila in _dtTipoCliente.Rows)
            {
                if (fila["ID_TIPO_CLIENTE"] == DBNull.Value) continue;
                _dal.EjecutarSinRetorno("[ESEGURIDAD].[SP_ROL_TIPO_CLIENTE]", new
                {
                    ACCION = "GUARDAR",
                    ID_ROL_TPC = fila["ID_ROL_TPC"] == DBNull.Value ? 0 : Convert.ToInt32(fila["ID_ROL_TPC"]),
                    ID_ROL = IdRol,
                    ID_TIPO_CLIENTE = Convert.ToInt32(fila["ID_TIPO_CLIENTE"]),
                    ACTIVO = fila["ACTIVO"] == DBNull.Value ? true : Convert.ToBoolean(fila["ACTIVO"]),
                    FECHA_ASIGNACION = fila["FECHA_ASIGNACION"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(fila["FECHA_ASIGNACION"]),
                    USUARIO = Configuracion.UsuarioActual
                });
            }
            CargarTipoClienteExistentes(IdRol);
        }

        private void btnAgregarTipoCliente_Click(object sender, EventArgs e)
        {
            AgregarTipoCliente();
        }
        #endregion

        #region === GUARDAR ===
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;
            try
            {
                Cursor = Cursors.WaitCursor;
                var dtResult = _dal.EjecutarConsulta("[ESEGURIDAD].[SP_ROL]", new
                {
                    ACCION = "GUARDAR",
                    ID_ROL = IdRol,
                    NOMBRE_ROL = txtNOMBRE_ROL.Text.Trim(),
                    USUARIO_CREA = Configuracion.UsuarioActual,
                    USUARIO_ACT = Configuracion.UsuarioActual
                });
                if (dtResult == null || dtResult.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Error al guardar el rol.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                IdRol = Convert.ToInt32(dtResult.Rows[0]["ID_GENERADO"]);
                GuardarTipoCliente();
                XtraMessageBox.Show("Rol guardado correctamente.",
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
        #endregion

        #region === ELIMINAR ===
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (IdRol == 0)
            {
                LimpiarFormulario();
                return;
            }
            if (MessageBox.Show("¿Desea eliminar este rol?",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            try
            {
                // SP_ROL.ELIMINAR lanza RAISERROR si el rol tiene usuarios activos asignados.
                _dal.EjecutarSinRetorno("[ESEGURIDAD].[SP_ROL]", new
                {
                    ACCION = "ELIMINAR",
                    ID_ROL = IdRol
                });
                XtraMessageBox.Show("Rol eliminado correctamente.",
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
            if (string.IsNullOrWhiteSpace(txtNOMBRE_ROL.Text))
            {
                XtraMessageBox.Show("El nombre del rol es obligatorio.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNOMBRE_ROL.Focus();
                return false;
            }
            return true;
        }
        #endregion

        #region === LIMPIAR ===
        private void LimpiarFormulario()
        {
            IdRol = 0;
            txtNOMBRE_ROL.Text = "";
            _dtTipoCliente?.Clear();
        }
        #endregion

        #region === BOTONES ===
        private void ConfigurarBotones(bool esNuevo)
        {
            btnEliminar.Enabled = !esNuevo && IdRol > 0;
            btnAgregarTipoCliente.Enabled = !esNuevo && IdRol > 0;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            ConfigurarBotones(esNuevo: true);
            txtNOMBRE_ROL.Focus();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
