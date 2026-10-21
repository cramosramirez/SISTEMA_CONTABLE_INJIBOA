using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.RP.Esquemas
{
    class ListadoPagosVencidosEsquema
    {
        public static DataTable Crear()
        {
            var dt = new DataTable("ListadoPagosVencidos");
            dt.Columns.Add("ID_CATEGORIA", typeof(int));
            dt.Columns.Add("CATEGORIA", typeof(string));
            dt.Columns.Add("TIPO", typeof(string));
            dt.Columns.Add("COMPROBANTE", typeof(string));
            dt.Columns.Add("QUEDAN", typeof(string));
            dt.Columns.Add("VENCE", typeof(DateTime));
            dt.Columns.Add("SALDO", typeof(decimal));
            dt.Columns.Add("SIN_VENCER", typeof(decimal));
            dt.Columns.Add("D1_30", typeof(decimal));
            dt.Columns.Add("D31_60", typeof(decimal));
            dt.Columns.Add("D61_90", typeof(decimal));
            dt.Columns.Add("D91_MAS", typeof(decimal));
            dt.Columns.Add("DIAS", typeof(int));
            return dt;
        }
    }
}
