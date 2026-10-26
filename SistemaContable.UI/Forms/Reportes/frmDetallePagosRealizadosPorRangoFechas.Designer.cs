
namespace SistemaContable.UI.Forms.Reportes
{
    partial class frmDetallePagosRealizadosPorRangoFechas
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dteFECHA_FIN = new DevExpress.XtraEditors.DateEdit();
            this.dteFECHA_INICIO = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPANTALLA = new System.Windows.Forms.RadioButton();
            this.rbEXCEL = new System.Windows.Forms.RadioButton();
            this.rbPDF = new System.Windows.Forms.RadioButton();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.btnProceder = new System.Windows.Forms.Button();
            this.txtHASTA_DESCRIPCION = new System.Windows.Forms.TextBox();
            this.txtHASTA = new System.Windows.Forms.TextBox();
            this.txtDESDE_DESCRIPCION = new System.Windows.Forms.TextBox();
            this.txtDESDE = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_FIN.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_FIN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_INICIO.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_INICIO.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dteFECHA_FIN);
            this.panel1.Controls.Add(this.dteFECHA_INICIO);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnFinalizar);
            this.panel1.Controls.Add(this.btnProceder);
            this.panel1.Controls.Add(this.txtHASTA_DESCRIPCION);
            this.panel1.Controls.Add(this.txtHASTA);
            this.panel1.Controls.Add(this.txtDESDE_DESCRIPCION);
            this.panel1.Controls.Add(this.txtDESDE);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(594, 328);
            this.panel1.TabIndex = 0;
            // 
            // dteFECHA_FIN
            // 
            this.dteFECHA_FIN.EditValue = null;
            this.dteFECHA_FIN.EnterMoveNextControl = true;
            this.dteFECHA_FIN.Location = new System.Drawing.Point(360, 145);
            this.dteFECHA_FIN.Name = "dteFECHA_FIN";
            this.dteFECHA_FIN.Properties.AllowMouseWheel = false;
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
            this.dteFECHA_FIN.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dteFECHA_FIN.Size = new System.Drawing.Size(102, 24);
            this.dteFECHA_FIN.TabIndex = 67;
            // 
            // dteFECHA_INICIO
            // 
            this.dteFECHA_INICIO.EditValue = null;
            this.dteFECHA_INICIO.EnterMoveNextControl = true;
            this.dteFECHA_INICIO.Location = new System.Drawing.Point(204, 146);
            this.dteFECHA_INICIO.Name = "dteFECHA_INICIO";
            this.dteFECHA_INICIO.Properties.AllowMouseWheel = false;
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
            this.dteFECHA_INICIO.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dteFECHA_INICIO.Size = new System.Drawing.Size(102, 24);
            this.dteFECHA_INICIO.TabIndex = 65;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(313, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 22);
            this.label1.TabIndex = 68;
            this.label1.Text = "A:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(132, 149);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(66, 22);
            this.label18.TabIndex = 66;
            this.label18.Text = "De:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbPANTALLA);
            this.groupBox1.Controls.Add(this.rbEXCEL);
            this.groupBox1.Controls.Add(this.rbPDF);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(136, 194);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 60);
            this.groupBox1.TabIndex = 64;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de formato";
            // 
            // rbPANTALLA
            // 
            this.rbPANTALLA.AutoSize = true;
            this.rbPANTALLA.Checked = true;
            this.rbPANTALLA.Location = new System.Drawing.Point(51, 24);
            this.rbPANTALLA.Name = "rbPANTALLA";
            this.rbPANTALLA.Size = new System.Drawing.Size(88, 22);
            this.rbPANTALLA.TabIndex = 0;
            this.rbPANTALLA.TabStop = true;
            this.rbPANTALLA.Text = "Pantalla";
            this.rbPANTALLA.UseVisualStyleBackColor = true;
            // 
            // rbEXCEL
            // 
            this.rbEXCEL.AutoSize = true;
            this.rbEXCEL.Location = new System.Drawing.Point(233, 24);
            this.rbEXCEL.Name = "rbEXCEL";
            this.rbEXCEL.Size = new System.Drawing.Size(66, 22);
            this.rbEXCEL.TabIndex = 2;
            this.rbEXCEL.Text = "Excel";
            this.rbEXCEL.UseVisualStyleBackColor = true;
            // 
            // rbPDF
            // 
            this.rbPDF.AutoSize = true;
            this.rbPDF.Location = new System.Drawing.Point(150, 24);
            this.rbPDF.Name = "rbPDF";
            this.rbPDF.Size = new System.Drawing.Size(51, 22);
            this.rbPDF.TabIndex = 1;
            this.rbPDF.Text = "Pdf";
            this.rbPDF.UseVisualStyleBackColor = true;
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Location = new System.Drawing.Point(316, 272);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(124, 38);
            this.btnFinalizar.TabIndex = 63;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.UseVisualStyleBackColor = true;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnProceder
            // 
            this.btnProceder.Location = new System.Drawing.Point(155, 272);
            this.btnProceder.Name = "btnProceder";
            this.btnProceder.Size = new System.Drawing.Size(124, 38);
            this.btnProceder.TabIndex = 62;
            this.btnProceder.Text = "Proceder";
            this.btnProceder.UseVisualStyleBackColor = true;
            this.btnProceder.Click += new System.EventHandler(this.btnProceder_Click);
            // 
            // txtHASTA_DESCRIPCION
            // 
            this.txtHASTA_DESCRIPCION.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHASTA_DESCRIPCION.Location = new System.Drawing.Point(273, 94);
            this.txtHASTA_DESCRIPCION.Name = "txtHASTA_DESCRIPCION";
            this.txtHASTA_DESCRIPCION.ReadOnly = true;
            this.txtHASTA_DESCRIPCION.Size = new System.Drawing.Size(295, 24);
            this.txtHASTA_DESCRIPCION.TabIndex = 61;
            this.txtHASTA_DESCRIPCION.TabStop = false;
            // 
            // txtHASTA
            // 
            this.txtHASTA.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHASTA.Location = new System.Drawing.Point(162, 93);
            this.txtHASTA.MaxLength = 20;
            this.txtHASTA.Name = "txtHASTA";
            this.txtHASTA.Size = new System.Drawing.Size(105, 24);
            this.txtHASTA.TabIndex = 57;
            this.txtHASTA.Leave += new System.EventHandler(this.txtHASTA_Leave);
            // 
            // txtDESDE_DESCRIPCION
            // 
            this.txtDESDE_DESCRIPCION.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDESDE_DESCRIPCION.Location = new System.Drawing.Point(273, 65);
            this.txtDESDE_DESCRIPCION.Name = "txtDESDE_DESCRIPCION";
            this.txtDESDE_DESCRIPCION.ReadOnly = true;
            this.txtDESDE_DESCRIPCION.Size = new System.Drawing.Size(295, 24);
            this.txtDESDE_DESCRIPCION.TabIndex = 60;
            this.txtDESDE_DESCRIPCION.TabStop = false;
            // 
            // txtDESDE
            // 
            this.txtDESDE.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDESDE.Location = new System.Drawing.Point(162, 64);
            this.txtDESDE.MaxLength = 20;
            this.txtDESDE.Name = "txtDESDE";
            this.txtDESDE.Size = new System.Drawing.Size(105, 24);
            this.txtDESDE.TabIndex = 56;
            this.txtDESDE.Leave += new System.EventHandler(this.txtDESDE_Leave);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(83, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 23);
            this.label5.TabIndex = 59;
            this.label5.Text = "Hasta";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 23);
            this.label4.TabIndex = 58;
            this.label4.Text = "Proveedor, desde";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Segoe UI", 11.26957F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Blue;
            this.label11.Location = new System.Drawing.Point(24, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(547, 30);
            this.label11.TabIndex = 43;
            this.label11.Text = "Detalle de Pagos Realizados por Rango de Fechas ";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmDetallePagosRealizadosPorRangoFechas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 328);
            this.Controls.Add(this.panel1);
            this.Name = "frmDetallePagosRealizadosPorRangoFechas";
            this.Text = "Detalle de Pagos Realizados Por Rango de Fechas";
            this.Load += new System.EventHandler(this.frmDetallePagosRealizadosPorRangoFechas_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_FIN.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_FIN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_INICIO.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_INICIO.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtHASTA_DESCRIPCION;
        private System.Windows.Forms.TextBox txtHASTA;
        private System.Windows.Forms.TextBox txtDESDE_DESCRIPCION;
        private System.Windows.Forms.TextBox txtDESDE;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbPANTALLA;
        private System.Windows.Forms.RadioButton rbEXCEL;
        private System.Windows.Forms.RadioButton rbPDF;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.Button btnProceder;
        private DevExpress.XtraEditors.DateEdit dteFECHA_FIN;
        private DevExpress.XtraEditors.DateEdit dteFECHA_INICIO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label18;
    }
}