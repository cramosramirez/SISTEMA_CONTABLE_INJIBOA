using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Globalization;
using DevExpress.XtraEditors;
using ComboBox = System.Windows.Forms.ComboBox;

namespace SistemaContable.UI.Forms.Proveedores
{
    public partial class frmDocumentoCompra : Form
    {
        #region Campos privados

        private enum EstadoFormulario
        {
            Nuevo,
            Guardado,
            Validado
        } 

        private readonly DALBase _dal = new DALBase();
        private MinisterioHaciendaHelper _mhHelper;
        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(10)
        };

        private int _idEntidad = 0;
        private string _codigoEntidad = string.Empty;        
        private string _idTipoContribProveedor = "";
        private string _idTipoPersona = "";
        private bool _validarCompIVAR = false;

        public int IdCcfCompra { get; set; } = 0;

        #endregion
        public frmDocumentoCompra()
        {
            InitializeComponent();
        }


        private void frmDocumentoCompra_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);
            InicializarHelperMinisterioHacienda();
            CargarCombos();
            cbxSUCURSAL.SelectedValue = 1;
            // Cargar de Combos Reuqeridos por MH
            CargarTipoServicio();   // Independiente 
            CargarTipoOperacion();  // Independiente (al cambiar dispara cascada)
            CargarTipoRenta();
            LimpiarCombo(cbxCLASIFICACION, "ID_CLASIFICA");
            LimpiarCombo(cbxSECTOR, "ID_SECTOR");
            LimpiarCombo(cbxTIPO_COSTO, "ID_TIPO_COSTO");

            // Búsqueda * + Enter en txtPROVEEDOR
            FormHelper.RegistrarBusqueda(
                txtPROVEEDOR,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_ENTIDAD",
                    Accion = "BUSCAR",
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
                fila => AsignarProveedor(fila)
            );
            txtPROVEEDOR.Leave += txtPROVEEDOR_Leave;
            txtCONSULTA_MH.Leave += txtCONSULTA_MH_Leave;

            ConfigurarTextBoxDecimal(
                txtGRAVADA, txtEXENTA, txtEXCLUIDO, txtPERCEPCION,
                txtIVA, txtFOVIAL, txtCONTRANS, txtTOTAL,
                txtCARGO, txtABONO, txtAPLICABLE_RENTA, txtRENTA,
                txtIVAR, txtSALDO
            );        


            // GRAVADA y EXENTA: handlers que calculan APLICABLE_RENTA/RENTA y luego recalculan totales
            txtGRAVADA.Leave += txtBaseRenta_Leave;
            txtEXENTA.Leave += txtBaseRenta_Leave;
            txtAPLICABLE_RENTA.Leave += txtAplicableRenta_Leave;

            EngancharRecalculo(
                txtEXCLUIDO, txtPERCEPCION, txtFOVIAL, txtCONTRANS,
                txtCARGO, txtABONO
            );

            if (IdCcfCompra > 0)
            {
                CargarCcfExistente(IdCcfCompra);
                ConfigurarCRUD(EstadoFormulario.Guardado);
                
            }                
            else
            {
                CargarSiguienteNumQuedan();
                ConfigurarCRUD(EstadoFormulario.Nuevo);                
            }
                
        }

        private void ConfigurarCRUD(EstadoFormulario estado)
        {

            switch (estado)
            {
                case EstadoFormulario.Nuevo:
                    txtPROVEEDOR.Enabled = true; 
                    btnGuardar.Enabled = true;
                    btnValidar.Enabled = false;
                    btnAdicionar.Enabled = false;
                    btnImprimirQuedan.Enabled = false;
                    btnImprimirRetencion.Enabled = false;
                    btnCorreo.Enabled = false;
                    btnProvision.Enabled = false;
                    break;
                case EstadoFormulario.Guardado:
                    txtPROVEEDOR.Enabled = false;
                    btnGuardar.Enabled = true;
                    btnValidar.Enabled = _validarCompIVAR;
                    btnAdicionar.Enabled = _codigoEntidad.Equals(Configuracion.CodigoCCJIBOA); // solo para CC Jiboa
                    btnImprimirQuedan.Enabled = true;
                    btnImprimirRetencion.Enabled = (ObtenerDecimal(txtIVAR) > 0);
                    btnCorreo.Enabled = false;
                    btnProvision.Enabled = true;
                    break;
                case EstadoFormulario.Validado:
                    txtPROVEEDOR.Enabled = false;
                    btnGuardar.Enabled = true;
                    btnValidar.Enabled = false;
                    btnAdicionar.Enabled = false;
                    btnImprimirQuedan.Enabled = true;
                    btnImprimirRetencion.Enabled = (ObtenerDecimal(txtIVAR) > 0);
                    btnCorreo.Enabled = true;
                    btnProvision.Enabled = true;
                    break;
            }
        }

        private void CargarCcfExistente(int idCcfCompra)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                DataTable dt = _dal.EjecutarConsulta("SP_CREDITO_FISCAL_COMPRA",
                    new
                    {
                        ACCION = "OBTENER",
                        ID_CCF_COMPRA = idCcfCompra
                    });

                if (dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No se encontró el documento solicitado.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    return;
                }

                DataRow r = dt.Rows[0];

                // ---------- Estado del form ----------                
                _idQuedanActual = Convert.ToInt32(r["ID_QUEDAN"]);

                // ---------- Quedan ----------
                lblNUM_QUEDAN.Text = r["NUM_QUEDAN"].ToString();

                // ---------- Proveedor ----------
                _idEntidad = Convert.ToInt32(r["ID_ENTIDAD"]);
                _idTipoPersona = r["ID_TIPO_ENTIDAD"].ToString();
                _codigoEntidad = r["CODIGO_ENTIDAD"].ToString();
                _idTipoContribProveedor = r["ID_TIPO_CONTRIB"] == DBNull.Value
                                          ? "0"
                                          : r["ID_TIPO_CONTRIB"].ToString();
                _validarCompIVAR = Convert.ToInt32(r["VALIDAR_COMPIVAR"]) == 1 ? true : false; 
                txtPROVEEDOR.Text = _codigoEntidad; 
                txtNOMBRE_PROVEEDOR.Text = r["NOMBRE_ENTIDAD"]?.ToString();
                txtNRC.Text = r["NRC"].ToString();
                txtNIT.Text = r["NIT"].ToString();
                txtTELEFONO.Text = r["CELULAR"].ToString();
                txtCORREO.Text = r["CORREO"].ToString();
                txtACTIVIDAD_PRIMARIA.Text = r["ACTIVIDAD_PRIMARIA"].ToString();
                txtTIPO_CONTRIBUYENTE.Text = r["TIPO_CONTRIBUYENTE"].ToString();
                txtDIRECCION.Text = r["COMPLEMENTO"].ToString();

                // ---------- Documento fiscal ----------
                cbxTIPO_DTE.SelectedValue = Convert.ToInt32(r["ID_TIPO_DTE"]);
                txtNUM_CONTROL.Text = AsString(r["NUM_CONTROL"]);
                txtCOD_GENERACION.Text = AsString(r["COD_GENERACION"]);
                txtSELLO_RECIBIDO.Text = AsString(r["SELLO_RECIBIDO"]);
                mskFECHA_EMISION.Text = AsFecha(r["FECHA_EMISION"]);
                mskFECHA_RECIBIDO.Text = AsFecha(r["FECHA_RECIBIDO"]);
                mskFECHA_VENCE.Text = AsFecha(r["FECHA_VENCE"]);
                txtORDEN.Text = AsString(r["ORDEN"]);

                cbxSUCURSAL.SelectedValue = Convert.ToInt32(r["ID_SUCURSAL"]);

                // ---------- Cascada de clasificación ----------
                // IMPORTANTE: orden estricto. Cargar el combo hijo con el filtro
                // del padre antes de asignarle el SelectedValue.
                int idTipoServi = ToInt(r["ID_TIPO_SERVI"]);
                int idTipoOpera = ToInt(r["ID_TIPO_OPERA"]);
                int idClasifica = ToInt(r["ID_CLASIFICA"]);
                int idSector = ToInt(r["ID_SECTOR"]);
                int idTipoCosto = ToInt(r["ID_TIPO_COSTO"]);

                // Independientes
                cbxTIPO_SERVICIO.SelectedValue = idTipoServi;
                cbxTIPO_OPERACION.SelectedValue = idTipoOpera;

                // Dependientes (cargar + asignar)
                CargarClasificacion(idTipoOpera);
                cbxCLASIFICACION.SelectedValue = idClasifica;

                CargarSector(idTipoOpera, idClasifica);
                cbxSECTOR.SelectedValue = idSector;

                CargarTipoCosto(idTipoOpera, idClasifica, idSector);
                cbxTIPO_COSTO.SelectedValue = idTipoCosto;

                // ---------- Tipo de renta ----------
                if (r["ID_TIPO_RENTA"] != DBNull.Value)
                    cbxTIPO_RENTA.SelectedValue = Convert.ToInt32(r["ID_TIPO_RENTA"]);

                // ---------- Montos ----------
                AsignarDecimal(txtGRAVADA, ToDecimal(r["GRAVADA"]));
                AsignarDecimal(txtEXENTA, ToDecimal(r["EXENTA"]));
                AsignarDecimal(txtEXCLUIDO, ToDecimal(r["NO_SUJETA"]));
                AsignarDecimal(txtPERCEPCION, ToDecimal(r["PERCEPCION"]));
                AsignarDecimal(txtIVA, ToDecimal(r["IVA"]));
                AsignarDecimal(txtFOVIAL, ToDecimal(r["FOVIAL"]));
                AsignarDecimal(txtCONTRANS, ToDecimal(r["COTRANS"]));
                AsignarDecimal(txtTOTAL, ToDecimal(r["TOTAL"]));
                AsignarDecimal(txtCARGO, ToDecimal(r["CARGO"]));
                AsignarDecimal(txtABONO, ToDecimal(r["ABONO"]));
                AsignarDecimal(txtAPLICABLE_RENTA, ToDecimal(r["APLICABLE_RENTA"]));
                AsignarDecimal(txtRENTA, ToDecimal(r["RENTA"]));
                AsignarDecimal(txtIVAR, ToDecimal(r["IVAR"]));
                AsignarDecimal(txtSALDO, ToDecimal(r["SALDO"]));

                // ---------- Observación ----------
                txtOBSERVACION.Text = AsString(r["OBSERVACION"]);             
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar el documento:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void CargarSiguienteNumQuedan()
        {
            var num = _dal.ObtenerNumeracionPrevia("QD",null);   // sin año -> corrido
            if (num == null)
            {
                MessageBox.Show(
                    "No existe numeración activa para el Quedan (QD).\n" +
                    "Configúrela en DOCUMENTO_NUMERACION antes de continuar.",
                    "Numeración no encontrada",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblNUM_QUEDAN.Text = "";
                return;
            }
            lblNUM_QUEDAN.Text = num.SiguienteNumeroFormateado();
        }

        #region Carga de combos

        private void CargarCombos()
        {
            CargarCombo("SP_TIPO_DTE", "BUSCAR_FISCAL_COMPRAS_CREDITO", "ID_TIPO_DTE", "ABREVIATURA", cbxTIPO_DTE);                          
            CargarCombo("SP_SUCURSAL", "BUSCAR", "ID_SUCURSAL", "NOMBRE", cbxSUCURSAL);         
        }

        private void CargarCombo(string sp, string accion, string valueMember,
            string displayMember, ComboBox combo)
        {
            try
            {
                var dt = _dal.EjecutarConsulta(sp, new { ACCION = accion });
                var filaVacia = dt.NewRow();
                filaVacia[valueMember] = -1;
                filaVacia[displayMember] = "-- Seleccione --";
                dt.Rows.InsertAt(filaVacia, 0);

                combo.DataSource = dt;
                combo.ValueMember = valueMember;
                combo.DisplayMember = displayMember;
            }
            catch { }
        }

        private void CargarTipoServicio()
        {
            DataTable dt = _dal.EjecutarConsulta("SP_COMPRA_TIPO_SERVICIO",
                new { ACCION = "COMBO" });

            InsertarFilaSeleccione(dt, "ID_TIPO_SERVI");

            cbxTIPO_SERVICIO.DataSource = dt;
            cbxTIPO_SERVICIO.ValueMember = "ID_TIPO_SERVI";
            cbxTIPO_SERVICIO.DisplayMember = "NOMBRE";
        }
        private void CargarTipoOperacion()
        {
            DataTable dt = _dal.EjecutarConsulta("SP_COMPRA_TIPO_OPERACION",
                new { ACCION = "COMBO" });

            InsertarFilaSeleccione(dt, "ID_TIPO_OPERA");
            cbxTIPO_OPERACION.DataSource = dt;
            cbxTIPO_OPERACION.ValueMember = "ID_TIPO_OPERA";
            cbxTIPO_OPERACION.DisplayMember = "NOMBRE";
        }
        private void CargarClasificacion(int idTipoOpera)
        {
            DataTable dt = _dal.EjecutarConsulta("SP_COMPRA_CLASIFICACION",
                new
                {
                    ACCION = "COMBO",
                    ID_TIPO_OPERA = idTipoOpera
                });
            InsertarFilaSeleccione(dt, "ID_CLASIFICA");
            cbxCLASIFICACION.DataSource = dt;
            cbxCLASIFICACION.ValueMember = "ID_CLASIFICA";
            cbxCLASIFICACION.DisplayMember = "NOMBRE";
        }
        private void CargarSector(int idTipoOpera, int idClasifica)
        {
            DataTable dt = _dal.EjecutarConsulta("SP_COMPRA_SECTOR",
                new
                {
                    ACCION = "COMBO",
                    ID_TIPO_OPERA = idTipoOpera,
                    ID_CLASIFICA = idClasifica
                });
            InsertarFilaSeleccione(dt, "ID_SECTOR");
            cbxSECTOR.DataSource = dt;
            cbxSECTOR.ValueMember = "ID_SECTOR";
            cbxSECTOR.DisplayMember = "NOMBRE";
        }
        private void CargarTipoCosto(int idTipoOpera, int idClasifica, int idSector)
        {
            DataTable dt = _dal.EjecutarConsulta("SP_COMPRA_TIPO_COSTO",
                new
                {
                    ACCION = "COMBO",
                    ID_TIPO_OPERA = idTipoOpera,
                    ID_CLASIFICA = idClasifica,
                    ID_SECTOR = idSector
                });
            InsertarFilaSeleccione(dt, "ID_TIPO_COSTO");
            cbxTIPO_COSTO.DataSource = dt;
            cbxTIPO_COSTO.ValueMember = "ID_TIPO_COSTO";
            cbxTIPO_COSTO.DisplayMember = "NOMBRE";
        }

        private void CargarTipoRenta()
        {
            DataTable dt = _dal.EjecutarConsulta("SP_TIPO_RENTA",
                new
                {
                    ACCION = "COMBO",
                    QUEDAN = true        // filtra por QUEDAN = 1
                });            
            cbxTIPO_RENTA.DataSource = dt;
            cbxTIPO_RENTA.ValueMember = "ID_TIPO_RENTA";
            cbxTIPO_RENTA.DisplayMember = "DESCRIPCION";
        }

        private void InsertarFilaSeleccione(DataTable dt, string idField)
        {
            DataRow fila = dt.NewRow();
            fila[idField] = -1;
            fila["CODIGO"] = "";
            fila["NOMBRE"] = "-- Seleccione --";
            dt.Rows.InsertAt(fila, 0);
        }
        private void LimpiarCombo(ComboBox cbx, string idField)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(idField, typeof(int));
            dt.Columns.Add("CODIGO", typeof(string));
            dt.Columns.Add("NOMBRE", typeof(string));
            DataRow fila = dt.NewRow();
            fila[idField] = -1;
            fila["CODIGO"] = "";
            fila["NOMBRE"] = "-- Seleccione --";
            dt.Rows.Add(fila);
            cbx.DataSource = dt;
            cbx.ValueMember = idField;
            cbx.DisplayMember = "NOMBRE";
        }
        private bool TieneSeleccion(ComboBox cbx)
        {
            if (cbx.SelectedValue == null) return false;
            if (cbx.SelectedValue == DBNull.Value) return false;
            if (cbx.SelectedValue is DataRowView) return false;
            return true;
        }       
        private void cbxCLASIFICACION_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarCombo(cbxSECTOR, "ID_SECTOR");
            LimpiarCombo(cbxTIPO_COSTO, "ID_TIPO_COSTO");
            if (!TieneSeleccion(cbxCLASIFICACION)) return;
            if (!TieneSeleccion(cbxTIPO_OPERACION)) return;
            int idTipoOpera = Convert.ToInt32(cbxTIPO_OPERACION.SelectedValue);
            int idClasifica = Convert.ToInt32(cbxCLASIFICACION.SelectedValue);
            CargarSector(idTipoOpera, idClasifica);
        }     
        #endregion

        private void txtPROVEEDOR_Leave(object sender, EventArgs e)
        {
            string codigo = txtPROVEEDOR.Text.Trim();
            if (codigo == "*") return;
            if (string.IsNullOrWhiteSpace(codigo))
            {
                LimpiarProveedor();
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
                    AsignarProveedor(dt.Rows[0]);
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
        private void AsignarProveedor(DataRow fila)
        {
            _idEntidad = Convert.ToInt32(fila["ID_ENTIDAD"]);
            _codigoEntidad = fila["CODIGO_ENTIDAD"].ToString();
            txtPROVEEDOR.Text = fila["CODIGO_ENTIDAD"].ToString();
            txtNOMBRE_PROVEEDOR.Text = fila["NOMBRE"].ToString();
            txtNRC.Text = fila["NRC"].ToString();
            txtNIT.Text = fila["NIT"].ToString();
            txtTELEFONO.Text = fila["CELULAR"].ToString();
            txtCORREO.Text = fila["CORREO"].ToString();
            txtACTIVIDAD_PRIMARIA.Text = fila["ACTIVIDAD_PRIMARIA"].ToString();
            txtTIPO_CONTRIBUYENTE.Text = fila["TIPO_CONTRIBUYENTE"].ToString(); 
            txtDIRECCION.Text = fila["COMPLEMENTO"].ToString();
            _idTipoContribProveedor = fila["ID_TIPO_CONTRIB"].ToString();
            _idTipoPersona = fila["ID_TIPO_ENTIDAD"].ToString();
            RecalcularTotales();
        }
        private void LimpiarProveedor()
        {
            _idEntidad = 0;
            _codigoEntidad = string.Empty;
            txtNOMBRE_PROVEEDOR.Text = string.Empty;
            txtNRC.Text = string.Empty;
            txtNIT.Text = string.Empty;
            txtTELEFONO.Text = string.Empty;
            txtCORREO.Text = string.Empty;
            txtACTIVIDAD_PRIMARIA.Text = string.Empty;
            txtTIPO_CONTRIBUYENTE.Text = string.Empty;
            txtDIRECCION.Text = string.Empty;
        }

        private void txtCONSULTA_MH_Leave(object sender, EventArgs e)
        {
            _mhHelper.OnTxtConsultaLeave(txtCONSULTA_MH.Text);
        }
       
        private void CargarComboFiltrado(int idClasifica)
        {
            try
            {
                var dt = _dal.EjecutarConsulta("SP_COMPRA_TIPO_COSTO", new
                {
                    ACCION = "BUSCAR_POR_CLASIFICACION",
                    ID_CLASIFICA = idClasifica
                });

                var filaVacia = dt.NewRow();
                filaVacia["ID_TIPO_COSTO"] = DBNull.Value;
                filaVacia["NOMBRE"] = "-- Seleccione --";
                dt.Rows.InsertAt(filaVacia, 0);

                cbxTIPO_COSTO.DataSource = dt;
                cbxTIPO_COSTO.ValueMember = "ID_TIPO_COSTO";
                cbxTIPO_COSTO.DisplayMember = "NOMBRE";
            }
            catch { }
        }

        private void cbxTIPO_OPERACION_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LimpiarCombo(cbxCLASIFICACION, "ID_CLASIFICA");
            LimpiarCombo(cbxSECTOR, "ID_SECTOR");
            LimpiarCombo(cbxTIPO_COSTO, "ID_TIPO_COSTO");
            if (!TieneSeleccion(cbxTIPO_OPERACION)) return;
            int idTipoOpera = Convert.ToInt32(cbxTIPO_OPERACION.SelectedValue);
            CargarClasificacion(idTipoOpera);
        }

        private void cbxCLASIFICACION_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LimpiarCombo(cbxSECTOR, "ID_SECTOR");
            LimpiarCombo(cbxTIPO_COSTO, "ID_TIPO_COSTO");
            if (!TieneSeleccion(cbxCLASIFICACION)) return;
            if (!TieneSeleccion(cbxTIPO_OPERACION)) return;
            int idTipoOpera = Convert.ToInt32(cbxTIPO_OPERACION.SelectedValue);
            int idClasifica = Convert.ToInt32(cbxCLASIFICACION.SelectedValue);
            CargarSector(idTipoOpera, idClasifica);
        }

        private void cbxSECTOR_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LimpiarCombo(cbxTIPO_COSTO, "ID_TIPO_COSTO");
            if (!TieneSeleccion(cbxSECTOR)) return;
            if (!TieneSeleccion(cbxCLASIFICACION)) return;
            if (!TieneSeleccion(cbxTIPO_OPERACION)) return;
            int idTipoOpera = Convert.ToInt32(cbxTIPO_OPERACION.SelectedValue);
            int idClasifica = Convert.ToInt32(cbxCLASIFICACION.SelectedValue);
            int idSector = Convert.ToInt32(cbxSECTOR.SelectedValue);
            CargarTipoCosto(idTipoOpera, idClasifica, idSector);
        }


        //********************************************************************************************
        private void ConfigurarTextBoxDecimal(params TextBox[] textboxes)
        {
            foreach (var tb in textboxes)
            {
                tb.Enter += TxtDecimal_Enter;
                tb.KeyPress += TxtDecimal_KeyPress;
                tb.Leave += TxtDecimal_Leave;
            }
        }

        private void EngancharRecalculo(params TextBox[] textboxes)
        {
            foreach (var tb in textboxes)
                tb.Leave += (s, e) => RecalcularTotales();
        }
        private void TxtDecimal_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void TxtDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;            
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
            {
                e.Handled = true;
                return;
            }
            // Solo un punto decimal
            if (e.KeyChar == '.' && tb.Text.Contains("."))
                e.Handled = true;
        }
        private void TxtDecimal_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;

            // Vacío -> limpio
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "";
                return;
            }
            // Parsea (si no parsea, limpia)
            if (!decimal.TryParse(tb.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
            {
                tb.Text = "";
                return;
            }
            // Cero -> limpio (cubre "0", "0.", "0.0", "0.00", "00", etc.)
            if (valor == 0m)
            {
                tb.Text = "";
                return;
            }
            // Valor válido -> formato N2
            tb.Text = valor.ToString("N2");
        }

        // Solo GRAVADA y EXENTA recalculan APLICABLE_RENTA y RENTA.
        // Después de eso, todo lo demás (TOTAL, SALDO, IVA, IVAR) sigue su curso normal.
        private void txtBaseRenta_Leave(object sender, EventArgs e)
        {
            if (ObtenerIdTipoRenta() != 0)
            {
                decimal aplicable = ObtenerDecimal(txtGRAVADA) + ObtenerDecimal(txtEXENTA);
                decimal renta = Math.Round(aplicable * ObtenerValorTipoRenta(), 2);

                AsignarDecimal(txtAPLICABLE_RENTA, aplicable);
                AsignarDecimal(txtRENTA, renta);
            }
            else
            {
                AsignarDecimal(txtAPLICABLE_RENTA, 0);
                AsignarDecimal(txtRENTA, 0);
            }

            RecalcularTotales();
        }

        // El usuario sobreescribe APLICABLE_RENTA -> recalcular solo la RENTA en base a eso
        private void txtAplicableRenta_Leave(object sender, EventArgs e)
        {
            if (ObtenerIdTipoRenta() != 0)
            {
                decimal aplicable = ObtenerDecimal(txtAPLICABLE_RENTA);
                decimal renta = Math.Round(aplicable * ObtenerValorTipoRenta(), 2);
                AsignarDecimal(txtRENTA, renta);
            }

            RecalcularTotales();
        }

        private decimal ObtenerDecimal(TextBox tb)
        {
            if (string.IsNullOrWhiteSpace(tb.Text)) return 0;
            return decimal.TryParse(tb.Text, out decimal v) ? v : 0;
        }
        private decimal ObtenerValorTipoRenta()
        {
            if (cbxTIPO_RENTA.SelectedItem is DataRowView fila)
            {
                object v = fila["VALOR"];
                if (v != null && v != DBNull.Value)
                    return Convert.ToDecimal(v);
            }
            return 0;
        }
        private int ObtenerIdTipoRenta()
        {
            if (cbxTIPO_RENTA.SelectedValue == null) return 0;
            if (cbxTIPO_RENTA.SelectedValue == DBNull.Value) return 0;
            if (cbxTIPO_RENTA.SelectedValue is DataRowView) return 0;
            return Convert.ToInt32(cbxTIPO_RENTA.SelectedValue);
        }

        /// <summary>Asigna un decimal a un TextBox; si es 0 lo deja limpio</summary>
        private void AsignarDecimal(TextBox tb, decimal valor)
        {
            tb.Text = valor == 0 ? "" : valor.ToString("N2");
        }
        private void RecalcularTotales()
        {
            decimal gravada = ObtenerDecimal(txtGRAVADA);
            decimal exenta = ObtenerDecimal(txtEXENTA);
            decimal excluido = ObtenerDecimal(txtEXCLUIDO);
            decimal percepcion = ObtenerDecimal(txtPERCEPCION);
            decimal fovial = ObtenerDecimal(txtFOVIAL);
            decimal contrans = ObtenerDecimal(txtCONTRANS);
            decimal cargo = ObtenerDecimal(txtCARGO);
            decimal abono = ObtenerDecimal(txtABONO);
            decimal renta = ObtenerDecimal(txtRENTA);   // lee, no calcula
            // IVA = 13% de la base gravada
            decimal iva = Math.Round(gravada * 0.13m, 2);
            // IVAR (1%): solo si gravada >= 100 y proveedor NO es Gran Contribuyente
            decimal ivar = 0;
            bool retieneIva = gravada >= 100m
                              && _idTipoContribProveedor != "3"
                              && _idTipoContribProveedor != "0";
            if (retieneIva)
                ivar = Math.Round(gravada * 0.01m, 2);

            decimal total = gravada + exenta + excluido + percepcion + iva + fovial + contrans;
            decimal saldo = total - cargo - abono - renta - ivar;

            AsignarDecimal(txtIVA, iva);
            AsignarDecimal(txtIVAR, ivar);
            AsignarDecimal(txtTOTAL, total);
            AsignarDecimal(txtSALDO, saldo);
        }

        private void mskFECHA_RECIBIDO_Leave(object sender, EventArgs e)
        {
            if (DateTime.TryParseExact(mskFECHA_RECIBIDO.Text, "dd/MM/yyyy",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecha))
            {
                mskFECHA_VENCE.Text = fecha.AddDays(30).ToString("dd/MM/yyyy");
            }
        }

        private void cbxTIPO_SERVICIO_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!(cbxTIPO_SERVICIO.SelectedItem is DataRowView fila)) return;
            object valorRenta = fila["ID_TIPO_RENTA"];
            if (valorRenta == DBNull.Value || _idTipoPersona.Equals("2"))
                cbxTIPO_RENTA.SelectedIndex = 0;
            else
                cbxTIPO_RENTA.SelectedValue = Convert.ToInt32(valorRenta);
            txtBaseRenta_Leave(sender, e);
            //RecalcularTotales();   // refleja el cambio de renta sugerida
        }

        private void cbxTIPO_RENTA_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ObtenerIdTipoRenta() != 0)
            {
                decimal aplicable = ObtenerDecimal(txtAPLICABLE_RENTA);
                decimal renta = Math.Round(aplicable * ObtenerValorTipoRenta(), 2);
                AsignarDecimal(txtRENTA, renta);
            }
            else
            {
                AsignarDecimal(txtRENTA, 0);
            }

            RecalcularTotales();
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        // ================================================================
        //  frmDocumentoCompra - Operación GUARDAR (atómica vía SP)
        //
        //  El SP_CREDITO_FISCAL_COMPRA.GUARDAR maneja internamente:
        //    - Si @ID_CCF_COMPRA = 0 y @ID_QUEDAN = 0  -> crea Quedan + CCF
        //    - Si @ID_CCF_COMPRA = 0 y @ID_QUEDAN > 0  -> solo CCF (mismo Quedan)
        //    - Si @ID_CCF_COMPRA > 0                   -> UPDATE del CCF
        //
        //  El front se reduce a UNA sola llamada. Ya no hay rollback manual.
        // ================================================================


        #region === VARIABLES DE ESTADO ===

        // ID del Quedan al que pertenece la operación actual.
        //   0  = el SP lo creará al guardar el primer CCF
        //  > 0 = ya existe (se reutiliza para CCFs adicionales del mismo proveedor)
        private int _idQuedanActual = 0;

        #endregion



        #region === VALIDACIONES ===

        private bool ValidarCampos()
        {
            if (_idEntidad == 0)
            {
                XtraMessageBox.Show("Debe seleccionar un proveedor.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPROVEEDOR.Focus();
                return false;
            }

            if (!TieneSeleccion(cbxTIPO_DTE))
            {
                XtraMessageBox.Show("Debe seleccionar el tipo de documento.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxTIPO_DTE.Focus();
                return false;
            }

            if (!TieneSeleccion(cbxSUCURSAL))
            {
                XtraMessageBox.Show("Debe seleccionar la sucursal.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxSUCURSAL.Focus();
                return false;
            }

            if (!DateTime.TryParseExact(mskFECHA_EMISION.Text, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                XtraMessageBox.Show("Ingrese una fecha de emisión válida.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskFECHA_EMISION.Focus();
                return false;
            }

            if (!DateTime.TryParseExact(mskFECHA_RECIBIDO.Text, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                XtraMessageBox.Show("Ingrese una fecha de recibido válida.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskFECHA_RECIBIDO.Focus();
                return false;
            }

            // Cascada de clasificación
            if (!TieneSeleccion(cbxTIPO_SERVICIO)) { AvisoValidacion(cbxTIPO_SERVICIO, "tipo de servicio"); return false; }
            if (!TieneSeleccion(cbxTIPO_OPERACION)) { AvisoValidacion(cbxTIPO_OPERACION, "tipo de operación"); return false; }
            if (!TieneSeleccion(cbxCLASIFICACION)) { AvisoValidacion(cbxCLASIFICACION, "clasificación"); return false; }
            if (!TieneSeleccion(cbxSECTOR)) { AvisoValidacion(cbxSECTOR, "sector"); return false; }
            if (!TieneSeleccion(cbxTIPO_COSTO)) { AvisoValidacion(cbxTIPO_COSTO, "tipo de costo"); return false; }

            if (ObtenerDecimal(txtTOTAL) <= 0)
            {
                XtraMessageBox.Show("El total del documento debe ser mayor a cero.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGRAVADA.Focus();
                return false;
            }

            return true;
        }

        private void AvisoValidacion(System.Windows.Forms.ComboBox cbx, string nombre)
        {
            XtraMessageBox.Show($"Debe seleccionar el {nombre}.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            cbx.Focus();
        }

        #endregion


        #region === GUARDADO (una sola llamada al SP) ===

        private void GuardarDocumento()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var parametros = new
                {
                    ACCION = "GUARDAR",
                    ID_CCF_COMPRA = IdCcfCompra,
                    ID_QUEDAN = _idQuedanActual,          // 0 -> el SP creará el Quedan
                    NUM_QUEDAN = (int?)Convert.ToInt32(lblNUM_QUEDAN.Text),
                    ID_TIPO_DTE = ObtenerIdCombo(cbxTIPO_DTE),                    
                    NUM_CONTROL = NullIfEmpty(txtNUM_CONTROL.Text),
                    COD_GENERACION = NullIfEmpty(txtCOD_GENERACION.Text),
                    SELLO_RECIBIDO = NullIfEmpty(txtSELLO_RECIBIDO.Text),
                    FECHA_EMISION = ParsearFecha(mskFECHA_EMISION.Text),
                    TIPO_MONEDA = "USD",
                    ID_ENTIDAD = _idEntidad,
                    CODIGO_ENTIDAD = _codigoEntidad,
                    FECHA_RECIBIDO = ParsearFecha(mskFECHA_RECIBIDO.Text),
                    FECHA_VENCE = ParsearFechaOpcional(mskFECHA_VENCE.Text),
                    ORDEN = NullIfEmpty(txtORDEN.Text),
                    ID_SUCURSAL = Convert.ToInt32(cbxSUCURSAL.SelectedValue),
                    ID_TIPO_SERVI = ObtenerIdCombo(cbxTIPO_SERVICIO),
                    ID_TIPO_OPERA = ObtenerIdCombo(cbxTIPO_OPERACION),
                    ID_CLASIFICA = ObtenerIdCombo(cbxCLASIFICACION),
                    ID_SECTOR = ObtenerIdCombo(cbxSECTOR),
                    ID_TIPO_COSTO = ObtenerIdCombo(cbxTIPO_COSTO),
                    NO_SUJETA = ObtenerDecimal(txtEXCLUIDO),
                    EXENTA = ObtenerDecimal(txtEXENTA),
                    GRAVADA = ObtenerDecimal(txtGRAVADA),
                    PERCEPCION = ObtenerDecimal(txtPERCEPCION),
                    IVA = ObtenerDecimal(txtIVA),
                    FOVIAL = ObtenerDecimal(txtFOVIAL),
                    COTRANS = ObtenerDecimal(txtCONTRANS),
                    TOTAL = ObtenerDecimal(txtTOTAL),
                    CARGO = ObtenerDecimal(txtCARGO),
                    ABONO = ObtenerDecimal(txtABONO),
                    RENTA = ObtenerDecimal(txtRENTA),
                    IVAR = ObtenerDecimal(txtIVAR),
                    SALDO = ObtenerDecimal(txtSALDO),
                    OBSERVACION = NullIfEmpty(txtOBSERVACION.Text),
                    USUARIO = Configuracion.UsuarioActual,                    
                    ID_TIPO_RENTA = ObtenerIdCombo(cbxTIPO_RENTA),
                    APLICABLE_RENTA = ObtenerDecimal(txtAPLICABLE_RENTA)
                };

                DataTable dt = _dal.EjecutarConsulta("SP_CREDITO_FISCAL_COMPRA", parametros);

                if (dt.Rows.Count == 0)
                    throw new Exception("El SP no devolvió los IDs generados.");

                // El SP devuelve siempre los tres campos:
                DataRow row = dt.Rows[0];
                IdCcfCompra = Convert.ToInt32(row["ID_GENERADO"]);
                _idQuedanActual = Convert.ToInt32(row["ID_QUEDAN_GENERADO"]);
                _validarCompIVAR = ObtenerDecimal(txtIVAR) > 0 ? true : false;
                int numQuedan = Convert.ToInt32(row["NUM_QUEDAN_GENERADO"]);
                lblNUM_QUEDAN.Text = numQuedan.ToString();
                ConfigurarCRUD(EstadoFormulario.Guardado);
                XtraMessageBox.Show("Documento guardado correctamente.",
                    "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Ocurrió un error al guardar el documento:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion


        #region === BOTÓN "NUEVO CCF MISMO QUEDAN" ===

        private void btnADICIONAR_Click(object sender, EventArgs e)
        {
            if (_idQuedanActual == 0)
            {
                XtraMessageBox.Show(
                    "Aún no se ha guardado ningún documento. Primero guarde uno para crear el Quedan.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show(
                    "¿Agregar otro documento al mismo Quedan y proveedor?",
                    "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            LimpiarParaNuevoCcf();
        }

        private void LimpiarParaNuevoCcf()
        {            

            IdCcfCompra = 0;

            cbxTIPO_DTE.SelectedValue = -1;
            txtNUM_CONTROL.Text = "";
            txtCOD_GENERACION.Text = "";
            txtSELLO_RECIBIDO.Text = "";
            mskFECHA_EMISION.Text = "";
            mskFECHA_RECIBIDO.Text = DateTime.Today.ToString("dd/MM/yyyy");
            mskFECHA_VENCE.Text = "";
            txtORDEN.Text = "";

            cbxTIPO_SERVICIO.SelectedValue = -1;
            cbxTIPO_OPERACION.SelectedValue = -1;
            LimpiarCombo(cbxCLASIFICACION, "ID_CLASIFICA");
            LimpiarCombo(cbxSECTOR, "ID_SECTOR");
            LimpiarCombo(cbxTIPO_COSTO, "ID_TIPO_COSTO");
            cbxTIPO_RENTA.SelectedIndex = 0;

            foreach (var tb in new[] {
                txtGRAVADA, txtEXENTA, txtEXCLUIDO, txtPERCEPCION,
                txtIVA,     txtFOVIAL, txtCONTRANS, txtTOTAL,
                txtCARGO,   txtABONO,  txtAPLICABLE_RENTA, txtRENTA,
                txtIVAR,    txtSALDO, txtOBSERVACION })
            {
                tb.Text = "";
            }            
            txtCONSULTA_MH.Focus();
        }

        #endregion


        #region === HELPERS DE PARSEO ===

        private static DateTime ParsearFecha(string texto)
        {
            return DateTime.ParseExact(texto, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        private static DateTime? ParsearFechaOpcional(string texto)
        {
            if (DateTime.TryParseExact(texto, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime f))
                return f;
            return null;
        }

        private static string NullIfEmpty(string texto)
        {
            return string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
        }

        private static string AsString(object valor)
        {
            return valor == null || valor == DBNull.Value ? "" : valor.ToString();
        }

        private static string AsFecha(object valor)
        {
            if (valor == null || valor == DBNull.Value) return "";
            return Convert.ToDateTime(valor).ToString("dd/MM/yyyy");
        }

        private static int ToInt(object valor)
        {
            if (valor == null || valor == DBNull.Value) return 0;
            return Convert.ToInt32(valor);
        }

        private static decimal ToDecimal(object valor)
        {
            if (valor == null || valor == DBNull.Value) return 0;
            return Convert.ToDecimal(valor);
        }

        private int? ObtenerIdCombo(ComboBox cbx)
        {
            if (cbx.SelectedValue == null) return null;
            if (cbx.SelectedValue == DBNull.Value) return null;
            if (cbx.SelectedValue is DataRowView) return null;
            return Convert.ToInt32(cbx.SelectedValue);
        }

        #endregion

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;
            GuardarDocumento();
            ConfigurarCRUD(EstadoFormulario.Guardado);
        }

        private void InicializarHelperMinisterioHacienda()
        {
            _mhHelper = new MinisterioHaciendaHelper(
                ownerControl: this,
                actualizarEstado: (texto, color) =>
                {
                    lblESTADO_MH.Text = texto;
                    lblESTADO_MH.ForeColor = color;
                },
                actualizarSelloRecibido: (texto) => txtSELLO_RECIBIDO.Text = texto,
                actualizarCodGeneracion: (texto) => txtCOD_GENERACION.Text = texto,
                actualizarNumControl: (texto) => txtNUM_CONTROL.Text = texto,
                actualizarFechaEmision: (fecha) => mskFECHA_EMISION.Text = fecha,
                actualizarGravada: (texto) => txtGRAVADA.Text = texto,
                actualizarExenta: (texto) => txtEXENTA.Text = texto,
                actualizarFOVIAL: (texto) => txtFOVIAL.Text = texto,
                actualizarCOTRANS: (texto) => txtCONTRANS.Text = texto,
                actualizarIVAR: (texto) => txtIVAR.Text = texto,
                actualizarComboTipoDte: (campo, codigo) =>
                {
                    SeleccionarComboPorCodigo(cbxTIPO_DTE, campo, codigo);                     
                },
                null,
                onConsultaExitosa: () =>
                {
                    mskFECHA_RECIBIDO.Focus();
                    RecalcularTotales();
                },
                "03" // CÓDIGO DE CCF ELECTRONICO
            );
        }

        void SeleccionarComboPorCodigo(ComboBox combo, string campoCodigo, string valor)
        {
            try
            {
                var dt = combo.DataSource as DataTable;
                if (dt == null) return;
                foreach (DataRow fila in dt.Rows)
                {
                    if (fila[campoCodigo]?.ToString() == valor)
                    {
                        combo.SelectedValue = fila[combo.ValueMember];
                        break;
                    }
                }
            }
            catch { }
        }

        private void btnProvision_Click(object sender, EventArgs e)
        {
            using (var frm = new frmDocumentoCompraProvision())
            {
                frm.IdCcfCompra = IdCcfCompra;
                frm.ShowDialog(this);
            }
        }
    }
}

