using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing.Printing;

namespace SistemaContable.RP.Partidas
{
    public partial class RptPartida_Parciales : SistemaContable.RP.ReporteBase
    {
        public string _NID_PARTIDA { get; set; }
        public string _ID_PARTIDA { get; set; }
        public string _Titulo { get; set; }

        // Valores mostrados en PASAN y VIENEN
        private decimal _vienenCargo = 0;
        private decimal _vienenAbono = 0;

        private decimal _pasanCargo = 0;
        private decimal _pasanAbono = 0;

        public RptPartida_Parciales()
        {
            InitializeComponent();

            this.BeforePrint += (s, e) =>
            {
                _vienenCargo = 0;
                _vienenAbono = 0;
                _pasanCargo = 0;
                _pasanAbono = 0;
            };

            // Guarda el último acumulado impreso
            Detail.BeforePrint += (s, e) =>
            {
                object cargo = GetCurrentColumnValue("CARGO_ACUM");
                object abono = GetCurrentColumnValue("ABONO_ACUM");

                _pasanCargo = cargo == DBNull.Value || cargo == null
                    ? 0
                    : Convert.ToDecimal(cargo);

                _pasanAbono = abono == DBNull.Value || abono == null
                    ? 0
                    : Convert.ToDecimal(abono);
            };

            // Muestra VIENEN
            PageHeader.BeforePrint += (s, e) =>
            {
                bool mostrarVienen = _vienenCargo != 0 || _vienenAbono != 0;

                xrTableCellVienenTitulo.Text = mostrarVienen ? "VIENEN..." : "";
                xrTableCellVienenCargo.Text =
                    _vienenCargo == 0 ? "" : _vienenCargo.ToString("N2");

                xrTableCellVienenAbono.Text =
                    _vienenAbono == 0 ? "" : _vienenAbono.ToString("N2");
            };

            // Muestra PASAN
            PageFooter.BeforePrint += (s, e) =>
            {
                xrTableCellPasanCargo.Text = _pasanCargo.ToString("N2");
                xrTableCellPasanAbono.Text = _pasanAbono.ToString("N2");

                // El PASAN actual será el VIENEN de la siguiente página
                _vienenCargo = _pasanCargo;
                _vienenAbono = _pasanAbono;
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

            if (dt != null)
            {
                if (!dt.Columns.Contains("CARGO_ACUM"))
                    dt.Columns.Add("CARGO_ACUM", typeof(decimal));

                if (!dt.Columns.Contains("ABONO_ACUM"))
                    dt.Columns.Add("ABONO_ACUM", typeof(decimal));

                decimal cargoAcum = 0;
                decimal abonoAcum = 0;

                foreach (DataRow row in dt.Rows)
                {
                    decimal cargo = row["CARGO"] == DBNull.Value
                        ? 0
                        : Convert.ToDecimal(row["CARGO"]);

                    decimal abono = row["ABONO"] == DBNull.Value
                        ? 0
                        : Convert.ToDecimal(row["ABONO"]);

                    cargoAcum += cargo;
                    abonoAcum += abono;

                    row["CARGO_ACUM"] = cargoAcum;
                    row["ABONO_ACUM"] = abonoAcum;
                }
            }

            DataSource = dt;
            DataMember = "";
        }

        private void xrTableCell3_BeforePrint(object sender, PrintEventArgs e)
        {
            XRTableCell celda = (XRTableCell)sender;

            int nivel = 1;

            object v = GetCurrentColumnValue("NIVEL");

            if (v != null && v != DBNull.Value)
                nivel = Convert.ToInt32(v);

            int rightPadding = 10;

            switch (nivel)
            {
                case 2:
                    rightPadding = 25;
                    break;
                case 3:
                    rightPadding = 40;
                    break;
                case 4:
                    rightPadding = 55;
                    break;
            }

            celda.Padding = new PaddingInfo(
                0,
                rightPadding,
                0,
                0,
                100F);
        }

        private void xrLabel11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel lbl = sender as XRLabel;

            lbl.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
        }
    }
}