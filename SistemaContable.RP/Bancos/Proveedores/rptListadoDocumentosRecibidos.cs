using ClosedXML.Excel;
using SistemaContable.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptListadoDocumentosRecibidos : SistemaContable.RP.ReporteBase
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool ClasificarPorOrden { get; set; }

        private static readonly (int Codigo, string Nombre)[] Grupos =
           {
                (1, "Sin Orden"),
                (2, "JIBOA"),
                (3, "Planta Cogeneración"),
                (4, "Planta Fotovoltaica")
            };

        public rptListadoDocumentosRecibidos()
        {
            InitializeComponent();
        }

        // ============================================================
        // Carga de datos para el reporte visual DevExpress
        // Retorna solo el detalle (primera tabla del SP)
        // ============================================================
        public override void CargarDatos()
        {
            DataTable dt = ObtenerDatos();
            this.DataSource = dt;
            this.DataMember = "";
        }

        // ============================================================
        // Fuente de datos del reporte visual (solo detalle)
        // ============================================================
        private DataTable ObtenerDatos()
        {
            var dal = new DALBase();
            return dal.EjecutarConsulta("SP_QUEDAN_RPT", new
            {
                ACCION = "LISTADO_COMPROBANTES_RECIBIDOS",
                FECHA_INI = FechaInicial,
                FECHA_FIN = FechaFinal,
                CLASIFICAR_POR_ORDEN_COMPRA = ClasificarPorOrden
            });
        }
       
        // ============================================================
        // Fuente de datos para exportación filtrada por grupo
        // Retorna DataSet con detalle + resumen del grupo indicado
        // ============================================================
        private DataSet ObtenerDatosCompletos(int? grupo)
        {
            var dal = new DALBase();
            return dal.EjecutarMultiple("SP_QUEDAN_RPT", new
            {
                ACCION = "LISTADO_COMPROBANTES_RECIBIDOS",
                FECHA_INI = FechaInicial,
                FECHA_FIN = FechaFinal,
                CLASIFICAR_POR_ORDEN_COMPRA = ClasificarPorOrden,
                GRUPO = grupo
            });
        }

        private static string TruncarNombreHoja(string nombre)
        {
            // Excel limita nombres de hoja a 31 caracteres
            return nombre.Length > 31 ? nombre.Substring(0, 31) : nombre;
        }

        // ============================================================
        // Exportar a Excel (dos hojas: detalle + resumen por proveedor)
        // ============================================================
        public bool ExportarAExcel()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files|*.xlsx";
                sfd.Title = "Guardar archivo Excel";
                sfd.FileName = "COMPROBANTES_RECIBIDOS_" +
                               DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");

                if (sfd.ShowDialog() != DialogResult.OK) return false;

                using (var wb = new XLWorkbook())
                {
                    // ✅ AQUÍ va el foreach
                    foreach (var grupo in Grupos)
                    {
                        DataSet ds = ObtenerDatosCompletos(grupo.Codigo);

                        if (ds.Tables.Count < 2)
                            throw new Exception(
                                $"El SP no devolvió los resultados esperados para el grupo '{grupo.Nombre}'.");

                        DataTable dtDetalle = ds.Tables[0];
                        DataTable dtResumen = ds.Tables[1];

                        string nombreDetalle = TruncarNombreHoja($"{grupo.Nombre} - Detalle");
                        string nombreResumen = TruncarNombreHoja($"{grupo.Nombre} - Resumen");

                        AgregarHojaDetalle(wb, nombreDetalle, dtDetalle);
                        AgregarHojaResumen(wb, nombreResumen, dtResumen);
                    }

                    wb.SaveAs(sfd.FileName);
                }
            }

            return true;
        }

        // ============================================================
        // Hoja 1: Detalle de comprobantes
        // ============================================================
        private void AgregarHojaDetalle(XLWorkbook wb, string nombreHoja, DataTable dt)
        {
            var ws = wb.Worksheets.Add(nombreHoja);

            string[] encabezados =
            {
                "N° Quedan", "Tipo DTE", "Código Generación", "Fecha Emisión",
                "Fecha Recibido", "Fecha Vence", "Nombre", "Orden", "Afecta",
                "Total", "Saldo", "N° Retención"
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

            if (dt.Rows.Count > 0)
            {
                ws.Columns().AdjustToContents();
                ws.RangeUsed().SetAutoFilter();
            }
            ws.SheetView.FreezeRows(1);
        }

        // ============================================================
        // Hoja 2: Resumen agrupado por proveedor
        // ============================================================
        private void AgregarHojaResumen(XLWorkbook wb, string nombreHoja, DataTable dt)
        {
            var ws = wb.Worksheets.Add(nombreHoja);

            string[] encabezados = { "Proveedor", "Afecta", "Total", "Saldo" };

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
                ws.Cell(fila, 1).Value = ObtenerTexto(row, "PROVEEDOR");
                EscribirDecimal(ws, fila, 2, row, "AFECTA");
                EscribirDecimal(ws, fila, 3, row, "TOTAL");
                EscribirDecimal(ws, fila, 4, row, "SALDO");
                fila++;
            }

            if (dt.Rows.Count > 0)
            {
                ws.Columns().AdjustToContents();
                ws.RangeUsed().SetAutoFilter();
            }
            ws.SheetView.FreezeRows(1);
        }

        // ============================================================
        // Helpers para lectura segura del DataRow
        // ============================================================
        private static int ObtenerEntero(DataRow row, string columna)
        {
            if (!row.Table.Columns.Contains(columna)) return 0;
            if (row[columna] == DBNull.Value) return 0;
            return Convert.ToInt32(row[columna]);
        }

        private static string ObtenerTexto(DataRow row, string columna)
        {
            if (!row.Table.Columns.Contains(columna)) return "";
            if (row[columna] == DBNull.Value) return "";
            return row[columna].ToString();
        }

        private static void EscribirFecha(IXLWorksheet ws, int fila, int col,
            DataRow row, string columna)
        {
            if (!row.Table.Columns.Contains(columna)) return;
            if (row[columna] == DBNull.Value) return;

            var celda = ws.Cell(fila, col);
            celda.Value = Convert.ToDateTime(row[columna]);
            celda.Style.DateFormat.Format = "dd/MM/yyyy";
        }

        private static void EscribirDecimal(IXLWorksheet ws, int fila, int col,
            DataRow row, string columna)
        {
            if (!row.Table.Columns.Contains(columna)) return;
            if (row[columna] == DBNull.Value) return;

            var celda = ws.Cell(fila, col);
            celda.Value = Convert.ToDecimal(row[columna]);
            celda.Style.NumberFormat.Format = "#,##0.00";
        }
    }
}
