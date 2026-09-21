namespace SistemaContable.UI.Forms.Ventas
{
    partial class frmConsultaSolicitudAgricola
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.layoutPrincipal = new System.Windows.Forms.TableLayoutPanel();
            this.tabTiposSolicitud = new System.Windows.Forms.TabControl();
            this.tabProductor = new System.Windows.Forms.TabPage();
            this.tabTransportista = new System.Windows.Forms.TabPage();
            this.tabFrenteRoza = new System.Windows.Forms.TabPage();
            this.tabFrenteQuerqueo = new System.Windows.Forms.TabPage();
            this.lblSolicitudes = new System.Windows.Forms.Label();
            this.gridSolicitudes = new DevExpress.XtraGrid.GridControl();
            this.gridViewSolicitudes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.gridDetalle = new DevExpress.XtraGrid.GridControl();
            this.gridViewDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.lblEstado = new System.Windows.Forms.Label();
            this.btnCerrar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSeleccionar = new DevExpress.XtraEditors.SimpleButton();
            this.layoutPrincipal.SuspendLayout();
            this.tabTiposSolicitud.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSolicitudes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSolicitudes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetalle)).BeginInit();
            this.pnlAcciones.SuspendLayout();
            this.SuspendLayout();
            //
            // layoutPrincipal
            //
            this.layoutPrincipal.ColumnCount = 1;
            this.layoutPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPrincipal.Controls.Add(this.tabTiposSolicitud, 0, 0);
            this.layoutPrincipal.Controls.Add(this.lblSolicitudes, 0, 1);
            this.layoutPrincipal.Controls.Add(this.gridSolicitudes, 0, 2);
            this.layoutPrincipal.Controls.Add(this.lblDetalle, 0, 3);
            this.layoutPrincipal.Controls.Add(this.gridDetalle, 0, 4);
            this.layoutPrincipal.Controls.Add(this.pnlAcciones, 0, 5);
            this.layoutPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPrincipal.Location = new System.Drawing.Point(0, 0);
            this.layoutPrincipal.Name = "layoutPrincipal";
            this.layoutPrincipal.Padding = new System.Windows.Forms.Padding(10);
            this.layoutPrincipal.RowCount = 6;
            this.layoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.layoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.layoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.layoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.layoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.layoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.layoutPrincipal.Size = new System.Drawing.Size(1084, 661);
            this.layoutPrincipal.TabIndex = 0;
            //
            // tabTiposSolicitud
            //
            this.tabTiposSolicitud.Controls.Add(this.tabProductor);
            this.tabTiposSolicitud.Controls.Add(this.tabTransportista);
            this.tabTiposSolicitud.Controls.Add(this.tabFrenteRoza);
            this.tabTiposSolicitud.Controls.Add(this.tabFrenteQuerqueo);
            this.tabTiposSolicitud.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTiposSolicitud.Location = new System.Drawing.Point(13, 13);
            this.tabTiposSolicitud.Name = "tabTiposSolicitud";
            this.tabTiposSolicitud.SelectedIndex = 0;
            this.tabTiposSolicitud.Size = new System.Drawing.Size(1058, 34);
            this.tabTiposSolicitud.TabIndex = 0;
            this.tabTiposSolicitud.SelectedIndexChanged += new System.EventHandler(this.tabTiposSolicitud_SelectedIndexChanged);
            //
            // tabProductor
            //
            this.tabProductor.Location = new System.Drawing.Point(4, 24);
            this.tabProductor.Name = "tabProductor";
            this.tabProductor.Size = new System.Drawing.Size(1050, 6);
            this.tabProductor.TabIndex = 0;
            this.tabProductor.Text = "Productor";
            this.tabProductor.UseVisualStyleBackColor = true;
            //
            // tabTransportista
            //
            this.tabTransportista.Location = new System.Drawing.Point(4, 24);
            this.tabTransportista.Name = "tabTransportista";
            this.tabTransportista.Size = new System.Drawing.Size(1050, 6);
            this.tabTransportista.TabIndex = 1;
            this.tabTransportista.Text = "Transportista";
            this.tabTransportista.UseVisualStyleBackColor = true;
            //
            // tabFrenteRoza
            //
            this.tabFrenteRoza.Location = new System.Drawing.Point(4, 24);
            this.tabFrenteRoza.Name = "tabFrenteRoza";
            this.tabFrenteRoza.Size = new System.Drawing.Size(1050, 6);
            this.tabFrenteRoza.TabIndex = 2;
            this.tabFrenteRoza.Text = "Frente Roza";
            this.tabFrenteRoza.UseVisualStyleBackColor = true;
            //
            // tabFrenteQuerqueo
            //
            this.tabFrenteQuerqueo.Location = new System.Drawing.Point(4, 24);
            this.tabFrenteQuerqueo.Name = "tabFrenteQuerqueo";
            this.tabFrenteQuerqueo.Size = new System.Drawing.Size(1050, 6);
            this.tabFrenteQuerqueo.TabIndex = 3;
            this.tabFrenteQuerqueo.Text = "Frente Querqueo";
            this.tabFrenteQuerqueo.UseVisualStyleBackColor = true;
            //
            // lblSolicitudes
            //
            this.lblSolicitudes.AutoSize = true;
            this.lblSolicitudes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSolicitudes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSolicitudes.Location = new System.Drawing.Point(13, 50);
            this.lblSolicitudes.Name = "lblSolicitudes";
            this.lblSolicitudes.Size = new System.Drawing.Size(1058, 28);
            this.lblSolicitudes.TabIndex = 0;
            this.lblSolicitudes.Text = "Solicitudes agrícolas";
            this.lblSolicitudes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // gridSolicitudes
            //
            this.gridSolicitudes.AccessibleName = "Solicitudes agrícolas";
            this.gridSolicitudes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSolicitudes.Location = new System.Drawing.Point(13, 81);
            this.gridSolicitudes.MainView = this.gridViewSolicitudes;
            this.gridSolicitudes.Name = "gridSolicitudes";
            this.gridSolicitudes.Size = new System.Drawing.Size(1058, 264);
            this.gridSolicitudes.TabIndex = 1;
            this.gridSolicitudes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSolicitudes});
            //
            // gridViewSolicitudes
            //
            this.gridViewSolicitudes.GridControl = this.gridSolicitudes;
            this.gridViewSolicitudes.Name = "gridViewSolicitudes";
            this.gridViewSolicitudes.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewSolicitudes_FocusedRowChanged);
            this.gridViewSolicitudes.DoubleClick += new System.EventHandler(this.gridViewSolicitudes_DoubleClick);
            //
            // lblDetalle
            //
            this.lblDetalle.AutoSize = true;
            this.lblDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDetalle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetalle.Location = new System.Drawing.Point(13, 328);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(1058, 28);
            this.lblDetalle.TabIndex = 2;
            this.lblDetalle.Text = "Productos de la solicitud seleccionada";
            this.lblDetalle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // gridDetalle
            //
            this.gridDetalle.AccessibleName = "Productos de la solicitud seleccionada";
            this.gridDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDetalle.Location = new System.Drawing.Point(13, 359);
            this.gridDetalle.MainView = this.gridViewDetalle;
            this.gridDetalle.Name = "gridDetalle";
            this.gridDetalle.Size = new System.Drawing.Size(1058, 257);
            this.gridDetalle.TabIndex = 3;
            this.gridDetalle.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDetalle});
            //
            // gridViewDetalle
            //
            this.gridViewDetalle.GridControl = this.gridDetalle;
            this.gridViewDetalle.Name = "gridViewDetalle";
            //
            // pnlAcciones
            //
            this.pnlAcciones.Controls.Add(this.lblEstado);
            this.pnlAcciones.Controls.Add(this.btnCerrar);
            this.pnlAcciones.Controls.Add(this.btnSeleccionar);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAcciones.Location = new System.Drawing.Point(13, 602);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(1058, 46);
            this.pnlAcciones.TabIndex = 4;
            //
            // lblEstado
            //
            this.lblEstado.AutoEllipsis = true;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEstado.Location = new System.Drawing.Point(0, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(4, 0, 8, 0);
            this.lblEstado.Size = new System.Drawing.Size(808, 46);
            this.lblEstado.TabIndex = 0;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnCerrar
            //
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCerrar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.cerrarProducto32x32;
            this.btnCerrar.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCerrar.ImageOptions.ImageToTextIndent = 8;
            this.btnCerrar.Location = new System.Drawing.Point(808, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(110, 46);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.Text = "Cerrar";
            //
            // btnSeleccionar
            //
            this.btnSeleccionar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSeleccionar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.seleccionarProducto32x32;
            this.btnSeleccionar.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSeleccionar.ImageOptions.ImageToTextIndent = 8;
            this.btnSeleccionar.Location = new System.Drawing.Point(918, 0);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(140, 46);
            this.btnSeleccionar.TabIndex = 2;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            //
            // frmConsultaSolicitudAgricola
            //
            this.AcceptButton = this.btnSeleccionar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCerrar;
            this.ClientSize = new System.Drawing.Size(1320, 720);
            this.Controls.Add(this.layoutPrincipal);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "frmConsultaSolicitudAgricola";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Solicitudes agrícolas";
            this.Shown += new System.EventHandler(this.frmConsultaSolicitudAgricola_Shown);
            this.layoutPrincipal.ResumeLayout(false);
            this.layoutPrincipal.PerformLayout();
            this.tabTiposSolicitud.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSolicitudes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSolicitudes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetalle)).EndInit();
            this.pnlAcciones.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel layoutPrincipal;
        private System.Windows.Forms.TabControl tabTiposSolicitud;
        private System.Windows.Forms.TabPage tabProductor;
        private System.Windows.Forms.TabPage tabTransportista;
        private System.Windows.Forms.TabPage tabFrenteRoza;
        private System.Windows.Forms.TabPage tabFrenteQuerqueo;
        private System.Windows.Forms.Label lblSolicitudes;
        private DevExpress.XtraGrid.GridControl gridSolicitudes;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSolicitudes;
        private System.Windows.Forms.Label lblDetalle;
        private DevExpress.XtraGrid.GridControl gridDetalle;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDetalle;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.Label lblEstado;
        private DevExpress.XtraEditors.SimpleButton btnCerrar;
        private DevExpress.XtraEditors.SimpleButton btnSeleccionar;
    }
}
