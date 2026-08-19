using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Iva
{
    public partial class Rpt_LbVentasCCF : SistemaContable.RP.ReporteBase
    {
        public string _anio { get; set; }
        public string _mes { get; set; }
        public Rpt_LbVentasCCF()
        {
            InitializeComponent();
           
           
        }
        public override void CargarDatos()
        {
            DataTable dt = EjecutarSP("[EIVA].RPT_LBVENTA_CCF", new
            {
                anio = _anio,
                mes = _mes
            });
            this.DataSource = dt;
            this.DataMember = "";
        }
        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Rpt_lbCcfResumen resumen = (Rpt_lbCcfResumen)xrSubreport1.ReportSource;
            if (resumen != null)
            {
                resumen._aniodt = this._anio;
                resumen._mesdt = this._mes;
                resumen.CargarDatos();
            }

        }
    }
}
