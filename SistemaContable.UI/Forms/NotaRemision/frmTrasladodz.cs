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
using DevExpress.XtraEditors.Repository;
using System.Data.SqlClient;

namespace SistemaContable.UI.Forms.NotaRemision
{
    public partial class frmTrasladodz : Form
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

        private int _ID_PROV_TRANSP = 0;
        private int _ID_MOTORISTA = 0;

        public int IdTraslado { get; set; } = 0;
        public frmTrasladodz()
        {
            InitializeComponent();
            this.SetStyle(
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.AllPaintingInWmPaint,
                 true);
            this.UpdateStyles();
        }

        private void frmTrasladodz_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);
            CargarSucursal();
            CargarTipoDte();
            CargarTansposte();
            CargarZafra();
            CargarSegmento();
            

            mskFECHA_EMISION.Text = DateTime.Now.ToString("dd/MM/yyyy");


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
                    ParametrosExtra = new { ROL = "NR_DZ" }
                },
                fila => AsignarProveedor(fila)
            );
            txtPROVEEDOR.Leave += txtPROVEEDOR_Leave;


            FormHelper.RegistrarBusqueda(
               txtPROV_TRANSP,
               new BusquedaConfig
               {
                   StoredProcedure = "[EGENERALES].SP_PROVEEDOR_TRANSPORTE",
                   Accion = "BUSCAR",
                   Columnas = new Dictionary<string, string>
                   {
                        { "ID_PROV_TRANSP", "ID" },
                        { "NOMBRE",         "NOMBRE"    },
                        { "TRANSPORTE",            "TRANSPORTE"       }
                   },
                   Anchos = new Dictionary<string, int>
                   {
                        { "CODIGO_ENTIDAD", 100 },
                        { "NOMBRE",         300 },
                        { "TRANSPORTE",            300 }
                   }

               },
               fila => AsignarProveedorTransporte(fila)
           );
            txtPROV_TRANSP.Leave += txtPROV_TRANSP_Leave;

            FormHelper.RegistrarBusqueda(
              txtMotorista,
              new BusquedaConfig
              {
                  StoredProcedure = "[EGENERALES].SP_MOTORISTA",
                  Accion = "BUSCAR",
                  Columnas = new Dictionary<string, string>
                  {
                        { "ID_MOTORISTA", "ID" },
                        { "NOMBRE",         "NOMBRE"    },
                        { "LICENCIA",            "LICENCIA"       }
                  },
                  Anchos = new Dictionary<string, int>
                  {
                        { "CODIGO_ENTIDAD", 100 },
                        { "NOMBRE",         300 },
                        { "LICENCIA",            60 }
                  }

              },
              fila => AsignarMotorista(fila)
          );
            txtMotorista.Leave += txtMotorista_Leave;


            ConfigurarTextBoxDecimal(
                txtGRAVADA, txtTOTAL
            );

           
            if (IdTraslado > 0)
            {
                CargarNRExistente(IdTraslado);
                CargarNR_DTExistente(IdTraslado);


            }
            else
            {

                txtCOD_GENERACION.Text = DALBase.NuevoGUID();
                txtPROVEEDOR.Text = "4061-4";
                txtPROVEEDOR_Leave(null,null);
                CargarSiguienteNumNotaRemision();

                ConfigurarCRUD(EstadoFormulario.Nuevo);
            }

        }

        private void ConfigurarCRUD(EstadoFormulario estado)
        {

            switch (estado)
            {
                case EstadoFormulario.Nuevo:
                    txtPROVEEDOR.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnValidar.Enabled = false;
                    btnImprimir.Enabled = false;
                    btnCorreo.Enabled = false;

                    break;
                case EstadoFormulario.Guardado:
                    txtPROVEEDOR.Enabled = false;
                    btnGuardar.Enabled = true;
                    btnValidar.Enabled = true;
                    btnImprimir.Enabled = true;
                    btnCorreo.Enabled = false;
                    break;
                case EstadoFormulario.Validado:
                    txtPROVEEDOR.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnValidar.Enabled = false;
                    btnImprimir.Enabled = true;
                    btnCorreo.Enabled = true;

                    break;
            }
        }

        private void CargarNRExistente(int IdTraslado)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                DataTable dt = _dal.EjecutarConsulta("[EDTE].SP_NOTA_REMISION",
                    new
                    {
                        ACCION = "OBTENER",
                        ID_NTREMISIONENC = IdTraslado
                    });

                if (dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No se encontró el documento solicitado.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    return;
                }

                DataRow r = dt.Rows[0];

                // ---------- Proveedor ----------
                _idEntidad = Convert.ToInt32(r["ID_CLIENTE"]);

                _codigoEntidad = r["COD_REF"].ToString();


                txtPROVEEDOR.Text = _codigoEntidad;
                txtNOMBRE_PROVEEDOR.Text = r["NOMBRE_ENTIDAD"]?.ToString();
                txtNRC.Text = r["NRC"].ToString();
                txtNIT.Text = r["NIT"].ToString();
                txtTELEFONO.Text = r["CELULAR"].ToString();
                txtCORREO.Text = r["CORREO"].ToString();
                txtACTIVIDAD_PRIMARIA.Text = r["ACTIVIDAD_PRIMARIA"].ToString();
                txtDIRECCION.Text = r["COMPLEMENTO"].ToString();

                // ---------- Documento fiscal ----------

                mskFECHA_EMISION.Text = AsFecha(r["FECHA"]);

                txtNumero_NR.Text = AsString(r["NUMDOC"]);
                txtSELLO_RECIBIDO.Text = AsString(r["SELLORECEPCION"]);
                txtCOD_GENERACION.Text = AsString(r["CODGENERACION"]);
                txtNUM_CONTROL.Text = AsString(r["NUMCONTROL"]);
                txtObservacion.Text = AsString(r["OBSERVACIONES"]);

                txtGRAVADA.Text = AsString(r["AFECTA"]);
                txtTOTAL.Text = AsString(r["TOTALVENTA"]);


                _ID_PROV_TRANSP = Convert.ToInt32(r["ID_PROV_TRANSP"]);
                txtPROV_TRANSP.Text = r["PROV_TXTTRANSPORTE"].ToString();
                cbxIdTransposte.SelectedValue = Convert.ToInt32(r["ID_TRANSPORTE"]);
                txtPlaca.Text = r["PLACA"].ToString();
                txtRemolque.Text = r["REMOLQUE"].ToString();
                _ID_MOTORISTA = Convert.ToInt32(r["ID_MOTORISTA"]);

                txtMotorista.Text = r["MOTORISTA"].ToString();
                txtLicencia.Text = r["LICENCIA"].ToString();

               
                cbxID_ZAFRA.SelectedValue = Convert.ToInt32(r["ID_ZAFRA"]);

                cbxID_SEGMENTO.SelectedValue = Convert.ToInt32(r["ID_SEGMENTO"]);
                cbxID_SEGMENTO_SelectedIndexChanged(null, null);
                cbxID_DTSEGMENTO.SelectedValue = Convert.ToInt32(r["ID_DTSEGMENTO"]);

                mskFECHA_DTE_DZ.Text = AsFecha(r["FECHA_DTE_DZ"]);

                txtCOD_GENERACION_DZ.Text = r["COD_GENERACION_DZ"].ToString();
                txtNUM_CONTROL_DZ.Text = r["NUM_CONTROL_DZ"].ToString();
               

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar el documento:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                if (string.IsNullOrWhiteSpace(txtSELLO_RECIBIDO.Text))
                {
                    ConfigurarCRUD(EstadoFormulario.Guardado);
                }
                else
                {
                    ConfigurarCRUD(EstadoFormulario.Validado);
                }


            }
        }
        private void CargarNR_DTExistente(int IdTraslado)
        {
            try
            {
                DataTable dt2 = _dal.EjecutarConsulta("[EDTE].SP_NOTAREMISION_DET",
                    new
                    {
                        ACCION = "OBTENER",
                        ID_NTREMISIONENC = IdTraslado
                    });

                if (dt2.Rows.Count > 0)
                {
                    _dtDeta.Rows.Clear();

                    foreach (DataRow row in dt2.Rows)
                    {
                        DataRow nueva = _dtDeta.NewRow();

                        nueva["ID_PRODUCTO"] = row["ID_PRODUCTO"];
                        nueva["COD_REF"] = row["COD_REF"];
                        nueva["DESCRIPCION"] = row["DESCRIPCION"];
                        nueva["ID_UNIDAD_MEDIDA"] = row["ID_UNIDAD_MEDIDA"];
                        nueva["UNIDAD_MEDIDA"] = row["UNIDAD_MEDIDA"];
                        nueva["CANTIDAD"] = row["CANTIDAD"];
                        nueva["PRECIO"] = row["PRECIO"];
                        nueva["TOTAL"] = row["TOTAL"];

                        _dtDeta.Rows.Add(nueva);
                    }
                }

                if (string.IsNullOrWhiteSpace(txtSELLO_RECIBIDO.Text))
                {
                    AgregarFilaVacia();
                }

                //var view = gridControl1.MainView as GridView;
                //view.Columns["ELIMINAR"].OptionsColumn.AllowEdit =
                //    string.IsNullOrWhiteSpace(txtSELLO_RECIBIDO.Text);


                var view = gridControl1.MainView as GridView;

                bool editable = string.IsNullOrWhiteSpace(txtSELLO_RECIBIDO.Text);

                view.OptionsBehavior.Editable = editable;
                view.OptionsBehavior.ReadOnly = !editable;

                view.Appearance.Row.BackColor = editable
                                    ? Color.White
                                    : Color.LightGray;


                gridControl1.RefreshDataSource();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar el documento:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AsignarDecimal(TextBox tb, decimal valor)
        {
            tb.Text = valor == 0 ? "" : valor.ToString("N2");
        }
        private static string NullIfEmpty(string texto)
        {
            return string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
        }

        private static string AsString(object valor)
        {
            return valor == null || valor == DBNull.Value ? "" : valor.ToString();
        }

        private static string AsFecha(object valor)
        {
            if (valor == null || valor == DBNull.Value) return "";
            return Convert.ToDateTime(valor).ToString("dd/MM/yyyy");
        }

        private static int ToInt(object valor)
        {
            if (valor == null || valor == DBNull.Value) return 0;
            return Convert.ToInt32(valor);
        }

        private static decimal ToDecimal(object valor)
        {
            if (valor == null || valor == DBNull.Value) return 0;
            return Convert.ToDecimal(valor);
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
        private void CargarSucursal()
        {
            DataTable dt = _dal.EjecutarConsulta("SP_SUCURSAL",
                new { ACCION = "OBTENER", ID_SUCURSAL = 1 });
            cbxSUCURSAL.DataSource = dt;
            cbxSUCURSAL.ValueMember = "ID_SUCURSAL";
            cbxSUCURSAL.DisplayMember = "NOMBRE";
        }
        private void CargarTansposte()
        {
            DataTable dt = _dal.EjecutarConsulta("[EGENERALES].SP_TRANSPORTE",
                new { ACCION = "OBTENER_CB" });
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr["ID_TRANSPORTE"] = 0;
                dr["NOMBRE"] = "-- SELECCIONAR --";

                dt.Rows.InsertAt(dr, 0);
                cbxIdTransposte.DataSource = dt;
            cbxIdTransposte.ValueMember = "ID_TRANSPORTE";
            cbxIdTransposte.DisplayMember = "NOMBRE";
            cbxIdTransposte.SelectedIndex = -1;
                cbxIdTransposte.SelectedIndex = 0;
            }
            else
            {
                cbxIdTransposte.DataSource = null;
            }
        }
        private void CargarZafra()
        {
            DataTable dt = _dal.EjecutarConsulta("[EGENERALES].[SP_ZAFRA]",
                new { ACCION = "OBTENER_CB" });
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr["ID_ZAFRA"] = 0;
                dr["NOMBRE_ZAFRA"] = "-- SELECCIONAR --";

                dt.Rows.InsertAt(dr, 0);
                cbxID_ZAFRA.DataSource = dt;
            cbxID_ZAFRA.ValueMember = "ID_ZAFRA";
            cbxID_ZAFRA.DisplayMember = "NOMBRE_ZAFRA";
                cbxID_ZAFRA.SelectedIndex = 0;
                }
                else
                {
                cbxID_ZAFRA.DataSource = null;
                }
            }

        private void CargarSegmento()
        {
            DataTable dt = _dal.EjecutarConsulta("[EDIZUCAR].CB_SEGMENTOS_VTDZ",
                new
                {
                    ACCION = "SEGMENTOS_CB"
                });
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr["ID_SEGMENTO"] = 0;
                dr["DETALLE"] = "-- SELECCIONAR --";

                dt.Rows.InsertAt(dr, 0);

                cbxID_SEGMENTO.DataSource = dt;
            cbxID_SEGMENTO.ValueMember = "ID_SEGMENTO";
            cbxID_SEGMENTO.DisplayMember = "DETALLE";
                cbxID_SEGMENTO.SelectedIndex = 0;
            }
            else
            {
                cbxID_SEGMENTO.DataSource = null;
            }
        }
        private void CargarSegmentoDt(int _ID_SEGMENTO)
        {
            DataTable dt = _dal.EjecutarConsulta("[EDIZUCAR].CB_SEGMENTOSDT_VTDZ",
                new
                {
                    ACCION = "CLIENTE_SEGMENTOS_CB",
                    ID_SEGMENTO = _ID_SEGMENTO
                });


            if (dt != null && dt.Rows.Count > 0)
            {                
                DataRow dr = dt.NewRow();
                dr["ID_DTSEGMENTO"] = 0;
                dr["DETALLE"] = "-- SELECCIONAR --";
                
                dt.Rows.InsertAt(dr, 0);
                cbxID_DTSEGMENTO.DataSource = dt;
                cbxID_DTSEGMENTO.ValueMember = "ID_DTSEGMENTO";
                cbxID_DTSEGMENTO.DisplayMember = "DETALLE";
               
                cbxID_DTSEGMENTO.SelectedIndex = 0;
            }
            else
            {
                cbxID_DTSEGMENTO.DataSource = null;
            }

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
                    ROL = "NR_DZ"
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

        /// proveedor transporte
        private void txtPROV_TRANSP_Leave(object sender, EventArgs e)
        {
            string codigo = txtPROV_TRANSP.Text.Trim();
            if (codigo == "*") return;
            if (string.IsNullOrWhiteSpace(codigo))
            {
                LimpiarProveedorTransporte();
                return;
            }
            //try
            //{
            //    var dt = _dal.EjecutarConsulta("SP_ENTIDAD", new
            //    {
            //        ACCION = "BUSCAR_POR_CODIGO",
            //        FILTRO = codigo,
            //        ROL = "PRO"
            //    });

            //    if (dt.Rows.Count > 0)
            //        AsignarProveedor(dt.Rows[0]);
            //    else
            //    {
            //        LimpiarProveedor();
            //        DevExpress.XtraEditors.XtraMessageBox.Show(
            //            $"No se encontró el proveedor con código '{codigo}'.",
            //            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        txtPROVEEDOR.Focus();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    DevExpress.XtraEditors.XtraMessageBox.Show(
            //        $"Error al buscar proveedor: {ex.Message}",
            //        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        private void AsignarProveedorTransporte(DataRow fila)
        {
            _ID_PROV_TRANSP = Convert.ToInt32(fila["ID_PROV_TRANSP"]);

            txtPROV_TRANSP.Text = fila["NOMBRE"].ToString();


        }
        private void LimpiarProveedorTransporte()
        {
            _ID_PROV_TRANSP = 0;
            txtPROV_TRANSP.Text = string.Empty;
        }

        private void txtMotorista_Leave(object sender, EventArgs e)
        {
            string codigo = txtMotorista.Text.Trim();
            if (codigo == "*") return;
            if (string.IsNullOrWhiteSpace(codigo))
            {
                LimpiarMotorista();
                return;
            }

        }
        private void AsignarMotorista(DataRow fila)
        {
            _ID_MOTORISTA = Convert.ToInt32(fila["ID_MOTORISTA"]);

            txtMotorista.Text = fila["NOMBRE"].ToString();
            txtLicencia.Text = fila["LICENCIA"].ToString();

        }
        private void LimpiarMotorista()
        {
            _ID_MOTORISTA = 0;
            txtMotorista.Text = string.Empty;
            txtLicencia.Text = string.Empty;
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

            // Crear columna de botón eliminar
            var colEliminar = view.Columns.AddField("ELIMINAR");
            colEliminar.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            colEliminar.Visible = true;
            colEliminar.Width = 50;
            colEliminar.Caption = " ";

            RepositoryItemButtonEdit btnEliminar = new RepositoryItemButtonEdit();
            btnEliminar.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // Configurar botón
            btnEliminar.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            btnEliminar.Buttons[0].ImageOptions.Image = Properties.Resources.eliminarFila32x32;
            btnEliminar.Buttons[0].Caption = "";

            btnEliminar.Buttons[0].ToolTip = "Eliminar";

            // Evento click
            btnEliminar.ButtonClick += (s, e) =>
            {
                var view_item = gridControl1.MainView as GridView;
                int fila = view_item.FocusedRowHandle;

                if (fila >= 0)
                {
                    var result = MessageBox.Show(
                        "¿Desea eliminar esta fila?",
                        "Confirmar",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {


                        object valor = view_item.GetRowCellValue(fila, "ID_PRODUCTO");

                        // ✅ Si es fila nueva (no existe en BD)
                        if (valor == null || valor == DBNull.Value)
                        {
                            view_item.DeleteRow(fila);
                            ActualizarCuadre();
                            return;
                        }

                        int idProducto = Convert.ToInt32(valor);

                        // ⚠️ Si ya está guardado en BD → eliminar en BD
                        if (IdTraslado > 0)
                        {


                            _dal.EjecutarSinRetorno("[EDTE].SP_NOTAREMISION_DET", new
                            {
                                ACCION = "ELIMINAR",
                                ID_NTREMISIONENC = IdTraslado,
                                ID_PRODUCTO = idProducto
                            });
                        }

                        view_item.DeleteRow(fila);
                        ActualizarCuadre();
                    }
                }
            };


            gridControl1.RepositoryItems.Add(btnEliminar);
            colEliminar.ColumnEdit = btnEliminar;

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
            if (string.IsNullOrWhiteSpace(txtSELLO_RECIBIDO.Text))
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
                ParametrosExtra = new { ROL_PROD = "NRE DIZUCAR" }
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
                    view.SetFocusedRowCellValue("ID_PRODUCTO",
                        frm.FilaSeleccionada["ID_PRODUCTO"].ToString());

                    view.SetFocusedRowCellValue("COD_REF",
                        frm.FilaSeleccionada["COD_REF"].ToString());

                    view.SetFocusedRowCellValue("DESCRIPCION",
                        frm.FilaSeleccionada["DESCRIPCION"].ToString());

                    view.SetFocusedRowCellValue("ID_UNIDAD_MEDIDA",
                        frm.FilaSeleccionada["ID_UNIDAD_MEDIDA"].ToString());

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
                    "Seleccione un Bodega Destino.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPROVEEDOR.Focus();
                return false;
            }

            if (!FormHelper.ValidarFecha(mskFECHA_EMISION, "Fecha de la Nota Remision"))
                return false;


            if (cbxID_ZAFRA.SelectedIndex == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Seleccione una zafra.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxID_ZAFRA.Focus();
                return false;
            }

            if (cbxID_SEGMENTO.SelectedIndex == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Seleccione un segmento.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cbxID_SEGMENTO.Focus();
                return false;
            }

            if (cbxID_DTSEGMENTO.SelectedIndex == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Seleccione un Cliente de segmento.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cbxID_DTSEGMENTO.Focus();
                return false;
            }

            if (!FormHelper.ValidarFecha(mskFECHA_DTE_DZ, "Fecha de DTE Dizucar"))
                return false;

            if (string.IsNullOrWhiteSpace(txtCOD_GENERACION_DZ.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Cod. Generación de Información DTE Dizucar requerido",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCOD_GENERACION_DZ.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNUM_CONTROL_DZ.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "N° Contol de Información DTE Dizucar requerido",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNUM_CONTROL_DZ.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPROV_TRANSP.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Seleccione un Proveedor de Transposte.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPROV_TRANSP.Focus();
                return false;
            }

            if (cbxIdTransposte.SelectedIndex == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Seleccione un tipo de transporte.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cbxIdTransposte.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPlaca.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ingresar la placa del Transposte.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPlaca.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMotorista.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Seleccione un Motoarista.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMotorista.Focus();
                return false;
            }

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
                    ID_SUCURSAL = cbxSUCURSAL.SelectedValue,
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
                    OBSERVACIONES = txtObservacion.Text,
                    USER_CREA = Configuracion.UsuarioActual,
                    OPCIONNR = "NRDZ",

                    ID_PROV_TRANSP = _ID_PROV_TRANSP,
                    ID_TRANSPORTE = cbxIdTransposte.SelectedValue,
                    PLACA = txtPlaca.Text,
                    REMOLQUE = txtRemolque.Text,
                    ID_MOTORISTA = _ID_MOTORISTA,
                    MARCHAMOS1 = txtCOD_GENERACION_DZ.Text,
                    MARCHAMOS3 = txtNUM_CONTROL_DZ.Text,
                    ID_ZAFRA = cbxID_ZAFRA.SelectedValue,
                    ID_SEGMENTO = cbxID_SEGMENTO.SelectedValue,
                    ID_DTSEGMENTO = cbxID_DTSEGMENTO.SelectedValue,
                    FECHA_DTE_DZ = FormHelper.ObtenerFecha(mskFECHA_DTE_DZ),
                    COD_GENERACION_DZ =txtCOD_GENERACION_DZ.Text,
                    NUM_CONTROL_DZ =txtNUM_CONTROL_DZ.Text

                });

                if (dtNR.Rows.Count == 0) return;
                IdTraslado = Convert.ToInt32(dtNR.Rows[0]["ID_GENERADO"]);
                _idNR = Convert.ToInt32(dtNR.Rows[0]["ID_GENERADO"]);
                txtNUM_CONTROL.Text = Convert.ToString(dtNR.Rows[0]["NCONT"]);
                txtNumero_NR.Text = Convert.ToString(dtNR.Rows[0]["INTERN"]);

                // 3. Guardar líneas detalle
                foreach (DataRow fila in _dtDeta.Rows)
                {
                    string cta = fila["ID_PRODUCTO"].ToString().Trim();
                    if (string.IsNullOrWhiteSpace(cta)) continue;

                    _dal.EjecutarSinRetorno("[EDTE].SP_NOTAREMISION_DET", new
                    {
                        ACCION = "GUARDAR",
                        ID_NTREMISIONDT = 0,
                        ID_NTREMISIONENC = _idNR,
                        ID_EMISOR = 1,
                        CODGENERACION = txtCOD_GENERACION.Text,
                        ID_PRODUCTO = Convert.ToInt32(fila["ID_PRODUCTO"]),
                        COD_REF = fila["COD_REF"],
                        DESCRIPCION = fila["DESCRIPCION"],
                        CANTIDAD = Convert.ToDecimal(fila["CANTIDAD"]),
                        ID_UNIDAD_MEDIDA = Convert.ToInt32(fila["ID_UNIDAD_MEDIDA"]),
                        UNIDAD_MEDIDA = fila["UNIDAD_MEDIDA"],
                        PRECIO = Convert.ToDecimal(fila["PRECIO"]),
                        EXENTA = 0,
                        GRAVADA = Convert.ToDecimal(fila["TOTAL"]),
                        TOTAL = Convert.ToDecimal(fila["TOTAL"]),
                        USER_CREA = Configuracion.UsuarioActual,
                    });
                }


                _dal.EjecutarSinRetorno("[EDTE].SP_NOTAREMISION_JSON", new
                {
                    ID_NTREMISIONENC = IdTraslado
                });


                if (_idNR != 0)
                {
                    ConfigurarCRUD(EstadoFormulario.Guardado);
                }
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Nota Remision guarda correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                // Quitar texto técnico del SP si viene incluido
                if (mensaje.Contains("]:"))
                {
                    mensaje = mensaje.Substring(mensaje.IndexOf("]:") + 2).Trim();
                }

                DevExpress.XtraEditors.XtraMessageBox.Show(
                    mensaje,
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

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

        private void cbxID_SEGMENTO_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void cbxID_SEGMENTO_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbxID_SEGMENTO.SelectedValue != null &&
                    int.TryParse(cbxID_SEGMENTO.SelectedValue.ToString(), out int idSegmento))
            {
                CargarSegmentoDt(idSegmento);
            }

        }
    }


}
