namespace SistemaContable.UI.Forms.PartidasCatorcenaPag
{
    partial class frmPartidaCatorcenaPag
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.barraBotones = new DevExpress.XtraEditors.PanelControl();
            this.btnNuevo = new DevExpress.XtraEditors.SimpleButton();
            this.btnProcesar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
            this.lblTitulo = new DevExpress.XtraEditors.LabelControl();
            this.grpCriterios = new DevExpress.XtraEditors.GroupControl();
            this.cbEmpresa = new System.Windows.Forms.ComboBox();
            this.cbCatorcena = new System.Windows.Forms.ComboBox();
            this.txtCuenta = new DevExpress.XtraEditors.TextEdit();
            this.txtNombre = new DevExpress.XtraEditors.TextEdit();
            this.lblEmpresa = new DevExpress.XtraEditors.LabelControl();
            this.lblCatorcena = new DevExpress.XtraEditors.LabelControl();
            this.lblCuenta = new DevExpress.XtraEditors.LabelControl();
            this.lblNombre = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelTotales = new DevExpress.XtraEditors.PanelControl();
            this.lblTotalCargo = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalAbono = new DevExpress.XtraEditors.LabelControl();
            this.lblDiferencia = new DevExpress.XtraEditors.LabelControl();
            this.txtTotalCargo = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalAbono = new DevExpress.XtraEditors.TextEdit();
            this.txtDiferencia = new DevExpress.XtraEditors.TextEdit();
            this.lblEstado = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barraBotones)).BeginInit();
            this.barraBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpCriterios)).BeginInit();
            this.grpCriterios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuenta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTotales)).BeginInit();
            this.panelTotales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalCargo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAbono.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiferencia.Properties)).BeginInit();
            this.SuspendLayout();
            // barraBotones
            this.barraBotones.Controls.Add(this.btnNuevo);
            this.barraBotones.Controls.Add(this.btnProcesar);
            this.barraBotones.Controls.Add(this.btnSalir);
            this.barraBotones.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraBotones.Location = new System.Drawing.Point(0, 0);
            this.barraBotones.Name = "barraBotones";
            this.barraBotones.Size = new System.Drawing.Size(1040, 58);
            this.barraBotones.TabIndex = 0;
            // btnNuevo
            this.btnNuevo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevo.Location = new System.Drawing.Point(12, 8);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(105, 42);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // btnProcesar
            this.btnProcesar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.guardar2_32x32;
            this.btnProcesar.Location = new System.Drawing.Point(123, 8);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(125, 42);
            this.btnProcesar.TabIndex = 1;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // btnSalir
            this.btnSalir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnSalir.Location = new System.Drawing.Point(254, 8);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(105, 42);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // lblTitulo
            this.lblTitulo.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Appearance.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitulo.Appearance.Options.UseFont = true;
            this.lblTitulo.Appearance.Options.UseForeColor = true;
            this.lblTitulo.Location = new System.Drawing.Point(395, 70);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(251, 18);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Partida de pago por catorcena";
            // grpCriterios
            this.grpCriterios.Controls.Add(this.cbEmpresa);
            this.grpCriterios.Controls.Add(this.cbCatorcena);
            this.grpCriterios.Controls.Add(this.txtCuenta);
            this.grpCriterios.Controls.Add(this.txtNombre);
            this.grpCriterios.Controls.Add(this.lblEmpresa);
            this.grpCriterios.Controls.Add(this.lblCatorcena);
            this.grpCriterios.Controls.Add(this.lblCuenta);
            this.grpCriterios.Controls.Add(this.lblNombre);
            this.grpCriterios.Location = new System.Drawing.Point(12, 98);
            this.grpCriterios.Name = "grpCriterios";
            this.grpCriterios.Size = new System.Drawing.Size(1016, 120);
            this.grpCriterios.TabIndex = 2;
            this.grpCriterios.Text = "Criterios de generación";
            // cbEmpresa
            this.cbEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEmpresa.Enabled = false;
            this.cbEmpresa.FormattingEnabled = true;
            this.cbEmpresa.Location = new System.Drawing.Point(104, 37);
            this.cbEmpresa.Name = "cbEmpresa";
            this.cbEmpresa.Size = new System.Drawing.Size(350, 21);
            this.cbEmpresa.TabIndex = 0;
            this.cbEmpresa.SelectionChangeCommitted += new System.EventHandler(this.cbEmpresa_SelectionChangeCommitted);
            // cbCatorcena
            this.cbCatorcena.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCatorcena.FormattingEnabled = true;
            this.cbCatorcena.Location = new System.Drawing.Point(570, 37);
            this.cbCatorcena.Name = "cbCatorcena";
            this.cbCatorcena.Size = new System.Drawing.Size(420, 21);
            this.cbCatorcena.TabIndex = 1;
            // txtCuenta
            this.txtCuenta.Location = new System.Drawing.Point(104, 75);
            this.txtCuenta.Name = "txtCuenta";
            this.txtCuenta.Properties.MaxLength = 30;
            this.txtCuenta.Size = new System.Drawing.Size(220, 20);
            this.txtCuenta.TabIndex = 2;
            // txtNombre
            this.txtNombre.Location = new System.Drawing.Point(430, 75);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Properties.MaxLength = 160;
            this.txtNombre.Size = new System.Drawing.Size(560, 20);
            this.txtNombre.TabIndex = 3;
            // labels
            this.lblEmpresa.Location = new System.Drawing.Point(49, 40);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(49, 13);
            this.lblEmpresa.Text = "Empresa:*";
            this.lblCatorcena.Location = new System.Drawing.Point(499, 40);
            this.lblCatorcena.Name = "lblCatorcena";
            this.lblCatorcena.Size = new System.Drawing.Size(65, 13);
            this.lblCatorcena.Text = "Catorcena:*";
            this.lblCuenta.Location = new System.Drawing.Point(52, 78);
            this.lblCuenta.Name = "lblCuenta";
            this.lblCuenta.Size = new System.Drawing.Size(46, 13);
            this.lblCuenta.Text = "Cuenta:*";
            this.lblNombre.Location = new System.Drawing.Point(337, 78);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(87, 13);
            this.lblNombre.Text = "Contrapartida:*";
            // grid
            this.gridControl1.Location = new System.Drawing.Point(12, 230);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1016, 330);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridView1 });
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // panelTotales
            this.panelTotales.Controls.Add(this.lblTotalCargo);
            this.panelTotales.Controls.Add(this.lblTotalAbono);
            this.panelTotales.Controls.Add(this.lblDiferencia);
            this.panelTotales.Controls.Add(this.txtTotalCargo);
            this.panelTotales.Controls.Add(this.txtTotalAbono);
            this.panelTotales.Controls.Add(this.txtDiferencia);
            this.panelTotales.Location = new System.Drawing.Point(12, 566);
            this.panelTotales.Name = "panelTotales";
            this.panelTotales.Size = new System.Drawing.Size(1016, 54);
            this.panelTotales.TabIndex = 4;
            this.lblTotalCargo.Location = new System.Drawing.Point(434, 19);
            this.lblTotalCargo.Text = "Total cargo:";
            this.lblTotalAbono.Location = new System.Drawing.Point(634, 19);
            this.lblTotalAbono.Text = "Total abono:";
            this.lblDiferencia.Location = new System.Drawing.Point(833, 19);
            this.lblDiferencia.Text = "Diferencia:";
            this.txtTotalCargo.Location = new System.Drawing.Point(500, 16);
            this.txtTotalCargo.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTotalCargo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtTotalCargo.Properties.ReadOnly = true;
            this.txtTotalCargo.Size = new System.Drawing.Size(120, 20);
            this.txtTotalAbono.Location = new System.Drawing.Point(702, 16);
            this.txtTotalAbono.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTotalAbono.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtTotalAbono.Properties.ReadOnly = true;
            this.txtTotalAbono.Size = new System.Drawing.Size(120, 20);
            this.txtDiferencia.Location = new System.Drawing.Point(890, 16);
            this.txtDiferencia.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtDiferencia.Properties.Appearance.Options.UseFont = true;
            this.txtDiferencia.Properties.Appearance.Options.UseTextOptions = true;
            this.txtDiferencia.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtDiferencia.Properties.ReadOnly = true;
            this.txtDiferencia.Size = new System.Drawing.Size(110, 20);
            // lblEstado
            this.lblEstado.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblEstado.Appearance.Options.UseForeColor = true;
            this.lblEstado.Location = new System.Drawing.Point(15, 632);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(0, 13);
            this.lblEstado.TabIndex = 5;
            // form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 660);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.panelTotales);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.grpCriterios);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.barraBotones);
            this.MinimumSize = new System.Drawing.Size(1050, 700);
            this.Name = "frmPartidaCatorcenaPag";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Partida de pago por catorcena";
            ((System.ComponentModel.ISupportInitialize)(this.barraBotones)).EndInit();
            this.barraBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpCriterios)).EndInit();
            this.grpCriterios.ResumeLayout(false);
            this.grpCriterios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuenta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTotales)).EndInit();
            this.panelTotales.ResumeLayout(false);
            this.panelTotales.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalCargo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAbono.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiferencia.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private DevExpress.XtraEditors.PanelControl barraBotones;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private DevExpress.XtraEditors.SimpleButton btnProcesar;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private DevExpress.XtraEditors.LabelControl lblTitulo;
        private DevExpress.XtraEditors.GroupControl grpCriterios;
        private System.Windows.Forms.ComboBox cbEmpresa;
        private System.Windows.Forms.ComboBox cbCatorcena;
        private DevExpress.XtraEditors.TextEdit txtCuenta;
        private DevExpress.XtraEditors.TextEdit txtNombre;
        private DevExpress.XtraEditors.LabelControl lblEmpresa;
        private DevExpress.XtraEditors.LabelControl lblCatorcena;
        private DevExpress.XtraEditors.LabelControl lblCuenta;
        private DevExpress.XtraEditors.LabelControl lblNombre;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelTotales;
        private DevExpress.XtraEditors.LabelControl lblTotalCargo;
        private DevExpress.XtraEditors.LabelControl lblTotalAbono;
        private DevExpress.XtraEditors.LabelControl lblDiferencia;
        private DevExpress.XtraEditors.TextEdit txtTotalCargo;
        private DevExpress.XtraEditors.TextEdit txtTotalAbono;
        private DevExpress.XtraEditors.TextEdit txtDiferencia;
        private DevExpress.XtraEditors.LabelControl lblEstado;
    }
}
