using SistemaContable.DAL;
using System;
using System.Data;

namespace SistemaContable.UI.Helpers
{
    /// <summary>
    /// Helper para obtener/consultar números correlativos de partidas contables
    /// mediante el SP CONTA.SP_NUMERADOR_PARTIDA.
    /// </summary>
    public static class NumeradorPartidaHelper
    {
        // ============================================================
        // DTO con el resultado del numerador
        // ============================================================
        public class InfoNumerador
        {
            public string   TipoPartida         { get; set; }
            public string   Descripcion         { get; set; }
            public short    Anio                { get; set; }
            public byte     Mes                 { get; set; }
            public int      NumUltimo           { get; set; }
            public string   NumUltimoFormateado { get; set; }
            public int?     NumSiguiente        { get; set; }   // solo en Consultar
            public string   NumSiguienteFormateado  { get; set; }   // solo en Consultar          
            public DateTime? FechaUltimo        { get; set; }
            public string   UserUltimo          { get; set; }
            
        }

        // ============================================================
        // OBTENER SIGUIENTE (consume el número - lo incrementa)
        // ============================================================

        /// <summary>
        /// Obtiene el siguiente número de partida para el tipo/año/mes.
        /// Incrementa el numerador. Si no existe, lo crea con NUM_ULTIMO = 1.
        /// </summary>
        /// <param name="tipoPartida">Código del tipo de partida (ej: "DIA")</param>
        /// <param name="anio">Año (2000-2999)</param>
        /// <param name="mes">Mes (1-12)</param>
        /// <returns>InfoNumerador con el número asignado y su formato</returns>
        public static InfoNumerador ObtenerSiguiente(
            string tipoPartida, int anio, int mes)
        {
            var dal = new DALBase();
            var dt = dal.EjecutarConsulta("CONTA.SP_NUMERADOR_PARTIDA", new
            {
                ACCION       = "OBTENER_SIGUIENTE",
                TIPO_PARTIDA = tipoPartida,
                ANIO         = (short)anio,
                MES          = (byte)mes,
                USUARIO      = Configuracion.UsuarioActual
            });

            if (dt.Rows.Count == 0)
                throw new Exception(
                    $"No se pudo obtener el siguiente número de partida para " +
                    $"'{tipoPartida}' {anio}/{mes}.");

            return MapearFila(dt.Rows[0]);
        }

        /// <summary>
        /// Obtiene el siguiente número de partida usando el año y mes de una fecha.
        /// </summary>
        public static InfoNumerador ObtenerSiguiente(string tipoPartida, DateTime fecha)
        {
            return ObtenerSiguiente(tipoPartida, fecha.Year, fecha.Month);
        }

        /// <summary>
        /// Obtiene el siguiente número de partida usando la fecha actual.
        /// </summary>
        public static InfoNumerador ObtenerSiguiente(string tipoPartida)
        {
            return ObtenerSiguiente(tipoPartida, DateTime.Today);
        }

        // ============================================================
        // CONSULTAR (no modifica - solo lee el estado actual)
        // ============================================================

        /// <summary>
        /// Consulta el estado actual del numerador sin modificarlo.
        /// Útil para mostrar en pantalla cuál sería el próximo número.
        /// </summary>
        /// <returns>InfoNumerador con NumUltimo y NumSiguiente. NULL si no existe registro.</returns>
        public static InfoNumerador Consultar(
            string tipoPartida, int anio, int mes)
        {
            var dal = new DALBase();
            var dt = dal.EjecutarConsulta("CONTA.SP_NUMERADOR_PARTIDA", new
            {
                ACCION       = "CONSULTAR",
                TIPO_PARTIDA = tipoPartida,
                ANIO         = (short)anio,
                MES          = (byte)mes
            });

            if (dt.Rows.Count == 0) return null;

            return MapearFilaConsulta(dt.Rows[0]);
        }

        public static InfoNumerador Consultar(string tipoPartida, DateTime fecha)
        {
            return Consultar(tipoPartida, fecha.Year, fecha.Month);
        }

        public static InfoNumerador Consultar(string tipoPartida)
        {
            return Consultar(tipoPartida, DateTime.Today);
        }

        // ============================================================
        // Mapeos internos
        // ============================================================

        // Mapeo para el resultado de OBTENER_SIGUIENTE
        private static InfoNumerador MapearFila(DataRow r)
        {
            return new InfoNumerador
            {                
                NumUltimo               = SafeInt(r,      "NUM_ULTIMO"),
                NumUltimoFormateado     = SafeStr(r,      "NUM_ULTIMO_FORMATEADO")             
            };
        }

        // Mapeo para el resultado de CONSULTAR (tiene columnas adicionales)
        private static InfoNumerador MapearFilaConsulta(DataRow r)
        {
            return new InfoNumerador
            {
                TipoPartida        = SafeStr(r,   "TIPO_PARTIDA"),
                Descripcion        = SafeStr(r,   "DESCRIPCION"),
                Anio               = SafeShort(r, "ANIO"),
                Mes                = SafeByte(r,  "MES"),
                NumUltimo          = SafeInt(r,   "NUM_ULTIMO"),
                NumUltimoFormateado         = SafeStr(r,   "NUM_ULTIMO_FORMATEADO"),
                NumSiguiente                = SafeIntNull(r, "NUM_SIGUIENTE"),
                NumSiguienteFormateado      = SafeStr(r,   "NUM_SIGUIENTE_FORMATEADO"),               
                FechaUltimo        = SafeDate(r,  "FECHA_ULTIMO"),
                UserUltimo         = SafeStr(r,   "USER_ULTIMO")
            };
        }

        // ============================================================
        // Helpers de conversión segura
        // ============================================================
        private static string SafeStr(DataRow r, string col)
        {
            if (!r.Table.Columns.Contains(col)) return null;
            return r[col] == DBNull.Value ? null : r[col].ToString();
        }

        private static int SafeInt(DataRow r, string col)
        {
            if (!r.Table.Columns.Contains(col) || r[col] == DBNull.Value) return 0;
            return Convert.ToInt32(r[col]);
        }

        private static int? SafeIntNull(DataRow r, string col)
        {
            if (!r.Table.Columns.Contains(col) || r[col] == DBNull.Value) return null;
            return Convert.ToInt32(r[col]);
        }

        private static short SafeShort(DataRow r, string col)
        {
            if (!r.Table.Columns.Contains(col) || r[col] == DBNull.Value) return 0;
            return Convert.ToInt16(r[col]);
        }

        private static byte SafeByte(DataRow r, string col)
        {
            if (!r.Table.Columns.Contains(col) || r[col] == DBNull.Value) return 0;
            return Convert.ToByte(r[col]);
        }

        private static DateTime? SafeDate(DataRow r, string col)
        {
            if (!r.Table.Columns.Contains(col) || r[col] == DBNull.Value) return null;
            return Convert.ToDateTime(r[col]);
        }
    }
}
