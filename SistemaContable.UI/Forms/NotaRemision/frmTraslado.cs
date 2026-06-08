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
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;

namespace SistemaContable.UI.Forms.NotaRemision
{
    public partial class frmTraslado : Form
    {
        private enum EstadoFormulario
        {
            Nuevo,
            Guardado,
            Validado
        }

        private readonly DALBase _dal = new DALBase();
        private DataTable _dtDeta;  // DataTable que alimenta el grid
        private int _idNR = 0;
        private int _idEntidad = 0;
        private string _codigoEntidad = string.Empty;
        private string _columnaAnteriorGrid = string.Empty;
        public int IdTraslado { get; set; } = 0;



        public frmTraslado()
        {
            InitializeComponent();
            this.SetStyle(
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.AllPaintingInWmPaint,
                 true);
            this.UpdateStyles();
        }

        private void frmTraslado_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);
            CargarTipoDte();
            CargarSiguienteNumNotaRemision();
            mskFECHA_EMISION.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtCOD_GENERACION.Text = DALBase.NuevoGUID();

            InicializarGridDetalle();
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
                    ParametrosExtra = new { ROL = "TRASL" }
                },
                fila => AsignarProveedor(fila)
            );
            txtPROVEEDOR.Leave += txtPROVEEDOR_Leave;


            ConfigurarTextBoxDecimal(
                txtGRAVADA, txtTOTAL
            );




        }

        private void CargarTipoDte()
        {
            DataTable dt = _dal.EjecutarConsulta("SP_TIPO_DTE",
                new
                {
                    ACCION = "OBTENER",
                    ID_TIPO_DTE = 3
                });

            cbxTIPO_DTE.DataSource = dt;
            cbxTIPO_DTE.ValueMember = "ID_TIPO_DTE";
            cbxTIPO_DTE.DisplayMember = "ABREVIATURA_E";
        }

        private void CargarSiguienteNumNotaRemision()
        {
            var num = _dal.ObtenerNumeracionPrevia("NR", null);   // sin año -> corrido
            if (num == null)
            {
                MessageBox.Show(
                    "No existe numeración activa para la Nota de Remision.\n" +
                    "Configúrela en DOCUMENTO_NUMERACION antes de continuar.",
                    "Numeración no encontrada",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumero_NR.Text = "";
                return;
            }
            txtNumero_NR.Text = num.SiguienteNumeroFormateado();
        }

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

            txtDIRECCION.Text = fila["COMPLEMENTO"].ToString();

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

            txtDIRECCION.Text = string.Empty;
        }
        private void ConfigurarTextBoxDecimal(params TextBox[] textboxes)
        {
            foreach (var tb in textboxes)
            {
                tb.Enter += TxtDecimal_Enter;
                tb.KeyPress += TxtDecimal_KeyPress;
                tb.Leave += TxtDecimal_Leave;
            }
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

        #region Grid detalle

        private void InicializarGridDetalle()
        {
            // Crear DataTable con las columnas de CHEQUE_PARTIDA
            _dtDeta = new DataTable();

            _dtDeta.Columns.Add("ID_PRODUCTO", typeof(int));
            _dtDeta.Columns.Add("COD_REF", typeof(string));
            _dtDeta.Columns.Add("DESCRIPCION", typeof(string));
            _dtDeta.Columns.Add("ID_UNIDAD_MEDIDA", typeof(int));
            _dtDeta.Columns.Add("UNIDAD_MEDIDA", typeof(string));
            _dtDeta.Columns.Add("CANTIDAD", typeof(decimal));

            _dtDeta.Columns.Add("PRECIO", typeof(decimal));

            _dtDeta.Columns.Add("TOTAL", typeof(decimal));

            // Agregar fila vacía inicial
            AgregarFilaVacia();

            // Enlazar al grid
            gridControl1.DataSource = _dtDeta;

            // Configurar columnas visibles con títulos
            var view = gridControl1.MainView as GridView;
            if (view == null) return;

            view.Columns.Clear();
            view.PopulateColumns();

            ConfigurarColumna(view, "ID_PRODUCTO", "ID_PRODUCTO", 80, false, false);
            ConfigurarColumna(view, "COD_REF", "COD_REF", 80, false, true);
            ConfigurarColumna(view, "DESCRIPCION", "DESCRIPCION", 350, false, true);
            ConfigurarColumna(view, "ID_UNIDAD_MEDIDA", "ID_UNIDAD_MEDIDA", 80, false, false);
            ConfigurarColumna(view, "UNIDAD_MEDIDA", "UM", 100, true, true);
            ConfigurarColumna(view, "CANTIDAD", "CANTIDAD", 75, false, true);
            ConfigurarColumna(view, "PRECIO", "PRECIO", 75, true, true);
            ConfigurarColumna(view, "TOTAL", "TOTAL", 75, true, true);

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
                if (ev.Column.FieldName == "CANTIDAD")
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

            view.CellValueChanged += (s, ev) =>
            {
                if (ev.Column.FieldName == "CANTIDAD" || ev.Column.FieldName == "PRECIO")
                { ActualizarCuadre(); }

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
            //ActualizarCuadre();
        }



        private void ConfigurarColumna(GridView view, string fieldName,
        string caption, int width, bool readOnly, bool visible)
        {
            var col = view.Columns.ColumnByFieldName(fieldName);

            if (col != null)
            {
                col.Caption = caption;
                col.Width = width;
                // ✅ usar el parámetro visible
                col.Visible = visible;

                // ✅ validación correcta
                col.OptionsColumn.ReadOnly = readOnly;

                // ✅ opcional (recomendado)
                col.OptionsColumn.AllowEdit = !readOnly;
            }
        }


        private void AgregarFilaVacia()
        {
            var fila = _dtDeta.NewRow();

            fila["ID_PRODUCTO"] = DBNull.Value;
            fila["COD_REF"] = string.Empty;
            fila["DESCRIPCION"] = string.Empty;
            fila["ID_UNIDAD_MEDIDA"] = DBNull.Value;
            fila["UNIDAD_MEDIDA"] = string.Empty;
            fila["CANTIDAD"] = 0m;
            fila["PRECIO"] = 0m;

            fila["TOTAL"] = 0m;
            _dtDeta.Rows.Add(fila);
        }



        private void AgregarFilaPartida(string cod_ref)
        {
            // Limpiar filas vacías antes de agregar
            for (int i = _dtDeta.Rows.Count - 1; i >= 0; i--)
            {
                var r = _dtDeta.Rows[i];
                if (string.IsNullOrWhiteSpace(r["COD_REF"].ToString()) &&
                    Convert.ToDecimal(r["CANTIDAD"]) == 0 &&
                    Convert.ToDecimal(r["TOTAL"]) == 0)
                    _dtDeta.Rows.RemoveAt(i);
            }

            // Agregar fila con la cuenta contable
            var fila = _dtDeta.NewRow();



            fila["ID_PRODUCTO"] = DBNull.Value;
            fila["COD_REF"] = cod_ref;
            fila["DESCRIPCION"] = string.Empty;
            fila["ID_UNIDAD_MEDIDA"] = DBNull.Value;
            fila["UNIDAD_MEDIDA"] = string.Empty;
            fila["CANTIDAD"] = string.Empty;
            fila["PRECIO"] = 0m;
            fila["TOTAL"] = 0m;
            _dtDeta.Rows.Add(fila);

            // Agregar fila vacía para siguiente ingreso
            AgregarFilaVacia();
            // Agregar fila vacía para siguiente ingreso
            ActualizarCuadre();
        }

        private void GridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            var view = sender as GridView;
            if (view == null) return;

            string colActual = view.FocusedColumn?.FieldName;

            // Verificar * en CTACONTABLE PRIMERO antes de cualquier otra acción
            if (colActual == "COD_REF")
            {
                string texto = view.ActiveEditor?.Text?.Trim();
                if (string.IsNullOrEmpty(texto))
                    texto = view.GetFocusedDisplayText()?.Trim();

                if (texto == "*")
                {
                    e.Handled = true;
                    AbrirBusquedaCuenta(view);
                    return; // salir sin hacer nada más
                }
            }

            // Si está en CARGO presiona Enter → saltar directo a CTACONTABLE de la siguiente fila
            if (colActual == "CANTIDAD")
            {
                e.Handled = true;
                view.CloseEditor();
                int filaActual = view.FocusedRowHandle;

                if (filaActual == _dtDeta.Rows.Count - 1)
                    AgregarFilaVacia();

                view.FocusedRowHandle = filaActual + 1;
                view.FocusedColumn = view.Columns["COD_REF"];
                view.ShowEditor();
                return;
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

                if (filaActual == _dtDeta.Rows.Count - 1)
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

            if (_columnaAnteriorGrid == "COD_REF" &&
                e.FocusedColumn?.FieldName == "DETALLE")
            {
                string cta = view.GetFocusedRowCellValue("COD_REF")?.ToString();
                if (string.IsNullOrWhiteSpace(cta)) return;

                string detalleActual = view.GetFocusedRowCellValue("DESCRIPCION")?.ToString();
                if (!string.IsNullOrWhiteSpace(detalleActual)) return;

                //string detalle = $"CH # {txtNUMERO_CHEQUE.Text.Trim()} " +
                //                 $"{txtNOMBRE_CHEQUE.Text.Trim()}";

                //view.SetFocusedRowCellValue("DETALLE", detalle);
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

        private void AbrirBusquedaCuenta(GridView view)
        {
            var config = new BusquedaConfig
            {
                StoredProcedure = "[EINVENTARIO].[SP_PRODUCTO]",
                Columnas = new Dictionary<string, string>
                {
                    { "ID_PRODUCTO",        "ID_PRODUCTO" },
                    { "COD_REF",        "COD_REF" },
                    { "DESCRIPCION", "NOMBRE" },
                    { "ID_UNIDAD_MEDIDA",        "ID_UNIDAD_MEDIDA" },
                    { "UNIMEDIDA", "UNIMEDIDA" }
                },
                Anchos = new Dictionary<string, int>
                    {
                        { "COD_REF", 100 },
                        { "DESCRIPCION",         300 },
                        { "UNIMEDIDA",            120 }
                    },
                ColumnasOcultas = new List<string>
                {
                    "ID_PRODUCTO",
                    "ID_UNIDAD_MEDIDA"
                },
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
                    view.SetFocusedRowCellValue("COD_REF",
                        frm.FilaSeleccionada["COD_REF"].ToString());

                    view.SetFocusedRowCellValue("DESCRIPCION",
                        frm.FilaSeleccionada["DESCRIPCION"].ToString());


                    view.SetFocusedRowCellValue("UNIDAD_MEDIDA",
                       frm.FilaSeleccionada["UNIMEDIDA"].ToString());

                    view.SetFocusedRowCellValue("UNIDAD_MEDIDA",
                       frm.FilaSeleccionada["UNIMEDIDA"].ToString());


                    view.FocusedColumn = view.Columns["DESCRIPCION"];
                    view.ShowEditor();
                }
                else
                {
                    view.SetFocusedRowCellValue("COD_REF", string.Empty);
                }
            }
        }

        private decimal CalcularTotalCargo()
        {
            decimal total = 0;
            foreach (DataRow fila in _dtDeta.Rows)
                total += Convert.ToDecimal(fila["TOTAL"]);
            return total;
        }

        private void ActualizarCuadre()
        {
            decimal totalN = 0;

            foreach (DataRow fila in _dtDeta.Rows)
            {
                // Leer directo como decimal sin pasar por ToString
                if (fila["TOTAL"] != DBNull.Value)
                    totalN += Convert.ToDecimal(fila["TOTAL"]);

            }



            txtGRAVADA.Text = $"{totalN:N2}";
            txtTOTAL.Text = $"{totalN:N2}";



        }
        #endregion

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNOMBRE_PROVEEDOR.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Seleccione un Cliente.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPROVEEDOR.Focus();
                return false;
            }

            if (!FormHelper.ValidarFecha(mskFECHA_EMISION, "Fecha de la Nota Remision"))
                return false;



            // Verificar que haya al menos una línea en la partida
            bool tieneLineas = false;
            foreach (DataRow fila in _dtDeta.Rows)
            {
                if (!string.IsNullOrWhiteSpace(fila["COD_REF"].ToString()))
                {
                    tieneLineas = true;
                    break;
                }
            }

            if (!tieneLineas)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Debe ingresar al menos un producto al detalle.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                // 1. Guardar encabezado del NR
                var dtNR = _dal.EjecutarConsulta("[EDTE].SP_NOTA_REMISION", new
                {
                    ACCION = "GUARDAR",
                    ID_NTREMISIONENC = IdTraslado,
                    ID_EMISOR = 1,
                    ID_SUCURSAL = 1,
                    ID_ALMACEN = Configuracion.Id_Almacen,
                    ID_CAJERO = Configuracion.Id_Cajero,
                    ID_CONDPAGO = 1,
                    ID_CLIENTE = _idEntidad,
                    COD_REF = _codigoEntidad,
                    TPDOC = "NRE",
                    FECHA = FormHelper.ObtenerFecha(mskFECHA_EMISION),
                    SALFEC = FormHelper.ObtenerSalfec(mskFECHA_EMISION),
                    NUMDOC = txtNumero_NR.Text,
                    CODGENERACION = txtCOD_GENERACION.Text,
                    NUMCONTROL = txtNUM_CONTROL.Text,
                    NUMINTERNO = txtNumero_NR.Text,
                    AFECTA = txtGRAVADA.Text,
                    TOTALVENTA = txtTOTAL.Text,
                    TOTALLETRAS = string.Empty,
                    OBSERVACIONES = txtObservacion.Text,
                    TPCONTRIBUYENTE = string.Empty,
                    USER_CREA = Configuracion.UsuarioActual,
                    TPCONTRIBUYENTEEMISOR = string.Empty,
                    TPDOCRECTOR = string.Empty,
                    NDOCRECTOR = string.Empty,
                    TRANSPORTE = string.Empty,
                    MOTORISTA = string.Empty,
                    LICENCIA = string.Empty,
                    PLACA = string.Empty,
                    MARCHAMOS = string.Empty,
                    OPCIONNR = "NR",
                    ID_ZAFRA = string.Empty


                });

                if (dtNR.Rows.Count == 0) return;
                _idNR = Convert.ToInt32(dtNR.Rows[0]["ID_GENERADO"]);
                txtNUM_CONTROL.Text = Convert.ToString(dtNR.Rows[0]["NCONT"]);
                txtNumero_NR.Text = Convert.ToString(dtNR.Rows[0]["INTERN"]);

                /*   // 3. Guardar líneas de la partida contable
               foreach (DataRow fila in _dtPartida.Rows)
               {
                   string cta = fila["CTACONTABLE"].ToString().Trim();
                   if (string.IsNullOrWhiteSpace(cta)) continue;

                   _dal.EjecutarSinRetorno("SP_CHEQUE_PARTIDA", new
                   {
                       ACCION = "GUARDAR",
                       ID_CHEQUE_PAR = 0,
                       ID_CHEQUE = _idCheque,
                       CTACONTABLE = cta,
                       DETALLE = fila["DETALLE"].ToString().Trim(),
                       CARGO = Convert.ToDecimal(fila["CARGO"]),
                       ABONO = Convert.ToDecimal(fila["ABONO"]),
                       USUARIO_CREA = Configuracion.UsuarioActual,
                       USUARIO_ACT = Configuracion.UsuarioActual
                   });
               }*/

                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Nota Remision guarda correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    $"Error al guardar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimirQuedan_Click(object sender, EventArgs e)
        {

        }

        private void btnCorreo_Click(object sender, EventArgs e)
        {

        }
    }


}
