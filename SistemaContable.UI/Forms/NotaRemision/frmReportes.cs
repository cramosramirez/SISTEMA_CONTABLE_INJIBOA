using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.NotaRemision
{
    public partial class frmReportes : Form
    {
       
        public frmReportes()
        {
            InitializeComponent();
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {
            lstReportes.SelectedIndex = 0;
        }

        private void lstReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string reporte = lstReportes.SelectedItem?.ToString();

            switch (reporte)
            {
                case "Detalle  xls":

                    lblDestino.Visible = true;
                    cboDestino.Visible = true;

                    lblProducto.Visible = true;
                    cboProducto.Visible = true;

                    break;

                case "Detalle de  Dizucar xls":

                    lblDestino.Visible = false;
                    cboDestino.Visible = false;

                    lblProducto.Visible = false;
                    cboProducto.Visible = false;

                    break;
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
    }
}