using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Exportacion
{
    partial class frmDestinos : DevExpress.XtraEditors.XtraForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // ==================== Datos del Destino (maestro) ====================
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        private DevExpress.XtraEditors.TextEdit txtNumDes;
        private DevExpress.XtraEditors.TextEdit txtDescripcion;
        private DevExpress.XtraEditors.CheckEdit cboAreaCA;
        private DevExpress.XtraEditors.LookUpEdit cboCodPaisMH;
        private DevExpress.XtraEditors.ComboBoxEdit cboEstado;

        private DevExpress.XtraLayout.LayoutControlItem liNumDes;
        private DevExpress.XtraLayout.LayoutControlItem liDescripcion;
        private DevExpress.XtraLayout.LayoutControlItem liAreaCA;
        private DevExpress.XtraLayout.LayoutControlItem liCodPaisMH;
        private DevExpress.XtraLayout.LayoutControlItem liEstado;

        private DevExpress.XtraEditors.GroupControl pnlBotones;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraEditors.SimpleButton btnEditar;
        private DevExpress.XtraEditors.SimpleButton btnEliminar;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.SimpleButton btnFinalizar;

        // ==================== Divisor: lista de destinos / puertos del destino ====================
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;

        private DevExpress.XtraEditors.GroupControl grpListaDestinos;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colNumDes;
        private DevExpress.XtraGrid.Columns.GridColumn colDescripcion;
        private DevExpress.XtraGrid.Columns.GridColumn colAreaCA;
        private DevExpress.XtraGrid.Columns.GridColumn colCodPaisMH;
        private DevExpress.XtraGrid.Columns.GridColumn colEstado;
        private DevExpress.XtraGrid.Columns.GridColumn colIngresadoPor;
        private DevExpress.XtraGrid.Columns.GridColumn colModificadoPor;
        private DevExpress.XtraGrid.Columns.GridColumn colFechaIngreso;
        private DevExpress.XtraGrid.Columns.GridColumn colFechaModificacion;

        // ==================== Puertos del destino (detalle) ====================
        private DevExpress.XtraEditors.GroupControl grpPuertos;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;

        private DevExpress.XtraEditors.TextEdit txtNumPto;
        private DevExpress.XtraEditors.TextEdit txtDescripcionPuerto;
        private DevExpress.XtraEditors.ComboBoxEdit cboEstadoPuerto;

        private DevExpress.XtraLayout.LayoutControlItem liNumPto;
        private DevExpress.XtraLayout.LayoutControlItem liDescripcionPuerto;
        private DevExpress.XtraLayout.LayoutControlItem liEstadoPuerto;

        private DevExpress.XtraEditors.GroupControl pnlBotonesPuertos;
        private DevExpress.XtraEditors.SimpleButton btnNuevoPuerto;
        private DevExpress.XtraEditors.SimpleButton btnGuardarPuerto;
        private DevExpress.XtraEditors.SimpleButton btnEditarPuerto;
        private DevExpress.XtraEditors.SimpleButton btnEliminarPuerto;
        private DevExpress.XtraEditors.SimpleButton btnCancelarPuerto;

        private DevExpress.XtraEditors.SearchControl searchControl2;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn colNumPto;
        private DevExpress.XtraGrid.Columns.GridColumn colDescripcionPuerto;
        private DevExpress.XtraGrid.Columns.GridColumn colEstadoPuerto;
        private DevExpress.XtraGrid.Columns.GridColumn colIngresadoPorPuerto;
        private DevExpress.XtraGrid.Columns.GridColumn colModificadoPorPuerto;
        private DevExpress.XtraGrid.Columns.GridColumn colFechaIngresoPuerto;
        private DevExpress.XtraGrid.Columns.GridColumn colFechaModificacionPuerto;

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtNumDes = new DevExpress.XtraEditors.TextEdit();
            this.txtDescripcion = new DevExpress.XtraEditors.TextEdit();
            this.cboAreaCA = new DevExpress.XtraEditors.CheckEdit();
            this.cboCodPaisMH = new DevExpress.XtraEditors.LookUpEdit();
            this.cboEstado = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.liNumDes = new DevExpress.XtraLayout.LayoutControlItem();
            this.liDescripcion = new DevExpress.XtraLayout.LayoutControlItem();
            this.liAreaCA = new DevExpress.XtraLayout.LayoutControlItem();
            this.liCodPaisMH = new DevExpress.XtraLayout.LayoutControlItem();
            this.liEstado = new DevExpress.XtraLayout.LayoutControlItem();
            this.pnlBotones = new DevExpress.XtraEditors.GroupControl();
            this.btnFinalizar = new DevExpress.XtraEditors.SimpleButton();
            this.btnNuevo = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditar = new DevExpress.XtraEditors.SimpleButton();
            this.btnEliminar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.grpListaDestinos = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNumDes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescripcion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAreaCA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCodPaisMH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEstado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIngresadoPor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModificadoPor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFechaIngreso = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFechaModificacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.grpPuertos = new DevExpress.XtraEditors.GroupControl();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNumPto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescripcionPuerto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEstadoPuerto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIngresadoPorPuerto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModificadoPorPuerto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFechaIngresoPuerto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFechaModificacionPuerto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.searchControl2 = new DevExpress.XtraEditors.SearchControl();
            this.pnlBotonesPuertos = new DevExpress.XtraEditors.GroupControl();
            this.btnNuevoPuerto = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardarPuerto = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditarPuerto = new DevExpress.XtraEditors.SimpleButton();
            this.btnEliminarPuerto = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelarPuerto = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.txtNumPto = new DevExpress.XtraEditors.TextEdit();
            this.txtDescripcionPuerto = new DevExpress.XtraEditors.TextEdit();
            this.cboEstadoPuerto = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.liNumPto = new DevExpress.XtraLayout.LayoutControlItem();
            this.liDescripcionPuerto = new DevExpress.XtraLayout.LayoutControlItem();
            this.liEstadoPuerto = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumDes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAreaCA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCodPaisMH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEstado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liNumDes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liDescripcion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liAreaCA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liCodPaisMH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liEstado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBotones)).BeginInit();
            this.pnlBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpListaDestinos)).BeginInit();
            this.grpListaDestinos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpPuertos)).BeginInit();
            this.grpPuertos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBotonesPuertos)).BeginInit();
            this.pnlBotonesPuertos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumPto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcionPuerto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEstadoPuerto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liNumPto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liDescripcionPuerto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liEstadoPuerto)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtNumDes);
            this.layoutControl1.Controls.Add(this.txtDescripcion);
            this.layoutControl1.Controls.Add(this.cboAreaCA);
            this.layoutControl1.Controls.Add(this.cboCodPaisMH);
            this.layoutControl1.Controls.Add(this.cboEstado);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(900, 130);
            this.layoutControl1.TabIndex = 2;
            // 
            // txtNumDes
            // 
            this.txtNumDes.Location = new System.Drawing.Point(84, 2);
            this.txtNumDes.Name = "txtNumDes";
            this.txtNumDes.Properties.Mask.EditMask = "\\d*";
            this.txtNumDes.Properties.MaxLength = 10;
            this.txtNumDes.Properties.ReadOnly = true;
            this.txtNumDes.Size = new System.Drawing.Size(787, 20);
            this.txtNumDes.StyleController = this.layoutControl1;
            this.txtNumDes.TabIndex = 4;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(84, 26);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Properties.MaxLength = 100;
            this.txtDescripcion.Size = new System.Drawing.Size(787, 20);
            this.txtDescripcion.StyleController = this.layoutControl1;
            this.txtDescripcion.TabIndex = 5;
            // 
            // cboAreaCA
            // 
            this.cboAreaCA.Location = new System.Drawing.Point(84, 50);
            this.cboAreaCA.Name = "cboAreaCA";
            this.cboAreaCA.Properties.Caption = "Pertenece al área CA";
            this.cboAreaCA.Size = new System.Drawing.Size(250, 20);
            this.cboAreaCA.StyleController = this.layoutControl1;
            this.cboAreaCA.TabIndex = 6;
            // 
            // cboCodPaisMH
            // 
            this.cboCodPaisMH.Location = new System.Drawing.Point(84, 74);
            this.cboCodPaisMH.Name = "cboCodPaisMH";
            this.cboCodPaisMH.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODI_MH", "Codigo"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VALORES", "Pais")});
            this.cboCodPaisMH.Properties.DisplayMember = "VALORES";
            this.cboCodPaisMH.Properties.NullText = "";
            this.cboCodPaisMH.Properties.ValueMember = "CODI_MH";
            this.cboCodPaisMH.Size = new System.Drawing.Size(787, 20);
            this.cboCodPaisMH.StyleController = this.layoutControl1;
            this.cboCodPaisMH.TabIndex = 7;
            this.cboCodPaisMH.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.cboCodPaisMH_EditValueChanging);
            // 
            // cboEstado
            // 
            this.cboEstado.Location = new System.Drawing.Point(84, 98);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Properties.Items.AddRange(new object[] {
            "ACTIVO",
            "INACTIVO"});
            this.cboEstado.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboEstado.Size = new System.Drawing.Size(787, 20);
            this.cboEstado.StyleController = this.layoutControl1;
            this.cboEstado.TabIndex = 8;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.liNumDes,
            this.liDescripcion,
            this.liAreaCA,
            this.liCodPaisMH,
            this.liEstado});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(883, 161);
            this.layoutControlGroup1.Text = "Datos del Destino";
            // 
            // liNumDes
            // 
            this.liNumDes.Control = this.txtNumDes;
            this.liNumDes.Location = new System.Drawing.Point(0, 0);
            this.liNumDes.Name = "liNumDes";
            this.liNumDes.Size = new System.Drawing.Size(863, 24);
            this.liNumDes.Text = "No. Destino:";
            this.liNumDes.TextSize = new System.Drawing.Size(60, 13);
            this.liNumDes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // liDescripcion
            // 
            this.liDescripcion.Control = this.txtDescripcion;
            this.liDescripcion.Location = new System.Drawing.Point(0, 24);
            this.liDescripcion.Name = "liDescripcion";
            this.liDescripcion.Size = new System.Drawing.Size(863, 24);
            this.liDescripcion.Text = "Destino:";
            this.liDescripcion.TextSize = new System.Drawing.Size(60, 13);
            // 
            // liAreaCA
            // 
            this.liAreaCA.Control = this.cboAreaCA;
            this.liAreaCA.Location = new System.Drawing.Point(0, 48);
            this.liAreaCA.Name = "liAreaCA";
            this.liAreaCA.Size = new System.Drawing.Size(863, 24);
            this.liAreaCA.Text = "¿Área CA?";
            this.liAreaCA.TextSize = new System.Drawing.Size(60, 13);
            // 
            // liCodPaisMH
            // 
            this.liCodPaisMH.Control = this.cboCodPaisMH;
            this.liCodPaisMH.Location = new System.Drawing.Point(0, 72);
            this.liCodPaisMH.Name = "liCodPaisMH";
            this.liCodPaisMH.Size = new System.Drawing.Size(863, 24);
            this.liCodPaisMH.Text = "Pais (MH):";
            this.liCodPaisMH.TextSize = new System.Drawing.Size(60, 13);
            // 
            // liEstado
            // 
            this.liEstado.Control = this.cboEstado;
            this.liEstado.Location = new System.Drawing.Point(0, 96);
            this.liEstado.Name = "liEstado";
            this.liEstado.Size = new System.Drawing.Size(863, 24);
            this.liEstado.Text = "Estado:";
            this.liEstado.TextSize = new System.Drawing.Size(60, 13);
            // 
            // pnlBotones
            // 
            this.pnlBotones.Controls.Add(this.btnFinalizar);
            this.pnlBotones.Controls.Add(this.btnNuevo);
            this.pnlBotones.Controls.Add(this.btnGuardar);
            this.pnlBotones.Controls.Add(this.btnEditar);
            this.pnlBotones.Controls.Add(this.btnEliminar);
            this.pnlBotones.Controls.Add(this.btnCancelar);
            this.pnlBotones.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBotones.Location = new System.Drawing.Point(790, 130);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Size = new System.Drawing.Size(110, 581);
            this.pnlBotones.TabIndex = 1;
            this.pnlBotones.Text = "Acciones - Destino";
            this.pnlBotones.Appearance.BorderColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.pnlBotones.Appearance.Options.UseBorderColor = true;
            this.pnlBotones.AppearanceCaption.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.pnlBotones.AppearanceCaption.ForeColor = System.Drawing.Color.White;
            this.pnlBotones.AppearanceCaption.Options.UseBackColor = true;
            this.pnlBotones.AppearanceCaption.Options.UseForeColor = true;
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.Appearance.Options.UseFont = true;
            this.btnFinalizar.Appearance.Options.UseTextOptions = true;
            this.btnFinalizar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnFinalizar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnFinalizar.Location = new System.Drawing.Point(10, 285);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(90, 44);
            this.btnFinalizar.TabIndex = 159;
            this.btnFinalizar.TabStop = false;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevo.Location = new System.Drawing.Point(10, 35);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(90, 44);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.guardar2_32x32;
            this.btnGuardar.Location = new System.Drawing.Point(10, 85);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(90, 44);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.editar3_32x32;
            this.btnEditar.Location = new System.Drawing.Point(10, 135);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(90, 44);
            this.btnEditar.TabIndex = 2;
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.eliminar32x32;
            this.btnEliminar.Location = new System.Drawing.Point(10, 185);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(90, 44);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.cancelar32x32;
            this.btnCancelar.Location = new System.Drawing.Point(10, 235);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(90, 44);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 130);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.grpListaDestinos);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.grpPuertos);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(790, 581);
            this.splitContainerControl1.SplitterPosition = 220;
            this.splitContainerControl1.TabIndex = 0;
            // 
            // grpListaDestinos
            // 
            this.grpListaDestinos.Controls.Add(this.gridControl1);
            this.grpListaDestinos.Controls.Add(this.searchControl1);
            this.grpListaDestinos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpListaDestinos.Location = new System.Drawing.Point(0, 0);
            this.grpListaDestinos.Name = "grpListaDestinos";
            this.grpListaDestinos.Size = new System.Drawing.Size(790, 220);
            this.grpListaDestinos.TabIndex = 0;
            this.grpListaDestinos.Text = "Destinos registrados";
            this.grpListaDestinos.Appearance.BorderColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.grpListaDestinos.Appearance.Options.UseBorderColor = true;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 43);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(786, 175);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNumDes,
            this.colDescripcion,
            this.colAreaCA,
            this.colCodPaisMH,
            this.colEstado,
            this.colIngresadoPor,
            this.colModificadoPor,
            this.colFechaIngreso,
            this.colFechaModificacion});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colNumDes
            // 
            this.colNumDes.Caption = "No. Destino";
            this.colNumDes.FieldName = "NUMDES";
            this.colNumDes.Name = "colNumDes";
            this.colNumDes.OptionsColumn.AllowSize = false;
            this.colNumDes.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colNumDes.Width = 87;
            // 
            // colDescripcion
            // 
            this.colDescripcion.Caption = "Descripcion";
            this.colDescripcion.FieldName = "DESCRIPCION";
            this.colDescripcion.Name = "colDescripcion";
            this.colDescripcion.Visible = true;
            this.colDescripcion.VisibleIndex = 0;
            this.colDescripcion.Width = 300;
            // 
            // colAreaCA
            // 
            this.colAreaCA.Caption = "Area CA";
            this.colAreaCA.FieldName = "AREA_CA";
            this.colAreaCA.Name = "colAreaCA";
            this.colAreaCA.OptionsColumn.AllowSize = false;
            this.colAreaCA.Visible = true;
            this.colAreaCA.VisibleIndex = 1;
            this.colAreaCA.Width = 60;
            // 
            // colCodPaisMH
            // 
            this.colCodPaisMH.Caption = "Pais (MH)";
            this.colCodPaisMH.FieldName = "CODPAIS_MH";
            this.colCodPaisMH.Name = "colCodPaisMH";
            this.colCodPaisMH.Visible = true;
            this.colCodPaisMH.VisibleIndex = 2;
            this.colCodPaisMH.Width = 120;
            // 
            // colEstado
            // 
            this.colEstado.Caption = "Estado";
            this.colEstado.FieldName = "ESTADO";
            this.colEstado.Name = "colEstado";
            this.colEstado.OptionsColumn.AllowSize = false;
            this.colEstado.Visible = true;
            this.colEstado.VisibleIndex = 3;
            this.colEstado.Width = 70;
            // 
            // colIngresadoPor
            // 
            this.colIngresadoPor.Caption = "Ingresado por";
            this.colIngresadoPor.FieldName = "INGRESADO_POR";
            this.colIngresadoPor.Name = "colIngresadoPor";
            // 
            // colModificadoPor
            // 
            this.colModificadoPor.Caption = "Modificado por";
            this.colModificadoPor.FieldName = "MODIFICADO_POR";
            this.colModificadoPor.Name = "colModificadoPor";
            // 
            // colFechaIngreso
            // 
            this.colFechaIngreso.Caption = "Fecha ingreso";
            this.colFechaIngreso.FieldName = "FECHA_INGRESO";
            this.colFechaIngreso.Name = "colFechaIngreso";
            // 
            // colFechaModificacion
            // 
            this.colFechaModificacion.Caption = "Fecha modificacion";
            this.colFechaModificacion.FieldName = "FECHA_MODIFICACION";
            this.colFechaModificacion.Name = "colFechaModificacion";
            // 
            // searchControl1
            // 
            this.searchControl1.Client = this.gridControl1;
            this.searchControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchControl1.Location = new System.Drawing.Point(2, 23);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Properties.Client = this.gridControl1;
            this.searchControl1.Size = new System.Drawing.Size(786, 20);
            this.searchControl1.TabIndex = 1;
            // 
            // grpPuertos
            // 
            this.grpPuertos.Controls.Add(this.gridControl2);
            this.grpPuertos.Controls.Add(this.searchControl2);
            this.grpPuertos.Controls.Add(this.pnlBotonesPuertos);
            this.grpPuertos.Controls.Add(this.layoutControl2);
            this.grpPuertos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPuertos.Location = new System.Drawing.Point(0, 0);
            this.grpPuertos.Name = "grpPuertos";
            this.grpPuertos.Size = new System.Drawing.Size(790, 351);
            this.grpPuertos.TabIndex = 0;
            this.grpPuertos.Text = "Puertos del destino";
            this.grpPuertos.Appearance.BorderColor = System.Drawing.Color.FromArgb(0, 158, 115);
            this.grpPuertos.Appearance.Options.UseBorderColor = true;
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(2, 123);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(676, 226);
            this.gridControl2.TabIndex = 0;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNumPto,
            this.colDescripcionPuerto,
            this.colEstadoPuerto,
            this.colIngresadoPorPuerto,
            this.colModificadoPorPuerto,
            this.colFechaIngresoPuerto,
            this.colFechaModificacionPuerto});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // colNumPto
            // 
            this.colNumPto.Caption = "No. Puerto";
            this.colNumPto.FieldName = "NUMPTO";
            this.colNumPto.Name = "colNumPto";
            this.colNumPto.OptionsColumn.AllowSize = false;
            this.colNumPto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colNumPto.Width = 87;
            // 
            // colDescripcionPuerto
            // 
            this.colDescripcionPuerto.Caption = "Descripcion";
            this.colDescripcionPuerto.FieldName = "DESCRIPCION";
            this.colDescripcionPuerto.Name = "colDescripcionPuerto";
            this.colDescripcionPuerto.Visible = true;
            this.colDescripcionPuerto.VisibleIndex = 0;
            this.colDescripcionPuerto.Width = 400;
            // 
            // colEstadoPuerto
            // 
            this.colEstadoPuerto.Caption = "Estado";
            this.colEstadoPuerto.FieldName = "ESTADO";
            this.colEstadoPuerto.Name = "colEstadoPuerto";
            this.colEstadoPuerto.OptionsColumn.AllowSize = false;
            this.colEstadoPuerto.Visible = true;
            this.colEstadoPuerto.VisibleIndex = 1;
            this.colEstadoPuerto.Width = 58;
            // 
            // colIngresadoPorPuerto
            // 
            this.colIngresadoPorPuerto.Caption = "Ingresado por";
            this.colIngresadoPorPuerto.FieldName = "INGRESADO_POR";
            this.colIngresadoPorPuerto.Name = "colIngresadoPorPuerto";
            // 
            // colModificadoPorPuerto
            // 
            this.colModificadoPorPuerto.Caption = "Modificado por";
            this.colModificadoPorPuerto.FieldName = "MODIFICADO_POR";
            this.colModificadoPorPuerto.Name = "colModificadoPorPuerto";
            // 
            // colFechaIngresoPuerto
            // 
            this.colFechaIngresoPuerto.Caption = "Fecha ingreso";
            this.colFechaIngresoPuerto.FieldName = "FECHA_INGRESO";
            this.colFechaIngresoPuerto.Name = "colFechaIngresoPuerto";
            // 
            // colFechaModificacionPuerto
            // 
            this.colFechaModificacionPuerto.Caption = "Fecha modificacion";
            this.colFechaModificacionPuerto.FieldName = "FECHA_MODIFICACION";
            this.colFechaModificacionPuerto.Name = "colFechaModificacionPuerto";
            // 
            // searchControl2
            // 
            this.searchControl2.Client = this.gridControl2;
            this.searchControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchControl2.Location = new System.Drawing.Point(2, 103);
            this.searchControl2.Name = "searchControl2";
            this.searchControl2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl2.Properties.Client = this.gridControl2;
            this.searchControl2.Size = new System.Drawing.Size(676, 20);
            this.searchControl2.TabIndex = 3;
            // 
            // pnlBotonesPuertos
            // 
            this.pnlBotonesPuertos.Controls.Add(this.btnNuevoPuerto);
            this.pnlBotonesPuertos.Controls.Add(this.btnGuardarPuerto);
            this.pnlBotonesPuertos.Controls.Add(this.btnEditarPuerto);
            this.pnlBotonesPuertos.Controls.Add(this.btnEliminarPuerto);
            this.pnlBotonesPuertos.Controls.Add(this.btnCancelarPuerto);
            this.pnlBotonesPuertos.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBotonesPuertos.Location = new System.Drawing.Point(678, 103);
            this.pnlBotonesPuertos.Name = "pnlBotonesPuertos";
            this.pnlBotonesPuertos.Size = new System.Drawing.Size(110, 246);
            this.pnlBotonesPuertos.TabIndex = 1;
            this.pnlBotonesPuertos.Text = "Acciones - Puerto";
            this.pnlBotonesPuertos.Appearance.BorderColor = System.Drawing.Color.FromArgb(0, 158, 115);
            this.pnlBotonesPuertos.Appearance.Options.UseBorderColor = true;
            this.pnlBotonesPuertos.AppearanceCaption.BackColor = System.Drawing.Color.FromArgb(0, 158, 115);
            this.pnlBotonesPuertos.AppearanceCaption.ForeColor = System.Drawing.Color.White;
            this.pnlBotonesPuertos.AppearanceCaption.Options.UseBackColor = true;
            this.pnlBotonesPuertos.AppearanceCaption.Options.UseForeColor = true;
            // 
            // btnNuevoPuerto
            // 
            this.btnNuevoPuerto.Enabled = false;
            this.btnNuevoPuerto.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevoPuerto.Location = new System.Drawing.Point(10, 30);
            this.btnNuevoPuerto.Name = "btnNuevoPuerto";
            this.btnNuevoPuerto.Size = new System.Drawing.Size(90, 40);
            this.btnNuevoPuerto.TabIndex = 0;
            this.btnNuevoPuerto.Text = "Agregar";
            this.btnNuevoPuerto.Click += new System.EventHandler(this.btnNuevoPuerto_Click);
            // 
            // btnGuardarPuerto
            // 
            this.btnGuardarPuerto.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.guardar2_32x32;
            this.btnGuardarPuerto.Location = new System.Drawing.Point(10, 75);
            this.btnGuardarPuerto.Name = "btnGuardarPuerto";
            this.btnGuardarPuerto.Size = new System.Drawing.Size(90, 40);
            this.btnGuardarPuerto.TabIndex = 1;
            this.btnGuardarPuerto.Text = "Guardar";
            this.btnGuardarPuerto.Click += new System.EventHandler(this.btnGuardarPuerto_Click);
            // 
            // btnEditarPuerto
            // 
            this.btnEditarPuerto.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.editar3_32x32;
            this.btnEditarPuerto.Location = new System.Drawing.Point(10, 120);
            this.btnEditarPuerto.Name = "btnEditarPuerto";
            this.btnEditarPuerto.Size = new System.Drawing.Size(90, 40);
            this.btnEditarPuerto.TabIndex = 2;
            this.btnEditarPuerto.Text = "Editar";
            this.btnEditarPuerto.Click += new System.EventHandler(this.btnEditarPuerto_Click);
            // 
            // btnEliminarPuerto
            // 
            this.btnEliminarPuerto.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.eliminar32x32;
            this.btnEliminarPuerto.Location = new System.Drawing.Point(10, 165);
            this.btnEliminarPuerto.Name = "btnEliminarPuerto";
            this.btnEliminarPuerto.Size = new System.Drawing.Size(90, 40);
            this.btnEliminarPuerto.TabIndex = 3;
            this.btnEliminarPuerto.Text = "Eliminar";
            this.btnEliminarPuerto.Click += new System.EventHandler(this.btnEliminarPuerto_Click);
            // 
            // btnCancelarPuerto
            // 
            this.btnCancelarPuerto.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.cancelar32x32;
            this.btnCancelarPuerto.Location = new System.Drawing.Point(10, 210);
            this.btnCancelarPuerto.Name = "btnCancelarPuerto";
            this.btnCancelarPuerto.Size = new System.Drawing.Size(90, 40);
            this.btnCancelarPuerto.TabIndex = 4;
            this.btnCancelarPuerto.Text = "Cancelar";
            this.btnCancelarPuerto.Click += new System.EventHandler(this.btnCancelarPuerto_Click);
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.txtNumPto);
            this.layoutControl2.Controls.Add(this.txtDescripcionPuerto);
            this.layoutControl2.Controls.Add(this.cboEstadoPuerto);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl2.Location = new System.Drawing.Point(2, 23);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(786, 80);
            this.layoutControl2.TabIndex = 2;
            // 
            // txtNumPto
            // 
            this.txtNumPto.Location = new System.Drawing.Point(80, 17);
            this.txtNumPto.Name = "txtNumPto";
            this.txtNumPto.Properties.Mask.EditMask = "\\d*";
            this.txtNumPto.Properties.MaxLength = 10;
            this.txtNumPto.Properties.ReadOnly = true;
            this.txtNumPto.Size = new System.Drawing.Size(677, 20);
            this.txtNumPto.StyleController = this.layoutControl2;
            this.txtNumPto.TabIndex = 3;
            // 
            // txtDescripcionPuerto
            // 
            this.txtDescripcionPuerto.Location = new System.Drawing.Point(80, 41);
            this.txtDescripcionPuerto.Name = "txtDescripcionPuerto";
            this.txtDescripcionPuerto.Properties.MaxLength = 100;
            this.txtDescripcionPuerto.Size = new System.Drawing.Size(677, 20);
            this.txtDescripcionPuerto.StyleController = this.layoutControl2;
            this.txtDescripcionPuerto.TabIndex = 4;
            // 
            // cboEstadoPuerto
            // 
            this.cboEstadoPuerto.Location = new System.Drawing.Point(80, 65);
            this.cboEstadoPuerto.Name = "cboEstadoPuerto";
            this.cboEstadoPuerto.Properties.Items.AddRange(new object[] {
            "ACT",
            "INA"});
            this.cboEstadoPuerto.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboEstadoPuerto.Size = new System.Drawing.Size(677, 20);
            this.cboEstadoPuerto.StyleController = this.layoutControl2;
            this.cboEstadoPuerto.TabIndex = 5;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.liNumPto,
            this.liDescripcionPuerto,
            this.liEstadoPuerto});
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(769, 113);
            this.layoutControlGroup2.Text = "Datos del Puerto";
            // 
            // liNumPto
            // 
            this.liNumPto.Control = this.txtNumPto;
            this.liNumPto.Location = new System.Drawing.Point(0, 0);
            this.liNumPto.Name = "liNumPto";
            this.liNumPto.Size = new System.Drawing.Size(749, 24);
            this.liNumPto.Text = "No. Puerto:";
            this.liNumPto.TextSize = new System.Drawing.Size(56, 13);
            this.liNumPto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // liDescripcionPuerto
            // 
            this.liDescripcionPuerto.Control = this.txtDescripcionPuerto;
            this.liDescripcionPuerto.Location = new System.Drawing.Point(0, 24);
            this.liDescripcionPuerto.Name = "liDescripcionPuerto";
            this.liDescripcionPuerto.Size = new System.Drawing.Size(749, 24);
            this.liDescripcionPuerto.Text = "Puerto:";
            this.liDescripcionPuerto.TextSize = new System.Drawing.Size(56, 13);
            // 
            // liEstadoPuerto
            // 
            this.liEstadoPuerto.Control = this.cboEstadoPuerto;
            this.liEstadoPuerto.Location = new System.Drawing.Point(0, 48);
            this.liEstadoPuerto.Name = "liEstadoPuerto";
            this.liEstadoPuerto.Size = new System.Drawing.Size(749, 24);
            this.liEstadoPuerto.Text = "Estado:";
            this.liEstadoPuerto.TextSize = new System.Drawing.Size(56, 13);
            // 
            // frmDestinos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 711);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmDestinos";
            this.Text = "Mantenimiento de Destinos y Puertos";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNumDes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAreaCA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCodPaisMH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEstado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liNumDes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liDescripcion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liAreaCA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liCodPaisMH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liEstado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBotones)).EndInit();
            this.pnlBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpListaDestinos)).EndInit();
            this.grpListaDestinos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpPuertos)).EndInit();
            this.grpPuertos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBotonesPuertos)).EndInit();
            this.pnlBotonesPuertos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNumPto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcionPuerto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEstadoPuerto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liNumPto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liDescripcionPuerto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liEstadoPuerto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}