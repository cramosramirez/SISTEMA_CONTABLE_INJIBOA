
namespace SistemaContable.UI.Forms
{
    partial class frmLogin
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.txtUSUARIO = new System.Windows.Forms.TextBox();
            this.txtCLAVE = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SistemaContable.UI.Properties.Resources.login;
            this.pictureBox1.Location = new System.Drawing.Point(24, 23);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(73, 87);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(109, 28);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(58, 17);
            this.Label1.TabIndex = 14;
            this.Label1.Text = "Usuario:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(200, 129);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(99, 34);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(122, 74);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(46, 17);
            this.Label2.TabIndex = 15;
            this.Label2.Text = "Clave:";
            // 
            // btnIngresar
            // 
            this.btnIngresar.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIngresar.Location = new System.Drawing.Point(75, 129);
            this.btnIngresar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(99, 34);
            this.btnIngresar.TabIndex = 2;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.UseVisualStyleBackColor = true;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // txtUSUARIO
            // 
            this.txtUSUARIO.Location = new System.Drawing.Point(178, 26);
            this.txtUSUARIO.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUSUARIO.Name = "txtUSUARIO";
            this.txtUSUARIO.Size = new System.Drawing.Size(130, 23);
            this.txtUSUARIO.TabIndex = 0;
            this.txtUSUARIO.Tag = "Usuario";
            this.txtUSUARIO.Text = "admin";
            // 
            // txtCLAVE
            // 
            this.txtCLAVE.Location = new System.Drawing.Point(178, 72);
            this.txtCLAVE.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCLAVE.Name = "txtCLAVE";
            this.txtCLAVE.PasswordChar = '*';
            this.txtCLAVE.Size = new System.Drawing.Size(130, 23);
            this.txtCLAVE.TabIndex = 1;
            this.txtCLAVE.Tag = "Clave";
            this.txtCLAVE.Text = "Admin123";
            this.txtCLAVE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCLAVE_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.txtCLAVE);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Controls.Add(this.txtUSUARIO);
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Controls.Add(this.btnIngresar);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(374, 211);
            this.panel1.TabIndex = 1;
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 211);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio de Sesión";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnCancelar;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button btnIngresar;
        internal System.Windows.Forms.TextBox txtUSUARIO;
        internal System.Windows.Forms.TextBox txtCLAVE;
        private System.Windows.Forms.Panel panel1;
    }
}