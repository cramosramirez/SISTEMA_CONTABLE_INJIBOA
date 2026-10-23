using SistemaContable.DAL;
using System;
using System.Data;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptListadoPagosPorVencer : SistemaContable.RP.ReporteBase
    {
        public DateTime Fecha { get; set; }
        public bool IncluirVencidos { get; set; }
        public bool EsResumen { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
        public rptListadoPagosPorVencer()
        {
            InitializeComponent();
            NombreArchivoSugerido =
               "LISTADO_PAGOS_POR_VENCER_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
        }        

        // ============================================================
        // Carga de datos para el reporte visual DevExpress
        // ============================================================
        public override void CargarDatos()
        {
            xrFECHA.Text = Fecha.ToString("dd/MM/yyyy") ;            
            hQuedan.Visible = !EsResumen;
            hVence.Visible = !EsResumen;
            hTipo.Visible = !EsResumen;
            hComprobante.Text = !EsResumen ? "COMPROBANTE" : "PROVEEDOR";

            DataTable dt = ObtenerDatos();
            this.DataSource = dt;
            this.DataMember = "";
        }

        // ============================================================
        // Fuente de datos del reporte (columnas: ListadoPagosPorVencerEsquema)
        // ============================================================
        private DataTable ObtenerDatos()
        {
            var dal = new DALBase();
            return dal.EjecutarConsulta("SP_RPT_PAGOS_POR_VENCER", new
            {                
                FECHA = Fecha,
                INCLUIR_YA_VENCIDOS = IncluirVencidos,
                ES_RESUMEN = EsResumen,
                DESDE = Desde,
                HASTA = Hasta
            });
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = EsResumen;
        }
    }
}
