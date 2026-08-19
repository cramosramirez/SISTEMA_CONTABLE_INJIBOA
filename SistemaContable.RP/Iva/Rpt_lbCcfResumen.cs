using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Iva
{
    public partial class Rpt_lbCcfResumen : SistemaContable.RP.ReporteBase
    {
        public string _aniodt { get; set; }
        public string _mesdt { get; set; }
        public Rpt_lbCcfResumen()
        {
            InitializeComponent();
           
        }
        public override void CargarDatos()
        {
            DataTable dt = EjecutarSP("[EIVA].RTP_VENTAS_RESUMEN", new
            {
                anio = _aniodt,
                mes = _mesdt
            });
            this.DataSource = dt;
            this.DataMember = "";
        }
       
    }
}
