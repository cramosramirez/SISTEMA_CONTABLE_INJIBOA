using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.RP.Esquemas
{
    class FacturaSujetoExcluidoEsquema
    {
        public static DataTable Crear()
        {
            var dt = new DataTable("FacturaSujetoExcluido");
            // Identificación / encabezado
            dt.Columns.Add("ID_FSE", typeof(int));
            dt.Columns.Add("COD_GENERACION", typeof(string));
            dt.Columns.Add("NUM_CONTROL", typeof(string));
            dt.Columns.Add("NUM_INTERNO", typeof(string));
            dt.Columns.Add("SELLO_RECIBIDO", typeof(string));
            dt.Columns.Add("FHPROCESAMIENTO", typeof(DateTime));            
            dt.Columns.Add("CONCEPTO", typeof(string));
            dt.Columns.Add("FECHA_EMISION", typeof(DateTime));

            // Pendiente JSON (por ahora en blanco)
            dt.Columns.Add("URL_VALIDACION", typeof(string));
            dt.Columns.Add("VERSION", typeof(string));
            dt.Columns.Add("AMBIENTE", typeof(string));
            dt.Columns.Add("TIPO_TRANSMISION", typeof(string));
            dt.Columns.Add("MODELO_FACT", typeof(string));
            dt.Columns.Add("TIPO_MONEDA", typeof(string));

            

            // Cliente
            dt.Columns.Add("NOMBRE", typeof(string));
            dt.Columns.Add("NIT", typeof(string));
            dt.Columns.Add("DUI", typeof(string));
            dt.Columns.Add("TELEFONO", typeof(string));
            dt.Columns.Add("CORREO", typeof(string));
            dt.Columns.Add("DIRECCION", typeof(string));
            dt.Columns.Add("DEPARTAMENTO", typeof(string));
            dt.Columns.Add("MUNICIPIO", typeof(string));
            dt.Columns.Add("TIPO_CONTRIBUYENTE", typeof(string));
            dt.Columns.Add("ACTIVIDAD", typeof(string));


            // Montos de encabezado
            dt.Columns.Add("MONTO", typeof(decimal));
            dt.Columns.Add("IVA", typeof(decimal));
            dt.Columns.Add("SUBTOTAL", typeof(decimal));
            dt.Columns.Add("RENTA", typeof(decimal));
            dt.Columns.Add("IVAR", typeof(decimal));
            dt.Columns.Add("TOTAL", typeof(decimal));
            dt.Columns.Add("TOTAL_LETRAS", typeof(string));

            return dt;
        }
    }
}
