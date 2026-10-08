using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.SqlServer.Server;
using SistemaContable.DAL;
using SistemaContable.RP.Partidas;
using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Contabilidad
{

    public partial class frmComprobantes : DevExpress.XtraEditors.XtraForm
    {
        private readonly DALBase _dal = new DALBase();
        public int IdComprobante { get; set; } = 0;
        private DataTable _dtPartida;  // DataTable que alimenta el grid
        private string _columnaAnteriorGrid = string.Empty;
        private bool _asignandoCuentaPorCodigo = false;

        private DataTable _dtPartidas = new DataTable();
        private int _indiceActual = -1;

        private EstadoFormulario _estadoActual = EstadoFormulario.Inicializar;
        
        private bool _modoBusqueda = false;
        private bool _cargandoPartida = false;

        private enum EstadoFormulario
        {
            Inicializar,
            Pendiente,
            Nuevo,
            Buscar,
            Supender,
            Imprimir,
            Modificar,
            Eliminar,
            Grabar,
            Ignorar,
            Success
        }
        private void HabilitarBotones(
            bool nuevo,
            bool buscar,
            bool supender,
            bool imprimir,
            bool anterior,
            bool siguiente,
            bool modificar,
            bool eliminar,
            bool grabar,
            bool ignorar)
        {
            
            btBuscar.Enabled = buscar;
            btSuspender.Enabled = supender;
            btImprimir.Enabled = imprimir;
            btAnterior.Enabled = anterior;
            btSiguiente.Enabled = siguiente;
            btModificar.Enabled = modificar;
            btEliminar.Enabled = eliminar;
            btGrabar.Enabled = grabar;
            btIgnorar.Enabled = ignorar;
            btNuevo.Enabled = nuevo;
        }
        private void ConfigurarCRUD(EstadoFormulario estado, bool chequeAnulado = false)
        {
            _estadoActual = estado;
            switch (estado)
            {
                case EstadoFormulario.Inicializar:
                    _modoBusqueda = false;
                    HabilitarBotones(
                         nuevo : true,
                         buscar : true,
                         supender : false,
                         imprimir : false,
                         anterior : false,
                         siguiente : false,
                         modificar : false,
                         eliminar :false,
                         grabar : false,
                         ignorar : false  );
                    HabilitarControlesEncabezado(false);
                    HabilitarControlesPartida(false);
                    break;

                case EstadoFormulario.Pendiente:
                    _modoBusqueda = false;
                    HabilitarBotones(
                        nuevo: false,
                         buscar: false,
                         supender: false,
                         imprimir: false,
                         anterior: false,
                         siguiente: false,
                         modificar: false,
                         eliminar: false,
                         grabar: false,
                         ignorar: false);
                    HabilitarControlesEncabezado(false);
                    HabilitarControlesPartida(false);
                    break;

                case EstadoFormulario.Nuevo:
                    _modoBusqueda = false;
                    HabilitarBotones(
                         nuevo: false,
                         buscar: true,
                         supender: false,
                         imprimir: false,
                         anterior: false,
                         siguiente: false,
                         modificar: false,
                         eliminar: false,
                         grabar: true,
                         ignorar: true);
                    HabilitarControlesEncabezado(true);
                    HabilitarControlesPartida(true);
                    break;

                case EstadoFormulario.Buscar:
                    _modoBusqueda = true;
                    HabilitarBotones(
                        nuevo: true,
                         buscar: true,
                         supender: false,
                         imprimir: false,
                         anterior: true,
                         siguiente: true,
                         modificar: true,
                         eliminar: false,
                         grabar: false,
                         ignorar: false);
                    HabilitarControlesEncabezado(false);
                    HabilitarControlesPartida(false);
                    HabilitarControlesBusqueda(true);
                    break;

                case EstadoFormulario.Modificar:
                    _modoBusqueda = true;
                    HabilitarBotones(
                        nuevo: false,
                         buscar: false,
                         supender: false,
                         imprimir: true,
                         anterior: false,
                         siguiente: false,
                         modificar: false,
                         eliminar: true,
                         grabar: true,
                         ignorar: true);
                    HabilitarControlesEncabezado(true);
                    HabilitarControlesPartida(true);
                    HabilitarControlesBusqueda(false);
                    break;

                case EstadoFormulario.Grabar:
                    _modoBusqueda = false;

                    HabilitarBotones(
                         nuevo: true,
                         buscar: false,
                         supender: false,
                         imprimir: true,
                         anterior: false,
                         siguiente: false,
                         modificar: false,
                         eliminar: false,
                         grabar: false,
                         ignorar: false);
                    HabilitarControlesEncabezado(false);
                    HabilitarControlesPartida(false);
                    break;


                case EstadoFormulario.Success:
                    _modoBusqueda = false;

                    HabilitarBotones(
                         nuevo: true,
                         buscar: false,
                         supender: false,
                         imprimir: true,
                         anterior: false,
                         siguiente: false,
                         modificar: false,
                         eliminar: false,
                         grabar: false,
                         ignorar: false);
                    HabilitarControlesEncabezado(false);
                    HabilitarControlesPartida(false);
                    break;
            }
        }




        // ============================================================
        // Habilita/deshabilita los controles del encabezado del cheque
        // ============================================================
        //private void HabilitarControlesEncabezado(bool habilitar)
        //{

        //    txtTipo.Enabled = habilitar;   // tipo de partida
        //    txtConcepto.Enabled = habilitar;
        //    txtNumero.Enabled = habilitar;
        //    deFecha.Enabled = habilitar;            
        //}


        private void HabilitarControlesEncabezado(bool habilitar)
        {
            txtTipo.ReadOnly = !habilitar;
            txtConcepto.ReadOnly = !habilitar;
            txtNumero.ReadOnly = !habilitar;
            deFecha.Properties.ReadOnly = !habilitar;

            Color backColor = habilitar ? Color.White : Color.WhiteSmoke;

            txtTipo.BackColor = backColor;
            txtConcepto.BackColor = backColor;
            txtNumero.BackColor = backColor;
            deFecha.BackColor = backColor;
        }
        private void HabilitarControlesBusqueda(bool habilitar)
        {
            txtTipo.ReadOnly = !habilitar;
            txtNumero.ReadOnly = !habilitar;
            Color backColor = habilitar ? Color.White : Color.WhiteSmoke;

            txtTipo.BackColor = backColor;           
            txtNumero.BackColor = backColor;
        }
        // ============================================================
        // Habilita/deshabilita la grilla de partida contable
        // ============================================================
        private void HabilitarControlesPartida(bool habilitar)
        {
            gridView1.OptionsBehavior.Editable = habilitar;
            gridView1.OptionsBehavior.ReadOnly = !habilitar;

            gridView1.Appearance.Row.BackColor = habilitar
                ? Color.White
                : Color.FromArgb(245, 245, 245);

            gridView1.Appearance.FocusedRow.BackColor = habilitar
                ? Color.FromArgb(255, 255, 192)
                : Color.FromArgb(245, 245, 245);
        }
       
        private void LimpiarControles()
        {
              IdComprobante  = 0;
            _dtPartida.Clear();
         _columnaAnteriorGrid = string.Empty;
         _asignandoCuentaPorCodigo = false;
        _dtPartidas.Clear();
         _indiceActual = -1;
       _modoBusqueda = false;
         _cargandoPartida = false;
            txtId.Text = string.Empty;
            txtNID_PARTIDA.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtTipo.Text = string.Empty;
            txtTipoNombre.Text = string.Empty;
            txtConcepto.Text = string.Empty;
            deFecha.Text = string.Empty;
            lblTOTAL_CARGO.Text = "0";
            lblTOTAL_ABONO.Text = "0";
            lblDIFERENCIA.Text = "0";
            ConfigurarCRUD(EstadoFormulario.Inicializar);
        }
        private void ConfigurarToolTips()
        {
            TooltipHelper.Configurar(
                            (txtTipo, "Ingrese * y presione Enter para mostrar todos los tipos de comprobantes."),
                            (txtNumero, "Ingrese * y presione Enter para mostrar todos los comprobantes relacionado al tipo.")

                                 );
        }
        public frmComprobantes()
        {
            InitializeComponent();
            ConfigurarToolTips();
        }
        private void frmComprobantes_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);
            lbMesAnio.Text = Configuracion.PeriodoDescripcion;
            _modoBusqueda = false;


            if (int.TryParse(Configuracion.PeriodoAnio.ToString(), out int anio) &&
            int.TryParse(Configuracion.PeriodoMes.ToString(), out int mes))
            {
                DateTime primerDia = new DateTime(anio, mes, 1);
                DateTime ultimoDia = primerDia.AddMonths(1).AddDays(-1);
                deFecha.Properties.MinValue = primerDia;
                deFecha.Properties.MaxValue = ultimoDia;
            }



            InicializarGridPartida();

            // Búsqueda * + Enter en txtPROVEEDOR
            FormHelper.RegistrarBusqueda(
                txtTipo,
                new BusquedaConfig
                {
                    StoredProcedure = "[CONTA].SP_TIPO_PARTIDA",
                    Accion = "BUSCAR",
                    Columnas = new Dictionary<string, string>
                    {
                        { "TIPO_PARTIDA", "Tipo" },
                        { "DESCRIPCION",         "Nombre"    }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "TIPO_PARTIDA", 100 },
                        { "DESCRIPCION",         300 }
                    }

                },
                fila => AsignarTipo(fila)
            );
           this.txtTipo.Leave += txtTipo_Leave;

            FormHelper.RegistrarBusqueda(
                txtNumero,
                new BusquedaConfig
                {
                    StoredProcedure = "[CONTA].SP_BUSCAR_PARTIDA",
                    Accion = "BUSCAR",

                    Columnas = new Dictionary<string, string>
                    {
                        { "TIPO_PARTIDA", "Tipo" },
                        { "NUM_PARTIDA", "Numero" },
                        { "FECHA_PARTIDA", "Fecha" },
                        { "CONCEPTO", "Concepto" },
                         { "TOTAL_CARGO", "Cargo" },
                          { "TOTAL_ABONO", "Abono" }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "TIPO_PARTIDA", 50 },
                        { "FECHA_PARTIDA",  50 },
                        { "CONCEPTO", 250 },
                         { "TOTAL_CARGO", 50 },
                          { "TOTAL_ABONO", 50 }
                    },

                    ObtenerParametrosExtra = () => new
                    {
                        TIPO_PARTIDA = txtTipo.Text,
                        ANIO = Configuracion.PeriodoAnio,
                        MES = Configuracion.PeriodoMes
                    }

                },
                fila => AsignarNumero(fila)
            );
            this.txtNumero.Leave += txtNumero_Leave;

            ConfigurarCRUD(EstadoFormulario.Inicializar);
        }
        private void CargarListaPartidas()
        {
            _dtPartidas = _dal.EjecutarConsulta("[CONTA].SP_BUSCAR_PARTIDA", new
            {
                ACCION = "LISTA",
                TIPO_PARTIDA = txtTipo.Text,
                ANIO = Configuracion.PeriodoAnio,
                MES = Configuracion.PeriodoMes
            });

            _indiceActual = -1;

            if (_dtPartidas != null && _dtPartidas.Rows.Count > 0)
            {
                string idActual = txtId.Text;

                for (int i = 0; i < _dtPartidas.Rows.Count; i++)
                {
                    if (_dtPartidas.Rows[i]["ID_PARTIDA"].ToString() == idActual)
                    {
                        _indiceActual = i;
                        break;
                    }
                }
            }

            ActualizarBotones();
            FormHelper.OcultarMensajeRibbon(this);
        }
        private void CargarEnca(int ID_PARTIDA)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                DataTable dt = _dal.EjecutarConsulta("[CONTA].VIEW_PARATIDA_ENC",
                    new
                    {
                        ACCION = "OBTENER_ENCA",
                        ID_PARTIDA = ID_PARTIDA
                    });

                if (dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No se encontró el documento solicitado.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    return;
                }

                DataRow r = dt.Rows[0];

                
                txtId.Text = r["ID_PARTIDA"]?.ToString();
                txtNID_PARTIDA.Text = r["NID_PARTIDA"]?.ToString();
                txtNumero.Text = r["NUM_PARTIDA"]?.ToString().PadLeft(6, '0');
                txtTipo.Text = r["TIPO_PARTIDA"]?.ToString();
                txtTipoNombre.Text = r["DESCRIPCION"]?.ToString();
                txtConcepto.Text = r["CONCEPTO"]?.ToString();

                if (r["FECHA_PARTIDA"] != DBNull.Value)
                {
                    deFecha.Text = Convert.ToDateTime(r["FECHA_PARTIDA"])
                        .ToString("dd/MM/yyyy");
                }
                lblTOTAL_CARGO.Text = r["TOTAL_CARGO"]?.ToString();
                lblTOTAL_ABONO.Text = r["TOTAL_ABONO"]?.ToString();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar el documento:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;

                if (!string.IsNullOrWhiteSpace(txtId.Text) &&
                    int.TryParse(txtId.Text, out int id))
                {
                    CargarDetalle(id);
                }

            }
        }
        private void CargarDetalle(int ID_PARTIDA)
        {
            try
            {
                DataTable dt2 = _dal.EjecutarConsulta("[CONTA].VIEW_DATOS_PARTIDA",
                    new
                    {
                        ACCION = "OBTENER_DETA",
                        ID_PARTIDA = ID_PARTIDA
                    });

                if (dt2.Rows.Count > 0)
                {
                    _dtPartida.Rows.Clear();

                    foreach (DataRow row in dt2.Rows)
                    {
                        DataRow nueva = _dtPartida.NewRow();

                        nueva["LINEA"] = row["LINEA"];
                        nueva["CUENTA"] = row["CUENTA"];
                        nueva["CONCEPTO"] = row["CONCEPTO"];
                        nueva["CARGO"] = row["CARGO"];
                        nueva["ABONO"] = row["ABONO"];

                        _dtPartida.Rows.Add(nueva);
                    }
                }

                //if (string.IsNullOrWhiteSpace(txtKilogramos.Text))
                //{
                //    AgregarFilaVacia();
                //}

                //var view = gridControl1.MainView as GridView;
                //view.Columns["ELIMINAR"].OptionsColumn.AllowEdit =
                //    string.IsNullOrWhiteSpace(txtSELLO_RECIBIDO.Text);


                var view = gridControl1.MainView as GridView;

                //bool editable = string.IsNullOrWhiteSpace(txtKilogramos.Text);

                //view.OptionsBehavior.Editable = editable;
                //view.OptionsBehavior.ReadOnly = !editable;

                //view.Appearance.Row.BackColor = editable
                //                    ? Color.White
                //                    : Color.LightGray;


                gridControl1.RefreshDataSource();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar el documento:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                FormHelper.OcultarMensajeRibbon(this);
            }
        }
        private void txtTipo_Leave(object sender, EventArgs e)
        {
            string codigo = txtTipo.Text.Trim();
            if (codigo == "*") return;
            if (string.IsNullOrWhiteSpace(codigo))
            {
                LimpiarTipo();
                return;
            }
            try
            {
                var dt = _dal.EjecutarConsulta("[CONTA].SP_TIPO_PARTIDA", new
                {
                    ACCION = "BUSCAR_TIPO",
                    FILTRO = codigo
                });

                if (dt.Rows.Count > 0)
                    AsignarTipo(dt.Rows[0]);
                else
                {
                    LimpiarTipo();
                    Alertas.Error($"No se encontró el tipo de comprobante con código '{codigo}'.");
                    txtTipo.Focus();
                }
            }
            catch (Exception ex)
            {
                Alertas.Error($"Error al buscar tipo de comprobante: {ex.Message}");
            }
        }
        private void AsignarTipo(DataRow fila)
        {
            txtTipo.Text = fila["TIPO_PARTIDA"].ToString();
            txtTipoNombre.Text = fila["DESCRIPCION"].ToString();

            if (_modoBusqueda)
                return;

            if (string.IsNullOrWhiteSpace(deFecha.Text))
            {
                deFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                deFecha_Leave(null, null);
            }
        }
        private void LimpiarTipo()
        {
            txtTipo.Text = string.Empty;
            txtTipoNombre.Text = string.Empty;
        }

        private void deFecha_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTipo.Text))
                return;
            if (deFecha.EditValue == null)
                return;

            var infoPartida = NumeradorPartidaHelper.Consultar(txtTipo.Text, Convert.ToDateTime(deFecha.Text));
            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            { txtNumero.Text = infoPartida.NumSiguienteFormateado; }
        }
        private void deFecha_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTipo.Text))
                return;
            if (deFecha.EditValue == null)
                return;

            var infoPartida = NumeradorPartidaHelper.Consultar(txtTipo.Text, Convert.ToDateTime(deFecha.Text));
            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            { txtNumero.Text = infoPartida.NumSiguienteFormateado; }
        }

        private void deFecha_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTipo.Text))
            {
                Alertas.Advertencia("Seleccione un tipo de comprobante.");
                              return;
            }

            if (deFecha.EditValue == null)
                return;

            DateTime fecha = deFecha.DateTime;

            DateTime periodo = new DateTime(
                                            Configuracion.PeriodoAnio,
                                            Configuracion.PeriodoMes,
                                            1);



            if (fecha.Month != periodo.Month ||
                fecha.Year != periodo.Year)
            {
                Alertas.Advertencia(
                    $"Solo se permiten fechas del período {periodo:MM/yyyy}.");

                deFecha.EditValue = null;
                deFecha.Focus();
                return;
            }

            var infoPartida = NumeradorPartidaHelper.Consultar(txtTipo.Text, fecha);

            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            { txtNumero.Text = infoPartida.NumSiguienteFormateado; }

        }
        private void txtNumero_Leave(object sender, EventArgs e)
        {
            if (_cargandoPartida)
                return;

            if (string.IsNullOrWhiteSpace(txtTipo.Text))
            {
                Alertas.Advertencia("Seleccione un tipo de comprobante.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNumero.Text))
                return;

            string numero = txtNumero.Text.Trim();

            // Si no está habilitada la búsqueda, no permitir *
            if (!_modoBusqueda && numero == "*")
            {
                txtNumero.Text = string.Empty;
                return;
            }

            // Permitir únicamente * o números
            if (numero != "*" && !numero.All(char.IsDigit))
            {
                Alertas.Error("Solo se permiten números o '*'.");
                txtNumero.Focus();
                txtNumero.SelectAll();
                return;
            }

            // Si es *
            if (numero == "*")
                return;

            if (numero.Length > 6)
            {
                Alertas.Error("El número no puede tener más de 6 dígitos.");
                txtNumero.Focus();
                txtNumero.SelectAll();
                return;
            }

            txtNumero.Text = numero.PadLeft(6, '0');

            // Si no está en modo búsqueda, solo formatea
            if (!_modoBusqueda)
                return;

            try
            {
                var dt = _dal.EjecutarConsulta("[CONTA].SP_BUSCAR_PARTIDA", new
                {
                    ACCION = "BUSCAR_N",
                    TIPO_PARTIDA = txtTipo.Text,
                    ANIO = Configuracion.PeriodoAnio,
                    MES = Configuracion.PeriodoMes,
                    FILTRO = txtNumero.Text
                });

                if (dt != null && dt.Rows.Count > 0)
                {
                    AsignarNumero(dt.Rows[0]);
                }
                else
                {
                    Alertas.Error($"No se encontró el comprobante N° '{txtNumero.Text}'.");
                    txtNumero.Focus();
                    txtNumero.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Alertas.Error($"Error al buscar el comprobante: {ex.Message}");
            }
        }

        private void AsignarNumero(DataRow fila)
        {
            _cargandoPartida = true;

            try
            {
                txtId.Text = fila["ID_PARTIDA"]?.ToString();
                txtNID_PARTIDA.Text = fila["NID_PARTIDA"]?.ToString();
                txtNumero.Text = fila["NUM_PARTIDA"]?.ToString().PadLeft(6, '0');
                txtTipo.Text = fila["TIPO_PARTIDA"]?.ToString();
                txtTipoNombre.Text = fila["DESCRIPCION"]?.ToString();
                txtConcepto.Text = fila["CONCEPTO"]?.ToString();

                if (fila["FECHA_PARTIDA"] != DBNull.Value)
                {
                    deFecha.Text = Convert.ToDateTime(fila["FECHA_PARTIDA"])
                        .ToString("dd/MM/yyyy");
                }

                lblTOTAL_CARGO.Text = fila["TOTAL_CARGO"]?.ToString();
                lblTOTAL_ABONO.Text = fila["TOTAL_ABONO"]?.ToString();
                if (!string.IsNullOrWhiteSpace(txtId.Text) &&
                   int.TryParse(txtId.Text, out int id))
                {
                    CargarDetalle(id);
                    CargarListaPartidas();
                }
            }
            finally
            {
                _cargandoPartida = false;
            }
        }
        private void LimpiarNumero()
        {
            txtTipo.Text = string.Empty;
            txtTipoNombre.Text = string.Empty;
        }
        private void txtNumero_KeyDown(object sender, KeyEventArgs e)
        {
            if (!_modoBusqueda && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!_modoBusqueda && e.KeyChar == '*')
            {
                e.Handled = true;
            }
        }
        #region Grid de Partida Contable

        private void InicializarGridPartida()
        {
            // Crear DataTable con las columnas de CHEQUE_PARTIDA
            _dtPartida = new DataTable();
            _dtPartida.Columns.Add("LINEA", typeof(int));
            _dtPartida.Columns.Add("CUENTA", typeof(string));
            _dtPartida.Columns.Add("CONCEPTO", typeof(string));
            _dtPartida.Columns.Add("CARGO", typeof(decimal));
            _dtPartida.Columns.Add("ABONO", typeof(decimal));

            // ✅ Recalcular cuadre cuando cambien filas (manual o por código)
            _dtPartida.RowChanged += (s, ev) => ActualizarCuadre();
            _dtPartida.RowDeleted += (s, ev) => ActualizarCuadre();

            gridView1.ShownEditor += GridView1_ShownEditor;
            gridView1.CellValueChanged += GridView1_CellValueChanged;

            // Actualiza el label de cuenta contable en cuanto cambia el valor de la columna
            _dtPartida.ColumnChanged += (s, ev) =>
            {
                if (ev.Column.ColumnName == "CUENTA")
                {
                    // Solo mostrar el mensaje si viene de digitación/selección del usuario
                    if (!_asignandoCuentaPorCodigo)
                        ActualizarEstadoCuenta(ev.Row["CUENTA"]?.ToString());
                }
            };

            // Agregar fila vacía inicial
            AgregarFilaVacia();

            // Enlazar al grid
            gridControl1.DataSource = _dtPartida;

            // Configurar columnas visibles con títulos
            var view = gridControl1.MainView as GridView;
            if (view == null) return;

            view.Columns.Clear();
            view.PopulateColumns();

            ConfigurarColumna(view, "LINEA", "ORDEN", 0, false);
            ConfigurarColumna(view, "CUENTA", "CUENTA", 150);
            ConfigurarColumna(view, "CONCEPTO", "DETALLE DE LA APLICACION", 350);
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
            gridView1.FocusedRowChanged += GridView_FocusedRowChanged;
        }

        private bool _actualizandoCargoAbono = false;

        private void GridView1_CellValueChanged(object sender,
    DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (_actualizandoCargoAbono)
                return;

            try
            {
                _actualizandoCargoAbono = true;

                decimal valor = 0;

                if (e.Value != null && e.Value != DBNull.Value)
                    decimal.TryParse(e.Value.ToString(), out valor);

                if (e.Column.FieldName == "CARGO")
                {
                    if (valor > 0)
                        gridView1.SetRowCellValue(e.RowHandle, "ABONO", 0m);
                }
                else if (e.Column.FieldName == "ABONO")
                {
                    if (valor > 0)
                        gridView1.SetRowCellValue(e.RowHandle, "CARGO", 0m);
                }
            }
            finally
            {
                _actualizandoCargoAbono = false;
            }
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

        private void btAdiconarItem_Click(object sender, EventArgs e)
        {
            AgregarFilaVacia();
        }
        private void AgregarFilaVacia()
        {
            var fila = _dtPartida.NewRow();
            fila["LINEA"] = 0;
            fila["CUENTA"] = string.Empty;
            fila["CONCEPTO"] = string.Empty;
            fila["CARGO"] = 0m;
            fila["ABONO"] = 0m;
            _dtPartida.Rows.Add(fila);
        }

        private void AgregarFilaPartida(string ctaContable)
        {
            _asignandoCuentaPorCodigo = true;
            try
            {
                // Limpiar filas vacías antes de agregar
                for (int i = _dtPartida.Rows.Count - 1; i >= 0; i--)
                {
                    var r = _dtPartida.Rows[i];
                    if (string.IsNullOrWhiteSpace(r["CUENTA"].ToString()) &&
                        Convert.ToDecimal(r["CARGO"]) == 0 &&
                        Convert.ToDecimal(r["ABONO"]) == 0)
                        _dtPartida.Rows.RemoveAt(i);
                }

                // Agregar fila con la cuenta contable
                if (_dtPartida.Rows.Count == 0)
                {
                    var fila = _dtPartida.NewRow();
                    fila["LINEA"] = 0;
                    fila["CUENTA"] = ctaContable;
                    fila["CONCEPTO"] = string.Empty;
                    fila["CARGO"] = 0m;
                    fila["ABONO"] = 0m;
                    _dtPartida.Rows.Add(fila);
                }
                else
                {
                    var fila = _dtPartida.Rows[0];
                    fila["CUENTA"] = ctaContable;
                }

                // Agregar fila vacía para siguiente ingreso
                AgregarFilaVacia();
            }
            finally
            {
                _asignandoCuentaPorCodigo = false;
            }
        }

        private void ActualizarEstadoCuenta(string codigo)
        {
            var (texto, esValida) = CuentaContableHint.Obtener(codigo);

            if (string.IsNullOrWhiteSpace(texto) || !gridControl1.Enabled)
            {
                FormHelper.OcultarMensajeRibbon(this);
                return;
            }
            FormHelper.MostrarMensajeRibbon(this, texto);
        }

        /// <summary>
        /// Actualiza la fila 1 (cuenta del banco) con el abono y detalle,
        /// y crea/sobreescribe la fila 2 con la cuenta por pagar del proveedor.
        /// </summary>
        private void AplicarPartidaPago(decimal totalPago, string cuentaPorPagar)
        {
            if (_dtPartida.Rows.Count == 0) return;
            _asignandoCuentaPorCodigo = true;
            try
            {
                string detalle = "";

                // ============================================================
                // Fila 1: cuenta del banco -> ABONO + DETALLE
                // ============================================================
                _dtPartida.Rows[0]["CONCEPTO"] = detalle;
                _dtPartida.Rows[0]["CARGO"] = 0m;
                _dtPartida.Rows[0]["ABONO"] = totalPago;

                // ============================================================
                // Fila 2: cuenta por pagar del proveedor -> CARGO + DETALLE
                // Si ya existe (segunda llamada) se sobrescribe; si no, se crea.
                // ============================================================
                if (_dtPartida.Rows.Count >= 2)
                {
                    _dtPartida.Rows[1]["CUENTA"] = cuentaPorPagar;
                    _dtPartida.Rows[1]["CONCEPTO"] = "";
                    _dtPartida.Rows[1]["CARGO"] = totalPago;
                    _dtPartida.Rows[1]["ABONO"] = 0m;
                }
                else
                {
                    var fila = _dtPartida.NewRow();
                    fila["LINEA"] = 0;
                    fila["CUENTA"] = cuentaPorPagar;
                    fila["CONCEPTO"] = detalle;
                    fila["CARGO"] = totalPago;
                    fila["ABONO"] = 0m;
                    _dtPartida.Rows.Add(fila);
                }

                // Eliminar filas vacías sobrantes después de la fila 2
                for (int i = _dtPartida.Rows.Count - 1; i >= 2; i--)
                {
                    var r = _dtPartida.Rows[i];
                    if (string.IsNullOrWhiteSpace(r["CUENTA"].ToString()) &&
                        Convert.ToDecimal(r["CARGO"]) == 0 &&
                        Convert.ToDecimal(r["ABONO"]) == 0)
                    {
                        _dtPartida.Rows.RemoveAt(i);
                    }
                }

                // Asegurar fila vacía al final para captura adicional
                DataRow ultima = _dtPartida.Rows[_dtPartida.Rows.Count - 1];
                if (!string.IsNullOrWhiteSpace(ultima["CUENTA"].ToString()) ||
                    Convert.ToDecimal(ultima["CARGO"]) != 0 ||
                    Convert.ToDecimal(ultima["ABONO"]) != 0)
                {
                    AgregarFilaVacia();
                }
            }
            finally
            {
                _asignandoCuentaPorCodigo = false;
            }

        }

        private void GridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter && e.KeyCode != Keys.Tab) return;

            var view = sender as GridView;
            if (view == null) return;

            string colActual = view.FocusedColumn?.FieldName;

            // Verificar * en CTACONTABLE PRIMERO antes de cualquier otra acción
            if (colActual == "CUENTA")
            {
                string texto = view.ActiveEditor?.Text?.Trim();
                if (string.IsNullOrEmpty(texto))
                    texto = view.GetFocusedDisplayText()?.Trim();

                if (texto == "*" && e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    AbrirBusquedaCuenta(view);
                    return; // salir sin hacer nada más
                }
            }

            // ✅ Caso especial: DETALLE de la fila 0 (cuenta del banco)
            // → saltar directo a CTACONTABLE de la fila 1
            //if (colActual == "CONCEPTO" && view.FocusedRowHandle == 0)
            //{
            //    e.Handled = true;
            //    view.CloseEditor();
            //    view.UpdateCurrentRow();

            //    if (_dtPartida.Rows.Count < 2)
            //        AgregarFilaVacia();

            //    _columnaAnteriorGrid = "CUENTA";

            //    view.FocusedRowHandle = 1;
            //    view.FocusedColumn = view.Columns["CUENTA"];
            //    view.ShowEditor();
            //    return;
            //}


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
                    view.FocusedColumn = view.Columns["CUENTA"];
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

            // ============================================================
            // Mostrar/ocultar el panel de estado de cuenta contable
            // ============================================================
            if (_columnaAnteriorGrid == "CUENTA" && e.FocusedColumn?.FieldName != "CUENTA")
            {
                string cta = view.GetFocusedRowCellValue("CUENTA")?.ToString();
                MostrarEstadoCuenta(cta);
            }
            else
            {
                FormHelper.OcultarMensajeRibbon(this);
            }

            if (_columnaAnteriorGrid == "CUENTA" &&
                e.FocusedColumn?.FieldName == "CONCEPTO")
            {
                string cta = view.GetFocusedRowCellValue("CUENTA")?.ToString();
                if (string.IsNullOrWhiteSpace(cta)) return;

                string detalleActual = view.GetFocusedRowCellValue("CONCEPTO")?.ToString();
                if (string.IsNullOrWhiteSpace(detalleActual))
                {
                    string detalle = "";

                    view.SetFocusedRowCellValue("CONCEPTO", detalle);
                }
                else
                {
                    view.SetFocusedRowCellValue("CONCEPTO", detalleActual);
                }
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

        private void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // Al cambiar de fila, resetear contexto de columna anterior
            // y ocultar mensaje (el contexto ya no es válido)
            _columnaAnteriorGrid = string.Empty;
            FormHelper.OcultarMensajeRibbon(this);
        }
        private void GridView1_ShownEditor(object sender, EventArgs e)
        {
            var view = sender as GridView;
            if (view?.ActiveEditor is TextEdit editor)
            {
                // Solo para columnas de texto (evitar aplicar a numéricas)
                string columna = view.FocusedColumn?.FieldName;
                if (columna == "CUENTA" )
                {
                    editor.KeyPress += Cuenta_KeyPress;
                }
                    if (columna == "CUENTA" || columna == "CONCEPTO")
                {
                    editor.Properties.CharacterCasing = CharacterCasing.Upper;
                }
                // === Columnas numéricas: bloquear doble punto ===
                if (columna == "CARGO" || columna == "ABONO")
                {
                    editor.KeyPress -= EditorMonto_KeyPress;
                    editor.KeyPress += EditorMonto_KeyPress;

                    editor.Validating -= EditorMonto_Validating;
                    editor.Validating += EditorMonto_Validating;
                }
            }
        }

        private void Cuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txt = sender as DevExpress.XtraEditors.TextEdit;

            if (txt == null)
                return;

            // Permitir teclas de control
            if (char.IsControl(e.KeyChar))
                return;

            // Permitir * solo si está vacío
            if (e.KeyChar == '*')
            {
                if (!string.IsNullOrEmpty(txt.Text))
                    e.Handled = true;

                return;
            }

            // Solo números y punto
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }

            // No permitir ".."
            if (e.KeyChar == '.')
            {
                int pos = txt.SelectionStart;

                if (pos > 0 && txt.Text[pos - 1] == '.')
                {
                    e.Handled = true;
                    return;
                }
            }
        }
        private void EditorMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            var editor = sender as TextEdit;
            if (editor == null) return;

            char c = e.KeyChar;

            // Permitir teclas de control (backspace, delete, etc.)
            if (char.IsControl(c)) return;

            // Permitir dígitos
            if (char.IsDigit(c)) return;

            // Permitir un solo punto decimal
            if (c == '.')
            {
                if (editor.Text.Contains("."))
                    e.Handled = true;   // ya hay uno, bloquear
                return;
            }

            // Cualquier otra tecla se bloquea
            e.Handled = true;
        }

        private void EditorMonto_Validating(object sender, CancelEventArgs e)
        {
            var editor = sender as TextEdit;
            if (editor == null) return;

            if (decimal.TryParse(editor.Text,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out decimal valor))
            {
                // Redondear a 2 decimales
                decimal redondeado = Math.Round(valor, 2, MidpointRounding.AwayFromZero);
                editor.Text = redondeado.ToString("0.00");
            }
        }
        private void MostrarEstadoCuenta(string codigo)
        {
            var (texto, esValida) = CuentaContableHint.Obtener(codigo);

            if (string.IsNullOrWhiteSpace(texto) || !gridControl1.Enabled)
            {
                FormHelper.OcultarMensajeRibbon(this);
                return;
            }
            FormHelper.MostrarMensajeRibbon(this, texto);
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
                Anchos = new Dictionary<string, int>
                {
                    { "CUENTA",  130 },
                    { "NOMBRE_CUENTA",  400 }
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
                    view.SetFocusedRowCellValue("CUENTA",
                        frm.FilaSeleccionada["CUENTA"].ToString());

                    view.FocusedColumn = view.Columns["CONCEPTO"];
                    view.ShowEditor();
                }
                else
                {
                    view.SetFocusedRowCellValue("CUENTA", string.Empty);
                }
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
                lblCUADRE.Text = "";
                lblCUADRE.ForeColor = Color.Transparent;
            }
            else if (diferencia == 0)
            {
                lblCUADRE.Text = "✓ Comprobante cuadrado";
                lblCUADRE.ForeColor = Color.Green;
            }
            else
            {
                lblCUADRE.Text = "⚠ Comprobante Descuadre";
                lblCUADRE.ForeColor = Color.OrangeRed;
            }
        }

        private void LimpiarFilasVaciasGrid()
        {
            for (int i = _dtPartida.Rows.Count - 1; i >= 0; i--)
            {
                var fila = _dtPartida.Rows[i];
                string cta = fila["CUENTA"]?.ToString()?.Trim() ?? "";

                if (string.IsNullOrEmpty(cta))   // solo cuenta vacía
                {
                    _dtPartida.Rows.RemoveAt(i);
                }
            }
        }

        #endregion
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtTipo.Text))
            {
                Alertas.Advertencia("Seleccione un tipo de comprobante.");
                txtTipo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtConcepto.Text))
            {
                Alertas.Advertencia("Ingrese el concepto del comprobante.");
                txtConcepto.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(deFecha.Text))
            {
                Alertas.Advertencia("Ingrese la fecha del comprobante.");
                deFecha.Focus();
                return false;
            }

            DateTime fecha;
            if (!DateTime.TryParse(deFecha.Text, out fecha))
            {
                Alertas.Advertencia("La fecha ingresada no es válida.");
                deFecha.Focus();
                return false;
            }

            if (fecha.Date > DateTime.Today)
            {
                Alertas.Advertencia("La fecha no puede ser mayor a la fecha actual.");
                deFecha.Focus();
                return false;
            }

            if (fecha.Date < DateTime.Today.AddDays(-60))
            {
                Alertas.Advertencia("La fecha no puede ser menor a 60 días.");
                deFecha.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                Alertas.Advertencia("El número de comprobante es requerido.");
                txtNumero.Focus();
                return false;
            }

            // Verificar que exista al menos una línea
            bool tieneLineas = _dtPartida.AsEnumerable()
                .Any(x => !string.IsNullOrWhiteSpace(Convert.ToString(x["CUENTA"])));

            if (!tieneLineas)
            {
                XtraMessageBox.Show(
                    "Debe ingresar al menos una línea en el comprobante contable.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            // Validar cuadre
            decimal totalCargo = 0;
            decimal totalAbono = 0;

            foreach (DataRow fila in _dtPartida.Rows)
            {
                totalCargo += fila["CARGO"] == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(fila["CARGO"]);

                totalAbono += fila["ABONO"] == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(fila["ABONO"]);
            }

            if (Math.Round(totalCargo, 2) != Math.Round(totalAbono, 2))
            {
                decimal diferencia = Math.Abs(totalCargo - totalAbono);

                XtraMessageBox.Show(
                    $"El comprobante contable no cuadra.\n\n" +
                    $"Total Cargo: {totalCargo:N2}\n" +
                    $"Total Abono: {totalAbono:N2}\n" +
                    $"Diferencia: {diferencia:N2}",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        private class DetalleLineaDto
        {
            public string CUENTA { get; set; }
            public string CONCEPTO { get; set; }
            public decimal CARGO { get; set; }
            public decimal ABONO { get; set; }
        }

        private List<SqlDataRecord> ConstruirRegistrosDetalle(DataTable dt)
        {
            var metaData = new SqlMetaData[]
            {
        new SqlMetaData("LINEA", SqlDbType.Int),
        new SqlMetaData("CUENTA", SqlDbType.NVarChar, 30),
        new SqlMetaData("CONCEPTO", SqlDbType.NVarChar, 500),
        new SqlMetaData("CARGO", SqlDbType.Decimal, 20, 2),
        new SqlMetaData("ABONO", SqlDbType.Decimal, 20, 2),
        new SqlMetaData("ID_CENTRO", SqlDbType.Int),
        new SqlMetaData("ID_ENTIDAD", SqlDbType.Int),
        new SqlMetaData("UID_DOCUMENTO", SqlDbType.NVarChar, 40)
            };

            var registros = new List<SqlDataRecord>();

            foreach (DataRow row in dt.Rows)
            {
                var record = new SqlDataRecord(metaData);

                record.SetInt32(0, Convert.ToInt32(row["LINEA"]));
                record.SetString(1, Convert.ToString(row["CUENTA"]));

                if (row["CONCEPTO"] == DBNull.Value)
                    record.SetDBNull(2);
                else
                    record.SetString(2, Convert.ToString(row["CONCEPTO"]));

                decimal cargo = row["CARGO"] == DBNull.Value ? 0m : Convert.ToDecimal(row["CARGO"]);
                decimal abono = row["ABONO"] == DBNull.Value ? 0m : Convert.ToDecimal(row["ABONO"]);

                record.SetDecimal(3, cargo);
                record.SetDecimal(4, abono);

                // Estas columnas no vienen en el DataTable
                record.SetDBNull(5); // ID_CENTRO
                record.SetDBNull(6); // ID_ENTIDAD
                record.SetDBNull(7); // UID_DOCUMENTO

                registros.Add(record);
            }

            return registros;
        }

        private decimal ConvertirDecimal(object valor)
        {
            decimal resultado;
            string texto = Convert.ToString(valor);

            if (decimal.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out resultado) ||
                decimal.TryParse(texto, NumberStyles.Any, CultureInfo.CurrentCulture, out resultado))
                return resultado;

            throw new InvalidOperationException("Valor numerico invalido en el detalle.");
        }



        private void InsertarPartida(DataTable dt, decimal totalCargo, decimal totalAbono)
        {
            DateTime fecha = Convert.ToDateTime(deFecha.Text);
            string tipoPartida = Convert.ToString(txtTipo.Text);
            int numPartida = int.TryParse(txtNumero.Text, out var np) ? np : 0;

            using (var cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["SistemaContable"].ConnectionString))
            using (var cmd = new SqlCommand("[CONTA].[SP_PARTIDA_GUARDAR]", cnn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@TIPO_PARTIDA", tipoPartida);
                cmd.Parameters.AddWithValue("@ANIO", fecha.Year);
                cmd.Parameters.AddWithValue("@MES", fecha.Month);
                cmd.Parameters.AddWithValue("@NUM_PARTIDA", numPartida);
                cmd.Parameters.AddWithValue("@FECHA_PARTIDA", fecha);
                cmd.Parameters.AddWithValue("@CONCEPTO", txtConcepto.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_CREA", Configuracion.UsuarioActual);

                var registrosDetalle = ConstruirRegistrosDetalle(dt);
                var detalleParam = cmd.Parameters.AddWithValue("@DETALLE", registrosDetalle);
                detalleParam.SqlDbType = SqlDbType.Structured;
                detalleParam.TypeName = "CONTA.TT_DETAPARTIDA";

                var pIdOut = cmd.Parameters.Add("@ID_PARTIDA_OUT", SqlDbType.BigInt);
                pIdOut.Direction = ParameterDirection.Output;

                var pNidOut = cmd.Parameters.Add("@NID_PARTIDA_OUT", SqlDbType.NVarChar, 40);
                pNidOut.Direction = ParameterDirection.Output;

                try
                {
                    cnn.Open();
                    cmd.ExecuteNonQuery();

                    long idPartida = pIdOut.Value != DBNull.Value ? Convert.ToInt64(pIdOut.Value) : 0;
                    string nidPartida = Convert.ToString(pNidOut.Value);

                    txtId.Text = idPartida.ToString();
                    txtNID_PARTIDA.Text = nidPartida;

                    Alertas.Exito($"Partida {nidPartida} guardada correctamente.");
                    ConfigurarCRUD(EstadoFormulario.Success);
                }
                catch (SqlException ex)
                {
                    Alertas.Error(ex.Message);
                }
            }
        }


        private void ActualizarPartida(long idPartida, DataTable dt, decimal totalCargo, decimal totalAbono)
        {
            DateTime fecha = Convert.ToDateTime(deFecha.Text);

            using (var cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["SistemaContable"].ConnectionString))
            using (var cmd = new SqlCommand("[CONTA].[SP_PARTIDA_ACTUALIZAR]", cnn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@ID_PARTIDA", idPartida);
                cmd.Parameters.AddWithValue("@FECHA_PARTIDA", fecha);
                cmd.Parameters.AddWithValue("@CONCEPTO", txtConcepto.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_MODIFICA", Configuracion.UsuarioActual);

                var registrosDetalle = ConstruirRegistrosDetalle(dt);
                var detalleParam = cmd.Parameters.AddWithValue("@DETALLE", registrosDetalle);
                detalleParam.SqlDbType = SqlDbType.Structured;
                detalleParam.TypeName = "CONTA.TT_DETAPARTIDA";

                var pNidOut = cmd.Parameters.Add("@NID_PARTIDA_OUT", SqlDbType.NVarChar, 40);
                pNidOut.Direction = ParameterDirection.Output;
                try
                {
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    string nidPartida = Convert.ToString(pNidOut.Value);

                    txtId.Text = idPartida.ToString();
                    txtNID_PARTIDA.Text = nidPartida;
                    //MostrarMensaje("Éxito", $"Partida {txtNidPartida.Text} actualizada correctamente.", "success");
                    Alertas.Exito($"Partida {nidPartida} actualizada correctamente.");
                    ConfigurarCRUD(EstadoFormulario.Success);
                    // NO se limpia la sesión ni el grid: queda visible lo recién actualizado.
                }
                catch (SqlException ex)
                {
                    Alertas.Error(ex.Message);
                }
            }
        }

        private void btBuscar_Click(object sender, EventArgs e)
        {
            ConfigurarCRUD(EstadoFormulario.Buscar);
        }

        private void btSuspender_Click(object sender, EventArgs e)
        {

        }

        private void btImprimir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNID_PARTIDA.Text) || txtNID_PARTIDA.Text.Trim() == "-1")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "No se ha procesado la partida.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                var reporte = new RptPartida_Movimiento
                {
                    _NID_PARTIDA = txtNID_PARTIDA.Text.Trim()
                };

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
        }
        private void ActualizarBotones()
        {
            btAnterior.Enabled =
                _dtPartidas != null &&
                _dtPartidas.Rows.Count > 0 &&
                _indiceActual > 0;

            btSiguiente.Enabled =
                _dtPartidas != null &&
                _dtPartidas.Rows.Count > 0 &&
                _indiceActual < _dtPartidas.Rows.Count - 1;
        }
        private void btAnterior_Click(object sender, EventArgs e)
        {           
                if (_dtPartidas == null || _dtPartidas.Rows.Count == 0)
                    return;

                if (_indiceActual <= 0)
                    return;

                _indiceActual--;

                AsignarNumero(_dtPartidas.Rows[_indiceActual]);

                ActualizarBotones();
            }

        private void btSiguiente_Click(object sender, EventArgs e)
        {
           
                if (_dtPartidas == null || _dtPartidas.Rows.Count == 0)
                    return;

                if (_indiceActual >= _dtPartidas.Rows.Count - 1)
                    return;

                _indiceActual++;

                AsignarNumero(_dtPartidas.Rows[_indiceActual]);

                ActualizarBotones();
            }

        private void btModificar_Click(object sender, EventArgs e)
        {
            FormHelper.OcultarMensajeRibbon(this);
            ConfigurarCRUD(EstadoFormulario.Modificar);
        }

        private void btEliminar_Click(object sender, EventArgs e)
        {
            FormHelper.OcultarMensajeRibbon(this);

            try
            {
                DialogResult respuesta = XtraMessageBox.Show(
                    $"¿Está seguro que desea eliminar el comprobante {txtNID_PARTIDA.Text}?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (respuesta != DialogResult.Yes)
                    return;

                string codigo = XtraInputBox.Show(
                    "Ingrese el código de autorización para eliminar la partida:",
                    "Código de eliminación",
                    "");

                if (string.IsNullOrWhiteSpace(codigo))
                {
                    Alertas.Advertencia("Debe ingresar un código de eliminación.");
                    return;
                }

                var parametros = new DynamicParameters();

                parametros.Add("@NID_PARTIDA", txtNID_PARTIDA.Text.Trim());
                parametros.Add("@USUARIO", Configuracion.UsuarioActual);
                parametros.Add("@COD_ELIMINAR", codigo.Trim());

                parametros.Add("@pResCode",
                    dbType: DbType.Int32,
                    direction: ParameterDirection.Output);

                parametros.Add("@pMsg",
                    dbType: DbType.String,
                    size: 500,
                    direction: ParameterDirection.Output);

                _dal.EjecutarConSalida("[CONTA].DEL_PARATIDA_VENTAS", parametros);

                int resultado = parametros.Get<int>("@pResCode");
                string mensaje = parametros.Get<string>("@pMsg");

                if (resultado == 0)
                {
                    Alertas.Exito(mensaje);



                    LimpiarControles();
                }
                else
                {
                    Alertas.Error(mensaje);
                }
            }
            catch (Exception ex)
            {
                Alertas.Error("Error al eliminar la partida:\n" + ex.Message);
            }
        }

        private void btGrabar_Click(object sender, EventArgs e)
        {
            LimpiarFilasVaciasGrid();

            if (!ValidarCampos())
                return;

            FormHelper.OcultarMensajeRibbon(this);

            long idPartidaExistente;
            bool esActualizacion = long.TryParse(txtId.Text, out idPartidaExistente)
                                   && idPartidaExistente > 0;

            if (esActualizacion)
            {
                DialogResult respuesta = XtraMessageBox.Show(
                    "¿Está seguro que desea actualizar el Comprobante?\n\nLos cambios realizados serán guardados permanentemente.",
                    "Confirmar actualización",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                    return;

                ActualizarPartida(
                    Convert.ToInt32(txtId.Text),
                    _dtPartida,
                    Convert.ToDecimal(lblTOTAL_CARGO.Text),
                    Convert.ToDecimal(lblTOTAL_ABONO.Text));
            }
            else
            {
                InsertarPartida(
                    _dtPartida,
                    Convert.ToDecimal(lblTOTAL_CARGO.Text),
                    Convert.ToDecimal(lblTOTAL_ABONO.Text));
            }
        }

        private void btIgnorar_Click(object sender, EventArgs e)
        {
            FormHelper.OcultarMensajeRibbon(this);
            LimpiarControles();
        }

        private void btNuevo_Click(object sender, EventArgs e)
        {
            FormHelper.OcultarMensajeRibbon(this);
            ConfigurarCRUD(EstadoFormulario.Nuevo);
        }

        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
