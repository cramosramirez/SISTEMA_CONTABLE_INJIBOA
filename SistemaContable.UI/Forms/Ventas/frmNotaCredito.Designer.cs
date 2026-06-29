namespace SistemaContable.UI.Forms.Ventas
{
    partial class frmNotaCredito
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            // Totales
            this.txtVENTA_GRAVADA = new System.Windows.Forms.TextBox();
            this.txtVENTA_EXENTA = new System.Windows.Forms.TextBox();
            this.txtIVA = new System.Windows.Forms.TextBox();
            this.txtSUBTOTAL = new System.Windows.Forms.TextBox();
            this.txtRETENCION = new System.Windows.Forms.TextBox();
            this.txtPERCEPCION = new System.Windows.Forms.TextBox();
            this.txtTOTAL_VENTA = new System.Windows.Forms.TextBox();
            this.txtDESCUENTO = new System.Windows.Forms.TextBox();
            this.txtPORC_DESCUENTO = new System.Windows.Forms.TextBox();
            // Formas de pago
            this.txtRECIB_EFECTIVO = new System.Windows.Forms.TextBox();
            this.txtRECIB_REMESA = new System.Windows.Forms.TextBox();
            this.txtRECIB_CHEQUE = new System.Windows.Forms.TextBox();
            this.txtRECIB_NOTAABONO = new System.Windows.Forms.TextBox();
            this.txtRECIB_ANTICIPO = new System.Windows.Forms.TextBox();
            this.txtRECIB_EFECTIVO_CAMBIO = new System.Windows.Forms.TextBox();
            this.txtRECIB_REMESA_BANCO = new System.Windows.Forms.TextBox();
            this.txtRECIB_REMESA_CUENTA = new System.Windows.Forms.TextBox();
            this.txtRECIB_REMESA_MONTO = new System.Windows.Forms.TextBox();
            this.txtRECIB_CHEQUE_BANCO = new System.Windows.Forms.TextBox();
            this.txtRECIB_CHEQUE_CUENTA = new System.Windows.Forms.TextBox();
            this.txtRECIB_CHEQUE_MONTO = new System.Windows.Forms.TextBox();
            this.txtRECIB_NOTAABONO_BANCO = new System.Windows.Forms.TextBox();
            this.txtRECIB_NOTAABONO_CUENTA = new System.Windows.Forms.TextBox();
            this.txtRECIB_NOTAABONO_MONTO = new System.Windows.Forms.TextBox();
            // Labels totales / formas pago
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.separatorControl3 = new DevExpress.XtraEditors.SeparatorControl();
            // GroupBox datos del documento
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkPERCEPCION = new DevExpress.XtraEditors.CheckEdit();
            this.cbxVENDEDOR = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.txtOBSERVACION = new System.Windows.Forms.TextBox();
            this.txtMOTIVO = new System.Windows.Forms.TextBox();
            this.lblMOTIVO = new System.Windows.Forms.Label();
            this.cbxCONDPAGO = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.txtNUMINTERNO = new System.Windows.Forms.TextBox();
            this.cbxSUCURSAL = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.mskFECHA_VENCE = new System.Windows.Forms.MaskedTextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.mskFECHA = new System.Windows.Forms.MaskedTextBox();
            this.cbxTIPO_DTE = new System.Windows.Forms.ComboBox();
            this.txtNUM_CONTROL = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtSELLO_RECIBIDO = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCOD_GENERACION = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            // Referencia doc origen (dentro de groupBox1)
            this.lblDocOrigenNum = new System.Windows.Forms.Label();
            this.txtDOC_ORIGEN_NUM = new System.Windows.Forms.TextBox();
            this.lblDocOrigenFecha = new System.Windows.Forms.Label();
            this.txtDOC_ORIGEN_FECHA = new System.Windows.Forms.TextBox();
            this.lblDocOrigenCodGen = new System.Windows.Forms.Label();
            this.txtDOC_ORIGEN_CODGEN = new System.Windows.Forms.TextBox();
            // GroupBox botones
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnFinalizar = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnValidar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCorreo = new DevExpress.XtraEditors.SimpleButton();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.btnNuevo = new DevExpress.XtraEditors.SimpleButton();
            // GroupBox datos del cliente
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelNRC = new System.Windows.Forms.Label();
            this.txtNRC = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtDIRECCION = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTIPO_CONTRIBUYENTE = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.txtACTIVIDAD_PRIMARIA = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.txtCORREO = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTELEFONO = new System.Windows.Forms.TextBox();
            this.txtNOMBRE_CLIENTE = new System.Windows.Forms.TextBox();
            this.txtNIT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDUI = new System.Windows.Forms.TextBox();
            this.txtCCF_ORIGEN = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkPERCEPCION.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl3)).BeginInit();
            this.SuspendLayout();

            // ── gridControl1 ─────────────────────────────────────────────
            this.gridControl1.Location = new System.Drawing.Point(11, 417);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1103, 130);
            this.gridControl1.TabIndex = 148;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridView1 });

            // ── gridView1 ─────────────────────────────────────────────────
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";

            // ── txtVENTA_GRAVADA ─────────────────────────────────────────
            this.txtVENTA_GRAVADA.BackColor = System.Drawing.SystemColors.Window;
            this.txtVENTA_GRAVADA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtVENTA_GRAVADA.Location = new System.Drawing.Point(961, 567);
            this.txtVENTA_GRAVADA.Name = "txtVENTA_GRAVADA";
            this.txtVENTA_GRAVADA.Size = new System.Drawing.Size(153, 22);
            this.txtVENTA_GRAVADA.TabIndex = 149;
            this.txtVENTA_GRAVADA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label4.Location = new System.Drawing.Point(846, 573);
            this.label4.Name = "label4";
            this.label4.Text = "Venta Gravado ($)";

            // ── txtVENTA_EXENTA ───────────────────────────────────────────
            this.txtVENTA_EXENTA.BackColor = System.Drawing.SystemColors.Window;
            this.txtVENTA_EXENTA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtVENTA_EXENTA.Location = new System.Drawing.Point(961, 623);
            this.txtVENTA_EXENTA.Name = "txtVENTA_EXENTA";
            this.txtVENTA_EXENTA.Size = new System.Drawing.Size(153, 22);
            this.txtVENTA_EXENTA.TabIndex = 157;
            this.txtVENTA_EXENTA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label5.Location = new System.Drawing.Point(847, 626);
            this.label5.Name = "label5";
            this.label5.Text = "Venta Exentas ($)";

            // ── txtIVA ────────────────────────────────────────────────────
            this.txtIVA.BackColor = System.Drawing.SystemColors.Window;
            this.txtIVA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtIVA.Location = new System.Drawing.Point(961, 679);
            this.txtIVA.Name = "txtIVA";
            this.txtIVA.Size = new System.Drawing.Size(153, 22);
            this.txtIVA.TabIndex = 159;
            this.txtIVA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label10.Location = new System.Drawing.Point(906, 682);
            this.label10.Name = "label10";
            this.label10.Text = "IVA ($)";

            // ── txtDESCUENTO / txtPORC_DESCUENTO ─────────────────────────
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label34.Location = new System.Drawing.Point(868, 654);
            this.label34.Name = "label34";
            this.label34.Text = "Descuento ($)";
            this.txtPORC_DESCUENTO.BackColor = System.Drawing.SystemColors.Window;
            this.txtPORC_DESCUENTO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtPORC_DESCUENTO.Location = new System.Drawing.Point(961, 651);
            this.txtPORC_DESCUENTO.Name = "txtPORC_DESCUENTO";
            this.txtPORC_DESCUENTO.Size = new System.Drawing.Size(39, 22);
            this.txtPORC_DESCUENTO.TabIndex = 183;
            this.txtPORC_DESCUENTO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDESCUENTO.BackColor = System.Drawing.SystemColors.Window;
            this.txtDESCUENTO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtDESCUENTO.Location = new System.Drawing.Point(1006, 651);
            this.txtDESCUENTO.Name = "txtDESCUENTO";
            this.txtDESCUENTO.Size = new System.Drawing.Size(108, 22);
            this.txtDESCUENTO.TabIndex = 185;
            this.txtDESCUENTO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            // ── txtSUBTOTAL ───────────────────────────────────────────────
            this.txtSUBTOTAL.BackColor = System.Drawing.SystemColors.Window;
            this.txtSUBTOTAL.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtSUBTOTAL.Location = new System.Drawing.Point(961, 707);
            this.txtSUBTOTAL.Name = "txtSUBTOTAL";
            this.txtSUBTOTAL.Size = new System.Drawing.Size(153, 22);
            this.txtSUBTOTAL.TabIndex = 166;
            this.txtSUBTOTAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label12.Location = new System.Drawing.Point(875, 710);
            this.label12.Name = "label12";
            this.label12.Text = "Sub Total ($)";

            // ── txtRETENCION ──────────────────────────────────────────────
            this.txtRETENCION.BackColor = System.Drawing.SystemColors.Window;
            this.txtRETENCION.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtRETENCION.Location = new System.Drawing.Point(961, 735);
            this.txtRETENCION.Name = "txtRETENCION";
            this.txtRETENCION.Size = new System.Drawing.Size(153, 22);
            this.txtRETENCION.TabIndex = 168;
            this.txtRETENCION.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label22.Location = new System.Drawing.Point(865, 738);
            this.label22.Name = "label22";
            this.label22.Text = "- Retención ($)";

            // ── txtPERCEPCION ─────────────────────────────────────────────
            this.txtPERCEPCION.BackColor = System.Drawing.SystemColors.Window;
            this.txtPERCEPCION.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtPERCEPCION.Location = new System.Drawing.Point(961, 763);
            this.txtPERCEPCION.Name = "txtPERCEPCION";
            this.txtPERCEPCION.Size = new System.Drawing.Size(153, 22);
            this.txtPERCEPCION.TabIndex = 170;
            this.txtPERCEPCION.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label23.Location = new System.Drawing.Point(857, 766);
            this.label23.Name = "label23";
            this.label23.Text = "+ Percepción ($)";

            // ── txtTOTAL_VENTA ────────────────────────────────────────────
            this.txtTOTAL_VENTA.BackColor = System.Drawing.SystemColors.Window;
            this.txtTOTAL_VENTA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtTOTAL_VENTA.Location = new System.Drawing.Point(961, 791);
            this.txtTOTAL_VENTA.Name = "txtTOTAL_VENTA";
            this.txtTOTAL_VENTA.Size = new System.Drawing.Size(153, 22);
            this.txtTOTAL_VENTA.TabIndex = 172;
            this.txtTOTAL_VENTA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label24.Location = new System.Drawing.Point(864, 794);
            this.label24.Name = "label24";
            this.label24.Text = "Total Venta ($)";

            // ── Formas de pago ────────────────────────────────────────────
            this.txtRECIB_EFECTIVO.BackColor = System.Drawing.SystemColors.Window;
            this.txtRECIB_EFECTIVO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtRECIB_EFECTIVO.Location = new System.Drawing.Point(678, 567);
            this.txtRECIB_EFECTIVO.Name = "txtRECIB_EFECTIVO";
            this.txtRECIB_EFECTIVO.Size = new System.Drawing.Size(153, 22);
            this.txtRECIB_EFECTIVO.TabIndex = 153;
            this.txtRECIB_EFECTIVO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label7.Location = new System.Drawing.Point(601, 570);
            this.label7.Text = "Efectivo ($)";

            this.txtRECIB_REMESA.BackColor = System.Drawing.SystemColors.Window;
            this.txtRECIB_REMESA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtRECIB_REMESA.Location = new System.Drawing.Point(678, 595);
            this.txtRECIB_REMESA.Name = "txtRECIB_REMESA";
            this.txtRECIB_REMESA.Size = new System.Drawing.Size(153, 22);
            this.txtRECIB_REMESA.TabIndex = 155;
            this.txtRECIB_REMESA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRECIB_REMESA.Leave += new System.EventHandler(this.txtRECIB_REMESA_Leave);
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label6.Location = new System.Drawing.Point(602, 598);
            this.label6.Text = "Remesa ($)";

            this.txtRECIB_CHEQUE.BackColor = System.Drawing.SystemColors.Window;
            this.txtRECIB_CHEQUE.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtRECIB_CHEQUE.Location = new System.Drawing.Point(678, 623);
            this.txtRECIB_CHEQUE.Name = "txtRECIB_CHEQUE";
            this.txtRECIB_CHEQUE.Size = new System.Drawing.Size(153, 22);
            this.txtRECIB_CHEQUE.TabIndex = 161;
            this.txtRECIB_CHEQUE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRECIB_CHEQUE.Leave += new System.EventHandler(this.txtRECIB_CHEQUE_Leave);
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label9.Location = new System.Drawing.Point(602, 626);
            this.label9.Text = "Cheque ($)";

            this.txtRECIB_NOTAABONO.BackColor = System.Drawing.SystemColors.Window;
            this.txtRECIB_NOTAABONO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtRECIB_NOTAABONO.Location = new System.Drawing.Point(678, 651);
            this.txtRECIB_NOTAABONO.Name = "txtRECIB_NOTAABONO";
            this.txtRECIB_NOTAABONO.Size = new System.Drawing.Size(153, 22);
            this.txtRECIB_NOTAABONO.TabIndex = 163;
            this.txtRECIB_NOTAABONO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRECIB_NOTAABONO.Leave += new System.EventHandler(this.txtRECIB_NOTAABONO_Leave);
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label8.Location = new System.Drawing.Point(560, 654);
            this.label8.Text = "Nota de Abono ($)";

            this.txtRECIB_ANTICIPO.BackColor = System.Drawing.SystemColors.Window;
            this.txtRECIB_ANTICIPO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtRECIB_ANTICIPO.Location = new System.Drawing.Point(678, 679);
            this.txtRECIB_ANTICIPO.Name = "txtRECIB_ANTICIPO";
            this.txtRECIB_ANTICIPO.Size = new System.Drawing.Size(153, 22);
            this.txtRECIB_ANTICIPO.TabIndex = 174;
            this.txtRECIB_ANTICIPO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label25.Location = new System.Drawing.Point(599, 682);
            this.label25.Text = "Anticipo ($)";

            this.txtRECIB_EFECTIVO_CAMBIO.BackColor = System.Drawing.SystemColors.Window;
            this.txtRECIB_EFECTIVO_CAMBIO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtRECIB_EFECTIVO_CAMBIO.Location = new System.Drawing.Point(678, 707);
            this.txtRECIB_EFECTIVO_CAMBIO.Name = "txtRECIB_EFECTIVO_CAMBIO";
            this.txtRECIB_EFECTIVO_CAMBIO.Size = new System.Drawing.Size(153, 22);
            this.txtRECIB_EFECTIVO_CAMBIO.TabIndex = 176;
            this.txtRECIB_EFECTIVO_CAMBIO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label26.Location = new System.Drawing.Point(607, 710);
            this.label26.Text = "Vuelto ($)";

            // ── Separadores y detalle bancario ────────────────────────────
            this.separatorControl1.BackColor = System.Drawing.Color.Transparent;
            this.separatorControl1.LineColor = System.Drawing.Color.Blue;
            this.separatorControl1.LineThickness = 1;
            this.separatorControl1.Location = new System.Drawing.Point(6, 580);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(539, 23);
            this.separatorControl1.TabIndex = 192;

            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold);
            this.label42.Location = new System.Drawing.Point(10, 565);
            this.label42.Text = "Remesa:";
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label43.Location = new System.Drawing.Point(10, 600);
            this.label43.Text = "Banco:";
            this.txtRECIB_REMESA_BANCO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtRECIB_REMESA_BANCO.Location = new System.Drawing.Point(11, 618);
            this.txtRECIB_REMESA_BANCO.Name = "txtRECIB_REMESA_BANCO";
            this.txtRECIB_REMESA_BANCO.Size = new System.Drawing.Size(223, 22);
            this.txtRECIB_REMESA_BANCO.TabIndex = 186;
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label40.Location = new System.Drawing.Point(237, 600);
            this.label40.Text = "N° Cuenta:";
            this.txtRECIB_REMESA_CUENTA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtRECIB_REMESA_CUENTA.Location = new System.Drawing.Point(237, 618);
            this.txtRECIB_REMESA_CUENTA.Name = "txtRECIB_REMESA_CUENTA";
            this.txtRECIB_REMESA_CUENTA.Size = new System.Drawing.Size(164, 22);
            this.txtRECIB_REMESA_CUENTA.TabIndex = 187;
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label41.Location = new System.Drawing.Point(406, 600);
            this.label41.Text = "N° Monto:";
            this.txtRECIB_REMESA_MONTO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtRECIB_REMESA_MONTO.Location = new System.Drawing.Point(402, 618);
            this.txtRECIB_REMESA_MONTO.Name = "txtRECIB_REMESA_MONTO";
            this.txtRECIB_REMESA_MONTO.Size = new System.Drawing.Size(144, 22);
            this.txtRECIB_REMESA_MONTO.TabIndex = 188;

            this.separatorControl2.BackColor = System.Drawing.Color.Transparent;
            this.separatorControl2.LineColor = System.Drawing.Color.Blue;
            this.separatorControl2.LineThickness = 1;
            this.separatorControl2.Location = new System.Drawing.Point(6, 648);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Size = new System.Drawing.Size(539, 23);
            this.separatorControl2.TabIndex = 193;

            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold);
            this.label44.Location = new System.Drawing.Point(10, 633);
            this.label44.Text = "Cheque:";
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label45.Location = new System.Drawing.Point(10, 668);
            this.label45.Text = "Banco:";
            this.txtRECIB_CHEQUE_BANCO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtRECIB_CHEQUE_BANCO.Location = new System.Drawing.Point(11, 686);
            this.txtRECIB_CHEQUE_BANCO.Name = "txtRECIB_CHEQUE_BANCO";
            this.txtRECIB_CHEQUE_BANCO.Size = new System.Drawing.Size(223, 22);
            this.txtRECIB_CHEQUE_BANCO.TabIndex = 196;
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label46.Location = new System.Drawing.Point(237, 668);
            this.label46.Text = "N° Cuenta:";
            this.txtRECIB_CHEQUE_CUENTA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtRECIB_CHEQUE_CUENTA.Location = new System.Drawing.Point(237, 686);
            this.txtRECIB_CHEQUE_CUENTA.Name = "txtRECIB_CHEQUE_CUENTA";
            this.txtRECIB_CHEQUE_CUENTA.Size = new System.Drawing.Size(164, 22);
            this.txtRECIB_CHEQUE_CUENTA.TabIndex = 197;
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label47.Location = new System.Drawing.Point(406, 668);
            this.label47.Text = "N° Monto:";
            this.txtRECIB_CHEQUE_MONTO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtRECIB_CHEQUE_MONTO.Location = new System.Drawing.Point(402, 686);
            this.txtRECIB_CHEQUE_MONTO.Name = "txtRECIB_CHEQUE_MONTO";
            this.txtRECIB_CHEQUE_MONTO.Size = new System.Drawing.Size(144, 22);
            this.txtRECIB_CHEQUE_MONTO.TabIndex = 198;

            this.separatorControl3.BackColor = System.Drawing.Color.Transparent;
            this.separatorControl3.LineColor = System.Drawing.Color.Blue;
            this.separatorControl3.LineThickness = 1;
            this.separatorControl3.Location = new System.Drawing.Point(6, 716);
            this.separatorControl3.Name = "separatorControl3";
            this.separatorControl3.Size = new System.Drawing.Size(539, 23);
            this.separatorControl3.TabIndex = 199;

            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold);
            this.label48.Location = new System.Drawing.Point(10, 701);
            this.label48.Text = "Nota de Abono:";
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label49.Location = new System.Drawing.Point(10, 736);
            this.label49.Text = "Banco:";
            this.txtRECIB_NOTAABONO_BANCO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtRECIB_NOTAABONO_BANCO.Location = new System.Drawing.Point(11, 754);
            this.txtRECIB_NOTAABONO_BANCO.Name = "txtRECIB_NOTAABONO_BANCO";
            this.txtRECIB_NOTAABONO_BANCO.Size = new System.Drawing.Size(223, 22);
            this.txtRECIB_NOTAABONO_BANCO.TabIndex = 200;
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label50.Location = new System.Drawing.Point(237, 736);
            this.label50.Text = "N° Cuenta:";
            this.txtRECIB_NOTAABONO_CUENTA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtRECIB_NOTAABONO_CUENTA.Location = new System.Drawing.Point(237, 754);
            this.txtRECIB_NOTAABONO_CUENTA.Name = "txtRECIB_NOTAABONO_CUENTA";
            this.txtRECIB_NOTAABONO_CUENTA.Size = new System.Drawing.Size(164, 22);
            this.txtRECIB_NOTAABONO_CUENTA.TabIndex = 201;
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label35.Location = new System.Drawing.Point(406, 736);
            this.label35.Text = "N° Monto:";
            this.txtRECIB_NOTAABONO_MONTO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtRECIB_NOTAABONO_MONTO.Location = new System.Drawing.Point(402, 754);
            this.txtRECIB_NOTAABONO_MONTO.Name = "txtRECIB_NOTAABONO_MONTO";
            this.txtRECIB_NOTAABONO_MONTO.Size = new System.Drawing.Size(144, 22);
            this.txtRECIB_NOTAABONO_MONTO.TabIndex = 202;

            // ── groupBox1 (Datos del Documento) ───────────────────────────
            this.groupBox1.Controls.Add(this.chkPERCEPCION);
            this.groupBox1.Controls.Add(this.cbxVENDEDOR);
            this.groupBox1.Controls.Add(this.label32);
            this.groupBox1.Controls.Add(this.label36);
            this.groupBox1.Controls.Add(this.txtOBSERVACION);
            this.groupBox1.Controls.Add(this.lblMOTIVO);
            this.groupBox1.Controls.Add(this.txtMOTIVO);
            this.groupBox1.Controls.Add(this.cbxCONDPAGO);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.txtNUMINTERNO);
            this.groupBox1.Controls.Add(this.cbxSUCURSAL);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.mskFECHA_VENCE);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.mskFECHA);
            this.groupBox1.Controls.Add(this.cbxTIPO_DTE);
            this.groupBox1.Controls.Add(this.txtNUM_CONTROL);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txtSELLO_RECIBIDO);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtCOD_GENERACION);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.lblDocOrigenNum);
            this.groupBox1.Controls.Add(this.txtDOC_ORIGEN_NUM);
            this.groupBox1.Controls.Add(this.lblDocOrigenFecha);
            this.groupBox1.Controls.Add(this.txtDOC_ORIGEN_FECHA);
            this.groupBox1.Controls.Add(this.lblDocOrigenCodGen);
            this.groupBox1.Controls.Add(this.txtDOC_ORIGEN_CODGEN);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(11, 149);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1105, 259);
            this.groupBox1.TabIndex = 178;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Documento";

            // chkPERCEPCION
            this.chkPERCEPCION.Location = new System.Drawing.Point(964, 218);
            this.chkPERCEPCION.Name = "chkPERCEPCION";
            this.chkPERCEPCION.Properties.Caption = "Percepcion";
            this.chkPERCEPCION.Size = new System.Drawing.Size(75, 20);
            this.chkPERCEPCION.TabIndex = 195;

            // cbxVENDEDOR
            this.cbxVENDEDOR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxVENDEDOR.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.cbxVENDEDOR.Location = new System.Drawing.Point(418, 80);
            this.cbxVENDEDOR.Name = "cbxVENDEDOR";
            this.cbxVENDEDOR.Size = new System.Drawing.Size(392, 22);
            this.cbxVENDEDOR.TabIndex = 193;

            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label32.Location = new System.Drawing.Point(351, 85);
            this.label32.Text = "Vendedor";

            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label36.Location = new System.Drawing.Point(50, 224);
            this.label36.Text = "Observación";
            this.txtOBSERVACION.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtOBSERVACION.Location = new System.Drawing.Point(129, 221);
            this.txtOBSERVACION.Multiline = true;
            this.txtOBSERVACION.Name = "txtOBSERVACION";
            this.txtOBSERVACION.Size = new System.Drawing.Size(716, 25);
            this.txtOBSERVACION.TabIndex = 191;

            // txtMOTIVO  (nuevo campo requerido para Nota de Crédito)
            this.lblMOTIVO.AutoSize = true;
            this.lblMOTIVO.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold);
            this.lblMOTIVO.ForeColor = System.Drawing.Color.DarkRed;
            this.lblMOTIVO.Location = new System.Drawing.Point(50, 196);
            this.lblMOTIVO.Name = "lblMOTIVO";
            this.lblMOTIVO.Text = "* Motivo";
            this.txtMOTIVO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtMOTIVO.Location = new System.Drawing.Point(129, 193);
            this.txtMOTIVO.Name = "txtMOTIVO";
            this.txtMOTIVO.Size = new System.Drawing.Size(820, 22);
            this.txtMOTIVO.TabIndex = 190;

            // cbxCONDPAGO
            this.cbxCONDPAGO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCONDPAGO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.cbxCONDPAGO.Location = new System.Drawing.Point(893, 22);
            this.cbxCONDPAGO.Name = "cbxCONDPAGO";
            this.cbxCONDPAGO.Size = new System.Drawing.Size(198, 22);
            this.cbxCONDPAGO.TabIndex = 185;
            this.cbxCONDPAGO.SelectedIndexChanged += new System.EventHandler(this.cbxCONDPAGO_SelectedIndexChanged);
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label29.Location = new System.Drawing.Point(828, 25);
            this.label29.Text = "Condicion";

            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label28.Location = new System.Drawing.Point(362, 27);
            this.label28.Text = "Numero";
            this.txtNUMINTERNO.BackColor = System.Drawing.SystemColors.Control;
            this.txtNUMINTERNO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtNUMINTERNO.Location = new System.Drawing.Point(418, 22);
            this.txtNUMINTERNO.Name = "txtNUMINTERNO";
            this.txtNUMINTERNO.ReadOnly = true;
            this.txtNUMINTERNO.Size = new System.Drawing.Size(137, 22);
            this.txtNUMINTERNO.TabIndex = 183;
            this.txtNUMINTERNO.TabStop = false;

            this.cbxSUCURSAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSUCURSAL.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.cbxSUCURSAL.Location = new System.Drawing.Point(129, 80);
            this.cbxSUCURSAL.Name = "cbxSUCURSAL";
            this.cbxSUCURSAL.Size = new System.Drawing.Size(198, 22);
            this.cbxSUCURSAL.TabIndex = 181;
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label27.Location = new System.Drawing.Point(72, 80);
            this.label27.Text = "Sucursal";

            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label19.Location = new System.Drawing.Point(845, 55);
            this.label19.Text = "Vence";
            this.mskFECHA_VENCE.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.mskFECHA_VENCE.Location = new System.Drawing.Point(893, 52);
            this.mskFECHA_VENCE.Mask = "00/00/0000";
            this.mskFECHA_VENCE.Name = "mskFECHA_VENCE";
            this.mskFECHA_VENCE.Size = new System.Drawing.Size(198, 22);
            this.mskFECHA_VENCE.TabIndex = 158;
            this.mskFECHA_VENCE.TextChanged += new System.EventHandler(this.mskFECHA_VENCE_TextChanged);

            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label21.Location = new System.Drawing.Point(571, 25);
            this.label21.Text = "Fecha NCR";
            this.mskFECHA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.mskFECHA.Location = new System.Drawing.Point(656, 22);
            this.mskFECHA.Mask = "00/00/0000";
            this.mskFECHA.Name = "mskFECHA";
            this.mskFECHA.Size = new System.Drawing.Size(154, 22);
            this.mskFECHA.TabIndex = 156;

            this.cbxTIPO_DTE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTIPO_DTE.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.cbxTIPO_DTE.Location = new System.Drawing.Point(129, 24);
            this.cbxTIPO_DTE.Name = "cbxTIPO_DTE";
            this.cbxTIPO_DTE.Size = new System.Drawing.Size(198, 22);
            this.cbxTIPO_DTE.TabIndex = 148;
            this.cbxTIPO_DTE.SelectedIndexChanged += new System.EventHandler(this.cbxTIPO_DTE_SelectedIndexChanged);
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label18.Location = new System.Drawing.Point(25, 27);
            this.label18.Text = "Tipo documento";

            this.txtNUM_CONTROL.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtNUM_CONTROL.Location = new System.Drawing.Point(647, 110);
            this.txtNUM_CONTROL.Name = "txtNUM_CONTROL";
            this.txtNUM_CONTROL.Size = new System.Drawing.Size(444, 22);
            this.txtNUM_CONTROL.TabIndex = 151;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label16.Location = new System.Drawing.Point(549, 113);
            this.label16.Text = "Res./N° Control";

            this.txtSELLO_RECIBIDO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtSELLO_RECIBIDO.Location = new System.Drawing.Point(129, 138);
            this.txtSELLO_RECIBIDO.Name = "txtSELLO_RECIBIDO";
            this.txtSELLO_RECIBIDO.Size = new System.Drawing.Size(418, 22);
            this.txtSELLO_RECIBIDO.TabIndex = 149;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label15.Location = new System.Drawing.Point(59, 141);
            this.label15.Text = "Serie/Sello";

            this.txtCOD_GENERACION.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtCOD_GENERACION.Location = new System.Drawing.Point(129, 108);
            this.txtCOD_GENERACION.Name = "txtCOD_GENERACION";
            this.txtCOD_GENERACION.Size = new System.Drawing.Size(418, 22);
            this.txtCOD_GENERACION.TabIndex = 150;
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label17.Location = new System.Drawing.Point(7, 113);
            this.label17.Text = "N°/Cod. Generación";

            // Referencia al CCF origen (dentro de groupBox1)
            this.lblDocOrigenNum.AutoSize = true;
            this.lblDocOrigenNum.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblDocOrigenNum.Location = new System.Drawing.Point(7, 167);
            this.lblDocOrigenNum.Name = "lblDocOrigenNum";
            this.lblDocOrigenNum.Text = "CCF N° Interno";
            this.txtDOC_ORIGEN_NUM.BackColor = System.Drawing.SystemColors.Control;
            this.txtDOC_ORIGEN_NUM.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtDOC_ORIGEN_NUM.Location = new System.Drawing.Point(129, 164);
            this.txtDOC_ORIGEN_NUM.Name = "txtDOC_ORIGEN_NUM";
            this.txtDOC_ORIGEN_NUM.ReadOnly = true;
            this.txtDOC_ORIGEN_NUM.Size = new System.Drawing.Size(120, 22);
            this.txtDOC_ORIGEN_NUM.TabStop = false;

            this.lblDocOrigenFecha.AutoSize = true;
            this.lblDocOrigenFecha.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblDocOrigenFecha.Location = new System.Drawing.Point(256, 167);
            this.lblDocOrigenFecha.Name = "lblDocOrigenFecha";
            this.lblDocOrigenFecha.Text = "Fecha CCF";
            this.txtDOC_ORIGEN_FECHA.BackColor = System.Drawing.SystemColors.Control;
            this.txtDOC_ORIGEN_FECHA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtDOC_ORIGEN_FECHA.Location = new System.Drawing.Point(335, 164);
            this.txtDOC_ORIGEN_FECHA.Name = "txtDOC_ORIGEN_FECHA";
            this.txtDOC_ORIGEN_FECHA.ReadOnly = true;
            this.txtDOC_ORIGEN_FECHA.Size = new System.Drawing.Size(110, 22);
            this.txtDOC_ORIGEN_FECHA.TabStop = false;

            this.lblDocOrigenCodGen.AutoSize = true;
            this.lblDocOrigenCodGen.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblDocOrigenCodGen.Location = new System.Drawing.Point(452, 167);
            this.lblDocOrigenCodGen.Name = "lblDocOrigenCodGen";
            this.lblDocOrigenCodGen.Text = "Cod. Generación CCF";
            this.txtDOC_ORIGEN_CODGEN.BackColor = System.Drawing.SystemColors.Control;
            this.txtDOC_ORIGEN_CODGEN.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtDOC_ORIGEN_CODGEN.Location = new System.Drawing.Point(578, 164);
            this.txtDOC_ORIGEN_CODGEN.Name = "txtDOC_ORIGEN_CODGEN";
            this.txtDOC_ORIGEN_CODGEN.ReadOnly = true;
            this.txtDOC_ORIGEN_CODGEN.Size = new System.Drawing.Size(330, 22);
            this.txtDOC_ORIGEN_CODGEN.TabStop = false;

            // ── groupBox2 (Botones) ───────────────────────────────────────
            this.groupBox2.Controls.Add(this.btnFinalizar);
            this.groupBox2.Controls.Add(this.btnGuardar);
            this.groupBox2.Controls.Add(this.btnValidar);
            this.groupBox2.Controls.Add(this.btnCorreo);
            this.groupBox2.Controls.Add(this.btnImprimir);
            this.groupBox2.Controls.Add(this.btnNuevo);
            this.groupBox2.Location = new System.Drawing.Point(1122, 229);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(115, 340);
            this.groupBox2.TabIndex = 179;
            this.groupBox2.TabStop = false;

            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.guardar2_32x32;
            this.btnGuardar.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnGuardar.ImageOptions.ImageToTextIndent = 10;
            this.btnGuardar.Location = new System.Drawing.Point(6, 10);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(101, 47);
            this.btnGuardar.TabIndex = 159;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            this.btnValidar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnValidar.Appearance.Options.UseFont = true;
            this.btnValidar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.validar3_32x32;
            this.btnValidar.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnValidar.ImageOptions.ImageToTextIndent = 10;
            this.btnValidar.Location = new System.Drawing.Point(6, 64);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Size = new System.Drawing.Size(101, 47);
            this.btnValidar.TabIndex = 160;
            this.btnValidar.Text = "Validar";

            this.btnImprimir.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnImprimir.Appearance.Options.UseFont = true;
            this.btnImprimir.Appearance.Options.UseTextOptions = true;
            this.btnImprimir.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnImprimir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.imprimir32x32;
            this.btnImprimir.ImageOptions.ImageToTextIndent = 10;
            this.btnImprimir.Location = new System.Drawing.Point(6, 117);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(101, 47);
            this.btnImprimir.TabIndex = 161;
            this.btnImprimir.Text = "Imprimir";

            this.btnCorreo.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnCorreo.Appearance.Options.UseFont = true;
            this.btnCorreo.Appearance.Options.UseTextOptions = true;
            this.btnCorreo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnCorreo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.correo32x32;
            this.btnCorreo.ImageOptions.ImageToTextIndent = 10;
            this.btnCorreo.Location = new System.Drawing.Point(5, 170);
            this.btnCorreo.Name = "btnCorreo";
            this.btnCorreo.Size = new System.Drawing.Size(102, 47);
            this.btnCorreo.TabIndex = 162;
            this.btnCorreo.Text = "Correo";

            this.btnNuevo.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.Appearance.Options.UseFont = true;
            this.btnNuevo.Appearance.Options.UseTextOptions = true;
            this.btnNuevo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnNuevo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevo.ImageOptions.ImageToTextIndent = 10;
            this.btnNuevo.Location = new System.Drawing.Point(6, 224);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(101, 47);
            this.btnNuevo.TabIndex = 203;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);

            this.btnFinalizar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnFinalizar.Appearance.Options.UseFont = true;
            this.btnFinalizar.Appearance.Options.UseTextOptions = true;
            this.btnFinalizar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnFinalizar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnFinalizar.Location = new System.Drawing.Point(6, 278);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(101, 47);
            this.btnFinalizar.TabIndex = 163;
            this.btnFinalizar.TabStop = false;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.Click += new System.EventHandler(this.btnSalir_Click);

            // ── groupBox3 (Datos del Cliente) ─────────────────────────────
            this.groupBox3.Controls.Add(this.labelNRC);
            this.groupBox3.Controls.Add(this.txtNRC);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.txtDIRECCION);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtTIPO_CONTRIBUYENTE);
            this.groupBox3.Controls.Add(this.label39);
            this.groupBox3.Controls.Add(this.txtACTIVIDAD_PRIMARIA);
            this.groupBox3.Controls.Add(this.label38);
            this.groupBox3.Controls.Add(this.label37);
            this.groupBox3.Controls.Add(this.txtCORREO);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtTELEFONO);
            this.groupBox3.Controls.Add(this.txtNOMBRE_CLIENTE);
            this.groupBox3.Controls.Add(this.txtNIT);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtDUI);
            this.groupBox3.Controls.Add(this.txtCCF_ORIGEN);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(11, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1105, 139);
            this.groupBox3.TabIndex = 180;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos del Cliente";

            this.labelNRC.AutoSize = true;
            this.labelNRC.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.labelNRC.Location = new System.Drawing.Point(618, 110);
            this.labelNRC.Text = "NRC";
            this.txtNRC.BackColor = System.Drawing.SystemColors.Control;
            this.txtNRC.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtNRC.Location = new System.Drawing.Point(660, 107);
            this.txtNRC.Name = "txtNRC";
            this.txtNRC.ReadOnly = true;
            this.txtNRC.Size = new System.Drawing.Size(210, 22);
            this.txtNRC.TabStop = false;

            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label11.Location = new System.Drawing.Point(17, 110);
            this.label11.Text = "Correo CC";
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.textBox1.Location = new System.Drawing.Point(78, 107);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(423, 22);
            this.textBox1.TabStop = false;

            this.txtDIRECCION.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIRECCION.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtDIRECCION.Location = new System.Drawing.Point(79, 79);
            this.txtDIRECCION.Name = "txtDIRECCION";
            this.txtDIRECCION.ReadOnly = true;
            this.txtDIRECCION.Size = new System.Drawing.Size(422, 22);
            this.txtDIRECCION.TabStop = false;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label1.Location = new System.Drawing.Point(17, 82);
            this.label1.Text = "Dirección";

            this.txtTIPO_CONTRIBUYENTE.BackColor = System.Drawing.SystemColors.Control;
            this.txtTIPO_CONTRIBUYENTE.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtTIPO_CONTRIBUYENTE.Location = new System.Drawing.Point(659, 81);
            this.txtTIPO_CONTRIBUYENTE.Name = "txtTIPO_CONTRIBUYENTE";
            this.txtTIPO_CONTRIBUYENTE.ReadOnly = true;
            this.txtTIPO_CONTRIBUYENTE.Size = new System.Drawing.Size(440, 22);
            this.txtTIPO_CONTRIBUYENTE.TabStop = false;
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label39.Location = new System.Drawing.Point(523, 84);
            this.label39.Text = "Tipo de contribuyente";

            this.txtACTIVIDAD_PRIMARIA.BackColor = System.Drawing.SystemColors.Control;
            this.txtACTIVIDAD_PRIMARIA.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtACTIVIDAD_PRIMARIA.Location = new System.Drawing.Point(659, 53);
            this.txtACTIVIDAD_PRIMARIA.Name = "txtACTIVIDAD_PRIMARIA";
            this.txtACTIVIDAD_PRIMARIA.ReadOnly = true;
            this.txtACTIVIDAD_PRIMARIA.Size = new System.Drawing.Size(441, 22);
            this.txtACTIVIDAD_PRIMARIA.TabStop = false;
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label38.Location = new System.Drawing.Point(578, 56);
            this.label38.Text = "Act. Primaria";

            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label37.Location = new System.Drawing.Point(243, 56);
            this.label37.Text = "Correo";
            this.txtCORREO.BackColor = System.Drawing.SystemColors.Control;
            this.txtCORREO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtCORREO.Location = new System.Drawing.Point(292, 51);
            this.txtCORREO.Name = "txtCORREO";
            this.txtCORREO.ReadOnly = true;
            this.txtCORREO.Size = new System.Drawing.Size(263, 22);
            this.txtCORREO.TabStop = false;

            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label13.Location = new System.Drawing.Point(17, 54);
            this.label13.Text = "Telefono";
            this.txtTELEFONO.BackColor = System.Drawing.SystemColors.Control;
            this.txtTELEFONO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtTELEFONO.Location = new System.Drawing.Point(79, 51);
            this.txtTELEFONO.Name = "txtTELEFONO";
            this.txtTELEFONO.ReadOnly = true;
            this.txtTELEFONO.Size = new System.Drawing.Size(153, 22);
            this.txtTELEFONO.TabStop = false;

            this.txtNOMBRE_CLIENTE.BackColor = System.Drawing.SystemColors.Control;
            this.txtNOMBRE_CLIENTE.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtNOMBRE_CLIENTE.Location = new System.Drawing.Point(232, 23);
            this.txtNOMBRE_CLIENTE.Name = "txtNOMBRE_CLIENTE";
            this.txtNOMBRE_CLIENTE.ReadOnly = true;
            this.txtNOMBRE_CLIENTE.Size = new System.Drawing.Size(456, 22);
            this.txtNOMBRE_CLIENTE.TabStop = false;

            this.txtNIT.BackColor = System.Drawing.SystemColors.Control;
            this.txtNIT.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtNIT.Location = new System.Drawing.Point(889, 23);
            this.txtNIT.Name = "txtNIT";
            this.txtNIT.ReadOnly = true;
            this.txtNIT.Size = new System.Drawing.Size(210, 22);
            this.txtNIT.TabStop = false;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label2.Location = new System.Drawing.Point(849, 26);
            this.label2.Text = "NIT";

            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.label3.Location = new System.Drawing.Point(694, 26);
            this.label3.Text = "DUI";
            this.txtDUI.BackColor = System.Drawing.SystemColors.Control;
            this.txtDUI.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtDUI.Location = new System.Drawing.Point(737, 23);
            this.txtDUI.Name = "txtDUI";
            this.txtDUI.ReadOnly = true;
            this.txtDUI.Size = new System.Drawing.Size(106, 22);
            this.txtDUI.TabStop = false;

            // txtCCF_ORIGEN (campo de búsqueda del CCF - reemplaza txtCLIENTE)
            this.txtCCF_ORIGEN.BackColor = System.Drawing.SystemColors.Window;
            this.txtCCF_ORIGEN.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtCCF_ORIGEN.Location = new System.Drawing.Point(100, 23);
            this.txtCCF_ORIGEN.Name = "txtCCF_ORIGEN";
            this.txtCCF_ORIGEN.Size = new System.Drawing.Size(130, 22);
            this.txtCCF_ORIGEN.TabIndex = 132;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.DarkBlue;
            this.label14.Location = new System.Drawing.Point(10, 26);
            this.label14.Name = "label14";
            this.label14.Text = "CCF Origen";

            // ── Form principal ────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 820);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            // Totales
            this.Controls.Add(this.txtVENTA_GRAVADA);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtVENTA_EXENTA);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtIVA);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.txtPORC_DESCUENTO);
            this.Controls.Add(this.txtDESCUENTO);
            this.Controls.Add(this.txtSUBTOTAL);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtRETENCION);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.txtPERCEPCION);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.txtTOTAL_VENTA);
            this.Controls.Add(this.label24);
            // Formas de pago
            this.Controls.Add(this.txtRECIB_EFECTIVO);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtRECIB_REMESA);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtRECIB_CHEQUE);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtRECIB_NOTAABONO);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtRECIB_ANTICIPO);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.txtRECIB_EFECTIVO_CAMBIO);
            this.Controls.Add(this.label26);
            // Detalle bancario
            this.Controls.Add(this.separatorControl1);
            this.Controls.Add(this.label42);
            this.Controls.Add(this.label43);
            this.Controls.Add(this.txtRECIB_REMESA_BANCO);
            this.Controls.Add(this.label40);
            this.Controls.Add(this.txtRECIB_REMESA_CUENTA);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.txtRECIB_REMESA_MONTO);
            this.Controls.Add(this.separatorControl2);
            this.Controls.Add(this.label44);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.txtRECIB_CHEQUE_BANCO);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.txtRECIB_CHEQUE_CUENTA);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.txtRECIB_CHEQUE_MONTO);
            this.Controls.Add(this.separatorControl3);
            this.Controls.Add(this.label48);
            this.Controls.Add(this.label49);
            this.Controls.Add(this.txtRECIB_NOTAABONO_BANCO);
            this.Controls.Add(this.label50);
            this.Controls.Add(this.txtRECIB_NOTAABONO_CUENTA);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.txtRECIB_NOTAABONO_MONTO);
            this.Name = "frmNotaCredito";
            this.Text = "Nota de Crédito";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmNotaCredito_Load);

            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkPERCEPCION.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        // ── Control declarations ─────────────────────────────────────────
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        // Totales
        private System.Windows.Forms.TextBox txtVENTA_GRAVADA;
        private System.Windows.Forms.TextBox txtVENTA_EXENTA;
        private System.Windows.Forms.TextBox txtIVA;
        private System.Windows.Forms.TextBox txtSUBTOTAL;
        private System.Windows.Forms.TextBox txtRETENCION;
        private System.Windows.Forms.TextBox txtPERCEPCION;
        private System.Windows.Forms.TextBox txtTOTAL_VENTA;
        private System.Windows.Forms.TextBox txtDESCUENTO;
        private System.Windows.Forms.TextBox txtPORC_DESCUENTO;
        // Formas de pago
        private System.Windows.Forms.TextBox txtRECIB_EFECTIVO;
        private System.Windows.Forms.TextBox txtRECIB_REMESA;
        private System.Windows.Forms.TextBox txtRECIB_CHEQUE;
        private System.Windows.Forms.TextBox txtRECIB_NOTAABONO;
        private System.Windows.Forms.TextBox txtRECIB_ANTICIPO;
        private System.Windows.Forms.TextBox txtRECIB_EFECTIVO_CAMBIO;
        private System.Windows.Forms.TextBox txtRECIB_REMESA_BANCO;
        private System.Windows.Forms.TextBox txtRECIB_REMESA_CUENTA;
        private System.Windows.Forms.TextBox txtRECIB_REMESA_MONTO;
        private System.Windows.Forms.TextBox txtRECIB_CHEQUE_BANCO;
        private System.Windows.Forms.TextBox txtRECIB_CHEQUE_CUENTA;
        private System.Windows.Forms.TextBox txtRECIB_CHEQUE_MONTO;
        private System.Windows.Forms.TextBox txtRECIB_NOTAABONO_BANCO;
        private System.Windows.Forms.TextBox txtRECIB_NOTAABONO_CUENTA;
        private System.Windows.Forms.TextBox txtRECIB_NOTAABONO_MONTO;
        // Labels
        private System.Windows.Forms.Label label1, label2, label3, label4, label5;
        private System.Windows.Forms.Label label6, label7, label8, label9, label10;
        private System.Windows.Forms.Label label11, label12, label13, label14;
        private System.Windows.Forms.Label label15, label16, label17, label18, label19;
        private System.Windows.Forms.Label label21, label22, label23, label24, label25;
        private System.Windows.Forms.Label label26, label27, label28, label29, label32;
        private System.Windows.Forms.Label label34, label35, label36, label37, label38;
        private System.Windows.Forms.Label label39, label40, label41, label42, label43;
        private System.Windows.Forms.Label label44, label45, label46, label47, label48;
        private System.Windows.Forms.Label label49, label50;
        // Separadores
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private DevExpress.XtraEditors.SeparatorControl separatorControl3;
        // GroupBox datos del documento
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.CheckEdit chkPERCEPCION;
        private System.Windows.Forms.ComboBox cbxVENDEDOR;
        private System.Windows.Forms.TextBox txtOBSERVACION;
        private System.Windows.Forms.TextBox txtMOTIVO;
        private System.Windows.Forms.Label lblMOTIVO;
        private System.Windows.Forms.ComboBox cbxCONDPAGO;
        private System.Windows.Forms.TextBox txtNUMINTERNO;
        private System.Windows.Forms.ComboBox cbxSUCURSAL;
        private System.Windows.Forms.MaskedTextBox mskFECHA_VENCE;
        private System.Windows.Forms.MaskedTextBox mskFECHA;
        private System.Windows.Forms.ComboBox cbxTIPO_DTE;
        private System.Windows.Forms.TextBox txtNUM_CONTROL;
        private System.Windows.Forms.TextBox txtSELLO_RECIBIDO;
        private System.Windows.Forms.TextBox txtCOD_GENERACION;
        // Referencia CCF origen
        private System.Windows.Forms.Label lblDocOrigenNum;
        private System.Windows.Forms.TextBox txtDOC_ORIGEN_NUM;
        private System.Windows.Forms.Label lblDocOrigenFecha;
        private System.Windows.Forms.TextBox txtDOC_ORIGEN_FECHA;
        private System.Windows.Forms.Label lblDocOrigenCodGen;
        private System.Windows.Forms.TextBox txtDOC_ORIGEN_CODGEN;
        // GroupBox botones
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton btnFinalizar;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraEditors.SimpleButton btnValidar;
        private DevExpress.XtraEditors.SimpleButton btnCorreo;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        // GroupBox datos del cliente
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labelNRC;
        private System.Windows.Forms.TextBox txtNRC;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtDIRECCION;
        private System.Windows.Forms.TextBox txtTIPO_CONTRIBUYENTE;
        private System.Windows.Forms.TextBox txtACTIVIDAD_PRIMARIA;
        private System.Windows.Forms.TextBox txtCORREO;
        private System.Windows.Forms.TextBox txtTELEFONO;
        private System.Windows.Forms.TextBox txtNOMBRE_CLIENTE;
        private System.Windows.Forms.TextBox txtNIT;
        private System.Windows.Forms.TextBox txtDUI;
        private System.Windows.Forms.TextBox txtCCF_ORIGEN;
    }
}