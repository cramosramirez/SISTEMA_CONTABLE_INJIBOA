
namespace SistemaContable.UI.Forms.Bancos
{
    partial class frmChequeDocumentosContado
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCODIGO_ENTIDAD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPROVEEDOR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTIPO_DOC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCOD_GENERACION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNUM_CONTROL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFECHA_RECIBIDO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGRAVADA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEXENTA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNO_SUJETA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPERCEPCION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIVA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFOVIAL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCOTRANS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTOTAL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRENTA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRETENCION_IVA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSALDO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRetornar = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdicionar = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gridControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1216, 303);
            this.panel1.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1216, 303);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCODIGO_ENTIDAD,
            this.colPROVEEDOR,
            this.colTIPO_DOC,
            this.colCOD_GENERACION,
            this.colNUM_CONTROL,
            this.colFECHA_RECIBIDO,
            this.colGRAVADA,
            this.colEXENTA,
            this.colNO_SUJETA,
            this.colPERCEPCION,
            this.colIVA,
            this.colFOVIAL,
            this.colCOTRANS,
            this.colTOTAL,
            this.colRENTA,
            this.colRETENCION_IVA,
            this.colSALDO});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // colCODIGO_ENTIDAD
            // 
            this.colCODIGO_ENTIDAD.Caption = "Código";
            this.colCODIGO_ENTIDAD.FieldName = "CODIGO_ENTIDAD";
            this.colCODIGO_ENTIDAD.MinWidth = 21;
            this.colCODIGO_ENTIDAD.Name = "colCODIGO_ENTIDAD";
            this.colCODIGO_ENTIDAD.Visible = true;
            this.colCODIGO_ENTIDAD.VisibleIndex = 0;
            this.colCODIGO_ENTIDAD.Width = 79;
            // 
            // colPROVEEDOR
            // 
            this.colPROVEEDOR.Caption = "Proveedor";
            this.colPROVEEDOR.FieldName = "PROVEEDOR";
            this.colPROVEEDOR.MinWidth = 21;
            this.colPROVEEDOR.Name = "colPROVEEDOR";
            this.colPROVEEDOR.Visible = true;
            this.colPROVEEDOR.VisibleIndex = 1;
            this.colPROVEEDOR.Width = 79;
            // 
            // colTIPO_DOC
            // 
            this.colTIPO_DOC.AppearanceCell.Options.UseTextOptions = true;
            this.colTIPO_DOC.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTIPO_DOC.Caption = "Tipo Doc.";
            this.colTIPO_DOC.FieldName = "TIPO_DOC";
            this.colTIPO_DOC.MinWidth = 21;
            this.colTIPO_DOC.Name = "colTIPO_DOC";
            this.colTIPO_DOC.Visible = true;
            this.colTIPO_DOC.VisibleIndex = 2;
            this.colTIPO_DOC.Width = 79;
            // 
            // colCOD_GENERACION
            // 
            this.colCOD_GENERACION.Caption = "No. / Cód. Generación";
            this.colCOD_GENERACION.FieldName = "COD_GENERACION";
            this.colCOD_GENERACION.MinWidth = 21;
            this.colCOD_GENERACION.Name = "colCOD_GENERACION";
            this.colCOD_GENERACION.Visible = true;
            this.colCOD_GENERACION.VisibleIndex = 3;
            this.colCOD_GENERACION.Width = 79;
            // 
            // colNUM_CONTROL
            // 
            this.colNUM_CONTROL.Caption = "Res. / No. Control";
            this.colNUM_CONTROL.FieldName = "NUM_CONTROL";
            this.colNUM_CONTROL.MinWidth = 21;
            this.colNUM_CONTROL.Name = "colNUM_CONTROL";
            this.colNUM_CONTROL.Width = 79;
            // 
            // colFECHA_RECIBIDO
            // 
            this.colFECHA_RECIBIDO.AppearanceCell.Options.UseTextOptions = true;
            this.colFECHA_RECIBIDO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFECHA_RECIBIDO.AppearanceHeader.Options.UseTextOptions = true;
            this.colFECHA_RECIBIDO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFECHA_RECIBIDO.Caption = "Recibido";
            this.colFECHA_RECIBIDO.FieldName = "FECHA_RECIBIDO";
            this.colFECHA_RECIBIDO.MinWidth = 21;
            this.colFECHA_RECIBIDO.Name = "colFECHA_RECIBIDO";
            this.colFECHA_RECIBIDO.Visible = true;
            this.colFECHA_RECIBIDO.VisibleIndex = 4;
            this.colFECHA_RECIBIDO.Width = 79;
            // 
            // colGRAVADA
            // 
            this.colGRAVADA.AppearanceHeader.Options.UseTextOptions = true;
            this.colGRAVADA.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGRAVADA.Caption = "Afecta";
            this.colGRAVADA.DisplayFormat.FormatString = "N2";
            this.colGRAVADA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGRAVADA.FieldName = "GRAVADA";
            this.colGRAVADA.MinWidth = 21;
            this.colGRAVADA.Name = "colGRAVADA";
            this.colGRAVADA.Visible = true;
            this.colGRAVADA.VisibleIndex = 5;
            this.colGRAVADA.Width = 79;
            // 
            // colEXENTA
            // 
            this.colEXENTA.AppearanceHeader.Options.UseTextOptions = true;
            this.colEXENTA.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEXENTA.Caption = "Exenta";
            this.colEXENTA.DisplayFormat.FormatString = "N2";
            this.colEXENTA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEXENTA.FieldName = "EXENTA";
            this.colEXENTA.MinWidth = 21;
            this.colEXENTA.Name = "colEXENTA";
            this.colEXENTA.Visible = true;
            this.colEXENTA.VisibleIndex = 6;
            this.colEXENTA.Width = 79;
            // 
            // colNO_SUJETA
            // 
            this.colNO_SUJETA.AppearanceHeader.Options.UseTextOptions = true;
            this.colNO_SUJETA.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNO_SUJETA.Caption = "Excluido";
            this.colNO_SUJETA.DisplayFormat.FormatString = "N2";
            this.colNO_SUJETA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNO_SUJETA.FieldName = "EXCLUIDO";
            this.colNO_SUJETA.MinWidth = 21;
            this.colNO_SUJETA.Name = "colNO_SUJETA";
            this.colNO_SUJETA.Visible = true;
            this.colNO_SUJETA.VisibleIndex = 7;
            this.colNO_SUJETA.Width = 79;
            // 
            // colPERCEPCION
            // 
            this.colPERCEPCION.AppearanceHeader.Options.UseTextOptions = true;
            this.colPERCEPCION.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPERCEPCION.Caption = "Percepción";
            this.colPERCEPCION.DisplayFormat.FormatString = "N2";
            this.colPERCEPCION.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPERCEPCION.FieldName = "PERCEPCION";
            this.colPERCEPCION.MinWidth = 21;
            this.colPERCEPCION.Name = "colPERCEPCION";
            this.colPERCEPCION.Visible = true;
            this.colPERCEPCION.VisibleIndex = 8;
            this.colPERCEPCION.Width = 79;
            // 
            // colIVA
            // 
            this.colIVA.AppearanceHeader.Options.UseTextOptions = true;
            this.colIVA.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIVA.Caption = "Iva";
            this.colIVA.DisplayFormat.FormatString = "N2";
            this.colIVA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colIVA.FieldName = "IVA";
            this.colIVA.MinWidth = 21;
            this.colIVA.Name = "colIVA";
            this.colIVA.Visible = true;
            this.colIVA.VisibleIndex = 9;
            this.colIVA.Width = 79;
            // 
            // colFOVIAL
            // 
            this.colFOVIAL.AppearanceHeader.Options.UseTextOptions = true;
            this.colFOVIAL.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFOVIAL.Caption = "Fovial";
            this.colFOVIAL.DisplayFormat.FormatString = "n2";
            this.colFOVIAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFOVIAL.FieldName = "FOVIAL";
            this.colFOVIAL.MinWidth = 21;
            this.colFOVIAL.Name = "colFOVIAL";
            this.colFOVIAL.Visible = true;
            this.colFOVIAL.VisibleIndex = 10;
            this.colFOVIAL.Width = 79;
            // 
            // colCOTRANS
            // 
            this.colCOTRANS.AppearanceHeader.Options.UseTextOptions = true;
            this.colCOTRANS.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCOTRANS.Caption = "Cotrans";
            this.colCOTRANS.DisplayFormat.FormatString = "N2";
            this.colCOTRANS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCOTRANS.FieldName = "COTRANS";
            this.colCOTRANS.MinWidth = 21;
            this.colCOTRANS.Name = "colCOTRANS";
            this.colCOTRANS.Visible = true;
            this.colCOTRANS.VisibleIndex = 11;
            this.colCOTRANS.Width = 79;
            // 
            // colTOTAL
            // 
            this.colTOTAL.AppearanceHeader.Options.UseTextOptions = true;
            this.colTOTAL.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTOTAL.Caption = "Total";
            this.colTOTAL.DisplayFormat.FormatString = "N2";
            this.colTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTOTAL.FieldName = "TOTAL";
            this.colTOTAL.MinWidth = 21;
            this.colTOTAL.Name = "colTOTAL";
            this.colTOTAL.Visible = true;
            this.colTOTAL.VisibleIndex = 12;
            this.colTOTAL.Width = 79;
            // 
            // colRENTA
            // 
            this.colRENTA.AppearanceHeader.Options.UseTextOptions = true;
            this.colRENTA.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRENTA.Caption = "Renta";
            this.colRENTA.DisplayFormat.FormatString = "N2";
            this.colRENTA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRENTA.FieldName = "RENTA";
            this.colRENTA.MinWidth = 21;
            this.colRENTA.Name = "colRENTA";
            this.colRENTA.Visible = true;
            this.colRENTA.VisibleIndex = 13;
            this.colRENTA.Width = 79;
            // 
            // colRETENCION_IVA
            // 
            this.colRETENCION_IVA.AppearanceHeader.Options.UseTextOptions = true;
            this.colRETENCION_IVA.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRETENCION_IVA.Caption = "Retención 1%";
            this.colRETENCION_IVA.DisplayFormat.FormatString = "N2";
            this.colRETENCION_IVA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRETENCION_IVA.FieldName = "IVAR";
            this.colRETENCION_IVA.MinWidth = 21;
            this.colRETENCION_IVA.Name = "colRETENCION_IVA";
            this.colRETENCION_IVA.Visible = true;
            this.colRETENCION_IVA.VisibleIndex = 14;
            this.colRETENCION_IVA.Width = 79;
            // 
            // colSALDO
            // 
            this.colSALDO.AppearanceHeader.Options.UseTextOptions = true;
            this.colSALDO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSALDO.Caption = "Saldo";
            this.colSALDO.DisplayFormat.FormatString = "N2";
            this.colSALDO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSALDO.FieldName = "SALDO";
            this.colSALDO.MinWidth = 21;
            this.colSALDO.Name = "colSALDO";
            this.colSALDO.Visible = true;
            this.colSALDO.VisibleIndex = 15;
            this.colSALDO.Width = 79;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnRetornar);
            this.panel2.Controls.Add(this.btnAdicionar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 303);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1216, 104);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(15, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(396, 20);
            this.label1.TabIndex = 131;
            this.label1.Text = "Haga doble-clic sobre la fila deseada para abrir el editor";
            // 
            // btnRetornar
            // 
            this.btnRetornar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRetornar.Appearance.Options.UseFont = true;
            this.btnRetornar.Appearance.Options.UseTextOptions = true;
            this.btnRetornar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnRetornar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.retornar32x32;
            this.btnRetornar.ImageOptions.ImageToTextIndent = 10;
            this.btnRetornar.Location = new System.Drawing.Point(160, 42);
            this.btnRetornar.Name = "btnRetornar";
            this.btnRetornar.Size = new System.Drawing.Size(117, 47);
            this.btnRetornar.TabIndex = 130;
            this.btnRetornar.Text = "Retornar";
            this.btnRetornar.Click += new System.EventHandler(this.btnRetornar_Click);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdicionar.Appearance.Options.UseFont = true;
            this.btnAdicionar.Appearance.Options.UseTextOptions = true;
            this.btnAdicionar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnAdicionar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.agregarDoc2_32x32;
            this.btnAdicionar.ImageOptions.ImageToTextIndent = 10;
            this.btnAdicionar.Location = new System.Drawing.Point(17, 42);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(122, 47);
            this.btnAdicionar.TabIndex = 6;
            this.btnAdicionar.Text = "Adicionar documento";
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // frmChequeDocumentosContado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 407);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChequeDocumentosContado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compras de contado";
            this.Load += new System.EventHandler(this.frmChequeDocumentosContado_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnAdicionar;
        private DevExpress.XtraEditors.SimpleButton btnRetornar;
        private DevExpress.XtraGrid.Columns.GridColumn colCODIGO_ENTIDAD;
        private DevExpress.XtraGrid.Columns.GridColumn colPROVEEDOR;
        private DevExpress.XtraGrid.Columns.GridColumn colTIPO_DOC;
        private DevExpress.XtraGrid.Columns.GridColumn colCOD_GENERACION;
        private DevExpress.XtraGrid.Columns.GridColumn colNUM_CONTROL;
        private DevExpress.XtraGrid.Columns.GridColumn colFECHA_RECIBIDO;
        private DevExpress.XtraGrid.Columns.GridColumn colGRAVADA;
        private DevExpress.XtraGrid.Columns.GridColumn colEXENTA;
        private DevExpress.XtraGrid.Columns.GridColumn colNO_SUJETA;
        private DevExpress.XtraGrid.Columns.GridColumn colPERCEPCION;
        private DevExpress.XtraGrid.Columns.GridColumn colIVA;
        private DevExpress.XtraGrid.Columns.GridColumn colFOVIAL;
        private DevExpress.XtraGrid.Columns.GridColumn colCOTRANS;
        private DevExpress.XtraGrid.Columns.GridColumn colTOTAL;
        private DevExpress.XtraGrid.Columns.GridColumn colRENTA;
        private DevExpress.XtraGrid.Columns.GridColumn colRETENCION_IVA;
        private DevExpress.XtraGrid.Columns.GridColumn colSALDO;
        private System.Windows.Forms.Label label1;
    }
}