
namespace SistemaContable.UI.Forms.Iva
{
    partial class frmF28
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmF28));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnFinalizar = new DevExpress.XtraEditors.SimpleButton();
            this.lblEstado = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.bt_AbrirDirectorio = new DevExpress.XtraEditors.SimpleButton();
            this.cb_Mes = new System.Windows.Forms.ComboBox();
            this.bt_DescargarCsv = new DevExpress.XtraEditors.SimpleButton();
            this.bt_DescargaXls = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_Anio = new DevExpress.XtraEditors.TextEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.ck_Anexo5 = new DevExpress.XtraEditors.CheckEdit();
            this.ck_Anexo3 = new DevExpress.XtraEditors.CheckEdit();
            this.ck_Anexo2 = new DevExpress.XtraEditors.CheckEdit();
            this.ck_Anexo1 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Anio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ck_Anexo5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ck_Anexo3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ck_Anexo2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ck_Anexo1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
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
            this.groupControl1.Size = new System.Drawing.Size(800, 450);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "📊 Generacion de Informe DGII F-28";
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.Appearance.Options.UseFont = true;
            this.btnFinalizar.Appearance.Options.UseTextOptions = true;
            this.btnFinalizar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnFinalizar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnFinalizar.Location = new System.Drawing.Point(445, 228);
            this.btnFinalizar.Margin = new System.Windows.Forms.Padding(2);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(120, 45);
            this.btnFinalizar.TabIndex = 55;
            this.btnFinalizar.TabStop = false;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // lblEstado
            // 
            this.lblEstado.BackColor = System.Drawing.Color.Transparent;
            this.lblEstado.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblEstado.Location = new System.Drawing.Point(175, 303);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(264, 23);
            this.lblEstado.TabIndex = 11;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(40, 277);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(525, 23);
            this.progressBar1.TabIndex = 9;
            // 
            // bt_AbrirDirectorio
            // 
            this.bt_AbrirDirectorio.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bt_AbrirDirectorio.ImageOptions.Image")));
            this.bt_AbrirDirectorio.Location = new System.Drawing.Point(310, 228);
            this.bt_AbrirDirectorio.Name = "bt_AbrirDirectorio";
            this.bt_AbrirDirectorio.Size = new System.Drawing.Size(129, 45);
            this.bt_AbrirDirectorio.TabIndex = 8;
            this.bt_AbrirDirectorio.Text = "Abrir Directorio ";
            this.bt_AbrirDirectorio.Click += new System.EventHandler(this.bt_AbrirDirectorio_Click);
            // 
            // cb_Mes
            // 
            this.cb_Mes.FormattingEnabled = true;
            this.cb_Mes.Location = new System.Drawing.Point(146, 63);
            this.cb_Mes.Name = "cb_Mes";
            this.cb_Mes.Size = new System.Drawing.Size(168, 21);
            this.cb_Mes.TabIndex = 7;
            // 
            // bt_DescargarCsv
            // 
            this.bt_DescargarCsv.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bt_DescargarCsv.ImageOptions.Image")));
            this.bt_DescargarCsv.Location = new System.Drawing.Point(175, 228);
            this.bt_DescargarCsv.Name = "bt_DescargarCsv";
            this.bt_DescargarCsv.Size = new System.Drawing.Size(129, 45);
            this.bt_DescargarCsv.TabIndex = 6;
            this.bt_DescargarCsv.Text = "Descargar CSV";
            this.bt_DescargarCsv.Click += new System.EventHandler(this.bt_DescargarCsv_Click);
            // 
            // bt_DescargaXls
            // 
            this.bt_DescargaXls.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bt_DescargaXls.ImageOptions.Image")));
            this.bt_DescargaXls.Location = new System.Drawing.Point(40, 228);
            this.bt_DescargaXls.Name = "bt_DescargaXls";
            this.bt_DescargaXls.Size = new System.Drawing.Size(129, 45);
            this.bt_DescargaXls.TabIndex = 5;
            this.bt_DescargaXls.Text = "Descargar XLS";
            this.bt_DescargaXls.Click += new System.EventHandler(this.bt_DescargaXls_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(146, 44);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(23, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Mes";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(40, 44);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(22, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Año";
            // 
            // txt_Anio
            // 
            this.txt_Anio.Location = new System.Drawing.Point(40, 63);
            this.txt_Anio.Name = "txt_Anio";
            this.txt_Anio.Size = new System.Drawing.Size(86, 20);
            this.txt_Anio.TabIndex = 1;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.ck_Anexo5);
            this.groupControl2.Controls.Add(this.ck_Anexo3);
            this.groupControl2.Controls.Add(this.ck_Anexo2);
            this.groupControl2.Controls.Add(this.ck_Anexo1);
            this.groupControl2.Location = new System.Drawing.Point(40, 89);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(525, 133);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Seleccione el Informe a Emitir";
            // 
            // ck_Anexo5
            // 
            this.ck_Anexo5.Location = new System.Drawing.Point(5, 104);
            this.ck_Anexo5.Name = "ck_Anexo5";
            this.ck_Anexo5.Properties.Caption = "Anexo Contribuyente";
            this.ck_Anexo5.Size = new System.Drawing.Size(212, 20);
            this.ck_Anexo5.TabIndex = 3;
            // 
            // ck_Anexo3
            // 
            this.ck_Anexo3.Location = new System.Drawing.Point(6, 78);
            this.ck_Anexo3.Name = "ck_Anexo3";
            this.ck_Anexo3.Properties.Caption = "Anexo Consumidor Final";
            this.ck_Anexo3.Size = new System.Drawing.Size(212, 20);
            this.ck_Anexo3.TabIndex = 2;
            // 
            // ck_Anexo2
            // 
            this.ck_Anexo2.Location = new System.Drawing.Point(6, 52);
            this.ck_Anexo2.Name = "ck_Anexo2";
            this.ck_Anexo2.Properties.Caption = "Anexo Exportacion";
            this.ck_Anexo2.Size = new System.Drawing.Size(212, 20);
            this.ck_Anexo2.TabIndex = 1;
            // 
            // ck_Anexo1
            // 
            this.ck_Anexo1.Location = new System.Drawing.Point(5, 26);
            this.ck_Anexo1.Name = "ck_Anexo1";
            this.ck_Anexo1.Properties.Caption = "Anexo Compra";
            this.ck_Anexo1.Size = new System.Drawing.Size(213, 20);
            this.ck_Anexo1.TabIndex = 0;
            // 
            // frmF28
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupControl1);
            this.Name = "frmF28";
            this.Tag = "Consulta";
            this.Text = "DGII F-28";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Anio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ck_Anexo5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ck_Anexo3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ck_Anexo2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ck_Anexo1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnFinalizar;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ProgressBar progressBar1;
        private DevExpress.XtraEditors.SimpleButton bt_AbrirDirectorio;
        private System.Windows.Forms.ComboBox cb_Mes;
        private DevExpress.XtraEditors.SimpleButton bt_DescargarCsv;
        private DevExpress.XtraEditors.SimpleButton bt_DescargaXls;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txt_Anio;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.CheckEdit ck_Anexo5;
        private DevExpress.XtraEditors.CheckEdit ck_Anexo3;
        private DevExpress.XtraEditors.CheckEdit ck_Anexo2;
        private DevExpress.XtraEditors.CheckEdit ck_Anexo1;
    }
}