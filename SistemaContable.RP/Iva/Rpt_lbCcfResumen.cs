using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace ERPMH.Iva.rpt
{
    public partial class Rpt_lbCcfResumen : DevExpress.XtraReports.UI.XtraReport
    {
        public Rpt_lbCcfResumen()
        {
            InitializeComponent();
           
        }
        public void CargarDetalle(string salfec)

        {
           
          sdsListResumenlib.Queries["EIVA_RTP_VENTAS_RESUMEN"].Parameters[0].Value = salfec;
        }
    }
}
