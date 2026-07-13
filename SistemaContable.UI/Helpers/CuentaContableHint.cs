using SistemaContable.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.UI.Helpers
{
    public static class CuentaContableHint
    {
        private static readonly Dictionary<string, (string Nombre, bool EsValida)> _cache
            = new Dictionary<string, (string, bool)>();

        private static DALBase _dal;

        public static void Configurar(DALBase dal)
        {
            _dal = dal;
        }

        // Devuelve el texto a mostrar en el tooltip/label:
        // - Nombre de la cuenta si es válida (de detalle)
        // - Mensaje de error si no existe o es acumulativa
        public static (string Texto, bool EsValida) Obtener(string codigoCuenta)
        {
            if (string.IsNullOrWhiteSpace(codigoCuenta))
                return ("", true); // vacío no es error, solo no hay nada que mostrar

            if (_cache.TryGetValue(codigoCuenta, out var cacheado))
                return (cacheado.Nombre, cacheado.EsValida);

            (string Texto, bool EsValida) resultado;

            try
            {
                var dt = _dal.EjecutarConsulta("SP_CATALOGO_CUENTA", new
                {
                    ACCION = "OBTENER",
                    CUENTA = codigoCuenta
                });

                if (dt.Rows.Count == 0)
                {
                    resultado = ("CUENTA NO EXISTE", false);
                }
                else
                {
                    var fila = dt.Rows[0];
                    bool esDetalle = fila["ES_DETALLE"] != DBNull.Value && Convert.ToBoolean(fila["ES_DETALLE"]);

                    resultado = esDetalle
                        ? (fila["NOMBRE_CUENTA"].ToString(), true)
                        : ("CUENTA ACUMULATIVA (NO ASIGNABLE)", false);
                }
            }
            catch
            {
                resultado = ("", true); // si falla la consulta, no molestar con tooltip
            }

            _cache[codigoCuenta] = resultado;
            return resultado;
        }
    }
}
