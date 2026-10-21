using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.RP.Esquemas
{
    class ListadoCorrelativoQuedanEsquema
    {
        public static DataTable Crear()
        {
            var dt = new DataTable("ListadoCorrelativoQuedan");
            dt.Columns.Add("QUEDAN", typeof(string));
            dt.Columns.Add("NOMBRE", typeof(string));
            dt.Columns.Add("TIPO_DOC", typeof(string));
            dt.Columns.Add("NUM_DOC", typeof(string));
            dt.Columns.Add("FECHA_RECIBIDO", typeof(DateTime));
            dt.Columns.Add("FECHA_VENCE", typeof(DateTime));
            dt.Columns.Add("MONTO_ORIGINAL", typeof(decimal));
            dt.Columns.Add("ABONO", typeof(decimal));
            dt.Columns.Add("SALDO", typeof(decimal));
            dt.Columns.Add("DIAS_VENCIDO", typeof(int));
            dt.Columns.Add("SIN_VENCER", typeof(decimal));
            return dt;
        }
    }
}
