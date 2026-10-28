using System;
using System.Data;

namespace SistemaContable.RP.Esquemas
{
    public static class ChequesPlanillaEsquema
    {
        public static DataTable Crear()
        {
            var dt = new DataTable("ChequesPlanilla");
            dt.Columns.Add("CODIGO", typeof(int));
            dt.Columns.Add("NOMBRE", typeof(string));
            dt.Columns.Add("TONELADAS", typeof(decimal));
            dt.Columns.Add("VALOR", typeof(decimal));
            dt.Columns.Add("NETO", typeof(decimal));
            dt.Columns.Add("CHEQUE", typeof(string));
            dt.Columns.Add("FECHA_INICIO", typeof(DateTime));
            dt.Columns.Add("FECHA_FIN", typeof(DateTime));
            dt.Columns.Add("CATORCENA", typeof(int));
            return dt;
        }
    }
}
