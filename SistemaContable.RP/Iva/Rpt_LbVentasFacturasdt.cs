using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Iva
{
    public partial class Rpt_LbVentasFacturasdt : SistemaContable.RP.ReporteBase
    {
        public string _anio { get; set; }
        public string _mes { get; set; }
        public Rpt_LbVentasFacturasdt()
        {
            InitializeComponent();
        }
        public override void CargarDatos()
        {
            DataTable dt = EjecutarSP("[EIVA].[RPT_LBVENT_FA_DET]", new
            {
                anio = _anio,
                mes = _mes
            });
            this.DataSource = dt;
            this.DataMember = "";
        }

    }
}
