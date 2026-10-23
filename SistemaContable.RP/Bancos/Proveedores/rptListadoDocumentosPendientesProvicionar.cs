using DevExpress.XtraReports.UI;
using SistemaContable.DAL;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptListadoDocumentosPendientesProvicionar :  SistemaContable.RP.ReporteBase
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public rptListadoDocumentosPendientesProvicionar()
        {
            InitializeComponent();
            NombreArchivoSugerido =
               "COMPROBANTES_PENDIENTES_PROVISIONAR_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");            
        }

        // ============================================================
        // Carga de datos para el reporte visual DevExpress
        // Retorna solo el detalle (primera tabla del SP)
        // ============================================================
        public override void CargarDatos()
        {
            if (FechaInicial == FechaFinal)
            {
                xrTITULO.Text = "Listado de Documentos Pedientes de Provisionar al Día " + FechaInicial.ToString("dd/MM/yyyy");
            }
            else
            {
                xrTITULO.Text = "Listado de Documentos Pedientes de Provisionar del Día " + FechaInicial.ToString("dd/MM/yyyy") + " al " + FechaFinal.ToString("dd/MM/yyyy");
            }

            DataTable dt = ObtenerDatos();
            this.DataSource = dt;
            this.DataMember = "";
        }

        // ============================================================
        // Fuente de datos del reporte visual (solo detalle)
        // ============================================================
        private DataTable ObtenerDatos()
        {
            var dal = new DALBase();
            return dal.EjecutarConsulta("SP_RPT_COMPROBANTES_PENDIENTES_PROVISIONAR", new
            {
                FECHA_INI = FechaInicial,
                FECHA_FIN = FechaFinal
            });
        }


    }
}
