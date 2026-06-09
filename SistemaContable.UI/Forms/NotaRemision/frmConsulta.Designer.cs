
namespace SistemaContable.UI.Forms.NotaRemision
{
    partial class frmConsulta
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gvDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID_NTREMISIONENC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FECHA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NUMDOC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NOMCLIENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CODGENERACION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NUMCONTROL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SELLORECEPCION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ESTADO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnNuevoNR = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalle)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gridControl1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1387, 450);
            this.panel1.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridControl1.Location = new System.Drawing.Point(0, 80);
            this.gridControl1.MainView = this.gvDetalle;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1387, 200);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetalle});
            // 
            // gvDetalle
            // 
            this.gvDetalle.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID_NTREMISIONENC,
            this.FECHA,
            this.NUMDOC,
            this.NOMCLIENTE,
            this.CODGENERACION,
            this.NUMCONTROL,
            this.SELLORECEPCION,
            this.ESTADO});
            this.gvDetalle.GridControl = this.gridControl1;
            this.gvDetalle.Name = "gvDetalle";
            // 
            // ID_NTREMISIONENC
            // 
            this.ID_NTREMISIONENC.Caption = "Sistema(Id)";
            this.ID_NTREMISIONENC.Name = "ID_NTREMISIONENC";
            this.ID_NTREMISIONENC.Visible = true;
            this.ID_NTREMISIONENC.VisibleIndex = 0;
            this.ID_NTREMISIONENC.Width = 100;
            // 
            // FECHA
            // 
            this.FECHA.Caption = "Fecha";
            this.FECHA.Name = "FECHA";
            this.FECHA.Visible = true;
            this.FECHA.VisibleIndex = 1;
            this.FECHA.Width = 102;
            // 
            // NUMDOC
            // 
            this.NUMDOC.Caption = "N° Documento";
            this.NUMDOC.Name = "NUMDOC";
            this.NUMDOC.Visible = true;
            this.NUMDOC.VisibleIndex = 2;
            this.NUMDOC.Width = 159;
            // 
            // NOMCLIENTE
            // 
            this.NOMCLIENTE.Caption = "Cliente";
            this.NOMCLIENTE.Name = "NOMCLIENTE";
            this.NOMCLIENTE.Visible = true;
            this.NOMCLIENTE.VisibleIndex = 3;
            this.NOMCLIENTE.Width = 357;
            // 
            // CODGENERACION
            // 
            this.CODGENERACION.Caption = "N° Generación";
            this.CODGENERACION.Name = "CODGENERACION";
            this.CODGENERACION.Visible = true;
            this.CODGENERACION.VisibleIndex = 4;
            this.CODGENERACION.Width = 143;
            // 
            // NUMCONTROL
            // 
            this.NUMCONTROL.Caption = "N° Control";
            this.NUMCONTROL.Name = "NUMCONTROL";
            this.NUMCONTROL.Visible = true;
            this.NUMCONTROL.VisibleIndex = 5;
            this.NUMCONTROL.Width = 97;
            // 
            // SELLORECEPCION
            // 
            this.SELLORECEPCION.Caption = "Sello Recibido";
            this.SELLORECEPCION.Name = "SELLORECEPCION";
            this.SELLORECEPCION.Visible = true;
            this.SELLORECEPCION.VisibleIndex = 6;
            this.SELLORECEPCION.Width = 36;
            // 
            // ESTADO
            // 
            this.ESTADO.Caption = "Estado";
            this.ESTADO.Name = "ESTADO";
            this.ESTADO.Visible = true;
            this.ESTADO.VisibleIndex = 7;
            this.ESTADO.Width = 27;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnNuevoNR);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1387, 80);
            this.panel2.TabIndex = 4;
            // 
            // btnNuevoNR
            // 
            this.btnNuevoNR.AllowFocus = false;
            this.btnNuevoNR.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevoNR.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnNuevoNR.Location = new System.Drawing.Point(23, 6);
            this.btnNuevoNR.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnNuevoNR.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnNuevoNR.Margin = new System.Windows.Forms.Padding(2);
            this.btnNuevoNR.Name = "btnNuevoNR";
            this.btnNuevoNR.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnNuevoNR.Size = new System.Drawing.Size(74, 64);
            this.btnNuevoNR.TabIndex = 2;
            this.btnNuevoNR.TabStop = false;
            this.btnNuevoNR.Text = "Nuevo";
            this.btnNuevoNR.ToolTip = "Nueva Nota Remision";
            this.btnNuevoNR.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnNuevoNR.ToolTipTitle = "Operación";
            this.btnNuevoNR.Click += new System.EventHandler(this.btnNuevoNR_Click);
            // 
            // frmConsulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1387, 450);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConsulta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Nota Reminision";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalle)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnNuevoNR;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetalle;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraGrid.Columns.GridColumn ID_NTREMISIONENC;
        private DevExpress.XtraGrid.Columns.GridColumn FECHA;
        private DevExpress.XtraGrid.Columns.GridColumn NUMDOC;
        private DevExpress.XtraGrid.Columns.GridColumn NOMCLIENTE;
        private DevExpress.XtraGrid.Columns.GridColumn CODGENERACION;
        private DevExpress.XtraGrid.Columns.GridColumn NUMCONTROL;
        private DevExpress.XtraGrid.Columns.GridColumn SELLORECEPCION;
        private DevExpress.XtraGrid.Columns.GridColumn ESTADO;
    }
}