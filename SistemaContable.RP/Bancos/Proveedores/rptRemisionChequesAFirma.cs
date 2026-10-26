using DevExpress.XtraReports.UI;
using SistemaContable.DAL;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptRemisionChequesAFirma : SistemaContable.RP.ReporteBase
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public rptRemisionChequesAFirma()
        {
            InitializeComponent();
            NombreArchivoSugerido =
               "REMISION_CHEQUES_A_FIRMA_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
        }

        public override void CargarDatos()
        {
            var cultura = new CultureInfo("es-ES"); 
            xrFECHA_MEMO.Text = FechaFinal.ToString("d 'de' MMMM 'de' yyyy", cultura);

            DataTable dt = ObtenerDatos();
            this.DataSource = dt;
            this.DataMember = "";
        }

        private DataTable ObtenerDatos()
        {
            var dal = new DALBase();
            return dal.EjecutarConsulta("SP_RPT_REMISION_CHEQUES_A_FIRMA", new
            {
                FECHA_INI = FechaInicial,
                FECHA_FIN = FechaFinal
            });
        }

    }
}
