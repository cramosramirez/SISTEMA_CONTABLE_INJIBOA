namespace SistemaContable.UI.Forms.Ventas
{
    partial class frmConsultaCreditoFiscal
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNuevoCCF = new DevExpress.XtraEditors.SimpleButton();
            this.btnFinalizar = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gvDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID_CCFENC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDITAR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riEditar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colVER_R = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riVerR = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colFECHA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNUMINTERNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOMBRE_ENTIDAD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCOD_GENERACION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFHPROCESAMIENTO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSELLORECEPCION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTOTALVENTA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riVerQ = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riVerR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riVerQ)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnNuevoCCF);
            this.panel1.Controls.Add(this.btnFinalizar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1450, 58);
            this.panel1.TabIndex = 0;
            // 
            // btnNuevoCCF
            // 
            this.btnNuevoCCF.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoCCF.Appearance.Options.UseFont = true;
            this.btnNuevoCCF.Appearance.Options.UseTextOptions = true;
            this.btnNuevoCCF.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnNuevoCCF.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevoCCF.Location = new System.Drawing.Point(24, 11);
            this.btnNuevoCCF.Margin = new System.Windows.Forms.Padding(2);
            this.btnNuevoCCF.Name = "btnNuevoCCF";
            this.btnNuevoCCF.Size = new System.Drawing.Size(120, 38);
            this.btnNuevoCCF.TabIndex = 0;
            this.btnNuevoCCF.TabStop = false;
            this.btnNuevoCCF.Text = "Nuevo CCF";
            this.btnNuevoCCF.Click += new System.EventHandler(this.btnNuevoCCF_Click);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.Appearance.Options.UseFont = true;
            this.btnFinalizar.Appearance.Options.UseTextOptions = true;
            this.btnFinalizar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnFinalizar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnFinalizar.Location = new System.Drawing.Point(152, 11);
            this.btnFinalizar.Margin = new System.Windows.Forms.Padding(2);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(120, 38);
            this.btnFinalizar.TabIndex = 1;
            this.btnFinalizar.TabStop = false;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Location = new System.Drawing.Point(0, 58);
            this.gridControl1.MainView = this.gvDetalle;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riEditar,
            this.riVerR,
            this.riVerQ});
            this.gridControl1.Size = new System.Drawing.Size(1450, 392);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.Tag = "Consulta";
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetalle});
            // 
            // gvDetalle
            // 
            this.gvDetalle.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID_CCFENC,
            this.colEDITAR,
            this.colVER_R,
            this.colFECHA,
            this.colNUMINTERNO,
            this.colNOMBRE_ENTIDAD,
            this.colCOD_GENERACION,
            this.colFHPROCESAMIENTO,
            this.colSELLORECEPCION,
            this.colTOTALVENTA});
            this.gvDetalle.GridControl = this.gridControl1;
            this.gvDetalle.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvDetalle.Name = "gvDetalle";
            this.gvDetalle.OptionsView.ShowIndicator = false;
            this.gvDetalle.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvDetalle.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvDetalle_RowCellClick);
            // 
            // colID_CCFENC
            // 
            this.colID_CCFENC.Caption = "Sistema(Id)";
            this.colID_CCFENC.FieldName = "ID_CCFENC";
            this.colID_CCFENC.MinWidth = 18;
            this.colID_CCFENC.Name = "colID_CCFENC";
            this.colID_CCFENC.OptionsColumn.AllowEdit = false;
            this.colID_CCFENC.OptionsColumn.AllowSize = false;
            // 
            // colEDITAR
            // 
            this.colEDITAR.Caption = "Editar";
            this.colEDITAR.ColumnEdit = this.riEditar;
            this.colEDITAR.MinWidth = 18;
            this.colEDITAR.Name = "colEDITAR";
            this.colEDITAR.OptionsColumn.AllowSize = false;
            this.colEDITAR.OptionsColumn.ShowCaption = false;
            this.colEDITAR.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colEDITAR.Visible = true;
            this.colEDITAR.VisibleIndex = 0;
            this.colEDITAR.Width = 41;
            // 
            // riEditar
            // 
            this.riEditar.AutoHeight = false;
            editorButtonImageOptions1.Image = global::SistemaContable.UI.Properties.Resources.editar3_32x32;
            this.riEditar.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.riEditar.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.riEditar.Name = "riEditar";
            this.riEditar.UseReadOnlyAppearance = false;
            // 
            // colVER_R
            // 
            this.colVER_R.Caption = "Ver";
            this.colVER_R.ColumnEdit = this.riVerR;
            this.colVER_R.MinWidth = 18;
            this.colVER_R.Name = "colVER_R";
            this.colVER_R.OptionsColumn.AllowSize = false;
            this.colVER_R.OptionsColumn.ShowCaption = false;
            this.colVER_R.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colVER_R.Visible = true;
            this.colVER_R.VisibleIndex = 1;
            this.colVER_R.Width = 41;
            // 
            // riVerR
            // 
            this.riVerR.AutoHeight = false;
            editorButtonImageOptions2.Image = global::SistemaContable.UI.Properties.Resources.quedan2_32x32;
            this.riVerR.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.riVerR.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.riVerR.Name = "riVerR";
            this.riVerR.UseReadOnlyAppearance = false;
            // 
            // colFECHA
            // 
            this.colFECHA.AppearanceCell.Options.UseTextOptions = true;
            this.colFECHA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFECHA.Caption = "Fecha CCF";
            this.colFECHA.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colFECHA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colFECHA.FieldName = "FECHA";
            this.colFECHA.MinWidth = 18;
            this.colFECHA.Name = "colFECHA";
            this.colFECHA.OptionsColumn.AllowEdit = false;
            this.colFECHA.OptionsColumn.AllowSize = false;
            this.colFECHA.Visible = true;
            this.colFECHA.VisibleIndex = 2;
            this.colFECHA.Width = 90;
            // 
            // colNUMINTERNO
            // 
            this.colNUMINTERNO.Caption = "N° Interno";
            this.colNUMINTERNO.FieldName = "NUMINTERNO";
            this.colNUMINTERNO.MinWidth = 18;
            this.colNUMINTERNO.Name = "colNUMINTERNO";
            this.colNUMINTERNO.OptionsColumn.AllowEdit = false;
            this.colNUMINTERNO.OptionsColumn.AllowSize = false;
            this.colNUMINTERNO.Visible = true;
            this.colNUMINTERNO.VisibleIndex = 3;
            this.colNUMINTERNO.Width = 90;
            // 
            // colNOMBRE_ENTIDAD
            // 
            this.colNOMBRE_ENTIDAD.Caption = "Cliente";
            this.colNOMBRE_ENTIDAD.FieldName = "NOMBRE_ENTIDAD";
            this.colNOMBRE_ENTIDAD.MinWidth = 18;
            this.colNOMBRE_ENTIDAD.Name = "colNOMBRE_ENTIDAD";
            this.colNOMBRE_ENTIDAD.OptionsColumn.AllowEdit = false;
            this.colNOMBRE_ENTIDAD.OptionsColumn.AllowSize = false;
            this.colNOMBRE_ENTIDAD.Visible = true;
            this.colNOMBRE_ENTIDAD.VisibleIndex = 4;
            this.colNOMBRE_ENTIDAD.Width = 247;
            // 
            // colCOD_GENERACION
            // 
            this.colCOD_GENERACION.Caption = "Cód. Generación";
            this.colCOD_GENERACION.FieldName = "CODGENERACION";
            this.colCOD_GENERACION.MinWidth = 18;
            this.colCOD_GENERACION.Name = "colCOD_GENERACION";
            this.colCOD_GENERACION.OptionsColumn.AllowEdit = false;
            this.colCOD_GENERACION.OptionsColumn.AllowSize = false;
            this.colCOD_GENERACION.Visible = true;
            this.colCOD_GENERACION.VisibleIndex = 5;
            this.colCOD_GENERACION.Width = 236;
            // 
            // colFHPROCESAMIENTO
            // 
            this.colFHPROCESAMIENTO.AppearanceCell.Options.UseTextOptions = true;
            this.colFHPROCESAMIENTO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colFHPROCESAMIENTO.Caption = "N° Control";
            this.colFHPROCESAMIENTO.FieldName = "NUMCONTROL";
            this.colFHPROCESAMIENTO.MinWidth = 18;
            this.colFHPROCESAMIENTO.Name = "colFHPROCESAMIENTO";
            this.colFHPROCESAMIENTO.OptionsColumn.AllowEdit = false;
            this.colFHPROCESAMIENTO.OptionsColumn.AllowSize = false;
            this.colFHPROCESAMIENTO.Visible = true;
            this.colFHPROCESAMIENTO.VisibleIndex = 6;
            this.colFHPROCESAMIENTO.Width = 220;
            // 
            // colSELLORECEPCION
            // 
            this.colSELLORECEPCION.AppearanceCell.Options.UseTextOptions = true;
            this.colSELLORECEPCION.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colSELLORECEPCION.Caption = "Sello Recepción";
            this.colSELLORECEPCION.FieldName = "SELLORECEPCION";
            this.colSELLORECEPCION.MinWidth = 18;
            this.colSELLORECEPCION.Name = "colSELLORECEPCION";
            this.colSELLORECEPCION.OptionsColumn.AllowEdit = false;
            this.colSELLORECEPCION.OptionsColumn.AllowSize = false;
            this.colSELLORECEPCION.Visible = true;
            this.colSELLORECEPCION.VisibleIndex = 7;
            this.colSELLORECEPCION.Width = 200;
            // 
            // colTOTALVENTA
            // 
            this.colTOTALVENTA.AppearanceCell.Options.UseTextOptions = true;
            this.colTOTALVENTA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTOTALVENTA.Caption = "Total Venta";
            this.colTOTALVENTA.DisplayFormat.FormatString = "c2";
            this.colTOTALVENTA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTOTALVENTA.FieldName = "TOTALVENTA";
            this.colTOTALVENTA.MinWidth = 18;
            this.colTOTALVENTA.Name = "colTOTALVENTA";
            this.colTOTALVENTA.OptionsColumn.AllowEdit = false;
            this.colTOTALVENTA.OptionsColumn.AllowSize = false;
            this.colTOTALVENTA.Visible = true;
            this.colTOTALVENTA.VisibleIndex = 8;
            this.colTOTALVENTA.Width = 100;
            // 
            // riVerQ
            // 
            this.riVerQ.AutoHeight = false;
            this.riVerQ.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.riVerQ.Name = "riVerQ";
            this.riVerQ.UseReadOnlyAppearance = false;
            // 
            // frmConsultaCreditoFiscal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1450, 450);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConsultaCreditoFiscal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Consulta";
            this.Text = "Consulta Comprobante de Crédito Fiscal";
            this.Load += new System.EventHandler(this.frmConsultaCreditoFiscal_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riVerR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riVerQ)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnNuevoCCF;
        private DevExpress.XtraEditors.SimpleButton btnFinalizar;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetalle;
        private DevExpress.XtraGrid.Columns.GridColumn colID_CCFENC;
        private DevExpress.XtraGrid.Columns.GridColumn colEDITAR;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riEditar;
        private DevExpress.XtraGrid.Columns.GridColumn colVER_R;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riVerR;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riVerQ;
        private DevExpress.XtraGrid.Columns.GridColumn colFECHA;
        private DevExpress.XtraGrid.Columns.GridColumn colNUMINTERNO;
        private DevExpress.XtraGrid.Columns.GridColumn colNOMBRE_ENTIDAD;
        private DevExpress.XtraGrid.Columns.GridColumn colCOD_GENERACION;
        private DevExpress.XtraGrid.Columns.GridColumn colFHPROCESAMIENTO;
        private DevExpress.XtraGrid.Columns.GridColumn colSELLORECEPCION;
        private DevExpress.XtraGrid.Columns.GridColumn colTOTALVENTA;
    }
}