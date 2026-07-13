
namespace SistemaContable.UI.Forms.Bancos
{
    partial class frmCuentasBancarias
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
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.txtCTACONTABLE = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mskFECHA_APERTURA = new System.Windows.Forms.MaskedTextBox();
            this.cbxBANCO = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNOMBRE = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNUM_CUENTA = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCORRELATIVO_CHEQUE = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkActiva = new System.Windows.Forms.CheckBox();
            this.txtNOMBRE_CUENTA_CONTABLE = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Century Gothic", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1196, 68);
            this.label2.TabIndex = 2;
            this.label2.Text = "Banco:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(132, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(24, 25);
            this.comboBox1.TabIndex = 3;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtNOMBRE_CUENTA_CONTABLE);
            this.panel1.Controls.Add(this.chkActiva);
            this.panel1.Controls.Add(this.txtCORRELATIVO_CHEQUE);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btnSalir);
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Controls.Add(this.btnGuardar);
            this.panel1.Controls.Add(this.txtCTACONTABLE);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.mskFECHA_APERTURA);
            this.panel1.Controls.Add(this.cbxBANCO);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtNOMBRE);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtNUM_CUENTA);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1241, 422);
            this.panel1.TabIndex = 0;
            // 
            // btnSalir
            // 
            this.btnSalir.Appearance.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Appearance.Options.UseFont = true;
            this.btnSalir.Location = new System.Drawing.Point(691, 358);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(125, 35);
            this.btnSalir.TabIndex = 9;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Appearance.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Appearance.Options.UseFont = true;
            this.btnCancelar.Location = new System.Drawing.Point(535, 358);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(125, 35);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.Location = new System.Drawing.Point(379, 358);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(125, 35);
            this.btnGuardar.TabIndex = 7;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtCTACONTABLE
            // 
            this.txtCTACONTABLE.Location = new System.Drawing.Point(442, 214);
            this.txtCTACONTABLE.Name = "txtCTACONTABLE";
            this.txtCTACONTABLE.Size = new System.Drawing.Size(179, 26);
            this.txtCTACONTABLE.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(278, 214);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(158, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "Cuenta contable";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(276, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Fecha de apertura";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mskFECHA_APERTURA
            // 
            this.mskFECHA_APERTURA.Location = new System.Drawing.Point(442, 168);
            this.mskFECHA_APERTURA.Mask = "00/00/0000";
            this.mskFECHA_APERTURA.Name = "mskFECHA_APERTURA";
            this.mskFECHA_APERTURA.Size = new System.Drawing.Size(532, 26);
            this.mskFECHA_APERTURA.TabIndex = 3;
            // 
            // cbxBANCO
            // 
            this.cbxBANCO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBANCO.FormattingEnabled = true;
            this.cbxBANCO.Location = new System.Drawing.Point(442, 122);
            this.cbxBANCO.Name = "cbxBANCO";
            this.cbxBANCO.Size = new System.Drawing.Size(532, 27);
            this.cbxBANCO.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(356, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Banco";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNOMBRE
            // 
            this.txtNOMBRE.Location = new System.Drawing.Point(442, 77);
            this.txtNOMBRE.Name = "txtNOMBRE";
            this.txtNOMBRE.Size = new System.Drawing.Size(532, 26);
            this.txtNOMBRE.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(259, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nombre de la cuenta";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNUM_CUENTA
            // 
            this.txtNUM_CUENTA.Location = new System.Drawing.Point(442, 34);
            this.txtNUM_CUENTA.Name = "txtNUM_CUENTA";
            this.txtNUM_CUENTA.Size = new System.Drawing.Size(532, 26);
            this.txtNUM_CUENTA.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(294, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "N° de Cuenta";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCORRELATIVO_CHEQUE
            // 
            this.txtCORRELATIVO_CHEQUE.Location = new System.Drawing.Point(442, 260);
            this.txtCORRELATIVO_CHEQUE.Name = "txtCORRELATIVO_CHEQUE";
            this.txtCORRELATIVO_CHEQUE.Size = new System.Drawing.Size(532, 26);
            this.txtCORRELATIVO_CHEQUE.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(278, 260);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(158, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "Correlativo Cheque";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkActiva
            // 
            this.chkActiva.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActiva.Location = new System.Drawing.Point(379, 302);
            this.chkActiva.Name = "chkActiva";
            this.chkActiva.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkActiva.Size = new System.Drawing.Size(75, 21);
            this.chkActiva.TabIndex = 6;
            this.chkActiva.Text = "Activa";
            this.chkActiva.UseVisualStyleBackColor = true;
            // 
            // txtNOMBRE_CUENTA_CONTABLE
            // 
            this.txtNOMBRE_CUENTA_CONTABLE.BackColor = System.Drawing.SystemColors.Control;
            this.txtNOMBRE_CUENTA_CONTABLE.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtNOMBRE_CUENTA_CONTABLE.Location = new System.Drawing.Point(625, 215);
            this.txtNOMBRE_CUENTA_CONTABLE.Name = "txtNOMBRE_CUENTA_CONTABLE";
            this.txtNOMBRE_CUENTA_CONTABLE.ReadOnly = true;
            this.txtNOMBRE_CUENTA_CONTABLE.Size = new System.Drawing.Size(349, 26);
            this.txtNOMBRE_CUENTA_CONTABLE.TabIndex = 244;
            this.txtNOMBRE_CUENTA_CONTABLE.TabStop = false;
            // 
            // frmCuentasBancarias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1241, 422);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCuentasBancarias";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cuentas Bancarias";
            this.Load += new System.EventHandler(this.frmCuentasBancarias_Load);
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtNOMBRE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNUM_CUENTA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxBANCO;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCTACONTABLE;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox mskFECHA_APERTURA;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.TextBox txtCORRELATIVO_CHEQUE;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkActiva;
        private System.Windows.Forms.TextBox txtNOMBRE_CUENTA_CONTABLE;
    }
}