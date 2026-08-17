using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace ERPMH.Iva.rpt
{
    public partial class Rpt_LbVentasFacturasdt : DevExpress.XtraReports.UI.XtraReport
    {
        public Rpt_LbVentasFacturasdt(Object salfec)
        {
            InitializeComponent();
           SqsLibrosIva.Queries["EIVA_RPT_LBVENT_FA_DET"].Parameters[0].Value = salfec;
        }

    }
}
