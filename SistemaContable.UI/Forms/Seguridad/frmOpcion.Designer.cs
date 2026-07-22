namespace SistemaContable.UI.Forms.Seguridad
{
    partial class frmOpcion
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
            this.colNOMBRE_OPCION = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNIVEL = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colORDEN = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colACTIVO = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colFORMULARIO_WIN = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colIMAGEN_SVG = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListOpciones = new DevExpress.XtraTreeList.TreeList();
            this.groupDetalle = new DevExpress.XtraEditors.GroupControl();
            this.txtIMAGEN_SVG = new DevExpress.XtraEditors.TextEdit();
            this.lblImagenSvg = new DevExpress.XtraEditors.LabelControl();
            this.txtFORMULARIO_WIN = new DevExpress.XtraEditors.TextEdit();
            this.lblFormularioWin = new DevExpress.XtraEditors.LabelControl();
            this.chkACTIVO = new DevExpress.XtraEditors.CheckEdit();
            this.spinORDEN = new DevExpress.XtraEditors.SpinEdit();
            this.lblOrden = new DevExpress.XtraEditors.LabelControl();
            this.txtNOMBRE_OPCION = new DevExpress.XtraEditors.TextEdit();
            this.lblNombreOpcion = new DevExpress.XtraEditors.LabelControl();
            this.lblNIVEL = new DevExpress.XtraEditors.LabelControl();
            this.lblOpcionPadre = new DevExpress.XtraEditors.LabelControl();
            this.btnNuevo = new DevExpress.XtraEditors.SimpleButton();
            this.btnAgregarHijo = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnEliminar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalir = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListOpciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupDetalle)).BeginInit();
            this.groupDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIMAGEN_SVG.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFORMULARIO_WIN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkACTIVO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinORDEN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNOMBRE_OPCION.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // colNOMBRE_OPCION
            // 
            this.colNOMBRE_OPCION.Caption = "Opción";
            this.colNOMBRE_OPCION.FieldName = "NOMBRE_OPCION";
            this.colNOMBRE_OPCION.Name = "colNOMBRE_OPCION";
            this.colNOMBRE_OPCION.Visible = true;
            this.colNOMBRE_OPCION.VisibleIndex = 0;
            this.colNOMBRE_OPCION.Width = 200;
            // 
            // colNIVEL
            // 
            this.colNIVEL.Caption = "Nivel";
            this.colNIVEL.FieldName = "NIVEL";
            this.colNIVEL.Name = "colNIVEL";
            this.colNIVEL.Visible = true;
            this.colNIVEL.VisibleIndex = 1;
            this.colNIVEL.Width = 45;
            // 
            // colORDEN
            // 
            this.colORDEN.Caption = "Orden";
            this.colORDEN.FieldName = "ORDEN";
            this.colORDEN.Name = "colORDEN";
            this.colORDEN.Visible = true;
            this.colORDEN.VisibleIndex = 2;
            this.colORDEN.Width = 45;
            // 
            // colACTIVO
            // 
            this.colACTIVO.Caption = "Activo";
            this.colACTIVO.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colACTIVO.FieldName = "ACTIVO";
            this.colACTIVO.Name = "colACTIVO";
            this.colACTIVO.Visible = true;
            this.colACTIVO.VisibleIndex = 3;
            this.colACTIVO.Width = 45;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colFORMULARIO_WIN
            // 
            this.colFORMULARIO_WIN.Caption = "Formulario";
            this.colFORMULARIO_WIN.FieldName = "FORMULARIO_WIN";
            this.colFORMULARIO_WIN.Name = "colFORMULARIO_WIN";
            this.colFORMULARIO_WIN.Visible = true;
            this.colFORMULARIO_WIN.VisibleIndex = 4;
            this.colFORMULARIO_WIN.Width = 160;
            // 
            // colIMAGEN_SVG
            // 
            this.colIMAGEN_SVG.Caption = "Ícono";
            this.colIMAGEN_SVG.FieldName = "IMAGEN_SVG";
            this.colIMAGEN_SVG.Name = "colIMAGEN_SVG";
            this.colIMAGEN_SVG.Visible = true;
            this.colIMAGEN_SVG.VisibleIndex = 5;
            this.colIMAGEN_SVG.Width = 120;
            // 
            // treeListOpciones
            // 
            this.treeListOpciones.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colNOMBRE_OPCION,
            this.colNIVEL,
            this.colORDEN,
            this.colACTIVO,
            this.colFORMULARIO_WIN,
            this.colIMAGEN_SVG});
            this.treeListOpciones.Location = new System.Drawing.Point(2, 2);
            this.treeListOpciones.Name = "treeListOpciones";
            this.treeListOpciones.OptionsBehavior.Editable = false;
            this.treeListOpciones.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeListOpciones.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.treeListOpciones.Size = new System.Drawing.Size(1084, 795);
            this.treeListOpciones.TabIndex = 0;
            this.treeListOpciones.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListOpciones_FocusedNodeChanged);
            // 
            // groupDetalle
            // 
            this.groupDetalle.AppearanceCaption.BackColor = System.Drawing.Color.Blue;
            this.groupDetalle.AppearanceCaption.ForeColor = System.Drawing.Color.White;
            this.groupDetalle.AppearanceCaption.Options.UseBackColor = true;
            this.groupDetalle.AppearanceCaption.Options.UseForeColor = true;
            this.groupDetalle.Controls.Add(this.txtIMAGEN_SVG);
            this.groupDetalle.Controls.Add(this.lblImagenSvg);
            this.groupDetalle.Controls.Add(this.txtFORMULARIO_WIN);
            this.groupDetalle.Controls.Add(this.lblFormularioWin);
            this.groupDetalle.Controls.Add(this.chkACTIVO);
            this.groupDetalle.Controls.Add(this.spinORDEN);
            this.groupDetalle.Controls.Add(this.lblOrden);
            this.groupDetalle.Controls.Add(this.txtNOMBRE_OPCION);
            this.groupDetalle.Controls.Add(this.lblNombreOpcion);
            this.groupDetalle.Controls.Add(this.lblNIVEL);
            this.groupDetalle.Controls.Add(this.lblOpcionPadre);
            this.groupDetalle.Location = new System.Drawing.Point(1092, 8);
            this.groupDetalle.Name = "groupDetalle";
            this.groupDetalle.Size = new System.Drawing.Size(230, 300);
            this.groupDetalle.TabIndex = 1;
            this.groupDetalle.Text = "Detalle de la Opción";
            // 
            // txtIMAGEN_SVG
            // 
            this.txtIMAGEN_SVG.Location = new System.Drawing.Point(8, 225);
            this.txtIMAGEN_SVG.Name = "txtIMAGEN_SVG";
            this.txtIMAGEN_SVG.Size = new System.Drawing.Size(214, 20);
            this.txtIMAGEN_SVG.TabIndex = 10;
            // 
            // lblImagenSvg
            // 
            this.lblImagenSvg.Location = new System.Drawing.Point(8, 208);
            this.lblImagenSvg.Name = "lblImagenSvg";
            this.lblImagenSvg.Size = new System.Drawing.Size(78, 13);
            this.lblImagenSvg.TabIndex = 9;
            this.lblImagenSvg.Text = "Ícono (recurso):";
            // 
            // txtFORMULARIO_WIN
            // 
            this.txtFORMULARIO_WIN.Location = new System.Drawing.Point(8, 181);
            this.txtFORMULARIO_WIN.Name = "txtFORMULARIO_WIN";
            this.txtFORMULARIO_WIN.Size = new System.Drawing.Size(214, 20);
            this.txtFORMULARIO_WIN.TabIndex = 8;
            // 
            // lblFormularioWin
            // 
            this.lblFormularioWin.Location = new System.Drawing.Point(8, 164);
            this.lblFormularioWin.Name = "lblFormularioWin";
            this.lblFormularioWin.Size = new System.Drawing.Size(112, 13);
            this.lblFormularioWin.TabIndex = 7;
            this.lblFormularioWin.Text = "Formulario (WinForms):";
            // 
            // chkACTIVO
            // 
            this.chkACTIVO.Location = new System.Drawing.Point(100, 138);
            this.chkACTIVO.Name = "chkACTIVO";
            this.chkACTIVO.Properties.Caption = "Activo";
            this.chkACTIVO.Size = new System.Drawing.Size(75, 20);
            this.chkACTIVO.TabIndex = 6;
            // 
            // spinORDEN
            // 
            this.spinORDEN.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinORDEN.Location = new System.Drawing.Point(8, 137);
            this.spinORDEN.Name = "spinORDEN";
            this.spinORDEN.Properties.MaxValue = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.spinORDEN.Size = new System.Drawing.Size(80, 20);
            this.spinORDEN.TabIndex = 5;
            // 
            // lblOrden
            // 
            this.lblOrden.Location = new System.Drawing.Point(8, 120);
            this.lblOrden.Name = "lblOrden";
            this.lblOrden.Size = new System.Drawing.Size(34, 13);
            this.lblOrden.TabIndex = 4;
            this.lblOrden.Text = "Orden:";
            // 
            // txtNOMBRE_OPCION
            // 
            this.txtNOMBRE_OPCION.Location = new System.Drawing.Point(8, 93);
            this.txtNOMBRE_OPCION.Name = "txtNOMBRE_OPCION";
            this.txtNOMBRE_OPCION.Size = new System.Drawing.Size(214, 20);
            this.txtNOMBRE_OPCION.TabIndex = 3;
            // 
            // lblNombreOpcion
            // 
            this.lblNombreOpcion.Location = new System.Drawing.Point(8, 76);
            this.lblNombreOpcion.Name = "lblNombreOpcion";
            this.lblNombreOpcion.Size = new System.Drawing.Size(41, 13);
            this.lblNombreOpcion.TabIndex = 2;
            this.lblNombreOpcion.Text = "Nombre:";
            // 
            // lblNIVEL
            // 
            this.lblNIVEL.Location = new System.Drawing.Point(8, 50);
            this.lblNIVEL.Name = "lblNIVEL";
            this.lblNIVEL.Size = new System.Drawing.Size(36, 13);
            this.lblNIVEL.TabIndex = 1;
            this.lblNIVEL.Text = "Nivel: 1";
            // 
            // lblOpcionPadre
            // 
            this.lblOpcionPadre.Location = new System.Drawing.Point(8, 30);
            this.lblOpcionPadre.Name = "lblOpcionPadre";
            this.lblOpcionPadre.Size = new System.Drawing.Size(63, 13);
            this.lblOpcionPadre.TabIndex = 0;
            this.lblOpcionPadre.Text = "Padre: (Raíz)";
            // 
            // btnNuevo
            // 
            this.btnNuevo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnNuevo.Location = new System.Drawing.Point(1094, 314);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(107, 38);
            this.btnNuevo.TabIndex = 2;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnAgregarHijo
            // 
            this.btnAgregarHijo.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.nuevo32x32;
            this.btnAgregarHijo.Location = new System.Drawing.Point(1207, 314);
            this.btnAgregarHijo.Name = "btnAgregarHijo";
            this.btnAgregarHijo.Size = new System.Drawing.Size(117, 38);
            this.btnAgregarHijo.TabIndex = 3;
            this.btnAgregarHijo.Text = "Agregar Hijo";
            this.btnAgregarHijo.Click += new System.EventHandler(this.btnAgregarHijo_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.guardar2_32x32;
            this.btnGuardar.Location = new System.Drawing.Point(1094, 358);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(107, 38);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.eliminar32x32;
            this.btnEliminar.Location = new System.Drawing.Point(1207, 358);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(117, 38);
            this.btnEliminar.TabIndex = 5;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.salir32x32;
            this.btnSalir.Location = new System.Drawing.Point(1094, 434);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(230, 38);
            this.btnSalir.TabIndex = 6;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // frmOpcion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1330, 819);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnAgregarHijo);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.groupDetalle);
            this.Controls.Add(this.treeListOpciones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOpcion";
            this.Tag = "CONSULTA";
            this.Text = "Mantenimiento de Opciones (Menú)";
            this.Load += new System.EventHandler(this.frmOpcion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListOpciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupDetalle)).EndInit();
            this.groupDetalle.ResumeLayout(false);
            this.groupDetalle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIMAGEN_SVG.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFORMULARIO_WIN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkACTIVO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinORDEN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNOMBRE_OPCION.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeListOpciones;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.GroupControl groupDetalle;
        private DevExpress.XtraEditors.LabelControl lblOpcionPadre;
        private DevExpress.XtraEditors.LabelControl lblNIVEL;
        private DevExpress.XtraEditors.LabelControl lblNombreOpcion;
        private DevExpress.XtraEditors.TextEdit txtNOMBRE_OPCION;
        private DevExpress.XtraEditors.LabelControl lblOrden;
        private DevExpress.XtraEditors.SpinEdit spinORDEN;
        private DevExpress.XtraEditors.CheckEdit chkACTIVO;
        private DevExpress.XtraEditors.LabelControl lblFormularioWin;
        private DevExpress.XtraEditors.TextEdit txtFORMULARIO_WIN;
        private DevExpress.XtraEditors.LabelControl lblImagenSvg;
        private DevExpress.XtraEditors.TextEdit txtIMAGEN_SVG;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private DevExpress.XtraEditors.SimpleButton btnAgregarHijo;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraEditors.SimpleButton btnEliminar;
        private DevExpress.XtraEditors.SimpleButton btnSalir;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNOMBRE_OPCION;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNIVEL;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colORDEN;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colACTIVO;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colFORMULARIO_WIN;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colIMAGEN_SVG;
    }
}