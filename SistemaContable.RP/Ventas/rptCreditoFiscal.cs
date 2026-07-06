using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Ventas
{
    public partial class rptCreditoFiscal : SistemaContable.RP.ReporteBase
    {
        public int IdCCFEnc { get; set; }

        public rptCreditoFiscal()
        {
            InitializeComponent();
        }

        public override void CargarDatos()
        {
            if (IdCCFEnc == 0)
                throw new InvalidOperationException(
                    "Debe asignar IdCCFEnc antes de imprimir.");

            DataTable dt = EjecutarSP("[EDTE].[SP_CREDITOFISCAL_RPT]", new
            {
                ACCION = "CCF",
                ID_CCFENC = IdCCFEnc,
                ID_EMISOR = 1
            });

            this.DataSource = dt;
            this.DataMember = "";
        }
    }
}