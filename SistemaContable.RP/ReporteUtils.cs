using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraReports.UI;

namespace SistemaContable.RP
{
    public static class ReporteUtils
    {
        public static void ConfigurarQR(XRBarCode barCode)
        {
            if (barCode.Symbology is QRCodeGenerator qr)
            {
                qr.CompactionMode = QRCodeCompactionMode.Byte;
                qr.ErrorCorrectionLevel = QRCodeErrorCorrectionLevel.M; // La URL es larga, con H quedaría muy denso
            }
        }
    }
}
