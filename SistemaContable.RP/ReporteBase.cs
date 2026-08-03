using DevExpress.XtraReports.UI;
using SistemaContable.DAL;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SistemaContable.RP
{
    public class ReporteBase : XtraReport
    {
        private TopMarginBand topMarginBand1;
        private DetailBand detailBand1;
        private BottomMarginBand bottomMarginBand1;
        protected readonly DALBase _dal = new DALBase();

        protected ReporteBase()
        {
            // Configuración común a todos los reportes (opcional)
            this.PaperKind = System.Drawing.Printing.PaperKind.Letter;
        }

        /// <summary>
        /// Ejecuta un SP y devuelve un DataTable.
        /// </summary>
        protected DataTable EjecutarSP(string sp, object parametros)
        {
            return _dal.EjecutarConsulta(sp, parametros);
        }

       
        /// <summary>
        /// Cada reporte concreto implementa cómo cargar sus datos.
        /// </summary>
        public virtual void CargarDatos(){}

        /// <summary>
        /// Imprime mostrando el cuadro de diálogo para seleccionar impresora sin mostrar reporte
        /// </summary>
        public bool ImprimirConDialogo()
        {
            CargarDatos();
            var resultado = this.PrintDialog();
            return resultado == System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// Imprime directo a la impresora por defecto sin mostrar reporte
        /// </summary>
        public void ImprimirDirecto(string nombreImpresora = null)
        {
            CargarDatos();
            if (!string.IsNullOrWhiteSpace(nombreImpresora))
                this.PrinterName = nombreImpresora;
            this.Print();
        }

        /// <summary>
        /// Envia el reporte a un formulario en pantalla
        /// </summary>
        public void MostrarPreview()
        {
            CargarDatos();
            var timer = new Timer { Interval = 50 };
            timer.Tick += (s, e) =>
            {
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm.GetType().Name.Contains("PrintPreview"))
                    {
                        frm.WindowState = FormWindowState.Maximized;
                        ForzarIconosGrandes(frm);
                        timer.Stop();
                        timer.Dispose();
                        return;
                    }
                }
            };
            timer.Start();

            this.ShowPreview();
        }

        private void ForzarIconosGrandes(Form previewForm)
        {
            var barManagers = previewForm.GetType()
                .GetFields(System.Reflection.BindingFlags.NonPublic |
                           System.Reflection.BindingFlags.Instance)
                .Where(f => typeof(DevExpress.XtraBars.BarManager).IsAssignableFrom(f.FieldType))
                .Select(f => (DevExpress.XtraBars.BarManager)f.GetValue(previewForm))
                .Where(bm => bm != null);

            foreach (var bm in barManagers)
            {
                foreach (DevExpress.XtraBars.BarItem item in bm.Items)
                {
                    item.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
                }
                bm.ForceLinkCreate();
            }
        }

        private void InitializeComponent()
        {
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // detailBand1
            // 
            this.detailBand1.Name = "detailBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // ReporteBase
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.detailBand1,
            this.bottomMarginBand1});
            this.Version = "20.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}
