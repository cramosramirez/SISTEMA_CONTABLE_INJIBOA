using ClosedXML.Excel;
using SistemaContable.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptListadoComprobantesRecibidos : SistemaContable.RP.ReporteBase
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        
        public rptListadoComprobantesRecibidos()
        {
            InitializeComponent();
            NombreArchivoSugerido =
               "COMPROBANTES_RECIBIDOS_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");            
        }

        // ============================================================
        // Carga de datos para el reporte visual DevExpress
        // Retorna solo el detalle (primera tabla del SP)
        // ============================================================
        public override void CargarDatos()
        {
            if (FechaInicial == FechaFinal)
            {
                xrTITULO.Text = "Listado de Documentos Recibidos el Día " + FechaInicial.ToString("dd/MM/yyyy");
            }
            else
            {
                xrTITULO.Text = "Listado de Documentos Recibidos del Día " + FechaInicial.ToString("dd/MM/yyyy") + " al " + FechaFinal.ToString("dd/MM/yyyy");
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
            return dal.EjecutarConsulta("SP_RPT_LISTADO_COMPROBANTES_RECIBIDOS", new
            {                
                FECHA_INI = FechaInicial,
                FECHA_FIN = FechaFinal                
            });
        }     
        
    }
}
