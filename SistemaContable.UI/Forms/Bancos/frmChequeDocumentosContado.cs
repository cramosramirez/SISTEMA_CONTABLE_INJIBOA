using DevExpress.XtraEditors;
using SistemaContable.DAL;
using SistemaContable.UI.Forms.Proveedores;
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

namespace SistemaContable.UI.Forms.Bancos
{
    public partial class frmChequeDocumentosContado : Form
    {
        private readonly DALBase _dal = new DALBase();

        private DataTable _dtDocumentos;
        public string UidEnlaceCheque { get; set; }
        public DataTable TotalesAcumulados { get; private set; }
        public decimal TotalNetoAPagar { get; private set; }

        public frmChequeDocumentosContado()
        {
            InitializeComponent();
        }

        private void frmChequeDocumentosContado_Load(object sender, EventArgs e)
        {
            //FormHelper.Inicializar(this);

            if (string.IsNullOrWhiteSpace(UidEnlaceCheque))
            {
                XtraMessageBox.Show(
                    "No se indicó el identificador de enlace del cheque.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            InicializarGrid();            
            CargarDocumentos();
        }      

        private void InicializarGrid()
        {
            gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.OptionsSelection.MultiSelect = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = true;
            // ✅ Activar la fila de totales al pie del grid
            gridView1.OptionsView.ShowFooter = true;
            // Estilo: fuente y colores
            gridView1.Appearance.Row.ForeColor = Color.Black;
            gridView1.Appearance.Row.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            gridView1.Appearance.Row.Options.UseForeColor = true;
            gridView1.Appearance.Row.Options.UseFont = true;

            gridView1.Appearance.FocusedRow.BackColor = Color.FromArgb(204, 229, 255);
            gridView1.Appearance.FocusedRow.ForeColor = Color.Black;
            gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            gridView1.Appearance.FocusedRow.Options.UseForeColor = true;

            gridView1.Appearance.HideSelectionRow.BackColor = Color.FromArgb(204, 229, 255);
            gridView1.Appearance.HideSelectionRow.ForeColor = Color.Black;
            gridView1.Appearance.HideSelectionRow.Options.UseBackColor = true;
            gridView1.Appearance.HideSelectionRow.Options.UseForeColor = true;

            gridView1.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            gridView1.Appearance.HeaderPanel.Options.UseFont = true;

            // ✅ Estilo llamativo para la fila de totales
            gridView1.Appearance.FooterPanel.BackColor = Color.FromArgb(30, 64, 175);   // azul oscuro
            gridView1.Appearance.FooterPanel.ForeColor = Color.White;
            gridView1.Appearance.FooterPanel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            gridView1.Appearance.FooterPanel.Options.UseBackColor = true;
            gridView1.Appearance.FooterPanel.Options.UseForeColor = true;
            gridView1.Appearance.FooterPanel.Options.UseFont = true;
            gridView1.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            //Cursor de mano para incdicar al usuario que puede seleccionar
            gridView1.MouseMove += (s, ev) =>
            {
                var hitInfo = gridView1.CalcHitInfo(ev.Location);
                gridControl1.Cursor = hitInfo.InRow ? Cursors.Hand : Cursors.Default;
            };

            gridView1.CustomDrawFooter += (s, ev) =>
            {
                using (var brush = new SolidBrush(Color.FromArgb(30, 64, 175)))
                {
                    ev.Graphics.FillRectangle(brush, ev.Bounds);
                }
            };

            gridView1.CustomDrawFooterCell += (s, ev) =>
            {
                using (var brush = new SolidBrush(Color.FromArgb(30, 64, 175)))
                {
                    ev.Graphics.FillRectangle(brush, ev.Bounds);
                }

                // Dibujar el texto del sumario en blanco
                using (var sf = new StringFormat
                {
                    Alignment = StringAlignment.Far,    // derecha
                    LineAlignment = StringAlignment.Center
                })
                using (var brush = new SolidBrush(Color.White))
                using (var font = new Font("Segoe UI", 9f, FontStyle.Bold))
                {
                    // El primer footer (TOTALES:) lo alineamos a la izquierda
                    if (ev.Column.FieldName == "COD_GENERACION")
                        sf.Alignment = StringAlignment.Near;

                    ev.Graphics.DrawString(ev.Info.DisplayText, font, brush,
                        ev.Bounds, sf);
                }

                ev.Handled = true;   // ← evita que DevExpress lo vuelva a pintar
            };

            gridView1.DoubleClick += gridView1_DoubleClick;

            // ✅ Sumario en columnas numéricas (totales en el footer)
            AgregarSumarioTotal(colGRAVADA);
            AgregarSumarioTotal(colEXENTA);
            AgregarSumarioTotal(colNO_SUJETA);
            AgregarSumarioTotal(colPERCEPCION);
            AgregarSumarioTotal(colIVA);
            AgregarSumarioTotal(colFOVIAL);
            AgregarSumarioTotal(colCOTRANS);
            AgregarSumarioTotal(colTOTAL);
            AgregarSumarioTotal(colRENTA);
            AgregarSumarioTotal(colRETENCION_IVA);
            AgregarSumarioTotal(colSALDO);

            // ✅ Label "TOTALES" en la primera columna del footer
            colCOD_GENERACION.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            colCOD_GENERACION.SummaryItem.DisplayFormat = "TOTALES:";

            // Formato de fecha
            colFECHA_RECIBIDO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            colFECHA_RECIBIDO.DisplayFormat.FormatString = "dd/MM/yyyy";
        }

        private void AgregarSumarioTotal(DevExpress.XtraGrid.Columns.GridColumn col)
        {
            col.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            col.SummaryItem.DisplayFormat = "{0:N2}";
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) return;

            var idCcfObj = gridView1.GetFocusedRowCellValue("ID_CCF_COMPRA");
            if (idCcfObj == null || idCcfObj == DBNull.Value) return;
            int idCcfCompra = Convert.ToInt32(idCcfObj);
            try
            {
                using (var frm = new frmDocumentoCompra())
                {
                    frm.EsContado = true;
                    frm.UidEnlaceCheque = UidEnlaceCheque;
                    frm.IdCcfCompra = idCcfCompra;   // ← clave: abre en modo edición
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        CargarDocumentos();   // refrescar grid por si se modificó
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "Error al abrir el documento:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // Carga del grid desde el SP
        // ============================================================
        private void CargarDocumentos()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _dtDocumentos = _dal.EjecutarConsulta("SP_CREDITO_FISCAL_COMPRA",
                    new
                    {
                        ACCION = "LISTAR_POR_UID",
                        UID_ENLACE_CHEQUE = UidEnlaceCheque
                    });

                gridControl1.DataSource = _dtDocumentos;
                this.BeginInvoke(new Action(() => gridView1.BestFitColumns()));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "Error al cargar los documentos:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frm = new frmDocumentoCompra())
                {
                    // Propiedades nuevas que se implementarán en frmDocumentoCompra
                    frm.EsContado = true;
                    frm.UidEnlaceCheque = UidEnlaceCheque;

                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        // Refrescar el grid para que aparezca el CCF recién creado
                        CargarDocumentos();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "Error al abrir el formulario de compra:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRetornar_Click(object sender, EventArgs e)
        {
            // Permitimos retornar aunque no haya documentos, para que
            // el usuario pueda salir sin agregar nada si se equivocó al
            // abrir el form. El control de validación lo hace frmCheques.
            try
            {
                Cursor = Cursors.WaitCursor;

                TotalesAcumulados = _dal.EjecutarConsulta("SP_CREDITO_FISCAL_COMPRA",
                    new
                    {
                        ACCION = "OBTENER_TOTALES_POR_UID",
                        UID_ENLACE_CHEQUE = UidEnlaceCheque
                    });

                if (TotalesAcumulados != null && TotalesAcumulados.Rows.Count > 0)
                {
                    var fila = TotalesAcumulados.Rows[0];
                    TotalNetoAPagar = fila["TOTAL_NETO_A_PAGAR"] == DBNull.Value
                        ? 0m
                        : Convert.ToDecimal(fila["TOTAL_NETO_A_PAGAR"]);
                }
                else
                {
                    TotalNetoAPagar = 0m;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "Error al obtener los totales:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}
