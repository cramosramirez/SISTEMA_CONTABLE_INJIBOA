using System;
using System.Collections.Generic;
using SistemaContable.UI.Helpers;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using SistemaContable.DAL;
using System.Drawing;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace SistemaContable.UI.Forms.Proveedores
{
    public partial class frmDocumentoCompraProvision : Form
    {
        public int IdCcfCompra { get; set; } = 0;

        private readonly DALBase _dal = new DALBase();
        private readonly string _esquemaProcedimientos;
        private DataTable _dtPartida;  // DataTable que alimenta el grid
        private string _columnaAnteriorGrid = string.Empty;

        public frmDocumentoCompraProvision()
            : this(null)
        {
        }

        public frmDocumentoCompraProvision(string esquemaProcedimientos)
        {
            _esquemaProcedimientos = string.IsNullOrWhiteSpace(esquemaProcedimientos)
                ? null
                : esquemaProcedimientos.Trim('[', ']');
            InitializeComponent();
        }

        private string ObtenerProcedimiento(string nombre)
        {
            return string.IsNullOrEmpty(_esquemaProcedimientos)
                ? nombre
                : $"[{_esquemaProcedimientos}].[{nombre}]";
        }

        private void frmProvisionQuedan_Load(object sender, EventArgs e)
        {
            InicializarGridPartida();
            CargarProvision();
        }

        private void InicializarGridPartida()
        {
            // Crear DataTable con las columnas de CHEQUE_PARTIDA
            _dtPartida = new DataTable();
            _dtPartida.Columns.Add("ORDEN", typeof(int));
            _dtPartida.Columns.Add("CTACONTABLE", typeof(string));
            _dtPartida.Columns.Add("DETALLE", typeof(string));
            _dtPartida.Columns.Add("CARGO", typeof(decimal));
            _dtPartida.Columns.Add("ABONO", typeof(decimal));

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
            ConfigurarColumna(view, "CTACONTABLE", "CUENTA", 100);
            ConfigurarColumna(view, "DETALLE", "DETALLE DE LA APLICACION", 400);
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

            view.CellValueChanged += (s, ev) =>
            {
                if (ev.Column.FieldName == "CARGO" || ev.Column.FieldName == "ABONO")
                    ActualizarCuadre();
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
            ActualizarCuadre();

            this.BeginInvoke(new Action(() =>
            {
                // Esperar a que el form esté visible
                if (!this.IsHandleCreated) return;
                EnfocarUltimaCelda();
            }));
        }

        private void EnfocarUltimaCelda()
        {
            var view = gridControl1.MainView as GridView;
            if (view == null || view.RowCount == 0) return;

            gridControl1.Focus();           // foco al control padre primero
            view.FocusedRowHandle = view.RowCount - 1;      // ultima fila

            var col = view.Columns.ColumnByFieldName("CTACONTABLE");
            if (col != null)
                view.FocusedColumn = col;

            // Diferir ShowEditor() para asegurar que el grid ya está pintado
            this.BeginInvoke(new Action(() =>
            {
                view.ShowEditor();
                if (view.ActiveEditor != null)
                    view.ActiveEditor.SelectAll();
            }));
        }

        private void AgregarFilaVacia()
        {
            var fila = _dtPartida.NewRow();
            fila["ORDEN"] = _dtPartida.Rows.Count + 1; 
            fila["CTACONTABLE"] = string.Empty;
            fila["DETALLE"] = string.Empty;
            fila["CARGO"] = 0m;
            fila["ABONO"] = 0m;
            _dtPartida.Rows.Add(fila);
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

            // Si está en CARGO presiona Enter → saltar directo a CTACONTABLE de la siguiente fila
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


        private void MostrarEstadoCuenta(string codigo)
        {
            var (texto, esValida) = CuentaContableHint.Obtener(codigo);

            if (string.IsNullOrWhiteSpace(texto))
            {
                OcultarEstadoCuenta();
                return;
            }
            lblESTADO_CUENTA.Text = texto;
            lblESTADO_CUENTA.ForeColor = esValida ? Color.Black : Color.DarkRed;
            lblESTADO_CUENTA.Font = new Font(
                lblESTADO_CUENTA.Font,
                esValida ? FontStyle.Regular : FontStyle.Bold);
            pnESTADO_CUENTA.Visible = true;
        }

        private void OcultarEstadoCuenta()
        {
            pnESTADO_CUENTA.Visible = false;
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
                OcultarEstadoCuenta();
            }

            if (_columnaAnteriorGrid == "CTACONTABLE" &&
                e.FocusedColumn?.FieldName == "DETALLE")
            {
                string cta = view.GetFocusedRowCellValue("CTACONTABLE")?.ToString();
                if (string.IsNullOrWhiteSpace(cta)) return;

                string detalleActual = view.GetFocusedRowCellValue("DETALLE")?.ToString();
                if (!string.IsNullOrWhiteSpace(detalleActual)) return;

                string detalle = "";

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
                StoredProcedure = ObtenerProcedimiento("SP_CATALOGO_CUENTA"),
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

        private void CargarProvision()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                DataTable dt = _dal.EjecutarConsulta(ObtenerProcedimiento("SP_CREDITO_FISCAL_PROVISION"),
                    new
                    {
                        ACCION = "CARGAR_PROVISION",
                        ID_CCF_COMPRA = IdCcfCompra
                    });

                _dtPartida.Rows.Clear();

                foreach (DataRow r in dt.Rows)
                {
                    DataRow fila = _dtPartida.NewRow();
                    fila["ORDEN"] = r["ORDEN"] == DBNull.Value ? 0 : Convert.ToInt32(r["ORDEN"]);
                    fila["CTACONTABLE"] = r["CTACONTABLE"] == DBNull.Value ? "" : r["CTACONTABLE"];
                    fila["DETALLE"] = r["DETALLE"] == DBNull.Value ? "" : r["DETALLE"];
                    fila["CARGO"] = r["CARGO"] == DBNull.Value ? 0m : Convert.ToDecimal(r["CARGO"]);
                    fila["ABONO"] = r["ABONO"] == DBNull.Value ? 0m : Convert.ToDecimal(r["ABONO"]);
                    _dtPartida.Rows.Add(fila);
                }                
                AgregarFilaVacia();
                ActualizarCuadre();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "Error al cargar la provisión:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private bool Validar()
        {
            var view = gridControl1.MainView as GridView;
            if (view == null) return false;

            // Cerrar el editor si está abierto para que los valores estén persistidos en el DataTable
            view.CloseEditor();
            view.UpdateCurrentRow();

            decimal totalCargo = 0, totalAbono = 0;
            int filasValidas = 0;
            int numeroFila = 0;

            foreach (DataRow fila in _dtPartida.Rows)
            {
                numeroFila++;
                int rowHandle = numeroFila - 1;   // índice 0-based del grid

                string cta = fila["CTACONTABLE"]?.ToString()?.Trim() ?? string.Empty;
                string detalle = fila["DETALLE"]?.ToString()?.Trim() ?? string.Empty;
                decimal cargo = fila["CARGO"] == DBNull.Value ? 0 : Convert.ToDecimal(fila["CARGO"]);
                decimal abono = fila["ABONO"] == DBNull.Value ? 0 : Convert.ToDecimal(fila["ABONO"]);

                bool tieneCta = !string.IsNullOrWhiteSpace(cta);
                bool tieneDetalle = !string.IsNullOrWhiteSpace(detalle);
                bool tieneMonto = (cargo != 0 || abono != 0);

                // Fila completamente vacía -> se ignora
                if (!tieneCta && !tieneDetalle && !tieneMonto) continue;

                // ============================================================
                // Cuenta sin detalle
                // ============================================================
                if (tieneCta && !tieneDetalle)
                {                    
                    XtraMessageBox.Show(
                        $"Fila {numeroFila}: tiene cuenta contable pero falta el detalle.",
                        "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    EnfocarCelda(view, rowHandle, "DETALLE");
                    return false;
                }

                // ============================================================
                // Detalle sin cuenta
                // ============================================================
                if (!tieneCta && tieneDetalle)
                {                    
                    XtraMessageBox.Show(
                        $"Fila {numeroFila}: tiene detalle pero falta la cuenta contable.",
                        "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    EnfocarCelda(view, rowHandle, "CTACONTABLE");
                    return false;
                }

                // ============================================================
                // Cuenta + detalle pero sin monto
                // ============================================================
                if (tieneCta && tieneDetalle && !tieneMonto)
                {
                    EnfocarCelda(view, rowHandle, "CARGO");
                    XtraMessageBox.Show(
                        $"Fila {numeroFila}: debe ingresar un valor en CARGO o ABONO.",
                        "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // ============================================================
                // Cargo y abono ambos con valor
                // ============================================================
                if (cargo != 0 && abono != 0)
                {
                    EnfocarCelda(view, rowHandle, "ABONO");
                    XtraMessageBox.Show(
                        $"Fila {numeroFila}: no puede tener valor en CARGO y ABONO al mismo tiempo.",
                        "Datos incoherentes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Fila válida -> acumular
                totalCargo += cargo;
                totalAbono += abono;
                filasValidas++;
            }

            // ============================================================
            // Partida vacía
            // ============================================================
            if (filasValidas == 0)
            {
                // Enfoca la primera celda para que el usuario empiece a capturar
                EnfocarCelda(view, 0, "CTACONTABLE");
                XtraMessageBox.Show("La partida no puede estar vacía.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // ============================================================
            // Cuadre
            // ============================================================
            if (totalCargo != totalAbono)
            {
                XtraMessageBox.Show(
                    $"No se puede guardar la partida porque está descuadrada.\n\n" +
                    $"Cargo:      {totalCargo:N2}\n" +
                    $"Abono:      {totalAbono:N2}\n" +
                    $"Diferencia: {Math.Abs(totalCargo - totalAbono):N2}",
                    "Partida descuadrada",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Lleva el foco a una celda específica del grid y abre el editor.
        /// </summary>
        private void EnfocarCelda(GridView view, int rowHandle, string fieldName)
        {
            if (view == null) return;

            // Validar que la fila exista
            if (rowHandle < 0 || rowHandle >= view.RowCount) return;

            view.FocusedRowHandle = rowHandle;

            var col = view.Columns.ColumnByFieldName(fieldName);
            if (col != null)
                view.FocusedColumn = col;

            // Diferir la apertura del editor para después de cerrar el MessageBox
            this.BeginInvoke(new Action(() =>
            {
                view.ShowEditor();
                if (view.ActiveEditor != null)
                    view.ActiveEditor.SelectAll();
            }));
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarFilasVaciasGrid();
                if (!Validar())
                {
                    return;
                }
                Cursor = Cursors.WaitCursor;
                int filas = _dal.EjecutarConsultaConTVP(
                     ObtenerProcedimiento("SP_CREDITO_FISCAL_PROVISION"),
                     "PARTIDA_PROVISION",              // nombre del parámetro
                     "typeCREDITO_FISCAL_PROVISION",   // nombre del TYPE
                     _dtPartida,
                 new
                 {
                     ACCION = "GUARDAR",
                     ID_CCF_COMPRA = IdCcfCompra,
                     USUARIO = Configuracion.UsuarioActual
                 });
                if (filas > 0)
                {
                    XtraMessageBox.Show(
                        "Provisión guardada con éxito",
                        "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show(
                        "No se logro guardar la provisión",
                        "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException sqlEx)
            {                
                XtraMessageBox.Show(sqlEx.Message,
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "Error al guardar la provisión:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
