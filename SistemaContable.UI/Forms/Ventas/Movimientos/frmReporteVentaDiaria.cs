using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using SistemaContable.DAL;
using SistemaContable.RP.Ventas.Movimientos;
using System;
using System.Data;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Ventas.Movimientos
{
    public partial class frmReporteVentaDiaria : Form
    {
        private const string ConsultaCentrosCosto = @"
SELECT ID_CENTRO, NOMBRE AS NOMBRE_CENTRO
FROM EDTE.CENTROCOSTO
ORDER BY NOMBRE;";

        private readonly DALBase _dal = new DALBase();

        public frmReporteVentaDiaria()
        {
            InitializeComponent();
        }

        private void frmReporteVentaDiaria_Load(object sender, EventArgs e)
        {
            dteFechaDesde.DateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dteFechaHasta.DateTime = DateTime.Today;
            CargarCentrosCosto();
        }

        private void CargarCentrosCosto()
        {
            try
            {
                DataTable centros = _dal.EjecutarConsultaSql(ConsultaCentrosCosto);
                cboCentroCosto.DataSource = centros;
                cboCentroCosto.ValueMember = "ID_CENTRO";
                cboCentroCosto.DisplayMember = "NOMBRE_CENTRO";
                cboCentroCosto.SelectedIndex = centros.Rows.Count > 0 ? 0 : -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("No fue posible cargar los centros de costo.\n\n" + ex.Message,
                    "Centros de costo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCriterios(out int idCentro)
        {
            idCentro = 0;
            if (dteFechaDesde.EditValue == null || dteFechaHasta.EditValue == null)
            {
                XtraMessageBox.Show("Seleccione las dos fechas.", "Datos requeridos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dteFechaDesde.DateTime.Date > dteFechaHasta.DateTime.Date)
            {
                XtraMessageBox.Show("La fecha desde no puede ser mayor que la fecha hasta.",
                    "Rango de fechas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dteFechaDesde.Focus();
                return false;
            }

            if (cboCentroCosto.SelectedValue == null ||
                !int.TryParse(cboCentroCosto.SelectedValue.ToString(), out idCentro) || idCentro <= 0)
            {
                XtraMessageBox.Show("Seleccione un centro de costo.", "Datos requeridos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCentroCosto.Focus();
                return false;
            }

            return true;
        }

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            if (!ValidarCriterios(out int idCentro)) return;

            try
            {
                btnVistaPrevia.Enabled = false;
                Cursor = Cursors.WaitCursor;
                var reporte = new RptDetalleVentasDiarasdtResumen(
                    dteFechaDesde.DateTime.Date, dteFechaHasta.DateTime.Date, idCentro);
                new ReportPrintTool(reporte).ShowPreviewDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("No fue posible generar el reporte.\n\n" + ex.Message,
                    "Reporte de venta diaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnVistaPrevia.Enabled = true;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
