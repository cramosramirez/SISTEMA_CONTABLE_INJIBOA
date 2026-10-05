namespace SistemaContable.UI.Forms.Ventas.Movimientos
{
    partial class frmReporteVentaDiaria
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.picReporte = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.btnCerrar = new DevExpress.XtraEditors.SimpleButton();
            this.btnVistaPrevia = new DevExpress.XtraEditors.SimpleButton();
            this.grpCriterios = new System.Windows.Forms.GroupBox();
            this.cboCentroCosto = new System.Windows.Forms.ComboBox();
            this.lblCentroCosto = new System.Windows.Forms.Label();
            this.dteFechaHasta = new DevExpress.XtraEditors.DateEdit();
            this.lblFechaHasta = new System.Windows.Forms.Label();
            this.dteFechaDesde = new DevExpress.XtraEditors.DateEdit();
            this.lblFechaDesde = new System.Windows.Forms.Label();
            this.pnlContenido.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReporte)).BeginInit();
            this.pnlAcciones.SuspendLayout();
            this.grpCriterios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaHasta.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaHasta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaDesde.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaDesde.Properties)).BeginInit();
            this.SuspendLayout();
            this.pnlContenido.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.pnlContenido.Controls.Add(this.pnlAcciones);
            this.pnlContenido.Controls.Add(this.grpCriterios);
            this.pnlContenido.Controls.Add(this.pnlEncabezado);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Size = new System.Drawing.Size(640, 372);
            this.pnlContenido.TabIndex = 0;
            //
            // pnlEncabezado
            //
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(36, 99, 156);
            this.pnlEncabezado.Controls.Add(this.picReporte);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Controls.Add(this.lblDescripcion);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(640, 88);
            this.pnlEncabezado.TabIndex = 0;
            //
            // picReporte
            //
            this.picReporte.Image = global::SistemaContable.UI.Properties.Resources.VentasReportes48x48;
            this.picReporte.Location = new System.Drawing.Point(24, 20);
            this.picReporte.Name = "picReporte";
            this.picReporte.Size = new System.Drawing.Size(48, 48);
            this.picReporte.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picReporte.TabStop = false;
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(88, 16);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(223, 35);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Reporte de venta diaria";
            //
            // lblDescripcion
            //
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(224, 235, 245);
            this.lblDescripcion.Location = new System.Drawing.Point(91, 52);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(359, 20);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "Seleccione el período y el centro de costo que desea consultar.";
            //
            // pnlAcciones
            //
            this.pnlAcciones.BackColor = System.Drawing.Color.White;
            this.pnlAcciones.Controls.Add(this.btnCerrar);
            this.pnlAcciones.Controls.Add(this.btnVistaPrevia);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcciones.Location = new System.Drawing.Point(0, 301);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(640, 71);
            this.pnlAcciones.TabIndex = 2;
            this.btnCerrar.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.Appearance.Options.UseFont = true;
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnCerrar.Appearance.BorderColor = System.Drawing.Color.FromArgb(180, 188, 197);
            this.btnCerrar.Appearance.ForeColor = System.Drawing.Color.FromArgb(55, 65, 75);
            this.btnCerrar.Appearance.Options.UseBackColor = true;
            this.btnCerrar.Appearance.Options.UseBorderColor = true;
            this.btnCerrar.Appearance.Options.UseForeColor = true;
            this.btnCerrar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnCerrar.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCerrar.ImageOptions.ImageToTextIndent = 8;
            this.btnCerrar.Location = new System.Drawing.Point(467, 14);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(145, 44);
            this.btnCerrar.TabIndex = 4;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            this.btnVistaPrevia.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVistaPrevia.Appearance.BackColor = System.Drawing.Color.White;
            this.btnVistaPrevia.Appearance.BorderColor = System.Drawing.Color.FromArgb(36, 99, 156);
            this.btnVistaPrevia.Appearance.ForeColor = System.Drawing.Color.FromArgb(36, 99, 156);
            this.btnVistaPrevia.Appearance.Options.UseBackColor = true;
            this.btnVistaPrevia.Appearance.Options.UseBorderColor = true;
            this.btnVistaPrevia.Appearance.Options.UseForeColor = true;
            this.btnVistaPrevia.Appearance.Options.UseFont = true;
            this.btnVistaPrevia.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(36, 99, 156);
            this.btnVistaPrevia.AppearanceHovered.BorderColor = System.Drawing.Color.FromArgb(36, 99, 156);
            this.btnVistaPrevia.AppearanceHovered.ForeColor = System.Drawing.Color.White;
            this.btnVistaPrevia.AppearanceHovered.Options.UseBackColor = true;
            this.btnVistaPrevia.AppearanceHovered.Options.UseBorderColor = true;
            this.btnVistaPrevia.AppearanceHovered.Options.UseForeColor = true;
            this.btnVistaPrevia.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(26, 78, 124);
            this.btnVistaPrevia.AppearancePressed.BorderColor = System.Drawing.Color.FromArgb(26, 78, 124);
            this.btnVistaPrevia.AppearancePressed.ForeColor = System.Drawing.Color.White;
            this.btnVistaPrevia.AppearancePressed.Options.UseBackColor = true;
            this.btnVistaPrevia.AppearancePressed.Options.UseBorderColor = true;
            this.btnVistaPrevia.AppearancePressed.Options.UseForeColor = true;
            this.btnVistaPrevia.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.imprimir32x32;
            this.btnVistaPrevia.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnVistaPrevia.ImageOptions.ImageToTextIndent = 8;
            this.btnVistaPrevia.Location = new System.Drawing.Point(298, 14);
            this.btnVistaPrevia.Name = "btnVistaPrevia";
            this.btnVistaPrevia.Size = new System.Drawing.Size(157, 44);
            this.btnVistaPrevia.TabIndex = 3;
            this.btnVistaPrevia.Text = "Vista previa";
            this.btnVistaPrevia.Click += new System.EventHandler(this.btnVistaPrevia_Click);
            this.grpCriterios.Controls.Add(this.cboCentroCosto);
            this.grpCriterios.Controls.Add(this.lblCentroCosto);
            this.grpCriterios.Controls.Add(this.dteFechaHasta);
            this.grpCriterios.Controls.Add(this.lblFechaHasta);
            this.grpCriterios.Controls.Add(this.dteFechaDesde);
            this.grpCriterios.Controls.Add(this.lblFechaDesde);
            this.grpCriterios.BackColor = System.Drawing.Color.White;
            this.grpCriterios.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpCriterios.ForeColor = System.Drawing.Color.FromArgb(45, 55, 65);
            this.grpCriterios.Location = new System.Drawing.Point(24, 108);
            this.grpCriterios.Name = "grpCriterios";
            this.grpCriterios.Size = new System.Drawing.Size(588, 169);
            this.grpCriterios.TabIndex = 0;
            this.grpCriterios.TabStop = false;
            this.grpCriterios.Text = "Criterios del reporte";
            this.cboCentroCosto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCentroCosto.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboCentroCosto.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboCentroCosto.FormattingEnabled = true;
            this.cboCentroCosto.Location = new System.Drawing.Point(165, 108);
            this.cboCentroCosto.Name = "cboCentroCosto";
            this.cboCentroCosto.Size = new System.Drawing.Size(386, 29);
            this.cboCentroCosto.TabIndex = 2;
            this.lblCentroCosto.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCentroCosto.ForeColor = System.Drawing.Color.FromArgb(65, 75, 85);
            this.lblCentroCosto.Location = new System.Drawing.Point(27, 108);
            this.lblCentroCosto.Size = new System.Drawing.Size(130, 29);
            this.lblCentroCosto.Text = "Centro de costo:";
            this.lblCentroCosto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dteFechaHasta.EditValue = null;
            this.dteFechaHasta.EnterMoveNextControl = true;
            this.dteFechaHasta.Location = new System.Drawing.Point(421, 50);
            this.dteFechaHasta.Name = "dteFechaHasta";
            this.dteFechaHasta.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dteFechaHasta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.dteFechaHasta.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.dteFechaHasta.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dteFechaHasta.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteFechaHasta.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dteFechaHasta.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteFechaHasta.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dteFechaHasta.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dteFechaHasta.Properties.Appearance.Options.UseFont = true;
            this.dteFechaHasta.Size = new System.Drawing.Size(130, 26);
            this.dteFechaHasta.TabIndex = 1;
            this.lblFechaHasta.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFechaHasta.ForeColor = System.Drawing.Color.FromArgb(65, 75, 85);
            this.lblFechaHasta.Location = new System.Drawing.Point(337, 49);
            this.lblFechaHasta.Size = new System.Drawing.Size(83, 26);
            this.lblFechaHasta.Text = "Hasta:";
            this.lblFechaHasta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dteFechaDesde.EditValue = null;
            this.dteFechaDesde.EnterMoveNextControl = true;
            this.dteFechaDesde.Location = new System.Drawing.Point(165, 50);
            this.dteFechaDesde.Name = "dteFechaDesde";
            this.dteFechaDesde.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dteFechaDesde.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.dteFechaDesde.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.dteFechaDesde.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dteFechaDesde.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteFechaDesde.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dteFechaDesde.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteFechaDesde.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dteFechaDesde.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dteFechaDesde.Properties.Appearance.Options.UseFont = true;
            this.dteFechaDesde.Size = new System.Drawing.Size(130, 26);
            this.dteFechaDesde.TabIndex = 0;
            this.lblFechaDesde.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFechaDesde.ForeColor = System.Drawing.Color.FromArgb(65, 75, 85);
            this.lblFechaDesde.Location = new System.Drawing.Point(74, 49);
            this.lblFechaDesde.Size = new System.Drawing.Size(83, 26);
            this.lblFechaDesde.Text = "Desde:";
            this.lblFechaDesde.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AcceptButton = this.btnVistaPrevia;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCerrar;
            this.ClientSize = new System.Drawing.Size(640, 372);
            this.Controls.Add(this.pnlContenido);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReporteVentaDiaria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reporte de venta diaria";
            this.Load += new System.EventHandler(this.frmReporteVentaDiaria_Load);
            this.pnlContenido.ResumeLayout(false);
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReporte)).EndInit();
            this.pnlAcciones.ResumeLayout(false);
            this.grpCriterios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaHasta.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaHasta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaDesde.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaDesde.Properties)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.PictureBox picReporte;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.GroupBox grpCriterios;
        private DevExpress.XtraEditors.DateEdit dteFechaDesde;
        private DevExpress.XtraEditors.DateEdit dteFechaHasta;
        private System.Windows.Forms.ComboBox cboCentroCosto;
        private System.Windows.Forms.Label lblFechaDesde;
        private System.Windows.Forms.Label lblFechaHasta;
        private System.Windows.Forms.Label lblCentroCosto;
        private DevExpress.XtraEditors.SimpleButton btnVistaPrevia;
        private DevExpress.XtraEditors.SimpleButton btnCerrar;
    }
}
