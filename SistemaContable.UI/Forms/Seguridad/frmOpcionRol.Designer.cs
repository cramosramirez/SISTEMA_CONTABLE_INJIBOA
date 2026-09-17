namespace SistemaContable.UI.Forms.Seguridad
{
    partial class frmOpcionRol
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
            this.groupRol = new DevExpress.XtraEditors.GroupControl();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.cbxROL = new System.Windows.Forms.ComboBox();
            this.lblRol = new System.Windows.Forms.Label();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
            this.panelAcciones = new System.Windows.Forms.Panel();
            this.lblResumenOpciones = new System.Windows.Forms.Label();
            this.btnContraer = new DevExpress.XtraEditors.SimpleButton();
            this.btnExpandir = new DevExpress.XtraEditors.SimpleButton();
            this.btnDesmarcarTodas = new DevExpress.XtraEditors.SimpleButton();
            this.btnMarcarTodas = new DevExpress.XtraEditors.SimpleButton();
            this.treeListOpciones = new DevExpress.XtraTreeList.TreeList();
            ((System.ComponentModel.ISupportInitialize)(this.groupRol)).BeginInit();
            this.groupRol.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListOpciones)).BeginInit();
            this.SuspendLayout();
            // 
            // groupRol
            // 
            this.groupRol.AppearanceCaption.BackColor = System.Drawing.Color.Blue;
            this.groupRol.AppearanceCaption.BackColor2 = System.Drawing.Color.Blue;
            this.groupRol.AppearanceCaption.ForeColor = System.Drawing.Color.White;
            this.groupRol.AppearanceCaption.Options.UseBackColor = true;
            this.groupRol.AppearanceCaption.Options.UseForeColor = true;
            this.groupRol.AppearanceCaption.Options.UseTextOptions = true;
            this.groupRol.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupRol.Controls.Add(this.lblAyuda);
            this.groupRol.Controls.Add(this.cbxROL);
            this.groupRol.Controls.Add(this.lblRol);
            this.groupRol.Location = new System.Drawing.Point(8, 6);
            this.groupRol.Name = "groupRol";
            this.groupRol.Size = new System.Drawing.Size(970, 96);
            this.groupRol.TabIndex = 0;
            this.groupRol.Text = "Asignación de opciones por rol";
            // 
            // lblAyuda
            // 
            this.lblAyuda.AutoSize = true;
            this.lblAyuda.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblAyuda.ForeColor = System.Drawing.Color.DimGray;
            this.lblAyuda.Location = new System.Drawing.Point(78, 68);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(588, 13);
            this.lblAyuda.TabIndex = 2;
            this.lblAyuda.Text = "Marque los módulos y opciones que verá el rol. Los niveles padres necesarios se guardan automáticamente.";
            // 
            // cbxROL
            // 
            this.cbxROL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxROL.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbxROL.FormattingEnabled = true;
            this.cbxROL.Location = new System.Drawing.Point(78, 38);
            this.cbxROL.Name = "cbxROL";
            this.cbxROL.Size = new System.Drawing.Size(870, 23);
            this.cbxROL.TabIndex = 1;
            this.cbxROL.SelectedIndexChanged += new System.EventHandler(this.cbxROL_SelectedIndexChanged);
            // 
            // lblRol
            // 
            this.lblRol.AutoSize = true;
            this.lblRol.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRol.Location = new System.Drawing.Point(17, 42);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(28, 15);
            this.lblRol.TabIndex = 0;
            this.lblRol.Text = "Rol:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.guardar2_32x32;
            this.btnGuardar.Location = new System.Drawing.Point(990, 12);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 40);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.btnSalir.Appearance.Options.UseFont = true;
            this.btnSalir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnSalir.Location = new System.Drawing.Point(990, 58);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(120, 40);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // panelAcciones
            // 
            this.panelAcciones.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelAcciones.Controls.Add(this.lblResumenOpciones);
            this.panelAcciones.Controls.Add(this.btnContraer);
            this.panelAcciones.Controls.Add(this.btnExpandir);
            this.panelAcciones.Controls.Add(this.btnDesmarcarTodas);
            this.panelAcciones.Controls.Add(this.btnMarcarTodas);
            this.panelAcciones.Location = new System.Drawing.Point(8, 110);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new System.Drawing.Size(1102, 48);
            this.panelAcciones.TabIndex = 3;
            // 
            // lblResumenOpciones
            // 
            this.lblResumenOpciones.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblResumenOpciones.Location = new System.Drawing.Point(817, 8);
            this.lblResumenOpciones.Name = "lblResumenOpciones";
            this.lblResumenOpciones.Size = new System.Drawing.Size(274, 32);
            this.lblResumenOpciones.TabIndex = 4;
            this.lblResumenOpciones.Text = "0 opciones seleccionadas";
            this.lblResumenOpciones.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnContraer
            // 
            this.btnContraer.Location = new System.Drawing.Point(438, 8);
            this.btnContraer.Name = "btnContraer";
            this.btnContraer.Size = new System.Drawing.Size(100, 32);
            this.btnContraer.TabIndex = 3;
            this.btnContraer.Text = "Contraer";
            this.btnContraer.Click += new System.EventHandler(this.btnContraer_Click);
            // 
            // btnExpandir
            // 
            this.btnExpandir.Location = new System.Drawing.Point(332, 8);
            this.btnExpandir.Name = "btnExpandir";
            this.btnExpandir.Size = new System.Drawing.Size(100, 32);
            this.btnExpandir.TabIndex = 2;
            this.btnExpandir.Text = "Expandir";
            this.btnExpandir.Click += new System.EventHandler(this.btnExpandir_Click);
            // 
            // btnDesmarcarTodas
            // 
            this.btnDesmarcarTodas.Location = new System.Drawing.Point(166, 8);
            this.btnDesmarcarTodas.Name = "btnDesmarcarTodas";
            this.btnDesmarcarTodas.Size = new System.Drawing.Size(160, 32);
            this.btnDesmarcarTodas.TabIndex = 1;
            this.btnDesmarcarTodas.Text = "Desmarcar todas";
            this.btnDesmarcarTodas.Click += new System.EventHandler(this.btnDesmarcarTodas_Click);
            // 
            // btnMarcarTodas
            // 
            this.btnMarcarTodas.Location = new System.Drawing.Point(8, 8);
            this.btnMarcarTodas.Name = "btnMarcarTodas";
            this.btnMarcarTodas.Size = new System.Drawing.Size(152, 32);
            this.btnMarcarTodas.TabIndex = 0;
            this.btnMarcarTodas.Text = "Marcar todas";
            this.btnMarcarTodas.Click += new System.EventHandler(this.btnMarcarTodas_Click);
            // 
            // treeListOpciones
            // 
            this.treeListOpciones.Location = new System.Drawing.Point(8, 166);
            this.treeListOpciones.Name = "treeListOpciones";
            this.treeListOpciones.OptionsBehavior.Editable = false;
            this.treeListOpciones.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeListOpciones.Size = new System.Drawing.Size(1102, 525);
            this.treeListOpciones.TabIndex = 4;
            // 
            // frmOpcionRol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 700);
            this.Controls.Add(this.treeListOpciones);
            this.Controls.Add(this.panelAcciones);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupRol);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOpcionRol";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opciones por Rol";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOpcionRol_FormClosing);
            this.Load += new System.EventHandler(this.frmOpcionRol_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupRol)).EndInit();
            this.groupRol.ResumeLayout(false);
            this.groupRol.PerformLayout();
            this.panelAcciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListOpciones)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupRol;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.ComboBox cbxROL;
        private System.Windows.Forms.Label lblRol;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private System.Windows.Forms.Panel panelAcciones;
        private System.Windows.Forms.Label lblResumenOpciones;
        private DevExpress.XtraEditors.SimpleButton btnMarcarTodas;
        private DevExpress.XtraEditors.SimpleButton btnDesmarcarTodas;
        private DevExpress.XtraEditors.SimpleButton btnExpandir;
        private DevExpress.XtraEditors.SimpleButton btnContraer;
        private DevExpress.XtraTreeList.TreeList treeListOpciones;
    }
}
