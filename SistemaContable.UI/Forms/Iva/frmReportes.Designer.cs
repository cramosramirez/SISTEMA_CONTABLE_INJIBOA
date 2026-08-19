using System.Drawing;
using System.Windows.Forms;
namespace SistemaContable.UI.Forms.Iva
{
    partial class frmReportes
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlTitulo;
        private Label lblTitulo;

        private SplitContainer splitContainer1;

        private Panel pnlListaReportes;
        private Label lblReportesHeader;
        private ListBox lstReportes;

        private Panel pnlCriteriosContenedor;

        private Panel pnlDestino;   // se conserva por compatibilidad con el code-behind
        private Panel pnlProducto;  // se conserva por compatibilidad con el code-behind

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportes));
            this.pnlTitulo = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlListaReportes = new System.Windows.Forms.Panel();
            this.lstReportes = new System.Windows.Forms.ListBox();
            this.lblReportesHeader = new System.Windows.Forms.Label();
            this.pnlCriteriosContenedor = new System.Windows.Forms.Panel();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnFinalizar = new DevExpress.XtraEditors.SimpleButton();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.ck_DtExportacion = new System.Windows.Forms.CheckBox();
            this.cb_Mes = new System.Windows.Forms.ComboBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_Anio = new DevExpress.XtraEditors.TextEdit();
            this.pnlDestino = new System.Windows.Forms.Panel();
            this.pnlProducto = new System.Windows.Forms.Panel();
            this.pnlTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlListaReportes.SuspendLayout();
            this.pnlCriteriosContenedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Anio.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTitulo
            // 
            this.pnlTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(59)))), ((int)(((byte)(87)))));
            this.pnlTitulo.Controls.Add(this.lblTitulo);
            this.pnlTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitulo.Location = new System.Drawing.Point(0, 0);
            this.pnlTitulo.Name = "pnlTitulo";
            this.pnlTitulo.Padding = new System.Windows.Forms.Padding(24, 0, 24, 0);
            this.pnlTitulo.Size = new System.Drawing.Size(1057, 42);
            this.pnlTitulo.TabIndex = 1;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 14F);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(24, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(247, 25);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Libros de Compras y Ventas";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(222)))), ((int)(((byte)(230)))));
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 42);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.pnlListaReportes);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.splitContainer1.Panel2.Controls.Add(this.pnlCriteriosContenedor);
            this.splitContainer1.Size = new System.Drawing.Size(1057, 508);
            this.splitContainer1.SplitterDistance = 211;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // pnlListaReportes
            // 
            this.pnlListaReportes.BackColor = System.Drawing.Color.White;
            this.pnlListaReportes.Controls.Add(this.lstReportes);
            this.pnlListaReportes.Controls.Add(this.lblReportesHeader);
            this.pnlListaReportes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlListaReportes.Location = new System.Drawing.Point(0, 0);
            this.pnlListaReportes.Name = "pnlListaReportes";
            this.pnlListaReportes.Padding = new System.Windows.Forms.Padding(16);
            this.pnlListaReportes.Size = new System.Drawing.Size(211, 508);
            this.pnlListaReportes.TabIndex = 0;
            // 
            // lstReportes
            // 
            this.lstReportes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstReportes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstReportes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstReportes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstReportes.ItemHeight = 32;
            this.lstReportes.Items.AddRange(new object[] {
            "Libro de Ventas a Consumidor Final",
            "Libro de Ventas a Contribuyentes",
            "Libro de Ventas de Liquidaciones",
            "Libro de Compras"});
            this.lstReportes.Location = new System.Drawing.Point(16, 44);
            this.lstReportes.Name = "lstReportes";
            this.lstReportes.Size = new System.Drawing.Size(179, 448);
            this.lstReportes.TabIndex = 0;
            this.lstReportes.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstReportes_DrawItem);
            this.lstReportes.SelectedIndexChanged += new System.EventHandler(this.lstReportes_SelectedIndexChanged);
            // 
            // lblReportesHeader
            // 
            this.lblReportesHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReportesHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F);
            this.lblReportesHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(148)))), ((int)(((byte)(166)))));
            this.lblReportesHeader.Location = new System.Drawing.Point(16, 16);
            this.lblReportesHeader.Name = "lblReportesHeader";
            this.lblReportesHeader.Size = new System.Drawing.Size(179, 28);
            this.lblReportesHeader.TabIndex = 1;
            this.lblReportesHeader.Text = "Tipo de Libro";
            // 
            // pnlCriteriosContenedor
            // 
            this.pnlCriteriosContenedor.Controls.Add(this.btnExcel);
            this.pnlCriteriosContenedor.Controls.Add(this.btnFinalizar);
            this.pnlCriteriosContenedor.Controls.Add(this.btnImprimir);
            this.pnlCriteriosContenedor.Controls.Add(this.groupControl1);
            this.pnlCriteriosContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCriteriosContenedor.Location = new System.Drawing.Point(0, 0);
            this.pnlCriteriosContenedor.Name = "pnlCriteriosContenedor";
            this.pnlCriteriosContenedor.Padding = new System.Windows.Forms.Padding(28, 24, 28, 24);
            this.pnlCriteriosContenedor.Size = new System.Drawing.Size(845, 508);
            this.pnlCriteriosContenedor.TabIndex = 0;
            // 
            // btnExcel
            // 
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.Appearance.Options.UseTextOptions = true;
            this.btnExcel.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnExcel.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.excel_48x48;
            this.btnExcel.ImageOptions.ImageToTextIndent = 10;
            this.btnExcel.Location = new System.Drawing.Point(184, 137);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(150, 47);
            this.btnExcel.TabIndex = 12;
            this.btnExcel.Text = "Exportar Excel";
            this.btnExcel.Visible = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.Appearance.Options.UseFont = true;
            this.btnFinalizar.Appearance.Options.UseTextOptions = true;
            this.btnFinalizar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnFinalizar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnFinalizar.Location = new System.Drawing.Point(340, 137);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(119, 47);
            this.btnFinalizar.TabIndex = 11;
            this.btnFinalizar.TabStop = false;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Appearance.Options.UseFont = true;
            this.btnImprimir.Appearance.Options.UseTextOptions = true;
            this.btnImprimir.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnImprimir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.ImageOptions.Image")));
            this.btnImprimir.ImageOptions.ImageToTextIndent = 10;
            this.btnImprimir.Location = new System.Drawing.Point(59, 137);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(119, 47);
            this.btnImprimir.TabIndex = 10;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.ck_DtExportacion);
            this.groupControl1.Controls.Add(this.cb_Mes);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txt_Anio);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(28, 24);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(789, 107);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "Criterios de búsqueda";
            // 
            // ck_DtExportacion
            // 
            this.ck_DtExportacion.AutoSize = true;
            this.ck_DtExportacion.Location = new System.Drawing.Point(324, 57);
            this.ck_DtExportacion.Name = "ck_DtExportacion";
            this.ck_DtExportacion.Size = new System.Drawing.Size(134, 17);
            this.ck_DtExportacion.TabIndex = 12;
            this.ck_DtExportacion.Text = "Detalle de Exportacion";
            this.ck_DtExportacion.UseVisualStyleBackColor = true;
            // 
            // cb_Mes
            // 
            this.cb_Mes.FormattingEnabled = true;
            this.cb_Mes.Location = new System.Drawing.Point(138, 57);
            this.cb_Mes.Name = "cb_Mes";
            this.cb_Mes.Size = new System.Drawing.Size(168, 21);
            this.cb_Mes.TabIndex = 11;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(138, 38);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(23, 13);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "Mes";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(32, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(22, 13);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "Año";
            // 
            // txt_Anio
            // 
            this.txt_Anio.Location = new System.Drawing.Point(32, 57);
            this.txt_Anio.Name = "txt_Anio";
            this.txt_Anio.Size = new System.Drawing.Size(86, 20);
            this.txt_Anio.TabIndex = 8;
            // 
            // pnlDestino
            // 
            this.pnlDestino.Location = new System.Drawing.Point(0, 0);
            this.pnlDestino.Name = "pnlDestino";
            this.pnlDestino.Size = new System.Drawing.Size(0, 0);
            this.pnlDestino.TabIndex = 2;
            this.pnlDestino.Visible = false;
            // 
            // pnlProducto
            // 
            this.pnlProducto.Location = new System.Drawing.Point(0, 0);
            this.pnlProducto.Name = "pnlProducto";
            this.pnlProducto.Size = new System.Drawing.Size(0, 0);
            this.pnlProducto.TabIndex = 3;
            this.pnlProducto.Visible = false;
            // 
            // frmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1057, 550);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnlTitulo);
            this.Controls.Add(this.pnlDestino);
            this.Controls.Add(this.pnlProducto);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(820, 480);
            this.Name = "frmReportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Consulta";
            this.Text = "Reportes de Notas de Remisión";
            this.Load += new System.EventHandler(this.frmReportes_Load);
            this.pnlTitulo.ResumeLayout(false);
            this.pnlTitulo.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlListaReportes.ResumeLayout(false);
            this.pnlCriteriosContenedor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Anio.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        // Dibuja cada elemento de la lista con un estilo tipo "sidebar" moderno,
        // resaltando el ítem seleccionado con una barra de acento a la izquierda.
        private void lstReportes_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            bool seleccionado = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Color fondo = seleccionado ? ColorTranslator.FromHtml("#E8F0FE") : Color.White;
            Color texto = seleccionado ? ColorTranslator.FromHtml("#1F3B57") : ColorTranslator.FromHtml("#2E2E2E");

            using (var brFondo = new SolidBrush(fondo))
                e.Graphics.FillRectangle(brFondo, e.Bounds);

            if (seleccionado)
            {
                using (var brAcento = new SolidBrush(ColorTranslator.FromHtml("#1F3B57")))
                    e.Graphics.FillRectangle(brAcento, e.Bounds.X, e.Bounds.Y, 3, e.Bounds.Height);
            }

            string texto_item = lstReportes.Items[e.Index].ToString();
            using (var brTexto = new SolidBrush(texto))
            {
                var rectTexto = new Rectangle(e.Bounds.X + 14, e.Bounds.Y, e.Bounds.Width - 14, e.Bounds.Height);
                var formato = new StringFormat { LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString(texto_item, lstReportes.Font, brTexto, rectTexto, formato);
            }

            e.DrawFocusRectangle();
        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnFinalizar;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private ComboBox cb_Mes;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txt_Anio;
        private CheckBox ck_DtExportacion;
    }
}
