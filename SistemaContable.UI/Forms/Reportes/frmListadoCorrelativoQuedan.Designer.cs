
namespace SistemaContable.UI.Forms.Reportes
{
    partial class frmListadoCorrelativoQuedan
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtHASTA_DESCRIPCION = new System.Windows.Forms.TextBox();
            this.txtHASTA = new System.Windows.Forms.TextBox();
            this.txtDESDE_DESCRIPCION = new System.Windows.Forms.TextBox();
            this.txtDESDE = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbRango = new System.Windows.Forms.RadioButton();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rbPorCorrelativoQuedan = new System.Windows.Forms.RadioButton();
            this.rbPorProveedor = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.btnProceder = new System.Windows.Forms.Button();
            this.chkIncluirYaPagados = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbPANTALLA = new System.Windows.Forms.RadioButton();
            this.rbEXCEL = new System.Windows.Forms.RadioButton();
            this.rbPDF = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtHASTA_DESCRIPCION);
            this.groupBox1.Controls.Add(this.txtHASTA);
            this.groupBox1.Controls.Add(this.txtDESDE_DESCRIPCION);
            this.groupBox1.Controls.Add(this.txtDESDE);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.separatorControl2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.panel5);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.separatorControl1);
            this.groupBox1.Location = new System.Drawing.Point(23, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(687, 163);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtHASTA_DESCRIPCION
            // 
            this.txtHASTA_DESCRIPCION.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHASTA_DESCRIPCION.Location = new System.Drawing.Point(362, 121);
            this.txtHASTA_DESCRIPCION.Name = "txtHASTA_DESCRIPCION";
            this.txtHASTA_DESCRIPCION.ReadOnly = true;
            this.txtHASTA_DESCRIPCION.Size = new System.Drawing.Size(306, 24);
            this.txtHASTA_DESCRIPCION.TabIndex = 6;
            this.txtHASTA_DESCRIPCION.TabStop = false;
            // 
            // txtHASTA
            // 
            this.txtHASTA.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHASTA.Location = new System.Drawing.Point(251, 120);
            this.txtHASTA.MaxLength = 20;
            this.txtHASTA.Name = "txtHASTA";
            this.txtHASTA.ReadOnly = true;
            this.txtHASTA.Size = new System.Drawing.Size(105, 24);
            this.txtHASTA.TabIndex = 5;
            this.txtHASTA.Leave += new System.EventHandler(this.txtHASTA_Leave);
            // 
            // txtDESDE_DESCRIPCION
            // 
            this.txtDESDE_DESCRIPCION.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDESDE_DESCRIPCION.Location = new System.Drawing.Point(362, 92);
            this.txtDESDE_DESCRIPCION.Name = "txtDESDE_DESCRIPCION";
            this.txtDESDE_DESCRIPCION.ReadOnly = true;
            this.txtDESDE_DESCRIPCION.Size = new System.Drawing.Size(306, 24);
            this.txtDESDE_DESCRIPCION.TabIndex = 4;
            this.txtDESDE_DESCRIPCION.TabStop = false;
            // 
            // txtDESDE
            // 
            this.txtDESDE.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDESDE.Location = new System.Drawing.Point(251, 91);
            this.txtDESDE.MaxLength = 20;
            this.txtDESDE.Name = "txtDESDE";
            this.txtDESDE.ReadOnly = true;
            this.txtDESDE.Size = new System.Drawing.Size(105, 24);
            this.txtDESDE.TabIndex = 3;
            this.txtDESDE.Leave += new System.EventHandler(this.txtDESDE_Leave);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(172, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 23);
            this.label5.TabIndex = 64;
            this.label5.Text = "Hasta";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(172, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 23);
            this.label4.TabIndex = 63;
            this.label4.Text = "Desde";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // separatorControl2
            // 
            this.separatorControl2.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.separatorControl2.LineThickness = 2;
            this.separatorControl2.Location = new System.Drawing.Point(161, 73);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.separatorControl2.Size = new System.Drawing.Size(20, 83);
            this.separatorControl2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbRango);
            this.panel1.Controls.Add(this.rbTodos);
            this.panel1.Location = new System.Drawing.Point(19, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(133, 60);
            this.panel1.TabIndex = 1;
            // 
            // rbRango
            // 
            this.rbRango.AutoSize = true;
            this.rbRango.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRango.Location = new System.Drawing.Point(15, 32);
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
            this.rbTodos.Location = new System.Drawing.Point(15, 5);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(70, 22);
            this.rbTodos.TabIndex = 0;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            this.rbTodos.CheckedChanged += new System.EventHandler(this.rbTodos_CheckedChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rbPorCorrelativoQuedan);
            this.panel5.Controls.Add(this.rbPorProveedor);
            this.panel5.Location = new System.Drawing.Point(235, 24);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(372, 34);
            this.panel5.TabIndex = 0;
            // 
            // rbPorCorrelativoQuedan
            // 
            this.rbPorCorrelativoQuedan.AutoSize = true;
            this.rbPorCorrelativoQuedan.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPorCorrelativoQuedan.Location = new System.Drawing.Point(175, 5);
            this.rbPorCorrelativoQuedan.Name = "rbPorCorrelativoQuedan";
            this.rbPorCorrelativoQuedan.Size = new System.Drawing.Size(194, 22);
            this.rbPorCorrelativoQuedan.TabIndex = 1;
            this.rbPorCorrelativoQuedan.Text = "Correlativo de Quedan";
            this.rbPorCorrelativoQuedan.UseVisualStyleBackColor = true;
            // 
            // rbPorProveedor
            // 
            this.rbPorProveedor.AutoSize = true;
            this.rbPorProveedor.Checked = true;
            this.rbPorProveedor.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPorProveedor.Location = new System.Drawing.Point(15, 5);
            this.rbPorProveedor.Name = "rbPorProveedor";
            this.rbPorProveedor.Size = new System.Drawing.Size(134, 22);
            this.rbPorProveedor.TabIndex = 0;
            this.rbPorProveedor.TabStop = true;
            this.rbPorProveedor.Text = "Por Proveedor";
            this.rbPorProveedor.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(80, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(137, 24);
            this.label11.TabIndex = 43;
            this.label11.Text = "Ordenado por:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // separatorControl1
            // 
            this.separatorControl1.LineThickness = 2;
            this.separatorControl1.Location = new System.Drawing.Point(2, 59);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.separatorControl1.Size = new System.Drawing.Size(684, 24);
            this.separatorControl1.TabIndex = 0;
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Location = new System.Drawing.Point(386, 308);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(124, 38);
            this.btnFinalizar.TabIndex = 3;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.UseVisualStyleBackColor = true;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnProceder
            // 
            this.btnProceder.Location = new System.Drawing.Point(225, 308);
            this.btnProceder.Name = "btnProceder";
            this.btnProceder.Size = new System.Drawing.Size(124, 38);
            this.btnProceder.TabIndex = 2;
            this.btnProceder.Text = "Proceder";
            this.btnProceder.UseVisualStyleBackColor = true;
            this.btnProceder.Click += new System.EventHandler(this.btnProceder_Click);
            // 
            // chkIncluirYaPagados
            // 
            this.chkIncluirYaPagados.AutoSize = true;
            this.chkIncluirYaPagados.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncluirYaPagados.Location = new System.Drawing.Point(272, 191);
            this.chkIncluirYaPagados.Name = "chkIncluirYaPagados";
            this.chkIncluirYaPagados.Size = new System.Drawing.Size(191, 22);
            this.chkIncluirYaPagados.TabIndex = 0;
            this.chkIncluirYaPagados.Text = "Incluir los ya pagados";
            this.chkIncluirYaPagados.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbPANTALLA);
            this.groupBox2.Controls.Add(this.rbEXCEL);
            this.groupBox2.Controls.Add(this.rbPDF);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(206, 230);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 60);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tipo de formato";
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
            this.rbEXCEL.Location = new System.Drawing.Point(214, 24);
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
            // frmListadoCorrelativoQuedan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 367);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.chkIncluirYaPagados);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.btnProceder);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListadoCorrelativoQuedan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Listado Correlativo de Quedan";
            this.Load += new System.EventHandler(this.frmListadoCorrelativoQuedan_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbRango;
        private System.Windows.Forms.RadioButton rbTodos;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton rbPorCorrelativoQuedan;
        private System.Windows.Forms.RadioButton rbPorProveedor;
        private System.Windows.Forms.TextBox txtHASTA_DESCRIPCION;
        private System.Windows.Forms.TextBox txtHASTA;
        private System.Windows.Forms.TextBox txtDESDE_DESCRIPCION;
        private System.Windows.Forms.TextBox txtDESDE;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.Button btnProceder;
        private System.Windows.Forms.CheckBox chkIncluirYaPagados;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbPANTALLA;
        private System.Windows.Forms.RadioButton rbEXCEL;
        private System.Windows.Forms.RadioButton rbPDF;
    }
}