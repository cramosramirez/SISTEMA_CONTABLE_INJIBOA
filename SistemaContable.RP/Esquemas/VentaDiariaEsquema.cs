using System;
using System.Data;

namespace SistemaContable.RP.Esquemas
{
    public static class VentaDiariaEsquema
    {
        public static DataTable CrearDetalle()
        {
            var dt = new DataTable("VentaDiariaDetalle");

            dt.Columns.Add("FECHAD", typeof(DateTime));
            dt.Columns.Add("FECHAH", typeof(DateTime));
            dt.Columns.Add("CCCOSTO", typeof(string));
            dt.Columns.Add("tipofact", typeof(string));
            dt.Columns.Add("fpago", typeof(string));
            dt.Columns.Add("condpago", typeof(int));
            dt.Columns.Add("fecha", typeof(DateTime));
            dt.Columns.Add("nomcliente", typeof(string));
            dt.Columns.Add("afecta", typeof(decimal));
            dt.Columns.Add("exenta", typeof(decimal));
            dt.Columns.Add("descuento", typeof(decimal));
            dt.Columns.Add("subtotal", typeof(decimal));
            dt.Columns.Add("iva", typeof(decimal));
            dt.Columns.Add("percepcion", typeof(decimal));
            dt.Columns.Add("valortot", typeof(decimal));
            dt.Columns.Add("CODGENERACION", typeof(string));
            dt.Columns.Add("NUMCONTROL", typeof(string));
            dt.Columns.Add("SELLORECEPCION", typeof(string));
            dt.Columns.Add("id_familia", typeof(int));
            dt.Columns.Add("nomfamilia", typeof(string));

            return dt;
        }

        public static DataTable CrearResumen()
        {
            var dt = new DataTable("VentaDiariaResumen");

            dt.Columns.Add("nomproducto", typeof(string));
            dt.Columns.Add("cantidad", typeof(decimal));
            dt.Columns.Add("valor", typeof(decimal));

            return dt;
        }

        public static DataSet CrearDataSet()
        {
            var ds = new DataSet("VentaDiaria");
            ds.Tables.Add(CrearDetalle());
            ds.Tables.Add(CrearResumen());
            return ds;
        }
    }
}
