using SistemaContable.RP.Iva;
using SistemaContable.UI.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using SistemaContable.DAL;

namespace SistemaContable.UI.Forms.Iva
{
    public partial class frmReportes : Form
    {
        private void InicializarMeses()
        {
            cb_Mes.DataSource = FormHelper.ObtenerMeses();
            cb_Mes.DisplayMember = "Mes";
            cb_Mes.ValueMember = "Value";
            cb_Mes.SelectedValue = DateTime.Now.Month.ToString("00");
        }
        private void InicializarDatos()
        {
            txt_Anio.Text = DateTime.Now.Year.ToString();
            int mesAnterior = DateTime.Now.Month - 1;
            // Manejar el caso de enero (mes 1), para que reste y quede diciembre (12)
            if (mesAnterior == 0)
            {
                mesAnterior = 12;
            }
            cb_Mes.SelectedValue = mesAnterior.ToString("00");
            ck_DtExportacion.Checked = false;
        }
        public frmReportes()
        {
            InitializeComponent();
            InicializarMeses();
            InicializarDatos();
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {

            lstReportes.SelectedIndex = 0;
        }

        private void lstReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string reporte = lstReportes.SelectedItem?.ToString();
            /*
             Libro de Ventas a Consumidor Final
Libro de Ventas a Contribuyentes
Libro de Ventas de Liquidaciones
Libro de Compras
             */

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                string anio = txt_Anio.Text;
                string mes = cb_Mes.SelectedValue?.ToString();
                string tipoReporte = lstReportes.SelectedItem?.ToString();
                

                if (string.IsNullOrWhiteSpace(anio))
                {
                    Alertas.Error("Año es requerido");
                    return;
                }

                if (string.IsNullOrWhiteSpace(mes))
                {
                    Alertas.Error("Mes es requerido");
                    return;
                }

                if (string.IsNullOrWhiteSpace(tipoReporte))
                {
                    Alertas.Error("Tipo de Libro es requerido");
                    return;
                }

                switch (tipoReporte)
                {
                    case "Libro de Ventas a Consumidor Final" when !ck_DtExportacion.Checked:
                        Cursor = Cursors.WaitCursor;
                        var reporte = new Rpt_LbVentasFacturas { _anio = anio, _mes= mes };
                        reporte.MostrarPreview();
                        break;

                    case "Libro de Ventas a Consumidor Final" when ck_DtExportacion.Checked:
                        Cursor = Cursors.WaitCursor;
                        var reporteExp = new Rpt_LbVentasFacturasdt { _anio = anio, _mes = mes };
                        reporteExp.MostrarPreview();
                        break;

                    case "Libro de Ventas a Contribuyentes":
                        Cursor = Cursors.WaitCursor;
                        var reporteCcf = new Rpt_LbVentasCCF { _anio = anio, _mes = mes };
                        reporteCcf.MostrarPreview();
                        break;

                    case "Libro de Ventas de Liquidaciones":
                        Cursor = Cursors.WaitCursor;
                        var reporteQl = new Rpt_LbVentasLq { _anio = anio, _mes = mes };
                        reporteQl.MostrarPreview();
                        break;
                    case "Libro de Compras":
                        Cursor = Cursors.WaitCursor;
                        var reporteC = new Rpt_LbCompra { _anio = anio, _mes = mes };
                        reporteC.MostrarPreview();
                        break;
                }
               
            }
            catch (Exception ex)
            {               
                Alertas.Error(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
