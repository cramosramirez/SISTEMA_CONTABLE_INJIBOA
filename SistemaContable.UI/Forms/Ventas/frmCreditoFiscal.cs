using SistemaContable.DAL;
using SistemaContable.RP.Ventas;
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
    public partial class frmCreditoFiscal : Form
    {
        private enum EstadoFormulario { Nuevo, Guardado, Validado }
        #region Campos privados
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtDetalle;
        private int _idEntidad = 0;
        private string _codigoEntidad = string.Empty;
        private string _columnaAnteriorGrid = string.Empty;
        #endregion
        public int IdCCFEnc { get; set; } = 0;
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
        public frmCreditoFiscal()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        #region CARGA INICIAL
        private void frmCreditoFiscal_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);
            CargarTipoDte();
            CargarSucursal();
            CargarCondicionPago();
            CargarVendedor();
            CargarEmisor(1);
            CargarTasasRetencion();
            if (cbxTIPO_DTE.Items.Count > 0) cbxTIPO_DTE.SelectedIndex = 0;
            InicializarGridDetalle();
            FormHelper.RegistrarBusqueda(
                txtCLIENTE,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_ENTIDAD",
                    Accion = "BUSCAR_CONTRIBUYENTES",      // CCF: solo contribuyentes
                    Columnas = new Dictionary<string, string>
                    {
                        { "CODIGO_ENTIDAD", "CLIENTE" },
                        { "NOMBRE",         "NOMBRE"  },
                        { "NIT",            "NIT"     },
                        { "NRC",            "NRC"     }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "CODIGO_ENTIDAD", 200 },
                        { "NOMBRE",         300 },
                        { "NIT",            150 },
                        { "NRC",            150 }
                    },
                    ParametrosExtra = new { ROL = "CLI" }
                },
                fila => AsignarCliente(fila)
            );
            if (IdCCFEnc == 0)
            {
                LimpiarFormulario();
                ConfigurarCRUD(EstadoFormulario.Nuevo);
                txtCLIENTE.Focus();
            }
            else
            {
                CargarCCFExistente(IdCCFEnc);
            }
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var reporte = new rptCreditoFiscal { IdCCFEnc = IdCCFEnc };
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
        private void CargarCCFExistente(int idCCFEnc)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DataTable dt = _dal.EjecutarConsulta("[EDTE].[SP_CREDITOFISCAL_ENC]",
                    new { ACCION = "OBTENER", ID_CCFENC = idCCFEnc, ID_EMISOR = 1 });
                if (dt == null || dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No se encontró el documento solicitado.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    return;
                }
                DataRow r = dt.Rows[0];
                IdCCFEnc = Convert.ToInt32(r["ID_CCFENC"]);
                // FIX: ID_CLIENTE ahora guarda ID_ENTIDAD (no el código de cliente).
                // El SP ya devuelve ID_ENTIDAD y CODIGO_ENTIDAD por separado (JOIN a ENTIDAD),
                // así que usamos esas columnas para separar el id real del código a mostrar.
                _idEntidad = r.Table.Columns.Contains("ID_ENTIDAD") && r["ID_ENTIDAD"] != DBNull.Value
                                    ? Convert.ToInt32(r["ID_ENTIDAD"])
                                    : (int.TryParse(r["ID_CLIENTE"].ToString().Trim(), out int idFallback) ? idFallback : 0);
                _codigoEntidad = r.Table.Columns.Contains("CODIGO_ENTIDAD")
                                    ? AsString(r["CODIGO_ENTIDAD"])
                                    : r["ID_CLIENTE"].ToString().Trim();
                txtCLIENTE.Text = _codigoEntidad;
                txtNOMBRE_CLIENTE.Text = AsString(r["NOMBRE_ENTIDAD"]);
                txtDUI.Text = AsString(r["DUI"]);
                txtNIT.Text = AsString(r["NIT"]);
                txtNRC.Text = AsString(r["NRC"]);
                txtTELEFONO.Text = AsString(r["CELULAR"]);
                txtCORREO.Text = AsString(r["CORREO"]);
                txtACTIVIDAD_PRIMARIA.Text = AsString(r["ACTIVIDAD_PRIMARIA"]);
                txtDIRECCION.Text = AsString(r["COMPLEMENTO"]);
                cbxTIPO_DTE.SelectedValue = Convert.ToInt32(r["ID_TIPO_DTE"]);
                cbxSUCURSAL.SelectedValue = Convert.ToInt32(r["ID_SUCURSAL"]);
                cbxCONDPAGO.SelectedValue = Convert.ToInt32(r["ID_CONDPAGO"]);
                mskFECHA.Text = AsFecha(r["FECHA"]);
                mskFECHA_VENCE.Text = AsFecha(r["FECHA_VENCE"]);
                txtNUMINTERNO.Text = AsString(r["NUMINTERNO"]);
                txtNUM_CONTROL.Text = AsString(r["NUMCONTROL"]);
                txtCOD_GENERACION.Text = AsString(r["CODGENERACION"]);
                txtSELLO_RECIBIDO.Text = AsString(r["SELLORECEPCION"]);
                AsignarDecimal(txtVENTA_GRAVADA, ToDecimal(r["AFECTA"]));
                AsignarDecimal(txtVENTA_EXENTA, ToDecimal(r["EXCENTA"]));
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
                CargarCCFDetalleExistente(idCCFEnc);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar el documento:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                ConfigurarCRUD(string.IsNullOrWhiteSpace(txtSELLO_RECIBIDO.Text)
                    ? EstadoFormulario.Guardado
                    : EstadoFormulario.Validado);
            }
        }
        private void CargarCCFDetalleExistente(int idCCFEnc)
        {
            DataTable dt = _dal.EjecutarConsulta("[EDTE].[SP_CREDITOFISCAL_DET]",
                new { ACCION = "LISTAR", ID_CCFENC = idCCFEnc, ID_EMISOR = 1 });
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
                    f["DESCUENTO"] = row["DESCUENTO_VALOR"];
                    f["EXENTO"] = row["EXENTA"];
                    f["GRAVADO"] = row["GRAVADA"];
                    f["TOTAL"] = row["TOTAL"];
                    f["ID_UNIDAD_MEDIDA"] = row.Table.Columns.Contains("ID_UNIDAD_MEDIDA")
                                                ? row["ID_UNIDAD_MEDIDA"] : (object)0;
                    _dtDetalle.Rows.Add(f);
                }
            }
            AgregarFilaVacia();
            ActualizarTotales();
        }
        private void CargarSiguienteNumDocumento(string abreviaturaDTE, int anio)
        {
            var num = _dal.ObtenerNumeracionPrevia(abreviaturaDTE, anio);
            if (num == null)
            {
                MessageBox.Show(
                    "No existe numeración activa para el tipo de documento seleccionado.\n" +
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
            txtNOMBRE_CLIENTE.Text = fila["NOMBRE"].ToString();
            txtNIT.Text = fila["NIT"].ToString();
            txtNRC.Text = fila.Table.Columns.Contains("NRC") ? fila["NRC"].ToString() : "";
            txtDUI.Text = fila["DUI"].ToString();
            txtTELEFONO.Text = fila["TELEFONO"].ToString();
            txtCORREO.Text = fila["CORREO"].ToString();
            txtACTIVIDAD_PRIMARIA.Text = fila["ACTIVIDAD_PRIMARIA"].ToString();
            txtDIRECCION.Text = fila["COMPLEMENTO"].ToString();
            txtTIPO_CONTRIBUYENTE.Text = fila["TIPO_CONTRIBUYENTE"].ToString();
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
            // Para CCF cargamos solo el tipo CCF (ID = 2)
            DataTable dt = _dal.EjecutarConsulta("SP_TIPO_DTE",
                new { ACCION = "OBTENER", ID_TIPO_DTE = 2 });
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
        #region GRID DETALLE  (CCF: sin columna No-Sujeta)
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
            _dtDetalle.Columns.Add("ES_EXENTO", typeof(bool));
            _dtDetalle.Columns.Add("DESCUENTO", typeof(decimal));
            _dtDetalle.Columns.Add("EXENTO", typeof(decimal));
            _dtDetalle.Columns.Add("GRAVADO", typeof(decimal));
            _dtDetalle.Columns.Add("TOTAL", typeof(decimal));
            _dtDetalle.Columns.Add("ID_UNIDAD_MEDIDA", typeof(int));
            AgregarFilaVacia();
            gridControl1.DataSource = _dtDetalle;
            var view = gridControl1.MainView as GridView;
            if (view == null) return;
            view.Columns.Clear();
            view.PopulateColumns();
            OcultarColumna(view, "ID_PRODUCTO");
            ConfigurarColumna(view, "COD_REF", "Código", 90, true);
            ConfigurarColumna(view, "DESCRIPCION", "Descripción", 250, true);
            ConfigurarColumna(view, "UM", "U.M.", 55, false);
            ConfigurarColumna(view, "CANTIDAD", "Cantidad", 75, true);
            ConfigurarColumna(view, "PRECIO", "Precio", 85, true);
            ConfigurarColumna(view, "PORC_DESC", "%Desc.", 55, true);
            ConfigurarColumna(view, "GRAVADO", "Gravado", 85, false);
            ConfigurarColumnaCheckBox(view, "ES_EXENTO", "Exento", 55);
            ConfigurarColumna(view, "EXENTO", "Exenta $", 85, false);
            ConfigurarColumna(view, "TOTAL", "Total", 90, false);
            ConfigurarColumna(view, "DESCUENTO", "Descuento", 75, false, false);
            ConfigurarColumna(view, "ID_UNIDAD_MEDIDA", "ID_UM", 80, false, false);
            view.Columns["COD_REF"].VisibleIndex = 0;
            view.Columns["DESCRIPCION"].VisibleIndex = 1;
            view.Columns["UM"].VisibleIndex = 2;
            view.Columns["CANTIDAD"].VisibleIndex = 3;
            view.Columns["PRECIO"].VisibleIndex = 4;
            view.Columns["PORC_DESC"].VisibleIndex = 5;
            view.Columns["GRAVADO"].VisibleIndex = 6;
            view.Columns["ES_EXENTO"].VisibleIndex = 7;
            view.Columns["EXENTO"].VisibleIndex = 8;
            view.Columns["TOTAL"].VisibleIndex = 9;
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
            repoEliminar.Buttons[0].ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.eliminarFila32x32;
            repoEliminar.Buttons[0].Caption = "";
            repoEliminar.Buttons[0].ToolTip = "Eliminar fila";
            repoEliminar.ButtonClick += (s, ev) => EliminarFilaDetalle();
            gridControl1.RepositoryItems.Add(repoEliminar);
            colEliminar.ColumnEdit = repoEliminar;
            foreach (var campo in new[] { "UM", "EXENTO", "GRAVADO", "TOTAL" })
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
                if (ev.Column.FieldName == "CANTIDAD" || ev.Column.FieldName == "PORC_DESC" ||
                    ev.Column.FieldName == "PRECIO" || ev.Column.FieldName == "DESCUENTO" ||
                    ev.Column.FieldName == "EXENTO" || ev.Column.FieldName == "GRAVADO" ||
                    ev.Column.FieldName == "TOTAL")
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
                    ev.Column.FieldName == "PORC_DESC" || ev.Column.FieldName == "ES_EXENTO")
                    RecalcularLinea(s as GridView, ev.RowHandle);
            };
            view.KeyDown += GridView_KeyDown;
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
        private void AgregarFilaVacia()
        {
            var fila = _dtDetalle.NewRow();
            fila["ID_PRODUCTO"] = 0;
            fila["COD_REF"] = "";
            fila["DESCRIPCION"] = "";
            fila["UM"] = "";
            fila["CANTIDAD"] = 0m;
            fila["PRECIO"] = 0m;
            fila["PORC_DESC"] = 0m;
            fila["DESCUENTO"] = 0m;
            fila["EXENTO"] = 0m;
            fila["GRAVADO"] = 0m;
            fila["TOTAL"] = 0m;
            fila["ES_EXENTO"] = false;
            fila["ID_UNIDAD_MEDIDA"] = 0;
            _dtDetalle.Rows.Add(fila);
        }
        private void RecalcularLinea(GridView view, int rowHandle)
        {
            if (view == null || rowHandle < 0) return;
            decimal cantidad = ObtenerDecimal(view, rowHandle, "CANTIDAD");
            decimal precio = ObtenerDecimal(view, rowHandle, "PRECIO");
            decimal porcDesc = ObtenerDecimal(view, rowHandle, "PORC_DESC");
            decimal subtotalLinea = cantidad * precio;
            decimal descuento = porcDesc > 0 ? Math.Round(subtotalLinea * porcDesc / 100m, 2) : 0m;
            decimal total = subtotalLinea - descuento;
            var valExento = view.GetRowCellValue(rowHandle, "ES_EXENTO");
            bool esExento = valExento != null && valExento != DBNull.Value && Convert.ToBoolean(valExento);
            decimal montoExento = esExento ? total : 0m;
            decimal montoGravado = !esExento ? total : 0m;
            view.SetRowCellValue(rowHandle, "DESCUENTO", descuento);
            view.SetRowCellValue(rowHandle, "TOTAL", total);
            view.SetRowCellValue(rowHandle, "EXENTO", montoExento);
            view.SetRowCellValue(rowHandle, "GRAVADO", montoGravado);
            if (rowHandle < _dtDetalle.Rows.Count)
            {
                _dtDetalle.Rows[rowHandle]["ES_EXENTO"] = esExento;
                _dtDetalle.Rows[rowHandle]["DESCUENTO"] = descuento;
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
            decimal totalExento = 0m, totalGravado = 0m, totalDescuento = 0m;
            foreach (DataRow fila in _dtDetalle.Rows)
            {
                totalExento += fila["EXENTO"] == DBNull.Value ? 0 : Convert.ToDecimal(fila["EXENTO"]);
                totalGravado += fila["GRAVADO"] == DBNull.Value ? 0 : Convert.ToDecimal(fila["GRAVADO"]);
                totalDescuento += fila["DESCUENTO"] == DBNull.Value ? 0 : Convert.ToDecimal(fila["DESCUENTO"]);
            }
            // CCF siempre aplica IVA 13%; retención/percepción según contribuyente
            decimal subTotal = (totalGravado + totalExento) - totalDescuento;
            decimal iva = Math.Round(subTotal * _porcIVA, 2);
            decimal retencion = 0m;
            decimal percepcion = 0m;
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
            decimal totalFinal = subTotal + iva - retencion + percepcion;
            txtVENTA_EXENTA.Text = totalExento.ToString("N2");
            txtVENTA_GRAVADA.Text = totalGravado.ToString("N2");
            txtSUBTOTAL.Text = subTotal.ToString("N2");
            txtIVA.Text = iva.ToString("N2");
            txtRETENCION.Text = retencion.ToString("N2");
            txtPERCEPCION.Text = percepcion.ToString("N2");
            txtDESCUENTO.Text = totalDescuento.ToString("N2");
            txtTOTAL_VENTA.Text = totalFinal.ToString("N2");
            string condicion = cbxCONDPAGO.Text?.Trim().ToUpper() ?? "";
            txtRECIB_EFECTIVO.Text = condicion == "CONTADO" && totalFinal > 0
                ? totalFinal.ToString("N2") : "0.00";
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
                if (filaActual == _dtDetalle.Rows.Count - 1) AgregarFilaVacia();
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
                if (filaActual == _dtDetalle.Rows.Count - 1) AgregarFilaVacia();
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
                var dt = _dal.EjecutarConsulta("[EINVENTARIO].[SP_PRODUCTO]",
                    new { ACCION = "BUSCAR", FILTRO = cod });
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
            var config = new BusquedaConfig
            {
                StoredProcedure = "[EINVENTARIO].[SP_PRODUCTO]",
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
            view.SetRowCellValue(rowHandle, "COD_REF", fila["COD_REF"].ToString());
            view.SetRowCellValue(rowHandle, "DESCRIPCION", fila["DESCRIPCION"].ToString());
            view.SetRowCellValue(rowHandle, "UM", fila["UNIMEDIDA"].ToString());
            view.SetRowCellValue(rowHandle, "PRECIO", Convert.ToDecimal(fila["PRECIO"]));
            view.SetRowCellValue(rowHandle, "ES_EXENTO", esExento);
            _dtDetalle.Rows[rowHandle]["ID_PRODUCTO"] = Convert.ToInt32(fila["ID_PRODUCTO"]);
            _dtDetalle.Rows[rowHandle]["ES_EXENTO"] = esExento;
            _dtDetalle.Rows[rowHandle]["ID_UNIDAD_MEDIDA"] = Convert.ToInt32(fila["ID_UNIDAD_MEDIDA"]);
            RecalcularLinea(view, rowHandle);
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
            if (IdCCFEnc > 0 && !esFilaVacia)
            {
                object val = view.GetRowCellValue(fila, "ID_PRODUCTO");
                if (val != null && val != DBNull.Value)
                {
                    int idProducto = Convert.ToInt32(val);
                    if (idProducto > 0)
                    {
                        _dal.EjecutarSinRetorno("[EDTE].[SP_CREDITOFISCAL_DET]", new
                        {
                            ACCION = "ELIMINAR",
                            ID_CCFENC = IdCCFEnc,
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
            try
            {
                var view = gridControl1.MainView as GridView;
                view?.CloseEditor();
                view?.UpdateCurrentRow();
                string NUMDOC = NullIfEmpty(txtNUMINTERNO.Text);
                int? idTipoDoc = ObtenerIdCombo(cbxTIPO_DTE);
                var dtVenta = _dal.EjecutarConsulta("[EDTE].[SP_CREDITOFISCAL_ENC]", new
                {
                    ACCION = "GUARDAR",
                    ID_CCFENC = IdCCFEnc,
                    ID_EMISOR = 1,
                    ID_SUCURSAL = ObtenerIdCombo(cbxSUCURSAL),
                    ID_ALMACEN = Configuracion.Id_Almacen,
                    ID_CAJERO = Configuracion.Id_Cajero,
                    ID_CAJA = Configuracion.Id_Cajero,
                    ID_CONDPAGO = ObtenerIdCombo(cbxCONDPAGO),
                    // FIX: ID_CLIENTE ahora guarda el ID_ENTIDAD real (no el código de cliente).
                    // COD_REF sigue guardando el código legible para referencia.
                    ID_CLIENTE = _idEntidad.ToString(),  // char(10)
                    COD_REF = _codigoEntidad,
                    ID_TIPO_DTE = idTipoDoc,
                    TPDOC = NullIfEmpty(cbxTIPO_DTE.Text),
                    FECHA = ParsearFecha(mskFECHA.Text),
                    SALFEC = FormHelper.ObtenerSalfec(mskFECHA),
                    NUMDOC = NUMDOC,
                    NUMINTERNO = txtNUMINTERNO.Text,
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
                    DESCUENTO = 0,
                    DESCUENTO_VALOR = ObtenerTextBoxDecimal(txtDESCUENTO),
                    SUBTOTAL = ObtenerTextBoxDecimal(txtSUBTOTAL),
                    IVA = ObtenerTextBoxDecimal(txtIVA),
                    IVARETENIDO = ObtenerTextBoxDecimal(txtRETENCION),
                    IVAPERCIBIDO = ObtenerTextBoxDecimal(txtPERCEPCION),
                    TOTALVENTA = ObtenerTextBoxDecimal(txtTOTAL_VENTA),
                    AP_PERCEPCION = chkPERCEPCION.Checked,
                    OBSERVACIONES = txtOBSERVACION.Text.Trim(),
                    TPCONTRIBUYENTE = _idTipoContribCliente,
                    TPCONTRIBUYENTEEMISOR = _idTipoContribEMISOR,
                    NIT = NullIfEmpty(txtNIT.Text),
                    NRC = NullIfEmpty(txtNRC.Text),
                    ID_ESTADO = 1,
                    USUARIO = Configuracion.UsuarioActual,
                });
                if (dtVenta == null || dtVenta.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Error al guardar el crédito fiscal.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                IdCCFEnc = Convert.ToInt32(dtVenta.Rows[0]["ID_GENERADO"]);
                if (dtVenta.Columns.Contains("NCONT") && dtVenta.Rows[0]["NCONT"] != DBNull.Value)
                    txtNUM_CONTROL.Text = Convert.ToString(dtVenta.Rows[0]["NCONT"]);
                if (dtVenta.Columns.Contains("INTERN") && dtVenta.Rows[0]["INTERN"] != DBNull.Value)
                    txtNUMINTERNO.Text = Convert.ToString(dtVenta.Rows[0]["INTERN"]);
                // Limpiar y reinsertar detalle
                _dal.EjecutarSinRetorno("[EDTE].[SP_CREDITOFISCAL_DET]", new
                {
                    ACCION = "LIMPIAR_LOTE",
                    ID_CCFENC = IdCCFEnc,
                    ID_EMISOR = 1
                });
                foreach (DataRow fila in _dtDetalle.Rows)
                {
                    if (string.IsNullOrWhiteSpace(fila["COD_REF"].ToString())) continue;
                    _dal.EjecutarSinRetorno("[EDTE].[SP_CREDITOFISCAL_DET]", new
                    {
                        ACCION = "GUARDAR",
                        ID_CCFDET = 0,
                        ID_CCFENC = IdCCFEnc,
                        ID_EMISOR = 1,
                        CODGENERACION = txtCOD_GENERACION.Text.Trim(),
                        ID_TIPO_DTE = idTipoDoc,
                        TPDOC = NullIfEmpty(cbxTIPO_DTE.Text),
                        FECHA = ParsearFecha(mskFECHA.Text),
                        SALFEC = FormHelper.ObtenerSalfec(mskFECHA),
                        NUMDOC = txtNUMINTERNO.Text.Trim(),
                        ID_PRODUCTO = Convert.ToInt32(fila["ID_PRODUCTO"]),
                        COD_REF = fila["COD_REF"].ToString(),
                        DESCRIPCION = fila["DESCRIPCION"].ToString(),
                        CANTIDAD = Convert.ToDecimal(fila["CANTIDAD"]),
                        ID_UNIDAD_MEDIDA = Convert.ToInt32(fila["ID_UNIDAD_MEDIDA"]),
                        UNIDAD_MEDIDA = fila["UM"].ToString(),
                        PRECIO = Convert.ToDecimal(fila["PRECIO"]),
                        DESCUENTO = Convert.ToInt32(fila["PORC_DESC"]),
                        DESCUENTO_VALOR = Convert.ToDecimal(fila["DESCUENTO"]),
                        ES_EXENTO = Convert.ToBoolean(fila["ES_EXENTO"]),
                        EXENTA = Convert.ToDecimal(fila["EXENTO"]),
                        GRAVADA = Convert.ToDecimal(fila["GRAVADO"]),
                        TOTAL = Convert.ToDecimal(fila["TOTAL"]),
                        USUARIO = Configuracion.UsuarioActual,
                    });
                }

                _dal.EjecutarSinRetorno("[EDTE].[SP_CREDITOFISCAL_JSON]", new
                {
                    ID_CCFENC = IdCCFEnc
                });



                XtraMessageBox.Show("Crédito fiscal guardado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConfigurarCRUD(EstadoFormulario.Guardado);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error al guardar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidarCampos()
        {
            if (!FormHelper.ValidarFecha(mskFECHA, "Fecha")) return false;
            if (string.IsNullOrWhiteSpace(_codigoEntidad))
            {
                XtraMessageBox.Show("Seleccione un cliente.",
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
        #region LIMPIAR
        private void LimpiarFormulario()
        {
            _idEntidad = 0;
            _codigoEntidad = string.Empty;
            IdCCFEnc = 0;
            txtCLIENTE.Text = "";
            txtNOMBRE_CLIENTE.Text = "";
            txtNIT.Text = "";
            txtNRC.Text = "";
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
            txtRECIB_REMESA_BANCO.Text = "";
            txtRECIB_REMESA_CUENTA.Text = "";
            txtRECIB_REMESA_MONTO.Text = "0.00";
            txtRECIB_CHEQUE_BANCO.Text = "";
            txtRECIB_CHEQUE_CUENTA.Text = "";
            txtRECIB_CHEQUE_MONTO.Text = "0.00";
            txtRECIB_NOTAABONO_BANCO.Text = "";
            txtRECIB_NOTAABONO_CUENTA.Text = "";
            txtRECIB_NOTAABONO_MONTO.Text = "0.00";
            txtVENTA_EXENTA.Text = "0.00";
            txtVENTA_GRAVADA.Text = "0.00";
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
            CargarSucursal();
            if (cbxSUCURSAL.Items.Count > 0) cbxSUCURSAL.SelectedIndex = 0;
            CargarVendedor();
            if (cbxVENDEDOR.Items.Count > 0) cbxVENDEDOR.SelectedIndex = 0;
            mskFECHA.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtCOD_GENERACION.Text = DALBase.NuevoGUID();
            CargarSiguienteNumDocumento(NullIfEmpty(cbxTIPO_DTE.Text), AnioDte);
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
            if (decimal.TryParse(txtRECIB_REMESA.Text.Replace(",", ""), out decimal val) && val > 0)
            {
                txtRECIB_REMESA_MONTO.Text = val.ToString("N2");
                txtRECIB_REMESA_BANCO.Focus();
            }
        }
        private void txtRECIB_CHEQUE_Leave(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtRECIB_CHEQUE.Text.Replace(",", ""), out decimal val) && val > 0)
            {
                txtRECIB_CHEQUE_MONTO.Text = val.ToString("N2");
                txtRECIB_CHEQUE_BANCO.Focus();
            }
        }
        private void txtRECIB_NOTAABONO_Leave(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtRECIB_NOTAABONO.Text.Replace(",", ""), out decimal val) && val > 0)
            {
                txtRECIB_NOTAABONO_MONTO.Text = val.ToString("N2");
                txtRECIB_NOTAABONO_BANCO.Focus();
            }
        }
        #endregion
        private void cbxTIPO_DTE_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? idTipoDte = ObtenerIdCombo(cbxTIPO_DTE);
            if (idTipoDte == null || idTipoDte <= 0) return;
            AnioDte = ObtenerAnioPorDte(idTipoDte);
        }
    }
}