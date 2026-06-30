using DevExpress.XtraEditors;
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var reporte = new rptListadoDocumentosRecibidos { FechaInicial = dteFECHA_INICIO.DateTime, FechaFinal = dteFECHA_FIN.DateTime };
                reporte.MostrarPreview();
              
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
