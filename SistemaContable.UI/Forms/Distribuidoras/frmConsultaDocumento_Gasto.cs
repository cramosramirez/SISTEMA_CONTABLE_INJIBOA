using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using SistemaContable.DAL;
using SistemaContable.UI.Interfaces;
using System;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SistemaContable.RP.Bancos.Proveedores;
using DevExpress.XtraEditors;

namespace SistemaContable.UI.Forms.Distribuidoras
{
    public partial class frmConsultaDocumento_Gasto : Form, IRefrescable
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtDetalle;
        public frmConsultaDocumento_Gasto()
        {
            InitializeComponent();
            riVerRVacio = new RepositoryItemButtonEdit();
            riVerRVacio.Buttons.Clear(); // sin botón visible
            riVerRVacio.TextEditStyle = TextEditStyles.DisableTextEditor;
            riVerRVacio.ReadOnly = true;
            gridControl1.RepositoryItems.Add(riVerRVacio);
        }

        private void frmConsultaDocumento_Gasto_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarDatos();
        }
        private void ConfigurarGrid()
        {
            // --- Vista general ---
            gridControl1.ForceInitialize();
            gvDetalle.OptionsView.ShowGroupPanel = false;   // panel de agrupación visible
            gvDetalle.OptionsView.ShowAutoFilterRow = true;
            gvDetalle.OptionsBehavior.AutoExpandAllGroups = true;
            gvDetalle.OptionsBehavior.Editable = true;   // necesario para los botones por fila

            // --- Buscador global ---
            gvDetalle.OptionsFind.AlwaysVisible = true;
            gvDetalle.OptionsFind.FindNullPrompt = "Introduzca el texto a buscar...";

            gvDetalle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gvDetalle.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvDetalle.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvDetalle.Appearance.FocusedRow.BackColor = Color.FromArgb(204, 229, 255);
            gvDetalle.Appearance.FocusedRow.Options.UseBackColor = true;
            gvDetalle.Appearance.HideSelectionRow.BackColor = Color.FromArgb(204, 229, 255);
            gvDetalle.Appearance.HideSelectionRow.Options.UseBackColor = true;
            gvDetalle.Appearance.Row.ForeColor = Color.Black;
            gvDetalle.Appearance.Row.Font = new Font("Segoe UI", 9f);
            gvDetalle.Appearance.Row.Options.UseFont = true;

            gvDetalle.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            gvDetalle.Appearance.HeaderPanel.Options.UseFont = true;
            gvDetalle.Appearance.Row.Font = new Font("Segoe UI", 9f);
            gvDetalle.Appearance.Row.Options.UseFont = true;


            // --- Embedded Navigator: solo Nuevo ---
            gridControl1.UseEmbeddedNavigator = true;
            var nav = gridControl1.EmbeddedNavigator;
            nav.Buttons.Append.Visible = false;
            nav.Buttons.Remove.Visible = false;
            nav.Buttons.Edit.Visible = false;
            nav.Buttons.EndEdit.Visible = false;
            nav.Buttons.CancelEdit.Visible = false;

            gvDetalle.CustomRowCellEdit += GvDetalle_CustomRowCellEdit;
        }

        private void GvDetalle_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column == colVER_R)
            {
                object val = gvDetalle.GetRowCellValue(e.RowHandle, "ID_COMPROBANTE_RET");
                bool tieneRetencion = val != null && val != DBNull.Value;

                e.RepositoryItem = tieneRetencion ? riVerR : riVerRVacio;
            }
        }

        private void CargarDatos()
        {
            _dtDetalle = _dal.EjecutarConsulta("DISTRIB.SP_CREDITO_FISCAL_GASTO",
                new { ACCION = "LISTAR_COMPROBANTES_CCF" });
            gridControl1.DataSource = _dtDetalle;
            ConfigurarSumariosTotales();
        }

        private int? ObtenerIdFilaActiva()
        {
            if (gvDetalle.FocusedRowHandle < 0) return null;
            object val = gvDetalle.GetRowCellValue(gvDetalle.FocusedRowHandle, "ID_CCF_GASTO");
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }

        private void AbrirDocumento(int idCcfGasto)
        {
            using (var frm = new frmDocumento_Gasto())
            {
                frm.IdCcfGasto = idCcfGasto;                
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    CargarDatos();
                }
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void riEditar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int? id = ObtenerIdFilaActiva();
            if (id.HasValue) AbrirDocumento(id.Value);
        }

      
        public void Refrescar()
        {
            CargarDatos();
        }

        private void btnNuevoGasto_Click(object sender, EventArgs e)
        {
            AbrirDocumento(0);
        }

        private void riVerR_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                int? id = ObtenerIdFilaActiva();
                if (id.HasValue)
                {
                    var reporte = new rptCompRetencion { IdCcfCompra = id.Value, Origen = rptCompRetencion.OrigenDatos.Distribucion };
                    reporte.MostrarPreview();
                }
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
        private void ConfigurarSumariosTotales()
        {
            // Activar el footer del grid
            gvDetalle.OptionsView.ShowFooter = true;

            // Estilo llamativo para el footer
            gvDetalle.Appearance.FooterPanel.BackColor = Color.FromArgb(30, 64, 175);
            gvDetalle.Appearance.FooterPanel.ForeColor = Color.White;
            gvDetalle.Appearance.FooterPanel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            gvDetalle.Appearance.FooterPanel.Options.UseBackColor = true;
            gvDetalle.Appearance.FooterPanel.Options.UseForeColor = true;
            gvDetalle.Appearance.FooterPanel.Options.UseFont = true;
            gvDetalle.Appearance.FooterPanel.TextOptions.HAlignment =
                DevExpress.Utils.HorzAlignment.Far;

            // Repintar el footer con el color del fondo
            gvDetalle.CustomDrawFooter += (s, ev) =>
            {
                using (var brush = new SolidBrush(Color.FromArgb(30, 64, 175)))
                    ev.Graphics.FillRectangle(brush, ev.Bounds);
            };

            gvDetalle.CustomDrawFooterCell += (s, ev) =>
            {
                using (var brush = new SolidBrush(Color.FromArgb(30, 64, 175)))
                    ev.Graphics.FillRectangle(brush, ev.Bounds);

                using (var sf = new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Center
                })
                using (var brush = new SolidBrush(Color.White))
                using (var font = new Font("Segoe UI", 9f, FontStyle.Bold))
                {
                    ev.Graphics.DrawString(ev.Info.DisplayText, font, brush,
                        ev.Bounds, sf);
                }

                ev.Handled = true;
            };

            // Agregar summary a la columna TOTAL
            var colTotal = gvDetalle.Columns["SALDO"];
            if (colTotal != null)
            {
                colTotal.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                colTotal.SummaryItem.DisplayFormat = "{0:C2}";
            }
        }
    }
}
