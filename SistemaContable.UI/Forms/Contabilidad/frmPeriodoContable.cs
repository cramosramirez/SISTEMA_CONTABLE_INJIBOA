using DevExpress.XtraEditors;
using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Contabilidad
{
    public partial class frmPeriodoContable : Form
    {
        // Valores seleccionados (se leen desde afuera después del ShowDialog)
        public int AnioSeleccionado { get; private set; }
        public int MesSeleccionado { get; private set; }

        public frmPeriodoContable()
        {
            InitializeComponent();
        }

        private void frmPeriodoContable_Load(object sender, EventArgs e)
        {            
            ConfigurarAnio();
            ConfigurarLookupMes();
            lkpMes.EditValue = Configuracion.PeriodoMes;
        }

        private void ConfigurarAnio()
        {
            spnAnio.Properties.IsFloatValue = false;
            spnAnio.Properties.MinValue = 2000;
            spnAnio.Properties.MaxValue = 2100;
            spnAnio.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            spnAnio.Properties.Mask.EditMask = "####";
            spnAnio.Properties.Mask.UseMaskAsDisplayFormat = true;
            spnAnio.Properties.DisplayFormat.FormatString = "0";
            spnAnio.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            spnAnio.Properties.EditFormat.FormatString = "0";
            spnAnio.Value = Configuracion.PeriodoAnio;
        }

        private void ConfigurarLookupMes()
        {
            // Armar el DataTable con los meses
            var dtMeses = new DataTable();
            dtMeses.Columns.Add("NUMERO", typeof(int));
            dtMeses.Columns.Add("NOMBRE", typeof(string));

            var culturaEs = new System.Globalization.CultureInfo("es-ES");
            for (int m = 1; m <= 12; m++)
            {
                string nombre = culturaEs.DateTimeFormat.GetMonthName(m).ToUpper();                
                dtMeses.Rows.Add(m, nombre);
            }

            // Configurar el LookUpEdit
            lkpMes.Properties.DataSource = dtMeses;
            lkpMes.Properties.DisplayMember = "NOMBRE";
            lkpMes.Properties.ValueMember = "NUMERO";

            // Columnas visibles en el dropdown
            lkpMes.Properties.Columns.Clear();
            lkpMes.Properties.Columns.Add(
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NUMERO", "N°", 40));
            lkpMes.Properties.Columns.Add(
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NOMBRE", "Mes", 120));

            // Opciones del lookup
            lkpMes.Properties.NullText = "";
            lkpMes.Properties.ShowHeader = true;
            lkpMes.Properties.ShowFooter = false;
            lkpMes.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSearch;
            lkpMes.Properties.AutoSearchColumnIndex = 1;   // busca por nombre del mes
            lkpMes.Properties.DropDownRows = 12;           // mostrar los 12 de una
            lkpMes.Properties.TextEditStyle =
                DevExpress.XtraEditors.Controls.TextEditStyles.Standard;   // permite tipear     
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close(); 
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Validar año
            if (spnAnio.EditValue == null || spnAnio.Value <= 0)
            {
                XtraMessageBox.Show(
                    "Debe ingresar un año válido.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spnAnio.Focus();
                return;
            }

            // Validar mes
            if (lkpMes.EditValue == null || lkpMes.EditValue == DBNull.Value)
            {
                XtraMessageBox.Show(
                    "Debe seleccionar un mes.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lkpMes.Focus();
                return;
            }

            AnioSeleccionado = Convert.ToInt32(spnAnio.Value);
            MesSeleccionado = Convert.ToInt32(lkpMes.EditValue);

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
