namespace SistemaContable.UI.Forms.Exportacion
{
    partial class frmFechaEmbarque : DevExpress.XtraEditors.XtraForm
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

        private void InitializeComponent()
        {
            this.grpDatos = new DevExpress.XtraEditors.GroupControl();
            this.panelDatos = new DevExpress.XtraEditors.PanelControl();
            this.lbIdContrato = new DevExpress.XtraEditors.LabelControl();
            this.txtTonelContrato = new DevExpress.XtraEditors.TextEdit();
            this.lblToneladas = new DevExpress.XtraEditors.LabelControl();
            this.txtContrato = new DevExpress.XtraEditors.TextEdit();
            this.lblContrato = new DevExpress.XtraEditors.LabelControl();
            this.txtCliente = new DevExpress.XtraEditors.TextEdit();
            this.lblCliente = new DevExpress.XtraEditors.LabelControl();
            this.panelGrid = new DevExpress.XtraEditors.PanelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIdContrato = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCodigo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFechaInicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colFechaFinal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colToneladas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colObservaciones = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colActivo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colEstado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelToolbar = new DevExpress.XtraEditors.PanelControl();
            this.btnFinalizar = new DevExpress.XtraEditors.SimpleButton();
            this.txtBuscar = new DevExpress.XtraEditors.TextEdit();
            this.btnExportarExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnEliminar = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnNuevo = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatos)).BeginInit();
            this.grpDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelDatos)).BeginInit();
            this.panelDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTonelContrato.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContrato.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCliente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelGrid)).BeginInit();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelToolbar)).BeginInit();
            this.panelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuscar.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grpDatos
            // 
            this.grpDatos.Controls.Add(this.panelDatos);
            this.grpDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpDatos.Location = new System.Drawing.Point(0, 0);
            this.grpDatos.Name = "grpDatos";
            this.grpDatos.Size = new System.Drawing.Size(944, 107);
            this.grpDatos.TabIndex = 0;
            this.grpDatos.Text = "Datos";
            // 
            // panelDatos
            // 
            this.panelDatos.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelDatos.Appearance.Options.UseBackColor = true;
            this.panelDatos.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelDatos.Controls.Add(this.lbIdContrato);
            this.panelDatos.Controls.Add(this.txtTonelContrato);
            this.panelDatos.Controls.Add(this.lblToneladas);
            this.panelDatos.Controls.Add(this.txtContrato);
            this.panelDatos.Controls.Add(this.lblContrato);
            this.panelDatos.Controls.Add(this.txtCliente);
            this.panelDatos.Controls.Add(this.lblCliente);
            this.panelDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDatos.Location = new System.Drawing.Point(2, 23);
            this.panelDatos.Name = "panelDatos";
            this.panelDatos.Size = new System.Drawing.Size(940, 82);
            this.panelDatos.TabIndex = 0;
            // 
            // lbIdContrato
            // 
            this.lbIdContrato.Location = new System.Drawing.Point(30, 6);
            this.lbIdContrato.Name = "lbIdContrato";
            this.lbIdContrato.Size = new System.Drawing.Size(0, 13);
            this.lbIdContrato.TabIndex = 6;
            this.lbIdContrato.Visible = false;
            // 
            // txtTonelContrato
            // 
            this.txtTonelContrato.Location = new System.Drawing.Point(680, 45);
            this.txtTonelContrato.Name = "txtTonelContrato";
            this.txtTonelContrato.Properties.Appearance.ForeColor = System.Drawing.Color.RoyalBlue;
            this.txtTonelContrato.Properties.Appearance.Options.UseForeColor = true;
            this.txtTonelContrato.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTonelContrato.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtTonelContrato.Properties.Mask.EditMask = "n2";
            this.txtTonelContrato.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtTonelContrato.Properties.ReadOnly = true;
            this.txtTonelContrato.Size = new System.Drawing.Size(140, 20);
            this.txtTonelContrato.TabIndex = 5;
            // 
            // lblToneladas
            // 
            this.lblToneladas.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblToneladas.Appearance.Options.UseFont = true;
            this.lblToneladas.Location = new System.Drawing.Point(680, 25);
            this.lblToneladas.Name = "lblToneladas";
            this.lblToneladas.Size = new System.Drawing.Size(77, 14);
            this.lblToneladas.TabIndex = 4;
            this.lblToneladas.Text = "TONELADAS:";
            // 
            // txtContrato
            // 
            this.txtContrato.Location = new System.Drawing.Point(410, 45);
            this.txtContrato.Name = "txtContrato";
            this.txtContrato.Properties.Appearance.ForeColor = System.Drawing.Color.RoyalBlue;
            this.txtContrato.Properties.Appearance.Options.UseForeColor = true;
            this.txtContrato.Properties.ReadOnly = true;
            this.txtContrato.Size = new System.Drawing.Size(220, 20);
            this.txtContrato.TabIndex = 3;
            // 
            // lblContrato
            // 
            this.lblContrato.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblContrato.Appearance.Options.UseFont = true;
            this.lblContrato.Location = new System.Drawing.Point(410, 25);
            this.lblContrato.Name = "lblContrato";
            this.lblContrato.Size = new System.Drawing.Size(88, 14);
            this.lblContrato.TabIndex = 2;
            this.lblContrato.Text = "N° CONTRATO:";
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(30, 45);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Properties.Appearance.ForeColor = System.Drawing.Color.RoyalBlue;
            this.txtCliente.Properties.Appearance.Options.UseForeColor = true;
            this.txtCliente.Properties.ReadOnly = true;
            this.txtCliente.Size = new System.Drawing.Size(340, 20);
            this.txtCliente.TabIndex = 1;
            // 
            // lblCliente
            // 
            this.lblCliente.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCliente.Appearance.Options.UseFont = true;
            this.lblCliente.Location = new System.Drawing.Point(30, 25);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(53, 14);
            this.lblCliente.TabIndex = 0;
            this.lblCliente.Text = "CLIENTE:";
            // 
            // panelGrid
            // 
            this.panelGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelGrid.Controls.Add(this.gridControl1);
            this.panelGrid.Controls.Add(this.panelToolbar);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 107);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(944, 383);
            this.panelGrid.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 92);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEdit1,
            this.repositoryItemSpinEdit1,
            this.repositoryItemCheckEdit1,
            this.repositoryItemMemoEdit1});
            this.gridControl1.Size = new System.Drawing.Size(940, 289);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIdContrato,
            this.colCodigo,
            this.colFechaInicio,
            this.colFechaFinal,
            this.colToneladas,
            this.colObservaciones,
            this.colActivo,
            this.colEstado});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridView1_InitNewRow);
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // colIdContrato
            // 
            this.colIdContrato.Caption = "IdContrato";
            this.colIdContrato.FieldName = "IDCONTEXP";
            this.colIdContrato.Name = "colIdContrato";
            this.colIdContrato.OptionsColumn.AllowEdit = false;
            // 
            // colCodigo
            // 
            this.colCodigo.Caption = "Código";
            this.colCodigo.FieldName = "IDCNTEXD";
            this.colCodigo.Name = "colCodigo";
            this.colCodigo.OptionsColumn.AllowEdit = false;
            this.colCodigo.OptionsColumn.ReadOnly = true;
            this.colCodigo.Visible = true;
            this.colCodigo.VisibleIndex = 0;
            this.colCodigo.Width = 70;
            // 
            // colFechaInicio
            // 
            this.colFechaInicio.Caption = "Fecha Inicio";
            this.colFechaInicio.ColumnEdit = this.repositoryItemDateEdit1;
            this.colFechaInicio.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colFechaInicio.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colFechaInicio.FieldName = "FECHA_INICIO";
            this.colFechaInicio.Name = "colFechaInicio";
            this.colFechaInicio.Visible = true;
            this.colFechaInicio.VisibleIndex = 1;
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // colFechaFinal
            // 
            this.colFechaFinal.Caption = "Fecha Final";
            this.colFechaFinal.ColumnEdit = this.repositoryItemDateEdit1;
            this.colFechaFinal.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colFechaFinal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colFechaFinal.FieldName = "FECHA_FINAL";
            this.colFechaFinal.Name = "colFechaFinal";
            this.colFechaFinal.Visible = true;
            this.colFechaFinal.VisibleIndex = 2;
            // 
            // colToneladas
            // 
            this.colToneladas.Caption = "Toneladas";
            this.colToneladas.ColumnEdit = this.repositoryItemSpinEdit1;
            this.colToneladas.DisplayFormat.FormatString = "n2";
            this.colToneladas.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colToneladas.FieldName = "TONELADAS";
            this.colToneladas.Name = "colToneladas";
            this.colToneladas.Visible = true;
            this.colToneladas.VisibleIndex = 3;
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.EditFormat.FormatString = "n2";
            this.repositoryItemSpinEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemSpinEdit1.Mask.EditMask = "n2";
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // colObservaciones
            // 
            this.colObservaciones.Caption = "Observacion";
            this.colObservaciones.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colObservaciones.FieldName = "OBSERVACIONES";
            this.colObservaciones.Name = "colObservaciones";
            this.colObservaciones.Visible = true;
            this.colObservaciones.VisibleIndex = 4;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colActivo
            // 
            this.colActivo.Caption = "Activo";
            this.colActivo.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colActivo.FieldName = "ACTIVO";
            this.colActivo.Name = "colActivo";
            this.colActivo.Visible = true;
            this.colActivo.VisibleIndex = 5;
            this.colActivo.Width = 60;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colEstado
            // 
            this.colEstado.Caption = "Estado";
            this.colEstado.FieldName = "ESTADO";
            this.colEstado.Name = "colEstado";
            // 
            // panelToolbar
            // 
            this.panelToolbar.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelToolbar.Appearance.Options.UseBackColor = true;
            this.panelToolbar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelToolbar.Controls.Add(this.btnFinalizar);
            this.panelToolbar.Controls.Add(this.txtBuscar);
            this.panelToolbar.Controls.Add(this.btnExportarExcel);
            this.panelToolbar.Controls.Add(this.btnEliminar);
            this.panelToolbar.Controls.Add(this.btnGuardar);
            this.panelToolbar.Controls.Add(this.btnNuevo);
            this.panelToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelToolbar.Location = new System.Drawing.Point(2, 2);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(940, 90);
            this.panelToolbar.TabIndex = 0;
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.Appearance.Options.UseFont = true;
            this.btnFinalizar.Appearance.Options.UseTextOptions = true;
            this.btnFinalizar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnFinalizar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnFinalizar.Location = new System.Drawing.Point(607, 12);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(90, 44);
            this.btnFinalizar.TabIndex = 160;
            this.btnFinalizar.TabStop = false;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(15, 62);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Properties.NullValuePrompt = "Introduzca el texto a buscar...";
            this.txtBuscar.Size = new System.Drawing.Size(910, 20);
            this.txtBuscar.TabIndex = 4;
            this.txtBuscar.EditValueChanged += new System.EventHandler(this.txtBuscar_EditValueChanged);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.excel_48x48;
            this.btnExportarExcel.Location = new System.Drawing.Point(471, 12);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(130, 44);
            this.btnExportarExcel.TabIndex = 3;
            this.btnExportarExcel.Text = "Exportar Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.eliminar32x32;
            this.btnEliminar.Location = new System.Drawing.Point(375, 12);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(90, 44);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.guardar2_32x32;
            this.btnGuardar.Location = new System.Drawing.Point(279, 12);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(90, 44);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevo.Location = new System.Drawing.Point(183, 12);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(90, 44);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // frmFechaEmbarque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 490);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.grpDatos);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmFechaEmbarque";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FECHAS DE EMBARQUE";
            this.Load += new System.EventHandler(this.frmFechaEmbarque_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpDatos)).EndInit();
            this.grpDatos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelDatos)).EndInit();
            this.panelDatos.ResumeLayout(false);
            this.panelDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTonelContrato.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContrato.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCliente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelGrid)).EndInit();
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelToolbar)).EndInit();
            this.panelToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtBuscar.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpDatos;
        private DevExpress.XtraEditors.PanelControl panelDatos;
        private DevExpress.XtraEditors.LabelControl lblCliente;
        private DevExpress.XtraEditors.TextEdit txtCliente;
        private DevExpress.XtraEditors.LabelControl lblContrato;
        private DevExpress.XtraEditors.TextEdit txtContrato;
        private DevExpress.XtraEditors.LabelControl lblToneladas;
        private DevExpress.XtraEditors.TextEdit txtTonelContrato;
        private DevExpress.XtraEditors.LabelControl lbIdContrato;

        private DevExpress.XtraEditors.PanelControl panelGrid;
        private DevExpress.XtraEditors.PanelControl panelToolbar;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraEditors.SimpleButton btnEliminar;
        private DevExpress.XtraEditors.SimpleButton btnExportarExcel;
        private DevExpress.XtraEditors.TextEdit txtBuscar;

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colIdContrato;
        private DevExpress.XtraGrid.Columns.GridColumn colCodigo;
        private DevExpress.XtraGrid.Columns.GridColumn colFechaInicio;
        private DevExpress.XtraGrid.Columns.GridColumn colFechaFinal;
        private DevExpress.XtraGrid.Columns.GridColumn colToneladas;
        private DevExpress.XtraGrid.Columns.GridColumn colObservaciones;
        private DevExpress.XtraGrid.Columns.GridColumn colActivo;
        private DevExpress.XtraGrid.Columns.GridColumn colEstado;

        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.SimpleButton btnFinalizar;
    }
}
