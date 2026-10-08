using SistemaContable.RP.Partidas;
using SistemaContable.UI.Helpers;
using System;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Contabilidad
{
    public partial class frmImpresion : DevExpress.XtraEditors.XtraForm
    {
        public string _tipo { get; set; } = "";
        public string _TipoNombre { get; set; } = "";
        public string _numero { get; set; } = "";
        public int _anio { get; set; } = 0;
        public int _mes { get; set; } = 0;

        public frmImpresion()
        {
            InitializeComponent();
        }

        private void frmImpresion_Load(object sender, EventArgs e)
        {
            txtTipo.Text = _tipo;
            txtTipoNombre.Text = _TipoNombre;
            txtNumeroInicio.Text = _numero;
            txtNumeroFinal.Text = _numero;
            txtAnio.Text = _anio.ToString();
            txtMes.Text = _mes.ToString();
            ckBorrador.Checked = true;
            //lbPartida.Text = $"{_tipo}-{_numero}";
        }

        private void btPantalla_Click(object sender, EventArgs e)
        {
            // Vista previa
          

            try
            {
                Cursor = Cursors.WaitCursor;
                if (ckBorrador.Checked)
                {
                    var reporte = new RptPartida_Movimiento
                    {
                       
                        _TIPO = txtTipo.Text,
                        _ANIO = Convert.ToInt32(txtAnio.Text),
                        _MES = Convert.ToInt32(txtMes.Text),
                        _NDESDE = txtNumeroInicio.Text,
                        _NHASTA = txtNumeroFinal.Text
                    };

                    reporte.MostrarPreview();
                }
                if (ckFormal.Checked)
                {
                    var reporte = new RptPartida_Parciales
                    {
                        //_TIPO = txtTipo.Text,
                        //_ANIO = Convert.ToInt32(txtAnio.Text),
                        //_MES = Convert.ToInt32(txtMes.Text),
                        //_NDESDE = txtNumeroInicio.Text,
                        //_NHASTA = txtNumeroFinal.Text
                    };

                    reporte.MostrarPreview();
                }
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

        private void btImprimir_Click(object sender, EventArgs e)
        {
            string nidPartida =
                $"{_tipo}-{_anio}{_mes.ToString().PadLeft(2, '0')}-{_numero.PadLeft(6, '0')}";

            DialogResult resp = MessageBox.Show(
                $"¿Desea imprimir la partida {nidPartida}?",
                "Impresión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resp != DialogResult.Yes)
                return;

            // Aquí llamarás tu reporte:
            // var rpt = new RPT_PARTIDA();
            // rpt.CargarDatos(nidPartida);
            // rpt.Print();

            MessageBox.Show(
                "Documento enviado a impresión.",
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ckBorrador_CheckedChanged(object sender, EventArgs e)
        {
            if (ckBorrador.Checked)
            {
                ckFormal.Checked = false;
            }
        }

        private void ckFormal_CheckedChanged(object sender, EventArgs e)
        {
            if (ckFormal.Checked)
            {
                ckBorrador.Checked = false;
            }
        }

    }
}