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
            this.grpTipoCliente = new System.Windows.Forms.GroupBox();
            this.btnAgregarTipoCliente = new DevExpress.XtraEditors.SimpleButton();
            this.gridTipoCliente = new DevExpress.XtraGrid.GridControl();
            this.gvTipoCliente = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.grpTipoCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTipoCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTipoCliente)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNuevo
            // 
            this.btnNuevo.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.Appearance.Options.UseFont = true;
            this.btnNuevo.Appearance.Options.UseTextOptions = true;
            this.btnNuevo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnNuevo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevo.Location = new System.Drawing.Point(475, 31);
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
            this.btnGuardar.Location = new System.Drawing.Point(475, 75);
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
            this.btnEliminar.Location = new System.Drawing.Point(475, 119);
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
            this.btnSalir.Location = new System.Drawing.Point(475, 163);
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
            this.groupControl1.Size = new System.Drawing.Size(454, 89);
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
            // grpTipoCliente
            // 
            this.grpTipoCliente.Controls.Add(this.btnAgregarTipoCliente);
            this.grpTipoCliente.Controls.Add(this.gridTipoCliente);
            this.grpTipoCliente.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.grpTipoCliente.Location = new System.Drawing.Point(8, 98);
            this.grpTipoCliente.Margin = new System.Windows.Forms.Padding(2);
            this.grpTipoCliente.Name = "grpTipoCliente";
            this.grpTipoCliente.Padding = new System.Windows.Forms.Padding(2);
            this.grpTipoCliente.Size = new System.Drawing.Size(454, 208);
            this.grpTipoCliente.TabIndex = 14;
            this.grpTipoCliente.TabStop = false;
            this.grpTipoCliente.Text = "Tipo Cliente Asociado";
            // 
            // btnAgregarTipoCliente
            // 
            this.btnAgregarTipoCliente.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnAgregarTipoCliente.Appearance.Options.UseFont = true;
            this.btnAgregarTipoCliente.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnAgregarTipoCliente.Location = new System.Drawing.Point(8, 16);
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
            this.gridTipoCliente.Location = new System.Drawing.Point(8, 51);
            this.gridTipoCliente.MainView = this.gvTipoCliente;
            this.gridTipoCliente.Margin = new System.Windows.Forms.Padding(2);
            this.gridTipoCliente.Name = "gridTipoCliente";
            this.gridTipoCliente.Size = new System.Drawing.Size(292, 138);
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
            // frmRol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 317);
            this.Controls.Add(this.grpTipoCliente);
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
            this.grpTipoCliente.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTipoCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTipoCliente)).EndInit();
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
        private System.Windows.Forms.GroupBox grpTipoCliente;
        private DevExpress.XtraEditors.SimpleButton btnAgregarTipoCliente;
        private DevExpress.XtraGrid.GridControl gridTipoCliente;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTipoCliente;
    }
}