namespace SistemaContable.UI.Forms.Invalidacion
{
    partial class frmConsultaInvalidacion
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
            this.gvLista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEditar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riEditar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colVerR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riVerR = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.riVerQ = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colFechaAnulacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFechaDoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHoraAnulacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTipoDte = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNombreDte = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCodGen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSello = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumControl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSelloAnulado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUsuario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMotivo = new DevExpress.XtraGrid.Columns.GridColumn();

            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riVerR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riVerQ)).BeginInit();
            this.SuspendLayout();

            //
            // panel1
            //
            this.panel1.Controls.Add(this.btnNuevo);
            this.panel1.Controls.Add(this.btnSalir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1450, 58);
            this.panel1.TabIndex = 0;

            //
            // btnNuevo
            //
            this.btnNuevo.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.Appearance.Options.UseFont = true;
            this.btnNuevo.Appearance.Options.UseTextOptions = true;
            this.btnNuevo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnNuevo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevo.Location = new System.Drawing.Point(24, 11);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(2);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(120, 38);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.TabStop = false;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);

            //
            // btnSalir
            //
            this.btnSalir.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Appearance.Options.UseFont = true;
            this.btnSalir.Appearance.Options.UseTextOptions = true;
            this.btnSalir.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnSalir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnSalir.Location = new System.Drawing.Point(152, 11);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(2);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(120, 38);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.TabStop = false;
            this.btnSalir.Text = "Finalizar";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);

            //
            // gridControl1
            //
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Location = new System.Drawing.Point(0, 58);
            this.gridControl1.MainView = this.gvLista;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
                this.riEditar,
                this.riVerR,
                this.riVerQ });
            this.gridControl1.Size = new System.Drawing.Size(1450, 392);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
                this.gvLista });

            //
            // gvLista
            //
            this.gvLista.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                this.colID,
                this.colEditar,
                this.colVerR,
                this.colFechaAnulacion,
                this.colFechaDoc,
                this.colHoraAnulacion,
                this.colTipoDte,
                this.colNombreDte,
                this.colCodGen,
                this.colSello,
                this.colNumControl,
                this.colSelloAnulado,
                this.colUsuario,
                this.colMotivo });
            this.gvLista.GridControl = this.gridControl1;
            this.gvLista.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvLista.Name = "gvLista";
            this.gvLista.OptionsView.ShowIndicator = false;
            this.gvLista.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvLista.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvLista_RowCellClick);

            //
            // colID  (oculta)
            //
            this.colID.Caption = "Sistema(Id)";
            this.colID.FieldName = "ID_INVALIDACIONJson";
            this.colID.MinWidth = 18;
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.OptionsColumn.AllowSize = false;
            this.colID.Visible = false;
            this.colID.Width = 80;

            //
            // colEditar
            //
            this.colEditar.Caption = "Editar";
            this.colEditar.ColumnEdit = this.riEditar;
            this.colEditar.MinWidth = 18;
            this.colEditar.Name = "colEditar";
            this.colEditar.OptionsColumn.AllowSize = false;
            this.colEditar.OptionsColumn.ShowCaption = false;
            this.colEditar.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colEditar.Visible = true;
            this.colEditar.VisibleIndex = 0;
            this.colEditar.Width = 41;

            //
            // riEditar
            //
            this.riEditar.AutoHeight = false;
            editorButtonImageOptions1.Image = global::SistemaContable.UI.Properties.Resources.editar3_32x32;
            this.riEditar.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(
                    DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false,
                    editorButtonImageOptions1,
                    new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None),
                    serializableAppearanceObject1, serializableAppearanceObject2,
                    serializableAppearanceObject3, serializableAppearanceObject4,
                    "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            this.riEditar.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.riEditar.Name = "riEditar";
            this.riEditar.UseReadOnlyAppearance = false;

            //
            // colVerR
            //
            this.colVerR.Caption = "Ver";
            this.colVerR.ColumnEdit = this.riVerR;
            this.colVerR.MinWidth = 18;
            this.colVerR.Name = "colVerR";
            this.colVerR.OptionsColumn.AllowSize = false;
            this.colVerR.OptionsColumn.ShowCaption = false;
            this.colVerR.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colVerR.Visible = true;
            this.colVerR.VisibleIndex = 1;
            this.colVerR.Width = 41;

            //
            // riVerR  (igual que frmConsultaFactura — declaración inline)
            //
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.riVerR.AutoHeight = false;
            editorButtonImageOptions2.Image = global::SistemaContable.UI.Properties.Resources.quedan2_32x32;
            this.riVerR.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(
                    DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false,
                    editorButtonImageOptions2,
                    new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None),
                    serializableAppearanceObject5, serializableAppearanceObject6,
                    serializableAppearanceObject7, serializableAppearanceObject8,
                    "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            this.riVerR.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.riVerR.Name = "riVerR";
            this.riVerR.UseReadOnlyAppearance = false;

            //
            // riVerQ
            //
            this.riVerQ.AutoHeight = false;
            this.riVerQ.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.riVerQ.Name = "riVerQ";
            this.riVerQ.UseReadOnlyAppearance = false;

            //
            // colFechaAnulacion
            //
            this.colFechaAnulacion.Caption = "Fecha Anulacion";
            this.colFechaAnulacion.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colFechaAnulacion.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colFechaAnulacion.FieldName = "fhProcesamiento_anulado";
            this.colFechaAnulacion.MinWidth = 18;
            this.colFechaAnulacion.Name = "colFechaAnulacion";
            this.colFechaAnulacion.OptionsColumn.AllowEdit = false;
            this.colFechaAnulacion.OptionsColumn.AllowSize = false;
            this.colFechaAnulacion.Visible = true;
            this.colFechaAnulacion.VisibleIndex = 2;
            this.colFechaAnulacion.Width = 120;

            //
            // colFechaDoc
            //
            this.colFechaDoc.Caption = "Fecha Documento";
            this.colFechaDoc.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colFechaDoc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colFechaDoc.FieldName = "IDENTIFICACION_fecAnula";
            this.colFechaDoc.MinWidth = 18;
            this.colFechaDoc.Name = "colFechaDoc";
            this.colFechaDoc.OptionsColumn.AllowEdit = false;
            this.colFechaDoc.OptionsColumn.AllowSize = false;
            this.colFechaDoc.Visible = true;
            this.colFechaDoc.VisibleIndex = 3;
            this.colFechaDoc.Width = 120;

            //
            // colHoraAnulacion
            //
            this.colHoraAnulacion.Caption = "Hora Anulacion";
            this.colHoraAnulacion.DisplayFormat.FormatString = "hh:mm:ss tt";
            this.colHoraAnulacion.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colHoraAnulacion.FieldName = "IDENTIFICACION_horAnula";
            this.colHoraAnulacion.MinWidth = 18;
            this.colHoraAnulacion.Name = "colHoraAnulacion";
            this.colHoraAnulacion.OptionsColumn.AllowEdit = false;
            this.colHoraAnulacion.OptionsColumn.AllowSize = false;
            this.colHoraAnulacion.Visible = true;
            this.colHoraAnulacion.VisibleIndex = 4;
            this.colHoraAnulacion.Width = 100;

            //
            // colTipoDte
            //
            this.colTipoDte.AppearanceCell.Options.UseTextOptions = true;
            this.colTipoDte.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTipoDte.Caption = "Tipo DTE";
            this.colTipoDte.FieldName = "DOCUMENTO_tipoDte";
            this.colTipoDte.MinWidth = 18;
            this.colTipoDte.Name = "colTipoDte";
            this.colTipoDte.OptionsColumn.AllowEdit = false;
            this.colTipoDte.OptionsColumn.AllowSize = false;
            this.colTipoDte.Visible = true;
            this.colTipoDte.VisibleIndex = 5;
            this.colTipoDte.Width = 80;

            //
            // colNombreDte
            //
            this.colNombreDte.Caption = "DTE";
            this.colNombreDte.FieldName = "DOCUMENTO_tipoDte_NOMBRE";
            this.colNombreDte.MinWidth = 18;
            this.colNombreDte.Name = "colNombreDte";
            this.colNombreDte.OptionsColumn.AllowEdit = false;
            this.colNombreDte.OptionsColumn.AllowSize = false;
            this.colNombreDte.Visible = true;
            this.colNombreDte.VisibleIndex = 6;
            this.colNombreDte.Width = 160;

            //
            // colCodGen
            //
            this.colCodGen.Caption = "Cód. Generación";
            this.colCodGen.FieldName = "DOCUMENTO_codigoGeneracion";
            this.colCodGen.MinWidth = 18;
            this.colCodGen.Name = "colCodGen";
            this.colCodGen.OptionsColumn.AllowEdit = false;
            this.colCodGen.OptionsColumn.AllowSize = false;
            this.colCodGen.Visible = true;
            this.colCodGen.VisibleIndex = 7;
            this.colCodGen.Width = 300;

            //
            // colSello
            //
            this.colSello.Caption = "Sello Recibido";
            this.colSello.FieldName = "DOCUMENTO_selloRecibido";
            this.colSello.MinWidth = 18;
            this.colSello.Name = "colSello";
            this.colSello.OptionsColumn.AllowEdit = false;
            this.colSello.OptionsColumn.AllowSize = false;
            this.colSello.Visible = true;
            this.colSello.VisibleIndex = 8;
            this.colSello.Width = 300;

            //
            // colNumControl
            //
            this.colNumControl.Caption = "N° Control";
            this.colNumControl.FieldName = "DOCUMENTO_numeroControl";
            this.colNumControl.MinWidth = 18;
            this.colNumControl.Name = "colNumControl";
            this.colNumControl.OptionsColumn.AllowEdit = false;
            this.colNumControl.OptionsColumn.AllowSize = false;
            this.colNumControl.Visible = true;
            this.colNumControl.VisibleIndex = 9;
            this.colNumControl.Width = 220;

            //
            // colSelloAnulado
            //
            this.colSelloAnulado.Caption = "Sello Anulado";
            this.colSelloAnulado.FieldName = "selloRecibido_anulado";
            this.colSelloAnulado.MinWidth = 18;
            this.colSelloAnulado.Name = "colSelloAnulado";
            this.colSelloAnulado.OptionsColumn.AllowEdit = false;
            this.colSelloAnulado.OptionsColumn.AllowSize = false;
            this.colSelloAnulado.Visible = true;
            this.colSelloAnulado.VisibleIndex = 10;
            this.colSelloAnulado.Width = 300;

            //
            // colUsuario
            //
            this.colUsuario.Caption = "Usuario";
            this.colUsuario.FieldName = "USUARIO_CREA";
            this.colUsuario.MinWidth = 18;
            this.colUsuario.Name = "colUsuario";
            this.colUsuario.OptionsColumn.AllowEdit = false;
            this.colUsuario.OptionsColumn.AllowSize = false;
            this.colUsuario.Visible = true;
            this.colUsuario.VisibleIndex = 11;
            this.colUsuario.Width = 100;

            //
            // colMotivo
            //
            this.colMotivo.Caption = "Motivo";
            this.colMotivo.FieldName = "MOTIVO_CONTROL_INTERNO";
            this.colMotivo.MinWidth = 18;
            this.colMotivo.Name = "colMotivo";
            this.colMotivo.OptionsColumn.AllowEdit = false;
            this.colMotivo.OptionsColumn.AllowSize = false;
            this.colMotivo.Visible = true;
            this.colMotivo.VisibleIndex = 12;
            this.colMotivo.Width = 300;

            //
            // frmConsultaInvalidacion
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1450, 450);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConsultaInvalidacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta de Invalidaciones";
            this.Load += new System.EventHandler(this.frmConsultaInvalidacion_Load);

            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riVerR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riVerQ)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLista;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colEditar;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riEditar;
        private DevExpress.XtraGrid.Columns.GridColumn colVerR;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riVerR;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riVerQ;
        private DevExpress.XtraGrid.Columns.GridColumn colFechaAnulacion;
        private DevExpress.XtraGrid.Columns.GridColumn colFechaDoc;
        private DevExpress.XtraGrid.Columns.GridColumn colHoraAnulacion;
        private DevExpress.XtraGrid.Columns.GridColumn colTipoDte;
        private DevExpress.XtraGrid.Columns.GridColumn colNombreDte;
        private DevExpress.XtraGrid.Columns.GridColumn colCodGen;
        private DevExpress.XtraGrid.Columns.GridColumn colSello;
        private DevExpress.XtraGrid.Columns.GridColumn colNumControl;
        private DevExpress.XtraGrid.Columns.GridColumn colSelloAnulado;
        private DevExpress.XtraGrid.Columns.GridColumn colUsuario;
        private DevExpress.XtraGrid.Columns.GridColumn colMotivo;
    }
}