using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SistemaContable.DAL;
using SistemaContable.RP.Partidas;
using SistemaContable.UI.Helpers;
using SistemaContable.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Proveedores
{
    public partial class frmProvisionDiaria : Form, IRefrescable
    {
        private readonly DALBase _dal = new DALBase();
        private long _IdPartidaExistente = 0;
        public frmProvisionDiaria()
        {
            InitializeComponent();
        }

        private void frmProvisionDiaria_Load(object sender, EventArgs e)
        {            
            InicializarDatos();
            InicializarGrids();
            CargarGrids();

            dateEdit1.Leave += (s, ev) => CargarGrids();
            cbxTIPO_PROVEEDOR.SelectionChangeCommitted += (s, ev) => CargarGrids();
            dateEdit1.EditValueChanged += (s, ev) => CargarGrids();
        }

        private void InicializarDatos()
        {
            dateEdit1.DateTime = DateTime.Today;
            CargarTiposProveedor();
            AsignarConcepto();
        }       

        private void AsignarConcepto()
        {           
            txtCONCEPTO_PARTIDA.Text = "PARTIDA DEL DIA " + dateEdit1.Text;
            AsignarNumeroPartida();
        }

        private void AsignarNumeroPartida()
        {
            var infoPartida = NumeradorPartidaHelper.Consultar("DI", dateEdit1.DateTime.Year, dateEdit1.DateTime.Month);
            txtNUMERO_PARTIDA.Text = infoPartida.NumSiguienteFormateado;
        }

        private void CargarTiposProveedor()
        {
            var dt = new DataTable();
            dt.Columns.Add("CODIGO", typeof(string));
            dt.Columns.Add("DESCRIPCION", typeof(string));
            dt.Rows.Add("T", "[TODOS]");
            dt.Rows.Add("P", "PROVEEDORES");
            dt.Rows.Add(Configuracion.CodigoCCJIBOA, "CENTRO DE SERVICIOS JIBOA S.A. DE C.V.");
            dt.Rows.Add(Configuracion.CodigoHIBRONSA, "HIERROS Y BRONCES S.A. DE C.V.");

            cbxTIPO_PROVEEDOR.DataSource = dt;
            cbxTIPO_PROVEEDOR.DisplayMember = "DESCRIPCION";
            cbxTIPO_PROVEEDOR.ValueMember = "CODIGO";
            cbxTIPO_PROVEEDOR.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxTIPO_PROVEEDOR.SelectedIndex = 0;
        }

        private void InicializarGrids()
        {
            // gridControl1 (arriba) -> SIN provisión
            ConfigurarGrid(gridControl1, gridView1);
            gridControl1.Dock = DockStyle.Fill;

            // gridControl2 (abajo) -> CON provisión
            ConfigurarGrid(gridControl2, gridView2);
            gridControl2.Dock = DockStyle.Fill;
        }

        private void ConfigurarGrid(GridControl grid, GridView view)
        {
            // Mostrar el caption del grid como encabezado azul con texto blanco
            grid.UseEmbeddedNavigator = false;
            //grid.ViewCaptionHeight = 28;
            grid.ShowOnlyPredefinedDetails = true;            
            view.Appearance.Row.ForeColor = Color.Black;
            view.Appearance.Row.Options.UseForeColor = true;            
            view.Appearance.FocusedRow.ForeColor = Color.Black;
            view.Appearance.FocusedRow.Options.UseForeColor = true;            
            view.Appearance.HideSelectionRow.ForeColor = Color.Black;
            view.Appearance.HideSelectionRow.Options.UseForeColor = true;
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsBehavior.Editable = false;
            view.OptionsSelection.MultiSelect = false;
            view.OptionsSelection.EnableAppearanceFocusedRow = true;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;

            view.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            view.Appearance.HeaderPanel.Options.UseFont = true;
            view.Appearance.Row.Font = new Font("Segoe UI", 9f);
            view.Appearance.Row.Options.UseFont = true;
        }

        private void CargarGrids()
        {
            btnGenerarPartida.Enabled = false; 
            string tipoProveedor = cbxTIPO_PROVEEDOR.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(tipoProveedor)) return;

            if (dateEdit1.EditValue == null || dateEdit1.EditValue == DBNull.Value)
            {
                gridControl1.DataSource = null;
                gridControl2.DataSource = null;
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                // gridControl1 (arriba) -> SIN provisión
                DataTable dtSinProvision = _dal.EjecutarConsulta(
                    "SP_CREDITO_FISCAL_LISTAR_PROVISIONADOS",
                    new
                    {
                        ACCION = "LISTAR_SIN_PROVISION",
                        FECHA = dateEdit1.DateTime,
                        TIPO_PROVEEDOR = tipoProveedor,
                        CODIGO_CCJIBOA = Configuracion.CodigoCCJIBOA,
                        CODIGO_HIBRONSA = Configuracion.CodigoHIBRONSA
                    });

                gridControl1.DataSource = dtSinProvision;
                
                // gridControl2 (abajo) -> CON provisión
                DataTable dtConProvision = _dal.EjecutarConsulta(
                    "SP_CREDITO_FISCAL_LISTAR_PROVISIONADOS",
                    new
                    {
                        ACCION = "LISTAR_CON_PROVISION",
                        FECHA = dateEdit1.DateTime,
                        TIPO_PROVEEDOR = tipoProveedor,
                        CODIGO_CCJIBOA = Configuracion.CodigoCCJIBOA,
                        CODIGO_HIBRONSA = Configuracion.CodigoHIBRONSA
                    });

                gridControl2.DataSource = dtConProvision;

                VerificarPartidaExistente();


                if (cbxTIPO_PROVEEDOR.SelectedIndex == 0 && dtSinProvision.Rows.Count == 0)
                    btnGenerarPartida.Enabled = true;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los documentos:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Verifica si ya existe una partida generada para la fecha y tipo actuales.
        /// Si existe, asigna el número formateado al txtNUMERO_PARTIDA y habilita el
        /// botón de imprimir. Si no existe, deja el número sugerido (siguiente correlativo).
        /// </summary>
        private void VerificarPartidaExistente()
        {
            try
            {
                DataTable dt = _dal.EjecutarConsulta(
                    "SP_CREDITO_FISCAL_PROVISION",
                    new
                    {
                        ACCION = "OBTENER_PARTIDA_POR_FECHA",
                        FECHA_PARTIDA = dateEdit1.DateTime,
                        TIPO_PARTIDA = txtTIPO_PARTIDA.Text.Trim()
                    });

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Ya existe partida generada
                    var fila = dt.Rows[0];
                    string numeroFormateado = fila["NID_PARTIDA"]?.ToString() ?? "";

                    _IdPartidaExistente = Convert.ToInt64(fila["ID_PARTIDA"]);
                    txtNUMERO_PARTIDA.Text = numeroFormateado;
                    btnImprimirPartida.Enabled = true;
                }
                else
                {
                    // No existe aún: sugerir el siguiente correlativo
                    _IdPartidaExistente = 0;
                    AsignarNumeroPartida();
                    btnImprimirPartida.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                // No bloqueamos la carga por un error acá
                AsignarNumeroPartida();
                btnImprimirPartida.Enabled = false;

                System.Diagnostics.Debug.WriteLine(
                    $"Error al verificar partida existente: {ex.Message}");
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateEdit1_Leave(object sender, EventArgs e)
        {
            AsignarConcepto(); 
        }

        private void EnterComoTab(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
               
        }

        public void Refrescar()
        {
            CargarGrids();
        }

        private void btnGenerarPartida_Click(object sender, EventArgs e)
        {
            // ============================================================
            // Validaciones
            // ============================================================
            if (dateEdit1.EditValue == null || dateEdit1.EditValue == DBNull.Value)
            {
                XtraMessageBox.Show(
                    "Debe seleccionar una fecha.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateEdit1.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTIPO_PARTIDA.Text))
            {
                XtraMessageBox.Show(
                    "Debe indicar el tipo de partida.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTIPO_PARTIDA.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCONCEPTO_PARTIDA.Text))
            {
                XtraMessageBox.Show(
                    "Debe indicar el concepto de la partida.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCONCEPTO_PARTIDA.Focus();
                return;
            }

            // ============================================================
            // Confirmación
            // ============================================================
            var resp = XtraMessageBox.Show(
                $"¿Está seguro de enviar la provisión a contabilidad para la fecha " +
                $"{dateEdit1.DateTime:dd/MM/yyyy}?\n\n" +
                "Esta acción generará la partida contable correspondiente.",
                "Confirmar envío",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (resp != DialogResult.Yes) return;

            // ============================================================
            // Ejecutar el SP
            // ============================================================
            try
            {
                Cursor = Cursors.WaitCursor;
                btnGenerarPartida.Enabled = false;

                object resultado = _dal.EjecutarEscalar("SP_CREDITO_FISCAL_PROVISION", new
                {
                    ACCION = "ENVIAR_PROVISION_CONTABILIDAD",
                    USUARIO = Configuracion.UsuarioActual,
                    TIPO_PARTIDA = txtTIPO_PARTIDA.Text.Trim(),
                    FECHA_PARTIDA = dateEdit1.DateTime,
                    CONCEPTO_PARTIDA = txtCONCEPTO_PARTIDA.Text.Trim()
                });

                int idPartidaGenerada = 0;
                if (resultado != null && resultado != DBNull.Value)
                    int.TryParse(resultado.ToString(), out idPartidaGenerada);

                if (idPartidaGenerada > 0)
                {
                    XtraMessageBox.Show(
                   "Provisión enviada a contabilidad.",
                   "Enviado",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);


                   
                   btnImprimirPartida.Enabled = true;                   
                   

                    // Recargar los grids para reflejar el cambio de estado
                    CargarGrids();
                }
                else
                {
                    XtraMessageBox.Show(
                      "Error al enviar partida a contabilidad.",
                      "Enviado",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
                }

               
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "Error al enviar la provisión:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnImprimirPartida_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var reporte = new RptPartida_Movimiento { _ID_PARTIDA = _IdPartidaExistente };
                reporte.MostrarPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al imprimir:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}
