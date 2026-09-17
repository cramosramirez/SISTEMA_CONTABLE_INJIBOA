
namespace SistemaContable.UI.Forms.Distribuidoras
{
    partial class frmConsultaDocumento_Gasto
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions4 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject13 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject14 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject15 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject16 = new DevExpress.Utils.SerializableAppearanceObject();
            this.riVerRVacio = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colFECHA_EMISION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSALDO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCOD_GENERACION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTIPO_DTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOMBRE_ENTIDAD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riVerR = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colVER_R = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFECHA_RECIBIDO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDITAR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riEditar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gvDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID_CCF_COMPRA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnFinalizar = new DevExpress.XtraEditors.SimpleButton();
            this.btnNuevoGasto = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.riVerRVacio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riVerR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // riVerRVacio
            // 
            this.riVerRVacio.AutoHeight = false;
            this.riVerRVacio.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.riVerRVacio.Name = "riVerRVacio";
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
            this.colFECHA_EMISION.MinWidth = 21;
            this.colFECHA_EMISION.Name = "colFECHA_EMISION";
            this.colFECHA_EMISION.OptionsColumn.AllowEdit = false;
            this.colFECHA_EMISION.OptionsColumn.FixedWidth = true;
            this.colFECHA_EMISION.Visible = true;
            this.colFECHA_EMISION.VisibleIndex = 6;
            this.colFECHA_EMISION.Width = 87;
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
            this.colSALDO.MinWidth = 21;
            this.colSALDO.Name = "colSALDO";
            this.colSALDO.OptionsColumn.AllowEdit = false;
            this.colSALDO.OptionsColumn.FixedWidth = true;
            this.colSALDO.Visible = true;
            this.colSALDO.VisibleIndex = 5;
            this.colSALDO.Width = 105;
            // 
            // colCOD_GENERACION
            // 
            this.colCOD_GENERACION.Caption = "N°/Cod. Generación";
            this.colCOD_GENERACION.FieldName = "COD_GENERACION";
            this.colCOD_GENERACION.MinWidth = 21;
            this.colCOD_GENERACION.Name = "colCOD_GENERACION";
            this.colCOD_GENERACION.OptionsColumn.AllowEdit = false;
            this.colCOD_GENERACION.OptionsColumn.FixedWidth = true;
            this.colCOD_GENERACION.Visible = true;
            this.colCOD_GENERACION.VisibleIndex = 4;
            this.colCOD_GENERACION.Width = 276;
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
            // colNOMBRE_ENTIDAD
            // 
            this.colNOMBRE_ENTIDAD.Caption = "Proveedor";
            this.colNOMBRE_ENTIDAD.FieldName = "NOMBRE_ENTIDAD";
            this.colNOMBRE_ENTIDAD.MinWidth = 21;
            this.colNOMBRE_ENTIDAD.Name = "colNOMBRE_ENTIDAD";
            this.colNOMBRE_ENTIDAD.OptionsColumn.AllowEdit = false;
            this.colNOMBRE_ENTIDAD.OptionsColumn.FixedWidth = true;
            this.colNOMBRE_ENTIDAD.Visible = true;
            this.colNOMBRE_ENTIDAD.VisibleIndex = 2;
            this.colNOMBRE_ENTIDAD.Width = 289;
            // 
            // riVerR
            // 
            this.riVerR.AllowFocused = false;
            this.riVerR.AutoHeight = false;
            editorButtonImageOptions3.Image = global::SistemaContable.UI.RecursosAdicionales01.retencion20x20;
            this.riVerR.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, serializableAppearanceObject10, serializableAppearanceObject11, serializableAppearanceObject12, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.riVerR.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.riVerR.Name = "riVerR";
            this.riVerR.ReadOnly = true;
            this.riVerR.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // colVER_R
            // 
            this.colVER_R.ColumnEdit = this.riVerR;
            this.colVER_R.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            this.colVER_R.MaxWidth = 40;
            this.colVER_R.MinWidth = 40;
            this.colVER_R.Name = "colVER_R";
            this.colVER_R.OptionsColumn.AllowSize = false;
            this.colVER_R.OptionsColumn.FixedWidth = true;
            this.colVER_R.OptionsColumn.ShowCaption = false;
            this.colVER_R.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colVER_R.Visible = true;
            this.colVER_R.VisibleIndex = 1;
            this.colVER_R.Width = 40;
            // 
            // colFECHA_RECIBIDO
            // 
            this.colFECHA_RECIBIDO.AppearanceCell.Options.UseTextOptions = true;
            this.colFECHA_RECIBIDO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFECHA_RECIBIDO.Caption = "Recibido";
            this.colFECHA_RECIBIDO.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colFECHA_RECIBIDO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colFECHA_RECIBIDO.FieldName = "FECHA_RECIBIDO";
            this.colFECHA_RECIBIDO.MinWidth = 21;
            this.colFECHA_RECIBIDO.Name = "colFECHA_RECIBIDO";
            this.colFECHA_RECIBIDO.OptionsColumn.AllowEdit = false;
            this.colFECHA_RECIBIDO.OptionsColumn.FixedWidth = true;
            this.colFECHA_RECIBIDO.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colFECHA_RECIBIDO.Visible = true;
            this.colFECHA_RECIBIDO.VisibleIndex = 7;
            this.colFECHA_RECIBIDO.Width = 87;
            // 
            // colEDITAR
            // 
            this.colEDITAR.ColumnEdit = this.riEditar;
            this.colEDITAR.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            this.colEDITAR.MaxWidth = 40;
            this.colEDITAR.MinWidth = 40;
            this.colEDITAR.Name = "colEDITAR";
            this.colEDITAR.OptionsColumn.AllowSize = false;
            this.colEDITAR.OptionsColumn.FixedWidth = true;
            this.colEDITAR.OptionsColumn.ShowCaption = false;
            this.colEDITAR.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colEDITAR.Visible = true;
            this.colEDITAR.VisibleIndex = 0;
            this.colEDITAR.Width = 40;
            // 
            // riEditar
            // 
            this.riEditar.AllowFocused = false;
            this.riEditar.AutoHeight = false;
            editorButtonImageOptions4.Image = global::SistemaContable.UI.RecursosAdicionales01.editar2_20x20;
            this.riEditar.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject13, serializableAppearanceObject14, serializableAppearanceObject15, serializableAppearanceObject16, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.riEditar.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.riEditar.Name = "riEditar";
            this.riEditar.ReadOnly = true;
            this.riEditar.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.riEditar.UseReadOnlyAppearance = false;
            this.riEditar.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riEditar_ButtonClick);
            // 
            // gvDetalle
            // 
            this.gvDetalle.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID_CCF_COMPRA,
            this.colEDITAR,
            this.colVER_R,
            this.colNOMBRE_ENTIDAD,
            this.colTIPO_DTE,
            this.colCOD_GENERACION,
            this.colSALDO,
            this.colFECHA_EMISION,
            this.colFECHA_RECIBIDO});
            this.gvDetalle.GridControl = this.gridControl1;
            this.gvDetalle.Name = "gvDetalle";
            this.gvDetalle.OptionsView.ShowIndicator = false;
            this.gvDetalle.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colID_CCF_COMPRA, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colID_CCF_COMPRA
            // 
            this.colID_CCF_COMPRA.Caption = "gridColumn1";
            this.colID_CCF_COMPRA.FieldName = "ID_CCF_COMPRA";
            this.colID_CCF_COMPRA.MinWidth = 21;
            this.colID_CCF_COMPRA.Name = "colID_CCF_COMPRA";
            this.colID_CCF_COMPRA.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.colID_CCF_COMPRA.Width = 79;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gvDetalle;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riEditar,
            this.riVerR,
            this.riVerRVacio});
            this.gridControl1.Size = new System.Drawing.Size(1343, 521);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetalle});
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gridControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1343, 521);
            this.panel2.TabIndex = 8;
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
            this.label1.Size = new System.Drawing.Size(1343, 38);
            this.label1.TabIndex = 3;
            this.label1.Text = "Gastos de Distribuidoras";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFinalizar);
            this.panel1.Controls.Add(this.btnNuevoGasto);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1343, 46);
            this.panel1.TabIndex = 7;
            // 
            // frmConsultaDocumento_Gasto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 567);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConsultaDocumento_Gasto";
            this.Tag = "CONSULTA";
            this.Text = "Consulta de Gastos de Distribuidoras";
            this.Load += new System.EventHandler(this.frmConsultaDocumento_Gasto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.riVerRVacio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riVerR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riVerRVacio;
        private DevExpress.XtraGrid.Columns.GridColumn colFECHA_EMISION;
        private DevExpress.XtraGrid.Columns.GridColumn colSALDO;
        private DevExpress.XtraGrid.Columns.GridColumn colCOD_GENERACION;
        private DevExpress.XtraGrid.Columns.GridColumn colTIPO_DTE;
        private DevExpress.XtraGrid.Columns.GridColumn colNOMBRE_ENTIDAD;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riVerR;
        private DevExpress.XtraGrid.Columns.GridColumn colVER_R;
        private DevExpress.XtraGrid.Columns.GridColumn colFECHA_RECIBIDO;
        private DevExpress.XtraGrid.Columns.GridColumn colEDITAR;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riEditar;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetalle;
        private DevExpress.XtraGrid.Columns.GridColumn colID_CCF_COMPRA;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnFinalizar;
        private DevExpress.XtraEditors.SimpleButton btnNuevoGasto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
    }
}