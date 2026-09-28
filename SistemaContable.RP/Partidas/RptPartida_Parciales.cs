using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;

namespace SistemaContable.RP.Partidas
{
    public partial class RptPartida_Parciales : SistemaContable.RP.ReporteBase
    {
        public string _NID_PARTIDA { get; set; }
        public string _Titulo { get; set; }
        public RptPartida_Parciales()
        {
            InitializeComponent();
        }
        public override void CargarDatos()
        {
            lbTitulo.Text = _Titulo;
            DataTable dt = EjecutarSP("[CONTA].RPT_PARTIDA_PARCIALES", new
            {
                NID_PARTIDA = _NID_PARTIDA
            });



            this.DataSource = dt;
            this.DataMember = "";
        }
        private void xrLabelParciales_BeforePrint(object sender, PrintEventArgs e)
        {
            XRLabel lbl = (XRLabel)sender;

            int nivel = 0;

            object v = GetCurrentColumnValue("NIVEL");

            if (v != null)
                nivel = Convert.ToInt32(v);

            float rightEdge = 608.0F; // límite derecho de la columna PARCIALES

            lbl.LeftF = rightEdge - lbl.WidthF - (nivel * 15F);
        }
    }
}
