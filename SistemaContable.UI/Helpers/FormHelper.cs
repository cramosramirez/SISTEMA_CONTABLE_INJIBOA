using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using ComboBox = System.Windows.Forms.ComboBox;

namespace SistemaContable.UI.Helpers
{
    public static class FormHelper
    {

        private static readonly System.Text.RegularExpressions.Regex _regexCorreo =
            new System.Text.RegularExpressions.Regex(
               @"^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$",
               System.Text.RegularExpressions.RegexOptions.Compiled);

        public static void Inicializar(Form formulario)
        {
            //AplicarPropiedadesEstandar(formulario);
            AplicarEnterComoTab(formulario);
            AplicarMayusculas(formulario);            
        }

        /// <summary>
        /// Aplica las propiedades estándar a todos los formularios del sistema.
        /// Llamar en el evento Load de cada formulario.
        /// </summary>
        public static void AplicarPropiedadesEstandar(Form formulario)
        {
            formulario.ShowInTaskbar = false;
            formulario.MinimizeBox = false;
            formulario.MaximizeBox = false;
            formulario.StartPosition = FormStartPosition.CenterParent;            
        }

        /// <summary>
        /// Recorre todos los controles del formulario y asigna
        /// el comportamiento Enter = Tab automáticamente.
        /// Llamar en el evento Load de cada formulario.
        /// </summary>        
        public static void AplicarEnterComoTab(Control contenedor)
        {
            foreach (Control ctrl in contenedor.Controls)
            {
                // Controles DevExpress
                if (ctrl is TextEdit ||
                    ctrl is TextBox  ||
                    ctrl is ButtonEdit ||
                    ctrl is LookUpEdit ||
                    ctrl is SearchLookUpEdit ||
                    ctrl is SpinEdit ||
                    ctrl is DateEdit ||
                    ctrl is MemoEdit ||                    
                    ctrl is ComboBoxEdit)
                {
                    ctrl.KeyDown -= Control_EnterComoTab; // evitar duplicados
                    ctrl.KeyDown += Control_EnterComoTab;
                }

                // Controles estándar WinForms
                if (ctrl is TextBox ||
                    ctrl is CheckBox ||
                    ctrl is RichTextBox ||
                    ctrl is NumericUpDown ||
                    ctrl is MaskedTextBox  ||
                    ctrl is ComboBox)
                {
                    ctrl.KeyDown -= Control_EnterComoTab;
                    ctrl.KeyDown += Control_EnterComoTab;
                }

                // Recursivo para contenedores (Panel, GroupBox, LayoutControl etc.)
                if (ctrl.HasChildren)
                    AplicarEnterComoTab(ctrl);
            }
        }

        public static void AplicarMayusculas(Control contenedor)
        {
            foreach (Control ctrl in contenedor.Controls)
            {
                // Si el control está marcado como NO_MAYUSCULAS, se respeta tal cual
                bool excluido = ctrl.Tag is string tag &&
                                tag.Equals("NO_MAYUSCULAS", StringComparison.OrdinalIgnoreCase);

                if (!excluido)
                {
                   if (ctrl is TextEdit ||
                   ctrl is ButtonEdit ||
                   ctrl is MemoEdit)
                    {
                        ((TextEdit)ctrl).Properties.CharacterCasing = CharacterCasing.Upper;
                    }

                    // Controles estándar WinForms
                    if (ctrl is TextBox tb)
                        tb.CharacterCasing = CharacterCasing.Upper;
                }
                // Recursivo para contenedores
                if (ctrl.HasChildren)
                    AplicarMayusculas(ctrl);
            }
        }

        private static void Control_EnterComoTab(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var ctrl = sender as Control;

                // Si el campo tiene búsqueda registrada y contiene *
                // dejar que Campo_AsteriscoBusqueda_KeyDown lo maneje
                if (ctrl != null &&
                    _lookups.ContainsKey(ctrl) &&
                    ctrl.Text?.Trim() == "*")
                    return;

                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        // Configuración del SP y columnas
        private static readonly Dictionary<Control, BusquedaConfig> _lookups
            = new Dictionary<Control, BusquedaConfig>();

        // Callback de asignación de campos
        private static readonly Dictionary<Control, Action<DataRow>> _callbacks
            = new Dictionary<Control, Action<DataRow>>();


        public static void RegistrarBusqueda(
            TextBox  campo, BusquedaConfig config, Action<DataRow> alSeleccionar)
        {
            _lookups[campo] = config;
            _callbacks[campo] = alSeleccionar;                  
            campo.KeyDown -= Campo_AsteriscoBusqueda_KeyDown;
            campo.KeyDown += Campo_AsteriscoBusqueda_KeyDown;
        }

        private static void Campo_AsteriscoBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            var campo = sender as TextBox;
            if (campo == null) return;
            if (!_lookups.ContainsKey(campo)) return;
            if (campo.Text?.Trim() != "*") return;

            e.SuppressKeyPress = true;
            AbrirBusqueda(campo);
        }

        /// <summary>
        /// Abre el formulario de búsqueda asociado al campo sin que el usuario
        /// tenga que digitar * + Enter. Reutiliza la misma lógica del KeyDown.
        /// </summary>
        public static void AbrirBusqueda(TextBox campo)
        {
            if (campo == null) return;
            if (!_lookups.ContainsKey(campo)) return;

            var config = _lookups[campo];

            using (var frm = new frmBusquedaGenerica(config))
            {
                Point posicionCampo = campo.PointToScreen(new Point(0, campo.Height));
                Rectangle pantalla = Screen.FromControl(campo).WorkingArea;
                int posX = posicionCampo.X;
                int posY = posicionCampo.Y;

                if (posY + frm.Height > pantalla.Bottom)
                    posY = posicionCampo.Y - campo.Height - frm.Height;
                if (posX + frm.Width > pantalla.Right)
                    posX = pantalla.Right - frm.Width;

                frm.StartPosition = FormStartPosition.Manual;
                frm.Location = new Point(posX, posY);

                if (frm.ShowDialog() == DialogResult.OK && frm.FilaSeleccionada != null)
                {
                    campo.Text = string.Empty;

                    if (_callbacks.ContainsKey(campo))
                        _callbacks[campo].Invoke(frm.FilaSeleccionada);

                    SendKeys.Send("{TAB}");
                }
                else
                {
                    campo.Text = string.Empty;
                }
            }
        }

        public static string ObtenerUUID()
        {
            return Guid.NewGuid().ToString().ToUpper();    
        } 

        #region Operaciones de formulario

        /// <summary>
        /// Limpia todos los controles del formulario
        /// </summary>
        public static void LimpiarControles(Control contenedor)
        {
            foreach (Control ctrl in contenedor.Controls)
            {
                if (ctrl is TextBox tb)
                    tb.Text = string.Empty;
                else if (ctrl is MaskedTextBox mtb)
                    mtb.Text = string.Empty;
                else if (ctrl is ComboBox cbx)
                    cbx.SelectedIndex = 0; // selecciona la primera opción (-- SELECCIONE --)
                else if (ctrl is CheckBox chk)
                    chk.Checked = false;
                else if (ctrl is DateTimePicker dtp)
                    dtp.Value = DateTime.Today;
                // Controles DevExpress
                else if (ctrl is TextEdit te)
                    te.Text = string.Empty;
                else if (ctrl is ComboBoxEdit cbxe)
                    cbxe.SelectedIndex = 0;
                else if (ctrl is DateEdit de)
                    de.DateTime = DateTime.Today;
                else if (ctrl is CheckEdit che)
                    che.Checked = false;
                else if (ctrl is SpinEdit se)
                    se.Value = 0;
                // Recursivo para contenedores
                if (ctrl.HasChildren)
                    LimpiarControles(ctrl);
            }
        }

        #endregion

        #region Validación de fechas

        /// <summary>
        /// Valida que un MaskedTextBox contenga una fecha válida.
        /// Retorna true si es válida, false si no lo es.
        /// </summary>
        public static bool ValidarFecha(MaskedTextBox campo, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(campo.Text.Replace("/", "").Trim()))
            {
                XtraMessageBox.Show($"El campo '{nombreCampo}' es requerido.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                campo.Focus();
                return false;
            }

            if (!DateTime.TryParseExact(campo.Text, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out _))
            {
                XtraMessageBox.Show($"La '{nombreCampo}' no es válida. ",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                campo.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Convierte el texto de un MaskedTextBox a DateTime.
        /// Retorna null si el campo está vacío.
        /// </summary>
        public static DateTime? ObtenerFecha(MaskedTextBox campo)
        {
            if (string.IsNullOrWhiteSpace(campo.Text.Replace("/", "").Trim()))
                return null;
            if (DateTime.TryParseExact(campo.Text, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime fecha))
                return fecha;
            return null;
        }

        public static string ObtenerSalfec(MaskedTextBox campo)
        {
            if (string.IsNullOrWhiteSpace(campo.Text.Replace("/", "").Trim()))
                return null;

            if (DateTime.TryParseExact(campo.Text, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime fecha))
            {
                return fecha.ToString("yyyyMM");
            }

            return null;
        }

        public static DialogResult MostrarMensajeHtml(
            string mensaje,
            string titulo = "Información",
            MessageBoxIcon icono = MessageBoxIcon.Information)
        {
            var iconoSys = icono == MessageBoxIcon.Warning ? SystemIcons.Warning :
                           icono == MessageBoxIcon.Error ? SystemIcons.Error :
                           icono == MessageBoxIcon.Question ? SystemIcons.Question :
                                                             SystemIcons.Information;

            var args = new XtraMessageBoxArgs
            {
                Caption = titulo,
                Text = mensaje,
                Buttons = new[] { DialogResult.OK },
                DefaultButtonIndex = 0,
                Icon = iconoSys,
                AllowHtmlText = DefaultBoolean.True
            };

            return XtraMessageBox.Show(args);
        }
        #endregion
        
        public static bool EsCorreoValido(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo)) return true;   // vacío es válido (no obligatorio)
            return _regexCorreo.IsMatch(correo.Trim());
        }

        public static void ResaltarCombosEnFoco(Control padre, Color? colorFoco = null)
        {
            Color color = colorFoco ?? Color.FromArgb(255, 254, 248);

            foreach (Control ctrl in padre.Controls)
            {
                if (ctrl is ComboBox cbx)
                    AplicarResaltadoCombo(cbx, color);

                if (ctrl.HasChildren)
                    ResaltarCombosEnFoco(ctrl, colorFoco);
            }
        }

        private static void AplicarResaltadoCombo(ComboBox combo, Color colorFoco)
        {
            Color colorNormal = SystemColors.Window;
            Color colorInhabilita = SystemColors.Control;   // gris estándar de Windows

            combo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;

            combo.DrawItem += (s, e) =>
            {
                if (e.Index < 0) return;

                // ============ FONDO ============
                Color fondo;
                if (!combo.Enabled)
                    fondo = colorInhabilita;
                else if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    fondo = SystemColors.Highlight;
                else if (combo.Focused)
                    fondo = colorFoco;
                else
                    fondo = colorNormal;

                using (var brush = new SolidBrush(fondo))
                    e.Graphics.FillRectangle(brush, e.Bounds);

                // ============ TEXTO ============
                string texto = combo.GetItemText(combo.Items[e.Index]);

                Color colorTexto;
                if (!combo.Enabled)
                    colorTexto = SystemColors.GrayText;
                else if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    colorTexto = SystemColors.HighlightText;
                else
                    colorTexto = SystemColors.WindowText;

                using (var brushTexto = new SolidBrush(colorTexto))
                    e.Graphics.DrawString(texto, e.Font, brushTexto, e.Bounds);

                e.DrawFocusRectangle();
            };
            combo.Enter += (s, e) => combo.Invalidate();
            combo.Leave += (s, e) => combo.Invalidate();
            combo.EnabledChanged += (s, e) => combo.Invalidate();   // ← clave
        }

        public static List<MesItem> ObtenerMeses()
        {
            return new List<MesItem>
    {
        new MesItem { Value = "01", Mes = "Enero" },
        new MesItem { Value = "02", Mes = "Febrero" },
        new MesItem { Value = "03", Mes = "Marzo" },
        new MesItem { Value = "04", Mes = "Abril" },
        new MesItem { Value = "05", Mes = "Mayo" },
        new MesItem { Value = "06", Mes = "Junio" },
        new MesItem { Value = "07", Mes = "Julio" },
        new MesItem { Value = "08", Mes = "Agosto" },
        new MesItem { Value = "09", Mes = "Septiembre" },
        new MesItem { Value = "10", Mes = "Octubre" },
        new MesItem { Value = "11", Mes = "Noviembre" },
        new MesItem { Value = "12", Mes = "Diciembre" }
    };
        }
    }

    public class MesItem
    {
        public string Value { get; set; }
        public string Mes { get; set; }
    }
    public class TipLibro
    {    public int Value { get; set; }
        public string Libro { get; set; }

    }
}