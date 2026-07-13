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
            this.colNOMBRE_COMERCIAL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTIPO_PERSONA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNRC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNIT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCORREO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTELEFONO = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.colNOMBRE_COMERCIAL,
            this.colTIPO_PERSONA,
            this.colNRC,
            this.colNIT,
            this.colCORREO,
            this.colTELEFONO});
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
            this.colEDITAR.Caption = " ";
            this.colEDITAR.ColumnEdit = this.riEditar;
            this.colEDITAR.MinWidth = 24;
            this.colEDITAR.Name = "colEDITAR";
            this.colEDITAR.OptionsColumn.AllowSize = false;
            this.colEDITAR.OptionsColumn.ShowCaption = false;
            this.colEDITAR.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colEDITAR.Visible = true;
            this.colEDITAR.VisibleIndex = 0;
            this.colEDITAR.Width = 55;
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
            this.colCODIGO_ENTIDAD.Width = 147;
            // 
            // colNOMBRE
            // 
            this.colNOMBRE.Caption = "Nombre";
            this.colNOMBRE.FieldName = "NOMBRE";
            this.colNOMBRE.MinWidth = 24;
            this.colNOMBRE.Name = "colNOMBRE";
            this.colNOMBRE.OptionsColumn.AllowEdit = false;
            this.colNOMBRE.Visible = true;
            this.colNOMBRE.VisibleIndex = 2;
            this.colNOMBRE.Width = 400;
            // 
            // colNOMBRE_COMERCIAL
            // 
            this.colNOMBRE_COMERCIAL.Caption = "Nombre Comercial";
            this.colNOMBRE_COMERCIAL.FieldName = "NOMBRE_COMERCIAL";
            this.colNOMBRE_COMERCIAL.MinWidth = 24;
            this.colNOMBRE_COMERCIAL.Name = "colNOMBRE_COMERCIAL";
            this.colNOMBRE_COMERCIAL.OptionsColumn.AllowEdit = false;
            this.colNOMBRE_COMERCIAL.Visible = true;
            this.colNOMBRE_COMERCIAL.VisibleIndex = 3;
            this.colNOMBRE_COMERCIAL.Width = 267;
            // 
            // colTIPO_PERSONA
            // 
            this.colTIPO_PERSONA.Caption = "Tipo Persona";
            this.colTIPO_PERSONA.FieldName = "NOMBRE_TIPO_PERSONA";
            this.colTIPO_PERSONA.MinWidth = 24;
            this.colTIPO_PERSONA.Name = "colTIPO_PERSONA";
            this.colTIPO_PERSONA.OptionsColumn.AllowEdit = false;
            this.colTIPO_PERSONA.Visible = true;
            this.colTIPO_PERSONA.VisibleIndex = 4;
            this.colTIPO_PERSONA.Width = 160;
            // 
            // colNRC
            // 
            this.colNRC.AppearanceCell.Options.UseTextOptions = true;
            this.colNRC.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNRC.Caption = "NRC";
            this.colNRC.FieldName = "NRC";
            this.colNRC.MinWidth = 24;
            this.colNRC.Name = "colNRC";
            this.colNRC.OptionsColumn.AllowEdit = false;
            this.colNRC.Visible = true;
            this.colNRC.VisibleIndex = 5;
            this.colNRC.Width = 120;
            // 
            // colNIT
            // 
            this.colNIT.Caption = "NIT";
            this.colNIT.FieldName = "NIT";
            this.colNIT.MinWidth = 24;
            this.colNIT.Name = "colNIT";
            this.colNIT.OptionsColumn.AllowEdit = false;
            this.colNIT.Visible = true;
            this.colNIT.VisibleIndex = 6;
            this.colNIT.Width = 147;
            // 
            // colCORREO
            // 
            this.colCORREO.Caption = "Correo";
            this.colCORREO.FieldName = "CORREO";
            this.colCORREO.MinWidth = 24;
            this.colCORREO.Name = "colCORREO";
            this.colCORREO.OptionsColumn.AllowEdit = false;
            this.colCORREO.Visible = true;
            this.colCORREO.VisibleIndex = 7;
            this.colCORREO.Width = 267;
            // 
            // colTELEFONO
            // 
            this.colTELEFONO.Caption = "Teléfono";
            this.colTELEFONO.FieldName = "TELEFONO";
            this.colTELEFONO.MinWidth = 24;
            this.colTELEFONO.Name = "colTELEFONO";
            this.colTELEFONO.OptionsColumn.AllowEdit = false;
            this.colTELEFONO.Visible = true;
            this.colTELEFONO.VisibleIndex = 8;
            this.colTELEFONO.Width = 133;
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
        private DevExpress.XtraGrid.Columns.GridColumn colNOMBRE_COMERCIAL;
        private DevExpress.XtraGrid.Columns.GridColumn colTIPO_PERSONA;
        private DevExpress.XtraGrid.Columns.GridColumn colNRC;
        private DevExpress.XtraGrid.Columns.GridColumn colNIT;
        private DevExpress.XtraGrid.Columns.GridColumn colCORREO;
        private DevExpress.XtraGrid.Columns.GridColumn colTELEFONO;
        private void btnSalir_Click(object sender, System.EventArgs e) => this.Close();
    }
}