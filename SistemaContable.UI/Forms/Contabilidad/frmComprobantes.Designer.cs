
namespace SistemaContable.UI.Forms.Contabilidad
{
    partial class frmComprobantes : DevExpress.XtraEditors.XtraForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmComprobantes));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.btSalir = new DevExpress.XtraEditors.SimpleButton();
            this.btNuevo = new DevExpress.XtraEditors.SimpleButton();
            this.lbMesAnio = new System.Windows.Forms.Label();
            this.btIgnorar = new DevExpress.XtraEditors.SimpleButton();
            this.btGrabar = new DevExpress.XtraEditors.SimpleButton();
            this.btEliminar = new DevExpress.XtraEditors.SimpleButton();
            this.btModificar = new DevExpress.XtraEditors.SimpleButton();
            this.btSiguiente = new DevExpress.XtraEditors.SimpleButton();
            this.btAnterior = new DevExpress.XtraEditors.SimpleButton();
            this.btImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.btSuspender = new DevExpress.XtraEditors.SimpleButton();
            this.btBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.lblDIFERENCIA = new System.Windows.Forms.Label();
            this.lblTOTAL_ABONO = new System.Windows.Forms.Label();
            this.lblTOTAL_CARGO = new System.Windows.Forms.Label();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btAdiconarItem = new DevExpress.XtraEditors.SimpleButton();
            this.lblCUADRE = new System.Windows.Forms.Label();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txtId = new DevExpress.XtraEditors.TextEdit();
            this.txtNID_PARTIDA = new DevExpress.XtraEditors.TextEdit();
            this.deFecha = new DevExpress.XtraEditors.DateEdit();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.txtTipoNombre = new System.Windows.Forms.TextBox();
            this.txtTipo = new System.Windows.Forms.TextBox();
            this.txtConcepto = new System.Windows.Forms.TextBox();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNID_PARTIDA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFecha.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFecha.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.groupControl4);
            this.groupControl1.Controls.Add(this.groupControl3);
            this.groupControl1.Controls.Add(this.groupControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1038, 611);
            this.groupControl1.TabIndex = 0;
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.btSalir);
            this.groupControl4.Controls.Add(this.btNuevo);
            this.groupControl4.Controls.Add(this.lbMesAnio);
            this.groupControl4.Controls.Add(this.btIgnorar);
            this.groupControl4.Controls.Add(this.btGrabar);
            this.groupControl4.Controls.Add(this.btEliminar);
            this.groupControl4.Controls.Add(this.btModificar);
            this.groupControl4.Controls.Add(this.btSiguiente);
            this.groupControl4.Controls.Add(this.btAnterior);
            this.groupControl4.Controls.Add(this.btImprimir);
            this.groupControl4.Controls.Add(this.btSuspender);
            this.groupControl4.Controls.Add(this.btBuscar);
            this.groupControl4.Controls.Add(this.lblDIFERENCIA);
            this.groupControl4.Controls.Add(this.lblTOTAL_ABONO);
            this.groupControl4.Controls.Add(this.lblTOTAL_CARGO);
            this.groupControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl4.Location = new System.Drawing.Point(2, 479);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(1034, 124);
            this.groupControl4.TabIndex = 2;
            // 
            // btSalir
            // 
            this.btSalir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btSalir.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btSalir.Location = new System.Drawing.Point(810, 50);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(60, 56);
            this.btSalir.TabIndex = 195;
            this.btSalir.Text = "Salir";
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // btNuevo
            // 
            this.btNuevo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btNuevo.ImageOptions.Image")));
            this.btNuevo.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btNuevo.Location = new System.Drawing.Point(744, 49);
            this.btNuevo.Name = "btNuevo";
            this.btNuevo.Size = new System.Drawing.Size(60, 56);
            this.btNuevo.TabIndex = 194;
            this.btNuevo.Text = "Nuevo";
            this.btNuevo.Click += new System.EventHandler(this.btNuevo_Click);
            // 
            // lbMesAnio
            // 
            this.lbMesAnio.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbMesAnio.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMesAnio.ForeColor = System.Drawing.Color.Yellow;
            this.lbMesAnio.Location = new System.Drawing.Point(22, 26);
            this.lbMesAnio.Name = "lbMesAnio";
            this.lbMesAnio.Size = new System.Drawing.Size(196, 21);
            this.lbMesAnio.TabIndex = 193;
            this.lbMesAnio.Text = "-";
            this.lbMesAnio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btIgnorar
            // 
            this.btIgnorar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btIgnorar.ImageOptions.Image")));
            this.btIgnorar.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btIgnorar.Location = new System.Drawing.Point(653, 50);
            this.btIgnorar.Name = "btIgnorar";
            this.btIgnorar.Size = new System.Drawing.Size(60, 56);
            this.btIgnorar.TabIndex = 31;
            this.btIgnorar.Text = "Ignorar";
            this.btIgnorar.Click += new System.EventHandler(this.btIgnorar_Click);
            // 
            // btGrabar
            // 
            this.btGrabar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(115)))), ((int)(((byte)(70)))));
            this.btGrabar.Appearance.Options.UseBackColor = true;
            this.btGrabar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btGrabar.ImageOptions.Image")));
            this.btGrabar.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btGrabar.Location = new System.Drawing.Point(587, 50);
            this.btGrabar.Name = "btGrabar";
            this.btGrabar.Size = new System.Drawing.Size(60, 56);
            this.btGrabar.TabIndex = 30;
            this.btGrabar.Text = "Grabar";
            this.btGrabar.Click += new System.EventHandler(this.btGrabar_Click);
            // 
            // btEliminar
            // 
            this.btEliminar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btEliminar.Appearance.Options.UseBackColor = true;
            this.btEliminar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btEliminar.ImageOptions.Image")));
            this.btEliminar.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btEliminar.Location = new System.Drawing.Point(521, 50);
            this.btEliminar.Name = "btEliminar";
            this.btEliminar.Size = new System.Drawing.Size(60, 56);
            this.btEliminar.TabIndex = 29;
            this.btEliminar.Text = "Eliminar";
            this.btEliminar.Click += new System.EventHandler(this.btEliminar_Click);
            // 
            // btModificar
            // 
            this.btModificar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btModificar.ImageOptions.Image")));
            this.btModificar.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btModificar.Location = new System.Drawing.Point(455, 50);
            this.btModificar.Name = "btModificar";
            this.btModificar.Size = new System.Drawing.Size(60, 56);
            this.btModificar.TabIndex = 28;
            this.btModificar.Text = "Modificar";
            this.btModificar.Click += new System.EventHandler(this.btModificar_Click);
            // 
            // btSiguiente
            // 
            this.btSiguiente.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btSiguiente.ImageOptions.Image")));
            this.btSiguiente.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btSiguiente.Location = new System.Drawing.Point(389, 50);
            this.btSiguiente.Name = "btSiguiente";
            this.btSiguiente.Size = new System.Drawing.Size(60, 56);
            this.btSiguiente.TabIndex = 27;
            this.btSiguiente.Text = "Siguente";
            this.btSiguiente.Click += new System.EventHandler(this.btSiguiente_Click);
            // 
            // btAnterior
            // 
            this.btAnterior.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btAnterior.ImageOptions.Image")));
            this.btAnterior.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btAnterior.Location = new System.Drawing.Point(323, 50);
            this.btAnterior.Name = "btAnterior";
            this.btAnterior.Size = new System.Drawing.Size(60, 56);
            this.btAnterior.TabIndex = 26;
            this.btAnterior.Text = "Anterior";
            this.btAnterior.Click += new System.EventHandler(this.btAnterior_Click);
            // 
            // btImprimir
            // 
            this.btImprimir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btImprimir.ImageOptions.Image")));
            this.btImprimir.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btImprimir.Location = new System.Drawing.Point(158, 50);
            this.btImprimir.Name = "btImprimir";
            this.btImprimir.Size = new System.Drawing.Size(60, 56);
            this.btImprimir.TabIndex = 25;
            this.btImprimir.Text = "Imprimir";
            this.btImprimir.Click += new System.EventHandler(this.btImprimir_Click);
            // 
            // btSuspender
            // 
            this.btSuspender.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btSuspender.ImageOptions.Image")));
            this.btSuspender.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btSuspender.Location = new System.Drawing.Point(92, 50);
            this.btSuspender.Name = "btSuspender";
            this.btSuspender.Size = new System.Drawing.Size(60, 56);
            this.btSuspender.TabIndex = 24;
            this.btSuspender.Text = "Suspender";
            this.btSuspender.Click += new System.EventHandler(this.btSuspender_Click);
            // 
            // btBuscar
            // 
            this.btBuscar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btBuscar.ImageOptions.Image")));
            this.btBuscar.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btBuscar.Location = new System.Drawing.Point(26, 50);
            this.btBuscar.Name = "btBuscar";
            this.btBuscar.Size = new System.Drawing.Size(60, 56);
            this.btBuscar.TabIndex = 23;
            this.btBuscar.Text = "Buscar";
            this.btBuscar.Click += new System.EventHandler(this.btBuscar_Click);
            // 
            // lblDIFERENCIA
            // 
            this.lblDIFERENCIA.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDIFERENCIA.Location = new System.Drawing.Point(876, 50);
            this.lblDIFERENCIA.Name = "lblDIFERENCIA";
            this.lblDIFERENCIA.Size = new System.Drawing.Size(158, 23);
            this.lblDIFERENCIA.TabIndex = 22;
            this.lblDIFERENCIA.Text = "0.00";
            this.lblDIFERENCIA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTOTAL_ABONO
            // 
            this.lblTOTAL_ABONO.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTOTAL_ABONO.Location = new System.Drawing.Point(917, 23);
            this.lblTOTAL_ABONO.Name = "lblTOTAL_ABONO";
            this.lblTOTAL_ABONO.Size = new System.Drawing.Size(117, 23);
            this.lblTOTAL_ABONO.TabIndex = 21;
            this.lblTOTAL_ABONO.Text = "0.00";
            this.lblTOTAL_ABONO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTOTAL_CARGO
            // 
            this.lblTOTAL_CARGO.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTOTAL_CARGO.Location = new System.Drawing.Point(772, 23);
            this.lblTOTAL_CARGO.Name = "lblTOTAL_CARGO";
            this.lblTOTAL_CARGO.Size = new System.Drawing.Size(143, 23);
            this.lblTOTAL_CARGO.TabIndex = 20;
            this.lblTOTAL_CARGO.Text = "0.00";
            this.lblTOTAL_CARGO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.btAdiconarItem);
            this.groupControl3.Controls.Add(this.lblCUADRE);
            this.groupControl3.Controls.Add(this.gridControl1);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl3.Location = new System.Drawing.Point(2, 137);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(1034, 342);
            this.groupControl3.TabIndex = 1;
            // 
            // btAdiconarItem
            // 
            this.btAdiconarItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btAdiconarItem.ImageOptions.Image")));
            this.btAdiconarItem.Location = new System.Drawing.Point(2, 53);
            this.btAdiconarItem.Name = "btAdiconarItem";
            this.btAdiconarItem.Size = new System.Drawing.Size(22, 27);
            this.btAdiconarItem.TabIndex = 24;
            this.btAdiconarItem.ToolTip = "Adicinar Item";
            this.btAdiconarItem.Click += new System.EventHandler(this.btAdiconarItem_Click);
            // 
            // lblCUADRE
            // 
            this.lblCUADRE.BackColor = System.Drawing.Color.Transparent;
            this.lblCUADRE.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCUADRE.Location = new System.Drawing.Point(261, 0);
            this.lblCUADRE.Name = "lblCUADRE";
            this.lblCUADRE.Size = new System.Drawing.Size(377, 21);
            this.lblCUADRE.TabIndex = 23;
            this.lblCUADRE.Text = "-";
            this.lblCUADRE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gridControl1.Location = new System.Drawing.Point(2, 53);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1030, 287);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txtId);
            this.groupControl2.Controls.Add(this.txtNID_PARTIDA);
            this.groupControl2.Controls.Add(this.deFecha);
            this.groupControl2.Controls.Add(this.txtNumero);
            this.groupControl2.Controls.Add(this.txtTipoNombre);
            this.groupControl2.Controls.Add(this.txtTipo);
            this.groupControl2.Controls.Add(this.txtConcepto);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(2, 23);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1034, 114);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Datos de Comporbante";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(649, 28);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(66, 20);
            this.txtId.TabIndex = 193;
            this.txtId.Visible = false;
            // 
            // txtNID_PARTIDA
            // 
            this.txtNID_PARTIDA.Location = new System.Drawing.Point(721, 28);
            this.txtNID_PARTIDA.Name = "txtNID_PARTIDA";
            this.txtNID_PARTIDA.Size = new System.Drawing.Size(198, 20);
            this.txtNID_PARTIDA.TabIndex = 192;
            this.txtNID_PARTIDA.Visible = false;
            // 
            // deFecha
            // 
            this.deFecha.EditValue = null;
            this.deFecha.Location = new System.Drawing.Point(265, 74);
            this.deFecha.Name = "deFecha";
            this.deFecha.Properties.Mask.EditMask = "dd/mm/yyyy";
            this.deFecha.Size = new System.Drawing.Size(118, 20);
            this.deFecha.TabIndex = 13;
            this.deFecha.EditValueChanged += new System.EventHandler(this.deFecha_EditValueChanged);
            this.deFecha.TextChanged += new System.EventHandler(this.deFecha_TextChanged);
            this.deFecha.Leave += new System.EventHandler(this.deFecha_Leave);
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(75, 75);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(104, 21);
            this.txtNumero.TabIndex = 12;
            this.txtNumero.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNumero_KeyDown);
            this.txtNumero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumero_KeyPress);
            this.txtNumero.Leave += new System.EventHandler(this.txtNumero_Leave);
            // 
            // txtTipoNombre
            // 
            this.txtTipoNombre.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTipoNombre.Location = new System.Drawing.Point(137, 48);
            this.txtTipoNombre.Name = "txtTipoNombre";
            this.txtTipoNombre.ReadOnly = true;
            this.txtTipoNombre.Size = new System.Drawing.Size(246, 21);
            this.txtTipoNombre.TabIndex = 11;
            // 
            // txtTipo
            // 
            this.txtTipo.Location = new System.Drawing.Point(75, 48);
            this.txtTipo.Name = "txtTipo";
            this.txtTipo.Size = new System.Drawing.Size(56, 21);
            this.txtTipo.TabIndex = 10;
            // 
            // txtConcepto
            // 
            this.txtConcepto.Location = new System.Drawing.Point(422, 48);
            this.txtConcepto.Multiline = true;
            this.txtConcepto.Name = "txtConcepto";
            this.txtConcepto.Size = new System.Drawing.Size(497, 46);
            this.txtConcepto.TabIndex = 9;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(422, 31);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(53, 14);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Concepto";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(227, 74);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(32, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Fecha";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(26, 74);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(43, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Numero";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(45, 48);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tipo";
            // 
            // frmComprobantes
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 611);
            this.Controls.Add(this.groupControl1);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Name = "frmComprobantes";
            this.Text = "Comprobantes";
            this.Load += new System.EventHandler(this.frmComprobantes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNID_PARTIDA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFecha.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFecha.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.TextBox txtConcepto;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.TextBox txtTipoNombre;
        private System.Windows.Forms.TextBox txtTipo;
        private DevExpress.XtraEditors.DateEdit deFecha;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private System.Windows.Forms.Label lblDIFERENCIA;
        private System.Windows.Forms.Label lblTOTAL_ABONO;
        private System.Windows.Forms.Label lblTOTAL_CARGO;
        private System.Windows.Forms.Label lblCUADRE;
        private DevExpress.XtraEditors.SimpleButton btIgnorar;
        private DevExpress.XtraEditors.SimpleButton btGrabar;
        private DevExpress.XtraEditors.SimpleButton btEliminar;
        private DevExpress.XtraEditors.SimpleButton btModificar;
        private DevExpress.XtraEditors.SimpleButton btSiguiente;
        private DevExpress.XtraEditors.SimpleButton btAnterior;
        private DevExpress.XtraEditors.SimpleButton btImprimir;
        private DevExpress.XtraEditors.SimpleButton btSuspender;
        private DevExpress.XtraEditors.SimpleButton btBuscar;
        private DevExpress.XtraEditors.TextEdit txtNID_PARTIDA;
        private DevExpress.XtraEditors.SimpleButton btAdiconarItem;
        private System.Windows.Forms.Label lbMesAnio;
        private DevExpress.XtraEditors.TextEdit txtId;
        private DevExpress.XtraEditors.SimpleButton btNuevo;
        private DevExpress.XtraEditors.SimpleButton btSalir;
    }
}