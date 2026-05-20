using DevExpress.XtraEditors;
using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Bancos
{
    public partial class frmCuentasBancarias : DevExpress.XtraEditors.XtraForm
    {
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
                    ParametrosExtra = new { ES_DETALLE = true }
                },
                fila =>
                {
                    txtCTACONTABLE.Text = fila["CUENTA"].ToString();                    
                }
            );
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
                    USUARIO_ACT = Configuracion.UsuarioActual
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
    }
}