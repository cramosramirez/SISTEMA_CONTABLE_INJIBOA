using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using SistemaContable.DAL;

namespace SistemaContable.UI.Helpers
{
    public static class Actualizador
    {
        private const string ARCHIVO_VERSION = "version.txt";

        public static void VerificarActualizacion()
        {
            try
            {
                string rutaServidor = Configuracion.RutaServidor;
                if (string.IsNullOrWhiteSpace(rutaServidor)) return;

                string rutaTxt = Path.Combine(rutaServidor, ARCHIVO_VERSION);
                if (!File.Exists(rutaTxt)) return;

                string verStr     = File.ReadAllText(rutaTxt).Trim();
                Version verServ   = new Version(verStr);
                Version verLocal  = Assembly.GetExecutingAssembly().GetName().Version;

                if (verServ <= verLocal) return;

                // Nueva versión disponible — aplicar silenciosamente
                string archivoZip  = $"SistemaContable_{verStr}.zip";
                string rutaZip     = Path.Combine(rutaServidor, "Releases", archivoZip);
                string rutaUpdater = Path.Combine(rutaServidor, "Updater.exe");

                if (!File.Exists(rutaZip) || !File.Exists(rutaUpdater)) return;

                string tempUpdater = Path.Combine(
                    Path.GetTempPath(),
                    $"SC_Updater_{DateTime.Now.Ticks}.exe");

                File.Copy(rutaUpdater, tempUpdater, true);

                string dirApp = AppDomain.CurrentDomain.BaseDirectory;

                Process.Start(new ProcessStartInfo
                {
                    FileName       = tempUpdater,
                    Arguments      = $"\"{rutaZip}\" \"{dirApp}\"",
                    WindowStyle    = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                });

                Application.Exit();
                Environment.Exit(0);
            }
            catch
            {
                // Fallo silencioso — el usuario continúa normalmente
            }
        }
    }
}
