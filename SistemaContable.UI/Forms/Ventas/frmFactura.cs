using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System.Linq;
using System.Globalization;
namespace SistemaContable.UI.Forms.Ventas
{
    public partial class frmFactura : Form
    {
        private enum EstadoFormulario { Nuevo, Guardado, Validado }
        #region Campos privados
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtDetalle;
        private int _idEntidad = 0;
        private string _codigoEntidad = string.Empty;
        private string _columnaAnteriorGrid = string.Empty;
        // CODIPROVEEDOR del cliente seleccionado, usado para filtrar el combo "Solicitud"
        // (mismo patrón que frmCreditoFiscal). [EDTE].[SP_FACTURA_ENC] @ACCION='OBTENER' todavía
        // no devuelve la columna CODIPROVEEDOR, así que al reabrir una factura existente el
        // combo Solicitud queda vacío (igual que en CCF); se completa al buscar el cliente.
        private string _codiProveedorCliente = string.Empty;
        private string _codigoTransportistaCliente = string.Empty;
        private string _codigoFrenteRozaCliente = string.Empty;
        private string _codigoFrenteQuerqueoCliente = string.Empty;
        // (2026-09-21) Mismos campos que frmCreditoFiscal para la integración de solicitudes.
        private string _tipoSolicitudSeleccionada = string.Empty;
        private int? _idTipoSolicitudSeleccionada = null;
        private string _codigoSolicitudSeleccionada = string.Empty;
        private string _nombreProveedorSolicitud = string.Empty;
        private string _nombreZafraSolicitud = string.Empty;
        // Historial de integración SIGESTA (solicitud agrícola importada) - 2026-08-25:
        // igual que frmCreditoFiscal, se conserva el ID_SOLICITUD internamente
        // (ya no hay combo visible) para poder guardarlo en [EDTE].[SP_FACTURA_ENC].
        private int? _idSolicitudSeleccionada = null;
        // (2026-08-26) ID_CUENTA_FINAN de la solicitud agrícola seleccionada (mismo patrón
        // que frmCreditoFiscal). Se guarda directo en FACTURA_ENC.ID_CUENTA_FINAN para que
        // [ESOLICITUD].[SP_CREDITO_ENCA] (GUARDAR_DESDE_FACTURA) lo lea sin hacer joins.
        private int? _idCuentaFinanSolicitud = null;
        #endregion
        public int IdFactEnc { get; set; } = 0;
        public int AnioDte { get; set; } = 0;
        private int? _diasCredito = null;
        public int _idTipoContribCliente { get; set; } = 0;
        public int _idTipoPersona { get; set; } = 0;

        public int _idTipoContribEMISOR { get; set; } = 0;
        public int _idTipoPersonaEMISOR { get; set; } = 0;

        // Tasas fiscales (se cargan desde [EMH].[DTRETENCION])
        private decimal _porcIVA = 0.13m;
        private decimal _porcIVARET = 0.01m;
        private decimal _porcIVAPER = 0.01m;
        private decimal _extraerIVA = 0m;
        private decimal _extraerRENTA = 0m;

        private int _idEstado = 1;   // ← FALTA ESTA LÍNEA

        public frmFactura()
        {
            InitializeComponent();
            txtRECIB_EFECTIVO.Leave += txtRECIB_EFECTIVO_Leave_FormaPago;   // vuelto (2026-10-01)
            // N° Monto de remesa / cheque / nota de abono: solo lectura, siempre igual al monto de arriba
            foreach (TextBox m in new[] { txtRECIB_REMESA_MONTO, txtRECIB_CHEQUE_MONTO, txtRECIB_NOTAABONO_MONTO })
            {
                m.ReadOnly = true;
                m.TabStop = false;
            }
            txtRECIB_ANTICIPO.Leave += (s, e) => { LimitarPagoNoEfectivo(txtRECIB_ANTICIPO); ActualizarTotales(); };
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// ToolTip de búsqueda genérica ("*" + Enter), igual que en frmCreditoFiscal,
        /// para el campo de búsqueda de Cliente.
        /// </summary>
        private void ConfigurarToolTips()
        {
            TooltipHelper.Configurar(
                            (txtCLIENTE, "Ingrese * y presione Enter para mostrar todos los clientes.")
                                 );
        }

        #region CARGA INICIAL
        private void frmFactura_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);
            ConfigurarToolTips();
            CargarTipoDte();
            CargarSucursal();
            CargarCondicionPago();
            CargarZafra();
            CargarCentroCosto();
            CargarVendedor();
            CargarEmisor(1);
            CargarTasasRetencion();
            if (cbxTIPO_DTE.Items.Count > 0) cbxTIPO_DTE.SelectedIndex = 0;
            InicializarGridDetalle();
            FormHelper.RegistrarBusqueda(
                txtCLIENTE,
                new BusquedaConfig
                {
                    StoredProcedure = "[EDTE].[SP_BUSCAR_CLIENTE_FACTURA]",
                    Accion = "BUSCAR_NO_CONTRIBUYENTES",
                    Columnas = new Dictionary<string, string>
                    {
                        { "CODIGO_ENTIDAD", "CLIENTE" },
                        { "NOMBRE",         "NOMBRE"  },
                        { "NIT",            "NIT"     },
                        { "DUI",            "DUI"     }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "CODIGO_ENTIDAD", 200 },
                        { "NOMBRE",         300 },
                        { "NIT",            150 },
                        { "DUI",            150 }
                    },
                    ParametrosExtra = new
                    {
                        ROL = "CLI",
                        ID_ROL_USUARIO = Configuracion.IdRolActual
                    }
                },
                fila => AsignarCliente(fila)
            );
            if (IdFactEnc == 0)
            {
                LimpiarFormulario();
                ConfigurarCRUD(EstadoFormulario.Nuevo);
                txtCLIENTE.Focus();
            }
            else
            {
                CargarFacturaExistente(IdFactEnc);
            }
        }
        private void ConfigurarCRUD(EstadoFormulario estado)
        {
            switch (estado)
            {
                case EstadoFormulario.Nuevo:
                    txtCLIENTE.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnValidar.Enabled = false;
                    btnImprimir.Enabled = false;
                    btnCorreo.Enabled = false;
                    break;
                case EstadoFormulario.Guardado:
                    txtCLIENTE.Enabled = false;
                    btnGuardar.Enabled = true;
                    btnValidar.Enabled = true;
                    btnImprimir.Enabled = true;
                    btnCorreo.Enabled = false;
                    break;
                case EstadoFormulario.Validado:
                    txtCLIENTE.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnValidar.Enabled = false;
                    btnImprimir.Enabled = true;
                    btnCorreo.Enabled = true;
                    break;
            }
        }
        private void CargarFacturaExistente(int idFactEnc)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                DataTable dt = _dal.EjecutarConsulta("[EDTE].[SP_FACTURA_ENC]",
                    new { ACCION = "OBTENER", ID_FACTENC = idFactEnc, ID_EMISOR = 1 });

                if (dt == null || dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No se encontró el documento solicitado.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    return;
                }

                DataRow r = dt.Rows[0];
                IdFactEnc = Convert.ToInt32(r["ID_FACTENC"]);

                // NUEVO: estado real del documento (0 anulado / 1 activo / 2 validado)
                _idEstado = r.Table.Columns.Contains("ID_ESTADO") && r["ID_ESTADO"] != DBNull.Value
                                ? Convert.ToInt32(r["ID_ESTADO"])
                                : 1;

                _idEntidad = Convert.ToInt32(r["ID_CLIENTE"]);
                _codigoEntidad = r["COD_REF"].ToString();

                txtCLIENTE.Text = _codigoEntidad;
                txtNOMBRE_CLIENTE.Text = AsString(r["NOMBRE_ENTIDAD"]);
                txtDUI.Text = AsString(r["DUI"]);
                txtNIT.Text = AsString(r["NIT"]);
                txtTELEFONO.Text = AsString(r["CELULAR"]);
                txtCORREO.Text = AsString(r["CORREO"]);
                txtACTIVIDAD_PRIMARIA.Text = AsString(r["ACTIVIDAD_PRIMARIA"]);
                txtDIRECCION.Text = AsString(r["COMPLEMENTO"]);

                cbxTIPO_DTE.SelectedValue = Convert.ToInt32(r["ID_TIPO_DTE"]);
                cbxSUCURSAL.SelectedValue = Convert.ToInt32(r["ID_SUCURSAL"]);
                cbxCONDPAGO.SelectedValue = Convert.ToInt32(r["ID_CONDPAGO"]);
                cbxZAFRA.SelectedValue = r["ID_ZAFRA"] == DBNull.Value ? null : (object)Convert.ToInt32(r["ID_ZAFRA"]);
                cbxCENTRO_COSTO.SelectedValue = r["ID_CENTRO"] == DBNull.Value ? null : (object)Convert.ToInt32(r["ID_CENTRO"]);

                mskFECHA.Text = AsFecha(r["FECHA"]);
                mskFECHA_VENCE.Text = AsFecha(r["FECHA_VENCE"]);
                txtNUMINTERNO.Text = FormatearNumInterno(AsString(r["NUMINTERNO"]));
                txtNUM_CONTROL.Text = AsString(r["NUMCONTROL"]);
                txtCOD_GENERACION.Text = AsString(r["CODGENERACION"]);
                txtSELLO_RECIBIDO.Text = AsString(r["SELLORECEPCION"]);

                AsignarDecimal(txtVENTA_GRAVADA, ToDecimal(r["AFECTA"]));
                AsignarDecimal(txtVENTA_EXENTA, ToDecimal(r["EXCENTA"]));
                AsignarDecimal(txtPORC_DESCUENTO, ToDecimal(r["DESCUENTO"]));
                AsignarDecimal(txtDESCUENTO, ToDecimal(r["DESCUENTO_VALOR"]));
                AsignarDecimal(txtSUBTOTAL, ToDecimal(r["SUBTOTAL"]));
                AsignarDecimal(txtIVA, ToDecimal(r["IVA"]));
                AsignarDecimal(txtRETENCION, ToDecimal(r["IVARETENIDO"]));
                AsignarDecimal(txtPERCEPCION, ToDecimal(r["IVAPERCIBIDO"]));
                AsignarDecimal(txtTOTAL_VENTA, ToDecimal(r["TOTALVENTA"]));

                AsignarDecimal(txtRECIB_EFECTIVO, ToDecimal(r["RECIB_EFECTIVO"]));
                AsignarDecimal(txtRECIB_REMESA, ToDecimal(r["RECIB_REMESA"]));
                AsignarDecimal(txtRECIB_CHEQUE, ToDecimal(r["RECIB_CHEQUE"]));
                AsignarDecimal(txtRECIB_NOTAABONO, ToDecimal(r["RECIB_NOTAABONO"]));
                AsignarDecimal(txtRECIB_ANTICIPO, ToDecimal(r["RECIB_ANTICIPO"]));
                AsignarDecimal(txtRECIB_EFECTIVO_CAMBIO, ToDecimal(r["RECIB_EFECTIVO_CAMBIO"]));

                // NUEVOS
                txtRECIB_REMESA_BANCO.Text = AsString(r["RECIB_REMESA_BANCO"]);
                txtRECIB_REMESA_CUENTA.Text = AsString(r["RECIB_REMESA_CUENTA"]);
                AsignarDecimal(txtRECIB_REMESA_MONTO, ToDecimal(r["RECIB_REMESA_MONTO"]));
                txtRECIB_CHEQUE_BANCO.Text = AsString(r["RECIB_CHEQUE_BANCO"]);
                txtRECIB_CHEQUE_CUENTA.Text = AsString(r["RECIB_CHEQUE_CUENTA"]);
                AsignarDecimal(txtRECIB_CHEQUE_MONTO, ToDecimal(r["RECIB_CHEQUE_MONTO"]));
                txtRECIB_NOTAABONO_BANCO.Text = AsString(r["RECIB_NOTAABONO_BANCO"]);
                txtRECIB_NOTAABONO_CUENTA.Text = AsString(r["RECIB_NOTAABONO_CUENTA"]);
                AsignarDecimal(txtRECIB_NOTAABONO_MONTO, ToDecimal(r["RECIB_NOTAABONO_MONTO"]));

                chkPERCEPCION.Checked = Convert.ToBoolean(r["AP_PERCEPCION"]);
                txtOBSERVACION.Text = AsString(r["OBSERVACIONES"]);

                // CODIPROVEEDOR del cliente (necesario para "Solicitud Agrícola" al reabrir).
                CargarCodigosSolicitudCliente(null);

                // Historial de integración SIGESTA (solicitud agrícola importada) - 2026-08-25
                _idSolicitudSeleccionada = r.Table.Columns.Contains("ID_SOLICITUD") && r["ID_SOLICITUD"] != DBNull.Value
                                                ? Convert.ToInt32(r["ID_SOLICITUD"]) : (int?)null;
                AsignarDatosHistorialSolicitud(r);
                // (2026-08-26) AsignarDatosHistorialSolicitud ya intenta leer ID_CUENTA_FINAN de
                // "r", pero por claridad se deja explícito aquí también (mismo patrón que CCF).
                _idCuentaFinanSolicitud = r.Table.Columns.Contains("ID_CUENTA_FINAN") && r["ID_CUENTA_FINAN"] != DBNull.Value
                    ? (int?)Convert.ToInt32(r["ID_CUENTA_FINAN"]) : null;
                _tipoSolicitudSeleccionada = ValorCodigo(r, "TIPO_SOLICITUD");
                _idTipoSolicitudSeleccionada = r.Table.Columns.Contains("ID_TIPO_SOLICITUD") && r["ID_TIPO_SOLICITUD"] != DBNull.Value
                    ? (int?)Convert.ToInt32(r["ID_TIPO_SOLICITUD"]) : null;
                _codigoSolicitudSeleccionada = ValorCodigo(r, "CODIGO");
                _nombreProveedorSolicitud = ValorCodigo(r, "NOMBRE_PROVEEDOR");
                _nombreZafraSolicitud = ValorCodigo(r, "NOMBRE_ZAFRA");
                AplicarCentroCostoPorSolicitud(false);   // 2026-10-05 bloquear centro si hay solicitud

                ActualizarEstadoBotonSolicitudAgricola();

                CargarFacturaDetalleExistente(idFactEnc);
                AplicarEstadoCobroPorCondicion();   // 2026-10-01
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar el documento:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;

                // Validado SOLO si el documento está realmente validado (ID_ESTADO = 2).
                // Cualquier otro estado (guardado) mantiene Guardar habilitado.
                ConfigurarCRUD(_idEstado == 2
                    ? EstadoFormulario.Validado
                    : EstadoFormulario.Guardado);
            }
        }
        private void CargarFacturaDetalleExistente(int idFactEnc)
        {
            DataTable dt = _dal.EjecutarConsulta("[EDTE].[SP_FACTURA_DET]",
                new { ACCION = "LISTAR", ID_FACTENC = idFactEnc, ID_EMISOR = 1 });
            _dtDetalle.Clear();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var f = _dtDetalle.NewRow();
                    f["ID_PRODUCTO"] = row["ID_PRODUCTO"];
                    f["COD_REF"] = row["COD_REF"];
                    f["DESCRIPCION"] = row["DESCRIPCION"];
                    f["UM"] = row["UNIDAD_MEDIDA"];
                    f["PORC_DESC"] = row["DESCUENTO"];
                    f["CANTIDAD"] = row["CANTIDAD"];
                    f["PRECIO"] = row["PRECIO"];
                    f["ES_EXENTO"] = row["ES_EXENTO"];
                    f["ES_NOSUJETA"] = row.Table.Columns.Contains("ES_NOSUJETA")
                                            ? row["ES_NOSUJETA"] : (object)false;
                    f["DESCUENTO"] = row["DESCUENTO_VALOR"];
                    f["NOSUJETA"] = row.Table.Columns.Contains("NOSUJETA")
                                            ? row["NOSUJETA"] : (object)0m;
                    f["EXENTO"] = row["EXENTA"];
                    f["GRAVADO"] = row["GRAVADA"];
                    f["TOTAL"] = row["TOTAL"];
                    f["ID_UNIDAD_MEDIDA"] = row.Table.Columns.Contains("ID_UNIDAD_MEDIDA")
                                            ? row["ID_UNIDAD_MEDIDA"] : (object)0;
                    // Historial de integración SIGESTA: conservar la trazabilidad al reabrir,
                    // para que un re-guardado no la pierda.
                    f["ID_SOLICITUD"] = row.Table.Columns.Contains("ID_SOLICITUD")
                                            ? row["ID_SOLICITUD"] : (object)DBNull.Value;
                    f["ID_SOLIC_DETA"] = row.Table.Columns.Contains("ID_SOLIC_DETA")
                                            ? row["ID_SOLIC_DETA"] : (object)DBNull.Value;
                    f["UID_SOLIC_DETA"] = row.Table.Columns.Contains("UID_SOLIC_DETA")
                                            ? row["UID_SOLIC_DETA"] : (object)DBNull.Value;
                    _dtDetalle.Rows.Add(f);
                }
            }
            AgregarFilaVacia();
            ActualizarTotales();
        }
        private void CargarSiguienteNumFactura(string abreviaturaDTE, int anio)
        {
            var num = _dal.ObtenerNumeracionPrevia(abreviaturaDTE, anio);
            if (num == null)
            {
                MessageBox.Show(
                    "No existe numeración activa para la Factura (FA).\n" +
                    "Configúrela en DOCUMENTO_NUMERACION antes de continuar.",
                    "Numeración no encontrada",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNUMINTERNO.Text = "";
                return;
            }
            txtNUMINTERNO.Text = num.SiguienteNumeroFormateado();
        }
        #endregion
        #region CLIENTE
        private void AsignarCliente(DataRow fila)
        {
            _idEntidad = Convert.ToInt32(fila["ID_ENTIDAD"]);
            _codigoEntidad = fila["CODIGO_ENTIDAD"].ToString();
            _idTipoContribCliente = Convert.ToInt32(fila["ID_TIPO_CONTRIB"]);
            _idTipoPersona = Convert.ToInt32(fila["ID_TIPO_PERSONA"]);
            txtCLIENTE.Text = _codigoEntidad;
            txtNOMBRE_CLIENTE.Text = fila["NOMBRE"].ToString();
            txtNIT.Text = fila["NIT"].ToString();
            txtDUI.Text = fila["DUI"].ToString();
            txtTELEFONO.Text = fila["TELEFONO"].ToString();
            txtCORREO.Text = fila["CORREO"].ToString();
            txtACTIVIDAD_PRIMARIA.Text = fila["ACTIVIDAD_PRIMARIA"].ToString();
            txtDIRECCION.Text = fila["COMPLEMENTO"].ToString();
            txtTIPO_CONTRIBUYENTE.Text = fila["TIPO_CONTRIBUYENTE"].ToString();
            // Lectura del CODIPROVEEDOR para filtrar las solicitudes. Si la búsqueda
            // resumida todavía no expone la columna, se recupera desde OBTENER.
            CargarCodigosSolicitudCliente(fila);
            _idSolicitudSeleccionada = null;
            AsignarDatosHistorialSolicitud(null);
            ActualizarEstadoBotonSolicitudAgricola();
        }
        private void ActualizarEstadoBotonSolicitudAgricola()
        {
            bool hayDatosVisiblesCliente =
                !string.IsNullOrWhiteSpace(txtCLIENTE.Text) ||
                !string.IsNullOrWhiteSpace(txtNOMBRE_CLIENTE.Text) ||
                !string.IsNullOrWhiteSpace(txtDUI.Text) ||
                !string.IsNullOrWhiteSpace(txtNIT.Text) ||
                !string.IsNullOrWhiteSpace(txtTELEFONO.Text) ||
                !string.IsNullOrWhiteSpace(txtCORREO.Text) ||
                !string.IsNullOrWhiteSpace(txtACTIVIDAD_PRIMARIA.Text) ||
                !string.IsNullOrWhiteSpace(txtDIRECCION.Text) ||
                !string.IsNullOrWhiteSpace(txtTIPO_CONTRIBUYENTE.Text);

            bool clienteCargado = _idEntidad > 0 && hayDatosVisiblesCliente;

            // (2026-10-01) Solo se habilita si la condición es CRÉDITO y el cliente
            // tiene código relacionado (Productor, Transportista, Roza o Querqueo).
            bool tieneCodigoRelacionado =
                !string.IsNullOrWhiteSpace(_codiProveedorCliente) ||
                !string.IsNullOrWhiteSpace(_codigoTransportistaCliente) ||
                !string.IsNullOrWhiteSpace(_codigoFrenteRozaCliente) ||
                !string.IsNullOrWhiteSpace(_codigoFrenteQuerqueoCliente);
            bool esCredito = EsCondicionCredito();

            bool habilitar = clienteCargado && tieneCodigoRelacionado && esCredito;
            btn_solicitudAgricola.Enabled = habilitar;
            btn_solicitudAgricola.ToolTip =
                !clienteCargado          ? "Seleccione un cliente antes de consultar solicitudes agrícolas."
              : !tieneCodigoRelacionado  ? "El cliente no tiene código relacionado (Productor, Transportista, Roza o Querqueo)."
              : !esCredito               ? "La solicitud agrícola solo aplica cuando la condición de pago es CRÉDITO."
              :                            "Consultar solicitudes agrícolas del cliente seleccionado.";
        }

        // CRÉDITO = ID_CONDPAGO 2 (CONTADO = 1)
        private bool EsCondicionCredito()
        {
            return ObtenerIdCombo(cbxCONDPAGO) == 2;
        }
        private void CargarCodigosSolicitudCliente(DataRow filaBusqueda)
        {
            _codiProveedorCliente = ObtenerCodiProveedorCliente(filaBusqueda);
            _codigoTransportistaCliente = string.Empty;
            _codigoFrenteRozaCliente = string.Empty;
            _codigoFrenteQuerqueoCliente = string.Empty;

            DataTable entidad = _dal.EjecutarConsulta("[EDTE].[SP_ENTIDAD]", new
            {
                ACCION = "OBTENER",
                ID_ENTIDAD = _idEntidad
            });
            if (entidad != null && entidad.Rows.Count > 0)
                _codigoTransportistaCliente = ValorCodigo(entidad.Rows[0], "CODTRANSPORT");

            DataTable integracion = _dal.EjecutarConsulta("[EDTE].[SP_ENTIDAD]", new
            {
                ACCION = "OBTENER_INTEGRACION",
                ID_ENTIDAD = _idEntidad
            });
            if (integracion != null && integracion.Rows.Count > 0)
            {
                DataRow r = integracion.Rows[0];
                _codigoFrenteRozaCliente = ValorCodigo(r, "ID_PROVEEDOR_ROZA");
                _codigoFrenteQuerqueoCliente = ValorCodigo(r, "ID_PROVEE_QQ");
            }
        }
        private static string ValorCodigo(DataRow fila, string columna)
        {
            return fila != null && fila.Table.Columns.Contains(columna) &&
                   fila[columna] != DBNull.Value
                ? fila[columna].ToString().Trim()
                : string.Empty;
        }
        private string ObtenerCodiProveedorCliente(DataRow fila)
        {
            if (fila != null && fila.Table.Columns.Contains("CODIPROVEEDOR") &&
                fila["CODIPROVEEDOR"] != DBNull.Value)
            {
                string codiProveedor = fila["CODIPROVEEDOR"].ToString().Trim();
                if (!string.IsNullOrWhiteSpace(codiProveedor))
                    return codiProveedor;
            }

            DataTable entidad = _dal.EjecutarConsulta("[EDTE].[SP_ENTIDAD]", new
            {
                ACCION = "OBTENER",
                ID_ENTIDAD = _idEntidad
            });

            return entidad != null && entidad.Rows.Count > 0 &&
                   entidad.Columns.Contains("CODIPROVEEDOR") &&
                   entidad.Rows[0]["CODIPROVEEDOR"] != DBNull.Value
                ? entidad.Rows[0]["CODIPROVEEDOR"].ToString().Trim()
                : string.Empty;
        }
        /// <summary>
        /// Historial de integración SIGESTA (solicitud agrícola importada) - 2026-08-25:
        /// refleja en los 3 campos de solo lectura (txtNUM_SOLICITUD, txtNOMBRE_CUENTA,
        /// txtREFERENCIA) los datos de la solicitud agrícola vinculada, o los limpia si
        /// encabezadoSolicitud es null. Mismo patrón que frmCreditoFiscal.
        /// </summary>
        private void AsignarDatosHistorialSolicitud(DataRow encabezadoSolicitud)
        {
            if (encabezadoSolicitud == null)
            {
                txtNUM_SOLICITUD.Text = "";
                txtNOMBRE_CUENTA.Text = "";
                txtREFERENCIA.Text = "";
                _idCuentaFinanSolicitud = null;
                _tipoSolicitudSeleccionada = string.Empty;
                _idTipoSolicitudSeleccionada = null;
                _codigoSolicitudSeleccionada = string.Empty;
                _nombreProveedorSolicitud = string.Empty;
                _nombreZafraSolicitud = string.Empty;
                cbxCENTRO_COSTO.Enabled = true;   // 2026-10-05 sin solicitud: centro libre
                return;
            }
            txtNUM_SOLICITUD.Text = encabezadoSolicitud.Table.Columns.Contains("NUM_SOLICITUD") &&
                                     encabezadoSolicitud["NUM_SOLICITUD"] != DBNull.Value
                ? encabezadoSolicitud["NUM_SOLICITUD"].ToString() : "";
            txtNOMBRE_CUENTA.Text = encabezadoSolicitud.Table.Columns.Contains("NOMBRE_CUENTA") &&
                                     encabezadoSolicitud["NOMBRE_CUENTA"] != DBNull.Value
                ? encabezadoSolicitud["NOMBRE_CUENTA"].ToString() : "";
            txtREFERENCIA.Text = encabezadoSolicitud.Table.Columns.Contains("UID_SOLICITUD") &&
                                  encabezadoSolicitud["UID_SOLICITUD"] != DBNull.Value
                ? encabezadoSolicitud["UID_SOLICITUD"].ToString() : "";
            // (2026-08-26) Igual que frmCreditoFiscal.
            _idCuentaFinanSolicitud = encabezadoSolicitud.Table.Columns.Contains("ID_CUENTA_FINAN") &&
                                       encabezadoSolicitud["ID_CUENTA_FINAN"] != DBNull.Value
                ? (int?)Convert.ToInt32(encabezadoSolicitud["ID_CUENTA_FINAN"]) : null;
            _tipoSolicitudSeleccionada = ValorCodigo(encabezadoSolicitud, "TIPO_SOLICITUD");
            _idTipoSolicitudSeleccionada = encabezadoSolicitud.Table.Columns.Contains("ID_TIPO_SOLICITUD") &&
                                            encabezadoSolicitud["ID_TIPO_SOLICITUD"] != DBNull.Value
                ? (int?)Convert.ToInt32(encabezadoSolicitud["ID_TIPO_SOLICITUD"]) : null;
            _codigoSolicitudSeleccionada = ValorCodigo(encabezadoSolicitud, "CODIGO");
            _nombreProveedorSolicitud = ValorCodigo(encabezadoSolicitud, "NOMBRE_PROVEEDOR");
            // La zafra siempre procede del combo seleccionado (igual que CCF).
            _nombreZafraSolicitud = cbxZAFRA.Text == null ? string.Empty : cbxZAFRA.Text.Trim();
        }
        private void cbxZAFRA_SelectedIndexChanged(object sender, EventArgs e)
        {
            _idSolicitudSeleccionada = null;
            AsignarDatosHistorialSolicitud(null);
        }
        private void CargarEmisor(int idEmisor)
        {
            DataTable dt = _dal.EjecutarConsulta("[dbo].[SP_EMISOR]",
                new { ACCION = "OBTENER", ID_EMISOR = idEmisor });
            if (dt == null || dt.Rows.Count == 0) return;
            DataRow r = dt.Rows[0];
            _idTipoContribEMISOR = Convert.ToInt32(r["ID_TIPO_CONTRIB"].ToString());
            _idTipoPersonaEMISOR = Convert.ToInt32(r["ID_TIPO_PERSONA"].ToString());
        }

        private void CargarTasasRetencion()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_DTRETENCION]",
                new { ACCION = "OBTENER", ID_DTRETENCION = 1 });
            if (dt == null || dt.Rows.Count == 0) return;
            DataRow r = dt.Rows[0];
            _porcIVA = r["IVA"] == DBNull.Value ? 0.13m : Convert.ToDecimal(r["IVA"]) / 100m;
            _porcIVARET = r["IVARET"] == DBNull.Value ? 0.01m : Convert.ToDecimal(r["IVARET"]) / 100m;
            _porcIVAPER = r["IVAPER"] == DBNull.Value ? 0.01m : Convert.ToDecimal(r["IVAPER"]) / 100m;
            _extraerIVA = r["EXTRAER_IVA"] == DBNull.Value ? 0m : Convert.ToDecimal(r["EXTRAER_IVA"]);
            _extraerRENTA = r["EXTRAER_RENTA"] == DBNull.Value ? 0m : Convert.ToDecimal(r["EXTRAER_RENTA"]);
        }
        #endregion
        #region COMBOS
        private void CargarTipoDte()
        {
            DataTable dt = _dal.EjecutarConsulta("SP_TIPO_DTE",
                new { ACCION = "OBTENER", ID_TIPO_DTE = 1 });
            cbxTIPO_DTE.DataSource = dt;
            cbxTIPO_DTE.ValueMember = "ID_TIPO_DTE";
            cbxTIPO_DTE.DisplayMember = "ABREVIATURA";
        }
        private void CargarSucursal()
        {
            DataTable dt = _dal.EjecutarConsulta("SP_SUCURSAL",
                new { ACCION = "OBTENER", ID_SUCURSAL = 1 });
            cbxSUCURSAL.DataSource = dt;
            cbxSUCURSAL.ValueMember = "ID_SUCURSAL";
            cbxSUCURSAL.DisplayMember = "NOMBRE";
        }
        private void CargarCondicionPago()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_CONDICION_OPERACION]",
                new { ACCION = "OBTENER", ID_CONDICION_OPERACION = -1 });
            cbxCONDPAGO.DataSource = dt;
            cbxCONDPAGO.ValueMember = "ID_CONDICION_OPERACION";
            cbxCONDPAGO.DisplayMember = "VALORES";
        }
        private void CargarZafra()
        {
            DataTable dt = _dal.EjecutarConsulta("[EGENERALES].[SP_ZAFRA]",
                new { ACCION = "OBTENER", ID_ZAFRA = 1 });
            cbxZAFRA.DataSource = dt;
            cbxZAFRA.ValueMember = "ID_ZAFRA";
            cbxZAFRA.DisplayMember = "NOMBRE_ZAFRA";
        }
        private void CargarCentroCosto()
        {
            // (2026-09-16) Filtrado por rol, igual que en frmCreditoFiscal: si el rol
            // del usuario tiene centros de costo asignados en ESEGURIDAD.ROL_CENTROCOSTO
            // (frmRol), solo se muestran esos; si no tiene ninguno asignado, se
            // muestran todos por defecto. Se usa el SP independiente
            // [EDTE].[SP_BUSCAR_CENTROCOSTO] (no [EDTE].[SP_CENTROCOSTO]).
            DataTable dt = _dal.EjecutarConsulta("[EDTE].[SP_BUSCAR_CENTROCOSTO]",
                new { ACCION = "LISTAR_POR_ROL", ID_ROL_USUARIO = Configuracion.IdRolActual });
            cbxCENTRO_COSTO.DataSource = dt;
            cbxCENTRO_COSTO.ValueMember = "ID_CENTRO";
            cbxCENTRO_COSTO.DisplayMember = "NOMBRE";
        }
        private void CargarVendedor()
        {
            DataTable dt = _dal.EjecutarConsulta("[EDTE].[SP_VENDEDOR]",
                new { ACCION = "OBTENER", ID_VENDEDOR = -1 });
            cbxVENDEDOR.DataSource = dt;
            cbxVENDEDOR.ValueMember = "ID_VENDEDOR";
            cbxVENDEDOR.DisplayMember = "NOMBRE";
        }
        private int ObtenerAnioPorDte(int? idTipoDte)
        {
            DataTable dt = _dal.EjecutarConsulta("[dbo].[SP_DOCUMENTO_NUMERACION]",
                new { ACCION = "OBTENER_ANIO_POR_DTE", ID_TIPO_DTE = idTipoDte });
            if (dt == null || dt.Rows.Count == 0) return 0;
            return Convert.ToInt32(dt.Rows[0]["ANIO"]);
        }
        #endregion
        #region GRID DETALLE
        private void InicializarGridDetalle()
        {
            _dtDetalle = new DataTable();
            _dtDetalle.Columns.Add("ID_PRODUCTO", typeof(int));
            _dtDetalle.Columns.Add("COD_REF", typeof(string));
            _dtDetalle.Columns.Add("DESCRIPCION", typeof(string));
            _dtDetalle.Columns.Add("UM", typeof(string));
            _dtDetalle.Columns.Add("PORC_DESC", typeof(decimal));
            _dtDetalle.Columns.Add("CANTIDAD", typeof(decimal));
            _dtDetalle.Columns.Add("PRECIO", typeof(decimal));
            _dtDetalle.Columns.Add("ES_NOSUJETA", typeof(bool));
            _dtDetalle.Columns.Add("ES_EXENTO", typeof(bool));
            _dtDetalle.Columns.Add("DESCUENTO", typeof(decimal));
            _dtDetalle.Columns.Add("NOSUJETA", typeof(decimal));
            _dtDetalle.Columns.Add("EXENTO", typeof(decimal));
            _dtDetalle.Columns.Add("GRAVADO", typeof(decimal));
            _dtDetalle.Columns.Add("TOTAL", typeof(decimal));
            _dtDetalle.Columns.Add("ID_UNIDAD_MEDIDA", typeof(int));
            // Historial de integración SIGESTA (solicitud agrícola importada) - 2026-08-25:
            // trazabilidad por línea hacia [ESOLICITUD].[SP_SOLICITUDES_SIGESTA]
            // ACTION='PRODUCTOR_DETALLE'. No se muestran en el grid (ver OcultarColumna abajo);
            // se envían a [EDTE].[SP_FACTURA_DET] al guardar.
            _dtDetalle.Columns.Add("ID_SOLICITUD", typeof(int));
            _dtDetalle.Columns.Add("ID_SOLIC_DETA", typeof(int));
            _dtDetalle.Columns.Add("UID_SOLIC_DETA", typeof(string));
            AgregarFilaVacia();
            gridControl1.DataSource = _dtDetalle;
            var view = gridControl1.MainView as GridView;
            if (view == null) return;
            view.Columns.Clear();
            view.PopulateColumns();
            OcultarColumna(view, "ID_PRODUCTO");
            OcultarColumna(view, "ID_SOLICITUD");
            OcultarColumna(view, "ID_SOLIC_DETA");
            OcultarColumna(view, "UID_SOLIC_DETA");
            ConfigurarColumna(view, "COD_REF", "Código", 90, true);
            ConfigurarColumna(view, "DESCRIPCION", "Descripción", 250, true);
            ConfigurarColumna(view, "UM", "U.M.", 55, false);
            ConfigurarColumna(view, "CANTIDAD", "Cantidad", 75, true);
            ConfigurarColumna(view, "PRECIO", "Precio", 105, true);
            ConfigurarColumna(view, "PORC_DESC", "%Desc.", 55, true);
            ConfigurarColumna(view, "GRAVADO", "Gravado", 85, false);
            ConfigurarColumnaCheckBox(view, "ES_NOSUJETA", "No Sujeta", 65);
            ConfigurarColumna(view, "NOSUJETA", "No Sujeta $", 85, false);
            ConfigurarColumnaCheckBox(view, "ES_EXENTO", "Exento", 55);
            ConfigurarColumna(view, "EXENTO", "Exenta $", 85, false);
            ConfigurarColumna(view, "TOTAL", "Total", 90, false);
            ConfigurarColumna(view, "DESCUENTO", "Descuento", 75, false, false);
            ConfigurarColumna(view, "ID_UNIDAD_MEDIDA", "ID_UNIDAD_MEDIDA", 80, false, false);
            view.Columns["COD_REF"].VisibleIndex = 0;
            view.Columns["DESCRIPCION"].VisibleIndex = 1;
            view.Columns["UM"].VisibleIndex = 2;
            view.Columns["CANTIDAD"].VisibleIndex = 3;
            view.Columns["PRECIO"].VisibleIndex = 4;
            view.Columns["PORC_DESC"].VisibleIndex = 5;
            view.Columns["GRAVADO"].VisibleIndex = 6;
            view.Columns["ES_NOSUJETA"].VisibleIndex = 7;
            view.Columns["NOSUJETA"].VisibleIndex = 8;
            view.Columns["ES_EXENTO"].VisibleIndex = 9;
            view.Columns["EXENTO"].VisibleIndex = 10;
            view.Columns["TOTAL"].VisibleIndex = 11;
            var repoPrecio = new RepositoryItemTextEdit();
            repoPrecio.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            repoPrecio.Mask.EditMask = "n6";
            repoPrecio.Mask.UseMaskAsDisplayFormat = true;
            gridControl1.RepositoryItems.Add(repoPrecio);
            view.Columns["PRECIO"].ColumnEdit = repoPrecio;
            // (2026-10-01) CANTIDAD se captura a 4 decimales (PRECIO a 6 decimales).
            var repoCantidad = new RepositoryItemTextEdit();
            repoCantidad.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            repoCantidad.Mask.EditMask = "n4";
            repoCantidad.Mask.UseMaskAsDisplayFormat = true;
            gridControl1.RepositoryItems.Add(repoCantidad);
            view.Columns["CANTIDAD"].ColumnEdit = repoCantidad;
            var colEliminar = view.Columns.AddField("ELIMINAR");
            colEliminar.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            colEliminar.Caption = " ";
            colEliminar.Width = 36;
            colEliminar.Visible = true;
            colEliminar.OptionsColumn.AllowEdit = true;
            colEliminar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            var repoEliminar = new RepositoryItemButtonEdit();
            repoEliminar.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            repoEliminar.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            repoEliminar.Buttons[0].ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.EliminarFila24x24;
            repoEliminar.Buttons[0].Caption = "";
            repoEliminar.Buttons[0].ToolTip = "Eliminar fila";
            repoEliminar.ButtonClick += (s, ev) => EliminarFilaDetalle();
            gridControl1.RepositoryItems.Add(repoEliminar);
            colEliminar.ColumnEdit = repoEliminar;
            foreach (var campo in new[] { "UM", "NOSUJETA", "EXENTO", "GRAVADO", "TOTAL" })
            {
                var col = view.Columns[campo];
                if (col == null) continue;
                col.AppearanceCell.BackColor = Color.FromArgb(240, 240, 240);
                col.AppearanceCell.Options.UseBackColor = true;
                col.AppearanceCell.ForeColor = Color.FromArgb(90, 90, 90);
                col.AppearanceCell.Options.UseForeColor = true;
            }
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsBehavior.Editable = true;
            view.OptionsBehavior.AutoSelectAllInEditor = true;
            view.OptionsNavigation.EnterMoveNextColumn = true;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.OptionsSelection.EnableAppearanceFocusedRow = true;
            view.Appearance.FocusedRow.BackColor = Color.FromArgb(204, 229, 255);
            view.Appearance.FocusedRow.Options.UseBackColor = true;
            view.Appearance.HideSelectionRow.BackColor = Color.FromArgb(204, 229, 255);
            view.Appearance.HideSelectionRow.Options.UseBackColor = true;
            view.Appearance.Row.ForeColor = Color.Black;
            view.Appearance.Row.Font = new Font("Segoe UI", 9f);
            view.Appearance.Row.Options.UseFont = true;
            view.Appearance.Row.Options.UseForeColor = true;
            view.Appearance.HeaderPanel.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            view.Appearance.HeaderPanel.Options.UseFont = true;
            view.CustomColumnDisplayText += (s, ev) =>
            {
                // (2026-09-16) CANTIDAD se muestra a 4 decimales, igual que en
                // frmCreditoFiscal. El dato sigue guardándose igual (decimal, sin
                // redondear) — este cambio es solo del texto que se pinta en la celda.
                if (ev.Column.FieldName == "CANTIDAD")
                {
                    if (ev.Value == null || ev.Value == DBNull.Value) { ev.DisplayText = "0.0000"; return; }
                    ev.DisplayText = decimal.TryParse(ev.Value.ToString(), out decimal cantidad)
                        ? cantidad.ToString("N4")
                        : "0.0000";
                    return;
                }
                if (ev.Column.FieldName == "PRECIO")
                {
                    if (ev.Value == null || ev.Value == DBNull.Value)
                    {
                        ev.DisplayText = "0.000000";
                        return;
                    }

                    ev.DisplayText = decimal.TryParse(ev.Value.ToString(), out decimal precio)
                        ? precio.ToString("N6")
                        : "0.000000";
                    return;
                }
                if (ev.Column.FieldName == "PORC_DESC" ||
                    ev.Column.FieldName == "DESCUENTO" ||
                    ev.Column.FieldName == "NOSUJETA" || ev.Column.FieldName == "EXENTO" ||
                    ev.Column.FieldName == "GRAVADO" || ev.Column.FieldName == "TOTAL")
                {
                    if (ev.Value == null || ev.Value == DBNull.Value) { ev.DisplayText = "0.00"; return; }
                    if (decimal.TryParse(ev.Value.ToString(), out decimal val))
                        ev.DisplayText = val.ToString("N2");
                    else
                        ev.DisplayText = "0.00";
                }
            };
            view.CellValueChanged += (s, ev) =>
            {
                if (ev.Column.FieldName == "CANTIDAD" || ev.Column.FieldName == "PRECIO" ||
                    ev.Column.FieldName == "PORC_DESC" || ev.Column.FieldName == "ES_EXENTO" ||
                    ev.Column.FieldName == "ES_NOSUJETA")
                    RecalcularLinea(s as GridView, ev.RowHandle);
            };
            view.KeyDown += GridView_KeyDown;
            // (2026-10-01) Con detalle de solicitud agrícola no se puede cambiar el producto.
            view.ShowingEditor += (s, ev) =>
            {
                var gv = s as GridView;
                string campo = gv?.FocusedColumn?.FieldName;
                if ((campo == "COD_REF" || campo == "DESCRIPCION") && DetalleDesdeSolicitud())
                    ev.Cancel = true;
            };
            view.FocusedColumnChanged += GridView_FocusedColumnChanged;
            ActualizarTotales();
        }
        private void ConfigurarColumnaCheckBox(GridView view, string field, string caption, int width)
        {
            var col = view.Columns.ColumnByFieldName(field);
            if (col == null) return;
            col.Caption = caption;
            col.Width = width;
            col.Visible = true;
            col.OptionsColumn.AllowEdit = true;
            var repo = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            repo.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard;
            gridControl1.RepositoryItems.Add(repo);
            col.ColumnEdit = repo;
        }
        private void ConfigurarColumna(GridView view, string field, string caption, int width, bool editable, bool visible = true)
        {
            var col = view.Columns.ColumnByFieldName(field);
            if (col == null) { System.Diagnostics.Debug.WriteLine($"[WARN] Columna '{field}' no encontrada."); return; }
            col.Caption = caption;
            col.Width = width;
            col.Visible = visible;
            col.OptionsColumn.AllowEdit = editable;
        }
        private void OcultarColumna(GridView view, string field)
        {
            var col = view.Columns.ColumnByFieldName(field);
            if (col != null) col.Visible = false;
        }
        // (2026-10-01) Cuando el detalle viene de una SOLICITUD AGRÍCOLA solo se permiten
        // los ítems de la solicitud: no se agregan filas nuevas ni se cambia el producto.
        private bool DetalleDesdeSolicitud()
        {
            if (_dtDetalle == null) return false;
            return _dtDetalle.Rows.Cast<DataRow>().Any(r =>
                r.RowState != DataRowState.Deleted &&
                ((r.Table.Columns.Contains("ID_SOLIC_DETA") && r["ID_SOLIC_DETA"] != DBNull.Value) ||
                 (r.Table.Columns.Contains("UID_SOLIC_DETA") && r["UID_SOLIC_DETA"] != DBNull.Value &&
                  !string.IsNullOrWhiteSpace(r["UID_SOLIC_DETA"].ToString()))));
        }
        private void AvisarSoloItemsSolicitud()
        {
            XtraMessageBox.Show(
                "El detalle viene de una solicitud agrícola.\n\nSolo se permiten los ítems de la solicitud; no se pueden agregar ni cambiar productos.",
                "Solicitud agrícola", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void AgregarFilaVacia()
        {
            if (DetalleDesdeSolicitud()) return;   // 2026-10-01 solo ítems de la solicitud
            var fila = _dtDetalle.NewRow();
            fila["ID_PRODUCTO"] = 0;
            fila["COD_REF"] = "";
            fila["DESCRIPCION"] = "";
            fila["UM"] = "";
            fila["CANTIDAD"] = 0m;
            fila["PRECIO"] = 0m;
            fila["PORC_DESC"] = 0m;
            fila["DESCUENTO"] = 0m;
            fila["NOSUJETA"] = 0m;
            fila["EXENTO"] = 0m;
            fila["GRAVADO"] = 0m;
            fila["TOTAL"] = 0m;
            fila["ES_EXENTO"] = false;
            fila["ES_NOSUJETA"] = false;
            fila["ID_UNIDAD_MEDIDA"] = 0;
            fila["ID_SOLICITUD"] = DBNull.Value;
            fila["ID_SOLIC_DETA"] = DBNull.Value;
            fila["UID_SOLIC_DETA"] = DBNull.Value;
            _dtDetalle.Rows.Add(fila);
        }
        private void RecalcularLinea(GridView view, int rowHandle)
        {
            if (view == null || rowHandle < 0) return;
            // (2026-10-01) Cantidad a 4 decimales, precio a 6 decimales; los montos
            // de la linea (subtotal, descuento, total) quedan a 2 decimales.
            decimal cantidad = Math.Round(ObtenerDecimal(view, rowHandle, "CANTIDAD"), 4);
            decimal precio = Math.Round(ObtenerDecimal(view, rowHandle, "PRECIO"), 6);
            decimal porcDesc = ObtenerDecimal(view, rowHandle, "PORC_DESC");
            decimal subtotalLinea = Math.Round(cantidad * precio, 2, MidpointRounding.AwayFromZero);
            decimal descuento = porcDesc > 0 ? Math.Round(subtotalLinea * porcDesc / 100m, 2) : 0m;
            decimal total = subtotalLinea - descuento;
            var valExento = view.GetRowCellValue(rowHandle, "ES_EXENTO");
            var valNosujeta = view.GetRowCellValue(rowHandle, "ES_NOSUJETA");
            bool esExento = valExento != null && valExento != DBNull.Value && Convert.ToBoolean(valExento);
            bool esNosujeta = valNosujeta != null && valNosujeta != DBNull.Value && Convert.ToBoolean(valNosujeta);
            if (esNosujeta) esExento = false;
            decimal montoNosujeta = esNosujeta ? total : 0m;
            decimal montoExento = esExento ? total : 0m;
            decimal montoGravado = (!esExento && !esNosujeta) ? total : 0m;
            view.SetRowCellValue(rowHandle, "DESCUENTO", descuento);
            view.SetRowCellValue(rowHandle, "TOTAL", total);
            view.SetRowCellValue(rowHandle, "NOSUJETA", montoNosujeta);
            view.SetRowCellValue(rowHandle, "EXENTO", montoExento);
            view.SetRowCellValue(rowHandle, "GRAVADO", montoGravado);
            if (rowHandle < _dtDetalle.Rows.Count)
            {
                _dtDetalle.Rows[rowHandle]["ES_EXENTO"] = esExento;
                _dtDetalle.Rows[rowHandle]["ES_NOSUJETA"] = esNosujeta;
                _dtDetalle.Rows[rowHandle]["DESCUENTO"] = descuento;
                _dtDetalle.Rows[rowHandle]["NOSUJETA"] = montoNosujeta;
                _dtDetalle.Rows[rowHandle]["EXENTO"] = montoExento;
                _dtDetalle.Rows[rowHandle]["GRAVADO"] = montoGravado;
                _dtDetalle.Rows[rowHandle]["TOTAL"] = total;
            }
            ActualizarTotales();
        }
        private decimal ObtenerDecimal(GridView view, int rowHandle, string field)
        {
            var val = view.GetRowCellValue(rowHandle, field);
            if (val == null || val == DBNull.Value) return 0m;
            return decimal.TryParse(val.ToString(), out decimal d) ? d : 0m;
        }
        private void ActualizarTotales()
        {
            // 1. Sumar columnas del detalle
            decimal totalNosujeta = 0m, totalExento = 0m, totalGravado = 0m,
                    totalVenta = 0m, totalDescuento = 0m;
            foreach (DataRow fila in _dtDetalle.Rows)
            {
                totalNosujeta += fila["NOSUJETA"] == DBNull.Value ? 0 : Convert.ToDecimal(fila["NOSUJETA"]);
                totalExento += fila["EXENTO"] == DBNull.Value ? 0 : Convert.ToDecimal(fila["EXENTO"]);
                totalGravado += fila["GRAVADO"] == DBNull.Value ? 0 : Convert.ToDecimal(fila["GRAVADO"]);
                totalVenta += fila["TOTAL"] == DBNull.Value ? 0 : Convert.ToDecimal(fila["TOTAL"]);
                totalDescuento += fila["DESCUENTO"] == DBNull.Value ? 0 : Convert.ToDecimal(fila["DESCUENTO"]);
            }

            int? idTipoDte = ObtenerIdCombo(cbxTIPO_DTE);
            decimal subTotal = (totalGravado + totalExento) - totalDescuento;
            decimal iva = 0m;
            decimal retencion = 0m;
            decimal percepcion = 0m;

            // ── FACTURA (1) ─────────────────────────────────────────────────────
            // IVA ya está incluido en el precio (no se desglosa), retención = 0
            if (idTipoDte == 1)
            {
                iva = 0m;
                retencion = 0m;
                percepcion = 0m;
            }
            // ── CCF (2) ─────────────────────────────────────────────────────────
            // IVA se calcula sobre el subtotal; retención/percepción dependen
            // de la clasificación del emisor y del cliente
            else if (idTipoDte == 2)
            {
                iva = Math.Round(subTotal * _porcIVA, 2);

                // Emisor Grande (3) → Cliente Pequeño o Mediano (1 o 2)
                if (_idTipoContribEMISOR == 3 &&
                   (_idTipoContribCliente == 1 || _idTipoContribCliente == 2))
                {
                    if (chkPERCEPCION.Checked)
                    {
                        percepcion = subTotal >= 100 ? Math.Round(subTotal * _porcIVAPER, 2) : 0m;
                        retencion = 0m;
                    }
                    else
                    {
                        retencion = subTotal >= 100 ? Math.Round(subTotal * _porcIVARET, 2) : 0m;
                        percepcion = 0m;
                    }
                }
                // Emisor Grande (3) → Cliente Grande (3): sin retención ni percepción
                else if (_idTipoContribEMISOR == 3 && _idTipoContribCliente == 3)
                {
                    retencion = 0m;
                    percepcion = 0m;
                }
                // Cualquier otra combinación: sin retención ni percepción
                else
                {
                    retencion = 0m;
                    percepcion = 0m;
                }
            }

            decimal totalFinal = subTotal + iva - retencion + percepcion;

            // 2. Actualizar controles
            txtVENTA_NOSUJETA.Text = totalNosujeta.ToString("N2");
            txtVENTA_EXENTA.Text = totalExento.ToString("N2");
            txtVENTA_GRAVADA.Text = totalGravado.ToString("N2");
            txtSUBTOTAL.Text = subTotal.ToString("N2");
            txtIVA.Text = iva.ToString("N2");
            txtRETENCION.Text = retencion.ToString("N2");
            txtPERCEPCION.Text = percepcion.ToString("N2");
            txtDESCUENTO.Text = totalDescuento.ToString("N2");
            txtTOTAL_VENTA.Text = totalFinal.ToString("N2");

            // FIX (2026-10-01): en CONTADO el efectivo es lo que FALTA despues de remesa, cheque,
            // nota de abono y anticipo. Antes se ponia el total completo y, al consultar un
            // documento guardado, el efectivo quedaba encima (ej. 81.56 en lugar de 1.56).
            decimal otrosPagos = ObtenerTextBoxDecimal(txtRECIB_REMESA)
                               + ObtenerTextBoxDecimal(txtRECIB_CHEQUE)
                               + ObtenerTextBoxDecimal(txtRECIB_NOTAABONO)
                               + ObtenerTextBoxDecimal(txtRECIB_ANTICIPO);
            decimal efectivoPendiente = Math.Max(0m, totalFinal - otrosPagos);
            if (EsCondicionContado())
            {
                txtRECIB_EFECTIVO.Text = efectivoPendiente.ToString("N2");
                txtRECIB_EFECTIVO_CAMBIO.Text = "0.00";
            }
            else
            {
                LimpiarCamposCobro();
            }
        }
        private decimal ObtenerTextBoxDecimal(TextBox txt)
        {
            if (txt == null || string.IsNullOrWhiteSpace(txt.Text)) return 0m;
            return decimal.TryParse(txt.Text.Replace(",", ""), out decimal val) ? val : 0m;
        }
        #endregion
        #region NAVEGACIÓN DEL GRID
        private void GridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            var view = sender as GridView;
            if (view == null) return;
            string colActual = view.FocusedColumn?.FieldName;
            if (colActual == "COD_REF")
            {
                string texto = view.ActiveEditor?.Text?.Trim()
                            ?? view.GetFocusedDisplayText()?.Trim();
                if (texto == "*") { e.Handled = true; AbrirBusquedaProducto(view); return; }
            }
            if (colActual == "PRECIO")
            {
                e.Handled = true;
                view.CloseEditor();
                view.FocusedColumn = view.Columns["ELIMINAR"];
                return;
            }
            if (colActual == "ELIMINAR")
            {
                e.Handled = true;
                int filaActual = view.FocusedRowHandle;
                if (filaActual == _dtDetalle.Rows.Count - 1)
                {
                    if (DetalleDesdeSolicitud()) { view.CloseEditor(); e.Handled = true; return; }
                    AgregarFilaVacia();
                }
                view.FocusedRowHandle = filaActual + 1;
                view.FocusedColumn = view.Columns["COD_REF"];
                view.ShowEditor();
                return;
            }
            int colIndex = view.FocusedColumn?.VisibleIndex ?? 0;
            int totalCols = view.VisibleColumns.Count;
            int siguiente = colIndex + 1;
            while (siguiente < totalCols && !view.VisibleColumns[siguiente].OptionsColumn.AllowEdit)
                siguiente++;
            if (siguiente < totalCols)
            {
                view.FocusedColumn = view.VisibleColumns[siguiente];
                view.ShowEditor();
            }
            else
            {
                view.CloseEditor();
                int filaActual = view.FocusedRowHandle;
                if (filaActual == _dtDetalle.Rows.Count - 1)
                {
                    if (DetalleDesdeSolicitud()) { view.CloseEditor(); e.Handled = true; return; }
                    AgregarFilaVacia();
                }
                view.FocusedRowHandle = filaActual + 1;
                view.FocusedColumn = view.Columns["COD_REF"];
                view.ShowEditor();
            }
            e.Handled = true;
        }
        private void GridView_FocusedColumnChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            var view = sender as GridView;
            if (view == null) return;
            if (_columnaAnteriorGrid == "COD_REF" && e.FocusedColumn?.FieldName == "DESCRIPCION")
            {
                string cod = view.GetFocusedRowCellValue("COD_REF")?.ToString()?.Trim();
                if (string.IsNullOrWhiteSpace(cod)) return;
                string descActual = view.GetFocusedRowCellValue("DESCRIPCION")?.ToString();
                if (!string.IsNullOrWhiteSpace(descActual)) return;
                // (2026-09-16) Filtrado por Rol de Producto, igual que en frmCreditoFiscal.
                var dt = _dal.EjecutarConsulta("[EINVENTARIO].[SP_BUSCAR_PRODUCTO_CCF]",
                    new { ACCION = "BUSCAR", FILTRO = cod, ID_ROL_USUARIO = Configuracion.IdRolActual });
                var encontrado = dt.AsEnumerable()
                    .FirstOrDefault(r => r["COD_REF"].ToString().Trim()
                        .Equals(cod, StringComparison.OrdinalIgnoreCase));
                if (encontrado != null) AsignarProductoAFila(view, encontrado);
                view.ShowEditor();
                this.BeginInvoke(new Action(() => { view.ActiveEditor?.SelectAll(); }));
            }
            _columnaAnteriorGrid = e.FocusedColumn?.FieldName ?? "";
        }
        #endregion
        #region BÚSQUEDA DE PRODUCTO
        private void AbrirBusquedaProducto(GridView view)
        {
            if (DetalleDesdeSolicitud()) { AvisarSoloItemsSolicitud(); return; }
            // (2026-09-16) Filtrado por rol, igual que en frmCreditoFiscal: si el rol
            // del usuario tiene Roles de Producto asignados en ESEGURIDAD.ROL_ROL_PROD
            // (frmRol), solo se muestran los productos que tengan asignado alguno de
            // esos roles (pestaña "Roles del Producto" de frmProducto); si no tiene
            // ninguno asignado, se muestran todos por defecto. Se usa el SP
            // independiente [EINVENTARIO].[SP_BUSCAR_PRODUCTO_CCF] (compartido con
            // frmCreditoFiscal; no [EINVENTARIO].[SP_PRODUCTO], que siguen usando
            // frmProducto, frmDocumentoCompra, etc. sin este filtro).
            var config = new BusquedaConfig
            {
                StoredProcedure = "[EINVENTARIO].[SP_BUSCAR_PRODUCTO_CCF]",
                Accion = "BUSCAR",
                Columnas = new Dictionary<string, string>
                {
                    { "COD_REF",     "CÓDIGO"      },
                    { "DESCRIPCION", "DESCRIPCIÓN" },
                    { "UNIMEDIDA",   "U/M"         },
                    { "PRECIO",      "PRECIO"      }
                },
                Anchos = new Dictionary<string, int>
                {
                    { "COD_REF",     100 },
                    { "DESCRIPCION", 400 },
                    { "UNIMEDIDA",   80  },
                    { "PRECIO",      100 }
                },
                ParametrosExtra = new
                {
                    ID_ROL_USUARIO = Configuracion.IdRolActual
                }
            };
            using (var frm = new frmBusquedaGenerica(config))
            {
                Point posGrid = gridControl1.PointToScreen(new Point(0, gridControl1.Height));
                Rectangle pant = Screen.FromControl(gridControl1).WorkingArea;
                int posX = posGrid.X;
                int posY = posGrid.Y;
                if (posY + frm.Height > pant.Bottom) posY = posGrid.Y - frm.Height;
                if (posX + frm.Width > pant.Right) posX = pant.Right - frm.Width;
                frm.StartPosition = FormStartPosition.Manual;
                frm.Location = new Point(posX, posY);
                if (frm.ShowDialog() == DialogResult.OK && frm.FilaSeleccionada != null)
                {
                    AsignarProductoAFila(view, frm.FilaSeleccionada);
                    view.FocusedColumn = view.Columns["CANTIDAD"];
                    view.ShowEditor();
                }
                else
                    view.SetFocusedRowCellValue("COD_REF", string.Empty);
            }
        }
        private void AsignarProductoAFila(GridView view, DataRow fila)
        {
            int rowHandle = view.FocusedRowHandle;
            if (rowHandle < 0 || rowHandle >= _dtDetalle.Rows.Count) return;
            bool esExento = Convert.ToBoolean(fila["ES_EXENTO"]);
            bool esNosujeta = Convert.ToBoolean(fila["ES_NOSUJETA"]);
            view.SetRowCellValue(rowHandle, "COD_REF", fila["COD_REF"].ToString());
            view.SetRowCellValue(rowHandle, "DESCRIPCION", fila["DESCRIPCION"].ToString());
            view.SetRowCellValue(rowHandle, "UM", fila["UNIMEDIDA"].ToString());
            view.SetRowCellValue(rowHandle, "PRECIO", Convert.ToDecimal(fila["PRECIO"]));
            view.SetRowCellValue(rowHandle, "ES_EXENTO", esExento);
            view.SetRowCellValue(rowHandle, "ES_NOSUJETA", esNosujeta);
            _dtDetalle.Rows[rowHandle]["ID_PRODUCTO"] = Convert.ToInt32(fila["ID_PRODUCTO"]);
            _dtDetalle.Rows[rowHandle]["ES_EXENTO"] = esExento;
            _dtDetalle.Rows[rowHandle]["ES_NOSUJETA"] = esNosujeta;
            _dtDetalle.Rows[rowHandle]["ID_UNIDAD_MEDIDA"] = Convert.ToInt32(fila["ID_UNIDAD_MEDIDA"]);
            RecalcularLinea(view, rowHandle);
        }
        #endregion
        #region SOLICITUD AGRÍCOLA
        private void btn_solicitudAgricola_Click(object sender, EventArgs e)
        {
            if (_idEntidad <= 0)
            {
                XtraMessageBox.Show(
                    "Seleccione primero un cliente antes de consultar las solicitudes.",
                    "Solicitud agrícola",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtCLIENTE.Focus();
                return;
            }

            int? idZafra = ObtenerIdCombo(cbxZAFRA);
            if (!idZafra.HasValue || idZafra.Value <= 0)
            {
                XtraMessageBox.Show(
                    "Seleccione la zafra antes de consultar las solicitudes.",
                    "Solicitud agrícola",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                cbxZAFRA.Focus();
                return;
            }

            using (var frm = new frmConsultaSolicitudAgricola(
                _codiProveedorCliente,
                _codigoTransportistaCliente,
                _codigoFrenteRozaCliente,
                _codigoFrenteQuerqueoCliente,
                idZafra.Value,
                cbxZAFRA.Text))
            {
                if (frm.ShowDialog(this) != DialogResult.OK ||
                    frm.SolicitudSeleccionada == null)
                    return;

                try
                {
                    Cursor = Cursors.WaitCursor;

                    int idSolicitud = Convert.ToInt32(
                        frm.SolicitudSeleccionada["ID_SOLICITUD"]);

                    CargarDetalleSolicitudAgricola(frm.DetalleSeleccionado);
                    _idSolicitudSeleccionada = idSolicitud;
                    AsignarDatosHistorialSolicitud(frm.SolicitudSeleccionada);
                    AplicarCentroCostoPorSolicitud(true);   // 2026-10-05 centro fijo
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(
                        "No fue posible cargar los productos de la solicitud:\n\n" + ex.Message,
                        "Solicitud agrícola",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }
        private void CargarDetalleSolicitudAgricola(DataTable detalleSolicitud)
        {
            if (detalleSolicitud == null || detalleSolicitud.Rows.Count == 0)
                throw new InvalidOperationException(
                    "La solicitud seleccionada no contiene productos para facturar.");

            DataTable detallePreparado = _dtDetalle.Clone();

            foreach (DataRow productoSolicitud in detalleSolicitud.Rows)
            {
                string codigoLocal = Convert.ToString(
                    productoSolicitud["COD_REF"]).Trim();

                if (string.IsNullOrWhiteSpace(codigoLocal))
                    throw new InvalidOperationException(
                        "La solicitud contiene un producto que todavía no está relacionado.");

                DataTable productoParaGrid = _dal.EjecutarConsulta(
                    "[EINVENTARIO].[SP_PRODUCTO]",
                    new
                    {
                        ACCION = "BUSCAR_PRODUCTO_COD_REF",
                        COD_REF = codigoLocal
                    });

                DataRow datosProductoGrid = productoParaGrid?.AsEnumerable()
                    .FirstOrDefault();

                if (datosProductoGrid == null)
                    throw new InvalidOperationException(
                        $"No se encontró el producto local con código '{codigoLocal}'.");

                decimal cantidad = productoSolicitud["CANTIDAD"] == DBNull.Value
                    ? 0m
                    : Convert.ToDecimal(productoSolicitud["CANTIDAD"]);
                decimal precio = datosProductoGrid["PRECIO"] == DBNull.Value
                    ? 0m
                    : Convert.ToDecimal(datosProductoGrid["PRECIO"]);

                if (cantidad <= 0)
                    throw new InvalidOperationException(
                        $"El producto '{codigoLocal}' tiene una cantidad inválida.");

                bool esExento = datosProductoGrid["ES_EXENTO"] != DBNull.Value &&
                    Convert.ToBoolean(datosProductoGrid["ES_EXENTO"]);
                decimal total = cantidad * precio;

                DataRow filaDetalle = detallePreparado.NewRow();
                filaDetalle["ID_PRODUCTO"] = Convert.ToInt32(
                    datosProductoGrid["ID_PRODUCTO"]);
                filaDetalle["COD_REF"] = Convert.ToString(
                    datosProductoGrid["COD_REF"]).Trim();
                filaDetalle["DESCRIPCION"] = Convert.ToString(
                    datosProductoGrid["NOMBRE_PRODUCTO"]);
                filaDetalle["UM"] = Convert.ToString(
                    datosProductoGrid["UNIMEDIDA"]);
                filaDetalle["PORC_DESC"] = 0m;
                filaDetalle["CANTIDAD"] = cantidad;
                filaDetalle["PRECIO"] = precio;
                filaDetalle["ES_EXENTO"] = esExento;
                // frmFactura maneja también "No Sujeta" (columna que CCF no tiene); la solicitud
                // agrícola no trae ese dato, así que por defecto entra como no-exenta/no-nosujeta.
                filaDetalle["ES_NOSUJETA"] = false;
                filaDetalle["DESCUENTO"] = 0m;
                filaDetalle["NOSUJETA"] = 0m;
                filaDetalle["EXENTO"] = esExento ? total : 0m;
                filaDetalle["GRAVADO"] = esExento ? 0m : total;
                filaDetalle["TOTAL"] = total;
                filaDetalle["ID_UNIDAD_MEDIDA"] = Convert.ToInt32(
                    datosProductoGrid["ID_UNIDAD_MEDIDA"]);
                // Historial de integración SIGESTA: trazabilidad hacia la línea de la
                // solicitud agrícola de origen (ACTION='PRODUCTOR_DETALLE').
                filaDetalle["ID_SOLICITUD"] = productoSolicitud.Table.Columns.Contains("ID_SOLICITUD") &&
                                               productoSolicitud["ID_SOLICITUD"] != DBNull.Value
                    ? (object)Convert.ToInt32(productoSolicitud["ID_SOLICITUD"]) : DBNull.Value;
                filaDetalle["ID_SOLIC_DETA"] = productoSolicitud.Table.Columns.Contains("ID_SOLIC_DETA") &&
                                                     productoSolicitud["ID_SOLIC_DETA"] != DBNull.Value
                    ? (object)Convert.ToInt32(productoSolicitud["ID_SOLIC_DETA"]) : DBNull.Value;
                filaDetalle["UID_SOLIC_DETA"] = productoSolicitud.Table.Columns.Contains("UID_SOLIC_DETA") &&
                                                      productoSolicitud["UID_SOLIC_DETA"] != DBNull.Value
                    ? (object)productoSolicitud["UID_SOLIC_DETA"].ToString() : DBNull.Value;
                detallePreparado.Rows.Add(filaDetalle);
            }

            _dtDetalle.Clear();
            foreach (DataRow filaPreparada in detallePreparado.Rows)
                _dtDetalle.ImportRow(filaPreparada);

            gridControl1.RefreshDataSource();
            ActualizarTotales();
        }
        #endregion
        #region ELIMINAR FILA
        private void EliminarFilaDetalle()
        {
            var view = gridControl1.MainView as GridView;
            if (view == null) return;
            int fila = view.FocusedRowHandle;
            if (fila < 0) return;
            bool esFilaVacia = string.IsNullOrWhiteSpace(
                view.GetRowCellValue(fila, "COD_REF")?.ToString());
            int filasConProducto = _dtDetalle.Rows.Cast<DataRow>()
                .Count(r => !string.IsNullOrWhiteSpace(r["COD_REF"].ToString()));
            if (!esFilaVacia && filasConProducto <= 1)
            {
                XtraMessageBox.Show("Debe haber al menos una línea en el detalle.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("¿Desea eliminar esta fila?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            if (IdFactEnc > 0 && !esFilaVacia)
            {
                object val = view.GetRowCellValue(fila, "ID_PRODUCTO");
                if (val != null && val != DBNull.Value)
                {
                    int idProducto = Convert.ToInt32(val);
                    if (idProducto > 0)
                    {
                        _dal.EjecutarSinRetorno("[EDTE].[SP_FACTURA_DET]", new
                        {
                            ACCION = "ELIMINAR",
                            ID_FACTENC = IdFactEnc,
                            ID_PRODUCTO = idProducto,
                            ID_EMISOR = 1
                        });
                    }
                }
            }
            view.DeleteRow(fila);
            ActualizarTotales();
        }
        #endregion
        #region GUARDAR
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;
            if (!ValidarFormaPagoContado()) return;   // 2026-10-01
            try
            {
                var view = gridControl1.MainView as GridView;
                view?.CloseEditor();
                view?.UpdateCurrentRow();
                // (2026-08-26) Igual que frmCreditoFiscal: para disparar GUARDAR_DESDE_FACTURA
                // en SP_CREDITO_ENCA solo la primera vez que se guarda esta factura (INSERT),
                // no en cada UPDATE/re-guardado.
                bool esNuevaFactura = IdFactEnc == 0;
                string NUMDOC = NullIfEmpty(txtNUMINTERNO.Text);
                var dtVenta = _dal.EjecutarConsulta("[EDTE].[SP_FACTURA_ENC]", new
                {
                    ACCION = "GUARDAR",
                    ID_FACTENC = IdFactEnc,
                    ID_EMISOR = 1,
                    ID_SUCURSAL = ObtenerIdCombo(cbxSUCURSAL),
                    ID_ALMACEN = Configuracion.Id_Almacen,
                    ID_CAJERO = Configuracion.Id_Cajero,
                    ID_CAJA = Configuracion.Id_Cajero,
                    ID_CONDPAGO = ObtenerIdCombo(cbxCONDPAGO),
                    ID_CLIENTE = _idEntidad,
                    COD_REF = _codigoEntidad,
                    ID_TIPO_DTE = ObtenerIdCombo(cbxTIPO_DTE),
                    TPDOC = NullIfEmpty(cbxTIPO_DTE.Text),
                    FECHA = ParsearFecha(mskFECHA.Text),
                    SALFEC = FormHelper.ObtenerSalfec(mskFECHA),
                    NUMDOC = NUMDOC,
                    NUMINTERNO = FormatearNumInterno(txtNUMINTERNO.Text),
                    CODGENERACION = txtCOD_GENERACION.Text.Trim(),
                    NUMCONTROL = txtNUM_CONTROL.Text.Trim(),
                    SELLORECEPCION = txtSELLO_RECIBIDO.Text.Trim(),
                    FECHA_VENCE = ParsearFechaOpcional(mskFECHA_VENCE.Text),
                    DIAS_CREDITO = _diasCredito,
                    RECIB_EFECTIVO = ObtenerTextBoxDecimal(txtRECIB_EFECTIVO),
                    RECIB_REMESA = ObtenerTextBoxDecimal(txtRECIB_REMESA),
                    RECIB_CHEQUE = ObtenerTextBoxDecimal(txtRECIB_CHEQUE),
                    RECIB_NOTAABONO = ObtenerTextBoxDecimal(txtRECIB_NOTAABONO),
                    RECIB_ANTICIPO = ObtenerTextBoxDecimal(txtRECIB_ANTICIPO),
                    RECIB_EFECTIVO_CAMBIO = ObtenerTextBoxDecimal(txtRECIB_EFECTIVO_CAMBIO),
                    // NUEVOS
                    RECIB_REMESA_BANCO = NullIfEmpty(txtRECIB_REMESA_BANCO.Text),
                    RECIB_REMESA_CUENTA = NullIfEmpty(txtRECIB_REMESA_CUENTA.Text),
                    RECIB_REMESA_MONTO = ObtenerTextBoxDecimal(txtRECIB_REMESA_MONTO),
                    RECIB_CHEQUE_BANCO = NullIfEmpty(txtRECIB_CHEQUE_BANCO.Text),
                    RECIB_CHEQUE_CUENTA = NullIfEmpty(txtRECIB_CHEQUE_CUENTA.Text),
                    RECIB_CHEQUE_MONTO = ObtenerTextBoxDecimal(txtRECIB_CHEQUE_MONTO),
                    RECIB_NOTAABONO_BANCO = NullIfEmpty(txtRECIB_NOTAABONO_BANCO.Text),
                    RECIB_NOTAABONO_CUENTA = NullIfEmpty(txtRECIB_NOTAABONO_CUENTA.Text),
                    RECIB_NOTAABONO_MONTO = ObtenerTextBoxDecimal(txtRECIB_NOTAABONO_MONTO),
                    AFECTA = ObtenerTextBoxDecimal(txtVENTA_GRAVADA),
                    EXCENTA = ObtenerTextBoxDecimal(txtVENTA_EXENTA),
                    DESCUENTO = ObtenerTextBoxDecimal(txtPORC_DESCUENTO),
                    DESCUENTO_VALOR = ObtenerTextBoxDecimal(txtDESCUENTO),
                    SUBTOTAL = ObtenerTextBoxDecimal(txtSUBTOTAL),
                    IVA = ObtenerTextBoxDecimal(txtIVA),
                    IVARETENIDO = ObtenerTextBoxDecimal(txtRETENCION),
                    IVAPERCIBIDO = ObtenerTextBoxDecimal(txtPERCEPCION),
                    TOTALVENTA = ObtenerTextBoxDecimal(txtTOTAL_VENTA),
                    AP_PERCEPCION = chkPERCEPCION.Checked,
                    OBSERVACIONES = txtOBSERVACION.Text.Trim(),
                    TPCONTRIBUYENTE = _idTipoContribCliente,
                    ID_ESTADO = 1,
                    ID_ZAFRA = ObtenerIdCombo(cbxZAFRA),
                    ID_CENTRO = ObtenerIdCombo(cbxCENTRO_COSTO),
                    ID_TIPO_ENTIDAD = _idTipoPersona,
                    USUARIO = Configuracion.UsuarioActual,
                    // Historial de integración SIGESTA (solicitud agrícola importada) - 2026-08-25
                    ID_SOLICITUD = _idSolicitudSeleccionada,
                    NUM_SOLICITUD = NullIfEmpty(txtNUM_SOLICITUD.Text),
                    NOMBRE_CUENTA = NullIfEmpty(txtNOMBRE_CUENTA.Text),
                    UID_SOLICITUD = NullIfEmpty(txtREFERENCIA.Text),
                    // (2026-08-26) NUEVO: se guardan directo en la factura para que
                    // [ESOLICITUD].[SP_CREDITO_ENCA] los lea sin hacer joins.
                    ID_CUENTA_FINAN = _idCuentaFinanSolicitud,
                    TIPO_SOLICITUD = NullIfEmpty(_tipoSolicitudSeleccionada),
                    ID_TIPO_SOLICITUD = _idTipoSolicitudSeleccionada,
                    NOMBRE_ZAFRA = NullIfEmpty(_nombreZafraSolicitud),
                    CODIGO = NullIfEmpty(_codigoSolicitudSeleccionada),
                    NOMBRE_PROVEEDOR = NullIfEmpty(_nombreProveedorSolicitud),
                });
                if (dtVenta == null || dtVenta.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Error al guardar la factura.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                IdFactEnc = Convert.ToInt32(dtVenta.Rows[0]["ID_GENERADO"]);
                if (dtVenta.Columns.Contains("NCONT") && dtVenta.Rows[0]["NCONT"] != DBNull.Value)
                    txtNUM_CONTROL.Text = Convert.ToString(dtVenta.Rows[0]["NCONT"]);
                if (dtVenta.Columns.Contains("INTERN") && dtVenta.Rows[0]["INTERN"] != DBNull.Value)
                    txtNUMINTERNO.Text = FormatearNumInterno(Convert.ToString(dtVenta.Rows[0]["INTERN"]));
                _dal.EjecutarSinRetorno("[EDTE].[SP_FACTURA_DET]", new
                {
                    ACCION = "ELIMINAR_POR_FACTURA",
                    ID_FACTENC = IdFactEnc,
                    ID_EMISOR = 1
                });
                foreach (DataRow fila in _dtDetalle.Rows)
                {
                    if (string.IsNullOrWhiteSpace(fila["COD_REF"].ToString())) continue;
                    _dal.EjecutarSinRetorno("[EDTE].[SP_FACTURA_DET]", new
                    {
                        ACCION = "GUARDAR",
                        ID_FACTDET = 0,
                        ID_FACTENC = IdFactEnc,
                        ID_EMISOR = 1,
                        CODGENERACION = txtCOD_GENERACION.Text.Trim(),
                        ID_TIPO_DTE = ObtenerIdCombo(cbxTIPO_DTE),
                        TPDOC = NullIfEmpty(cbxTIPO_DTE.Text),
                        FECHA = ParsearFecha(mskFECHA.Text),
                        SALFEC = FormHelper.ObtenerSalfec(mskFECHA),
                        NUMDOC = txtNUMINTERNO.Text.Trim(),
                        ID_PRODUCTO = Convert.ToInt32(fila["ID_PRODUCTO"]),
                        COD_REF = fila["COD_REF"].ToString(),
                        DESCRIPCION = fila["DESCRIPCION"].ToString(),
                        CANTIDAD = Math.Round(Convert.ToDecimal(fila["CANTIDAD"]), 4),
                        ID_UNIDAD_MEDIDA = Convert.ToInt32(fila["ID_UNIDAD_MEDIDA"]),
                        UNIDAD_MEDIDA = fila["UM"].ToString(),
                        PRECIO = Math.Round(Convert.ToDecimal(fila["PRECIO"]), 6),
                        DESCUENTO = Convert.ToInt32(fila["PORC_DESC"]),
                        DESCUENTO_VALOR = Convert.ToDecimal(fila["DESCUENTO"]),
                        ES_EXENTO = Convert.ToBoolean(fila["ES_EXENTO"]),
                        EXENTA = Convert.ToDecimal(fila["EXENTO"]),
                        GRAVADA = Convert.ToDecimal(fila["GRAVADO"]),
                        TOTAL = Convert.ToDecimal(fila["TOTAL"]),
                        USUARIO = Configuracion.UsuarioActual,
                        // Historial de integración SIGESTA (solicitud agrícola importada) - 2026-08-25
                        ID_SOLICITUD = fila.Table.Columns.Contains("ID_SOLICITUD") && fila["ID_SOLICITUD"] != DBNull.Value
                            ? (int?)Convert.ToInt32(fila["ID_SOLICITUD"]) : null,
                        ID_SOLIC_DETA = fila.Table.Columns.Contains("ID_SOLIC_DETA") && fila["ID_SOLIC_DETA"] != DBNull.Value
                            ? (int?)Convert.ToInt32(fila["ID_SOLIC_DETA"]) : null,
                        UID_SOLIC_DETA = fila.Table.Columns.Contains("UID_SOLIC_DETA") && fila["UID_SOLIC_DETA"] != DBNull.Value
                            ? fila["UID_SOLIC_DETA"].ToString() : null,
                    });
                }

                _dal.EjecutarSinRetorno("[EDTE].[SP_FACTURA_JSON]", new
                {
                    ID_FACTENC = IdFactEnc
                });

                _dal.EjecutarSinRetorno("[EIVA].[SP_LBVENTAFA_FAE_INS]", new
                {
                    ID_FACTENC = IdFactEnc
                });

                // (2026-08-26) Igual que frmCreditoFiscal: si esta Factura viene de una
                // Solicitud Agrícola y es la primera vez que se guarda, se genera el registro
                // en [INJIBOA].[dbo].[CREDITO_ENCA] llamando explícitamente a
                // [ESOLICITUD].[SP_CREDITO_ENCA] ACCION='GUARDAR_DESDE_FACTURA'. NO es un
                // disparador de base de datos. Si falla, la Factura queda guardada igual.
                // (2026-10-02) Solicitud de TRANSPORTISTA -> CCF_ENCA_TRANS / CCF_DETA_TRANS
                // con ID_TIPO_COMPROB = 2 (Factura). Genera también CREDITO_ENCA_TRANS.
                if (esNuevaFactura && _idSolicitudSeleccionada.HasValue && EsSolicitudTransportista())
                {
                    try
                    {
                        GuardarSolicitudTransportista(2);   // ID_TIPO_COMPROB = 2 (Factura)
                    }
                    catch (Exception exTrans)
                    {
                        XtraMessageBox.Show(
                            "La factura se guardó correctamente, pero no se pudo generar " +
                            "el registro CCF_ENCA_TRANS/CCF_DETA_TRANS/CREDITO_ENCA_TRANS del transportista:\n\n" + exTrans.Message,
                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (esNuevaFactura && _idSolicitudSeleccionada.HasValue)
                {
                    try
                    {
                        _dal.EjecutarSinRetorno("[ESOLICITUD].[SP_CREDITO_ENCA]", new
                        {
                            ACCION = "GUARDAR_DESDE_FACTURA",
                            ID_FACTENC = IdFactEnc,
                            ID_EMISOR = 1,
                            USUARIO = Configuracion.UsuarioActual,
                        });
                    }
                    catch (Exception exCredito)
                    {
                        XtraMessageBox.Show(
                            "La factura se guardó correctamente, pero no se pudo generar " +
                            "el registro de crédito agrícola asociado:\n\n" + exCredito.Message,
                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                XtraMessageBox.Show("Factura guardada correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConfigurarCRUD(EstadoFormulario.Guardado);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error al guardar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // ---------------------------------------------------------------------------
        // (2026-10-02) Solicitud agrícola de TRANSPORTISTA (código = ENTIDAD.CODTRANSPORT):
        // se guarda en [INJIBOA].[dbo].[CCF_ENCA_TRANS] / [CCF_DETA_TRANS] mediante
        // [ESOLICITUD].[SP_CCF_ENCA_TRANS]. Cañeros (CODIPROVEEDOR) siguen igual.
        // ---------------------------------------------------------------------------
        // (2026-10-05) Centro de costo fijo según el tipo de solicitud agrícola:
        //   PRODUCTOR     -> ID_CENTRO 3 (VENTAS A PRODUCTORES DE CAÑA)
        //   TRANSPORTISTA -> ID_CENTRO 6 (VENTAS A TRANSPORTISTAS)
        // El combo queda bloqueado mientras el documento tenga solicitud de esos tipos.
        private const int CENTRO_PRODUCTORES = 3;
        private const int CENTRO_TRANSPORTISTAS = 6;
        private bool EsSolicitudProductor()
        {
            string tipo = (_tipoSolicitudSeleccionada ?? string.Empty).Trim().ToUpperInvariant();
            if (tipo.Contains("PRODUCTOR")) return true;
            if (tipo.Length > 0) return false;
            string codigo = (_codigoSolicitudSeleccionada ?? string.Empty).Trim();
            return codigo.Length > 0 && codigo == (_codiProveedorCliente ?? string.Empty).Trim();
        }
        private void AplicarCentroCostoPorSolicitud(bool asignar)
        {
            int? centroFijo = null;
            if (_idSolicitudSeleccionada.HasValue)
            {
                if (EsSolicitudTransportista()) centroFijo = CENTRO_TRANSPORTISTAS;
                else if (EsSolicitudProductor()) centroFijo = CENTRO_PRODUCTORES;
            }
            if (!centroFijo.HasValue)
            {
                cbxCENTRO_COSTO.Enabled = true;
                return;
            }
            if (asignar)
            {
                cbxCENTRO_COSTO.SelectedValue = centroFijo.Value;
                if (ObtenerIdCombo(cbxCENTRO_COSTO) != centroFijo.Value)
                    XtraMessageBox.Show(
                        $"El centro de costo {centroFijo.Value} no está disponible para su rol.\n\n" +
                        "Solicite que se lo asignen en Roles (centros de costo).",
                        "Centro de costo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            cbxCENTRO_COSTO.Enabled = false;
        }
        private bool EsSolicitudTransportista()
        {
            string tipo = (_tipoSolicitudSeleccionada ?? string.Empty).Trim().ToUpperInvariant();
            if (tipo.Contains("TRANSPORT")) return true;
            if (tipo.Length > 0) return false;   // PRODUCTOR, FRENTE ROZA, FRENTE QUERQUEO...
            string codigo = (_codigoSolicitudSeleccionada ?? string.Empty).Trim();
            string codTrans = (_codigoTransportistaCliente ?? string.Empty).Trim();
            string codProv = (_codiProveedorCliente ?? string.Empty).Trim();
            return codigo.Length > 0 && codigo == codTrans && codigo != codProv;
        }

        private void GuardarSolicitudTransportista(int idTipoComprob)
        {
            string codTransport = !string.IsNullOrWhiteSpace(_codigoSolicitudSeleccionada)
                ? _codigoSolicitudSeleccionada.Trim()
                : (_codigoTransportistaCliente ?? string.Empty).Trim();

            var dtEnca = _dal.EjecutarConsulta("[ESOLICITUD].[SP_CCF_ENCA_TRANS]", new
            {
                ACCION = "GUARDAR_ENCA",
                CODTRANSPORT = codTransport,
                ID_ZAFRA = ObtenerIdCombo(cbxZAFRA),
                ID_SOLICITUD = _idSolicitudSeleccionada,
                ID_CUENTA_FINAN = _idCuentaFinanSolicitud,
                ID_TIPO_COMPROB = idTipoComprob,           // 1 = CCF, 2 = Factura
                NO_CCF = txtNUMINTERNO.Text.Trim(),
                FECHA = ParsearFecha(mskFECHA.Text),
                SUB_TOTAL = ObtenerTextBoxDecimal(txtSUBTOTAL),
                DESCTO_MONTO = ObtenerTextBoxDecimal(txtDESCUENTO),
                IVA = ObtenerTextBoxDecimal(txtIVA),
                TOTAL = ObtenerTextBoxDecimal(txtTOTAL_VENTA),
                USUARIO = Configuracion.UsuarioActual,
            });
            if (dtEnca == null || dtEnca.Rows.Count == 0)
                throw new InvalidOperationException("SP_CCF_ENCA_TRANS no devolvió el ID generado.");

            int idCcfTrans = Convert.ToInt32(dtEnca.Rows[0]["ID_GENERADO"]);
            foreach (DataRow fila in _dtDetalle.Rows)
            {
                if (string.IsNullOrWhiteSpace(fila["COD_REF"].ToString())) continue;
                _dal.EjecutarSinRetorno("[ESOLICITUD].[SP_CCF_ENCA_TRANS]", new
                {
                    ACCION = "GUARDAR_DETA",
                    ID_CCF_TRANS = idCcfTrans,
                    COD_REF = fila["COD_REF"].ToString(),   // el SP resuelve el ID_PRODUCTO de SIGESTA
                    NOMBRE_PRODUCTO = fila["DESCRIPCION"].ToString(),
                    CANTIDAD = Math.Round(Convert.ToDecimal(fila["CANTIDAD"]), 4),
                    PRECIO_UNITARIO = Math.Round(Convert.ToDecimal(fila["PRECIO"]), 6),
                    TOTAL_LINEA = Convert.ToDecimal(fila["TOTAL"]),
                });
            }

            // Crédito agrícola del transportista en [INJIBOA].[dbo].[CREDITO_ENCA_TRANS]
            // (igual que GUARDAR_DESDE_CCF para cañeros), leído de CCF_ENCA_TRANS por UID_CCF.
            Guid uidCcf = (Guid)dtEnca.Rows[0]["UID_CCF"];
            _dal.EjecutarSinRetorno("[ESOLICITUD].[SP_CCF_ENCA_TRANS]", new
            {
                ACCION = "GUARDAR_CREDITO",
                UID_CCF = uidCcf,
                USUARIO = Configuracion.UsuarioActual,
            });
        }

        private bool ValidarCampos()
        {
            if (!FormHelper.ValidarFecha(mskFECHA, "Fecha")) return false;
            if (cbxTIPO_DTE.SelectedValue == null)
            {
                XtraMessageBox.Show("Seleccione el tipo de DTE.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbxSUCURSAL.SelectedValue == null)
            {
                XtraMessageBox.Show("Seleccione la sucursal.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            bool tieneLineas = _dtDetalle.Rows.Cast<DataRow>()
                .Any(f => !string.IsNullOrWhiteSpace(f["COD_REF"].ToString()));
            if (!tieneLineas)
            {
                XtraMessageBox.Show("Debe ingresar al menos un producto en el detalle.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // (2026-10-01) Si el detalle viene de una solicitud agrícola, todas las líneas
            // deben ser de la solicitud.
            if (DetalleDesdeSolicitud())
            {
                DataRow ajena = _dtDetalle.Rows.Cast<DataRow>().FirstOrDefault(r =>
                    !string.IsNullOrWhiteSpace(r["COD_REF"].ToString()) &&
                    r["ID_SOLIC_DETA"] == DBNull.Value &&
                    (r["UID_SOLIC_DETA"] == DBNull.Value || string.IsNullOrWhiteSpace(r["UID_SOLIC_DETA"].ToString())));
                if (ajena != null)
                {
                    XtraMessageBox.Show($"El producto '{ajena["DESCRIPCION"]}' no pertenece a la solicitud agrícola.\n\nSolo se permiten los ítems de la solicitud.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            foreach (DataRow fila in _dtDetalle.Rows)
            {
                if (string.IsNullOrWhiteSpace(fila["COD_REF"].ToString())) continue;
                if (Convert.ToDecimal(fila["CANTIDAD"]) <= 0)
                {
                    XtraMessageBox.Show($"El producto '{fila["DESCRIPCION"]}' tiene cantidad 0.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
        #endregion
        #region FORMA DE PAGO (CONTADO) - 2026-10-01
        // Igual que frmCreditoFiscal: CONTADO = ID_CONDPAGO 1
        private bool EsCondicionContado()
        {
            return ObtenerIdCombo(cbxCONDPAGO) == 1;
        }

        // Suma de remesa + cheque + nota de abono + anticipo (lo que NO es efectivo).
        private decimal OtrosPagosContado()
        {
            return ObtenerTextBoxDecimal(txtRECIB_REMESA)
                 + ObtenerTextBoxDecimal(txtRECIB_CHEQUE)
                 + ObtenerTextBoxDecimal(txtRECIB_NOTAABONO)
                 + ObtenerTextBoxDecimal(txtRECIB_ANTICIPO);
        }

        // No pasarse del monto maximo: remesa + cheque + nota de abono + anticipo no pueden
        // sumar mas que el total de la venta (solo el EFECTIVO puede exceder y genera vuelto).
        private void LimitarPagoNoEfectivo(TextBox txt)
        {
            if (!EsCondicionContado()) return;
            decimal total = ObtenerTextBoxDecimal(txtTOTAL_VENTA);
            decimal exceso = OtrosPagosContado() - total;
            if (exceso <= 0) return;
            decimal valor = ObtenerTextBoxDecimal(txt);
            decimal permitido = Math.Max(0m, valor - exceso);
            txt.Text = permitido.ToString("N2");
            XtraMessageBox.Show(
                $"Remesa + cheque + nota de abono + anticipo no pueden pasar del total de la venta ({total:N2}).\n" +
                $"El monto se ajustó al máximo permitido: {permitido:N2}.",
                "Forma de pago", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Al salir de Efectivo: si el cliente entrega mas de lo pendiente, se calcula el vuelto.
        private void txtRECIB_EFECTIVO_Leave_FormaPago(object sender, EventArgs e)
        {
            if (!EsCondicionContado()) return;
            decimal total = ObtenerTextBoxDecimal(txtTOTAL_VENTA);
            decimal efectivo = ObtenerTextBoxDecimal(txtRECIB_EFECTIVO);
            decimal vuelto = Math.Max(0m, efectivo + OtrosPagosContado() - total);
            txtRECIB_EFECTIVO.Text = efectivo.ToString("N2");
            txtRECIB_EFECTIVO_CAMBIO.Text = vuelto.ToString("N2");
        }

        private bool ValidarDatosMovimiento(string forma, decimal monto, TextBox txtBanco, TextBox txtCuenta)
        {
            if (monto <= 0) return true;
            if (string.IsNullOrWhiteSpace(txtBanco.Text))
            {
                XtraMessageBox.Show($"{forma}: ingrese el Banco (monto {monto:N2}).",
                    "Forma de pago", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBanco.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCuenta.Text))
            {
                XtraMessageBox.Show($"{forma}: ingrese el N° de Cuenta (monto {monto:N2}).",
                    "Forma de pago", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCuenta.Focus();
                return false;
            }
            return true;
        }

        // Validacion al guardar: en CONTADO la forma de pago debe cuadrar con el total.
        //   Efectivo + Remesa + Cheque + Nota de abono + Anticipo - Vuelto = Total venta
        private bool ValidarFormaPagoContado()
        {
            if (!EsCondicionContado()) return true;

            decimal total    = ObtenerTextBoxDecimal(txtTOTAL_VENTA);
            decimal efectivo = ObtenerTextBoxDecimal(txtRECIB_EFECTIVO);
            decimal remesa   = ObtenerTextBoxDecimal(txtRECIB_REMESA);
            decimal cheque   = ObtenerTextBoxDecimal(txtRECIB_CHEQUE);
            decimal nota     = ObtenerTextBoxDecimal(txtRECIB_NOTAABONO);
            decimal vuelto   = ObtenerTextBoxDecimal(txtRECIB_EFECTIVO_CAMBIO);
            decimal pagado   = efectivo + OtrosPagosContado() - vuelto;

            if (efectivo < 0 || remesa < 0 || cheque < 0 || nota < 0 || vuelto < 0)
            {
                XtraMessageBox.Show("La forma de pago no puede tener montos negativos.",
                    "Forma de pago", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (OtrosPagosContado() > total)
            {
                XtraMessageBox.Show($"Remesa + cheque + nota de abono + anticipo ({OtrosPagosContado():N2}) " +
                    $"no pueden pasar del total de la venta ({total:N2}).",
                    "Forma de pago", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (vuelto > efectivo)
            {
                XtraMessageBox.Show("El vuelto no puede ser mayor que el efectivo recibido.",
                    "Forma de pago", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (Math.Abs(pagado - total) > 0.005m)
            {
                XtraMessageBox.Show(
                    "La forma de pago no cuadra con el total de la venta (condición CONTADO).\n\n" +
                    $"Efectivo:        {efectivo,12:N2}\n" +
                    $"Remesa:          {remesa,12:N2}\n" +
                    $"Cheque:          {cheque,12:N2}\n" +
                    $"Nota de abono:   {nota,12:N2}\n" +
                    $"Anticipo:        {ObtenerTextBoxDecimal(txtRECIB_ANTICIPO),12:N2}\n" +
                    $"(-) Vuelto:      {vuelto,12:N2}\n" +
                    $"Total pagado:    {pagado,12:N2}\n" +
                    $"Total venta:     {total,12:N2}\n" +
                    $"Diferencia:      {total - pagado,12:N2}",
                    "Forma de pago", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Si hay monto en remesa / cheque / nota de abono, los datos del movimiento son obligatorios
            if (!ValidarDatosMovimiento("Remesa", remesa, txtRECIB_REMESA_BANCO, txtRECIB_REMESA_CUENTA)) return false;
            if (!ValidarDatosMovimiento("Cheque", cheque, txtRECIB_CHEQUE_BANCO, txtRECIB_CHEQUE_CUENTA)) return false;
            if (!ValidarDatosMovimiento("Nota de abono", nota, txtRECIB_NOTAABONO_BANCO, txtRECIB_NOTAABONO_CUENTA)) return false;

            // El monto de cada bloque (remesa / cheque / nota de abono) debe ser el mismo de arriba
            if (remesa != ObtenerTextBoxDecimal(txtRECIB_REMESA_MONTO))
                txtRECIB_REMESA_MONTO.Text = remesa.ToString("N2");
            if (cheque != ObtenerTextBoxDecimal(txtRECIB_CHEQUE_MONTO))
                txtRECIB_CHEQUE_MONTO.Text = cheque.ToString("N2");
            if (nota != ObtenerTextBoxDecimal(txtRECIB_NOTAABONO_MONTO))
                txtRECIB_NOTAABONO_MONTO.Text = nota.ToString("N2");
            return true;
        }
        #endregion
        #region LIMPIAR
        private void LimpiarFormulario()
        {
            _idEntidad = 0;
            _codigoEntidad = string.Empty;
            IdFactEnc = 0;
            txtCLIENTE.Text = "";
            txtNOMBRE_CLIENTE.Text = "";
            txtNIT.Text = "";
            txtDUI.Text = "";
            txtTELEFONO.Text = "";
            txtCORREO.Text = "";
            txtACTIVIDAD_PRIMARIA.Text = "";
            txtDIRECCION.Text = "";
            txtTIPO_CONTRIBUYENTE.Text = "";
            txtCOD_GENERACION.Text = "";
            txtSELLO_RECIBIDO.Text = "";
            txtOBSERVACION.Text = "";
            txtNUMINTERNO.Text = "";
            txtNUM_CONTROL.Text = "";
            mskFECHA.Text = "";
            mskFECHA_VENCE.Text = "";
            chkPERCEPCION.Checked = false;
            txtRECIB_EFECTIVO.Text = "0.00";
            txtRECIB_REMESA.Text = "0.00";
            txtRECIB_CHEQUE.Text = "0.00";
            txtRECIB_NOTAABONO.Text = "0.00";
            txtRECIB_ANTICIPO.Text = "0.00";
            txtRECIB_EFECTIVO_CAMBIO.Text = "0.00";
            // NUEVOS
            txtRECIB_REMESA_BANCO.Text = "";
            txtRECIB_REMESA_CUENTA.Text = "";
            txtRECIB_REMESA_MONTO.Text = "0.00";
            txtRECIB_CHEQUE_BANCO.Text = "";
            txtRECIB_CHEQUE_CUENTA.Text = "";
            txtRECIB_CHEQUE_MONTO.Text = "0.00";
            txtRECIB_NOTAABONO_BANCO.Text = "";
            txtRECIB_NOTAABONO_CUENTA.Text = "";
            txtRECIB_NOTAABONO_MONTO.Text = "0.00";
            txtVENTA_NOSUJETA.Text = "0.00";
            txtVENTA_EXENTA.Text = "0.00";
            txtVENTA_GRAVADA.Text = "0.00";
            txtPORC_DESCUENTO.Text = "0.00";
            txtDESCUENTO.Text = "0.00";
            txtIVA.Text = "0.00";
            txtSUBTOTAL.Text = "0.00";
            txtRETENCION.Text = "0.00";
            txtPERCEPCION.Text = "0.00";
            txtTOTAL_VENTA.Text = "0.00";
            _dtDetalle.Clear();
            AgregarFilaVacia();
            ActualizarTotales();
            CargarTipoDte();
            if (cbxTIPO_DTE.Items.Count > 0) cbxTIPO_DTE.SelectedIndex = 0;
            CargarCondicionPago();
            if (cbxCONDPAGO.Items.Count > 0) cbxCONDPAGO.SelectedIndex = 0;
            CargarZafra();
            if (cbxZAFRA.Items.Count > 0) cbxZAFRA.SelectedIndex = 0;
            CargarCentroCosto();
            if (cbxCENTRO_COSTO.Items.Count > 0) cbxCENTRO_COSTO.SelectedIndex = 0;
            CargarSucursal();
            if (cbxSUCURSAL.Items.Count > 0) cbxSUCURSAL.SelectedIndex = 0;
            CargarVendedor();
            if (cbxVENDEDOR.Items.Count > 0) cbxVENDEDOR.SelectedIndex = 0;
            mskFECHA.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtCOD_GENERACION.Text = DALBase.NuevoGUID();
            CargarSiguienteNumFactura(NullIfEmpty(cbxTIPO_DTE.Text), AnioDte);
            // Historial de integración SIGESTA (solicitud agrícola importada) - 2026-08-25
            _idSolicitudSeleccionada = null;
            AsignarDatosHistorialSolicitud(null);
            _codiProveedorCliente = string.Empty;
            _codigoTransportistaCliente = string.Empty;
            _codigoFrenteRozaCliente = string.Empty;
            _codigoFrenteQuerqueoCliente = string.Empty;
        }
        #endregion
        #region HELPERS
        private void AsignarDecimal(TextBox tb, decimal valor)
            => tb.Text = valor.ToString("N2");
        private static DateTime ParsearFecha(string texto)
            => DateTime.ParseExact(texto, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        private static DateTime? ParsearFechaOpcional(string texto)
        {
            if (DateTime.TryParseExact(texto, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime f))
                return f;
            return null;
        }
        private static string NullIfEmpty(string texto)
            => string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
        // (2026-10-05) N° Interno siempre a 15 dígitos (el SP devolvía INTERN sin ceros).
        private static string FormatearNumInterno(string valor)
        {
            string v = (valor ?? string.Empty).Trim();
            return v.Length > 0 && v.Length < 15 && v.All(char.IsDigit) ? v.PadLeft(15, '0') : v;
        }
        private static string AsString(object valor)
            => valor == null || valor == DBNull.Value ? "" : valor.ToString();
        private static string AsFecha(object valor)
        {
            if (valor == null || valor == DBNull.Value) return "";
            return Convert.ToDateTime(valor).ToString("dd/MM/yyyy");
        }
        private static decimal ToDecimal(object valor)
            => valor == null || valor == DBNull.Value ? 0 : Convert.ToDecimal(valor);
        private int? ObtenerIdCombo(System.Windows.Forms.ComboBox cbx)
        {
            if (cbx.SelectedValue == null || cbx.SelectedValue == DBNull.Value) return null;
            if (cbx.SelectedValue is DataRowView) return null;
            return Convert.ToInt32(cbx.SelectedValue);
        }
        public static string ObtenerSalfec(MaskedTextBox campo)
        {
            if (string.IsNullOrWhiteSpace(campo.Text.Replace("/", "").Trim()))
                return null;
            if (DateTime.TryParseExact(campo.Text, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime fecha))
                return fecha.ToString("yyyyMM");
            return null;
        }
        #endregion
        #region BOTONES
        private void btnEliminar_Click(object sender, EventArgs e) => EliminarFilaDetalle();
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            ConfigurarCRUD(EstadoFormulario.Nuevo);
        }
        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
        private void btnFinalizar_Click(object sender, EventArgs e) => this.Close();
        #endregion
        #region FECHAS Y CONDICIÓN DE PAGO
        private void cbxCONDPAGO_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcularDiasVenceDefault();
            AplicarEstadoCobroPorCondicion();   // 2026-10-01 (igual que frmCreditoFiscal)
            ActualizarEstadoBotonSolicitudAgricola();   // 2026-10-01 solo en CRÉDITO

            if (_dtDetalle != null)
                ActualizarTotales();
        }

        // Forma de pago solo habilitada en CONTADO; en CREDITO se limpia (igual que frmCreditoFiscal)
        private void AplicarEstadoCobroPorCondicion()
        {
            bool habilitarCobro = EsCondicionContado();
            Control[] camposCobro =
            {
                txtRECIB_EFECTIVO, txtRECIB_REMESA, txtRECIB_CHEQUE, txtRECIB_NOTAABONO,
                txtRECIB_ANTICIPO, txtRECIB_EFECTIVO_CAMBIO,
                txtRECIB_REMESA_BANCO, txtRECIB_REMESA_CUENTA, txtRECIB_REMESA_MONTO,
                txtRECIB_CHEQUE_BANCO, txtRECIB_CHEQUE_CUENTA, txtRECIB_CHEQUE_MONTO,
                txtRECIB_NOTAABONO_BANCO, txtRECIB_NOTAABONO_CUENTA, txtRECIB_NOTAABONO_MONTO
            };
            foreach (Control campo in camposCobro)
                campo.Enabled = habilitarCobro;

            if (!habilitarCobro)
                LimpiarCamposCobro();
        }

        private void LimpiarCamposCobro()
        {
            txtRECIB_EFECTIVO.Text = "0.00";
            txtRECIB_REMESA.Text = "0.00";
            txtRECIB_CHEQUE.Text = "0.00";
            txtRECIB_NOTAABONO.Text = "0.00";
            txtRECIB_ANTICIPO.Text = "0.00";
            txtRECIB_EFECTIVO_CAMBIO.Text = "0.00";
            txtRECIB_REMESA_BANCO.Text = string.Empty;
            txtRECIB_REMESA_CUENTA.Text = string.Empty;
            txtRECIB_REMESA_MONTO.Text = "0.00";
            txtRECIB_CHEQUE_BANCO.Text = string.Empty;
            txtRECIB_CHEQUE_CUENTA.Text = string.Empty;
            txtRECIB_CHEQUE_MONTO.Text = "0.00";
            txtRECIB_NOTAABONO_BANCO.Text = string.Empty;
            txtRECIB_NOTAABONO_CUENTA.Text = string.Empty;
            txtRECIB_NOTAABONO_MONTO.Text = "0.00";
        }
        private void CalcularDiasVenceDefault()
        {
            int? idCondPago = ObtenerIdCombo(cbxCONDPAGO);
            if (idCondPago == 1)
            {
                _diasCredito = null;
                mskFECHA_VENCE.Text = string.Empty;
                mskFECHA_VENCE.Enabled = false;
                return;
            }
            mskFECHA_VENCE.Enabled = true;
            bool tieneFechaDoc = DateTime.TryParseExact(
                mskFECHA.Text.Trim(), "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime fechaDoc);
            if (!tieneFechaDoc) return;
            if (string.IsNullOrWhiteSpace(mskFECHA_VENCE.Text.Replace("/", "").Trim()))
            {
                _diasCredito = 15;
                mskFECHA_VENCE.Text = fechaDoc.AddDays(15).ToString("dd/MM/yyyy");
            }
            else
            {
                bool tieneFechaVence = DateTime.TryParseExact(
                    mskFECHA_VENCE.Text.Trim(), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out DateTime fechaVence);
                _diasCredito = (tieneFechaVence && fechaVence > fechaDoc)
                    ? (int?)(fechaVence - fechaDoc).Days
                    : 15;
            }
        }
        private void mskFECHA_VENCE_TextChanged(object sender, EventArgs e)
        {
            if (ObtenerIdCombo(cbxCONDPAGO) == 1) return;
            string textoVence = mskFECHA_VENCE.Text.Replace(" ", "").Replace("_", "").Trim();
            if (textoVence.Length < 10) return;
            string textoDoc = mskFECHA.Text.Trim();
            string[] formatos = { "dd/MM/yyyy", "yyyy-MM-dd" };
            bool tieneFechaDoc = DateTime.TryParseExact(textoDoc, formatos, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaDoc);
            bool tieneFechaVence = DateTime.TryParseExact(textoVence, formatos, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaVence);
            if (!tieneFechaDoc || !tieneFechaVence) return;
            if (fechaVence <= fechaDoc)
            {
                XtraMessageBox.Show("La fecha de vencimiento debe ser mayor a la fecha del documento.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskFECHA_VENCE.Text = string.Empty;
                _diasCredito = null;
                return;
            }
            _diasCredito = (fechaVence - fechaDoc).Days;
        }
        #endregion
        #region FORMAS DE PAGO - DETALLE
        private void txtRECIB_REMESA_Leave(object sender, EventArgs e)
        {
            LimitarPagoNoEfectivo(txtRECIB_REMESA);   // no pasarse del total
            ActualizarTotales();   // recalcula el efectivo pendiente (2026-10-01)
            decimal.TryParse(txtRECIB_REMESA.Text.Replace(",", ""), out decimal val);
            txtRECIB_REMESA.Text = val.ToString("N2");
            txtRECIB_REMESA_MONTO.Text = val.ToString("N2");   // el monto del bloque siempre = monto de arriba
            if (val > 0 && string.IsNullOrWhiteSpace(txtRECIB_REMESA_BANCO.Text))
                txtRECIB_REMESA_BANCO.Focus();
        }
        private void txtRECIB_CHEQUE_Leave(object sender, EventArgs e)
        {
            LimitarPagoNoEfectivo(txtRECIB_CHEQUE);   // no pasarse del total
            ActualizarTotales();   // recalcula el efectivo pendiente (2026-10-01)
            decimal.TryParse(txtRECIB_CHEQUE.Text.Replace(",", ""), out decimal val);
            txtRECIB_CHEQUE.Text = val.ToString("N2");
            txtRECIB_CHEQUE_MONTO.Text = val.ToString("N2");   // el monto del bloque siempre = monto de arriba
            if (val > 0 && string.IsNullOrWhiteSpace(txtRECIB_CHEQUE_BANCO.Text))
                txtRECIB_CHEQUE_BANCO.Focus();
        }
        private void txtRECIB_NOTAABONO_Leave(object sender, EventArgs e)
        {
            LimitarPagoNoEfectivo(txtRECIB_NOTAABONO);   // no pasarse del total
            ActualizarTotales();   // recalcula el efectivo pendiente (2026-10-01)
            decimal.TryParse(txtRECIB_NOTAABONO.Text.Replace(",", ""), out decimal val);
            txtRECIB_NOTAABONO.Text = val.ToString("N2");
            txtRECIB_NOTAABONO_MONTO.Text = val.ToString("N2");   // el monto del bloque siempre = monto de arriba
            if (val > 0 && string.IsNullOrWhiteSpace(txtRECIB_NOTAABONO_BANCO.Text))
                txtRECIB_NOTAABONO_BANCO.Focus();
        }
        #endregion
        private void cbxTIPO_DTE_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? idTipoDte = ObtenerIdCombo(cbxTIPO_DTE);
            if (idTipoDte == null || idTipoDte <= 0) return;
            AnioDte = ObtenerAnioPorDte(idTipoDte);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (IdFactEnc == 0)
            {
                XtraMessageBox.Show("Guarde la factura antes de imprimir.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                var reporte = new SistemaContable.RP.Ventas.rptFactura { IdFactEnc = IdFactEnc };
                reporte.MostrarPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al imprimir:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}




