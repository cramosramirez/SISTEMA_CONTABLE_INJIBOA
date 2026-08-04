using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Ventas
{
    public partial class rptFactura : SistemaContable.RP.ReporteBase
    {
        public int IdFactEnc { get; set; }

        public rptFactura()
        {
            InitializeComponent();
        }

        public override void CargarDatos()
        {
            if (IdFactEnc == 0)
                throw new InvalidOperationException(
                    "Debe asignar IdFactEnc antes de imprimir.");

            DataTable dt = EjecutarSP("[EDTE].[SP_FACTURA_RPT]", new
            {
                ACCION = "FA",              // REVISAR: confirmar el código de acción del SP de reporte de Factura
                ID_FACTENC = IdFactEnc,
                ID_EMISOR = 1
            });

            this.DataSource = dt;
            this.DataMember = "";
        }
    }
}
