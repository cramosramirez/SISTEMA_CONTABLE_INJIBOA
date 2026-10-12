using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Partidas
{
    public partial class RptPartida_Movimiento : SistemaContable.RP.ReporteBase
    {
        public string _NID_PARTIDA { get; set; }
        public long _ID_PARTIDA { get; set; }

        public string _TIPO { get; set; }
        public int _ANIO { get; set; }
        public int _MES { get; set; }
        public string _NDESDE { get; set; }
        public string _NHASTA { get; set; }

        public RptPartida_Movimiento()
        {
            InitializeComponent();
        }
        public override void CargarDatos()
        {
           
            DataTable dt = EjecutarSP("[CONTA].RPT_PARTIDA_MOVIMIENTO", new
            {
                NID_PARTIDA = _NID_PARTIDA,
                ID_PARTIDA = _ID_PARTIDA,
                TIPO= _TIPO,
                ANIO= _ANIO,
                MES= _MES,
                NDESDE= _NDESDE,
                NHASTA= _NHASTA

            });



            this.DataSource = dt;
            this.DataMember = "";
        }

    }
}
