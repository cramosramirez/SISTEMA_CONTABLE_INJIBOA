using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptChequeAnexo : SistemaContable.RP.ReporteBase
    {
        public int IdCheque { get; set; }
        public rptChequeAnexo()
        {
            InitializeComponent();
        }

        public override void CargarDatos()
        {
            if (IdCheque == 0)
                throw new InvalidOperationException(
                    "Debe asignar IdCheque antes de imprimir.");

            DataTable dt = EjecutarSP("SP_CHEQUE_RPT", new
            {
                ACCION = "CHEQUE_ANEXO",
                ID_CHEQUE = IdCheque
            });

            this.DataSource = dt;
            this.DataMember = "";
        }

    }
}
