using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.RP.Esquemas
{
    class DetallePagosRealizadosPorFechaEsquema
    {

        public static DataTable Crear()
        {
            var dt = new DataTable("Factura");

            // Identificación / encabezado
            dt.Columns.Add("No", typeof(int));
            dt.Columns.Add("FECHA", typeof(DateTime));
            dt.Columns.Add("CHEQUE", typeof(string));
            dt.Columns.Add("TIPO", typeof(string));
            dt.Columns.Add("COMPROBANTE", typeof(string));
            dt.Columns.Add("PROVEEDOR", typeof(string));
            dt.Columns.Add("CONCEPTO", typeof(string));
            dt.Columns.Add("VALOR", typeof(decimal));
            dt.Columns.Add("R CONT", typeof(string));
            dt.Columns.Add("NETO", typeof(decimal));            
            dt.Columns.Add("1%", typeof(decimal));
            dt.Columns.Add("COMP", typeof(string));
            dt.Columns.Add("RET", typeof(string));
            dt.Columns.Add("EMISION", typeof(string));
            dt.Columns.Add("RENTA", typeof(decimal));
            dt.Columns.Add("TOTAL", typeof(decimal));         

            return dt;
        }
    }
}
