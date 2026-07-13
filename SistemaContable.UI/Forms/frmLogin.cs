using DevExpress.XtraEditors;
using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Windows.Forms;


namespace SistemaContable.UI.Forms
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUSUARIO.Text))
            {
                XtraMessageBox.Show(
                    "Ingrese su usuario.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUSUARIO.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCLAVE.Text))
            {
                XtraMessageBox.Show(
                     "Ingrese su contraseña.", "Validación",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCLAVE.Focus();
                return;
            }

            try
            {
                string claveHash = HashClave(txtCLAVE.Text.Trim());
                var dal = new DALBase();
                var dt = dal.EjecutarConsulta("SP_USUARIO", new
                {
                    ACCION = "LOGIN",
                    USUARIO = txtUSUARIO.Text.Trim().ToUpper(),
                    CLAVE = claveHash
                });

                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    Configuracion.UsuarioActual = row["USUARIO"].ToString();
                    Configuracion.NombreUsuarioActual = row["NOMBRE"].ToString();
                    Configuracion.IdRolActual = Convert.ToInt32(row["ID_ROL"]);
                    Configuracion.NombreRolActual = row["NOMBRE_ROL"].ToString();

                    // Configurar Helper de Cuentas Contables para Grid
                    CuentaContableHint.Configurar(new DALBase());

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show("Usuario o contraseña incorrectos.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCLAVE.Text = string.Empty;
                    txtCLAVE.Focus();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error al conectar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string HashClave(string clave)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(clave));
                return BitConverter.ToString(bytes)
                                   .Replace("-", "")
                                   .ToLower();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            FormHelper.AplicarEnterComoTab(this);
            txtCLAVE.KeyDown += txtCLAVE_KeyDown;
            this.pictureBox1.Image = global::SistemaContable.UI.Properties.Resources.login;
        }

        private void txtCLAVE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnIngresar_Click(sender, e);
            }
        }

      
    }      
}

