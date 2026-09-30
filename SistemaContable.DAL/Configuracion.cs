using System;
using System.Configuration;
using System.Reflection;

namespace SistemaContable.DAL
{
    public static class Configuracion
    {

        public static int PeriodoAnio { get; set; } = DateTime.Today.Year;
        public static int PeriodoMes { get; set; } = DateTime.Today.Month;

        /// <summary>
        /// Devuelve el período formateado como "Septiembre 2026".
        /// </summary>
        public static string PeriodoDescripcion
        {
            get
            {                
                var culturaEs = new System.Globalization.CultureInfo("es-ES");
                string nombreMes = culturaEs.DateTimeFormat.GetMonthName(PeriodoMes).ToUpper();                                
                return $"{nombreMes} {PeriodoAnio}";
            }
        }

        public static string CadenaConexion
        {
            get
            {
                var cs = ConfigurationManager.ConnectionStrings["SistemaContable"];
                if (cs == null)
                {
                    throw new InvalidOperationException(
                        "No se encontró la cadena de conexión 'SistemaContable' en el archivo de configuración. " +
                        "Verifique que el archivo SistemaContable.exe.config exista en el folder de la aplicación " +
                        "y contenga la sección <connectionStrings>.");
                }
                return cs.ConnectionString;
            }
        }

        public static string NombreEmpresa
        {
            get { return ConfigurationManager.AppSettings["NombreEmpresa"] ?? "INJIBOA"; }
        }

        public static string CodigoCCJIBOA
        {
            get { return ConfigurationManager.AppSettings["CodigoCCJIBOA"] ?? string.Empty; }
        }

        public static string CodigoHIBRONSA
        {
            get { return ConfigurationManager.AppSettings["CodigoHIBRONSA"] ?? string.Empty; }
        }

        public static string RutaServidor
        {
            get { return ConfigurationManager.AppSettings["RutaServidor"] ?? string.Empty; }
        }

        public static string VersionSistema
        {
            get
            {
                return Assembly.GetEntryAssembly()?
                               .GetName().Version?.ToString() ?? "1.0.0.0";
            }
        }

        #region Sesión del usuario activo

        /// <summary>Nombre de usuario (PK de la tabla USUARIO)</summary>
        public static string UsuarioActual { get; set; }

        /// <summary>Nombre completo del usuario</summary>
        public static string NombreUsuarioActual { get; set; }

        public static bool CerrarSesionSolicitada { get; set; } = false;

        /// <summary>ID del rol asignado al usuario</summary>
        public static int IdRolActual { get; set; }

        /// <summary>Nombre del rol asignado al usuario</summary>
        public static string NombreRolActual { get; set; }
        public static int Id_Almacen { get; set; }
        public static int Id_Cajero { get; set; }

        #endregion


    }
}
