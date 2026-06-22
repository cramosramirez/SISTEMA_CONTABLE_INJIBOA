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
using SistemaContable.RP;

namespace SistemaContable.UI.Forms.Bancos
{
    public partial class frmCheques : DevExpress.XtraEditors.XtraForm
    {
        private enum EstadoFormulario
        {
            Nuevo,
            Guardado,
            Impreso
        }
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtPartida;  // DataTable que alimenta el grid
        private int _idCheque = 0;     // 0 = nuevo, >0 = edición
        private string _columnaAnteriorGrid = string.Empty;
        private DataTable _documentosPago; // Documentos a pagar mediante Quedan
        private string _uidEnlaceCheque = string.Empty;

     

        public frmCheques()
        {            
            InitializeComponent();
        }

        private void frmCheques_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);
            mskFECHA_CHEQUE.Text = DateTime.Today.ToString("dd/MM/yyyy");

            if (!VerificarCCFsHuerfanos())
            {
                _uidEnlaceCheque = FormHelper.ObtenerUUID();  
            }

            InicializarGridPartida();

            FormHelper.RegistrarBusqueda(
                txtOPERACION,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_TIPO_PARTIDA",
                    Accion = "BUSCAR_PARA_CHEQUE",
                    Columnas = new Dictionary<string, string>
                    {
                        { "CODIGO_PAR", "CODIGO" },
                        { "NOMBRE",     "NOMBRE" }
                    }
                },
                fila =>
                {
                    txtOPERACION.Tag = fila["ID_TIPO_PARTIDA"].ToString();
                    txtOPERACION.Text = fila["CODIGO_PAR"].ToString();
                }
            );

            FormHelper.RegistrarBusqueda(
                txtNUM_CUENTA,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_CUENTA_BANCARIA",
                    Columnas = new Dictionary<string, string>
                    {
                        { "NUM_CUENTA", "CUENTA" },
                        { "NOMBRE",     "NOMBRE" }
                    }
                },
                fila =>
                {
                    txtNUM_CUENTA.Tag = fila["ID_CTA_BANCO"].ToString();
                    txtNUM_CUENTA.Text = fila["NUM_CUENTA"].ToString();
                    txtNOMBRE.Text = fila["NOMBRE"].ToString();
                    // Incrementar el correlativo en 1 para mostrar el siguiente número
                    int correlativo = 0;
                    if (fila["CORRELATIVO_CHEQUE"] != DBNull.Value)
                        int.TryParse(fila["CORRELATIVO_CHEQUE"].ToString(), out correlativo);
                    txtNUMERO_CHEQUE.Text = (correlativo + 1).ToString();
                    txtMONEDA.Text = "DOLARES";

                    // Al seleccionar cuenta bancaria agregar
                    // su cuenta contable como primera fila del grid
                    string ctaContable = fila["CTACONTABLE"].ToString();
                    if (!string.IsNullOrWhiteSpace(ctaContable))
                        AgregarFilaPartida(ctaContable);
                }
            );

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
            ConfigurarCRUD(EstadoFormulario.Nuevo);
        }

        
        private bool _procesandoLeaveProveedor = false;
        private void txtPROVEEDOR_Leave(object sender, EventArgs e)
        {
            if (_procesandoLeaveProveedor) return;   // ← evita reentrancia
            _procesandoLeaveProveedor = true;
            try
            {
                string codigo = txtPROVEEDOR.Text.Trim();
                string cuentaPorPagar = "";

                if ((codigo == "*") || (string.IsNullOrWhiteSpace(codigo))) return;
                
                // Validar que el usuario haya seleccionado primero la cuenta del banco
                if (_dtPartida.Rows.Count == 0 ||
                    string.IsNullOrWhiteSpace(_dtPartida.Rows[0]["CTACONTABLE"].ToString()))
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Debe seleccionar primero la cuenta bancaria.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        }

        private void ConfigurarCRUD(EstadoFormulario estado)
        {

            switch (estado)
            {
                case EstadoFormulario.Nuevo:
                    txtOPERACION.Enabled = true;
                    txtNUM_CUENTA.Enabled = true;
                    mskFECHA_CHEQUE.Enabled = true;
                    txtPROVEEDOR.Enabled = true;
                    txtNOMBRE.Enabled = true;
                    txtNUMERO_CHEQUE.Enabled = true; 
                    txtCANTIDAD.Enabled = true;
                    txtNOMBRE_CHEQUE.Enabled = true;
                    txtNUMERO_PARTIDA.Enabled = true;
                    txtCONCEPTO.Enabled = true;
                    gridControl1.Enabled = true;

                    btnImprimir.Enabled = false;
                    btnGuardar.Enabled = true;
                    btnCCF_Contado.Enabled = true;
                    btnAgregar.Enabled = false;
                    btnBorrarFila.Enabled = true;
                    btnEliminar.Enabled = false; 
                    break;
                case EstadoFormulario.Guardado:
                    txtOPERACION.Enabled = false;
                    txtNUM_CUENTA.Enabled = false;
                    mskFECHA_CHEQUE.Enabled = false;
                    txtPROVEEDOR.Enabled = false;
                    txtNOMBRE.Enabled = false;
                    txtNUMERO_CHEQUE.Enabled = false;
                    txtCANTIDAD.Enabled = false;
                    txtNOMBRE_CHEQUE.Enabled = false;
                    txtNUMERO_PARTIDA.Enabled = false;
                    txtCONCEPTO.Enabled = false;
                    gridControl1.Enabled = false;  

                    btnImprimir.Enabled = true;
                    btnGuardar.Enabled = false;
                    btnCCF_Contado.Enabled = false;
                    btnAgregar.Enabled = true;
                    btnBorrarFila.Enabled = false;
                    btnEliminar.Enabled = true;
                    break;
                case EstadoFormulario.Impreso:
                    btnImprimir.Enabled = true;
                    btnGuardar.Enabled = false;
                    btnCCF_Contado.Enabled = false;
                    btnAgregar.Enabled = true;
                    btnBorrarFila.Enabled = false;
                    btnEliminar.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// Verifica si hay CCFs al contado huérfanos del usuario actual y pregunta
        /// si los quiere retomar. Si acepta, asigna ese UID y abre directamente
        /// el formulario frmChequeDocumentosContado.
        /// Retorna true si retomó, false en caso contrario.
        /// </summary>
        private bool VerificarCCFsHuerfanos()
        {
            try
            {
                var dt = _dal.EjecutarConsulta("SP_CREDITO_FISCAL_COMPRA", new
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
                    $"Existen {cantidad} documento(s) al contado pendiente(s) de un cheque anterior.\n\n" +
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
                    "Error al verificar CCFs pendientes:\n\n" + ex.Message,
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
                    frm.UidEnlaceCheque = _uidEnlaceCheque;
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
                        // ... lógica ...
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

            /*
            view.CellValueChanged += (s, ev) =>
            {
                if (ev.Column.FieldName == "CARGO" || ev.Column.FieldName == "ABONO")
                    ActualizarCuadre();
            };
           */

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

        /// <summary>
        /// Actualiza la fila 1 (cuenta del banco) con el abono y detalle,
        /// y crea/sobreescribe la fila 2 con la cuenta por pagar del proveedor.
        /// </summary>
        private void AplicarPartidaPago(decimal totalPago, string cuentaPorPagar)
        {
            if (_dtPartida.Rows.Count == 0) return;

            string detalle = $"CH # {txtNUMERO_CHEQUE.Text.Trim()} {txtNOMBRE_CHEQUE.Text.Trim()}";

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

        private void GridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            var view = sender as GridView;
            if (view == null) return;

            string colActual = view.FocusedColumn?.FieldName;

            // Verificar * en CTACONTABLE PRIMERO antes de cualquier otra acción
            if (colActual == "CTACONTABLE")
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

            if (_columnaAnteriorGrid == "CTACONTABLE" &&
                e.FocusedColumn?.FieldName == "DETALLE")
            {
                string cta = view.GetFocusedRowCellValue("CTACONTABLE")?.ToString();
                if (string.IsNullOrWhiteSpace(cta)) return;

                string detalleActual = view.GetFocusedRowCellValue("DETALLE")?.ToString();
                if (!string.IsNullOrWhiteSpace(detalleActual)) return;

                string detalle = $"CH # {txtNUMERO_CHEQUE.Text.Trim()} " +
                                 $"{txtNOMBRE_CHEQUE.Text.Trim()}";

                view.SetFocusedRowCellValue("DETALLE", detalle);
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
                StoredProcedure = "SP_CATALOGO_CUENTA",
                Columnas = new Dictionary<string, string>
        {
            { "CUENTA",        "CUENTA" },
            { "NOMBRE_CUENTA", "NOMBRE" }
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

            if (!FormHelper.ValidarFecha(mskFECHA_CHEQUE, "Fecha del Cheque"))
                return false;

            DateTime? fecha = FormHelper.ObtenerFecha(mskFECHA_CHEQUE);
            if (fecha.HasValue)
            {
                if (fecha > DateTime.Today)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "La fecha no puede ser mayor a la fecha actual.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mskFECHA_CHEQUE.Focus();
                    return false;
                }
                else if (fecha < DateTime.Today.AddDays(-5))
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "La fecha no puede ser menor a 5 días.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mskFECHA_CHEQUE.Focus();
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(txtNOMBRE_CHEQUE.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "El nombre del cheque es requerido.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNOMBRE_CHEQUE.Focus();
                return false;
            }

            // ✅ Validar que la cantidad sea mayor que cero
            decimal cantidad = ObtenerDecimal(txtCANTIDAD);
            if (cantidad <= 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "La cantidad del cheque debe ser mayor que cero.",
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

            return true;
        }

        private decimal CalcularTotalCargo()
        {
            decimal total = 0;
            foreach (DataRow fila in _dtPartida.Rows)
                total += Convert.ToDecimal(fila["CARGO"]);
            return total;
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
                        _dtPartida.Rows[0]["ABONO"] = valor;                        
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
            if (string.IsNullOrWhiteSpace(txtCONCEPTO.Text)) return;
            if (_dtPartida.Rows.Count == 0) return;

            // Asignar el detalle en la primera fila del grid (cuenta del banco)
            string detalle = $"CH # {txtNUMERO_CHEQUE.Text.Trim()} {txtNOMBRE_CHEQUE.Text.Trim()}";
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

        private void btnCCF_Contado_Click(object sender, EventArgs e)
        {
            AbrirContadoConUid();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

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

                // Parámetros del cheque
                var parametros = new
                {
                    ACCION = "GUARDAR",
                    ID_CHEQUE = 0,
                    ID_CTA_BANCO = Convert.ToInt32(txtNUM_CUENTA.Tag ?? 0),
                    NUM_CHEQUE = string.IsNullOrWhiteSpace(txtNUMERO_CHEQUE.Text)
                                          ? (int?)null
                                          : (int?)Convert.ToInt32(txtNUMERO_CHEQUE.Text),
                    FECHA_CHEQUE = FormHelper.ObtenerFecha(mskFECHA_CHEQUE),
                    MONTO = ObtenerDecimal(txtCANTIDAD),
                    NOMBRE_CHEQUE = NullIfEmpty(txtNOMBRE_CHEQUE.Text),
                    NUM_PARTIDA = string.IsNullOrWhiteSpace(txtNUMERO_PARTIDA.Text)
                                          ? (int?)null
                                          : (int?)Convert.ToInt32(txtNUMERO_PARTIDA.Text),
                    CONCEPTO = NullIfEmpty(txtCONCEPTO.Text),
                    ID_ENTIDAD = (int?)null,
                    CODIGO_ENTIDAD = NullIfEmpty(txtPROVEEDOR.Text),
                    UID_ENLACE_CHEQUE = _uidEnlaceCheque,
                    USUARIO = Configuracion.UsuarioActual
                };

                // Llamada al SP con TVP
                int idCheque = _dal.EjecutarConsultaConTVP(
                    "SP_CHEQUE",
                    "PARTIDA",
                    "typeCHEQUE_PARTIDA",
                    _dtPartida,
                    parametros);

                if (idCheque > 0)
                {
                    _idCheque = idCheque;
                    ConfigurarCRUD(EstadoFormulario.Guardado);
                }
                else
                    throw new Exception("El SP no devolvió el ID generado.");

                var args = new XtraMessageBoxArgs
                {
                    Caption = "Guardado",
                    Text = $"Cheque <b>N° {txtNUMERO_CHEQUE.Text.Trim()}</b> guardado correctamente.",
                    Buttons = new[] { DialogResult.OK },
                    Icon = SystemIcons.Information,
                    AllowHtmlText = DefaultBoolean.True
                };
                XtraMessageBox.Show(args);                
               
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
            }
            finally
            {
                Cursor = Cursors.Default;
            }
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
                XtraMessageBox.Show("Debe guardar el cheque antes de imprimir.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                var reporte = new rptCheque { IdCheque = _idCheque };
                reporte.CargarDatos();
                reporte.ImprimirConDialogo();
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            _idCheque = 0;
            FormHelper.LimpiarControles(this);
            _dtPartida.Clear();
            AgregarFilaVacia();
            mskFECHA_CHEQUE.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtOPERACION.Focus();
            ActualizarCuadre();
            ConfigurarCRUD(EstadoFormulario.Nuevo);

            // Abrir automáticamente la búsqueda de Tipo de Operación
            this.BeginInvoke(new Action(() =>
            {
                txtOPERACION.Focus();
                FormHelper.AbrirBusqueda(txtOPERACION);
            }));
        }
    }
}
