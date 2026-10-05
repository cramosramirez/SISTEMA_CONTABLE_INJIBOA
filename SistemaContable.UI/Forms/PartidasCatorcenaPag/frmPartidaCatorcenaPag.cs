using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using SistemaContable.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.PartidasCatorcenaPag
{
    public partial class frmPartidaCatorcenaPag : XtraForm
    {
        private const int EmpresaPredeterminada = 1;
        private readonly DALBase _dal = new DALBase();

        public frmPartidaCatorcenaPag()
        {
            InitializeComponent();
            ConfigurarFormulario();
            CargarEmpresas();
        }

        private void ConfigurarFormulario()
        {
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowAutoFilterRow = false;
            gridView1.OptionsView.ShowFooter = true;
            gridView1.OptionsView.ColumnAutoWidth = false;
            lblEstado.Text = "Seleccione la catorcena y complete la cuenta de contrapartida.";
            LimpiarResultado();
        }

        private void CargarEmpresas()
        {
            try
            {
                DataTable empresas = _dal.EjecutarConsultaSql($@"
                    SELECT NUMEMPR, NOMBRE AS NOMBRE_EMPRESA
                    FROM PAG.dbo.GEN_EMPRESAS
                    WHERE NUMEMPR = {EmpresaPredeterminada}");

                cbEmpresa.DataSource = empresas;
                cbEmpresa.ValueMember = "NUMEMPR";
                cbEmpresa.DisplayMember = "NOMBRE_EMPRESA";

                if (empresas.Rows.Count > 0)
                    cbEmpresa.SelectedIndex = 0;
                else
                    cbEmpresa.SelectedIndex = -1;

                CargarCatorcenas();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("No fue posible cargar la empresa.\n" + ex.Message,
                    "Partida de catorcena", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCatorcenas()
        {
            cbCatorcena.DataSource = null;
            if (!ObtenerEmpresaSeleccionada(out int numEmpresa))
                return;

            try
            {
                DataTable catorcenas = _dal.EjecutarConsultaSql($@"
                    SELECT TOP 2
                        IDCATAGR,
                        FECHA_INICIO,
                        FECHA_FINAL,
                        CASE WHEN ESTADO = 'ACT' THEN 1 ELSE 0 END AS ESTADO,
                        CONCAT('Catorcena ', IDCATAGR, ' | ',
                               CONVERT(VARCHAR(10), FECHA_INICIO, 103), ' - ',
                               CONVERT(VARCHAR(10), FECHA_FINAL, 103),
                               CASE WHEN ESTADO = 'ACT' THEN ' | Activa' ELSE ' | Inactiva' END) AS DESCRIPCION
                    FROM PAG.dbo.PAG_CATORCENAS_AGR
                    WHERE NUMEMPR = {EmpresaPredeterminada}
                    ORDER BY IDCATAGR DESC");

                cbCatorcena.DataSource = catorcenas;
                cbCatorcena.ValueMember = "IDCATAGR";
                cbCatorcena.DisplayMember = "DESCRIPCION";
                cbCatorcena.SelectedIndex = catorcenas.Rows.Count > 0 ? 0 : -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("No fue posible cargar las catorcenas.\n" + ex.Message,
                    "Partida de catorcena", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ObtenerEmpresaSeleccionada(out int numEmpresa)
        {
            return int.TryParse(Convert.ToString(cbEmpresa.SelectedValue), out numEmpresa);
        }

        private bool ValidarCriterios(out int numEmpresa, out int idCatorcena)
        {
            numEmpresa = idCatorcena = 0;

            if (!ObtenerEmpresaSeleccionada(out numEmpresa))
            {
                XtraMessageBox.Show("Seleccione una empresa.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbEmpresa.Focus();
                return false;
            }

            if (!int.TryParse(Convert.ToString(cbCatorcena.SelectedValue), out idCatorcena))
            {
                XtraMessageBox.Show("Seleccione una catorcena.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbCatorcena.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCuenta.Text))
            {
                XtraMessageBox.Show("Ingrese la cuenta de contrapartida.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCuenta.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                XtraMessageBox.Show("Ingrese el concepto de la contrapartida.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            return true;
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            if (!ValidarCriterios(out int numEmpresa, out int idCatorcena))
                return;

            try
            {
                Cursor = Cursors.WaitCursor;
                btnProcesar.Enabled = false;
                lblEstado.Text = "Preparando la partida...";

                DataTable detalle = _dal.EjecutarConsulta(
                    "[CONTA].[CRE_PARTIDA_PAG_PLANILLA_CATORCENA]", new
                    {
                        NUMEMPR = numEmpresa,
                        IDCATAGR = idCatorcena,
                        CUENTA = txtCuenta.Text.Trim(),
                        NOMBRE = txtNombre.Text.Trim()
                    });

                gridControl1.DataSource = detalle;
                FormatearColumnas();
                MostrarTotales(detalle);

                if (detalle.Rows.Count == 0)
                {
                    lblEstado.Text = "La catorcena no produjo movimientos contables.";
                    XtraMessageBox.Show(lblEstado.Text, "Partida de catorcena",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                LimpiarResultado();
                lblEstado.Text = "No fue posible preparar la partida.";
                XtraMessageBox.Show(lblEstado.Text + "\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnProcesar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void MostrarTotales(DataTable detalle)
        {
            decimal totalCargo = 0m;
            decimal totalAbono = 0m;

            foreach (DataRow fila in detalle.Rows)
            {
                totalCargo += fila["TOTAL_CARGO"] == DBNull.Value ? 0m : Convert.ToDecimal(fila["TOTAL_CARGO"]);
                totalAbono += fila["TOTAL_ABONO"] == DBNull.Value ? 0m : Convert.ToDecimal(fila["TOTAL_ABONO"]);
            }

            decimal diferencia = totalCargo - totalAbono;
            txtTotalCargo.Text = totalCargo.ToString("N2");
            txtTotalAbono.Text = totalAbono.ToString("N2");
            txtDiferencia.Text = diferencia.ToString("N2");

            bool cuadrada = Math.Abs(diferencia) < 0.01m;
            txtDiferencia.ForeColor = cuadrada ? Color.DarkGreen : Color.DarkRed;
            lblEstado.Text = cuadrada
                ? "Vista previa preparada correctamente. La partida está cuadrada."
                : "La partida no está cuadrada. Revise los cargos y abonos antes de continuar.";

            if (!cuadrada)
                XtraMessageBox.Show(lblEstado.Text, "Validación contable",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void FormatearColumnas()
        {
            gridView1.PopulateColumns();
            ConfigurarColumna("FECHA_MOV", "Fecha", 90, "dd/MM/yyyy", FormatType.DateTime);
            ConfigurarColumna("NUMPPD", "Catorcena", 75);
            ConfigurarColumna("IDACT", "Actividad", 70);
            ConfigurarColumna("CUENTA", "Cuenta", 145);
            ConfigurarColumna("CONCEPTO", "Concepto", 370);
            ConfigurarColumna("TOTAL_CARGO", "Cargo", 110, "n2", FormatType.Numeric, true);
            ConfigurarColumna("TOTAL_ABONO", "Abono", 110, "n2", FormatType.Numeric, true);
            ConfigurarColumna("TPMOV", "Tipo", 55);
        }

        private void ConfigurarColumna(string campo, string titulo, int ancho,
            string formato = null, FormatType tipoFormato = FormatType.None, bool sumar = false)
        {
            GridColumn columna = gridView1.Columns[campo];
            if (columna == null) return;

            columna.Caption = titulo;
            columna.Width = ancho;
            columna.OptionsColumn.AllowEdit = false;
            if (!string.IsNullOrWhiteSpace(formato))
            {
                columna.DisplayFormat.FormatType = tipoFormato;
                columna.DisplayFormat.FormatString = formato;
            }
            if (sumar)
            {
                columna.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                columna.SummaryItem.DisplayFormat = "{0:n2}";
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtCuenta.Text = string.Empty;
            txtNombre.Text = string.Empty;
            if (cbCatorcena.Items.Count > 0)
                cbCatorcena.SelectedIndex = 0;
            LimpiarResultado();
            lblEstado.Text = "Seleccione la catorcena y complete la cuenta de contrapartida.";
            txtCuenta.Focus();
        }

        private void LimpiarResultado()
        {
            gridControl1.DataSource = null;
            txtTotalCargo.Text = "0.00";
            txtTotalAbono.Text = "0.00";
            txtDiferencia.Text = "0.00";
            txtDiferencia.ForeColor = Color.DarkGreen;
        }

        private void cbEmpresa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargarCatorcenas();
            LimpiarResultado();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
