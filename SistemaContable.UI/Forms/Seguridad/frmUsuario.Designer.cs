namespace SistemaContable.UI.Forms.Seguridad
{
    partial class frmUsuario
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblUSUARIO = new System.Windows.Forms.Label();
            this.txtUSUARIO = new System.Windows.Forms.TextBox();
            this.lblValidacionUsuario = new System.Windows.Forms.Label();
            this.lblID_ROL = new System.Windows.Forms.Label();
            this.cbxID_ROL = new System.Windows.Forms.ComboBox();
            this.lblNOMBRE = new System.Windows.Forms.Label();
            this.txtNOMBRE = new System.Windows.Forms.TextBox();
            this.lblEMAIL = new System.Windows.Forms.Label();
            this.txtEMAIL = new System.Windows.Forms.TextBox();
            this.lblCLAVE = new System.Windows.Forms.Label();
            this.txtCLAVE = new System.Windows.Forms.TextBox();
            this.lblCLAVE_CONFIRMAR = new System.Windows.Forms.Label();
            this.txtCLAVE_CONFIRMAR = new System.Windows.Forms.TextBox();
            this.chkACTIVO = new System.Windows.Forms.CheckBox();
            this.chkBLOQUEADO = new System.Windows.Forms.CheckBox();
            this.lblFECHA_ULTACCESO_TITULO = new System.Windows.Forms.Label();
            this.lblFECHA_ULTACCESO = new System.Windows.Forms.Label();
            this.btnNuevo = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnEliminar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.BackColor = System.Drawing.Color.Blue;
            this.groupControl1.AppearanceCaption.BackColor2 = System.Drawing.Color.Blue;
            this.groupControl1.AppearanceCaption.Options.UseBackColor = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.lblUSUARIO);
            this.groupControl1.Controls.Add(this.txtUSUARIO);
            this.groupControl1.Controls.Add(this.lblValidacionUsuario);
            this.groupControl1.Controls.Add(this.lblID_ROL);
            this.groupControl1.Controls.Add(this.cbxID_ROL);
            this.groupControl1.Controls.Add(this.lblNOMBRE);
            this.groupControl1.Controls.Add(this.txtNOMBRE);
            this.groupControl1.Controls.Add(this.lblEMAIL);
            this.groupControl1.Controls.Add(this.txtEMAIL);
            this.groupControl1.Controls.Add(this.lblCLAVE);
            this.groupControl1.Controls.Add(this.txtCLAVE);
            this.groupControl1.Controls.Add(this.lblCLAVE_CONFIRMAR);
            this.groupControl1.Controls.Add(this.txtCLAVE_CONFIRMAR);
            this.groupControl1.Controls.Add(this.chkACTIVO);
            this.groupControl1.Controls.Add(this.chkBLOQUEADO);
            this.groupControl1.Controls.Add(this.lblFECHA_ULTACCESO_TITULO);
            this.groupControl1.Controls.Add(this.lblFECHA_ULTACCESO);
            this.groupControl1.Location = new System.Drawing.Point(8, 2);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(322, 252);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Datos del Usuario";
            // 
            // lblUSUARIO
            // 
            this.lblUSUARIO.AutoSize = true;
            this.lblUSUARIO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblUSUARIO.Location = new System.Drawing.Point(47, 37);
            this.lblUSUARIO.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUSUARIO.Name = "lblUSUARIO";
            this.lblUSUARIO.Size = new System.Drawing.Size(46, 14);
            this.lblUSUARIO.TabIndex = 0;
            this.lblUSUARIO.Text = "Usuario";
            // 
            // txtUSUARIO
            // 
            this.txtUSUARIO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtUSUARIO.Location = new System.Drawing.Point(98, 34);
            this.txtUSUARIO.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtUSUARIO.MaxLength = 100;
            this.txtUSUARIO.Name = "txtUSUARIO";
            this.txtUSUARIO.Size = new System.Drawing.Size(98, 22);
            this.txtUSUARIO.TabIndex = 1;
            this.txtUSUARIO.Leave += new System.EventHandler(this.txtUSUARIO_Leave);
            // 
            // lblValidacionUsuario
            // 
            this.lblValidacionUsuario.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblValidacionUsuario.Location = new System.Drawing.Point(201, 34);
            this.lblValidacionUsuario.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblValidacionUsuario.Name = "lblValidacionUsuario";
            this.lblValidacionUsuario.Size = new System.Drawing.Size(26, 18);
            this.lblValidacionUsuario.TabIndex = 99;
            this.lblValidacionUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblID_ROL
            // 
            this.lblID_ROL.AutoSize = true;
            this.lblID_ROL.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblID_ROL.Location = new System.Drawing.Point(70, 64);
            this.lblID_ROL.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblID_ROL.Name = "lblID_ROL";
            this.lblID_ROL.Size = new System.Drawing.Size(23, 14);
            this.lblID_ROL.TabIndex = 2;
            this.lblID_ROL.Text = "Rol";
            // 
            // cbxID_ROL
            // 
            this.cbxID_ROL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxID_ROL.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.cbxID_ROL.FormattingEnabled = true;
            this.cbxID_ROL.Location = new System.Drawing.Point(98, 61);
            this.cbxID_ROL.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbxID_ROL.Name = "cbxID_ROL";
            this.cbxID_ROL.Size = new System.Drawing.Size(195, 22);
            this.cbxID_ROL.TabIndex = 3;
            // 
            // lblNOMBRE
            // 
            this.lblNOMBRE.AutoSize = true;
            this.lblNOMBRE.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblNOMBRE.Location = new System.Drawing.Point(43, 91);
            this.lblNOMBRE.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNOMBRE.Name = "lblNOMBRE";
            this.lblNOMBRE.Size = new System.Drawing.Size(50, 14);
            this.lblNOMBRE.TabIndex = 4;
            this.lblNOMBRE.Text = "Nombre";
            // 
            // txtNOMBRE
            // 
            this.txtNOMBRE.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtNOMBRE.Location = new System.Drawing.Point(98, 88);
            this.txtNOMBRE.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNOMBRE.MaxLength = 200;
            this.txtNOMBRE.Name = "txtNOMBRE";
            this.txtNOMBRE.Size = new System.Drawing.Size(195, 22);
            this.txtNOMBRE.TabIndex = 5;
            // 
            // lblEMAIL
            // 
            this.lblEMAIL.AutoSize = true;
            this.lblEMAIL.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblEMAIL.Location = new System.Drawing.Point(59, 115);
            this.lblEMAIL.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEMAIL.Name = "lblEMAIL";
            this.lblEMAIL.Size = new System.Drawing.Size(34, 14);
            this.lblEMAIL.TabIndex = 6;
            this.lblEMAIL.Text = "Email";
            // 
            // txtEMAIL
            // 
            this.txtEMAIL.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtEMAIL.Location = new System.Drawing.Point(98, 115);
            this.txtEMAIL.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtEMAIL.MaxLength = 100;
            this.txtEMAIL.Name = "txtEMAIL";
            this.txtEMAIL.Size = new System.Drawing.Size(195, 22);
            this.txtEMAIL.TabIndex = 7;
            // 
            // lblCLAVE
            // 
            this.lblCLAVE.AutoSize = true;
            this.lblCLAVE.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblCLAVE.Location = new System.Drawing.Point(59, 144);
            this.lblCLAVE.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCLAVE.Name = "lblCLAVE";
            this.lblCLAVE.Size = new System.Drawing.Size(35, 14);
            this.lblCLAVE.TabIndex = 8;
            this.lblCLAVE.Text = "Clave";
            // 
            // txtCLAVE
            // 
            this.txtCLAVE.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtCLAVE.Location = new System.Drawing.Point(98, 141);
            this.txtCLAVE.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCLAVE.MaxLength = 100;
            this.txtCLAVE.Name = "txtCLAVE";
            this.txtCLAVE.PasswordChar = '*';
            this.txtCLAVE.Size = new System.Drawing.Size(195, 22);
            this.txtCLAVE.TabIndex = 9;
            // 
            // lblCLAVE_CONFIRMAR
            // 
            this.lblCLAVE_CONFIRMAR.AutoSize = true;
            this.lblCLAVE_CONFIRMAR.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblCLAVE_CONFIRMAR.Location = new System.Drawing.Point(4, 171);
            this.lblCLAVE_CONFIRMAR.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCLAVE_CONFIRMAR.Name = "lblCLAVE_CONFIRMAR";
            this.lblCLAVE_CONFIRMAR.Size = new System.Drawing.Size(90, 14);
            this.lblCLAVE_CONFIRMAR.TabIndex = 10;
            this.lblCLAVE_CONFIRMAR.Text = "Confirmar Clave";
            // 
            // txtCLAVE_CONFIRMAR
            // 
            this.txtCLAVE_CONFIRMAR.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.txtCLAVE_CONFIRMAR.Location = new System.Drawing.Point(98, 168);
            this.txtCLAVE_CONFIRMAR.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCLAVE_CONFIRMAR.MaxLength = 100;
            this.txtCLAVE_CONFIRMAR.Name = "txtCLAVE_CONFIRMAR";
            this.txtCLAVE_CONFIRMAR.PasswordChar = '*';
            this.txtCLAVE_CONFIRMAR.Size = new System.Drawing.Size(195, 22);
            this.txtCLAVE_CONFIRMAR.TabIndex = 11;
            // 
            // chkACTIVO
            // 
            this.chkACTIVO.AutoSize = true;
            this.chkACTIVO.Checked = true;
            this.chkACTIVO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkACTIVO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.chkACTIVO.Location = new System.Drawing.Point(82, 197);
            this.chkACTIVO.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkACTIVO.Name = "chkACTIVO";
            this.chkACTIVO.Size = new System.Drawing.Size(60, 18);
            this.chkACTIVO.TabIndex = 12;
            this.chkACTIVO.Text = "Activo";
            this.chkACTIVO.UseVisualStyleBackColor = true;
            // 
            // chkBLOQUEADO
            // 
            this.chkBLOQUEADO.AutoSize = true;
            this.chkBLOQUEADO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.chkBLOQUEADO.Location = new System.Drawing.Point(165, 197);
            this.chkBLOQUEADO.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkBLOQUEADO.Name = "chkBLOQUEADO";
            this.chkBLOQUEADO.Size = new System.Drawing.Size(83, 18);
            this.chkBLOQUEADO.TabIndex = 13;
            this.chkBLOQUEADO.Text = "Bloqueado";
            this.chkBLOQUEADO.UseVisualStyleBackColor = true;
            // 
            // lblFECHA_ULTACCESO_TITULO
            // 
            this.lblFECHA_ULTACCESO_TITULO.AutoSize = true;
            this.lblFECHA_ULTACCESO_TITULO.Font = new System.Drawing.Font("Tahoma", 8.765218F);
            this.lblFECHA_ULTACCESO_TITULO.Location = new System.Drawing.Point(15, 226);
            this.lblFECHA_ULTACCESO_TITULO.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFECHA_ULTACCESO_TITULO.Name = "lblFECHA_ULTACCESO_TITULO";
            this.lblFECHA_ULTACCESO_TITULO.Size = new System.Drawing.Size(82, 14);
            this.lblFECHA_ULTACCESO_TITULO.TabIndex = 14;
            this.lblFECHA_ULTACCESO_TITULO.Text = "Último acceso";
            // 
            // lblFECHA_ULTACCESO
            // 
            this.lblFECHA_ULTACCESO.AutoSize = true;
            this.lblFECHA_ULTACCESO.Font = new System.Drawing.Font("Tahoma", 8.765218F, System.Drawing.FontStyle.Italic);
            this.lblFECHA_ULTACCESO.Location = new System.Drawing.Point(101, 226);
            this.lblFECHA_ULTACCESO.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFECHA_ULTACCESO.Name = "lblFECHA_ULTACCESO";
            this.lblFECHA_ULTACCESO.Size = new System.Drawing.Size(41, 14);
            this.lblFECHA_ULTACCESO.TabIndex = 15;
            this.lblFECHA_ULTACCESO.Text = "Nunca";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.Appearance.Options.UseFont = true;
            this.btnNuevo.Appearance.Options.UseTextOptions = true;
            this.btnNuevo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnNuevo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevo.Location = new System.Drawing.Point(334, 8);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(2);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(87, 38);
            this.btnNuevo.TabIndex = 20;
            this.btnNuevo.TabStop = false;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.guardar2_32x32;
            this.btnGuardar.Location = new System.Drawing.Point(334, 52);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(87, 38);
            this.btnGuardar.TabIndex = 21;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.Appearance.Options.UseFont = true;
            this.btnEliminar.Appearance.Options.UseTextOptions = true;
            this.btnEliminar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnEliminar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.eliminar32x32;
            this.btnEliminar.Location = new System.Drawing.Point(334, 96);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(87, 38);
            this.btnEliminar.TabIndex = 22;
            this.btnEliminar.TabStop = false;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnSalir.Appearance.Options.UseFont = true;
            this.btnSalir.Appearance.Options.UseTextOptions = true;
            this.btnSalir.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnSalir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnSalir.Location = new System.Drawing.Point(334, 140);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(2);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(87, 38);
            this.btnSalir.TabIndex = 23;
            this.btnSalir.TabStop = false;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // frmUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 268);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnSalir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Usuario";
            this.Load += new System.EventHandler(this.frmUsuario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraEditors.SimpleButton btnEliminar;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Label lblUSUARIO;
        private System.Windows.Forms.TextBox txtUSUARIO;
        private System.Windows.Forms.Label lblValidacionUsuario;
        private System.Windows.Forms.Label lblID_ROL;
        private System.Windows.Forms.ComboBox cbxID_ROL;
        private System.Windows.Forms.Label lblNOMBRE;
        private System.Windows.Forms.TextBox txtNOMBRE;
        private System.Windows.Forms.Label lblEMAIL;
        private System.Windows.Forms.TextBox txtEMAIL;
        private System.Windows.Forms.Label lblCLAVE;
        private System.Windows.Forms.TextBox txtCLAVE;
        private System.Windows.Forms.Label lblCLAVE_CONFIRMAR;
        private System.Windows.Forms.TextBox txtCLAVE_CONFIRMAR;
        private System.Windows.Forms.CheckBox chkACTIVO;
        private System.Windows.Forms.CheckBox chkBLOQUEADO;
        private System.Windows.Forms.Label lblFECHA_ULTACCESO_TITULO;
        private System.Windows.Forms.Label lblFECHA_ULTACCESO;
    }
}