namespace SistemaContable.UI.Forms.Seguridad
{
    partial class frmConsultaUsuario
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
            this.gvUsuarios = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colEDITAR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riEditar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colUSUARIO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOMBRE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEMAIL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOMBRE_ROL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colACTIVO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBLOQUEADO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFECHA_ULTACCESO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUsuarios)).BeginInit();
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
            this.panel1.Size = new System.Drawing.Size(1000, 71);
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
            this.gridControl1.MainView = this.gvUsuarios;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riEditar});
            this.gridControl1.Size = new System.Drawing.Size(1000, 500);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.Tag = "Consulta";
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUsuarios});
            //
            // gvUsuarios
            //
            this.gvUsuarios.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colEDITAR,
            this.colUSUARIO,
            this.colNOMBRE,
            this.colEMAIL,
            this.colNOMBRE_ROL,
            this.colACTIVO,
            this.colBLOQUEADO,
            this.colFECHA_ULTACCESO});
            this.gvUsuarios.GridControl = this.gridControl1;
            this.gvUsuarios.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvUsuarios.Name = "gvUsuarios";
            this.gvUsuarios.OptionsView.ShowIndicator = false;
            this.gvUsuarios.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
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
            // colUSUARIO
            //
            this.colUSUARIO.Caption = "Usuario";
            this.colUSUARIO.FieldName = "USUARIO";
            this.colUSUARIO.MinWidth = 24;
            this.colUSUARIO.Name = "colUSUARIO";
            this.colUSUARIO.OptionsColumn.AllowEdit = false;
            this.colUSUARIO.Visible = true;
            this.colUSUARIO.VisibleIndex = 1;
            this.colUSUARIO.Width = 130;
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
            this.colNOMBRE.Width = 220;
            //
            // colEMAIL
            //
            this.colEMAIL.Caption = "Email";
            this.colEMAIL.FieldName = "EMAIL";
            this.colEMAIL.MinWidth = 24;
            this.colEMAIL.Name = "colEMAIL";
            this.colEMAIL.OptionsColumn.AllowEdit = false;
            this.colEMAIL.Visible = true;
            this.colEMAIL.VisibleIndex = 3;
            this.colEMAIL.Width = 180;
            //
            // colNOMBRE_ROL
            //
            this.colNOMBRE_ROL.Caption = "Rol";
            this.colNOMBRE_ROL.FieldName = "NOMBRE_ROL";
            this.colNOMBRE_ROL.MinWidth = 24;
            this.colNOMBRE_ROL.Name = "colNOMBRE_ROL";
            this.colNOMBRE_ROL.OptionsColumn.AllowEdit = false;
            this.colNOMBRE_ROL.Visible = true;
            this.colNOMBRE_ROL.VisibleIndex = 4;
            this.colNOMBRE_ROL.Width = 140;
            //
            // colACTIVO
            //
            this.colACTIVO.AppearanceCell.Options.UseTextOptions = true;
            this.colACTIVO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colACTIVO.Caption = "Activo";
            this.colACTIVO.FieldName = "ACTIVO";
            this.colACTIVO.MinWidth = 24;
            this.colACTIVO.Name = "colACTIVO";
            this.colACTIVO.OptionsColumn.AllowEdit = false;
            this.colACTIVO.Visible = true;
            this.colACTIVO.VisibleIndex = 5;
            this.colACTIVO.Width = 60;
            //
            // colBLOQUEADO
            //
            this.colBLOQUEADO.AppearanceCell.Options.UseTextOptions = true;
            this.colBLOQUEADO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBLOQUEADO.Caption = "Bloqueado";
            this.colBLOQUEADO.FieldName = "BLOQUEADO";
            this.colBLOQUEADO.MinWidth = 24;
            this.colBLOQUEADO.Name = "colBLOQUEADO";
            this.colBLOQUEADO.OptionsColumn.AllowEdit = false;
            this.colBLOQUEADO.Visible = true;
            this.colBLOQUEADO.VisibleIndex = 6;
            this.colBLOQUEADO.Width = 75;
            //
            // colFECHA_ULTACCESO
            //
            this.colFECHA_ULTACCESO.Caption = "Último Acceso";
            this.colFECHA_ULTACCESO.FieldName = "FECHA_ULTACCESO";
            this.colFECHA_ULTACCESO.MinWidth = 24;
            this.colFECHA_ULTACCESO.Name = "colFECHA_ULTACCESO";
            this.colFECHA_ULTACCESO.OptionsColumn.AllowEdit = false;
            this.colFECHA_ULTACCESO.Visible = true;
            this.colFECHA_ULTACCESO.VisibleIndex = 7;
            this.colFECHA_ULTACCESO.Width = 130;
            //
            // frmConsultaUsuario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 571);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frmConsultaUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "CONSULTA";
            this.Text = "Consulta de Usuarios";
            this.Load += new System.EventHandler(this.frmConsultaUsuario_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUsuarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUsuarios;
        private DevExpress.XtraGrid.Columns.GridColumn colEDITAR;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riEditar;
        private DevExpress.XtraGrid.Columns.GridColumn colUSUARIO;
        private DevExpress.XtraGrid.Columns.GridColumn colNOMBRE;
        private DevExpress.XtraGrid.Columns.GridColumn colEMAIL;
        private DevExpress.XtraGrid.Columns.GridColumn colNOMBRE_ROL;
        private DevExpress.XtraGrid.Columns.GridColumn colACTIVO;
        private DevExpress.XtraGrid.Columns.GridColumn colBLOQUEADO;
        private DevExpress.XtraGrid.Columns.GridColumn colFECHA_ULTACCESO;
        private void btnSalir_Click(object sender, System.EventArgs e) => this.Close();
    }
}