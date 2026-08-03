
namespace SistemaContable.UI.Forms.Proveedores.ParametrosReportes
{
    partial class frmParametroDocumentosQuedan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmParametroDocumentosQuedan));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFinalizar = new DevExpress.XtraEditors.SimpleButton();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dteFECHA_FIN = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.dteFECHA_INICIO = new DevExpress.XtraEditors.DateEdit();
            this.label18 = new System.Windows.Forms.Label();
            this.btnExportar = new DevExpress.XtraEditors.SimpleButton();
            this.chkClasificarPorOrden = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_FIN.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_FIN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_INICIO.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_INICIO.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExportar);
            this.panel1.Controls.Add(this.btnFinalizar);
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(551, 209);
            this.panel1.TabIndex = 0;
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.Appearance.Options.UseFont = true;
            this.btnFinalizar.Appearance.Options.UseTextOptions = true;
            this.btnFinalizar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnFinalizar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnFinalizar.Location = new System.Drawing.Point(392, 141);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(119, 47);
            this.btnFinalizar.TabIndex = 2;
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
            this.btnImprimir.Location = new System.Drawing.Point(79, 141);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(119, 47);
            this.btnImprimir.TabIndex = 0;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkClasificarPorOrden);
            this.groupBox1.Controls.Add(this.dteFECHA_FIN);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dteFECHA_INICIO);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.139131F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(523, 101);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criterios";
            // 
            // dteFECHA_FIN
            // 
            this.dteFECHA_FIN.EditValue = null;
            this.dteFECHA_FIN.EnterMoveNextControl = true;
            this.dteFECHA_FIN.Location = new System.Drawing.Point(335, 37);
            this.dteFECHA_FIN.Name = "dteFECHA_FIN";
            this.dteFECHA_FIN.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dteFECHA_FIN.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dteFECHA_FIN.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.dteFECHA_FIN.Properties.Appearance.Options.UseFont = true;
            this.dteFECHA_FIN.Properties.Appearance.Options.UseForeColor = true;
            this.dteFECHA_FIN.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFECHA_FIN.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFECHA_FIN.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dteFECHA_FIN.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteFECHA_FIN.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dteFECHA_FIN.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteFECHA_FIN.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dteFECHA_FIN.Size = new System.Drawing.Size(123, 24);
            this.dteFECHA_FIN.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(266, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 22);
            this.label1.TabIndex = 60;
            this.label1.Text = "Al:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dteFECHA_INICIO
            // 
            this.dteFECHA_INICIO.EditValue = null;
            this.dteFECHA_INICIO.EnterMoveNextControl = true;
            this.dteFECHA_INICIO.Location = new System.Drawing.Point(136, 38);
            this.dteFECHA_INICIO.Name = "dteFECHA_INICIO";
            this.dteFECHA_INICIO.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dteFECHA_INICIO.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dteFECHA_INICIO.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.dteFECHA_INICIO.Properties.Appearance.Options.UseFont = true;
            this.dteFECHA_INICIO.Properties.Appearance.Options.UseForeColor = true;
            this.dteFECHA_INICIO.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFECHA_INICIO.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFECHA_INICIO.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dteFECHA_INICIO.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteFECHA_INICIO.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dteFECHA_INICIO.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteFECHA_INICIO.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dteFECHA_INICIO.Size = new System.Drawing.Size(123, 24);
            this.dteFECHA_INICIO.TabIndex = 1;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(64, 40);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(66, 22);
            this.label18.TabIndex = 0;
            this.label18.Text = "Del:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnExportar
            // 
            this.btnExportar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportar.Appearance.Options.UseFont = true;
            this.btnExportar.Appearance.Options.UseTextOptions = true;
            this.btnExportar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnExportar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.excel_48x48;
            this.btnExportar.ImageOptions.ImageToTextIndent = 10;
            this.btnExportar.Location = new System.Drawing.Point(238, 141);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(119, 47);
            this.btnExportar.TabIndex = 1;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // chkClasificarPorOrden
            // 
            this.chkClasificarPorOrden.AutoSize = true;
            this.chkClasificarPorOrden.Location = new System.Drawing.Point(100, 74);
            this.chkClasificarPorOrden.Name = "chkClasificarPorOrden";
            this.chkClasificarPorOrden.Size = new System.Drawing.Size(242, 21);
            this.chkClasificarPorOrden.TabIndex = 3;
            this.chkClasificarPorOrden.Text = "Clasificar por Orden de Compra";
            this.chkClasificarPorOrden.UseVisualStyleBackColor = true;
            // 
            // frmParametroDocumentosQuedan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 209);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmParametroDocumentosQuedan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Listado de comprobantes recibidos";
            this.Load += new System.EventHandler(this.frmParametroDocumentosQuedan_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_FIN.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_FIN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_INICIO.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_INICIO.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.DateEdit dteFECHA_FIN;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit dteFECHA_INICIO;
        private System.Windows.Forms.Label label18;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private DevExpress.XtraEditors.SimpleButton btnFinalizar;
        private DevExpress.XtraEditors.SimpleButton btnExportar;
        private System.Windows.Forms.CheckBox chkClasificarPorOrden;
    }
}