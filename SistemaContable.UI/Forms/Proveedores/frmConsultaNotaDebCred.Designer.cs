
namespace SistemaContable.UI.Forms.Proveedores
{
    partial class frmConsultaNotaDebCred
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
            this.colFECHA_RECIBIDO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCOD_GENERACION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTIPO_DTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOMBRE_ENTIDAD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riEditar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colEDITAR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colQUEDAN_APLICADO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTIPO_APLICADO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCODGENERACION_APLICADO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSALDO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFECHA_EMISION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnNuevoQuedan = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFinalizar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.colFECHA_RECIBIDO.VisibleIndex = 9;
            this.colFECHA_RECIBIDO.Width = 87;
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
            this.colCOD_GENERACION.VisibleIndex = 3;
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
            this.colTIPO_DTE.VisibleIndex = 2;
            this.colTIPO_DTE.Width = 79;
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
            this.colNOMBRE_ENTIDAD.VisibleIndex = 1;
            this.colNOMBRE_ENTIDAD.Width = 289;
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
            // colEDITAR
            // 
            this.colEDITAR.ColumnEdit = this.riEditar;
            this.colEDITAR.MinWidth = 21;
            this.colEDITAR.Name = "colEDITAR";
            this.colEDITAR.OptionsColumn.FixedWidth = true;
            this.colEDITAR.OptionsColumn.ShowCaption = false;
            this.colEDITAR.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colEDITAR.Visible = true;
            this.colEDITAR.VisibleIndex = 0;
            this.colEDITAR.Width = 35;
            // 
            // gvDetalle
            // 
            this.gvDetalle.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colEDITAR,
            this.colNOMBRE_ENTIDAD,
            this.colTIPO_DTE,
            this.colCOD_GENERACION,
            this.colQUEDAN_APLICADO,
            this.colTIPO_APLICADO,
            this.colCODGENERACION_APLICADO,
            this.colSALDO,
            this.colFECHA_EMISION,
            this.colFECHA_RECIBIDO});
            this.gvDetalle.GridControl = this.gridControl1;
            this.gvDetalle.Name = "gvDetalle";
            this.gvDetalle.OptionsView.ShowIndicator = false;
            // 
            // colQUEDAN_APLICADO
            // 
            this.colQUEDAN_APLICADO.AppearanceCell.Options.UseTextOptions = true;
            this.colQUEDAN_APLICADO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQUEDAN_APLICADO.Caption = "No. Quedan Aplicado";
            this.colQUEDAN_APLICADO.FieldName = "NUM_QUEDAN";
            this.colQUEDAN_APLICADO.MinWidth = 21;
            this.colQUEDAN_APLICADO.Name = "colQUEDAN_APLICADO";
            this.colQUEDAN_APLICADO.OptionsColumn.FixedWidth = true;
            this.colQUEDAN_APLICADO.Visible = true;
            this.colQUEDAN_APLICADO.VisibleIndex = 4;
            this.colQUEDAN_APLICADO.Width = 114;
            // 
            // colTIPO_APLICADO
            // 
            this.colTIPO_APLICADO.Caption = "Tipo Comp. Aplicado";
            this.colTIPO_APLICADO.FieldName = "TIPO_DTE_APLICADO";
            this.colTIPO_APLICADO.MinWidth = 21;
            this.colTIPO_APLICADO.Name = "colTIPO_APLICADO";
            this.colTIPO_APLICADO.OptionsColumn.FixedWidth = true;
            this.colTIPO_APLICADO.Visible = true;
            this.colTIPO_APLICADO.VisibleIndex = 5;
            this.colTIPO_APLICADO.Width = 108;
            // 
            // colCODGENERACION_APLICADO
            // 
            this.colCODGENERACION_APLICADO.Caption = "N°/Cod. Generación Aplicado";
            this.colCODGENERACION_APLICADO.FieldName = "COD_GENERACION_APLICADO";
            this.colCODGENERACION_APLICADO.MinWidth = 21;
            this.colCODGENERACION_APLICADO.Name = "colCODGENERACION_APLICADO";
            this.colCODGENERACION_APLICADO.OptionsColumn.FixedWidth = true;
            this.colCODGENERACION_APLICADO.Visible = true;
            this.colCODGENERACION_APLICADO.VisibleIndex = 6;
            this.colCODGENERACION_APLICADO.Width = 276;
            // 
            // colSALDO
            // 
            this.colSALDO.Caption = "Saldo";
            this.colSALDO.DisplayFormat.FormatString = "c2";
            this.colSALDO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSALDO.FieldName = "SALDO";
            this.colSALDO.MinWidth = 21;
            this.colSALDO.Name = "colSALDO";
            this.colSALDO.OptionsColumn.FixedWidth = true;
            this.colSALDO.Visible = true;
            this.colSALDO.VisibleIndex = 7;
            this.colSALDO.Width = 105;
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
            this.colFECHA_EMISION.VisibleIndex = 8;
            this.colFECHA_EMISION.Width = 87;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gvDetalle;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riEditar});
            this.gridControl1.Size = new System.Drawing.Size(1070, 603);
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
            this.panel2.Size = new System.Drawing.Size(1070, 603);
            this.panel2.TabIndex = 4;
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
            this.btnNuevoQuedan.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnNuevoQuedan.ToolTipTitle = "Operación";
            this.btnNuevoQuedan.Click += new System.EventHandler(this.btnNuevoQuedan_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFinalizar);
            this.panel1.Controls.Add(this.btnNuevoQuedan);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1070, 46);
            this.panel1.TabIndex = 3;
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
            this.btnFinalizar.Size = new System.Drawing.Size(120, 38);
            this.btnFinalizar.TabIndex = 2;
            this.btnFinalizar.TabStop = false;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnFinalizar.ToolTipTitle = "Operación";
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // frmConsultaNotaDebCred
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 649);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConsultaNotaDebCred";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "CONSULTA";
            this.Text = "Consulta Nota Débito/Crédito";
            this.Load += new System.EventHandler(this.frmConsultaNotaDebCred_Load);
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn colFECHA_RECIBIDO;
        private DevExpress.XtraGrid.Columns.GridColumn colCOD_GENERACION;
        private DevExpress.XtraGrid.Columns.GridColumn colTIPO_DTE;
        private DevExpress.XtraGrid.Columns.GridColumn colNOMBRE_ENTIDAD;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riEditar;
        private DevExpress.XtraGrid.Columns.GridColumn colEDITAR;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetalle;
        private DevExpress.XtraGrid.Columns.GridColumn colFECHA_EMISION;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnNuevoQuedan;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.Columns.GridColumn colQUEDAN_APLICADO;
        private DevExpress.XtraGrid.Columns.GridColumn colTIPO_APLICADO;
        private DevExpress.XtraGrid.Columns.GridColumn colCODGENERACION_APLICADO;
        private DevExpress.XtraGrid.Columns.GridColumn colSALDO;
        private DevExpress.XtraEditors.SimpleButton btnFinalizar;
    }
}