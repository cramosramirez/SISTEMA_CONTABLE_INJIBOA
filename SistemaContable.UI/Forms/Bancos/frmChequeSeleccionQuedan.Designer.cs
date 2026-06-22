
namespace SistemaContable.UI.Forms.Bancos
{
    partial class frmChequeSeleccionQuedan
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
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridViewPagar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtValorAPagar = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridViewPendientePago = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTIPO_DTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCOD_GENERACION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSELLO_RECIBIDO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFECHA_VENCE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNUM_QUEDAN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSALDO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnRetornar = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBajarTodos = new DevExpress.XtraEditors.SimpleButton();
            this.btnSubirTodos = new DevExpress.XtraEditors.SimpleButton();
            this.btnBajar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSubir = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNOMBRE_PROVEEDOR = new System.Windows.Forms.TextBox();
            this.txtPROVEEDOR = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPagar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPendientePago)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gridControl2);
            this.panel1.Controls.Add(this.txtValorAPagar);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.gridControl1);
            this.panel1.Controls.Add(this.btnRetornar);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnBajarTodos);
            this.panel1.Controls.Add(this.btnSubirTodos);
            this.panel1.Controls.Add(this.btnBajar);
            this.panel1.Controls.Add(this.btnSubir);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtNOMBRE_PROVEEDOR);
            this.panel1.Controls.Add(this.txtPROVEEDOR);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1277, 630);
            this.panel1.TabIndex = 0;
            // 
            // gridControl2
            // 
            this.gridControl2.Location = new System.Drawing.Point(13, 347);
            this.gridControl2.MainView = this.gridViewPagar;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(1252, 202);
            this.gridControl2.TabIndex = 134;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPagar});
            // 
            // gridViewPagar
            // 
            this.gridViewPagar.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gridViewPagar.GridControl = this.gridControl2;
            this.gridViewPagar.Name = "gridViewPagar";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tipo Doc.";
            this.gridColumn1.FieldName = "TIPO_DTE";
            this.gridColumn1.MinWidth = 21;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowSize = false;
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 87;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "No./Código generación";
            this.gridColumn2.FieldName = "COD_GENERACION";
            this.gridColumn2.MinWidth = 280;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowSize = false;
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 280;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Serie/Sello recepción";
            this.gridColumn3.FieldName = "SELLO_RECIBIDO";
            this.gridColumn3.MinWidth = 283;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowSize = false;
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 289;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Vence";
            this.gridColumn4.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn4.FieldName = "FECHA_VENCE";
            this.gridColumn4.MinWidth = 105;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowSize = false;
            this.gridColumn4.OptionsColumn.FixedWidth = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 114;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "Quedan";
            this.gridColumn5.FieldName = "NUM_QUEDAN";
            this.gridColumn5.MinWidth = 87;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowSize = false;
            this.gridColumn5.OptionsColumn.FixedWidth = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 114;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Valor";
            this.gridColumn6.DisplayFormat.FormatString = "N2";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "SALDO";
            this.gridColumn6.MinWidth = 105;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowSize = false;
            this.gridColumn6.OptionsColumn.FixedWidth = true;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 131;
            // 
            // txtValorAPagar
            // 
            this.txtValorAPagar.BackColor = System.Drawing.Color.Teal;
            this.txtValorAPagar.Font = new System.Drawing.Font("Verdana", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorAPagar.ForeColor = System.Drawing.Color.White;
            this.txtValorAPagar.Location = new System.Drawing.Point(317, 575);
            this.txtValorAPagar.Multiline = true;
            this.txtValorAPagar.Name = "txtValorAPagar";
            this.txtValorAPagar.ReadOnly = true;
            this.txtValorAPagar.Size = new System.Drawing.Size(134, 30);
            this.txtValorAPagar.TabIndex = 133;
            this.txtValorAPagar.TabStop = false;
            this.txtValorAPagar.Text = "$ 0.00";
            this.txtValorAPagar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(177, 579);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 18);
            this.label4.TabIndex = 132;
            this.label4.Text = "Valor a cancelar:";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(13, 73);
            this.gridControl1.MainView = this.gridViewPendientePago;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1252, 202);
            this.gridControl1.TabIndex = 130;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPendientePago});
            // 
            // gridViewPendientePago
            // 
            this.gridViewPendientePago.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTIPO_DTE,
            this.colCOD_GENERACION,
            this.colSELLO_RECIBIDO,
            this.colFECHA_VENCE,
            this.colNUM_QUEDAN,
            this.colSALDO});
            this.gridViewPendientePago.GridControl = this.gridControl1;
            this.gridViewPendientePago.Name = "gridViewPendientePago";
            // 
            // colTIPO_DTE
            // 
            this.colTIPO_DTE.Caption = "Tipo Doc.";
            this.colTIPO_DTE.FieldName = "TIPO_DTE";
            this.colTIPO_DTE.MinWidth = 21;
            this.colTIPO_DTE.Name = "colTIPO_DTE";
            this.colTIPO_DTE.OptionsColumn.AllowSize = false;
            this.colTIPO_DTE.OptionsColumn.FixedWidth = true;
            this.colTIPO_DTE.Visible = true;
            this.colTIPO_DTE.VisibleIndex = 0;
            this.colTIPO_DTE.Width = 87;
            // 
            // colCOD_GENERACION
            // 
            this.colCOD_GENERACION.Caption = "No./Código generación";
            this.colCOD_GENERACION.FieldName = "COD_GENERACION";
            this.colCOD_GENERACION.MinWidth = 280;
            this.colCOD_GENERACION.Name = "colCOD_GENERACION";
            this.colCOD_GENERACION.OptionsColumn.AllowSize = false;
            this.colCOD_GENERACION.OptionsColumn.FixedWidth = true;
            this.colCOD_GENERACION.Visible = true;
            this.colCOD_GENERACION.VisibleIndex = 1;
            this.colCOD_GENERACION.Width = 280;
            // 
            // colSELLO_RECIBIDO
            // 
            this.colSELLO_RECIBIDO.Caption = "Serie/Sello recepción";
            this.colSELLO_RECIBIDO.FieldName = "SELLO_RECIBIDO";
            this.colSELLO_RECIBIDO.MinWidth = 283;
            this.colSELLO_RECIBIDO.Name = "colSELLO_RECIBIDO";
            this.colSELLO_RECIBIDO.OptionsColumn.AllowSize = false;
            this.colSELLO_RECIBIDO.OptionsColumn.FixedWidth = true;
            this.colSELLO_RECIBIDO.Visible = true;
            this.colSELLO_RECIBIDO.VisibleIndex = 2;
            this.colSELLO_RECIBIDO.Width = 283;
            // 
            // colFECHA_VENCE
            // 
            this.colFECHA_VENCE.AppearanceCell.Options.UseTextOptions = true;
            this.colFECHA_VENCE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFECHA_VENCE.AppearanceHeader.Options.UseTextOptions = true;
            this.colFECHA_VENCE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFECHA_VENCE.Caption = "Vence";
            this.colFECHA_VENCE.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colFECHA_VENCE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colFECHA_VENCE.FieldName = "FECHA_VENCE";
            this.colFECHA_VENCE.MinWidth = 105;
            this.colFECHA_VENCE.Name = "colFECHA_VENCE";
            this.colFECHA_VENCE.OptionsColumn.AllowSize = false;
            this.colFECHA_VENCE.OptionsColumn.FixedWidth = true;
            this.colFECHA_VENCE.Visible = true;
            this.colFECHA_VENCE.VisibleIndex = 3;
            this.colFECHA_VENCE.Width = 114;
            // 
            // colNUM_QUEDAN
            // 
            this.colNUM_QUEDAN.AppearanceCell.Options.UseTextOptions = true;
            this.colNUM_QUEDAN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNUM_QUEDAN.AppearanceHeader.Options.UseTextOptions = true;
            this.colNUM_QUEDAN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNUM_QUEDAN.Caption = "Quedan";
            this.colNUM_QUEDAN.FieldName = "NUM_QUEDAN";
            this.colNUM_QUEDAN.MinWidth = 87;
            this.colNUM_QUEDAN.Name = "colNUM_QUEDAN";
            this.colNUM_QUEDAN.OptionsColumn.AllowSize = false;
            this.colNUM_QUEDAN.OptionsColumn.FixedWidth = true;
            this.colNUM_QUEDAN.Visible = true;
            this.colNUM_QUEDAN.VisibleIndex = 4;
            this.colNUM_QUEDAN.Width = 114;
            // 
            // colSALDO
            // 
            this.colSALDO.Caption = "Valor";
            this.colSALDO.DisplayFormat.FormatString = "N2";
            this.colSALDO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSALDO.FieldName = "SALDO";
            this.colSALDO.MinWidth = 105;
            this.colSALDO.Name = "colSALDO";
            this.colSALDO.OptionsColumn.AllowSize = false;
            this.colSALDO.OptionsColumn.FixedWidth = true;
            this.colSALDO.Visible = true;
            this.colSALDO.VisibleIndex = 5;
            this.colSALDO.Width = 131;
            // 
            // btnRetornar
            // 
            this.btnRetornar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRetornar.Appearance.Options.UseFont = true;
            this.btnRetornar.Appearance.Options.UseTextOptions = true;
            this.btnRetornar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnRetornar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.retornar32x32;
            this.btnRetornar.ImageOptions.ImageToTextIndent = 10;
            this.btnRetornar.Location = new System.Drawing.Point(13, 571);
            this.btnRetornar.Name = "btnRetornar";
            this.btnRetornar.Size = new System.Drawing.Size(116, 47);
            this.btnRetornar.TabIndex = 129;
            this.btnRetornar.Text = "Retornar";
            this.btnRetornar.Click += new System.EventHandler(this.btnRetornar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkGreen;
            this.label3.Location = new System.Drawing.Point(24, 322);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 18);
            this.label3.TabIndex = 128;
            this.label3.Text = "A pagar:";
            // 
            // btnBajarTodos
            // 
            this.btnBajarTodos.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBajarTodos.Appearance.Options.UseFont = true;
            this.btnBajarTodos.Appearance.Options.UseTextOptions = true;
            this.btnBajarTodos.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnBajarTodos.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.flechaAbajoTodos32x32;
            this.btnBajarTodos.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.BottomCenter;
            this.btnBajarTodos.ImageOptions.ImageToTextIndent = 0;
            this.btnBajarTodos.Location = new System.Drawing.Point(261, 291);
            this.btnBajarTodos.Name = "btnBajarTodos";
            this.btnBajarTodos.Size = new System.Drawing.Size(38, 34);
            this.btnBajarTodos.TabIndex = 127;
            this.btnBajarTodos.Click += new System.EventHandler(this.btnBajarTodos_Click);
            // 
            // btnSubirTodos
            // 
            this.btnSubirTodos.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubirTodos.Appearance.Options.UseFont = true;
            this.btnSubirTodos.Appearance.Options.UseTextOptions = true;
            this.btnSubirTodos.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnSubirTodos.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.flechaArribaTodos32x32;
            this.btnSubirTodos.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.BottomCenter;
            this.btnSubirTodos.ImageOptions.ImageToTextIndent = 0;
            this.btnSubirTodos.Location = new System.Drawing.Point(391, 291);
            this.btnSubirTodos.Name = "btnSubirTodos";
            this.btnSubirTodos.Size = new System.Drawing.Size(38, 34);
            this.btnSubirTodos.TabIndex = 126;
            this.btnSubirTodos.Click += new System.EventHandler(this.btnSubirTodos_Click);
            // 
            // btnBajar
            // 
            this.btnBajar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBajar.Appearance.Options.UseFont = true;
            this.btnBajar.Appearance.Options.UseTextOptions = true;
            this.btnBajar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnBajar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.flechaAbajo32x32;
            this.btnBajar.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.BottomCenter;
            this.btnBajar.ImageOptions.ImageToTextIndent = 0;
            this.btnBajar.Location = new System.Drawing.Point(217, 291);
            this.btnBajar.Name = "btnBajar";
            this.btnBajar.Size = new System.Drawing.Size(38, 34);
            this.btnBajar.TabIndex = 125;
            this.btnBajar.Click += new System.EventHandler(this.btnBajar_Click);
            // 
            // btnSubir
            // 
            this.btnSubir.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubir.Appearance.Options.UseFont = true;
            this.btnSubir.Appearance.Options.UseTextOptions = true;
            this.btnSubir.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnSubir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.flechaArriba32x32;
            this.btnSubir.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.BottomCenter;
            this.btnSubir.ImageOptions.ImageToTextIndent = 0;
            this.btnSubir.Location = new System.Drawing.Point(347, 291);
            this.btnSubir.Name = "btnSubir";
            this.btnSubir.Size = new System.Drawing.Size(38, 34);
            this.btnSubir.TabIndex = 124;
            this.btnSubir.Click += new System.EventHandler(this.btnSubir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 18);
            this.label2.TabIndex = 123;
            this.label2.Text = "Documentos pendientes de pago";
            // 
            // txtNOMBRE_PROVEEDOR
            // 
            this.txtNOMBRE_PROVEEDOR.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNOMBRE_PROVEEDOR.Location = new System.Drawing.Point(228, 17);
            this.txtNOMBRE_PROVEEDOR.Name = "txtNOMBRE_PROVEEDOR";
            this.txtNOMBRE_PROVEEDOR.ReadOnly = true;
            this.txtNOMBRE_PROVEEDOR.Size = new System.Drawing.Size(1037, 24);
            this.txtNOMBRE_PROVEEDOR.TabIndex = 121;
            this.txtNOMBRE_PROVEEDOR.TabStop = false;
            // 
            // txtPROVEEDOR
            // 
            this.txtPROVEEDOR.BackColor = System.Drawing.SystemColors.Control;
            this.txtPROVEEDOR.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPROVEEDOR.Location = new System.Drawing.Point(93, 17);
            this.txtPROVEEDOR.Name = "txtPROVEEDOR";
            this.txtPROVEEDOR.ReadOnly = true;
            this.txtPROVEEDOR.Size = new System.Drawing.Size(134, 24);
            this.txtPROVEEDOR.TabIndex = 120;
            this.txtPROVEEDOR.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 18);
            this.label1.TabIndex = 122;
            this.label1.Text = "Proveedor";
            // 
            // frmChequeSeleccionQuedan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1277, 630);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChequeSeleccionQuedan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selección de documentos";
            this.Load += new System.EventHandler(this.frmChequeSeleccionPago_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPagar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPendientePago)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNOMBRE_PROVEEDOR;
        private System.Windows.Forms.TextBox txtPROVEEDOR;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnSubir;
        private DevExpress.XtraEditors.SimpleButton btnBajarTodos;
        private DevExpress.XtraEditors.SimpleButton btnSubirTodos;
        private DevExpress.XtraEditors.SimpleButton btnBajar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtValorAPagar;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPendientePago;
        private DevExpress.XtraEditors.SimpleButton btnRetornar;
        private DevExpress.XtraGrid.Columns.GridColumn colTIPO_DTE;
        private DevExpress.XtraGrid.Columns.GridColumn colCOD_GENERACION;
        private DevExpress.XtraGrid.Columns.GridColumn colSELLO_RECIBIDO;
        private DevExpress.XtraGrid.Columns.GridColumn colFECHA_VENCE;
        private DevExpress.XtraGrid.Columns.GridColumn colNUM_QUEDAN;
        private DevExpress.XtraGrid.Columns.GridColumn colSALDO;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPagar;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
    }
}