
namespace SistemaContable.UI.Forms.NotaRemision
{
    partial class frmConsulta
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gvDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID_NR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDITAR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riEditar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colVER_Q = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riVerQ = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colFECHA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNUMDOC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOMCLIENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCODGENERACION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNUMCONTROL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSELLORECEPCION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colESTADO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnNuevoNR = new DevExpress.XtraEditors.SimpleButton();
            this.btnFinalizar = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riVerQ)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gridControl1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1218, 399);
            this.panel1.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Location = new System.Drawing.Point(0, 58);
            this.gridControl1.MainView = this.gvDetalle;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riEditar,
            this.riVerQ});
            this.gridControl1.Size = new System.Drawing.Size(1218, 341);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetalle});
            // 
            // gvDetalle
            // 
            this.gvDetalle.Appearance.DetailTip.Options.UseTextOptions = true;
            this.gvDetalle.Appearance.DetailTip.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvDetalle.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID_NR,
            this.colEDITAR,
            this.colVER_Q,
            this.colFECHA,
            this.colNUMDOC,
            this.colNOMCLIENTE,
            this.colCODGENERACION,
            this.colNUMCONTROL,
            this.colSELLORECEPCION,
            this.colESTADO});
            this.gvDetalle.DetailHeight = 284;
            this.gvDetalle.GridControl = this.gridControl1;
            this.gvDetalle.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvDetalle.Name = "gvDetalle";
            this.gvDetalle.OptionsView.ShowIndicator = false;
            this.gvDetalle.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvDetalle.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvDetalle_RowCellClick);
            // 
            // colID_NR
            // 
            this.colID_NR.Caption = "Sistema(Id)";
            this.colID_NR.FieldName = "ID_NTREMISIONENC";
            this.colID_NR.MinWidth = 18;
            this.colID_NR.Name = "colID_NR";
            this.colID_NR.OptionsColumn.AllowSize = false;
            this.colID_NR.Visible = true;
            this.colID_NR.VisibleIndex = 0;
            this.colID_NR.Width = 87;
            // 
            // colEDITAR
            // 
            this.colEDITAR.Caption = "Editar";
            this.colEDITAR.ColumnEdit = this.riEditar;
            this.colEDITAR.MinWidth = 18;
            this.colEDITAR.Name = "colEDITAR";
            this.colEDITAR.OptionsColumn.AllowSize = false;
            this.colEDITAR.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colEDITAR.Visible = true;
            this.colEDITAR.VisibleIndex = 1;
            this.colEDITAR.Width = 54;
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
            // 
            // colVER_Q
            // 
            this.colVER_Q.Caption = "Ver ";
            this.colVER_Q.ColumnEdit = this.riVerQ;
            this.colVER_Q.MinWidth = 18;
            this.colVER_Q.Name = "colVER_Q";
            this.colVER_Q.OptionsColumn.AllowSize = false;
            this.colVER_Q.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colVER_Q.Visible = true;
            this.colVER_Q.VisibleIndex = 2;
            this.colVER_Q.Width = 54;
            // 
            // riVerQ
            // 
            this.riVerQ.AutoHeight = false;
            editorButtonImageOptions2.Image = global::SistemaContable.UI.Properties.Resources.quedan2_32x32;
            this.riVerQ.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.riVerQ.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.riVerQ.Name = "riVerQ";
            this.riVerQ.UseReadOnlyAppearance = false;
            // 
            // colFECHA
            // 
            this.colFECHA.Caption = "Fecha";
            this.colFECHA.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colFECHA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colFECHA.FieldName = "FECHA";
            this.colFECHA.Name = "colFECHA";
            this.colFECHA.OptionsColumn.AllowSize = false;
            this.colFECHA.Visible = true;
            this.colFECHA.VisibleIndex = 3;
            this.colFECHA.Width = 81;
            // 
            // colNUMDOC
            // 
            this.colNUMDOC.Caption = "Numero";
            this.colNUMDOC.FieldName = "NUMDOC";
            this.colNUMDOC.Name = "colNUMDOC";
            this.colNUMDOC.OptionsColumn.AllowSize = false;
            this.colNUMDOC.Visible = true;
            this.colNUMDOC.VisibleIndex = 4;
            this.colNUMDOC.Width = 85;
            // 
            // colNOMCLIENTE
            // 
            this.colNOMCLIENTE.Caption = "Cliente";
            this.colNOMCLIENTE.FieldName = "NOMCLIENTE";
            this.colNOMCLIENTE.Name = "colNOMCLIENTE";
            this.colNOMCLIENTE.OptionsColumn.AllowSize = false;
            this.colNOMCLIENTE.Visible = true;
            this.colNOMCLIENTE.VisibleIndex = 5;
            this.colNOMCLIENTE.Width = 204;
            // 
            // colCODGENERACION
            // 
            this.colCODGENERACION.Caption = "Cod. Generacion";
            this.colCODGENERACION.FieldName = "CODGENERACION";
            this.colCODGENERACION.Name = "colCODGENERACION";
            this.colCODGENERACION.OptionsColumn.AllowSize = false;
            this.colCODGENERACION.Visible = true;
            this.colCODGENERACION.VisibleIndex = 6;
            this.colCODGENERACION.Width = 204;
            // 
            // colNUMCONTROL
            // 
            this.colNUMCONTROL.Caption = "N° Control";
            this.colNUMCONTROL.FieldName = "NUMCONTROL";
            this.colNUMCONTROL.Name = "colNUMCONTROL";
            this.colNUMCONTROL.OptionsColumn.AllowSize = false;
            this.colNUMCONTROL.Visible = true;
            this.colNUMCONTROL.VisibleIndex = 7;
            this.colNUMCONTROL.Width = 204;
            // 
            // colSELLORECEPCION
            // 
            this.colSELLORECEPCION.Caption = "Sello Recepcion";
            this.colSELLORECEPCION.FieldName = "SELLORECEPCION";
            this.colSELLORECEPCION.Name = "colSELLORECEPCION";
            this.colSELLORECEPCION.OptionsColumn.AllowSize = false;
            this.colSELLORECEPCION.Visible = true;
            this.colSELLORECEPCION.VisibleIndex = 8;
            this.colSELLORECEPCION.Width = 157;
            // 
            // colESTADO
            // 
            this.colESTADO.Caption = "Estado";
            this.colESTADO.FieldName = "ESTADO";
            this.colESTADO.Name = "colESTADO";
            this.colESTADO.OptionsColumn.AllowSize = false;
            this.colESTADO.Visible = true;
            this.colESTADO.VisibleIndex = 9;
            this.colESTADO.Width = 69;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnNuevoNR);
            this.panel2.Controls.Add(this.btnFinalizar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1218, 58);
            this.panel2.TabIndex = 4;
            // 
            // btnNuevoNR
            // 
            this.btnNuevoNR.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoNR.Appearance.Options.UseFont = true;
            this.btnNuevoNR.Appearance.Options.UseTextOptions = true;
            this.btnNuevoNR.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnNuevoNR.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevoNR.Location = new System.Drawing.Point(24, 11);
            this.btnNuevoNR.Margin = new System.Windows.Forms.Padding(2);
            this.btnNuevoNR.Name = "btnNuevoNR";
            this.btnNuevoNR.Size = new System.Drawing.Size(120, 38);
            this.btnNuevoNR.TabIndex = 55;
            this.btnNuevoNR.TabStop = false;
            this.btnNuevoNR.Text = "Nuevo";
            this.btnNuevoNR.Click += new System.EventHandler(this.btnNuevoNR_Click);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.Appearance.Options.UseFont = true;
            this.btnFinalizar.Appearance.Options.UseTextOptions = true;
            this.btnFinalizar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnFinalizar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnFinalizar.Location = new System.Drawing.Point(148, 11);
            this.btnFinalizar.Margin = new System.Windows.Forms.Padding(2);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(120, 38);
            this.btnFinalizar.TabIndex = 54;
            this.btnFinalizar.TabStop = false;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // frmConsulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1218, 399);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConsulta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Nota Reminision";
            this.Load += new System.EventHandler(this.frmConsulta_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riVerQ)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnFinalizar;
        private DevExpress.XtraEditors.SimpleButton btnNuevoNR;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetalle;
        private DevExpress.XtraGrid.Columns.GridColumn colID_NR;
        private DevExpress.XtraGrid.Columns.GridColumn colEDITAR;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riEditar;
        private DevExpress.XtraGrid.Columns.GridColumn colVER_Q;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riVerQ;
        private DevExpress.XtraGrid.Columns.GridColumn colFECHA;
        private DevExpress.XtraGrid.Columns.GridColumn colNUMDOC;
        private DevExpress.XtraGrid.Columns.GridColumn colNOMCLIENTE;
        private DevExpress.XtraGrid.Columns.GridColumn colCODGENERACION;
        private DevExpress.XtraGrid.Columns.GridColumn colNUMCONTROL;
        private DevExpress.XtraGrid.Columns.GridColumn colSELLORECEPCION;
        private DevExpress.XtraGrid.Columns.GridColumn colESTADO;
    }
}