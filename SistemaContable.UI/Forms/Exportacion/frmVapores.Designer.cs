
using System;
using System.Drawing;
using System.Windows.Forms;


namespace SistemaContable.UI.Forms.Exportacion
{
    partial class frmVapores : DevExpress.XtraEditors.XtraForm
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

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        private DevExpress.XtraEditors.TextEdit txtNumVap;
        private DevExpress.XtraEditors.TextEdit txtDescripcion;
        private DevExpress.XtraEditors.ComboBoxEdit cboEstado;

        private DevExpress.XtraLayout.LayoutControlItem liNumVap;
        private DevExpress.XtraLayout.LayoutControlItem liDescripcion;
        private DevExpress.XtraLayout.LayoutControlItem liEstado;

        private DevExpress.XtraEditors.PanelControl pnlBotones;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraEditors.SimpleButton btnEditar;
        private DevExpress.XtraEditors.SimpleButton btnEliminar;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;

        private DevExpress.XtraEditors.GroupControl grpLista;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colNumVap;
        private DevExpress.XtraGrid.Columns.GridColumn colDescripcion;
        private DevExpress.XtraGrid.Columns.GridColumn colEstado;
        private DevExpress.XtraGrid.Columns.GridColumn colIngresadoPor;
        private DevExpress.XtraGrid.Columns.GridColumn colModificadoPor;
        private DevExpress.XtraGrid.Columns.GridColumn colFechaIngreso;
        private DevExpress.XtraGrid.Columns.GridColumn colFechaModificacion;

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtNumVap = new DevExpress.XtraEditors.TextEdit();
            this.txtDescripcion = new DevExpress.XtraEditors.TextEdit();
            this.cboEstado = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.liNumVap = new DevExpress.XtraLayout.LayoutControlItem();
            this.liDescripcion = new DevExpress.XtraLayout.LayoutControlItem();
            this.liEstado = new DevExpress.XtraLayout.LayoutControlItem();
            this.pnlBotones = new DevExpress.XtraEditors.PanelControl();
            this.btnFinalizar = new DevExpress.XtraEditors.SimpleButton();
            this.btnNuevo = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditar = new DevExpress.XtraEditors.SimpleButton();
            this.btnEliminar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.grpLista = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNumVap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescripcion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEstado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIngresadoPor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModificadoPor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFechaIngreso = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFechaModificacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumVap.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEstado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liNumVap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liDescripcion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liEstado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBotones)).BeginInit();
            this.pnlBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpLista)).BeginInit();
            this.grpLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtNumVap);
            this.layoutControl1.Controls.Add(this.txtDescripcion);
            this.layoutControl1.Controls.Add(this.cboEstado);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(760, 130);
            this.layoutControl1.TabIndex = 2;
            // 
            // txtNumVap
            // 
            this.txtNumVap.Location = new System.Drawing.Point(76, 33);
            this.txtNumVap.Name = "txtNumVap";
            this.txtNumVap.Properties.Mask.EditMask = "\\d*";
            this.txtNumVap.Properties.MaxLength = 10;
            this.txtNumVap.Size = new System.Drawing.Size(672, 20);
            this.txtNumVap.StyleController = this.layoutControl1;
            this.txtNumVap.TabIndex = 4;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(76, 81);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Properties.MaxLength = 100;
            this.txtDescripcion.Size = new System.Drawing.Size(672, 20);
            this.txtDescripcion.StyleController = this.layoutControl1;
            this.txtDescripcion.TabIndex = 5;
            // 
            // cboEstado
            // 
            this.cboEstado.Location = new System.Drawing.Point(76, 57);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Properties.Items.AddRange(new object[] {
            "ACTIVO",
            "INACTIVO"});
            this.cboEstado.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboEstado.Size = new System.Drawing.Size(672, 20);
            this.cboEstado.StyleController = this.layoutControl1;
            this.cboEstado.TabIndex = 6;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.liNumVap,
            this.liDescripcion,
            this.liEstado});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(760, 130);
            this.layoutControlGroup1.Text = "Datos del Vapor";
            // 
            // liNumVap
            // 
            this.liNumVap.Control = this.txtNumVap;
            this.liNumVap.Location = new System.Drawing.Point(0, 0);
            this.liNumVap.Name = "liNumVap";
            this.liNumVap.Size = new System.Drawing.Size(740, 24);
            this.liNumVap.Text = "No. Vapor:";
            this.liNumVap.TextSize = new System.Drawing.Size(52, 13);
            this.liNumVap.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // liDescripcion
            // 
            this.liDescripcion.Control = this.txtDescripcion;
            this.liDescripcion.Location = new System.Drawing.Point(0, 48);
            this.liDescripcion.Name = "liDescripcion";
            this.liDescripcion.Size = new System.Drawing.Size(740, 41);
            this.liDescripcion.Text = "Vapor:";
            this.liDescripcion.TextSize = new System.Drawing.Size(52, 13);
            // 
            // liEstado
            // 
            this.liEstado.Control = this.cboEstado;
            this.liEstado.Location = new System.Drawing.Point(0, 24);
            this.liEstado.Name = "liEstado";
            this.liEstado.Size = new System.Drawing.Size(740, 24);
            this.liEstado.Text = "Estado:";
            this.liEstado.TextSize = new System.Drawing.Size(52, 13);
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
            this.pnlBotones.Location = new System.Drawing.Point(650, 130);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Size = new System.Drawing.Size(110, 320);
            this.pnlBotones.TabIndex = 1;
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.Appearance.Options.UseFont = true;
            this.btnFinalizar.Appearance.Options.UseTextOptions = true;
            this.btnFinalizar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnFinalizar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnFinalizar.Location = new System.Drawing.Point(10, 265);
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
            this.btnNuevo.Location = new System.Drawing.Point(10, 15);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(90, 44);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.guardar2_32x32;
            this.btnGuardar.Location = new System.Drawing.Point(10, 65);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(90, 44);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.editar3_32x32;
            this.btnEditar.Location = new System.Drawing.Point(10, 115);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(90, 44);
            this.btnEditar.TabIndex = 2;
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.eliminar32x32;
            this.btnEliminar.Location = new System.Drawing.Point(10, 165);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(90, 44);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.cancelar32x32;
            this.btnCancelar.Location = new System.Drawing.Point(10, 215);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(90, 44);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // grpLista
            // 
            this.grpLista.Controls.Add(this.gridControl1);
            this.grpLista.Controls.Add(this.searchControl1);
            this.grpLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLista.Location = new System.Drawing.Point(0, 130);
            this.grpLista.Name = "grpLista";
            this.grpLista.Size = new System.Drawing.Size(650, 320);
            this.grpLista.TabIndex = 0;
            this.grpLista.Text = "Vapores registrados";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 43);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(646, 275);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNumVap,
            this.colDescripcion,
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
            // colNumVap
            // 
            this.colNumVap.Caption = "No. Vapor";
            this.colNumVap.FieldName = "NUMVAP";
            this.colNumVap.Name = "colNumVap";
            this.colNumVap.OptionsColumn.AllowSize = false;
            this.colNumVap.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colNumVap.Width = 87;
            // 
            // colDescripcion
            // 
            this.colDescripcion.Caption = "Descripcion";
            this.colDescripcion.FieldName = "DESCRIPCION";
            this.colDescripcion.Name = "colDescripcion";
            this.colDescripcion.Visible = true;
            this.colDescripcion.VisibleIndex = 0;
            this.colDescripcion.Width = 478;
            // 
            // colEstado
            // 
            this.colEstado.Caption = "Estado";
            this.colEstado.FieldName = "ESTADO";
            this.colEstado.Name = "colEstado";
            this.colEstado.OptionsColumn.AllowSize = false;
            this.colEstado.Visible = true;
            this.colEstado.VisibleIndex = 1;
            this.colEstado.Width = 58;
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
            this.searchControl1.Size = new System.Drawing.Size(646, 20);
            this.searchControl1.TabIndex = 1;
            // 
            // frmVapores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 450);
            this.Controls.Add(this.grpLista);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmVapores";
            this.Text = "Mantenimiento de Vapores";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNumVap.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEstado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liNumVap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liDescripcion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liEstado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBotones)).EndInit();
            this.pnlBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpLista)).EndInit();
            this.grpLista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnFinalizar;
    }
}
