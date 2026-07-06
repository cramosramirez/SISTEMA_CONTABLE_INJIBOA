using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Traslados.NotaRemision
{
    public partial class RptNotaRemision_Traslado_Bodegas : SistemaContable.RP.ReporteBase
    {
        public int _id { get; set; }
        public RptNotaRemision_Traslado_Bodegas()
        {
            InitializeComponent();
        }
        public override void CargarDatos()
        {
            DataTable dt = EjecutarSP("[EREPORTES].RPT_NOTA_REMISION", new
            {
                ACCION = "NRBD",
                ID_NTREMISIONENC = _id
            });

            

            this.DataSource = dt;
            this.DataMember = "";
        }
    }
}
