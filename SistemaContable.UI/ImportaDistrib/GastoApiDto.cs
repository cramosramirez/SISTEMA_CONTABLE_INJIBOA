namespace SistemaContable.UI.ImportaDistrib
{
    public class GastoApiDto
    {
        public string idempresa { get; set; }
        public string documento { get; set; }
        public string idtipodocumento { get; set; }
        public string idlugarfacturacion { get; set; }
        public string idlugarretiro { get; set; }
        public string fechafacturacion { get; set; }
        public string idcliente { get; set; }
        public string nombrecliente { get; set; }
        public string idproducto { get; set; }
        public string descripcionproducto { get; set; }
        public string idcategoriaproducto { get; set; }
        public string idclaseventa { get; set; }
        public decimal cantidad { get; set; }
        public decimal sacos50 { get; set; }
        public decimal precio { get; set; }
        public decimal subtotal { get; set; }
        public decimal iva { get; set; }
        public decimal retencion { get; set; }
        public decimal totalingresos { get; set; }
        public string ingliq { get; set; }
        public string fechaliquidacion { get; set; }
        public string numliquidacion { get; set; }
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
        public decimal subtotadmininv { get; set; }
        public string codgeneracion { get; set; }
        public string dte { get; set; }
    }
}