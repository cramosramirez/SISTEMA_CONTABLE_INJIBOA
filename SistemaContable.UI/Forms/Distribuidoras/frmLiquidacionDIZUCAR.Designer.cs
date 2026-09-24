
namespace SistemaContable.UI.Forms.Distribuidoras
{
    partial class frmLiquidacionDIZUCAR
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
            this.panelFiltros = new System.Windows.Forms.Panel();
            this.cbxEMPRESA = new System.Windows.Forms.ComboBox();
            this.btnGenerarTodos = new DevExpress.XtraEditors.SimpleButton();
            this.btnImportarDIZUCAR = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExpandirContraer = new DevExpress.XtraEditors.SimpleButton();
            this.dteFECHA_LIQUIDACION = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.panelResumen = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblTOTAL_REINTEGROS = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblREMESA = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblTOTAL_GASTOS = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTOTAL_CREDITO = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTOTAL_VENTAS = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panelGASTOS = new System.Windows.Forms.Panel();
            this.tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
            this.tabNavigationPage1 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridCLQ = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabNavigationPage2 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridGASTOS = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_LIQUIDACION.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_LIQUIDACION.Properties)).BeginInit();
            this.panelResumen.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelGASTOS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).BeginInit();
            this.tabPane1.SuspendLayout();
            this.tabNavigationPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCLQ)).BeginInit();
            this.tabNavigationPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridGASTOS)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFiltros
            // 
            this.panelFiltros.BackColor = System.Drawing.Color.RoyalBlue;
            this.panelFiltros.Controls.Add(this.cbxEMPRESA);
            this.panelFiltros.Controls.Add(this.btnGenerarTodos);
            this.panelFiltros.Controls.Add(this.btnImportarDIZUCAR);
            this.panelFiltros.Controls.Add(this.label3);
            this.panelFiltros.Controls.Add(this.label2);
            this.panelFiltros.Controls.Add(this.btnExpandirContraer);
            this.panelFiltros.Controls.Add(this.dteFECHA_LIQUIDACION);
            this.panelFiltros.Controls.Add(this.label1);
            this.panelFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltros.Location = new System.Drawing.Point(0, 0);
            this.panelFiltros.Name = "panelFiltros";
            this.panelFiltros.Size = new System.Drawing.Size(1539, 95);
            this.panelFiltros.TabIndex = 0;
            // 
            // cbxEMPRESA
            // 
            this.cbxEMPRESA.Font = new System.Drawing.Font("Tahoma", 11.89565F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxEMPRESA.FormattingEnabled = true;
            this.cbxEMPRESA.Location = new System.Drawing.Point(461, 48);
            this.cbxEMPRESA.Name = "cbxEMPRESA";
            this.cbxEMPRESA.Size = new System.Drawing.Size(342, 31);
            this.cbxEMPRESA.TabIndex = 1;
            // 
            // btnGenerarTodos
            // 
            this.btnGenerarTodos.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarTodos.Appearance.Options.UseFont = true;
            this.btnGenerarTodos.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnGenerarTodos.ImageOptions.ImageToTextIndent = 10;
            this.btnGenerarTodos.Location = new System.Drawing.Point(1150, 53);
            this.btnGenerarTodos.Name = "btnGenerarTodos";
            this.btnGenerarTodos.Size = new System.Drawing.Size(254, 34);
            this.btnGenerarTodos.TabIndex = 2;
            this.btnGenerarTodos.Text = "Generar documentos (CLQ y gastos)";
            this.btnGenerarTodos.Click += new System.EventHandler(this.btnGenerarTodos_Click);
            // 
            // btnImportarDIZUCAR
            // 
            this.btnImportarDIZUCAR.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportarDIZUCAR.Appearance.Options.UseFont = true;
            this.btnImportarDIZUCAR.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnImportarDIZUCAR.ImageOptions.ImageToTextIndent = 10;
            this.btnImportarDIZUCAR.Location = new System.Drawing.Point(1150, 11);
            this.btnImportarDIZUCAR.Name = "btnImportarDIZUCAR";
            this.btnImportarDIZUCAR.Size = new System.Drawing.Size(254, 35);
            this.btnImportarDIZUCAR.TabIndex = 0;
            this.btnImportarDIZUCAR.Text = "Importar API de DIZUCAR";
            this.btnImportarDIZUCAR.Click += new System.EventHandler(this.btnImportarDIZUCAR_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 11.26957F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(368, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 27);
            this.label3.TabIndex = 3;
            this.label3.Text = "Empresa:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.26957F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fecha de Liquidación:";
            // 
            // btnExpandirContraer
            // 
            this.btnExpandirContraer.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpandirContraer.Appearance.Options.UseFont = true;
            this.btnExpandirContraer.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnExpandirContraer.ImageOptions.ImageToTextIndent = 10;
            this.btnExpandirContraer.Location = new System.Drawing.Point(1014, 11);
            this.btnExpandirContraer.Name = "btnExpandirContraer";
            this.btnExpandirContraer.Size = new System.Drawing.Size(119, 34);
            this.btnExpandirContraer.TabIndex = 1;
            this.btnExpandirContraer.Text = "Expandir todos";
            this.btnExpandirContraer.Click += new System.EventHandler(this.btnExpandirContraer_Click);
            // 
            // dteFECHA_LIQUIDACION
            // 
            this.dteFECHA_LIQUIDACION.EditValue = null;
            this.dteFECHA_LIQUIDACION.EnterMoveNextControl = true;
            this.dteFECHA_LIQUIDACION.Location = new System.Drawing.Point(212, 51);
            this.dteFECHA_LIQUIDACION.Name = "dteFECHA_LIQUIDACION";
            this.dteFECHA_LIQUIDACION.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dteFECHA_LIQUIDACION.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.26957F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dteFECHA_LIQUIDACION.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.dteFECHA_LIQUIDACION.Properties.Appearance.Options.UseFont = true;
            this.dteFECHA_LIQUIDACION.Properties.Appearance.Options.UseForeColor = true;
            this.dteFECHA_LIQUIDACION.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFECHA_LIQUIDACION.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFECHA_LIQUIDACION.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dteFECHA_LIQUIDACION.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteFECHA_LIQUIDACION.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dteFECHA_LIQUIDACION.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteFECHA_LIQUIDACION.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dteFECHA_LIQUIDACION.Size = new System.Drawing.Size(123, 28);
            this.dteFECHA_LIQUIDACION.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 13.77391F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(402, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Liquidación diaria de Distribuidoras";
            // 
            // panelResumen
            // 
            this.panelResumen.Controls.Add(this.panel6);
            this.panelResumen.Controls.Add(this.panel5);
            this.panelResumen.Controls.Add(this.panel3);
            this.panelResumen.Controls.Add(this.panel2);
            this.panelResumen.Controls.Add(this.panel1);
            this.panelResumen.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelResumen.Location = new System.Drawing.Point(0, 95);
            this.panelResumen.Name = "panelResumen";
            this.panelResumen.Size = new System.Drawing.Size(1539, 99);
            this.panelResumen.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(249)))), ((int)(((byte)(225)))));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.lblTOTAL_REINTEGROS);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Location = new System.Drawing.Point(680, 17);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(215, 66);
            this.panel6.TabIndex = 5;
            // 
            // lblTOTAL_REINTEGROS
            // 
            this.lblTOTAL_REINTEGROS.Font = new System.Drawing.Font("Tahoma", 11.89565F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTOTAL_REINTEGROS.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTOTAL_REINTEGROS.Location = new System.Drawing.Point(1, 33);
            this.lblTOTAL_REINTEGROS.Name = "lblTOTAL_REINTEGROS";
            this.lblTOTAL_REINTEGROS.Size = new System.Drawing.Size(211, 23);
            this.lblTOTAL_REINTEGROS.TabIndex = 1;
            this.lblTOTAL_REINTEGROS.Text = "$ 0.00";
            this.lblTOTAL_REINTEGROS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.Color.Green;
            this.label8.Location = new System.Drawing.Point(4, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(208, 23);
            this.label8.TabIndex = 0;
            this.label8.Text = "REINTEGROS";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(216)))), ((int)(((byte)(249)))));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.lblREMESA);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Location = new System.Drawing.Point(901, 17);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(215, 66);
            this.panel5.TabIndex = 4;
            // 
            // lblREMESA
            // 
            this.lblREMESA.Font = new System.Drawing.Font("Tahoma", 11.89565F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblREMESA.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblREMESA.Location = new System.Drawing.Point(3, 33);
            this.lblREMESA.Name = "lblREMESA";
            this.lblREMESA.Size = new System.Drawing.Size(209, 23);
            this.lblREMESA.TabIndex = 1;
            this.lblREMESA.Text = "$ 0.00";
            this.lblREMESA.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(3, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(209, 23);
            this.label6.TabIndex = 0;
            this.label6.Text = "REMESA BANCARIA";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblTOTAL_GASTOS);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Location = new System.Drawing.Point(238, 17);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(215, 66);
            this.panel3.TabIndex = 2;
            // 
            // lblTOTAL_GASTOS
            // 
            this.lblTOTAL_GASTOS.Font = new System.Drawing.Font("Tahoma", 11.89565F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTOTAL_GASTOS.ForeColor = System.Drawing.Color.Maroon;
            this.lblTOTAL_GASTOS.Location = new System.Drawing.Point(1, 33);
            this.lblTOTAL_GASTOS.Name = "lblTOTAL_GASTOS";
            this.lblTOTAL_GASTOS.Size = new System.Drawing.Size(211, 23);
            this.lblTOTAL_GASTOS.TabIndex = 1;
            this.lblTOTAL_GASTOS.Text = "$ 0.00";
            this.lblTOTAL_GASTOS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Brown;
            this.label9.Location = new System.Drawing.Point(3, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(209, 23);
            this.label9.TabIndex = 0;
            this.label9.Text = "TOTAL GASTOS";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblTOTAL_CREDITO);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(459, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(215, 66);
            this.panel2.TabIndex = 1;
            // 
            // lblTOTAL_CREDITO
            // 
            this.lblTOTAL_CREDITO.Font = new System.Drawing.Font("Tahoma", 11.89565F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTOTAL_CREDITO.ForeColor = System.Drawing.Color.Maroon;
            this.lblTOTAL_CREDITO.Location = new System.Drawing.Point(3, 33);
            this.lblTOTAL_CREDITO.Name = "lblTOTAL_CREDITO";
            this.lblTOTAL_CREDITO.Size = new System.Drawing.Size(209, 23);
            this.lblTOTAL_CREDITO.TabIndex = 1;
            this.lblTOTAL_CREDITO.Text = "$ 0.00";
            this.lblTOTAL_CREDITO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Brown;
            this.label7.Location = new System.Drawing.Point(3, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(209, 23);
            this.label7.TabIndex = 0;
            this.label7.Text = "TOTAL CRÉDITOS";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(249)))), ((int)(((byte)(225)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblTOTAL_VENTAS);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(17, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 66);
            this.panel1.TabIndex = 0;
            // 
            // lblTOTAL_VENTAS
            // 
            this.lblTOTAL_VENTAS.Font = new System.Drawing.Font("Tahoma", 11.89565F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTOTAL_VENTAS.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTOTAL_VENTAS.Location = new System.Drawing.Point(1, 33);
            this.lblTOTAL_VENTAS.Name = "lblTOTAL_VENTAS";
            this.lblTOTAL_VENTAS.Size = new System.Drawing.Size(211, 23);
            this.lblTOTAL_VENTAS.TabIndex = 1;
            this.lblTOTAL_VENTAS.Text = "$ 0.00";
            this.lblTOTAL_VENTAS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Green;
            this.label4.Location = new System.Drawing.Point(4, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(208, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "LIQUIDACION DIARIA";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelGASTOS
            // 
            this.panelGASTOS.Controls.Add(this.tabPane1);
            this.panelGASTOS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGASTOS.Location = new System.Drawing.Point(0, 194);
            this.panelGASTOS.Name = "panelGASTOS";
            this.panelGASTOS.Size = new System.Drawing.Size(1539, 568);
            this.panelGASTOS.TabIndex = 3;
            // 
            // tabPane1
            // 
            this.tabPane1.AllowTransitionAnimation = DevExpress.Utils.DefaultBoolean.False;
            this.tabPane1.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPane1.Appearance.Options.UseFont = true;
            this.tabPane1.Appearance.Options.UseTextOptions = true;
            this.tabPane1.Controls.Add(this.tabNavigationPage1);
            this.tabPane1.Controls.Add(this.tabNavigationPage2);
            this.tabPane1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPane1.Font = new System.Drawing.Font("Segoe UI", 11.26957F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPane1.Location = new System.Drawing.Point(0, 0);
            this.tabPane1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.tabPane1.Name = "tabPane1";
            this.tabPane1.PageProperties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.Text;
            this.tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tabNavigationPage1,
            this.tabNavigationPage2});
            this.tabPane1.RegularSize = new System.Drawing.Size(1539, 568);
            this.tabPane1.SelectedPage = this.tabNavigationPage1;
            this.tabPane1.Size = new System.Drawing.Size(1539, 568);
            this.tabPane1.TabIndex = 0;
            this.tabPane1.Text = "tabPane1";
            // 
            // tabNavigationPage1
            // 
            this.tabNavigationPage1.Appearance.BackColor = System.Drawing.Color.Gold;
            this.tabNavigationPage1.Appearance.Options.UseBackColor = true;
            this.tabNavigationPage1.Caption = "tabNavigationPage1";
            this.tabNavigationPage1.Controls.Add(this.gridControl1);
            this.tabNavigationPage1.Name = "tabNavigationPage1";
            this.tabNavigationPage1.PageText = "CLQ Y DOCUMENTOS DE VENTA";
            this.tabNavigationPage1.Size = new System.Drawing.Size(1539, 528);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridCLQ;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1539, 528);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridCLQ});
            // 
            // gridCLQ
            // 
            this.gridCLQ.GridControl = this.gridControl1;
            this.gridCLQ.Name = "gridCLQ";
            this.gridCLQ.OptionsView.ShowIndicator = false;
            // 
            // tabNavigationPage2
            // 
            this.tabNavigationPage2.Caption = "tabNavigationPage2";
            this.tabNavigationPage2.Controls.Add(this.gridControl2);
            this.tabNavigationPage2.Name = "tabNavigationPage2";
            this.tabNavigationPage2.PageText = "GASTOS APLICADOS";
            this.tabNavigationPage2.Size = new System.Drawing.Size(1539, 528);
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 0);
            this.gridControl2.MainView = this.gridGASTOS;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(1539, 528);
            this.gridControl2.TabIndex = 1;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridGASTOS});
            // 
            // gridGASTOS
            // 
            this.gridGASTOS.GridControl = this.gridControl2;
            this.gridGASTOS.Name = "gridGASTOS";
            // 
            // frmLiquidacionDIZUCAR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1539, 762);
            this.Controls.Add(this.panelGASTOS);
            this.Controls.Add(this.panelResumen);
            this.Controls.Add(this.panelFiltros);
            this.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLiquidacionDIZUCAR";
            this.Tag = "CONSULTA";
            this.Text = "DIZUCAR - Liquidación diaria";
            this.Load += new System.EventHandler(this.frmLiquidacionDIZUCAR_Load);
            this.panelFiltros.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_LIQUIDACION.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFECHA_LIQUIDACION.Properties)).EndInit();
            this.panelResumen.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelGASTOS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).EndInit();
            this.tabPane1.ResumeLayout(false);
            this.tabNavigationPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCLQ)).EndInit();
            this.tabNavigationPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridGASTOS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFiltros;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit dteFECHA_LIQUIDACION;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxEMPRESA;
        private System.Windows.Forms.Panel panelResumen;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblTOTAL_GASTOS;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTOTAL_CREDITO;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTOTAL_VENTAS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelGASTOS;
        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridCLQ;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridGASTOS;
        private DevExpress.XtraEditors.SimpleButton btnExpandirContraer;
        private DevExpress.XtraEditors.SimpleButton btnImportarDIZUCAR;
        private DevExpress.XtraEditors.SimpleButton btnGenerarTodos;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblREMESA;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblTOTAL_REINTEGROS;
        private System.Windows.Forms.Label label8;
    }
}