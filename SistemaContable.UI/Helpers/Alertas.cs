using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Helpers
{
    public static class Alertas
    {
        /// <summary>
        /// Formulario de Alertas del sistema
        /// </summary>
        public static void Error(string mensaje)
        {
            MessageBox.Show(mensaje, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void Exito(string mensaje)
        {
            MessageBox.Show(mensaje, "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void Advertencia(string mensaje)
        {
            MessageBox.Show(mensaje, "Advertencia",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Pregunta Si/No al usuario. Devuelve true si confirma (Yes).
        /// </summary>
        public static bool Confirmar(string mensaje, string titulo = "Confirmar")
        {
            var resultado = MessageBox.Show(mensaje, titulo,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2); // por defecto en "No" para evitar borrados accidentales

            return resultado == DialogResult.Yes;
        }
    }
}