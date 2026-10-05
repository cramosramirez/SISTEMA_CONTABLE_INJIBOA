using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace SistemaContable.RP.Ventas.Movimientos
{
    public partial class RptDetalleVentasDiarasdtResumen : DevExpress.XtraReports.UI.XtraReport
    {
        public RptDetalleVentasDiarasdtResumen(DateTime fechaDesde, DateTime fechaHasta, int idCentro)
        {
            InitializeComponent();
            SdsDtResumenVenta.Queries["EFACTURACION_RPT_VENTAS_DIARIAS"].Parameters[0].Value = fechaDesde;
            SdsDtResumenVenta.Queries["EFACTURACION_RPT_VENTAS_DIARIAS"].Parameters[1].Value = fechaHasta;
            SdsDtResumenVenta.Queries["EFACTURACION_RPT_VENTAS_DIARIAS"].Parameters[2].Value = idCentro;

            SdsDtResumenVenta.Queries["EFACTURACION_RPT_VENTAS_DIARIAS_DTRESUMEN"].Parameters[0].Value = fechaDesde;
            SdsDtResumenVenta.Queries["EFACTURACION_RPT_VENTAS_DIARIAS_DTRESUMEN"].Parameters[1].Value = fechaHasta;
            SdsDtResumenVenta.Queries["EFACTURACION_RPT_VENTAS_DIARIAS_DTRESUMEN"].Parameters[2].Value = idCentro;
        }

    }
}
