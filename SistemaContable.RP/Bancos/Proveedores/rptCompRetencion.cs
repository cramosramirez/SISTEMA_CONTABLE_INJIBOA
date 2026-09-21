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
        // ============================================================
        // Origen de datos soportado por el reporte
        // ============================================================
        public enum OrigenDatos
        {
            Compra,         // desde CCF de compra normal
            Distribucion    // desde CCF con distribución
        }


        public int IdCcfCompra { get; set; }
        public string NombreProcedimiento { get; set; } = "SP_COMPROBANTE_RETENCION_RPT";
        public OrigenDatos Origen { get; set; } = OrigenDatos.Compra; // por defecto  CCF de compra normal

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

            switch (Origen)
            {
                case OrigenDatos.Compra:
                    CargarDatosCompra();
                    break;

                case OrigenDatos.Distribucion:
                    CargarDatosDistribuidora();
                    break;

                default:
                    throw new NotImplementedException(
                        $"Origen de datos '{Origen}' no está implementado.");
            }
        }

        private void CargarDatosCompra()
        {
            DataTable dt = EjecutarSP(NombreProcedimiento, new
            {
                ACCION = "COMPROBANTE_RETENCION",
                ID_CCF_COMPRA = IdCcfCompra
            });

            this.DataSource = dt;
            this.DataMember = "";
        }

        public void CargarDatosDistribuidora()
        {
            DataTable dt = EjecutarSP(NombreProcedimiento, new
            {
                ACCION = "COMPROBANTE_RETENCION_DISTRIBUIDORA",
                ID_CCF_COMPRA = IdCcfCompra
            });

            this.DataSource = dt;
            this.DataMember = "";
        }

    }
}
