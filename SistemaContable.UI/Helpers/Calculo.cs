using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.UI.Helpers
{
    /// <summary>
    /// Utilidades de cálculo numérico para todo el sistema.
    /// </summary>
    public static class Calculo
    {
        /// <summary>
        /// Redondea con reglas contables (siempre sube en .5),
        /// no con banker's rounding como hace Math.Round por defecto.
        /// </summary>
        public static decimal Redondear(decimal valor, int decimales = 2)
        {
            return Math.Round(valor, decimales, MidpointRounding.AwayFromZero);
        }
    }
}
