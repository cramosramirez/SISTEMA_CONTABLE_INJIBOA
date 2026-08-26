using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Ventas
{
    public partial class rptNotaCredito : SistemaContable.RP.ReporteBase
    {
        public int IdNTCEnc { get; set; }

        public rptNotaCredito()
        {
            InitializeComponent();
        }

        public override void CargarDatos()
        {
            if (IdNTCEnc == 0)
                throw new InvalidOperationException(
                    "Debe asignar IdNTCEnc antes de imprimir.");

            DataTable dt = EjecutarSP("[EDTE].[SP_NOTACREDITO_RPT]", new
            {
                ACCION = "NCR",
                ID_NTCENC = IdNTCEnc,
                ID_EMISOR = 1
            });

            this.DataSource = dt;
            this.DataMember = "";
        }
    }
}
