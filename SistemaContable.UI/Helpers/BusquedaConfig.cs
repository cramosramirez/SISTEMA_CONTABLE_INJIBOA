using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.UI.Helpers
{
    public class BusquedaConfig
    {
        /// <summary>Nombre del Stored Procedure a ejecutar</summary>
        public string StoredProcedure { get; set; }
        /// <summary>Nombre del Stored Procedure a ejecutar</summary>
        public string Accion { get; set; } = "BUSCAR"; // ← por defecto sera BUSCAR

        /// <summary>
        /// Nombre del parámetro de acción esperado por el procedimiento.
        /// La mayoría usa ACCION; algunos procedimientos de integración usan ACTION.
        /// </summary>
        public string NombreParametroAccion { get; set; } = "ACCION";

        /// <summary>
        /// Columnas a mostrar en el grid.
        /// Key   = nombre real del campo en el DataTable
        /// Value = título a mostrar en la columna del grid
        /// </summary>
        public Dictionary<string, string> Columnas { get; set; }

        // Anchos opcionales — Key = nombre del campo, Value = ancho en pixels
        public Dictionary<string, int> Anchos { get; set; }

        public List<string> ColumnasOcultas { get; set; } = new List<string>();

        /// <summary>
        /// Parámetros adicionales que se envían al SP
        /// además de ACCION y FILTRO.
        /// Ejemplo: new { ES_DETALLE = true }
        /// </summary>
        public object ParametrosExtra { get; set; }

        public Func<object> ObtenerParametrosExtra { get; set; }

        public BusquedaConfig()
        {
            Columnas = new Dictionary<string, string>();
            Anchos = new Dictionary<string, int>();
            ParametrosExtra = null;
            Accion = "BUSCAR";
        }
    }
}
