using SistemaContable.DAL;
using System;
using System.Data;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptListadoPagosPendFechaRecepcion : SistemaContable.RP.ReporteBase
    {
        // TODO: confirmar con el procedimiento almacenado que se está desarrollando
        private const string NombreSP = "SP_QUEDAN_RPT";
        private const string Accion = "PAGOS_PEND_FECHA_RECEPCION";

        /// <summary>
        /// Fechas del rango: se muestran en el título y se envían al procedimiento.
        /// Usan los parámetros del reporte prmFechaInicial y prmFechaFinal.
        /// </summary>
        public DateTime FechaInicial
        {
            get { return prmFechaInicial.Value is DateTime ? (DateTime)prmFechaInicial.Value : DateTime.Today; }
            set { prmFechaInicial.Value = value.Date; }
        }

        public DateTime FechaFinal
        {
            get { return prmFechaFinal.Value is DateTime ? (DateTime)prmFechaFinal.Value : DateTime.Today; }
            set { prmFechaFinal.Value = value.Date; }
        }

        public rptListadoPagosPendFechaRecepcion()
        {
            InitializeComponent();
            FechaInicial = DateTime.Today;
            FechaFinal = DateTime.Today;
        }

        public rptListadoPagosPendFechaRecepcion(DateTime fechaInicial, DateTime fechaFinal) : this()
        {
            FechaInicial = fechaInicial;
            FechaFinal = fechaFinal;
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
        // Fuente de datos del reporte (columnas: ListadoPagosPendFechaRecepcionEsquema)
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
