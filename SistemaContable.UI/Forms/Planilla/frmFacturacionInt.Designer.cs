namespace SistemaContable.UI.Forms.Planilla
{
    partial class frmFacturacionInt
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // NOTA: Designer redactado a mano como punto de partida (no generado por el diseñador visual
        // de Visual Studio), pero siguiendo la misma estructura que usan las pantallas reales del
        // sistema (frmFactura, frmConsultaFactura): GroupBox con título para agrupar los campos de
        // filtro, panel aparte solo con botones de acción, TabIndex por control, columnas de grid con
        // MinWidth/AllowSize/AllowEdit. Ábrelo en el diseñador con DevExpress instalado para ajustar
        // anclas/tamaños/skin si hace falta; los nombres de los controles ya coinciden con
        // frmFacturacionInt.cs.

        private void InitializeComponent()
        {
            this.grpFiltros = new System.Windows.Forms.GroupBox();
            this.chkNoSellados = new System.Windows.Forms.CheckBox();
            this.cbxTIPO_PLANILLA = new System.Windows.Forms.ComboBox();
            this.lblTipoPlanilla = new System.Windows.Forms.Label();
            this.cbxCATORCENA = new System.Windows.Forms.ComboBox();
            this.lblCatorcena = new System.Windows.Forms.Label();
            this.cbxZAFRA = new System.Windows.Forms.ComboBox();
            this.lblZafra = new System.Windows.Forms.Label();
            this.cbxTIPO_COMPROBANTE = new System.Windows.Forms.ComboBox();
            this.lblTipoComprobante = new System.Windows.Forms.Label();

            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
            this.btnReporte = new DevExpress.XtraEditors.SimpleButton();
            this.btnImportar = new DevExpress.XtraEditors.SimpleButton();
            this.btnConsultar = new DevExpress.XtraEditors.SimpleButton();

            this.panelCandidatos = new System.Windows.Forms.Panel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gvCandidatos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID_COMPROB_ENCA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNUM_DOCTO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSERIE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCODIGO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOM_PROVEEDOR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFECHA_C = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colITEMS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSUMAS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIVA_C = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRENTA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSUBTOTAL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIVA_RETENIDO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTOTAL_C = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAPLICA = new DevExpress.XtraGrid.Columns.GridColumn();

            this.panelNuevo = new System.Windows.Forms.Panel();
            this.btnNuevo = new DevExpress.XtraEditors.SimpleButton();

            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gvDocumentos = new DevExpress.XtraGrid.Views.Grid.GridView();

            this.grpFiltros.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.panelCandidatos.SuspendLayout();
            this.panelNuevo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCandidatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDocumentos)).BeginInit();
            this.SuspendLayout();
            //
            // grpFiltros
            //
            this.grpFiltros.Controls.Add(this.chkNoSellados);
            this.grpFiltros.Controls.Add(this.cbxTIPO_PLANILLA);
            this.grpFiltros.Controls.Add(this.lblTipoPlanilla);
            this.grpFiltros.Controls.Add(this.cbxCATORCENA);
            this.grpFiltros.Controls.Add(this.lblCatorcena);
            this.grpFiltros.Controls.Add(this.cbxZAFRA);
            this.grpFiltros.Controls.Add(this.lblZafra);
            this.grpFiltros.Controls.Add(this.cbxTIPO_COMPROBANTE);
            this.grpFiltros.Controls.Add(this.lblTipoComprobante);
            this.grpFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFiltros.Location = new System.Drawing.Point(0, 0);
            this.grpFiltros.Name = "grpFiltros";
            this.grpFiltros.Size = new System.Drawing.Size(1120, 170);
            this.grpFiltros.TabIndex = 0;
            this.grpFiltros.TabStop = false;
            this.grpFiltros.Text = "Filtros";
            //
            // lblTipoComprobante
            //
            this.lblTipoComprobante.AutoSize = true;
            this.lblTipoComprobante.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoComprobante.Location = new System.Drawing.Point(16, 30);
            this.lblTipoComprobante.Name = "lblTipoComprobante";
            this.lblTipoComprobante.Size = new System.Drawing.Size(107, 14);
            this.lblTipoComprobante.TabIndex = 0;
            this.lblTipoComprobante.Text = "Tipo Comprobante:";
            //
            // cbxTIPO_COMPROBANTE
            //
            this.cbxTIPO_COMPROBANTE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTIPO_COMPROBANTE.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTIPO_COMPROBANTE.FormattingEnabled = true;
            this.cbxTIPO_COMPROBANTE.Location = new System.Drawing.Point(150, 27);
            this.cbxTIPO_COMPROBANTE.Name = "cbxTIPO_COMPROBANTE";
            this.cbxTIPO_COMPROBANTE.Size = new System.Drawing.Size(300, 22);
            this.cbxTIPO_COMPROBANTE.TabIndex = 1;
            //
            // lblZafra
            //
            this.lblZafra.AutoSize = true;
            this.lblZafra.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZafra.Location = new System.Drawing.Point(16, 62);
            this.lblZafra.Name = "lblZafra";
            this.lblZafra.Size = new System.Drawing.Size(35, 14);
            this.lblZafra.TabIndex = 2;
            this.lblZafra.Text = "Zafra:";
            //
            // cbxZAFRA
            //
            this.cbxZAFRA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxZAFRA.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxZAFRA.FormattingEnabled = true;
            this.cbxZAFRA.Location = new System.Drawing.Point(150, 59);
            this.cbxZAFRA.Name = "cbxZAFRA";
            this.cbxZAFRA.Size = new System.Drawing.Size(160, 22);
            this.cbxZAFRA.TabIndex = 3;
            this.cbxZAFRA.SelectedIndexChanged += new System.EventHandler(this.cbxZAFRA_SelectedIndexChanged);
            //
            // lblCatorcena
            //
            this.lblCatorcena.AutoSize = true;
            this.lblCatorcena.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCatorcena.Location = new System.Drawing.Point(330, 62);
            this.lblCatorcena.Name = "lblCatorcena";
            this.lblCatorcena.Size = new System.Drawing.Size(62, 14);
            this.lblCatorcena.TabIndex = 4;
            this.lblCatorcena.Text = "Catorcena:";
            //
            // cbxCATORCENA
            //
            this.cbxCATORCENA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCATORCENA.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCATORCENA.FormattingEnabled = true;
            this.cbxCATORCENA.Location = new System.Drawing.Point(420, 59);
            this.cbxCATORCENA.Name = "cbxCATORCENA";
            this.cbxCATORCENA.Size = new System.Drawing.Size(140, 22);
            this.cbxCATORCENA.TabIndex = 5;
            //
            // lblTipoPlanilla
            //
            this.lblTipoPlanilla.AutoSize = true;
            this.lblTipoPlanilla.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoPlanilla.Location = new System.Drawing.Point(16, 94);
            this.lblTipoPlanilla.Name = "lblTipoPlanilla";
            this.lblTipoPlanilla.Size = new System.Drawing.Size(76, 14);
            this.lblTipoPlanilla.TabIndex = 6;
            this.lblTipoPlanilla.Text = "Tipo Planilla:";
            //
            // cbxTIPO_PLANILLA
            //
            this.cbxTIPO_PLANILLA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTIPO_PLANILLA.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTIPO_PLANILLA.FormattingEnabled = true;
            this.cbxTIPO_PLANILLA.Location = new System.Drawing.Point(150, 91);
            this.cbxTIPO_PLANILLA.Name = "cbxTIPO_PLANILLA";
            this.cbxTIPO_PLANILLA.Size = new System.Drawing.Size(300, 22);
            this.cbxTIPO_PLANILLA.TabIndex = 7;
            //
            // chkNoSellados
            //
            this.chkNoSellados.AutoSize = true;
            this.chkNoSellados.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNoSellados.Location = new System.Drawing.Point(470, 94);
            this.chkNoSellados.Name = "chkNoSellados";
            this.chkNoSellados.Size = new System.Drawing.Size(88, 18);
            this.chkNoSellados.TabIndex = 8;
            this.chkNoSellados.Text = "No Sellados";
            //
            // panelBotones — solo botones de acción (igual que panel1 en frmConsultaFactura),
            // en una sola fila debajo del grupo de filtros, igual que la pantalla web.
            //
            this.panelBotones.Controls.Add(this.btnSalir);
            this.panelBotones.Controls.Add(this.btnReporte);
            this.panelBotones.Controls.Add(this.btnImportar);
            this.panelBotones.Controls.Add(this.btnConsultar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBotones.Location = new System.Drawing.Point(0, 170);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(1120, 54);
            this.panelBotones.TabIndex = 1;
            //
            // btnConsultar
            //
            this.btnConsultar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultar.Appearance.Options.UseFont = true;
            this.btnConsultar.Appearance.Options.UseTextOptions = true;
            this.btnConsultar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnConsultar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.BuscarProducto32x32;
            this.btnConsultar.Location = new System.Drawing.Point(16, 10);
            this.btnConsultar.Margin = new System.Windows.Forms.Padding(2);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(100, 34);
            this.btnConsultar.TabIndex = 0;
            this.btnConsultar.TabStop = false;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            //
            // btnImportar
            //
            this.btnImportar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportar.Appearance.Options.UseFont = true;
            this.btnImportar.Appearance.Options.UseTextOptions = true;
            this.btnImportar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            // TODO: sin ícono dedicado en Resources para "Importar"; ajustar si existe uno mejor.
            this.btnImportar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.procesar32x32;
            this.btnImportar.Location = new System.Drawing.Point(126, 10);
            this.btnImportar.Margin = new System.Windows.Forms.Padding(2);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(100, 34);
            this.btnImportar.TabIndex = 1;
            this.btnImportar.TabStop = false;
            this.btnImportar.Text = "Importar";
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            //
            // btnReporte
            //
            this.btnReporte.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporte.Appearance.Options.UseFont = true;
            this.btnReporte.Appearance.Options.UseTextOptions = true;
            this.btnReporte.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnReporte.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.reportes_48x48;
            this.btnReporte.Location = new System.Drawing.Point(236, 10);
            this.btnReporte.Margin = new System.Windows.Forms.Padding(2);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(100, 34);
            this.btnReporte.TabIndex = 2;
            this.btnReporte.TabStop = false;
            this.btnReporte.Text = "Reporte";
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            //
            // btnSalir
            //
            this.btnSalir.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Appearance.Options.UseFont = true;
            this.btnSalir.Appearance.Options.UseTextOptions = true;
            this.btnSalir.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnSalir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnSalir.Location = new System.Drawing.Point(346, 10);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(2);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(100, 34);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.TabStop = false;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            //
            // panelCandidatos — grid superior (candidatos, SP_COMPROB_PLANILLA)
            //
            this.panelCandidatos.Controls.Add(this.gridControl1);
            this.panelCandidatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCandidatos.Location = new System.Drawing.Point(0, 224);
            this.panelCandidatos.Name = "panelCandidatos";
            this.panelCandidatos.Size = new System.Drawing.Size(1120, 220);
            this.panelCandidatos.TabIndex = 2;
            //
            // gridControl1
            //
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gvCandidatos;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1120, 220);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCandidatos});
            //
            // gvCandidatos
            //
            this.gvCandidatos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID_COMPROB_ENCA,
            this.colNUM_DOCTO,
            this.colSERIE,
            this.colCODIGO,
            this.colNOM_PROVEEDOR,
            this.colFECHA_C,
            this.colITEMS,
            this.colSUMAS,
            this.colIVA_C,
            this.colRENTA,
            this.colSUBTOTAL,
            this.colIVA_RETENIDO,
            this.colTOTAL_C,
            this.colAPLICA});
            this.gvCandidatos.GridControl = this.gridControl1;
            this.gvCandidatos.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvCandidatos.Name = "gvCandidatos";
            this.gvCandidatos.OptionsView.ShowIndicator = false;
            this.gvCandidatos.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            //
            // colID_COMPROB_ENCA
            //
            this.colID_COMPROB_ENCA.Caption = "Id";
            this.colID_COMPROB_ENCA.FieldName = "ID_COMPROB_ENCA";
            this.colID_COMPROB_ENCA.MinWidth = 18;
            this.colID_COMPROB_ENCA.Name = "colID_COMPROB_ENCA";
            this.colID_COMPROB_ENCA.OptionsColumn.AllowEdit = false;
            this.colID_COMPROB_ENCA.OptionsColumn.AllowSize = false;
            this.colID_COMPROB_ENCA.Visible = true;
            this.colID_COMPROB_ENCA.VisibleIndex = 0;
            this.colID_COMPROB_ENCA.Width = 70;
            //
            // colNUM_DOCTO
            //
            this.colNUM_DOCTO.Caption = "Documento";
            this.colNUM_DOCTO.FieldName = "NUM_DOCTO";
            this.colNUM_DOCTO.MinWidth = 18;
            this.colNUM_DOCTO.Name = "colNUM_DOCTO";
            this.colNUM_DOCTO.OptionsColumn.AllowEdit = false;
            this.colNUM_DOCTO.OptionsColumn.AllowSize = false;
            this.colNUM_DOCTO.Visible = true;
            this.colNUM_DOCTO.VisibleIndex = 1;
            this.colNUM_DOCTO.Width = 110;
            //
            // colSERIE
            //
            this.colSERIE.Caption = "Serie";
            this.colSERIE.FieldName = "SERIE";
            this.colSERIE.MinWidth = 18;
            this.colSERIE.Name = "colSERIE";
            this.colSERIE.OptionsColumn.AllowEdit = false;
            this.colSERIE.OptionsColumn.AllowSize = false;
            this.colSERIE.Visible = true;
            this.colSERIE.VisibleIndex = 2;
            this.colSERIE.Width = 90;
            //
            // colCODIGO
            //
            this.colCODIGO.Caption = "Codigo";
            this.colCODIGO.FieldName = "CODIGO";
            this.colCODIGO.MinWidth = 18;
            this.colCODIGO.Name = "colCODIGO";
            this.colCODIGO.OptionsColumn.AllowEdit = false;
            this.colCODIGO.OptionsColumn.AllowSize = false;
            this.colCODIGO.Visible = true;
            this.colCODIGO.VisibleIndex = 3;
            this.colCODIGO.Width = 90;
            //
            // colNOM_PROVEEDOR
            //
            this.colNOM_PROVEEDOR.Caption = "Proveedor";
            this.colNOM_PROVEEDOR.FieldName = "NOM_PROVEEDOR";
            this.colNOM_PROVEEDOR.MinWidth = 18;
            this.colNOM_PROVEEDOR.Name = "colNOM_PROVEEDOR";
            this.colNOM_PROVEEDOR.OptionsColumn.AllowEdit = false;
            this.colNOM_PROVEEDOR.OptionsColumn.AllowSize = false;
            this.colNOM_PROVEEDOR.Visible = true;
            this.colNOM_PROVEEDOR.VisibleIndex = 4;
            this.colNOM_PROVEEDOR.Width = 260;
            //
            // colFECHA_C
            //
            this.colFECHA_C.Caption = "Fecha";
            this.colFECHA_C.FieldName = "FECHA";
            this.colFECHA_C.MinWidth = 18;
            this.colFECHA_C.Name = "colFECHA_C";
            this.colFECHA_C.OptionsColumn.AllowEdit = false;
            this.colFECHA_C.OptionsColumn.AllowSize = false;
            this.colFECHA_C.Visible = true;
            this.colFECHA_C.VisibleIndex = 5;
            this.colFECHA_C.Width = 90;
            //
            // colITEMS
            //
            this.colITEMS.Caption = "Items";
            this.colITEMS.FieldName = "ITEMS";
            this.colITEMS.MinWidth = 18;
            this.colITEMS.Name = "colITEMS";
            this.colITEMS.OptionsColumn.AllowEdit = false;
            this.colITEMS.OptionsColumn.AllowSize = false;
            this.colITEMS.Visible = true;
            this.colITEMS.VisibleIndex = 6;
            this.colITEMS.Width = 60;
            //
            // colSUMAS
            //
            this.colSUMAS.AppearanceCell.Options.UseTextOptions = true;
            this.colSUMAS.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSUMAS.Caption = "Sumas";
            this.colSUMAS.DisplayFormat.FormatString = "n2";
            this.colSUMAS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSUMAS.FieldName = "SUMAS";
            this.colSUMAS.MinWidth = 18;
            this.colSUMAS.Name = "colSUMAS";
            this.colSUMAS.OptionsColumn.AllowEdit = false;
            this.colSUMAS.OptionsColumn.AllowSize = false;
            this.colSUMAS.Visible = true;
            this.colSUMAS.VisibleIndex = 7;
            this.colSUMAS.Width = 90;
            //
            // colIVA_C
            //
            this.colIVA_C.AppearanceCell.Options.UseTextOptions = true;
            this.colIVA_C.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colIVA_C.Caption = "Iva";
            this.colIVA_C.DisplayFormat.FormatString = "n2";
            this.colIVA_C.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colIVA_C.FieldName = "IVA";
            this.colIVA_C.MinWidth = 18;
            this.colIVA_C.Name = "colIVA_C";
            this.colIVA_C.OptionsColumn.AllowEdit = false;
            this.colIVA_C.OptionsColumn.AllowSize = false;
            this.colIVA_C.Visible = true;
            this.colIVA_C.VisibleIndex = 8;
            this.colIVA_C.Width = 80;
            //
            // colRENTA
            //
            this.colRENTA.AppearanceCell.Options.UseTextOptions = true;
            this.colRENTA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colRENTA.Caption = "Renta";
            this.colRENTA.DisplayFormat.FormatString = "n2";
            this.colRENTA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRENTA.FieldName = "RENTA";
            this.colRENTA.MinWidth = 18;
            this.colRENTA.Name = "colRENTA";
            this.colRENTA.OptionsColumn.AllowEdit = false;
            this.colRENTA.OptionsColumn.AllowSize = false;
            this.colRENTA.Visible = true;
            this.colRENTA.VisibleIndex = 9;
            this.colRENTA.Width = 80;
            //
            // colSUBTOTAL
            //
            this.colSUBTOTAL.AppearanceCell.Options.UseTextOptions = true;
            this.colSUBTOTAL.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSUBTOTAL.Caption = "Subtotal";
            this.colSUBTOTAL.DisplayFormat.FormatString = "n2";
            this.colSUBTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSUBTOTAL.FieldName = "SUBTOTAL";
            this.colSUBTOTAL.MinWidth = 18;
            this.colSUBTOTAL.Name = "colSUBTOTAL";
            this.colSUBTOTAL.OptionsColumn.AllowEdit = false;
            this.colSUBTOTAL.OptionsColumn.AllowSize = false;
            this.colSUBTOTAL.Visible = true;
            this.colSUBTOTAL.VisibleIndex = 10;
            this.colSUBTOTAL.Width = 90;
            //
            // colIVA_RETENIDO
            //
            this.colIVA_RETENIDO.AppearanceCell.Options.UseTextOptions = true;
            this.colIVA_RETENIDO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colIVA_RETENIDO.Caption = "Iva Retenido";
            this.colIVA_RETENIDO.DisplayFormat.FormatString = "n2";
            this.colIVA_RETENIDO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colIVA_RETENIDO.FieldName = "IVA_RETENIDO";
            this.colIVA_RETENIDO.MinWidth = 18;
            this.colIVA_RETENIDO.Name = "colIVA_RETENIDO";
            this.colIVA_RETENIDO.OptionsColumn.AllowEdit = false;
            this.colIVA_RETENIDO.OptionsColumn.AllowSize = false;
            this.colIVA_RETENIDO.Visible = true;
            this.colIVA_RETENIDO.VisibleIndex = 11;
            this.colIVA_RETENIDO.Width = 90;
            //
            // colTOTAL_C
            //
            this.colTOTAL_C.AppearanceCell.Options.UseTextOptions = true;
            this.colTOTAL_C.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTOTAL_C.Caption = "Total";
            this.colTOTAL_C.DisplayFormat.FormatString = "n2";
            this.colTOTAL_C.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTOTAL_C.FieldName = "TOTAL";
            this.colTOTAL_C.MinWidth = 18;
            this.colTOTAL_C.Name = "colTOTAL_C";
            this.colTOTAL_C.OptionsColumn.AllowEdit = false;
            this.colTOTAL_C.OptionsColumn.AllowSize = false;
            this.colTOTAL_C.Visible = true;
            this.colTOTAL_C.VisibleIndex = 12;
            this.colTOTAL_C.Width = 90;
            //
            // colAPLICA — solo viene poblada cuando ID_TIPO_COMPROB = 1 (CCF físico/electrónico).
            //
            this.colAPLICA.Caption = "Aplica";
            this.colAPLICA.FieldName = "APLICA";
            this.colAPLICA.MinWidth = 18;
            this.colAPLICA.Name = "colAPLICA";
            this.colAPLICA.OptionsColumn.AllowEdit = false;
            this.colAPLICA.OptionsColumn.AllowSize = false;
            this.colAPLICA.Visible = true;
            this.colAPLICA.VisibleIndex = 13;
            this.colAPLICA.Width = 70;
            //
            // panelNuevo
            //
            this.panelNuevo.Controls.Add(this.btnNuevo);
            this.panelNuevo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNuevo.Location = new System.Drawing.Point(0, 444);
            this.panelNuevo.Name = "panelNuevo";
            this.panelNuevo.Size = new System.Drawing.Size(1120, 50);
            this.panelNuevo.TabIndex = 3;
            //
            // btnNuevo
            //
            this.btnNuevo.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.Appearance.Options.UseFont = true;
            this.btnNuevo.Appearance.Options.UseTextOptions = true;
            this.btnNuevo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnNuevo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevo.Location = new System.Drawing.Point(16, 6);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(2);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(110, 38);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.TabStop = false;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            //
            // gridControl2 — grid inferior (documentos ya generados)
            //
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 494);
            this.gridControl2.MainView = this.gvDocumentos;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(1120, 256);
            this.gridControl2.TabIndex = 4;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDocumentos});
            //
            // gvDocumentos — NO trae columnas fijas: se repuebla por código (PopulateColumns) al llamar
            // SP_CREDITOFISCAL_ENC @ACCION='LISTAR' (ver AjustarColumnasDocumentos en el .cs).
            //
            this.gvDocumentos.GridControl = this.gridControl2;
            this.gvDocumentos.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvDocumentos.Name = "gvDocumentos";
            this.gvDocumentos.OptionsView.ShowIndicator = false;
            this.gvDocumentos.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            //
            // frmFacturacionInt
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 750);
            this.Controls.Add(this.gridControl2);
            this.Controls.Add(this.panelNuevo);
            this.Controls.Add(this.panelCandidatos);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.grpFiltros);
            this.Name = "frmFacturacionInt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Facturación Interna";
            this.Load += new System.EventHandler(this.frmFacturacionInt_Load);
            this.grpFiltros.ResumeLayout(false);
            this.grpFiltros.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.panelCandidatos.ResumeLayout(false);
            this.panelNuevo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCandidatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDocumentos)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpFiltros;
        private System.Windows.Forms.Label lblTipoComprobante;
        private System.Windows.Forms.ComboBox cbxTIPO_COMPROBANTE;
        private System.Windows.Forms.Label lblZafra;
        private System.Windows.Forms.ComboBox cbxZAFRA;
        private System.Windows.Forms.Label lblCatorcena;
        private System.Windows.Forms.ComboBox cbxCATORCENA;
        private System.Windows.Forms.Label lblTipoPlanilla;
        private System.Windows.Forms.ComboBox cbxTIPO_PLANILLA;
        private System.Windows.Forms.CheckBox chkNoSellados;

        private System.Windows.Forms.Panel panelBotones;
        private DevExpress.XtraEditors.SimpleButton btnConsultar;
        private DevExpress.XtraEditors.SimpleButton btnImportar;
        private DevExpress.XtraEditors.SimpleButton btnReporte;
        private DevExpress.XtraEditors.SimpleButton btnSalir;

        private System.Windows.Forms.Panel panelCandidatos;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCandidatos;
        private DevExpress.XtraGrid.Columns.GridColumn colID_COMPROB_ENCA;
        private DevExpress.XtraGrid.Columns.GridColumn colNUM_DOCTO;
        private DevExpress.XtraGrid.Columns.GridColumn colSERIE;
        private DevExpress.XtraGrid.Columns.GridColumn colCODIGO;
        private DevExpress.XtraGrid.Columns.GridColumn colNOM_PROVEEDOR;
        private DevExpress.XtraGrid.Columns.GridColumn colFECHA_C;
        private DevExpress.XtraGrid.Columns.GridColumn colITEMS;
        private DevExpress.XtraGrid.Columns.GridColumn colSUMAS;
        private DevExpress.XtraGrid.Columns.GridColumn colIVA_C;
        private DevExpress.XtraGrid.Columns.GridColumn colRENTA;
        private DevExpress.XtraGrid.Columns.GridColumn colSUBTOTAL;
        private DevExpress.XtraGrid.Columns.GridColumn colIVA_RETENIDO;
        private DevExpress.XtraGrid.Columns.GridColumn colTOTAL_C;
        private DevExpress.XtraGrid.Columns.GridColumn colAPLICA;

        private System.Windows.Forms.Panel panelNuevo;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDocumentos;
    }
}
