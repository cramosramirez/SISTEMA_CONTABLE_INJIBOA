using DevExpress.XtraEditors;
using SistemaContable.DAL;
using SistemaContable.RP;
using SistemaContable.RP.Bancos.Proveedores;
using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Reportes
{
    public partial class frmPagosVencidosPorFecha : Form
    {
        private readonly DALBase _dal = new DALBase();
        public frmPagosVencidosPorFecha()
        {
            InitializeComponent();
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool AsignarProveedor(DataRow fila, TextBox txtCODIGO, TextBox txtNOMBRE)
        {

            txtCODIGO.Text = fila["CODIGO_ENTIDAD"].ToString();
            txtNOMBRE.Text = fila["NOMBRE"].ToString();
            return true;
        }

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            HabilitarRango();
        }

        private void rbRango_CheckedChanged(object sender, EventArgs e)
        {
            HabilitarRango();
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

        private void HabilitarRango()
        {
            if (rbTodos.Checked)
            {
                txtDESDE.ReadOnly = true;
                txtHASTA.ReadOnly = true;
                txtDESDE.Text = "";
                txtHASTA.Text = "";
                txtDESDE_DESCRIPCION.Text = "";
                txtHASTA_DESCRIPCION.Text = "";
            }
            else
            {
                txtDESDE.ReadOnly = false;
                txtHASTA.ReadOnly = false;
            }
        }

        private void frmPagosVencidosPorFecha_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);

            dteFECHA_INICIO.EditValue = DateTime.Today;
            dteFECHA_FIN.EditValue = DateTime.Today;

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

        private void btnProceder_Click(object sender, EventArgs e)
        {
            if (dteFECHA_INICIO.EditValue == null || dteFECHA_FIN.EditValue == null)
            {
                XtraMessageBox.Show("Debe seleccionar las fechas.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (rbRango.Checked && (txtDESDE.Text.Trim() == "" || txtHASTA.Text.Trim() == ""))
            {
                XtraMessageBox.Show("Debe indicar el rango de proveedor.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                var rpt = new rptListadoPagosPendFechaRecepcion
                {
                    FechaInicial = dteFECHA_INICIO.DateTime,
                    FechaFinal = dteFECHA_FIN.DateTime,                     
                    TipoFiltro = rbRECEPCION.Checked ? "R" : rbFACTURA.Checked ? "F" : "V",
                    Desde = txtDESDE.Text,
                    Hasta = txtHASTA.Text,
                    IncluirYaPagados = chkIncluirYaPagados.Checked,
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
    }
}
