using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace SistemaContable.DAL
{
    /// <summary>
    /// Logger centralizado. Escribe a la tabla LOG_ERROR vía SP_LOG.
    /// Si la BD no responde, escribe a un archivo de fallback local.
    /// </summary>
    public static class Logger
    {
        private static string CadenaConexion =>
            ConfigurationManager.ConnectionStrings["SistemaContable"].ConnectionString;

        // Palabras que indican un parámetro sensible (no se loguea su valor)
        private static readonly string[] PalabrasClaveSensibles =
            { "PASSWORD", "CLAVE", "TOKEN", "SECRET", "PWD" };

        // ============================================================
        // API pública
        // ============================================================

        /// <summary>
        /// Registra un error con su excepción asociada.
        /// </summary>
        public static void Error(string mensaje, Exception ex = null,
            string modulo = null, string spNombre = null, object parametros = null)
        {
            Log("ERROR", mensaje, ex, modulo, spNombre, parametros);
        }

        /// <summary>
        /// Registra una advertencia (algo inusual pero no es error técnico).
        /// </summary>
        public static void Advertencia(string mensaje, string modulo = null)
        {
            Log("WARNING", mensaje, null, modulo, null, null);
        }

        /// <summary>
        /// Registra información (operaciones importantes a auditar).
        /// </summary>
        public static void Info(string mensaje, string modulo = null)
        {
            Log("INFO", mensaje, null, modulo, null, null);
        }

        // ============================================================
        // Núcleo
        // ============================================================
        private static void Log(string nivel, string mensaje, Exception ex,
            string modulo, string spNombre, object parametros)
        {
            try
            {
                string detalle = ex?.ToString();
                string paramsJson = SerializarParametrosSeguro(parametros);
                string usuario = ObtenerUsuarioActual();

                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Execute("SP_LOG", new
                    {
                        ACCION = "INSERTAR",
                        NIVEL = nivel,
                        USUARIO = usuario,
                        EQUIPO = Environment.MachineName,
                        MODULO = Truncar(modulo, 200),
                        SP_NOMBRE = Truncar(spNombre, 200),
                        MENSAJE = mensaje ?? "(sin mensaje)",
                        DETALLE = detalle,
                        PARAMETROS = paramsJson
                    }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception logEx)
            {
                // Si falla el log a BD, escribir a archivo de fallback
                EscribirArchivoFallback(nivel, mensaje, ex, modulo, spNombre, logEx);
            }
        }

        // ============================================================
        // Fallback a archivo
        // ============================================================
        private static void EscribirArchivoFallback(string nivel, string mensaje,
            Exception ex, string modulo, string spNombre, Exception logEx)
        {
            try
            {
                string carpeta = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "SistemaContable", "Logs");

                Directory.CreateDirectory(carpeta);

                string archivo = Path.Combine(carpeta,
                    $"log_{DateTime.Now:yyyyMMdd}.txt");

                var linea = new System.Text.StringBuilder();
                linea.AppendLine("==========================================================");
                linea.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{nivel}]");
                linea.AppendLine($"  Modulo:    {modulo}");
                linea.AppendLine($"  SP:        {spNombre}");
                linea.AppendLine($"  Equipo:    {Environment.MachineName}");
                linea.AppendLine($"  Usuario:   {ObtenerUsuarioActual()}");
                linea.AppendLine($"  Mensaje:   {mensaje}");
                if (ex != null)
                    linea.AppendLine($"  Exception: {ex}");
                linea.AppendLine($"  (Log a BD falló: {logEx?.Message})");
                linea.AppendLine();

                File.AppendAllText(archivo, linea.ToString());
            }
            catch
            {
                // Si hasta el archivo falla, no hacemos más nada
            }
        }

        // ============================================================
        // Serialización segura de parámetros
        // (oculta valores que parezcan sensibles)
        // ============================================================
        private static string SerializarParametrosSeguro(object parametros)
        {
            if (parametros == null) return null;

            try
            {
                var dict = new Dictionary<string, object>();
                var props = parametros.GetType().GetProperties();

                foreach (var prop in props)
                {
                    string nombre = prop.Name.ToUpperInvariant();
                    bool esSensible = false;

                    foreach (var clave in PalabrasClaveSensibles)
                    {
                        if (nombre.Contains(clave))
                        {
                            esSensible = true;
                            break;
                        }
                    }

                    dict[prop.Name] = esSensible
                        ? "***"
                        : prop.GetValue(parametros);
                }

                return Newtonsoft.Json.JsonConvert.SerializeObject(dict);
            }
            catch
            {
                return "(no serializable)";
            }
        }

        // ============================================================
        // Usuario actual (intenta leer de Configuracion)
        // ============================================================
        private static string ObtenerUsuarioActual()
        {
            try
            {
                // Si Configuracion.UsuarioActual no está disponible, usa Environment.UserName
                return Configuracion.UsuarioActual ?? Environment.UserName;
            }
            catch
            {
                return Environment.UserName;
            }
        }

        private static string Truncar(string s, int maxLen)
            => string.IsNullOrEmpty(s) || s.Length <= maxLen
                ? s
                : s.Substring(0, maxLen);
    }
}