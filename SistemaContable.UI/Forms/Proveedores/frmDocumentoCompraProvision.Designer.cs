
namespace SistemaContable.UI.Forms.Proveedores
{
    partial class frmDocumentoCompraProvision
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocumentoCompraProvision));
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnBorrarFila = new DevExpress.XtraEditors.SimpleButton();
            this.btnFinalizar = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.lblDIFERENCIA = new System.Windows.Forms.Label();
            this.lblTOTAL_ABONO = new System.Windows.Forms.Label();
            this.lblTOTAL_CARGO = new System.Windows.Forms.Label();
            this.lblCUADRE = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gridControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1082, 446);
            this.panel1.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1082, 446);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnBorrarFila);
            this.panel2.Controls.Add(this.btnFinalizar);
            this.panel2.Controls.Add(this.btnGuardar);
            this.panel2.Controls.Add(this.lblDIFERENCIA);
            this.panel2.Controls.Add(this.lblTOTAL_ABONO);
            this.panel2.Controls.Add(this.lblTOTAL_CARGO);
            this.panel2.Controls.Add(this.lblCUADRE);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 444);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1082, 86);
            this.panel2.TabIndex = 0;
            // 
            // btnBorrarFila
            // 
            this.btnBorrarFila.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBorrarFila.Appearance.Options.UseFont = true;
            this.btnBorrarFila.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.eliminarFila32x32;
            this.btnBorrarFila.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnBorrarFila.ImageOptions.ImageToTextIndent = 10;
            this.btnBorrarFila.Location = new System.Drawing.Point(148, 20);
            this.btnBorrarFila.Name = "btnBorrarFila";
            this.btnBorrarFila.Size = new System.Drawing.Size(119, 47);
            this.btnBorrarFila.TabIndex = 2;
            this.btnBorrarFila.Text = "Borrar fila";
            this.btnBorrarFila.Click += new System.EventHandler(this.btnBorrarFila_Click);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.Appearance.Options.UseFont = true;
            this.btnFinalizar.Appearance.Options.UseTextOptions = true;
            this.btnFinalizar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnFinalizar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnFinalizar.Location = new System.Drawing.Point(336, 22);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(119, 47);
            this.btnFinalizar.TabIndex = 3;
            this.btnFinalizar.TabStop = false;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.ImageOptions.Image")));
            this.btnGuardar.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnGuardar.ImageOptions.ImageToTextIndent = 10;
            this.btnGuardar.Location = new System.Drawing.Point(12, 20);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(119, 47);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblDIFERENCIA
            // 
            this.lblDIFERENCIA.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDIFERENCIA.Location = new System.Drawing.Point(824, 32);
            this.lblDIFERENCIA.Name = "lblDIFERENCIA";
            this.lblDIFERENCIA.Size = new System.Drawing.Size(258, 23);
            this.lblDIFERENCIA.TabIndex = 23;
            this.lblDIFERENCIA.Text = "0.00";
            this.lblDIFERENCIA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTOTAL_ABONO
            // 
            this.lblTOTAL_ABONO.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTOTAL_ABONO.Location = new System.Drawing.Point(965, 5);
            this.lblTOTAL_ABONO.Name = "lblTOTAL_ABONO";
            this.lblTOTAL_ABONO.Size = new System.Drawing.Size(117, 23);
            this.lblTOTAL_ABONO.TabIndex = 22;
            this.lblTOTAL_ABONO.Text = "0.00";
            this.lblTOTAL_ABONO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTOTAL_CARGO
            // 
            this.lblTOTAL_CARGO.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTOTAL_CARGO.Location = new System.Drawing.Point(821, 5);
            this.lblTOTAL_CARGO.Name = "lblTOTAL_CARGO";
            this.lblTOTAL_CARGO.Size = new System.Drawing.Size(143, 23);
            this.lblTOTAL_CARGO.TabIndex = 21;
            this.lblTOTAL_CARGO.Text = "0.00";
            this.lblTOTAL_CARGO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCUADRE
            // 
            this.lblCUADRE.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCUADRE.Location = new System.Drawing.Point(431, 31);
            this.lblCUADRE.Name = "lblCUADRE";
            this.lblCUADRE.Size = new System.Drawing.Size(377, 21);
            this.lblCUADRE.TabIndex = 20;
            this.lblCUADRE.Text = "-";
            this.lblCUADRE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmDocumentoCompraProvision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 530);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDocumentoCompraProvision";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Provisión de documento";
            this.Load += new System.EventHandler(this.frmProvisionQuedan_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblDIFERENCIA;
        private System.Windows.Forms.Label lblTOTAL_ABONO;
        private System.Windows.Forms.Label lblTOTAL_CARGO;
        private System.Windows.Forms.Label lblCUADRE;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraEditors.SimpleButton btnFinalizar;
        private DevExpress.XtraEditors.SimpleButton btnBorrarFila;
    }
}