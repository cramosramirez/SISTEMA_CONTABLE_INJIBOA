
namespace SistemaContable.UI.Forms.Distribuidoras
{
    partial class frmConsultaDocumento_CLQ
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
            this.btnNuevoCLQ = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gvDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID_CLQ_ENCA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDITAR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riEditar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colFECHA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNUMERO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDISTRIBUIDORA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDOCUMENTO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTOTAL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSERIE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRESOLUCION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFinalizar);
            this.panel1.Controls.Add(this.btnNuevoCLQ);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1383, 46);
            this.panel1.TabIndex = 7;
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
            // btnNuevoCLQ
            // 
            this.btnNuevoCLQ.AllowFocus = false;
            this.btnNuevoCLQ.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnNuevoCLQ.Appearance.Options.UseFont = true;
            this.btnNuevoCLQ.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevoCLQ.Location = new System.Drawing.Point(10, 5);
            this.btnNuevoCLQ.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnNuevoCLQ.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnNuevoCLQ.Name = "btnNuevoCLQ";
            this.btnNuevoCLQ.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnNuevoCLQ.Size = new System.Drawing.Size(109, 38);
            this.btnNuevoCLQ.TabIndex = 1;
            this.btnNuevoCLQ.TabStop = false;
            this.btnNuevoCLQ.Text = "Nuevo";
            this.btnNuevoCLQ.ToolTip = "Nuevo Quedan";
            this.btnNuevoCLQ.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnNuevoCLQ.ToolTipTitle = "Operación";
            this.btnNuevoCLQ.Click += new System.EventHandler(this.btnNuevoCLQ_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.26957F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1383, 38);
            this.label1.TabIndex = 3;
            this.label1.Text = "Comprobantes de Liquidación (CLQ)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gridControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1383, 508);
            this.panel2.TabIndex = 8;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gvDetalle;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riEditar});
            this.gridControl1.Size = new System.Drawing.Size(1383, 508);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetalle});
            // 
            // gvDetalle
            // 
            this.gvDetalle.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID_CLQ_ENCA,
            this.colEDITAR,
            this.colFECHA,
            this.colNUMERO,
            this.colDISTRIBUIDORA,
            this.colDOCUMENTO,
            this.colTOTAL,
            this.colSERIE,
            this.colRESOLUCION});
            this.gvDetalle.GridControl = this.gridControl1;
            this.gvDetalle.Name = "gvDetalle";
            this.gvDetalle.OptionsView.ShowIndicator = false;
            this.gvDetalle.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colID_CLQ_ENCA, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colID_CLQ_ENCA
            // 
            this.colID_CLQ_ENCA.Caption = "gridColumn1";
            this.colID_CLQ_ENCA.FieldName = "ID_CLQ_ENCA";
            this.colID_CLQ_ENCA.MinWidth = 21;
            this.colID_CLQ_ENCA.Name = "colID_CLQ_ENCA";
            this.colID_CLQ_ENCA.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.colID_CLQ_ENCA.Width = 79;
            // 
            // colEDITAR
            // 
            this.colEDITAR.ColumnEdit = this.riEditar;
            this.colEDITAR.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            this.colEDITAR.MaxWidth = 30;
            this.colEDITAR.MinWidth = 30;
            this.colEDITAR.Name = "colEDITAR";
            this.colEDITAR.OptionsColumn.AllowSize = false;
            this.colEDITAR.OptionsColumn.FixedWidth = true;
            this.colEDITAR.OptionsColumn.ShowCaption = false;
            this.colEDITAR.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colEDITAR.Visible = true;
            this.colEDITAR.VisibleIndex = 0;
            this.colEDITAR.Width = 30;
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
            // colFECHA
            // 
            this.colFECHA.AppearanceCell.Options.UseTextOptions = true;
            this.colFECHA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFECHA.Caption = "Fecha";
            this.colFECHA.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colFECHA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colFECHA.FieldName = "FECHA";
            this.colFECHA.GroupFormat.FormatString = "dd/MM/yyyy";
            this.colFECHA.GroupFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colFECHA.MinWidth = 21;
            this.colFECHA.Name = "colFECHA";
            this.colFECHA.OptionsColumn.AllowEdit = false;
            this.colFECHA.OptionsColumn.FixedWidth = true;
            this.colFECHA.Visible = true;
            this.colFECHA.VisibleIndex = 1;
            this.colFECHA.Width = 87;
            // 
            // colNUMERO
            // 
            this.colNUMERO.Caption = "Número";
            this.colNUMERO.FieldName = "NUMERO";
            this.colNUMERO.MinWidth = 150;
            this.colNUMERO.Name = "colNUMERO";
            this.colNUMERO.OptionsColumn.AllowEdit = false;
            this.colNUMERO.OptionsColumn.FixedWidth = true;
            this.colNUMERO.Visible = true;
            this.colNUMERO.VisibleIndex = 2;
            this.colNUMERO.Width = 150;
            // 
            // colDISTRIBUIDORA
            // 
            this.colDISTRIBUIDORA.Caption = "Distribuidora";
            this.colDISTRIBUIDORA.FieldName = "DISTRIBUIDORA";
            this.colDISTRIBUIDORA.MinWidth = 400;
            this.colDISTRIBUIDORA.Name = "colDISTRIBUIDORA";
            this.colDISTRIBUIDORA.OptionsColumn.AllowEdit = false;
            this.colDISTRIBUIDORA.OptionsColumn.FixedWidth = true;
            this.colDISTRIBUIDORA.Visible = true;
            this.colDISTRIBUIDORA.VisibleIndex = 3;
            this.colDISTRIBUIDORA.Width = 400;
            // 
            // colDOCUMENTO
            // 
            this.colDOCUMENTO.Caption = "Documento";
            this.colDOCUMENTO.FieldName = "DOCUMENTO";
            this.colDOCUMENTO.MinWidth = 300;
            this.colDOCUMENTO.Name = "colDOCUMENTO";
            this.colDOCUMENTO.OptionsColumn.AllowEdit = false;
            this.colDOCUMENTO.OptionsColumn.FixedWidth = true;
            this.colDOCUMENTO.Visible = true;
            this.colDOCUMENTO.VisibleIndex = 4;
            this.colDOCUMENTO.Width = 300;
            // 
            // colTOTAL
            // 
            this.colTOTAL.AppearanceCell.Options.UseTextOptions = true;
            this.colTOTAL.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTOTAL.Caption = "Total";
            this.colTOTAL.DisplayFormat.FormatString = "c2";
            this.colTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTOTAL.FieldName = "TOTAL";
            this.colTOTAL.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTOTAL.MinWidth = 21;
            this.colTOTAL.Name = "colTOTAL";
            this.colTOTAL.OptionsColumn.AllowEdit = false;
            this.colTOTAL.OptionsColumn.FixedWidth = true;
            this.colTOTAL.Visible = true;
            this.colTOTAL.VisibleIndex = 5;
            this.colTOTAL.Width = 105;
            // 
            // colSERIE
            // 
            this.colSERIE.AppearanceCell.Options.UseTextOptions = true;
            this.colSERIE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSERIE.Caption = "Serie";
            this.colSERIE.FieldName = "SERIE";
            this.colSERIE.MinWidth = 150;
            this.colSERIE.Name = "colSERIE";
            this.colSERIE.OptionsColumn.AllowEdit = false;
            this.colSERIE.OptionsColumn.FixedWidth = true;
            this.colSERIE.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colSERIE.Visible = true;
            this.colSERIE.VisibleIndex = 6;
            this.colSERIE.Width = 150;
            // 
            // colRESOLUCION
            // 
            this.colRESOLUCION.Caption = "Resolución";
            this.colRESOLUCION.FieldName = "RESOLUCION";
            this.colRESOLUCION.MinWidth = 24;
            this.colRESOLUCION.Name = "colRESOLUCION";
            this.colRESOLUCION.Visible = true;
            this.colRESOLUCION.VisibleIndex = 7;
            this.colRESOLUCION.Width = 159;
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // frmConsultaDocumento_CLQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1383, 554);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConsultaDocumento_CLQ";
            this.Tag = "CONSULTA";
            this.Text = "Consulta de Comprobantes de Liquidación";
            this.Load += new System.EventHandler(this.frmConsultaDocumento_CLQ_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnFinalizar;
        private DevExpress.XtraEditors.SimpleButton btnNuevoCLQ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetalle;
        private DevExpress.XtraGrid.Columns.GridColumn colID_CLQ_ENCA;
        private DevExpress.XtraGrid.Columns.GridColumn colEDITAR;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riEditar;
        private DevExpress.XtraGrid.Columns.GridColumn colDISTRIBUIDORA;
        private DevExpress.XtraGrid.Columns.GridColumn colDOCUMENTO;
        private DevExpress.XtraGrid.Columns.GridColumn colNUMERO;
        private DevExpress.XtraGrid.Columns.GridColumn colTOTAL;
        private DevExpress.XtraGrid.Columns.GridColumn colFECHA;
        private DevExpress.XtraGrid.Columns.GridColumn colSERIE;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colRESOLUCION;
    }
}