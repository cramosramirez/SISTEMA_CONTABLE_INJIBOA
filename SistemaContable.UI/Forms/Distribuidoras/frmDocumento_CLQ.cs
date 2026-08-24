using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComboBox = System.Windows.Forms.ComboBox;

namespace SistemaContable.UI.Forms.Distribuidoras
{
    public partial class frmDocumento_CLQ : Form
    {
        #region Campos privados

        private bool _actualizandoDesdeBusqueda = false;        
        private string _ultimoCodigoValidado = string.Empty;
        private readonly ParametrosTipoClqBusqueda _parametrosTipoClq = new ParametrosTipoClqBusqueda();
        private enum EstadoFormulario { Nuevo, Guardado }

        private readonly DALBase _dal = new DALBase();
        private DataTable _dtDetalle;   // único renglón ficticio

        private int _idEntidad = 0;
        private string _codigoEntidad = string.Empty;

        public int IdClqEnca { get; set; } = 0;

        #endregion

        public frmDocumento_CLQ()
        {
            InitializeComponent();
        }

        private void frmDocumento_CLQ_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);
            CargarCombos();
            InicializarGridDetalle();

            dteFECHA.DateTime = DateTime.Today;

            // ---------------- Búsqueda de Cliente ----------------
            FormHelper.RegistrarBusqueda(
                txtCLIENTE,
                new BusquedaConfig
                {
                    StoredProcedure = "DISTRIB.SP_CLQ_ENCA",
                    Accion = "BUSCAR_CLIENTES_DISTRIBUIDORAS",
                    Columnas = new Dictionary<string, string>
                    {
                        { "CODIGO_ENTIDAD", "CLIENTE" },
                        { "NOMBRE",         "NOMBRE"  },
                        { "NIT",            "NIT"     }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "CODIGO_ENTIDAD", 100 },
                        { "NOMBRE",         300 },
                        { "NIT",            120 }
                    }
                },
                fila => AsignarCliente(fila)
            );
            txtCLIENTE.Leave += txtCLIENTE_Leave;

            // ---------------- Búsqueda de Tipo de CLQ ----------------
            FormHelper.RegistrarBusqueda(
                txtCODIGO_CLQ,
                new BusquedaConfig
                {
                    StoredProcedure = "DISTRIB.SP_CLQ_ENCA",
                    Accion = "BUSCAR_TIPO_CLQ",
                    Columnas = new Dictionary<string, string>
                    {
                        { "CODIGO_CLQ",      "CODIGO"      },
                        { "NOMBRE_TIPO_CLQ", "NOMBRE"      },
                        { "SERIE",           "SERIE"       },
                        { "RESOLUCION",      "RESOLUCIÓN"  }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "CODIGO_CLQ",         100 },
                        { "NOMBRE_TIPO_CLQ",    300 },
                        { "SERIE",              70 },
                        { "RESOLUCION",         120 }
                    },
                    ParametrosExtra = _parametrosTipoClq
                },
                fila => AsignarTipoClq(fila)
            );
            txtCODIGO_CLQ.Leave += txtCODIGO_CLQ_Leave;

            ConfigurarTextBoxDecimal(
                txtGRAVADA, txtEXENTA, txtPORC_DESCUENTO, txtDESCUENTO,
                txtSUBTOTAL, txtIVA, txtIVAR, txtPERCEPCION, txtTOTAL
            );

            txtGRAVADA.ReadOnly = true;
            txtEXENTA.ReadOnly = true;
            txtDESCUENTO.ReadOnly = true;
            txtSUBTOTAL.ReadOnly = true;
            txtIVA.ReadOnly = true;
            txtTOTAL.ReadOnly = true;

            EngancharRecalculo(txtGRAVADA, txtEXENTA, txtPORC_DESCUENTO, txtIVAR, txtPERCEPCION);

            FormHelper.ResaltarCombosEnFoco(this);

            if (IdClqEnca > 0)
            {
                CargarClqExistente(IdClqEnca);
                ConfigurarCRUD(EstadoFormulario.Guardado);
            }
            else
            {
                ConfigurarCRUD(EstadoFormulario.Nuevo);
            }
            FormHelper.ResaltarCombosEnFoco(this);
        }

        private void ConfigurarCRUD(EstadoFormulario estado)
        {
            bool esNuevo = estado == EstadoFormulario.Nuevo;
            txtCLIENTE.Enabled = esNuevo;
            txtCODIGO_CLQ.Enabled = esNuevo;
            txtNUMERO_CLQ.Enabled = esNuevo;
        }

        // ============================================================
        // Carga de combos
        // ============================================================
        private void CargarCombos()
        {
            CargarCombo("DISTRIB.SP_CLQ_ENCA", "BUSCAR_SUCURSAL", "ID_SUCURSAL", "NOMBRE", cbxSUCURSAL);            
            CargarCombo("DISTRIB.SP_CLQ_ENCA", "BUSCAR_CONDICION_PAGO", "ID_COND_PAGO", "NOMBRE_COND_PAGO", cbxCONDICION_PAGO);
            CargarCombo("DISTRIB.SP_CLQ_ENCA", "BUSCAR_TVTA", "ID_TVTA", "TVTA", cbxTIPO_VENTA, "VENTA GRAVADA");
            CargarCombo("DISTRIB.SP_CLQ_ENCA", "BUSCAR_LDESPACHO", "ID_LDESPACHO", "LDESPACHO", cbxLUGAR_DESPACHO, "INGENIO JIBOA");
            CargarCombo("DISTRIB.SP_CLQ_ENCA", "BUSCAR_GVTA", "ID_GVTA", "GVTA", cbxGENTRAS, "VENTA DISTRIBUIDOR");
            CargarCombo("DISTRIB.SP_CLQ_ENCA", "BUSCAR_ZAFRA", "ID_ZAFRA", "NOMBRE_ZAFRA", cbxZAFRA);
        }

        private void CargarCombo(string sp, string accion, string valueMember,
                string displayMember, ComboBox combo, string valorPorDefecto = null)
        {
            try
            {
                var dt = _dal.EjecutarConsulta(sp, new { ACCION = accion });

                foreach (DataColumn col in dt.Columns)
                    col.AllowDBNull = true;

                bool soloUnResultado = dt.Rows.Count == 1;

                var filaVacia = dt.NewRow();
                filaVacia[valueMember] = -1;
                filaVacia[displayMember] = "-- Seleccione --";
                dt.Rows.InsertAt(filaVacia, 0);

                combo.DataSource = null;
                combo.DisplayMember = displayMember;
                combo.ValueMember = valueMember;
                combo.DataSource = dt;

                if (soloUnResultado)
                {
                    combo.SelectedIndex = 1;
                }
                else if (!string.IsNullOrWhiteSpace(valorPorDefecto))
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        if (string.Equals(r[displayMember]?.ToString()?.Trim(), valorPorDefecto,
                            StringComparison.OrdinalIgnoreCase))
                        {
                            combo.SelectedValue = r[valueMember];
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error cargando combo: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCentroCosto(string codigoEntidad)
        {
            try
            {
                var dt = _dal.EjecutarConsulta("DISTRIB.SP_CLQ_ENCA", new
                {
                    ACCION = "BUSCAR_CENTRO",
                    CODIGO_ENTIDAD = codigoEntidad
                });

                foreach (DataColumn col in dt.Columns)
                    col.AllowDBNull = true;

                bool soloUnResultado = dt.Rows.Count == 1;   // ✅ antes de insertar el placeholder

                var filaVacia = dt.NewRow();
                filaVacia["ID_CENTRO"] = -1;
                filaVacia["NOMBRE"] = "-- Seleccione --";
                dt.Rows.InsertAt(filaVacia, 0);

                cbxCENTRO_COSTO.DataSource = null;
                cbxCENTRO_COSTO.DisplayMember = "NOMBRE";
                cbxCENTRO_COSTO.ValueMember = "ID_CENTRO";
                cbxCENTRO_COSTO.DataSource = dt;

                if (soloUnResultado)
                    cbxCENTRO_COSTO.SelectedIndex = 1;   // ✅ salta el placeholder (índice 0), selecciona el único real
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error cargando centro de costo: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool TieneSeleccion(ComboBox cbx)
        {
            if (cbx.SelectedValue == null) return false;
            if (cbx.SelectedValue == DBNull.Value) return false;
            if (cbx.SelectedValue is DataRowView) return false;
            if (cbx.SelectedValue is int i && i == -1) return false;
            return true;
        }

        private int? ObtenerIdCombo(ComboBox cbx) => TieneSeleccion(cbx) ? Convert.ToInt32(cbx.SelectedValue) : (int?)null;

        // ============================================================
        // Cliente
        // ============================================================
        private void txtCLIENTE_Leave(object sender, EventArgs e)
        {
            string codigo = txtCLIENTE.Text.Trim();
            if (codigo == "*" || string.IsNullOrWhiteSpace(codigo)) return;

            try
            {
                var dt = _dal.EjecutarConsulta("SP_ENTIDAD", new
                {
                    ACCION = "BUSCAR_POR_CODIGO",
                    FILTRO = codigo,
                    ROL = "CLI"
                });

                if (dt.Rows.Count > 0)
                    AsignarCliente(dt.Rows[0]);
                else
                {
                    LimpiarCliente();
                    XtraMessageBox.Show($"No se encontró el cliente con código '{codigo}'.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCLIENTE.Focus();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error al buscar cliente: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AsignarCliente(DataRow fila)
        {
            _idEntidad = Convert.ToInt32(fila["ID_ENTIDAD"]);
            _codigoEntidad = fila["CODIGO_ENTIDAD"].ToString();
            _parametrosTipoClq.CODIGO_ENTIDAD = _codigoEntidad;
            CargarCentroCosto(_codigoEntidad);
            txtCLIENTE.Text = _codigoEntidad;
            txtNOMBRE_CLIENTE.Text = fila["NOMBRE"].ToString();
            txtNRC.Text = SafeStr(fila, "NRC");
            txtNIT.Text = SafeStr(fila, "NIT");
            txtTELEFONO.Text = SafeStr(fila, "CELULAR");
            txtCORREO.Text = SafeStr(fila, "CORREO");
            txtACTIVIDAD_PRIMARIA.Text = SafeStr(fila, "ACTIVIDAD_PRIMARIA");
            txtTIPO_CONTRIBUYENTE.Text = SafeStr(fila, "TIPO_CONTRIBUYENTE");
            txtDIRECCION.Text = SafeStr(fila, "COMPLEMENTO");
        }

        private void txtCODIGO_CLQ_Leave(object sender, EventArgs e)
        {
            string codigo = txtCODIGO_CLQ.Text.Trim();
            if (codigo == "*" || string.IsNullOrWhiteSpace(codigo)) return;

            try
            {
                var dt = _dal.EjecutarConsulta("DISTRIB.SP_CLQ_ENCA", new
                {
                    ACCION = "BUSCAR_TIPO_CLQ",
                    FILTRO = codigo,
                    CODIGO_ENTIDAD = _codigoEntidad
                });

                // Buscar coincidencia EXACTA por CODIGO_CLQ (el FILTRO usa LIKE, podría traer varias)
                DataRow fila = null;
                foreach (DataRow r in dt.Rows)
                {
                    if (string.Equals(r["CODIGO_CLQ"].ToString().Trim(), codigo, StringComparison.OrdinalIgnoreCase))
                    {
                        fila = r;
                        break;
                    }
                }

                if (fila != null)
                    AsignarTipoClq(fila);
                else
                {
                    LimpiarTipoClq();
                    XtraMessageBox.Show($"No se encontró el tipo de documento con código '{codigo}'.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCODIGO_CLQ.Focus();
                    txtCODIGO_CLQ.SelectAll();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error al buscar tipo de documento: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarTipoClq()
        {
            txtCODIGO_CLQ.Tag = null;
            txtNOMBRE_TIPO_CLQ.Text = string.Empty;
            txtSERIE.Text = string.Empty;
            txtRESOLUCION.Text = string.Empty;
        }

        private void LimpiarCliente()
        {
            _idEntidad = 0;
            _codigoEntidad = string.Empty;
            _parametrosTipoClq.CODIGO_ENTIDAD = null;
            CargarCentroCosto(null);
            txtNOMBRE_CLIENTE.Text = txtNRC.Text = txtNIT.Text = txtTELEFONO.Text =
                txtCORREO.Text = txtACTIVIDAD_PRIMARIA.Text = txtTIPO_CONTRIBUYENTE.Text =
                txtDIRECCION.Text = string.Empty;
        }

        // ============================================================
        // Tipo de CLQ
        // ============================================================
        private void AsignarTipoClq(DataRow fila)
        {
            txtCODIGO_CLQ.Tag = fila["ID_TIPO_CLQ"].ToString();
            txtCODIGO_CLQ.Text = fila["CODIGO_CLQ"].ToString();
            txtNOMBRE_TIPO_CLQ.Text = fila["NOMBRE_TIPO_CLQ"].ToString();
            txtSERIE.Text = SafeStr(fila, "SERIE");
            txtRESOLUCION.Text = SafeStr(fila, "RESOLUCION");
        }

        // ============================================================
        // Grid de detalle — único renglón ficticio
        // ============================================================
        private void InicializarGridDetalle()
        {
            _dtDetalle = new DataTable();
            _dtDetalle.Columns.Add("ID_PRODUCTO", typeof(int));
            _dtDetalle.Columns.Add("COD_REF", typeof(string));
            _dtDetalle.Columns.Add("DESCRIPCION", typeof(string));
            _dtDetalle.Columns.Add("CANTIDAD", typeof(decimal));
            _dtDetalle.Columns.Add("UNIDAD", typeof(string));
            _dtDetalle.Columns.Add("ES_EXENTO", typeof(bool));
            _dtDetalle.Columns.Add("PRECIO", typeof(decimal));
            _dtDetalle.Columns.Add("GRAVADO", typeof(decimal));
            _dtDetalle.Columns.Add("EXENTO", typeof(decimal));
            _dtDetalle.Columns.Add("SUBTOTAL", typeof(decimal));

            var fila = _dtDetalle.NewRow();
            fila["CANTIDAD"] = 0m;
            _dtDetalle.Rows.Add(fila);

            gridControl1.DataSource = _dtDetalle;
            gridDETALLE_PROD.OptionsCustomization.AllowColumnMoving = false;
            gridDETALLE_PROD.OptionsCustomization.AllowSort = false;
            gridDETALLE_PROD.OptionsView.ShowGroupPanel = false;
            gridDETALLE_PROD.OptionsBehavior.Editable = true;
            gridDETALLE_PROD.OptionsNavigation.EnterMoveNextColumn = true;

            gridDETALLE_PROD.Columns["ID_PRODUCTO"].Visible = false;
            gridDETALLE_PROD.Columns["GRAVADO"].OptionsColumn.AllowEdit = false;
            gridDETALLE_PROD.Columns["EXENTO"].OptionsColumn.AllowEdit = false;
            gridDETALLE_PROD.Columns["SUBTOTAL"].OptionsColumn.AllowEdit = false;
            gridDETALLE_PROD.Columns["DESCRIPCION"].OptionsColumn.AllowEdit = false;
            gridDETALLE_PROD.Columns["UNIDAD"].OptionsColumn.AllowEdit = false;
            // Anchos fijos, sin poder redimensionar ni ordenar
            ConfigurarColumnaDetalle(gridDETALLE_PROD.Columns["COD_REF"], "CODIGO", 100);
            ConfigurarColumnaDetalle(gridDETALLE_PROD.Columns["DESCRIPCION"], "DESCRIPCIÓN", 320);
            ConfigurarColumnaDetalle(gridDETALLE_PROD.Columns["CANTIDAD"], "CANTIDAD", 90);
            ConfigurarColumnaDetalle(gridDETALLE_PROD.Columns["UNIDAD"], "UNIDAD", 90);
            ConfigurarColumnaDetalle(gridDETALLE_PROD.Columns["ES_EXENTO"], "Es Exento", 70);
            ConfigurarColumnaDetalle(gridDETALLE_PROD.Columns["PRECIO"], "PRECIO", 110);
            ConfigurarColumnaDetalle(gridDETALLE_PROD.Columns["GRAVADO"], "GRAVADO", 110);
            ConfigurarColumnaDetalle(gridDETALLE_PROD.Columns["EXENTO"], "EXENTO", 110);
            ConfigurarColumnaDetalle(gridDETALLE_PROD.Columns["SUBTOTAL"], "SUBTOTAL", 120);

            var view = gridControl1.MainView as GridView;
            if (view == null) return;
            view.Appearance.Row.ForeColor = Color.Black;
            view.Appearance.Row.Font = new Font("Segoe UI", 9f);
            view.Appearance.Row.Options.UseFont = true;
            view.Appearance.Row.Options.UseForeColor = true;
            view.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            view.Appearance.HeaderPanel.Options.UseFont = true;

            gridDETALLE_PROD.KeyDown += GridDetalle_KeyDown;            
            gridDETALLE_PROD.CellValueChanged += GridDetalle_CellValueChanged;
        }

        private void GridDetalle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (gridDETALLE_PROD.FocusedColumn?.FieldName != "COD_REF") return;

            string texto = gridDETALLE_PROD.ActiveEditor?.Text?.Trim();
            if (texto != "*") return;

            e.Handled = true;
            AbrirBusquedaProducto();
        }

        private void AbrirBusquedaProducto()
        {
            var config = new BusquedaConfig
            {
                StoredProcedure = "DISTRIB.SP_CLQ_ENCA",
                Accion = "BUSCAR_PRODUCTO",
                Columnas = new Dictionary<string, string>
                {
                    { "COD_REF",     "CÓDIGO" },
                    { "DESCRIPCION", "DESCRIPCIÓN" }
                },
                Anchos = new Dictionary<string, int>
                {
                    { "COD_REF", 100 }, { "DESCRIPCION", 350 }
                },                
                ParametrosExtra = _parametrosTipoClq
            };

            using (var frm = new frmBusquedaGenerica(config))
            {
                // Posicionar justo debajo del grid, mismo patrón que AbrirBusquedaCuenta
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

                if (frm.ShowDialog(this) == DialogResult.OK && frm.FilaSeleccionada != null)
                {
                    var fila = frm.FilaSeleccionada;
                    bool esExento = fila["ES_EXENTO"] != DBNull.Value && Convert.ToBoolean(fila["ES_EXENTO"]);

                    _actualizandoDesdeBusqueda = true;
                    gridDETALLE_PROD.SetFocusedRowCellValue("ID_PRODUCTO", fila["ID_PRODUCTO"]);
                    gridDETALLE_PROD.SetFocusedRowCellValue("COD_REF", fila["COD_REF"]);
                    gridDETALLE_PROD.SetFocusedRowCellValue("DESCRIPCION", fila["DESCRIPCION"]);
                    gridDETALLE_PROD.SetFocusedRowCellValue("UNIDAD", SafeStr(fila, "UNIMEDIDA"));
                    gridDETALLE_PROD.SetFocusedRowCellValue("ES_EXENTO", esExento);

                    gridDETALLE_PROD.FocusedColumn = gridDETALLE_PROD.Columns["CANTIDAD"];   
                    gridDETALLE_PROD.ShowEditor();
                    _actualizandoDesdeBusqueda = false;  

                    RecalcularDetalleYEncabezado();
                }
                else
                {
                    gridDETALLE_PROD.SetFocusedRowCellValue("COD_REF", string.Empty);
                }
            }
        }

        // ============================================================
        // Cálculo financiero
        // ============================================================
        private void ConfigurarTextBoxDecimal(params TextBox[] textboxes)
        {
            foreach (var tb in textboxes)
            {
                tb.Enter += (s, e) => ((TextBox)s).SelectAll();
                tb.KeyPress += TxtDecimal_KeyPress;
                tb.Leave += TxtDecimal_Leave;
            }
        }

        private void TxtDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b') { e.Handled = true; return; }
            if (e.KeyChar == '.' && tb.Text.Contains(".")) e.Handled = true;
        }

        private void TxtDecimal_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text)) { tb.Text = ""; return; }
            if (!decimal.TryParse(tb.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor)) { tb.Text = ""; return; }
            tb.Text = valor == 0m ? "" : valor.ToString("N2");
        }

        private void EngancharRecalculo(params TextBox[] textboxes)
        {
            foreach (var tb in textboxes)
                tb.Leave += (s, e) => RecalcularTotales();
        }

        private decimal ObtenerDecimal(TextBox tb)
            => string.IsNullOrWhiteSpace(tb.Text) ? 0 : (decimal.TryParse(tb.Text, out decimal v) ? v : 0);

        private void AsignarDecimal(TextBox tb, decimal valor) => tb.Text = valor == 0 ? "" : valor.ToString("N2");

        // ============================================================
        // Detalle -> Encabezado (fuente de verdad es el grid)
        // ============================================================
        private void GridDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string campo = e.Column.FieldName;

            if (campo == "COD_REF" && !_actualizandoDesdeBusqueda)
            {
                string codigo = e.Value?.ToString()?.Trim() ?? "";
                if (codigo != _ultimoCodigoValidado)
                    ValidarCodigoProductoManual(codigo);
                return;
            }

            if (campo != "CANTIDAD" && campo != "PRECIO" && campo != "ES_EXENTO") return;
            RecalcularDetalleYEncabezado();
        }
        
        private void ValidarCodigoProductoManual(string codigo)
        {
            codigo = codigo?.Trim();

            if (string.IsNullOrWhiteSpace(codigo))
            {
                LimpiarFilaProducto();
                return;
            }

            try
            {
                var dt = _dal.EjecutarConsulta("DISTRIB.SP_CLQ_ENCA", new
                {
                    ACCION = "BUSCAR_PRODUCTO",
                    FILTRO = codigo,
                    CODIGO_ENTIDAD = _codigoEntidad
                });

                // Coincidencia EXACTA (el FILTRO usa LIKE, podría traer varias)
                DataRow fila = null;
                foreach (DataRow r in dt.Rows)
                {
                    if (string.Equals(r["COD_REF"].ToString().Trim(), codigo, StringComparison.OrdinalIgnoreCase))
                    {
                        fila = r;
                        break;
                    }
                }

                if (fila == null)
                {
                    XtraMessageBox.Show($"El producto con código '{codigo}' no existe.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LimpiarFilaProducto();
                    gridDETALLE_PROD.FocusedColumn = gridDETALLE_PROD.Columns["COD_REF"];
                    gridDETALLE_PROD.ShowEditor();
                    return;
                }

                bool esExento = fila["ES_EXENTO"] != DBNull.Value && Convert.ToBoolean(fila["ES_EXENTO"]);

                _actualizandoDesdeBusqueda = true;
                gridDETALLE_PROD.SetFocusedRowCellValue("ID_PRODUCTO", fila["ID_PRODUCTO"]);
                gridDETALLE_PROD.SetFocusedRowCellValue("DESCRIPCION", fila["DESCRIPCION"]);
                gridDETALLE_PROD.SetFocusedRowCellValue("UNIDAD", SafeStr(fila, "UNIMEDIDA"));
                gridDETALLE_PROD.SetFocusedRowCellValue("ES_EXENTO", esExento);
                _actualizandoDesdeBusqueda = false;

                RecalcularDetalleYEncabezado();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error al validar el producto: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFilaProducto()
        {
            _actualizandoDesdeBusqueda = true;
            gridDETALLE_PROD.SetFocusedRowCellValue("ID_PRODUCTO", DBNull.Value);
            gridDETALLE_PROD.SetFocusedRowCellValue("COD_REF", string.Empty);
            gridDETALLE_PROD.SetFocusedRowCellValue("DESCRIPCION", string.Empty);
            gridDETALLE_PROD.SetFocusedRowCellValue("UNIDAD", string.Empty);
            gridDETALLE_PROD.SetFocusedRowCellValue("ES_EXENTO", false);
            _actualizandoDesdeBusqueda = false;
        }

        // CANTIDAD x PRECIO -> GRAVADO o EXENTO (según ES_EXENTO) -> sube al encabezado
        private void RecalcularDetalleYEncabezado()
        {
            decimal cantidad = ToDecimal(_dtDetalle.Rows[0]["CANTIDAD"]);
            decimal precio = ToDecimal(_dtDetalle.Rows[0]["PRECIO"]);
            bool esExento = _dtDetalle.Rows[0]["ES_EXENTO"] != DBNull.Value && Convert.ToBoolean(_dtDetalle.Rows[0]["ES_EXENTO"]);

            decimal monto = Calculo.Redondear(cantidad * precio, 2);

            decimal gravado = esExento ? 0 : monto;
            decimal exento = esExento ? monto : 0;
            decimal subtotalDetalle = gravado + exento;

            _actualizandoDesdeBusqueda = true;
            gridDETALLE_PROD.SetFocusedRowCellValue("GRAVADO", gravado);
            gridDETALLE_PROD.SetFocusedRowCellValue("EXENTO", exento);
            gridDETALLE_PROD.SetFocusedRowCellValue("SUBTOTAL", subtotalDetalle);
            _actualizandoDesdeBusqueda = false;

            // Sube al encabezado — dispara el cálculo de descuento/IVA/total
            AsignarDecimal(txtGRAVADA, gravado);
            AsignarDecimal(txtEXENTA, exento);
            RecalcularTotales();
        }

        // SUBTOTAL_BRUTO = GRAVADO+EXENTO; DESCUENTO sobre el bruto;
        // IVA = 13% sobre GRAVADO neto de descuento; TOTAL = SUBTOTAL + IVA - IVAR + PERCEPCION
        private void RecalcularTotales()
        {
            decimal gravado = ObtenerDecimal(txtGRAVADA);
            decimal exenta = ObtenerDecimal(txtEXENTA);
            decimal porcDescto = ObtenerDecimal(txtPORC_DESCUENTO);
            decimal ivar = ObtenerDecimal(txtIVAR);
            decimal percepcion = ObtenerDecimal(txtPERCEPCION);

            decimal subtotalBruto = gravado + exenta;
            decimal descuento = Calculo.Redondear(subtotalBruto * porcDescto / 100m, 2);
            decimal subtotal = subtotalBruto - descuento;

            decimal gravadoNeto = gravado - Calculo.Redondear(gravado * porcDescto / 100m, 2);
            decimal iva = Calculo.Redondear(gravadoNeto * 0.13m, 2);

            decimal total = subtotal + iva - ivar + percepcion;

            AsignarDecimal(txtDESCUENTO, descuento);
            AsignarDecimal(txtSUBTOTAL, subtotal);
            AsignarDecimal(txtIVA, iva);
            AsignarDecimal(txtTOTAL, total);
        }

        // ============================================================
        // Cargar / Guardar
        // ============================================================
        private void CargarClqExistente(int idClqEnca)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var ds = _dal.EjecutarMultiple("DISTRIB.SP_CLQ_ENCA", new
                {
                    ACCION = "OBTENER",
                    ID_CLQ_ENCA = idClqEnca
                });

                if (ds.Tables[0].Rows.Count == 0)
                {
                    XtraMessageBox.Show("No se encontró el comprobante solicitado.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    return;
                }

                DataRow r = ds.Tables[0].Rows[0];

                _idEntidad = ToInt(r["ID_ENTIDAD"]);
                _codigoEntidad = SafeStr(r, "CODIGO_ENTIDAD");

                txtCODIGO_CLQ.Tag = SafeStr(r, "ID_TIPO_CLQ");
                txtCODIGO_CLQ.Text = SafeStr(r, "CODIGO_CLQ");
                txtNOMBRE_TIPO_CLQ.Text = SafeStr(r, "NOMBRE_TIPO_CLQ");
                txtSERIE.Text = SafeStr(r, "SERIE");
                txtRESOLUCION.Text = SafeStr(r, "RESOLUCION");
                txtNUMERO_CLQ.Text = SafeStr(r, "NUMERO_CLQ");
                dteFECHA.DateTime = r["FECHA"] != DBNull.Value ? Convert.ToDateTime(r["FECHA"]) : DateTime.Today;

                txtCLIENTE.Text = _codigoEntidad;
                txtNOMBRE_CLIENTE.Text = SafeStr(r, "NOMBRE_ENTIDAD");
                txtNRC.Text = SafeStr(r, "NRC");
                txtNIT.Text = SafeStr(r, "NIT");

                cbxCONDICION_PAGO.SelectedValue = ToInt(r["ID_COND_PAGO"]);
                cbxSUCURSAL.SelectedValue = ToInt(r["ID_SUCURSAL"]);
                cbxCENTRO_COSTO.SelectedValue = ToInt(r["ID_CENTRO"]);
                cbxTIPO_VENTA.SelectedValue = ToInt(r["ID_TVTA"]);
                cbxLUGAR_DESPACHO.SelectedValue = ToInt(r["ID_LDESPACHO"]);
                cbxGENTRAS.SelectedValue = ToInt(r["ID_GVTA"]);

                AsignarDecimal(txtGRAVADA, ToDecimal(r["GRAVADO"]));
                AsignarDecimal(txtEXENTA, ToDecimal(r["EXENTO"]));
                AsignarDecimal(txtPORC_DESCUENTO, ToDecimal(r["PORC_DESCTO"]));
                AsignarDecimal(txtDESCUENTO, ToDecimal(r["DESCUENTO"]));
                AsignarDecimal(txtSUBTOTAL, ToDecimal(r["SUBTOTAL"]));
                AsignarDecimal(txtIVA, ToDecimal(r["IVA"]));
                AsignarDecimal(txtIVAR, ToDecimal(r["IVAR"]));
                AsignarDecimal(txtPERCEPCION, ToDecimal(r["PERCEPCION"]));
                AsignarDecimal(txtTOTAL, ToDecimal(r["TOTAL"]));

                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    DataRow d = ds.Tables[1].Rows[0];
                    _dtDetalle.Rows[0]["ID_PRODUCTO"] = d["ID_PRODUCTO"];
                    _dtDetalle.Rows[0]["COD_REF"] = SafeStr(d, "COD_REF");
                    _dtDetalle.Rows[0]["DESCRIPCION"] = SafeStr(d, "DESCRIPCION");
                    _dtDetalle.Rows[0]["CANTIDAD"] = ToDecimal(d["CANTIDAD"]);
                    _dtDetalle.Rows[0]["UNIDAD"] = SafeStr(d, "UNIDAD");
                    _dtDetalle.Rows[0]["PRECIO"] = ToDecimal(d["PRECIO"]);
                    _dtDetalle.Rows[0]["GRAVADO"] = ToDecimal(d["GRAVADO"]);
                    _dtDetalle.Rows[0]["EXENTO"] = ToDecimal(d["EXENTO"]);
                    _dtDetalle.Rows[0]["SUBTOTAL"] = ToDecimal(d["SUBTOTAL"]);
                    gridControl1.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar el comprobante:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private bool ValidarCampos()
        {
            if (_idEntidad == 0)
            {
                XtraMessageBox.Show("Debe seleccionar un cliente.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCLIENTE.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(SafeStr2(txtCODIGO_CLQ.Tag)))
            {
                XtraMessageBox.Show("Debe seleccionar el tipo de documento.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCODIGO_CLQ.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNUMERO_CLQ.Text))
            {
                XtraMessageBox.Show("Debe digitar el número de CLQ.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNUMERO_CLQ.Focus();
                return false;
            }

            if (!ValidarCombo(cbxSUCURSAL, "la sucursal")) return false;
            if (!ValidarCombo(cbxCENTRO_COSTO, "el centro de costo")) return false;
            if (!ValidarCombo(cbxCONDICION_PAGO, "la condición de pago")) return false;
            if (!ValidarCombo(cbxTIPO_VENTA, "el tipo de venta")) return false;
            if (!ValidarCombo(cbxLUGAR_DESPACHO, "el lugar de despacho")) return false;
            if (!ValidarCombo(cbxGENTRAS, "la generación de transacción")) return false;

            // Producto del detalle
            string codRef = SafeStr2(_dtDetalle.Rows[0]["COD_REF"]);
            if (string.IsNullOrWhiteSpace(codRef) || _dtDetalle.Rows[0]["ID_PRODUCTO"] == DBNull.Value)
            {
                XtraMessageBox.Show("Debe seleccionar un producto válido en el detalle.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridDETALLE_PROD.FocusedColumn = gridDETALLE_PROD.Columns["COD_REF"];
                gridDETALLE_PROD.ShowEditor();
                return false;
            }

            decimal cantidad = ToDecimal(_dtDetalle.Rows[0]["CANTIDAD"]);
            if (cantidad <= 0)
            {
                XtraMessageBox.Show("La cantidad del producto debe ser mayor a cero.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridDETALLE_PROD.FocusedColumn = gridDETALLE_PROD.Columns["CANTIDAD"];
                gridDETALLE_PROD.ShowEditor();
                return false;
            }

            decimal precio = ToDecimal(_dtDetalle.Rows[0]["PRECIO"]);
            if (precio <= 0)
            {
                XtraMessageBox.Show("El precio del producto debe ser mayor a cero.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridDETALLE_PROD.FocusedColumn = gridDETALLE_PROD.Columns["PRECIO"];
                gridDETALLE_PROD.ShowEditor();
                return false;
            }

            if (ObtenerDecimal(txtTOTAL) <= 0)
            {
                XtraMessageBox.Show("El total del documento debe ser mayor a cero.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void GuardarDocumento()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var parametros = new
                {
                    ACCION = "GUARDAR",
                    ID_CLQ_ENCA = IdClqEnca,
                    ID_TIPO_CLQ = ParseIntNullable(SafeStr2(txtCODIGO_CLQ.Tag)),
                    CODIGO_CLQ = NullIfEmpty(txtCODIGO_CLQ.Text),
                    NUMERO_CLQ = ParseIntNullable(txtNUMERO_CLQ.Text),
                    SERIE = NullIfEmpty(txtSERIE.Text),
                    RESOLUCION = NullIfEmpty(txtRESOLUCION.Text),
                    FECHA = dteFECHA.DateTime,
                    ID_ENTIDAD = _idEntidad,
                    CODIGO_ENTIDAD = _codigoEntidad,
                    ID_COND_PAGO = ObtenerIdCombo(cbxCONDICION_PAGO),
                    ID_ZAFRA = ObtenerIdCombo(cbxZAFRA),
                    ID_SUCURSAL = ObtenerIdCombo(cbxSUCURSAL),
                    ID_CENTRO = ObtenerIdCombo(cbxCENTRO_COSTO),
                    GRAVADO = ObtenerDecimal(txtGRAVADA),
                    EXENTO = ObtenerDecimal(txtEXENTA),
                    PORC_DESCTO = ObtenerDecimal(txtPORC_DESCUENTO),
                    DESCUENTO = ObtenerDecimal(txtDESCUENTO),
                    SUBTOTAL = ObtenerDecimal(txtSUBTOTAL),
                    IVA = ObtenerDecimal(txtIVA),
                    IVAR = ObtenerDecimal(txtIVAR),
                    PERCEPCION = ObtenerDecimal(txtPERCEPCION),
                    TOTAL = ObtenerDecimal(txtTOTAL),
                    ID_GVTA = ObtenerIdCombo(cbxGENTRAS),
                    ID_TVTA = ObtenerIdCombo(cbxTIPO_VENTA),
                    ID_LDESPACHO = ObtenerIdCombo(cbxLUGAR_DESPACHO),
                    USUARIO = Configuracion.UsuarioActual,
                    ID_PRODUCTO = _dtDetalle.Rows[0]["ID_PRODUCTO"] == DBNull.Value ? (int?)null : Convert.ToInt32(_dtDetalle.Rows[0]["ID_PRODUCTO"]),
                    DET_COD_REF = NullIfEmpty(SafeStr2(_dtDetalle.Rows[0]["COD_REF"])),
                    DET_DESCRIPCION = NullIfEmpty(SafeStr2(_dtDetalle.Rows[0]["DESCRIPCION"])),
                    DET_CANTIDAD = ToDecimal(_dtDetalle.Rows[0]["CANTIDAD"]),
                    DET_UNIDAD = NullIfEmpty(SafeStr2(_dtDetalle.Rows[0]["UNIDAD"])),
                    DET_ES_EXENTO = _dtDetalle.Rows[0]["ES_EXENTO"] != DBNull.Value && Convert.ToBoolean(_dtDetalle.Rows[0]["ES_EXENTO"]),
                    DET_PRECIO = ToDecimal(_dtDetalle.Rows[0]["PRECIO"]),
                    DET_GRAVADO = ToDecimal(_dtDetalle.Rows[0]["GRAVADO"]),
                    DET_EXENTO = ToDecimal(_dtDetalle.Rows[0]["EXENTO"]),
                    DET_SUBTOTAL = ToDecimal(_dtDetalle.Rows[0]["SUBTOTAL"])
                };

                DataTable dt = _dal.EjecutarConsulta("DISTRIB.SP_CLQ_ENCA", parametros);

                if (dt.Rows.Count == 0)
                    throw new Exception("El SP no devolvió el ID generado.");

                IdClqEnca = Convert.ToInt32(dt.Rows[0]["ID_GENERADO"]);
                ConfigurarCRUD(EstadoFormulario.Guardado);

                XtraMessageBox.Show("Comprobante guardado correctamente.",
                    "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException sqlEx)
            {
                XtraMessageBox.Show(sqlEx.Message, "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al guardar el comprobante:\n\n" + ex.Message,
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
        private static string NullIfEmpty(string s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
        private static string SafeStr(DataRow r, string campo) => r.Table.Columns.Contains(campo) && r[campo] != DBNull.Value ? r[campo].ToString() : "";
        private static string SafeStr2(object v) => v == null || v == DBNull.Value ? "" : v.ToString();
        private static int ToInt(object v) => v == null || v == DBNull.Value ? 0 : Convert.ToInt32(v);
        private static decimal ToDecimal(object v) => v == null || v == DBNull.Value ? 0 : Convert.ToDecimal(v);
        private static int? ParseIntNullable(string s) => int.TryParse(s, out int v) ? v : (int?)null;
        private void ConfigurarColumnaDetalle(DevExpress.XtraGrid.Columns.GridColumn col, string caption, int ancho)
        {
            if (col == null) return;
            col.Caption = caption;
            col.Width = ancho;
            col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            col.OptionsColumn.FixedWidth = true;   // impide que el usuario lo redimensione arrastrando
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (IdClqEnca == 0 && txtCLIENTE.Text.Trim() != "")
            {
                DialogResult resultado = XtraMessageBox.Show("¿Está seguro de cerrar sin guardar la información?",
                                             "Validación",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Information);
                if (resultado == DialogResult.Yes)
                {
                    Close();
                }               
            }
            else
            {
                Close();
            }            
        }

       

        private bool ValidarCombo(ComboBox cbx, string nombreCampo)
        {
            if (!TieneSeleccion(cbx))
            {
                XtraMessageBox.Show($"Debe seleccionar {nombreCampo}.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbx.Focus();
                return false;
            }
            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;
            GuardarDocumento();
        }
    }

    public class ParametrosTipoClqBusqueda
    {
        public string CODIGO_ENTIDAD { get; set; }
    }
}