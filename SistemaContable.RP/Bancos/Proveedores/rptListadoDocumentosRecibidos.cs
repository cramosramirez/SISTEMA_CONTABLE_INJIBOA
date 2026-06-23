using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptListadoDocumentosRecibidos : SistemaContable.RP.ReporteBase
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public rptListadoDocumentosRecibidos()
        {
            InitializeComponent();
        }
        public override void CargarDatos()
        {           
            DataTable dt = EjecutarSP("SP_QUEDAN_RPT", new
            {
                ACCION = "LISTADO_COMPROBANTES_RECIBIDOS",
                FECHA_INI = FechaInicial,
                FECHA_FIN = FechaFinal,
            });

            if (FechaInicial == FechaFinal)
                xrTITULO.Text = $"Listado de Documentos Recibido el Día {FechaInicial.ToString("dd/MM/yyyy")}";
            else
                xrTITULO.Text = $"Listado de Documentos Recibido del {FechaInicial.ToString("dd/MM/yyyy")} al {FechaFinal.ToString("dd/MM/yyyy")}";

            this.DataSource = dt;
            this.DataMember = "";
        }
    }
}
