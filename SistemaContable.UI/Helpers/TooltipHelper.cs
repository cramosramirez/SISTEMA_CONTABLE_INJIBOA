using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Helpers
{

    public static class TooltipHelper
    {
        private static readonly ToolTip _toolTip = new ToolTip
        {
            ShowAlways = true,
            InitialDelay = 300,
            ReshowDelay = 100,
            AutoPopDelay = 5000
        };

        public static void Configurar(params (Control Control, string Mensaje)[] controles)
        {
            foreach (var item in controles)
            {
                item.Control.Tag = item.Mensaje;

                item.Control.Enter += MostrarAyuda;
                item.Control.Leave += OcultarAyuda;
            }
        }

        private static void MostrarAyuda(object sender, EventArgs e)
        {
            if (sender is Control ctrl &&
                string.IsNullOrWhiteSpace(ctrl.Text) &&
                ctrl.Tag is string mensaje)
            {
                _toolTip.Show(
                    mensaje,
                    ctrl,
                    0,
                    ctrl.Height + 5,
                    4000);
            }
        }

        private static void OcultarAyuda(object sender, EventArgs e)
        {
            if (sender is Control ctrl)
            {
                _toolTip.Hide(ctrl);
            }
        }
    }
}