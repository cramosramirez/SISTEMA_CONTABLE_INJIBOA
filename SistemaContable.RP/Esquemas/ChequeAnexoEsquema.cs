using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.RP.Esquemas
{
    public static class ChequeAnexoEsquema
    {
        public static DataTable Crear()
        {
            var dt = new DataTable("ChequeAnexo");
            dt.Columns.Add("ID_CCF_COMPRA", typeof(int));
            dt.Columns.Add("CODIGO_ENTIDAD", typeof(string));
            dt.Columns.Add("NOMBRE_ENTIDAD", typeof(string));
            dt.Columns.Add("NUM_CHEQUE", typeof(int));
            dt.Columns.Add("NUM_CUENTA", typeof(string));
            dt.Columns.Add("FECHA_CHEQUE", typeof(DateTime));
            dt.Columns.Add("TIPO_DTE", typeof(string));
            dt.Columns.Add("COD_GENERACION", typeof(string));
            dt.Columns.Add("FECHA_RECIBIDO", typeof(DateTime));
            dt.Columns.Add("TOTAL", typeof(decimal));
            dt.Columns.Add("ABONOS", typeof(decimal));
            dt.Columns.Add("SALDO", typeof(decimal));
            dt.Columns.Add("PAGO", typeof(decimal));

            dt.Rows.Add(
            1,
            "240681-4",            
            "RAMOS RAMIREZ CHRISTIAM EDUARDO",
            95623362,
            "0965100135**70",
            "2026-07-24",
            "DTE3",
            "AC16449D-8605-4FC5-BD5E-F4526C8D15F3",
            "2026-06-24",
            1792.00,
            0.00,
            1632.00,
            1632.00
            );

            dt.Rows.Add(
            2,
            "240681-4",
            "RAMOS RAMIREZ CHRISTIAM EDUARDO",
            95623362,
            "0965100135**70",
            "2026-07-24",
            "DTE3",
            "B26D1162-33C4-4E53-9AEF-0AB9FB573317",
            "2026-07-13",
            1792.00,
            0.00,
            1632.00,
            1632.00
            );

            return dt;
        }
    }
}
