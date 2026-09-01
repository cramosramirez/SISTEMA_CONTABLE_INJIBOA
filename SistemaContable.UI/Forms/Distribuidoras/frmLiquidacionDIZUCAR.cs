using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SistemaContable.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Distribuidoras
{
    public partial class frmLiquidacionDIZUCAR : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataSet _ds;
        private DataTable _dtGastos;

        private GridView gridDocumentos;
        private GridView gridProductos;
        private GridView gridGastosDetalle;

        private bool _todosExpandidosClq = false;
        private bool _todosExpandidosGastos = false;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit _riBotonGenerar;

        public frmLiquidacionDIZUCAR()
        {
            InitializeComponent();
        }

        private void frmLiquidacionDIZUCAR_Load(object sender, EventArgs e)
        {
            dteFECHA_LIQUIDACION.DateTime = DateTime.Today;

            ConfigurarNivelesGridCLQ();
            ConfigurarNivelesGridGastos();
            dteFECHA_LIQUIDACION.EditValueChanged += (s, ev) => CargarDatos();
            cbxEMPRESA.SelectedIndexChanged += (s, ev) => AplicarFiltroEmpresa();

            linkExpandirContraer.Click += LinkExpandirContraer_Click;
            linkGenerarTodos.Click += LinkGenerarTodos_Click;
            ActualizarTextoLink(); 
            tabPane1.SelectedPageChanged += (s, ev) => ActualizarTextoLink();

            CargarDatos();
        }

        private void LinkExpandirContraer_Click(object sender, EventArgs e)
        {
            if (tabPane1.SelectedPageIndex == 0)   // CLQ y Documentos de Venta
            {
                _todosExpandidosClq = !_todosExpandidosClq;
                ExpandirTodasLasFilas(gridCLQ, _todosExpandidosClq);
            }
            else   // Gastos Aplicados
            {
                _todosExpandidosGastos = !_todosExpandidosGastos;
                ExpandirTodasLasFilas(gridGASTOS, _todosExpandidosGastos);
            }
            ActualizarTextoLink();
        }

        private void LinkGenerarTodos_Click(object sender, EventArgs e)
        {
            if (cbxEMPRESA.SelectedValue == null || cbxEMPRESA.SelectedValue == DBNull.Value)
            {
                XtraMessageBox.Show("Debe seleccionar una empresa antes de generar.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idEmpresa = Convert.ToInt32(cbxEMPRESA.SelectedValue);
            DateTime fecha = dteFECHA_LIQUIDACION.DateTime.Date;

            var respuesta = XtraMessageBox.Show(
                $"Se generarán todos los comprobantes de liquidación pendientes para " +
                $"{cbxEMPRESA.Text} en la fecha {fecha:dd/MM/yyyy}.\n\n¿Desea continuar?",
                "Confirmar generación masiva", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes) return;

            try
            {
                Cursor = Cursors.WaitCursor;

                var dt = _dal.EjecutarConsulta("DISTRIB.SP_LIQUIDACION_GENERAR_CLQ", new
                {
                    ACCION = "GENERAR_MASIVO",
                    ID_EMPRESA = idEmpresa,
                    FECHA_PROCESA = fecha,
                    USUARIO = Configuracion.UsuarioActual
                });

                int generados = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["CANTIDAD_GENERADOS"]) : 0;
                string errores = dt.Rows.Count > 0 ? dt.Rows[0]["ERRORES"]?.ToString() : "";

                string mensaje = $"Se generaron {generados} comprobante(s) correctamente.";
                if (!string.IsNullOrWhiteSpace(errores))
                    mensaje += $"\n\nOcurrieron errores en algunos documentos:\n{errores}";

                XtraMessageBox.Show(mensaje, "Generación masiva",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarDatos();   // recarga el grid para reflejar los nuevos documento_generado = 1
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al generar los comprobantes:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ActualizarTextoLink()
        {
            bool estadoActual = tabPane1.SelectedPageIndex == 0 ? _todosExpandidosClq : _todosExpandidosGastos;
            linkExpandirContraer.Text = estadoActual ? "Contraer todos" : "Expandir todos";
        }

        private void ExpandirTodasLasFilas(GridView view, bool expandir)
        {
            for (int i = 0; i < view.RowCount; i++)
                view.SetMasterRowExpanded(i, expandir);
        }

        // ============================================================
        // Niveles del grid maestro-detalle (se crean UNA sola vez)
        // ============================================================
        private void ConfigurarNivelesGridCLQ()
        {
            gridDocumentos = new GridView(gridControl1) { Name = "gridDocumentos" };
            gridProductos = new GridView(gridControl1) { Name = "gridProductos" };

            var nodoDocumentos = new GridLevelNode();
            nodoDocumentos.RelationName = "Documentos";
            nodoDocumentos.LevelTemplate = gridDocumentos;
            gridControl1.LevelTree.Nodes.Add(nodoDocumentos);

            var nodoProductos = new GridLevelNode();
            nodoProductos.RelationName = "Productos";
            nodoProductos.LevelTemplate = gridProductos;
            nodoDocumentos.Nodes.Add(nodoProductos);
        }

        private void ConfigurarNivelesGridGastos()
        {
            gridGastosDetalle = new GridView(gridControl2) { Name = "gridGastosDetalle" };
            var nodoDetalle = new GridLevelNode();
            nodoDetalle.RelationName = "GastosDetalle";
            nodoDetalle.LevelTemplate = gridGastosDetalle;
            gridControl2.LevelTree.Nodes.Add(nodoDetalle);
        }

        // ============================================================
        // Carga principal
        // ============================================================
        private void CargarDatos()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                _ds = _dal.EjecutarMultiple("DISTRIB.SP_DIZUCAR_SIMULACION", new
                {
                    ACCION = "LISTAR_GRID",
                    FECHA_PROCESA = dteFECHA_LIQUIDACION.DateTime.Date
                });

                _ds.Tables[0].TableName = "EMPRESA";
                _ds.Tables[1].TableName = "CLQ";
                _ds.Tables[2].TableName = "VENTA";
                _ds.Tables[3].TableName = "DETALLE";
                _dtGastos = _ds.Tables[4];
                _dtGastos.TableName = "GASTOS_DETALLE";   // ✅ renombrado (antes "GASTOS")

                _ds.Relations.Clear();

                _ds.Relations.Add("Documentos",
                    _ds.Tables["CLQ"].Columns["id_clq"],
                    _ds.Tables["VENTA"].Columns["id_clq"]);

                _ds.Relations.Add("Productos",
                    _ds.Tables["VENTA"].Columns["id_venta"],
                    _ds.Tables["DETALLE"].Columns["id_venta"]);

                // ============================================================
                // ✅ NUEVO: tabla resumen (maestro) para Gastos, agrupada en
                // memoria por empresa+concepto, sumando el total.
                // ============================================================
                var dtGastosGrupo = new DataTable("GASTOS_GRUPO");
                dtGastosGrupo.Columns.Add("id_empresa_prorrateo_gasto", typeof(int));
                dtGastosGrupo.Columns.Add("concepto_general", typeof(string));
                dtGastosGrupo.Columns.Add("total", typeof(decimal));

                var grupos = _dtGastos.AsEnumerable()
                    .GroupBy(r => new
                    {
                        Empresa = r.Field<int>("id_empresa_prorrateo_gasto"),
                        Concepto = r.Field<string>("concepto_general")
                    });

                foreach (var g in grupos)
                {
                    var fila = dtGastosGrupo.NewRow();
                    fila["id_empresa_prorrateo_gasto"] = g.Key.Empresa;
                    fila["concepto_general"] = g.Key.Concepto;
                    fila["total"] = g.Sum(r => ToDecimal(r["total"]));
                    dtGastosGrupo.Rows.Add(fila);
                }

                _ds.Tables.Add(dtGastosGrupo);

                _ds.Relations.Add("GastosDetalle",
                    new[] { dtGastosGrupo.Columns["id_empresa_prorrateo_gasto"], dtGastosGrupo.Columns["concepto_general"] },
                    new[] { _dtGastos.Columns["id_empresa_prorrateo_gasto"], _dtGastos.Columns["concepto_general"] });

                gridControl1.DataSource = _ds;
                gridControl1.DataMember = "CLQ";

                ConfigurarGridCLQ();
                ConfigurarGridDocumentos();
                ConfigurarGridProductos();

                gridControl2.DataSource = _ds;              // ✅ ya no es _dtGastos directo
                gridControl2.DataMember = "GASTOS_GRUPO";    // ✅ nuevo maestro

                ConfigurarGridGastos();
                ConfigurarGridGastosDetalle();               // ✅ NUEVO

                PoblarComboEmpresa(_ds.Tables["EMPRESA"]);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar la liquidación:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void PoblarComboEmpresa(DataTable dtEmpresa)
        {
            string seleccionPrevia = cbxEMPRESA.SelectedValue?.ToString();

            cbxEMPRESA.DataSource = dtEmpresa;
            cbxEMPRESA.ValueMember = "id_empresa";
            cbxEMPRESA.DisplayMember = "nom_empresa";

            if (dtEmpresa.Rows.Count == 0)
            {
                // Sin empresas con datos para esta fecha -> KPIs en cero
                ReiniciarKPIs();
                return;            }

            if (!string.IsNullOrEmpty(seleccionPrevia) &&
                dtEmpresa.AsEnumerable().Any(r => r["id_empresa"].ToString() == seleccionPrevia))
            {
                cbxEMPRESA.SelectedValue = Convert.ToInt32(seleccionPrevia);
            }
            else
            {
                cbxEMPRESA.SelectedIndex = 0;
            }

            AplicarFiltroEmpresa();
        }

        private void ReiniciarKPIs()
        {
            lblTOTAL_VENTAS.Text = 0m.ToString("C2");
            lblTOTAL_CREDITO.Text = 0m.ToString("C2");
            lblTOTAL_GASTOS.Text = 0m.ToString("C2");
            lblVENTAS_MENOS_GASTOS.Text = 0m.ToString("C2");
        }

        private void AplicarFiltroEmpresa()
        {
            if (cbxEMPRESA.SelectedValue == null || _ds == null) return;

            int idEmpresa = Convert.ToInt32(cbxEMPRESA.SelectedValue);

            gridCLQ.ActiveFilterString = $"[id_empresa] = {idEmpresa}";
            gridGASTOS.ActiveFilterString = $"[id_empresa_prorrateo_gasto] = {idEmpresa}";

            ActualizarKPIs(idEmpresa);
        }

        // ============================================================
        // KPIs
        // ============================================================
        private void ActualizarKPIs(int idEmpresa)
        {
            decimal totalVentas = 0;
            decimal totalCredito = 0;

            foreach (DataRow clqRow in _ds.Tables["CLQ"].Select($"id_empresa = {idEmpresa}"))
            {
                foreach (DataRow ventaRow in clqRow.GetChildRows("Documentos"))
                {
                    decimal total = ToDecimal(ventaRow["total"]);
                    totalVentas += total;

                    string condicion = ventaRow["condicion_de_pago"]?.ToString() ?? "";
                    if (condicion.ToUpper().Contains("CREDITO"))
                        totalCredito += total;
                }
            }

            decimal totalGastos = 0;
            foreach (DataRow gastoRow in _dtGastos.Select($"id_empresa_prorrateo_gasto = {idEmpresa}"))
                totalGastos += ToDecimal(gastoRow["total"]);

            lblTOTAL_VENTAS.Text = totalVentas.ToString("C2");
            lblTOTAL_CREDITO.Text = totalCredito.ToString("C2");
            lblTOTAL_GASTOS.Text = totalGastos.ToString("C2");
            lblVENTAS_MENOS_GASTOS.Text = (totalVentas - totalGastos).ToString("C2");
        }

        private static decimal ToDecimal(object valor)
            => valor == null || valor == DBNull.Value ? 0 : Convert.ToDecimal(valor);

        // ============================================================
        // Nivel CLQ
        // ============================================================
        private void ConfigurarGridCLQ()
        {
            ConfigurarEstiloBase(gridCLQ);
            gridCLQ.OptionsView.ShowIndicator = false;
            gridCLQ.OptionsFind.AlwaysVisible = true;
            gridCLQ.Appearance.Row.Font = new Font(gridCLQ.Appearance.Row.Font, FontStyle.Bold);
            gridCLQ.Appearance.Row.Options.UseFont = true;

            gridCLQ.Appearance.HeaderPanel.Font = new Font(gridCLQ.Appearance.HeaderPanel.Font, FontStyle.Bold);
            gridCLQ.Appearance.HeaderPanel.Options.UseFont = true;

            OcultarColumna(gridCLQ, "id_clq");
            OcultarColumna(gridCLQ, "id_empresa");

            RenombrarColumna(gridCLQ, "numliq", "N° CLQ");
            RenombrarColumna(gridCLQ, "serieclq", "Serie");
            RenombrarColumna(gridCLQ, "fecha", "Fecha");

            if (gridCLQ.Columns["numliq"] != null) gridCLQ.Columns["numliq"].Width = 100;
            if (gridCLQ.Columns["serieclq"] != null) gridCLQ.Columns["serieclq"].Width = 130;
            if (gridCLQ.Columns["fecha"] != null) gridCLQ.Columns["fecha"].Width = 380;   
           
            var colGenerado = AgregarColumna(gridCLQ, "documento_generado", "Generado");
            colGenerado.Width = 80;
            colGenerado.VisibleIndex = 3;
            colGenerado.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
            {
                ReadOnly = true
            };
            colGenerado.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colGenerado.AppearanceCell.Options.UseTextOptions = true;
            colGenerado.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colGenerado.AppearanceHeader.Options.UseTextOptions = true;

            var colSacos = AgregarColumnaNumero(gridCLQ, "total_sacos", "Sacos");
            colSacos.Width = 80;
            colSacos.VisibleIndex = 4;

            AgregarColumnaCalculada(gridCLQ, "SUBTOTAL_CLQ", "Subtotal").Width = 150;
            AgregarColumnaCalculada(gridCLQ, "IVA_CLQ", "IVA").Width = 150;
            AgregarColumnaCalculada(gridCLQ, "RETENCION_CLQ", "Retención").Width = 150;
            AgregarColumnaCalculada(gridCLQ, "TOTAL_CLQ", "Total").Width = 150;

            gridCLQ.OptionsBehavior.Editable = true;
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridCLQ.Columns)
                col.OptionsColumn.AllowEdit = (col.FieldName == "accion_generar");

            gridCLQ.CustomUnboundColumnData -= GridCLQ_CustomUnboundColumnData;
            gridCLQ.CustomUnboundColumnData += GridCLQ_CustomUnboundColumnData;

            ConfigurarBotonGenerarGrid();
        }

        private void ConfigurarBotonGenerarGrid()
        {
            // Botón de Generar/Regenerar comprobante de liquidación
            _riBotonGenerar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            _riBotonGenerar.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            _riBotonGenerar.Buttons.Clear();

            var boton = new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
            {
                Image = ObtenerImagenRecurso("descargar20x20")
            };
            var superTip = new DevExpress.Utils.SuperToolTip();
            superTip.Items.Add("Generar comprobante de liquidación (CLQ)");
            boton.SuperTip = superTip;
            _riBotonGenerar.Buttons.Add(boton);
            _riBotonGenerar.ButtonClick += RiBotonGenerar_ButtonClick;

            gridControl1.RepositoryItems.Add(_riBotonGenerar);

            var colGenerar = AgregarColumna(gridCLQ, " ", "");
            colGenerar.Width = 40;
            colGenerar.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            colGenerar.ColumnEdit = _riBotonGenerar;
            colGenerar.OptionsColumn.AllowEdit = true;   // el botón necesita que la celda sea "editable" para responder al clic
        }

        private Image ObtenerImagenRecurso(string nombre)
        {
            object recurso = Properties.Resources.ResourceManager.GetObject(nombre)
                              ?? SistemaContable.UI.RecursosAdicionales01.ResourceManager.GetObject(nombre);
            return recurso as Image;
        }

        private void RiBotonGenerar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int rowHandle = gridCLQ.FocusedRowHandle;
            if (rowHandle < 0) return;

            int idClq = Convert.ToInt32(gridCLQ.GetRowCellValue(rowHandle, "id_clq"));
            bool yaGenerado = gridCLQ.GetRowCellValue(rowHandle, "documento_generado") != DBNull.Value
                               && Convert.ToBoolean(gridCLQ.GetRowCellValue(rowHandle, "documento_generado"));

            if (yaGenerado)
            {
                var respuesta = XtraMessageBox.Show(
                    "Este CLQ ya tiene un comprobante generado. ¿Desea regenerarlo? " +
                    "El documento existente se eliminará y se creará uno nuevo.",
                    "Confirmar regeneración", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes) return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                var dt = _dal.EjecutarConsulta("DISTRIB.SP_LIQUIDACION_GENERAR_CLQ", new
                {
                    ACCION = "GENERAR",
                    ID_CLQ = idClq,
                    USUARIO = Configuracion.UsuarioActual
                });

                XtraMessageBox.Show("Comprobante generado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarDatos();   // recarga el grid para reflejar documento_generado = 1
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al generar el comprobante:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }


        private void GridCLQ_CustomUnboundColumnData(object sender,
            DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (!e.IsGetData) return;
            if (!(e.Row is DataRowView drv)) return;

            string campo = e.Column.FieldName;
            bool esColumnaCLQ = campo == "SUBTOTAL_CLQ" || campo == "IVA_CLQ"
                              || campo == "RETENCION_CLQ" || campo == "TOTAL_CLQ";
            if (!esColumnaCLQ) return;

            decimal subtotal = 0, iva = 0, retencion = 0, total = 0;
            foreach (DataRow ventaRow in drv.Row.GetChildRows("Documentos"))
            {
                subtotal += ToDecimal(ventaRow["subtotal"]);
                iva += ToDecimal(ventaRow["iva"]);
                retencion += ToDecimal(ventaRow["retencion"]);
                total += ToDecimal(ventaRow["total"]);
            }

            switch (campo)
            {
                case "SUBTOTAL_CLQ": e.Value = subtotal; break;
                case "IVA_CLQ": e.Value = iva; break;
                case "RETENCION_CLQ": e.Value = retencion; break;
                case "TOTAL_CLQ": e.Value = total; break;
            }
        }

        // ============================================================
        // Nivel Documentos (ventas)
        // ============================================================
        private void ConfigurarGridDocumentos()
        {
            ConfigurarEstiloBase(gridDocumentos);
            gridDocumentos.OptionsView.ShowIndicator = false;
            gridDocumentos.OptionsBehavior.AutoPopulateColumns = false;
            gridDocumentos.Columns.Clear();

            AgregarColumnaTexto(gridDocumentos, "dte", "DTE").Width = 220;
            AgregarColumnaTexto(gridDocumentos, "nombrecliente", "Cliente").Width = 300;
            AgregarColumnaTexto(gridDocumentos, "tipo_dte", "Tipo").Width = 70;
            AgregarColumnaTexto(gridDocumentos, "condicion_de_pago", "Condición").Width = 200;

            AgregarColumnaMoneda(gridDocumentos, "subtotal", "Subtotal").Width = 150;
            AgregarColumnaMoneda(gridDocumentos, "iva", "IVA").Width = 150;
            AgregarColumnaMoneda(gridDocumentos, "retencion", "Retención").Width = 150;
            AgregarColumnaMoneda(gridDocumentos, "total", "Total").Width = 150;

            gridDocumentos.RowStyle -= GridDocumentos_RowStyle;
            gridDocumentos.RowStyle += GridDocumentos_RowStyle;
        }

        // ============================================================
        // Nivel Productos (detalle)
        // ============================================================
        private void ConfigurarGridProductos()
        {
            ConfigurarEstiloBase(gridProductos);
            OcultarIndicadorSinHijos(gridProductos);
            gridProductos.OptionsBehavior.AutoPopulateColumns = false;
            gridProductos.Columns.Clear();

            AgregarColumnaTexto(gridProductos, "descripcionproducto", "Producto").Width = 230;
            AgregarColumnaNumero(gridProductos, "cantidad", "Cantidad").Width = 50;
            AgregarColumnaNumero(gridProductos, "sacos50", "Sacos").Width = 50;
            AgregarColumnaMoneda(gridProductos, "precio", "Precio").Width = 50;
            AgregarColumnaMoneda(gridProductos, "subtotal", "Subtotal").Width = 80;
            AgregarColumnaMoneda(gridProductos, "iva", "IVA").Width = 80;
            AgregarColumnaMoneda(gridProductos, "total", "Total").Width = 150;
        }

        private void GridDocumentos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view == null || e.RowHandle < 0) return;

            string condicion = view.GetRowCellValue(e.RowHandle, "condicion_de_pago")?.ToString() ?? "";

            if (condicion.ToUpper().Contains("CREDITO"))
            {
                e.Appearance.BackColor = Color.FromArgb(253, 226, 226);   // rojo pálido
                e.Appearance.Options.UseBackColor = true;
            }
        }

        private void ConfigurarEstiloBase(GridView view)
        {
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsBehavior.Editable = false;
            view.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            view.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            
            view.Appearance.Row.Font = new Font("Segoe UI", 9f);
            view.Appearance.Row.Options.UseFont = true;
            view.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9f);
            view.Appearance.HeaderPanel.Options.UseFont = true;

            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.OptionsSelection.EnableAppearanceFocusedRow = true;

            view.Appearance.FocusedRow.BackColor = Color.FromArgb(204, 229, 255);
            view.Appearance.FocusedRow.ForeColor = Color.Black;
            view.Appearance.FocusedRow.Options.UseBackColor = true;
            view.Appearance.FocusedRow.Options.UseForeColor = true;

            // Mantiene el color aunque el grid pierda el foco (al hacer clic en otro control)
            view.Appearance.HideSelectionRow.BackColor = Color.FromArgb(204, 229, 255);
            view.Appearance.HideSelectionRow.ForeColor = Color.Black;
            view.Appearance.HideSelectionRow.Options.UseBackColor = true;
            view.Appearance.HideSelectionRow.Options.UseForeColor = true;
        }

        // Se aplica solo al nivel más profundo (Productos), que no tiene hijos
        // y por eso no pierde ninguna funcionalidad al ocultar el indicador.
        private void OcultarIndicadorSinHijos(GridView view)
        {
            view.OptionsView.ShowIndicator = false;
        }

        // ============================================================
        // Grid de gastos (plano, agrupado por concepto)
        // ============================================================
        private void ConfigurarGridGastos()
        {
            ConfigurarEstiloBase(gridGASTOS);
            gridGASTOS.OptionsBehavior.AutoPopulateColumns = false;
            gridGASTOS.OptionsView.ShowIndicator = false;
            gridGASTOS.Columns.Clear();

            gridGASTOS.Appearance.Row.Font = new Font(gridGASTOS.Appearance.Row.Font, FontStyle.Bold);
            gridGASTOS.Appearance.Row.Options.UseFont = true;

            gridGASTOS.Appearance.HeaderPanel.Font = new Font(gridGASTOS.Appearance.HeaderPanel.Font, FontStyle.Bold);
            gridGASTOS.Appearance.HeaderPanel.Options.UseFont = true;

            var colEmpresaProrrateo = AgregarColumna(gridGASTOS, "id_empresa_prorrateo_gasto", "");
            colEmpresaProrrateo.Visible = false;

            AgregarColumnaTexto(gridGASTOS, "concepto_general", "Concepto").Width = 900;
            AgregarColumnaMoneda(gridGASTOS, "total", "Total").Width = 150;
        }

        private void ConfigurarGridGastosDetalle()
        {
            ConfigurarEstiloBase(gridGastosDetalle);
            OcultarIndicadorSinHijos(gridGastosDetalle);
            gridGastosDetalle.OptionsBehavior.AutoPopulateColumns = false;
            gridGastosDetalle.Columns.Clear();

            AgregarColumnaTexto(gridGastosDetalle, "dte_gasto", "DTE").Width = 220;
            AgregarColumnaTexto(gridGastosDetalle, "codgeneracion_gasto", "Cód. Generación").Width = 250;
            AgregarColumnaTexto(gridGastosDetalle, "nom_empresa_gasto", "Empresa").Width = 110;

            AgregarColumnaMoneda(gridGastosDetalle, "subtotal", "Subtotal").Width = 150;
            AgregarColumnaMoneda(gridGastosDetalle, "iva", "IVA").Width = 150;
            AgregarColumnaMoneda(gridGastosDetalle, "retencion", "Retención").Width = 150;
            AgregarColumnaMoneda(gridGastosDetalle, "total", "Total").Width = 150;
        }


        // ============================================================
        // Helpers compartidos
        // ============================================================      

        private DevExpress.XtraGrid.Columns.GridColumn AgregarColumna(GridView view, string field, string caption)
        {
            var col = view.Columns[field] ?? view.Columns.AddField(field);   
            col.Caption = caption;
            col.Visible = true;
            col.VisibleIndex = view.Columns.Count - 1;
            col.OptionsColumn.AllowEdit = false;
            return col;
        }

        private DevExpress.XtraGrid.Columns.GridColumn AgregarColumnaTexto(GridView view, string field, string caption)
        {
            return AgregarColumna(view, field, caption);  
        }

        private DevExpress.XtraGrid.Columns.GridColumn AgregarColumnaMoneda(GridView view, string field, string caption)
        {
            var col = AgregarColumna(view, field, caption);
            col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            col.DisplayFormat.FormatString = "c2";
            col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            col.AppearanceCell.Options.UseTextOptions = true;
            col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            col.AppearanceHeader.Options.UseTextOptions = true;
            return col;  
        }

        private DevExpress.XtraGrid.Columns.GridColumn AgregarColumnaNumero(GridView view, string field, string caption)
        {
            var col = AgregarColumna(view, field, caption);
            col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            col.DisplayFormat.FormatString = "n2";
            col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            col.AppearanceCell.Options.UseTextOptions = true;
            col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            col.AppearanceHeader.Options.UseTextOptions = true;
            return col;   
        }

        private void OcultarColumna(GridView view, string nombreCampo)
        {
            if (view.Columns[nombreCampo] != null)
                view.Columns[nombreCampo].Visible = false;
        }

        private void RenombrarColumna(GridView view, string nombreCampo, string caption)
        {
            if (view.Columns[nombreCampo] != null)
                view.Columns[nombreCampo].Caption = caption;
        }

        private DevExpress.XtraGrid.Columns.GridColumn AgregarColumnaCalculada(GridView view, string nombreCampo, string caption)
        {
            if (view.Columns[nombreCampo] != null) return view.Columns[nombreCampo];

            var col = view.Columns.AddField(nombreCampo);
            col.Caption = caption;
            col.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            col.DisplayFormat.FormatString = "c2";
            col.OptionsColumn.AllowEdit = false;
            col.Visible = true;
            col.VisibleIndex = view.Columns.Count - 1;
            col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            col.AppearanceCell.Options.UseTextOptions = true;

            col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            col.AppearanceHeader.Options.UseTextOptions = true;
            return col;   
        }

    }
}
