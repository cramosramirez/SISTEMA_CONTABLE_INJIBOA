
namespace SistemaContable.UI.Forms.PartidasVentas
{
    partial class frmConsultaPartidaVenta : DevExpress.XtraEditors.XtraForm
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.deFecha = new DevExpress.XtraEditors.DateEdit();
            this.cb_Mes = new System.Windows.Forms.ComboBox();
            this.txt_Anio = new DevExpress.XtraEditors.TextEdit();
            this.cbTipoPartida = new System.Windows.Forms.ComboBox();
            this.lblPartida = new DevExpress.XtraEditors.LabelControl();
            this.lblFecha = new DevExpress.XtraEditors.LabelControl();
            this.lblCentroCosto = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.barraBotones)).BeginInit();
            this.barraBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpCriterios)).BeginInit();
            this.grpCriterios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deFecha.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFecha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Anio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
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
            this.barraBotones.Size = new System.Drawing.Size(1310, 55);
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
            this.btnProcesar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.buscar1_48x48;
            this.btnProcesar.Location = new System.Drawing.Point(110, 8);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(132, 41);
            this.btnProcesar.TabIndex = 1;
            this.btnProcesar.Text = "Ver Partidas";
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnSalir.Location = new System.Drawing.Point(248, 8);
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
            this.lblTitulo.Size = new System.Drawing.Size(204, 16);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Consulta de Partidas de Ventas";
            // 
            // grpCriterios
            // 
            this.grpCriterios.Controls.Add(this.labelControl1);
            this.grpCriterios.Controls.Add(this.deFecha);
            this.grpCriterios.Controls.Add(this.cb_Mes);
            this.grpCriterios.Controls.Add(this.txt_Anio);
            this.grpCriterios.Controls.Add(this.cbTipoPartida);
            this.grpCriterios.Controls.Add(this.lblPartida);
            this.grpCriterios.Controls.Add(this.lblFecha);
            this.grpCriterios.Controls.Add(this.lblCentroCosto);
            this.grpCriterios.Location = new System.Drawing.Point(12, 80);
            this.grpCriterios.Name = "grpCriterios";
            this.grpCriterios.Size = new System.Drawing.Size(876, 110);
            this.grpCriterios.TabIndex = 2;
            this.grpCriterios.Text = "Criterios de Consulta de Partida";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(443, 68);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(39, 13);
            this.labelControl1.TabIndex = 193;
            this.labelControl1.Text = "Fecha:*";
            // 
            // deFecha
            // 
            this.deFecha.EditValue = null;
            this.deFecha.Location = new System.Drawing.Point(488, 68);
            this.deFecha.Name = "deFecha";
            this.deFecha.Size = new System.Drawing.Size(143, 20);
            this.deFecha.TabIndex = 194;
            // 
            // cb_Mes
            // 
            this.cb_Mes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_Mes.FormattingEnabled = true;
            this.cb_Mes.Location = new System.Drawing.Point(248, 37);
            this.cb_Mes.Name = "cb_Mes";
            this.cb_Mes.Size = new System.Drawing.Size(180, 23);
            this.cb_Mes.TabIndex = 192;
            // 
            // txt_Anio
            // 
            this.txt_Anio.Location = new System.Drawing.Point(105, 37);
            this.txt_Anio.Name = "txt_Anio";
            this.txt_Anio.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_Anio.Properties.Appearance.Options.UseFont = true;
            this.txt_Anio.Size = new System.Drawing.Size(90, 22);
            this.txt_Anio.TabIndex = 191;
            // 
            // cbTipoPartida
            // 
            this.cbTipoPartida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoPartida.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTipoPartida.FormattingEnabled = true;
            this.cbTipoPartida.Location = new System.Drawing.Point(105, 66);
            this.cbTipoPartida.Name = "cbTipoPartida";
            this.cbTipoPartida.Size = new System.Drawing.Size(323, 22);
            this.cbTipoPartida.TabIndex = 190;
            this.cbTipoPartida.TextChanged += new System.EventHandler(this.cbTipoPartida_TextChanged);
            // 
            // lblPartida
            // 
            this.lblPartida.Location = new System.Drawing.Point(70, 36);
            this.lblPartida.Name = "lblPartida";
            this.lblPartida.Size = new System.Drawing.Size(29, 13);
            this.lblPartida.TabIndex = 0;
            this.lblPartida.Text = "Año:*";
            // 
            // lblFecha
            // 
            this.lblFecha.Location = new System.Drawing.Point(213, 41);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(29, 13);
            this.lblFecha.TabIndex = 2;
            this.lblFecha.Text = "Mes:*";
            // 
            // lblCentroCosto
            // 
            this.lblCentroCosto.Location = new System.Drawing.Point(32, 68);
            this.lblCentroCosto.Name = "lblCentroCosto";
            this.lblCentroCosto.Size = new System.Drawing.Size(67, 13);
            this.lblCentroCosto.TabIndex = 8;
            this.lblCentroCosto.Text = "Tipo Partida:*";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl1.Location = new System.Drawing.Point(0, 196);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1310, 324);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "Lista de Partidas";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 23);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1306, 299);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // frmConsultaPartidaVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1310, 520);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.grpCriterios);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.barraBotones);
            this.Name = "frmConsultaPartidaVenta";
            this.Text = "frmPartidaVenta";
            ((System.ComponentModel.ISupportInitialize)(this.barraBotones)).EndInit();
            this.barraBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpCriterios)).EndInit();
            this.grpCriterios.ResumeLayout(false);
            this.grpCriterios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deFecha.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFecha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Anio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
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

        private DevExpress.XtraEditors.LabelControl lblFecha;

        // NUEVO: combo de Centro de Costo
        private DevExpress.XtraEditors.LabelControl lblCentroCosto;
        private System.Windows.Forms.ComboBox cbTipoPartida;
        private System.Windows.Forms.ComboBox cb_Mes;
        private DevExpress.XtraEditors.TextEdit txt_Anio;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit deFecha;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
