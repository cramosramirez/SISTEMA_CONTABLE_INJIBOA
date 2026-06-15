using System.Configuration;
using System.Reflection;

namespace SistemaContable.DAL
{
    public static class Configuracion
    {
        public static string CadenaConexion
        {
            get
            {
                return ConfigurationManager
                       .ConnectionStrings["SistemaContable"]
                       .ConnectionString;
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

        /// <summary>ID del rol asignado al usuario</summary>
        public static int IdRolActual { get; set; }

        /// <summary>Nombre del rol asignado al usuario</summary>
        public static string NombreRolActual { get; set; }
        public static int Id_Almacen { get; set; }
        public static int Id_Cajero { get; set; }

        #endregion


    }
}
