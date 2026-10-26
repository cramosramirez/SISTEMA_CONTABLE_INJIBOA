using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.RP.Esquemas
{
    class RemisionChequesAFirmaEsquema
    {
        public static DataTable Crear()
        {
            var dt = new DataTable("Remision");
                        
            dt.Columns.Add("No", typeof(int));
            dt.Columns.Add("FECHA", typeof(DateTime));
            dt.Columns.Add("CHEQUE", typeof(string));
            dt.Columns.Add("PROVEEDOR", typeof(string));
            dt.Columns.Add("VALOR", typeof(decimal));
           
            return dt;
        }
    }
}
