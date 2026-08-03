using ClosedXML.Excel;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptListadoDocumentosRecibidos : SistemaContable.RP.ReporteBase
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool ClasificarPorOrden { get; set; }

        public rptListadoDocumentosRecibidos()
        {
            InitializeComponent();
        }

        private DataTable ObtenerDatos()
        {
            return EjecutarSP("SP_QUEDAN_RPT", new
            {
                ACCION = "LISTADO_COMPROBANTES_RECIBIDOS",
                FECHA_INI = FechaInicial,
                FECHA_FIN = FechaFinal,
                CLASIFICAR_POR_ORDEN_COMPRA = ClasificarPorOrden
            });
        }
        public override void CargarDatos()
        {           
            DataTable dt = ObtenerDatos();

            if (FechaInicial == FechaFinal)
                xrTITULO.Text = $"Listado de Documentos Recibido el Día {FechaInicial.ToString("dd/MM/yyyy")}";
            else
                xrTITULO.Text = $"Listado de Documentos Recibido del {FechaInicial.ToString("dd/MM/yyyy")} al {FechaFinal.ToString("dd/MM/yyyy")}";

            this.DataSource = dt;
            this.DataMember = "";
        }

        /// <summary>
        /// Exporta el listado de documentos recibidos a Excel, dejando que el usuario
        /// elija dónde guardar el archivo mediante un diálogo de Guardar como.
        /// </summary>
        /// <returns>True si el usuario guardó el archivo; False si canceló el diálogo.</returns>
        public bool ExportarAExcel()
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Archivo de Excel (*.xlsx)|*.xlsx";
                sfd.Title = "Guardar Listado de Documentos Recibidos";
                sfd.FileName = $"ListadoDocumentosRecibidos_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return false; // el usuario canceló

                ExportarAExcel(sfd.FileName);
                return true;
            }
        }

        /// <summary>
        /// Exporta el listado de documentos recibidos directamente a Excel, sin necesidad
        /// de llamar a CargarDatos ni de mostrar el reporte. Ejecuta su propia consulta.
        /// </summary>
        /// <param name="rutaArchivo">Ruta completa del archivo a generar, ej: C:\Reportes\Documentos.xlsx</param>
        public void ExportarAExcel(string rutaArchivo)
        {
            DataTable dt = ObtenerDatos();

            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Documentos Recibidos");

                string[] encabezados =
                {
                    "N° Quedan", "Tipo DTE", "Código Generación", "Fecha Emisión",
                    "Fecha Recibido", "Fecha Vence", "Nombre", "Orden",
                    "Afecta", "Total", "Saldo", "N° Retención"
                };

                for (int col = 0; col < encabezados.Length; col++)
                {
                    var celda = ws.Cell(1, col + 1);
                    celda.Value = encabezados[col];
                    celda.Style.Font.Bold = true;
                    celda.Style.Fill.BackgroundColor = XLColor.LightGray;
                }

                int fila = 2;
                foreach (DataRow row in dt.Rows)
                {
                    ws.Cell(fila, 1).Value = ObtenerEntero(row, "NUM_QUEDAN");
                    ws.Cell(fila, 2).Value = ObtenerTexto(row, "TIPO_DTE");
                    ws.Cell(fila, 3).Value = ObtenerTexto(row, "COD_GENERACION");

                    EscribirFecha(ws, fila, 4, row, "FECHA_EMISION");
                    EscribirFecha(ws, fila, 5, row, "FECHA_RECIBIDO");
                    EscribirFecha(ws, fila, 6, row, "FECHA_VENCE");

                    ws.Cell(fila, 7).Value = ObtenerTexto(row, "NOMBRE");
                    ws.Cell(fila, 8).Value = ObtenerTexto(row, "ORDEN");

                    EscribirDecimal(ws, fila, 9, row, "AFECTA");
                    EscribirDecimal(ws, fila, 10, row, "TOTAL");
                    EscribirDecimal(ws, fila, 11, row, "SALDO");

                    ws.Cell(fila, 12).Value = ObtenerTexto(row, "NUMRET");
                    fila++;
                }

                ws.Columns().AdjustToContents();
                ws.SheetView.FreezeRows(1);
                ws.RangeUsed().SetAutoFilter();
                wb.SaveAs(rutaArchivo);
            }
        }

        // --- Helpers para lectura segura de valores (evitan errores por DBNull) ---

        private static string ObtenerTexto(DataRow row, string columna)
            => row[columna] == DBNull.Value ? string.Empty : row[columna].ToString();

        private static int? ObtenerEntero(DataRow row, string columna)
            => row[columna] == DBNull.Value ? (int?)null : Convert.ToInt32(row[columna]);

        private static void EscribirFecha(IXLWorksheet ws, int fila, int col, DataRow row, string columna)
        {
            if (row[columna] == DBNull.Value) return;
            var celda = ws.Cell(fila, col);
            celda.Value = Convert.ToDateTime(row[columna]);
            celda.Style.DateFormat.Format = "dd/MM/yyyy";
        }

        private static void EscribirDecimal(IXLWorksheet ws, int fila, int col, DataRow row, string columna)
        {
            if (row[columna] == DBNull.Value) return;
            var celda = ws.Cell(fila, col);
            celda.Value = Convert.ToDecimal(row[columna]);
            celda.Style.NumberFormat.Format = "#,##0.00";
        }
    }
}
