namespace SistemaContable.UI.Forms.Clientes
{
    partial class frmConsultaCliente
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNuevo = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gvEntidades = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID_ENTIDAD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDITAR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riEditar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colCODIGO_ENTIDAD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOMBRE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNRC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDUI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNIT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTELEFONO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCORREO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTIPO_CONTRIBUYENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEntidades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnNuevo);
            this.panel1.Controls.Add(this.btnSalir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1733, 71);
            this.panel1.TabIndex = 0;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.Appearance.Options.UseFont = true;
            this.btnNuevo.Appearance.Options.UseTextOptions = true;
            this.btnNuevo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnNuevo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevo.Location = new System.Drawing.Point(16, 12);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(147, 47);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.TabStop = false;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnSalir.Appearance.Options.UseFont = true;
            this.btnSalir.Appearance.Options.UseTextOptions = true;
            this.btnSalir.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnSalir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnSalir.Location = new System.Drawing.Point(173, 12);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(147, 47);
            this.btnSalir.TabIndex = 1;
            this.btnSalir.TabStop = false;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridControl1.Location = new System.Drawing.Point(0, 71);
            this.gridControl1.MainView = this.gvEntidades;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riEditar});
            this.gridControl1.Size = new System.Drawing.Size(1733, 606);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.Tag = "Consulta";
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvEntidades});
            // 
            // gvEntidades
            // 
            this.gvEntidades.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID_ENTIDAD,
            this.colEDITAR,
            this.colCODIGO_ENTIDAD,
            this.colNOMBRE,
            this.colNRC,
            this.colDUI,
            this.colNIT,
            this.colTELEFONO,
            this.colCORREO,
            this.colTIPO_CONTRIBUYENTE});
            this.gvEntidades.DetailHeight = 431;
            this.gvEntidades.GridControl = this.gridControl1;
            this.gvEntidades.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvEntidades.Name = "gvEntidades";
            this.gvEntidades.OptionsView.ShowIndicator = false;
            this.gvEntidades.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // colID_ENTIDAD
            // 
            this.colID_ENTIDAD.FieldName = "ID_ENTIDAD";
            this.colID_ENTIDAD.MinWidth = 27;
            this.colID_ENTIDAD.Name = "colID_ENTIDAD";
            this.colID_ENTIDAD.Width = 100;
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
            // colCODIGO_ENTIDAD
            // 
            this.colCODIGO_ENTIDAD.Caption = "Código";
            this.colCODIGO_ENTIDAD.FieldName = "CODIGO_ENTIDAD";
            this.colCODIGO_ENTIDAD.MinWidth = 24;
            this.colCODIGO_ENTIDAD.Name = "colCODIGO_ENTIDAD";
            this.colCODIGO_ENTIDAD.OptionsColumn.AllowEdit = false;
            this.colCODIGO_ENTIDAD.Visible = true;
            this.colCODIGO_ENTIDAD.VisibleIndex = 1;
            this.colCODIGO_ENTIDAD.Width = 111;
            // 
            // colNOMBRE
            // 
            this.colNOMBRE.Caption = "Nombre";
            this.colNOMBRE.FieldName = "NOMBRE";
            this.colNOMBRE.MaxWidth = 500;
            this.colNOMBRE.MinWidth = 21;
            this.colNOMBRE.Name = "colNOMBRE";
            this.colNOMBRE.OptionsColumn.AllowEdit = false;
            this.colNOMBRE.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.colNOMBRE.Visible = true;
            this.colNOMBRE.VisibleIndex = 2;
            this.colNOMBRE.Width = 248;
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
            this.colDUI.Width = 120;
            // 
            // colNIT
            // 
            this.colNIT.Caption = "NIT";
            this.colNIT.FieldName = "NIT";
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
            this.colCORREO.FieldName = "CORREO";
            this.colCORREO.MinWidth = 21;
            this.colCORREO.Name = "colCORREO";
            this.colCORREO.OptionsColumn.AllowEdit = false;
            this.colCORREO.Visible = true;
            this.colCORREO.VisibleIndex = 7;
            this.colCORREO.Width = 107;
            // 
            // colTIPO_CONTRIBUYENTE
            // 
            this.colTIPO_CONTRIBUYENTE.Caption = "Tipo contribuyente";
            this.colTIPO_CONTRIBUYENTE.FieldName = "NOMBRE_TIPO_CONTRIB";
            this.colTIPO_CONTRIBUYENTE.MinWidth = 24;
            this.colTIPO_CONTRIBUYENTE.Name = "colTIPO_CONTRIBUYENTE";
            this.colTIPO_CONTRIBUYENTE.OptionsColumn.AllowEdit = false;
            this.colTIPO_CONTRIBUYENTE.Visible = true;
            this.colTIPO_CONTRIBUYENTE.VisibleIndex = 8;
            this.colTIPO_CONTRIBUYENTE.Width = 120;
            // 
            // frmConsultaCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1733, 677);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmConsultaCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "CONSULTA";
            this.Text = "Consulta de Entidades";
            this.Load += new System.EventHandler(this.frmConsultaCliente_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEntidades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvEntidades;
        private DevExpress.XtraGrid.Columns.GridColumn colID_ENTIDAD;
        private DevExpress.XtraGrid.Columns.GridColumn colEDITAR;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riEditar;
        private DevExpress.XtraGrid.Columns.GridColumn colCODIGO_ENTIDAD;
        private DevExpress.XtraGrid.Columns.GridColumn colNOMBRE;
        private DevExpress.XtraGrid.Columns.GridColumn colNRC;
        private DevExpress.XtraGrid.Columns.GridColumn colDUI;
        private DevExpress.XtraGrid.Columns.GridColumn colNIT;
        private DevExpress.XtraGrid.Columns.GridColumn colTELEFONO;
        private DevExpress.XtraGrid.Columns.GridColumn colCORREO;
        private DevExpress.XtraGrid.Columns.GridColumn colTIPO_CONTRIBUYENTE;
        private void btnSalir_Click(object sender, System.EventArgs e) => this.Close();
    }
}
