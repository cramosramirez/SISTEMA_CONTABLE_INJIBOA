using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Http;
using Newtonsoft.Json;
using SistemaContable.DAL;

namespace SistemaContable.UI.ImportaDistrib
{
    public class ImportadorDizucar : DALBase
    {
        private static string UrlApi =>
            ConfigurationManager.AppSettings["UrlConsultaDistrib"];

        // ============================================================
        // 1) Token del día (base de datos ERPMH, servidor distinto)
        // ============================================================
        public string ObtenerTokenDelDia()
        {
            using (var cn = new SqlConnection(CadenaConexionDistrib))
            {
                cn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT TOP 1 TOKENDZ FROM [ERPMH].[EDIZUCAR].[TOKEN] WHERE ACTIVO = 1", cn))
                {
                    object resultado = cmd.ExecuteScalar();
                    if (resultado == null || resultado == DBNull.Value)
                        throw new Exception("No se encontró un token activo en ERPMH.EDIZUCAR.TOKEN.");

                    return resultado.ToString();
                }
            }
        }

        // ============================================================
        // 2) Llamada al API (tipo = "D" ventas, "A" gastos)
        // ============================================================
        public string LlamarApiDizucar(string tipo, DateTime fecha, string token)
        {
            using (var cliente = new HttpClient())
            {
                cliente.DefaultRequestHeaders.Add("code", token);
                cliente.DefaultRequestHeaders.Add("tipo", tipo);
                cliente.DefaultRequestHeaders.Add("fecha", fecha.ToString("dd/MM/yyyy"));

                var respuesta = cliente.PostAsync(UrlApi, null).Result;
                respuesta.EnsureSuccessStatusCode();

                return respuesta.Content.ReadAsStringAsync().Result;
            }
        }

        // ============================================================
        // 3) Ventas -> staging dbo.ventas
        // ============================================================
        public int ImportarVentas(string json, DateTime fecha)
        {
            var lista = JsonConvert.DeserializeObject<List<VentaApiDto>>(json) ?? new List<VentaApiDto>();

            DataTable dt = new DataTable();
            dt.Columns.Add("idempresa", typeof(string));
            dt.Columns.Add("numerodocumento", typeof(string));
            dt.Columns.Add("tipodocumento", typeof(int));
            dt.Columns.Add("descripciondocumento", typeof(string));
            dt.Columns.Add("idlugarfacturacion", typeof(int));
            dt.Columns.Add("idlugarretiro", typeof(string));
            dt.Columns.Add("fechafacturacion", typeof(DateTime));
            dt.Columns.Add("idcliente", typeof(string));
            dt.Columns.Add("nombrecliente", typeof(string));
            dt.Columns.Add("nitcliente", typeof(string));
            dt.Columns.Add("direccioncliente", typeof(string));
            dt.Columns.Add("municipiocliente", typeof(string));
            dt.Columns.Add("departamentocliente", typeof(string));
            dt.Columns.Add("idproducto", typeof(int));
            dt.Columns.Add("descripcionproducto", typeof(string));
            dt.Columns.Add("idcategoriaproducto", typeof(int));
            dt.Columns.Add("idclaseventa", typeof(string));
            dt.Columns.Add("descripcionventa", typeof(string));
            dt.Columns.Add("cantidad", typeof(decimal));
            dt.Columns.Add("sacos50", typeof(decimal));
            dt.Columns.Add("precio", typeof(decimal));
            dt.Columns.Add("subtotal", typeof(decimal));
            dt.Columns.Add("iva", typeof(decimal));
            dt.Columns.Add("retencion", typeof(decimal));
            dt.Columns.Add("totalingresos", typeof(decimal));
            dt.Columns.Add("ingliq", typeof(decimal));
            dt.Columns.Add("fecliq", typeof(DateTime));
            dt.Columns.Add("numliq", typeof(string));
            dt.Columns.Add("costoliquidacion", typeof(decimal));
            dt.Columns.Add("subtotaliquidacion", typeof(decimal));
            dt.Columns.Add("ivaliquidacion", typeof(decimal));
            dt.Columns.Add("totaliquidacion", typeof(decimal));
            dt.Columns.Add("numliqemp", typeof(string));
            dt.Columns.Add("subtotalemp", typeof(decimal));
            dt.Columns.Add("ivaemp", typeof(decimal));
            dt.Columns.Add("totalemp", typeof(decimal));
            dt.Columns.Add("subtotalflete", typeof(decimal));
            dt.Columns.Add("ivaflete", typeof(decimal));
            dt.Columns.Add("totalflete", typeof(decimal));
            dt.Columns.Add("subtotmaquilado", typeof(decimal));
            dt.Columns.Add("subtotaladminiv", typeof(decimal));
            dt.Columns.Add("subtotpublicidad", typeof(decimal));
            dt.Columns.Add("ivapublicidad", typeof(decimal));
            dt.Columns.Add("totalpublicidad", typeof(decimal));
            dt.Columns.Add("subtotcomision", typeof(decimal));
            dt.Columns.Add("ivacomision", typeof(decimal));
            dt.Columns.Add("totalcomision", typeof(decimal));
            dt.Columns.Add("controlsistemahora1", typeof(int));
            dt.Columns.Add("controlsistemahora2", typeof(int));
            dt.Columns.Add("controlsistemahora3", typeof(int));
            dt.Columns.Add("unidadmedida", typeof(string));
            dt.Columns.Add("serieclq", typeof(string));
            dt.Columns.Add("seriedoc", typeof(string));
            dt.Columns.Add("subtotenvasado", typeof(decimal));
            dt.Columns.Add("ivaenvasado", typeof(decimal));
            dt.Columns.Add("totenvasado", typeof(decimal));
            dt.Columns.Add("subtcargado", typeof(decimal));
            dt.Columns.Add("ivacargado", typeof(decimal));
            dt.Columns.Add("totcargado", typeof(decimal));
            dt.Columns.Add("subtmaqotprod", typeof(decimal));
            dt.Columns.Add("ivamaqotprod", typeof(decimal));
            dt.Columns.Add("totmaqotprod", typeof(decimal));
            dt.Columns.Add("subtcomotprod", typeof(decimal));
            dt.Columns.Add("ivacomotprod", typeof(decimal));
            dt.Columns.Add("totcomotprod", typeof(decimal));
            dt.Columns.Add("dte", typeof(string));
            dt.Columns.Add("subtotal_fleje_carton", typeof(decimal));
            dt.Columns.Add("total_fleje_carton", typeof(decimal));
            dt.Columns.Add("subtotal_entarimado_fleje_carton", typeof(decimal));
            dt.Columns.Add("total_entarimado_fleje_carton", typeof(decimal));
            dt.Columns.Add("subtotal_servicio_cargado_adicional", typeof(decimal));
            dt.Columns.Add("total_servicio_cargado_adicional", typeof(decimal));
            dt.Columns.Add("condicion_de_pago", typeof(string));

            foreach (var v in lista)
            {
                var fila = dt.NewRow();
                fila["idempresa"] = ANull(v.idempresa);
                fila["numerodocumento"] = ANull(v.numerodocumento);
                fila["tipodocumento"] = AInt(v.tipodocumento);
                fila["descripciondocumento"] = ANull(v.descripciondocumento);
                fila["idlugarfacturacion"] = AInt(v.idlugarfacturacion);
                fila["idlugarretiro"] = ANull(v.idlugarretiro);
                fila["fechafacturacion"] = AFechaCorta(v.fechafacturacion);
                fila["idcliente"] = ANull(v.idcliente);
                fila["nombrecliente"] = ANull(v.nombrecliente);
                fila["nitcliente"] = ANull(v.nitcliente);
                fila["direccioncliente"] = ANull(v.direccioncliente);
                fila["municipiocliente"] = ANull(v.municipiocliente);
                fila["departamentocliente"] = ANull(v.departamentocliente);
                fila["idproducto"] = AInt(v.idproducto);
                fila["descripcionproducto"] = ANull(v.descripcionproducto);
                fila["idcategoriaproducto"] = AInt(v.idcategoriaproducto);
                fila["idclaseventa"] = ANull(v.idclaseventa);
                fila["descripcionventa"] = ANull(v.descripcionventa);
                fila["cantidad"] = v.cantidad;
                fila["sacos50"] = v.sacos50;
                fila["precio"] = v.precio;
                fila["subtotal"] = v.subtotal;
                fila["iva"] = v.iva;
                fila["retencion"] = v.retencion;
                fila["totalingresos"] = v.totalingresos;
                fila["ingliq"] = ADecimal(v.ingliq);
                fila["fecliq"] = AFechaIso(v.fecliq);
                fila["numliq"] = ANull(v.numliq);
                fila["costoliquidacion"] = v.costoliquidacion;
                fila["subtotaliquidacion"] = v.subtotaliquidacion;
                fila["ivaliquidacion"] = v.ivaliquidacion;
                fila["totaliquidacion"] = v.totaliquidacion;
                fila["numliqemp"] = ANull(v.numliqemp);
                fila["subtotalemp"] = v.subtotalemp;
                fila["ivaemp"] = v.ivaemp;
                fila["totalemp"] = v.totalemp;
                fila["subtotalflete"] = v.subtotalflete;
                fila["ivaflete"] = v.ivaflete;
                fila["totalflete"] = v.totalflete;
                fila["subtotmaquilado"] = v.subtotmaquilado;
                fila["subtotaladminiv"] = v.subtotaladminiv;
                fila["subtotpublicidad"] = v.subtotpublicidad;
                fila["ivapublicidad"] = v.ivapublicidad;
                fila["totalpublicidad"] = v.totalpublicidad;
                fila["subtotcomision"] = v.subtotcomision;
                fila["ivacomision"] = v.ivacomision;
                fila["totalcomision"] = v.totalcomision;
                fila["controlsistemahora1"] = AInt(v.controlsistemahora1);
                fila["controlsistemahora2"] = AInt(v.controlsistemahora2);
                fila["controlsistemahora3"] = AInt(v.controlsistemahora3);
                fila["unidadmedida"] = ANull(v.unidadmedida);
                fila["serieclq"] = ANull(v.serieclq);
                fila["seriedoc"] = ANull(v.seriedoc);
                fila["subtotenvasado"] = v.subtotenvasado;
                fila["ivaenvasado"] = v.ivaenvasado;
                fila["totenvasado"] = v.totenvasado;
                fila["subtcargado"] = v.subtcargado;
                fila["ivacargado"] = v.ivacargado;
                fila["totcargado"] = v.totcargado;
                fila["subtmaqotprod"] = v.subtmaqotprod;
                fila["ivamaqotprod"] = v.ivamaqotprod;
                fila["totmaqotprod"] = v.totmaqotprod;
                fila["subtcomotprod"] = v.subtcomotprod;
                fila["ivacomotprod"] = v.ivacomotprod;
                fila["totcomotprod"] = v.totcomotprod;
                fila["dte"] = ANull(v.dte);
                fila["subtotal_fleje_carton"] = v.subtotal_fleje_carton;
                fila["total_fleje_carton"] = v.total_fleje_carton;
                fila["subtotal_entarimado_fleje_carton"] = v.subtotal_entarimado_fleje_carton;
                fila["total_entarimado_fleje_carton"] = v.total_entarimado_fleje_carton;
                fila["subtotal_servicio_cargado_adicional"] = v.subtotal_servicio_cargado_adicional;
                fila["total_servicio_cargado_adicional"] = v.total_servicio_cargado_adicional;
                fila["condicion_de_pago"] = ANull(v.condicion_de_pago);
                dt.Rows.Add(fila);
            }

            using (var cn = new SqlConnection(CadenaConexion))
            {
                cn.Open();               

                using (var bulk = new SqlBulkCopy(cn) { DestinationTableName = "dbo.ventas" })
                {
                    foreach (DataColumn col in dt.Columns)
                        bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);

                    bulk.WriteToServer(dt);
                }
            }

            return dt.Rows.Count;
        }

        // ============================================================
        // 4) Gastos -> staging dbo.gastos
        // ============================================================
        public int ImportarGastos(string json, DateTime fecha)
        {
            var lista = JsonConvert.DeserializeObject<List<GastoApiDto>>(json) ?? new List<GastoApiDto>();

            DataTable dt = new DataTable();
            dt.Columns.Add("idempresa", typeof(string));
            dt.Columns.Add("documento", typeof(string));
            dt.Columns.Add("idtipodocumento", typeof(string));
            dt.Columns.Add("idlugarfacturacion", typeof(int));
            dt.Columns.Add("idlugarretiro", typeof(string));
            dt.Columns.Add("fechafacturacion", typeof(DateTime));
            dt.Columns.Add("idcliente", typeof(string));
            dt.Columns.Add("nombrecliente", typeof(string));
            dt.Columns.Add("idproducto", typeof(int));
            dt.Columns.Add("descripcionproducto", typeof(string));
            dt.Columns.Add("idcategoriaproducto", typeof(int));
            dt.Columns.Add("idclaseventa", typeof(string));
            dt.Columns.Add("cantidad", typeof(decimal));
            dt.Columns.Add("sacos50", typeof(decimal));
            dt.Columns.Add("precio", typeof(decimal));
            dt.Columns.Add("subtotal", typeof(decimal));
            dt.Columns.Add("iva", typeof(decimal));
            dt.Columns.Add("retencion", typeof(decimal));
            dt.Columns.Add("totalingresos", typeof(decimal));
            dt.Columns.Add("ingliq", typeof(decimal));
            dt.Columns.Add("fechaliquidacion", typeof(DateTime));
            dt.Columns.Add("numliquidacion", typeof(int));
            dt.Columns.Add("subtotaliquidacion", typeof(decimal));
            dt.Columns.Add("ivaliquidacion", typeof(decimal));
            dt.Columns.Add("totaliquidacion", typeof(decimal));
            dt.Columns.Add("numliqemp", typeof(string));
            dt.Columns.Add("subtotalemp", typeof(decimal));
            dt.Columns.Add("ivaemp", typeof(decimal));
            dt.Columns.Add("totalemp", typeof(decimal));
            dt.Columns.Add("subtotalflete", typeof(decimal));
            dt.Columns.Add("ivaflete", typeof(decimal));
            dt.Columns.Add("totalflete", typeof(decimal));
            dt.Columns.Add("subtotmaquilado", typeof(decimal));
            dt.Columns.Add("subtotadmininv", typeof(decimal));
            dt.Columns.Add("codgeneracion", typeof(string));
            dt.Columns.Add("dte", typeof(string));

            foreach (var g in lista)
            {
                var fila = dt.NewRow();
                fila["idempresa"] = ANull(g.idempresa);
                fila["documento"] = ANull(g.documento);
                fila["idtipodocumento"] = ANull(g.idtipodocumento);
                fila["idlugarfacturacion"] = AInt(g.idlugarfacturacion);
                fila["idlugarretiro"] = ANull(g.idlugarretiro);
                fila["fechafacturacion"] = AFechaCorta(g.fechafacturacion);
                fila["idcliente"] = ANull(g.idcliente);
                fila["nombrecliente"] = ANull(g.nombrecliente);
                fila["idproducto"] = AInt(g.idproducto);
                fila["descripcionproducto"] = ANull(g.descripcionproducto);
                fila["idcategoriaproducto"] = AInt(g.idcategoriaproducto);
                fila["idclaseventa"] = ANull(g.idclaseventa);
                fila["cantidad"] = g.cantidad;
                fila["sacos50"] = g.sacos50;
                fila["precio"] = g.precio;
                fila["subtotal"] = g.subtotal;
                fila["iva"] = g.iva;
                fila["retencion"] = g.retencion;
                fila["totalingresos"] = g.totalingresos;
                fila["ingliq"] = ADecimal(g.ingliq);
                fila["fechaliquidacion"] = AFechaCorta(g.fechaliquidacion);
                fila["numliquidacion"] = AInt(g.numliquidacion);
                fila["subtotaliquidacion"] = g.subtotaliquidacion;
                fila["ivaliquidacion"] = g.ivaliquidacion;
                fila["totaliquidacion"] = g.totaliquidacion;
                fila["numliqemp"] = ANull(g.numliqemp);
                fila["subtotalemp"] = g.subtotalemp;
                fila["ivaemp"] = g.ivaemp;
                fila["totalemp"] = g.totalemp;
                fila["subtotalflete"] = g.subtotalflete;
                fila["ivaflete"] = g.ivaflete;
                fila["totalflete"] = g.totalflete;
                fila["subtotmaquilado"] = g.subtotmaquilado;
                fila["subtotadmininv"] = g.subtotadmininv;
                fila["codgeneracion"] = ANull(g.codgeneracion);
                fila["dte"] = ANull(g.dte);
                dt.Rows.Add(fila);
            }

            using (var cn = new SqlConnection(CadenaConexion))
            {
                cn.Open();
                               
                using (var bulk = new SqlBulkCopy(cn) { DestinationTableName = "dbo.gastos" })
                {
                    foreach (DataColumn col in dt.Columns)
                        bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);

                    bulk.WriteToServer(dt);
                }
            }

            return dt.Rows.Count;
        }
              

        // ============================================================
        // 6) Orquestador completo
        // ============================================================
        public (int ventas, int gastos) ImportarTodo(DateTime fecha)
        {
            string token = ObtenerTokenDelDia();

            EjecutarConsulta("DISTRIB.SP_DIZUCARLIQ_PROCESO_LIQUIDACION",
                new { ACCION = "LIMPIAR_STAGING", FECHA_PROCESA = fecha.Date });

            string jsonVentas = LlamarApiDizucar("D", fecha, token);
            int cantVentas = ImportarVentas(jsonVentas, fecha);

            string jsonGastos = LlamarApiDizucar("A", fecha, token);
            int cantGastos = ImportarGastos(jsonGastos, fecha);

            EjecutarConsulta("DISTRIB.SP_DIZUCARLIQ_PROCESO_LIQUIDACION",
                new { ACCION = "TRANSFORMAR", FECHA_PROCESA = fecha.Date });

            return (cantVentas, cantGastos);
        }

        // ============================================================
        // Helpers de conversión segura (texto -> tipo real)
        // ============================================================
        private static object ANull(string valor) => string.IsNullOrWhiteSpace(valor) ? (object)DBNull.Value : valor;

        private static object AInt(string valor)
            => int.TryParse(valor, out int v) ? (object)v : DBNull.Value;

        private static object ADecimal(string valor)
            => decimal.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal v) ? (object)v : DBNull.Value;

        // "29-08-2026" -> DateTime
        private static object AFechaCorta(string valor)
            => DateTime.TryParseExact(valor, "dd-MM-yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime f) ? (object)f : DBNull.Value;

        // "2026-08-29T00:00:00" -> DateTime
        private static object AFechaIso(string valor)
            => DateTime.TryParse(valor, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime f) ? (object)f : DBNull.Value;
    }
}