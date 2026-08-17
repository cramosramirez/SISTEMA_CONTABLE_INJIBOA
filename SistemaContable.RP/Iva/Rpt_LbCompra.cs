using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace ERPMH.Iva.rpt
{
    public partial class Rpt_LbCompra : DevExpress.XtraReports.UI.XtraReport
    {
        public Rpt_LbCompra(Object salfec)
        {
            InitializeComponent();
           SqsLibrosIva.Queries["EIVA_RPT_LBCOMPRAS"].Parameters[0].Value = salfec;
        }

    }
}
