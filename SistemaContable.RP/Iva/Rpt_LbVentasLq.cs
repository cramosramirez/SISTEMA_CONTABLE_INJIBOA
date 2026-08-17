using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace ERPMH.Iva.rpt
{
    public partial class Rpt_LbVentasLq : DevExpress.XtraReports.UI.XtraReport
    {
        public Rpt_LbVentasLq(Object salfec)
        {
            InitializeComponent();
            SqsLibrosIva.Queries["EIVA_RPT_LBVENT_LQ"].Parameters[0].Value = salfec;
            SqsLibrosIva.Queries["EIVA_RPT_LBVENT_LQ2"].Parameters[0].Value = salfec;
            SqsLibrosIva.Queries["EIVA_RPT_LBVENT_LQ3"].Parameters[0].Value = salfec;
        }

    }
}
