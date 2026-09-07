
namespace SistemaContable.UI.Forms.Exportacion
{
    partial class frmContrato : DevExpress.XtraEditors.XtraForm
    {

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // ==================== Panel de acciones generales ====================
        private DevExpress.XtraEditors.GroupControl pnlBotones;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private DevExpress.XtraEditors.SimpleButton btnFinalizar;

        // ==================== Lista principal de contratos ====================
        private DevExpress.XtraEditors.GroupControl grpListaContratos;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;

        // Columnas de acciones por fila (abren ventanas modales)
        private DevExpress.XtraGrid.Columns.GridColumn colEditar;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riEditar;
        private DevExpress.XtraGrid.Columns.GridColumn colFechaEmbarque;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riFechaEmbarque;
        private DevExpress.XtraGrid.Columns.GridColumn colAddendum;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riAddendum;

        // Columnas de datos (según SP_EXP_CONTRATOS_CONSULTAR)
        private DevExpress.XtraGrid.Columns.GridColumn colIdContrato;
        private DevExpress.XtraGrid.Columns.GridColumn colZafra;
        private DevExpress.XtraGrid.Columns.GridColumn colMercado;
        private DevExpress.XtraGrid.Columns.GridColumn colCliente;
        private DevExpress.XtraGrid.Columns.GridColumn colContrato;
        private DevExpress.XtraGrid.Columns.GridColumn colProducto;
        private DevExpress.XtraGrid.Columns.GridColumn colToneladas;
        private DevExpress.XtraGrid.Columns.GridColumn colFecha;
        private DevExpress.XtraGrid.Columns.GridColumn colPDF;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riPDF;
        private DevExpress.XtraGrid.Columns.GridColumn colEstado;

        private void InitializeComponent()
        {
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions5 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject17 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject18 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject19 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject20 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions6 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject21 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject22 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject23 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject24 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions7 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject25 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject26 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject27 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject28 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions8 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject29 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject30 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject31 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject32 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pnlBotones = new DevExpress.XtraEditors.GroupControl();
            this.btnFinalizar = new DevExpress.XtraEditors.SimpleButton();
            this.btnNuevo = new DevExpress.XtraEditors.SimpleButton();
            this.grpListaContratos = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colEditar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riEditar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colFechaEmbarque = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riFechaEmbarque = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colAddendum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riAddendum = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colIdContrato = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colZafra = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMercado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCliente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContrato = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProducto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colToneladas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFecha = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPDF = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riPDF = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colEstado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBotones)).BeginInit();
            this.pnlBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpListaContratos)).BeginInit();
            this.grpListaContratos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riFechaEmbarque)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riAddendum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPDF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBotones
            // 
            this.pnlBotones.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.pnlBotones.Appearance.Options.UseBorderColor = true;
            this.pnlBotones.AppearanceCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.pnlBotones.AppearanceCaption.ForeColor = System.Drawing.Color.White;
            this.pnlBotones.AppearanceCaption.Options.UseBackColor = true;
            this.pnlBotones.AppearanceCaption.Options.UseForeColor = true;
            this.pnlBotones.Controls.Add(this.btnFinalizar);
            this.pnlBotones.Controls.Add(this.btnNuevo);
            this.pnlBotones.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBotones.Location = new System.Drawing.Point(1179, 0);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Size = new System.Drawing.Size(110, 711);
            this.pnlBotones.TabIndex = 1;
            this.pnlBotones.Text = "Acciones";
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.Appearance.Options.UseFont = true;
            this.btnFinalizar.Appearance.Options.UseTextOptions = true;
            this.btnFinalizar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnFinalizar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnFinalizar.Location = new System.Drawing.Point(10, 85);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(90, 44);
            this.btnFinalizar.TabIndex = 1;
            this.btnFinalizar.TabStop = false;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevo.Location = new System.Drawing.Point(10, 35);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(90, 44);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // grpListaContratos
            // 
            this.grpListaContratos.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.grpListaContratos.Appearance.Options.UseBorderColor = true;
            this.grpListaContratos.Controls.Add(this.gridControl1);
            this.grpListaContratos.Controls.Add(this.searchControl1);
            this.grpListaContratos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpListaContratos.Location = new System.Drawing.Point(0, 0);
            this.grpListaContratos.Name = "grpListaContratos";
            this.grpListaContratos.Size = new System.Drawing.Size(1179, 711);
            this.grpListaContratos.TabIndex = 0;
            this.grpListaContratos.Text = "Contratos registrados";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 43);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riEditar,
            this.riFechaEmbarque,
            this.riAddendum,
            this.riPDF});
            this.gridControl1.Size = new System.Drawing.Size(1175, 666);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colEditar,
            this.colFechaEmbarque,
            this.colAddendum,
            this.colIdContrato,
            this.colZafra,
            this.colMercado,
            this.colCliente,
            this.colContrato,
            this.colProducto,
            this.colToneladas,
            this.colFecha,
            this.colPDF,
            this.colEstado});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowHeight = 32;
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            // 
            // colEditar
            // 
            this.colEditar.Caption = "Editar";
            this.colEditar.ColumnEdit = this.riEditar;
            this.colEditar.Name = "colEditar";
            this.colEditar.OptionsColumn.AllowSize = false;
            this.colEditar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colEditar.Visible = true;
            this.colEditar.VisibleIndex = 0;
            this.colEditar.Width = 46;
            // 
            // riEditar
            // 
            this.riEditar.AutoHeight = false;
            editorButtonImageOptions5.ImageUri.Uri = "Edit";
            this.riEditar.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject17, serializableAppearanceObject18, serializableAppearanceObject19, serializableAppearanceObject20, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.riEditar.Name = "riEditar";
            this.riEditar.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // colFechaEmbarque
            // 
            this.colFechaEmbarque.Caption = "Fecha Embarque";
            this.colFechaEmbarque.ColumnEdit = this.riFechaEmbarque;
            this.colFechaEmbarque.Name = "colFechaEmbarque";
            this.colFechaEmbarque.OptionsColumn.AllowSize = false;
            this.colFechaEmbarque.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colFechaEmbarque.Visible = true;
            this.colFechaEmbarque.VisibleIndex = 1;
            this.colFechaEmbarque.Width = 89;
            // 
            // riFechaEmbarque
            // 
            this.riFechaEmbarque.AutoHeight = false;
            editorButtonImageOptions6.ImageUri.Uri = "spreadsheet/longdate";
            this.riFechaEmbarque.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject21, serializableAppearanceObject22, serializableAppearanceObject23, serializableAppearanceObject24, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.riFechaEmbarque.Name = "riFechaEmbarque";
            this.riFechaEmbarque.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // colAddendum
            // 
            this.colAddendum.Caption = "Addendum";
            this.colAddendum.ColumnEdit = this.riAddendum;
            this.colAddendum.Name = "colAddendum";
            this.colAddendum.OptionsColumn.AllowSize = false;
            this.colAddendum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colAddendum.Visible = true;
            this.colAddendum.VisibleIndex = 2;
            this.colAddendum.Width = 63;
            // 
            // riAddendum
            // 
            this.riAddendum.AutoHeight = false;
            editorButtonImageOptions7.ImageUri.Uri = "richedit/addparagraphtotableofcontents";
            this.riAddendum.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions7, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject25, serializableAppearanceObject26, serializableAppearanceObject27, serializableAppearanceObject28, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.riAddendum.Name = "riAddendum";
            this.riAddendum.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // colIdContrato
            // 
            this.colIdContrato.Caption = "Id";
            this.colIdContrato.FieldName = "IDCONTEXP";
            this.colIdContrato.Name = "colIdContrato";
            this.colIdContrato.OptionsColumn.AllowSize = false;
            this.colIdContrato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colIdContrato.Visible = true;
            this.colIdContrato.VisibleIndex = 3;
            this.colIdContrato.Width = 40;
            // 
            // colZafra
            // 
            this.colZafra.Caption = "Zafra";
            this.colZafra.FieldName = "NOM_ZAFRA";
            this.colZafra.Name = "colZafra";
            this.colZafra.Visible = true;
            this.colZafra.VisibleIndex = 4;
            this.colZafra.Width = 77;
            // 
            // colMercado
            // 
            this.colMercado.Caption = "Mercado";
            this.colMercado.FieldName = "NOM_MERCADO";
            this.colMercado.Name = "colMercado";
            this.colMercado.Visible = true;
            this.colMercado.VisibleIndex = 5;
            this.colMercado.Width = 77;
            // 
            // colCliente
            // 
            this.colCliente.Caption = "Cliente";
            this.colCliente.FieldName = "NOM_CLIENTE";
            this.colCliente.Name = "colCliente";
            this.colCliente.Visible = true;
            this.colCliente.VisibleIndex = 6;
            this.colCliente.Width = 189;
            // 
            // colContrato
            // 
            this.colContrato.Caption = "Contrato";
            this.colContrato.FieldName = "NUMERO_CONTRATO";
            this.colContrato.Name = "colContrato";
            this.colContrato.Visible = true;
            this.colContrato.VisibleIndex = 7;
            this.colContrato.Width = 119;
            // 
            // colProducto
            // 
            this.colProducto.Caption = "Producto";
            this.colProducto.FieldName = "NOM_PRODUCTO";
            this.colProducto.Name = "colProducto";
            this.colProducto.Visible = true;
            this.colProducto.VisibleIndex = 8;
            this.colProducto.Width = 110;
            // 
            // colToneladas
            // 
            this.colToneladas.Caption = "Toneladas";
            this.colToneladas.DisplayFormat.FormatString = "{0:N2}";
            this.colToneladas.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colToneladas.FieldName = "TONELADAS";
            this.colToneladas.Name = "colToneladas";
            this.colToneladas.Visible = true;
            this.colToneladas.VisibleIndex = 9;
            this.colToneladas.Width = 86;
            // 
            // colFecha
            // 
            this.colFecha.Caption = "Fecha";
            this.colFecha.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colFecha.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colFecha.FieldName = "FECHA_CONTRATO";
            this.colFecha.Name = "colFecha";
            this.colFecha.Visible = true;
            this.colFecha.VisibleIndex = 10;
            this.colFecha.Width = 77;
            // 
            // colPDF
            // 
            this.colPDF.Caption = "PDF";
            this.colPDF.ColumnEdit = this.riPDF;
            this.colPDF.FieldName = "PDF";
            this.colPDF.Name = "colPDF";
            this.colPDF.OptionsColumn.AllowSize = false;
            this.colPDF.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPDF.Visible = true;
            this.colPDF.VisibleIndex = 11;
            this.colPDF.Width = 77;
            // 
            // riPDF
            // 
            this.riPDF.AutoHeight = false;
            this.riPDF.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Ver PDF", -1, true, true, false, editorButtonImageOptions8, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject29, serializableAppearanceObject30, serializableAppearanceObject31, serializableAppearanceObject32, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.riPDF.Name = "riPDF";
            this.riPDF.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // colEstado
            // 
            this.colEstado.Caption = "Estado";
            this.colEstado.FieldName = "ESTADO";
            this.colEstado.Name = "colEstado";
            this.colEstado.OptionsColumn.AllowSize = false;
            this.colEstado.Visible = true;
            this.colEstado.VisibleIndex = 12;
            this.colEstado.Width = 102;
            // 
            // searchControl1
            // 
            this.searchControl1.Client = this.gridControl1;
            this.searchControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchControl1.Location = new System.Drawing.Point(2, 23);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Properties.Client = this.gridControl1;
            this.searchControl1.Size = new System.Drawing.Size(1175, 20);
            this.searchControl1.TabIndex = 1;
            // 
            // frmContrato
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 711);
            this.Controls.Add(this.grpListaContratos);
            this.Controls.Add(this.pnlBotones);
            this.Name = "frmContrato";
            this.Tag = "Consulta";
            this.Text = "Mantenimiento de Contratos";
            ((System.ComponentModel.ISupportInitialize)(this.pnlBotones)).EndInit();
            this.pnlBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpListaContratos)).EndInit();
            this.grpListaContratos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEditar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riFechaEmbarque)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riAddendum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPDF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
