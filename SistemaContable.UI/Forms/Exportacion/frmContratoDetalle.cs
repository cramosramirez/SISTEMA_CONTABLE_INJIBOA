using Dapper;
using SistemaContable.DAL;
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
using System.IO;

namespace SistemaContable.UI.Forms.Exportacion
{
    public partial class frmContratoDetalle : DevExpress.XtraEditors.XtraForm
    {
        private readonly DALBase _dal = new DALBase();
        public int IdContrato { get; set; } = 0;
        public int? IdCont = 0;
        private readonly frmContrato _frmContrato;

        public frmContratoDetalle(frmContrato frmContrato)
        {
            InitializeComponent();
            CargarMercado();
            CargarCliente();
            CargarZafra();
            CargarPodructo();
            _frmContrato = frmContrato;
        }
        

        private void frmContratoDetalle_Load(object sender, EventArgs e)
        {
            if (IdContrato > 0)
            {
                lblTitulo.Text = "ACTUALIZAR CONTRATO";
                btnNuevo.Enabled = false;
                btnGuardar.Text = "Actualizar";
                btnEliminar.Enabled = true;
                CargarContrao(IdContrato);
            }
            else
            {
                lblTitulo.Text = "NUEVO CONTRATO";
                cboAplicaNominacion.SelectedIndex = 1;
                cboEstado.SelectedIndex = 0;
                btnEliminar.Enabled = false;
            }
        }
        private void CargarContrao(int IdContrato)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                DataTable dt = _dal.EjecutarConsulta("[EEXPORTACION].[SP_EXP_CONTRATOS_CONSULTAR_POR_ID]",
                    new
                    {
                        IDCONTEXP = IdContrato
                    });

                if (dt.Rows.Count == 0)
                {
                    Alertas.Advertencia("No se encontró el documento solicitado.");
                    Close();
                    return;
                }

                DataRow r = dt.Rows[0];

               
                cboMercado.EditValue = r["CODMDO"]?.ToString();
                cboCliente.EditValue = Convert.ToInt32(r["ID_ENTIDAD"]);
                deFechaContrato.EditValue = Convert.ToDateTime(r["FECHA_CONTRATO"]);
                DateTime? fecha = r.Field<DateTime?>("FECHA_FIJAR_VOLUMEN");
                deFechaFijarVolumen.EditValue = fecha;
                txtNumeroContrato.Text = r["NUMERO_CONTRATO"]?.ToString();
                cboZafra.EditValue = Convert.ToInt32(r["ID_ZAFRA"]);
                cboProducto.EditValue = Convert.ToInt32(r["ID_PRODUCTO"]);
                cboProducto_EditValueChanged(null, null);
                
                txtPrecio.Value = Convert.ToDecimal(r["PRECIO"]);
                txtVariacionAvg.Value = Convert.ToDecimal(r["VARIACION_AVG"]);
                txtToneladas.Value = Convert.ToDecimal(r["TONELADAS"]);
                txtToneladasMin.Value = Convert.ToDecimal(r["TONELADAS_MIN"]);
                txtToneladasMax.Value = Convert.ToDecimal(r["TONELADAS_MAX"]);
                cboAplicaNominacion.EditValue = r["APLICA_NOMINACION"]?.ToString();
                cboEstado.EditValue = r["ESTADO"]?.ToString();




            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                if (mensaje.Contains("]:"))
                    mensaje = mensaje.Substring(mensaje.IndexOf("]:") + 2).Trim();
                Alertas.Error(mensaje);
            }
            finally
            {
                Cursor = Cursors.Default;
                
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
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(cboMercado.Text))
            {
                Alertas.Advertencia("Seleccionar Mercado.");
                cboMercado.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(cboCliente.Text))
            {
                Alertas.Advertencia("Seleccionar Cliente.");
                cboCliente.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(deFechaContrato.Text))
            {
                Alertas.Advertencia("Ingresar Fecha.");
                deFechaContrato.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNumeroContrato.Text))
            {
                Alertas.Advertencia("Ingresar Numero de Contrato.");
                txtNumeroContrato.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(cboZafra.Text))
            {
                Alertas.Advertencia("Seleccionar Zafra.");
                cboZafra.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(cboProducto.Text))
            {
                Alertas.Advertencia("Seleccionar Producto.");
                cboProducto.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(cboProducto.Text))
            {
                Alertas.Advertencia("Seleccionar Producto.");
                cboProducto.Focus();
                return false;
            }


            return true;
        }
        private void Guardar()
        {
            if (!ValidarCampos()) return;

            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@CODMDO", cboMercado.EditValue);
                parametros.Add("@ID_ENTIDAD", cboCliente.EditValue);
                parametros.Add("@NUMERO_CONTRATO", txtNumeroContrato.Text.Trim().ToUpper());
                parametros.Add("@ID_ZAFRA", cboZafra.EditValue);
                parametros.Add("@ID_PRODUCTO", cboProducto.EditValue);
                parametros.Add("@TONELADAS", txtToneladas.Value);
                parametros.Add("@FECHA_CONTRATO", Convert.ToDateTime(deFechaContrato.Text));
                parametros.Add("@PRESENTACION", txtPresentacion.Text.Trim().ToUpper());
                parametros.Add("@PRECIO", txtPrecio.Value);
                parametros.Add("@VARIACION_AVG", txtVariacionAvg.Value);
                parametros.Add("@TONELADAS_MIN", txtToneladasMin.Value);
                parametros.Add("@TONELADAS_MAX", txtToneladasMax.Value);
                if (deFechaFijarVolumen.EditValue == null)
                { parametros.Add("@FECHA_FIJAR_VOLUMEN", null); }
                else { parametros.Add("@FECHA_FIJAR_VOLUMEN", deFechaFijarVolumen.DateTime.Date); }
                parametros.Add("@APLICA_NOMINACION", cboAplicaNominacion.EditValue);
                parametros.Add("@ESTADO", cboEstado.EditValue);
                parametros.Add("@FIJACIONES", null);
                parametros.Add("@PDF", _pdfBytes ?? (object)DBNull.Value, dbType: DbType.Binary);
                parametros.Add("@INGRESADO_POR", Configuracion.UsuarioActual);
                parametros.Add("@IDCONTEXP", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@Resultado", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@Mensaje", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                _dal.EjecutarConSalida("[EEXPORTACION].[SP_EXP_CONTRATOS_INSERTAR]", parametros);

                int? resultado = parametros.Get<int?>("@Resultado");
                string mensaje = parametros.Get<string>("@Mensaje");

                IdCont = parametros.Get<int?>("@IDCONTEXP");
                if (resultado == 1)
                {
                    _frmContrato.CargarContratos();
                    Alertas.Exito(mensaje);
                   
                }
                else
                {
                    Alertas.Advertencia(mensaje);
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                if (mensaje.Contains("]:"))
                    mensaje = mensaje.Substring(mensaje.IndexOf("]:") + 2).Trim();
                Alertas.Error(mensaje);
            }
        }
        private void Updat()
        {
            if (!ValidarCampos()) return;

            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@IDCONTEXP", IdContrato);
                parametros.Add("@CODMDO", cboMercado.EditValue);
                parametros.Add("@ID_ENTIDAD", cboCliente.EditValue);
                parametros.Add("@NUMERO_CONTRATO", txtNumeroContrato.Text.Trim().ToUpper());
                parametros.Add("@ID_ZAFRA", cboZafra.EditValue);
                parametros.Add("@ID_PRODUCTO", cboProducto.EditValue);
                parametros.Add("@TONELADAS", txtToneladas.Value);
                parametros.Add("@FECHA_CONTRATO", Convert.ToDateTime(deFechaContrato.Text));
                parametros.Add("@PRESENTACION", txtPresentacion.Text.Trim().ToUpper());
                parametros.Add("@PRECIO", txtPrecio.Value);
                parametros.Add("@VARIACION_AVG", txtVariacionAvg.Value);
                parametros.Add("@TONELADAS_MIN", txtToneladasMin.Value);
                parametros.Add("@TONELADAS_MAX", txtToneladasMax.Value);
                if (deFechaFijarVolumen.EditValue == null)
                { parametros.Add("@FECHA_FIJAR_VOLUMEN", null); }
                else { parametros.Add("@FECHA_FIJAR_VOLUMEN", deFechaFijarVolumen.DateTime.Date); }
                parametros.Add("@APLICA_NOMINACION", cboAplicaNominacion.EditValue);
                parametros.Add("@ESTADO", cboEstado.EditValue);
                parametros.Add("@FIJACIONES", null);
                parametros.Add("@PDF",_pdfBytes ?? (object)DBNull.Value,dbType: DbType.Binary );
                parametros.Add("@MODIFICADO_POR", Configuracion.UsuarioActual);

                parametros.Add("@Resultado", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@Mensaje", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                _dal.EjecutarConSalida("[EEXPORTACION].[SP_EXP_CONTRATOS_ACTUALIZAR]", parametros);

                int? resultado = parametros.Get<int?>("@Resultado");
                string mensaje = parametros.Get<string>("@Mensaje");

                if (resultado == 1)
                {
                    _frmContrato.CargarContratos();
                    Alertas.Exito(mensaje);
                }
                else
                {
                    Alertas.Advertencia(mensaje);
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                if (mensaje.Contains("]:"))
                    mensaje = mensaje.Substring(mensaje.IndexOf("]:") + 2).Trim();
                Alertas.Error(mensaje);
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (IdContrato > 0)
            {
                Updat();
            }
            else
            {
                Guardar();
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        // campo de la clase
        private byte[] _pdfBytes = null;
        private string _pdfRutaSeleccionada = null;

        private void beArchivoPdf_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Seleccionar archivo PDF";
                ofd.Filter = "Archivos PDF (*.pdf)|*.pdf";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var fi = new FileInfo(ofd.FileName);

                    const long MAX_MB = 10;
                    if (fi.Length > MAX_MB * 1024 * 1024)
                    {
                        Alertas.Advertencia($"El archivo supera el tamaño máximo permitido ({MAX_MB} MB).");
                        return;
                    }

                    _pdfRutaSeleccionada = ofd.FileName;
                    beArchivoPdf.Text = fi.Name;
                    btnSubirPdf.Enabled = true;
                }
            }
        }

        private void btnSubirPdf_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_pdfRutaSeleccionada) || !File.Exists(_pdfRutaSeleccionada))
            {
                Alertas.Advertencia("Seleccione un archivo PDF válido.");
                return;
            }

            try
            {
                _pdfBytes = File.ReadAllBytes(_pdfRutaSeleccionada);
                Alertas.Exito("PDF cargado. Se guardará junto con el contrato al presionar Guardar.");
                btnSubirPdf.Enabled = false; // ya está listo en memoria
            }
            catch (Exception ex)
            {
                Alertas.Error("No se pudo leer el archivo: " + ex.Message);
            }
        }
        private void Limpiar()
        {
            cboMercado.Text = string.Empty;
            cboCliente.Text = string.Empty;
            deFechaContrato.Text = string.Empty;
            deFechaFijarVolumen.Text = string.Empty;
            txtNumeroContrato.Text = string.Empty;
            cboZafra.Text = string.Empty;
            cboProducto.Text = string.Empty;
            txtPresentacion.Text = string.Empty;
            txtPrecio.Value = 0;
                txtVariacionAvg.Value = 0;
            txtToneladas.Value = 0;
            txtToneladasMin.Value = 0;
            txtToneladasMax.Value = 0;
            cboAplicaNominacion.SelectedIndex = 1;
                cboEstado.SelectedIndex = 1;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
           

        }
    }
}
