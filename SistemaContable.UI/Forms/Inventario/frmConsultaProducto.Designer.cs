namespace SistemaContable.UI.Forms.Inventario
{
    partial class frmConsultaProducto
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
            this.gvProductos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID_PRODUCTO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDITAR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riEditar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colCOD_REF = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDESCRIPCION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCATEGORIA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSUBCATEGORIA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUNIMEDIDA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPRECIO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colESTADO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductos)).BeginInit();
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
            this.panel1.Size = new System.Drawing.Size(1200, 58);
            this.panel1.TabIndex = 0;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.Appearance.Options.UseFont = true;
            this.btnNuevo.Appearance.Options.UseTextOptions = true;
            this.btnNuevo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnNuevo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevo.Location = new System.Drawing.Point(12, 10);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(110, 38);
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
            this.btnSalir.Location = new System.Drawing.Point(130, 10);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(110, 38);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.TabStop = false;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 58);
            this.gridControl1.MainView = this.gvProductos;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riEditar});
            this.gridControl1.Size = new System.Drawing.Size(1200, 492);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.Tag = "Consulta";
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProductos});
            // 
            // gvProductos
            // 
            this.gvProductos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID_PRODUCTO,
            this.colEDITAR,
            this.colCOD_REF,
            this.colDESCRIPCION,
            this.colCATEGORIA,
            this.colSUBCATEGORIA,
            this.colUNIMEDIDA,
            this.colPRECIO,
            this.colESTADO});
            this.gvProductos.GridControl = this.gridControl1;
            this.gvProductos.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvProductos.Name = "gvProductos";
            this.gvProductos.OptionsView.ShowIndicator = false;
            this.gvProductos.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // colID_PRODUCTO
            // 
            this.colID_PRODUCTO.FieldName = "ID_PRODUCTO";
            this.colID_PRODUCTO.Name = "colID_PRODUCTO";
            // 
            // colEDITAR
            // 
            this.colEDITAR.Caption = " ";
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
            this.riEditar.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riEditar_ButtonClick);
            // 
            // colCOD_REF
            // 
            this.colCOD_REF.Caption = "Código";
            this.colCOD_REF.FieldName = "COD_REF";
            this.colCOD_REF.MinWidth = 18;
            this.colCOD_REF.Name = "colCOD_REF";
            this.colCOD_REF.OptionsColumn.AllowEdit = false;
            this.colCOD_REF.Visible = true;
            this.colCOD_REF.VisibleIndex = 1;
            this.colCOD_REF.Width = 110;
            // 
            // colDESCRIPCION
            // 
            this.colDESCRIPCION.Caption = "Descripción";
            this.colDESCRIPCION.FieldName = "DESCRIPCION";
            this.colDESCRIPCION.MinWidth = 18;
            this.colDESCRIPCION.Name = "colDESCRIPCION";
            this.colDESCRIPCION.OptionsColumn.AllowEdit = false;
            this.colDESCRIPCION.Visible = true;
            this.colDESCRIPCION.VisibleIndex = 2;
            this.colDESCRIPCION.Width = 300;
            // 
            // colCATEGORIA
            // 
            this.colCATEGORIA.Caption = "Categoría";
            this.colCATEGORIA.FieldName = "NOMBRE_CATEGORIA";
            this.colCATEGORIA.MinWidth = 18;
            this.colCATEGORIA.Name = "colCATEGORIA";
            this.colCATEGORIA.OptionsColumn.AllowEdit = false;
            this.colCATEGORIA.Visible = true;
            this.colCATEGORIA.VisibleIndex = 3;
            this.colCATEGORIA.Width = 150;
            // 
            // colSUBCATEGORIA
            // 
            this.colSUBCATEGORIA.Caption = "Subcategoría";
            this.colSUBCATEGORIA.FieldName = "NOMBRE_SUBCATEGORIA";
            this.colSUBCATEGORIA.MinWidth = 18;
            this.colSUBCATEGORIA.Name = "colSUBCATEGORIA";
            this.colSUBCATEGORIA.OptionsColumn.AllowEdit = false;
            this.colSUBCATEGORIA.Visible = true;
            this.colSUBCATEGORIA.VisibleIndex = 4;
            this.colSUBCATEGORIA.Width = 150;
            // 
            // colUNIMEDIDA
            // 
            this.colUNIMEDIDA.AppearanceCell.Options.UseTextOptions = true;
            this.colUNIMEDIDA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUNIMEDIDA.Caption = "U/M";
            this.colUNIMEDIDA.FieldName = "UNIMEDIDA";
            this.colUNIMEDIDA.MinWidth = 18;
            this.colUNIMEDIDA.Name = "colUNIMEDIDA";
            this.colUNIMEDIDA.OptionsColumn.AllowEdit = false;
            this.colUNIMEDIDA.Visible = true;
            this.colUNIMEDIDA.VisibleIndex = 5;
            this.colUNIMEDIDA.Width = 55;
            // 
            // colPRECIO
            // 
            this.colPRECIO.AppearanceCell.Options.UseTextOptions = true;
            this.colPRECIO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPRECIO.Caption = "Precio";
            this.colPRECIO.DisplayFormat.FormatString = "N2";
            this.colPRECIO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPRECIO.FieldName = "PRECIO";
            this.colPRECIO.MinWidth = 18;
            this.colPRECIO.Name = "colPRECIO";
            this.colPRECIO.OptionsColumn.AllowEdit = false;
            this.colPRECIO.Visible = true;
            this.colPRECIO.VisibleIndex = 6;
            this.colPRECIO.Width = 90;
            // 
            // colESTADO
            // 
            this.colESTADO.AppearanceCell.Options.UseTextOptions = true;
            this.colESTADO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colESTADO.Caption = "Estado";
            this.colESTADO.FieldName = "ESTADO";
            this.colESTADO.MinWidth = 18;
            this.colESTADO.Name = "colESTADO";
            this.colESTADO.OptionsColumn.AllowEdit = false;
            this.colESTADO.Visible = true;
            this.colESTADO.VisibleIndex = 7;
            this.colESTADO.Width = 70;
            // 
            // frmConsultaProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 550);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frmConsultaProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Consulta";
            this.Text = "Consulta de Productos";
            this.Load += new System.EventHandler(this.frmConsultaProducto_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProductos;
        private DevExpress.XtraGrid.Columns.GridColumn colID_PRODUCTO;
        private DevExpress.XtraGrid.Columns.GridColumn colEDITAR;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riEditar;
        private DevExpress.XtraGrid.Columns.GridColumn colCOD_REF;
        private DevExpress.XtraGrid.Columns.GridColumn colDESCRIPCION;
        private DevExpress.XtraGrid.Columns.GridColumn colCATEGORIA;
        private DevExpress.XtraGrid.Columns.GridColumn colSUBCATEGORIA;
        private DevExpress.XtraGrid.Columns.GridColumn colUNIMEDIDA;
        private DevExpress.XtraGrid.Columns.GridColumn colPRECIO;
        private DevExpress.XtraGrid.Columns.GridColumn colESTADO;
        // Métodos auxiliares referenciados desde el .cs
        private void btnSalir_Click(object sender, System.EventArgs e) => this.Close();
    }
}