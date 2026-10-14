using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SistemaContable.DAL;
using SistemaContable.RP.Planilla;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;

namespace SistemaContable.UI.Forms.Planilla
{
    /// <summary>Migración WinForms del control web ERPMH ListComprobLQ.</summary>
    public partial class frmLiquidacionCorte : Form
    {
        private const int ID_TIPO_COMPROBANTE = 12;
        private const int ES_COMP_VFP = 0;

        private const string SP_TIPO_COMPROBANTE = "[ECOMPROB].[CB_TIPO_COMPROB_LQ]";
        private const string SP_ZAFRAS = "[ECOMPROB].[CB_ZAFRAS_ACT]";
        private const string SP_CATORCENAS = "[ECOMPROB].[CB_CATORCENA_ZAFRA]";
        private const string SP_TIPO_PLANILLA = "[ECOMPROB].[CB_TIPO_PLANILLA]";
        private const string SP_LISTA_COMPROBANTES = "[ECOMPROB].[LIST_COMPROB_LIQUIDACION]";
        private const string SP_LISTA_LIQUIDACIONES = "[ECOMPROB].[LIST_COMPROB_LIQUIDACION_EMAIL]";
        private const string SP_FECHAS = "[ECOMPROB].[VIEW_COMPROB_LIQUIDACION_EMAIL_FECHA_DT]";
        private const string SP_GRUPOS_ENVIO = "[ECOMPROB].[CB_COMPROBANTES_LIQUIDACION_ORDEN]";
        private const string SP_PENDIENTES_IMPORTAR = "[ECOMPROB].[LIST_COMPROB_LIQUIDACION_PENDIENTE_IMPORTAR]";
        private const string SP_IMPORTAR = "[ECOMPROB].[CRE_COMPROB_LIQUIDACION]";
        private const string SP_ELIMINAR_PRUEBA = "[ECOMPROB].[CRE_COMPROB_LIQUIDACION_ELIMINAR_PRUEBA]";
        private const string SP_DATOS_ENVIO = "[ECOMPROB].[VIEW_COMPROB_LIQUIDACION_EMAIL]";
        private const string SP_MARCAR_ENVIADO = "[ECOMPROB].[UPD_COMPROB_LIQUIDACION_EMAIL]";

        private readonly DALBase _dal = new DALBase();
        private bool _cargando;
        private bool _consultaRealizada;

        public frmLiquidacionCorte()
        {
            InitializeComponent();
        }

        private void frmLiquidacionCorte_Load(object sender, EventArgs e)
        {
            ConfigurarGrid(gvComprobantes);
            ConfigurarGrid(gvLiquidaciones);
            AjustarIconos();
            _cargando = true;
            try
            {
                CargarTipoComprobante();
                CargarZafras();
            }
            finally { _cargando = false; }
            ActualizarModoPrueba();
            ActualizarEstadoBotones();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            DividirMitad();
            Resize += (s, a) => DividirMitad();
        }

        private void DividirMitad()
        {
            if (WindowState == FormWindowState.Minimized) return;
            int mitad = (splitMain.Width - splitMain.SplitterWidth) / 2;
            if (mitad > splitMain.Panel1MinSize && mitad < splitMain.Width - splitMain.Panel2MinSize)
                splitMain.SplitterDistance = mitad;
        }

        private void AjustarIconos()
        {
            foreach (var boton in new[] { btnConsultar, btnImportar, btnEliminarPrueba, btnEnviar, btnSalir })
            {
                Image original = boton.ImageOptions.Image;
                if (original == null) continue;
                var bmp = new Bitmap(26, 26);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(original, 0, 0, 26, 26);
                }
                boton.ImageOptions.Image = bmp;
            }
        }

        private static int? ValorCombo(System.Windows.Forms.ComboBox combo)
        {
            object valor = combo.SelectedValue;
            if (valor == null || valor == DBNull.Value || valor is DataRowView) return null;
            return int.TryParse(Convert.ToString(valor), out int resultado) ? resultado : (int?)null;
        }

        private int? TipoContribuyente
        {
            get
            {
                if (rbContribuyente.Checked) return 1;
                if (rbNoContribuyente.Checked) return 0;
                return null;
            }
        }

        private static void EnlazarCombo(System.Windows.Forms.ComboBox combo, DataTable tabla, string valor, string texto)
        {
            combo.DataSource = tabla;
            combo.ValueMember = valor;
            combo.DisplayMember = texto;
            combo.SelectedIndex = tabla.Rows.Count > 0 ? 0 : -1;
        }

        private void CargarTipoComprobante()
        {
            var dt = _dal.EjecutarConsulta(SP_TIPO_COMPROBANTE, new { ID_TIPO_COMPROB = ID_TIPO_COMPROBANTE });
            EnlazarCombo(cbxTipoComprobante, dt, "ID_TIPO_COMPROB", "NOMBRE_TIPO");
        }

        private void CargarZafras()
        {
            var dt = _dal.EjecutarConsulta(SP_ZAFRAS);
            EnlazarCombo(cbxZafra, dt, "ID_ZAFRA", "NOMBRE_ZAFRA");
            if (ValorCombo(cbxZafra).HasValue) CargarCatorcenas();
        }

        private void CargarCatorcenas()
        {
            int? zafra = ValorCombo(cbxZafra);
            if (!zafra.HasValue) return;
            var dt = _dal.EjecutarConsulta(SP_CATORCENAS, new { ID_ZAFRA = zafra.Value });
            EnlazarCombo(cbxCatorcena, dt, "ID_CATORCENA", "CATORCENA");
            CargarTiposPlanilla();
        }

        private void CargarTiposPlanilla()
        {
            int? zafra = ValorCombo(cbxZafra);
            int? catorcena = ValorCombo(cbxCatorcena);
            if (!zafra.HasValue || !catorcena.HasValue) return;
            var dt = _dal.EjecutarConsulta(SP_TIPO_PLANILLA,
                new { ID_ZAFRA = zafra.Value, ID_CATORCENA = catorcena.Value });
            EnlazarCombo(cbxTipoPlanilla, dt, "ID_TIPO_PLANILLA", "NOMBRE_TIPO_PLANILLA");
        }

        private void CargarGruposEnvio()
        {
            if (!_consultaRealizada)
            {
                cbxGrupoEnvio.DataSource = null;
                cbxGrupoEnvio.Enabled = false;
                lblGrupoEnvio.Enabled = false;
                return;
            }

            int? zafra = ValorCombo(cbxZafra);
            int? catorcena = ValorCombo(cbxCatorcena);
            int? tipoPlanilla = ValorCombo(cbxTipoPlanilla);
            if (!zafra.HasValue || !catorcena.HasValue || !tipoPlanilla.HasValue)
            {
                cbxGrupoEnvio.DataSource = null;
                cbxGrupoEnvio.Enabled = false;
                lblGrupoEnvio.Enabled = false;
                return;
            }

            var dt = _dal.EjecutarConsulta(SP_GRUPOS_ENVIO, new
            {
                ID_CATORCENA = catorcena.Value,
                ID_TIPO_PLANILLA = tipoPlanilla.Value,
                ID_ZAFRA = zafra.Value,
                ES_CONTRIBUYENTE = TipoContribuyente,
                ES_PRUEBA = chkPrueba.Checked ? 1 : 0
            });
            EnlazarCombo(cbxGrupoEnvio, dt, "ORDEN_ENVIO", "DESCRIPCION_GRUPO");
            bool hayGrupos = dt.Rows.Count > 0 && gvLiquidaciones.RowCount > 0;
            cbxGrupoEnvio.Enabled = hayGrupos;
            lblGrupoEnvio.Enabled = hayGrupos;
        }

        private void CargarFechas()
        {
            if (!FiltrosBasicosCompletos() || !TipoContribuyente.HasValue) return;
            try
            {
                var dt = _dal.EjecutarConsulta(SP_FECHAS, new
                {
                    ID_ZAFRA = ValorCombo(cbxZafra),
                    ID_CATORCENA = ValorCombo(cbxCatorcena),
                    ES_CONTRIBUYENTE = TipoContribuyente,
                    ID_TIPO_PLANILLA = ValorCombo(cbxTipoPlanilla),
                    ES_COMP_VFP
                });
                if (dt.Rows.Count == 0) return;
                DataRow fila = dt.Rows[0];
                if (dt.Columns.Contains("FECHA_COMPROBANTE_INI") && fila["FECHA_COMPROBANTE_INI"] != DBNull.Value)
                    dtpFechaInicial.Value = Convert.ToDateTime(fila["FECHA_COMPROBANTE_INI"]);
                if (dt.Columns.Contains("FECHA_COMPROBANTE_FIN") && fila["FECHA_COMPROBANTE_FIN"] != DBNull.Value)
                    dtpFechaFinal.Value = Convert.ToDateTime(fila["FECHA_COMPROBANTE_FIN"]);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("No fue posible obtener las fechas sugeridas:\n\n" + ex.Message,
                    "Comprobantes de Liquidación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool FiltrosBasicosCompletos()
        {
            return ValorCombo(cbxZafra).HasValue && ValorCombo(cbxCatorcena).HasValue &&
                   ValorCombo(cbxTipoPlanilla).HasValue;
        }

        private bool ValidarFiltros(bool validarFechas)
        {
            if (!FiltrosBasicosCompletos())
            {
                XtraMessageBox.Show("Seleccione Zafra, Catorcena y Tipo Planilla.",
                    "Comprobantes de Liquidación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!TipoContribuyente.HasValue)
            {
                XtraMessageBox.Show("Seleccione si el proveedor es contribuyente o no contribuyente.",
                    "Comprobantes de Liquidación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (validarFechas && dtpFechaInicial.Value.Date > dtpFechaFinal.Value.Date)
            {
                XtraMessageBox.Show("La fecha inicial no puede ser mayor que la fecha final.",
                    "Comprobantes de Liquidación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private object FiltrosLista(bool incluirPrueba)
        {
            if (incluirPrueba)
            {
                return new
                {
                    ID_ZAFRA = ValorCombo(cbxZafra),
                    ID_CATORCENA = ValorCombo(cbxCatorcena),
                    ES_CONTRIBUYENTE = TipoContribuyente,
                    ID_TIPO_PLANILLA = ValorCombo(cbxTipoPlanilla),
                    ES_COMP_VFP,
                    ES_PRUEBA = chkPrueba.Checked ? 1 : 0
                };
            }
            return new
            {
                ID_ZAFRA = ValorCombo(cbxZafra),
                ID_CATORCENA = ValorCombo(cbxCatorcena),
                ES_CONTRIBUYENTE = TipoContribuyente,
                ID_TIPO_PLANILLA = ValorCombo(cbxTipoPlanilla),
                ES_COMP_VFP,
                ES_PRUEBA = chkPrueba.Checked ? 1 : 0
            };
        }

        private void CargarListados(bool mostrarResultadoVacio = false)
        {
            var pendientes = _dal.EjecutarConsulta(SP_LISTA_COMPROBANTES, FiltrosLista(true));
            var generados = _dal.EjecutarConsulta(SP_LISTA_LIQUIDACIONES, FiltrosLista(false));
            gridComprobantes.DataSource = pendientes;
            gridLiquidaciones.DataSource = generados;
            gvComprobantes.PopulateColumns();
            gvLiquidaciones.PopulateColumns();
            FormatearColumnas(gvComprobantes);
            FormatearColumnas(gvLiquidaciones);
            lblPendientes.Text = $"Comprobantes disponibles  —  {pendientes.Rows.Count} registro(s)";
            lblGenerados.Text = $"Liquidaciones importadas  —  {generados.Rows.Count} registro(s)";
            _consultaRealizada = true;
            CargarGruposEnvio();
            ActualizarEstadoBotones();

            if (mostrarResultadoVacio && pendientes.Rows.Count == 0 && generados.Rows.Count == 0)
            {
                XtraMessageBox.Show(
                    "No existen liquidaciones importadas para los filtros seleccionados.\n\n" +
                    "Puede continuar con el botón Importar para generar los comprobantes de esta selección.",
                    "Consulta sin registros", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ConfigurarGrid(GridView view)
        {
            view.OptionsBehavior.Editable = false;
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ShowFooter = true;
            view.OptionsView.EnableAppearanceEvenRow = true;
            view.OptionsView.ColumnAutoWidth = false;
            view.OptionsFind.AlwaysVisible = true;
            view.OptionsFind.FindNullPrompt = "Introduzca el texto a buscar...";
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            view.Appearance.FooterPanel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            view.RowCellStyle += Grid_RowCellStyle;
        }

        private void Grid_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (!chkPrueba.Checked || e.Column == null || e.Column.FieldName != "EMAIL") return;

            e.Appearance.ForeColor = Color.Red;
            e.Appearance.FontStyleDelta = FontStyle.Bold;
        }

        private static void FormatearColumnas(GridView view)
        {
            string[] numericas = { "VALOR", "IVA", "VALOR_BRUTO", "RETENCION_IVA", "VALOR_NETO", "SUMAS", "RENTA", "SUBTOTAL", "IVA_RETENIDO", "TOTAL" };
            foreach (GridColumn columna in view.Columns)
            {
                columna.OptionsColumn.AllowEdit = false;
                if (Array.IndexOf(numericas, columna.FieldName) >= 0)
                {
                    columna.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    columna.DisplayFormat.FormatString = "N2";
                    columna.Summary.Clear();
                    columna.Summary.Add(DevExpress.Data.SummaryItemType.Sum, columna.FieldName, "{0:N2}");
                    columna.Width = 105;
                }
                else if (columna.FieldName.StartsWith("FECHA", StringComparison.OrdinalIgnoreCase))
                {
                    columna.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    columna.DisplayFormat.FormatString = "dd/MM/yyyy";
                    columna.Width = 100;
                }
                else if (columna.FieldName == "NOM_PROVEEDOR") columna.Width = 250;
                else if (columna.FieldName == "EMAIL") columna.Width = 220;
                else if (columna.FieldName == "DESCRIPCION_CONCEPTO") columna.Width = 360;
                else columna.Width = 90;
            }
        }

        private void ActualizarEstadoBotones()
        {
            bool filtros = FiltrosBasicosCompletos() && TipoContribuyente.HasValue;
            bool hayGenerados = _consultaRealizada && gvLiquidaciones.RowCount > 0;
            bool hayGrupo = hayGenerados && ValorCombo(cbxGrupoEnvio).HasValue;
            btnConsultar.Enabled = filtros;
            // La importacion obtiene/genera los comprobantes; por eso debe permitirse
            // despues de consultar incluso cuando el listado inicial venga vacio.
            btnImportar.Enabled = filtros && _consultaRealizada;
            btnEliminarPrueba.Enabled = filtros && chkPrueba.Checked && hayGenerados;
            btnEnviar.Enabled = filtros && hayGrupo;
            dtpFechaInicial.Enabled = filtros;
            dtpFechaFinal.Enabled = filtros;
            cbxGrupoEnvio.Enabled = hayGrupo;
            lblGrupoEnvio.Enabled = hayGrupo;
        }

        private void cbxZafra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando) return;
            _cargando = true;
            try { CargarCatorcenas(); } finally { _cargando = false; }
            LimpiarListados();
        }

        private void cbxCatorcena_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando) return;
            _cargando = true;
            try { CargarTiposPlanilla(); } finally { _cargando = false; }
            LimpiarListados();
        }

        private void cbxTipoPlanilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando) return;
            CargarFechas();
            LimpiarListados();
        }

        private void TipoContribuyente_CheckedChanged(object sender, EventArgs e)
        {
            if (_cargando || !((RadioButton)sender).Checked) return;
            CargarFechas();
            LimpiarListados();
        }

        private void chkPrueba_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarModoPrueba();
            LimpiarListados();
            ActualizarEstadoBotones();
        }

        private void ActualizarModoPrueba()
        {
            bool prueba = chkPrueba.Checked;
            lblModoPrueba.Visible = prueba;
            btnEnviar.Text = prueba ? "Enviar prueba" : "Enviar";
            Text = prueba
                ? "Comprobantes de Liquidación - MODO PRUEBA"
                : "Comprobantes de Liquidación";
            grpFiltros.BackColor = prueba
                ? Color.FromArgb(255, 250, 230)
                : Color.FromArgb(245, 247, 250);
            chkPrueba.ForeColor = prueba ? Color.DarkRed : SystemColors.ControlText;
            chkPrueba.Font = new Font("Segoe UI", 9F,
                prueba ? FontStyle.Bold : FontStyle.Regular);
        }

        private void cbxGrupoEnvio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_cargando) ActualizarEstadoBotones();
        }

        private void LimpiarListados()
        {
            _consultaRealizada = false;
            gridComprobantes.DataSource = null;
            gridLiquidaciones.DataSource = null;
            cbxGrupoEnvio.DataSource = null;
            cbxGrupoEnvio.Enabled = false;
            lblGrupoEnvio.Enabled = false;
            lblPendientes.Text = "Comprobantes disponibles";
            lblGenerados.Text = "Liquidaciones importadas";
            ActualizarEstadoBotones();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (!ValidarFiltros(false)) return;
            EjecutarConEspera(() => CargarListados(true), "consultar los comprobantes");
        }

        private async void btnImportar_Click(object sender, EventArgs e)
        {
            if (!ValidarFiltros(true)) return;
            string tipo = TipoContribuyente == 1 ? "Contribuyente" : "No contribuyente";
            if (XtraMessageBox.Show(
                    $"¿Importar los comprobantes de liquidación?\n\nZafra: {cbxZafra.Text}\nCatorcena: {cbxCatorcena.Text}\nTipo planilla: {cbxTipoPlanilla.Text}\nTipo contribuyente: {tipo}\nPrueba: {(chkPrueba.Checked ? "Sí" : "No")}",
                    "Importar liquidaciones", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            int? idZafra = ValorCombo(cbxZafra);
            int? idCatorcena = ValorCombo(cbxCatorcena);
            int? idTipoPlanilla = ValorCombo(cbxTipoPlanilla);
            int? esContribuyente = TipoContribuyente;
            int esPrueba = chkPrueba.Checked ? 1 : 0;
            DateTime fechaInicial = dtpFechaInicial.Value.Date;
            DateTime fechaFinal = dtpFechaFinal.Value.Date;

            MostrarProgresoImportacion();
            try
            {
                Cursor = Cursors.WaitCursor;
                panelBotones.Enabled = false;
                DataTable pendientes = await Task.Run(() => _dal.EjecutarConsulta(SP_PENDIENTES_IMPORTAR, new
                {
                    ID_ZAFRA = idZafra,
                    ID_CATORCENA = idCatorcena,
                    ES_CONTRIBUYENTE = esContribuyente,
                    ID_TIPO_PLANILLA = idTipoPlanilla,
                    ES_PRUEBA = esPrueba
                }));

                if (pendientes.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No existen proveedores pendientes de importar para la selección actual.",
                        "Importar liquidaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ConfigurarProgresoImportacion(pendientes.Rows.Count);
                int importados = 0;
                var errores = new List<string>();

                for (int indice = 0; indice < pendientes.Rows.Count; indice++)
                {
                    DataRow fila = pendientes.Rows[indice];
                    int codigo = Convert.ToInt32(fila["CODIGO"]);
                    string proveedor = Texto(fila, "NOM_PROVEEDOR");
                    string correo = Texto(fila, "EMAIL");
                    ActualizarProgresoCorreo(indice, pendientes.Rows.Count,
                        $"Importando comprobante {indice + 1} de {pendientes.Rows.Count}: {proveedor} — {correo}");

                    try
                    {
                        await Task.Run(() =>
                        {
                            var p = new DynamicParameters();
                            p.Add("@ID_ZAFRA", idZafra);
                            p.Add("@ID_CATORCENA", idCatorcena);
                            p.Add("@ES_CONTRIBUYENTE", esContribuyente);
                            p.Add("@ID_TIPO_PLANILLA", idTipoPlanilla);
                            p.Add("@ES_COMP_VFP", ES_COMP_VFP);
                            p.Add("@ES_PRUEBA", esPrueba);
                            p.Add("@FECHA_COMPROBANTE_INI", fechaInicial);
                            p.Add("@FECHA_COMPROBANTE_FIN", fechaFinal);
                            p.Add("@msg", dbType: DbType.String, size: -1, direction: ParameterDirection.Output);
                            p.Add("@CODIGO", codigo);
                            _dal.EjecutarConSalida(SP_IMPORTAR, p);
                        });
                        importados++;
                    }
                    catch (Exception ex)
                    {
                        errores.Add($"{proveedor}: {ex.Message}");
                    }

                    ActualizarProgresoCorreo(indice + 1, pendientes.Rows.Count,
                        $"Comprobante procesado {indice + 1} de {pendientes.Rows.Count}: {proveedor}");
                }

                CompletarProgresoImportacion(pendientes.Rows.Count, importados, errores.Count);
                CargarListados();
                var resumen = new StringBuilder();
                resumen.AppendLine($"Comprobantes importados: {importados}");
                if (errores.Count > 0)
                {
                    resumen.AppendLine($"Con error: {errores.Count}");
                    resumen.AppendLine();
                    foreach (string error in errores) resumen.AppendLine("• " + error);
                }
                XtraMessageBox.Show(resumen.ToString(), "Importar liquidaciones", MessageBoxButtons.OK,
                    errores.Count == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"No fue posible importar los comprobantes:\n\n{ex.Message}",
                    "Comprobantes de Liquidación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                panelBotones.Enabled = true;
                OcultarProgresoCorreo();
                ActualizarEstadoBotones();
            }
        }

        private void btnEliminarPrueba_Click(object sender, EventArgs e)
        {
            if (!ValidarFiltros(false)) return;
            if (!chkPrueba.Checked)
            {
                XtraMessageBox.Show("Active la opción Prueba para eliminar solamente datos de prueba.",
                    "Eliminar pruebas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (XtraMessageBox.Show(
                    $"Se eliminarán los comprobantes de prueba de:\n\nZafra: {cbxZafra.Text}\nCatorcena: {cbxCatorcena.Text}\nTipo planilla: {cbxTipoPlanilla.Text}\n\n¿Desea continuar?",
                    "Eliminar pruebas", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            EjecutarConEspera(() =>
            {
                var p = new DynamicParameters();
                p.Add("@ID_ZAFRA", ValorCombo(cbxZafra));
                p.Add("@ID_CATORCENA", ValorCombo(cbxCatorcena));
                p.Add("@ID_TIPO_PLANILLA", ValorCombo(cbxTipoPlanilla));
                p.Add("@msg", dbType: DbType.String, size: -1, direction: ParameterDirection.Output);
                _dal.EjecutarConSalida(SP_ELIMINAR_PRUEBA, p);
                string mensaje = p.Get<string>("@msg");
                CargarListados();
                XtraMessageBox.Show(string.IsNullOrWhiteSpace(mensaje) ? "Pruebas eliminadas." : mensaje,
                    "Eliminar pruebas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }, "eliminar las pruebas");
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (!ValidarFiltros(false)) return;
            int? grupoEnvio = ValorCombo(cbxGrupoEnvio);
            if (!grupoEnvio.HasValue)
            {
                XtraMessageBox.Show("Seleccione un grupo de envío.", "Enviar liquidaciones",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                EjecutarConEspera(() => EnviarGrupo(grupoEnvio.Value), "enviar los comprobantes por correo");
            }
            finally
            {
                OcultarProgresoCorreo();
            }
        }

        private void EnviarGrupo(int grupoEnvio)
        {
            DataTable documentos = _dal.EjecutarConsulta(SP_DATOS_ENVIO, new
            {
                ID_ZAFRA = ValorCombo(cbxZafra),
                ID_CATORCENA = ValorCombo(cbxCatorcena),
                ES_CONTRIBUYENTE = TipoContribuyente,
                ID_TIPO_PLANILLA = ValorCombo(cbxTipoPlanilla),
                ES_COMP_VFP,
                ORDEN_ENVIO = grupoEnvio,
                ES_PRUEBA = chkPrueba.Checked ? 1 : 0
            });

            if (documentos.Rows.Count == 0)
            {
                XtraMessageBox.Show("El grupo seleccionado no contiene documentos.",
                    "Enviar liquidaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int pendientes = 0;
            foreach (DataRow fila in documentos.Rows)
                if (!EsVerdadero(fila, "ES_ENVIADO")) pendientes++;

            if (pendientes == 0)
            {
                XtraMessageBox.Show("Todos los documentos de este grupo ya fueron enviados.",
                    "Enviar liquidaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string modo = chkPrueba.Checked ? "PRUEBA" : "PRODUCCIÓN";
            if (XtraMessageBox.Show(
                    $"Se enviarán {pendientes} comprobante(s) del grupo {grupoEnvio}.\n\nModo: {modo}\n¿Desea continuar?",
                    "Confirmar envío", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            ValidarConfiguracionCorreo();
            string correoPrueba = chkPrueba.Checked ? ObtenerCorreoPrueba() : null;
            int enviados = 0;
            int omitidos = 0;
            int procesados = 0;
            var errores = new List<string>();
            MostrarProgresoCorreo(pendientes);

            foreach (DataRow fila in documentos.Rows)
            {
                if (EsVerdadero(fila, "ES_ENVIADO"))
                {
                    omitidos++;
                    continue;
                }

                string proveedor = Texto(fila, "NOM_PROVEEDOR");
                string correoProveedor = Texto(fila, "EMAIL");
                string destinatario = chkPrueba.Checked ? correoPrueba : correoProveedor;
                string destinoVisible = chkPrueba.Checked
                    ? $"{destinatario} (prueba; correo del proveedor: {correoProveedor})"
                    : destinatario;
                ActualizarProgresoCorreo(procesados, pendientes,
                    $"Enviando correo {procesados + 1} de {pendientes} a {destinoVisible} — {proveedor}");
                try
                {
                    if (string.IsNullOrWhiteSpace(destinatario))
                        throw new InvalidOperationException("El proveedor no tiene correo electrónico.");

                    EnviarDocumento(fila, destinatario, chkPrueba.Checked);
                    _dal.EjecutarSinRetorno(SP_MARCAR_ENVIADO, new
                    {
                        ID_ZAFRA = Convert.ToInt32(fila["ID_ZAFRA"]),
                        ID_CATORCENA = Convert.ToInt32(fila["ID_CATORCENA"]),
                        ID_TIPO_PLANILLA = Convert.ToInt32(fila["ID_TIPO_PLANILLA"]),
                        CODIGO = Convert.ToInt32(fila["CODIGO"]),
                        ES_PRUEBA = chkPrueba.Checked ? 1 : 0
                    });
                    enviados++;
                }
                catch (Exception ex)
                {
                    errores.Add($"{proveedor}: {ex.Message}");
                }
                finally
                {
                    procesados++;
                    ActualizarProgresoCorreo(procesados, pendientes,
                        $"Correo procesado {procesados} de {pendientes}: {destinoVisible}");
                }
            }

            ActualizarProgresoCorreo(pendientes, pendientes,
                $"Proceso de envío finalizado: {enviados} enviado(s), {errores.Count} con error.");
            CargarListados();
            var resumen = new StringBuilder();
            resumen.AppendLine($"Correos enviados: {enviados}");
            if (omitidos > 0) resumen.AppendLine($"Ya enviados (omitidos): {omitidos}");
            if (errores.Count > 0)
            {
                resumen.AppendLine($"Con error: {errores.Count}");
                resumen.AppendLine();
                foreach (string error in errores) resumen.AppendLine("• " + error);
            }

            XtraMessageBox.Show(resumen.ToString(), "Resultado del envío",
                MessageBoxButtons.OK, errores.Count == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        private void MostrarProgresoCorreo(int total)
        {
            pbEnvioCorreo.Style = ProgressBarStyle.Continuous;
            pbEnvioCorreo.MarqueeAnimationSpeed = 0;
            pbEnvioCorreo.Minimum = 0;
            pbEnvioCorreo.Maximum = Math.Max(1, total);
            pbEnvioCorreo.Value = 0;
            lblProgresoCorreo.Text = $"Preparando el envío de {total} correo(s)...";
            lblProgresoCorreo.Visible = true;
            pbEnvioCorreo.Visible = true;
            MostrarPanelProgreso();
        }

        private void MostrarProgresoImportacion()
        {
            lblProgresoCorreo.Text = "Preparando la lista de proveedores pendientes de importar...";
            pbEnvioCorreo.Style = ProgressBarStyle.Marquee;
            pbEnvioCorreo.MarqueeAnimationSpeed = 30;
            lblProgresoCorreo.Visible = true;
            pbEnvioCorreo.Visible = true;
            MostrarPanelProgreso();
        }

        private void ConfigurarProgresoImportacion(int total)
        {
            pbEnvioCorreo.MarqueeAnimationSpeed = 0;
            pbEnvioCorreo.Style = ProgressBarStyle.Continuous;
            pbEnvioCorreo.Minimum = 0;
            pbEnvioCorreo.Maximum = Math.Max(1, total);
            pbEnvioCorreo.Value = 0;
            lblProgresoCorreo.Text = $"Preparando la importación de {total} comprobante(s)...";
            lblProgresoCorreo.Refresh();
            pbEnvioCorreo.Refresh();
            Application.DoEvents();
        }

        private void CompletarProgresoImportacion(int total, int importados, int errores)
        {
            pbEnvioCorreo.MarqueeAnimationSpeed = 0;
            pbEnvioCorreo.Style = ProgressBarStyle.Continuous;
            pbEnvioCorreo.Minimum = 0;
            pbEnvioCorreo.Maximum = Math.Max(1, total);
            pbEnvioCorreo.Value = Math.Max(1, total);
            lblProgresoCorreo.Text =
                $"Importación finalizada: {importados} importado(s), {errores} con error. Actualizando listados...";
            lblProgresoCorreo.Refresh();
            pbEnvioCorreo.Refresh();
            Application.DoEvents();
        }

        private void MostrarPanelProgreso()
        {
            SuspendLayout();
            panelProgreso.Visible = true;
            ResumeLayout(true);
            PerformLayout();
            splitMain.PerformLayout();
            Invalidate(true);
            Update();
            Application.DoEvents();
        }

        private void ActualizarProgresoCorreo(int procesados, int total, string mensaje)
        {
            pbEnvioCorreo.Maximum = Math.Max(1, total);
            pbEnvioCorreo.Value = Math.Min(procesados, pbEnvioCorreo.Maximum);
            lblProgresoCorreo.Text = mensaje;
            lblProgresoCorreo.Refresh();
            pbEnvioCorreo.Refresh();
            Application.DoEvents();
        }

        private void OcultarProgresoCorreo()
        {
            pbEnvioCorreo.MarqueeAnimationSpeed = 0;
            pbEnvioCorreo.Style = ProgressBarStyle.Continuous;
            lblProgresoCorreo.Visible = false;
            pbEnvioCorreo.Visible = false;
            pbEnvioCorreo.Value = 0;
            SuspendLayout();
            panelProgreso.Visible = false;
            ResumeLayout(true);
            PerformLayout();
            splitMain.PerformLayout();
            Invalidate(true);
            Update();
            gvComprobantes.RefreshData();
            gvLiquidaciones.RefreshData();
        }

        private void EnviarDocumento(DataRow fila, string destinatario, bool esPrueba)
        {
            int idZafra = Convert.ToInt32(fila["ID_ZAFRA"]);
            int idCatorcena = Convert.ToInt32(fila["ID_CATORCENA"]);
            int idTipoPlanilla = Convert.ToInt32(fila["ID_TIPO_PLANILLA"]);
            int codigo = Convert.ToInt32(fila["CODIGO"]);

            using (XtraReport reporte = CrearReporte(idZafra, idCatorcena, idTipoPlanilla, codigo,
                       esPrueba ? 1 : 0))
            {
                string carpeta = ObtenerConfiguracion("LiquidacionCarpetaPdf");
                Directory.CreateDirectory(carpeta);
                string archivo = Path.Combine(carpeta,
                    $"LQ{idZafra}{idCatorcena}{idTipoPlanilla}{ID_TIPO_COMPROBANTE}{codigo}.pdf");
                reporte.ExportToPdf(archivo);

                using (var mensaje = new MailMessage())
                using (var adjunto = new Attachment(archivo))
                using (var servidor = CrearClienteSmtp())
                {
                    string remitente = ObtenerConfiguracion("SmtpRemitente");
                    mensaje.From = new MailAddress(remitente);
                    mensaje.To.Add(new MailAddress(destinatario));
                    mensaje.Subject = (esPrueba ? "[PRUEBA] " : string.Empty) +
                                      "COMPROBANTE DE LIQUIDACIÓN";
                    mensaje.SubjectEncoding = Encoding.UTF8;
                    mensaje.BodyEncoding = Encoding.UTF8;
                    mensaje.IsBodyHtml = true;
                    mensaje.Body = ConstruirCuerpoCorreo(fila, idTipoPlanilla);
                    mensaje.Attachments.Add(adjunto);
                    servidor.Send(mensaje);
                }
            }
        }

        private static XtraReport CrearReporte(int idZafra, int idCatorcena, int idTipoPlanilla,
            int codigo, int esPrueba)
        {
            if (idTipoPlanilla == 1 || idTipoPlanilla == 11)
                return new RptComprobLQProv(idZafra, idCatorcena, idTipoPlanilla, codigo, esPrueba);
            if (idTipoPlanilla == 5)
                return new RptComprobLQProvVip(idZafra, idCatorcena, idTipoPlanilla, codigo, esPrueba);
            if (idTipoPlanilla == 2)
                return new RptComprobLQTrans(idZafra, idCatorcena, idTipoPlanilla, codigo, esPrueba);
            if (idTipoPlanilla == 6)
                throw new InvalidOperationException("Falta integrar el reporte RptComprobLQVFP.");
            throw new InvalidOperationException($"No existe un reporte configurado para el tipo de planilla {idTipoPlanilla}.");
        }

        private static SmtpClient CrearClienteSmtp()
        {
            string host = ObtenerConfiguracion("SmtpHost");
            int puerto = int.Parse(ObtenerConfiguracion("SmtpPuerto"));
            string usuario = ObtenerConfiguracion("SmtpUsuario");
            string nombreVariable = ObtenerConfiguracion("SmtpPasswordVariable");
            string clave = Environment.GetEnvironmentVariable(nombreVariable) ??
                           Environment.GetEnvironmentVariable(nombreVariable, EnvironmentVariableTarget.User) ??
                           Environment.GetEnvironmentVariable(nombreVariable, EnvironmentVariableTarget.Machine);
            if (string.IsNullOrWhiteSpace(clave))
                throw new ConfigurationErrorsException(
                    $"No se encontró la contraseña SMTP en la variable de entorno {nombreVariable}.");

            return new SmtpClient(host, puerto)
            {
                EnableSsl = bool.Parse(ObtenerConfiguracion("SmtpSsl")),
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(usuario, clave),
                TargetName = "STARTTLS/" + host,
                Timeout = 120000
            };
        }

        private static void ValidarConfiguracionCorreo()
        {
            ObtenerConfiguracion("SmtpHost");
            ObtenerConfiguracion("SmtpPuerto");
            ObtenerConfiguracion("SmtpUsuario");
            ObtenerConfiguracion("SmtpRemitente");
            ObtenerConfiguracion("SmtpPasswordVariable");
            ObtenerConfiguracion("LiquidacionCarpetaPdf");
        }

        private static string ObtenerConfiguracion(string clave)
        {
            string valor = ConfigurationManager.AppSettings[clave];
            if (string.IsNullOrWhiteSpace(valor))
                throw new ConfigurationErrorsException($"Falta configurar {clave} en App.config.");
            return valor.Trim();
        }

        private string ObtenerCorreoPrueba()
        {
            DataTable tabla = _dal.EjecutarConsultaSql(
                "SELECT TOP (1) CORREO FROM ECOMPROB.PLANILLA_PRUEBAS ORDER BY ID_PLANILLA_PRUEBA");
            if (tabla.Rows.Count == 0 || string.IsNullOrWhiteSpace(Convert.ToString(tabla.Rows[0]["CORREO"])))
                throw new InvalidOperationException("No existe un correo configurado en ECOMPROB.PLANILLA_PRUEBAS.");
            return Convert.ToString(tabla.Rows[0]["CORREO"]).Trim();
        }

        private static string ConstruirCuerpoCorreo(DataRow fila, int idTipoPlanilla)
        {
            string proveedor = WebUtility.HtmlEncode(Texto(fila, "NOM_PROVEEDOR"));
            string valor = FormatearMontoCorreo(fila, "VALOR");
            string empresa = WebUtility.HtmlEncode(Texto(fila, "EMPRESA_EMITIR_DOCUMENTO_A"));
            string fechaInicial = WebUtility.HtmlEncode(Texto(fila, "FECHA_DIA_MES_INI"));
            string fechaFinal = WebUtility.HtmlEncode(Texto(fila, "FECHA_DIA_MES_FIN"));
            string corte = WebUtility.HtmlEncode(Texto(fila, "CATORCENA"));
            string zafra = WebUtility.HtmlEncode(Texto(fila, "NOMBRE_ZAFRA"));
            string referencia = idTipoPlanilla == 5
                ? "el anticipo al valor final de pago"
                : "el valor de pago para el corte # " + corte;
            string periodo = string.IsNullOrWhiteSpace(fechaInicial) && string.IsNullOrWhiteSpace(fechaFinal)
                ? "No especificado"
                : fechaInicial + " al " + fechaFinal;
            string etiquetaCorte = idTipoPlanilla == 5 ? "Anticipo" : "Corte # " + corte;

            return "<!doctype html><html><head><meta charset='utf-8'>" +
                   "<meta name='viewport' content='width=device-width,initial-scale=1'>" +
                   "<meta name='color-scheme' content='light'><meta name='supported-color-schemes' content='light'>" +
                   "</head><body style='margin:0;padding:0;background-color:#eef2f6;color:#263746;'>" +
                   "<div style='display:none;max-height:0;overflow:hidden;color:transparent;'>" +
                   "Su comprobante de liquidación se encuentra adjunto.</div>" +
                   "<table role='presentation' width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#eef2f6' " +
                   "style='width:100%;background-color:#eef2f6;border-collapse:collapse;'>" +
                   "<tr><td align='center' style='padding:32px 12px;'>" +
                   "<table role='presentation' width='680' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' " +
                   "style='width:100%;max-width:680px;background-color:#ffffff;border-collapse:separate;border-spacing:0;" +
                   "border:1px solid #dce4eb;border-radius:12px;overflow:hidden;'>" +
                   "<tr><td bgcolor='#074f7d' style='padding:28px 34px;background-color:#074f7d;color:#ffffff;'>" +
                   "<div style='font-family:Segoe UI,Arial,sans-serif;font-size:12px;font-weight:700;letter-spacing:1px;" +
                   "text-transform:uppercase;color:#bfe3f5;'>Ingenio Central Azucarero Jiboa</div>" +
                   "<div style='font-family:Segoe UI,Arial,sans-serif;font-size:25px;line-height:32px;font-weight:700;" +
                   "margin-top:6px;'>Comprobante de liquidación</div>" +
                   "<div style='font-family:Segoe UI,Arial,sans-serif;font-size:14px;line-height:21px;color:#e7f4fb;" +
                   "margin-top:5px;'>Información correspondiente a su pago</div></td></tr>" +
                   "<tr><td style='padding:30px 34px 16px 34px;font-family:Segoe UI,Arial,sans-serif;color:#263746;'>" +
                   $"<p style='margin:0 0 18px 0;font-size:15px;line-height:24px;'>Estimado(a) <strong>{proveedor}</strong>:</p>" +
                   $"<p style='margin:0 0 22px 0;font-size:15px;line-height:24px;'>Hacemos llegar {referencia}.</p>" +
                   "<table role='presentation' width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#f3f8fb' " +
                   "style='width:100%;background-color:#f3f8fb;border:1px solid #d7e7f0;border-radius:8px;'>" +
                   "<tr><td style='padding:20px 22px;'>" +
                   "<div style='font-size:12px;font-weight:700;letter-spacing:.6px;text-transform:uppercase;color:#597486;'>Monto de liquidación</div>" +
                   $"<div style='font-size:28px;line-height:36px;font-weight:700;color:#074f7d;margin-top:3px;'>US$ {valor}</div>" +
                   "</td></tr><tr><td style='padding:0 22px 20px 22px;'>" +
                   "<table role='presentation' width='100%' cellspacing='0' cellpadding='0' border='0' style='border-collapse:collapse;'>" +
                   $"<tr><td width='33%' style='padding:9px 12px 9px 0;border-top:1px solid #d7e7f0;font-size:13px;color:#597486;'>Referencia<br><strong style='color:#263746;'>{etiquetaCorte}</strong></td>" +
                   $"<td width='34%' style='padding:9px 12px;border-top:1px solid #d7e7f0;font-size:13px;color:#597486;'>Período<br><strong style='color:#263746;'>{periodo}</strong></td>" +
                   $"<td width='33%' style='padding:9px 0 9px 12px;border-top:1px solid #d7e7f0;font-size:13px;color:#597486;'>Zafra<br><strong style='color:#263746;'>{zafra}</strong></td></tr>" +
                   "</table></td></tr></table>" +
                   "<table role='presentation' width='100%' cellspacing='0' cellpadding='0' border='0' style='margin-top:22px;border-collapse:collapse;'>" +
                   "<tr><td width='5' bgcolor='#6aa84f' style='width:5px;background-color:#6aa84f;border-radius:4px 0 0 4px;'>&nbsp;</td>" +
                   "<td bgcolor='#f6faf3' style='padding:16px 18px;background-color:#f6faf3;font-size:14px;line-height:23px;'>" +
                   $"Favor generar crédito fiscal a favor de <strong>{empresa}</strong> y presentarlo en nuestras oficinas " +
                   $"contables el día <strong>{fechaInicial}</strong> o <strong>{fechaFinal}</strong>, para su registro y relación " +
                   "del comprobante de retención.</td></tr></table>" +
                   "<table role='presentation' width='100%' cellspacing='0' cellpadding='0' border='0' style='margin-top:18px;border-collapse:collapse;'>" +
                   "<tr><td style='padding:13px 16px;background-color:#fff8e6;border:1px solid #f2d58a;border-radius:7px;" +
                   "font-size:13px;line-height:20px;color:#6e5618;'><strong>Documento adjunto:</strong> Consulte el PDF incluido " +
                   "en este correo para ver el detalle completo del comprobante.</td></tr></table>" +
                   "<p style='margin:24px 0 0 0;font-size:14px;line-height:22px;'>Atentamente,<br>" +
                   "<strong>Departamento de Contabilidad</strong></p></td></tr>" +
                   "<tr><td bgcolor='#f7f9fb' style='padding:17px 34px;background-color:#f7f9fb;border-top:1px solid #e2e8ee;" +
                   "font-family:Segoe UI,Arial,sans-serif;font-size:12px;line-height:18px;color:#6a7b88;'>" +
                   "Este es un mensaje informativo generado por el Sistema Contable INJIBOA. Por favor conserve el documento adjunto." +
                   "</td></tr></table></td></tr></table></body></html>";
        }

        private static string FormatearMontoCorreo(DataRow fila, string columna)
        {
            string texto = Texto(fila, columna);
            decimal monto;
            if (decimal.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out monto) ||
                decimal.TryParse(texto, NumberStyles.Any, CultureInfo.GetCultureInfo("es-SV"), out monto))
                return monto.ToString("N2", CultureInfo.GetCultureInfo("en-US"));
            return WebUtility.HtmlEncode(texto);
        }

        private static string Texto(DataRow fila, string columna)
        {
            return fila.Table.Columns.Contains(columna) && fila[columna] != DBNull.Value
                ? Convert.ToString(fila[columna]).Trim()
                : string.Empty;
        }

        private static bool EsVerdadero(DataRow fila, string columna)
        {
            return fila.Table.Columns.Contains(columna) && fila[columna] != DBNull.Value &&
                   Convert.ToBoolean(fila[columna]);
        }

        private void btnSalir_Click(object sender, EventArgs e) => Close();

        private void EjecutarConEspera(Action accion, string operacion)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                panelBotones.Enabled = false;
                accion();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"No fue posible {operacion}:\n\n{ex.Message}",
                    "Comprobantes de Liquidación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                panelBotones.Enabled = true;
                ActualizarEstadoBotones();
            }
        }

        private void gvLiquidaciones_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            object valor = ((GridView)sender).GetRowCellValue(e.RowHandle, "ES_ENVIADO");
            if (valor != null && valor != DBNull.Value && Convert.ToBoolean(valor))
                e.Appearance.BackColor = Color.FromArgb(226, 239, 218);
        }
    }
}
