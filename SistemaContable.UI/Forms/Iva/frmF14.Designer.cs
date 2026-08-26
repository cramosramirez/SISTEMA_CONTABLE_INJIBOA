
namespace SistemaContable.UI.Forms.Iva
{
    partial class frmF14
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblEstado = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.bt_AbrirDirectorio = new DevExpress.XtraEditors.SimpleButton();
            this.cb_Mes = new System.Windows.Forms.ComboBox();
            this.bt_DescargarCsv = new DevExpress.XtraEditors.SimpleButton();
            this.bt_DescargaXls = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnFinalizar = new DevExpress.XtraEditors.SimpleButton();
            this.txt_Anio = new DevExpress.XtraEditors.TextEdit();
            this.ck_Anexo2 = new DevExpress.XtraEditors.CheckEdit();
            this.ck_Anexo1 = new DevExpress.XtraEditors.CheckEdit();
            this.ck_Anexo3 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Anio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ck_Anexo2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ck_Anexo1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ck_Anexo3.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.groupControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(70)))), ((int)(((byte)(120)))));
            this.groupControl1.Appearance.Options.UseFont = true;
            this.groupControl1.Appearance.Options.UseForeColor = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(70)))), ((int)(((byte)(120)))));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.Controls.Add(this.btnFinalizar);
            this.groupControl1.Controls.Add(this.lblEstado);
            this.groupControl1.Controls.Add(this.progressBar1);
            this.groupControl1.Controls.Add(this.bt_AbrirDirectorio);
            this.groupControl1.Controls.Add(this.cb_Mes);
            this.groupControl1.Controls.Add(this.bt_DescargarCsv);
            this.groupControl1.Controls.Add(this.bt_DescargaXls);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txt_Anio);
            this.groupControl1.Controls.Add(this.groupControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Padding = new System.Windows.Forms.Padding(12);
            this.groupControl1.Size = new System.Drawing.Size(635, 330);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Generación de Informe DGII F-14";
            // 
            // lblEstado
            // 
            this.lblEstado.BackColor = System.Drawing.Color.Transparent;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblEstado.Location = new System.Drawing.Point(40, 269);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(561, 20);
            this.lblEstado.TabIndex = 11;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(40, 293);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(561, 20);
            this.progressBar1.TabIndex = 9;
            // 
            // bt_AbrirDirectorio
            // 
            this.bt_AbrirDirectorio.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(110)))), ((int)(((byte)(122)))));
            this.bt_AbrirDirectorio.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.bt_AbrirDirectorio.Appearance.ForeColor = System.Drawing.Color.White;
            this.bt_AbrirDirectorio.Appearance.Options.UseBackColor = true;
            this.bt_AbrirDirectorio.Appearance.Options.UseFont = true;
            this.bt_AbrirDirectorio.Appearance.Options.UseForeColor = true;
            this.bt_AbrirDirectorio.Location = new System.Drawing.Point(324, 215);
            this.bt_AbrirDirectorio.Name = "bt_AbrirDirectorio";
            this.bt_AbrirDirectorio.Size = new System.Drawing.Size(136, 44);
            this.bt_AbrirDirectorio.TabIndex = 8;
            this.bt_AbrirDirectorio.Text = "Abrir Directorio";
            this.bt_AbrirDirectorio.Click += new System.EventHandler(this.bt_AbrirDirectorio_Click);
            // 
            // cb_Mes
            // 
            this.cb_Mes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_Mes.FormattingEnabled = true;
            this.cb_Mes.Location = new System.Drawing.Point(155, 62);
            this.cb_Mes.Name = "cb_Mes";
            this.cb_Mes.Size = new System.Drawing.Size(180, 23);
            this.cb_Mes.TabIndex = 7;
            // 
            // bt_DescargarCsv
            // 
            this.bt_DescargarCsv.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(98)))), ((int)(((byte)(155)))));
            this.bt_DescargarCsv.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.bt_DescargarCsv.Appearance.ForeColor = System.Drawing.Color.White;
            this.bt_DescargarCsv.Appearance.Options.UseBackColor = true;
            this.bt_DescargarCsv.Appearance.Options.UseFont = true;
            this.bt_DescargarCsv.Appearance.Options.UseForeColor = true;
            this.bt_DescargarCsv.Location = new System.Drawing.Point(182, 215);
            this.bt_DescargarCsv.Name = "bt_DescargarCsv";
            this.bt_DescargarCsv.Size = new System.Drawing.Size(136, 44);
            this.bt_DescargarCsv.TabIndex = 6;
            this.bt_DescargarCsv.Text = "Descargar CSV";
            this.bt_DescargarCsv.Click += new System.EventHandler(this.bt_DescargarCsv_Click);
            // 
            // bt_DescargaXls
            // 
            this.bt_DescargaXls.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(115)))), ((int)(((byte)(70)))));
            this.bt_DescargaXls.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.bt_DescargaXls.Appearance.ForeColor = System.Drawing.Color.White;
            this.bt_DescargaXls.Appearance.Options.UseBackColor = true;
            this.bt_DescargaXls.Appearance.Options.UseFont = true;
            this.bt_DescargaXls.Appearance.Options.UseForeColor = true;
            this.bt_DescargaXls.Location = new System.Drawing.Point(40, 215);
            this.bt_DescargaXls.Name = "bt_DescargaXls";
            this.bt_DescargaXls.Size = new System.Drawing.Size(136, 44);
            this.bt_DescargaXls.TabIndex = 5;
            this.bt_DescargaXls.Text = "Descargar XLS";
            this.bt_DescargaXls.Click += new System.EventHandler(this.bt_DescargaXls_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(155, 42);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(23, 15);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Mes";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(40, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(22, 15);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Año";
            // 
            // groupControl2
            // 
            this.groupControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupControl2.Appearance.Options.UseFont = true;
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.groupControl2.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl2.Controls.Add(this.ck_Anexo3);
            this.groupControl2.Controls.Add(this.ck_Anexo2);
            this.groupControl2.Controls.Add(this.ck_Anexo1);
            this.groupControl2.Location = new System.Drawing.Point(40, 98);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(561, 111);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Seleccione el Informe a Emitir";
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnFinalizar.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFinalizar.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnFinalizar.Appearance.Options.UseBackColor = true;
            this.btnFinalizar.Appearance.Options.UseFont = true;
            this.btnFinalizar.Appearance.Options.UseForeColor = true;
            this.btnFinalizar.Appearance.Options.UseTextOptions = true;
            this.btnFinalizar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnFinalizar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnFinalizar.Location = new System.Drawing.Point(465, 215);
            this.btnFinalizar.Margin = new System.Windows.Forms.Padding(2);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(136, 44);
            this.btnFinalizar.TabIndex = 55;
            this.btnFinalizar.TabStop = false;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // txt_Anio
            // 
            this.txt_Anio.Location = new System.Drawing.Point(40, 62);
            this.txt_Anio.Name = "txt_Anio";
            this.txt_Anio.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_Anio.Properties.Appearance.Options.UseFont = true;
            this.txt_Anio.Size = new System.Drawing.Size(90, 22);
            this.txt_Anio.TabIndex = 1;
            // 
            // ck_Anexo2
            // 
            this.ck_Anexo2.Location = new System.Drawing.Point(6, 52);
            this.ck_Anexo2.Name = "ck_Anexo2";
            this.ck_Anexo2.Properties.Caption = "Anexo Pesonal de Planilla Salario";
            this.ck_Anexo2.Size = new System.Drawing.Size(212, 20);
            this.ck_Anexo2.TabIndex = 5;
            // 
            // ck_Anexo1
            // 
            this.ck_Anexo1.Location = new System.Drawing.Point(5, 26);
            this.ck_Anexo1.Name = "ck_Anexo1";
            this.ck_Anexo1.Properties.Caption = "Anexo Compra";
            this.ck_Anexo1.Size = new System.Drawing.Size(213, 20);
            this.ck_Anexo1.TabIndex = 4;
            // 
            // ck_Anexo3
            // 
            this.ck_Anexo3.Location = new System.Drawing.Point(5, 78);
            this.ck_Anexo3.Name = "ck_Anexo3";
            this.ck_Anexo3.Properties.Caption = "Anexo Unificado";
            this.ck_Anexo3.Size = new System.Drawing.Size(212, 20);
            this.ck_Anexo3.TabIndex = 6;
            // 
            // frmF14
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(635, 330);
            this.Controls.Add(this.groupControl1);
            this.Name = "frmF14";
            this.Tag = "";
            this.Text = "DGII F-14";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txt_Anio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ck_Anexo2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ck_Anexo1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ck_Anexo3.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton bt_DescargarCsv;
        private DevExpress.XtraEditors.SimpleButton bt_DescargaXls;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txt_Anio;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.ComboBox cb_Mes;
        private DevExpress.XtraEditors.SimpleButton bt_AbrirDirectorio;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblEstado;
        private DevExpress.XtraEditors.SimpleButton btnFinalizar;
        private DevExpress.XtraEditors.CheckEdit ck_Anexo2;
        private DevExpress.XtraEditors.CheckEdit ck_Anexo1;
        private DevExpress.XtraEditors.CheckEdit ck_Anexo3;
    }
}
