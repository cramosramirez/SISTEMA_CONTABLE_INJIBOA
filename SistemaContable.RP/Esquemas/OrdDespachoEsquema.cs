using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.RP.Esquemas
{
    class OrdDespachoEsquema
    {
        public static DataTable Crear()
        {
            var dt = new DataTable("Documento");

            // Encabezado
            dt.Columns.Add("ID_ODENC", typeof(int));
            dt.Columns.Add("COD_REF", typeof(string));
            dt.Columns.Add("FECHA", typeof(DateTime));
            dt.Columns.Add("CODGENERACION_OD", typeof(string));
            dt.Columns.Add("NOMCLIENTE", typeof(string));
            dt.Columns.Add("NOMPROVEEDOR", typeof(string));
            dt.Columns.Add("NOMTRANSPORT", typeof(string));
            dt.Columns.Add("PLACA", typeof(string));
            dt.Columns.Add("REMOLQUE", typeof(string));
            dt.Columns.Add("NOMMOTORISTA", typeof(string));
            dt.Columns.Add("LICENCIA", typeof(string));
            dt.Columns.Add("MARCHAMOS", typeof(string));
            dt.Columns.Add("NOMZAFRA", typeof(string));
            dt.Columns.Add("COD_REF", typeof(string));
            dt.Columns.Add("DESCRIPCION", typeof(string));
            dt.Columns.Add("CANTIDAD", typeof(string));
            dt.Columns.Add("ANULADO", typeof(string));
           
            return dt;
        }
    }
}
