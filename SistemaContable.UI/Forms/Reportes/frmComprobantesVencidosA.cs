using DevExpress.XtraEditors;
using SistemaContable.DAL;
using SistemaContable.RP;
using SistemaContable.RP.Bancos.Proveedores;
using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Reportes
{
    public partial class frmComprobantesVencidosA : Form
    {
        private readonly DALBase _dal = new DALBase();
        public frmComprobantesVencidosA()
        {
            InitializeComponent();
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmComprobantesVencidosA_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);
            // Quitar las flechitas up/down
            spDE.Properties.Buttons.Clear();
            spA.Properties.Buttons.Clear();
            // Solo enteros (sin decimales)
            spDE.Properties.IsFloatValue = false;
            spA.Properties.IsFloatValue = false;
            // Opcional: limitar rango
            spDE.Properties.MinValue = 1;
            spDE.Properties.MaxValue = 1000;
            spA.Properties.MinValue = 1;
            spA.Properties.MaxValue = 1000;
            
            spDE.Value = 1;
            spA.Value = 30;

            // Búsqueda * + Enter en txtDESDE y txtHASTA
            FormHelper.RegistrarBusqueda(
                txtDESDE,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_ENTIDAD",
                    Accion = "BUSCAR_CONTRIBUYENTES",
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
                fila => AsignarProveedor(fila, txtDESDE, txtDESDE_DESCRIPCION)
            );

            FormHelper.RegistrarBusqueda(
                txtHASTA,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_ENTIDAD",
                    Accion = "BUSCAR_CONTRIBUYENTES",
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
                fila => AsignarProveedor(fila, txtHASTA, txtHASTA_DESCRIPCION)
            );
        }

        private bool AsignarProveedor(DataRow fila, TextBox txtCODIGO, TextBox txtNOMBRE)
        {

            txtCODIGO.Text = fila["CODIGO_ENTIDAD"].ToString();
            txtNOMBRE.Text = fila["NOMBRE"].ToString();
            return true;
        }

        private void ObtenerProveedor(TextBox txtCODIGO, TextBox txtNOMBRE)
        {
            string codigo = txtCODIGO.Text.Trim();
            if (codigo == "*") return;
            if (string.IsNullOrWhiteSpace(codigo))
            {
                txtNOMBRE.Text = "";
                return;
            }
            try
            {
                var dt = _dal.EjecutarConsulta("SP_ENTIDAD", new
                {
                    ACCION = "BUSCAR_POR_CODIGO_CONTRIBUYENTE",
                    FILTRO = codigo,
                    ROL = "PRO"
                });

                if (dt.Rows.Count > 0)
                    AsignarProveedor(dt.Rows[0], txtCODIGO, txtNOMBRE);
                else
                {
                    txtNOMBRE.Text = "";
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        $"No se encontró el proveedor con código '{codigo}'.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCODIGO.Focus();
                }
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    $"Error al buscar proveedor: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private SalidaReporte ObtenerSalidaSeleccionada()
        {
            if (rbPDF.Checked) return SalidaReporte.PDF;
            if (rbEXCEL.Checked) return SalidaReporte.Excel;
            return SalidaReporte.Pantalla;   // default
        }

        private void txtDESDE_Leave(object sender, EventArgs e)
        {
            ObtenerProveedor(txtDESDE, txtDESDE_DESCRIPCION);
        }
        private void txtHASTA_Leave(object sender, EventArgs e)
        {
            ObtenerProveedor(txtHASTA, txtHASTA_DESCRIPCION);
        }

        private void btnProceder_Click(object sender, EventArgs e)
        {
            if (spDE.Value == 0 || spA.Value == 0)
            {
                XtraMessageBox.Show("Debe completar el rango de dias.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (spDE.Value > spA.Value)
            {
                XtraMessageBox.Show("El dia inicial no puede ser mayor que el final",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                var rpt = new rptListadoPagosVencidosProveedor
                {
                    Del_dia = Convert.ToInt32(spDE.Value),
                    Al_dia = Convert.ToInt32(spA.Value),                    
                    EsResumen = rbRESUMEN.Checked ? true : false,
                    Desde = txtDESDE.Text,
                    Hasta = txtHASTA.Text,
                    Salida = ObtenerSalidaSeleccionada()
                };
                rpt.Ejecutar();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al imprimir:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}
