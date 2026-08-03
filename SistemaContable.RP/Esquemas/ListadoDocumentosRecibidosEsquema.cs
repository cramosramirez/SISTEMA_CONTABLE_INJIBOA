using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.RP.Esquemas
{
    class ListadoDocumentosRecibidosEsquema
    {
        public static DataTable Crear()
        {
            var dt = new DataTable("ListadoDocumentos");
            dt.Columns.Add("NUM_QUEDAN", typeof(int));
            dt.Columns.Add("TIPO_DTE", typeof(string));
            dt.Columns.Add("COD_GENERACION", typeof(string));
            dt.Columns.Add("FECHA_EMISION", typeof(DateTime));
            dt.Columns.Add("FECHA_RECIBIDO", typeof(DateTime));
            dt.Columns.Add("FECHA_VENCE", typeof(DateTime));
            dt.Columns.Add("NOMBRE", typeof(string));
            dt.Columns.Add("ORDEN", typeof(string));
            dt.Columns.Add("AFECTA", typeof(decimal));
            dt.Columns.Add("TOTAL", typeof(decimal));
            dt.Columns.Add("SALDO", typeof(decimal));
            dt.Columns.Add("NUMRET", typeof(string));
            dt.Columns.Add("GRUPO", typeof(int));
            return dt;
        }
    }
}
