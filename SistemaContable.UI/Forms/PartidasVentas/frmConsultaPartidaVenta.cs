using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using SistemaContable.RP.Partidas;

namespace SistemaContable.UI.Forms.PartidasVentas
{
    public partial class frmConsultaPartidaVenta : DevExpress.XtraEditors.XtraForm
    {
        private readonly DALBase _dal = new DALBase();

        public frmConsultaPartidaVenta()
        {
            InitializeComponent();
            ConfigurarControles();
            CargarCentrosCosto();
            InicializarPeriodo();
            ConfigurarGrid();
        }

        #region Inicialización

        private void ConfigurarControles()
        {
            // La fecha es opcional: permitir limpiarla
            deFecha.Properties.AllowNullInput = DefaultBoolean.True;
            deFecha.Properties.Mask.EditMask = "dd/MM/yyyy";
            deFecha.Properties.Mask.UseMaskAsDisplayFormat = true;
            deFecha.EditValue = null;

            // Solo dígitos en el año
            txt_Anio.Properties.MaxLength = 4;
            txt_Anio.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txt_Anio.Properties.Mask.EditMask = @"\d{0,4}";
        }

        private void InicializarPeriodo()
        {
            txt_Anio.Text = DateTime.Now.Year.ToString();
            cb_Mes.DataSource = FormHelper.ObtenerMeses();
            cb_Mes.DisplayMember = "Mes";
            cb_Mes.ValueMember = "Value";
            cb_Mes.SelectedValue = DateTime.Now.Month.ToString("00");
        }

        private void CargarCentrosCosto()
        {
            DataTable dt = _dal.EjecutarConsulta("[CONTA].CB_TIPOS_PARTIDA_VENTA_CC");
            cbTipoPartida.DataSource = dt;
            cbTipoPartida.ValueMember = "ID_TPPARVENTA";
            cbTipoPartida.DisplayMember = "NOMBRE";
            cbTipoPartida.SelectedIndex = -1;
        }

        private RepositoryItemButtonEdit _btnVerPartida, _btnVerParcial, _btnEditar, _btnEliminar;

        private void ConfigurarGrid()
        {
            // Editable debe quedar en true para que los botones respondan al clic.
            // Las columnas de datos se bloquean con AllowEdit = false (ver FormatearColumnas).
            gridView1.OptionsBehavior.Editable = true;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowAutoFilterRow = false;
            gridView1.OptionsView.ShowFooter = true;
            gridView1.OptionsView.ColumnAutoWidth = false;

            _btnVerPartida = CrearBotonRepo("Ver partida", "Ver partida");
            _btnVerParcial = CrearBotonRepo("Ver parcial", "Ver parcial");
            _btnEditar = CrearBotonRepo("Editar", "Editar partida");
            _btnEliminar = CrearBotonRepo("Eliminar", "Eliminar partida");

            _btnVerPartida.ButtonClick += btnVerPartida_ButtonClick;
            _btnVerParcial.ButtonClick += btnVerParcial_ButtonClick;
            _btnEditar.ButtonClick += btnEditarPartida_ButtonClick;
            _btnEliminar.ButtonClick += btnEliminarPartida_ButtonClick;

            gridControl1.RepositoryItems.AddRange(new RepositoryItem[]
                { _btnVerPartida, _btnVerParcial, _btnEditar, _btnEliminar });
        }

        private RepositoryItemButtonEdit CrearBotonRepo(string texto, string tooltip)
        {
            var repo = new RepositoryItemButtonEdit();
            repo.TextEditStyle = TextEditStyles.HideTextEditor; // solo el botón, sin caja de texto
            repo.Buttons.Clear();
            repo.Buttons.Add(new EditorButton(ButtonPredefines.Glyph)
            {
                Caption = texto,
                ToolTip = tooltip
            });
            return repo;
        }

        #endregion

        #region Consulta

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            CargarPartidas(true);
        }

        private void CargarPartidas(bool avisarSiVacio)
        {
            if (!ValidarCriterios(out int anio, out int mes, out int idTipo, out DateTime? fecha))
                return;

            try
            {
                Cursor = Cursors.WaitCursor;

                DataTable dt = _dal.EjecutarConsulta("[CONTA].SEL_PARTIDA_ORIGEN", new
                {
                    ANIO = anio,
                    MES = mes,
                    ID_TPPARVENTA = idTipo,
                    FECHA_PARTIDA = fecha.HasValue ? (object)fecha.Value.Date : DBNull.Value
                });

                gridControl1.DataSource = dt;
                FormatearColumnas();

                if (avisarSiVacio && dt.Rows.Count == 0)
                    XtraMessageBox.Show("No se encontraron partidas con los criterios indicados.",
                        "Consulta de Partidas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al consultar las partidas:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private bool ValidarCriterios(out int anio, out int mes, out int idTipo, out DateTime? fecha)
        {
            anio = mes = idTipo = 0;
            fecha = null;

            if (!int.TryParse(txt_Anio.Text.Trim(), out anio) || anio < 2000)
            {
                XtraMessageBox.Show("Ingrese un año válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_Anio.Focus();
                return false;
            }

            if (cb_Mes.SelectedValue == null || !int.TryParse(cb_Mes.SelectedValue.ToString(), out mes))
            {
                XtraMessageBox.Show("Seleccione un mes.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cb_Mes.Focus();
                return false;
            }

            if (cbTipoPartida.SelectedIndex < 0 ||
                !int.TryParse(cbTipoPartida.SelectedValue?.ToString(), out idTipo))
            {
                XtraMessageBox.Show("Seleccione un centro de costo.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbTipoPartida.Focus();
                return false;
            }

            // Fecha opcional
            if (deFecha.EditValue != null && deFecha.EditValue != DBNull.Value)
            {
                fecha = deFecha.DateTime.Date;

                if (fecha.Value.Year != anio || fecha.Value.Month != mes)
                {
                    XtraMessageBox.Show("La fecha debe pertenecer al año y mes seleccionados.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    deFecha.Focus();
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Formato del grid

        private void FormatearColumnas()
        {
            gridView1.PopulateColumns();

            Configurar("TIPO_PARTIDA", "Tipo", 70);
            Configurar("ANIO", "Año", 60);
            Configurar("MES", "Mes", 50);
            Configurar("NUM_PARTIDA", "N° Partida", 90);
            Configurar("FECHA_PARTIDA", "Fecha", 90, "dd/MM/yyyy", FormatType.DateTime);
            Configurar("CONCEPTO", "Concepto", 350);
            Configurar("TOTAL_CARGO", "Total Cargo", 110, "n2", FormatType.Numeric, true);
            Configurar("TOTAL_ABONO", "Total Abono", 110, "n2", FormatType.Numeric, true);

            if (gridView1.Columns["NID_PARTIDA"] != null)
                gridView1.Columns["NID_PARTIDA"].Visible = false;

            // Bloquear edición de las columnas de datos
            foreach (GridColumn c in gridView1.Columns)
                c.OptionsColumn.AllowEdit = false;

            // Columnas de botones
            AgregarColumnaBoton("colVerPartida", "Ver partida", _btnVerPartida, 90);
            AgregarColumnaBoton("colVerParcial", "Ver parcial", _btnVerParcial, 90);
            AgregarColumnaBoton("colEditar", "Editar", _btnEditar, 70);
            AgregarColumnaBoton("colEliminar", "Eliminar", _btnEliminar, 75);
        }

        private void AgregarColumnaBoton(string nombre, string caption,
            RepositoryItemButtonEdit repo, int ancho)
        {
            GridColumn col = gridView1.Columns.AddField(nombre);
            col.Caption = caption;
            col.UnboundType = DevExpress.Data.UnboundColumnType.String;
            col.ColumnEdit = repo;
            col.Width = ancho;
            col.Visible = true;
            col.OptionsColumn.AllowEdit = true;          // necesario para que el botón responda
            col.OptionsColumn.AllowFocus = true;
            col.OptionsColumn.AllowSort = DefaultBoolean.False;
            col.OptionsFilter.AllowFilter = false;
            col.OptionsColumn.AllowMove = false;
            col.OptionsColumn.ShowInCustomizationForm = false;
            col.OptionsColumn.FixedWidth = true;
        }

        private void Configurar(string campo, string caption, int ancho,
            string formato = null, FormatType tipo = FormatType.None, bool sumar = false)
        {
            GridColumn col = gridView1.Columns[campo];
            if (col == null) return;

            col.Caption = caption;
            col.Width = ancho;

            if (formato != null)
            {
                col.DisplayFormat.FormatType = tipo;
                col.DisplayFormat.FormatString = formato;
            }

            if (sumar)
            {
                col.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                col.SummaryItem.DisplayFormat = "{0:n2}";
                col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            }
        }

        #endregion

        #region Otros botones

        #region Acciones por fila

        private object ObtenerIdPartidaSeleccionada()
        {
            return gridView1.GetFocusedRowCellValue("NID_PARTIDA");
        }

        private void btnVerPartida_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            object id = ObtenerIdPartidaSeleccionada();
            if (id == null) return;

            // TODO: abrir el formulario de detalle de la partida
            // using (var frm = new frmDetallePartida(Convert.ToInt64(id))) frm.ShowDialog(this);
            try
            {
                Cursor = Cursors.WaitCursor;
                var reporte = new RptPartida_Movimiento { _NID_PARTIDA = (string)id, _Titulo = "DETALLE DE PARTIDA CONTABLE" };
                reporte.MostrarPreview();
            }
            catch (Exception ex)
            {
                Alertas.Error(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            // XtraMessageBox.Show("Ver partida: " + id, "Pendiente");
        }

        private void btnVerParcial_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            object id = ObtenerIdPartidaSeleccionada();
            if (id == null) return;

            // TODO: abrir el formulario de parciales
            XtraMessageBox.Show("Ver parcial: " + id, "Pendiente");
        }

        private void btnEditarPartida_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            object id = ObtenerIdPartidaSeleccionada();
            if (id == null) return;

            // TODO: abrir el formulario en modo edición
            // using (var frm = new frmPartidaVenta(Convert.ToInt64(id)))
            //     if (frm.ShowDialog(this) == DialogResult.OK) CargarPartidas(false);
           
           XtraMessageBox.Show("Editar partida: " + id, "Pendiente");
        }

        private void btnEliminarPartida_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            object id = ObtenerIdPartidaSeleccionada();
            if (id == null) return;

            string numPartida = Convert.ToString(gridView1.GetFocusedRowCellValue("NUM_PARTIDA"));

            if (XtraMessageBox.Show($"¿Está seguro de eliminar la partida N° {numPartida}?\nEsta acción no se puede deshacer.",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            try
            {
                Cursor = Cursors.WaitCursor;

                // TODO: ajustar al SP real de eliminación
                _dal.EjecutarConsulta("[CONTA].DEL_PARTIDA",
                    new SqlParameter("@NID_PARTIDA", id));

                CargarPartidas(false); // refrescar el grid
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al eliminar la partida:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPeriodo();
            cbTipoPartida.SelectedIndex = -1;
            deFecha.EditValue = null;
            gridControl1.DataSource = null;
            cbTipoPartida.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        // Handler enlazado en el Designer; se deja vacío para no romper la compilación
        private void cbTipoPartida_TextChanged(object sender, EventArgs e) { }
    }
}