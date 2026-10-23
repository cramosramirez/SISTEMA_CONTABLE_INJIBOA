using SistemaContable.DAL;
using System;
using System.Data;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptListadoPagosPendFechaRecepcion : SistemaContable.RP.ReporteBase
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public string TipoFiltro { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
        public bool IncluirYaPagados { get; set; }
        public rptListadoPagosPendFechaRecepcion()
        {
            InitializeComponent();
            NombreArchivoSugerido =
               "PAGOS_VENCIDOS_POR_FECHA_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
        }

      
        // ============================================================
        // Carga de datos para el reporte visual DevExpress
        // ============================================================
        public override void CargarDatos()
        {
            xrTITULO.Text = "Pagos Pendientes por Fecha de " + TipoFiltro == "R" ? "Recepción" : TipoFiltro == "F" ? "Facturación" : "Vencimiento" 
                + " Del " + FechaInicial.ToString("dd/MM/yyyy") + " Al " + FechaFinal.ToString("dd/MM/yyyy");            
            DataTable dt = ObtenerDatos();
            this.DataSource = dt;
            this.DataMember = "";
        }

        // ============================================================
        // Fuente de datos del reporte (columnas: ListadoPagosPendFechaRecepcionEsquema)
        // ============================================================
        private DataTable ObtenerDatos()
        {
            var dal = new DALBase();
            return dal.EjecutarConsulta("SP_RPT_PAGOS_VENCIDOS_POR_FECHA", new
            {                
                FECHA_INI = FechaInicial,
                FECHA_FIN = FechaFinal,
                TIPO_FILTRO = TipoFiltro,
                DESDE = Desde,
                HASTA = Hasta,
                INCLUIR_YA_PAGADOS = IncluirYaPagados
            });
        }
    }
}
