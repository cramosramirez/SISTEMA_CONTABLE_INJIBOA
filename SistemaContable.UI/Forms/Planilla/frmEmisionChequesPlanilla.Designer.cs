namespace SistemaContable.UI.Forms.Planilla
{
    partial class frmEmisionChequesPlanilla
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.grpDatos = new System.Windows.Forms.GroupBox();
            this.lineaSeccion = new System.Windows.Forms.Label();
            this.lblSeccionCheques = new System.Windows.Forms.Label();
            this.lblCantidadCheques = new System.Windows.Forms.Label();
            this.txtNumPartida = new System.Windows.Forms.TextBox();
            this.txtUltimoCheque = new System.Windows.Forms.TextBox();
            this.txtPrimerCheque = new System.Windows.Forms.TextBox();
            this.txtNombreCuenta = new System.Windows.Forms.TextBox();
            this.txtCuenta = new System.Windows.Forms.TextBox();
            this.cbxEsContribuyente = new System.Windows.Forms.ComboBox();
            this.cbxTipoPlanilla = new System.Windows.Forms.ComboBox();
            this.cbxCatorcena = new System.Windows.Forms.ComboBox();
            this.cbxZafra = new System.Windows.Forms.ComboBox();
            this.labelPartida = new System.Windows.Forms.Label();
            this.labelUltimo = new System.Windows.Forms.Label();
            this.labelPrimero = new System.Windows.Forms.Label();
            this.labelCuenta = new System.Windows.Forms.Label();
            this.labelContribuyente = new System.Windows.Forms.Label();
            this.labelTipo = new System.Windows.Forms.Label();
            this.labelCatorcena = new System.Windows.Forms.Label();
            this.labelZafra = new System.Windows.Forms.Label();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.lblAcciones = new System.Windows.Forms.Label();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
            this.btnEliminarPruebas = new DevExpress.XtraEditors.SimpleButton();
            this.btnImprimirCheques = new DevExpress.XtraEditors.SimpleButton();
            this.btnVerReporte = new DevExpress.XtraEditors.SimpleButton();
            this.btnEmitir = new DevExpress.XtraEditors.SimpleButton();
            this.panelProgreso = new System.Windows.Forms.Panel();
            this.lblEstadoProceso = new System.Windows.Forms.Label();
            this.progressGeneracion = new System.Windows.Forms.ProgressBar();
            this.grpResultados = new System.Windows.Forms.GroupBox();
            this.gridCheques = new DevExpress.XtraGrid.GridControl();
            this.gvCheques = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.panelEncabezado.SuspendLayout();
            this.grpDatos.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.panelProgreso.SuspendLayout();
            this.grpResultados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCheques)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCheques)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEncabezado
            // 
            this.panelEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(121)))));
            this.panelEncabezado.Controls.Add(this.lblSubtitulo);
            this.panelEncabezado.Controls.Add(this.lblTitulo);
            this.panelEncabezado.Location = new System.Drawing.Point(12, 12);
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.Size = new System.Drawing.Size(720, 70);
            this.panelEncabezado.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(22, 42);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(382, 15);
            this.lblSubtitulo.TabIndex = 0;
            this.lblSubtitulo.Text = "Seleccione la planilla, la cuenta bancaria y el rango de cheques a emitir.";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(273, 25);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Emisión de cheques de planilla";
            // 
            // grpDatos
            // 
            this.grpDatos.BackColor = System.Drawing.Color.White;
            this.grpDatos.Controls.Add(this.lineaSeccion);
            this.grpDatos.Controls.Add(this.lblSeccionCheques);
            this.grpDatos.Controls.Add(this.lblCantidadCheques);
            this.grpDatos.Controls.Add(this.txtNumPartida);
            this.grpDatos.Controls.Add(this.txtUltimoCheque);
            this.grpDatos.Controls.Add(this.txtPrimerCheque);
            this.grpDatos.Controls.Add(this.txtNombreCuenta);
            this.grpDatos.Controls.Add(this.txtCuenta);
            this.grpDatos.Controls.Add(this.cbxEsContribuyente);
            this.grpDatos.Controls.Add(this.cbxTipoPlanilla);
            this.grpDatos.Controls.Add(this.cbxCatorcena);
            this.grpDatos.Controls.Add(this.cbxZafra);
            this.grpDatos.Controls.Add(this.labelPartida);
            this.grpDatos.Controls.Add(this.labelUltimo);
            this.grpDatos.Controls.Add(this.labelPrimero);
            this.grpDatos.Controls.Add(this.labelCuenta);
            this.grpDatos.Controls.Add(this.labelContribuyente);
            this.grpDatos.Controls.Add(this.labelTipo);
            this.grpDatos.Controls.Add(this.labelCatorcena);
            this.grpDatos.Controls.Add(this.labelZafra);
            this.grpDatos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpDatos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.grpDatos.Location = new System.Drawing.Point(12, 92);
            this.grpDatos.Name = "grpDatos";
            this.grpDatos.Size = new System.Drawing.Size(720, 270);
            this.grpDatos.TabIndex = 1;
            this.grpDatos.TabStop = false;
            this.grpDatos.Text = "  Datos de la planilla  ";
            // 
            // lineaSeccion
            // 
            this.lineaSeccion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.lineaSeccion.Location = new System.Drawing.Point(350, 157);
            this.lineaSeccion.Name = "lineaSeccion";
            this.lineaSeccion.Size = new System.Drawing.Size(347, 1);
            this.lineaSeccion.TabIndex = 0;
            // 
            // lblSeccionCheques
            // 
            this.lblSeccionCheques.AutoSize = true;
            this.lblSeccionCheques.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSeccionCheques.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(121)))));
            this.lblSeccionCheques.Location = new System.Drawing.Point(24, 150);
            this.lblSeccionCheques.Name = "lblSeccionCheques";
            this.lblSeccionCheques.Size = new System.Drawing.Size(118, 15);
            this.lblSeccionCheques.TabIndex = 1;
            this.lblSeccionCheques.Text = "DATOS DEL CHEQUE";
            // 
            // lblCantidadCheques
            // 
            this.lblCantidadCheques.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.lblCantidadCheques.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCantidadCheques.ForeColor = System.Drawing.Color.White;
            this.lblCantidadCheques.Location = new System.Drawing.Point(160, 143);
            this.lblCantidadCheques.Name = "lblCantidadCheques";
            this.lblCantidadCheques.Size = new System.Drawing.Size(180, 28);
            this.lblCantidadCheques.TabIndex = 19;
            this.lblCantidadCheques.Text = "Cheques a emitir: —";
            this.lblCantidadCheques.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNumPartida
            // 
            this.txtNumPartida.Location = new System.Drawing.Point(160, 220);
            this.txtNumPartida.Name = "txtNumPartida";
            this.txtNumPartida.ReadOnly = true;
            this.txtNumPartida.Size = new System.Drawing.Size(210, 23);
            this.txtNumPartida.TabIndex = 2;
            this.txtNumPartida.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtUltimoCheque
            // 
            this.txtUltimoCheque.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(249)))));
            this.txtUltimoCheque.Location = new System.Drawing.Point(492, 180);
            this.txtUltimoCheque.Name = "txtUltimoCheque";
            this.txtUltimoCheque.ReadOnly = true;
            this.txtUltimoCheque.Size = new System.Drawing.Size(205, 23);
            this.txtUltimoCheque.TabIndex = 3;
            this.txtUltimoCheque.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPrimerCheque
            // 
            this.txtPrimerCheque.Location = new System.Drawing.Point(160, 180);
            this.txtPrimerCheque.Name = "txtPrimerCheque";
            this.txtPrimerCheque.Size = new System.Drawing.Size(210, 23);
            this.txtPrimerCheque.TabIndex = 4;
            this.txtPrimerCheque.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrimerCheque.Leave += new System.EventHandler(this.txtPrimerCheque_Leave);
            // 
            // txtNombreCuenta
            // 
            this.txtNombreCuenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(249)))));
            this.txtNombreCuenta.Location = new System.Drawing.Point(376, 110);
            this.txtNombreCuenta.Name = "txtNombreCuenta";
            this.txtNombreCuenta.ReadOnly = true;
            this.txtNombreCuenta.Size = new System.Drawing.Size(321, 23);
            this.txtNombreCuenta.TabIndex = 5;
            // 
            // txtCuenta
            // 
            this.txtCuenta.Location = new System.Drawing.Point(160, 110);
            this.txtCuenta.Name = "txtCuenta";
            this.txtCuenta.Size = new System.Drawing.Size(210, 23);
            this.txtCuenta.TabIndex = 6;
            // 
            // cbxEsContribuyente
            // 
            this.cbxEsContribuyente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEsContribuyente.Location = new System.Drawing.Point(492, 70);
            this.cbxEsContribuyente.Name = "cbxEsContribuyente";
            this.cbxEsContribuyente.Size = new System.Drawing.Size(205, 23);
            this.cbxEsContribuyente.TabIndex = 7;
            this.cbxEsContribuyente.SelectedIndexChanged += new System.EventHandler(this.FiltroRango_SelectedIndexChanged);
            // 
            // cbxTipoPlanilla
            // 
            this.cbxTipoPlanilla.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoPlanilla.Location = new System.Drawing.Point(160, 70);
            this.cbxTipoPlanilla.Name = "cbxTipoPlanilla";
            this.cbxTipoPlanilla.Size = new System.Drawing.Size(210, 23);
            this.cbxTipoPlanilla.TabIndex = 8;
            this.cbxTipoPlanilla.SelectedIndexChanged += new System.EventHandler(this.FiltroRango_SelectedIndexChanged);
            // 
            // cbxCatorcena
            // 
            this.cbxCatorcena.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCatorcena.Location = new System.Drawing.Point(492, 30);
            this.cbxCatorcena.Name = "cbxCatorcena";
            this.cbxCatorcena.Size = new System.Drawing.Size(205, 23);
            this.cbxCatorcena.TabIndex = 9;
            this.cbxCatorcena.SelectedIndexChanged += new System.EventHandler(this.FiltroRango_SelectedIndexChanged);
            // 
            // cbxZafra
            // 
            this.cbxZafra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxZafra.Location = new System.Drawing.Point(160, 30);
            this.cbxZafra.Name = "cbxZafra";
            this.cbxZafra.Size = new System.Drawing.Size(210, 23);
            this.cbxZafra.TabIndex = 10;
            this.cbxZafra.SelectedIndexChanged += new System.EventHandler(this.cbxZafra_SelectedIndexChanged);
            // 
            // labelPartida
            // 
            this.labelPartida.Location = new System.Drawing.Point(24, 223);
            this.labelPartida.Name = "labelPartida";
            this.labelPartida.Size = new System.Drawing.Size(130, 20);
            this.labelPartida.TabIndex = 11;
            this.labelPartida.Text = "Partida inicial *";
            // 
            // labelUltimo
            // 
            this.labelUltimo.Location = new System.Drawing.Point(392, 183);
            this.labelUltimo.Name = "labelUltimo";
            this.labelUltimo.Size = new System.Drawing.Size(94, 20);
            this.labelUltimo.TabIndex = 12;
            this.labelUltimo.Text = "Último cheque *";
            // 
            // labelPrimero
            // 
            this.labelPrimero.Location = new System.Drawing.Point(24, 183);
            this.labelPrimero.Name = "labelPrimero";
            this.labelPrimero.Size = new System.Drawing.Size(130, 20);
            this.labelPrimero.TabIndex = 13;
            this.labelPrimero.Text = "Primer cheque *";
            // 
            // labelCuenta
            // 
            this.labelCuenta.Location = new System.Drawing.Point(24, 113);
            this.labelCuenta.Name = "labelCuenta";
            this.labelCuenta.Size = new System.Drawing.Size(130, 20);
            this.labelCuenta.TabIndex = 14;
            this.labelCuenta.Text = "Cuenta bancaria *";
            // 
            // labelContribuyente
            // 
            this.labelContribuyente.Location = new System.Drawing.Point(392, 73);
            this.labelContribuyente.Name = "labelContribuyente";
            this.labelContribuyente.Size = new System.Drawing.Size(94, 20);
            this.labelContribuyente.TabIndex = 15;
            this.labelContribuyente.Text = "Contribuyente *";
            // 
            // labelTipo
            // 
            this.labelTipo.Location = new System.Drawing.Point(24, 73);
            this.labelTipo.Name = "labelTipo";
            this.labelTipo.Size = new System.Drawing.Size(130, 20);
            this.labelTipo.TabIndex = 16;
            this.labelTipo.Text = "Tipo de planilla *";
            // 
            // labelCatorcena
            // 
            this.labelCatorcena.Location = new System.Drawing.Point(392, 33);
            this.labelCatorcena.Name = "labelCatorcena";
            this.labelCatorcena.Size = new System.Drawing.Size(94, 20);
            this.labelCatorcena.TabIndex = 17;
            this.labelCatorcena.Text = "Catorcena *";
            // 
            // labelZafra
            // 
            this.labelZafra.Location = new System.Drawing.Point(24, 33);
            this.labelZafra.Name = "labelZafra";
            this.labelZafra.Size = new System.Drawing.Size(130, 20);
            this.labelZafra.TabIndex = 18;
            this.labelZafra.Text = "Zafra *";
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.Color.White;
            this.panelBotones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBotones.Controls.Add(this.lblAcciones);
            this.panelBotones.Controls.Add(this.btnSalir);
            this.panelBotones.Controls.Add(this.btnEliminarPruebas);
            this.panelBotones.Controls.Add(this.btnImprimirCheques);
            this.panelBotones.Controls.Add(this.btnVerReporte);
            this.panelBotones.Controls.Add(this.btnEmitir);
            this.panelBotones.Location = new System.Drawing.Point(752, 92);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(228, 270);
            this.panelBotones.TabIndex = 7;
            // 
            // lblAcciones
            // 
            this.lblAcciones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAcciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(121)))));
            this.lblAcciones.Location = new System.Drawing.Point(14, 13);
            this.lblAcciones.Name = "lblAcciones";
            this.lblAcciones.Size = new System.Drawing.Size(198, 20);
            this.lblAcciones.TabIndex = 4;
            this.lblAcciones.Text = "ACCIONES";
            // 
            // btnSalir
            // 
            this.btnSalir.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSalir.Appearance.Options.UseFont = true;
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnSalir.Location = new System.Drawing.Point(14, 219);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(198, 38);
            this.btnSalir.TabIndex = 0;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnEliminarPruebas
            // 
            this.btnEliminarPruebas.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEliminarPruebas.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnEliminarPruebas.Appearance.Options.UseFont = true;
            this.btnEliminarPruebas.Appearance.Options.UseForeColor = true;
            this.btnEliminarPruebas.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.eliminar32x32;
            this.btnEliminarPruebas.Location = new System.Drawing.Point(14, 175);
            this.btnEliminarPruebas.Name = "btnEliminarPruebas";
            this.btnEliminarPruebas.Size = new System.Drawing.Size(198, 38);
            this.btnEliminarPruebas.TabIndex = 1;
            this.btnEliminarPruebas.Text = "Borrar pruebas";
            this.btnEliminarPruebas.Click += new System.EventHandler(this.btnEliminarPruebas_Click);
            // 
            // btnImprimirCheques
            // 
            this.btnImprimirCheques.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnImprimirCheques.Appearance.Options.UseFont = true;
            this.btnImprimirCheques.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.imprimir32x32;
            this.btnImprimirCheques.Location = new System.Drawing.Point(14, 131);
            this.btnImprimirCheques.Name = "btnImprimirCheques";
            this.btnImprimirCheques.Size = new System.Drawing.Size(198, 38);
            this.btnImprimirCheques.TabIndex = 2;
            this.btnImprimirCheques.Text = "Imprimir cheques";
            this.btnImprimirCheques.Click += new System.EventHandler(this.btnImprimirCheques_Click);
            // 
            // btnVerReporte
            // 
            this.btnVerReporte.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVerReporte.Appearance.Options.UseFont = true;
            this.btnVerReporte.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.imprimir32x32;
            this.btnVerReporte.Location = new System.Drawing.Point(14, 87);
            this.btnVerReporte.Name = "btnVerReporte";
            this.btnVerReporte.Size = new System.Drawing.Size(198, 38);
            this.btnVerReporte.TabIndex = 3;
            this.btnVerReporte.Text = "Ver reporte";
            this.btnVerReporte.Click += new System.EventHandler(this.btnVerReporte_Click);
            // 
            // btnEmitir
            // 
            this.btnEmitir.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.btnEmitir.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEmitir.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnEmitir.Appearance.Options.UseBackColor = true;
            this.btnEmitir.Appearance.Options.UseFont = true;
            this.btnEmitir.Appearance.Options.UseForeColor = true;
            this.btnEmitir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.validar3_32x32;
            this.btnEmitir.Location = new System.Drawing.Point(14, 43);
            this.btnEmitir.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.btnEmitir.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnEmitir.Name = "btnEmitir";
            this.btnEmitir.Size = new System.Drawing.Size(198, 38);
            this.btnEmitir.TabIndex = 4;
            this.btnEmitir.Text = "Emitir cheques";
            this.btnEmitir.Click += new System.EventHandler(this.btnEmitir_Click);
            // 
            // panelProgreso
            // 
            this.panelProgreso.BackColor = System.Drawing.Color.White;
            this.panelProgreso.Controls.Add(this.lblEstadoProceso);
            this.panelProgreso.Controls.Add(this.progressGeneracion);
            this.panelProgreso.Location = new System.Drawing.Point(20, 420);
            this.panelProgreso.Name = "panelProgreso";
            this.panelProgreso.Size = new System.Drawing.Size(960, 40);
            this.panelProgreso.TabIndex = 6;
            // 
            // lblEstadoProceso
            // 
            this.lblEstadoProceso.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstadoProceso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(121)))));
            this.lblEstadoProceso.Location = new System.Drawing.Point(0, 0);
            this.lblEstadoProceso.Name = "lblEstadoProceso";
            this.lblEstadoProceso.Size = new System.Drawing.Size(255, 36);
            this.lblEstadoProceso.TabIndex = 0;
            this.lblEstadoProceso.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressGeneracion
            // 
            this.progressGeneracion.Location = new System.Drawing.Point(260, 8);
            this.progressGeneracion.MarqueeAnimationSpeed = 25;
            this.progressGeneracion.Name = "progressGeneracion";
            this.progressGeneracion.Size = new System.Drawing.Size(700, 22);
            this.progressGeneracion.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressGeneracion.TabIndex = 1;
            // 
            // grpResultados
            // 
            this.grpResultados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpResultados.BackColor = System.Drawing.Color.White;
            this.grpResultados.Controls.Add(this.gridCheques);
            this.grpResultados.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpResultados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.grpResultados.Location = new System.Drawing.Point(12, 420);
            this.grpResultados.Name = "grpResultados";
            this.grpResultados.Padding = new System.Windows.Forms.Padding(12);
            this.grpResultados.Size = new System.Drawing.Size(976, 468);
            this.grpResultados.TabIndex = 5;
            this.grpResultados.TabStop = false;
            this.grpResultados.Text = "  Cheques generados — 0 registro(s)  ";
            // 
            // gridCheques
            // 
            this.gridCheques.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCheques.Location = new System.Drawing.Point(12, 28);
            this.gridCheques.MainView = this.gvCheques;
            this.gridCheques.Name = "gridCheques";
            this.gridCheques.Size = new System.Drawing.Size(952, 247);
            this.gridCheques.TabIndex = 0;
            this.gridCheques.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCheques});
            // 
            // gvCheques
            // 
            this.gvCheques.GridControl = this.gridCheques;
            this.gvCheques.Name = "gvCheques";
            // 
            // lblAyuda
            // 
            this.lblAyuda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(225)))));
            this.lblAyuda.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(84)))), ((int)(((byte)(0)))));
            this.lblAyuda.Location = new System.Drawing.Point(20, 372);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Padding = new System.Windows.Forms.Padding(12, 9, 8, 0);
            this.lblAyuda.Size = new System.Drawing.Size(960, 38);
            this.lblAyuda.TabIndex = 8;
            this.lblAyuda.Text = "Sugerencia: en Cuenta bancaria escriba * y presione Enter para seleccionar la cue" +
    "nta.";
            // 
            // frmEmisionChequesPlanilla
            // 
            this.AcceptButton = this.btnEmitir;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(1000, 900);
            this.Controls.Add(this.grpResultados);
            this.Controls.Add(this.panelProgreso);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.lblAyuda);
            this.Controls.Add(this.grpDatos);
            this.Controls.Add(this.panelEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1000, 760);
            this.Name = "frmEmisionChequesPlanilla";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Consulta";
            this.Text = "Emisión de cheques de planilla";
            this.Load += new System.EventHandler(this.frmEmisionChequesPlanilla_Load);
            this.panelEncabezado.ResumeLayout(false);
            this.panelEncabezado.PerformLayout();
            this.grpDatos.ResumeLayout(false);
            this.grpDatos.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.panelProgreso.ResumeLayout(false);
            this.grpResultados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCheques)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCheques)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.GroupBox grpDatos;
        private System.Windows.Forms.Panel panelEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblSeccionCheques;
        private System.Windows.Forms.Label lblCantidadCheques;
        private System.Windows.Forms.Label lineaSeccion;
        private System.Windows.Forms.ComboBox cbxZafra;
        private System.Windows.Forms.ComboBox cbxCatorcena;
        private System.Windows.Forms.ComboBox cbxTipoPlanilla;
        private System.Windows.Forms.ComboBox cbxEsContribuyente;
        private System.Windows.Forms.TextBox txtCuenta;
        private System.Windows.Forms.TextBox txtNombreCuenta;
        private System.Windows.Forms.TextBox txtPrimerCheque;
        private System.Windows.Forms.TextBox txtUltimoCheque;
        private System.Windows.Forms.TextBox txtNumPartida;
        private System.Windows.Forms.Label labelZafra;
        private System.Windows.Forms.Label labelCatorcena;
        private System.Windows.Forms.Label labelTipo;
        private System.Windows.Forms.Label labelContribuyente;
        private System.Windows.Forms.Label labelCuenta;
        private System.Windows.Forms.Label labelPrimero;
        private System.Windows.Forms.Label labelUltimo;
        private System.Windows.Forms.Label labelPartida;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Label lblAcciones;
        private DevExpress.XtraEditors.SimpleButton btnEmitir;
        private DevExpress.XtraEditors.SimpleButton btnVerReporte;
        private DevExpress.XtraEditors.SimpleButton btnImprimirCheques;
        private DevExpress.XtraEditors.SimpleButton btnEliminarPruebas;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private System.Windows.Forms.Panel panelProgreso;
        private System.Windows.Forms.Label lblEstadoProceso;
        private System.Windows.Forms.ProgressBar progressGeneracion;
        private System.Windows.Forms.GroupBox grpResultados;
        private DevExpress.XtraGrid.GridControl gridCheques;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCheques;
        private System.Windows.Forms.Label lblAyuda;
    }
}
