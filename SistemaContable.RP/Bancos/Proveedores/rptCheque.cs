using DevExpress.XtraReports.UI;
using SistemaContable.RP.Esquemas;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SistemaContable.RP.Bancos.Proveedores
{
    public partial class rptCheque : SistemaContable.RP.ReporteBase
    {
        public int IdCheque { get; set; } = -1;

        public int IdCatorcena { get; set; } = -1;

        public int IdTipoPlanilla { get; set; } = -1;

        public int EsContribuyente { get; set; } = 0;
        public rptCheque()
        {
            InitializeComponent();           
        }

        public override void CargarDatos()
        {
            if (IdCheque == 0)
                throw new InvalidOperationException(
                    "Debe asignar IdCheque antes de imprimir.");

            if (IdCatorcena == -1 && IdTipoPlanilla == -1)
            {
                CargarUnCheque();
            }
            else
            {
                CargarPlanilla();
            }           
        }

        public void CargarUnCheque()
        {
            DataTable dt = EjecutarSP("SP_CHEQUE_RPT", new
            {
                ACCION = "CHEQUE_PRINCIPAL",
                ID_CHEQUE = IdCheque
            });

            this.DataSource = dt;
            this.DataMember = "";
        }

        public void CargarPlanilla()
        {
            DataTable dt = EjecutarSP("SP_CHEQUE_RPT", new
            {
                ACCION = "CHEQUE_PLANILLA",
                ID_CHEQUE = (int?)null,
                ID_CATORCENA = IdCatorcena,
                ID_TIPO_PLANILLA = IdTipoPlanilla,
                ES_CONTRIBUYENTE = EsContribuyente
            });

            this.DataSource = dt;
            this.DataMember = "";
        }
    }
}
