using SistemaContable.DAL;
using System;
using System.Data;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptListadoPagosVencidosProveedor : SistemaContable.RP.ReporteBase
    {
        // TODO: confirmar con el procedimiento almacenado que se está desarrollando
        private const string NombreSP = "SP_QUEDAN_RPT";
        private const string Accion = "PAGOS_VENCIDOS_PROVEEDOR";

        /// <summary>
        /// Fecha de corte: se muestra en el título y se envía al procedimiento.
        /// Usa el parámetro del reporte prmFechaCorte.
        /// </summary>
        public DateTime FechaCorte
        {
            get { return prmFechaCorte.Value is DateTime ? (DateTime)prmFechaCorte.Value : DateTime.Today; }
            set { prmFechaCorte.Value = value.Date; }
        }

        public rptListadoPagosVencidosProveedor()
        {
            InitializeComponent();
            FechaCorte = DateTime.Today;
        }

        public rptListadoPagosVencidosProveedor(DateTime fechaCorte) : this()
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
        // Fuente de datos del reporte (columnas: ListadoPagosVencidosProveedorEsquema)
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
