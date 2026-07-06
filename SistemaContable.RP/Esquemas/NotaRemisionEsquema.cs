using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.RP.Esquemas
{
    public static class NotaRemisionEsquema
    {
        public static DataTable Crear()
        {
            var dt = new DataTable("Documento");

            // Encabezado
            dt.Columns.Add("ID_NTREMISIONENC", typeof(int));
            dt.Columns.Add("SELLORECEPCION", typeof(string));
            dt.Columns.Add("FECHA", typeof(DateTime));
            dt.Columns.Add("NUMINTERNO", typeof(string));
            dt.Columns.Add("ANULADO", typeof(string));
            dt.Columns.Add("OBSERVACIONES", typeof(string));
            dt.Columns.Add("LINKQR", typeof(string));


            dt.Columns.Add("version", typeof(int));
            dt.Columns.Add("ambiente", typeof(string));
            dt.Columns.Add("tipoDte", typeof(string));
            dt.Columns.Add("codigoGeneracion", typeof(string));
            dt.Columns.Add("NumeroControl", typeof(string));
            dt.Columns.Add("fecEmi", typeof(DateTime));
            dt.Columns.Add("horEmi", typeof(string));
            dt.Columns.Add("tipoMoneda", typeof(string));
            dt.Columns.Add("tipoModelo", typeof(string));
            dt.Columns.Add("tipoOperacion", typeof(string));

            // Emisor
            dt.Columns.Add("emisorNit", typeof(string));
            dt.Columns.Add("emisorNrc", typeof(string));
            dt.Columns.Add("emisorNombre", typeof(string));
            dt.Columns.Add("emisorNombreComercial", typeof(string));
            dt.Columns.Add("emisorTelefono", typeof(string));
            dt.Columns.Add("emisorCorreo", typeof(string));
            dt.Columns.Add("emisorDireccion", typeof(string));
            dt.Columns.Add("emisorDepart", typeof(string));
            dt.Columns.Add("emisorDistri", typeof(string));

            // Receptor
            dt.Columns.Add("receptorTipoDocumento", typeof(string));
            dt.Columns.Add("receptorNumDocumento", typeof(string));
            dt.Columns.Add("receptorNrc", typeof(string));
            dt.Columns.Add("receptorNombre", typeof(string));
            dt.Columns.Add("receptorNombreComercial", typeof(string));
            dt.Columns.Add("receptordescActividad", typeof(string));
            dt.Columns.Add("receptorTelefono", typeof(string));
            dt.Columns.Add("receptorCorreo", typeof(string));
            dt.Columns.Add("receptorDireccion", typeof(string));
            dt.Columns.Add("receptorDepart", typeof(string));
            dt.Columns.Add("receptorDistri", typeof(string));
            dt.Columns.Add("receptorCategoria", typeof(string));

            // Resumen
            dt.Columns.Add("totalNoSuj", typeof(decimal));
            dt.Columns.Add("totalExenta", typeof(decimal));
            dt.Columns.Add("totalGravada", typeof(decimal));
            dt.Columns.Add("subTotalVentas", typeof(decimal));
            dt.Columns.Add("montoTotalOperacion", typeof(decimal));
            dt.Columns.Add("totalLetras", typeof(string));
            dt.Columns.Add("ResumenObservaciones", typeof(string));

            // Detalle
            dt.Columns.Add("numItem", typeof(int));
            dt.Columns.Add("tipoItem", typeof(int));
            dt.Columns.Add("codigo", typeof(int));
            dt.Columns.Add("codTributo", typeof(string));
            dt.Columns.Add("descripcion", typeof(string));
            dt.Columns.Add("cantidad", typeof(decimal));
            dt.Columns.Add("uniMedida", typeof(int));
            dt.Columns.Add("NomUnidaMedida", typeof(string));
            dt.Columns.Add("precioUni", typeof(decimal));
            dt.Columns.Add("montoDescu", typeof(decimal));
            dt.Columns.Add("ventaNoSuj", typeof(decimal));
            dt.Columns.Add("ventaExenta", typeof(decimal));
            dt.Columns.Add("ventaGravada", typeof(decimal));


            // apendice
            dt.Columns.Add("Transporte", typeof(string));
            dt.Columns.Add("Motorista", typeof(string));
            dt.Columns.Add("Licencia", typeof(string));
            dt.Columns.Add("Placa", typeof(string));


            return dt;
        }
    }
}
