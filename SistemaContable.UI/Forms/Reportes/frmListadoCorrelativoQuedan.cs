using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Reportes
{
    public partial class frmListadoCorrelativoQuedan : Form
    {
        public frmListadoCorrelativoQuedan()
        {
            InitializeComponent();
        }

        private void frmListadoCorrelativoQuedan_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HabilitarRango()
        {
            if (rbTodos.Checked)
            {
                txtDESDE.ReadOnly = true;
                txtHASTA.ReadOnly = true;
                txtDESDE_DESCRIPCION.Text = "";
                txtHASTA_DESCRIPCION.Text = ""; 
            }
            else
            {
                txtDESDE.ReadOnly = false;
                txtHASTA.ReadOnly = false;
            }
        }

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            HabilitarRango();
        }

        private void rbRango_CheckedChanged(object sender, EventArgs e)
        {
            HabilitarRango();
        }

        
    }
}
