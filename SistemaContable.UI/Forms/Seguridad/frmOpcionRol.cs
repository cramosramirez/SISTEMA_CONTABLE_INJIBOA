using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Seguridad
{
    /// <summary>
    /// Mantenimiento independiente de la relación dbo.OPCION_ROL.
    /// No modifica el mantenimiento de roles ni el catálogo de opciones.
    /// </summary>
    public partial class frmOpcionRol : Form
    {
        private const string CampoSeleccion = "SELECCIONADO";
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtRoles;
        private DataTable _dtOpciones;
        private readonly Dictionary<int, bool> _estadoOriginal = new Dictionary<int, bool>();
        private bool _cargandoFormulario;
        private bool _actualizandoChecks;
        private bool _revirtiendoRol;
        private bool _hayCambios;
        private int _idRolCargado;

        public int IdRolInicial { get; set; }

        public frmOpcionRol()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmOpcionRol_Load(object sender, EventArgs e)
        {
            FormHelper.AplicarEnterComoTab(this);
            InicializarTreeOpciones();
            CargarRoles();
        }

        private void InicializarTreeOpciones()
        {
            treeListOpciones.KeyFieldName = "ID_OPCION";
            treeListOpciones.ParentFieldName = "ID_OPCION_PADRE";
            // ASIGNADO proviene de una expresión calculada del procedimiento y
            // ADO.NET la marca como solo lectura. El árbol usa una columna local
            // editable para que DevExpress pueda sincronizar los cheques.
            treeListOpciones.CheckBoxFieldName = CampoSeleccion;
            treeListOpciones.SyncCheckStateWithSourceOnChecking = true;
            treeListOpciones.OptionsView.ShowCheckBoxes = true;
            treeListOpciones.OptionsView.ShowIndicator = false;
            treeListOpciones.OptionsView.ShowHorzLines = true;
            treeListOpciones.OptionsView.ShowVertLines = false;
            treeListOpciones.OptionsBehavior.Editable = false;
            // La selección se controla manualmente: un hijo marca a todos sus
            // padres, pero no marca a sus hermanos.
            treeListOpciones.OptionsBehavior.AllowRecursiveNodeChecking = false;
            treeListOpciones.OptionsBehavior.AllowIndeterminateCheckState = false;
            treeListOpciones.OptionsSelection.EnableAppearanceFocusedCell = false;
            treeListOpciones.OptionsFind.AlwaysVisible = true;
            treeListOpciones.OptionsFind.FindNullPrompt = "Buscar opción del menú...";
            treeListOpciones.AfterCheckNode += treeListOpciones_AfterCheckNode;
        }

        private void CargarRoles()
        {
            try
            {
                _cargandoFormulario = true;
                ConfigurarEstadoCarga(true);

                _dtRoles = _dal.EjecutarConsulta("[ESEGURIDAD].[SP_ROL]", new
                {
                    ACCION = "BUSCAR"
                }) ?? new DataTable();

                if (!_dtRoles.Columns.Contains("ID_ROL") ||
                    !_dtRoles.Columns.Contains("NOMBRE_ROL"))
                {
                    throw new InvalidOperationException(
                        "La consulta de roles no devolvió ID_ROL y NOMBRE_ROL.");
                }

                cbxROL.DataSource = null;
                cbxROL.DisplayMember = "NOMBRE_ROL";
                cbxROL.ValueMember = "ID_ROL";
                cbxROL.DataSource = _dtRoles;

                if (_dtRoles.Rows.Count == 0)
                {
                    cbxROL.SelectedIndex = -1;
                    treeListOpciones.DataSource = null;
                    lblResumenOpciones.Text = "No hay roles disponibles";
                    return;
                }

                if (IdRolInicial > 0 && _dtRoles.Select("ID_ROL = " + IdRolInicial).Length > 0)
                    cbxROL.SelectedValue = IdRolInicial;
                else
                    cbxROL.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar los roles:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _cargandoFormulario = false;
                ConfigurarEstadoCarga(false);
            }

            int idRol = ObtenerIdRolSeleccionado();
            if (idRol > 0)
                CargarOpcionesRol(idRol);
        }

        private void CargarOpcionesRol(int idRol)
        {
            try
            {
                _actualizandoChecks = true;
                ConfigurarEstadoCarga(true);

                _dtOpciones = _dal.EjecutarConsulta("[ESEGURIDAD].[SP_OPCION_ROL]", new
                {
                    ACCION = "LISTAR",
                    ID_ROL = idRol
                }) ?? new DataTable();

                if (!_dtOpciones.Columns.Contains("ID_OPCION") ||
                    !_dtOpciones.Columns.Contains("ID_OPCION_PADRE") ||
                    !_dtOpciones.Columns.Contains("NOMBRE_OPCION") ||
                    !_dtOpciones.Columns.Contains("ASIGNADO"))
                {
                    throw new InvalidOperationException(
                        "La consulta de opciones no devolvió la estructura requerida.");
                }

                _estadoOriginal.Clear();
                DataColumn columnaSeleccion = _dtOpciones.Columns.Add(
                    CampoSeleccion, typeof(bool));
                columnaSeleccion.DefaultValue = false;

                foreach (DataRow fila in _dtOpciones.Rows)
                {
                    int idOpcion = Convert.ToInt32(fila["ID_OPCION"]);
                    bool asignado = fila["ASIGNADO"] != DBNull.Value &&
                                    Convert.ToBoolean(fila["ASIGNADO"]);
                    _estadoOriginal[idOpcion] = asignado;
                    fila[CampoSeleccion] = asignado;
                }

                treeListOpciones.DataSource = _dtOpciones;
                treeListOpciones.PopulateColumns();
                ConfigurarColumnasTree();
                NormalizarAncestrosSeleccionados();
                treeListOpciones.ExpandAll();

                _idRolCargado = idRol;
                _hayCambios = false;
                ActualizarResumen();
            }
            catch (Exception ex)
            {
                treeListOpciones.DataSource = null;
                XtraMessageBox.Show("Error al cargar las opciones del rol:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _actualizandoChecks = false;
                ConfigurarEstadoCarga(false);
            }
        }

        private void ConfigurarColumnasTree()
        {
            foreach (DevExpress.XtraTreeList.Columns.TreeListColumn columna in treeListOpciones.Columns)
                columna.Visible = false;

            var colOpcion = treeListOpciones.Columns.ColumnByFieldName("NOMBRE_OPCION");
            if (colOpcion != null)
            {
                colOpcion.Caption = "Opción del menú";
                colOpcion.Visible = true;
                colOpcion.Width = 430;
            }

            var colFormulario = treeListOpciones.Columns.ColumnByFieldName("FORMULARIO_WIN");
            if (colFormulario != null)
            {
                colFormulario.Caption = "Formulario";
                colFormulario.Visible = true;
                colFormulario.Width = 330;
            }

            var colActiva = treeListOpciones.Columns.ColumnByFieldName("OPCION_ACTIVA");
            if (colActiva != null)
            {
                colActiva.Caption = "Disponible";
                colActiva.Visible = true;
                colActiva.Width = 85;
            }

            // DevExpress inserta cada columna recién mostrada en la primera
            // posición. Se ordenan al final y en sentido inverso para dejar
            // la jerarquía del menú como la primera columna del árbol.
            if (colActiva != null) colActiva.VisibleIndex = 2;
            if (colFormulario != null) colFormulario.VisibleIndex = 1;
            if (colOpcion != null) colOpcion.VisibleIndex = 0;

            treeListOpciones.Appearance.Row.Font = new Font("Segoe UI", 9f);
            treeListOpciones.Appearance.Row.Options.UseFont = true;
            treeListOpciones.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            treeListOpciones.Appearance.HeaderPanel.Options.UseFont = true;
            treeListOpciones.Appearance.FocusedRow.BackColor = Color.FromArgb(204, 229, 255);
            treeListOpciones.Appearance.FocusedRow.Options.UseBackColor = true;
        }

        private int ObtenerIdRolSeleccionado()
        {
            if (cbxROL.SelectedValue == null || cbxROL.SelectedValue == DBNull.Value ||
                cbxROL.SelectedValue is DataRowView)
                return 0;

            int idRol;
            return int.TryParse(cbxROL.SelectedValue.ToString(), out idRol) ? idRol : 0;
        }

        private IEnumerable<TreeListNode> EnumerarNodos(TreeListNodes nodos)
        {
            foreach (TreeListNode nodo in nodos)
            {
                yield return nodo;
                foreach (TreeListNode hijo in EnumerarNodos(nodo.Nodes))
                    yield return hijo;
            }
        }

        private List<int> ObtenerIdsSeleccionados()
        {
            var ids = new HashSet<int>();

            foreach (TreeListNode nodo in EnumerarNodos(treeListOpciones.Nodes)
                         .Where(n => n.CheckState != CheckState.Unchecked))
            {
                TreeListNode actual = nodo;
                while (actual != null)
                {
                    ids.Add(Convert.ToInt32(actual.GetValue("ID_OPCION")));
                    actual = actual.ParentNode;
                }
            }

            return ids.OrderBy(id => id).ToList();
        }

        private void treeListOpciones_AfterCheckNode(object sender, NodeEventArgs e)
        {
            if (_actualizandoChecks) return;

            try
            {
                _actualizandoChecks = true;
                treeListOpciones.BeginUpdate();

                if (e.Node.CheckState == CheckState.Checked)
                {
                    SeleccionarAncestros(e.Node.ParentNode);
                }
                else
                {
                    // Un padre no puede quedar desmarcado si mantiene hijos
                    // seleccionados, porque el menú perdería su jerarquía.
                    DesmarcarDescendientes(e.Node);
                    ActualizarAncestros(e.Node.ParentNode);
                }

                _hayCambios = true;
            }
            finally
            {
                treeListOpciones.EndUpdate();
                _actualizandoChecks = false;
                ActualizarResumen();
            }
        }

        private void NormalizarAncestrosSeleccionados()
        {
            List<TreeListNode> seleccionados = EnumerarNodos(treeListOpciones.Nodes)
                .Where(n => n.CheckState == CheckState.Checked)
                .ToList();

            foreach (TreeListNode nodo in seleccionados)
                SeleccionarAncestros(nodo.ParentNode);
        }

        private void SeleccionarAncestros(TreeListNode padre)
        {
            while (padre != null)
            {
                padre.CheckState = CheckState.Checked;
                padre = padre.ParentNode;
            }
        }

        private void DesmarcarDescendientes(TreeListNode nodo)
        {
            foreach (TreeListNode hijo in nodo.Nodes)
            {
                hijo.CheckState = CheckState.Unchecked;
                DesmarcarDescendientes(hijo);
            }
        }

        private void ActualizarAncestros(TreeListNode padre)
        {
            while (padre != null)
            {
                bool tieneHijosSeleccionados = padre.Nodes
                    .Cast<TreeListNode>()
                    .Any(hijo => hijo.CheckState == CheckState.Checked);

                padre.CheckState = tieneHijosSeleccionados
                    ? CheckState.Checked
                    : CheckState.Unchecked;
                padre = padre.ParentNode;
            }
        }

        private void ActualizarResumen()
        {
            int seleccionadas = ObtenerIdsSeleccionados().Count;
            lblResumenOpciones.Text = seleccionadas == 1
                ? "1 opción seleccionada"
                : $"{seleccionadas} opciones seleccionadas";
        }

        private void CambiarSeleccionCompleta(bool seleccionada)
        {
            try
            {
                _actualizandoChecks = true;
                treeListOpciones.BeginUpdate();
                foreach (TreeListNode nodo in EnumerarNodos(treeListOpciones.Nodes))
                    nodo.CheckState = seleccionada ? CheckState.Checked : CheckState.Unchecked;
                _hayCambios = true;
            }
            finally
            {
                treeListOpciones.EndUpdate();
                _actualizandoChecks = false;
                ActualizarResumen();
            }
        }

        private void cbxROL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoFormulario || _revirtiendoRol) return;

            int nuevoIdRol = ObtenerIdRolSeleccionado();
            if (nuevoIdRol <= 0 || nuevoIdRol == _idRolCargado) return;

            if (_hayCambios && _idRolCargado > 0)
            {
                DialogResult respuesta = XtraMessageBox.Show(
                    "Hay cambios sin guardar. ¿Desea descartarlos y cambiar de rol?",
                    "Cambios pendientes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta != DialogResult.Yes)
                {
                    _revirtiendoRol = true;
                    cbxROL.SelectedValue = _idRolCargado;
                    _revirtiendoRol = false;
                    return;
                }
            }

            CargarOpcionesRol(nuevoIdRol);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int idRol = ObtenerIdRolSeleccionado();
            if (idRol <= 0)
            {
                XtraMessageBox.Show("Seleccione un rol antes de guardar.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxROL.Focus();
                return;
            }

            List<int> ids = ObtenerIdsSeleccionados();
            if (ids.Count == 0)
            {
                DialogResult respuesta = XtraMessageBox.Show(
                    "El rol quedará sin opciones de menú. ¿Desea continuar?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta != DialogResult.Yes) return;
            }

            try
            {
                ConfigurarEstadoCarga(true);
                Cursor = Cursors.WaitCursor;

                var seleccionadas = new HashSet<int>(ids);
                var cambios = _estadoOriginal
                    .Select(estado => new
                    {
                        ID_OPCION = estado.Key,
                        ACTIVO = seleccionadas.Contains(estado.Key),
                        ANTERIOR = estado.Value
                    })
                    .Where(estado => estado.ACTIVO != estado.ANTERIOR)
                    .ToList();

                bool guardadoMasivo = _dtOpciones.Columns.Contains("FORMULARIO_WIN") &&
                                      _dtOpciones.Columns.Contains("OPCION_ACTIVA");

                if (cambios.Count > 0 && guardadoMasivo)
                {
                    _dal.EjecutarSinRetorno("[ESEGURIDAD].[SP_OPCION_ROL]", new
                    {
                        ACCION = "GUARDAR",
                        ID_ROL = idRol,
                        IDS_OPCION = string.Join(",", ids),
                        USUARIO = Configuracion.UsuarioActual
                    });
                }
                else if (cambios.Count > 0)
                {
                    // Compatibilidad con la versión anterior del procedimiento,
                    // que recibe y actualiza una sola opción por ejecución.
                    foreach (var cambio in cambios)
                    {
                        _dal.EjecutarSinRetorno("[ESEGURIDAD].[SP_OPCION_ROL]", new
                        {
                            ACCION = "GUARDAR",
                            ID_ROL = idRol,
                            ID_OPCION = cambio.ID_OPCION,
                            ACTIVO = cambio.ACTIVO,
                            USUARIO_CREA = Configuracion.UsuarioActual,
                            USUARIO_ACT = Configuracion.UsuarioActual
                        });
                    }
                }

                CargarOpcionesRol(idRol);
                string mensaje = cambios.Count == 0
                    ? "No había cambios pendientes para guardar."
                    : "Opciones del rol guardadas correctamente.";
                XtraMessageBox.Show(mensaje,
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al guardar las opciones del rol:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                ConfigurarEstadoCarga(false);
            }
        }

        private void btnMarcarTodas_Click(object sender, EventArgs e)
        {
            CambiarSeleccionCompleta(true);
        }

        private void btnDesmarcarTodas_Click(object sender, EventArgs e)
        {
            CambiarSeleccionCompleta(false);
        }

        private void btnExpandir_Click(object sender, EventArgs e)
        {
            treeListOpciones.ExpandAll();
        }

        private void btnContraer_Click(object sender, EventArgs e)
        {
            treeListOpciones.CollapseAll();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmOpcionRol_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_hayCambios) return;

            DialogResult respuesta = XtraMessageBox.Show(
                "Hay cambios sin guardar. ¿Desea cerrar la pantalla?",
                "Cambios pendientes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (respuesta != DialogResult.Yes)
                e.Cancel = true;
        }

        private void ConfigurarEstadoCarga(bool cargando)
        {
            int idRol = ObtenerIdRolSeleccionado();
            cbxROL.Enabled = !cargando;
            treeListOpciones.Enabled = !cargando && idRol > 0;
            btnGuardar.Enabled = !cargando && idRol > 0;
            btnMarcarTodas.Enabled = !cargando && idRol > 0;
            btnDesmarcarTodas.Enabled = !cargando && idRol > 0;
            btnExpandir.Enabled = !cargando && idRol > 0;
            btnContraer.Enabled = !cargando && idRol > 0;
        }
    }
}
