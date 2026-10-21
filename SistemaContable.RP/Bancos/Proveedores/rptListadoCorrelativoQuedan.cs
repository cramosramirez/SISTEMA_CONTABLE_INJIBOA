using SistemaContable.DAL;
using System;
using System.Data;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptListadoCorrelativoQuedan : SistemaContable.RP.ReporteBase
    {
        // TODO: confirmar con el procedimiento almacenado que se está desarrollando
        private const string NombreSP = "SP_QUEDAN_RPT";
        private const string Accion = "LISTADO_CORRELATIVO_QUEDAN";

        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }

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
            return dal.EjecutarConsulta(NombreSP, new
            {
                ACCION = Accion,
                FECHA_INI = FechaInicial.Date,
                FECHA_FIN = FechaFinal.Date
            });
        }
    }
}
