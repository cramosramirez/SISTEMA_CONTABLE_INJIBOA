using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptCompRetencion :  SistemaContable.RP.ReporteBase
    {
        public int IdCcfCompra { get; set; }
        public string NombreProcedimiento { get; set; } = "SP_COMPROBANTE_RETENCION_RPT";

        public rptCompRetencion()
        {
            InitializeComponent();            
            ReporteUtils.ConfigurarQR(xrBarCode4); // El de la URL de validación
        }              

        public override void CargarDatos()
        {
            if (IdCcfCompra == 0)
                throw new InvalidOperationException(
                    "Debe asignar CCF de compra antes de imprimir.");

            DataTable dt = EjecutarSP(NombreProcedimiento, new
            {
                ACCION = "COMPROBANTE_RETENCION",
                ID_CCF_COMPRA = IdCcfCompra
            });

            this.DataSource = dt;
            this.DataMember = "";
        }

    }
}
