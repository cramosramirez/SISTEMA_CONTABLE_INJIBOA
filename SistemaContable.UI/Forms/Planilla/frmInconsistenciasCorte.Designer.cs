namespace SistemaContable.UI.Forms.Planilla
{
    partial class frmInconsistenciasCorte
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // 2026-10-05: Inconsistencias previas a generar CCF / Factura de planilla.
        // Mismos filtros que frmCreditoFiscalCorte / frmFacturaCorte + Tipo de documento.
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInconsistenciasCorte));
            this.grpFiltros = new System.Windows.Forms.GroupBox();
            this.chkSoloErrores = new System.Windows.Forms.CheckBox();
            this.cbxTIPO_DOCUMENTO = new System.Windows.Forms.ComboBox();
            this.lblTipoDocumento = new System.Windows.Forms.Label();
            this.cbxCATORCENA = new System.Windows.Forms.ComboBox();
            this.lblCatorcena = new System.Windows.Forms.Label();
            this.cbxZAFRA = new System.Windows.Forms.ComboBox();
            this.lblZafra = new System.Windows.Forms.Label();
            this.cbxTIPO_PLANILLA = new System.Windows.Forms.ComboBox();
            this.lblTipoPlanilla = new System.Windows.Forms.Label();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.btnCorregir = new DevExpress.XtraEditors.SimpleButton();
            this.btnConsultar = new DevExpress.XtraEditors.SimpleButton();
            this.lblResumen = new System.Windows.Forms.Label();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gvInconsistencias = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grpFiltros.SuspendLayout();
            this.panelBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInconsistencias)).BeginInit();
            this.SuspendLayout();
            // 
            // grpFiltros
            // 
            this.grpFiltros.Controls.Add(this.chkSoloErrores);
            this.grpFiltros.Controls.Add(this.cbxTIPO_DOCUMENTO);
            this.grpFiltros.Controls.Add(this.lblTipoDocumento);
            this.grpFiltros.Controls.Add(this.cbxCATORCENA);
            this.grpFiltros.Controls.Add(this.lblCatorcena);
            this.grpFiltros.Controls.Add(this.cbxZAFRA);
            this.grpFiltros.Controls.Add(this.lblZafra);
            this.grpFiltros.Controls.Add(this.cbxTIPO_PLANILLA);
            this.grpFiltros.Controls.Add(this.lblTipoPlanilla);
            this.grpFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFiltros.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpFiltros.Location = new System.Drawing.Point(8, 8);
            this.grpFiltros.Name = "grpFiltros";
            this.grpFiltros.Padding = new System.Windows.Forms.Padding(8);
            this.grpFiltros.Size = new System.Drawing.Size(1264, 92);
            this.grpFiltros.TabIndex = 0;
            this.grpFiltros.TabStop = false;
            this.grpFiltros.Text = "Filtros";
            // 
            // chkSoloErrores
            // 
            this.chkSoloErrores.AutoSize = true;
            this.chkSoloErrores.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkSoloErrores.Location = new System.Drawing.Point(700, 58);
            this.chkSoloErrores.Name = "chkSoloErrores";
            this.chkSoloErrores.Size = new System.Drawing.Size(171, 19);
            this.chkSoloErrores.TabIndex = 8;
            this.chkSoloErrores.Text = "Solo errores (ocultar avisos)";
            // 
            // cbxTIPO_DOCUMENTO
            // 
            this.cbxTIPO_DOCUMENTO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTIPO_DOCUMENTO.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbxTIPO_DOCUMENTO.Location = new System.Drawing.Point(516, 56);
            this.cbxTIPO_DOCUMENTO.Name = "cbxTIPO_DOCUMENTO";
            this.cbxTIPO_DOCUMENTO.Size = new System.Drawing.Size(160, 23);
            this.cbxTIPO_DOCUMENTO.TabIndex = 7;
            // 
            // lblTipoDocumento
            // 
            this.lblTipoDocumento.AutoSize = true;
            this.lblTipoDocumento.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTipoDocumento.Location = new System.Drawing.Point(430, 60);
            this.lblTipoDocumento.Name = "lblTipoDocumento";
            this.lblTipoDocumento.Size = new System.Drawing.Size(73, 15);
            this.lblTipoDocumento.TabIndex = 9;
            this.lblTipoDocumento.Text = "Documento:";
            // 
            // cbxCATORCENA
            // 
            this.cbxCATORCENA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCATORCENA.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbxCATORCENA.Location = new System.Drawing.Point(516, 24);
            this.cbxCATORCENA.Name = "cbxCATORCENA";
            this.cbxCATORCENA.Size = new System.Drawing.Size(120, 23);
            this.cbxCATORCENA.TabIndex = 3;
            // 
            // lblCatorcena
            // 
            this.lblCatorcena.AutoSize = true;
            this.lblCatorcena.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCatorcena.Location = new System.Drawing.Point(430, 28);
            this.lblCatorcena.Name = "lblCatorcena";
            this.lblCatorcena.Size = new System.Drawing.Size(64, 15);
            this.lblCatorcena.TabIndex = 10;
            this.lblCatorcena.Text = "Quincena:";
            // 
            // cbxZAFRA
            // 
            this.cbxZAFRA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxZAFRA.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbxZAFRA.Location = new System.Drawing.Point(136, 24);
            this.cbxZAFRA.Name = "cbxZAFRA";
            this.cbxZAFRA.Size = new System.Drawing.Size(160, 23);
            this.cbxZAFRA.TabIndex = 1;
            this.cbxZAFRA.SelectedIndexChanged += new System.EventHandler(this.cbxZAFRA_SelectedIndexChanged);
            // 
            // lblZafra
            // 
            this.lblZafra.AutoSize = true;
            this.lblZafra.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblZafra.Location = new System.Drawing.Point(16, 28);
            this.lblZafra.Name = "lblZafra";
            this.lblZafra.Size = new System.Drawing.Size(37, 15);
            this.lblZafra.TabIndex = 11;
            this.lblZafra.Text = "Zafra:";
            // 
            // cbxTIPO_PLANILLA
            // 
            this.cbxTIPO_PLANILLA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTIPO_PLANILLA.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbxTIPO_PLANILLA.Location = new System.Drawing.Point(136, 56);
            this.cbxTIPO_PLANILLA.Name = "cbxTIPO_PLANILLA";
            this.cbxTIPO_PLANILLA.Size = new System.Drawing.Size(260, 23);
            this.cbxTIPO_PLANILLA.TabIndex = 5;
            // 
            // lblTipoPlanilla
            // 
            this.lblTipoPlanilla.AutoSize = true;
            this.lblTipoPlanilla.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTipoPlanilla.Location = new System.Drawing.Point(16, 60);
            this.lblTipoPlanilla.Name = "lblTipoPlanilla";
            this.lblTipoPlanilla.Size = new System.Drawing.Size(75, 15);
            this.lblTipoPlanilla.TabIndex = 12;
            this.lblTipoPlanilla.Text = "Tipo Planilla:";
            // 
            // panelBotones
            // 
            this.panelBotones.Controls.Add(this.btnSalir);
            this.panelBotones.Controls.Add(this.btnCorregir);
            this.panelBotones.Controls.Add(this.btnImprimir);
            this.panelBotones.Controls.Add(this.btnConsultar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBotones.Location = new System.Drawing.Point(8, 100);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(1264, 54);
            this.panelBotones.TabIndex = 1;
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSalir.Appearance.Options.UseFont = true;
            this.btnSalir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnSalir.Location = new System.Drawing.Point(1144, 8);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(120, 40);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.TabStop = false;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnImprimir.Appearance.Options.UseFont = true;
            this.btnImprimir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.imprimir32x32;
            this.btnImprimir.Location = new System.Drawing.Point(136, 8);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(130, 40);
            this.btnImprimir.TabIndex = 1;
            this.btnImprimir.TabStop = false;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnCorregir
            // 
            this.btnCorregir.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCorregir.Appearance.Options.UseFont = true;
            this.btnCorregir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.procesar32x32;
            this.btnCorregir.Location = new System.Drawing.Point(272, 8);
            this.btnCorregir.Name = "btnCorregir";
            this.btnCorregir.Size = new System.Drawing.Size(130, 40);
            this.btnCorregir.TabIndex = 3;
            this.btnCorregir.TabStop = false;
            this.btnCorregir.Text = "Corregir";
            this.btnCorregir.Click += new System.EventHandler(this.btnCorregir_Click);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnConsultar.Appearance.Options.UseFont = true;
            this.btnConsultar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnConsultar.ImageOptions.Image")));
            this.btnConsultar.Location = new System.Drawing.Point(0, 8);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(130, 40);
            this.btnConsultar.TabIndex = 0;
            this.btnConsultar.TabStop = false;
            this.btnConsultar.Text = "Revisar";
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // lblResumen
            // 
            this.lblResumen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.lblResumen.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblResumen.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblResumen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(121)))));
            this.lblResumen.Location = new System.Drawing.Point(8, 154);
            this.lblResumen.Name = "lblResumen";
            this.lblResumen.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblResumen.Size = new System.Drawing.Size(1264, 28);
            this.lblResumen.TabIndex = 2;
            this.lblResumen.Text = "Seleccione los filtros y presione Revisar.";
            this.lblResumen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(8, 182);
            this.gridControl1.MainView = this.gvInconsistencias;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1264, 530);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInconsistencias});
            // 
            // gvInconsistencias
            // 
            this.gvInconsistencias.GridControl = this.gridControl1;
            this.gvInconsistencias.Name = "gvInconsistencias";
            // 
            // frmInconsistenciasCorte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.lblResumen);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.grpFiltros);
            this.Name = "frmInconsistenciasCorte";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Tag = "Consulta";
            this.Text = "Inconsistencias de planilla (antes de generar CCF / Factura)";
            this.Load += new System.EventHandler(this.frmInconsistenciasCorte_Load);
            this.grpFiltros.ResumeLayout(false);
            this.grpFiltros.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInconsistencias)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFiltros;
        private System.Windows.Forms.ComboBox cbxTIPO_DOCUMENTO;
        private System.Windows.Forms.Label lblTipoDocumento;
        private System.Windows.Forms.ComboBox cbxCATORCENA;
        private System.Windows.Forms.Label lblCatorcena;
        private System.Windows.Forms.ComboBox cbxZAFRA;
        private System.Windows.Forms.Label lblZafra;
        private System.Windows.Forms.ComboBox cbxTIPO_PLANILLA;
        private System.Windows.Forms.Label lblTipoPlanilla;
        private System.Windows.Forms.CheckBox chkSoloErrores;
        private System.Windows.Forms.Panel panelBotones;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private DevExpress.XtraEditors.SimpleButton btnCorregir;
        private DevExpress.XtraEditors.SimpleButton btnConsultar;
        private System.Windows.Forms.Label lblResumen;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInconsistencias;
    }
}
