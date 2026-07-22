using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace SistemaContable.UI.Forms.Seguridad
{
    public partial class frmOpcion : Form
    {
        #region === CAMPOS PRIVADOS ===
        private readonly DALBase _dal = new DALBase();

        // Estado del registro que se está editando (0 = nuevo)
        private int _idOpcionActual = 0;
        private int? _idOpcionPadreActual = null;
        private int _nivelActual = 1;

        // Cache del último árbol cargado, para poder resolver nombres de padre
        // y volver a seleccionar un nodo después de Guardar sin ir otra vez a la BD.
        private DataTable _dtArbol = null;
        #endregion

        public frmOpcion()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        #region === CARGA INICIAL ===
        private void frmOpcion_Load(object sender, EventArgs e)
        {
            // No se usa FormHelper.Inicializar(this) porque también aplica
            // mayúsculas forzadas a los campos de texto; aquí solo se quiere
            // el atajo Enter=Tab.
            FormHelper.AplicarEnterComoTab(this);

            ConfigurarTreeList();
            CargarArbol();
            LimpiarFormulario(idPadreSugerido: null, nivelSugerido: 1);
            ConfigurarBotones(esNuevo: true);
        }

        private void ConfigurarTreeList()
        {
            treeListOpciones.KeyFieldName = "ID_OPCION";
            treeListOpciones.ParentFieldName = "ID_OPCION_PADRE";
            treeListOpciones.OptionsView.ShowCheckBoxes = false;
            treeListOpciones.OptionsBehavior.Editable = false;
            treeListOpciones.OptionsSelection.MultiSelect = false;
        }
        #endregion

        #region === CARGAR ÁRBOL ===
        private void CargarArbol()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DataTable dt = _dal.EjecutarConsulta("[ESEGURIDAD].[SP_OPCION]",
                    new { ACCION = "ARBOL", ACTIVO = (bool?)null });
                _dtArbol = dt ?? new DataTable();
                treeListOpciones.DataSource = _dtArbol;
                treeListOpciones.ExpandAll();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar el árbol de opciones:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Intenta reseleccionar un nodo por ID_OPCION después de recargar el árbol
        /// (por ejemplo, luego de Guardar).
        /// </summary>
        private void SeleccionarNodoPorId(int idOpcion)
        {
            TreeListNode nodo = treeListOpciones.FindNodeByKeyID(idOpcion);
            if (nodo != null)
            {
                treeListOpciones.FocusedNode = nodo;
                treeListOpciones.SetFocusedNode(nodo);
            }
        }
        #endregion

        #region === SELECCIÓN DE NODO ===
        private void treeListOpciones_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null) return;
            int idOpcion = Convert.ToInt32(e.Node.GetValue("ID_OPCION"));
            CargarOpcionSeleccionada(idOpcion);
        }

        private void CargarOpcionSeleccionada(int idOpcion)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DataTable dt = _dal.EjecutarConsulta("[ESEGURIDAD].[SP_OPCION]",
                    new { ACCION = "OBTENER", ID_OPCION = idOpcion });
                if (dt == null || dt.Rows.Count == 0) return;

                DataRow r = dt.Rows[0];
                _idOpcionActual = Convert.ToInt32(r["ID_OPCION"]);
                _idOpcionPadreActual = r["ID_OPCION_PADRE"] == DBNull.Value ? (int?)null : Convert.ToInt32(r["ID_OPCION_PADRE"]);
                _nivelActual = r["NIVEL"] == DBNull.Value ? 1 : Convert.ToInt32(r["NIVEL"]);

                txtNOMBRE_OPCION.Text = AsString(r["NOMBRE_OPCION"]);
                spinORDEN.Value = r["ORDEN"] == DBNull.Value ? 0 : Convert.ToDecimal(r["ORDEN"]);
                chkACTIVO.Checked = r["ACTIVO"] != DBNull.Value && Convert.ToBoolean(r["ACTIVO"]);
                txtFORMULARIO_WIN.Text = AsString(r["FORMULARIO_WIN"]);
                txtIMAGEN_SVG.Text = AsString(r["IMAGEN_SVG"]);

                lblNIVEL.Text = "Nivel: " + _nivelActual;
                lblOpcionPadre.Text = "Padre: " + (r["NOMBRE_PADRE"] == DBNull.Value ? "(Raíz)" : r["NOMBRE_PADRE"].ToString());

                ConfigurarBotones(esNuevo: false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar la opción:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion

        #region === NUEVO / NUEVO HIJO ===
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario(idPadreSugerido: null, nivelSugerido: 1);
            ConfigurarBotones(esNuevo: true);
            txtNOMBRE_OPCION.Focus();
        }

        private void btnAgregarHijo_Click(object sender, EventArgs e)
        {
            if (treeListOpciones.FocusedNode == null)
            {
                XtraMessageBox.Show("Selecciona primero la opción padre en el árbol.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idPadre = Convert.ToInt32(treeListOpciones.FocusedNode.GetValue("ID_OPCION"));
            int nivelPadre = Convert.ToInt32(treeListOpciones.FocusedNode.GetValue("NIVEL"));

            LimpiarFormulario(idPadreSugerido: idPadre, nivelSugerido: nivelPadre + 1);
            ConfigurarBotones(esNuevo: true);
            txtNOMBRE_OPCION.Focus();
        }

        private void LimpiarFormulario(int? idPadreSugerido, int nivelSugerido)
        {
            _idOpcionActual = 0;
            _idOpcionPadreActual = idPadreSugerido;
            _nivelActual = nivelSugerido;

            txtNOMBRE_OPCION.Text = "";
            spinORDEN.Value = 0;
            chkACTIVO.Checked = true;
            txtFORMULARIO_WIN.Text = "";
            txtIMAGEN_SVG.Text = "";

            lblNIVEL.Text = "Nivel: " + _nivelActual;
            lblOpcionPadre.Text = "Padre: " + ObtenerNombrePadre(idPadreSugerido);
        }

        private string ObtenerNombrePadre(int? idPadre)
        {
            if (idPadre == null || _dtArbol == null) return "(Raíz)";
            foreach (DataRow r in _dtArbol.Rows)
            {
                if (Convert.ToInt32(r["ID_OPCION"]) == idPadre.Value)
                    return r["NOMBRE_OPCION"].ToString();
            }
            return "(Raíz)";
        }
        #endregion

        #region === GUARDAR ===
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;
            try
            {
                Cursor = Cursors.WaitCursor;
                var dtResult = _dal.EjecutarConsulta("[ESEGURIDAD].[SP_OPCION]", new
                {
                    ACCION = "GUARDAR",
                    ID_OPCION = _idOpcionActual,
                    NOMBRE_OPCION = txtNOMBRE_OPCION.Text.Trim(),
                    NIVEL = _nivelActual,
                    ORDEN = Convert.ToInt32(spinORDEN.Value),
                    ACTIVO = chkACTIVO.Checked,
                    ID_OPCION_PADRE = _idOpcionPadreActual,
                    IMAGEN_SVG = NullIfEmpty(txtIMAGEN_SVG.Text),
                    FORMULARIO_WIN = NullIfEmpty(txtFORMULARIO_WIN.Text),
                    USUARIO_CREA = Configuracion.UsuarioActual,
                    USUARIO_ACT = Configuracion.UsuarioActual
                });
                if (dtResult == null || dtResult.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Error al guardar la opción.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int idGuardado = Convert.ToInt32(dtResult.Rows[0]["ID_GENERADO"]);

                CargarArbol();
                SeleccionarNodoPorId(idGuardado);

                XtraMessageBox.Show("Opción guardada correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (_idOpcionActual == 0)
            {
                btnNuevo_Click(sender, e);
                return;
            }
            if (XtraMessageBox.Show("¿Desea eliminar esta opción?\n(No se puede si tiene sub-opciones o está asignada a algún rol).",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            try
            {
                Cursor = Cursors.WaitCursor;
                _dal.EjecutarSinRetorno("[ESEGURIDAD].[SP_OPCION]", new
                {
                    ACCION = "ELIMINAR",
                    ID_OPCION = _idOpcionActual
                });
                CargarArbol();
                LimpiarFormulario(idPadreSugerido: null, nivelSugerido: 1);
                ConfigurarBotones(esNuevo: true);

                XtraMessageBox.Show("Opción eliminada correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error al eliminar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion

        #region === VALIDAR ===
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNOMBRE_OPCION.Text))
            {
                XtraMessageBox.Show("El nombre de la opción es obligatorio.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNOMBRE_OPCION.Focus();
                return false;
            }
            return true;
        }
        #endregion

        #region === BOTONES ===
        private void ConfigurarBotones(bool esNuevo)
        {
            btnEliminar.Enabled = !esNuevo && _idOpcionActual != 0;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region === HELPERS ===
        private static string AsString(object val)
            => val == null || val == DBNull.Value ? "" : val.ToString();

        private static string NullIfEmpty(string texto)
            => string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
        #endregion
    }
}
