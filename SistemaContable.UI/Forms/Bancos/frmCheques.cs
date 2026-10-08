using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using SistemaContable.DAL;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using SistemaContable.RP.Bancos.Proveedores;
using DevExpress.XtraEditors.Repository;
using System.ComponentModel;
using SistemaContable.RP.Partidas;

namespace SistemaContable.UI.Forms.Bancos
{
    public partial class frmCheques : DevExpress.XtraEditors.XtraForm
    {
        private enum EstadoFormulario
        {
            Inicializar,
            Agregar,
            Modificar,
            Ignorar,
            Guardar,
            Impreso,
            Buscar
        }

        /// <summary>
        /// true  = cheque estándar (operación fija "CH", campo bloqueado).
        /// false = otras operaciones (RM, NA, NC, TE, TR) elegibles por el usuario.
        /// </summary>
        public bool ModoCheque { get; set; } = true;
        private bool _operacionEsCargoAlaCuenta = true;
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtPartida;  // DataTable que alimenta el grid
        private int _idCheque = 0;     // 0 = nuevo, >0 = edición
        private long _idPartidaCheque = 0;    
        private string _columnaAnteriorGrid = string.Empty;
        private DataTable _documentosPago; // Documentos a pagar mediante Quedan
        private string _uidEnlaceCheque = string.Empty;
        private string _ultimoDetalleAutoGenerado = string.Empty;
        private bool _flujoEsQuedan = false;
        private EstadoFormulario _estadoActual = EstadoFormulario.Inicializar;
        private bool _modoBusqueda = false;
        // Flag para distinguir asignaciones programáticas vs digitación/selección del usuario
        private bool _asignandoCuentaPorCodigo = false;
        private bool _procesandoLeaveOperacion = false;
        private bool _habilitaFlujoProveedor = true;   // default: CH
        public int CantidadDocumentosVinculados { get; private set; } = 0;

        public frmCheques()
        {            
            InitializeComponent();
        }

        private void frmCheques_Load(object sender, EventArgs e)
        {
            this.Text = ModoCheque ? "Cheques" : "Otras operaciones";
            FormHelper.Inicializar(this);
            btnIgnorar.CausesValidation = false;
            btnGuardar.CausesValidation = false;  
                       
            InicializarGridPartida();

            FormHelper.RegistrarBusqueda(
                txtOPERACION,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_TIPO_PARTIDA",
                    Accion = "DIFERENTE_CH",
                    Columnas = new Dictionary<string, string>
                    {
                        { "CODIGO_PAR", "CODIGO" },
                        { "NOMBRE",     "NOMBRE" }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "CODIGO_PAR",  100 },
                        { "NOMBRE",  400 }
                    },
                    AnchoFormulario = 400
                },
                fila =>
                {
                    bool esCargo = fila["ES_CARGO"] != DBNull.Value && Convert.ToBoolean(fila["ES_CARGO"]);
                    SetearDatosOperacion(fila["ID_TIPO_PARTIDA"].ToString(), esCargo);
                    txtOPERACION.Text = fila["CODIGO_PAR"].ToString();

                    if (_estadoActual == EstadoFormulario.Agregar &&
                        fila["CODIGO_PAR"].ToString().ToUpper() != "CH")
                    {
                        txtNUMERO_CHEQUE.Text = "";
                    }
                    AplicarReglasOperacion();
                    AsignarNumeroPartidaSugerido();
                }
            );

            FormHelper.RegistrarBusqueda(
                 txtNUM_CUENTA,
                 new BusquedaConfig
                 {
                     StoredProcedure = "SP_CUENTA_BANCARIA",
                     ParametrosExtra = new { ACTIVA = true },
                     Columnas = new Dictionary<string, string>
                     {
                        { "NUM_CUENTA", "CUENTA" },
                        { "NOMBRE",     "NOMBRE" }
                     },
                     Anchos = new Dictionary<string, int>
                     {
                        { "NUM_CUENTA", 110 },
                        { "NOMBRE",     400 }
                     }
                 },
                 fila =>
                 {
                     decimal montoActual = ObtenerDecimal(txtCANTIDAD);
                     CargarCuentaBanco(fila, montoActual);
                     FormHelper.EnfocarConDelay(txtNUMERO_CHEQUE);  
                 }
             );

            FormHelper.RegistrarBusqueda(
                txtPROVEEDOR,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_ENTIDAD",
                    Accion = "BUSCAR_CONTRIBUYENTES",
                    Columnas = new Dictionary<string, string>
                    {
                        { "CODIGO_ENTIDAD", "PROVEEDOR" },
                        { "NOMBRE",         "NOMBRE"    },
                        { "NIT",            "NIT"       }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "CODIGO_ENTIDAD", 100 },
                        { "NOMBRE",         300 },
                        { "NIT",            120 }
                    },
                    ParametrosExtra = new { ROL = "PRO" }
                },
                fila =>
                {
                    if (!_habilitaFlujoProveedor) return;
                    AsignarProveedor(fila);
                }
            );

            FormHelper.RegistrarBusqueda(
                txtNUMERO_CHEQUE,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_CHEQUE",
                    Accion = "BUSCAR",
                    Columnas = new Dictionary<string, string>
                    {
                        { "NUM_CHEQUE",    "NUMERO" },                        
                        { "CONCEPTO",  "CONCEPTO" },
                        { "FECHA_CHEQUE",  "FECHA" },
                        { "MONTO",  "MONTO" }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "NUM_CHEQUE", 100 },                                                
                        { "CONCEPTO", 350 },
                        { "FECHA", 20 },
                        { "MONTO", 80 }
                    },
                    // Los parámetros ID_CTA_BANCO y TIPO_PARTIDA se pasan dinámicamente
                    // en tiempo de ejecución. Usamos un delegate para armarlos al momento.
                    ObtenerParametrosExtra = () => new
                    {
                        ID_CTA_BANCO = Convert.ToInt32(txtNUM_CUENTA.Tag ?? 0),
                        TIPO_PARTIDA = txtOPERACION.Text.Trim()
                    },
                    AnchoFormulario = 1000
                },
                fila =>
                {
                    // Validar que se haya elegido una cuenta antes de buscar
                    if (txtNUM_CUENTA.Tag == null || Convert.ToInt32(txtNUM_CUENTA.Tag) == 0)
                    {
                        XtraMessageBox.Show(
                            "Debe seleccionar primero la cuenta bancaria.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    int idCheque = Convert.ToInt32(fila["ID_CHEQUE"]);
                    CargarCheque(idCheque);
                    FormHelper.EnfocarConDelay(btnModificar);
                }
            );

            ConfigurarCRUD(EstadoFormulario.Inicializar);                  
        }

        // ============================================================
        // CargarCuentaBanco        
        // Comportamiento:
        //   * Siempre asigna cuenta, nombre, cuenta contable en la partida.
        //   * En modo Agregar:  también asigna NUM_CHEQUE y NUMERO_PARTIDA sugeridos.
        //   * En modo Buscar:   NO asigna NUM_CHEQUE (el usuario lo va a buscar).
        // ============================================================
        private void CargarCuentaBanco(DataRow fila, decimal monto = 0m)
        {
            // ---- Datos básicos de la cuenta (siempre) ----
            txtNUM_CUENTA.Tag = fila["ID_CTA_BANCO"].ToString();
            txtNUM_CUENTA.Text = fila["NUM_CUENTA"].ToString();
            txtNOMBRE.Text = fila["NOMBRE"].ToString();
            txtMONEDA.Text = "DOLARES";
                       
            // ---- Diferencias según el modo ----
            if (_modoBusqueda)
            {
                // Modo búsqueda: NO sugerir NUM_CHEQUE ni NUMERO_PARTIDA
                // El usuario los ingresará (o usará *+Enter) para buscar
                txtNUMERO_CHEQUE.Text = string.Empty;
                txtNUMERO_PARTIDA.Text = string.Empty;

                // Esperar a que el SendKeys("{TAB}") de FormHelper termine
                // antes de forzar el foco al campo de búsqueda de cheque
                FormHelper.EnfocarConDelay(txtNUMERO_CHEQUE);
            }
            else
            {
                // ---- Cuenta contable como primera fila del grid (siempre) ----
                string ctaContable = fila["CTACONTABLE"].ToString();
                if (!string.IsNullOrWhiteSpace(ctaContable))
                    AgregarFilaPartida(ctaContable);

                if (monto > 0 && _dtPartida.Rows.Count > 0)
                {
                    string columna = _operacionEsCargoAlaCuenta ? "ABONO" : "CARGO";
                    _dtPartida.Rows[0][columna] = monto;
                    gridControl1.RefreshDataSource();
                    ActualizarCuadre();
                }

                // Modo alta: sugerir el siguiente número de cheque
                if (ModoCheque)
                {
                    int correlativo = 0;
                    if (fila["CORRELATIVO_CHEQUE"] != DBNull.Value)
                        int.TryParse(fila["CORRELATIVO_CHEQUE"].ToString(), out correlativo);
                    txtNUMERO_CHEQUE.Text = (correlativo + 1).ToString();
                }
                // Sugerir siguiente número de partida (sin consumirlo aún)                
                AsignarNumeroPartidaSugerido();
            }
        }

        private void CargarCheque(int idCheque)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // OBTENER devuelve 3 result sets: encabezado, partida, CCFs
                DataSet ds = _dal.EjecutarMultiple("SP_CHEQUE", new
                {
                    ACCION = "OBTENER",
                    ID_CHEQUE = idCheque
                });

                if (ds.Tables.Count < 3 || ds.Tables[0].Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        $"No se encontró el documento con ID {idCheque}.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ---------- Result set 1: ENCABEZADO ----------
                DataRow r = ds.Tables[0].Rows[0];

                _idCheque = Convert.ToInt32(r["ID_CHEQUE"]);
                _uidEnlaceCheque = FormHelper.ObtenerUUID();   // regenerar UID por si edita
                _flujoEsQuedan = r["ES_QUEDAN"] == DBNull.Value ? false : Convert.ToBoolean(r["ES_QUEDAN"]);
                txtOPERACION.Text = SafeStr(r["TIPO_PARTIDA"]);
                txtNUM_CUENTA.Tag = SafeStr(r["ID_CTA_BANCO"]);
                txtNUM_CUENTA.Text = SafeStr(r["NUM_CUENTA"]);
                txtNOMBRE.Text = SafeStr(r["NOMBRE_CUENTA"]);
                txtMONEDA.Text = "DOLARES";
                txtNUMERO_CHEQUE.Text = SafeStr(r["NUM_CHEQUE"]);
                txtNUMERO_PARTIDA.Text = SafeStr(r["NID_PARTIDA"]);   // formateado                             
                deFECHA_CHEQUE.DateTime = Convert.ToDateTime(r["FECHA_CHEQUE"]);
                txtNOMBRE_CHEQUE.Text = SafeStr(r["NOMBRE_CHEQUE"]);
                txtCONCEPTO.Text = SafeStr(r["CONCEPTO"]);
                txtPROVEEDOR.Tag = _flujoEsQuedan ? SafeStr(r["ID_ENTIDAD"]) : null;
                txtPROVEEDOR.Text = SafeStr(r["CODIGO_ENTIDAD"]);                
                chkImpreso.Checked = r["IMPRESO"] != DBNull.Value && Convert.ToBoolean(r["IMPRESO"]); 
                decimal monto = r["MONTO"] == DBNull.Value ? 0m : Convert.ToDecimal(r["MONTO"]);
                txtCANTIDAD.Text = monto.ToString("N2");
                bool estaAnulado = r.Table.Columns.Contains("ANULADO")
                        && r["ANULADO"] != DBNull.Value
                        && Convert.ToBoolean(r["ANULADO"]);
                bool esCargo = true;
                if (r.Table.Columns.Contains("ES_CARGO") && r["ES_CARGO"] != DBNull.Value)
                    esCargo = Convert.ToBoolean(r["ES_CARGO"]);
                _operacionEsCargoAlaCuenta = esCargo;
                _idPartidaCheque = r["ID_PARTIDA"] == DBNull.Value ? 0 : Convert.ToInt64(r["ID_PARTIDA"]);

                AplicarReglasOperacion();
                _asignandoCuentaPorCodigo = true;
                try
                {
                    // ---------- Result set 2: PARTIDA CONTABLE ----------
                    _dtPartida.Clear();
                    foreach (DataRow rp in ds.Tables[1].Rows)
                    {
                        var fila = _dtPartida.NewRow();
                        fila["ORDEN"] = rp["ORDEN"] == DBNull.Value ? 0 : Convert.ToInt32(rp["ORDEN"]);
                        fila["CTACONTABLE"] = SafeStr(rp["CTACONTABLE"]);
                        fila["DETALLE"] = SafeStr(rp["DETALLE"]);
                        fila["CARGO"] = rp["CARGO"] == DBNull.Value ? 0m : Convert.ToDecimal(rp["CARGO"]);
                        fila["ABONO"] = rp["ABONO"] == DBNull.Value ? 0m : Convert.ToDecimal(rp["ABONO"]);
                        _dtPartida.Rows.Add(fila);
                    }
                }
                finally
                {
                    _asignandoCuentaPorCodigo = false;
                }                

                ActualizarCuadre();

                // ---------- Result set 3: Documentos VINCULADOS ----------
                if (_documentosPago == null)
                {
                    _documentosPago = ds.Tables[2].Copy();
                }
                else
                {
                    _documentosPago.Clear();
                    foreach (DataRow rc in ds.Tables[2].Rows)
                        _documentosPago.ImportRow(rc);
                }
                CantidadDocumentosVinculados = _documentosPago == null ? 0 : _documentosPago.Rows.Count;

                // ---------- Estado final ----------
                if (estaAnulado)    
                {
                    ConfigurarCRUD(EstadoFormulario.Guardar, chequeAnulado: true);
                    btnImprimir.Enabled = false;
                }                    
                else
                {
                    ConfigurarCRUD(EstadoFormulario.Guardar);
                    //btnImprimir.Enabled = !chkImpreso.Checked;
                }
                    
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "Error al cargar el documento:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ConfigurarOperacion(bool enfocarCuenta = true)
        {
            if (ModoCheque)
            {
                txtOPERACION.Text = "CH";
                txtOPERACION.ReadOnly = true;
                txtOPERACION.Tag = null;
                _operacionEsCargoAlaCuenta = true;
            }
            else
            {
                txtOPERACION.Text = "";
                txtOPERACION.ReadOnly = false;
                txtOPERACION.Tag = null;
                _operacionEsCargoAlaCuenta = true;
                txtNUMERO_CHEQUE.Text = "";
            }
            AplicarReglasOperacion();
            if (enfocarCuenta)
                this.BeginInvoke(new Action(() => txtNUM_CUENTA.Focus()));
        }
        
        private bool _procesandoLeaveProveedor = false;
        private void txtPROVEEDOR_Leave(object sender, EventArgs e)
        {
            if (FormHelper.EsEscapeDeFoco(this) || txtPROVEEDOR.ReadOnly) return;
            if (_procesandoLeaveProveedor) return;   // ← evita reentrancia
            if (!_habilitaFlujoProveedor) return;

            _procesandoLeaveProveedor = true;
            try
            {
                string codigo = txtPROVEEDOR.Text.Trim();
                string cuentaPorPagar = "";

                if ((codigo == "*") || (string.IsNullOrWhiteSpace(codigo)))
                {
                    _documentosPago?.Clear();  
                    return;
                }
                
                // Validar que el usuario haya seleccionado primero la cuenta del banco
                if (_dtPartida.Rows.Count == 0 ||
                    string.IsNullOrWhiteSpace(_dtPartida.Rows[0]["CTACONTABLE"].ToString()))
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Debe seleccionar primero la cuenta bancaria.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPROVEEDOR.Tag = null;
                    txtPROVEEDOR.Text = string.Empty;
                    txtNUM_CUENTA.Focus();
                    return;
                }
                try
                {
                    var dt = _dal.EjecutarConsulta("SP_ENTIDAD", new
                    {
                        ACCION = "BUSCAR_POR_CODIGO",
                        FILTRO = codigo,
                        ROL = "PRO"
                    });

                    if (dt.Rows.Count > 0)
                    {
                        AsignarProveedor(dt.Rows[0]);
                        cuentaPorPagar = dt.Rows[0]["CUENTA_X_PAGAR"].ToString();

                        using (var frm = new frmChequeSeleccionQuedan())
                        {
                            frm.CodigoEntidad = codigo;
                            frm.NombreEntidad = dt.Rows[0]["NOMBRE"].ToString();

                            if (frm.ShowDialog(this) == DialogResult.OK)
                            {
                                _documentosPago = frm.DocumentosAPagar;
                                txtCANTIDAD.Text = frm.TotalAPagar.ToString("N2");
                                txtCANTIDAD.ReadOnly = true;
                                txtPROVEEDOR.ReadOnly = true;
                                btnDocumentos.Enabled = false;
                                _flujoEsQuedan = true;
                                txtCONCEPTO.Text = ConstruirConceptoPago(_documentosPago);
                                AplicarPartidaPago(frm.TotalAPagar, cuentaPorPagar);

                                // ✅ Mover el foco al concepto. El flag evita reentrancia.
                                this.BeginInvoke(new Action(() => txtCONCEPTO.Focus()));
                            }
                            else
                            {
                                LimpiarSeleccionPago();
                            }
                        }
                    }
                    else
                    {
                        LimpiarProveedor();
                        DevExpress.XtraEditors.XtraMessageBox.Show(
                            $"No se encontró el proveedor con código '{codigo}'.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPROVEEDOR.Focus();
                    }
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        $"Error al buscar proveedor: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                _procesandoLeaveProveedor = false;
            }
        }

        private void AsignarProveedor(DataRow fila)
        {
            txtPROVEEDOR.Tag = fila["ID_ENTIDAD"].ToString();
            txtPROVEEDOR.Text = fila["CODIGO_ENTIDAD"].ToString();
            txtNOMBRE_CHEQUE.Text = fila["NOMBRE"].ToString();            
        }
        private void LimpiarProveedor()
        {          
            txtNOMBRE_CHEQUE.Text = string.Empty;          
        }
        private void LimpiarSeleccionPago()
        {
            _documentosPago = null;            
            txtCANTIDAD.Text = "";
            txtPROVEEDOR.Tag = null;
            txtPROVEEDOR.Text = "";
            txtNOMBRE_CHEQUE.Text = "";
            btnDocumentos.Enabled = true; 
            _flujoEsQuedan = false;
        }

        private void ConfigurarCRUD(EstadoFormulario estado, bool chequeAnulado = false)
        {
            _estadoActual = estado;
            switch (estado)
            {                
                case EstadoFormulario.Inicializar:
                    _modoBusqueda = false;
                    HabilitarBotones(
                        agregar: true,
                        buscar: true,
                        modificar: false,
                        guardar: false,
                        ignorar: false,
                        eliminar: false,
                        imprimir: false,
                        anterior: false,
                        siguiente: false,
                        finalizar: true,
                        borrarFila: false,
                        anular: false,
                        documentos: false
                    );
                    HabilitarControlesEncabezado(false);
                    HabilitarControlesPartida(false);
                    break;

                case EstadoFormulario.Agregar:
                    _modoBusqueda = false;
                    HabilitarBotones(
                        agregar: false,
                        buscar: false,
                        modificar: false,
                        guardar: true,
                        ignorar: true,
                        eliminar: false,
                        imprimir: false,
                        anterior: false,
                        siguiente: false,
                        finalizar: false,
                        borrarFila: true,
                        anular: false,
                        documentos: true
                    );
                    HabilitarControlesEncabezado(true);
                    HabilitarControlesPartida(true);
                    break;

                case EstadoFormulario.Buscar:
                    _modoBusqueda = true;
                    HabilitarBotones(
                        agregar: false,
                        buscar: false,
                        modificar: false,
                        guardar: false,
                        ignorar: true,
                        eliminar: false,
                        imprimir: false,
                        anterior: false,
                        siguiente: false,
                        finalizar: false,
                        borrarFila: false,
                        anular: false,
                        documentos: false
                    );
                    HabilitarControlesEncabezado(false);
                    HabilitarControlesPartida(false);
                    // Solo cuenta bancaria y número de cheque para búsqueda
                    txtOPERACION.Enabled = true;
                    txtNUM_CUENTA.Enabled = true;
                    txtNUMERO_CHEQUE.ReadOnly = false;
                    txtNUMERO_CHEQUE.Enabled = true;
                    break;

                case EstadoFormulario.Modificar:
                    _modoBusqueda = false;
                    HabilitarBotones(
                        agregar: false,
                        buscar: false,
                        modificar: false,
                        guardar: true,
                        ignorar: true,
                        eliminar: false,
                        imprimir: false,
                        anterior: false,
                        siguiente: false,
                        finalizar: true,
                        borrarFila: true,
                        anular: false,
                        documentos: true
                    );

                    HabilitarControlesEncabezado(true);
                    HabilitarControlesPartida(true);
                    // Bloquear campos que rompen coherencia
                    txtNUM_CUENTA.Enabled = false;
                    txtOPERACION.Enabled = false;   // tipo de partida
                    txtNUMERO_CHEQUE.Enabled = true;
                    txtNUMERO_PARTIDA.Enabled = false;                    
                    deFECHA_CHEQUE.Enabled = false; 
                    break;

                case EstadoFormulario.Ignorar:
                    _modoBusqueda = false;
                    HabilitarBotones(
                        agregar: true,
                        buscar: true,
                        modificar: false,
                        guardar: false,
                        ignorar: false,
                        eliminar: false,
                        imprimir: false,
                        anterior: false,
                        siguiente: false,
                        finalizar: true,
                        borrarFila: true,
                        anular: false,
                        documentos: false
                    );
                    HabilitarControlesEncabezado(true);
                    HabilitarControlesPartida(true);
                    break;

                case EstadoFormulario.Guardar:
                    _modoBusqueda = false;

                    HabilitarBotones(
                        agregar: true,
                        buscar: true,
                        modificar: !chequeAnulado,
                        guardar: false,
                        ignorar: false,
                        eliminar: !chequeAnulado,
                        imprimir: !chequeAnulado,
                        anterior: true,
                        siguiente: true,
                        finalizar: true,
                        borrarFila: false,
                        anular: !chequeAnulado,
                        documentos: false 
                    );                    
                    HabilitarControlesEncabezado(false);
                    HabilitarControlesPartida(false);
                    break;                   
            }
        }

        private void HabilitarBotones(
            bool agregar,
            bool buscar,
            bool modificar,
            bool guardar,
            bool ignorar,
            bool eliminar,
            bool imprimir,
            bool anterior,
            bool siguiente,
            bool finalizar,
            bool borrarFila,
            bool anular,
            bool documentos)
        {
            btnAgregar.Enabled = agregar;
            btnBuscar.Enabled = buscar;
            btnModificar.Enabled = modificar;
            btnGuardar.Enabled = guardar;
            btnIgnorar.Enabled = ignorar;
            btnEliminar.Enabled = eliminar;
            btnImprimir.Enabled = imprimir;
            btnAnterior.Enabled = anterior;
            btnSiguiente.Enabled = siguiente;
            btnFinalizar.Enabled = finalizar;
            btnBorrarFila.Enabled = borrarFila;            
            btnAnular.Enabled = anular;
            btnDocumentos.Enabled = documentos; 
        }


        // ============================================================
        // Habilita/deshabilita los controles del encabezado del cheque
        // ============================================================
        private void HabilitarControlesEncabezado(bool habilitar)
        {
            txtOPERACION.Enabled = habilitar;   // tipo de partida
            txtNUM_CUENTA.Enabled = habilitar;            
            deFECHA_CHEQUE.Enabled = habilitar; 
            txtPROVEEDOR.Enabled = habilitar;
            txtNOMBRE.Enabled = habilitar;
            txtNUMERO_CHEQUE.Enabled = habilitar;
            txtCANTIDAD.Enabled = habilitar;
            txtNOMBRE_CHEQUE.Enabled = habilitar;
            txtNUMERO_PARTIDA.Enabled = habilitar;
            txtMONEDA.Enabled = habilitar;
            txtCONCEPTO.Enabled = habilitar;
        }

        // ============================================================
        // Habilita/deshabilita la grilla de partida contable
        // ============================================================
        private void HabilitarControlesPartida(bool habilitar)
        {
            gridControl1.Enabled = habilitar;
        }

        /// <summary>
        /// Verifica si hay CCFs al contado huérfanos del usuario actual y pregunta
        /// si los quiere retomar. Si acepta, asigna ese UID y abre directamente
        /// el formulario frmChequeDocumentosContado.
        /// Retorna true si retomó, false en caso contrario.
        /// </summary>
        private bool VerificarDocumentosHuerfanos()
        {
            try
            {
                var dt = _dal.EjecutarConsulta("SP_CHEQUE_CONTADO", new
                {
                    ACCION = "LISTAR_UIDS_HUERFANOS",
                    USUARIO = Configuracion.UsuarioActual
                });

                if (dt.Rows.Count == 0) return false;

                // Tomar el más reciente (si hay varios)
                DataRow rowMasReciente = dt.Rows[0];
                string uid = rowMasReciente["UID_ENLACE_CHEQUE"].ToString();
                int cantidad = Convert.ToInt32(rowMasReciente["CANTIDAD_DOCUMENTOS"]);

                var resp = DevExpress.XtraEditors.XtraMessageBox.Show(
                    $"Existen {cantidad} documento(s) al contado pendiente(s) de un proceso anterior.\n\n" +
                    "¿Desea retomarlos?",
                    "Documentos pendientes",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resp == DialogResult.Yes)
                {
                    _uidEnlaceCheque = uid;
                    // Abrir directamente frmChequeDocumentosContado con el UID
                    this.BeginInvoke(new Action(() => AbrirContadoConUid()));
                    return true;
                }
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Error al verificar documentos pendientes:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        /// <summary>
        /// Abre frmChequeDocumentosContado con el UID actual (ya asignado).
        /// Reutiliza la misma lógica que el botón CCF Contado, pero sin validar
        /// la cuenta bancaria (porque aún no se ha seleccionado).
        /// </summary>
        private void AbrirContadoConUid()
        {
            try
            {
                using (var frm = new frmChequeDocumentosContado())
                {
                    frm.numeroChequeSugerido = txtNUMERO_CHEQUE.Text;  
                    frm.UidEnlaceCheque = _uidEnlaceCheque;
                    frm.IdCheque = _idCheque;
                    var principal = Application.OpenForms["frmPrincipalRibbon"];

                    if (principal != null)
                    {
                        var ribbon = principal.Controls.Find("Ribbon", true);
                        var statusBar = principal.Controls.Find("StatusBar", true);
                        int alturaRibbon = ribbon.Length > 0 ? ribbon[0].Height : 0;
                        int alturaStatus = statusBar.Length > 0 ? statusBar[0].Height : 0;
                        frm.Width = principal.ClientRectangle.Width;
                        frm.Top = principal.Top + alturaRibbon;
                        frm.Left = principal.Left;
                        frm.StartPosition = FormStartPosition.CenterScreen;
                    }

                    if (frm.ShowDialog(principal ?? (Form)this) == DialogResult.OK)
                    {
                        CantidadDocumentosVinculados = frm.CantidadDocumentosVinculados; 
                    }
                }
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Error al abrir documentos al contado:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region Grid de Partida Contable

        private void InicializarGridPartida()
        {
            // Crear DataTable con las columnas de CHEQUE_PARTIDA
            _dtPartida = new DataTable();
            _dtPartida.Columns.Add("ORDEN", typeof(int));
            _dtPartida.Columns.Add("CTACONTABLE", typeof(string));
            _dtPartida.Columns.Add("DETALLE", typeof(string));
            _dtPartida.Columns.Add("CARGO", typeof(decimal));
            _dtPartida.Columns.Add("ABONO", typeof(decimal));

            // ✅ Recalcular cuadre cuando cambien filas (manual o por código)
            _dtPartida.RowChanged += (s, ev) => ActualizarCuadre();
            _dtPartida.RowDeleted += (s, ev) => ActualizarCuadre();
                        
            gridView1.ShownEditor += GridView1_ShownEditor;

            // Actualiza el label de cuenta contable en cuanto cambia el valor de la columna
            _dtPartida.ColumnChanged += (s, ev) =>
            {
                if (ev.Column.ColumnName == "CTACONTABLE")
                {
                    // Solo mostrar el mensaje si viene de digitación/selección del usuario
                    if (!_asignandoCuentaPorCodigo)
                        ActualizarEstadoCuenta(ev.Row["CTACONTABLE"]?.ToString());
                }
            };

            // Agregar fila vacía inicial
            AgregarFilaVacia();

            // Enlazar al grid
            gridControl1.DataSource = _dtPartida;            

            // Configurar columnas visibles con títulos
            var view = gridControl1.MainView as GridView;
            if (view == null) return;

            view.Columns.Clear();
            view.PopulateColumns();

            ConfigurarColumna(view, "ORDEN", "ORDEN", 0, false);
            ConfigurarColumna(view, "CTACONTABLE", "CUENTA", 150);
            ConfigurarColumna(view, "DETALLE", "DETALLE DE LA APLICACION", 350);
            ConfigurarColumna(view, "CARGO", "CARGO", 75);
            ConfigurarColumna(view, "ABONO", "ABONO", 75);

            foreach (DevExpress.XtraGrid.Columns.GridColumn col in view.Columns)
                col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;

            // Opciones del grid
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsBehavior.Editable = true;
            view.OptionsBehavior.AutoSelectAllInEditor = true;
            view.OptionsNavigation.EnterMoveNextColumn = true;

            // Selección por fila completa
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.OptionsSelection.EnableAppearanceFocusedRow = true;

            // Enter como Tab entre columnas y al final nueva fila
            view.KeyDown += GridView_KeyDown;
            view.FocusedColumnChanged += GridView_FocusedColumnChanged;

            // Formatear columnas numéricas
            view.CustomColumnDisplayText += (s, ev) =>
            {
                if (ev.Column.FieldName == "CARGO" || ev.Column.FieldName == "ABONO")
                {
                    if (ev.Value == null || ev.Value == DBNull.Value ||
                        string.IsNullOrWhiteSpace(ev.Value.ToString()))
                    {
                        ev.DisplayText = string.Empty;
                        return;
                    }
                    if (decimal.TryParse(ev.Value.ToString(), out decimal valor))
                        ev.DisplayText = valor == 0 ? string.Empty : valor.ToString("N2");
                    else
                        ev.DisplayText = string.Empty;
                }
            };          

            // Selección de fila completa
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
            gridView1.FocusedRowChanged += GridView_FocusedRowChanged;
        }

        private void ConfigurarColumna(GridView view, string fieldName,
            string caption, int width, bool visible = true)
        {
            if (!view.Columns.ColumnByFieldName(fieldName).Equals(null))
            {
                var col = view.Columns[fieldName];
                col.Caption = caption;
                col.Width = width;
                col.Visible = visible;
            }
        }

        private void AgregarFilaVacia()
        {
            var fila = _dtPartida.NewRow();
            fila["ORDEN"] = 0;
            fila["CTACONTABLE"] = string.Empty;
            fila["DETALLE"] = string.Empty;
            fila["CARGO"] = 0m;
            fila["ABONO"] = 0m;
            _dtPartida.Rows.Add(fila);
        }

        private void AgregarFilaPartida(string ctaContable)
        {
            _asignandoCuentaPorCodigo = true;
            try
            {
                // Limpiar filas vacías antes de agregar
                for (int i = _dtPartida.Rows.Count - 1; i >= 0; i--)
                {
                    var r = _dtPartida.Rows[i];
                    if (string.IsNullOrWhiteSpace(r["CTACONTABLE"].ToString()) &&
                        Convert.ToDecimal(r["CARGO"]) == 0 &&
                        Convert.ToDecimal(r["ABONO"]) == 0)
                        _dtPartida.Rows.RemoveAt(i);
                }

                // Agregar fila con la cuenta contable
                if (_dtPartida.Rows.Count == 0)
                {
                    var fila = _dtPartida.NewRow();
                    fila["ORDEN"] = 0;
                    fila["CTACONTABLE"] = ctaContable;
                    fila["DETALLE"] = string.Empty;
                    fila["CARGO"] = 0m;
                    fila["ABONO"] = 0m;
                    _dtPartida.Rows.Add(fila);
                }
                else
                {
                    var fila = _dtPartida.Rows[0];
                    fila["CTACONTABLE"] = ctaContable;
                }

                // Agregar fila vacía para siguiente ingreso
                AgregarFilaVacia();
            }
            finally
            {
                _asignandoCuentaPorCodigo = false;
            }                
        }

        private void ActualizarEstadoCuenta(string codigo)
        {
            var (texto, esValida) = CuentaContableHint.Obtener(codigo);

            if (string.IsNullOrWhiteSpace(texto) || !gridControl1.Enabled)
            {                
                FormHelper.OcultarMensajeRibbon(this);
                return;
            }
            FormHelper.MostrarMensajeRibbon(this, texto);            
        }

        /// <summary>
        /// Actualiza la fila 1 (cuenta del banco) con el abono y detalle,
        /// y crea/sobreescribe la fila 2 con la cuenta por pagar del proveedor.
        /// </summary>
        private void AplicarPartidaPago(decimal totalPago, string cuentaPorPagar)
        {
            if (_dtPartida.Rows.Count == 0) return;
            _asignandoCuentaPorCodigo = true;
            try
            {
                string detalle = $"{txtOPERACION.Text} # {txtNUMERO_CHEQUE.Text.Trim()} {txtNOMBRE_CHEQUE.Text.Trim()}";

                // ============================================================
                // Fila 1: cuenta del banco -> ABONO + DETALLE
                // ============================================================
                _dtPartida.Rows[0]["DETALLE"] = detalle;
                _dtPartida.Rows[0]["CARGO"] = 0m;
                _dtPartida.Rows[0]["ABONO"] = totalPago;

                // ============================================================
                // Fila 2: cuenta por pagar del proveedor -> CARGO + DETALLE
                // Si ya existe (segunda llamada) se sobrescribe; si no, se crea.
                // ============================================================
                if (_dtPartida.Rows.Count >= 2)
                {
                    _dtPartida.Rows[1]["CTACONTABLE"] = cuentaPorPagar;
                    _dtPartida.Rows[1]["DETALLE"] = "";
                    _dtPartida.Rows[1]["CARGO"] = totalPago;
                    _dtPartida.Rows[1]["ABONO"] = 0m;
                }
                else
                {
                    var fila = _dtPartida.NewRow();
                    fila["ORDEN"] = 0;
                    fila["CTACONTABLE"] = cuentaPorPagar;
                    fila["DETALLE"] = detalle;
                    fila["CARGO"] = totalPago;
                    fila["ABONO"] = 0m;
                    _dtPartida.Rows.Add(fila);
                }

                // Eliminar filas vacías sobrantes después de la fila 2
                for (int i = _dtPartida.Rows.Count - 1; i >= 2; i--)
                {
                    var r = _dtPartida.Rows[i];
                    if (string.IsNullOrWhiteSpace(r["CTACONTABLE"].ToString()) &&
                        Convert.ToDecimal(r["CARGO"]) == 0 &&
                        Convert.ToDecimal(r["ABONO"]) == 0)
                    {
                        _dtPartida.Rows.RemoveAt(i);
                    }
                }

                // Asegurar fila vacía al final para captura adicional
                DataRow ultima = _dtPartida.Rows[_dtPartida.Rows.Count - 1];
                if (!string.IsNullOrWhiteSpace(ultima["CTACONTABLE"].ToString()) ||
                    Convert.ToDecimal(ultima["CARGO"]) != 0 ||
                    Convert.ToDecimal(ultima["ABONO"]) != 0)
                {
                    AgregarFilaVacia();
                }
            }
            finally
            {
                _asignandoCuentaPorCodigo = false;
            }
            
        }

        private void GridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter && e.KeyCode != Keys.Tab) return;

            var view = sender as GridView;
            if (view == null) return;

            string colActual = view.FocusedColumn?.FieldName;

            // Verificar * en CTACONTABLE PRIMERO antes de cualquier otra acción
            if (colActual == "CTACONTABLE")
            {
                string texto = view.ActiveEditor?.Text?.Trim();
                if (string.IsNullOrEmpty(texto))
                    texto = view.GetFocusedDisplayText()?.Trim();

                if (texto == "*" && e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    AbrirBusquedaCuenta(view);
                    return; // salir sin hacer nada más
                }
            }

            // ✅ Caso especial: DETALLE de la fila 0 (cuenta del banco)
            // → saltar directo a CTACONTABLE de la fila 1
            if (colActual == "DETALLE" && view.FocusedRowHandle == 0)
            {
                e.Handled = true;
                view.CloseEditor();
                view.UpdateCurrentRow();

                if (_dtPartida.Rows.Count < 2)
                    AgregarFilaVacia();

                _columnaAnteriorGrid = "CTACONTABLE";

                view.FocusedRowHandle = 1;
                view.FocusedColumn = view.Columns["CTACONTABLE"];
                view.ShowEditor();
                return;
            }


            // Si está en CARGO con valor > 0 → saltar a CTACONTABLE de la siguiente fila
            // Si CARGO == 0 → comportamiento normal (pasa a ABONO)
            if (colActual == "CARGO")
            {
                view.CloseEditor();
                view.UpdateCurrentRow();

                decimal cargoActual = 0;
                object valorCargo = view.GetFocusedRowCellValue("CARGO");
                if (valorCargo != null && valorCargo != DBNull.Value)
                    decimal.TryParse(valorCargo.ToString(), out cargoActual);

                if (cargoActual > 0)
                {
                    e.Handled = true;
                    int filaActual = view.FocusedRowHandle;

                    if (filaActual == _dtPartida.Rows.Count - 1)
                        AgregarFilaVacia();

                    view.FocusedRowHandle = filaActual + 1;
                    view.FocusedColumn = view.Columns["CTACONTABLE"];
                    view.ShowEditor();
                    return;
                }
                // Si CARGO == 0 → caer al bloque general de Tab (avanza a ABONO)
            }

            // Enter como Tab entre columnas
            int colIndex = view.FocusedColumn?.VisibleIndex ?? 0;
            int totalCols = view.VisibleColumns.Count;

            if (colIndex < totalCols - 1)
            {
                view.FocusedColumn = view.VisibleColumns[colIndex + 1];
                view.ShowEditor();
            }
            else
            {
                view.CloseEditor();
                int filaActual = view.FocusedRowHandle;

                if (filaActual == _dtPartida.Rows.Count - 1)
                    AgregarFilaVacia();

                view.FocusedRowHandle = filaActual + 1;
                view.FocusedColumn = view.VisibleColumns[0];
                view.ShowEditor();
            }

            e.Handled = true;
        }

        private void GridView_FocusedColumnChanged(object sender,
             DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            var view = sender as GridView;
            if (view == null) return;

            // ============================================================
            // Mostrar/ocultar el panel de estado de cuenta contable
            // ============================================================
            if (_columnaAnteriorGrid == "CTACONTABLE" && e.FocusedColumn?.FieldName != "CTACONTABLE")
            {
                string cta = view.GetFocusedRowCellValue("CTACONTABLE")?.ToString();
                MostrarEstadoCuenta(cta);
            }
            else
            {
                FormHelper.OcultarMensajeRibbon(this);
            }

            if (_columnaAnteriorGrid == "CTACONTABLE" &&
                e.FocusedColumn?.FieldName == "DETALLE")
            {
                string cta = view.GetFocusedRowCellValue("CTACONTABLE")?.ToString();
                if (string.IsNullOrWhiteSpace(cta)) return;

                string codOp = txtOPERACION.Text?.Trim().ToUpper() ?? "";
                string detalle = null;

               
               detalle = $"{codOp} # {txtNUMERO_CHEQUE.Text.Trim()} " +
                         $"{txtNOMBRE_CHEQUE.Text.Trim()}";
               
                // Para las otras operaciones (RM, NA, TE, TR) no autogenerar

                if (detalle != null)
                {
                    view.SetFocusedRowCellValue("DETALLE", detalle);
                }
                view.ShowEditor();

                // Diferir el SelectAll hasta que el editor esté completamente activo
                this.BeginInvoke(new Action(() =>
                {
                    if (view.ActiveEditor != null)
                        view.ActiveEditor.SelectAll();
                }));
            }
            _columnaAnteriorGrid = e.FocusedColumn?.FieldName ?? string.Empty;
        }

        private void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // Al cambiar de fila, resetear contexto de columna anterior
            // y ocultar mensaje (el contexto ya no es válido)
            _columnaAnteriorGrid = string.Empty;
            FormHelper.OcultarMensajeRibbon(this);
        }

        private void MostrarEstadoCuenta(string codigo)
        {
            var (texto, esValida) = CuentaContableHint.Obtener(codigo);

            if (string.IsNullOrWhiteSpace(texto) || !gridControl1.Enabled)
            {
                FormHelper.OcultarMensajeRibbon(this);
                return;
            }
            FormHelper.MostrarMensajeRibbon(this, texto);            
        }        

        private void AbrirBusquedaCuenta(GridView view)
        {
            var config = new BusquedaConfig
            {
                StoredProcedure = "SP_CATALOGO_CUENTA",
                Columnas = new Dictionary<string, string>
                {
                    { "CUENTA",        "CUENTA" },
                    { "NOMBRE_CUENTA", "NOMBRE" }
                },
                Anchos = new Dictionary<string, int>
                {
                    { "CUENTA",  130 },
                    { "NOMBRE_CUENTA",  400 }
                },
                ParametrosExtra = new { ES_DETALLE = true }
            };

            using (var frm = new frmBusquedaGenerica(config))
            {
                Point posGrid = gridControl1.PointToScreen(new Point(0, gridControl1.Height));
                Rectangle pantalla = Screen.FromControl(gridControl1).WorkingArea;
                int posX = posGrid.X;
                int posY = posGrid.Y;

                if (posY + frm.Height > pantalla.Bottom)
                    posY = posGrid.Y - frm.Height;
                if (posX + frm.Width > pantalla.Right)
                    posX = pantalla.Right - frm.Width;

                frm.StartPosition = FormStartPosition.Manual;
                frm.Location = new Point(posX, posY);

                if (frm.ShowDialog() == DialogResult.OK
                    && frm.FilaSeleccionada != null)
                {
                    view.SetFocusedRowCellValue("CTACONTABLE",
                        frm.FilaSeleccionada["CUENTA"].ToString());

                    view.FocusedColumn = view.Columns["DETALLE"];
                    view.ShowEditor();
                }
                else
                {
                    view.SetFocusedRowCellValue("CTACONTABLE", string.Empty);
                }
            }
        }


        #endregion

        #region Guardar


        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtOPERACION.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Seleccione una operación.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOPERACION.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNUM_CUENTA.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Seleccione una cuenta bancaria.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNUM_CUENTA.Focus();
                return false;
            }

            if (deFECHA_CHEQUE.EditValue == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ingrese una fecha válida.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNUM_CUENTA.Focus();
                return false;
            }                

            DateTime? fecha = deFECHA_CHEQUE.DateTime;
            if (fecha.HasValue)
            {
                if (fecha > DateTime.Today)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "La fecha no puede ser mayor a la fecha actual.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    deFECHA_CHEQUE.Focus();
                    return false;
                }
                //else if (fecha < DateTime.Today.AddDays(-5))
                //{
                //    DevExpress.XtraEditors.XtraMessageBox.Show(
                //        "La fecha no puede ser menor a 5 días.",
                //        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    deFECHA_CHEQUE.Focus();
                //    return false;
                //}
            }

            if (string.IsNullOrWhiteSpace(txtNOMBRE_CHEQUE.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "El nombre del documento es requerido.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNOMBRE_CHEQUE.Focus();
                return false;
            }

            // ✅ Validar que la cantidad sea mayor que cero
            decimal cantidad = ObtenerDecimal(txtCANTIDAD);
            if (cantidad <= 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "La cantidad del documento debe ser mayor que cero.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCANTIDAD.Focus();
                return false;
            }

            // Verificar que haya al menos una línea en la partida
            bool tieneLineas = false;
            foreach (DataRow fila in _dtPartida.Rows)
            {
                if (!string.IsNullOrWhiteSpace(fila["CTACONTABLE"].ToString()))
                {
                    tieneLineas = true;
                    break;
                }
            }

            if (!tieneLineas)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Debe ingresar al menos una línea en la partida contable.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // ✅ Validar que la partida esté cuadrada
            decimal totalCargo = 0;
            decimal totalAbono = 0;
            foreach (DataRow fila in _dtPartida.Rows)
            {
                if (fila["CARGO"] != DBNull.Value)
                    totalCargo += Convert.ToDecimal(fila["CARGO"]);
                if (fila["ABONO"] != DBNull.Value)
                    totalAbono += Convert.ToDecimal(fila["ABONO"]);
            }

            if (totalCargo != totalAbono)
            {
                decimal diferencia = Math.Abs(totalCargo - totalAbono);                
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    $"La partida contable no cuadra.\n\n" +
                    $"Total Cargo: {totalCargo:N2}\n" +
                    $"Total Abono: {totalAbono:N2}\n" +
                    $"Diferencia: {diferencia:N2}",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);                
                return false;
            }     
            
            if(!_flujoEsQuedan && CantidadDocumentosVinculados == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                   "No se han adicionado documentos",
                   "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }       

        private void ActualizarCuadre()
        {
            decimal totalCargo = 0;
            decimal totalAbono = 0;

            foreach (DataRow fila in _dtPartida.Rows)
            {
                // Leer directo como decimal sin pasar por ToString
                if (fila["CARGO"] != DBNull.Value)
                    totalCargo += Convert.ToDecimal(fila["CARGO"]);
                if (fila["ABONO"] != DBNull.Value)
                    totalAbono += Convert.ToDecimal(fila["ABONO"]);
            }

            decimal diferencia = totalCargo - totalAbono;

            lblTOTAL_CARGO.Text = $"{totalCargo:N2}";
            lblTOTAL_ABONO.Text = $"{totalAbono:N2}";
            lblDIFERENCIA.Text = $"Diferencia: {Math.Abs(diferencia):N2}";

            if (totalCargo == 0 && totalAbono == 0)
            {
                lblCUADRE.Text = "○ Sin movimientos";
                lblCUADRE.ForeColor = Color.Gray;
            }
            else if (diferencia == 0)
            {
                lblCUADRE.Text = "✓ Partida cuadrada";
                lblCUADRE.ForeColor = Color.Green;
            }
            else
            {
                lblCUADRE.Text = "⚠ Descuadre";
                lblCUADRE.ForeColor = Color.OrangeRed;
            }
        }

        #endregion

        #region Cancelar y Salir

       
        private void btnFinalizar_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        #endregion

        private void txtCANTIDAD_Leave(object sender, EventArgs e)
        {
            if (FormHelper.EsEscapeDeFoco(this)) return;
            if (string.IsNullOrWhiteSpace(txtCANTIDAD.Text))
            {
                txtCANTIDAD.Text = "0.00";
                return;
            }

            if (decimal.TryParse(txtCANTIDAD.Text, out decimal valor))
            {
                txtCANTIDAD.Text = valor.ToString("N2");

                // Si no hay proveedor asignar en ABONO de la primera fila
                if (string.IsNullOrWhiteSpace(txtPROVEEDOR.Text)
                    && _dtPartida.Rows.Count > 0)
                {
                    string cta = _dtPartida.Rows[0]["CTACONTABLE"].ToString();
                    if (!string.IsNullOrWhiteSpace(cta))
                    {
                        if (_operacionEsCargoAlaCuenta)
                        {
                            _dtPartida.Rows[0]["ABONO"] = valor;
                            _dtPartida.Rows[0]["CARGO"] = 0m;
                        }
                        else
                        {
                            _dtPartida.Rows[0]["ABONO"] = 0m;
                            _dtPartida.Rows[0]["CARGO"] = valor;
                        }
                            
                        ActualizarCuadre();
                    }
                }
            }
            else
            {
                txtCANTIDAD.Text = "0.00";
                txtCANTIDAD.Focus();
            }
        }

        private void txtCANTIDAD_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números, punto decimal y backspace
            if (!char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                e.KeyChar != '\b')
            {
                e.Handled = true;
                return;
            }
            // Permitir solo un punto decimal
            if (e.KeyChar == '.' && txtCANTIDAD.Text.Contains("."))
                e.Handled = true;
        }
        private void txtCANTIDAD_Enter(object sender, EventArgs e)
        {
            txtCANTIDAD.SelectAll();
        }

        private void txtCONCEPTO_Leave(object sender, EventArgs e)
        {
            if (FormHelper.EsEscapeDeFoco(this)) return;
            if (string.IsNullOrWhiteSpace(txtCONCEPTO.Text)) return;
            if (_dtPartida.Rows.Count == 0) return;

            string codOp = txtOPERACION.Text?.Trim().ToUpper() ?? "";
            //if (codOp != "CH" && codOp != "NC") return;

            // Asignar el detalle en la primera fila del grid (cuenta del banco)
            string detalle;
            if (codOp == "CH")
                detalle = $"{codOp} # {txtNUMERO_CHEQUE.Text.Trim()} {txtNOMBRE_CHEQUE.Text.Trim()}";
            else
                detalle = $"{txtNOMBRE_CHEQUE.Text.Trim()}";

            _dtPartida.Rows[0]["DETALLE"] = detalle;
            gridControl1.RefreshDataSource();

            // Determinar a qué celda debe ir el foco
            bool hayProveedor = !string.IsNullOrWhiteSpace(txtPROVEEDOR.Text);
            int filaDestino = hayProveedor ? 1 : 0;
            string colDestino = hayProveedor ? "CTACONTABLE" : "DETALLE";

            // Si va a fila 2 (CTACONTABLE) y no existe, crearla
            if (hayProveedor && _dtPartida.Rows.Count <= filaDestino)
                AgregarFilaVacia();

            if (hayProveedor)
                _columnaAnteriorGrid = "CTACONTABLE";

            // Diferir el cambio de foco al grid
            this.BeginInvoke(new Action(() =>
            {
                var view = gridControl1.MainView as GridView;
                if (view == null) return;

                view.FocusedRowHandle = filaDestino;
                view.FocusedColumn = view.Columns[colDestino];
                view.ShowEditor();
                gridControl1.Focus();
                Application.DoEvents();

                if (view.ActiveEditor != null)
                {
                    if (view.ActiveEditor is DevExpress.XtraEditors.TextEdit textEdit)
                    {
                        textEdit.Focus();
                        textEdit.SelectionStart = 0;
                        textEdit.SelectionLength = 0;
                        textEdit.Select(0, textEdit.Text.Length);
                    }
                }
            }));
        }

        private void btnCCFContado_ItemClick(object sender, EventArgs e)
        {
            AbrirContadoConUid();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            LimpiarFilasVaciasGrid();
            if (!ValidarCampos()) return;

            // Elegir la acción del SP según el estado actual
            bool esModificacion = (_estadoActual == EstadoFormulario.Modificar);

            try
            {
                Cursor = Cursors.WaitCursor;

                if (_dtPartida.Rows.Count == 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Debe ingresar al menos una línea válida en la partida contable.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                // Construir TVP para pago de CCFs de Quedan (puede venir vacío)
                DataTable dtPagoQuedan = ConstruirTvpPagoCcfQuedan();

                // Parámetros del cheque
                var parametros = new
                {
                    ACCION = esModificacion ? "MODIFICAR" : "GUARDAR",
                    ID_CHEQUE = esModificacion ? _idCheque : 0,
                    ID_CTA_BANCO = Convert.ToInt32(txtNUM_CUENTA.Tag ?? 0),
                    NUM_CHEQUE = txtNUMERO_CHEQUE.Text.Trim(),
                    FECHA_CHEQUE = deFECHA_CHEQUE.DateTime,
                    MONTO = ObtenerDecimal(txtCANTIDAD),
                    NOMBRE_CHEQUE = NullIfEmpty(txtNOMBRE_CHEQUE.Text),
                    CONCEPTO = NullIfEmpty(txtCONCEPTO.Text),
                    ID_ENTIDAD = (_flujoEsQuedan && txtPROVEEDOR.Tag != null) ? Convert.ToInt32(txtPROVEEDOR.Tag) : (int?)null,
                    CODIGO_ENTIDAD = NullIfEmpty(txtPROVEEDOR.Text),
                    UID_ENLACE_CHEQUE = _uidEnlaceCheque,
                    USUARIO = Configuracion.UsuarioActual,
                    IMPRESO = 0,
                    TIPO_PARTIDA = NullIfEmpty(txtOPERACION.Text),
                    ES_QUEDAN = _flujoEsQuedan
                };

                // Llamada al SP con dos TVPs
                int idCheque = _dal.EjecutarConsultaConTVPs(
                    "SP_CHEQUE",
                    parametros,
                    ("PARTIDA", "typeCHEQUE_PARTIDA", _dtPartida),
                    ("PAGO_CCF_QUEDAN", "typeCHEQUE_PAGO_CCF_QUEDAN", dtPagoQuedan)
                );

                if (idCheque == 0)
                    throw new Exception("El SP no devolvió el ID generado.");

                _idCheque = idCheque;               

                // Mensaje de éxito según el modo
                string mensaje = esModificacion
                    ? $"Documento <b>N° {txtNUMERO_CHEQUE.Text.Trim()}</b> modificado correctamente."
                    : $"Documento <b>N° {txtNUMERO_CHEQUE.Text.Trim()}</b> guardado correctamente.";

                var args = new XtraMessageBoxArgs
                {
                    Caption = esModificacion ? "Modificado" : "Guardado",
                    Text = mensaje,
                    Buttons = new[] { DialogResult.OK },
                    Icon = SystemIcons.Information,
                    AllowHtmlText = DefaultBoolean.True
                };
                XtraMessageBox.Show(args);
                ConfigurarCRUD(EstadoFormulario.Guardar);
            }
            catch (Exception ex)
            {
                var args = new XtraMessageBoxArgs
                {
                    Caption = "Validación",
                    Text = $"<b>{ex.Message}</b>",
                    Buttons = new[] { DialogResult.OK },
                    Icon = SystemIcons.Warning,
                    AllowHtmlText = DefaultBoolean.True
                };
                XtraMessageBox.Show(args);   
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LimpiarFilasVaciasGrid()
        {
            for (int i = _dtPartida.Rows.Count - 1; i >= 0; i--)
            {
                var fila = _dtPartida.Rows[i];
                string cta = fila["CTACONTABLE"]?.ToString()?.Trim() ?? "";

                if (string.IsNullOrEmpty(cta))   // solo cuenta vacía
                {
                    _dtPartida.Rows.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Construye un DataTable con los IDs de CCFs de Quedan a pagar.
        /// Si no hay documentos seleccionados, retorna un DataTable vacío.
        /// </summary>
        private DataTable ConstruirTvpPagoCcfQuedan()
        {
            var dt = new DataTable();
            dt.Columns.Add("ID_CCF_COMPRA", typeof(int));
            if (_documentosPago == null || _documentosPago.Rows.Count == 0)
                return dt;
            foreach (DataRow fila in _documentosPago.Rows)
            {
                if (fila["ID_CCF_COMPRA"] == DBNull.Value) continue;

                int idCcf = Convert.ToInt32(fila["ID_CCF_COMPRA"]);
                dt.Rows.Add(idCcf);
            }
            return dt;
        }

        private static string NullIfEmpty(string texto)
        {
            return string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
        }

        private decimal ObtenerDecimal(TextBox tb)
        {
            if (string.IsNullOrWhiteSpace(tb.Text)) return 0;
            return decimal.TryParse(tb.Text, out decimal v) ? v : 0;
        }

        // ============================================================
        // NavegarCheque
        // Llama a SP_CHEQUE con la acción ANTERIOR o SIGUIENTE y,
        // si obtiene un ID válido, carga ese cheque.
        //
        // Si el SP no devuelve nada, muestra un mensaje al usuario.
        // ============================================================
        private void NavegarCheque(string accion)
        {
            if (_idCheque == 0)
            {
                XtraMessageBox.Show(
                    "Debe cargar primero un cheque.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNUM_CUENTA.Tag == null || Convert.ToInt32(txtNUM_CUENTA.Tag) == 0)
            {
                XtraMessageBox.Show(
                    "No hay cuenta bancaria seleccionada.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                DataTable dt = _dal.EjecutarConsulta("SP_CHEQUE", new
                {
                    ACCION = accion,
                    ID_CHEQUE = _idCheque,
                    ID_CTA_BANCO = Convert.ToInt32(txtNUM_CUENTA.Tag),
                    TIPO_PARTIDA = txtOPERACION.Text.Trim()
                });

                if (dt.Rows.Count == 0 || dt.Rows[0]["ID_CHEQUE"] == DBNull.Value)
                {
                    string mensaje = accion == "ANTERIOR"
                        ? "No hay más documentos hacia atrás."
                        : "No hay más documentos hacia adelante.";

                    XtraMessageBox.Show(mensaje, "Navegación",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int idChequeDestino = Convert.ToInt32(dt.Rows[0]["ID_CHEQUE"]);
                CargarCheque(idChequeDestino);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error al navegar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnBorrarFila_Click(object sender, EventArgs e)
        {
            BorrarFila();
        }

        private void BorrarFila()
        {
            var view = gridControl1.MainView as GridView;
            if (view == null) return;

            // ============================================================
            // 1) Validar que haya una fila seleccionada
            // ============================================================
            if (view.FocusedRowHandle < 0 || view.RowCount == 0)
            {
                XtraMessageBox.Show("Debe seleccionar una fila para borrar.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cerrar el editor si está abierto para que los valores estén persistidos
            view.CloseEditor();
            view.UpdateCurrentRow();

            int filaActual = view.FocusedRowHandle;
            DataRow fila = view.GetDataRow(filaActual);
            if (fila == null) return;

            // ============================================================
            // 2) Si la fila está vacía, no preguntar — solo eliminar
            // ============================================================
            string cta = fila["CTACONTABLE"]?.ToString()?.Trim() ?? string.Empty;
            string detalle = fila["DETALLE"]?.ToString()?.Trim() ?? string.Empty;
            decimal cargo = fila["CARGO"] == DBNull.Value ? 0 : Convert.ToDecimal(fila["CARGO"]);
            decimal abono = fila["ABONO"] == DBNull.Value ? 0 : Convert.ToDecimal(fila["ABONO"]);

            bool filaVacia = string.IsNullOrWhiteSpace(cta)
                             && string.IsNullOrWhiteSpace(detalle)
                             && cargo == 0
                             && abono == 0;

            // ============================================================
            // 3) Si tiene datos, confirmar antes de borrar
            // ============================================================
            if (!filaVacia)
            {
                var rta = XtraMessageBox.Show(
                    $"¿Está seguro de eliminar la fila seleccionada?\n\n" +
                    $"Cuenta: {cta}\n" +
                    $"Detalle: {detalle}\n" +
                    $"Cargo: {cargo:N2}\n" +
                    $"Abono: {abono:N2}",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rta != DialogResult.Yes) return;
            }

            // ============================================================
            // 4) Borrar la fila del DataTable
            // ============================================================
            _dtPartida.Rows.Remove(fila);

            // ============================================================
            // 5) Asegurar que siempre haya una fila vacía al final para captura
            // ============================================================
            if (_dtPartida.Rows.Count == 0)
            {
                AgregarFilaVacia();
            }
            else
            {
                // Verificar si la última fila está vacía. Si no, agregar una nueva.
                DataRow ultima = _dtPartida.Rows[_dtPartida.Rows.Count - 1];
                string ctaUlt = ultima["CTACONTABLE"]?.ToString()?.Trim() ?? string.Empty;
                decimal cargoUlt = ultima["CARGO"] == DBNull.Value ? 0 : Convert.ToDecimal(ultima["CARGO"]);
                decimal abonoUlt = ultima["ABONO"] == DBNull.Value ? 0 : Convert.ToDecimal(ultima["ABONO"]);

                if (!string.IsNullOrWhiteSpace(ctaUlt) || cargoUlt != 0 || abonoUlt != 0)
                    AgregarFilaVacia();
            }

            // ============================================================
            // 6) Reposicionar el foco
            // ============================================================
            int nuevaFila = Math.Min(filaActual, view.RowCount - 1);
            if (nuevaFila >= 0)
            {
                view.FocusedRowHandle = nuevaFila;
                var col = view.Columns.ColumnByFieldName("CTACONTABLE");
                if (col != null) view.FocusedColumn = col;
            }

            // ============================================================
            // 7) Refrescar totales y cuadre
            // ============================================================
            ActualizarCuadre();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
          
            if (_idCheque == 0)
            {
                XtraMessageBox.Show("Debe guardar el documento antes de imprimir.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                bool marcadoImpreso = chkImpreso.Checked;
                if (!marcadoImpreso)
                {
                    marcadoImpreso = MarcarChequeComoImpreso(_idCheque);
                }

                if (txtOPERACION.Text == "CH" && marcadoImpreso)
                {                    
                    var reporte = new rptCheque { IdCheque = _idCheque };
                    reporte.ImprimirConDialogo();                       
                }
                else 
                {
                    Cursor = Cursors.WaitCursor;
                    var reporte = new RptPartida_Movimiento { _ID_PARTIDA = _idPartidaCheque};
                    reporte.MostrarPreview();
                }
                // Si la impresión fué correcta habilitar la opción de anulación del cheque
                btnAnular.Enabled = true;

                // Mostrar en pantalla el anexo del cheque después de imprimir
                if (_flujoEsQuedan || (_documentosPago != null && _documentosPago.Rows.Count > 0))
                {
                    if ((XtraMessageBox.Show("¿Desea emitir anexo de documentos cancelados?",
                        "Validación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                    {
                        var reporteAnexo = new rptChequeAnexo { IdCheque = _idCheque };
                        reporteAnexo.MostrarPreview();
                    }
                }                                  
                
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

        /// <summary>
        /// Marca el cheque como impreso en la base de datos.
        /// </summary>
        private bool MarcarChequeComoImpreso(int idCheque)
        {
            try
            {
                DataTable dt = _dal.EjecutarConsulta("SP_CHEQUE", new
                {
                    ACCION = "MARCAR_IMPRESO",
                    ID_CHEQUE = idCheque,
                    USUARIO = Configuracion.UsuarioActual
                });
                if(dt.Rows.Count > 0)
                {
                    _idPartidaCheque = dt.Rows[0]["ID_PARTIDA_GENERADA"] == DBNull.Value ? 0 : Convert.ToInt64(dt.Rows[0]["ID_PARTIDA_GENERADA"]);
                }
                chkImpreso.Checked = true;
                return true;
            }
            catch (Exception ex)
            {
                // No bloquear al usuario si falla la marca, pero notificar
                XtraMessageBox.Show(
                    "El documento se imprimió, pero no se pudo marcar como impreso:\n\n" + ex.Message,
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idCheque == 0)
            {
                XtraMessageBox.Show("Debe cargar un documento para eliminarlo.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var resp = XtraMessageBox.Show(
                $"¿Está seguro que desea eliminar el documento N° {txtNUMERO_CHEQUE.Text.Trim()}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resp != DialogResult.Yes) return;

            try
            {
                Cursor = Cursors.WaitCursor;

                DataTable dt = _dal.EjecutarConsulta("SP_CHEQUE", new
                {
                    ACCION = "ELIMINAR",
                    ID_CHEQUE = _idCheque,
                    USUARIO = Configuracion.UsuarioActual
                });

                // El SP retorna si hay CCFs contado y el nuevo UID
                bool tieneContado = false;
                if (dt.Rows.Count > 0)
                {
                    tieneContado = Convert.ToBoolean(dt.Rows[0]["TIENE_CONTADO"]);
                }

                string mensaje = "Documento eliminado correctamente.";
                if (tieneContado)
                {
                    mensaje += "\n\nLos documentos al contado quedaron pendientes. " +
                               "Podrá retomarlos la próxima vez que abra esta pantalla.";
                }

                XtraMessageBox.Show(mensaje, "Eliminado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnIgnorar_Click(sender, e);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al eliminar el documento:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            _idCheque = 0;
            _modoBusqueda = false;
            _ultimoDetalleAutoGenerado = string.Empty;
            _dtPartida.Clear();
            _documentosPago?.Clear(); 
            _flujoEsQuedan = false;
            txtPROVEEDOR.Tag = null; 
            txtNUM_CUENTA.Tag = null;
            FormHelper.LimpiarControles(this);            
            AgregarFilaVacia();            
            deFECHA_CHEQUE.DateTime = DateTime.Today;
            ConfigurarOperacion();
            ActualizarCuadre();
            ConfigurarCRUD(EstadoFormulario.Agregar);
            //if (!VerificarDocumentosHuerfanos())
            //{
            //    _uidEnlaceCheque = FormHelper.ObtenerUUID();               
            //}
            _uidEnlaceCheque = FormHelper.ObtenerUUID();

            // Abrir automáticamente la búsqueda de Tipo de Operación
            this.BeginInvoke(new Action(() =>
            {
                if (ModoCheque)
                    txtNUM_CUENTA.Focus();
                else
                {
                    txtOPERACION.Focus();
                    SendKeys.Send("*{ENTER}");
                }                    
            }));
        }

        private static string SafeStr(object v)
           => v == DBNull.Value || v == null ? "" : v.ToString();

        /// <summary>
        /// Arma el concepto de pago a partir de los documentos seleccionados en el Quedan.
        /// Los que tienen NUM_CONTROL con formato DTE-... aportan su número truncado;
        /// cualquier otro formato se agrupa genéricamente como "CCF".
        /// Ejemplos: "PAGO DE DTE #123, 456"  |  "PAGO DE CCF"  |  "PAGO DE DTE #123, 456 Y CCF"
        /// </summary>
        private string ConstruirConceptoPago(DataTable documentos)
        {
            if (documentos == null || documentos.Rows.Count == 0) return string.Empty;

            var numerosDte = new List<string>();
            bool hayNoDte = false;

            foreach (DataRow fila in documentos.Rows)
            {
                string numControl = fila["NUM_CONTROL"]?.ToString() ?? string.Empty;

                if (numControl.StartsWith("DTE", StringComparison.OrdinalIgnoreCase))
                {
                    string numeroInterno = fila["NUM_CONTROL_INTERNO"]?.ToString();
                    if (!string.IsNullOrWhiteSpace(numeroInterno))
                        numerosDte.Add(numeroInterno);
                }
                else
                {
                    hayNoDte = true;
                }
            }

            var partes = new List<string>();
            if (numerosDte.Count > 0)
                partes.Add("DTE #" + string.Join(", ", numerosDte));
            if (hayNoDte)
                partes.Add("CCF");

            return partes.Count == 0 ? string.Empty : "PAGO DE " + string.Join(" Y ", partes);
        }

        private void btnIgnorar_Click(object sender, EventArgs e)
        {
            var resp = XtraMessageBox.Show(
                "¿Está seguro de descartar los cambios?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (resp == DialogResult.No) return;
            // ============================================================
            // Liberar documentos de contado (CCF + FSE) si estamos en Agregar
            // Los pone con UID_ENLACE_CHEQUE = '' para que queden disponibles
            // para asociarse a un cheque distinto.
            // Solo aplica a documentos con ID_CHEQUE IS NULL (no guardados aún).
            // ============================================================
            if (_estadoActual == EstadoFormulario.Agregar &&
                !string.IsNullOrWhiteSpace(_uidEnlaceCheque))
            {
                try
                {
                    _dal.EjecutarEscalar("SP_CHEQUE_CONTADO", new
                    {
                        ACCION = "LIBERAR_POR_UID",
                        UID_ENLACE_CHEQUE = _uidEnlaceCheque
                    });
                }
                catch (Exception ex)
                {
                    // No bloqueamos el ignorar por un error de liberación.
                    // El mecanismo de huérfanos los detectará en próxima apertura.
                    Logger.Advertencia(
                        $"No se pudieron liberar los documentos del UID {_uidEnlaceCheque}: {ex.Message}",
                        "CHEQUE_CONTADO");
                }
            }
            _idCheque = 0;
            _ultimoDetalleAutoGenerado = string.Empty;
            _dtPartida.Clear();
            _documentosPago?.Clear();
            _flujoEsQuedan = false;
            txtPROVEEDOR.Tag = null; 
            txtNUM_CUENTA.Tag = null;
            txtPROVEEDOR.ReadOnly = false;
            txtCANTIDAD.ReadOnly = false; 
            FormHelper.LimpiarControles(this);                                    
            ActualizarCuadre();
            ConfigurarCRUD(EstadoFormulario.Inicializar);            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            _idCheque = 0;
            _modoBusqueda = true;                        // ← activar modo búsqueda
            _ultimoDetalleAutoGenerado = string.Empty;
            _dtPartida.Clear();
            _documentosPago?.Clear();
            _flujoEsQuedan = false;
            txtPROVEEDOR.Tag = null;
            txtNUM_CUENTA.Tag = null;
            FormHelper.LimpiarControles(this);
            AgregarFilaVacia();            
            ConfigurarOperacion();
            ActualizarCuadre();
            ConfigurarCRUD(EstadoFormulario.Buscar);
            this.BeginInvoke(new Action(() =>
            {
                if (ModoCheque)
                    txtNUM_CUENTA.Focus();
                else
                    txtOPERACION.Focus();  
            }));
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (_idCheque == 0)
            {
                XtraMessageBox.Show(
                    "Debe cargar un documento para anularlo.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmación explícita al usuario
            var resp = XtraMessageBox.Show(
                $"¿Está seguro de anular el documento N° {txtNUMERO_CHEQUE.Text.Trim()}",
                "Anular documento",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resp != DialogResult.Yes) return;

            try
            {
                Cursor = Cursors.WaitCursor;

                _dal.EjecutarEscalar("SP_CHEQUE", new
                {
                    ACCION = "ANULAR",
                    ID_CHEQUE = _idCheque,
                    USUARIO = Configuracion.UsuarioActual
                });

                XtraMessageBox.Show(
                    $"Documento N° {txtNUMERO_CHEQUE.Text.Trim()} anulado correctamente.",
                    "Anulado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Recargar el cheque para reflejar los valores actualizados
                CargarCheque(_idCheque);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "Error al anular el documento:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (_idCheque == 0)
            {
                XtraMessageBox.Show(
                    "Debe cargar un documento para modificarlo.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ConfigurarCRUD(EstadoFormulario.Modificar);

            this.BeginInvoke(new Action(() =>
            {
                txtNOMBRE_CHEQUE.Focus();
                txtNOMBRE_CHEQUE.SelectAll();
            }));
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            NavegarCheque("ANTERIOR");
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            NavegarCheque("SIGUIENTE");
        }

        private void frmCheques_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormHelper.OcultarMensajeRibbon(this);
        }

        private void btnDocumentos_Click(object sender, EventArgs e)
        {
            AbrirContadoConUid();
        }
                
        private void GridView1_ShownEditor(object sender, EventArgs e)
        {
            var view = sender as GridView;
            if (view?.ActiveEditor is TextEdit editor)
            {
                // Solo para columnas de texto (evitar aplicar a numéricas)
                string columna = view.FocusedColumn?.FieldName;
                if (columna == "CTACONTABLE" || columna == "DETALLE")
                {
                    editor.Properties.CharacterCasing = CharacterCasing.Upper;
                }
                // === Columnas numéricas: bloquear doble punto ===
                if (columna == "CARGO" || columna == "ABONO")
                {
                    editor.KeyPress -= EditorMonto_KeyPress;
                    editor.KeyPress += EditorMonto_KeyPress;

                    editor.Validating -= EditorMonto_Validating;
                    editor.Validating += EditorMonto_Validating;
                }
            }
        }

        private void EditorMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            var editor = sender as TextEdit;
            if (editor == null) return;

            char c = e.KeyChar;

            // Permitir teclas de control (backspace, delete, etc.)
            if (char.IsControl(c)) return;

            // Permitir dígitos
            if (char.IsDigit(c)) return;

            // Permitir un solo punto decimal
            if (c == '.')
            {
                if (editor.Text.Contains("."))
                    e.Handled = true;   // ya hay uno, bloquear
                return;
            }

            // Cualquier otra tecla se bloquea
            e.Handled = true;
        }

        private void EditorMonto_Validating(object sender, CancelEventArgs e)
        {
            var editor = sender as TextEdit;
            if (editor == null) return;

            if (decimal.TryParse(editor.Text,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out decimal valor))
            {
                // Redondear a 2 decimales
                decimal redondeado = Math.Round(valor, 2, MidpointRounding.AwayFromZero);
                editor.Text = redondeado.ToString("0.00");
            }
        }

        private void deFECHA_CHEQUE_Leave(object sender, EventArgs e)
        {
            // 1. Escape de foco (Ignorar/Finalizar/etc.)
            if (FormHelper.EsEscapeDeFoco(this)) return;

            // 2. Solo aplicar en modo Agregar
            if (_estadoActual != EstadoFormulario.Agregar) return;

            // 3. Modo búsqueda no aplica
            if (_modoBusqueda) return;

            // 4. Validar que haya fecha
            DateTime? fecha = deFECHA_CHEQUE.DateTime;
            if (!fecha.HasValue)
            {
                txtNUMERO_PARTIDA.Text = "";
                return;
            }

            // 5. Recalcular número de partida sugerido
            try
            {
                string codOperacion = txtOPERACION.Text?.Trim();
                if (string.IsNullOrWhiteSpace(codOperacion))
                {
                    txtNUMERO_PARTIDA.Text = "";                    
                }
                else
                {
                    AsignarNumeroPartidaSugerido();
                }
                FormHelper.EnfocarConDelay(txtPROVEEDOR);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "Error al consultar el número de partida:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void txtOPERACION_Leave(object sender, EventArgs e)
        {
            // Solo validar cuando ModoCheque = false (en modo cheque no hay nada que validar)
            if (ModoCheque) return;
            if (FormHelper.EsEscapeDeFoco(this)) return;
            if (txtOPERACION.ReadOnly) return;
            if (_procesandoLeaveOperacion) return;

            string codigo = txtOPERACION.Text?.Trim().ToUpper();
            if (string.IsNullOrWhiteSpace(codigo))
            {
                txtOPERACION.Tag = null;
                return;
            }
            _procesandoLeaveOperacion = true;
            try
            {
                var dt = _dal.EjecutarConsulta("SP_TIPO_PARTIDA", new
                {
                    ACCION = "OBTENER_POR_CODIGO",
                    CODIGO_PAR = codigo
                });

                if (dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        $"La operación '{codigo}' no es válida.\n\n" +
                        "Valores permitidos: RM, NA, NC, TE, TR.",
                        "Operación no válida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtOPERACION.Tag = null;
                    txtOPERACION.Text = "";
                    this.BeginInvoke(new Action(() => txtOPERACION.Focus()));
                    return;
                }

                // Normalizar texto (por si digitaron en minúsculas) y setear el Tag con el ID
                var fila = dt.Rows[0];
                txtOPERACION.Text = fila["CODIGO_PAR"].ToString();
                bool esCargo = fila["ES_CARGO"] != DBNull.Value && Convert.ToBoolean(fila["ES_CARGO"]);
                SetearDatosOperacion(fila["ID_TIPO_PARTIDA"].ToString(), esCargo);
                txtOPERACION.ReadOnly = true;

                if (_estadoActual == EstadoFormulario.Agregar &&
                    fila["CODIGO_PAR"].ToString().ToUpper() != "CH")
                {
                    txtNUMERO_CHEQUE.Text = "";
                }

                AplicarReglasOperacion();               
                AsignarNumeroPartidaSugerido();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "Error al validar la operación:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _procesandoLeaveOperacion = false;
            }
        }

        /// <summary>
        /// Asigna el número de partida sugerido a txtNUMERO_PARTIDA basándose
        /// en la operación actual (txtOPERACION) y la fecha del cheque (deFECHA_CHEQUE).
        ///
        /// Si la operación o la fecha están vacías, deja el campo en blanco.
        /// </summary>
        private void AsignarNumeroPartidaSugerido()
        {
            // 1) Validar operación
            string codOperacion = txtOPERACION.Text?.Trim();
            if (string.IsNullOrWhiteSpace(codOperacion))
            {
                txtNUMERO_PARTIDA.Text = "";
                return;
            }

            // 2) Validar fecha
            if (deFECHA_CHEQUE.EditValue == null)
            {
                txtNUMERO_PARTIDA.Text = "";
                return;
            }

            DateTime fecha = deFECHA_CHEQUE.DateTime;

            // 3) Consultar numerador
            try
            {
                var infoPartida = NumeradorPartidaHelper.Consultar(
                    codOperacion,
                    fecha.Year,
                    fecha.Month);

                txtNUMERO_PARTIDA.Text = infoPartida.NumSiguienteFormateado;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "Error al consultar el número de partida:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Aplica las reglas de habilitación de campos y flujo según la operación actual.
        /// Se llama después de setear txtOPERACION, para configurar el resto del form.
        ///
        /// Reglas:
        ///   CH → flujo cheque completo (ModoCheque=true).
        ///   NC → flujo proveedor + Quedan/Contado habilitado (ModoCheque=false).
        ///   Otras (RM, NA, TE, TR) → sin flujo proveedor/documentos.
        /// </summary>
        private void AplicarReglasOperacion()
        {
            string codOp = txtOPERACION.Text?.Trim().ToUpper() ?? "";
            bool esCheque = codOp == "CH";
            bool esNotaCargo = codOp == "NC";
            //bool habilitaDocumentos = esCheque || esNotaCargo;
            bool habilitaFlujoProveedor = esCheque || esNotaCargo;

            // ============================================================
            // Campo NUMERO_CHEQUE
            //   CH: se autocalcula en CargarCuentaBanco (ya pasa).
            //   NC y otras: queda vacío y editable, lo llena el usuario.
            // ============================================================
            //if (!esCheque && !esCargaExistente)
            //{
            //        txtNUMERO_CHEQUE.Text = "";
            //    // Editable siempre (en los no-CH el usuario lo llena manualmente)
            //}

            // ============================================================
            // Botón DOCUMENTOS (abre contado): solo CH y NC
            // ============================================================
            //btnDocumentos.Enabled = habilitaDocumentos;

            // ============================================================
            // Flag de flujo proveedor
            // Lo usamos en el callback de txtPROVEEDOR para decidir si
            // ejecutar la lógica de Quedan/Contado o solo dejar el texto.
            // ============================================================
            _habilitaFlujoProveedor = habilitaFlujoProveedor;
        }

        // <summary>
        /// Establece el ID (Tag) y el flag ES_CARGO de la operación actual.
        /// Se llama después de que txtOPERACION tiene el código correcto.
        /// </summary>
        private void SetearDatosOperacion(string idTipoPartida, bool esCargo)
        {
            txtOPERACION.Tag = idTipoPartida;
            _operacionEsCargoAlaCuenta = esCargo;
        }

        private void txtNOMBRE_CHEQUE_Leave(object sender, EventArgs e)
        {
            if (FormHelper.EsEscapeDeFoco(this)) return;
            if (_estadoActual != EstadoFormulario.Agregar &&
                _estadoActual != EstadoFormulario.Modificar) return;

            string codOp = txtOPERACION.Text?.Trim().ToUpper() ?? "";
            if (codOp == "CH") return;

            // Pisa el valor existente del concepto con lo tipeado en NOMBRE
            txtCONCEPTO.Text = txtNOMBRE_CHEQUE.Text?.Trim() ?? "";
        }
    }

    
}
