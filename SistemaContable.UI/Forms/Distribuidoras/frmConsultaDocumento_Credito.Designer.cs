
namespace SistemaContable.UI.Forms.Distribuidoras
{
    partial class frmConsultaDocumento_Credito
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFinalizar = new DevExpress.XtraEditors.SimpleButton();
            this.btnNuevoGasto = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gvDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID_VTA_CRED = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDITAR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riEditar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colFECHA_EMISION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEMPRESA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTIPO_DTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNITCLIENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOMBRECLIENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNUM_CONTROL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSELLO_RECIBIDO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTOTAL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colABONO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSALDO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFinalizar);
            this.panel1.Controls.Add(this.btnNuevoGasto);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1667, 46);
            this.panel1.TabIndex = 9;
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.AllowFocus = false;
            this.btnFinalizar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnFinalizar.Appearance.Options.UseFont = true;
            this.btnFinalizar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnFinalizar.Location = new System.Drawing.Point(128, 5);
            this.btnFinalizar.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnFinalizar.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnFinalizar.Size = new System.Drawing.Size(116, 38);
            this.btnFinalizar.TabIndex = 2;
            this.btnFinalizar.TabStop = false;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.ToolTip = "Nuevo Quedan";
            this.btnFinalizar.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnFinalizar.ToolTipTitle = "Operación";
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnNuevoGasto
            // 
            this.btnNuevoGasto.AllowFocus = false;
            this.btnNuevoGasto.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnNuevoGasto.Appearance.Options.UseFont = true;
            this.btnNuevoGasto.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevoGasto.Location = new System.Drawing.Point(10, 5);
            this.btnNuevoGasto.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnNuevoGasto.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnNuevoGasto.Name = "btnNuevoGasto";
            this.btnNuevoGasto.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnNuevoGasto.Size = new System.Drawing.Size(109, 38);
            this.btnNuevoGasto.TabIndex = 1;
            this.btnNuevoGasto.TabStop = false;
            this.btnNuevoGasto.Text = "Nuevo";
            this.btnNuevoGasto.ToolTip = "Nuevo Quedan";
            this.btnNuevoGasto.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnNuevoGasto.ToolTipTitle = "Operación";
            this.btnNuevoGasto.Click += new System.EventHandler(this.btnNuevoGasto_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.26957F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1667, 38);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ventas al Crédito";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gridControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1667, 484);
            this.panel2.TabIndex = 10;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gvDetalle;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riEditar});
            this.gridControl1.Size = new System.Drawing.Size(1667, 484);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetalle});
            // 
            // gvDetalle
            // 
            this.gvDetalle.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID_VTA_CRED,
            this.colEDITAR,
            this.colFECHA_EMISION,
            this.colEMPRESA,
            this.colTIPO_DTE,
            this.colNITCLIENTE,
            this.colNOMBRECLIENTE,
            this.colNUM_CONTROL,
            this.colSELLO_RECIBIDO,
            this.colTOTAL,
            this.colABONO,
            this.colSALDO});
            this.gvDetalle.GridControl = this.gridControl1;
            this.gvDetalle.Name = "gvDetalle";
            this.gvDetalle.OptionsView.ShowIndicator = false;
            this.gvDetalle.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colID_VTA_CRED, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colFECHA_EMISION, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colEMPRESA, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colNOMBRECLIENTE, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colID_VTA_CRED
            // 
            this.colID_VTA_CRED.Caption = "gridColumn1";
            this.colID_VTA_CRED.FieldName = "ID_VTA_CRED";
            this.colID_VTA_CRED.MinWidth = 21;
            this.colID_VTA_CRED.Name = "colID_VTA_CRED";
            this.colID_VTA_CRED.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.colID_VTA_CRED.Width = 79;
            // 
            // colEDITAR
            // 
            this.colEDITAR.Caption = "Editar";
            this.colEDITAR.ColumnEdit = this.riEditar;
            this.colEDITAR.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            this.colEDITAR.MaxWidth = 40;
            this.colEDITAR.MinWidth = 40;
            this.colEDITAR.Name = "colEDITAR";
            this.colEDITAR.OptionsColumn.AllowSize = false;
            this.colEDITAR.OptionsColumn.FixedWidth = true;
            this.colEDITAR.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colEDITAR.ToolTip = "Editar comprobante";
            this.colEDITAR.Visible = true;
            this.colEDITAR.VisibleIndex = 0;
            this.colEDITAR.Width = 40;
            // 
            // riEditar
            // 
            this.riEditar.AllowFocused = false;
            this.riEditar.AutoHeight = false;
            editorButtonImageOptions1.Image = global::SistemaContable.UI.RecursosAdicionales01.editar2_20x20;
            this.riEditar.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.riEditar.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.riEditar.Name = "riEditar";
            this.riEditar.ReadOnly = true;
            this.riEditar.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.riEditar.UseReadOnlyAppearance = false;
            this.riEditar.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riEditar_ButtonClick);
            // 
            // colFECHA_EMISION
            // 
            this.colFECHA_EMISION.AppearanceCell.Options.UseTextOptions = true;
            this.colFECHA_EMISION.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFECHA_EMISION.Caption = "Facturación";
            this.colFECHA_EMISION.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colFECHA_EMISION.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colFECHA_EMISION.FieldName = "FECHA_EMISION";
            this.colFECHA_EMISION.GroupFormat.FormatString = "dd/MM/yyyy";
            this.colFECHA_EMISION.GroupFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colFECHA_EMISION.MinWidth = 100;
            this.colFECHA_EMISION.Name = "colFECHA_EMISION";
            this.colFECHA_EMISION.OptionsColumn.AllowEdit = false;
            this.colFECHA_EMISION.OptionsColumn.FixedWidth = true;
            this.colFECHA_EMISION.Visible = true;
            this.colFECHA_EMISION.VisibleIndex = 1;
            this.colFECHA_EMISION.Width = 100;
            // 
            // colEMPRESA
            // 
            this.colEMPRESA.Caption = "Empresa";
            this.colEMPRESA.FieldName = "EMPRESA";
            this.colEMPRESA.MinWidth = 230;
            this.colEMPRESA.Name = "colEMPRESA";
            this.colEMPRESA.OptionsColumn.AllowEdit = false;
            this.colEMPRESA.OptionsColumn.FixedWidth = true;
            this.colEMPRESA.Visible = true;
            this.colEMPRESA.VisibleIndex = 2;
            this.colEMPRESA.Width = 230;
            // 
            // colTIPO_DTE
            // 
            this.colTIPO_DTE.AppearanceCell.Options.UseTextOptions = true;
            this.colTIPO_DTE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTIPO_DTE.Caption = "Tipo Comp.";
            this.colTIPO_DTE.FieldName = "TIPO_DTE";
            this.colTIPO_DTE.MinWidth = 21;
            this.colTIPO_DTE.Name = "colTIPO_DTE";
            this.colTIPO_DTE.OptionsColumn.AllowEdit = false;
            this.colTIPO_DTE.OptionsColumn.FixedWidth = true;
            this.colTIPO_DTE.Visible = true;
            this.colTIPO_DTE.VisibleIndex = 3;
            this.colTIPO_DTE.Width = 80;
            // 
            // colNITCLIENTE
            // 
            this.colNITCLIENTE.Caption = "Documento";
            this.colNITCLIENTE.FieldName = "NITCLIENTE";
            this.colNITCLIENTE.MinWidth = 100;
            this.colNITCLIENTE.Name = "colNITCLIENTE";
            this.colNITCLIENTE.OptionsColumn.AllowEdit = false;
            this.colNITCLIENTE.Visible = true;
            this.colNITCLIENTE.VisibleIndex = 5;
            this.colNITCLIENTE.Width = 100;
            // 
            // colNOMBRECLIENTE
            // 
            this.colNOMBRECLIENTE.Caption = "Cliente";
            this.colNOMBRECLIENTE.FieldName = "NOMBRECLIENTE";
            this.colNOMBRECLIENTE.MinWidth = 250;
            this.colNOMBRECLIENTE.Name = "colNOMBRECLIENTE";
            this.colNOMBRECLIENTE.OptionsColumn.AllowEdit = false;
            this.colNOMBRECLIENTE.Visible = true;
            this.colNOMBRECLIENTE.VisibleIndex = 6;
            this.colNOMBRECLIENTE.Width = 251;
            // 
            // colNUM_CONTROL
            // 
            this.colNUM_CONTROL.Caption = "N° DTE";
            this.colNUM_CONTROL.FieldName = "NUM_CONTROL";
            this.colNUM_CONTROL.MinWidth = 250;
            this.colNUM_CONTROL.Name = "colNUM_CONTROL";
            this.colNUM_CONTROL.OptionsColumn.AllowEdit = false;
            this.colNUM_CONTROL.Visible = true;
            this.colNUM_CONTROL.VisibleIndex = 4;
            this.colNUM_CONTROL.Width = 260;
            // 
            // colSELLO_RECIBIDO
            // 
            this.colSELLO_RECIBIDO.Caption = "Sello";
            this.colSELLO_RECIBIDO.FieldName = "SELLO_RECIBIDO";
            this.colSELLO_RECIBIDO.MinWidth = 320;
            this.colSELLO_RECIBIDO.Name = "colSELLO_RECIBIDO";
            this.colSELLO_RECIBIDO.OptionsColumn.AllowEdit = false;
            this.colSELLO_RECIBIDO.Visible = true;
            this.colSELLO_RECIBIDO.VisibleIndex = 7;
            this.colSELLO_RECIBIDO.Width = 320;
            // 
            // colTOTAL
            // 
            this.colTOTAL.Caption = "Total";
            this.colTOTAL.DisplayFormat.FormatString = "c2";
            this.colTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTOTAL.FieldName = "TOTAL";
            this.colTOTAL.MinWidth = 100;
            this.colTOTAL.Name = "colTOTAL";
            this.colTOTAL.OptionsColumn.AllowEdit = false;
            this.colTOTAL.Visible = true;
            this.colTOTAL.VisibleIndex = 8;
            this.colTOTAL.Width = 100;
            // 
            // colABONO
            // 
            this.colABONO.Caption = "Abono";
            this.colABONO.DisplayFormat.FormatString = "c2";
            this.colABONO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colABONO.FieldName = "ABONO";
            this.colABONO.MinWidth = 100;
            this.colABONO.Name = "colABONO";
            this.colABONO.OptionsColumn.AllowEdit = false;
            this.colABONO.Visible = true;
            this.colABONO.VisibleIndex = 9;
            this.colABONO.Width = 100;
            // 
            // colSALDO
            // 
            this.colSALDO.AppearanceCell.Options.UseTextOptions = true;
            this.colSALDO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSALDO.Caption = "Saldo";
            this.colSALDO.DisplayFormat.FormatString = "c2";
            this.colSALDO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSALDO.FieldName = "SALDO";
            this.colSALDO.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSALDO.MinWidth = 100;
            this.colSALDO.Name = "colSALDO";
            this.colSALDO.OptionsColumn.AllowEdit = false;
            this.colSALDO.OptionsColumn.FixedWidth = true;
            this.colSALDO.Visible = true;
            this.colSALDO.VisibleIndex = 10;
            this.colSALDO.Width = 100;
            // 
            // frmConsultaDocumento_Credito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1667, 530);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConsultaDocumento_Credito";
            this.Tag = "CONSULTA";
            this.Text = "Consulta de ventas al crédito";
            this.Load += new System.EventHandler(this.frmConsultaDocumento_Credito_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnFinalizar;
        private DevExpress.XtraEditors.SimpleButton btnNuevoGasto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetalle;
        private DevExpress.XtraGrid.Columns.GridColumn colID_VTA_CRED;
        private DevExpress.XtraGrid.Columns.GridColumn colEDITAR;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riEditar;
        private DevExpress.XtraGrid.Columns.GridColumn colFECHA_EMISION;
        private DevExpress.XtraGrid.Columns.GridColumn colEMPRESA;
        private DevExpress.XtraGrid.Columns.GridColumn colTIPO_DTE;
        private DevExpress.XtraGrid.Columns.GridColumn colNUM_CONTROL;
        private DevExpress.XtraGrid.Columns.GridColumn colSELLO_RECIBIDO;
        private DevExpress.XtraGrid.Columns.GridColumn colSALDO;
        private DevExpress.XtraGrid.Columns.GridColumn colNITCLIENTE;
        private DevExpress.XtraGrid.Columns.GridColumn colNOMBRECLIENTE;
        private DevExpress.XtraGrid.Columns.GridColumn colTOTAL;
        private DevExpress.XtraGrid.Columns.GridColumn colABONO;
    }
}