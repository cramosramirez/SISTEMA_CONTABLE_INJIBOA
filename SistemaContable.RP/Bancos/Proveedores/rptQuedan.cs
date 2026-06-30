using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptQuedan : SistemaContable.RP.ReporteBase
    {
        public int IdQuedan { get; set; }

        public rptQuedan()
        {
            InitializeComponent();            
        }
        public override void CargarDatos()
        {
            if (IdQuedan == 0)
                throw new InvalidOperationException(
                    "Debe asignar IdQuedan antes de imprimir.");

            DataTable dt = EjecutarSP("SP_QUEDAN_RPT", new
            {
                ACCION = "QUEDAN",
                ID_QUEDAN = IdQuedan
            });

            this.DataSource = dt;
            this.DataMember = "";
        }
    }
}
