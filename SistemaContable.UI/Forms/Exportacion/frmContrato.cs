using SistemaContable.DAL;
using System;
using DevExpress.XtraGrid.Views.Base;
using System.Windows.Forms;
using System.Drawing;

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
        public void CargarContratos()
        {
            var dt = _dal.EjecutarConsulta("[EEXPORTACION].[SP_EXP_CONTRATOS_CONSULTAR]");
            gridControl1.DataSource = dt;
        }

        private void AbrirDocumento(int IdContrato)
        {
            using (var frm = new frmContratoDetalle(this))
            {
                frm.IdContrato = IdContrato;
                frm.ShowDialog(this);
            }
           
        }
        private void AbrirFechaEmbarque()
        {
            var contrato = ObtenerFilaActiva();

            if (contrato == null)
                return;

            using (var frm = new frmFechaEmbarque())
            {
                frm.IdContrato = contrato._IdContrato;
                frm.Client = contrato._Cliente;
                frm.NumContrato = contrato._NumeroContrato;
                frm.TonelContrato = contrato._Toneladas.ToString("N2");

                frm.ShowDialog(this);
            }
        }
        private void AbrirAddendum()
        {
            var contrato = ObtenerFilaActiva();

            if (contrato == null)
                return;

            using (var frm = new frmAddendum())
            {
                frm.IdContratoAddendum = contrato._IdContrato;
                frm.ClientAddendum = contrato._Cliente;
                frm.NumContratoAddendum = contrato._NumeroContrato;
                frm.TonelContratoAddendum = contrato._Toneladas.ToString("N2");

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
        private int? ObtenerIdFilaActiva()
        {
            if (gridView1.FocusedRowHandle < 0) return null;
            object val = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "IDCONTEXP");
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }
        private ContratoSeleccionado ObtenerFilaActiva()
        {
            if (gridView1.FocusedRowHandle < 0)
                return null;

            return new ContratoSeleccionado
            {
                _IdContrato = Convert.ToInt32(
                    gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "IDCONTEXP")),

                _Cliente = Convert.ToString(
                    gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NOM_CLIENTE")),

                _NumeroContrato = Convert.ToString(
                    gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NUMERO_CONTRATO")),

                _Toneladas = Convert.ToDecimal(
                    gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TONELADAS"))
            };
        }
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == colEditar)
            {
                int? id = ObtenerIdFilaActiva();
                if (id.HasValue) AbrirDocumento(id.Value);
            }
            if (e.Column == colFechaEmbarque)
            {
                AbrirFechaEmbarque();
            }
            if (e.Column == colAddendum)
            {
                AbrirAddendum();
            }


            //if (e.Column == colVER_Q)
            //{
            //    int? id = ObtenerIdFilaActiva();
            //    try
            //    {
            //        Cursor = Cursors.WaitCursor;
            //        var reporte = new RptNotaRemision { _id = (int)id };
            //        reporte.MostrarPreview();
            //    }
            //    catch (Exception ex)
            //    {
            //        XtraMessageBox.Show("Error al imprimir:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    finally
            //    {
            //        Cursor = Cursors.Default;
            //    }
            //}
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column == colEstado)
            {
                string estado = Convert.ToString(
                    gridView1.GetRowCellValue(e.RowHandle, colEstado)
                );

                switch (estado?.ToUpper())
                {
                    case "ACTIVO":
                        e.Appearance.BackColor = Color.LightGreen;
                        e.Appearance.ForeColor = Color.DarkGreen;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        
                        break;

                  

                    case "EN PROCESO":
                        e.Appearance.BackColor = Color.Khaki;
                        e.Appearance.ForeColor = Color.DarkOrange;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        break;

                    case "FINALIZADO":
                        e.Appearance.BackColor = Color.LightSkyBlue;
                        e.Appearance.ForeColor = Color.Navy;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        break;

                    case "INACTIVO":
                        e.Appearance.BackColor = Color.LightCoral;
                        e.Appearance.ForeColor = Color.DarkRed;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        break;
                }
            }
        }
        private class ContratoSeleccionado
        {
            public int _IdContrato { get; set; }
            public string _Cliente { get; set; }
            public string _NumeroContrato { get; set; }
            public decimal _Toneladas { get; set; }
        }
    }

}
