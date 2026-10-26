
namespace SistemaContable.UI.Forms.Reportes
{
    partial class frmSaldosPorproveedor
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPANTALLA = new System.Windows.Forms.RadioButton();
            this.rbEXCEL = new System.Windows.Forms.RadioButton();
            this.rbPDF = new System.Windows.Forms.RadioButton();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.btnProceder = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dteFECHA = new DevExpress.XtraEditors.DateEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.txtHASTA_DESCRIPCION = new System.Windows.Forms.TextBox();
            this.txtHASTA = new System.Windows.Forms.TextBox();
            this.txtDESDE_DESCRIPCION = new System.Windows.Forms.TextBox();
            this.txtDESDE = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rbRango = new System.Windows.Forms.RadioButton();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnFinalizar);
            this.panel1.Controls.Add(this.btnProceder);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(696, 324);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbPANTALLA);
            this.groupBox1.Controls.Add(this.rbEXCEL);
            this.groupBox1.Controls.Add(this.rbPDF);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(187, 184);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 60);
            this.groupBox1.TabIndex = 1;
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
            this.btnFinalizar.Location = new System.Drawing.Point(367, 262);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(124, 38);
            this.btnFinalizar.TabIndex = 3;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.UseVisualStyleBackColor = true;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnProceder
            // 
            this.btnProceder.Location = new System.Drawing.Point(206, 262);
            this.btnProceder.Name = "btnProceder";
            this.btnProceder.Size = new System.Drawing.Size(124, 38);
            this.btnProceder.TabIndex = 2;
            this.btnProceder.Text = "Proceder";
            this.btnProceder.UseVisualStyleBackColor = true;
            this.btnProceder.Click += new System.EventHandler(this.btnProceder_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.dteFECHA);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.separatorControl2);
            this.panel3.Controls.Add(this.separatorControl1);
            this.panel3.Controls.Add(this.txtHASTA_DESCRIPCION);
            this.panel3.Controls.Add(this.txtHASTA);
            this.panel3.Controls.Add(this.txtDESDE_DESCRIPCION);
            this.panel3.Controls.Add(this.txtDESDE);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.rbRango);
            this.panel3.Controls.Add(this.rbTodos);
            this.panel3.Location = new System.Drawing.Point(23, 21);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(652, 142);
            this.panel3.TabIndex = 0;
            // 
            // dteFECHA
            // 
            this.dteFECHA.EditValue = null;
            this.dteFECHA.EnterMoveNextControl = true;
            this.dteFECHA.Location = new System.Drawing.Point(100, 103);
            this.dteFECHA.Name = "dteFECHA";
            this.dteFECHA.Properties.AllowMouseWheel = false;
            this.dteFECHA.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dteFECHA.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dteFECHA.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.dteFECHA.Properties.Appearance.Options.UseFont = true;
            this.dteFECHA.Properties.Appearance.Options.UseForeColor = true;
            this.dteFECHA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFECHA.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFECHA.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dteFECHA.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteFECHA.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dteFECHA.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteFECHA.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dteFECHA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dteFECHA.Size = new System.Drawing.Size(123, 24);
            this.dteFECHA.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(4, 103);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 23);
            this.label11.TabIndex = 74;
            this.label11.Text = "Saldos al";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // separatorControl2
            // 
            this.separatorControl2.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.separatorControl2.LineThickness = 2;
            this.separatorControl2.Location = new System.Drawing.Point(123, -2);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.separatorControl2.Size = new System.Drawing.Size(20, 93);
            this.separatorControl2.TabIndex = 71;
            // 
            // separatorControl1
            // 
            this.separatorControl1.LineThickness = 2;
            this.separatorControl1.Location = new System.Drawing.Point(-2, 76);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.separatorControl1.Size = new System.Drawing.Size(652, 24);
            this.separatorControl1.TabIndex = 72;
            // 
            // txtHASTA_DESCRIPCION
            // 
            this.txtHASTA_DESCRIPCION.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHASTA_DESCRIPCION.Location = new System.Drawing.Point(316, 46);
            this.txtHASTA_DESCRIPCION.Name = "txtHASTA_DESCRIPCION";
            this.txtHASTA_DESCRIPCION.ReadOnly = true;
            this.txtHASTA_DESCRIPCION.Size = new System.Drawing.Size(317, 24);
            this.txtHASTA_DESCRIPCION.TabIndex = 5;
            this.txtHASTA_DESCRIPCION.TabStop = false;
            // 
            // txtHASTA
            // 
            this.txtHASTA.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHASTA.Location = new System.Drawing.Point(205, 45);
            this.txtHASTA.MaxLength = 20;
            this.txtHASTA.Name = "txtHASTA";
            this.txtHASTA.ReadOnly = true;
            this.txtHASTA.Size = new System.Drawing.Size(105, 24);
            this.txtHASTA.TabIndex = 3;            
            this.txtHASTA.Leave += new System.EventHandler(this.txtHASTA_Leave);
            // 
            // txtDESDE_DESCRIPCION
            // 
            this.txtDESDE_DESCRIPCION.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDESDE_DESCRIPCION.Location = new System.Drawing.Point(316, 17);
            this.txtDESDE_DESCRIPCION.Name = "txtDESDE_DESCRIPCION";
            this.txtDESDE_DESCRIPCION.ReadOnly = true;
            this.txtDESDE_DESCRIPCION.Size = new System.Drawing.Size(317, 24);
            this.txtDESDE_DESCRIPCION.TabIndex = 3;
            this.txtDESDE_DESCRIPCION.TabStop = false;
            // 
            // txtDESDE
            // 
            this.txtDESDE.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDESDE.Location = new System.Drawing.Point(205, 16);
            this.txtDESDE.MaxLength = 20;
            this.txtDESDE.Name = "txtDESDE";
            this.txtDESDE.ReadOnly = true;
            this.txtDESDE.Size = new System.Drawing.Size(105, 24);
            this.txtDESDE.TabIndex = 2;
            this.txtDESDE.Leave += new System.EventHandler(this.txtDESDE_Leave);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(126, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 23);
            this.label5.TabIndex = 70;
            this.label5.Text = "Hasta";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(126, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 23);
            this.label4.TabIndex = 69;
            this.label4.Text = "Desde";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rbRango
            // 
            this.rbRango.AutoSize = true;
            this.rbRango.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRango.Location = new System.Drawing.Point(20, 45);
            this.rbRango.Name = "rbRango";
            this.rbRango.Size = new System.Drawing.Size(97, 22);
            this.rbRango.TabIndex = 1;
            this.rbRango.Text = "Un Rango";
            this.rbRango.UseVisualStyleBackColor = true;
            this.rbRango.CheckedChanged += new System.EventHandler(this.rbRango_CheckedChanged);
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Checked = true;
            this.rbTodos.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTodos.Location = new System.Drawing.Point(20, 19);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(70, 22);
            this.rbTodos.TabIndex = 0;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            this.rbTodos.CheckedChanged += new System.EventHandler(this.rbTodos_CheckedChanged);
            // 
            // frmSaldosPorproveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 324);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSaldosPorproveedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Saldos por Proveedor";
            this.Load += new System.EventHandler(this.frmSaldosPorproveedor_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtHASTA_DESCRIPCION;
        private System.Windows.Forms.TextBox txtHASTA;
        private System.Windows.Forms.TextBox txtDESDE_DESCRIPCION;
        private System.Windows.Forms.TextBox txtDESDE;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbRango;
        private System.Windows.Forms.RadioButton rbTodos;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.DateEdit dteFECHA;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbPANTALLA;
        private System.Windows.Forms.RadioButton rbEXCEL;
        private System.Windows.Forms.RadioButton rbPDF;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.Button btnProceder;
    }
}