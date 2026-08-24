using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Exportacion
{
    partial class frmContratoDetalle : DevExpress.XtraEditors.XtraForm
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

        // ==================== Encabezado personalizado ====================
        private DevExpress.XtraEditors.PanelControl pnlTitulo;
        private DevExpress.XtraEditors.LabelControl lblTitulo;
        private DevExpress.XtraEditors.SimpleButton btnCerrar;

        // ==================== Contenido (Datos del Contrato) ====================
        private DevExpress.XtraEditors.PanelControl pnlContenido;
        private DevExpress.XtraEditors.LabelControl lblDatos;
        private DevExpress.XtraEditors.PanelControl pnlSeparador;

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        private DevExpress.XtraEditors.LookUpEdit cboMercado;
        private DevExpress.XtraEditors.LookUpEdit cboCliente;
        private DevExpress.XtraEditors.DateEdit deFechaContrato;
        private DevExpress.XtraEditors.TextEdit txtNumeroContrato;
        private DevExpress.XtraEditors.LookUpEdit cboZafra;
        private DevExpress.XtraEditors.LookUpEdit cboProducto;
        private DevExpress.XtraEditors.TextEdit txtPresentacion;
        private DevExpress.XtraEditors.SpinEdit txtPrecio;
        private DevExpress.XtraEditors.SpinEdit txtVariacionAvg;
        private DevExpress.XtraEditors.SpinEdit txtToneladas;
        private DevExpress.XtraEditors.DateEdit deFechaFijarVolumen;
        private DevExpress.XtraEditors.SpinEdit txtToneladasMin;
        private DevExpress.XtraEditors.SpinEdit txtToneladasMax;
        private DevExpress.XtraEditors.ComboBoxEdit cboAplicaNominacion;
        private DevExpress.XtraEditors.ComboBoxEdit cboEstado;

        // Fila compuesta de PDF: boton "Seleccionar archivo..." + boton "Subir PDF"
        private DevExpress.XtraEditors.PanelControl pnlPdf;
        private DevExpress.XtraEditors.ButtonEdit beArchivoPdf;
        private DevExpress.XtraEditors.SimpleButton btnSubirPdf;

        private DevExpress.XtraLayout.LayoutControlItem liMercado;
        private DevExpress.XtraLayout.LayoutControlItem liCliente;
        private DevExpress.XtraLayout.LayoutControlItem liFechaContrato;
        private DevExpress.XtraLayout.LayoutControlItem liNumeroContrato;
        private DevExpress.XtraLayout.LayoutControlItem liZafra;
        private DevExpress.XtraLayout.LayoutControlItem liProducto;
        private DevExpress.XtraLayout.LayoutControlItem liPresentacion;
        private DevExpress.XtraLayout.LayoutControlItem liPrecio;
        private DevExpress.XtraLayout.LayoutControlItem liVariacionAvg;
        private DevExpress.XtraLayout.LayoutControlItem liToneladas;
        private DevExpress.XtraLayout.LayoutControlItem liFechaFijarVolumen;
        private DevExpress.XtraLayout.LayoutControlItem liToneladasMin;
        private DevExpress.XtraLayout.LayoutControlItem liToneladasMax;
        private DevExpress.XtraLayout.LayoutControlItem liAplicaNominacion;
        private DevExpress.XtraLayout.LayoutControlItem liEstado;
        private DevExpress.XtraLayout.LayoutControlItem liPdf;

        // ==================== Pie: acciones ====================
        private DevExpress.XtraEditors.PanelControl pnlBotonesAccion;

        private void InitializeComponent()
        {
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pnlTitulo = new DevExpress.XtraEditors.PanelControl();
            this.btnCerrar = new DevExpress.XtraEditors.SimpleButton();
            this.lblTitulo = new DevExpress.XtraEditors.LabelControl();
            this.pnlContenido = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cboMercado = new DevExpress.XtraEditors.LookUpEdit();
            this.cboCliente = new DevExpress.XtraEditors.LookUpEdit();
            this.deFechaContrato = new DevExpress.XtraEditors.DateEdit();
            this.txtNumeroContrato = new DevExpress.XtraEditors.TextEdit();
            this.cboZafra = new DevExpress.XtraEditors.LookUpEdit();
            this.cboProducto = new DevExpress.XtraEditors.LookUpEdit();
            this.txtPresentacion = new DevExpress.XtraEditors.TextEdit();
            this.txtPrecio = new DevExpress.XtraEditors.SpinEdit();
            this.txtVariacionAvg = new DevExpress.XtraEditors.SpinEdit();
            this.txtToneladas = new DevExpress.XtraEditors.SpinEdit();
            this.deFechaFijarVolumen = new DevExpress.XtraEditors.DateEdit();
            this.txtToneladasMin = new DevExpress.XtraEditors.SpinEdit();
            this.txtToneladasMax = new DevExpress.XtraEditors.SpinEdit();
            this.cboAplicaNominacion = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboEstado = new DevExpress.XtraEditors.ComboBoxEdit();
            this.pnlPdf = new DevExpress.XtraEditors.PanelControl();
            this.btnSubirPdf = new DevExpress.XtraEditors.SimpleButton();
            this.beArchivoPdf = new DevExpress.XtraEditors.ButtonEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.liMercado = new DevExpress.XtraLayout.LayoutControlItem();
            this.liCliente = new DevExpress.XtraLayout.LayoutControlItem();
            this.liFechaContrato = new DevExpress.XtraLayout.LayoutControlItem();
            this.liNumeroContrato = new DevExpress.XtraLayout.LayoutControlItem();
            this.liZafra = new DevExpress.XtraLayout.LayoutControlItem();
            this.liProducto = new DevExpress.XtraLayout.LayoutControlItem();
            this.liPresentacion = new DevExpress.XtraLayout.LayoutControlItem();
            this.liPrecio = new DevExpress.XtraLayout.LayoutControlItem();
            this.liVariacionAvg = new DevExpress.XtraLayout.LayoutControlItem();
            this.liToneladas = new DevExpress.XtraLayout.LayoutControlItem();
            this.liFechaFijarVolumen = new DevExpress.XtraLayout.LayoutControlItem();
            this.liToneladasMin = new DevExpress.XtraLayout.LayoutControlItem();
            this.liToneladasMax = new DevExpress.XtraLayout.LayoutControlItem();
            this.liAplicaNominacion = new DevExpress.XtraLayout.LayoutControlItem();
            this.liEstado = new DevExpress.XtraLayout.LayoutControlItem();
            this.liPdf = new DevExpress.XtraLayout.LayoutControlItem();
            this.pnlSeparador = new DevExpress.XtraEditors.PanelControl();
            this.lblDatos = new DevExpress.XtraEditors.LabelControl();
            this.pnlBotonesAccion = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTitulo)).BeginInit();
            this.pnlTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlContenido)).BeginInit();
            this.pnlContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboMercado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCliente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaContrato.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaContrato.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroContrato.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboZafra.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProducto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPresentacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVariacionAvg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToneladas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaFijarVolumen.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaFijarVolumen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToneladasMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToneladasMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAplicaNominacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEstado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPdf)).BeginInit();
            this.pnlPdf.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beArchivoPdf.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liMercado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liFechaContrato)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liNumeroContrato)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liZafra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liProducto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liPresentacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liPrecio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liVariacionAvg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liToneladas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liFechaFijarVolumen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liToneladasMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liToneladasMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liAplicaNominacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liEstado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liPdf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSeparador)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBotonesAccion)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTitulo
            // 
            this.pnlTitulo.Appearance.BackColor = System.Drawing.Color.White;
            this.pnlTitulo.Appearance.Options.UseBackColor = true;
            this.pnlTitulo.Controls.Add(this.btnCerrar);
            this.pnlTitulo.Controls.Add(this.lblTitulo);
            this.pnlTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitulo.Location = new System.Drawing.Point(0, 0);
            this.pnlTitulo.Name = "pnlTitulo";
            this.pnlTitulo.Size = new System.Drawing.Size(746, 48);
            this.pnlTitulo.TabIndex = 0;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnCerrar.Appearance.BorderColor = System.Drawing.Color.White;
            this.btnCerrar.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.Appearance.ForeColor = System.Drawing.Color.Red;
            this.btnCerrar.Appearance.Options.UseBackColor = true;
            this.btnCerrar.Appearance.Options.UseBorderColor = true;
            this.btnCerrar.Appearance.Options.UseFont = true;
            this.btnCerrar.Appearance.Options.UseForeColor = true;
            this.btnCerrar.Location = new System.Drawing.Point(706, 8);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(32, 32);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.Text = "✕";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Appearance.Options.UseFont = true;
            this.lblTitulo.Appearance.Options.UseTextOptions = true;
            this.lblTitulo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTitulo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Location = new System.Drawing.Point(2, 2);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(742, 21);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "NUEVO CONTRATO";
            // 
            // pnlContenido
            // 
            this.pnlContenido.Appearance.BackColor = System.Drawing.Color.White;
            this.pnlContenido.Appearance.Options.UseBackColor = true;
            this.pnlContenido.Controls.Add(this.layoutControl1);
            this.pnlContenido.Controls.Add(this.pnlSeparador);
            this.pnlContenido.Controls.Add(this.lblDatos);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 48);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(20, 15, 20, 10);
            this.pnlContenido.Size = new System.Drawing.Size(746, 615);
            this.pnlContenido.TabIndex = 1;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cboMercado);
            this.layoutControl1.Controls.Add(this.cboCliente);
            this.layoutControl1.Controls.Add(this.deFechaContrato);
            this.layoutControl1.Controls.Add(this.txtNumeroContrato);
            this.layoutControl1.Controls.Add(this.cboZafra);
            this.layoutControl1.Controls.Add(this.cboProducto);
            this.layoutControl1.Controls.Add(this.txtPresentacion);
            this.layoutControl1.Controls.Add(this.txtPrecio);
            this.layoutControl1.Controls.Add(this.txtVariacionAvg);
            this.layoutControl1.Controls.Add(this.txtToneladas);
            this.layoutControl1.Controls.Add(this.deFechaFijarVolumen);
            this.layoutControl1.Controls.Add(this.txtToneladasMin);
            this.layoutControl1.Controls.Add(this.txtToneladasMax);
            this.layoutControl1.Controls.Add(this.cboAplicaNominacion);
            this.layoutControl1.Controls.Add(this.cboEstado);
            this.layoutControl1.Controls.Add(this.pnlPdf);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(22, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(771, 355, 650, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(702, 560);
            this.layoutControl1.TabIndex = 2;
            // 
            // cboMercado
            // 
            this.cboMercado.Location = new System.Drawing.Point(151, 3);
            this.cboMercado.Name = "cboMercado";
            this.cboMercado.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODMDO", "Codigo"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DESCRIPCION", "Mercado")});
            this.cboMercado.Properties.DisplayMember = "DESCRIPCION";
            this.cboMercado.Properties.NullText = "";
            this.cboMercado.Properties.ValueMember = "CODMDO";
            this.cboMercado.Size = new System.Drawing.Size(548, 20);
            this.cboMercado.StyleController = this.layoutControl1;
            this.cboMercado.TabIndex = 3;
            // 
            // cboCliente
            // 
            this.cboCliente.Location = new System.Drawing.Point(151, 27);
            this.cboCliente.Name = "cboCliente";
            this.cboCliente.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_ENTIDAD", "Codigo"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NOMBRE", "Cliente")});
            this.cboCliente.Properties.DisplayMember = "NOMBRE";
            this.cboCliente.Properties.NullText = "";
            this.cboCliente.Properties.ValueMember = "ID_ENTIDAD";
            this.cboCliente.Size = new System.Drawing.Size(548, 20);
            this.cboCliente.StyleController = this.layoutControl1;
            this.cboCliente.TabIndex = 4;
            // 
            // deFechaContrato
            // 
            this.deFechaContrato.EditValue = null;
            this.deFechaContrato.Location = new System.Drawing.Point(151, 51);
            this.deFechaContrato.Name = "deFechaContrato";
            this.deFechaContrato.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.deFechaContrato.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deFechaContrato.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.deFechaContrato.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deFechaContrato.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.deFechaContrato.Size = new System.Drawing.Size(548, 20);
            this.deFechaContrato.StyleController = this.layoutControl1;
            this.deFechaContrato.TabIndex = 5;
            // 
            // txtNumeroContrato
            // 
            this.txtNumeroContrato.Location = new System.Drawing.Point(151, 75);
            this.txtNumeroContrato.Name = "txtNumeroContrato";
            this.txtNumeroContrato.Properties.MaxLength = 100;
            this.txtNumeroContrato.Size = new System.Drawing.Size(548, 20);
            this.txtNumeroContrato.StyleController = this.layoutControl1;
            this.txtNumeroContrato.TabIndex = 6;
            // 
            // cboZafra
            // 
            this.cboZafra.Location = new System.Drawing.Point(151, 99);
            this.cboZafra.Name = "cboZafra";
            this.cboZafra.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_ZAFRA", "Codigo"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DESCRIPCION", "Zafra")});
            this.cboZafra.Properties.DisplayMember = "DESCRIPCION";
            this.cboZafra.Properties.NullText = "";
            this.cboZafra.Properties.ValueMember = "ID_ZAFRA";
            this.cboZafra.Size = new System.Drawing.Size(548, 20);
            this.cboZafra.StyleController = this.layoutControl1;
            this.cboZafra.TabIndex = 7;
            // 
            // cboProducto
            // 
            this.cboProducto.Location = new System.Drawing.Point(151, 123);
            this.cboProducto.Name = "cboProducto";
            this.cboProducto.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("COD_REF", "Código", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DESCRIPCION", "Producto"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PRESENTACION", "Presentación", 150, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.cboProducto.Properties.DisplayMember = "DESCRIPCION";
            this.cboProducto.Properties.NullText = "";
            this.cboProducto.Properties.ValueMember = "ID_PRODUCTO";
            this.cboProducto.Size = new System.Drawing.Size(548, 20);
            this.cboProducto.StyleController = this.layoutControl1;
            this.cboProducto.TabIndex = 8;
            this.cboProducto.EditValueChanged += new System.EventHandler(this.cboProducto_EditValueChanged);
            // 
            // txtPresentacion
            // 
            this.txtPresentacion.Location = new System.Drawing.Point(151, 147);
            this.txtPresentacion.Name = "txtPresentacion";
            this.txtPresentacion.Properties.MaxLength = 50;
            this.txtPresentacion.Size = new System.Drawing.Size(548, 20);
            this.txtPresentacion.StyleController = this.layoutControl1;
            this.txtPresentacion.TabIndex = 9;
            // 
            // txtPrecio
            // 
            this.txtPrecio.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPrecio.Location = new System.Drawing.Point(151, 171);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Properties.DisplayFormat.FormatString = "n2";
            this.txtPrecio.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPrecio.Properties.EditFormat.FormatString = "n2";
            this.txtPrecio.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPrecio.Properties.Mask.EditMask = "n2";
            this.txtPrecio.Properties.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtPrecio.Size = new System.Drawing.Size(548, 20);
            this.txtPrecio.StyleController = this.layoutControl1;
            this.txtPrecio.TabIndex = 10;
            // 
            // txtVariacionAvg
            // 
            this.txtVariacionAvg.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtVariacionAvg.Location = new System.Drawing.Point(151, 195);
            this.txtVariacionAvg.Name = "txtVariacionAvg";
            this.txtVariacionAvg.Properties.DisplayFormat.FormatString = "n4";
            this.txtVariacionAvg.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtVariacionAvg.Properties.EditFormat.FormatString = "n4";
            this.txtVariacionAvg.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtVariacionAvg.Properties.Mask.EditMask = "n4";
            this.txtVariacionAvg.Properties.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtVariacionAvg.Properties.MinValue = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.txtVariacionAvg.Size = new System.Drawing.Size(548, 20);
            this.txtVariacionAvg.StyleController = this.layoutControl1;
            this.txtVariacionAvg.TabIndex = 11;
            // 
            // txtToneladas
            // 
            this.txtToneladas.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtToneladas.Location = new System.Drawing.Point(151, 219);
            this.txtToneladas.Name = "txtToneladas";
            this.txtToneladas.Properties.DisplayFormat.FormatString = "n2";
            this.txtToneladas.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtToneladas.Properties.EditFormat.FormatString = "n2";
            this.txtToneladas.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtToneladas.Properties.Mask.EditMask = "n2";
            this.txtToneladas.Properties.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtToneladas.Size = new System.Drawing.Size(548, 20);
            this.txtToneladas.StyleController = this.layoutControl1;
            this.txtToneladas.TabIndex = 12;
            // 
            // deFechaFijarVolumen
            // 
            this.deFechaFijarVolumen.EditValue = null;
            this.deFechaFijarVolumen.Location = new System.Drawing.Point(151, 243);
            this.deFechaFijarVolumen.Name = "deFechaFijarVolumen";
            this.deFechaFijarVolumen.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.deFechaFijarVolumen.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deFechaFijarVolumen.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.deFechaFijarVolumen.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deFechaFijarVolumen.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.deFechaFijarVolumen.Size = new System.Drawing.Size(548, 20);
            this.deFechaFijarVolumen.StyleController = this.layoutControl1;
            this.deFechaFijarVolumen.TabIndex = 13;
            // 
            // txtToneladasMin
            // 
            this.txtToneladasMin.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtToneladasMin.Location = new System.Drawing.Point(151, 267);
            this.txtToneladasMin.Name = "txtToneladasMin";
            this.txtToneladasMin.Properties.DisplayFormat.FormatString = "n2";
            this.txtToneladasMin.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtToneladasMin.Properties.EditFormat.FormatString = "n2";
            this.txtToneladasMin.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtToneladasMin.Properties.Mask.EditMask = "n2";
            this.txtToneladasMin.Properties.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtToneladasMin.Size = new System.Drawing.Size(548, 20);
            this.txtToneladasMin.StyleController = this.layoutControl1;
            this.txtToneladasMin.TabIndex = 14;
            // 
            // txtToneladasMax
            // 
            this.txtToneladasMax.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtToneladasMax.Location = new System.Drawing.Point(151, 291);
            this.txtToneladasMax.Name = "txtToneladasMax";
            this.txtToneladasMax.Properties.DisplayFormat.FormatString = "n2";
            this.txtToneladasMax.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtToneladasMax.Properties.EditFormat.FormatString = "n2";
            this.txtToneladasMax.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtToneladasMax.Properties.Mask.EditMask = "n2";
            this.txtToneladasMax.Properties.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtToneladasMax.Size = new System.Drawing.Size(548, 20);
            this.txtToneladasMax.StyleController = this.layoutControl1;
            this.txtToneladasMax.TabIndex = 15;
            // 
            // cboAplicaNominacion
            // 
            this.cboAplicaNominacion.Location = new System.Drawing.Point(151, 315);
            this.cboAplicaNominacion.Name = "cboAplicaNominacion";
            this.cboAplicaNominacion.Properties.Items.AddRange(new object[] {
            "Sí",
            "No"});
            this.cboAplicaNominacion.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboAplicaNominacion.Size = new System.Drawing.Size(548, 20);
            this.cboAplicaNominacion.StyleController = this.layoutControl1;
            this.cboAplicaNominacion.TabIndex = 16;
            // 
            // cboEstado
            // 
            this.cboEstado.Location = new System.Drawing.Point(151, 339);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Properties.Items.AddRange(new object[] {
            "Activo",
            "Inactivo"});
            this.cboEstado.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboEstado.Size = new System.Drawing.Size(548, 20);
            this.cboEstado.StyleController = this.layoutControl1;
            this.cboEstado.TabIndex = 17;
            // 
            // pnlPdf
            // 
            this.pnlPdf.Controls.Add(this.btnSubirPdf);
            this.pnlPdf.Controls.Add(this.beArchivoPdf);
            this.pnlPdf.Location = new System.Drawing.Point(151, 363);
            this.pnlPdf.Name = "pnlPdf";
            this.pnlPdf.Size = new System.Drawing.Size(548, 194);
            this.pnlPdf.TabIndex = 18;
            // 
            // btnSubirPdf
            // 
            this.btnSubirPdf.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnSubirPdf.Appearance.Options.UseFont = true;
            this.btnSubirPdf.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSubirPdf.Enabled = false;
            this.btnSubirPdf.Location = new System.Drawing.Point(2, 22);
            this.btnSubirPdf.Name = "btnSubirPdf";
            this.btnSubirPdf.Size = new System.Drawing.Size(544, 30);
            this.btnSubirPdf.TabIndex = 1;
            this.btnSubirPdf.Text = "Subir PDF";
            // 
            // beArchivoPdf
            // 
            this.beArchivoPdf.Dock = System.Windows.Forms.DockStyle.Top;
            this.beArchivoPdf.Location = new System.Drawing.Point(2, 2);
            this.beArchivoPdf.Name = "beArchivoPdf";
            this.beArchivoPdf.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "Seleccionar archivo...", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.beArchivoPdf.Properties.ReadOnly = true;
            this.beArchivoPdf.Size = new System.Drawing.Size(544, 20);
            this.beArchivoPdf.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.liMercado,
            this.liCliente,
            this.liFechaContrato,
            this.liNumeroContrato,
            this.liZafra,
            this.liProducto,
            this.liPresentacion,
            this.liPrecio,
            this.liVariacionAvg,
            this.liToneladas,
            this.liFechaFijarVolumen,
            this.liToneladasMin,
            this.liToneladasMax,
            this.liAplicaNominacion,
            this.liEstado,
            this.liPdf});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(702, 560);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // liMercado
            // 
            this.liMercado.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.liMercado.AppearanceItemCaption.Options.UseFont = true;
            this.liMercado.Control = this.cboMercado;
            this.liMercado.Location = new System.Drawing.Point(0, 0);
            this.liMercado.Name = "liMercado";
            this.liMercado.Size = new System.Drawing.Size(700, 24);
            this.liMercado.Text = "Mercado:*";
            this.liMercado.TextSize = new System.Drawing.Size(136, 16);
            // 
            // liCliente
            // 
            this.liCliente.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.liCliente.AppearanceItemCaption.Options.UseFont = true;
            this.liCliente.Control = this.cboCliente;
            this.liCliente.Location = new System.Drawing.Point(0, 24);
            this.liCliente.Name = "liCliente";
            this.liCliente.Size = new System.Drawing.Size(700, 24);
            this.liCliente.Text = "Cliente:*";
            this.liCliente.TextSize = new System.Drawing.Size(136, 16);
            // 
            // liFechaContrato
            // 
            this.liFechaContrato.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.liFechaContrato.AppearanceItemCaption.Options.UseFont = true;
            this.liFechaContrato.Control = this.deFechaContrato;
            this.liFechaContrato.Location = new System.Drawing.Point(0, 48);
            this.liFechaContrato.Name = "liFechaContrato";
            this.liFechaContrato.Size = new System.Drawing.Size(700, 24);
            this.liFechaContrato.Text = "Fecha contrato:*";
            this.liFechaContrato.TextSize = new System.Drawing.Size(136, 16);
            // 
            // liNumeroContrato
            // 
            this.liNumeroContrato.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.liNumeroContrato.AppearanceItemCaption.Options.UseFont = true;
            this.liNumeroContrato.Control = this.txtNumeroContrato;
            this.liNumeroContrato.Location = new System.Drawing.Point(0, 72);
            this.liNumeroContrato.Name = "liNumeroContrato";
            this.liNumeroContrato.Size = new System.Drawing.Size(700, 24);
            this.liNumeroContrato.Text = "N° Contrato:*";
            this.liNumeroContrato.TextSize = new System.Drawing.Size(136, 16);
            // 
            // liZafra
            // 
            this.liZafra.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.liZafra.AppearanceItemCaption.Options.UseFont = true;
            this.liZafra.Control = this.cboZafra;
            this.liZafra.Location = new System.Drawing.Point(0, 96);
            this.liZafra.Name = "liZafra";
            this.liZafra.Size = new System.Drawing.Size(700, 24);
            this.liZafra.Text = "Zafra:*";
            this.liZafra.TextSize = new System.Drawing.Size(136, 16);
            // 
            // liProducto
            // 
            this.liProducto.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.liProducto.AppearanceItemCaption.Options.UseFont = true;
            this.liProducto.Control = this.cboProducto;
            this.liProducto.Location = new System.Drawing.Point(0, 120);
            this.liProducto.Name = "liProducto";
            this.liProducto.Size = new System.Drawing.Size(700, 24);
            this.liProducto.Text = "Producto:*";
            this.liProducto.TextSize = new System.Drawing.Size(136, 16);
            // 
            // liPresentacion
            // 
            this.liPresentacion.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.liPresentacion.AppearanceItemCaption.Options.UseFont = true;
            this.liPresentacion.Control = this.txtPresentacion;
            this.liPresentacion.Location = new System.Drawing.Point(0, 144);
            this.liPresentacion.Name = "liPresentacion";
            this.liPresentacion.Size = new System.Drawing.Size(700, 24);
            this.liPresentacion.Text = "Presentación:";
            this.liPresentacion.TextSize = new System.Drawing.Size(136, 16);
            // 
            // liPrecio
            // 
            this.liPrecio.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.liPrecio.AppearanceItemCaption.Options.UseFont = true;
            this.liPrecio.Control = this.txtPrecio;
            this.liPrecio.Location = new System.Drawing.Point(0, 168);
            this.liPrecio.Name = "liPrecio";
            this.liPrecio.Size = new System.Drawing.Size(700, 24);
            this.liPrecio.Text = "Precio($):";
            this.liPrecio.TextSize = new System.Drawing.Size(136, 16);
            // 
            // liVariacionAvg
            // 
            this.liVariacionAvg.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.liVariacionAvg.AppearanceItemCaption.Options.UseFont = true;
            this.liVariacionAvg.Control = this.txtVariacionAvg;
            this.liVariacionAvg.Location = new System.Drawing.Point(0, 192);
            this.liVariacionAvg.Name = "liVariacionAvg";
            this.liVariacionAvg.Size = new System.Drawing.Size(700, 24);
            this.liVariacionAvg.Text = "Variacion AVG($):";
            this.liVariacionAvg.TextSize = new System.Drawing.Size(136, 16);
            // 
            // liToneladas
            // 
            this.liToneladas.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.liToneladas.AppearanceItemCaption.Options.UseFont = true;
            this.liToneladas.Control = this.txtToneladas;
            this.liToneladas.Location = new System.Drawing.Point(0, 216);
            this.liToneladas.Name = "liToneladas";
            this.liToneladas.Size = new System.Drawing.Size(700, 24);
            this.liToneladas.Text = "Toneladas:*";
            this.liToneladas.TextSize = new System.Drawing.Size(136, 16);
            // 
            // liFechaFijarVolumen
            // 
            this.liFechaFijarVolumen.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.liFechaFijarVolumen.AppearanceItemCaption.Options.UseFont = true;
            this.liFechaFijarVolumen.Control = this.deFechaFijarVolumen;
            this.liFechaFijarVolumen.Location = new System.Drawing.Point(0, 240);
            this.liFechaFijarVolumen.Name = "liFechaFijarVolumen";
            this.liFechaFijarVolumen.Size = new System.Drawing.Size(700, 24);
            this.liFechaFijarVolumen.Text = "Fecha de fijar volumen:";
            this.liFechaFijarVolumen.TextSize = new System.Drawing.Size(136, 16);
            // 
            // liToneladasMin
            // 
            this.liToneladasMin.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.liToneladasMin.AppearanceItemCaption.Options.UseFont = true;
            this.liToneladasMin.Control = this.txtToneladasMin;
            this.liToneladasMin.Location = new System.Drawing.Point(0, 264);
            this.liToneladasMin.Name = "liToneladasMin";
            this.liToneladasMin.Size = new System.Drawing.Size(700, 24);
            this.liToneladasMin.Text = "Min. Toneladas:";
            this.liToneladasMin.TextSize = new System.Drawing.Size(136, 16);
            // 
            // liToneladasMax
            // 
            this.liToneladasMax.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.liToneladasMax.AppearanceItemCaption.Options.UseFont = true;
            this.liToneladasMax.Control = this.txtToneladasMax;
            this.liToneladasMax.Location = new System.Drawing.Point(0, 288);
            this.liToneladasMax.Name = "liToneladasMax";
            this.liToneladasMax.Size = new System.Drawing.Size(700, 24);
            this.liToneladasMax.Text = "Max. Toneladas:";
            this.liToneladasMax.TextSize = new System.Drawing.Size(136, 16);
            // 
            // liAplicaNominacion
            // 
            this.liAplicaNominacion.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.liAplicaNominacion.AppearanceItemCaption.Options.UseFont = true;
            this.liAplicaNominacion.Control = this.cboAplicaNominacion;
            this.liAplicaNominacion.Location = new System.Drawing.Point(0, 312);
            this.liAplicaNominacion.Name = "liAplicaNominacion";
            this.liAplicaNominacion.Size = new System.Drawing.Size(700, 24);
            this.liAplicaNominacion.Text = "Aplica nominación:";
            this.liAplicaNominacion.TextSize = new System.Drawing.Size(136, 16);
            // 
            // liEstado
            // 
            this.liEstado.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.liEstado.AppearanceItemCaption.Options.UseFont = true;
            this.liEstado.Control = this.cboEstado;
            this.liEstado.Location = new System.Drawing.Point(0, 336);
            this.liEstado.Name = "liEstado";
            this.liEstado.Size = new System.Drawing.Size(700, 24);
            this.liEstado.Text = "Estado:";
            this.liEstado.TextSize = new System.Drawing.Size(136, 16);
            // 
            // liPdf
            // 
            this.liPdf.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F);
            this.liPdf.AppearanceItemCaption.Options.UseFont = true;
            this.liPdf.Control = this.pnlPdf;
            this.liPdf.Location = new System.Drawing.Point(0, 360);
            this.liPdf.Name = "liPdf";
            this.liPdf.Size = new System.Drawing.Size(700, 198);
            this.liPdf.Text = "PDF (opcional):";
            this.liPdf.TextSize = new System.Drawing.Size(136, 16);
            // 
            // pnlSeparador
            // 
            this.pnlSeparador.Appearance.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlSeparador.Appearance.Options.UseBackColor = true;
            this.pnlSeparador.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSeparador.Location = new System.Drawing.Point(22, 38);
            this.pnlSeparador.Name = "pnlSeparador";
            this.pnlSeparador.Size = new System.Drawing.Size(702, 1);
            this.pnlSeparador.TabIndex = 1;
            // 
            // lblDatos
            // 
            this.lblDatos.Appearance.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.lblDatos.Appearance.Options.UseFont = true;
            this.lblDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDatos.Location = new System.Drawing.Point(22, 17);
            this.lblDatos.Name = "lblDatos";
            this.lblDatos.Size = new System.Drawing.Size(46, 21);
            this.lblDatos.TabIndex = 0;
            this.lblDatos.Text = "Datos";
            // 
            // pnlBotonesAccion
            // 
            this.pnlBotonesAccion.Appearance.BackColor = System.Drawing.Color.White;
            this.pnlBotonesAccion.Appearance.Options.UseBackColor = true;
            this.pnlBotonesAccion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBotonesAccion.Location = new System.Drawing.Point(0, 663);
            this.pnlBotonesAccion.Name = "pnlBotonesAccion";
            this.pnlBotonesAccion.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.pnlBotonesAccion.Size = new System.Drawing.Size(746, 68);
            this.pnlBotonesAccion.TabIndex = 2;
            // 
            // frmContratoDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 731);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlBotonesAccion);
            this.Controls.Add(this.pnlTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmContratoDetalle";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmContratoDetalle";
            this.Load += new System.EventHandler(this.frmContratoDetalle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTitulo)).EndInit();
            this.pnlTitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlContenido)).EndInit();
            this.pnlContenido.ResumeLayout(false);
            this.pnlContenido.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboMercado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCliente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaContrato.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaContrato.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroContrato.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboZafra.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProducto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPresentacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVariacionAvg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToneladas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaFijarVolumen.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaFijarVolumen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToneladasMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToneladasMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAplicaNominacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEstado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPdf)).EndInit();
            this.pnlPdf.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.beArchivoPdf.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liMercado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liFechaContrato)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liNumeroContrato)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liZafra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liProducto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liPresentacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liPrecio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liVariacionAvg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liToneladas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liFechaFijarVolumen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liToneladasMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liToneladasMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liAplicaNominacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liEstado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liPdf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSeparador)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBotonesAccion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}