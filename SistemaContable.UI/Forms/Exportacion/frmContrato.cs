using SistemaContable.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Exportacion
{
    public partial class frmContrato : DevExpress.XtraEditors.XtraForm
    {
        private readonly DALBase _dal = new DALBase();
        public frmContrato()
        {
            InitializeComponent();
            CargarContratos();
        }
        private void CargarContratos()
        {
            var dt = _dal.EjecutarConsulta("[EEXPORTACION].[SP_EXP_CONTRATOS_CONSULTAR]");
            gridControl1.DataSource = dt;
        }

        private void AbrirDocumento(int IdContrato)
        {
            using (var frm = new frmContratoDetalle())
            {
                frm.IdContrato = IdContrato;
                frm.ShowDialog(this);
            }
           
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            AbrirDocumento(IdContrato: 0);
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
