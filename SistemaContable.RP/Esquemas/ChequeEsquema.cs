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
            dt.Columns.Add("MONTO_EN_LETRAS", typeof(string));
            dt.Columns.Add("CTACONTABLE", typeof(string));
            dt.Columns.Add("DETALLE", typeof(string));
            dt.Columns.Add("CARGO", typeof(decimal));
            dt.Columns.Add("ABONO", typeof(decimal));
            dt.Columns.Add("NUM_CHEQUE", typeof(int));
            dt.Columns.Add("CONCEPTO", typeof(string));


            dt.Rows.Add(
            1,
            "SAN VICENTE, 20 DE JUNIO DE 2026",
            "****864.60",
            "*RAMOS RAMIREZ CHRISTIAM EDUARDO*",
            "***OCHOCIENTOS SESENTA Y CUATRO 60/100***",
            "1101.03.005",
            "CH # 9248825 RAMOS RAMIREZ CHRISTIAM EDUARDO",
            0.00,
            1600.00,
            94586,
            "PAGO DE HONORARIOS DE CHRISTIAM EDUARDO RAMOS RAMIREZ POR SERVICIO DE ASESORIA A SISTEMAS INFORMATICOS JUNIO 2026"
            );


            dt.Rows.Add(
            1,
            "SAN VICENTE, 20 DE JUNIO DE 2026",
            "****864.60",
            "*RAMOS RAMIREZ CHRISTIAM EDUARDO*",
            "***OCHOCIENTOS SESENTA Y CUATRO 60/100***",
            "2101.07.061",
            "CH # 9248825 DTE#4 RAMOS RAMIREZ CHRISTIAM EDUARDO",
            1600.00,
            0.00,
            94586,
            "PAGO DE HONORARIOS DE CHRISTIAM EDUARDO RAMOS RAMIREZ POR SERVICIO DE ASESORIA A SISTEMAS INFORMATICOS JUNIO 2026"
            );

            dt.Rows.Add(
            2,
            "SAN VICENTE, 20 DE JUNIO DE 2026",
            "****864.60",
            "*RAMOS RAMIREZ CHRISTIAM EDUARDO*",
            "***OCHOCIENTOS SESENTA Y CUATRO 60/100***",
            "2103.01.002",
            "CH # 9248825 PAGO DTE#1 RAMOS RAMIREZ CHRISTIAM EDUARDO",
            0.00,
            36.85,
             94587,
            "PAGO DE HONORARIOS DE CHRISTIAM EDUARDO RAMOS RAMIREZ POR SERVICIO DE ASESORIA A SISTEMAS INFORMATICOS JULIO 2026"
            );

            return dt;
        }
    }
}
