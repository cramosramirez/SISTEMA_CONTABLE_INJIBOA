namespace SistemaContable.UI.Forms.Seguridad
{
    partial class frmRol
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
            this.btnNuevo = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnEliminar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblNOMBRE_ROL = new System.Windows.Forms.Label();
            this.txtNOMBRE_ROL = new System.Windows.Forms.TextBox();
            this.xtraTabAsociaciones = new DevExpress.XtraTab.XtraTabControl();
            this.tabTipoCliente = new DevExpress.XtraTab.XtraTabPage();
            this.btnAgregarTipoCliente = new DevExpress.XtraEditors.SimpleButton();
            this.gridTipoCliente = new DevExpress.XtraGrid.GridControl();
            this.gvTipoCliente = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabRolProducto = new DevExpress.XtraTab.XtraTabPage();
            this.btnAgregarRolProducto = new DevExpress.XtraEditors.SimpleButton();
            this.gridRolProducto = new DevExpress.XtraGrid.GridControl();
            this.gvRolProducto = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabCentroCosto = new DevExpress.XtraTab.XtraTabPage();
            this.btnAgregarCentroCosto = new DevExpress.XtraEditors.SimpleButton();
            this.gridCentroCosto = new DevExpress.XtraGrid.GridControl();
            this.gvCentroCosto = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grpPermisos = new DevExpress.XtraEditors.GroupControl();
            this.chkPermisoCtasContables = new DevExpress.XtraEditors.CheckEdit();
            this.chkPermisoTipoCliente = new DevExpress.XtraEditors.CheckEdit();
            this.chkPermisoCodClienteProveedorSigesta = new DevExpress.XtraEditors.CheckEdit();
            this.chkPermisoRolProducto = new DevExpress.XtraEditors.CheckEdit();
            this.chkPermisoAsocioProducto = new DevExpress.XtraEditors.CheckEdit();
            this.chkPermisoCodProductoSigesta = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grpPermisosProveedor = new DevExpress.XtraEditors.GroupControl();
            this.chkPermisoCtasContablesProveedor = new DevExpress.XtraEditors.CheckEdit();
            this.chkPermisoTipoProveedor = new DevExpress.XtraEditors.CheckEdit();
            this.chkPermisoCodProveedorSigesta = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabAsociaciones)).BeginInit();
            this.xtraTabAsociaciones.SuspendLayout();
            this.tabTipoCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTipoCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTipoCliente)).BeginInit();
            this.tabRolProducto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRolProducto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRolProducto)).BeginInit();
            this.tabCentroCosto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCentroCosto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCentroCosto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpPermisos)).BeginInit();
            this.grpPermisos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermisoCtasContables.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermisoTipoCliente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermisoCodClienteProveedorSigesta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermisoRolProducto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermisoAsocioProducto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermisoCodProductoSigesta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpPermisosProveedor)).BeginInit();
            this.grpPermisosProveedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermisoCtasContablesProveedor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermisoTipoProveedor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermisoCodProveedorSigesta.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNuevo
            // 
            this.btnNuevo.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.Appearance.Options.UseFont = true;
            this.btnNuevo.Appearance.Options.UseTextOptions = true;
            this.btnNuevo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnNuevo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevo.Location = new System.Drawing.Point(498, 31);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(2);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(94, 38);
            this.btnNuevo.TabIndex = 10;
            this.btnNuevo.TabStop = false;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.guardar2_32x32;
            this.btnGuardar.Location = new System.Drawing.Point(498, 75);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(94, 38);
            this.btnGuardar.TabIndex = 11;
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
            this.btnEliminar.Location = new System.Drawing.Point(498, 119);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(94, 38);
            this.btnEliminar.TabIndex = 12;
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
            this.btnSalir.Location = new System.Drawing.Point(498, 163);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(2);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(94, 38);
            this.btnSalir.TabIndex = 13;
            this.btnSalir.TabStop = false;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.BackColor = System.Drawing.Color.Blue;
            this.groupControl1.AppearanceCaption.BackColor2 = System.Drawing.Color.Blue;
            this.groupControl1.AppearanceCaption.Options.UseBackColor = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.lblNOMBRE_ROL);
            this.groupControl1.Controls.Add(this.txtNOMBRE_ROL);
            this.groupControl1.Location = new System.Drawing.Point(8, 2);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(484, 89);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Datos del Rol";
            // 
            // lblNOMBRE_ROL
            // 
            this.lblNOMBRE_ROL.AutoSize = true;
            this.lblNOMBRE_ROL.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblNOMBRE_ROL.Location = new System.Drawing.Point(15, 41);
            this.lblNOMBRE_ROL.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNOMBRE_ROL.Name = "lblNOMBRE_ROL";
            this.lblNOMBRE_ROL.Size = new System.Drawing.Size(50, 14);
            this.lblNOMBRE_ROL.TabIndex = 0;
            this.lblNOMBRE_ROL.Text = "Nombre";
            // 
            // txtNOMBRE_ROL
            // 
            this.txtNOMBRE_ROL.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtNOMBRE_ROL.Location = new System.Drawing.Point(75, 38);
            this.txtNOMBRE_ROL.Margin = new System.Windows.Forms.Padding(2);
            this.txtNOMBRE_ROL.MaxLength = 100;
            this.txtNOMBRE_ROL.Name = "txtNOMBRE_ROL";
            this.txtNOMBRE_ROL.Size = new System.Drawing.Size(358, 22);
            this.txtNOMBRE_ROL.TabIndex = 1;
            // 
            // xtraTabAsociaciones
            // 
            this.xtraTabAsociaciones.Location = new System.Drawing.Point(8, 98);
            this.xtraTabAsociaciones.Margin = new System.Windows.Forms.Padding(2);
            this.xtraTabAsociaciones.Name = "xtraTabAsociaciones";
            this.xtraTabAsociaciones.SelectedTabPage = this.tabTipoCliente;
            this.xtraTabAsociaciones.Size = new System.Drawing.Size(484, 220);
            this.xtraTabAsociaciones.TabIndex = 14;
            this.xtraTabAsociaciones.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabTipoCliente,
            this.tabRolProducto,
            this.tabCentroCosto});
            // 
            // tabTipoCliente
            // 
            this.tabTipoCliente.Controls.Add(this.btnAgregarTipoCliente);
            this.tabTipoCliente.Controls.Add(this.gridTipoCliente);
            this.tabTipoCliente.Name = "tabTipoCliente";
            this.tabTipoCliente.Size = new System.Drawing.Size(482, 195);
            this.tabTipoCliente.Text = "Tipo Cliente";
            // 
            // btnAgregarTipoCliente
            // 
            this.btnAgregarTipoCliente.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnAgregarTipoCliente.Appearance.Options.UseFont = true;
            this.btnAgregarTipoCliente.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnAgregarTipoCliente.Location = new System.Drawing.Point(8, 8);
            this.btnAgregarTipoCliente.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarTipoCliente.Name = "btnAgregarTipoCliente";
            this.btnAgregarTipoCliente.Size = new System.Drawing.Size(135, 31);
            this.btnAgregarTipoCliente.TabIndex = 0;
            this.btnAgregarTipoCliente.TabStop = false;
            this.btnAgregarTipoCliente.Text = "Agregar";
            this.btnAgregarTipoCliente.Click += new System.EventHandler(this.btnAgregarTipoCliente_Click);
            // 
            // gridTipoCliente
            // 
            this.gridTipoCliente.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridTipoCliente.Location = new System.Drawing.Point(8, 43);
            this.gridTipoCliente.MainView = this.gvTipoCliente;
            this.gridTipoCliente.Margin = new System.Windows.Forms.Padding(2);
            this.gridTipoCliente.Name = "gridTipoCliente";
            this.gridTipoCliente.Size = new System.Drawing.Size(436, 143);
            this.gridTipoCliente.TabIndex = 1;
            this.gridTipoCliente.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTipoCliente});
            // 
            // gvTipoCliente
            // 
            this.gvTipoCliente.DetailHeight = 284;
            this.gvTipoCliente.GridControl = this.gridTipoCliente;
            this.gvTipoCliente.Name = "gvTipoCliente";
            this.gvTipoCliente.OptionsView.ShowIndicator = false;
            // 
            // tabRolProducto
            // 
            this.tabRolProducto.Controls.Add(this.btnAgregarRolProducto);
            this.tabRolProducto.Controls.Add(this.gridRolProducto);
            this.tabRolProducto.Name = "tabRolProducto";
            this.tabRolProducto.Size = new System.Drawing.Size(482, 195);
            this.tabRolProducto.Text = "Rol de Producto";
            // 
            // btnAgregarRolProducto
            // 
            this.btnAgregarRolProducto.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnAgregarRolProducto.Appearance.Options.UseFont = true;
            this.btnAgregarRolProducto.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnAgregarRolProducto.Location = new System.Drawing.Point(8, 8);
            this.btnAgregarRolProducto.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarRolProducto.Name = "btnAgregarRolProducto";
            this.btnAgregarRolProducto.Size = new System.Drawing.Size(135, 31);
            this.btnAgregarRolProducto.TabIndex = 0;
            this.btnAgregarRolProducto.TabStop = false;
            this.btnAgregarRolProducto.Text = "Agregar";
            this.btnAgregarRolProducto.Click += new System.EventHandler(this.btnAgregarRolProducto_Click);
            // 
            // gridRolProducto
            // 
            this.gridRolProducto.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridRolProducto.Location = new System.Drawing.Point(8, 43);
            this.gridRolProducto.MainView = this.gvRolProducto;
            this.gridRolProducto.Margin = new System.Windows.Forms.Padding(2);
            this.gridRolProducto.Name = "gridRolProducto";
            this.gridRolProducto.Size = new System.Drawing.Size(436, 143);
            this.gridRolProducto.TabIndex = 1;
            this.gridRolProducto.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRolProducto});
            // 
            // gvRolProducto
            // 
            this.gvRolProducto.DetailHeight = 284;
            this.gvRolProducto.GridControl = this.gridRolProducto;
            this.gvRolProducto.Name = "gvRolProducto";
            this.gvRolProducto.OptionsView.ShowIndicator = false;
            // 
            // tabCentroCosto
            // 
            this.tabCentroCosto.Controls.Add(this.btnAgregarCentroCosto);
            this.tabCentroCosto.Controls.Add(this.gridCentroCosto);
            this.tabCentroCosto.Name = "tabCentroCosto";
            this.tabCentroCosto.Size = new System.Drawing.Size(482, 195);
            this.tabCentroCosto.Text = "Centro de Costo";
            // 
            // btnAgregarCentroCosto
            // 
            this.btnAgregarCentroCosto.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnAgregarCentroCosto.Appearance.Options.UseFont = true;
            this.btnAgregarCentroCosto.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnAgregarCentroCosto.Location = new System.Drawing.Point(8, 8);
            this.btnAgregarCentroCosto.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarCentroCosto.Name = "btnAgregarCentroCosto";
            this.btnAgregarCentroCosto.Size = new System.Drawing.Size(135, 31);
            this.btnAgregarCentroCosto.TabIndex = 0;
            this.btnAgregarCentroCosto.TabStop = false;
            this.btnAgregarCentroCosto.Text = "Agregar";
            this.btnAgregarCentroCosto.Click += new System.EventHandler(this.btnAgregarCentroCosto_Click);
            // 
            // gridCentroCosto
            // 
            this.gridCentroCosto.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridCentroCosto.Location = new System.Drawing.Point(8, 43);
            this.gridCentroCosto.MainView = this.gvCentroCosto;
            this.gridCentroCosto.Margin = new System.Windows.Forms.Padding(2);
            this.gridCentroCosto.Name = "gridCentroCosto";
            this.gridCentroCosto.Size = new System.Drawing.Size(436, 143);
            this.gridCentroCosto.TabIndex = 1;
            this.gridCentroCosto.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCentroCosto});
            // 
            // gvCentroCosto
            // 
            this.gvCentroCosto.DetailHeight = 284;
            this.gvCentroCosto.GridControl = this.gridCentroCosto;
            this.gvCentroCosto.Name = "gvCentroCosto";
            this.gvCentroCosto.OptionsView.ShowIndicator = false;
            // 
            // grpPermisos
            // 
            this.grpPermisos.Controls.Add(this.chkPermisoCtasContables);
            this.grpPermisos.Controls.Add(this.chkPermisoTipoCliente);
            this.grpPermisos.Controls.Add(this.chkPermisoCodClienteProveedorSigesta);
            this.grpPermisos.Location = new System.Drawing.Point(8, 322);
            this.grpPermisos.Margin = new System.Windows.Forms.Padding(2);
            this.grpPermisos.Name = "grpPermisos";
            this.grpPermisos.Size = new System.Drawing.Size(484, 106);
            this.grpPermisos.TabIndex = 15;
            this.grpPermisos.Text = "Permisos Clientes";
            // 
            // chkPermisoCtasContables
            // 
            this.chkPermisoCtasContables.Location = new System.Drawing.Point(15, 30);
            this.chkPermisoCtasContables.Margin = new System.Windows.Forms.Padding(2);
            this.chkPermisoCtasContables.Name = "chkPermisoCtasContables";
            this.chkPermisoCtasContables.Properties.Caption = "Edición Cuentas Contables";
            this.chkPermisoCtasContables.Size = new System.Drawing.Size(210, 20);
            this.chkPermisoCtasContables.TabIndex = 0;
            // 
            // chkPermisoTipoCliente
            // 
            this.chkPermisoTipoCliente.Location = new System.Drawing.Point(15, 58);
            this.chkPermisoTipoCliente.Margin = new System.Windows.Forms.Padding(2);
            this.chkPermisoTipoCliente.Name = "chkPermisoTipoCliente";
            this.chkPermisoTipoCliente.Properties.Caption = "Edición de Tipos de Clientes";
            this.chkPermisoTipoCliente.Size = new System.Drawing.Size(210, 20);
            this.chkPermisoTipoCliente.TabIndex = 1;
            // 
            // chkPermisoCodClienteProveedorSigesta
            // 
            this.chkPermisoCodClienteProveedorSigesta.Location = new System.Drawing.Point(15, 83);
            this.chkPermisoCodClienteProveedorSigesta.Margin = new System.Windows.Forms.Padding(2);
            this.chkPermisoCodClienteProveedorSigesta.Name = "chkPermisoCodClienteProveedorSigesta";
            this.chkPermisoCodClienteProveedorSigesta.Properties.Caption = "Edición Códigos Cliente Sigesta";
            this.chkPermisoCodClienteProveedorSigesta.Size = new System.Drawing.Size(233, 20);
            this.chkPermisoCodClienteProveedorSigesta.TabIndex = 4;
            // 
            // chkPermisoRolProducto
            // 
            this.chkPermisoRolProducto.Location = new System.Drawing.Point(15, 55);
            this.chkPermisoRolProducto.Margin = new System.Windows.Forms.Padding(2);
            this.chkPermisoRolProducto.Name = "chkPermisoRolProducto";
            this.chkPermisoRolProducto.Properties.Caption = "Edición Roles de Productos";
            this.chkPermisoRolProducto.Size = new System.Drawing.Size(210, 20);
            this.chkPermisoRolProducto.TabIndex = 2;
            // 
            // chkPermisoAsocioProducto
            // 
            this.chkPermisoAsocioProducto.Location = new System.Drawing.Point(15, 25);
            this.chkPermisoAsocioProducto.Margin = new System.Windows.Forms.Padding(2);
            this.chkPermisoAsocioProducto.Name = "chkPermisoAsocioProducto";
            this.chkPermisoAsocioProducto.Properties.Caption = "Edición Asocio de Productos";
            this.chkPermisoAsocioProducto.Size = new System.Drawing.Size(210, 20);
            this.chkPermisoAsocioProducto.TabIndex = 3;
            // 
            // chkPermisoCodProductoSigesta
            // 
            this.chkPermisoCodProductoSigesta.Location = new System.Drawing.Point(15, 79);
            this.chkPermisoCodProductoSigesta.Margin = new System.Windows.Forms.Padding(2);
            this.chkPermisoCodProductoSigesta.Name = "chkPermisoCodProductoSigesta";
            this.chkPermisoCodProductoSigesta.Properties.Caption = "Edición Códigos Productos Sigesta";
            this.chkPermisoCodProductoSigesta.Size = new System.Drawing.Size(210, 20);
            this.chkPermisoCodProductoSigesta.TabIndex = 5;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.chkPermisoAsocioProducto);
            this.groupControl2.Controls.Add(this.chkPermisoCodProductoSigesta);
            this.groupControl2.Controls.Add(this.chkPermisoRolProducto);
            this.groupControl2.Location = new System.Drawing.Point(8, 436);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(2);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(484, 103);
            this.groupControl2.TabIndex = 16;
            this.groupControl2.Text = "Permisos Productos";
            // 
            // grpPermisosProveedor
            // 
            this.grpPermisosProveedor.Controls.Add(this.chkPermisoCtasContablesProveedor);
            this.grpPermisosProveedor.Controls.Add(this.chkPermisoTipoProveedor);
            this.grpPermisosProveedor.Controls.Add(this.chkPermisoCodProveedorSigesta);
            this.grpPermisosProveedor.Location = new System.Drawing.Point(6, 546);
            this.grpPermisosProveedor.Margin = new System.Windows.Forms.Padding(2);
            this.grpPermisosProveedor.Name = "grpPermisosProveedor";
            this.grpPermisosProveedor.Size = new System.Drawing.Size(484, 115);
            this.grpPermisosProveedor.TabIndex = 17;
            this.grpPermisosProveedor.Text = "Permisos Proveedor";
            // 
            // chkPermisoCtasContablesProveedor
            // 
            this.chkPermisoCtasContablesProveedor.Location = new System.Drawing.Point(17, 25);
            this.chkPermisoCtasContablesProveedor.Margin = new System.Windows.Forms.Padding(2);
            this.chkPermisoCtasContablesProveedor.Name = "chkPermisoCtasContablesProveedor";
            this.chkPermisoCtasContablesProveedor.Properties.Caption = "Edición Cuentas Contables de Proveedor";
            this.chkPermisoCtasContablesProveedor.Size = new System.Drawing.Size(280, 20);
            this.chkPermisoCtasContablesProveedor.TabIndex = 0;
            // 
            // chkPermisoTipoProveedor
            // 
            this.chkPermisoTipoProveedor.Location = new System.Drawing.Point(17, 53);
            this.chkPermisoTipoProveedor.Margin = new System.Windows.Forms.Padding(2);
            this.chkPermisoTipoProveedor.Name = "chkPermisoTipoProveedor";
            this.chkPermisoTipoProveedor.Properties.Caption = "Edición de Tipos de Proveedor";
            this.chkPermisoTipoProveedor.Size = new System.Drawing.Size(240, 20);
            this.chkPermisoTipoProveedor.TabIndex = 1;
            // 
            // chkPermisoCodProveedorSigesta
            // 
            this.chkPermisoCodProveedorSigesta.Location = new System.Drawing.Point(17, 78);
            this.chkPermisoCodProveedorSigesta.Margin = new System.Windows.Forms.Padding(2);
            this.chkPermisoCodProveedorSigesta.Name = "chkPermisoCodProveedorSigesta";
            this.chkPermisoCodProveedorSigesta.Properties.Caption = "Edición Códigos de Proveedor SIGESTA";
            this.chkPermisoCodProveedorSigesta.Size = new System.Drawing.Size(280, 20);
            this.chkPermisoCodProveedorSigesta.TabIndex = 2;
            // 
            // frmRol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 703);
            this.Controls.Add(this.grpPermisosProveedor);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.grpPermisos);
            this.Controls.Add(this.xtraTabAsociaciones);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnSalir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRol";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rol";
            this.Load += new System.EventHandler(this.frmRol_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabAsociaciones)).EndInit();
            this.xtraTabAsociaciones.ResumeLayout(false);
            this.tabTipoCliente.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTipoCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTipoCliente)).EndInit();
            this.tabRolProducto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridRolProducto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRolProducto)).EndInit();
            this.tabCentroCosto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCentroCosto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCentroCosto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpPermisos)).EndInit();
            this.grpPermisos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkPermisoCtasContables.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermisoTipoCliente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermisoCodClienteProveedorSigesta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermisoRolProducto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermisoAsocioProducto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermisoCodProductoSigesta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpPermisosProveedor)).EndInit();
            this.grpPermisosProveedor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkPermisoCtasContablesProveedor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermisoTipoProveedor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermisoCodProveedorSigesta.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraEditors.SimpleButton btnEliminar;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Label lblNOMBRE_ROL;
        private System.Windows.Forms.TextBox txtNOMBRE_ROL;
        private DevExpress.XtraTab.XtraTabControl xtraTabAsociaciones;
        private DevExpress.XtraTab.XtraTabPage tabTipoCliente;
        private DevExpress.XtraEditors.SimpleButton btnAgregarTipoCliente;
        private DevExpress.XtraGrid.GridControl gridTipoCliente;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTipoCliente;
        private DevExpress.XtraTab.XtraTabPage tabCentroCosto;
        private DevExpress.XtraEditors.SimpleButton btnAgregarCentroCosto;
        private DevExpress.XtraGrid.GridControl gridCentroCosto;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCentroCosto;
        private DevExpress.XtraTab.XtraTabPage tabRolProducto;
        private DevExpress.XtraEditors.SimpleButton btnAgregarRolProducto;
        private DevExpress.XtraGrid.GridControl gridRolProducto;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRolProducto;
        private DevExpress.XtraEditors.GroupControl grpPermisos;
        private DevExpress.XtraEditors.CheckEdit chkPermisoCtasContables;
        private DevExpress.XtraEditors.CheckEdit chkPermisoTipoCliente;
        private DevExpress.XtraEditors.CheckEdit chkPermisoRolProducto;
        private DevExpress.XtraEditors.CheckEdit chkPermisoAsocioProducto;
        private DevExpress.XtraEditors.CheckEdit chkPermisoCodClienteProveedorSigesta;
        private DevExpress.XtraEditors.CheckEdit chkPermisoCodProductoSigesta;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl grpPermisosProveedor;
        private DevExpress.XtraEditors.CheckEdit chkPermisoCtasContablesProveedor;
        private DevExpress.XtraEditors.CheckEdit chkPermisoTipoProveedor;
        private DevExpress.XtraEditors.CheckEdit chkPermisoCodProveedorSigesta;
    }
}
