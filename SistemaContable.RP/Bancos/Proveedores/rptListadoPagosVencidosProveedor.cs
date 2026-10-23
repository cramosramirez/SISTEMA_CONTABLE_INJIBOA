using SistemaContable.DAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptListadoPagosVencidosProveedor : SistemaContable.RP.ReporteBase
    {
        public int Del_dia { get; set; }
        public int Al_dia { get; set; }
        public bool EsResumen { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }

        public rptListadoPagosVencidosProveedor()
        {
            InitializeComponent();
            NombreArchivoSugerido =
               "COMPROBANTES_VENCIDOS_A_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
        }
       
        // ============================================================
        // Carga de datos para el reporte visual DevExpress
        // ============================================================
        public override void CargarDatos()
        {
            xrFECHA.Text = DateTime.Today.ToString("dd/MM/yyyy");
            hD1_30.Text = Del_dia.ToString() + " <=> " + Al_dia.ToString();
            DataTable dt = ObtenerDatos();
            this.DataSource = dt;
            this.DataMember = "";

            RenombrarColumnasExcel = new Dictionary<string, string>
                {
                    { "PERIODO", $"{Del_dia} <=> {Al_dia}" }
                };
        }

        // ============================================================
        // Fuente de datos del reporte (columnas: ListadoPagosVencidosProveedorEsquema)
        // ============================================================
        private DataTable ObtenerDatos()
        {
            var dal = new DALBase();
            return dal.EjecutarConsulta("SP_RPT_COMPROBANTES_VENCIDOS_A", new
            {
                DEL_DIA = Del_dia,
                AL_DIA = Al_dia,
                ES_RESUMEN = EsResumen,
                ORDEN = "V",
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
