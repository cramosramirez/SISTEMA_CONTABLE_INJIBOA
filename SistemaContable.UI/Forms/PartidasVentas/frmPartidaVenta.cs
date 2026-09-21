using Dapper;
using SistemaContable.DAL;
using SistemaContable.RP.Partidas;
using SistemaContable.UI.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.PartidasVentas
{
    public partial class frmPartidaVenta : DevExpress.XtraEditors.XtraForm
    {
        private readonly DALBase _dal = new DALBase();
        public frmPartidaVenta()
        {
            InitializeComponent();
            CargarCentrosCosto();
            btnImprimir.Enabled = false;
            btnProcesar.Enabled = true;
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
            cbTipoPartida.DisplayMember = "NOMBRE";
            cbTipoPartida.SelectedIndex = -1;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtNumero.Text = string.Empty;
            txtConcepto.Text = string.Empty;
            deFecha.EditValue = null;
            btnImprimir.Enabled = false;
            btnProcesar.Enabled = true;
            cbTipoPartida.SelectedIndex = -1;
            txtNumero.Focus();
        }
        private bool Procesar_Partidad(string procedimiento, string centroCosto)
        {
            try
            {
                var parametros = new DynamicParameters();

                parametros.Add("@ID_TPPARVENTA", Convert.ToInt32(cbTipoPartida.SelectedValue));
                parametros.Add("@TIPO_PARTIDA", txtPartida.Text);
                parametros.Add("@FECHA_PARTIDA", Convert.ToDateTime(deFecha.Text));
                parametros.Add("@NUM_PARTIDA", Convert.ToInt32(txtNumero.Text));
                parametros.Add("@CONCEPTO", txtConcepto.Text);
                parametros.Add("@USUARIO", Configuracion.UsuarioActual);
                parametros.Add("@ID_ESTADO", Convert.ToInt32(1));

                parametros.Add("@Resultado",
                    dbType: DbType.Int32,
                    direction: ParameterDirection.Output);

                parametros.Add("@Mensaje",
                    dbType: DbType.String,
                    size: 500,
                    direction: ParameterDirection.Output);

                parametros.Add("@NID_",
                   dbType: DbType.String,
                   size: 50,
                   direction: ParameterDirection.Output);

                _dal.EjecutarConSalida(procedimiento, parametros);

                int? resultado = parametros.Get<int?>("@Resultado");
                string mensaje = parametros.Get<string>("@Mensaje");
                txtNID_PARTIDA.Text = parametros.Get<string>("@NID_");
                if (resultado == 1)
                {
                    Alertas.Exito(mensaje+ centroCosto);
                    btnImprimir.Enabled = true;
                    btnProcesar.Enabled = false;
                    return true;
                }

                Alertas.Advertencia(mensaje);
                btnImprimir.Enabled = false;
                btnProcesar.Enabled = true;
                return false;
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;

                if (mensaje.Contains("]:"))
                    mensaje = mensaje.Substring(mensaje.IndexOf("]:") + 2).Trim();

                Alertas.Error(mensaje);
                return false;
            }
        }
        private void btnProcesar_Click(object sender, EventArgs e)
        {
            if (!ValidarCriterios())
                return;

            string centroCostoId = Convert.ToString(cbTipoPartida.SelectedValue);
           string centroCostoNombre = cbTipoPartida.Text;

            switch (cbTipoPartida.SelectedValue.ToString())
            {
                case "1":
                    Procesar_Partidad("[CONTA].CRE_PARTIDA_DIARIA_VENTA_MELAZA", centroCostoNombre);
                    break;
                case "2":
                    Procesar_Partidad("[CONTA].CRE_PARTIDA_DIARIA_VENTA_DISTRIBUIDORA", centroCostoNombre);
                    break;
                case "3":
                    Procesar_Partidad("[CONTA].CRE_PARTIDA_DIARIA_VENTA_OTROSPRODUCTOS", centroCostoNombre);
                    break;
                case "4":
                    Procesar_Partidad("[CONTA].CRE_PARTIDA_DIARIA_VENTA_CORTECANIA", centroCostoNombre);
                    break;
                case "5":
                    Procesar_Partidad("[CONTA].CRE_PARTIDA_DIARIA_VENTA_ELECTRICIDA", centroCostoNombre);
                    break;
                case "6":
                    Procesar_Partidad("[CONTA].CRE_PARTIDA_DIARIA_VENTA_EXPORTACION", centroCostoNombre);
                    break;
            }

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

            if (string.IsNullOrWhiteSpace(cbTipoPartida.Text))
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNID_PARTIDA.Text) || txtNID_PARTIDA.Text.Trim() == "-1")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "No se ha procesado la partida.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                var reporte = new RptPartida_Movimiento
                {
                    _NID_PARTIDA = txtNID_PARTIDA.Text.Trim(),
                    _Titulo = "DETALLE DE PARTIDA CONTABLE"
                };

                reporte.MostrarPreview();
            }
            catch (Exception ex)
            {
                Alertas.Error(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

    }
}
