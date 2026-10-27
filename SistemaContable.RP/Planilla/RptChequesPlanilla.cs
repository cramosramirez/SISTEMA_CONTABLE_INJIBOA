using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;

namespace SistemaContable.RP.Planilla
{
    public class RptChequesPlanilla : ReporteBase
    {
        private readonly int _idCatorcena;
        private readonly int _idTipoPlanilla;
        private readonly int _esContribuyente;

        public RptChequesPlanilla(int idCatorcena, int idTipoPlanilla, int esContribuyente)
        {
            _idCatorcena = idCatorcena;
            _idTipoPlanilla = idTipoPlanilla;
            _esContribuyente = esContribuyente;
            NombreArchivoSugerido = "Listado_Cheques_Planilla";
            ConstruirReporte();
        }

        public override void CargarDatos()
        {
            DataTable datos = EjecutarSP("SP_CHEQUE_RPT", new
            {
                ACCION = "LISTADO_CHEQUES_PLANILLA",
                ID_CHEQUE = (int?)null,
                ID_CATORCENA = _idCatorcena,
                ID_TIPO_PLANILLA = _idTipoPlanilla,
                ES_CONTRIBUYENTE = _esContribuyente
            });

            if (datos == null || datos.Rows.Count == 0)
                throw new InvalidOperationException(
                    "No se encontraron cheques generados para los filtros seleccionados.");

            DataSource = datos;
            DataMember = string.Empty;
        }

        private void ConstruirReporte()
        {
            PaperKind = PaperKind.Letter;
            Landscape = false;
            Margins = new Margins(50, 50, 45, 45);
            Font = new Font("Courier New", 8.5F);
            ShowPrintMarginsWarning = false;

            Bands.Clear();
            Bands.AddRange(new Band[]
            {
                new TopMarginBand { HeightF = 35F },
                CrearEncabezado(),
                CrearDetalle(),
                new BottomMarginBand { HeightF = 35F }
            });
        }

        private static PageHeaderBand CrearEncabezado()
        {
            var banda = new PageHeaderBand { HeightF = 145F };
            Font fuente = new Font("Courier New", 8.5F);

            var fecha = new XRPageInfo
            {
                PageInfo = PageInfo.DateTime,
                TextFormatString = "{0:dd/MM/yyyy}",
                BoundsF = new RectangleF(0, 5, 110, 18),
                Font = fuente
            };
            var hora = new XRPageInfo
            {
                PageInfo = PageInfo.DateTime,
                TextFormatString = "{0:HH:mm:ss}",
                BoundsF = new RectangleF(0, 25, 110, 18),
                Font = fuente
            };
            var paginas = new XRPageInfo
            {
                PageInfo = PageInfo.NumberOfTotal,
                TextFormatString = "Página {0} de {1}",
                BoundsF = new RectangleF(600, 5, 150, 20),
                Font = fuente,
                TextAlignment = TextAlignment.TopRight
            };

            var empresa = CrearEtiqueta("INGENIO CENTRAL AZUCARERO JIBOA S.A. DE C.V.", 110, 12, 490, 22,
                new Font("Courier New", 10F, FontStyle.Bold), TextAlignment.MiddleCenter);
            var titulo = CrearEtiqueta("LISTADO DE CHEQUES", 110, 35, 490, 20, fuente, TextAlignment.MiddleCenter);
            var periodo = CrearEtiqueta(string.Empty, 90, 57, 530, 20, fuente, TextAlignment.MiddleCenter);
            periodo.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text",
                "FormatString('PERIODO DEL {0:dd/MM/yyyy} AL {1:dd/MM/yyyy}', [FECHA_INICIO], [FECHA_FIN])"));
            var corte = CrearEtiqueta(string.Empty, 260, 79, 230, 20, fuente, TextAlignment.MiddleCenter);
            corte.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text",
                "FormatString('CORTE # {0}', [CATORCENA])"));

            banda.Controls.AddRange(new XRControl[]
            {
                fecha, hora, paginas, empresa, titulo, periodo, corte, CrearTabla(105F, true)
            });
            return banda;
        }

        private static DetailBand CrearDetalle()
        {
            var banda = new DetailBand { HeightF = 18F, KeepTogether = true };
            banda.Controls.Add(CrearTabla(0F, false));
            return banda;
        }

        private static XRTable CrearTabla(float y, bool encabezado)
        {
            var tabla = new XRTable
            {
                BoundsF = new RectangleF(0, y, 750, encabezado ? 25 : 18),
                Font = new Font("Courier New", 7.8F),
                Borders = encabezado ? BorderSide.Bottom : BorderSide.None,
                BorderWidth = encabezado ? 1.2F : 0F
            };
            var fila = new XRTableRow();
            tabla.Rows.Add(fila);

            fila.Cells.Add(CrearCelda("CODIGO", 55,
                encabezado ? TextAlignment.MiddleLeft : TextAlignment.MiddleRight, encabezado));
            XRTableCell nombre = CrearCelda("NOMBRE", 365, TextAlignment.MiddleLeft, encabezado);
            nombre.Padding = new PaddingInfo(8, 2, 0, 0, 100F);
            fila.Cells.Add(nombre);
            fila.Cells.Add(CrearCelda("TONELADAS", 85, TextAlignment.MiddleRight, encabezado,
                encabezado ? null : "{0:#,##0.000}"));
            fila.Cells.Add(CrearCelda("VALOR", 85, TextAlignment.MiddleRight, encabezado,
                encabezado ? null : "{0:#,##0.00}"));
            fila.Cells.Add(CrearCelda("NETO", 85, TextAlignment.MiddleRight, encabezado,
                encabezado ? null : "{0:#,##0.00}"));
            fila.Cells.Add(CrearCelda("CHEQUE", 75, TextAlignment.MiddleRight, encabezado));
            return tabla;
        }

        private static XRTableCell CrearCelda(
            string campo, float ancho, TextAlignment alineacion, bool encabezado, string formato = null)
        {
            var celda = new XRTableCell
            {
                Text = encabezado ? campo : string.Empty,
                Weight = ancho,
                TextAlignment = alineacion,
                CanGrow = false,
                Padding = new PaddingInfo(2, 2, 0, 0, 100F)
            };

            if (!encabezado)
            {
                celda.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[" + campo + "]"));
                if (!string.IsNullOrEmpty(formato)) celda.TextFormatString = formato;
            }

            return celda;
        }

        private static XRLabel CrearEtiqueta(
            string texto, float x, float y, float ancho, float alto, Font fuente, TextAlignment alineacion)
        {
            return new XRLabel
            {
                Text = texto,
                BoundsF = new RectangleF(x, y, ancho, alto),
                Font = fuente,
                TextAlignment = alineacion,
                CanGrow = false,
                Padding = new PaddingInfo(0, 0, 0, 0, 100F)
            };
        }
    }
}
