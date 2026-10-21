using System;
using System.Data;

namespace SistemaContable.RP.Helpers
{
    /// <summary>
    /// Métodos estáticos para lectura segura de valores de un DataRow.
    /// Devuelven un valor por defecto cuando la columna no existe o es DBNull.
    /// Pensados para ser reutilizables en todos los reportes y procesos del sistema.
    /// </summary>
    public static class DataRowHelper
    {
        /// <summary>
        /// Devuelve el valor entero de la columna, o 0 si no existe o es DBNull.
        /// </summary>
        public static int ObtenerEntero(DataRow row, string columna, int valorDefault = 0)
        {
            if (row == null || !row.Table.Columns.Contains(columna)) return valorDefault;
            if (row[columna] == DBNull.Value) return valorDefault;

            try { return Convert.ToInt32(row[columna]); }
            catch { return valorDefault; }
        }

        /// <summary>
        /// Devuelve el valor long de la columna, o 0 si no existe o es DBNull.
        /// </summary>
        public static long ObtenerLong(DataRow row, string columna, long valorDefault = 0)
        {
            if (row == null || !row.Table.Columns.Contains(columna)) return valorDefault;
            if (row[columna] == DBNull.Value) return valorDefault;

            try { return Convert.ToInt64(row[columna]); }
            catch { return valorDefault; }
        }

        /// <summary>
        /// Devuelve el valor decimal de la columna, o 0 si no existe o es DBNull.
        /// </summary>
        public static decimal ObtenerDecimal(DataRow row, string columna, decimal valorDefault = 0m)
        {
            if (row == null || !row.Table.Columns.Contains(columna)) return valorDefault;
            if (row[columna] == DBNull.Value) return valorDefault;

            try { return Convert.ToDecimal(row[columna]); }
            catch { return valorDefault; }
        }

        /// <summary>
        /// Devuelve el valor double de la columna, o 0 si no existe o es DBNull.
        /// </summary>
        public static double ObtenerDouble(DataRow row, string columna, double valorDefault = 0d)
        {
            if (row == null || !row.Table.Columns.Contains(columna)) return valorDefault;
            if (row[columna] == DBNull.Value) return valorDefault;

            try { return Convert.ToDouble(row[columna]); }
            catch { return valorDefault; }
        }

        /// <summary>
        /// Devuelve el texto de la columna, o string.Empty si no existe o es DBNull.
        /// </summary>
        public static string ObtenerTexto(DataRow row, string columna, string valorDefault = "")
        {
            if (row == null || !row.Table.Columns.Contains(columna)) return valorDefault;
            if (row[columna] == DBNull.Value) return valorDefault;

            return row[columna].ToString();
        }

        /// <summary>
        /// Devuelve la fecha de la columna, o null si no existe o es DBNull.
        /// </summary>
        public static DateTime? ObtenerFecha(DataRow row, string columna)
        {
            if (row == null || !row.Table.Columns.Contains(columna)) return null;
            if (row[columna] == DBNull.Value) return null;

            try { return Convert.ToDateTime(row[columna]); }
            catch { return null; }
        }

        /// <summary>
        /// Devuelve el bool de la columna, o false si no existe o es DBNull.
        /// </summary>
        public static bool ObtenerBool(DataRow row, string columna, bool valorDefault = false)
        {
            if (row == null || !row.Table.Columns.Contains(columna)) return valorDefault;
            if (row[columna] == DBNull.Value) return valorDefault;

            try { return Convert.ToBoolean(row[columna]); }
            catch { return valorDefault; }
        }

        /// <summary>
        /// Devuelve true si la columna existe en el DataRow y tiene valor NO nulo.
        /// </summary>
        public static bool TieneValor(DataRow row, string columna)
        {
            if (row == null || !row.Table.Columns.Contains(columna)) return false;
            return row[columna] != DBNull.Value;
        }
    }
}