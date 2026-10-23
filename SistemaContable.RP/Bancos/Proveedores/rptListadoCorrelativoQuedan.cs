using SistemaContable.DAL;
using System;
using System.Data;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptListadoCorrelativoQuedan : SistemaContable.RP.ReporteBase
    {       
        public string Orden { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
        public bool IncluirYaPagados { get; set; }

        public rptListadoCorrelativoQuedan()
        {
            InitializeComponent();
        }

        // ============================================================
        // Carga de datos para el reporte visual DevExpress
        // ============================================================
        public override void CargarDatos()
        {
            DataTable dt = ObtenerDatos();
            this.DataSource = dt;
            this.DataMember = "";
        }

        // ============================================================
        // Fuente de datos del reporte (columnas: ListadoCorrelativoQuedanEsquema)
        // ============================================================
        private DataTable ObtenerDatos()
        {
            var dal = new DALBase();
            return dal.EjecutarConsulta("SP_RPT_LISTADO_CORRELATIVO_QUEDAN", new
            {
                ORDEN = Orden,
                INCLUIR_YA_PAGADOS = IncluirYaPagados,
                DESDE = Desde,
                HASTA = Hasta
            });
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            gPROVEEDOR.Visible = (Orden == "PROVEEDOR");
            gQUEDAN.Visible = (Orden == "QUEDAN");
        }
    }
}
