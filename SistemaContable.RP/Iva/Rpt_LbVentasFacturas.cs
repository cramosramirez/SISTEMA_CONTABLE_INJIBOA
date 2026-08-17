using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace ERPMH.Iva.rpt
{
    public partial class Rpt_LbVentasFacturas : DevExpress.XtraReports.UI.XtraReport
    {
        public Rpt_LbVentasFacturas(Object salfec)
        {
            InitializeComponent();
           SqsLibrosIva.Queries["EIVA_RPT_LBVENT_FA_RESUMEN"].Parameters[0].Value = salfec;
        }

    }
}
