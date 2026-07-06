using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.RP.Esquemas
{
    public class CompRetencionEsquema
    {
        public static DataTable Crear()
        {
            var dt = new DataTable("CompRetencion");
            dt.Columns.Add("ID_COMPROBANTE_RET", typeof(string));
            dt.Columns.Add("COD_GENERACION", typeof(string));
            dt.Columns.Add("NUM_CONTROL", typeof(string));
            dt.Columns.Add("SELLO_RECIBIDO", typeof(string));
            dt.Columns.Add("NUM_INTERNO", typeof(string));
            dt.Columns.Add("MODELO_FACT", typeof(string));
            dt.Columns.Add("VERSION", typeof(string));
            dt.Columns.Add("AMBIENTE", typeof(string));
            dt.Columns.Add("TIPO_TRANSMISION", typeof(string));
            dt.Columns.Add("FH_EMISION", typeof(string));
            dt.Columns.Add("FHPROCESAMIENTO", typeof(string));
            dt.Columns.Add("NOMBRE", typeof(string));
            dt.Columns.Add("NIT", typeof(string));
            dt.Columns.Add("NRC", typeof(string));
            dt.Columns.Add("TELEFONO", typeof(string));
            dt.Columns.Add("TIPO_MONEDA", typeof(string));
            dt.Columns.Add("TIPO_CONTRIBUYENTE", typeof(string));
            dt.Columns.Add("ACTIVIDAD", typeof(string));
            dt.Columns.Add("CORREO", typeof(string));
            dt.Columns.Add("DIRECCION", typeof(string));
            dt.Columns.Add("MUNICIPIO", typeof(string));
            dt.Columns.Add("DEPARTAMENTO", typeof(string));
            dt.Columns.Add("DETALLE", typeof(string));
            dt.Columns.Add("URL_VALIDACION", typeof(string));
            dt.Columns.Add("RESU_TOTAL_IVA_RET_L", typeof(decimal));
            dt.Columns.Add("RESU_TOTAL_SUJETO_RET", typeof(decimal));
            dt.Columns.Add("RESU_TOTAL_IVA_RET", typeof(decimal));
            dt.Columns.Add("TOTAL", typeof(decimal));
            dt.Columns.Add("ANULADO", typeof(string));

            return dt;
        }
    }
}
