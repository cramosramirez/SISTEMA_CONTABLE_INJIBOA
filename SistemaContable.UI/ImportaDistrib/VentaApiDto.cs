using Newtonsoft.Json;

namespace SistemaContable.UI.ImportaDistrib
{
    public class VentaApiDto
    {
        public string idempresa { get; set; }
        public string numerodocumento { get; set; }
        public string tipodocumento { get; set; }
        public string descripciondocumento { get; set; }
        public string idlugarfacturacion { get; set; }
        public string idlugarretiro { get; set; }
        public string fechafacturacion { get; set; }
        public string idcliente { get; set; }
        public string nombrecliente { get; set; }
        public string nitcliente { get; set; }
        public string direccioncliente { get; set; }
        public string municipiocliente { get; set; }
        public string departamentocliente { get; set; }
        public string idproducto { get; set; }
        public string descripcionproducto { get; set; }
        public string idcategoriaproducto { get; set; }
        public string idclaseventa { get; set; }
        public string descripcionventa { get; set; }
        public decimal cantidad { get; set; }
        public decimal sacos50 { get; set; }
        public decimal precio { get; set; }
        public decimal subtotal { get; set; }
        public decimal iva { get; set; }
        public decimal retencion { get; set; }
        public decimal totalingresos { get; set; }
        public string ingliq { get; set; }
        public string fecliq { get; set; }
        public string numliq { get; set; }
        public decimal costoliquidacion { get; set; }
        public decimal subtotaliquidacion { get; set; }
        public decimal ivaliquidacion { get; set; }
        public decimal totaliquidacion { get; set; }
        public string numliqemp { get; set; }
        public decimal subtotalemp { get; set; }
        public decimal ivaemp { get; set; }
        public decimal totalemp { get; set; }
        public decimal subtotalflete { get; set; }
        public decimal ivaflete { get; set; }
        public decimal totalflete { get; set; }
        public decimal subtotmaquilado { get; set; }
        public decimal subtotaladminiv { get; set; }
        public decimal subtotpublicidad { get; set; }
        public decimal ivapublicidad { get; set; }
        public decimal totalpublicidad { get; set; }
        public decimal subtotcomision { get; set; }
        public decimal ivacomision { get; set; }
        public decimal totalcomision { get; set; }
        public string controlsistemahora1 { get; set; }
        public string controlsistemahora2 { get; set; }
        public string controlsistemahora3 { get; set; }
        public string unidadmedida { get; set; }
        public string serieclq { get; set; }
        public string seriedoc { get; set; }
        public decimal subtotenvasado { get; set; }
        public decimal ivaenvasado { get; set; }
        public decimal totenvasado { get; set; }
        public decimal subtcargado { get; set; }
        public decimal ivacargado { get; set; }
        public decimal totcargado { get; set; }
        public decimal subtmaqotprod { get; set; }
        public decimal ivamaqotprod { get; set; }
        public decimal totmaqotprod { get; set; }
        public decimal subtcomotprod { get; set; }
        public decimal ivacomotprod { get; set; }
        public decimal totcomotprod { get; set; }
        public string dte { get; set; }
        public decimal subtotal_fleje_carton { get; set; }
        public decimal total_fleje_carton { get; set; }
        public decimal subtotal_entarimado_fleje_carton { get; set; }
        public decimal total_entarimado_fleje_carton { get; set; }
        public decimal subtotal_servicio_cargado_adicional { get; set; }
        public decimal total_servicio_cargado_adicional { get; set; }

        [JsonProperty("Condicion de pago")]
        public string condicion_de_pago { get; set; }
    }
}