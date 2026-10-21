using ClosedXML.Excel;
using System;
using System.Data;
using System.Linq;

namespace SistemaContable.RP.Helpers
{
    /// <summary>
    /// Extension methods para trabajar con IXLWorksheet (ClosedXML).
    /// Centraliza el formato de celdas y la escritura de valores desde DataRow.
    /// </summary>
    public static class ExcelHelper
    {
        // ============================================================
        // Headers
        // ============================================================
        public const string FORMATO_NUMERO = "#,##0.00";

        /// <summary>Contable con $ y negativos entre paréntesis: $ 1,234.56 / ($ 1,234.56) / $ -</summary>
        public const string FORMATO_CONTABLE = "_-$ * #,##0.00_-;-$ * #,##0.00_-;_-$ * \"-\"??_-;_-@_-";

        /// <summary>Contable sin $ con negativos entre paréntesis: 1,234.56 / (1,234.56) / -</summary>
        public const string FORMATO_CONTABLE_SIMPLE = "#,##0.00;(#,##0.00);-";

        /// <summary>Entero con separador de miles: 1,234</summary>
        public const string FORMATO_ENTERO = "#,##0";

        /// <summary>Porcentaje con 2 decimales: 12.34%</summary>
        public const string FORMATO_PORCENTAJE = "0.00%";

        /// <summary>Fecha corta: 20/10/2026</summary>
        public const string FORMATO_FECHA = "dd/MM/yyyy";

        /// <summary>Fecha con hora: 20/10/2026 18:30</summary>
        public const string FORMATO_FECHA_HORA = "dd/MM/yyyy HH:mm";

        /// <summary>
        /// Escribe los encabezados en la fila 1 con formato bold + fondo gris.
        /// </summary>
        public static void EscribirHeaders(this IXLWorksheet ws, string[] encabezados)
        {
            if (ws == null || encabezados == null) return;

            for (int col = 0; col < encabezados.Length; col++)
            {
                var celda = ws.Cell(1, col + 1);
                celda.Value = encabezados[col];
                celda.Style.Font.Bold = true;
                celda.Style.Fill.BackgroundColor = XLColor.LightGray;
                celda.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
        }

        // ============================================================
        // Escritura de valores desde DataRow (lectura segura)
        // ============================================================

        /// <summary>
        /// Escribe el texto de la columna en la celda. Si no existe o es DBNull, deja la celda vacía.
        /// </summary>
        public static void EscribirTexto(this IXLWorksheet ws, int fila, int col,
            DataRow row, string columna)
        {
            if (ws == null || row == null) return;
            if (!row.Table.Columns.Contains(columna)) return;
            if (row[columna] == DBNull.Value) return;

            ws.Cell(fila, col).Value = row[columna].ToString();
        }

        /// <summary>
        /// Escribe el valor entero de la columna en la celda.
        /// </summary>
        public static void EscribirEntero(this IXLWorksheet ws, int fila, int col,
            DataRow row, string columna)
        {
            if (ws == null || row == null) return;
            if (!row.Table.Columns.Contains(columna)) return;
            if (row[columna] == DBNull.Value) return;

            ws.Cell(fila, col).Value = Convert.ToInt32(row[columna]);
        }

        /// <summary>
        /// Escribe el valor decimal en la celda con formato "#,##0.00".
        /// </summary>
        public static void EscribirDecimal(this IXLWorksheet ws, int fila, int col,
            DataRow row, string columna, string formato = FORMATO_CONTABLE)
        {
            if (ws == null || row == null) return;
            if (!row.Table.Columns.Contains(columna)) return;
            if (row[columna] == DBNull.Value) return;

            var celda = ws.Cell(fila, col);
            celda.Value = Convert.ToDecimal(row[columna]);
            celda.Style.NumberFormat.Format = formato;
        }

        /// <summary>
        /// Escribe la fecha en la celda con formato "dd/MM/yyyy".
        /// </summary>
        public static void EscribirFecha(this IXLWorksheet ws, int fila, int col,
            DataRow row, string columna, string formato = "dd/MM/yyyy")
        {
            if (ws == null || row == null) return;
            if (!row.Table.Columns.Contains(columna)) return;
            if (row[columna] == DBNull.Value) return;

            var celda = ws.Cell(fila, col);
            celda.Value = Convert.ToDateTime(row[columna]);
            celda.Style.DateFormat.Format = formato;
        }

        /// <summary>
        /// Escribe el bool en la celda como "Sí" / "No".
        /// </summary>
        public static void EscribirBool(this IXLWorksheet ws, int fila, int col,
            DataRow row, string columna, string textoTrue = "Sí", string textoFalse = "No")
        {
            if (ws == null || row == null) return;
            if (!row.Table.Columns.Contains(columna)) return;
            if (row[columna] == DBNull.Value) return;

            ws.Cell(fila, col).Value = Convert.ToBoolean(row[columna]) ? textoTrue : textoFalse;
        }

        // ============================================================
        // Finalización de hoja
        // ============================================================

        /// <summary>
        /// Ajusta el ancho de columnas al contenido, aplica autofiltro
        /// y congela la primera fila.
        /// </summary>
        public static void FinalizarHoja(this IXLWorksheet ws, bool autoFiltro = true, bool congelarHeader = true)
        {
            if (ws == null) return;

            try
            {
                if (ws.RangeUsed() != null)
                {
                    ws.Columns().AdjustToContents();
                    if (autoFiltro) ws.RangeUsed().SetAutoFilter();
                }
            }
            catch { /* hoja vacía */ }

            if (congelarHeader) ws.SheetView.FreezeRows(1);
        }

        // ============================================================
        // Exportación genérica de DataTable completo
        // ============================================================

        /// <summary>
        /// Exporta un DataTable completo a una hoja Excel con sus columnas tal cual.
        /// Útil para exportaciones rápidas de un DataSource sin mapeo manual.
        /// </summary>
        public static void ExportarDataTable(this IXLWorksheet ws, DataTable dt,
            bool detectarTipos = true)
        {
            if (ws == null || dt == null) return;

            // Headers
            var nombres = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();
            ws.EscribirHeaders(nombres);

            // Datos
            int fila = 2;
            foreach (DataRow row in dt.Rows)
            {
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    var columna = dt.Columns[col];
                    var valor = row[col];
                    if (valor == DBNull.Value) continue;

                    var celda = ws.Cell(fila, col + 1);

                    if (!detectarTipos)
                    {
                        celda.Value = valor.ToString();
                        continue;
                    }

                    // Formato según el tipo de la columna
                    Type tipo = columna.DataType;

                    if (tipo == typeof(DateTime))
                    {
                        celda.Value = Convert.ToDateTime(valor);
                        celda.Style.DateFormat.Format = "dd/MM/yyyy";
                    }
                    else if (tipo == typeof(decimal) || tipo == typeof(double) || tipo == typeof(float))
                    {
                        celda.Value = Convert.ToDecimal(valor);
                        celda.Style.NumberFormat.Format = FORMATO_CONTABLE;
                    }
                    else if (tipo == typeof(int) || tipo == typeof(long) || tipo == typeof(short))
                    {
                        celda.Value = Convert.ToInt64(valor);
                    }
                    else if (tipo == typeof(bool))
                    {
                        celda.Value = Convert.ToBoolean(valor) ? "Sí" : "No";
                    }
                    else
                    {
                        celda.Value = valor.ToString();
                    }
                }
                fila++;
            }

            ws.FinalizarHoja();
        }
    }
}