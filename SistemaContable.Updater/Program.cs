using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;

namespace SistemaContable.Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2) return;

            string rutaZip  = args[0];
            string dirLocal = args[1];
            string tempDir  = Path.Combine(Path.GetTempPath(),
                              "SC_Update_" + DateTime.Now.Ticks);

            try
            {
                EsperarCierreSistema();

                Directory.CreateDirectory(tempDir);
                ZipFile.ExtractToDirectory(rutaZip, tempDir);
                ReemplazarArchivos(tempDir, dirLocal);

                Directory.Delete(tempDir, true);

                string exe = Path.Combine(dirLocal, "SistemaContable.exe");
                if (File.Exists(exe))
                    Process.Start(exe);
            }
            catch
            {
                try
                {
                    string exe = Path.Combine(dirLocal, "SistemaContable.exe");
                    if (File.Exists(exe)) Process.Start(exe);
                }
                catch { }

                try { if (Directory.Exists(tempDir)) Directory.Delete(tempDir, true); }
                catch { }
            }
        }

        static void EsperarCierreSistema()
        {
            for (int i = 0; i < 30; i++)
            {
                bool abierto = false;
                foreach (var p in Process.GetProcessesByName("SistemaContable"))
                { abierto = true; break; }
                if (!abierto) break;
                Thread.Sleep(500);
            }
        }

        static void ReemplazarArchivos(string origen, string destino)
        {
            foreach (string archivo in Directory.GetFiles(origen, "*.*",
                     SearchOption.AllDirectories))
            {
                string relativo = archivo.Substring(origen.Length + 1);

                // Preservar App.config local (contiene cadena de conexión)
                if (relativo.Equals("SistemaContable.exe.config",
                    StringComparison.OrdinalIgnoreCase)) continue;

                string destFinal = Path.Combine(destino, relativo);
                string dir       = Path.GetDirectoryName(destFinal);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                File.Copy(archivo, destFinal, true);
            }
        }
    }
}
