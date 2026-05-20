using System;
using System.Configuration;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.UserSkins;
using SistemaContable.UI.Helpers;

namespace SistemaContable.UI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. Aplicar skin Office 2019 ColorFull
            SkinManager.EnableFormSkins();
            OfficeSkins.Register();
            DevExpress.LookAndFeel.UserLookAndFeel.Default
                      .SetSkinStyle("Office 2019 Colorful");
         

            // 2. Verificar actualización silenciosa ANTES del login
            Actualizador.VerificarActualizacion();

            // 3. Mostrar Ribbon
            Application.Run(new Forms.frmPrincipalRibbon());
        }
    }
}
