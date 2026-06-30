using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Helpers
{
    /// <summary>
    /// Métodos auxiliares para obtener valores de ComboBox manejando
    /// la fila "-- Seleccione --" (que tiene DBNull como ValueMember).
    /// </summary>
    public static class ComboHelper
    {
        /// <summary>
        /// Devuelve el SelectedValue como int? (null si no hay selección).
        /// </summary>
        public static int? ObtenerInt(ComboBox cbx)
        {
            if (cbx?.SelectedValue == null || cbx.SelectedValue == DBNull.Value)
                return null;

            int valor = Convert.ToInt32(cbx.SelectedValue);
            return valor == -1 ? (int?)null : valor;
        }

        /// <summary>
        /// Devuelve el SelectedValue como string (null si no hay selección).
        /// </summary>
        public static string ObtenerString(ComboBox cbx)
        {
            if (cbx?.SelectedValue == null || cbx.SelectedValue == DBNull.Value)
                return null;
            return cbx.SelectedValue.ToString();
        }

        /// <summary>
        /// Asigna un valor al combo, o selecciona "-- Seleccione --" si es null/DBNull.
        /// </summary>
        public static void SeleccionarValor(ComboBox cbx, object valor)
        {
            if (cbx == null) return;

            if (valor == null || valor == DBNull.Value)
            {
                cbx.SelectedIndex = 0;
                return;
            }

            cbx.SelectedValue = valor;
        }
    }
}
