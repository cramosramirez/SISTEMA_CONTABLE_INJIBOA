using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.RP.Esquemas
{
    class ListadoPagosPendFechaRecepcionEsquema
    {
        public static DataTable Crear()
        {
            var dt = new DataTable("ListadoPagosPendFechaRecepcion");
            dt.Columns.Add("CODIGO", typeof(string));
            dt.Columns.Add("QUEDAN", typeof(string));
            dt.Columns.Add("PROVEEDOR", typeof(string));
            dt.Columns.Add("TIPO", typeof(string));
            dt.Columns.Add("COMPROBANTE", typeof(string));
            dt.Columns.Add("RECIBIDO", typeof(DateTime));
            dt.Columns.Add("VENCE", typeof(DateTime));
            dt.Columns.Add("ORIG", typeof(decimal));
            dt.Columns.Add("ABONO", typeof(decimal));
            dt.Columns.Add("SALDO", typeof(decimal));
            dt.Columns.Add("DIAS_VENCIDO", typeof(string));
            dt.Columns.Add("SIN_VENCIMIENTO", typeof(decimal));
            return dt;
        }
    }
}
