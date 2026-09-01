using DevExpress.XtraEditors;
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

namespace SistemaContable.UI.Forms.Proveedores.ParametrosReportes
{
    public partial class frmExportarDocumentosQuedan : Form
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
       
        public frmExportarDocumentosQuedan()
        {
            InitializeComponent();
        }

        private void InicializarDatos()
        {
            DateTime hoy = DateTime.Today;

            dteFECHA_INICIO.DateTime = new DateTime(hoy.Year, hoy.Month, 1);
            dteFECHA_FIN.DateTime = hoy;
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var reporte = new rptListadoDocumentosRecibidos
                {
                    FechaInicial = dteFECHA_INICIO.DateTime,
                    FechaFinal = dteFECHA_FIN.DateTime,
                    ClasificarPorOrden = true
                };
                bool exportado = reporte.ExportarAExcel();
                if (exportado)
                {
                    XtraMessageBox.Show(
                        "Archivo exportado correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error al exportar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void frmExportarDocumentosQuedan_Load(object sender, EventArgs e)
        {
            InicializarDatos();
        }
    }
}
