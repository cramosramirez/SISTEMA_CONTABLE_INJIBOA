using ClosedXML.Excel;
using DevExpress.XtraReports.UI;
using SistemaContable.DAL;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SistemaContable.RP.Helpers;
using System.Collections.Generic;

namespace SistemaContable.RP
{
    // ============================================================
    // NUEVO: enum para controlar el modo de salida
    // ============================================================
    /// <summary>
    /// Modo de salida del reporte al llamar Ejecutar().
    /// </summary>
    public enum SalidaReporte
    {
        /// <summary>Muestra el preview visual del reporte (default).</summary>
        Pantalla,

        /// <summary>Exporta a PDF temporal y lo abre con el visor del sistema.</summary>
        PDF,

        /// <summary>Exporta el DataSource a Excel pidiendo ruta con SaveFileDialog.</summary>
        Excel
    }

    public class ReporteBase : XtraReport
    {
        public Dictionary<string, string> RenombrarColumnasExcel { get; set; }
        private TopMarginBand topMarginBand1;
        private DetailBand detailBand1;
        private BottomMarginBand bottomMarginBand1;
        protected readonly DALBase _dal = new DALBase();

        // ============================================================
        // NUEVAS propiedades
        // ============================================================

        /// <summary>
        /// Controla el modo de salida del reporte al llamar Ejecutar().
        /// </summary>
        public SalidaReporte Salida { get; set; } = SalidaReporte.Pantalla;

        /// <summary>
        /// Nombre sugerido (sin extensión) para los archivos de exportación.
        /// Si no se asigna, se genera "Reporte_yyyyMMdd_HHmmss".
        /// </summary>
        public string NombreArchivoSugerido { get; set; }

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
        public virtual void CargarDatos() { }

        // ============================================================
        // NUEVO: método único de ejecución
        // Rutea según el valor de la propiedad Salida.
        // Las excepciones se propagan para que las maneje el llamador.
        // ============================================================
        /// <summary>
        /// Ejecuta el reporte según el valor de la propiedad Salida.
        /// Reemplaza las llamadas directas a MostrarPreview() cuando se
        /// quiere que el modo (pantalla / PDF / Excel) sea configurable.
        /// </summary>
        public virtual string Ejecutar()
        {
            switch (Salida)
            {
                case SalidaReporte.Pantalla:
                    // MostrarPreview() ya llama CargarDatos() internamente
                    MostrarPreview();
                    return null;

                case SalidaReporte.PDF:
                    return EjecutarPDF();

                case SalidaReporte.Excel:
                    return EjecutarExcel();

                default:
                    throw new NotImplementedException(
                        $"La salida '{Salida}' no está implementada.");
            }
        }

        // ============================================================
        // NUEVO: Salida PDF (archivo temporal + abrir con visor del sistema)
        // ============================================================
        private string EjecutarPDF()
        {
            CargarDatos();

            string nombreBase = ObtenerNombreArchivoBase();
            string rutaPdf = Path.Combine(Path.GetTempPath(), nombreBase + ".pdf");

            // XtraReport ya tiene este método nativo
            this.ExportToPdf(rutaPdf);

            // Abrir con el visor por defecto del sistema
            var psi = new ProcessStartInfo
            {
                FileName = rutaPdf,
                UseShellExecute = true
            };
            Process.Start(psi);

            return rutaPdf;
        }

        // ============================================================
        // NUEVO: Salida Excel (exporta el DataSource en una sola hoja)
        // ============================================================
        private string EjecutarExcel()
        {
            CargarDatos();

            DataTable dt = this.DataSource as DataTable;

            if (dt == null || dt.Rows.Count == 0)
                throw new InvalidOperationException(
                    "El reporte no tiene datos para exportar.");

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files|*.xlsx";
                sfd.Title = "Guardar archivo Excel";
                sfd.FileName = ObtenerNombreArchivoBase() + ".xlsx";

                if (sfd.ShowDialog() != DialogResult.OK) return null;

                using (var wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Datos");
                    ws.ExportarDataTable(dt, renombrarColumnas: RenombrarColumnasExcel);
                    wb.SaveAs(sfd.FileName);
                }

                var psi = new ProcessStartInfo
                {
                    FileName = sfd.FileName,
                    UseShellExecute = true
                };
                Process.Start(psi);

                return sfd.FileName;
            }
        }

        /// <summary>
        /// Devuelve el nombre sugerido para archivos de exportación.
        /// Si NombreArchivoSugerido no fue asignado, genera uno con timestamp.
        /// </summary>
        private string ObtenerNombreArchivoBase()
        {
            return string.IsNullOrWhiteSpace(NombreArchivoSugerido)
                ? "Reporte_" + DateTime.Now.ToString("yyyyMMdd_HHmmss")
                : NombreArchivoSugerido;
        }

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