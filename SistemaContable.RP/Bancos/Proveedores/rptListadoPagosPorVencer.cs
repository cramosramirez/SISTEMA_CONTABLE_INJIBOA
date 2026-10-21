using SistemaContable.DAL;
using System;
using System.Data;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptListadoPagosPorVencer : SistemaContable.RP.ReporteBase
    {
        // TODO: confirmar con el procedimiento almacenado que se está desarrollando
        private const string NombreSP = "SP_QUEDAN_RPT";
        private const string Accion = "PAGOS_POR_VENCER";

        /// <summary>
        /// Fecha de corte: se muestra en el título y se envía al procedimiento.
        /// Usa el parámetro del reporte FechaCorte.
        /// </summary>
        public DateTime FechaCorte
        {
            get { return prmFechaCorte.Value is DateTime ? (DateTime)prmFechaCorte.Value : DateTime.Today; }
            set { prmFechaCorte.Value = value.Date; }
        }

        public rptListadoPagosPorVencer()
        {
            InitializeComponent();
            FechaCorte = DateTime.Today;
        }

        public rptListadoPagosPorVencer(DateTime fechaCorte) : this()
        {
            FechaCorte = fechaCorte;
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
        // Fuente de datos del reporte (columnas: ListadoPagosPorVencerEsquema)
        // ============================================================
        private DataTable ObtenerDatos()
        {
            var dal = new DALBase();
            return dal.EjecutarConsulta(NombreSP, new
            {
                ACCION = Accion,
                FECHA_CORTE = FechaCorte.Date
            });
        }
    }
}
