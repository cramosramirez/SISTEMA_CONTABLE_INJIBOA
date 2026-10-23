using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.RP.Esquemas
{
    class ListadoPagosPorVencerEsquema
    {
        public static DataTable Crear()
        {
            var dt = new DataTable("ListadoPagosPorVencer");
            dt.Columns.Add("PROVEEDOR", typeof(string));
            dt.Columns.Add("TIPO", typeof(string));
            dt.Columns.Add("COMPROBANTE", typeof(string));
            dt.Columns.Add("QUEDAN", typeof(int));
            dt.Columns.Add("VENCE", typeof(DateTime));
            dt.Columns.Add("SALDO", typeof(decimal));
            dt.Columns.Add("VENCIDOS", typeof(decimal));
            dt.Columns.Add("1 A 30 DIAS", typeof(decimal));
            dt.Columns.Add("31 A 60 DIAS", typeof(decimal));
            dt.Columns.Add("61 A 90 DIAS", typeof(decimal));
            dt.Columns.Add("91 Y MAS", typeof(decimal));
            dt.Columns.Add("DIAS", typeof(int));
            return dt;
        }
    }
}
