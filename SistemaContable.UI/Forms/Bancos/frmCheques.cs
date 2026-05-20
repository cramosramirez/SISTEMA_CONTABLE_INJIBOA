using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using SistemaContable.DAL;
using System.Drawing;

namespace SistemaContable.UI.Forms.Bancos
{
    public partial class frmCheques : DevExpress.XtraEditors.XtraForm
    {
        #region Campos privados

        private readonly DALBase _dal = new DALBase();
        private DataTable _dtPartida;  // DataTable que alimenta el grid
        private int _idCheque = 0;     // 0 = nuevo, >0 = edición
        private string _columnaAnteriorGrid = string.Empty;

        #endregion

        public frmCheques()
        {
            this.SetStyle(
               ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.AllPaintingInWmPaint,
               true);
            this.UpdateStyles();
            InitializeComponent();
        }

        private void frmCheques_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);
            mskFECHA_CHEQUE.Text = DateTime.Today.ToString("dd/MM/yyyy");

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
                    txtNUMERO_CHEQUE.Text = fila["CORRELATIVO_CHEQUE"].ToString();
                    txtMONEDA.Text = "DOLARES";

                    // Al seleccionar cuenta bancaria agregar
                    // su cuenta contable como primera fila del grid
                    string ctaContable = fila["CTACONTABLE"].ToString();
                    if (!string.IsNullOrWhiteSpace(ctaContable))
                        AgregarFilaPartida(ctaContable);
                }
            );
        }

        #region Grid de Partida Contable

        private void InicializarGridPartida()
        {
            // Crear DataTable con las columnas de CHEQUE_PARTIDA
            _dtPartida = new DataTable();
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
        }

        private void ConfigurarColumna(GridView view, string fieldName,
            string caption, int width)
        {
            if (!view.Columns.ColumnByFieldName(fieldName).Equals(null))
            {
                var col = view.Columns[fieldName];
                col.Caption = caption;
                col.Width = width;
                col.Visible = true;
            }
        }

        private void AgregarFilaVacia()
        {
            var fila = _dtPartida.NewRow();
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
            var fila = _dtPartida.NewRow();
            fila["CTACONTABLE"] = ctaContable;
            fila["DETALLE"] = string.Empty;
            fila["CARGO"] = 0m;
            fila["ABONO"] = 0m;
            _dtPartida.Rows.Add(fila);

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
                e.Handled = true;
                view.CloseEditor();
                int filaActual = view.FocusedRowHandle;

                if (filaActual == _dtPartida.Rows.Count - 1)
                    AgregarFilaVacia();

                view.FocusedRowHandle = filaActual + 1;
                view.FocusedColumn = view.Columns["CTACONTABLE"];
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                // 1. Guardar encabezado del CHEQUE
                var dtCheque = _dal.EjecutarConsulta("SP_CHEQUE", new
                {
                    ACCION = "GUARDAR",
                    ID_CHEQUE = _idCheque,
                    ID_CTA_BANCO = Convert.ToInt32(txtNUM_CUENTA.Tag ?? 0),
                    NUM_CHEQUE = Convert.ToInt32(txtNUMERO_CHEQUE.Text),
                    FECHA_CHEQUE = FormHelper.ObtenerFecha(mskFECHA_CHEQUE),
                    MONTO = CalcularTotalCargo(),
                    NOMBRE_CHEQUE = txtNOMBRE_CHEQUE.Text.Trim(),
                    CONCEPTO = txtCONCEPTO.Text.Trim(),
                    ID_ENTIDAD = DBNull.Value,
                    CODIGO_ENTIDAD = string.Empty,
                    USUARIO_CREA = Configuracion.UsuarioActual,
                    USUARIO_ACT = Configuracion.UsuarioActual
                });

                if (dtCheque.Rows.Count == 0) return;
                _idCheque = Convert.ToInt32(dtCheque.Rows[0]["ID_GENERADO"]);

                // 2. Eliminar partidas anteriores si es edición
                _dal.EjecutarSinRetorno("SP_CHEQUE_PARTIDA", new
                {
                    ACCION = "ELIMINAR_POR_CHEQUE",
                    ID_CHEQUE = _idCheque
                });

                // 3. Guardar líneas de la partida contable
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
                }

                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Cheque guardado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    $"Error al guardar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
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

            if (string.IsNullOrWhiteSpace(txtNOMBRE_CHEQUE.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "El nombre del cheque es requerido.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNOMBRE_CHEQUE.Focus();
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _idCheque = 0;
            FormHelper.LimpiarControles(this);
            _dtPartida.Clear();
            AgregarFilaVacia();
            mskFECHA_CHEQUE.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtOPERACION.Focus();
            ActualizarCuadre();
        }

        private void btnSalir_Click(object sender, EventArgs e)
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
                        //gridControl1.RefreshDataSource();
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

            // Asignar el detalle en la primera fila del grid
            string detalle = $"CH # {txtNUMERO_CHEQUE.Text.Trim()} {txtNOMBRE_CHEQUE.Text.Trim()}";
            _dtPartida.Rows[0]["DETALLE"] = detalle;
            gridControl1.RefreshDataSource();

            // Diferir el cambio de foco al grid
            this.BeginInvoke(new Action(() =>
            {
                var view = gridControl1.MainView as GridView;
                if (view == null) return;

                view.FocusedRowHandle = 0;
                view.FocusedColumn = view.Columns["DETALLE"];
                view.ShowEditor();
                gridControl1.Focus();

                if (view.ActiveEditor != null)
                    view.ActiveEditor.SelectAll();
            }));
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {

        }
    }
}