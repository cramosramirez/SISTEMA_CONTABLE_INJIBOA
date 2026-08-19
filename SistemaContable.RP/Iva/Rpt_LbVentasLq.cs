using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Iva
{
    public partial class Rpt_LbVentasLq : SistemaContable.RP.ReporteBase
    {
        public string _anio { get; set; }
        public string _mes { get; set; }
        public Rpt_LbVentasLq()
        {
            InitializeComponent();
            
        }

        public override void CargarDatos()
        {
            DataTable dt = EjecutarSP("[EIVA].RPT_LBVENT_LQ", new
            {
                anio = _anio,
                mes = _mes
            });
            this.DtLQ.DataSource = dt;
            this.DtLQ.DataMember = "";

            DataTable dt2 = EjecutarSP("[EIVA].RPT_LBVENT_LQ2", new
            {
                anio = _anio,
                mes = _mes
            });
            this.Dtlq2.DataSource = dt2;
            this.Dtlq2.DataMember = "";

            DataTable dt3 = EjecutarSP("[EIVA].RPT_LBVENT_LQ3", new
            {
                anio = _anio,
                mes = _mes
            });
            this.Dllq3.DataSource = dt3;
            this.Dllq3.DataMember = "";
        }
    }
}
