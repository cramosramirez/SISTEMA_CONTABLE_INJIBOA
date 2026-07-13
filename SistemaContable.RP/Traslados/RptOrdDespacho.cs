using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Traslados
{
    public partial class RptOrdDespacho : SistemaContable.RP.ReporteBase
    {
        public int _id { get; set; }
        public RptOrdDespacho()
        {
            InitializeComponent();
        }
        public override void CargarDatos()
        {
            DataTable dt = EjecutarSP("[EREPORTES].RPT_ORDENDESPACHO", new
            {
                ACCION = "NR",
                ID_ODENC = _id
            });



            this.DataSource = dt;
            this.DataMember = "";
        }
    }
}
