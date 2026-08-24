namespace SistemaContable.UI.Forms.Inventario
{
    partial class frmConsultaProductoExistente
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
            this.components = new System.ComponentModel.Container();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.lblFiltro = new System.Windows.Forms.Label();
            this.gridProductos = new DevExpress.XtraGrid.GridControl();
            this.gridViewProductos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlPie = new System.Windows.Forms.Panel();
            this.lblEstado = new System.Windows.Forms.Label();
            this.btnRegistrarNuevo = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSeleccionar = new DevExpress.XtraEditors.SimpleButton();
            this.temporizadorBusqueda = new System.Windows.Forms.Timer(this.components);
            this.pnlBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProductos)).BeginInit();
            this.pnlPie.SuspendLayout();
            this.SuspendLayout();
            // 
            // temporizadorBusqueda
            // 
            this.temporizadorBusqueda.Interval = 350;
            this.temporizadorBusqueda.Tick += new System.EventHandler(this.temporizadorBusqueda_Tick);
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.Controls.Add(this.btnBuscar);
            this.pnlBusqueda.Controls.Add(this.txtFiltro);
            this.pnlBusqueda.Controls.Add(this.lblFiltro);
            this.pnlBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBusqueda.Location = new System.Drawing.Point(0, 0);
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.Padding = new System.Windows.Forms.Padding(16, 14, 16, 10);
            this.pnlBusqueda.Size = new System.Drawing.Size(930, 58);
            this.pnlBusqueda.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.BuscarProducto32x32;
            this.btnBuscar.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnBuscar.ImageOptions.ImageToTextIndent = 8;
            this.btnBuscar.Location = new System.Drawing.Point(576, 6);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(105, 47);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtFiltro
            // 
            this.txtFiltro.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFiltro.Location = new System.Drawing.Point(133, 16);
            this.txtFiltro.MaxLength = 100;
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(428, 23);
            this.txtFiltro.TabIndex = 1;
            this.txtFiltro.TextChanged += new System.EventHandler(this.txtFiltro_TextChanged);
            this.txtFiltro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFiltro_KeyDown);
            // 
            // lblFiltro
            // 
            this.lblFiltro.AutoSize = true;
            this.lblFiltro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFiltro.Location = new System.Drawing.Point(16, 19);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(111, 15);
            this.lblFiltro.TabIndex = 0;
            this.lblFiltro.Text = "Buscar descripción:";
            // 
            // gridProductos
            // 
            this.gridProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProductos.Location = new System.Drawing.Point(0, 58);
            this.gridProductos.MainView = this.gridViewProductos;
            this.gridProductos.Name = "gridProductos";
            this.gridProductos.Size = new System.Drawing.Size(930, 427);
            this.gridProductos.TabIndex = 1;
            this.gridProductos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewProductos});
            // 
            // gridViewProductos
            // 
            this.gridViewProductos.GridControl = this.gridProductos;
            this.gridViewProductos.Name = "gridViewProductos";
            this.gridViewProductos.DoubleClick += new System.EventHandler(this.gridViewProductos_DoubleClick);
            // 
            // pnlPie
            // 
            this.pnlPie.Controls.Add(this.lblEstado);
            this.pnlPie.Controls.Add(this.btnRegistrarNuevo);
            this.pnlPie.Controls.Add(this.btnCancelar);
            this.pnlPie.Controls.Add(this.btnSeleccionar);
            this.pnlPie.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPie.Location = new System.Drawing.Point(0, 485);
            this.pnlPie.Name = "pnlPie";
            this.pnlPie.Size = new System.Drawing.Size(930, 62);
            this.pnlPie.TabIndex = 2;
            // 
            // lblEstado
            // 
            this.lblEstado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right) 
            | System.Windows.Forms.AnchorStyles.Top)));
            this.lblEstado.AutoEllipsis = true;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblEstado.Location = new System.Drawing.Point(16, 21);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(465, 22);
            this.lblEstado.TabIndex = 0;
            this.lblEstado.Text = "Seleccione un producto registrado.";
            // 
            // btnRegistrarNuevo
            // 
            this.btnRegistrarNuevo.AccessibleName = "Registrar un producto nuevo";
            this.btnRegistrarNuevo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegistrarNuevo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.registrarProducto32x32;
            this.btnRegistrarNuevo.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnRegistrarNuevo.ImageOptions.ImageToTextIndent = 8;
            this.btnRegistrarNuevo.Location = new System.Drawing.Point(499, 13);
            this.btnRegistrarNuevo.Name = "btnRegistrarNuevo";
            this.btnRegistrarNuevo.Size = new System.Drawing.Size(145, 36);
            this.btnRegistrarNuevo.TabIndex = 1;
            this.btnRegistrarNuevo.Text = "Registrar nuevo";
            this.btnRegistrarNuevo.Click += new System.EventHandler(this.btnRegistrarNuevo_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.cerrarProducto32x32;
            this.btnCancelar.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancelar.ImageOptions.ImageToTextIndent = 8;
            this.btnCancelar.Location = new System.Drawing.Point(658, 13);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120, 36);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeleccionar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.seleccionarProducto32x32;
            this.btnSeleccionar.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSeleccionar.ImageOptions.ImageToTextIndent = 8;
            this.btnSeleccionar.Location = new System.Drawing.Point(792, 13);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(120, 36);
            this.btnSeleccionar.TabIndex = 3;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // frmConsultaProductoExistente
            // 
            this.AcceptButton = this.btnBuscar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(930, 547);
            this.Controls.Add(this.gridProductos);
            this.Controls.Add(this.pnlPie);
            this.Controls.Add(this.pnlBusqueda);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(820, 480);
            this.Name = "frmConsultaProductoExistente";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Consulta de productos existentes";
            this.Shown += new System.EventHandler(this.frmConsultaProductoExistente_Shown);
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProductos)).EndInit();
            this.pnlPie.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBusqueda;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.Label lblFiltro;
        private DevExpress.XtraGrid.GridControl gridProductos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewProductos;
        private System.Windows.Forms.Panel pnlPie;
        private System.Windows.Forms.Label lblEstado;
        private DevExpress.XtraEditors.SimpleButton btnRegistrarNuevo;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.SimpleButton btnSeleccionar;
        private System.Windows.Forms.Timer temporizadorBusqueda;
    }
}
