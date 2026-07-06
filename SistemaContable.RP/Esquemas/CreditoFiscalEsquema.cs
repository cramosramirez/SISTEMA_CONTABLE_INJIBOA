using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SistemaContable.RP.Esquemas
{
    class CreditoFiscalEsquema
    {
        public static DataTable Crear()
        {
            var dt = new DataTable("CreditoFiscal");
            // Identificación / encabezado
            dt.Columns.Add("id_enc", typeof(int));
            dt.Columns.Add("CODGENERACION", typeof(string));
            dt.Columns.Add("NUMCONTROL", typeof(string));
            dt.Columns.Add("NUMINTERNO", typeof(string));
            dt.Columns.Add("SELLORECEPCION", typeof(string));
            dt.Columns.Add("FHPROCESAMIENTO", typeof(string));
            dt.Columns.Add("FECHA_CREA", typeof(DateTime));
            dt.Columns.Add("OBSERVACION", typeof(string));
            dt.Columns.Add("FMPAGO", typeof(string));

            // Pendiente JSON (por ahora en blanco)
            dt.Columns.Add("LINKQR", typeof(string));
            dt.Columns.Add("json", typeof(string));
            dt.Columns.Add("ambient", typeof(string));
            dt.Columns.Add("tptransmision", typeof(string));
            dt.Columns.Add("modfact", typeof(string));
            dt.Columns.Add("USD", typeof(string));

            // Pendiente (función/tabla no confirmada)
            dt.Columns.Add("emaildte", typeof(string));
            dt.Columns.Add("dtresul", typeof(string));

            // Cliente
            dt.Columns.Add("nombrecli", typeof(string));
            dt.Columns.Add("nit", typeof(string));
            dt.Columns.Add("registro", typeof(string));
            dt.Columns.Add("telecli", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("direcli", typeof(string));
            dt.Columns.Add("dpte", typeof(string));
            dt.Columns.Add("munic", typeof(string));
            dt.Columns.Add("tpcontry", typeof(string));
            dt.Columns.Add("codactivida", typeof(string));

            // Montos de encabezado
            dt.Columns.Add("afecta", typeof(decimal));
            dt.Columns.Add("descuento", typeof(decimal));
            dt.Columns.Add("iva", typeof(decimal));
            dt.Columns.Add("subtt", typeof(decimal));
            dt.Columns.Add("retencion", typeof(decimal));
            dt.Columns.Add("percepcion", typeof(decimal));
            dt.Columns.Add("valortot", typeof(decimal));
            dt.Columns.Add("ttletras", typeof(string));
            dt.Columns.Add("vfovial", typeof(decimal));
            dt.Columns.Add("vimpuesto2", typeof(decimal));

            // Detalle (línea)
            dt.Columns.Add("cantidad", typeof(decimal));
            dt.Columns.Add("descrip", typeof(string));
            dt.Columns.Add("precio", typeof(decimal));
            dt.Columns.Add("exenta", typeof(decimal));
            dt.Columns.Add("valor", typeof(decimal));
            dt.Columns.Add("pordesc1", typeof(int));

            return dt;
        }
    }
}
