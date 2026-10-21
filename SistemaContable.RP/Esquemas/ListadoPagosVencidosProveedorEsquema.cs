using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.RP.Esquemas
{
    class ListadoPagosVencidosProveedorEsquema
    {
        public static DataTable Crear()
        {
            var dt = new DataTable("ListadoPagosVencidosProveedor");
            dt.Columns.Add("CODIGO_ENTIDAD", typeof(string));
            dt.Columns.Add("NOMBRE_PROVEEDOR", typeof(string));
            dt.Columns.Add("TIPO", typeof(string));
            dt.Columns.Add("COMPROBANTE", typeof(string));
            dt.Columns.Add("QUEDAN", typeof(string));
            dt.Columns.Add("VENCE", typeof(DateTime));
            dt.Columns.Add("SALDO", typeof(decimal));
            dt.Columns.Add("SIN_VENCER", typeof(decimal));
            dt.Columns.Add("D1_30", typeof(decimal));
            dt.Columns.Add("DIAS", typeof(int));
            return dt;
        }
    }
}
