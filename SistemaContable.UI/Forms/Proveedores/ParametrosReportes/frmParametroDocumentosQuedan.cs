using DevExpress.XtraEditors;
using SistemaContable.RP;
using SistemaContable.RP.Bancos.Proveedores;
using System;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Proveedores.ParametrosReportes
{
    public partial class frmParametroDocumentosQuedan : Form
    {
        public frmParametroDocumentosQuedan()
        {
            InitializeComponent();
        }
        
        private void frmParametroDocumentosQuedan_Load(object sender, EventArgs e)
        {
            InicializarDatos();

        }
        private void InicializarDatos()
        {
            dteFECHA_INICIO.DateTime = DateTime.Today;
            dteFECHA_FIN.DateTime = DateTime.Today;
        }
               
        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Close();
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

                var rpt = new rptListadoComprobantesRecibidos
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

        /// <summary>
        /// Devuelve la salida seleccionada por el usuario según los radio buttons / combo.
        /// </summary>
        private SalidaReporte ObtenerSalidaSeleccionada()
        {
            if (rbPDF.Checked) return SalidaReporte.PDF;
            if (rbEXCEL.Checked) return SalidaReporte.Excel;
            return SalidaReporte.Pantalla;   // default
        }
    }
}
