using DevExpress.Utils;
using DevExpress.XtraEditors;
using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Bancos
{
    public partial class frmCuentasBancarias : Form
    {
        private readonly DALBase _dal = new DALBase();
        private int _idActual = 0;
        public frmCuentasBancarias()
        {
            InitializeComponent();            
        }

        private void frmCuentasBancarias_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);            
            CargarBancos();

            // 1. Búsqueda de Cuentas Bancarias
            FormHelper.RegistrarBusqueda(
                txtNUM_CUENTA,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_CUENTA_BANCARIA",
                    Columnas = new Dictionary<string, string>
                    {
                        { "NUM_CUENTA", "CUENTA" },
                        { "NOMBRE",     "NOMBRE" }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "NUM_CUENTA",  110 },
                        { "NOMBRE",  400 }
                    }
                },
                fila =>
                {                    
                    _idActual = Convert.ToInt32(fila["ID_CTA_BANCO"].ToString());
                    txtNUM_CUENTA.Text = fila["NUM_CUENTA"].ToString();
                    txtNOMBRE.Text = fila["NOMBRE"].ToString();
                    cbxBANCO.SelectedValue = fila["ID_BANCO"];
                    mskFECHA_APERTURA.Text = fila["FECHA_APERTURA"] != DBNull.Value ?
                         Convert.ToDateTime(fila["FECHA_APERTURA"]).ToString("dd/MM/yyyy") :
                         string.Empty;
                    txtCTACONTABLE.Text = fila["CTACONTABLE"].ToString();
                    txtCORRELATIVO_CHEQUE.Text = fila["CORRELATIVO_CHEQUE"].ToString();
                    chkActiva.Checked = fila["ACTIVA"] as bool? ?? false;
                }
            );

            // 2. Búsqueda de Catálogo de Cuentas (solo detalle)
            FormHelper.RegistrarBusqueda(
                txtCTACONTABLE,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_CATALOGO_CUENTA",
                    Columnas = new Dictionary<string, string>
                    {
                        { "CUENTA",        "CUENTA"    },
                        { "NOMBRE_CUENTA", "DESCRIPCION" }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "CUENTA",  130 },
                        { "NOMBRE_CUENTA",  400 }
                    },
                    ParametrosExtra = new { ES_DETALLE = true }
                },
                fila =>
                {
                    txtCTACONTABLE.Text = fila["CUENTA"].ToString();                    
                }
            );

            txtCORRELATIVO_CHEQUE.KeyPress += SoloNumeros_KeyPress;
            txtCORRELATIVO_CHEQUE.TextChanged += SoloNumeros_TextChanged;
            txtCTACONTABLE.Leave += (s, ev) => BuscarCuentaContablePorCodigo(txtCTACONTABLE, txtNOMBRE_CUENTA_CONTABLE);
            FormHelper.ResaltarCombosEnFoco(this);
        }

        private void CargarBancos()
        {
            var dal = new SistemaContable.DAL.DALBase();
            var dt = dal.EjecutarConsulta("SP_BANCO", new
            {
                ACCION = "BUSCAR"
            });

            // Agregar fila vacía al inicio
            var filaVacia = dt.NewRow();
            filaVacia["ID_BANCO"] = 0;
            filaVacia["NOMBRE"] = "-- SELECCIONE --";
            dt.Rows.InsertAt(filaVacia, 0);
                        
            cbxBANCO.DataSource = dt;
            cbxBANCO.DisplayMember = "NOMBRE";
            cbxBANCO.ValueMember = "ID_BANCO";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // VALIDACIONES
            if (string.IsNullOrWhiteSpace(txtNUM_CUENTA.Text))
            {
                XtraMessageBox.Show("El número de cuenta es requerido.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNUM_CUENTA.Focus();
                return;
            }
            // Validar fecha
            if (!FormHelper.ValidarFecha(mskFECHA_APERTURA, "Fecha de Apertura de cuenta"))
                return;


            try
            {                
                var dal = new DALBase();
                var dt = dal.EjecutarConsulta("SP_CUENTA_BANCARIA", new
                {
                    ACCION = "GUARDAR",
                    ID_CTA_BANCO = _idActual,
                    ID_BANCO = Convert.ToInt32(cbxBANCO.SelectedValue),
                    ID_TIPO_CTA = 2,
                    NUM_CUENTA = txtNUM_CUENTA.Text.Trim(),
                    NOMBRE = txtNOMBRE.Text.Trim(),
                    FECHA_APERTURA = FormHelper.ObtenerFecha(mskFECHA_APERTURA),
                    CTACONTABLE = txtCTACONTABLE.Text.Trim(),
                    USUARIO_CREA = Configuracion.UsuarioActual,
                    USUARIO_ACT = Configuracion.UsuarioActual,
                    ACTIVA = chkActiva.Checked,
                    CORRELATIVO_CHEQUE = string.IsNullOrEmpty(txtCORRELATIVO_CHEQUE.Text) ? 0 : int.Parse(txtCORRELATIVO_CHEQUE.Text)
            });

                XtraMessageBox.Show("Registro guardado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnCancelar_Click(sender, e);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error al guardar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _idActual = 0;
            FormHelper.LimpiarControles(this);
            txtNUM_CUENTA.Focus();
        }

        // ============================================================
        // Permite solo dígitos (0-9) en el TextBox
        // ============================================================
        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir dígitos y teclas de control (backspace, etc.)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
        private void SoloNumeros_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            string limpio = new string(tb.Text.Where(char.IsDigit).ToArray());

            if (tb.Text != limpio)
            {
                int pos = tb.SelectionStart - (tb.Text.Length - limpio.Length);
                tb.Text = limpio;
                tb.SelectionStart = Math.Max(0, pos);
            }
        }

        // ============================================================
        // Busca la cuenta contable por su código (CUENTA) y despliega
        // el nombre en el label/textbox destino. Si no existe, avisa.
        // ============================================================
        private void BuscarCuentaContablePorCodigo(TextBox txtCodigo, TextBox txtNombreDestino)
        {
            string codigo = txtCodigo.Text.Trim();
            if (string.IsNullOrEmpty(codigo))
            {
                txtNombreDestino.Clear();
                return;
            }
            try
            {
                var dt = _dal.EjecutarConsulta("SP_CATALOGO_CUENTA", new
                {
                    ACCION = "OBTENER",
                    CUENTA = codigo
                });

                if (dt.Rows.Count == 0)
                {
                    MostrarValidacion($"La cuenta contable '<b>{codigo}</b>' no existe.");
                    txtNombreDestino.Clear();
                    txtCodigo.Focus();
                    txtCodigo.SelectAll();
                    return;
                }

                var fila = dt.Rows[0];
                bool esDetalle = fila["ES_DETALLE"] != DBNull.Value && Convert.ToBoolean(fila["ES_DETALLE"]);

                if (!esDetalle)
                {
                    MostrarValidacion("No se puede asignar una cuenta acumulativa.");
                    txtNombreDestino.Clear();
                    txtCodigo.Focus();
                    txtCodigo.SelectAll();
                    return;
                }

                txtNombreDestino.Text = fila["NOMBRE_CUENTA"].ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error buscando cuenta contable: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarValidacion(string mensaje)
        {
            MostrarValidacionHtml($"<b>{mensaje}</b>");
        }
        private void MostrarValidacionHtml(string mensaje)
        {
            var args = new XtraMessageBoxArgs
            {
                Caption = "Validación",
                Text = mensaje,    // ya viene con HTML armado
                Buttons = new[] { DialogResult.OK },
                Icon = SystemIcons.Warning,
                AllowHtmlText = DefaultBoolean.True
            };
            XtraMessageBox.Show(args);
        }


    }
}