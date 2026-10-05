using SistemaContable.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SistemaContable.UI.Helpers
{
    internal sealed class ClientePlanillaValidacion
    {
        public int IdComprobante { get; set; }
        public int? IdEntidad { get; set; }
        public string CodigoImportado { get; set; }
        public string Nombre { get; set; }
    }

    internal static class ValidadorClientesPlanilla
    {
        public static List<string> Validar(
            DALBase dal,
            IEnumerable<ClientePlanillaValidacion> clientes,
            bool esTransportistas)
        {
            var lista = clientes?.ToList() ?? new List<ClientePlanillaValidacion>();
            var problemas = new List<string>();

            foreach (var cliente in lista.Where(x => string.IsNullOrWhiteSpace(x.CodigoImportado)))
                problemas.Add(Describir(cliente, "el código importado está vacío"));

            foreach (var cliente in lista.Where(x => !x.IdEntidad.HasValue || x.IdEntidad.Value <= 0))
                problemas.Add(Describir(cliente,
                    "no está relacionado con un cliente de ENTIDAD; revise CODIPROVEEDOR o CODTRANSPORT"));

            int[] idsEntidad = lista
                .Where(x => x.IdEntidad.HasValue && x.IdEntidad.Value > 0)
                .Select(x => x.IdEntidad.Value)
                .Distinct()
                .ToArray();

            if (idsEntidad.Length == 0)
                return problemas.Distinct().ToList();

            string ids = string.Join(",", idsEntidad);
            DataTable datos = dal.EjecutarConsultaSql($@"
SELECT
    E.ID_ENTIDAD,
    E.CODIPROVEEDOR,
    E.CODTRANSPORT,
    CASE WHEN EXISTS
    (
        SELECT 1
        FROM dbo.ENTIDAD_CLIENTE EC
        WHERE EC.ID_ENTIDAD = E.ID_ENTIDAD
          AND LEN(TRIM(ISNULL(EC.CUENTA_X_COBRAR, ''))) > 0
    ) THEN 1 ELSE 0 END AS TIENE_CUENTA_X_COBRAR
FROM dbo.ENTIDAD E
WHERE E.ID_ENTIDAD IN ({ids});");

            var porEntidad = datos.AsEnumerable()
                .ToDictionary(r => Convert.ToInt32(r["ID_ENTIDAD"]));

            foreach (var cliente in lista.Where(x => x.IdEntidad.HasValue && x.IdEntidad.Value > 0))
            {
                if (!porEntidad.TryGetValue(cliente.IdEntidad.Value, out DataRow entidad))
                {
                    problemas.Add(Describir(cliente, "la entidad relacionada ya no existe"));
                    continue;
                }

                string codigoRequerido = Convert.ToString(
                    entidad[esTransportistas ? "CODTRANSPORT" : "CODIPROVEEDOR"]);

                if (string.IsNullOrWhiteSpace(codigoRequerido))
                {
                    string campo = esTransportistas ? "CODTRANSPORT" : "CODIPROVEEDOR";
                    problemas.Add(Describir(cliente, $"el campo {campo} está vacío en ENTIDAD"));
                }

                if (Convert.ToInt32(entidad["TIENE_CUENTA_X_COBRAR"]) == 0)
                    problemas.Add(Describir(cliente, "no tiene CUENTA_X_COBRAR en ENTIDAD_CLIENTE"));
            }

            return problemas.Distinct().ToList();
        }

        private static string Describir(ClientePlanillaValidacion cliente, string problema)
        {
            string codigo = string.IsNullOrWhiteSpace(cliente.CodigoImportado)
                ? "SIN CÓDIGO"
                : cliente.CodigoImportado.Trim();
            string nombre = string.IsNullOrWhiteSpace(cliente.Nombre)
                ? "SIN NOMBRE"
                : cliente.Nombre.Trim();

            return $"{codigo} - {nombre}: {problema}.";
        }
    }
}
