using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace SistemaContable.UI.Helpers
{
    class MinisterioHaciendaHelper
    {
        private readonly HttpClient _http;
        private readonly Control _ownerControl;
        private readonly string[] _tiposDtePermitidos; // Array de tipos DTE permitidos

        // Delegados para actualizar controles específicos
        private readonly Action<string, Color> _actualizarEstado;
        private readonly Action<string> _actualizarSelloRecibido;
        private readonly Action<string> _actualizarCodGeneracion;
        private readonly Action<string> _actualizarNumControl;
        private readonly Action<string> _actualizarFechaEmision;
        private readonly Action<string, string> _actualizarComboTipoDte;
        private readonly Action<string> _actualizarObservacion;
        private readonly Action _onConsultaExitosa;

        public MinisterioHaciendaHelper(
            Control ownerControl,
            Action<string, Color> actualizarEstado,
            Action<string> actualizarSelloRecibido,
            Action<string> actualizarCodGeneracion,
            Action<string> actualizarNumControl,
            Action<string> actualizarFechaEmision,
            Action<string, string> actualizarComboTipoDte,
            Action<string> actualizarObservacion = null,
            Action onConsultaExitosa = null,
            params string[] tiposDtePermitidos) // Parámetro params para los tipos DTE
        {
            _http = new HttpClient();
            _http.Timeout = TimeSpan.FromSeconds(30);
            _ownerControl = ownerControl;
            _actualizarEstado = actualizarEstado;
            _actualizarSelloRecibido = actualizarSelloRecibido;
            _actualizarCodGeneracion = actualizarCodGeneracion;
            _actualizarNumControl = actualizarNumControl;
            _actualizarFechaEmision = actualizarFechaEmision;
            _actualizarComboTipoDte = actualizarComboTipoDte;
            _actualizarObservacion = actualizarObservacion;
            _onConsultaExitosa = onConsultaExitosa;
            // Si no se especifican tipos, usar "03" por defecto (crédito fiscal)
            _tiposDtePermitidos = tiposDtePermitidos?.Length > 0 ? tiposDtePermitidos : new[] { "03" };
        }

        // Método para verificar si el tipo DTE está permitido
        private bool EsTipoDtePermitido(string tipoDte)
        {
            if (string.IsNullOrWhiteSpace(tipoDte)) return false;

            foreach (var tipoPermitido in _tiposDtePermitidos)
            {
                if (tipoDte.Equals(tipoPermitido, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        // Obtener el mensaje descriptivo para el tipo de documento
        private string ObtenerMensajeTipoDocumento(JObject data)
        {
            string tipoDte = data["tipoDte"]?.ToString() ?? string.Empty;
            string nombreDte = data["nombDte"]?.ToString() ?? string.Empty;

            // Mapeo de tipos DTE a descripciones amigables
            var tiposDescriptivos = new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "01", "Factura" },            
            { "03", "Comprobante de crédito fiscal" },
            { "04", "Nota de remisión" },
            { "05", "Nota de crédito" },
            { "06", "Nota de débito" },
            { "07", "Comprobante de retención" },
            { "08", "Comprobante de liquidación" },
            { "09", "Documento contable de liquidación" },
            { "11", "Factura de exportación" },
            { "14", "Factura de sujeto excluido" },
            { "15", "Comprobante de donación" }
        };

            string descripcion = tiposDescriptivos.ContainsKey(tipoDte) ? tiposDescriptivos[tipoDte] : nombreDte.ToLower();
            return $"El documento no es un documento válido, es {descripcion}";
        }

        // Evento Leave para el TextBox de consulta
        public void OnTxtConsultaLeave(string urlConsulta)
        {
            string url = urlConsulta?.Trim();
            if (string.IsNullOrWhiteSpace(url)) return;

            _actualizarEstado("Consultando...", Color.Gray);

            Task.Run(() => ConsultarMH(url))
                .ContinueWith(t =>
                {
                    if (t.Exception != null)
                    {
                        _ownerControl.BeginInvoke(new Action(() =>
                        {
                            _actualizarEstado("No se obtuvo respuesta, intente consulta manual en el sitio Web del M.H.", Color.Red);
                        }));
                    }
                }, TaskContinuationOptions.OnlyOnFaulted);
        }

        private async Task ConsultarMH(string urlConsultaOficial)
        {
            try
            {
                string urlFinal = ConstruirUrlAPI(urlConsultaOficial);
                var response = await _http.GetAsync(urlFinal);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(json);

                _ownerControl.BeginInvoke(new Action(() => AsignarDatosMH(data)));
            }
            catch (TaskCanceledException)
            {
                _ownerControl.BeginInvoke(new Action(() =>
                {
                    _actualizarEstado("No se obtuvo respuesta, intente consulta manual en el sitio Web del M.H.", Color.Red);
                }));
            }
            catch (Exception)
            {
                _ownerControl.BeginInvoke(new Action(() =>
                {
                    _actualizarEstado("No se obtuvo respuesta, intente consulta manual en el sitio Web del M.H.", Color.Red);
                }));
            }
        }

        private string ConstruirUrlAPI(string urlUsuario)
        {
            try
            {
                Uri uri = new Uri(urlUsuario.ToUpper());
                var parametros = HttpUtility.ParseQueryString(uri.Query);
                string codGen = parametros["CODGEN"] ?? string.Empty;
                string fechaEmi = parametros["FECHAEMI"] ?? string.Empty;
                string ambiente = parametros["AMBIENTE"] ?? string.Empty;
                string baseUrl = ConfigurationManager.AppSettings["UrlConsultaMH"];
                return $"{baseUrl}codigoGeneracion={codGen}&fechaEmi={fechaEmi}&ambiente={ambiente}";
            }
            catch
            {
                return urlUsuario;
            }
        }

        private void AsignarDatosMH(JObject data)
        {
            try
            {
                string estadoDoc = data["estadoDoc"]?.ToString() ?? string.Empty;
                string descripcionEstado = data["descripcionEstado"]?.ToString() ?? string.Empty;
                var ajustes = data["ajustes"] as JArray;

                string textoEstado = $"{estadoDoc} - {descripcionEstado}";
                Color colorEstado = Color.Green;

                if (ajustes != null && ajustes.Count > 0)
                {
                    textoEstado += " - Documento posee ajustes";
                    colorEstado = Color.Red;
                }

                if (estadoDoc.Equals("Error", StringComparison.OrdinalIgnoreCase))
                {
                    colorEstado = Color.Red;
                    _actualizarEstado(textoEstado, colorEstado);
                    return;
                }
                else
                {
                    _actualizarEstado(textoEstado, colorEstado);
                }

                // Verificar si el tipo DTE está permitido
                string tipoDte = data["tipoDte"]?.ToString() ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(tipoDte) && EsTipoDtePermitido(tipoDte))
                {
                    _actualizarComboTipoDte?.Invoke("TIPO_DTE", tipoDte);
                }
                else
                {
                    _actualizarEstado(ObtenerMensajeTipoDocumento(data), Color.Red);
                    return;
                }

                _actualizarSelloRecibido(data["selloVal"]?.ToString() ?? string.Empty);
                _actualizarCodGeneracion(data["codGen"]?.ToString() ?? string.Empty);

                var identificacion = data["documento"]?["identificacion"];
                if (identificacion != null)
                {
                    _actualizarNumControl(identificacion["numeroControl"]?.ToString() ?? string.Empty);
                    string fecEmi = identificacion["fecEmi"]?.ToString() ?? string.Empty;
                    if (!string.IsNullOrWhiteSpace(fecEmi) && DateTime.TryParse(fecEmi, out DateTime fechaEmi))
                        _actualizarFechaEmision(fechaEmi.ToString("dd/MM/yyyy"));
                }

                var cuerpo = data["documento"]?["cuerpoDocumento"] as JArray;
                if (cuerpo != null && cuerpo.Count > 0 && _actualizarObservacion != null)
                    _actualizarObservacion(cuerpo[0]["descripcion"]?.ToString() ?? string.Empty);

                if (colorEstado == Color.Green && _onConsultaExitosa != null)
                {
                    _onConsultaExitosa();
                }
            }
            catch (Exception ex)
            {
                _actualizarEstado($"Error al procesar respuesta del MH: {ex.Message}", Color.Red);
            }
        }

        public void Dispose()
        {
            _http?.Dispose();
        }   
    }
}
