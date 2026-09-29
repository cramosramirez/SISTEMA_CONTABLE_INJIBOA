namespace SistemaContable.UI.Forms.Planilla
{
    partial class frmFacturaCorte
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // Rediseño 2026-09-29:
        //   - Filtros compactos en dos filas.
        //   - Barra de botones con Salir alineado a la derecha.
        //   - SplitContainer: arriba la tabla de control (maestro-detalle), abajo las facturas generadas;
        //     el usuario puede mover la división.
        //   - Títulos de sección con color.
        //   - Las columnas de gvCandidatos se crean por código (ConfigurarGridCandidatos).

        private void InitializeComponent()
        {
            this.grpFiltros = new System.Windows.Forms.GroupBox();
            this.chkNoSellados = new System.Windows.Forms.CheckBox();
            this.cbxCATORCENA = new System.Windows.Forms.ComboBox();
            this.lblCatorcena = new System.Windows.Forms.Label();
            this.cbxZAFRA = new System.Windows.Forms.ComboBox();
            this.lblZafra = new System.Windows.Forms.Label();
            this.cbxTIPO_PLANILLA = new System.Windows.Forms.ComboBox();
            this.lblTipoPlanilla = new System.Windows.Forms.Label();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
            this.btnReporte = new DevExpress.XtraEditors.SimpleButton();
            this.btnEliminarPruebas = new DevExpress.XtraEditors.SimpleButton();
            this.btnImportar = new DevExpress.XtraEditors.SimpleButton();
            this.btnConsultar = new DevExpress.XtraEditors.SimpleButton();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gvCandidatos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblTituloCandidatos = new System.Windows.Forms.Label();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gvDocumentos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelNuevo = new System.Windows.Forms.Panel();
            this.btnNuevo = new DevExpress.XtraEditors.SimpleButton();
            this.btnGenerar = new DevExpress.XtraEditors.SimpleButton();
            this.lblTituloDocumentos = new System.Windows.Forms.Label();
            this.grpFiltros.SuspendLayout();
            this.panelBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCandidatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDocumentos)).BeginInit();
            this.panelNuevo.SuspendLayout();
            this.SuspendLayout();
            //
            // grpFiltros
            //
            this.grpFiltros.Controls.Add(this.chkNoSellados);
            this.grpFiltros.Controls.Add(this.cbxCATORCENA);
            this.grpFiltros.Controls.Add(this.lblCatorcena);
            this.grpFiltros.Controls.Add(this.cbxZAFRA);
            this.grpFiltros.Controls.Add(this.lblZafra);
            this.grpFiltros.Controls.Add(this.cbxTIPO_PLANILLA);
            this.grpFiltros.Controls.Add(this.lblTipoPlanilla);
            this.grpFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFiltros.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFiltros.Location = new System.Drawing.Point(8, 8);
            this.grpFiltros.Name = "grpFiltros";
            this.grpFiltros.Padding = new System.Windows.Forms.Padding(8);
            this.grpFiltros.Size = new System.Drawing.Size(1264, 92);
            this.grpFiltros.TabIndex = 0;
            this.grpFiltros.TabStop = false;
            this.grpFiltros.Text = "Filtros";
            //
            // lblTipoPlanilla  (fila 2, columna 1)
            //
            this.lblTipoPlanilla.AutoSize = true;
            this.lblTipoPlanilla.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTipoPlanilla.Location = new System.Drawing.Point(16, 60);
            this.lblTipoPlanilla.Name = "lblTipoPlanilla";
            this.lblTipoPlanilla.Size = new System.Drawing.Size(76, 15);
            this.lblTipoPlanilla.TabIndex = 4;
            this.lblTipoPlanilla.Text = "Tipo Planilla:";
            //
            // cbxTIPO_PLANILLA
            //
            this.cbxTIPO_PLANILLA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTIPO_PLANILLA.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbxTIPO_PLANILLA.FormattingEnabled = true;
            this.cbxTIPO_PLANILLA.Location = new System.Drawing.Point(136, 56);
            this.cbxTIPO_PLANILLA.Name = "cbxTIPO_PLANILLA";
            this.cbxTIPO_PLANILLA.Size = new System.Drawing.Size(260, 23);
            this.cbxTIPO_PLANILLA.TabIndex = 5;
            //
            // lblZafra  (fila 1, columna 1)
            //
            this.lblZafra.AutoSize = true;
            this.lblZafra.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblZafra.Location = new System.Drawing.Point(16, 28);
            this.lblZafra.Name = "lblZafra";
            this.lblZafra.Size = new System.Drawing.Size(37, 15);
            this.lblZafra.TabIndex = 0;
            this.lblZafra.Text = "Zafra:";
            //
            // cbxZAFRA
            //
            this.cbxZAFRA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxZAFRA.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbxZAFRA.FormattingEnabled = true;
            this.cbxZAFRA.Location = new System.Drawing.Point(136, 24);
            this.cbxZAFRA.Name = "cbxZAFRA";
            this.cbxZAFRA.Size = new System.Drawing.Size(160, 23);
            this.cbxZAFRA.TabIndex = 1;
            this.cbxZAFRA.SelectedIndexChanged += new System.EventHandler(this.cbxZAFRA_SelectedIndexChanged);
            //
            // lblCatorcena  (fila 1, columna 2)
            //
            this.lblCatorcena.AutoSize = true;
            this.lblCatorcena.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCatorcena.Location = new System.Drawing.Point(430, 28);
            this.lblCatorcena.Name = "lblCatorcena";
            this.lblCatorcena.Size = new System.Drawing.Size(63, 15);
            this.lblCatorcena.TabIndex = 2;
            this.lblCatorcena.Text = "Catorcena:";
            //
            // cbxCATORCENA
            //
            this.cbxCATORCENA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCATORCENA.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbxCATORCENA.FormattingEnabled = true;
            this.cbxCATORCENA.Location = new System.Drawing.Point(516, 24);
            this.cbxCATORCENA.Name = "cbxCATORCENA";
            this.cbxCATORCENA.Size = new System.Drawing.Size(120, 23);
            this.cbxCATORCENA.TabIndex = 3;
            //
            // chkNoSellados
            //
            this.chkNoSellados.AutoSize = true;
            this.chkNoSellados.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkNoSellados.Location = new System.Drawing.Point(430, 58);
            this.chkNoSellados.Name = "chkNoSellados";
            this.chkNoSellados.Size = new System.Drawing.Size(145, 19);
            this.chkNoSellados.TabIndex = 6;
            this.chkNoSellados.Text = "Solo pendientes (No Sellados)";
            //
            // panelBotones
            //
            this.panelBotones.Controls.Add(this.btnSalir);
            this.panelBotones.Controls.Add(this.btnEliminarPruebas);
            this.panelBotones.Controls.Add(this.btnReporte);
            this.panelBotones.Controls.Add(this.btnImportar);
            this.panelBotones.Controls.Add(this.btnConsultar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBotones.Location = new System.Drawing.Point(8, 100);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(1264, 54);
            this.panelBotones.TabIndex = 1;
            //
            // btnConsultar
            //
            this.btnConsultar.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnConsultar.Appearance.Options.UseFont = true;
            this.btnConsultar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.buscar1_48x48;
            this.btnConsultar.Location = new System.Drawing.Point(0, 8);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(120, 40);
            this.btnConsultar.TabIndex = 0;
            this.btnConsultar.TabStop = false;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            //
            // btnImportar
            //
            this.btnImportar.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnImportar.Appearance.Options.UseFont = true;
            this.btnImportar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.descargar48x48;
            this.btnImportar.Location = new System.Drawing.Point(128, 8);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(120, 40);
            this.btnImportar.TabIndex = 1;
            this.btnImportar.TabStop = false;
            this.btnImportar.Text = "Importar";
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            //
            // btnReporte
            //
            this.btnReporte.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnReporte.Appearance.Options.UseFont = true;
            this.btnReporte.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.imprimir32x32;
            this.btnReporte.Location = new System.Drawing.Point(256, 8);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(120, 40);
            this.btnReporte.TabIndex = 2;
            this.btnReporte.TabStop = false;
            this.btnReporte.Text = "Reporte";
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            //
            // btnEliminarPruebas  (borra las facturas de planilla)
            //
            this.btnEliminarPruebas.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEliminarPruebas.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnEliminarPruebas.Appearance.Options.UseFont = true;
            this.btnEliminarPruebas.Appearance.Options.UseForeColor = true;
            this.btnEliminarPruebas.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.eliminar32x32;
            this.btnEliminarPruebas.Location = new System.Drawing.Point(384, 8);
            this.btnEliminarPruebas.Name = "btnEliminarPruebas";
            this.btnEliminarPruebas.Size = new System.Drawing.Size(160, 40);
            this.btnEliminarPruebas.TabIndex = 4;
            this.btnEliminarPruebas.TabStop = false;
            this.btnEliminarPruebas.Text = "Eliminar pruebas";
            this.btnEliminarPruebas.Click += new System.EventHandler(this.btnEliminarPruebas_Click);
            //
            // btnSalir  (anclado a la derecha)
            //
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSalir.Appearance.Options.UseFont = true;
            this.btnSalir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnSalir.Location = new System.Drawing.Point(1144, 8);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(120, 40);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.TabStop = false;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            //
            // splitMain  (arriba: tabla de control / abajo: facturas generadas)
            //
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(8, 154);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            //
            // splitMain.Panel1
            //
            this.splitMain.Panel1.Controls.Add(this.gridControl1);
            this.splitMain.Panel1.Controls.Add(this.lblTituloCandidatos);
            //
            // splitMain.Panel2
            //
            this.splitMain.Panel2.Controls.Add(this.gridControl2);
            this.splitMain.Panel2.Controls.Add(this.panelNuevo);
            this.splitMain.Panel2.Controls.Add(this.lblTituloDocumentos);
            this.splitMain.Size = new System.Drawing.Size(1264, 538);
            this.splitMain.SplitterDistance = 269;
            this.splitMain.SplitterWidth = 6;
            this.splitMain.TabIndex = 2;
            //
            // lblTituloCandidatos
            //
            this.lblTituloCandidatos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.lblTituloCandidatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloCandidatos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTituloCandidatos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(121)))));
            this.lblTituloCandidatos.Location = new System.Drawing.Point(0, 0);
            this.lblTituloCandidatos.Name = "lblTituloCandidatos";
            this.lblTituloCandidatos.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblTituloCandidatos.Size = new System.Drawing.Size(1264, 28);
            this.lblTituloCandidatos.TabIndex = 0;
            this.lblTituloCandidatos.Text = "Comprobantes de la planilla";
            this.lblTituloCandidatos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // gridControl1 — tabla de control (maestro-detalle, columnas por código)
            //
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 28);
            this.gridControl1.MainView = this.gvCandidatos;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1264, 241);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCandidatos});
            //
            // gvCandidatos
            //
            this.gvCandidatos.GridControl = this.gridControl1;
            this.gvCandidatos.Name = "gvCandidatos";
            //
            // lblTituloDocumentos
            //
            this.lblTituloDocumentos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(218)))));
            this.lblTituloDocumentos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloDocumentos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTituloDocumentos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(86)))), ((int)(((byte)(35)))));
            this.lblTituloDocumentos.Location = new System.Drawing.Point(0, 0);
            this.lblTituloDocumentos.Name = "lblTituloDocumentos";
            this.lblTituloDocumentos.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblTituloDocumentos.Size = new System.Drawing.Size(1264, 28);
            this.lblTituloDocumentos.TabIndex = 0;
            this.lblTituloDocumentos.Text = "Facturas generadas";
            this.lblTituloDocumentos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panelNuevo
            //
            this.panelNuevo.Controls.Add(this.btnGenerar);
            this.panelNuevo.Controls.Add(this.btnNuevo);
            this.panelNuevo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNuevo.Location = new System.Drawing.Point(0, 28);
            this.panelNuevo.Name = "panelNuevo";
            this.panelNuevo.Size = new System.Drawing.Size(1264, 50);
            this.panelNuevo.TabIndex = 1;
            //
            // btnNuevo
            //
            this.btnNuevo.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.Appearance.Options.UseFont = true;
            this.btnNuevo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevo.Location = new System.Drawing.Point(0, 5);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(120, 40);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.TabStop = false;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            //
            // btnGenerar  (emite las facturas de las filas seleccionadas arriba)
            //
            this.btnGenerar.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGenerar.Appearance.Options.UseFont = true;
            this.btnGenerar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.validar3_32x32;
            this.btnGenerar.Location = new System.Drawing.Point(128, 5);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(165, 40);
            this.btnGenerar.TabIndex = 1;
            this.btnGenerar.TabStop = false;
            this.btnGenerar.Text = "Generar Factura";
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            //
            // gridControl2 — facturas generadas (SP ... GENERADOS, columnas por código)
            //
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 78);
            this.gridControl2.MainView = this.gvDocumentos;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(1264, 187);
            this.gridControl2.TabIndex = 2;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDocumentos});
            //
            // gvDocumentos
            //
            this.gvDocumentos.GridControl = this.gridControl2;
            this.gvDocumentos.Name = "gvDocumentos";
            this.gvDocumentos.OptionsView.ShowIndicator = false;
            //
            // frmFacturaCorte
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 700);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.grpFiltros);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "frmFacturaCorte";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Factura de Planilla";
            this.Load += new System.EventHandler(this.frmFacturaCorte_Load);
            this.grpFiltros.ResumeLayout(false);
            this.grpFiltros.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCandidatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDocumentos)).EndInit();
            this.panelNuevo.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpFiltros;
        private System.Windows.Forms.Label lblZafra;
        private System.Windows.Forms.ComboBox cbxZAFRA;
        private System.Windows.Forms.Label lblCatorcena;
        private System.Windows.Forms.ComboBox cbxCATORCENA;
        private System.Windows.Forms.Label lblTipoPlanilla;
        private System.Windows.Forms.ComboBox cbxTIPO_PLANILLA;
        private System.Windows.Forms.CheckBox chkNoSellados;

        private System.Windows.Forms.Panel panelBotones;
        private DevExpress.XtraEditors.SimpleButton btnConsultar;
        private DevExpress.XtraEditors.SimpleButton btnImportar;
        private DevExpress.XtraEditors.SimpleButton btnReporte;
        private DevExpress.XtraEditors.SimpleButton btnEliminarPruebas;
        private DevExpress.XtraEditors.SimpleButton btnSalir;

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Label lblTituloCandidatos;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCandidatos;

        private System.Windows.Forms.Label lblTituloDocumentos;
        private System.Windows.Forms.Panel panelNuevo;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private DevExpress.XtraEditors.SimpleButton btnGenerar;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDocumentos;
    }
}
