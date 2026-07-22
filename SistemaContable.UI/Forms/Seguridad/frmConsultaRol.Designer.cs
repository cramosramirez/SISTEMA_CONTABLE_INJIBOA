namespace SistemaContable.UI.Forms.Seguridad
{
    partial class frmConsultaRol
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
            this.gvRoles = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID_ROL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDITAR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riEditar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colNOMBRE_ROL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTOTAL_USUARIOS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTOTAL_OPCIONES = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).BeginInit();
            this.SuspendLayout();
            //
            // panel1
            //
            this.panel1.Controls.Add(this.btnNuevo);
            this.panel1.Controls.Add(this.btnSalir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 71);
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
            this.gridControl1.Location = new System.Drawing.Point(0, 71);
            this.gridControl1.MainView = this.gvRoles;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riEditar});
            this.gridControl1.Size = new System.Drawing.Size(900, 500);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.Tag = "Consulta";
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRoles});
            //
            // gvRoles
            //
            this.gvRoles.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID_ROL,
            this.colEDITAR,
            this.colNOMBRE_ROL,
            this.colTOTAL_USUARIOS,
            this.colTOTAL_OPCIONES});
            this.gvRoles.GridControl = this.gridControl1;
            this.gvRoles.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvRoles.Name = "gvRoles";
            this.gvRoles.OptionsView.ShowIndicator = false;
            this.gvRoles.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            //
            // colID_ROL
            //
            this.colID_ROL.FieldName = "ID_ROL";
            this.colID_ROL.MinWidth = 27;
            this.colID_ROL.Name = "colID_ROL";
            this.colID_ROL.Width = 100;
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
            this.colEDITAR.Width = 32;
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
            // colNOMBRE_ROL
            //
            this.colNOMBRE_ROL.Caption = "Nombre";
            this.colNOMBRE_ROL.FieldName = "NOMBRE_ROL";
            this.colNOMBRE_ROL.MinWidth = 24;
            this.colNOMBRE_ROL.Name = "colNOMBRE_ROL";
            this.colNOMBRE_ROL.OptionsColumn.AllowEdit = false;
            this.colNOMBRE_ROL.Visible = true;
            this.colNOMBRE_ROL.VisibleIndex = 1;
            this.colNOMBRE_ROL.Width = 350;
            //
            // colTOTAL_USUARIOS
            //
            this.colTOTAL_USUARIOS.AppearanceCell.Options.UseTextOptions = true;
            this.colTOTAL_USUARIOS.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTOTAL_USUARIOS.Caption = "Usuarios";
            this.colTOTAL_USUARIOS.FieldName = "TOTAL_USUARIOS";
            this.colTOTAL_USUARIOS.MinWidth = 24;
            this.colTOTAL_USUARIOS.Name = "colTOTAL_USUARIOS";
            this.colTOTAL_USUARIOS.OptionsColumn.AllowEdit = false;
            this.colTOTAL_USUARIOS.Visible = true;
            this.colTOTAL_USUARIOS.VisibleIndex = 2;
            this.colTOTAL_USUARIOS.Width = 90;
            //
            // colTOTAL_OPCIONES
            //
            this.colTOTAL_OPCIONES.AppearanceCell.Options.UseTextOptions = true;
            this.colTOTAL_OPCIONES.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTOTAL_OPCIONES.Caption = "Opciones";
            this.colTOTAL_OPCIONES.FieldName = "TOTAL_OPCIONES";
            this.colTOTAL_OPCIONES.MinWidth = 24;
            this.colTOTAL_OPCIONES.Name = "colTOTAL_OPCIONES";
            this.colTOTAL_OPCIONES.OptionsColumn.AllowEdit = false;
            this.colTOTAL_OPCIONES.Visible = true;
            this.colTOTAL_OPCIONES.VisibleIndex = 3;
            this.colTOTAL_OPCIONES.Width = 90;
            //
            // frmConsultaRol
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 571);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frmConsultaRol";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "CONSULTA";
            this.Text = "Consulta de Roles";
            this.Load += new System.EventHandler(this.frmConsultaRol_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRoles;
        private DevExpress.XtraGrid.Columns.GridColumn colID_ROL;
        private DevExpress.XtraGrid.Columns.GridColumn colEDITAR;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riEditar;
        private DevExpress.XtraGrid.Columns.GridColumn colNOMBRE_ROL;
        private DevExpress.XtraGrid.Columns.GridColumn colTOTAL_USUARIOS;
        private DevExpress.XtraGrid.Columns.GridColumn colTOTAL_OPCIONES;
        private void btnSalir_Click(object sender, System.EventArgs e) => this.Close();
    }
}