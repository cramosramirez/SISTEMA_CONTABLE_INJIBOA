using ClosedXML.Excel;
using System;
using System.Collections.Generic;
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

        // ============================================================
        // Headers
        // ============================================================

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

        public static void EscribirTexto(this IXLWorksheet ws, int fila, int col,
            DataRow row, string columna)
        {
            if (ws == null || row == null) return;
            if (!row.Table.Columns.Contains(columna)) return;
            if (row[columna] == DBNull.Value) return;

            ws.Cell(fila, col).Value = row[columna].ToString();
        }

        public static void EscribirEntero(this IXLWorksheet ws, int fila, int col,
            DataRow row, string columna)
        {
            if (ws == null || row == null) return;
            if (!row.Table.Columns.Contains(columna)) return;
            if (row[columna] == DBNull.Value) return;

            ws.Cell(fila, col).Value = Convert.ToInt32(row[columna]);
        }

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

        public static void EscribirFecha(this IXLWorksheet ws, int fila, int col,
            DataRow row, string columna, string formato = FORMATO_FECHA)
        {
            if (ws == null || row == null) return;
            if (!row.Table.Columns.Contains(columna)) return;
            if (row[columna] == DBNull.Value) return;

            var celda = ws.Cell(fila, col);
            celda.Value = Convert.ToDateTime(row[columna]);
            celda.Style.DateFormat.Format = formato;
        }

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
        /// Si agregarTotales = true, agrega una fila al final con SUM() para
        /// las columnas decimal, double y float.
        /// </summary>
        public static void ExportarDataTable(this IXLWorksheet ws, DataTable dt,
            bool detectarTipos = true, bool agregarTotales = true,            
            Dictionary<string, string> renombrarColumnas = null)
        {
            if (ws == null || dt == null) return;

            // Headers
            // Headers con renombrado opcional
            var nombres = dt.Columns.Cast<DataColumn>()
                .Select(c => renombrarColumnas != null && renombrarColumnas.ContainsKey(c.ColumnName)
                             ? renombrarColumnas[c.ColumnName]
                             : c.ColumnName)
                .ToArray();
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

                    Type tipo = columna.DataType;

                    if (tipo == typeof(DateTime))
                    {
                        celda.Value = Convert.ToDateTime(valor);
                        celda.Style.DateFormat.Format = FORMATO_FECHA;
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

            // ============================================================
            // Fila de totales para columnas decimal/double/float
            // ============================================================
            if (agregarTotales && dt.Rows.Count > 0)
            {
                AgregarFilaTotales(ws, dt, fila);
            }

            ws.FinalizarHoja();
        }

        // ============================================================
        // Fila de totales (uso interno de ExportarDataTable)
        // ============================================================

        /// <summary>
        /// Agrega una fila de totales al final para las columnas decimal/double/float.
        /// En la primera columna no numérica escribe "TOTAL".
        /// Las demás columnas no numéricas quedan vacías pero con el mismo estilo.
        /// Usa fórmulas =SUM() para que se recalculen si el usuario edita.
        /// </summary>
        private static void AgregarFilaTotales(IXLWorksheet ws, DataTable dt, int filaTotales)
        {
            bool etiquetaColocada = false;

            for (int col = 0; col < dt.Columns.Count; col++)
            {
                var columna = dt.Columns[col];
                Type tipo = columna.DataType;
                var celda = ws.Cell(filaTotales, col + 1);

                bool esNumericaDecimal = tipo == typeof(decimal) ||
                                         tipo == typeof(double) ||
                                         tipo == typeof(float);

                // Estilo común para toda la fila de totales
                celda.Style.Fill.BackgroundColor = XLColor.LightGray;
                celda.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                celda.Style.Font.Bold = true;

                if (esNumericaDecimal)
                {
                    // Fórmula SUM que abarca de fila 2 hasta la última fila de datos
                    string letraCol = ObtenerLetraColumna(col + 1);
                    string rango = $"{letraCol}2:{letraCol}{filaTotales - 1}";

                    celda.FormulaA1 = $"=SUM({rango})";
                    celda.Style.NumberFormat.Format = FORMATO_CONTABLE;
                }
                else if (!etiquetaColocada)
                {
                    // Primera columna no numérica → etiqueta "TOTAL"
                    celda.Value = "TOTAL";
                    etiquetaColocada = true;
                }
                // (las demás columnas no numéricas quedan vacías pero con el estilo)
            }
        }

        /// <summary>
        /// Devuelve la letra de columna de Excel para un índice 1-based.
        /// 1 → A, 2 → B, ..., 27 → AA, 28 → AB
        /// </summary>
        private static string ObtenerLetraColumna(int indice)
        {
            string letra = "";
            while (indice > 0)
            {
                indice--;
                letra = (char)('A' + indice % 26) + letra;
                indice /= 26;
            }
            return letra;
        }
    }
}