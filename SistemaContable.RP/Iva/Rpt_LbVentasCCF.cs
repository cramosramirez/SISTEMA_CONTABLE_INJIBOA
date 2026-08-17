using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace ERPMH.Iva.rpt
{
    public partial class Rpt_LbVentasCCF : DevExpress.XtraReports.UI.XtraReport
    {
        public string _salfec = "";
        public Rpt_LbVentasCCF(Object salfec)
        {
            InitializeComponent();
            _salfec = Convert.ToString(salfec);

           SqsLibrosIva.Queries["EIVA_RPT_LBVENTA_CCF"].Parameters[0].Value = salfec;
           
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Rpt_lbCcfResumen resumen = (Rpt_lbCcfResumen)xrSubreport1.ReportSource;
            if (resumen != null)
            {
               
                    resumen.CargarDetalle(_salfec);
               
            }

        }
    }
}
