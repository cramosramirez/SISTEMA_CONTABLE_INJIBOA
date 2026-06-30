using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.RP.Esquemas
{
    class QuedanEsquema
    {
        public static DataTable Crear()
        {
            var dt = new DataTable("Quedan");
            dt.Columns.Add("ID_QUEDAN", typeof(int));
            dt.Columns.Add("NOM_PROVEEDOR", typeof(string));
            dt.Columns.Add("COMPROB", typeof(string));
            dt.Columns.Add("NUMERO", typeof(string));
            dt.Columns.Add("ORDEN_COMPRA", typeof(string));
            dt.Columns.Add("VALOR", typeof(decimal));
            dt.Columns.Add("SOLICITADO_POR", typeof(string));
            dt.Columns.Add("FECHA", typeof(DateTime));
            dt.Columns.Add("FECHA_LETRAS", typeof(string));
            dt.Columns.Add("ENTREGADO_POR", typeof(string));
            dt.Columns.Add("QUEDAN", typeof(int));
            dt.Columns.Add("NUMRETE", typeof(string)); 
            dt.Columns.Add("NUMCONTROL", typeof(string));
            dt.Columns.Add("SELLORECEPCION", typeof(string));
            dt.Columns.Add("ID_CCF_COMPRA", typeof(int));
            return dt;
        }
    }
}
