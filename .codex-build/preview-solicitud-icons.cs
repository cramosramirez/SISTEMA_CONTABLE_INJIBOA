using SistemaContable.UI.Forms.Ventas;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;

internal static class PreviewSolicitudIcons
{
    [STAThread]
    private static void Main(string[] args)
    {
        string outputPath = args.Length > 0
            ? args[0]
            : "frmConsultaSolicitudAgricola-iconos.png";

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var form = new frmConsultaSolicitudAgricola("0001", 2026);
        typeof(frmConsultaSolicitudAgricola)
            .GetField("_datosCargados", BindingFlags.Instance | BindingFlags.NonPublic)
            .SetValue(form, true);

        var timer = new Timer { Interval = 800 };
        timer.Tick += (sender, eventArgs) =>
        {
            timer.Stop();
            Control[] seleccionar = form.Controls.Find("btnSeleccionar", true);
            if (seleccionar.Length > 0)
                seleccionar[0].Enabled = true;

            using (var bitmap = new Bitmap(form.Width, form.Height))
            {
                form.DrawToBitmap(bitmap, new Rectangle(Point.Empty, form.Size));
                bitmap.Save(outputPath, ImageFormat.Png);
            }

            form.Close();
        };
        form.Shown += (sender, eventArgs) => timer.Start();
        Application.Run(form);
    }
}
