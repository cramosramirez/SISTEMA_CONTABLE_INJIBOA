
namespace SistemaContable.UI.Forms.Reportes
{
    partial class frmPagosVencidosPorFecha
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbPANTALLA = new System.Windows.Forms.RadioButton();
            this.rbEXCEL = new System.Windows.Forms.RadioButton();
            this.rbPDF = new System.Windows.Forms.RadioButton();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.btnProceder = new System.Windows.Forms.Button();
            this.chkIncluirYaPagados = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtHASTA_DESCRIPCION = new System.Windows.Forms.TextBox();
            this.txtHASTA = new System.Windows.Forms.TextBox();
            this.txtDESDE_DESCRIPCION = new System.Windows.Forms.TextBox();
            this.txtDESDE = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rbRango = new System.Windows.Forms.RadioButton();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbVENCIMIENTO = new System.Windows.Forms.RadioButton();
            this.rbFACTURA = new System.Windows.Forms.RadioButton();
            this.rbRECEPCION = new System.Windows.Forms.RadioButton();
            this.dteFECHA_FIN = new DevExpress.XtraEditors.DateEdit();
            this.dteFECHA_INICIO = new DevExpress.XtraEditors.DateEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_FIN.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_FIN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_INICIO.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_INICIO.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.btnFinalizar);
            this.panel1.Controls.Add(this.btnProceder);
            this.panel1.Controls.Add(this.chkIncluirYaPagados);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(616, 451);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbPANTALLA);
            this.groupBox2.Controls.Add(this.rbEXCEL);
            this.groupBox2.Controls.Add(this.rbPDF);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(147, 313);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 60);
            this.groupBox2.TabIndex = 3;
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
            // btnFinalizar
            // 
            this.btnFinalizar.Location = new System.Drawing.Point(327, 391);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(124, 38);
            this.btnFinalizar.TabIndex = 5;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.UseVisualStyleBackColor = true;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnProceder
            // 
            this.btnProceder.Location = new System.Drawing.Point(166, 391);
            this.btnProceder.Name = "btnProceder";
            this.btnProceder.Size = new System.Drawing.Size(124, 38);
            this.btnProceder.TabIndex = 4;
            this.btnProceder.Text = "Proceder";
            this.btnProceder.UseVisualStyleBackColor = true;
            this.btnProceder.Click += new System.EventHandler(this.btnProceder_Click);
            // 
            // chkIncluirYaPagados
            // 
            this.chkIncluirYaPagados.AutoSize = true;
            this.chkIncluirYaPagados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkIncluirYaPagados.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncluirYaPagados.Location = new System.Drawing.Point(213, 273);
            this.chkIncluirYaPagados.Name = "chkIncluirYaPagados";
            this.chkIncluirYaPagados.Size = new System.Drawing.Size(190, 22);
            this.chkIncluirYaPagados.TabIndex = 2;
            this.chkIncluirYaPagados.Text = "Incluir los ya pagados";
            this.chkIncluirYaPagados.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.txtHASTA_DESCRIPCION);
            this.panel3.Controls.Add(this.txtHASTA);
            this.panel3.Controls.Add(this.txtDESDE_DESCRIPCION);
            this.panel3.Controls.Add(this.txtDESDE);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.rbRango);
            this.panel3.Controls.Add(this.rbTodos);
            this.panel3.Location = new System.Drawing.Point(45, 137);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(515, 129);
            this.panel3.TabIndex = 1;
            // 
            // txtHASTA_DESCRIPCION
            // 
            this.txtHASTA_DESCRIPCION.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHASTA_DESCRIPCION.Location = new System.Drawing.Point(182, 85);
            this.txtHASTA_DESCRIPCION.Name = "txtHASTA_DESCRIPCION";
            this.txtHASTA_DESCRIPCION.ReadOnly = true;
            this.txtHASTA_DESCRIPCION.Size = new System.Drawing.Size(317, 24);
            this.txtHASTA_DESCRIPCION.TabIndex = 5;
            this.txtHASTA_DESCRIPCION.TabStop = false;
            // 
            // txtHASTA
            // 
            this.txtHASTA.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHASTA.Location = new System.Drawing.Point(71, 84);
            this.txtHASTA.MaxLength = 20;
            this.txtHASTA.Name = "txtHASTA";
            this.txtHASTA.ReadOnly = true;
            this.txtHASTA.Size = new System.Drawing.Size(105, 24);
            this.txtHASTA.TabIndex = 4;
            this.txtHASTA.Leave += new System.EventHandler(this.txtHASTA_Leave);
            // 
            // txtDESDE_DESCRIPCION
            // 
            this.txtDESDE_DESCRIPCION.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDESDE_DESCRIPCION.Location = new System.Drawing.Point(182, 56);
            this.txtDESDE_DESCRIPCION.Name = "txtDESDE_DESCRIPCION";
            this.txtDESDE_DESCRIPCION.ReadOnly = true;
            this.txtDESDE_DESCRIPCION.Size = new System.Drawing.Size(317, 24);
            this.txtDESDE_DESCRIPCION.TabIndex = 3;
            this.txtDESDE_DESCRIPCION.TabStop = false;
            // 
            // txtDESDE
            // 
            this.txtDESDE.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDESDE.Location = new System.Drawing.Point(71, 55);
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
            this.label5.Location = new System.Drawing.Point(-8, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 23);
            this.label5.TabIndex = 70;
            this.label5.Text = "Hasta";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(-8, 56);
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
            this.rbRango.Location = new System.Drawing.Point(305, 19);
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
            this.rbTodos.Location = new System.Drawing.Point(87, 19);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(194, 22);
            this.rbTodos.TabIndex = 0;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "Todos los proveedores";
            this.rbTodos.UseVisualStyleBackColor = true;
            this.rbTodos.CheckedChanged += new System.EventHandler(this.rbTodos_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.rbVENCIMIENTO);
            this.panel2.Controls.Add(this.rbFACTURA);
            this.panel2.Controls.Add(this.rbRECEPCION);
            this.panel2.Controls.Add(this.dteFECHA_FIN);
            this.panel2.Controls.Add(this.dteFECHA_INICIO);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Location = new System.Drawing.Point(45, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(515, 97);
            this.panel2.TabIndex = 0;
            // 
            // rbVENCIMIENTO
            // 
            this.rbVENCIMIENTO.AutoSize = true;
            this.rbVENCIMIENTO.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbVENCIMIENTO.Location = new System.Drawing.Point(322, 57);
            this.rbVENCIMIENTO.Name = "rbVENCIMIENTO";
            this.rbVENCIMIENTO.Size = new System.Drawing.Size(118, 22);
            this.rbVENCIMIENTO.TabIndex = 4;
            this.rbVENCIMIENTO.Text = "Vencimiento";
            this.rbVENCIMIENTO.UseVisualStyleBackColor = true;
            // 
            // rbFACTURA
            // 
            this.rbFACTURA.AutoSize = true;
            this.rbFACTURA.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFACTURA.Location = new System.Drawing.Point(214, 57);
            this.rbFACTURA.Name = "rbFACTURA";
            this.rbFACTURA.Size = new System.Drawing.Size(83, 22);
            this.rbFACTURA.TabIndex = 3;
            this.rbFACTURA.Text = "Factura";
            this.rbFACTURA.UseVisualStyleBackColor = true;
            // 
            // rbRECEPCION
            // 
            this.rbRECEPCION.AutoSize = true;
            this.rbRECEPCION.Checked = true;
            this.rbRECEPCION.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRECEPCION.Location = new System.Drawing.Point(84, 57);
            this.rbRECEPCION.Name = "rbRECEPCION";
            this.rbRECEPCION.Size = new System.Drawing.Size(103, 22);
            this.rbRECEPCION.TabIndex = 2;
            this.rbRECEPCION.TabStop = true;
            this.rbRECEPCION.Text = "Recepción";
            this.rbRECEPCION.UseVisualStyleBackColor = true;
            // 
            // dteFECHA_FIN
            // 
            this.dteFECHA_FIN.EditValue = null;
            this.dteFECHA_FIN.EnterMoveNextControl = true;
            this.dteFECHA_FIN.Location = new System.Drawing.Point(313, 15);
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
            this.dteFECHA_FIN.Size = new System.Drawing.Size(123, 24);
            this.dteFECHA_FIN.TabIndex = 1;
            // 
            // dteFECHA_INICIO
            // 
            this.dteFECHA_INICIO.EditValue = null;
            this.dteFECHA_INICIO.EnterMoveNextControl = true;
            this.dteFECHA_INICIO.Location = new System.Drawing.Point(174, 15);
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
            this.dteFECHA_INICIO.Size = new System.Drawing.Size(123, 24);
            this.dteFECHA_INICIO.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(14, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(154, 23);
            this.label11.TabIndex = 44;
            this.label11.Text = "Rango de Fechas";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmPagosVencidosPorFecha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 451);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPagosVencidosPorFecha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pagos Vencidos por Fecha";
            this.Load += new System.EventHandler(this.frmPagosVencidosPorFecha_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_FIN.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_FIN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_INICIO.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_INICIO.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.DateEdit dteFECHA_FIN;
        private DevExpress.XtraEditors.DateEdit dteFECHA_INICIO;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton rbVENCIMIENTO;
        private System.Windows.Forms.RadioButton rbFACTURA;
        private System.Windows.Forms.RadioButton rbRECEPCION;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbRango;
        private System.Windows.Forms.RadioButton rbTodos;
        private System.Windows.Forms.TextBox txtHASTA_DESCRIPCION;
        private System.Windows.Forms.TextBox txtHASTA;
        private System.Windows.Forms.TextBox txtDESDE_DESCRIPCION;
        private System.Windows.Forms.TextBox txtDESDE;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkIncluirYaPagados;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbPANTALLA;
        private System.Windows.Forms.RadioButton rbEXCEL;
        private System.Windows.Forms.RadioButton rbPDF;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.Button btnProceder;
    }
}