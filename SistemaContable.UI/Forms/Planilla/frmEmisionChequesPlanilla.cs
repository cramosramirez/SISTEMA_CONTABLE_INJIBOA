using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SistemaContable.DAL;
using SistemaContable.RP.Bancos.Proveedores;
using SistemaContable.RP.Planilla;
using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Planilla
{
    public partial class frmEmisionChequesPlanilla : Form
    {
        private const string PROCEDIMIENTO = "[dbo].[SP_EMISION_CHEQUES_PLANILLA]";
        private const string PROCEDIMIENTO_BORRAR_PRUEBAS = "[dbo].[SP_ELIMINAR_CHEQUES_PLANILLA_PRUEBA]";
        private const string PROCEDIMIENTO_CALCULAR_RANGO = "[dbo].[SP_CALCULAR_RANGO_CHEQUES_PLANILLA]";
        private const string TIPO_PARTIDA_CHEQUE = "CH";
        private const string MENSAJE_AYUDA_CUENTA =
            "Sugerencia: en Cuenta bancaria escriba * y presione Enter para seleccionar la cuenta.";
        private readonly DALBase _dal = new DALBase();
        private int _idCuenta;
        private int _versionCalculoRango;
        private DataTable _chequesGenerados;

        public frmEmisionChequesPlanilla()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CentrarContenido();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            CentrarContenido();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && txtCuenta.Focused && txtCuenta.Text.Trim() == "*")
            {
                FormHelper.AbrirBusqueda(txtCuenta);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CentrarContenido()
        {
            if (panelEncabezado == null) return;

            int anchoTotal = Math.Min(960, ClientSize.Width - 24);
            int izquierda = Math.Max(12, (ClientSize.Width - anchoTotal) / 2);
            int arriba = 12;

            panelEncabezado.Left = izquierda;
            panelEncabezado.Top = arriba;
            panelEncabezado.Width = anchoTotal;
            grpDatos.Left = izquierda;
            grpDatos.Top = arriba + 80;
            lblAyuda.Left = izquierda;
            lblAyuda.Top = arriba + 360;
            lblAyuda.Width = anchoTotal;
            panelBotones.Left = izquierda + grpDatos.Width + 12;
            panelBotones.Top = arriba + 80;
            panelBotones.Width = Math.Max(190, anchoTotal - grpDatos.Width - 12);
            panelBotones.Height = grpDatos.Height;

            int anchoBoton = panelBotones.Width - 28;
            btnEmitir.Width = anchoBoton;
            btnVerReporte.Width = anchoBoton;
            btnEliminarPruebas.Width = anchoBoton;
            btnSalir.Width = anchoBoton;
            lblAcciones.Width = anchoBoton;

            panelProgreso.Left = izquierda;
            panelProgreso.Top = arriba + 408;
            panelProgreso.Width = anchoTotal;
            progressGeneracion.Width = Math.Max(200, panelProgreso.Width - progressGeneracion.Left);
            AjustarAreaResultados();
        }

        private void AjustarAreaResultados()
        {
            int parteSuperior = panelProgreso.Visible
                ? panelProgreso.Bottom + 10
                : lblAyuda.Bottom + 10;

            grpResultados.Left = 12;
            grpResultados.Top = parteSuperior;
            grpResultados.Width = Math.Max(720, ClientSize.Width - 24);
            grpResultados.Height = Math.Max(150, ClientSize.Height - grpResultados.Top - 12);
        }

        private void frmEmisionChequesPlanilla_Load(object sender, EventArgs e)
        {
            CargarZafra();
            CargarTipoPlanilla();
            CargarContribuyente();
            ConfigurarBusquedaCuenta();
            AsignarNumeroPartidaSugerido();
            AjustarIcono(btnEmitir, 28);
            AjustarIcono(btnVerReporte, 28);
            AjustarIcono(btnEliminarPruebas, 28);
            AjustarIcono(btnSalir, 28);
            ConfigurarGridResultados();
            MostrarProgreso(false);
        }

        private void AsignarNumeroPartidaSugerido()
        {
            try
            {
                DateTime fecha = DateTime.Today;
                var infoPartida = NumeradorPartidaHelper.Consultar(
                    TIPO_PARTIDA_CHEQUE,
                    fecha.Year,
                    fecha.Month);

                txtNumPartida.Text = infoPartida == null
                    ? string.Empty
                    : infoPartida.NumSiguienteFormateado;
            }
            catch (Exception ex)
            {
                txtNumPartida.Text = string.Empty;
                XtraMessageBox.Show(
                    "Error al consultar el número de partida:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarZafra()
        {
            DataTable dt = _dal.EjecutarConsulta("[EGENERALES].[SP_ZAFRA]", new { ACCION = "OBTENER_CB" });
            cbxZafra.DataSource = dt;
            cbxZafra.ValueMember = "ID_ZAFRA";
            cbxZafra.DisplayMember = "NOMBRE_ZAFRA";
        }

        private void CargarCatorcena(int idZafra)
        {
            DataTable dt = _dal.EjecutarConsulta("[EGENERALES].[SP_CATORCENA]",
                new { ACCION = "OBTENER", ID_ZAFRA = idZafra });
            cbxCatorcena.DataSource = dt;
            cbxCatorcena.ValueMember = "ID_CATORCENA";
            cbxCatorcena.DisplayMember = "CATORCENA";
        }

        private void CargarTipoPlanilla()
        {
            DataTable dt = _dal.EjecutarConsulta("[EGENERALES].[SP_TIPO_PLANILLA]", new { ACCION = "OBTENER" });
            cbxTipoPlanilla.DataSource = dt;
            cbxTipoPlanilla.ValueMember = "ID_TIPO_PLANILLA";
            cbxTipoPlanilla.DisplayMember = "NOMBRE_TIPO_PLANILLA";
        }

        private void CargarContribuyente()
        {
            var dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("NOMBRE", typeof(string));
            dt.Rows.Add(1, "Contribuyente");
            dt.Rows.Add(0, "No contribuyente");
            cbxEsContribuyente.DataSource = dt;
            cbxEsContribuyente.ValueMember = "ID";
            cbxEsContribuyente.DisplayMember = "NOMBRE";
        }

        private void ConfigurarBusquedaCuenta()
        {
            FormHelper.RegistrarBusqueda(
                txtCuenta,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_CUENTA_BANCARIA",
                    ParametrosExtra = new { ACTIVA = true },
                    Columnas = new Dictionary<string, string>
                    {
                        { "NUM_CUENTA", "CUENTA" },
                        { "NOMBRE", "NOMBRE" }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "NUM_CUENTA", 130 },
                        { "NOMBRE", 380 }
                    },
                    AnchoFormulario = 560
                },
                fila =>
                {
                    _idCuenta = Convert.ToInt32(fila["ID_CTA_BANCO"]);
                    txtCuenta.Tag = _idCuenta;
                    txtCuenta.Text = Convert.ToString(fila["NUM_CUENTA"]);
                    txtNombreCuenta.Text = Convert.ToString(fila["NOMBRE"]);
                    if (fila.Table.Columns.Contains("CORRELATIVO_CHEQUE") && fila["CORRELATIVO_CHEQUE"] != DBNull.Value)
                    {
                        int correlativo;
                        if (int.TryParse(Convert.ToString(fila["CORRELATIVO_CHEQUE"]), out correlativo))
                            txtPrimerCheque.Text = (correlativo + 1).ToString();
                    }
                    txtPrimerCheque.Focus();
                });
        }

        private void cbxZafra_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarRangoCalculado();
            int? id = ValorCombo(cbxZafra);
            if (id.HasValue)
                CargarCatorcena(id.Value);
        }

        private async void FiltroRango_SelectedIndexChanged(object sender, EventArgs e)
        {
            await RecalcularRangoAsync();
        }

        private void LimpiarRangoCalculado()
        {
            _versionCalculoRango++;
            txtUltimoCheque.Text = string.Empty;
            lblCantidadCheques.Text = "Cheques a emitir: —";
            lblAyuda.Text = MENSAJE_AYUDA_CUENTA;
        }

        private async void txtPrimerCheque_Leave(object sender, EventArgs e)
        {
            await RecalcularRangoAsync();
        }

        private async Task RecalcularRangoAsync()
        {
            LimpiarRangoCalculado();
            int versionActual = _versionCalculoRango;
            int primerCheque;
            int? idZafra = ValorCombo(cbxZafra);
            int? idCatorcena = ValorCombo(cbxCatorcena);
            int? idTipoPlanilla = ValorCombo(cbxTipoPlanilla);
            int? esContribuyente = ValorCombo(cbxEsContribuyente);

            if (!int.TryParse(txtPrimerCheque.Text.Trim(), out primerCheque) || primerCheque <= 0 ||
                !idZafra.HasValue || !idCatorcena.HasValue ||
                !idTipoPlanilla.HasValue || !esContribuyente.HasValue)
            {
                return;
            }

            try
            {
                DataTable resultado = await Task.Run(() => _dal.EjecutarConsulta(
                    PROCEDIMIENTO_CALCULAR_RANGO,
                    new
                    {
                        ID_ZAFRA = idZafra.Value,
                        ID_CATORCENA = idCatorcena.Value,
                        ID_TIPO_PLANILLA = idTipoPlanilla.Value,
                        ES_CONTRIBUYENTE = esContribuyente.Value,
                        PRIMER_CHEQUE = primerCheque
                    }));

                if (versionActual != _versionCalculoRango)
                    return;

                if (resultado == null || resultado.Rows.Count == 0 ||
                    resultado.Rows[0]["ULTIMO_CHEQUE"] == DBNull.Value)
                {
                    LimpiarRangoCalculado();
                    XtraMessageBox.Show("No se encontraron beneficiarios para los filtros seleccionados.",
                        "Calcular cheques", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int cantidad = Convert.ToInt32(resultado.Rows[0]["CANTIDAD_CHEQUES"]);
                txtUltimoCheque.Text = Convert.ToString(resultado.Rows[0]["ULTIMO_CHEQUE"]);
                lblCantidadCheques.Text = "Cheques a emitir: " + cantidad;
                lblAyuda.Text = string.Format(
                    "Se emitirán {0} cheque(s). Rango calculado: {1} al {2}.",
                    cantidad, primerCheque, txtUltimoCheque.Text);
            }
            catch (Exception ex)
            {
                if (versionActual != _versionCalculoRango)
                    return;

                LimpiarRangoCalculado();
                XtraMessageBox.Show("No se pudo calcular el rango de cheques:\n" + ex.Message,
                    "Calcular cheques", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static int? ValorCombo(System.Windows.Forms.ComboBox combo)
        {
            object valor = combo.SelectedValue;
            if (valor == null || valor == DBNull.Value || valor is DataRowView)
                return null;
            int numero;
            return int.TryParse(valor.ToString(), out numero) ? numero : (int?)null;
        }

        private bool ValidarDatos(out int primerCheque, out int ultimoCheque, out int numPartida)
        {
            primerCheque = ultimoCheque = numPartida = 0;
            if (!ValorCombo(cbxZafra).HasValue || !ValorCombo(cbxCatorcena).HasValue ||
                !ValorCombo(cbxTipoPlanilla).HasValue || !ValorCombo(cbxEsContribuyente).HasValue)
                return MostrarValidacion("Debe seleccionar Zafra, Catorcena, Tipo de planilla y Tipo de contribuyente.", cbxZafra);

            if (_idCuenta <= 0)
                return MostrarValidacion("Debe seleccionar una cuenta bancaria. Escriba * y presione Enter para buscar.", txtCuenta);

            if (!int.TryParse(txtPrimerCheque.Text.Trim(), out primerCheque) || primerCheque <= 0)
                return MostrarValidacion("El primer cheque debe ser un número entero mayor que cero.", txtPrimerCheque);

            if (!int.TryParse(txtUltimoCheque.Text.Trim(), out ultimoCheque) || ultimoCheque <= 0)
                return MostrarValidacion("El último cheque debe ser un número entero mayor que cero.", txtUltimoCheque);

            if (ultimoCheque < primerCheque)
                return MostrarValidacion("El último cheque no puede ser menor que el primer cheque.", txtUltimoCheque);

            if (!int.TryParse(txtNumPartida.Text.Trim(), out numPartida) || numPartida <= 0)
                return MostrarValidacion("El número de partida debe ser un entero mayor que cero.", txtNumPartida);

            return true;
        }

        private static bool MostrarValidacion(string mensaje, Control control)
        {
            XtraMessageBox.Show(mensaje, "Emisión de cheques de planilla", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            control.Focus();
            return false;
        }

        private async void btnEmitir_Click(object sender, EventArgs e)
        {
            int primerCheque, ultimoCheque, numPartida;
            if (!ValidarDatos(out primerCheque, out ultimoCheque, out numPartida))
                return;

            int cantidad = ultimoCheque - primerCheque + 1;
            DialogResult confirmar = XtraMessageBox.Show(
                string.Format("Se emitirán cheques usando el rango {0} al {1} ({2} número(s)).\n\n¿Desea continuar?",
                    primerCheque, ultimoCheque, cantidad),
                "Confirmar emisión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar != DialogResult.Yes)
                return;

            try
            {
                CambiarEstadoGeneracion(true);
                Cursor = Cursors.WaitCursor;
                lblEstadoProceso.Text = "Generando el lote de cheques, espere...";
                MostrarProgreso(true);

                var parametros = new
                {
                    ID_ZAFRA = ValorCombo(cbxZafra).Value,
                    ID_CATORCENA = ValorCombo(cbxCatorcena).Value,
                    ID_TIPO_PLANILLA = ValorCombo(cbxTipoPlanilla).Value,
                    ES_CONTRIBUYENTE = ValorCombo(cbxEsContribuyente).Value,
                    ID_CUENTA = _idCuenta,
                    PRIMER_CHEQUE = primerCheque,
                    ULTIMO_CHEQUE = ultimoCheque,
                    NUM_PARTIDA = numPartida,
                    USUARIO_CREA = Configuracion.UsuarioActual,
                    FECHA_CREA = DateTime.Now,
                    ID_RANGO_COMPE = -1
                };

                DataTable resultado = await Task.Run(() => _dal.EjecutarConsulta(PROCEDIMIENTO, parametros));
                ValidarResultadoEmision(resultado);
                _chequesGenerados = await Task.Run(() => ObtenerChequesGenerados(resultado, primerCheque, ultimoCheque));
                MostrarChequesGenerados(_chequesGenerados);

                int generados = _chequesGenerados == null ? 0 : _chequesGenerados.Rows.Count;
                string detalle = "\n\nCheques encontrados en el rango: " + generados + ".";
                XtraMessageBox.Show("La emisión de cheques finalizó correctamente." + detalle,
                    "Emisión completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                int chequesDeLaGeneracion = 0;
                try
                {
                    DataTable chequesExistentes = await Task.Run(() =>
                        ObtenerChequesGenerados(null, primerCheque, ultimoCheque));
                    _chequesGenerados = chequesExistentes;
                    chequesDeLaGeneracion = chequesExistentes.Rows.Count;
                    MostrarChequesGenerados(_chequesGenerados);
                }
                catch
                {
                    // Se conserva el error original de la emisión.
                }

                string detalleParcial = chequesDeLaGeneracion > 0
                    ? string.Format(
                        "\n\nSe muestran en el grid los {0} cheque(s) existentes de esta generación.",
                        chequesDeLaGeneracion)
                    : "\n\nNo se encontraron cheques existentes de esta generación para mostrar.";

                XtraMessageBox.Show("No se pudo completar la emisión:\n" + ex.Message + detalleParcial,
                    "Emisión de cheques de planilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MostrarProgreso(false);
                Cursor = Cursors.Default;
                CambiarEstadoGeneracion(false);
            }
        }

        private static void ValidarResultadoEmision(DataTable resultado)
        {
            if (resultado == null || resultado.Rows.Count == 0 ||
                !resultado.Columns.Contains("CODIGO"))
                return;

            int codigo;
            if (!int.TryParse(Convert.ToString(resultado.Rows[0]["CODIGO"]), out codigo) || codigo >= 0)
                return;

            string mensaje = resultado.Columns.Contains("RESULTADO")
                ? Convert.ToString(resultado.Rows[0]["RESULTADO"])
                : "El procedimiento no pudo completar la emisión.";

            if (string.IsNullOrWhiteSpace(mensaje))
                mensaje = "El procedimiento no pudo completar la emisión.";

            throw new InvalidOperationException(
                mensaje + "\n\nSeleccione un rango de cheques no utilizado o borre primero las pruebas correspondientes.");
        }

        private DataTable ObtenerChequesGenerados(DataTable resultado, int primerCheque, int ultimoCheque)
        {
            if (resultado != null && resultado.Columns.Contains("ID_CHEQUE"))
                return resultado;

            DataTable disponibles = _dal.EjecutarConsulta("SP_CHEQUE", new
            {
                ACCION = "BUSCAR",
                FILTRO = (string)null,
                ID_CTA_BANCO = _idCuenta,
                TIPO_PARTIDA = "CH"
            });

            DataTable filtrados = disponibles.Clone();
            if (!disponibles.Columns.Contains("NUM_CHEQUE"))
                return filtrados;

            foreach (DataRow fila in disponibles.Rows)
            {
                int numero;
                if (int.TryParse(Convert.ToString(fila["NUM_CHEQUE"]), out numero) &&
                    numero >= primerCheque && numero <= ultimoCheque)
                    filtrados.ImportRow(fila);
            }

            return filtrados;
        }

        private void ConfigurarGridResultados()
        {
            gvCheques.OptionsBehavior.Editable = false;
            gvCheques.OptionsBehavior.ReadOnly = true;
            gvCheques.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvCheques.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvCheques.OptionsSelection.MultiSelect = false;
            gvCheques.OptionsView.ShowGroupPanel = false;
            gvCheques.OptionsView.ShowIndicator = false;
            gvCheques.OptionsView.ShowFooter = true;
            gvCheques.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.True;
            gvCheques.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gvCheques.OptionsView.EnableAppearanceEvenRow = true;
            gvCheques.OptionsView.ColumnAutoWidth = true;
            gvCheques.OptionsFind.AlwaysVisible = true;
            gvCheques.OptionsFind.FindNullPrompt = "Buscar cheque...";
            gvCheques.OptionsFind.ShowClearButton = true;
            gvCheques.OptionsFind.ShowFindButton = false;
            gvCheques.RowHeight = 28;
            gvCheques.ColumnPanelRowHeight = 32;

            gvCheques.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            gvCheques.Appearance.HeaderPanel.BackColor = Color.FromArgb(226, 236, 246);
            gvCheques.Appearance.HeaderPanel.BackColor2 = Color.FromArgb(226, 236, 246);
            gvCheques.Appearance.HeaderPanel.ForeColor = Color.FromArgb(31, 78, 121);
            gvCheques.Appearance.HeaderPanel.Options.UseFont = true;
            gvCheques.Appearance.HeaderPanel.Options.UseBackColor = true;
            gvCheques.Appearance.HeaderPanel.Options.UseForeColor = true;
            gvCheques.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gvCheques.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gvCheques.Appearance.Row.Font = new Font("Segoe UI", 9F);
            gvCheques.Appearance.Row.BackColor = Color.White;
            gvCheques.Appearance.Row.Options.UseFont = true;
            gvCheques.Appearance.Row.Options.UseBackColor = true;
            gvCheques.Appearance.EvenRow.BackColor = Color.FromArgb(242, 247, 252);
            gvCheques.Appearance.EvenRow.Options.UseBackColor = true;
            gvCheques.Appearance.FocusedRow.BackColor = Color.FromArgb(209, 229, 247);
            gvCheques.Appearance.FocusedRow.ForeColor = Color.FromArgb(25, 45, 65);
            gvCheques.Appearance.FocusedRow.Options.UseBackColor = true;
            gvCheques.Appearance.FocusedRow.Options.UseForeColor = true;
            gvCheques.Appearance.FooterPanel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            gvCheques.Appearance.FooterPanel.BackColor = Color.FromArgb(231, 239, 247);
            gvCheques.Appearance.FooterPanel.ForeColor = Color.FromArgb(31, 78, 121);
            gvCheques.Appearance.FooterPanel.Options.UseFont = true;
            gvCheques.Appearance.FooterPanel.Options.UseBackColor = true;
            gvCheques.Appearance.FooterPanel.Options.UseForeColor = true;
            gvCheques.RowCellClick += gvCheques_RowCellClick;
            gridCheques.SizeChanged += gridCheques_SizeChanged;
        }

        private void MostrarChequesGenerados(DataTable cheques)
        {
            gridCheques.DataSource = cheques;
            gvCheques.PopulateColumns();
            AjustarColumnasResultados();
            gridCheques.RefreshDataSource();
            int cantidad = cheques == null ? 0 : cheques.Rows.Count;
            grpResultados.Text = "  Cheques generados — " + cantidad + " registro(s)  ";
        }

        private void AjustarColumnasResultados()
        {
            foreach (GridColumn columna in gvCheques.Columns)
                columna.Visible = false;

            MostrarColumna("NUM_CHEQUE", "Cheque (ver)", 90, 0, null, DevExpress.Utils.HorzAlignment.Center);
            MostrarColumna("FECHA_CHEQUE", "Fecha", 100, 1, "dd/MM/yyyy", DevExpress.Utils.HorzAlignment.Center);
            MostrarColumna("NOMBRE_CHEQUE", "Beneficiario", 280, 2);
            MostrarColumna("CONCEPTO", "Concepto", 310, 3);
            MostrarColumna("MONTO", "Monto", 125, 4, "n2", DevExpress.Utils.HorzAlignment.Far);
            MostrarColumna("NUM_CUENTA", "Cuenta", 145, 5, null, DevExpress.Utils.HorzAlignment.Center);
            MostrarColumna("NOMBRE_CUENTA", "Banco / cuenta", 200, 6);
            MostrarColumna("NID_PARTIDA", "Partida", 100, 7, null, DevExpress.Utils.HorzAlignment.Center);
            MostrarColumna("IMPRESO", "Impreso", 80, 8, null, DevExpress.Utils.HorzAlignment.Center);
            MostrarColumna("ANULADO", "Anulado", 80, 9, null, DevExpress.Utils.HorzAlignment.Center);

            GridColumn monto = gvCheques.Columns["MONTO"];
            if (monto != null)
            {
                monto.Summary.Clear();
                monto.Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: {0:n2}");
            }

            GridColumn cheque = gvCheques.Columns["NUM_CHEQUE"];
            if (cheque != null)
            {
                cheque.AppearanceCell.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Underline);
                cheque.AppearanceCell.ForeColor = Color.FromArgb(0, 102, 170);
                cheque.AppearanceCell.Options.UseFont = true;
                cheque.AppearanceCell.Options.UseForeColor = true;
            }

            DistribuirColumnasResultados();
        }

        private void gridCheques_SizeChanged(object sender, EventArgs e)
        {
            DistribuirColumnasResultados();
        }

        private void DistribuirColumnasResultados()
        {
            if (gridCheques.ClientSize.Width <= 0 || gvCheques.Columns.Count == 0)
                return;

            var proporciones = new Dictionary<string, int>
            {
                { "FECHA_CHEQUE", 10 },
                { "NOMBRE_CHEQUE", 25 },
                { "CONCEPTO", 30 },
                { "MONTO", 12 },
                { "NUM_CHEQUE", 13 },
                { "NUM_CUENTA", 12 },
                { "NOMBRE_CUENTA", 18 },
                { "NID_PARTIDA", 10 },
                { "IMPRESO", 8 },
                { "ANULADO", 8 }
            };

            int pesoTotal = 0;
            foreach (var item in proporciones)
            {
                GridColumn columna = gvCheques.Columns[item.Key];
                if (columna != null && columna.Visible)
                    pesoTotal += item.Value;
            }

            if (pesoTotal == 0)
                return;

            int anchoDisponible = Math.Max(500, gridCheques.ClientSize.Width - 22);
            int anchoAsignado = 0;
            GridColumn ultimaVisible = null;

            foreach (var item in proporciones)
            {
                GridColumn columna = gvCheques.Columns[item.Key];
                if (columna == null || !columna.Visible)
                    continue;

                int ancho = (int)Math.Floor(anchoDisponible * (double)item.Value / pesoTotal);
                columna.Width = ancho;
                anchoAsignado += ancho;
                ultimaVisible = columna;
            }

            if (ultimaVisible != null)
                ultimaVisible.Width += anchoDisponible - anchoAsignado;
        }

        private void MostrarColumna(string nombre, string titulo, int ancho, int posicion,
            string formato = null,
            DevExpress.Utils.HorzAlignment alineacion = DevExpress.Utils.HorzAlignment.Near)
        {
            GridColumn columna = gvCheques.Columns[nombre];
            if (columna == null) return;
            columna.Visible = true;
            columna.VisibleIndex = posicion;
            columna.Caption = titulo;
            columna.Width = ancho;
            columna.MinWidth = Math.Min(ancho, 80);
            columna.OptionsColumn.FixedWidth = false;
            columna.AppearanceCell.TextOptions.HAlignment = alineacion;
            columna.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            if (!string.IsNullOrWhiteSpace(formato))
            {
                columna.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                columna.DisplayFormat.FormatString = formato;
            }
        }

        private void CambiarEstadoGeneracion(bool generando)
        {
            btnEmitir.Enabled = !generando;
            btnEliminarPruebas.Enabled = !generando;
            btnVerReporte.Enabled = !generando;
            btnSalir.Enabled = !generando;
            grpDatos.Enabled = !generando;
            gridCheques.Enabled = !generando;
        }

        private void MostrarProgreso(bool visible)
        {
            panelProgreso.Visible = visible;
            progressGeneracion.Style = visible ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
            progressGeneracion.MarqueeAnimationSpeed = visible ? 25 : 0;
            if (!visible)
                lblEstadoProceso.Text = string.Empty;
            AjustarAreaResultados();
        }

        private void btnVerReporte_Click(object sender, EventArgs e)
        {
            int? idCatorcena = ValorCombo(cbxCatorcena);
            int? idTipoPlanilla = ValorCombo(cbxTipoPlanilla);
            int? esContribuyente = ValorCombo(cbxEsContribuyente);

            if (!idCatorcena.HasValue || !idTipoPlanilla.HasValue || !esContribuyente.HasValue)
            {
                XtraMessageBox.Show(
                    "Seleccione Catorcena, Tipo de planilla y Contribuyente para generar el reporte.",
                    "Ver reporte", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var reporte = new RptChequesPlanilla(
                    idCatorcena.Value,
                    idTipoPlanilla.Value,
                    esContribuyente.Value);

                reporte.MostrarPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "No se pudo generar el reporte de cheques de planilla:\n" + ex.Message,
                    "Ver reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminarPruebas_Click(object sender, EventArgs e)
        {
            int? idZafra = ValorCombo(cbxZafra);
            int? idCatorcena = ValorCombo(cbxCatorcena);
            int? idTipoPlanilla = ValorCombo(cbxTipoPlanilla);
            int? esContribuyente = ValorCombo(cbxEsContribuyente);

            if (!idZafra.HasValue || !idCatorcena.HasValue || !idTipoPlanilla.HasValue ||
                !esContribuyente.HasValue || _idCuenta <= 0)
            {
                XtraMessageBox.Show(
                    "Seleccione Zafra, Catorcena, Tipo de planilla, Contribuyente y Cuenta bancaria.",
                    "Borrar pruebas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string seleccion = string.Format(
                "Zafra: {0}\nCatorcena: {1}\nTipo de planilla: {2}\nContribuyente: {3}\nCuenta: {4}",
                cbxZafra.Text, cbxCatorcena.Text, cbxTipoPlanilla.Text,
                cbxEsContribuyente.Text, txtCuenta.Text);

            if (XtraMessageBox.Show(
                    "Se eliminarán los cheques de prueba y sus partidas para esta selección:\n\n" +
                    seleccion + "\n\nTambién se liberarán los registros correspondientes de PLANILLA.",
                    "Borrar pruebas", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            if (XtraMessageBox.Show(
                    "Esta acción no se puede deshacer.\n\n¿Confirma el borrado?",
                    "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Stop,
                    MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            try
            {
                CambiarEstadoGeneracion(true);
                Cursor = Cursors.WaitCursor;
                lblEstadoProceso.Text = "Eliminando cheques de prueba, espere...";
                MostrarProgreso(true);

                DataTable resultado = await Task.Run(() => _dal.EjecutarConsulta(
                    PROCEDIMIENTO_BORRAR_PRUEBAS,
                    new
                    {
                        ID_ZAFRA = idZafra.Value,
                        ID_CATORCENA = idCatorcena.Value,
                        ID_TIPO_PLANILLA = idTipoPlanilla.Value,
                        ES_CONTRIBUYENTE = esContribuyente.Value,
                        ID_CUENTA = _idCuenta,
                        USUARIO = Configuracion.UsuarioActual
                    }));

                int cheques = 0;
                int partidas = 0;
                int planillas = 0;
                if (resultado != null && resultado.Rows.Count > 0)
                {
                    DataRow fila = resultado.Rows[0];
                    cheques = Convert.ToInt32(fila["CHEQUES_ELIMINADOS"]);
                    partidas = Convert.ToInt32(fila["PARTIDAS_ELIMINADAS"]);
                    planillas = Convert.ToInt32(fila["PLANILLAS_LIBERADAS"]);
                }

                _chequesGenerados = null;
                gridCheques.DataSource = null;
                grpResultados.Text = "  Cheques generados — 0 registro(s)  ";

                XtraMessageBox.Show(
                    string.Format("Cheques eliminados: {0}\nPartidas eliminadas: {1}\nRegistros de planilla liberados: {2}",
                        cheques, partidas, planillas),
                    "Borrar pruebas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("No se pudieron borrar las pruebas:\n" + ex.Message,
                    "Borrar pruebas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MostrarProgreso(false);
                Cursor = Cursors.Default;
                CambiarEstadoGeneracion(false);
            }
        }

        private void gvCheques_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.Column == null || e.RowHandle < 0)
                return;

            if (gvCheques.Columns["ID_CHEQUE"] == null)
            {
                XtraMessageBox.Show("No se pudo identificar el cheque seleccionado.",
                    "Ver cheque", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            object valor = gvCheques.GetRowCellValue(e.RowHandle, "ID_CHEQUE");
            int idCheque;
            if (valor == null || valor == DBNull.Value || !int.TryParse(valor.ToString(), out idCheque))
            {
                XtraMessageBox.Show("No se pudo identificar el cheque seleccionado.",
                    "Ver cheque", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var reporte = new rptCheque { IdCheque = idCheque };
                reporte.MostrarPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("No se pudo generar el reporte del cheque:\n" + ex.Message,
                    "Ver cheque", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static void AjustarIcono(SimpleButton boton, int px)
        {
            Image original = boton.ImageOptions.Image;
            if (original == null) return;
            var imagen = new Bitmap(px, px);
            using (Graphics g = Graphics.FromImage(imagen))
                g.DrawImage(original, 0, 0, px, px);
            boton.ImageOptions.Image = imagen;
        }
    }
}
