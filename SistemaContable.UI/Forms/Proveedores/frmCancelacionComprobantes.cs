using DevExpress.XtraEditors;
using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Proveedores
{
    public partial class frmCancelacionComprobantes : Form
    {
        private readonly DALBase _dal = new DALBase();        
        public int _idCcfCompra { get; set; } = 0;
        private EstadoFormulario _estadoActual = EstadoFormulario.Inicializar;
        private readonly ParametrosCcfBusqueda _parametrosCcf = new ParametrosCcfBusqueda();
        private enum EstadoFormulario
        {
            Inicializar,
            Grabar,
            Ignorar,
            Buscar,
            CancelarQuedan,
            Anterior,
            Siguiente,
            Modificar,
            Eliminar,
            Agregar            
        }
        public frmCancelacionComprobantes()
        {
            InitializeComponent();
        }

        private void frmCancelacionComprobantes_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);

            // Búsqueda de Proveedor (igual patrón que frmDocumentoCompra)
            FormHelper.RegistrarBusqueda(
                txtPROVEEDOR,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_ENTIDAD",
                    Accion = "BUSCAR",
                    Columnas = new Dictionary<string, string>
                    {
                        { "CODIGO_ENTIDAD", "PROVEEDOR" },
                        { "NOMBRE",         "NOMBRE"    },
                        { "NIT",            "NIT"       }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "CODIGO_ENTIDAD", 100 },
                        { "NOMBRE",         300 },
                        { "NIT",            120 }
                    },
                    ParametrosExtra = new { ROL = "PRO" }
                },
                fila => AsignarProveedor(fila)
            );

            // Búsqueda de CCF del proveedor ya seleccionado
            FormHelper.RegistrarBusqueda(
                txtCOD_GENERACION_CCF,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_CREDITO_FISCAL_COMPRA",
                    Accion = "LISTAR_CCF_DE_PROVEEDOR",
                    Columnas = new Dictionary<string, string>
                    {
                        { "TIPO_DTE",       "TIPO" },
                        { "COD_GENERACION", "COD. GENERACIÓN" },
                        { "NUM_QUEDAN",     "QUEDAN" },
                        { "FECHA_VENCE",    "VENCE" },
                        { "SALDO_FMT",      "SALDO" }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "TIPO_DTE",        60 },
                        { "COD_GENERACION", 350 },
                        { "NUM_QUEDAN",      70 },
                        { "FECHA_VENCE",     90 },
                        { "SALDO_FMT",      100 }
                    },
                    ParametrosExtra = _parametrosCcf   // ← objeto mutable, no anónimo
                },
                fila =>
                {
                    int idCcfSeleccionado = Convert.ToInt32(fila["ID_CCF_COMPRA"]);
                    CargarCcfParaAplicar(idCcfSeleccionado);
                }
            );
            ConfigurarTextBoxDecimal(txt_A_PAGAR);
            ConfigurarCRUD(EstadoFormulario.Inicializar);
        }

        private void AsignarProveedor(DataRow fila)
        {            
            txtPROVEEDOR.Text = fila["CODIGO_ENTIDAD"].ToString();
            txtNOMBRE_PROVEEDOR.Text = fila["NOMBRE"].ToString();
            _parametrosCcf.CODIGO_ENTIDAD = fila["CODIGO_ENTIDAD"].ToString();
            LimpiarSeleccionCcf();
        }

        private void txtPROVEEDOR_Leave(object sender, EventArgs e)
        {
            string codigo = txtPROVEEDOR.Text.Trim();
            if (codigo == "*") return;
            if (string.IsNullOrWhiteSpace(codigo))
            {
                LimpiarProveedorYCcf();
                return;
            }
            try
            {
                var dt = _dal.EjecutarConsulta("SP_ENTIDAD", new
                {
                    ACCION = "BUSCAR_POR_CODIGO",
                    FILTRO = codigo,
                    ROL = "PRO"
                });

                if (dt.Rows.Count > 0)
                    AsignarProveedor(dt.Rows[0]);
                else
                {
                    LimpiarProveedorYCcf();
                    XtraMessageBox.Show($"No se encontró el proveedor con código '{codigo}'.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPROVEEDOR.Focus();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error al buscar proveedor: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarSeleccionCcf()
        {
            _idCcfCompra = 0;
            txtTIPO_DTE_CCF.Text = string.Empty;
            txtSELLO_RECIBIDO_CCF.Text = string.Empty;
            txtCOD_GENERACION_CCF.Text = string.Empty;
            txtNUM_CONTROL_CCF.Text = string.Empty;
            txtNUM_QUEDAN.Text = string.Empty;
            txtNUM_CHEQUE.Text = string.Empty;
            mskFECHA_CHEQUE.Text = string.Empty;
            txtNUM_CTA_BANCARIA.Text = string.Empty;
            txtNOMBRE_CTA_BANCARIA.Text = string.Empty;
            txtVALOR.Text = string.Empty;
            txtSALDO.Text = string.Empty;
            txtABONOS.Text  = string.Empty;
            txtRETENCION.Text  = string.Empty;
            txtSALDO.Text = string.Empty;
            txt_A_PAGAR.Text = string.Empty;
        }

        private void LimpiarProveedorYCcf()
        {            
            txtNOMBRE_PROVEEDOR.Text = string.Empty;
            LimpiarSeleccionCcf();
        }

        private void CargarCcfParaAplicar(int idCcfCompra)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                DataTable dt = _dal.EjecutarConsulta("SP_CREDITO_FISCAL_COMPRA", new
                {
                    ACCION = "OBTENER",
                    ID_CCF_COMPRA = idCcfCompra
                });

                if (dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No se encontró el documento seleccionado.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataRow r = dt.Rows[0];          
                _idCcfCompra = idCcfCompra;                       

                // ---------- Datos del CCF al que se aplicará la NC/ND ----------
                txtTIPO_DTE_CCF.Text = AsString(r["TIPO_DTE_ABREV"]);
                txtSELLO_RECIBIDO_CCF.Text = AsString(r["SELLO_RECIBIDO"]);
                txtCOD_GENERACION_CCF.Text = AsString(r["COD_GENERACION"]);
                txtNUM_CONTROL_CCF.Text = AsString(r["NUM_CONTROL"]);
                txtNUM_QUEDAN.Text = AsString(r["NUM_QUEDAN"]);
                txtNUM_CHEQUE.Text = AsString(r["NUM_CHEQUE"]);
                mskFECHA_CHEQUE.Text = AsFecha(r["FECHA_CHEQUE"]);
                txtNUM_CTA_BANCARIA.Text = AsString(r["NUM_CTA_BANCARIA"]);
                txtNOMBRE_CTA_BANCARIA.Text = AsString(r["NOMBRE_CTA_BANCARIA"]);              

                AsignarDecimal(txtVALOR, ToDecimal(r["TOTAL"]));
                AsignarDecimal(txtRETENCION, ToDecimal(r["IVAR"]));
                AsignarDecimal(txtSALDO, ToDecimal(r["SALDO"]));
                if (r["A_PAGAR"] == DBNull.Value)                
                    AsignarDecimal(txt_A_PAGAR, ToDecimal(r["MONTO_CHEQUE"]));
                else
                    AsignarDecimal(txt_A_PAGAR, ToDecimal(r["A_PAGAR"]));

                btnModificar.Enabled = true;
                btnEliminar.Enabled = true; 
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar el documento:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void ConfigurarCRUD(EstadoFormulario estado, bool chequeAnulado = false)
        {
            _estadoActual = estado;
            switch (estado)
            {
                case EstadoFormulario.Inicializar:
                    txtPROVEEDOR.Text = string.Empty;
                    txtPROVEEDOR.Enabled = false;
                    txtCOD_GENERACION_CCF.Enabled = false;
                    txt_A_PAGAR.Enabled = false;
                    _idCcfCompra = 0;
                    LimpiarProveedorYCcf();
                    HabilitarBotones(
                        grabar: false,
                        ignorar: false,
                        buscar: true,
                        cancelarQuedan: true,
                        anterior: true,
                        siguiente: true,
                        modificar: true,
                        eliminar: true,
                        agregar: true,
                        finalizar: true
                       );
                    break;

                case EstadoFormulario.Grabar:
                    break;

                case EstadoFormulario.Agregar:
                    txtPROVEEDOR.Enabled = true;
                    txtCOD_GENERACION_CCF.Enabled = true;
                    txt_A_PAGAR.Enabled = true;
                    LimpiarProveedorYCcf();
                    HabilitarBotones(
                        grabar: true,
                        ignorar: true,
                        buscar: false,
                        cancelarQuedan: false,
                        anterior: false,
                        siguiente: false,
                        modificar: false,
                        eliminar: false,
                        agregar: false,
                        finalizar: true
                       );
                    break;

                case EstadoFormulario.Buscar:
                    txtPROVEEDOR.Enabled = true;
                    txtCOD_GENERACION_CCF.Enabled = true;
                    txt_A_PAGAR.Enabled = false;
                    LimpiarProveedorYCcf();
                    HabilitarBotones(
                        grabar: false,
                        ignorar: true,
                        buscar: false,
                        cancelarQuedan: true,
                        anterior: false,
                        siguiente: false,
                        modificar: false,
                        eliminar: false,
                        agregar: false,
                        finalizar: true
                       );
                    break;                    

                case EstadoFormulario.Modificar:
                    txt_A_PAGAR.Enabled = true;                    
                    HabilitarBotones(
                        grabar: true,
                        ignorar: true,
                        buscar: false,
                        cancelarQuedan: true,
                        anterior: false,
                        siguiente: false,
                        modificar: false,
                        eliminar: false,
                        agregar: false,
                        finalizar: true
                       );
                    break;                    

                case EstadoFormulario.Ignorar:
                    ConfigurarCRUD(EstadoFormulario.Inicializar);
                    break;

                
            }
        }
        private void HabilitarBotones(
            bool grabar,
            bool ignorar,
            bool buscar,
            bool cancelarQuedan,
            bool anterior,
            bool siguiente,
            bool modificar,
            bool eliminar,
            bool agregar,
            bool finalizar)
        {
            btnGrabar.Enabled = grabar;
            btnIgnorar.Enabled = ignorar;
            btnBuscar.Enabled = buscar;
            btnCancelarQuedan.Enabled = cancelarQuedan;

            btnAnterior.Enabled = anterior;
            btnSiguiente.Enabled = siguiente;
            btnModificar.Enabled = modificar;
            btnEliminar.Enabled = eliminar;
            btnAgregar.Enabled = agregar;
            btnFinalizar.Enabled = finalizar;          
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ConfigurarCRUD(EstadoFormulario.Buscar);
            txtPROVEEDOR.Focus();  
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static string AsString(object valor)
        {
            return valor == null || valor == DBNull.Value ? "" : valor.ToString();
        }
        private static string AsFecha(object valor)
        {
            if (valor == null || valor == DBNull.Value) return "";
            return Convert.ToDateTime(valor).ToString("dd/MM/yyyy");
        }
        private void AsignarDecimal(TextBox tb, decimal valor)
        {
            tb.Text = valor == 0 ? "" : valor.ToString("N2");
        }        private static decimal ToDecimal(object valor)
        {
            if (valor == null || valor == DBNull.Value) return 0;
            return Convert.ToDecimal(valor);
        }
        private decimal ObtenerDecimal(TextBox tb)
        {
            if (string.IsNullOrWhiteSpace(tb.Text)) return 0;
            return decimal.TryParse(tb.Text, out decimal v) ? v : 0;
        }

        private void ConfigurarTextBoxDecimal(params TextBox[] textboxes)
        {
            foreach (var tb in textboxes)
            {
                tb.Enter += TxtDecimal_Enter;
                tb.KeyPress += TxtDecimal_KeyPress;
                tb.Leave += TxtDecimal_Leave;
            }
        }

        private void TxtDecimal_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void TxtDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
            {
                e.Handled = true;
                return;
            }
            // Solo un punto decimal
            if (e.KeyChar == '.' && tb.Text.Contains("."))
                e.Handled = true;
        }
        private void TxtDecimal_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;

            // Vacío -> limpio
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "";
                return;
            }
            // Parsea (si no parsea, limpia)
            if (!decimal.TryParse(tb.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
            {
                tb.Text = "";
                return;
            }
            // Cero -> limpio (cubre "0", "0.", "0.0", "0.00", "00", etc.)
            if (valor == 0m)
            {
                tb.Text = "";
                return;
            }
            // Valor válido -> formato N2
            tb.Text = valor.ToString("N2");
        }

        private void btnIgnorar_Click(object sender, EventArgs e)
        {
            ConfigurarCRUD(EstadoFormulario.Ignorar);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ConfigurarCRUD(EstadoFormulario.Agregar);
            txtPROVEEDOR.Focus();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if(txtPROVEEDOR.Text == string.Empty)
            {
                XtraMessageBox.Show("Debe seleccionar un proveedor.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPROVEEDOR.Focus();
                return;
            }

            if (txtSELLO_RECIBIDO_CCF.Text == string.Empty)
            {
                XtraMessageBox.Show("Debe seleccionar un comprobante.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCOD_GENERACION_CCF.Focus();
                return;
            }

            if (ObtenerDecimal(txt_A_PAGAR) == 0)
            {
                XtraMessageBox.Show("El valor a pagar no puede ser cero.", "Validación",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_A_PAGAR.Focus();
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                var parametros = new
                {
                    ACCION = "CAMBIAR_MONTO_A_PAGAR",
                    ID_CCF_COMPRA = _idCcfCompra,
                    A_PAGAR = ObtenerDecimal(txt_A_PAGAR)
                };

                _dal.EjecutarConsulta("SP_CREDITO_FISCAL_COMPRA", parametros);              
                
                XtraMessageBox.Show("Documento guardado correctamente.",
                    "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConfigurarCRUD(EstadoFormulario.Inicializar);
            }
            catch (SqlException sqlEx)
            {
                XtraMessageBox.Show(sqlEx.Message,
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Ocurrió un error al guardar el documento:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ConfigurarCRUD(EstadoFormulario.Modificar);
            txt_A_PAGAR.Focus();  
        }
    }
}
