using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SistemaContable.UI.Forms.Seguridad
{
    public partial class frmUsuario : Form
    {
        #region === CAMPOS PRIVADOS ===
        private readonly DALBase _dal = new DALBase();
        #endregion

        /// <summary>
        /// Login del usuario que se está editando (PK real de dbo.USUARIO).
        /// null/"" indica que el formulario está en modo "Nuevo".
        /// </summary>
        public string Usuario { get; set; } = null;

        public frmUsuario()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        #region === CARGA INICIAL ===
        private void frmUsuario_Load(object sender, EventArgs e)
        {
            // No se usa FormHelper.Inicializar(this) porque también aplica
            // mayúsculas forzadas (AplicarMayusculas) a todos los campos de texto.
            // Aquí solo se quiere el atajo Enter=Tab, dejando el texto libre.
            FormHelper.AplicarEnterComoTab(this);
            CargarRoles();
            if (string.IsNullOrEmpty(Usuario))
            {
                LimpiarFormulario();
                ConfigurarBotones(esNuevo: true);
                txtUSUARIO.Focus();
            }
            else
            {
                CargarUsuarioExistente(Usuario);
            }
        }
        #endregion

        #region === COMBO ROL ===
        /// <summary>
        /// Carga el combo de Rol usando [ESEGURIDAD].[SP_ROL] ACCION=BUSCAR con
        /// @FILTRO = NULL. Nota: ese SP limita a TOP 20 registros; si la tabla ROL
        /// crece más de 20 filas habrá que ajustarlo para no perder roles aquí.
        /// </summary>
        private void CargarRoles()
        {
            DataTable dt = _dal.EjecutarConsulta("[ESEGURIDAD].[SP_ROL]",
                new { ACCION = "BUSCAR", FILTRO = (string)null });
            if (dt == null) dt = new DataTable();
            if (!dt.Columns.Contains("ID_ROL")) dt.Columns.Add("ID_ROL", typeof(int));
            if (!dt.Columns.Contains("NOMBRE_ROL")) dt.Columns.Add("NOMBRE_ROL", typeof(string));
            AgregarFilaVacia(dt, "NOMBRE_ROL");
            cbxID_ROL.DataSource = dt;
            cbxID_ROL.ValueMember = "ID_ROL";
            cbxID_ROL.DisplayMember = "NOMBRE_ROL";
            cbxID_ROL.SelectedIndex = 0;
        }

        private static void AgregarFilaVacia(DataTable dt, string displayMember)
        {
            foreach (DataColumn col in dt.Columns)
                col.AllowDBNull = true;
            DataRow fila = dt.NewRow();
            foreach (DataColumn col in dt.Columns)
            {
                try
                {
                    if (displayMember != null && col.ColumnName == displayMember)
                        fila[col] = "-- Sin rol --";
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
        #endregion

        #region === CARGAR USUARIO EXISTENTE ===
        private void CargarUsuarioExistente(string usuario)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DataTable dt = _dal.EjecutarConsulta("[dbo].[SP_USUARIO]",
                    new { ACCION = "OBTENER", USUARIO = usuario });
                if (dt == null || dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No se encontró el usuario solicitado.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    return;
                }
                DataRow r = dt.Rows[0];
                Usuario = r["USUARIO"].ToString();
                txtUSUARIO.Text = Usuario;
                txtUSUARIO.ReadOnly = true;
                txtNOMBRE.Text = AsString(r["NOMBRE"]);
                txtEMAIL.Text = AsString(r["EMAIL"]);
                chkACTIVO.Checked = r["ACTIVO"] != DBNull.Value && Convert.ToBoolean(r["ACTIVO"]);
                chkBLOQUEADO.Checked = r["BLOQUEADO"] != DBNull.Value && Convert.ToBoolean(r["BLOQUEADO"]);
                SetComboById(cbxID_ROL, r["ID_ROL"] == DBNull.Value ? (int?)null : Convert.ToInt32(r["ID_ROL"]));
                lblFECHA_ULTACCESO.Text = r["FECHA_ULTACCESO"] == DBNull.Value
                    ? "Nunca"
                    : Convert.ToDateTime(r["FECHA_ULTACCESO"]).ToString("dd/MM/yyyy HH:mm");
                txtCLAVE.Text = "";
                txtCLAVE_CONFIRMAR.Text = "";
                lblValidacionUsuario.Text = "";
                ConfigurarBotones(esNuevo: false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar el usuario:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion

        #region === VALIDACIÓN USUARIO (duplicado al crear) ===
        private void txtUSUARIO_Leave(object sender, EventArgs e)
        {
            // Solo aplica en modo "Nuevo": si ya se está editando, el login es fijo.
            if (!string.IsNullOrEmpty(Usuario)) return;
            string login = txtUSUARIO.Text.Trim();
            if (string.IsNullOrEmpty(login))
            {
                lblValidacionUsuario.Text = "";
                return;
            }
            try
            {
                DataTable dt = _dal.EjecutarConsulta("[dbo].[SP_USUARIO]",
                    new { ACCION = "OBTENER", USUARIO = login });
                bool existe = dt != null && dt.Rows.Count > 0;
                if (existe)
                {
                    lblValidacionUsuario.Text = "✘";
                    lblValidacionUsuario.ForeColor = Color.Red;
                    lblValidacionUsuario.Tag = "DUPLICADO";
                    XtraMessageBox.Show($"El usuario '{login}' ya existe.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    lblValidacionUsuario.Text = "✔";
                    lblValidacionUsuario.ForeColor = Color.Green;
                    lblValidacionUsuario.Tag = "OK";
                }
            }
            catch
            {
                lblValidacionUsuario.Text = "";
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
                // La clave viaja hasheada a la BD; si el campo queda vacío al editar,
                // SP_USUARIO conserva la clave actual (no se sobreescribe).
                string claveHash = string.IsNullOrEmpty(txtCLAVE.Text) ? null : HashClave(txtCLAVE.Text);

                var dtResult = _dal.EjecutarConsulta("[dbo].[SP_USUARIO]", new
                {
                    ACCION = "GUARDAR",
                    USUARIO = txtUSUARIO.Text.Trim(),
                    ID_ROL = ObtenerIdCombo(cbxID_ROL),
                    NOMBRE = txtNOMBRE.Text.Trim(),
                    EMAIL = NullIfEmpty(txtEMAIL.Text),
                    CLAVE = claveHash,
                    ACTIVO = chkACTIVO.Checked,
                    BLOQUEADO = chkBLOQUEADO.Checked,
                    USUARIO_CREA = Configuracion.UsuarioActual,
                    USUARIO_ACT = Configuracion.UsuarioActual
                });
                if (dtResult == null || dtResult.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Error al guardar el usuario.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Usuario = dtResult.Rows[0]["ID_GENERADO"].ToString();
                txtUSUARIO.Text = Usuario;
                txtUSUARIO.ReadOnly = true;
                txtCLAVE.Text = "";
                txtCLAVE_CONFIRMAR.Text = "";
                lblValidacionUsuario.Text = "";
                XtraMessageBox.Show("Usuario guardado correctamente.",
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

        #region === ELIMINAR (baja lógica) ===
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Usuario))
            {
                LimpiarFormulario();
                return;
            }
            if (MessageBox.Show("¿Desea desactivar este usuario?\n(SP_USUARIO.ELIMINAR hace baja lógica: pone ACTIVO = 0).",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            try
            {
                _dal.EjecutarSinRetorno("[dbo].[SP_USUARIO]", new
                {
                    ACCION = "ELIMINAR",
                    USUARIO = Usuario,
                    USUARIO_ACT = Configuracion.UsuarioActual
                });
                XtraMessageBox.Show("Usuario desactivado correctamente.",
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
            if (string.IsNullOrWhiteSpace(txtUSUARIO.Text))
            {
                XtraMessageBox.Show("El usuario (login) es obligatorio.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUSUARIO.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNOMBRE.Text))
            {
                XtraMessageBox.Show("El nombre del usuario es obligatorio.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNOMBRE.Focus();
                return false;
            }
            bool esNuevo = string.IsNullOrEmpty(Usuario);
            if (esNuevo && string.IsNullOrWhiteSpace(txtCLAVE.Text))
            {
                XtraMessageBox.Show("La clave es obligatoria para un usuario nuevo.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCLAVE.Focus();
                return false;
            }
            if (!string.IsNullOrEmpty(txtCLAVE.Text) && txtCLAVE.Text != txtCLAVE_CONFIRMAR.Text)
            {
                XtraMessageBox.Show("La clave y la confirmación no coinciden.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCLAVE_CONFIRMAR.Focus();
                return false;
            }
            if (esNuevo && lblValidacionUsuario.Tag as string == "DUPLICADO")
            {
                XtraMessageBox.Show("Ese usuario ya existe; elige otro login.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUSUARIO.Focus();
                return false;
            }
            return true;
        }
        #endregion

        #region === LIMPIAR ===
        private void LimpiarFormulario()
        {
            Usuario = null;
            txtUSUARIO.Text = "";
            txtUSUARIO.ReadOnly = false;
            txtNOMBRE.Text = "";
            txtEMAIL.Text = "";
            txtCLAVE.Text = "";
            txtCLAVE_CONFIRMAR.Text = "";
            chkACTIVO.Checked = true;
            chkBLOQUEADO.Checked = false;
            cbxID_ROL.SelectedIndex = 0;
            lblFECHA_ULTACCESO.Text = "Nunca";
            lblValidacionUsuario.Text = "";
        }
        #endregion

        #region === BOTONES ===
        private void ConfigurarBotones(bool esNuevo)
        {
            btnEliminar.Enabled = !esNuevo && !string.IsNullOrEmpty(Usuario);
            txtUSUARIO.ReadOnly = !esNuevo;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            ConfigurarBotones(esNuevo: true);
            txtUSUARIO.Focus();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region === HELPERS ===
        /// <summary>
        /// Hash de la clave en texto plano usando SHA256 (hex de 64 caracteres).
        /// No se usa salt: si más adelante el login usa un esquema distinto
        /// (BCrypt, PBKDF2 con salt, etc.) hay que unificar aquí y en el login.
        /// </summary>
        private static string HashClave(string claveTexto)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(claveTexto));
                var sb = new System.Text.StringBuilder(bytes.Length * 2);
                foreach (byte b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
        private static void SetComboById(System.Windows.Forms.ComboBox cbx, int? value)
        {
            if (value != null && value != 0)
                cbx.SelectedValue = value;
            else
                cbx.SelectedIndex = 0;
        }
        private static int? ObtenerIdCombo(System.Windows.Forms.ComboBox cbx)
        {
            if (cbx.SelectedValue == null || cbx.SelectedValue == DBNull.Value) return null;
            if (cbx.SelectedValue is DataRowView) return null;
            int val = Convert.ToInt32(cbx.SelectedValue);
            return val == 0 ? (int?)null : val;
        }
        private static string AsString(object val)
            => val == null || val == DBNull.Value ? "" : val.ToString();
        private static string NullIfEmpty(string texto)
            => string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
        #endregion
    }
}
