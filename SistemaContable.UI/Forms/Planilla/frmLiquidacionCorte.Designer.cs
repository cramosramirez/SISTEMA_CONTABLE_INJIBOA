namespace SistemaContable.UI.Forms.Planilla
{
    partial class frmLiquidacionCorte
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.grpFiltros = new System.Windows.Forms.GroupBox();
            this.lblTipoComprobante = new System.Windows.Forms.Label();
            this.cbxTipoComprobante = new System.Windows.Forms.ComboBox();
            this.lblZafra = new System.Windows.Forms.Label();
            this.cbxZafra = new System.Windows.Forms.ComboBox();
            this.lblCatorcena = new System.Windows.Forms.Label();
            this.cbxCatorcena = new System.Windows.Forms.ComboBox();
            this.lblTipoPlanilla = new System.Windows.Forms.Label();
            this.cbxTipoPlanilla = new System.Windows.Forms.ComboBox();
            this.chkPrueba = new System.Windows.Forms.CheckBox();
            this.lblGrupoEnvio = new System.Windows.Forms.Label();
            this.cbxGrupoEnvio = new System.Windows.Forms.ComboBox();
            this.lblTipoContribuyente = new System.Windows.Forms.Label();
            this.rbContribuyente = new System.Windows.Forms.RadioButton();
            this.rbNoContribuyente = new System.Windows.Forms.RadioButton();
            this.lblFechaInicial = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.lblFechaFinal = new System.Windows.Forms.Label();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.panelProgreso = new System.Windows.Forms.Panel();
            this.lblProgresoCorreo = new System.Windows.Forms.Label();
            this.pbEnvioCorreo = new System.Windows.Forms.ProgressBar();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnConsultar = new DevExpress.XtraEditors.SimpleButton();
            this.btnImportar = new DevExpress.XtraEditors.SimpleButton();
            this.btnEliminarPrueba = new DevExpress.XtraEditors.SimpleButton();
            this.btnEnviar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
            this.lblModoPrueba = new System.Windows.Forms.Label();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.gridComprobantes = new DevExpress.XtraGrid.GridControl();
            this.gvComprobantes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblPendientes = new System.Windows.Forms.Label();
            this.gridLiquidaciones = new DevExpress.XtraGrid.GridControl();
            this.gvLiquidaciones = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblGenerados = new System.Windows.Forms.Label();
            this.grpFiltros.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.panelProgreso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridComprobantes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvComprobantes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLiquidaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLiquidaciones)).BeginInit();
            this.SuspendLayout();
            // 
            // grpFiltros
            // 
            this.grpFiltros.Controls.Add(this.lblTipoComprobante);
            this.grpFiltros.Controls.Add(this.cbxTipoComprobante);
            this.grpFiltros.Controls.Add(this.lblZafra);
            this.grpFiltros.Controls.Add(this.cbxZafra);
            this.grpFiltros.Controls.Add(this.lblCatorcena);
            this.grpFiltros.Controls.Add(this.cbxCatorcena);
            this.grpFiltros.Controls.Add(this.lblTipoPlanilla);
            this.grpFiltros.Controls.Add(this.cbxTipoPlanilla);
            this.grpFiltros.Controls.Add(this.chkPrueba);
            this.grpFiltros.Controls.Add(this.lblGrupoEnvio);
            this.grpFiltros.Controls.Add(this.cbxGrupoEnvio);
            this.grpFiltros.Controls.Add(this.lblTipoContribuyente);
            this.grpFiltros.Controls.Add(this.rbContribuyente);
            this.grpFiltros.Controls.Add(this.rbNoContribuyente);
            this.grpFiltros.Controls.Add(this.lblFechaInicial);
            this.grpFiltros.Controls.Add(this.dtpFechaInicial);
            this.grpFiltros.Controls.Add(this.lblFechaFinal);
            this.grpFiltros.Controls.Add(this.dtpFechaFinal);
            this.grpFiltros.Controls.Add(this.panelBotones);
            this.grpFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFiltros.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpFiltros.Location = new System.Drawing.Point(8, 8);
            this.grpFiltros.Name = "grpFiltros";
            this.grpFiltros.Size = new System.Drawing.Size(1264, 221);
            this.grpFiltros.TabIndex = 0;
            this.grpFiltros.TabStop = false;
            this.grpFiltros.Text = "Envío de Comprobantes de Liquidación";
            // 
            // lblTipoComprobante
            // 
            this.lblTipoComprobante.AutoSize = true;
            this.lblTipoComprobante.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTipoComprobante.Location = new System.Drawing.Point(116, 28);
            this.lblTipoComprobante.Name = "lblTipoComprobante";
            this.lblTipoComprobante.Size = new System.Drawing.Size(109, 15);
            this.lblTipoComprobante.TabIndex = 0;
            this.lblTipoComprobante.Text = "Tipo comprobante:";
            // 
            // cbxTipoComprobante
            // 
            this.cbxTipoComprobante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoComprobante.Enabled = false;
            this.cbxTipoComprobante.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbxTipoComprobante.Location = new System.Drawing.Point(228, 24);
            this.cbxTipoComprobante.Name = "cbxTipoComprobante";
            this.cbxTipoComprobante.Size = new System.Drawing.Size(520, 23);
            this.cbxTipoComprobante.TabIndex = 0;
            // 
            // lblZafra
            // 
            this.lblZafra.AutoSize = true;
            this.lblZafra.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblZafra.Location = new System.Drawing.Point(181, 59);
            this.lblZafra.Name = "lblZafra";
            this.lblZafra.Size = new System.Drawing.Size(37, 15);
            this.lblZafra.TabIndex = 1;
            this.lblZafra.Text = "Zafra:";
            // 
            // cbxZafra
            // 
            this.cbxZafra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxZafra.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbxZafra.Location = new System.Drawing.Point(228, 55);
            this.cbxZafra.Name = "cbxZafra";
            this.cbxZafra.Size = new System.Drawing.Size(180, 23);
            this.cbxZafra.TabIndex = 1;
            this.cbxZafra.SelectedIndexChanged += new System.EventHandler(this.cbxZafra_SelectedIndexChanged);
            // 
            // lblCatorcena
            // 
            this.lblCatorcena.AutoSize = true;
            this.lblCatorcena.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCatorcena.Location = new System.Drawing.Point(448, 59);
            this.lblCatorcena.Name = "lblCatorcena";
            this.lblCatorcena.Size = new System.Drawing.Size(64, 15);
            this.lblCatorcena.TabIndex = 2;
            this.lblCatorcena.Text = "Catorcena:";
            // 
            // cbxCatorcena
            // 
            this.cbxCatorcena.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCatorcena.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbxCatorcena.Location = new System.Drawing.Point(520, 55);
            this.cbxCatorcena.Name = "cbxCatorcena";
            this.cbxCatorcena.Size = new System.Drawing.Size(228, 23);
            this.cbxCatorcena.TabIndex = 2;
            this.cbxCatorcena.SelectedIndexChanged += new System.EventHandler(this.cbxCatorcena_SelectedIndexChanged);
            // 
            // lblTipoPlanilla
            // 
            this.lblTipoPlanilla.AutoSize = true;
            this.lblTipoPlanilla.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTipoPlanilla.Location = new System.Drawing.Point(146, 90);
            this.lblTipoPlanilla.Name = "lblTipoPlanilla";
            this.lblTipoPlanilla.Size = new System.Drawing.Size(75, 15);
            this.lblTipoPlanilla.TabIndex = 3;
            this.lblTipoPlanilla.Text = "Tipo planilla:";
            // 
            // cbxTipoPlanilla
            // 
            this.cbxTipoPlanilla.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoPlanilla.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbxTipoPlanilla.Location = new System.Drawing.Point(228, 86);
            this.cbxTipoPlanilla.Name = "cbxTipoPlanilla";
            this.cbxTipoPlanilla.Size = new System.Drawing.Size(350, 23);
            this.cbxTipoPlanilla.TabIndex = 3;
            this.cbxTipoPlanilla.SelectedIndexChanged += new System.EventHandler(this.cbxTipoPlanilla_SelectedIndexChanged);
            // 
            // chkPrueba
            // 
            this.chkPrueba.AutoSize = true;
            this.chkPrueba.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkPrueba.Location = new System.Drawing.Point(480, 119);
            this.chkPrueba.Name = "chkPrueba";
            this.chkPrueba.Size = new System.Drawing.Size(63, 19);
            this.chkPrueba.TabIndex = 6;
            this.chkPrueba.Text = "Ejecutar como prueba";
            this.chkPrueba.CheckedChanged += new System.EventHandler(this.chkPrueba_CheckedChanged);
            // 
            // lblGrupoEnvio
            // 
            this.lblGrupoEnvio.AutoSize = true;
            this.lblGrupoEnvio.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGrupoEnvio.Location = new System.Drawing.Point(138, 183);
            this.lblGrupoEnvio.Name = "lblGrupoEnvio";
            this.lblGrupoEnvio.Size = new System.Drawing.Size(75, 15);
            this.lblGrupoEnvio.TabIndex = 10;
            this.lblGrupoEnvio.Text = "Grupo envío:";
            // 
            // cbxGrupoEnvio
            // 
            this.cbxGrupoEnvio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGrupoEnvio.Enabled = false;
            this.cbxGrupoEnvio.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbxGrupoEnvio.Location = new System.Drawing.Point(228, 179);
            this.cbxGrupoEnvio.Name = "cbxGrupoEnvio";
            this.cbxGrupoEnvio.Size = new System.Drawing.Size(350, 23);
            this.cbxGrupoEnvio.TabIndex = 10;
            this.cbxGrupoEnvio.SelectedIndexChanged += new System.EventHandler(this.cbxGrupoEnvio_SelectedIndexChanged);
            // 
            // lblTipoContribuyente
            // 
            this.lblTipoContribuyente.AutoSize = true;
            this.lblTipoContribuyente.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTipoContribuyente.Location = new System.Drawing.Point(84, 121);
            this.lblTipoContribuyente.Name = "lblTipoContribuyente";
            this.lblTipoContribuyente.Size = new System.Drawing.Size(111, 15);
            this.lblTipoContribuyente.TabIndex = 4;
            this.lblTipoContribuyente.Text = "Tipo contribuyente:";
            // 
            // rbContribuyente
            // 
            this.rbContribuyente.AutoSize = true;
            this.rbContribuyente.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rbContribuyente.Location = new System.Drawing.Point(228, 119);
            this.rbContribuyente.Name = "rbContribuyente";
            this.rbContribuyente.Size = new System.Drawing.Size(101, 19);
            this.rbContribuyente.TabIndex = 4;
            this.rbContribuyente.Text = "Contribuyente";
            this.rbContribuyente.CheckedChanged += new System.EventHandler(this.TipoContribuyente_CheckedChanged);
            // 
            // rbNoContribuyente
            // 
            this.rbNoContribuyente.AutoSize = true;
            this.rbNoContribuyente.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rbNoContribuyente.Location = new System.Drawing.Point(335, 119);
            this.rbNoContribuyente.Name = "rbNoContribuyente";
            this.rbNoContribuyente.Size = new System.Drawing.Size(118, 19);
            this.rbNoContribuyente.TabIndex = 5;
            this.rbNoContribuyente.Text = "No contribuyente";
            this.rbNoContribuyente.CheckedChanged += new System.EventHandler(this.TipoContribuyente_CheckedChanged);
            // 
            // lblFechaInicial
            // 
            this.lblFechaInicial.AutoSize = true;
            this.lblFechaInicial.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFechaInicial.Location = new System.Drawing.Point(84, 152);
            this.lblFechaInicial.Name = "lblFechaInicial";
            this.lblFechaInicial.Size = new System.Drawing.Size(140, 15);
            this.lblFechaInicial.TabIndex = 7;
            this.lblFechaInicial.Text = "Fecha documento inicial:";
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicial.Location = new System.Drawing.Point(228, 148);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(180, 23);
            this.dtpFechaInicial.TabIndex = 7;
            // 
            // lblFechaFinal
            // 
            this.lblFechaFinal.AutoSize = true;
            this.lblFechaFinal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFechaFinal.Location = new System.Drawing.Point(443, 152);
            this.lblFechaFinal.Name = "lblFechaFinal";
            this.lblFechaFinal.Size = new System.Drawing.Size(67, 15);
            this.lblFechaFinal.TabIndex = 8;
            this.lblFechaFinal.Text = "Fecha final:";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFinal.Location = new System.Drawing.Point(520, 148);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(228, 23);
            this.dtpFechaFinal.TabIndex = 8;
            // 
            // panelProgreso
            // 
            this.panelProgreso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(242)))), ((int)(((byte)(252)))));
            this.panelProgreso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProgreso.Controls.Add(this.lblProgresoCorreo);
            this.panelProgreso.Controls.Add(this.pbEnvioCorreo);
            this.panelProgreso.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelProgreso.Location = new System.Drawing.Point(8, 229);
            this.panelProgreso.Name = "panelProgreso";
            this.panelProgreso.Size = new System.Drawing.Size(1264, 52);
            this.panelProgreso.TabIndex = 3;
            this.panelProgreso.Visible = false;
            // 
            // lblProgresoCorreo
            // 
            this.lblProgresoCorreo.AutoEllipsis = true;
            this.lblProgresoCorreo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblProgresoCorreo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(63)))), ((int)(((byte)(114)))));
            this.lblProgresoCorreo.Location = new System.Drawing.Point(11, 4);
            this.lblProgresoCorreo.Name = "lblProgresoCorreo";
            this.lblProgresoCorreo.Size = new System.Drawing.Size(1240, 19);
            this.lblProgresoCorreo.TabIndex = 12;
            this.lblProgresoCorreo.Text = "Enviando correos...";
            this.lblProgresoCorreo.Visible = false;
            this.lblProgresoCorreo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // pbEnvioCorreo
            // 
            this.pbEnvioCorreo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbEnvioCorreo.Location = new System.Drawing.Point(11, 27);
            this.pbEnvioCorreo.Name = "pbEnvioCorreo";
            this.pbEnvioCorreo.Size = new System.Drawing.Size(1240, 17);
            this.pbEnvioCorreo.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbEnvioCorreo.TabIndex = 13;
            this.pbEnvioCorreo.Visible = false;
            // 
            // panelBotones
            // 
            this.panelBotones.Controls.Add(this.btnConsultar);
            this.panelBotones.Controls.Add(this.btnImportar);
            this.panelBotones.Controls.Add(this.btnEliminarPrueba);
            this.panelBotones.Controls.Add(this.btnEnviar);
            this.panelBotones.Controls.Add(this.btnSalir);
            this.panelBotones.Location = new System.Drawing.Point(600, 174);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(652, 42);
            this.panelBotones.TabIndex = 11;
            // 
            // btnConsultar
            // 
            this.btnConsultar.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnConsultar.Appearance.Options.UseFont = true;
            this.btnConsultar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.buscar1_48x48;
            this.btnConsultar.Location = new System.Drawing.Point(0, 0);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(120, 42);
            this.btnConsultar.TabIndex = 0;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnImportar
            // 
            this.btnImportar.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnImportar.Appearance.Options.UseFont = true;
            this.btnImportar.Enabled = false;
            this.btnImportar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.descargar48x48;
            this.btnImportar.Location = new System.Drawing.Point(128, 0);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(120, 42);
            this.btnImportar.TabIndex = 1;
            this.btnImportar.Text = "Importar";
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // btnEliminarPrueba
            // 
            this.btnEliminarPrueba.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEliminarPrueba.Appearance.ForeColor = System.Drawing.Color.Maroon;
            this.btnEliminarPrueba.Appearance.Options.UseFont = true;
            this.btnEliminarPrueba.Appearance.Options.UseForeColor = true;
            this.btnEliminarPrueba.Enabled = false;
            this.btnEliminarPrueba.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.eliminar32x32;
            this.btnEliminarPrueba.Location = new System.Drawing.Point(256, 0);
            this.btnEliminarPrueba.Name = "btnEliminarPrueba";
            this.btnEliminarPrueba.Size = new System.Drawing.Size(148, 42);
            this.btnEliminarPrueba.TabIndex = 2;
            this.btnEliminarPrueba.Text = "Eliminar prueba";
            this.btnEliminarPrueba.Click += new System.EventHandler(this.btnEliminarPrueba_Click);
            // 
            // btnEnviar
            // 
            this.btnEnviar.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEnviar.Appearance.Options.UseFont = true;
            this.btnEnviar.Enabled = false;
            this.btnEnviar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.correo32x32;
            this.btnEnviar.Location = new System.Drawing.Point(412, 0);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(132, 42);
            this.btnEnviar.TabIndex = 3;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSalir.Appearance.Options.UseFont = true;
            this.btnSalir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnSalir.Location = new System.Drawing.Point(552, 0);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(100, 42);
            this.btnSalir.TabIndex = 4;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // lblModoPrueba
            // 
            this.lblModoPrueba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(243)))), ((int)(((byte)(205)))));
            this.lblModoPrueba.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblModoPrueba.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblModoPrueba.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblModoPrueba.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(100)))), ((int)(((byte)(4)))));
            this.lblModoPrueba.Location = new System.Drawing.Point(8, 229);
            this.lblModoPrueba.Name = "lblModoPrueba";
            this.lblModoPrueba.Size = new System.Drawing.Size(1264, 34);
            this.lblModoPrueba.TabIndex = 2;
            this.lblModoPrueba.Text = "⚠ MODO PRUEBA — Los correos se enviarán únicamente al correo configurado para pruebas";
            this.lblModoPrueba.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblModoPrueba.Visible = false;
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(8, 198);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Vertical;
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.gridComprobantes);
            this.splitMain.Panel1.Controls.Add(this.lblPendientes);
            this.splitMain.Panel1MinSize = 420;
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.gridLiquidaciones);
            this.splitMain.Panel2.Controls.Add(this.lblGenerados);
            this.splitMain.Panel2MinSize = 420;
            this.splitMain.Size = new System.Drawing.Size(1264, 514);
            this.splitMain.SplitterDistance = 629;
            this.splitMain.SplitterWidth = 6;
            this.splitMain.TabIndex = 1;
            // 
            // gridComprobantes
            // 
            this.gridComprobantes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridComprobantes.Location = new System.Drawing.Point(0, 28);
            this.gridComprobantes.MainView = this.gvComprobantes;
            this.gridComprobantes.Name = "gridComprobantes";
            this.gridComprobantes.Size = new System.Drawing.Size(629, 486);
            this.gridComprobantes.TabIndex = 0;
            this.gridComprobantes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvComprobantes});
            // 
            // gvComprobantes
            // 
            this.gvComprobantes.GridControl = this.gridComprobantes;
            this.gvComprobantes.Name = "gvComprobantes";
            // 
            // lblPendientes
            // 
            this.lblPendientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.lblPendientes.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPendientes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPendientes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(63)))), ((int)(((byte)(114)))));
            this.lblPendientes.Location = new System.Drawing.Point(0, 0);
            this.lblPendientes.Name = "lblPendientes";
            this.lblPendientes.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblPendientes.Size = new System.Drawing.Size(629, 28);
            this.lblPendientes.TabIndex = 1;
            this.lblPendientes.Text = "Comprobantes disponibles";
            this.lblPendientes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gridLiquidaciones
            // 
            this.gridLiquidaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLiquidaciones.Location = new System.Drawing.Point(0, 28);
            this.gridLiquidaciones.MainView = this.gvLiquidaciones;
            this.gridLiquidaciones.Name = "gridLiquidaciones";
            this.gridLiquidaciones.Size = new System.Drawing.Size(629, 486);
            this.gridLiquidaciones.TabIndex = 0;
            this.gridLiquidaciones.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLiquidaciones});
            // 
            // gvLiquidaciones
            // 
            this.gvLiquidaciones.GridControl = this.gridLiquidaciones;
            this.gvLiquidaciones.Name = "gvLiquidaciones";
            this.gvLiquidaciones.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvLiquidaciones_RowStyle);
            // 
            // lblGenerados
            // 
            this.lblGenerados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(218)))));
            this.lblGenerados.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGenerados.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblGenerados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(87)))), ((int)(((byte)(35)))));
            this.lblGenerados.Location = new System.Drawing.Point(0, 0);
            this.lblGenerados.Name = "lblGenerados";
            this.lblGenerados.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblGenerados.Size = new System.Drawing.Size(629, 28);
            this.lblGenerados.TabIndex = 1;
            this.lblGenerados.Text = "Liquidaciones importadas";
            this.lblGenerados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmLiquidacionCorte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.lblModoPrueba);
            this.Controls.Add(this.panelProgreso);
            this.Controls.Add(this.grpFiltros);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1120, 680);
            this.Name = "frmLiquidacionCorte";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Consulta";
            this.Text = "Comprobantes de Liquidación";
            this.Load += new System.EventHandler(this.frmLiquidacionCorte_Load);
            this.grpFiltros.ResumeLayout(false);
            this.grpFiltros.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.panelProgreso.ResumeLayout(false);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridComprobantes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvComprobantes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLiquidaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLiquidaciones)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFiltros;
        private System.Windows.Forms.Label lblTipoComprobante;
        private System.Windows.Forms.ComboBox cbxTipoComprobante;
        private System.Windows.Forms.Label lblZafra;
        private System.Windows.Forms.ComboBox cbxZafra;
        private System.Windows.Forms.Label lblCatorcena;
        private System.Windows.Forms.ComboBox cbxCatorcena;
        private System.Windows.Forms.Label lblTipoPlanilla;
        private System.Windows.Forms.ComboBox cbxTipoPlanilla;
        private System.Windows.Forms.CheckBox chkPrueba;
        private System.Windows.Forms.Label lblGrupoEnvio;
        private System.Windows.Forms.ComboBox cbxGrupoEnvio;
        private System.Windows.Forms.Label lblTipoContribuyente;
        private System.Windows.Forms.RadioButton rbContribuyente;
        private System.Windows.Forms.RadioButton rbNoContribuyente;
        private System.Windows.Forms.Label lblFechaInicial;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.Label lblFechaFinal;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Panel panelProgreso;
        private System.Windows.Forms.Label lblProgresoCorreo;
        private System.Windows.Forms.ProgressBar pbEnvioCorreo;
        private System.Windows.Forms.Panel panelBotones;
        private DevExpress.XtraEditors.SimpleButton btnConsultar;
        private DevExpress.XtraEditors.SimpleButton btnImportar;
        private DevExpress.XtraEditors.SimpleButton btnEliminarPrueba;
        private DevExpress.XtraEditors.SimpleButton btnEnviar;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private System.Windows.Forms.Label lblModoPrueba;
        private System.Windows.Forms.SplitContainer splitMain;
        private DevExpress.XtraGrid.GridControl gridComprobantes;
        private DevExpress.XtraGrid.Views.Grid.GridView gvComprobantes;
        private System.Windows.Forms.Label lblPendientes;
        private DevExpress.XtraGrid.GridControl gridLiquidaciones;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLiquidaciones;
        private System.Windows.Forms.Label lblGenerados;
    }
}
