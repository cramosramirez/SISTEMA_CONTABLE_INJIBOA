using DevExpress.XtraReports.UI;
using SistemaContable.DAL;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptListadoSaldoPorProveedor : SistemaContable.RP.ReporteBase
    {
        public DateTime Fecha { get; set; }                
        public string Desde { get; set; }
        public string Hasta { get; set; }
        public rptListadoSaldoPorProveedor()
        {
            InitializeComponent();
            NombreArchivoSugerido =
               "SALDOS_POR_PROVEEDOR_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
        }

        public override void CargarDatos()
        {
            xrTITULO.Text = "Listado de Saldos por Proveedor al " + Fecha.ToString("dd/MM/yyyy");
            DataTable dt = ObtenerDatos();
            this.DataSource = dt;
            this.DataMember = "";
        }

        private DataTable ObtenerDatos()
        {
            var dal = new DALBase();
            return dal.EjecutarConsulta("SP_RPT_SALDOS_POR_PROVEEDOR", new
            {
                FECHA = Fecha,                
                DESDE = Desde,
                HASTA = Hasta                
            });
        }
    }

   
}
