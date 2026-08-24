using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Traslados
{
    public partial class RptSalidaBodega : SistemaContable.RP.ReporteBase
    {
        public int _id { get; set; }
        public RptSalidaBodega()
        {
            InitializeComponent();
        }
        public override void CargarDatos()
        {
            DataTable dt = EjecutarSP("[EREPORTES].RPT_DESPACHO_BODEGA", new
            {
                ACCION = "DB",
                ID_DBENC = _id
            });



            this.DataSource = dt;
            this.DataMember = "";
        }
    }
}
