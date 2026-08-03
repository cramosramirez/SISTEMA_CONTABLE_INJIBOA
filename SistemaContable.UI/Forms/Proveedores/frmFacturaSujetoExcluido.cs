using DevExpress.Utils;
using DevExpress.XtraEditors;
using SistemaContable.DAL;
using SistemaContable.RP.Bancos.Proveedores;
using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using ComboBox = System.Windows.Forms.ComboBox;

namespace SistemaContable.UI.Forms.Proveedores
{
    public partial class frmFacturaSujetoExcluido : Form
    {
        #region Campos privados

        private enum EstadoFormulario
        {
            Nuevo,
            Guardado
        }

        private readonly DALBase _dal = new DALBase();
        private int _idEntidad = 0;
        private string _codigoEntidad = string.Empty;
        private string _idTipoPersona = "";

        public int IdFse { get; set; } = 0;
        public bool EsContado { get; set; } = false;
        public string UidEnlaceCheque { get; set; } = string.Empty;

        #endregion

        public frmFacturaSujetoExcluido()
        {
            InitializeComponent();
        }

        private void frmFacturaSujetoExcluido_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);

            CargarCombos();
            cbxSUCURSAL.SelectedValue = 1;
            mskFECHA_RECIBIDO.Text = DateTime.Today.ToString("dd/MM/yyyy");           
            mskFECHA_EMISION.Text = DateTime.Today.ToString("dd/MM/yyyy");
            mskFECHA_RECIBIDO.ReadOnly = true;
            txtCOD_GENERACION.Text = DALBase.NuevoGUID(); 

            CargarTipoServicio();
            CargarTipoOperacion();
            LimpiarCombo(cbxCLASIFICACION, "ID_CLASIFICA");
            LimpiarCombo(cbxSECTOR, "ID_SECTOR");
            LimpiarCombo(cbxTIPO_COSTO, "ID_TIPO_COSTO");

            FormHelper.RegistrarBusqueda(
                txtPROVEEDOR,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_ENTIDAD",
                    Accion = "BUSCAR_NO_CONTRIBUYENTES",
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

            chkIVA.CheckedChanged += (s, ev) => RecalcularTotales();
            chkRENTA.CheckedChanged += (s, ev) => RecalcularTotales();

            txtMONTO.Enter += TxtDecimal_Enter;
            txtMONTO.KeyPress += TxtDecimal_KeyPress;
            txtMONTO.Leave += (s, ev) => { TxtDecimal_Leave(s, ev); RecalcularTotales(); };

            FormHelper.ResaltarCombosEnFoco(this);

            if (IdFse > 0)
            {
                CargarFseExistente(IdFse);
                ConfigurarCRUD(EstadoFormulario.Guardado);
            }
            else
            {
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
                    btnImprimirFSE.Enabled = false;
                    break;
                case EstadoFormulario.Guardado:
                    txtPROVEEDOR.Enabled = false;
                    btnGuardar.Enabled = true;
                    btnValidar.Enabled = true;
                    btnImprimirFSE.Enabled = true;
                    break;
            }
        }

        // ============================================================
        // Carga de combos
        // ============================================================
        private void CargarCombos()
        {
            // cbxTIPO_DTE: solo el registro de Factura Sujeto Excluido (CODIGO='14'),
            // preseleccionado — el usuario no elige otro tipo aquí.
            var dtTipoDte = _dal.EjecutarConsulta("SP_TIPO_DTE", new
            {
                ACCION = "BUSCAR_FSE"
            });
            cbxTIPO_DTE.DataSource = null;
            cbxTIPO_DTE.DisplayMember = "ABREVIATURA";
            cbxTIPO_DTE.ValueMember = "ID_TIPO_DTE";
            cbxTIPO_DTE.DataSource = dtTipoDte;

            if (dtTipoDte.Rows.Count > 0)
                cbxTIPO_DTE.SelectedIndex = 0;            

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
                new { ACCION = "COMBO", ID_TIPO_OPERA = idTipoOpera });
            InsertarFilaSeleccione(dt, "ID_CLASIFICA");
            cbxCLASIFICACION.DataSource = dt;
            cbxCLASIFICACION.ValueMember = "ID_CLASIFICA";
            cbxCLASIFICACION.DisplayMember = "NOMBRE";
        }

        private void CargarSector(int idTipoOpera, int idClasifica)
        {
            DataTable dt = _dal.EjecutarConsulta("SP_COMPRA_SECTOR",
                new { ACCION = "COMBO", ID_TIPO_OPERA = idTipoOpera, ID_CLASIFICA = idClasifica });
            InsertarFilaSeleccione(dt, "ID_SECTOR");
            cbxSECTOR.DataSource = dt;
            cbxSECTOR.ValueMember = "ID_SECTOR";
            cbxSECTOR.DisplayMember = "NOMBRE";
        }

        private void CargarTipoCosto(int idTipoOpera, int idClasifica, int idSector)
        {
            DataTable dt = _dal.EjecutarConsulta("SP_COMPRA_TIPO_COSTO",
                new { ACCION = "COMBO", ID_TIPO_OPERA = idTipoOpera, ID_CLASIFICA = idClasifica, ID_SECTOR = idSector });
            InsertarFilaSeleccione(dt, "ID_TIPO_COSTO");
            cbxTIPO_COSTO.DataSource = dt;
            cbxTIPO_COSTO.ValueMember = "ID_TIPO_COSTO";
            cbxTIPO_COSTO.DisplayMember = "NOMBRE";
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

        private void cbxTIPO_OPERACION_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LimpiarCombo(cbxCLASIFICACION, "ID_CLASIFICA");
            LimpiarCombo(cbxSECTOR, "ID_SECTOR");
            LimpiarCombo(cbxTIPO_COSTO, "ID_TIPO_COSTO");
            if (!TieneSeleccion(cbxTIPO_OPERACION)) return;
            CargarClasificacion(Convert.ToInt32(cbxTIPO_OPERACION.SelectedValue));
        }

        private void cbxCLASIFICACION_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LimpiarCombo(cbxSECTOR, "ID_SECTOR");
            LimpiarCombo(cbxTIPO_COSTO, "ID_TIPO_COSTO");
            if (!TieneSeleccion(cbxCLASIFICACION) || !TieneSeleccion(cbxTIPO_OPERACION)) return;
            CargarSector(Convert.ToInt32(cbxTIPO_OPERACION.SelectedValue),
                         Convert.ToInt32(cbxCLASIFICACION.SelectedValue));
        }

        private void cbxSECTOR_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LimpiarCombo(cbxTIPO_COSTO, "ID_TIPO_COSTO");
            if (!TieneSeleccion(cbxSECTOR) || !TieneSeleccion(cbxCLASIFICACION) || !TieneSeleccion(cbxTIPO_OPERACION)) return;
            CargarTipoCosto(Convert.ToInt32(cbxTIPO_OPERACION.SelectedValue),
                            Convert.ToInt32(cbxCLASIFICACION.SelectedValue),
                            Convert.ToInt32(cbxSECTOR.SelectedValue));
        }

        // ============================================================
        // Proveedor
        // ============================================================
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
                    XtraMessageBox.Show($"No se encontró el proveedor con código '{codigo}'.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPROVEEDOR.Focus();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error al buscar proveedor: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AsignarProveedor(DataRow fila)
        {
            _idEntidad = Convert.ToInt32(fila["ID_ENTIDAD"]);
            _codigoEntidad = fila["CODIGO_ENTIDAD"].ToString();
            _idTipoPersona = fila["ID_TIPO_ENTIDAD"].ToString();
            txtPROVEEDOR.Text = fila["CODIGO_ENTIDAD"].ToString();
            txtNOMBRE_PROVEEDOR.Text = fila["NOMBRE"].ToString();
            txtDUI.Text = fila["DUI"].ToString();
            txtNIT.Text = fila["NIT"].ToString();
            txtTELEFONO.Text = fila["CELULAR"].ToString();
            txtCORREO.Text = fila["CORREO"].ToString();
            txtACTIVIDAD_PRIMARIA.Text = fila["ACTIVIDAD_PRIMARIA"].ToString();
            txtTIPO_CONTRIBUYENTE.Text = fila["TIPO_CONTRIBUYENTE"].ToString();
            txtDIRECCION.Text = fila["COMPLEMENTO"].ToString();
        }

        private void LimpiarProveedor()
        {
            _idEntidad = 0;
            _codigoEntidad = string.Empty;
            txtNOMBRE_PROVEEDOR.Text = string.Empty;
            txtDUI.Text = string.Empty;
            txtNIT.Text = string.Empty;
            txtTELEFONO.Text = string.Empty;
            txtCORREO.Text = string.Empty;
            txtACTIVIDAD_PRIMARIA.Text = string.Empty;
            txtTIPO_CONTRIBUYENTE.Text = string.Empty;
            txtDIRECCION.Text = string.Empty;
        }

        // ============================================================
        // Cálculo (MONTO + checkboxes IVA/RENTA)
        // ============================================================
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
            if (e.KeyChar == '.' && tb.Text.Contains("."))
                e.Handled = true;
        }

        private void TxtDecimal_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text)) { tb.Text = ""; return; }
            if (!decimal.TryParse(tb.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
            {
                tb.Text = "";
                return;
            }
            tb.Text = valor == 0m ? "" : valor.ToString("N2");
        }

        private decimal ObtenerDecimal(TextBox tb)
        {
            if (string.IsNullOrWhiteSpace(tb.Text)) return 0;
            return decimal.TryParse(tb.Text, out decimal v) ? v : 0;
        }

        private void AsignarDecimal(TextBox tb, decimal valor)
        {
            tb.Text = valor == 0 ? "" : valor.ToString("N2");
        }

        // IVA se autoliquida: aparece en TOTAL y se resta de vuelta vía IVAR
        // (el proveedor sujeto excluido nunca recibe el IVA, JIBOA lo retiene
        // y lo declara directo a Hacienda).
        private void RecalcularTotales()
        {
            decimal monto = ObtenerDecimal(txtMONTO);

            decimal iva = chkIVA.Checked ? Calculo.Redondear(monto * 0.13m, 2) : 0;
            decimal ivar = iva; // mismo valor, se resta en SALDO
            decimal renta = chkRENTA.Checked ? Calculo.Redondear(monto * 0.10m, 2) : 0;

            decimal total = monto + iva;
            decimal saldo = total - renta - ivar;

            AsignarDecimal(txtIVA, iva);
            AsignarDecimal(txtIVAR, ivar);
            AsignarDecimal(txtRENTA, renta);
            AsignarDecimal(txtTOTAL, total);
            AsignarDecimal(txtSALDO, saldo);
        }

        // ============================================================
        // Cargar documento existente
        // ============================================================
        private void CargarFseExistente(int idFse)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                DataTable dt = _dal.EjecutarConsulta("SP_FACTURA_SUJETO_EXC", new
                {
                    ACCION = "OBTENER",
                    ID_FSE = idFse
                });

                if (dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No se encontró el documento solicitado.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    return;
                }

                DataRow r = dt.Rows[0];

                _idEntidad = Convert.ToInt32(r["ID_ENTIDAD"]);
                _codigoEntidad = r["CODIGO_ENTIDAD"].ToString();
                _idTipoPersona = r["ID_TIPO_ENTIDAD"].ToString();
                txtPROVEEDOR.Text = _codigoEntidad;
                txtNOMBRE_PROVEEDOR.Text = r["NOMBRE_ENTIDAD"]?.ToString();
                txtDUI.Text = AsString(r["NRC"]);
                txtNIT.Text = AsString(r["NIT"]);
                txtTELEFONO.Text = AsString(r["CELULAR"]);
                txtCORREO.Text = AsString(r["CORREO"]);
                txtACTIVIDAD_PRIMARIA.Text = AsString(r["ACTIVIDAD_PRIMARIA"]);
                txtTIPO_CONTRIBUYENTE.Text = AsString(r["TIPO_CONTRIBUYENTE"]);
                txtDIRECCION.Text = AsString(r["COMPLEMENTO"]);

                // Documento generado por el sistema
                txtNUM_CONTROL.Text = AsString(r["NUM_CONTROL"]);
                txtCOD_GENERACION.Text = AsString(r["COD_GENERACION"]);
                txtSELLO_RECIBIDO.Text = AsString(r["SELLO_RECIBIDO"]);
                mskFECHA_EMISION.Text = AsFecha(r["FECHA_EMISION"]);
                mskFECHA_RECIBIDO.Text = AsFecha(r["FECHA_RECIBIDO"]);                
                txtORDEN.Text = AsString(r["ORDEN"]);

                cbxSUCURSAL.SelectedValue = ToInt(r["ID_SUCURSAL"]);

                int idTipoServi = ToInt(r["ID_TIPO_SERVI"]);
                int idTipoOpera = ToInt(r["ID_TIPO_OPERA"]);
                int idClasifica = ToInt(r["ID_CLASIFICA"]);
                int idSector = ToInt(r["ID_SECTOR"]);
                int idTipoCosto = ToInt(r["ID_TIPO_COSTO"]);

                cbxTIPO_SERVICIO.SelectedValue = idTipoServi;
                cbxTIPO_OPERACION.SelectedValue = idTipoOpera;

                CargarClasificacion(idTipoOpera);
                cbxCLASIFICACION.SelectedValue = idClasifica;

                CargarSector(idTipoOpera, idClasifica);
                cbxSECTOR.SelectedValue = idSector;

                CargarTipoCosto(idTipoOpera, idClasifica, idSector);
                cbxTIPO_COSTO.SelectedValue = idTipoCosto;

                chkIVA.Checked = r["APLICA_IVA"] != DBNull.Value && Convert.ToBoolean(r["APLICA_IVA"]);
                chkRENTA.Checked = r["APLICA_RENTA"] != DBNull.Value && Convert.ToBoolean(r["APLICA_RENTA"]);
                chkArrendamiento.Checked = r["APLICA_ARR_AGRICOLA"] != DBNull.Value && Convert.ToBoolean(r["APLICA_ARR_AGRICOLA"]);

                AsignarDecimal(txtMONTO, ToDecimal(r["MONTO"]));
                AsignarDecimal(txtIVA, ToDecimal(r["IVA"]));
                AsignarDecimal(txtTOTAL, ToDecimal(r["TOTAL"]));
                AsignarDecimal(txtRENTA, ToDecimal(r["RENTA"]));
                AsignarDecimal(txtIVAR, ToDecimal(r["IVAR"]));
                AsignarDecimal(txtSALDO, ToDecimal(r["SALDO"]));

                txtCONCEPTO.Text = AsString(r["CONCEPTO"]);
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

        // ============================================================
        // Validaciones
        // ============================================================
        private bool ValidarCampos()
        {
            if (_idEntidad == 0)
            {
                XtraMessageBox.Show("Debe seleccionar un proveedor.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPROVEEDOR.Focus();
                return false;
            }

            if (!TieneSeleccion(cbxSUCURSAL))
            {
                XtraMessageBox.Show("Debe seleccionar la sucursal.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxSUCURSAL.Focus();
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

            if (!TieneSeleccion(cbxTIPO_SERVICIO)) { AvisoValidacion(cbxTIPO_SERVICIO, "tipo de servicio"); return false; }
            if (!TieneSeleccion(cbxTIPO_OPERACION)) { AvisoValidacion(cbxTIPO_OPERACION, "tipo de operación"); return false; }
            if (!TieneSeleccion(cbxCLASIFICACION)) { AvisoValidacion(cbxCLASIFICACION, "clasificación"); return false; }
            if (!TieneSeleccion(cbxSECTOR)) { AvisoValidacion(cbxSECTOR, "sector"); return false; }
            if (!TieneSeleccion(cbxTIPO_COSTO)) { AvisoValidacion(cbxTIPO_COSTO, "tipo de costo"); return false; }

            if (ObtenerDecimal(txtMONTO) <= 0)
            {
                XtraMessageBox.Show("El monto del documento debe ser mayor a cero.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMONTO.Focus();
                return false;
            }

            return true;
        }

        private void AvisoValidacion(ComboBox cbx, string nombre)
        {
            XtraMessageBox.Show($"Debe seleccionar el {nombre}.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            cbx.Focus();
        }

        // ============================================================
        // Guardar
        // ============================================================
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;
            GuardarDocumento();
        }

        private void GuardarDocumento()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var parametros = new
                {
                    ACCION = "GUARDAR",
                    ID_FSE = IdFse,
                    TIPO_MONEDA = "USD",
                    ID_CHEQUE = (int?)null,
                    NUM_CHEQUE = (int?)null,
                    ID_ENTIDAD = _idEntidad,
                    CODIGO_ENTIDAD = _codigoEntidad,
                    COD_GENERACION = txtCOD_GENERACION.Text,  
                    FECHA_EMISION = ParsearFecha(mskFECHA_EMISION.Text),
                    FECHA_RECIBIDO = ParsearFecha(mskFECHA_RECIBIDO.Text),
                    FECHA_VENCE = ParsearFecha(mskFECHA_RECIBIDO.Text),
                    ORDEN = NullIfEmpty(txtORDEN.Text),
                    ID_SUCURSAL = Convert.ToInt32(cbxSUCURSAL.SelectedValue),
                    ID_TIPO_SERVI = ObtenerIdCombo(cbxTIPO_SERVICIO),
                    ID_TIPO_OPERA = ObtenerIdCombo(cbxTIPO_OPERACION),
                    ID_CLASIFICA = ObtenerIdCombo(cbxCLASIFICACION),
                    ID_SECTOR = ObtenerIdCombo(cbxSECTOR),
                    ID_TIPO_COSTO = ObtenerIdCombo(cbxTIPO_COSTO),                    
                    MONTO = ObtenerDecimal(txtMONTO),
                    IVA = ObtenerDecimal(txtIVA),
                    TOTAL = ObtenerDecimal(txtTOTAL),
                    APLICABLE_RENTA = chkRENTA.Checked ? ObtenerDecimal(txtMONTO) : 0,
                    RENTA = ObtenerDecimal(txtRENTA),
                    IVAR = ObtenerDecimal(txtIVAR),
                    SALDO = ObtenerDecimal(txtSALDO),
                    APLICA_IVA = chkIVA.Checked,
                    APLICA_RENTA = chkRENTA.Checked,
                    APLICA_ARR_AGRICOLA = chkArrendamiento.Checked,
                    CONCEPTO = NullIfEmpty(txtCONCEPTO.Text),
                    USUARIO = Configuracion.UsuarioActual,
                    UID_ENLACE_CHEQUE = EsContado ? UidEnlaceCheque : string.Empty
                };

                DataTable dt = _dal.EjecutarConsulta("SP_FACTURA_SUJETO_EXC", parametros);

                if (dt.Rows.Count == 0)
                    throw new Exception("El SP no devolvió el ID generado.");

                IdFse = Convert.ToInt32(dt.Rows[0]["ID_GENERADO"]);

                // Refrescar campos generados por el sistema (NUM_CONTROL, COD_GENERACION, FECHA_EMISION, etc.)
                CargarFseExistente(IdFse);
                ConfigurarCRUD(EstadoFormulario.Guardado);

                XtraMessageBox.Show("Documento guardado correctamente.",
                    "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException sqlEx)
            {
                XtraMessageBox.Show(sqlEx.Message,
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        // ============================================================
        // Helpers
        // ============================================================
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

        private static string AsString(object valor)
            => valor == null || valor == DBNull.Value ? "" : valor.ToString();

        private static string AsFecha(object valor)
        {
            if (valor == null || valor == DBNull.Value) return "";
            return Convert.ToDateTime(valor).ToString("dd/MM/yyyy");
        }

        private static int ToInt(object valor)
            => valor == null || valor == DBNull.Value ? 0 : Convert.ToInt32(valor);

        private static decimal ToDecimal(object valor)
            => valor == null || valor == DBNull.Value ? 0 : Convert.ToDecimal(valor);

        private int? ObtenerIdCombo(ComboBox cbx)
        {
            if (cbx.SelectedValue == null) return null;
            if (cbx.SelectedValue == DBNull.Value) return null;
            if (cbx.SelectedValue is DataRowView) return null;
            return Convert.ToInt32(cbx.SelectedValue);
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (EsContado) DialogResult = DialogResult.OK;
            Close();
        }       

        private void mskFECHA_EMISION_Leave(object sender, EventArgs e)
        {           
            if (DateTime.TryParseExact(mskFECHA_RECIBIDO.Text, "dd/MM/yyyy",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecha))
            {
                mskFECHA_RECIBIDO.Text = fecha.ToString("dd/MM/yyyy");
            }
            
        }
        private void btnImprimirFSE_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var reporte = new rptSujetoExcluido { IdFse = IdFse };
                reporte.MostrarPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al imprimir:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}