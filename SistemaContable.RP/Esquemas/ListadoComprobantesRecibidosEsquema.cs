using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.RP.Esquemas
{
    class ListadoComprobantesRecibidosEsquema
    {

        public static DataTable Crear()
        {
            var dt = new DataTable("ListadoComprobantes");
            dt.Columns.Add("QUEDAN", typeof(int));
            dt.Columns.Add("TIPO", typeof(string));
            dt.Columns.Add("COMPROBANTE", typeof(string));
            dt.Columns.Add("NUMRET", typeof(string));
            dt.Columns.Add("FECHA", typeof(DateTime));
            dt.Columns.Add("RECIBIDO", typeof(DateTime));
            dt.Columns.Add("VENCE", typeof(DateTime));
            dt.Columns.Add("NOMBRE_PROVEEDOR", typeof(string));
            dt.Columns.Add("ORDEN", typeof(string));
            dt.Columns.Add("AFECTA", typeof(decimal));
            dt.Columns.Add("TOTAL", typeof(decimal));
            dt.Columns.Add("SALDO", typeof(decimal));                        
            return dt;
        }
    }
}
