using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptSujetoExcluido :  SistemaContable.RP.ReporteBase
    {

        public int IdFse { get; set; }
        public rptSujetoExcluido()
        {
            InitializeComponent();
            ReporteUtils.ConfigurarQR(xrBarCode4); // El de la URL de validación
        }

        public override void CargarDatos()
        {
            if (IdFse == 0)
                throw new InvalidOperationException(
                    "Debe asignar FSE antes de imprimir.");

            DataTable dt = EjecutarSP("SP_FACTURA_SUJETO_EXC_RPT", new
            {
                ACCION = "FACTURA_SUJETO_EXCLUIDO",
                ID_FSE = IdFse
            });

            this.DataSource = dt;
            this.DataMember = "";
        }
    }
}
