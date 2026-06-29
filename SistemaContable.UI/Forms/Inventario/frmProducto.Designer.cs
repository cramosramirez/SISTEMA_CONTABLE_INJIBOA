namespace SistemaContable.UI.Forms.Inventario
{
    partial class frmProducto
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
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnNuevo = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnEliminar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
            this.grpRoles = new System.Windows.Forms.GroupBox();
            this.btnAgregarRol = new DevExpress.XtraEditors.SimpleButton();
            this.gridRoles = new DevExpress.XtraGrid.GridControl();
            this.gvRoles = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblCOD_REF = new System.Windows.Forms.Label();
            this.txtCOD_REF = new System.Windows.Forms.TextBox();
            this.lblValidacionCodigo = new System.Windows.Forms.Label();
            this.txtULTIMOPRECIOCOMPRA = new System.Windows.Forms.TextBox();
            this.lblDESCRIPCION = new System.Windows.Forms.Label();
            this.lblULTIMOPRECIOCOMPRA = new System.Windows.Forms.Label();
            this.txtDESCRIPCION = new System.Windows.Forms.TextBox();
            this.txtDESC_VENTA = new System.Windows.Forms.TextBox();
            this.lblCATEGORIA = new System.Windows.Forms.Label();
            this.lblDESC_VENTA = new System.Windows.Forms.Label();
            this.cbxCATEGORIA = new System.Windows.Forms.ComboBox();
            this.txtPRECIO = new System.Windows.Forms.TextBox();
            this.lblSUBCATEGORIA = new System.Windows.Forms.Label();
            this.lblPRECIO = new System.Windows.Forms.Label();
            this.cbxSUBCATEGORIA = new System.Windows.Forms.ComboBox();
            this.chkES_INVENTARIO = new DevExpress.XtraEditors.CheckEdit();
            this.lblPRESENTACION = new System.Windows.Forms.Label();
            this.chkES_NOSUJETA = new DevExpress.XtraEditors.CheckEdit();
            this.cbxPRESENTACION = new System.Windows.Forms.ComboBox();
            this.chkES_EXENTO = new DevExpress.XtraEditors.CheckEdit();
            this.lblTIPOITEM = new System.Windows.Forms.Label();
            this.chkESTADO = new DevExpress.XtraEditors.CheckEdit();
            this.cbxTIPOITEM = new System.Windows.Forms.ComboBox();
            this.lblCODTRIBUTO = new System.Windows.Forms.Label();
            this.txtCCT_INVENT = new System.Windows.Forms.TextBox();
            this.cbxCODTRIBUTO = new System.Windows.Forms.ComboBox();
            this.lblCCT_INVENT = new System.Windows.Forms.Label();
            this.lblUNIMEDIDA = new System.Windows.Forms.Label();
            this.cbxTPINGRESO = new System.Windows.Forms.ComboBox();
            this.cbxUNIMEDIDA = new System.Windows.Forms.ComboBox();
            this.lblTPINGRESO = new System.Windows.Forms.Label();
            this.lblTPOPERACION = new System.Windows.Forms.Label();
            this.cbxTPOPERACION = new System.Windows.Forms.ComboBox();
            this.grpRoles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkES_INVENTARIO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkES_NOSUJETA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkES_EXENTO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkESTADO.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBotones
            // 
            this.panelBotones.Location = new System.Drawing.Point(981, 173);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(127, 208);
            this.panelBotones.TabIndex = 0;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.Appearance.Options.UseFont = true;
            this.btnNuevo.Appearance.Options.UseTextOptions = true;
            this.btnNuevo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnNuevo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevo.Location = new System.Drawing.Point(998, 182);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(101, 47);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.TabStop = false;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.guardar2_32x32;
            this.btnGuardar.Location = new System.Drawing.Point(998, 228);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(101, 47);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.Appearance.Options.UseFont = true;
            this.btnEliminar.Appearance.Options.UseTextOptions = true;
            this.btnEliminar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnEliminar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.eliminar32x32;
            this.btnEliminar.Location = new System.Drawing.Point(998, 276);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(101, 47);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.TabStop = false;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnSalir.Appearance.Options.UseFont = true;
            this.btnSalir.Appearance.Options.UseTextOptions = true;
            this.btnSalir.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnSalir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnSalir.Location = new System.Drawing.Point(998, 325);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(101, 47);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.TabStop = false;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // grpRoles
            // 
            this.grpRoles.Controls.Add(this.btnAgregarRol);
            this.grpRoles.Controls.Add(this.gridRoles);
            this.grpRoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.grpRoles.Location = new System.Drawing.Point(10, 292);
            this.grpRoles.Name = "grpRoles";
            this.grpRoles.Size = new System.Drawing.Size(962, 363);
            this.grpRoles.TabIndex = 2;
            this.grpRoles.TabStop = false;
            this.grpRoles.Text = "Roles del Producto";
            // 
            // btnAgregarRol
            // 
            this.btnAgregarRol.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnAgregarRol.Appearance.Options.UseFont = true;
            this.btnAgregarRol.Appearance.Options.UseTextOptions = true;
            this.btnAgregarRol.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnAgregarRol.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnAgregarRol.Location = new System.Drawing.Point(10, 20);
            this.btnAgregarRol.Name = "btnAgregarRol";
            this.btnAgregarRol.Size = new System.Drawing.Size(120, 32);
            this.btnAgregarRol.TabIndex = 0;
            this.btnAgregarRol.TabStop = false;
            this.btnAgregarRol.Text = "Agregar Rol";
            this.btnAgregarRol.Click += new System.EventHandler(this.btnAgregarRol_Click);
            // 
            // gridRoles
            // 
            this.gridRoles.Location = new System.Drawing.Point(10, 60);
            this.gridRoles.MainView = this.gvRoles;
            this.gridRoles.Name = "gridRoles";
            this.gridRoles.Size = new System.Drawing.Size(946, 200);
            this.gridRoles.TabIndex = 1;
            this.gridRoles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRoles});
            // 
            // gvRoles
            // 
            this.gvRoles.GridControl = this.gridRoles;
            this.gvRoles.Name = "gvRoles";
            this.gvRoles.OptionsView.ShowGroupPanel = false;
            this.gvRoles.OptionsView.ShowIndicator = false;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.BackColor = System.Drawing.Color.Blue;
            this.groupControl1.AppearanceCaption.BackColor2 = System.Drawing.Color.Blue;
            this.groupControl1.AppearanceCaption.Options.UseBackColor = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.lblCOD_REF);
            this.groupControl1.Controls.Add(this.txtCOD_REF);
            this.groupControl1.Controls.Add(this.lblValidacionCodigo);
            this.groupControl1.Controls.Add(this.txtULTIMOPRECIOCOMPRA);
            this.groupControl1.Controls.Add(this.lblDESCRIPCION);
            this.groupControl1.Controls.Add(this.lblULTIMOPRECIOCOMPRA);
            this.groupControl1.Controls.Add(this.txtDESCRIPCION);
            this.groupControl1.Controls.Add(this.txtDESC_VENTA);
            this.groupControl1.Controls.Add(this.lblCATEGORIA);
            this.groupControl1.Controls.Add(this.lblDESC_VENTA);
            this.groupControl1.Controls.Add(this.cbxCATEGORIA);
            this.groupControl1.Controls.Add(this.txtPRECIO);
            this.groupControl1.Controls.Add(this.lblSUBCATEGORIA);
            this.groupControl1.Controls.Add(this.lblPRECIO);
            this.groupControl1.Controls.Add(this.cbxSUBCATEGORIA);
            this.groupControl1.Controls.Add(this.chkES_INVENTARIO);
            this.groupControl1.Controls.Add(this.lblPRESENTACION);
            this.groupControl1.Controls.Add(this.chkES_NOSUJETA);
            this.groupControl1.Controls.Add(this.cbxPRESENTACION);
            this.groupControl1.Controls.Add(this.chkES_EXENTO);
            this.groupControl1.Controls.Add(this.lblTIPOITEM);
            this.groupControl1.Controls.Add(this.chkESTADO);
            this.groupControl1.Controls.Add(this.cbxTIPOITEM);
            this.groupControl1.Controls.Add(this.lblCODTRIBUTO);
            this.groupControl1.Controls.Add(this.txtCCT_INVENT);
            this.groupControl1.Controls.Add(this.cbxCODTRIBUTO);
            this.groupControl1.Controls.Add(this.lblCCT_INVENT);
            this.groupControl1.Controls.Add(this.lblUNIMEDIDA);
            this.groupControl1.Controls.Add(this.cbxTPINGRESO);
            this.groupControl1.Controls.Add(this.cbxUNIMEDIDA);
            this.groupControl1.Controls.Add(this.lblTPINGRESO);
            this.groupControl1.Controls.Add(this.lblTPOPERACION);
            this.groupControl1.Controls.Add(this.cbxTPOPERACION);
            this.groupControl1.Location = new System.Drawing.Point(20, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(952, 271);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Datos del Producto";
            // 
            // lblCOD_REF
            // 
            this.lblCOD_REF.AutoSize = true;
            this.lblCOD_REF.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblCOD_REF.Location = new System.Drawing.Point(16, 34);
            this.lblCOD_REF.Name = "lblCOD_REF";
            this.lblCOD_REF.Size = new System.Drawing.Size(44, 14);
            this.lblCOD_REF.TabIndex = 0;
            this.lblCOD_REF.Text = "Código";
            // 
            // txtCOD_REF
            // 
            this.txtCOD_REF.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtCOD_REF.Location = new System.Drawing.Point(116, 31);
            this.txtCOD_REF.Name = "txtCOD_REF";
            this.txtCOD_REF.Size = new System.Drawing.Size(155, 22);
            this.txtCOD_REF.TabIndex = 1;
            this.txtCOD_REF.Leave += new System.EventHandler(this.txtCOD_REF_Leave);
            // 
            // lblValidacionCodigo
            // 
            this.lblValidacionCodigo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblValidacionCodigo.Location = new System.Drawing.Point(276, 30);
            this.lblValidacionCodigo.Name = "lblValidacionCodigo";
            this.lblValidacionCodigo.Size = new System.Drawing.Size(35, 22);
            this.lblValidacionCodigo.TabIndex = 99;
            this.lblValidacionCodigo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtULTIMOPRECIOCOMPRA
            // 
            this.txtULTIMOPRECIOCOMPRA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtULTIMOPRECIOCOMPRA.Location = new System.Drawing.Point(656, 243);
            this.txtULTIMOPRECIOCOMPRA.Name = "txtULTIMOPRECIOCOMPRA";
            this.txtULTIMOPRECIOCOMPRA.Size = new System.Drawing.Size(160, 22);
            this.txtULTIMOPRECIOCOMPRA.TabIndex = 32;
            this.txtULTIMOPRECIOCOMPRA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDESCRIPCION
            // 
            this.lblDESCRIPCION.AutoSize = true;
            this.lblDESCRIPCION.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblDESCRIPCION.Location = new System.Drawing.Point(466, 36);
            this.lblDESCRIPCION.Name = "lblDESCRIPCION";
            this.lblDESCRIPCION.Size = new System.Drawing.Size(68, 14);
            this.lblDESCRIPCION.TabIndex = 2;
            this.lblDESCRIPCION.Text = "Descripción";
            // 
            // lblULTIMOPRECIOCOMPRA
            // 
            this.lblULTIMOPRECIOCOMPRA.AutoSize = true;
            this.lblULTIMOPRECIOCOMPRA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblULTIMOPRECIOCOMPRA.Location = new System.Drawing.Point(526, 246);
            this.lblULTIMOPRECIOCOMPRA.Name = "lblULTIMOPRECIOCOMPRA";
            this.lblULTIMOPRECIOCOMPRA.Size = new System.Drawing.Size(86, 14);
            this.lblULTIMOPRECIOCOMPRA.TabIndex = 31;
            this.lblULTIMOPRECIOCOMPRA.Text = "Últ. P. Compra";
            // 
            // txtDESCRIPCION
            // 
            this.txtDESCRIPCION.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtDESCRIPCION.Location = new System.Drawing.Point(540, 34);
            this.txtDESCRIPCION.Name = "txtDESCRIPCION";
            this.txtDESCRIPCION.Size = new System.Drawing.Size(391, 22);
            this.txtDESCRIPCION.TabIndex = 3;
            // 
            // txtDESC_VENTA
            // 
            this.txtDESC_VENTA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtDESC_VENTA.Location = new System.Drawing.Point(381, 243);
            this.txtDESC_VENTA.Name = "txtDESC_VENTA";
            this.txtDESC_VENTA.Size = new System.Drawing.Size(120, 22);
            this.txtDESC_VENTA.TabIndex = 30;
            this.txtDESC_VENTA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCATEGORIA
            // 
            this.lblCATEGORIA.AutoSize = true;
            this.lblCATEGORIA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblCATEGORIA.Location = new System.Drawing.Point(16, 64);
            this.lblCATEGORIA.Name = "lblCATEGORIA";
            this.lblCATEGORIA.Size = new System.Drawing.Size(58, 14);
            this.lblCATEGORIA.TabIndex = 4;
            this.lblCATEGORIA.Text = "Categoría";
            // 
            // lblDESC_VENTA
            // 
            this.lblDESC_VENTA.AutoSize = true;
            this.lblDESC_VENTA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblDESC_VENTA.Location = new System.Drawing.Point(256, 246);
            this.lblDESC_VENTA.Name = "lblDESC_VENTA";
            this.lblDESC_VENTA.Size = new System.Drawing.Size(90, 14);
            this.lblDESC_VENTA.TabIndex = 29;
            this.lblDESC_VENTA.Text = "% Desc. Venta";
            // 
            // cbxCATEGORIA
            // 
            this.cbxCATEGORIA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCATEGORIA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.cbxCATEGORIA.FormattingEnabled = true;
            this.cbxCATEGORIA.Location = new System.Drawing.Point(116, 61);
            this.cbxCATEGORIA.Name = "cbxCATEGORIA";
            this.cbxCATEGORIA.Size = new System.Drawing.Size(280, 22);
            this.cbxCATEGORIA.TabIndex = 5;
            this.cbxCATEGORIA.SelectedIndexChanged += new System.EventHandler(this.cbxCATEGORIA_SelectedIndexChanged);
            // 
            // txtPRECIO
            // 
            this.txtPRECIO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtPRECIO.Location = new System.Drawing.Point(96, 243);
            this.txtPRECIO.Name = "txtPRECIO";
            this.txtPRECIO.Size = new System.Drawing.Size(140, 22);
            this.txtPRECIO.TabIndex = 28;
            this.txtPRECIO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSUBCATEGORIA
            // 
            this.lblSUBCATEGORIA.AutoSize = true;
            this.lblSUBCATEGORIA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblSUBCATEGORIA.Location = new System.Drawing.Point(456, 68);
            this.lblSUBCATEGORIA.Name = "lblSUBCATEGORIA";
            this.lblSUBCATEGORIA.Size = new System.Drawing.Size(78, 14);
            this.lblSUBCATEGORIA.TabIndex = 6;
            this.lblSUBCATEGORIA.Text = "Subcategoría";
            // 
            // lblPRECIO
            // 
            this.lblPRECIO.AutoSize = true;
            this.lblPRECIO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblPRECIO.Location = new System.Drawing.Point(16, 246);
            this.lblPRECIO.Name = "lblPRECIO";
            this.lblPRECIO.Size = new System.Drawing.Size(40, 14);
            this.lblPRECIO.TabIndex = 27;
            this.lblPRECIO.Text = "Precio";
            // 
            // cbxSUBCATEGORIA
            // 
            this.cbxSUBCATEGORIA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSUBCATEGORIA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.cbxSUBCATEGORIA.FormattingEnabled = true;
            this.cbxSUBCATEGORIA.Location = new System.Drawing.Point(540, 62);
            this.cbxSUBCATEGORIA.Name = "cbxSUBCATEGORIA";
            this.cbxSUBCATEGORIA.Size = new System.Drawing.Size(391, 22);
            this.cbxSUBCATEGORIA.TabIndex = 7;
            // 
            // chkES_INVENTARIO
            // 
            this.chkES_INVENTARIO.Location = new System.Drawing.Point(251, 214);
            this.chkES_INVENTARIO.Name = "chkES_INVENTARIO";
            this.chkES_INVENTARIO.Properties.Caption = "Maneja Inventario";
            this.chkES_INVENTARIO.Size = new System.Drawing.Size(140, 20);
            this.chkES_INVENTARIO.TabIndex = 26;
            // 
            // lblPRESENTACION
            // 
            this.lblPRESENTACION.AutoSize = true;
            this.lblPRESENTACION.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblPRESENTACION.Location = new System.Drawing.Point(16, 94);
            this.lblPRESENTACION.Name = "lblPRESENTACION";
            this.lblPRESENTACION.Size = new System.Drawing.Size(77, 14);
            this.lblPRESENTACION.TabIndex = 8;
            this.lblPRESENTACION.Text = "Presentación";
            // 
            // chkES_NOSUJETA
            // 
            this.chkES_NOSUJETA.Location = new System.Drawing.Point(126, 214);
            this.chkES_NOSUJETA.Name = "chkES_NOSUJETA";
            this.chkES_NOSUJETA.Properties.Caption = "Es No Sujeta";
            this.chkES_NOSUJETA.Size = new System.Drawing.Size(110, 20);
            this.chkES_NOSUJETA.TabIndex = 25;
            // 
            // cbxPRESENTACION
            // 
            this.cbxPRESENTACION.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPRESENTACION.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.cbxPRESENTACION.FormattingEnabled = true;
            this.cbxPRESENTACION.Location = new System.Drawing.Point(116, 91);
            this.cbxPRESENTACION.Name = "cbxPRESENTACION";
            this.cbxPRESENTACION.Size = new System.Drawing.Size(280, 22);
            this.cbxPRESENTACION.TabIndex = 9;
            // 
            // chkES_EXENTO
            // 
            this.chkES_EXENTO.Location = new System.Drawing.Point(16, 214);
            this.chkES_EXENTO.Name = "chkES_EXENTO";
            this.chkES_EXENTO.Properties.Caption = "Es Exento";
            this.chkES_EXENTO.Size = new System.Drawing.Size(95, 20);
            this.chkES_EXENTO.TabIndex = 24;
            // 
            // lblTIPOITEM
            // 
            this.lblTIPOITEM.AutoSize = true;
            this.lblTIPOITEM.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblTIPOITEM.Location = new System.Drawing.Point(473, 101);
            this.lblTIPOITEM.Name = "lblTIPOITEM";
            this.lblTIPOITEM.Size = new System.Drawing.Size(61, 14);
            this.lblTIPOITEM.TabIndex = 10;
            this.lblTIPOITEM.Text = "Tipo Item";
            // 
            // chkESTADO
            // 
            this.chkESTADO.Location = new System.Drawing.Point(291, 181);
            this.chkESTADO.Name = "chkESTADO";
            this.chkESTADO.Properties.Caption = "Activo";
            this.chkESTADO.Size = new System.Drawing.Size(150, 20);
            this.chkESTADO.TabIndex = 22;
            // 
            // cbxTIPOITEM
            // 
            this.cbxTIPOITEM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTIPOITEM.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.cbxTIPOITEM.FormattingEnabled = true;
            this.cbxTIPOITEM.Location = new System.Drawing.Point(540, 90);
            this.cbxTIPOITEM.Name = "cbxTIPOITEM";
            this.cbxTIPOITEM.Size = new System.Drawing.Size(280, 22);
            this.cbxTIPOITEM.TabIndex = 11;
            // 
            // lblCODTRIBUTO
            // 
            this.lblCODTRIBUTO.AutoSize = true;
            this.lblCODTRIBUTO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblCODTRIBUTO.Location = new System.Drawing.Point(16, 124);
            this.lblCODTRIBUTO.Name = "lblCODTRIBUTO";
            this.lblCODTRIBUTO.Size = new System.Drawing.Size(47, 14);
            this.lblCODTRIBUTO.TabIndex = 12;
            this.lblCODTRIBUTO.Text = "Tributo";
            // 
            // txtCCT_INVENT
            // 
            this.txtCCT_INVENT.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtCCT_INVENT.Location = new System.Drawing.Point(116, 181);
            this.txtCCT_INVENT.Name = "txtCCT_INVENT";
            this.txtCCT_INVENT.Size = new System.Drawing.Size(150, 22);
            this.txtCCT_INVENT.TabIndex = 21;
            // 
            // cbxCODTRIBUTO
            // 
            this.cbxCODTRIBUTO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCODTRIBUTO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.cbxCODTRIBUTO.FormattingEnabled = true;
            this.cbxCODTRIBUTO.Location = new System.Drawing.Point(116, 121);
            this.cbxCODTRIBUTO.Name = "cbxCODTRIBUTO";
            this.cbxCODTRIBUTO.Size = new System.Drawing.Size(280, 22);
            this.cbxCODTRIBUTO.TabIndex = 13;
            // 
            // lblCCT_INVENT
            // 
            this.lblCCT_INVENT.AutoSize = true;
            this.lblCCT_INVENT.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblCCT_INVENT.Location = new System.Drawing.Point(16, 184);
            this.lblCCT_INVENT.Name = "lblCCT_INVENT";
            this.lblCCT_INVENT.Size = new System.Drawing.Size(73, 14);
            this.lblCCT_INVENT.TabIndex = 20;
            this.lblCCT_INVENT.Text = "Cta. Invent.";
            // 
            // lblUNIMEDIDA
            // 
            this.lblUNIMEDIDA.AutoSize = true;
            this.lblUNIMEDIDA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblUNIMEDIDA.Location = new System.Drawing.Point(448, 123);
            this.lblUNIMEDIDA.Name = "lblUNIMEDIDA";
            this.lblUNIMEDIDA.Size = new System.Drawing.Size(86, 14);
            this.lblUNIMEDIDA.TabIndex = 14;
            this.lblUNIMEDIDA.Text = "Unidad Medida";
            // 
            // cbxTPINGRESO
            // 
            this.cbxTPINGRESO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTPINGRESO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.cbxTPINGRESO.FormattingEnabled = true;
            this.cbxTPINGRESO.Location = new System.Drawing.Point(540, 151);
            this.cbxTPINGRESO.Name = "cbxTPINGRESO";
            this.cbxTPINGRESO.Size = new System.Drawing.Size(360, 22);
            this.cbxTPINGRESO.TabIndex = 19;
            // 
            // cbxUNIMEDIDA
            // 
            this.cbxUNIMEDIDA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUNIMEDIDA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.cbxUNIMEDIDA.FormattingEnabled = true;
            this.cbxUNIMEDIDA.Location = new System.Drawing.Point(540, 120);
            this.cbxUNIMEDIDA.Name = "cbxUNIMEDIDA";
            this.cbxUNIMEDIDA.Size = new System.Drawing.Size(280, 22);
            this.cbxUNIMEDIDA.TabIndex = 15;
            // 
            // lblTPINGRESO
            // 
            this.lblTPINGRESO.AutoSize = true;
            this.lblTPINGRESO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblTPINGRESO.Location = new System.Drawing.Point(463, 154);
            this.lblTPINGRESO.Name = "lblTPINGRESO";
            this.lblTPINGRESO.Size = new System.Drawing.Size(71, 14);
            this.lblTPINGRESO.TabIndex = 18;
            this.lblTPINGRESO.Text = "Tp. Ingreso";
            // 
            // lblTPOPERACION
            // 
            this.lblTPOPERACION.AutoSize = true;
            this.lblTPOPERACION.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblTPOPERACION.Location = new System.Drawing.Point(16, 154);
            this.lblTPOPERACION.Name = "lblTPOPERACION";
            this.lblTPOPERACION.Size = new System.Drawing.Size(85, 14);
            this.lblTPOPERACION.TabIndex = 16;
            this.lblTPOPERACION.Text = "Tp. Operación";
            // 
            // cbxTPOPERACION
            // 
            this.cbxTPOPERACION.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTPOPERACION.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.cbxTPOPERACION.FormattingEnabled = true;
            this.cbxTPOPERACION.Location = new System.Drawing.Point(116, 151);
            this.cbxTPOPERACION.Name = "cbxTPOPERACION";
            this.cbxTPOPERACION.Size = new System.Drawing.Size(307, 22);
            this.cbxTPOPERACION.TabIndex = 17;
            // 
            // frmProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 670);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.grpRoles);
            this.Controls.Add(this.panelBotones);
            this.Name = "frmProducto";
            this.Text = "Mantenimiento de Productos";
            this.Load += new System.EventHandler(this.frmProducto_Load);
            this.grpRoles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkES_INVENTARIO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkES_NOSUJETA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkES_EXENTO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkESTADO.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel panelBotones;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraEditors.SimpleButton btnEliminar;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private System.Windows.Forms.GroupBox grpRoles;
        private DevExpress.XtraEditors.SimpleButton btnAgregarRol;
        private DevExpress.XtraGrid.GridControl gridRoles;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRoles;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Label lblCOD_REF;
        private System.Windows.Forms.Label lblValidacionCodigo;
        private System.Windows.Forms.TextBox txtCOD_REF;
        private System.Windows.Forms.TextBox txtULTIMOPRECIOCOMPRA;
        private System.Windows.Forms.Label lblDESCRIPCION;
        private System.Windows.Forms.Label lblULTIMOPRECIOCOMPRA;
        private System.Windows.Forms.TextBox txtDESCRIPCION;
        private System.Windows.Forms.TextBox txtDESC_VENTA;
        private System.Windows.Forms.Label lblCATEGORIA;
        private System.Windows.Forms.Label lblDESC_VENTA;
        private System.Windows.Forms.ComboBox cbxCATEGORIA;
        private System.Windows.Forms.TextBox txtPRECIO;
        private System.Windows.Forms.Label lblSUBCATEGORIA;
        private System.Windows.Forms.Label lblPRECIO;
        private System.Windows.Forms.ComboBox cbxSUBCATEGORIA;
        private DevExpress.XtraEditors.CheckEdit chkES_INVENTARIO;
        private System.Windows.Forms.Label lblPRESENTACION;
        private DevExpress.XtraEditors.CheckEdit chkES_NOSUJETA;
        private System.Windows.Forms.ComboBox cbxPRESENTACION;
        private DevExpress.XtraEditors.CheckEdit chkES_EXENTO;
        private System.Windows.Forms.Label lblTIPOITEM;
        private DevExpress.XtraEditors.CheckEdit chkESTADO;
        private System.Windows.Forms.ComboBox cbxTIPOITEM;
        private System.Windows.Forms.Label lblCODTRIBUTO;
        private System.Windows.Forms.TextBox txtCCT_INVENT;
        private System.Windows.Forms.ComboBox cbxCODTRIBUTO;
        private System.Windows.Forms.Label lblCCT_INVENT;
        private System.Windows.Forms.Label lblUNIMEDIDA;
        private System.Windows.Forms.ComboBox cbxTPINGRESO;
        private System.Windows.Forms.ComboBox cbxUNIMEDIDA;
        private System.Windows.Forms.Label lblTPINGRESO;
        private System.Windows.Forms.Label lblTPOPERACION;
        private System.Windows.Forms.ComboBox cbxTPOPERACION;
    }
}