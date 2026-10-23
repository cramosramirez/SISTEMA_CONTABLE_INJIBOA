using DevExpress.XtraEditors;
using SistemaContable.RP;
using SistemaContable.RP.Bancos.Proveedores;
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
    public partial class frmComprobantesPendientesProvisionar : Form
    {
        public frmComprobantesPendientesProvisionar()
        {
            InitializeComponent();
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmComprobantesPendientesProvisionar_Load(object sender, EventArgs e)
        {
            InicializarDatos();
        }

        private void InicializarDatos()
        {
            dteFECHA_INICIO.DateTime = DateTime.Today;
            dteFECHA_FIN.DateTime = DateTime.Today;
        }

        /// <summary>
        /// Devuelve la salida seleccionada por el usuario según los radio buttons / combo.
        /// </summary>
        private SalidaReporte ObtenerSalidaSeleccionada()
        {
            if (rbPDF.Checked) return SalidaReporte.PDF;
            if (rbEXCEL.Checked) return SalidaReporte.Excel;
            return SalidaReporte.Pantalla;   // default
        }

        private void btnProceder_Click(object sender, EventArgs e)
        {
            if (dteFECHA_INICIO.EditValue == null || dteFECHA_FIN.EditValue == null)
            {
                XtraMessageBox.Show("Debe seleccionar las fechas.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            try
            {
                Cursor = Cursors.WaitCursor;

                var rpt = new rptListadoDocumentosPendientesProvicionar
                {
                    FechaInicial = dteFECHA_INICIO.DateTime,
                    FechaFinal = dteFECHA_FIN.DateTime,
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
