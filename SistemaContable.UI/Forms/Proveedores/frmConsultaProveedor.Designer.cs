
namespace SistemaContable.UI.Forms.Proveedores
{
    partial class frmConsultaProveedor
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
            this.riEditar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFinalizar = new DevExpress.XtraEditors.SimpleButton();
            this.btnNuevoQuedan = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gvDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID_ENTIDAD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDITAR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCODIGO_ENTIDAD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOMBRE_ENTIDAD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNRC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDUI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNIT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTELEFONO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCORREO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTIPO_CONTRIBUYENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalle)).BeginInit();
            this.SuspendLayout();
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
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFinalizar);
            this.panel1.Controls.Add(this.btnNuevoQuedan);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1321, 46);
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
            // btnNuevoQuedan
            // 
            this.btnNuevoQuedan.AllowFocus = false;
            this.btnNuevoQuedan.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnNuevoQuedan.Appearance.Options.UseFont = true;
            this.btnNuevoQuedan.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevoQuedan.Location = new System.Drawing.Point(10, 5);
            this.btnNuevoQuedan.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnNuevoQuedan.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnNuevoQuedan.Name = "btnNuevoQuedan";
            this.btnNuevoQuedan.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnNuevoQuedan.Size = new System.Drawing.Size(109, 38);
            this.btnNuevoQuedan.TabIndex = 1;
            this.btnNuevoQuedan.TabStop = false;
            this.btnNuevoQuedan.Text = "Nuevo";
            this.btnNuevoQuedan.ToolTip = "Nuevo Quedan";
            this.btnNuevoQuedan.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnNuevoQuedan.ToolTipTitle = "Operación";
            this.btnNuevoQuedan.Click += new System.EventHandler(this.btnNuevoQuedan_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.26957F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1321, 38);
            this.label1.TabIndex = 3;
            this.label1.Text = "Proveedores";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gridControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1321, 404);
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
            this.gridControl1.Size = new System.Drawing.Size(1321, 404);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetalle});
            // 
            // gvDetalle
            // 
            this.gvDetalle.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID_ENTIDAD,
            this.colEDITAR,
            this.colCODIGO_ENTIDAD,
            this.colNOMBRE_ENTIDAD,
            this.colNRC,
            this.colDUI,
            this.colNIT,
            this.colTELEFONO,
            this.colCORREO,
            this.colTIPO_CONTRIBUYENTE});
            this.gvDetalle.GridControl = this.gridControl1;
            this.gvDetalle.Name = "gvDetalle";
            this.gvDetalle.OptionsView.ShowIndicator = false;
            this.gvDetalle.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colID_ENTIDAD, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colNOMBRE_ENTIDAD, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colID_ENTIDAD
            // 
            this.colID_ENTIDAD.Caption = "gridColumn1";
            this.colID_ENTIDAD.FieldName = "ID_ENTIDAD";
            this.colID_ENTIDAD.MinWidth = 21;
            this.colID_ENTIDAD.Name = "colID_ENTIDAD";
            this.colID_ENTIDAD.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.colID_ENTIDAD.Width = 79;
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
            // colCODIGO_ENTIDAD
            // 
            this.colCODIGO_ENTIDAD.Caption = "Código";
            this.colCODIGO_ENTIDAD.FieldName = "CODIGO_ENTIDAD";
            this.colCODIGO_ENTIDAD.MinWidth = 24;
            this.colCODIGO_ENTIDAD.Name = "colCODIGO_ENTIDAD";
            this.colCODIGO_ENTIDAD.Visible = true;
            this.colCODIGO_ENTIDAD.VisibleIndex = 1;
            this.colCODIGO_ENTIDAD.Width = 111;
            // 
            // colNOMBRE_ENTIDAD
            // 
            this.colNOMBRE_ENTIDAD.Caption = "Nombre";
            this.colNOMBRE_ENTIDAD.FieldName = "NOMBRE";
            this.colNOMBRE_ENTIDAD.MaxWidth = 500;
            this.colNOMBRE_ENTIDAD.MinWidth = 21;
            this.colNOMBRE_ENTIDAD.Name = "colNOMBRE_ENTIDAD";
            this.colNOMBRE_ENTIDAD.OptionsColumn.AllowEdit = false;
            this.colNOMBRE_ENTIDAD.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.colNOMBRE_ENTIDAD.Visible = true;
            this.colNOMBRE_ENTIDAD.VisibleIndex = 2;
            this.colNOMBRE_ENTIDAD.Width = 248;
            // 
            // colNRC
            // 
            this.colNRC.AppearanceCell.Options.UseTextOptions = true;
            this.colNRC.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNRC.Caption = "NRC";
            this.colNRC.FieldName = "NRC";
            this.colNRC.MinWidth = 100;
            this.colNRC.Name = "colNRC";
            this.colNRC.OptionsColumn.AllowEdit = false;
            this.colNRC.Visible = true;
            this.colNRC.VisibleIndex = 3;
            this.colNRC.Width = 123;
            // 
            // colDUI
            // 
            this.colDUI.Caption = "DUI";
            this.colDUI.FieldName = "DUI";
            this.colDUI.MinWidth = 21;
            this.colDUI.Name = "colDUI";
            this.colDUI.OptionsColumn.AllowEdit = false;
            this.colDUI.Visible = true;
            this.colDUI.VisibleIndex = 4;
            this.colDUI.Width = 343;
            // 
            // colNIT
            // 
            this.colNIT.Caption = "NIT";
            this.colNIT.FieldName = "NIT";
            this.colNIT.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNIT.MinWidth = 21;
            this.colNIT.Name = "colNIT";
            this.colNIT.OptionsColumn.AllowEdit = false;
            this.colNIT.Visible = true;
            this.colNIT.VisibleIndex = 5;
            this.colNIT.Width = 130;
            // 
            // colTELEFONO
            // 
            this.colTELEFONO.AppearanceCell.Options.UseTextOptions = true;
            this.colTELEFONO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTELEFONO.Caption = "Teléfono";
            this.colTELEFONO.FieldName = "TELEFONO";
            this.colTELEFONO.GroupFormat.FormatString = "dd/MM/yyyy";
            this.colTELEFONO.GroupFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTELEFONO.MinWidth = 21;
            this.colTELEFONO.Name = "colTELEFONO";
            this.colTELEFONO.OptionsColumn.AllowEdit = false;
            this.colTELEFONO.Visible = true;
            this.colTELEFONO.VisibleIndex = 6;
            this.colTELEFONO.Width = 107;
            // 
            // colCORREO
            // 
            this.colCORREO.Caption = "Correo";
            this.colCORREO.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colCORREO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCORREO.FieldName = "CORREO";
            this.colCORREO.MinWidth = 21;
            this.colCORREO.Name = "colCORREO";
            this.colCORREO.OptionsColumn.AllowEdit = false;
            this.colCORREO.OptionsColumn.ImmediateUpdateRowPosition = DevExpress.Utils.DefaultBoolean.False;
            this.colCORREO.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colCORREO.Visible = true;
            this.colCORREO.VisibleIndex = 7;
            this.colCORREO.Width = 107;
            // 
            // colTIPO_CONTRIBUYENTE
            // 
            this.colTIPO_CONTRIBUYENTE.Caption = "Tipo contribuyente";
            this.colTIPO_CONTRIBUYENTE.FieldName = "TIPO_CONTRIBUYENTE";
            this.colTIPO_CONTRIBUYENTE.MinWidth = 24;
            this.colTIPO_CONTRIBUYENTE.Name = "colTIPO_CONTRIBUYENTE";
            this.colTIPO_CONTRIBUYENTE.Visible = true;
            this.colTIPO_CONTRIBUYENTE.VisibleIndex = 8;
            this.colTIPO_CONTRIBUYENTE.Width = 120;
            // 
            // frmConsultaProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1321, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConsultaProveedor";
            this.Tag = "CONSULTA";
            this.Text = "Consulta Proveedores";
            this.Load += new System.EventHandler(this.frmConsultaProveedor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riEditar;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnFinalizar;
        private DevExpress.XtraEditors.SimpleButton btnNuevoQuedan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetalle;
        private DevExpress.XtraGrid.Columns.GridColumn colID_ENTIDAD;
        private DevExpress.XtraGrid.Columns.GridColumn colEDITAR;
        private DevExpress.XtraGrid.Columns.GridColumn colNOMBRE_ENTIDAD;
        private DevExpress.XtraGrid.Columns.GridColumn colNRC;
        private DevExpress.XtraGrid.Columns.GridColumn colDUI;
        private DevExpress.XtraGrid.Columns.GridColumn colNIT;
        private DevExpress.XtraGrid.Columns.GridColumn colTELEFONO;
        private DevExpress.XtraGrid.Columns.GridColumn colCORREO;
        private DevExpress.XtraGrid.Columns.GridColumn colCODIGO_ENTIDAD;
        private DevExpress.XtraGrid.Columns.GridColumn colTIPO_CONTRIBUYENTE;
    }
}