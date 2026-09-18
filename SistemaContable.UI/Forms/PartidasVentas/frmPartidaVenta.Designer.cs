namespace SistemaContable.UI.Forms.PartidasVentas
{
    partial class frmPartidaVenta : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.barraBotones = new DevExpress.XtraEditors.PanelControl();
            this.btnNuevo = new DevExpress.XtraEditors.SimpleButton();
            this.btnProcesar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
            this.lblTitulo = new DevExpress.XtraEditors.LabelControl();
            this.grpCriterios = new DevExpress.XtraEditors.GroupControl();
            this.cbTipoPartida = new System.Windows.Forms.ComboBox();
            this.lblPartida = new DevExpress.XtraEditors.LabelControl();
            this.txtPartida = new DevExpress.XtraEditors.TextEdit();
            this.lblFecha = new DevExpress.XtraEditors.LabelControl();
            this.deFecha = new DevExpress.XtraEditors.DateEdit();
            this.lblNumero = new DevExpress.XtraEditors.LabelControl();
            this.txtNumero = new DevExpress.XtraEditors.TextEdit();
            this.lblConcepto = new DevExpress.XtraEditors.LabelControl();
            this.txtConcepto = new DevExpress.XtraEditors.TextEdit();
            this.lblCentroCosto = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barraBotones)).BeginInit();
            this.barraBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpCriterios)).BeginInit();
            this.grpCriterios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPartida.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFecha.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFecha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumero.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConcepto.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // barraBotones
            // 
            this.barraBotones.Controls.Add(this.btnNuevo);
            this.barraBotones.Controls.Add(this.btnProcesar);
            this.barraBotones.Controls.Add(this.btnSalir);
            this.barraBotones.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraBotones.Location = new System.Drawing.Point(0, 0);
            this.barraBotones.Name = "barraBotones";
            this.barraBotones.Size = new System.Drawing.Size(900, 55);
            this.barraBotones.TabIndex = 0;
            // 
            // btnNuevo
            // 
            this.btnNuevo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevo.Location = new System.Drawing.Point(10, 8);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(94, 41);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnProcesar
            // 
            this.btnProcesar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.guardar2_32x32;
            this.btnProcesar.Location = new System.Drawing.Point(110, 8);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(94, 41);
            this.btnProcesar.TabIndex = 1;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnSalir.Location = new System.Drawing.Point(210, 8);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(94, 41);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Appearance.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitulo.Appearance.Options.UseFont = true;
            this.lblTitulo.Appearance.Options.UseForeColor = true;
            this.lblTitulo.Location = new System.Drawing.Point(330, 60);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(151, 16);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Partida de Venta Diaria";
            // 
            // grpCriterios
            // 
            this.grpCriterios.Controls.Add(this.cbTipoPartida);
            this.grpCriterios.Controls.Add(this.lblPartida);
            this.grpCriterios.Controls.Add(this.txtPartida);
            this.grpCriterios.Controls.Add(this.lblFecha);
            this.grpCriterios.Controls.Add(this.deFecha);
            this.grpCriterios.Controls.Add(this.lblNumero);
            this.grpCriterios.Controls.Add(this.txtNumero);
            this.grpCriterios.Controls.Add(this.lblConcepto);
            this.grpCriterios.Controls.Add(this.txtConcepto);
            this.grpCriterios.Controls.Add(this.lblCentroCosto);
            this.grpCriterios.Location = new System.Drawing.Point(12, 80);
            this.grpCriterios.Name = "grpCriterios";
            this.grpCriterios.Size = new System.Drawing.Size(876, 142);
            this.grpCriterios.TabIndex = 2;
            this.grpCriterios.Text = "Criterios de Generacion de Partida";
            // 
            // cbTipoPartida
            // 
            this.cbTipoPartida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoPartida.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTipoPartida.FormattingEnabled = true;
            this.cbTipoPartida.Location = new System.Drawing.Point(112, 43);
            this.cbTipoPartida.Name = "cbTipoPartida";
            this.cbTipoPartida.Size = new System.Drawing.Size(266, 22);
            this.cbTipoPartida.TabIndex = 190;
            this.cbTipoPartida.TextChanged += new System.EventHandler(this.cbTipoPartida_TextChanged);
            // 
            // lblPartida
            // 
            this.lblPartida.Location = new System.Drawing.Point(62, 71);
            this.lblPartida.Name = "lblPartida";
            this.lblPartida.Size = new System.Drawing.Size(44, 13);
            this.lblPartida.TabIndex = 0;
            this.lblPartida.Text = "Partida:*";
            // 
            // txtPartida
            // 
            this.txtPartida.EditValue = "IN";
            this.txtPartida.Location = new System.Drawing.Point(112, 71);
            this.txtPartida.Name = "txtPartida";
            this.txtPartida.Properties.ReadOnly = true;
            this.txtPartida.Size = new System.Drawing.Size(71, 20);
            this.txtPartida.TabIndex = 1;
            // 
            // lblFecha
            // 
            this.lblFecha.Location = new System.Drawing.Point(190, 71);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(39, 13);
            this.lblFecha.TabIndex = 2;
            this.lblFecha.Text = "Fecha:*";
            // 
            // deFecha
            // 
            this.deFecha.EditValue = null;
            this.deFecha.Location = new System.Drawing.Point(235, 71);
            this.deFecha.Name = "deFecha";
            this.deFecha.Size = new System.Drawing.Size(143, 20);
            this.deFecha.TabIndex = 3;
            this.deFecha.EditValueChanged += new System.EventHandler(this.deFecha_EditValueChanged);
            // 
            // lblNumero
            // 
            this.lblNumero.Location = new System.Drawing.Point(392, 71);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(47, 13);
            this.lblNumero.TabIndex = 4;
            this.lblNumero.Text = "Número:*";
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(445, 71);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(110, 20);
            this.txtNumero.TabIndex = 5;
            // 
            // lblConcepto
            // 
            this.lblConcepto.Location = new System.Drawing.Point(50, 97);
            this.lblConcepto.Name = "lblConcepto";
            this.lblConcepto.Size = new System.Drawing.Size(56, 13);
            this.lblConcepto.TabIndex = 6;
            this.lblConcepto.Text = "Concepto:*";
            // 
            // txtConcepto
            // 
            this.txtConcepto.Location = new System.Drawing.Point(112, 97);
            this.txtConcepto.Name = "txtConcepto";
            this.txtConcepto.Size = new System.Drawing.Size(756, 20);
            this.txtConcepto.TabIndex = 7;
            // 
            // lblCentroCosto
            // 
            this.lblCentroCosto.Location = new System.Drawing.Point(17, 45);
            this.lblCentroCosto.Name = "lblCentroCosto";
            this.lblCentroCosto.Size = new System.Drawing.Size(89, 13);
            this.lblCentroCosto.TabIndex = 8;
            this.lblCentroCosto.Text = "Centro de Costo:*";
            // 
            // frmPartidaVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 242);
            this.Controls.Add(this.grpCriterios);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.barraBotones);
            this.Name = "frmPartidaVenta";
            this.Text = "frmPartidaVenta";
            ((System.ComponentModel.ISupportInitialize)(this.barraBotones)).EndInit();
            this.barraBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpCriterios)).EndInit();
            this.grpCriterios.ResumeLayout(false);
            this.grpCriterios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPartida.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFecha.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFecha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumero.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConcepto.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl barraBotones;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private DevExpress.XtraEditors.SimpleButton btnProcesar;
        private DevExpress.XtraEditors.SimpleButton btnSalir;

        private DevExpress.XtraEditors.LabelControl lblTitulo;

        private DevExpress.XtraEditors.GroupControl grpCriterios;

        private DevExpress.XtraEditors.LabelControl lblPartida;
        private DevExpress.XtraEditors.TextEdit txtPartida;

        private DevExpress.XtraEditors.LabelControl lblFecha;
        private DevExpress.XtraEditors.DateEdit deFecha;

        private DevExpress.XtraEditors.LabelControl lblNumero;
        private DevExpress.XtraEditors.TextEdit txtNumero;

        private DevExpress.XtraEditors.LabelControl lblConcepto;
        private DevExpress.XtraEditors.TextEdit txtConcepto;

        // NUEVO: combo de Centro de Costo
        private DevExpress.XtraEditors.LabelControl lblCentroCosto;
        private System.Windows.Forms.ComboBox cbTipoPartida;
    }
}
