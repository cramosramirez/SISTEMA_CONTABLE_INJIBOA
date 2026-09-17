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
        // (2026-09-16) Nuevas asociaciones de Rol, mismo patrón que Tipo Cliente,
        // pedidas por Roberto: Rol <-> Centro de Costo y Rol <-> Rol de Producto.
        private DataTable _dtCentroCosto;
        private DataTable _dtRolProducto;
        #endregion

        public int IdRol { get; set; } = 0;

        public frmRol()
        {
            InitializeComponent();
            InicializarPestanaTipoProveedor();
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
            InicializarGridCentroCosto();
            InicializarGridRolProducto();
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
                CargarTipoProveedorExistentes(IdRol);
                CargarCentroCostoExistentes(IdRol);
                CargarRolProductoExistentes(IdRol);
                CargarPermisos(IdRol);
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
            ConfigurarColumnaCheckBox(gridTipoCliente, view, "ACTIVO", "Activo", 55);

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
            AplicarAparienciaGrid(view);
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

        private void ConfigurarColumnaCheckBox(DevExpress.XtraGrid.GridControl grid, GridView view, string field, string caption, int width)
        {
            var col = view.Columns.ColumnByFieldName(field);
            if (col == null) return;
            col.Caption = caption;
            col.Width = width;
            col.Visible = true;
            col.OptionsColumn.AllowEdit = true;
            var repo = new RepositoryItemCheckEdit();
            repo.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard;
            grid.RepositoryItems.Add(repo);
            col.ColumnEdit = repo;
        }

        private void OcultarColumna(GridView view, string field)
        {
            var col = view.Columns.ColumnByFieldName(field);
            if (col != null) col.Visible = false;
        }

        /// <summary>
        /// Apariencia común a los tres grids de asociación (Tipo Cliente, Centro de
        /// Costo, Rol de Producto) — antes vivía repetida dentro de cada
        /// Inicializar...(), factorizada aquí para no duplicar código al agregar
        /// las dos asociaciones nuevas.
        /// </summary>
        private void AplicarAparienciaGrid(GridView view)
        {
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

        #region === GRID CENTRO DE COSTO (2026-09-16) ===
        // Misma estructura que "GRID TIPO CLIENTE" de arriba, aplicada a la nueva
        // asociación Rol <-> Centro de Costo pedida por Roberto. SP y tabla nuevos
        // ([ESEGURIDAD].[SP_ROL_CENTROCOSTO] / [ESEGURIDAD].[ROL_CENTROCOSTO]),
        // independientes de [EDTE].[SP_CENTROCOSTO] (que ya usan frmFactura y
        // frmCreditoFiscal para su combo) para no tocar nada existente.
        private void InicializarGridCentroCosto()
        {
            _dtCentroCosto = new DataTable();
            _dtCentroCosto.Columns.Add("ID_ROL_CC", typeof(int));
            _dtCentroCosto.Columns.Add("ID_ROL", typeof(int));
            _dtCentroCosto.Columns.Add("ID_CENTRO", typeof(int));
            _dtCentroCosto.Columns.Add("NOMBRE_CENTRO", typeof(string));
            _dtCentroCosto.Columns.Add("ACTIVO", typeof(bool));
            _dtCentroCosto.Columns.Add("FECHA_ASIGNACION", typeof(DateTime));
            gridCentroCosto.DataSource = _dtCentroCosto;

            var view = gridCentroCosto.MainView as GridView;
            if (view == null) return;
            view.Columns.Clear();
            view.PopulateColumns();

            OcultarColumna(view, "ID_ROL_CC");
            OcultarColumna(view, "ID_ROL");
            OcultarColumna(view, "ID_CENTRO");

            var colCentro = view.Columns["NOMBRE_CENTRO"];
            if (colCentro != null)
            {
                colCentro.Caption = "Centro de Costo";
                colCentro.Width = 220;
                colCentro.Visible = true;
                colCentro.VisibleIndex = 0;
                colCentro.OptionsColumn.AllowEdit = true;
                var repoTexto = new RepositoryItemTextEdit();
                repoTexto.KeyDown += (s, ev) =>
                {
                    if (ev.KeyCode != Keys.Enter) return;
                    var editor = s as TextEdit;
                    if (editor == null) return;
                    if (editor.Text?.Trim() != "*") return;
                    ev.Handled = true;
                    AbrirBusquedaCentroCostoGrid(view);
                };
                gridCentroCosto.RepositoryItems.Add(repoTexto);
                colCentro.ColumnEdit = repoTexto;
            }

            ConfigurarColumnaCheckBox(gridCentroCosto, view, "ACTIVO", "Activo", 55);

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
                gridCentroCosto.RepositoryItems.Add(repoFecha);
                colFecha.ColumnEdit = repoFecha;
            }

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
            repoEliminar.Buttons[0].ToolTip = "Eliminar centro de costo";
            repoEliminar.ButtonClick += (s, ev) => EliminarCentroCosto();
            gridCentroCosto.RepositoryItems.Add(repoEliminar);
            colEliminar.ColumnEdit = repoEliminar;

            AplicarAparienciaGrid(view);
        }

        /// <summary>
        /// Búsqueda genérica de Centro de Costo. Usa un SP independiente y nuevo,
        /// [EDTE].[SP_BUSCAR_CENTROCOSTO] (ACCION='BUSCAR', acepta @FILTRO), en vez
        /// de reutilizar [EDTE].[SP_CENTROCOSTO] — ese SP existente solo soporta
        /// ACCION='OBTENER' (sin @FILTRO) y frmBusquedaGenerica siempre envía
        /// @FILTRO, así que reutilizarlo tal cual habría roto la búsqueda o
        /// habría requerido modificar un SP que ya usan frmFactura/frmCreditoFiscal.
        /// </summary>
        private void AbrirBusquedaCentroCostoGrid(GridView view)
        {
            var config = new BusquedaConfig
            {
                StoredProcedure = "[EDTE].[SP_BUSCAR_CENTROCOSTO]",
                Accion = "BUSCAR",
                Columnas = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "NOMBRE", "CENTRO DE COSTO" }
                },
                Anchos = new System.Collections.Generic.Dictionary<string, int>
                {
                    { "NOMBRE", 300 }
                }
            };
            using (var frm = new frmBusquedaGenerica(config))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                if (frm.ShowDialog() == DialogResult.OK && frm.FilaSeleccionada != null)
                {
                    view.SetFocusedRowCellValue("ID_CENTRO",
                        Convert.ToInt32(frm.FilaSeleccionada["ID_CENTRO"]));
                    view.SetFocusedRowCellValue("NOMBRE_CENTRO", frm.FilaSeleccionada["NOMBRE"].ToString());
                }
                else
                {
                    view.SetFocusedRowCellValue("NOMBRE_CENTRO", "");
                }
            }
        }

        private void CargarCentroCostoExistentes(int idRol)
        {
            DataTable dt = _dal.EjecutarConsulta("[ESEGURIDAD].[SP_ROL_CENTROCOSTO]",
                new { ACCION = "LISTAR", ID_ROL = idRol });
            _dtCentroCosto.Clear();
            if (dt == null) return;
            foreach (DataRow row in dt.Rows)
            {
                var f = _dtCentroCosto.NewRow();
                f["ID_ROL_CC"] = row["ID_ROL_CC"];
                f["ID_ROL"] = row["ID_ROL"];
                f["ID_CENTRO"] = row["ID_CENTRO"] == DBNull.Value
                                     ? (object)DBNull.Value
                                     : Convert.ToInt32(row["ID_CENTRO"]);
                f["NOMBRE_CENTRO"] = row["NOMBRE_CENTRO"] == DBNull.Value
                                     ? ""
                                     : row["NOMBRE_CENTRO"].ToString();
                f["ACTIVO"] = row["ACTIVO"] == DBNull.Value
                                         ? true
                                         : Convert.ToBoolean(row["ACTIVO"]);
                f["FECHA_ASIGNACION"] = row["FECHA_ASIGNACION"] == DBNull.Value
                                         ? (object)DBNull.Value
                                         : Convert.ToDateTime(row["FECHA_ASIGNACION"]);
                _dtCentroCosto.Rows.Add(f);
            }
        }

        private void AgregarCentroCosto()
        {
            var fila = _dtCentroCosto.NewRow();
            fila["ID_ROL_CC"] = 0;
            fila["ID_ROL"] = IdRol;
            fila["ID_CENTRO"] = DBNull.Value;
            fila["NOMBRE_CENTRO"] = "";
            fila["ACTIVO"] = true;
            fila["FECHA_ASIGNACION"] = DateTime.Today;
            _dtCentroCosto.Rows.Add(fila);
        }

        private void EliminarCentroCosto()
        {
            var view = gridCentroCosto.MainView as GridView;
            if (view == null) return;
            int fila = view.FocusedRowHandle;
            if (fila < 0) return;
            if (MessageBox.Show("¿Desea eliminar este centro de costo del rol?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            object valId = view.GetRowCellValue(fila, "ID_ROL_CC");
            if (valId != null && valId != DBNull.Value)
            {
                int idRolCc = Convert.ToInt32(valId);
                if (idRolCc > 0)
                {
                    _dal.EjecutarSinRetorno("[ESEGURIDAD].[SP_ROL_CENTROCOSTO]", new
                    {
                        ACCION = "ELIMINAR",
                        ID_ROL_CC = idRolCc
                    });
                }
            }
            view.DeleteRow(fila);
        }

        private void GuardarCentroCosto()
        {
            var view = gridCentroCosto.MainView as GridView;
            view?.CloseEditor();
            view?.UpdateCurrentRow();
            foreach (DataRow fila in _dtCentroCosto.Rows)
            {
                if (fila["ID_CENTRO"] == DBNull.Value) continue;
                _dal.EjecutarSinRetorno("[ESEGURIDAD].[SP_ROL_CENTROCOSTO]", new
                {
                    ACCION = "GUARDAR",
                    ID_ROL_CC = fila["ID_ROL_CC"] == DBNull.Value ? 0 : Convert.ToInt32(fila["ID_ROL_CC"]),
                    ID_ROL = IdRol,
                    ID_CENTRO = Convert.ToInt32(fila["ID_CENTRO"]),
                    ACTIVO = fila["ACTIVO"] == DBNull.Value ? true : Convert.ToBoolean(fila["ACTIVO"]),
                    FECHA_ASIGNACION = fila["FECHA_ASIGNACION"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(fila["FECHA_ASIGNACION"]),
                    USUARIO = Configuracion.UsuarioActual
                });
            }
            CargarCentroCostoExistentes(IdRol);
        }

        private void btnAgregarCentroCosto_Click(object sender, EventArgs e)
        {
            AgregarCentroCosto();
        }
        #endregion

        #region === GRID ROL DE PRODUCTO (2026-09-16) ===
        // Misma estructura que "GRID TIPO CLIENTE", aplicada a la nueva asociación
        // Rol <-> Rol de Producto pedida por Roberto. SP y tabla nuevos
        // ([ESEGURIDAD].[SP_ROL_ROL_PROD] / [ESEGURIDAD].[ROL_ROL_PROD]). Para la
        // búsqueda genérica se reutiliza [EINVENTARIO].[SP_ROL_PROD] ACCION='BUSCAR',
        // que ya existe y ya se usa con este mismo mecanismo en frmProducto
        // (pestaña "Roles del Producto") — no se modifica ese SP.
        private void InicializarGridRolProducto()
        {
            _dtRolProducto = new DataTable();
            _dtRolProducto.Columns.Add("ID_ROL_RP", typeof(int));
            _dtRolProducto.Columns.Add("ID_ROL", typeof(int));
            _dtRolProducto.Columns.Add("ID_ROL_PROD", typeof(int));
            _dtRolProducto.Columns.Add("NOMBRE_ROL_PROD", typeof(string));
            _dtRolProducto.Columns.Add("ACTIVO", typeof(bool));
            _dtRolProducto.Columns.Add("FECHA_ASIGNACION", typeof(DateTime));
            gridRolProducto.DataSource = _dtRolProducto;

            var view = gridRolProducto.MainView as GridView;
            if (view == null) return;
            view.Columns.Clear();
            view.PopulateColumns();

            OcultarColumna(view, "ID_ROL_RP");
            OcultarColumna(view, "ID_ROL");
            OcultarColumna(view, "ID_ROL_PROD");

            var colRolProd = view.Columns["NOMBRE_ROL_PROD"];
            if (colRolProd != null)
            {
                colRolProd.Caption = "Rol de Producto";
                colRolProd.Width = 220;
                colRolProd.Visible = true;
                colRolProd.VisibleIndex = 0;
                colRolProd.OptionsColumn.AllowEdit = true;
                var repoTexto = new RepositoryItemTextEdit();
                repoTexto.KeyDown += (s, ev) =>
                {
                    if (ev.KeyCode != Keys.Enter) return;
                    var editor = s as TextEdit;
                    if (editor == null) return;
                    if (editor.Text?.Trim() != "*") return;
                    ev.Handled = true;
                    AbrirBusquedaRolProductoGrid(view);
                };
                gridRolProducto.RepositoryItems.Add(repoTexto);
                colRolProd.ColumnEdit = repoTexto;
            }

            ConfigurarColumnaCheckBox(gridRolProducto, view, "ACTIVO", "Activo", 55);

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
                gridRolProducto.RepositoryItems.Add(repoFecha);
                colFecha.ColumnEdit = repoFecha;
            }

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
            repoEliminar.Buttons[0].ToolTip = "Eliminar rol de producto";
            repoEliminar.ButtonClick += (s, ev) => EliminarRolProducto();
            gridRolProducto.RepositoryItems.Add(repoEliminar);
            colEliminar.ColumnEdit = repoEliminar;

            AplicarAparienciaGrid(view);
        }

        private void AbrirBusquedaRolProductoGrid(GridView view)
        {
            var config = new BusquedaConfig
            {
                StoredProcedure = "[EINVENTARIO].[SP_ROL_PROD]",
                Accion = "BUSCAR",
                Columnas = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "NOMBRE_ROL_PROD", "ROL DE PRODUCTO" }
                },
                Anchos = new System.Collections.Generic.Dictionary<string, int>
                {
                    { "NOMBRE_ROL_PROD", 300 }
                }
            };
            using (var frm = new frmBusquedaGenerica(config))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                if (frm.ShowDialog() == DialogResult.OK && frm.FilaSeleccionada != null)
                {
                    view.SetFocusedRowCellValue("ID_ROL_PROD",
                        Convert.ToInt32(frm.FilaSeleccionada["ID_ROL_PROD"]));
                    view.SetFocusedRowCellValue("NOMBRE_ROL_PROD", frm.FilaSeleccionada["NOMBRE_ROL_PROD"].ToString());
                }
                else
                {
                    view.SetFocusedRowCellValue("NOMBRE_ROL_PROD", "");
                }
            }
        }

        private void CargarRolProductoExistentes(int idRol)
        {
            DataTable dt = _dal.EjecutarConsulta("[ESEGURIDAD].[SP_ROL_ROL_PROD]",
                new { ACCION = "LISTAR", ID_ROL = idRol });
            _dtRolProducto.Clear();
            if (dt == null) return;
            foreach (DataRow row in dt.Rows)
            {
                var f = _dtRolProducto.NewRow();
                f["ID_ROL_RP"] = row["ID_ROL_RP"];
                f["ID_ROL"] = row["ID_ROL"];
                f["ID_ROL_PROD"] = row["ID_ROL_PROD"] == DBNull.Value
                                     ? (object)DBNull.Value
                                     : Convert.ToInt32(row["ID_ROL_PROD"]);
                f["NOMBRE_ROL_PROD"] = row["NOMBRE_ROL_PROD"] == DBNull.Value
                                     ? ""
                                     : row["NOMBRE_ROL_PROD"].ToString();
                f["ACTIVO"] = row["ACTIVO"] == DBNull.Value
                                         ? true
                                         : Convert.ToBoolean(row["ACTIVO"]);
                f["FECHA_ASIGNACION"] = row["FECHA_ASIGNACION"] == DBNull.Value
                                         ? (object)DBNull.Value
                                         : Convert.ToDateTime(row["FECHA_ASIGNACION"]);
                _dtRolProducto.Rows.Add(f);
            }
        }

        private void AgregarRolProducto()
        {
            var fila = _dtRolProducto.NewRow();
            fila["ID_ROL_RP"] = 0;
            fila["ID_ROL"] = IdRol;
            fila["ID_ROL_PROD"] = DBNull.Value;
            fila["NOMBRE_ROL_PROD"] = "";
            fila["ACTIVO"] = true;
            fila["FECHA_ASIGNACION"] = DateTime.Today;
            _dtRolProducto.Rows.Add(fila);
        }

        private void EliminarRolProducto()
        {
            var view = gridRolProducto.MainView as GridView;
            if (view == null) return;
            int fila = view.FocusedRowHandle;
            if (fila < 0) return;
            if (MessageBox.Show("¿Desea eliminar este rol de producto del rol?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            object valId = view.GetRowCellValue(fila, "ID_ROL_RP");
            if (valId != null && valId != DBNull.Value)
            {
                int idRolRp = Convert.ToInt32(valId);
                if (idRolRp > 0)
                {
                    _dal.EjecutarSinRetorno("[ESEGURIDAD].[SP_ROL_ROL_PROD]", new
                    {
                        ACCION = "ELIMINAR",
                        ID_ROL_RP = idRolRp
                    });
                }
            }
            view.DeleteRow(fila);
        }

        private void GuardarRolProducto()
        {
            var view = gridRolProducto.MainView as GridView;
            view?.CloseEditor();
            view?.UpdateCurrentRow();
            foreach (DataRow fila in _dtRolProducto.Rows)
            {
                if (fila["ID_ROL_PROD"] == DBNull.Value) continue;
                _dal.EjecutarSinRetorno("[ESEGURIDAD].[SP_ROL_ROL_PROD]", new
                {
                    ACCION = "GUARDAR",
                    ID_ROL_RP = fila["ID_ROL_RP"] == DBNull.Value ? 0 : Convert.ToInt32(fila["ID_ROL_RP"]),
                    ID_ROL = IdRol,
                    ID_ROL_PROD = Convert.ToInt32(fila["ID_ROL_PROD"]),
                    ACTIVO = fila["ACTIVO"] == DBNull.Value ? true : Convert.ToBoolean(fila["ACTIVO"]),
                    FECHA_ASIGNACION = fila["FECHA_ASIGNACION"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(fila["FECHA_ASIGNACION"]),
                    USUARIO = Configuracion.UsuarioActual
                });
            }
            CargarRolProductoExistentes(IdRol);
        }

        private void btnAgregarRolProducto_Click(object sender, EventArgs e)
        {
            AgregarRolProducto();
        }
        #endregion

        #region === PERMISOS (2026-09-16) ===
        // Grupo "Permisos" pedido por Roberto, siempre visible debajo de las
        // pestañas de asociaciones. No es una relación con catálogo (como Tipo
        // Cliente/Centro de Costo/Rol de Producto) sino 4 banderas propias del
        // rol, así que se guardan como columnas BIT directas en dbo.ROL a través
        // de un SP independiente y nuevo, [ESEGURIDAD].[SP_ROL_PERMISOS]
        // (OBTENER/GUARDAR), para no tocar [ESEGURIDAD].[SP_ROL] existente.
        private void CargarPermisos(int idRol)
        {
            DataTable dt = _dal.EjecutarConsulta("[ESEGURIDAD].[SP_ROL_PERMISOS]",
                new { ACCION = "OBTENER", ID_ROL = idRol });
            if (dt == null || dt.Rows.Count == 0)
            {
                chkPermisoCtasContables.Checked = false;
                chkPermisoTipoCliente.Checked = false;
                chkPermisoRolProducto.Checked = false;
                chkPermisoAsocioProducto.Checked = false;
                chkPermisoCodClienteProveedorSigesta.Checked = false;
                chkPermisoCodProductoSigesta.Checked = false;
                chkPermisoCtasContablesProveedor.Checked = false;
                chkPermisoTipoProveedor.Checked = false;
                chkPermisoCodProveedorSigesta.Checked = false;
                return;
            }
            DataRow r = dt.Rows[0];
            chkPermisoCtasContables.Checked = LeerPermiso(r, "PERMISO_EDICION_CTAS_CONTABLES");
            chkPermisoTipoCliente.Checked = LeerPermiso(r, "PERMISO_EDICION_TIPO_CLIENTE");
            chkPermisoRolProducto.Checked = LeerPermiso(r, "PERMISO_EDICION_ROL_PRODUCTO");
            chkPermisoAsocioProducto.Checked = LeerPermiso(r, "PERMISO_EDICION_ASOCIO_PRODUCTO");
            chkPermisoCodClienteProveedorSigesta.Checked = LeerPermiso(r, "PERMISO_EDICION_COD_CLIENTE_PROV_SIGESTA");
            chkPermisoCodProductoSigesta.Checked = LeerPermiso(r, "PERMISO_EDICION_COD_PRODUCTO_SIGESTA");
            chkPermisoCtasContablesProveedor.Checked = LeerPermiso(r, "PERMISO_EDICION_CTAS_PROVEEDOR");
            chkPermisoTipoProveedor.Checked = LeerPermiso(r, "PERMISO_EDICION_TIPO_PROVEEDOR");
            chkPermisoCodProveedorSigesta.Checked = LeerPermiso(r, "PERMISO_EDICION_COD_PROVEEDOR_SIGESTA");
        }

        private static bool LeerPermiso(DataRow fila, string columna)
        {
            return fila?.Table?.Columns.Contains(columna) == true
                && fila[columna] != DBNull.Value
                && Convert.ToBoolean(fila[columna]);
        }

        private void GuardarPermisos()
        {
            _dal.EjecutarSinRetorno("[ESEGURIDAD].[SP_ROL_PERMISOS]", new
            {
                ACCION = "GUARDAR",
                ID_ROL = IdRol,
                PERMISO_EDICION_CTAS_CONTABLES = chkPermisoCtasContables.Checked,
                PERMISO_EDICION_TIPO_CLIENTE = chkPermisoTipoCliente.Checked,
                PERMISO_EDICION_ROL_PRODUCTO = chkPermisoRolProducto.Checked,
                PERMISO_EDICION_ASOCIO_PRODUCTO = chkPermisoAsocioProducto.Checked,
                PERMISO_EDICION_COD_CLIENTE_PROV_SIGESTA = chkPermisoCodClienteProveedorSigesta.Checked,
                PERMISO_EDICION_COD_PRODUCTO_SIGESTA = chkPermisoCodProductoSigesta.Checked,
                PERMISO_EDICION_CTAS_PROVEEDOR = chkPermisoCtasContablesProveedor.Checked,
                PERMISO_EDICION_TIPO_PROVEEDOR = chkPermisoTipoProveedor.Checked,
                PERMISO_EDICION_COD_PROVEEDOR_SIGESTA = chkPermisoCodProveedorSigesta.Checked,
                USUARIO = Configuracion.UsuarioActual
            });
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
                GuardarTipoProveedor();
                GuardarCentroCosto();
                GuardarRolProducto();
                GuardarPermisos();
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
            _dtTipoProveedor?.Clear();
            _dtCentroCosto?.Clear();
            _dtRolProducto?.Clear();
            chkPermisoCtasContables.Checked = false;
            chkPermisoTipoCliente.Checked = false;
            chkPermisoRolProducto.Checked = false;
            chkPermisoAsocioProducto.Checked = false;
            chkPermisoCodClienteProveedorSigesta.Checked = false;
            chkPermisoCodProductoSigesta.Checked = false;
            chkPermisoCtasContablesProveedor.Checked = false;
            chkPermisoTipoProveedor.Checked = false;
            chkPermisoCodProveedorSigesta.Checked = false;
        }
        #endregion

        #region === BOTONES ===
        private void ConfigurarBotones(bool esNuevo)
        {
            btnEliminar.Enabled = !esNuevo && IdRol > 0;
            btnAgregarTipoCliente.Enabled = !esNuevo && IdRol > 0;
            btnAgregarTipoProveedor.Enabled = !esNuevo && IdRol > 0;
            btnAgregarCentroCosto.Enabled = !esNuevo && IdRol > 0;
            btnAgregarRolProducto.Enabled = !esNuevo && IdRol > 0;
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
