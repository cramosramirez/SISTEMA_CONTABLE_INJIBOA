using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.RP.Esquemas
{
    public static class ChequeEsquema
    {
        public static DataTable Crear()
        {
            var dt = new DataTable("Cheque");
            dt.Columns.Add("ID_CHEQUE", typeof(int));
            dt.Columns.Add("FECHA_LETRAS", typeof(string));            
            dt.Columns.Add("MONTO", typeof(string));
            dt.Columns.Add("NOMBRE_CHEQUE", typeof(string));
            dt.Columns.Add("FECHA_CHEQUE", typeof(DateTime));
            dt.Columns.Add("A_FAVOR_DE", typeof(string));
            dt.Columns.Add("PARTIDA", typeof(string));
            dt.Columns.Add("MONTO_EN_LETRAS", typeof(string));
            dt.Columns.Add("CTACONTABLE", typeof(string));
            dt.Columns.Add("DETALLE", typeof(string));
            dt.Columns.Add("CARGO", typeof(decimal));
            dt.Columns.Add("ABONO", typeof(decimal));
            dt.Columns.Add("NUM_CHEQUE", typeof(string));
            dt.Columns.Add("CONCEPTO", typeof(string));
            dt.Columns.Add("ORDEN_CORRELATIVO", typeof(int));           

            return dt;
        }
    }
}
