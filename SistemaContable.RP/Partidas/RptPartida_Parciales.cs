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
        public string _ID_PARTIDA { get; set; }
        public string _Titulo { get; set; }

        // acumulado de las filas ya impresas (cuentas de nivel 0)
        private decimal _acumCargo, _acumAbono;

        public RptPartida_Parciales()
        {
            InitializeComponent();

            this.BeforePrint += (s, e) => { _acumCargo = _acumAbono = 0; };

            // acumula cada fila que trae cargo/abono
            Detail.BeforePrint += (s, e) =>
            {
                object c = GetCurrentColumnValue("CARGO");
                object a = GetCurrentColumnValue("ABONO");
                if (c != null && c != DBNull.Value) _acumCargo += Convert.ToDecimal(c);
                if (a != null && a != DBNull.Value) _acumAbono += Convert.ToDecimal(a);
            };

            // al llegar al pie de la página = PASAN
            PageFooter.BeforePrint += (s, e) =>
            {
                xrTableCellPasanCargo.Text = _acumCargo.ToString("N2");
                xrTableCellPasanAbono.Text = _acumAbono.ToString("N2");
            };

            // la página siguiente muestra ese mismo valor como VIENEN
            PageHeader.BeforePrint += (s, e) =>
            {
                xrTableCellVienenCargo.Text = _acumCargo.ToString("N2");
                xrTableCellVienenAbono.Text = _acumAbono.ToString("N2");
            };
        }

        public override void CargarDatos()
        {
            lbTitulo.Text = _Titulo;
            DataTable dt = EjecutarSP("[CONTA].RPT_PARTIDA_PARCIALES", new
            {
                NID_PARTIDA = _NID_PARTIDA,
                ID_PARTIDA = _ID_PARTIDA
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