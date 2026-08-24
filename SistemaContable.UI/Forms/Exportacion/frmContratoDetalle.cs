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
    public partial class frmContratoDetalle : DevExpress.XtraEditors.XtraForm
    {
        private readonly DALBase _dal = new DALBase();
        public int IdContrato { get; set; } = 0;
        public frmContratoDetalle()
        {
            InitializeComponent();
            CargarMercado();
            CargarCliente();
            CargarZafra();
            CargarPodructo();
        }

        private void frmContratoDetalle_Load(object sender, EventArgs e)
        {
            if (IdContrato > 0)
            {
                lblTitulo.Text = "ACTUALIZAR CONTRATO";
            }
            else
            {
                lblTitulo.Text = "NUEVO CONTRATO";
            }
        }

        private void CargarMercado()
        {
            DataTable dt = _dal.EjecutarConsulta("[EEXPORTACION].[SP_EXP_MERCADOS_CONSULTAR]");

            cboMercado.Properties.DataSource = dt;
            cboMercado.Properties.ValueMember = "CODMDO";
            cboMercado.Properties.DisplayMember = "DESCRIPCION";
        }
        private void CargarCliente()
        {
            DataTable dt = _dal.EjecutarConsulta("[EEXPORTACION].CB_CLIENTE");
                 //new
                 //{
                 //    ACCION = "BUSCAR_EXP"
                 //});

            cboCliente.Properties.DataSource = dt;
            cboCliente.Properties.ValueMember = "ID_ENTIDAD";
            cboCliente.Properties.DisplayMember = "NOMBRE";
        }
        private void CargarZafra()
        {
            DataTable dt = _dal.EjecutarConsulta("[EGENERALES].[SP_ZAFRA]",
            new
            {
                ACCION = "OBTENER_CB"
            });

            cboZafra.Properties.DataSource = dt;
            cboZafra.Properties.ValueMember = "ID_ZAFRA";
            cboZafra.Properties.DisplayMember = "NOMBRE_ZAFRA";
        }

        private void CargarPodructo()
        {
            DataTable dt = _dal.EjecutarConsulta("[EINVENTARIO].CB_PRODUCTO_EXP_CONTRATO");

            cboProducto.Properties.DataSource = dt;
            cboProducto.Properties.ValueMember = "ID_PRODUCTO";
            cboProducto.Properties.DisplayMember = "DESCRIPCION";
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboProducto_EditValueChanged(object sender, EventArgs e)
        {
            if (cboProducto.EditValue != null)
            {
                txtPresentacion.Text = cboProducto.GetColumnValue("PRESENTACION")?.ToString();
            }
            else
            {
                txtPresentacion.Text = string.Empty;
            }
        }
    }
}
