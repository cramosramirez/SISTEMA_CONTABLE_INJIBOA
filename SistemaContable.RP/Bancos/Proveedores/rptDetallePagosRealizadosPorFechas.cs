using DevExpress.XtraReports.UI;
using SistemaContable.DAL;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptDetallePagosRealizadosPorFechas : SistemaContable.RP.ReporteBase
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
        public rptDetallePagosRealizadosPorFechas()
        {
            InitializeComponent();
            NombreArchivoSugerido =
               "DETALLE_DE_PAGOS_REALIZADOS_POR_FECHA_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
        }

        // ============================================================
        // Carga de datos para el reporte visual DevExpress
        // ============================================================
        public override void CargarDatos()
        {
            xrTITULO.Text = $"DETALLE DE PAGOS REALIZADOS DESDE EL {FechaInicial:dd/MM/yyyy} AL {FechaFinal:dd/MM/yyyy}";
            DataTable dt = ObtenerDatos();
            this.DataSource = dt;
            this.DataMember = "";
        }


        private DataTable ObtenerDatos()
        {
            var dal = new DALBase();
            return dal.EjecutarConsulta("SP_RPT_DETALLE_PAGOS_REALIZADOS_POR_FECHA", new
            {
                FECHA_INI = FechaInicial,
                FECHA_FIN = FechaFinal,                
                DESDE = Desde,
                HASTA = Hasta                
            });
        }
    }
}
