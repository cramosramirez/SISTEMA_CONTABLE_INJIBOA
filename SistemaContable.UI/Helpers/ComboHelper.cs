using System;
using System.Collections.Generic;
using System.Data;
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

        /// <summary>
        /// Indica si el combo tiene una selección válida.
        /// Considera como "sin selección":
        ///   * null / DBNull
        ///   * SelectedValue = -1 (valor centinela para "-- Seleccione --")
        ///   * SelectedValue es DataRowView (aún no bindeado)
        /// </summary>
        public static bool TieneSeleccion(ComboBox cbx)
        {
            if (cbx == null) return false;
            if (cbx.SelectedValue == null) return false;
            if (cbx.SelectedValue == DBNull.Value) return false;
            if (cbx.SelectedValue is DataRowView) return false;

            // Intentar convertir a int y verificar que no sea -1
            if (int.TryParse(cbx.SelectedValue.ToString(), out int valor))
                if (valor == -1) return false;

            return true;
        }
    }
}
