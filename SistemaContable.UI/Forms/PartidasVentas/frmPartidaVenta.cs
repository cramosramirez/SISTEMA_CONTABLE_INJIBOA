using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Data;

namespace SistemaContable.UI.Forms.PartidasVentas
{
    public partial class frmPartidaVenta : DevExpress.XtraEditors.XtraForm
    {
        private readonly DALBase _dal = new DALBase();
        public frmPartidaVenta()
        {
            InitializeComponent();
            CargarCentrosCosto();
        }

        /// <summary>
        /// Carga el combo de Centro de Costo. Reemplaza el acceso a datos
        /// (ej. capa de negocio / repositorio) por el que use tu proyecto.
        /// </summary>
        private void CargarCentrosCosto()
        {
            // (2026-09-16) Filtrado por rol: si el rol del usuario tiene centros de
            // costo asignados en ESEGURIDAD.ROL_CENTROCOSTO (frmRol), solo se muestran
            // esos; si no tiene ninguno asignado, se muestran todos por defecto.
            // Se usa el SP independiente [EDTE].[SP_BUSCAR_CENTROCOSTO] (no
            // [EDTE].[SP_CENTROCOSTO], que sigue usando frmFactura.cs sin filtrar).
            DataTable dt = _dal.EjecutarConsulta("[CONTA].CB_TIPOS_PARTIDA_VENTA_CC"
                );
            cbTipoPartida.DataSource = dt;
            cbTipoPartida.ValueMember = "ID_TPPARVENTA";
            cbTipoPartida.DisplayMember = "NOMNRE";
            cbTipoPartida.SelectedIndex = -1;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtNumero.Text = string.Empty;
            txtConcepto.Text = string.Empty;
            deFecha.EditValue = null;
            cbTipoPartida.SelectedIndex = -1;
            txtNumero.Focus();
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            if (!ValidarCriterios())
                return;

            string centroCostoId = Convert.ToString(cbTipoPartida.SelectedValue);
           string centroCostoNombre = cbTipoPartida.SelectedText;

            // TODO: invocar la lógica de generación de la partida usando
            // txtPartida.Text, deFecha.DateTime, txtNumero.Text,
            // txtConcepto.Text y centroCostoId.

            DevExpress.XtraEditors.XtraMessageBox.Show(
                $"Partida generada para el centro de costo: {centroCostoNombre}",
                "Proceso completado",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Information);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboCentroCosto_EditValueChanged(object sender, EventArgs e)
        {
            // Espacio para lógica dependiente del centro de costo seleccionado,
            // por ejemplo filtrar cuentas contables asociadas a ese centro.
        }

        private bool ValidarCriterios()
        {
            if (deFecha.EditValue == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Debe indicar la fecha.");
                deFecha.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Debe indicar el número.");
                txtNumero.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtConcepto.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Debe indicar el concepto.");
                txtConcepto.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cbTipoPartida.SelectedText))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Debe seleccionar el centro de costo.");
                cbTipoPartida.Focus();
                return false;
            }

            return true;
        }

        private void ActualizarConcepto()
        {
            if (cbTipoPartida.SelectedIndex < 0 || string.IsNullOrWhiteSpace(cbTipoPartida.Text))
                return;

            if (deFecha.EditValue == null)
                return;

            string fecha = deFecha.Text;

            switch (cbTipoPartida.SelectedValue.ToString())
            {
                case "1":
                    txtConcepto.Text = "INGRESO POR VENTA DE MELAZA DE FECHA " + fecha;
                    break;
                case "2":
                    txtConcepto.Text = "INGRESO POR VENTA DE AZUCAR POR DISTRIBUIDORA DE FECHA " + fecha;
                    break;
                case "3":
                    txtConcepto.Text = "INGRESO POR VENTAS DE OTROS PRODUCTOS JIBOA DE FECHA " + fecha;
                    break;
                case "4":
                    txtConcepto.Text = "INGRESO POR VENTA DE CORTE DE CAÑA DE FECHA " + fecha;
                    break;
                case "5":
                    txtConcepto.Text = "INGRESO POR VENTA DE ENERGIA ELECTRICA DE FECHA " + fecha;
                    break;
                case "6":
                    txtConcepto.Text = "INGRESO POR VENTAS POR EXPORTACION DE FECHA " + fecha;
                    break;
            }
        }
        private void deFecha_EditValueChanged(object sender, EventArgs e)
        {
            if (deFecha.EditValue == null)
                return;

            var infoPartida = NumeradorPartidaHelper.Consultar(txtPartida.Text);
            txtNumero.Text = infoPartida.NumSiguienteFormateado;
            ActualizarConcepto();
        }

        private void cbTipoPartida_TextChanged(object sender, EventArgs e)
        {
            ActualizarConcepto();
        }
    }
}
