
namespace SistemaContable.UI.Forms.Reportes
{
    partial class frmPagosVencidos
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
            this.label5 = new System.Windows.Forms.Label();
            this.txtC = new System.Windows.Forms.TextBox();
            this.txtB = new System.Windows.Forms.TextBox();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.btnProceder = new System.Windows.Forms.Button();
            this.txtHASTA_DESCRIPCION = new System.Windows.Forms.TextBox();
            this.txtHASTA = new System.Windows.Forms.TextBox();
            this.txtDESDE_DESCRIPCION = new System.Windows.Forms.TextBox();
            this.txtDESDE = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxTIPO_ORDEN = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rbtINCLUIR_VENCIDOS_N = new System.Windows.Forms.RadioButton();
            this.rbtINCLUIR_VENCIDOS_S = new System.Windows.Forms.RadioButton();
            this.txtA = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbtRESUMEN = new System.Windows.Forms.RadioButton();
            this.rbtDETALLADO = new System.Windows.Forms.RadioButton();
            this.txtD = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPANTALLA = new System.Windows.Forms.RadioButton();
            this.rbEXCEL = new System.Windows.Forms.RadioButton();
            this.rbPDF = new System.Windows.Forms.RadioButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rbtAlfabetico = new System.Windows.Forms.RadioButton();
            this.rbtPorCodigo = new System.Windows.Forms.RadioButton();
            this.chkInluirPlazoProveedores = new System.Windows.Forms.CheckBox();
            this.dteFECHA = new DevExpress.XtraEditors.DateEdit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 394);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 23);
            this.label5.TabIndex = 51;
            this.label5.Text = "Hasta";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtC
            // 
            this.txtC.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtC.Location = new System.Drawing.Point(124, 44);
            this.txtC.Name = "txtC";
            this.txtC.ReadOnly = true;
            this.txtC.Size = new System.Drawing.Size(41, 24);
            this.txtC.TabIndex = 2;
            this.txtC.Text = "90";
            this.txtC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtB
            // 
            this.txtB.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtB.Location = new System.Drawing.Point(77, 44);
            this.txtB.Name = "txtB";
            this.txtB.ReadOnly = true;
            this.txtB.Size = new System.Drawing.Size(41, 24);
            this.txtB.TabIndex = 1;
            this.txtB.Text = "60";
            this.txtB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Location = new System.Drawing.Point(285, 502);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(124, 38);
            this.btnFinalizar.TabIndex = 9;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.UseVisualStyleBackColor = true;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnProceder
            // 
            this.btnProceder.Location = new System.Drawing.Point(124, 502);
            this.btnProceder.Name = "btnProceder";
            this.btnProceder.Size = new System.Drawing.Size(124, 38);
            this.btnProceder.TabIndex = 8;
            this.btnProceder.Text = "Proceder";
            this.btnProceder.UseVisualStyleBackColor = true;
            this.btnProceder.Click += new System.EventHandler(this.btnProceder_Click);
            // 
            // txtHASTA_DESCRIPCION
            // 
            this.txtHASTA_DESCRIPCION.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHASTA_DESCRIPCION.Location = new System.Drawing.Point(203, 394);
            this.txtHASTA_DESCRIPCION.Name = "txtHASTA_DESCRIPCION";
            this.txtHASTA_DESCRIPCION.ReadOnly = true;
            this.txtHASTA_DESCRIPCION.Size = new System.Drawing.Size(295, 24);
            this.txtHASTA_DESCRIPCION.TabIndex = 55;
            // 
            // txtHASTA
            // 
            this.txtHASTA.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHASTA.Location = new System.Drawing.Point(92, 393);
            this.txtHASTA.Name = "txtHASTA";
            this.txtHASTA.Size = new System.Drawing.Size(105, 24);
            this.txtHASTA.TabIndex = 6;
            // 
            // txtDESDE_DESCRIPCION
            // 
            this.txtDESDE_DESCRIPCION.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDESDE_DESCRIPCION.Location = new System.Drawing.Point(203, 365);
            this.txtDESDE_DESCRIPCION.Name = "txtDESDE_DESCRIPCION";
            this.txtDESDE_DESCRIPCION.ReadOnly = true;
            this.txtDESDE_DESCRIPCION.Size = new System.Drawing.Size(295, 24);
            this.txtDESDE_DESCRIPCION.TabIndex = 53;
            // 
            // txtDESDE
            // 
            this.txtDESDE.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDESDE.Location = new System.Drawing.Point(92, 364);
            this.txtDESDE.Name = "txtDESDE";
            this.txtDESDE.Size = new System.Drawing.Size(105, 24);
            this.txtDESDE.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 365);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 23);
            this.label4.TabIndex = 50;
            this.label4.Text = "Desde";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbxTIPO_ORDEN
            // 
            this.cbxTIPO_ORDEN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTIPO_ORDEN.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTIPO_ORDEN.FormattingEnabled = true;
            this.cbxTIPO_ORDEN.Location = new System.Drawing.Point(105, 265);
            this.cbxTIPO_ORDEN.Name = "cbxTIPO_ORDEN";
            this.cbxTIPO_ORDEN.Size = new System.Drawing.Size(330, 25);
            this.cbxTIPO_ORDEN.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(277, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 23);
            this.label3.TabIndex = 48;
            this.label3.Text = "Incluir los ya vencidos";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(105, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 23);
            this.label2.TabIndex = 47;
            this.label2.Text = "Tipo de Reporte";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.rbtINCLUIR_VENCIDOS_N);
            this.panel4.Controls.Add(this.rbtINCLUIR_VENCIDOS_S);
            this.panel4.Location = new System.Drawing.Point(280, 186);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(155, 73);
            this.panel4.TabIndex = 3;
            // 
            // rbtINCLUIR_VENCIDOS_N
            // 
            this.rbtINCLUIR_VENCIDOS_N.AutoSize = true;
            this.rbtINCLUIR_VENCIDOS_N.Font = new System.Drawing.Font("Tahoma", 8.139131F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtINCLUIR_VENCIDOS_N.Location = new System.Drawing.Point(57, 37);
            this.rbtINCLUIR_VENCIDOS_N.Name = "rbtINCLUIR_VENCIDOS_N";
            this.rbtINCLUIR_VENCIDOS_N.Size = new System.Drawing.Size(46, 21);
            this.rbtINCLUIR_VENCIDOS_N.TabIndex = 1;
            this.rbtINCLUIR_VENCIDOS_N.Text = "No";
            this.rbtINCLUIR_VENCIDOS_N.UseVisualStyleBackColor = true;
            // 
            // rbtINCLUIR_VENCIDOS_S
            // 
            this.rbtINCLUIR_VENCIDOS_S.AutoSize = true;
            this.rbtINCLUIR_VENCIDOS_S.Checked = true;
            this.rbtINCLUIR_VENCIDOS_S.Font = new System.Drawing.Font("Tahoma", 8.139131F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtINCLUIR_VENCIDOS_S.Location = new System.Drawing.Point(57, 10);
            this.rbtINCLUIR_VENCIDOS_S.Name = "rbtINCLUIR_VENCIDOS_S";
            this.rbtINCLUIR_VENCIDOS_S.Size = new System.Drawing.Size(38, 21);
            this.rbtINCLUIR_VENCIDOS_S.TabIndex = 0;
            this.rbtINCLUIR_VENCIDOS_S.TabStop = true;
            this.rbtINCLUIR_VENCIDOS_S.Text = "Si";
            this.rbtINCLUIR_VENCIDOS_S.UseVisualStyleBackColor = true;
            // 
            // txtA
            // 
            this.txtA.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtA.Location = new System.Drawing.Point(30, 44);
            this.txtA.Name = "txtA";
            this.txtA.ReadOnly = true;
            this.txtA.Size = new System.Drawing.Size(41, 24);
            this.txtA.TabIndex = 0;
            this.txtA.Text = "30";
            this.txtA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(54, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 23);
            this.label1.TabIndex = 43;
            this.label1.Text = "Períodos Dia:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.rbtRESUMEN);
            this.panel3.Controls.Add(this.rbtDETALLADO);
            this.panel3.Location = new System.Drawing.Point(105, 186);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(155, 73);
            this.panel3.TabIndex = 2;
            // 
            // rbtRESUMEN
            // 
            this.rbtRESUMEN.AutoSize = true;
            this.rbtRESUMEN.Font = new System.Drawing.Font("Tahoma", 8.139131F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtRESUMEN.Location = new System.Drawing.Point(21, 39);
            this.rbtRESUMEN.Name = "rbtRESUMEN";
            this.rbtRESUMEN.Size = new System.Drawing.Size(90, 21);
            this.rbtRESUMEN.TabIndex = 1;
            this.rbtRESUMEN.Text = "Resumen";
            this.rbtRESUMEN.UseVisualStyleBackColor = true;
            // 
            // rbtDETALLADO
            // 
            this.rbtDETALLADO.AutoSize = true;
            this.rbtDETALLADO.Checked = true;
            this.rbtDETALLADO.Font = new System.Drawing.Font("Tahoma", 8.139131F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtDETALLADO.Location = new System.Drawing.Point(21, 12);
            this.rbtDETALLADO.Name = "rbtDETALLADO";
            this.rbtDETALLADO.Size = new System.Drawing.Size(90, 21);
            this.rbtDETALLADO.TabIndex = 0;
            this.rbtDETALLADO.TabStop = true;
            this.rbtDETALLADO.Text = "Detallado";
            this.rbtDETALLADO.UseVisualStyleBackColor = true;
            // 
            // txtD
            // 
            this.txtD.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtD.Location = new System.Drawing.Point(171, 44);
            this.txtD.Name = "txtD";
            this.txtD.ReadOnly = true;
            this.txtD.Size = new System.Drawing.Size(41, 24);
            this.txtD.TabIndex = 3;
            this.txtD.Text = "120";
            this.txtD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.txtD);
            this.panel2.Controls.Add(this.txtC);
            this.panel2.Controls.Add(this.txtB);
            this.panel2.Controls.Add(this.txtA);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(143, 68);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(251, 85);
            this.panel2.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(76, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(170, 23);
            this.label11.TabIndex = 42;
            this.label11.Text = "Reporte al";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.chkInluirPlazoProveedores);
            this.panel1.Controls.Add(this.btnFinalizar);
            this.panel1.Controls.Add(this.btnProceder);
            this.panel1.Controls.Add(this.txtHASTA_DESCRIPCION);
            this.panel1.Controls.Add(this.txtHASTA);
            this.panel1.Controls.Add(this.txtDESDE_DESCRIPCION);
            this.panel1.Controls.Add(this.txtDESDE);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cbxTIPO_ORDEN);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.dteFECHA);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(534, 561);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbPANTALLA);
            this.groupBox1.Controls.Add(this.rbEXCEL);
            this.groupBox1.Controls.Add(this.rbPDF);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(106, 431);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 60);
            this.groupBox1.TabIndex = 58;
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
            this.rbEXCEL.Location = new System.Drawing.Point(211, 24);
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
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.rbtAlfabetico);
            this.panel5.Controls.Add(this.rbtPorCodigo);
            this.panel5.Location = new System.Drawing.Point(152, 319);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(240, 34);
            this.panel5.TabIndex = 57;
            // 
            // rbtAlfabetico
            // 
            this.rbtAlfabetico.AutoSize = true;
            this.rbtAlfabetico.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtAlfabetico.Location = new System.Drawing.Point(124, 5);
            this.rbtAlfabetico.Name = "rbtAlfabetico";
            this.rbtAlfabetico.Size = new System.Drawing.Size(102, 22);
            this.rbtAlfabetico.TabIndex = 1;
            this.rbtAlfabetico.Text = "Alfabetico";
            this.rbtAlfabetico.UseVisualStyleBackColor = true;
            // 
            // rbtPorCodigo
            // 
            this.rbtPorCodigo.AutoSize = true;
            this.rbtPorCodigo.Checked = true;
            this.rbtPorCodigo.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtPorCodigo.Location = new System.Drawing.Point(15, 5);
            this.rbtPorCodigo.Name = "rbtPorCodigo";
            this.rbtPorCodigo.Size = new System.Drawing.Size(107, 22);
            this.rbtPorCodigo.TabIndex = 0;
            this.rbtPorCodigo.TabStop = true;
            this.rbtPorCodigo.Text = "Por Código";
            this.rbtPorCodigo.UseVisualStyleBackColor = true;
            // 
            // chkInluirPlazoProveedores
            // 
            this.chkInluirPlazoProveedores.AutoSize = true;
            this.chkInluirPlazoProveedores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkInluirPlazoProveedores.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInluirPlazoProveedores.Location = new System.Drawing.Point(136, 295);
            this.chkInluirPlazoProveedores.Name = "chkInluirPlazoProveedores";
            this.chkInluirPlazoProveedores.Size = new System.Drawing.Size(266, 22);
            this.chkInluirPlazoProveedores.TabIndex = 56;
            this.chkInluirPlazoProveedores.Text = "Incluir plazo de los proveedores";
            this.chkInluirPlazoProveedores.UseVisualStyleBackColor = true;
            // 
            // dteFECHA
            // 
            this.dteFECHA.EditValue = null;
            this.dteFECHA.EnterMoveNextControl = true;
            this.dteFECHA.Location = new System.Drawing.Point(252, 27);
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
            this.dteFECHA.TabIndex = 0;
            // 
            // frmPagosVencidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 561);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPagosVencidos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pagos Vencidos";
            this.Load += new System.EventHandler(this.frmPagosVencidos_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtC;
        private System.Windows.Forms.TextBox txtB;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.Button btnProceder;
        private System.Windows.Forms.TextBox txtHASTA_DESCRIPCION;
        private System.Windows.Forms.TextBox txtHASTA;
        private System.Windows.Forms.TextBox txtDESDE_DESCRIPCION;
        private System.Windows.Forms.TextBox txtDESDE;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxTIPO_ORDEN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rbtINCLUIR_VENCIDOS_N;
        private System.Windows.Forms.RadioButton rbtINCLUIR_VENCIDOS_S;
        private DevExpress.XtraEditors.DateEdit dteFECHA;
        private System.Windows.Forms.TextBox txtA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbtRESUMEN;
        private System.Windows.Forms.RadioButton rbtDETALLADO;
        private System.Windows.Forms.TextBox txtD;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton rbtAlfabetico;
        private System.Windows.Forms.RadioButton rbtPorCodigo;
        private System.Windows.Forms.CheckBox chkInluirPlazoProveedores;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbPANTALLA;
        private System.Windows.Forms.RadioButton rbEXCEL;
        private System.Windows.Forms.RadioButton rbPDF;
    }
}