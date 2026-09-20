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
        public string _Titulo { get; set; }
        public RptPartida_Movimiento()
        {
            InitializeComponent();
        }
        public override void CargarDatos()
        {
            lbTitulo.Text = _Titulo;
            DataTable dt = EjecutarSP("[CONTA].RPT_PARTIDA_MOVIMIENTO", new
            {
                NID_PARTIDA = _NID_PARTIDA
            });



            this.DataSource = dt;
            this.DataMember = "";
        }

    }
}
