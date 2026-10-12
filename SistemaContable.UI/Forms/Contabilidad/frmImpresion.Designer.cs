
namespace SistemaContable.UI.Forms.Contabilidad
{
    partial class frmImpresion : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImpresion));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtMes = new System.Windows.Forms.TextBox();
            this.txtAnio = new System.Windows.Forms.TextBox();
            this.btSalir = new DevExpress.XtraEditors.SimpleButton();
            this.btImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.btPantalla = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.ckDetalleParcial = new DevExpress.XtraEditors.CheckEdit();
            this.ckDetalleLinea = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.ckFormal = new DevExpress.XtraEditors.CheckEdit();
            this.ckBorrador = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtNumeroFinal = new System.Windows.Forms.TextBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtNumeroInicio = new System.Windows.Forms.TextBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtTipoNombre = new System.Windows.Forms.TextBox();
            this.txtTipo = new System.Windows.Forms.TextBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckDetalleParcial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckDetalleLinea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckFormal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckBorrador.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtMes);
            this.groupControl1.Controls.Add(this.txtAnio);
            this.groupControl1.Controls.Add(this.btSalir);
            this.groupControl1.Controls.Add(this.btImprimir);
            this.groupControl1.Controls.Add(this.btPantalla);
            this.groupControl1.Controls.Add(this.panelControl2);
            this.groupControl1.Controls.Add(this.panelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(519, 273);
            this.groupControl1.TabIndex = 0;
            // 
            // txtMes
            // 
            this.txtMes.Location = new System.Drawing.Point(26, 198);
            this.txtMes.Name = "txtMes";
            this.txtMes.Size = new System.Drawing.Size(56, 21);
            this.txtMes.TabIndex = 200;
            this.txtMes.Visible = false;
            // 
            // txtAnio
            // 
            this.txtAnio.Location = new System.Drawing.Point(26, 165);
            this.txtAnio.Name = "txtAnio";
            this.txtAnio.Size = new System.Drawing.Size(56, 21);
            this.txtAnio.TabIndex = 199;
            this.txtAnio.Visible = false;
            // 
            // btSalir
            // 
            this.btSalir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btSalir.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btSalir.Location = new System.Drawing.Point(278, 198);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(60, 56);
            this.btSalir.TabIndex = 198;
            this.btSalir.Text = "Salir";
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // btImprimir
            // 
            this.btImprimir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btImprimir.ImageOptions.Image")));
            this.btImprimir.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btImprimir.Location = new System.Drawing.Point(212, 198);
            this.btImprimir.Name = "btImprimir";
            this.btImprimir.Size = new System.Drawing.Size(60, 56);
            this.btImprimir.TabIndex = 197;
            this.btImprimir.Text = "Imprimir";
            this.btImprimir.Click += new System.EventHandler(this.btImprimir_Click);
            // 
            // btPantalla
            // 
            this.btPantalla.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btPantalla.ImageOptions.Image")));
            this.btPantalla.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btPantalla.Location = new System.Drawing.Point(146, 198);
            this.btPantalla.Name = "btPantalla";
            this.btPantalla.Size = new System.Drawing.Size(60, 56);
            this.btPantalla.TabIndex = 196;
            this.btPantalla.Text = "Buscar";
            this.btPantalla.Click += new System.EventHandler(this.btPantalla_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.ckDetalleParcial);
            this.panelControl2.Controls.Add(this.ckDetalleLinea);
            this.panelControl2.Location = new System.Drawing.Point(141, 135);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(203, 57);
            this.panelControl2.TabIndex = 1;
            // 
            // ckDetalleParcial
            // 
            this.ckDetalleParcial.Location = new System.Drawing.Point(5, 31);
            this.ckDetalleParcial.Name = "ckDetalleParcial";
            this.ckDetalleParcial.Properties.Caption = "Con detalle de parciales";
            this.ckDetalleParcial.Size = new System.Drawing.Size(192, 20);
            this.ckDetalleParcial.TabIndex = 22;
            // 
            // ckDetalleLinea
            // 
            this.ckDetalleLinea.Location = new System.Drawing.Point(5, 5);
            this.ckDetalleLinea.Name = "ckDetalleLinea";
            this.ckDetalleLinea.Properties.Caption = "Detallar Concepto Por Linea";
            this.ckDetalleLinea.Size = new System.Drawing.Size(192, 20);
            this.ckDetalleLinea.TabIndex = 21;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.panelControl1.Controls.Add(this.ckFormal);
            this.panelControl1.Controls.Add(this.ckBorrador);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.txtNumeroFinal);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.txtNumeroInicio);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtTipoNombre);
            this.panelControl1.Controls.Add(this.txtTipo);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(50, 26);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(410, 103);
            this.panelControl1.TabIndex = 0;
            // 
            // ckFormal
            // 
            this.ckFormal.Location = new System.Drawing.Point(259, 68);
            this.ckFormal.Name = "ckFormal";
            this.ckFormal.Properties.Caption = "Formal";
            this.ckFormal.Size = new System.Drawing.Size(75, 20);
            this.ckFormal.TabIndex = 21;
            this.ckFormal.CheckedChanged += new System.EventHandler(this.ckFormal_CheckedChanged);
            // 
            // ckBorrador
            // 
            this.ckBorrador.Location = new System.Drawing.Point(259, 41);
            this.ckBorrador.Name = "ckBorrador";
            this.ckBorrador.Properties.Caption = "Borrador";
            this.ckBorrador.Size = new System.Drawing.Size(75, 20);
            this.ckBorrador.TabIndex = 20;
            this.ckBorrador.CheckedChanged += new System.EventHandler(this.ckBorrador_CheckedChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(220, 42);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 14);
            this.labelControl4.TabIndex = 19;
            this.labelControl4.Text = "Tipo";
            // 
            // txtNumeroFinal
            // 
            this.txtNumeroFinal.Location = new System.Drawing.Point(91, 67);
            this.txtNumeroFinal.Name = "txtNumeroFinal";
            this.txtNumeroFinal.Size = new System.Drawing.Size(104, 21);
            this.txtNumeroFinal.TabIndex = 18;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(15, 67);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(70, 14);
            this.labelControl3.TabIndex = 17;
            this.labelControl3.Text = "Número Final";
            // 
            // txtNumeroInicio
            // 
            this.txtNumeroInicio.Location = new System.Drawing.Point(91, 40);
            this.txtNumeroInicio.Name = "txtNumeroInicio";
            this.txtNumeroInicio.Size = new System.Drawing.Size(104, 21);
            this.txtNumeroInicio.TabIndex = 16;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(9, 40);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 14);
            this.labelControl2.TabIndex = 15;
            this.labelControl2.Text = "Número Inicial";
            // 
            // txtTipoNombre
            // 
            this.txtTipoNombre.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTipoNombre.Location = new System.Drawing.Point(153, 13);
            this.txtTipoNombre.Name = "txtTipoNombre";
            this.txtTipoNombre.ReadOnly = true;
            this.txtTipoNombre.Size = new System.Drawing.Size(246, 21);
            this.txtTipoNombre.TabIndex = 14;
            // 
            // txtTipo
            // 
            this.txtTipo.Location = new System.Drawing.Point(91, 13);
            this.txtTipo.Name = "txtTipo";
            this.txtTipo.Size = new System.Drawing.Size(56, 21);
            this.txtTipo.TabIndex = 13;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(61, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 12;
            this.labelControl1.Text = "Tipo";
            // 
            // frmImpresion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 273);
            this.Controls.Add(this.groupControl1);
            this.Name = "frmImpresion";
            this.Text = "Impresion de Comprobantes";
            this.Load += new System.EventHandler(this.frmImpresion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckDetalleParcial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckDetalleLinea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckFormal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckBorrador.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.TextBox txtTipoNombre;
        private System.Windows.Forms.TextBox txtTipo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.CheckEdit ckDetalleParcial;
        private DevExpress.XtraEditors.CheckEdit ckDetalleLinea;
        private DevExpress.XtraEditors.CheckEdit ckFormal;
        private DevExpress.XtraEditors.CheckEdit ckBorrador;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.TextBox txtNumeroFinal;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.TextBox txtNumeroInicio;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btSalir;
        private DevExpress.XtraEditors.SimpleButton btImprimir;
        private DevExpress.XtraEditors.SimpleButton btPantalla;
        private System.Windows.Forms.TextBox txtMes;
        private System.Windows.Forms.TextBox txtAnio;
    }
}