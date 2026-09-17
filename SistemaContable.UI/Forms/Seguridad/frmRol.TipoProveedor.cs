using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;

namespace SistemaContable.UI.Forms.Seguridad
{
    public partial class frmRol
    {
        private DataTable _dtTipoProveedor;
        private XtraTabPage tabTipoProveedor;
        private SimpleButton btnAgregarTipoProveedor;
        private GridControl gridTipoProveedor;
        private GridView gvTipoProveedor;

        private void InicializarPestanaTipoProveedor()
        {
            tabTipoProveedor = new XtraTabPage
            {
                Name = "tabTipoProveedor",
                Text = "Tipo Proveedor"
            };
            btnAgregarTipoProveedor = new SimpleButton
            {
                Name = "btnAgregarTipoProveedor",
                Text = "Agregar",
                Location = new System.Drawing.Point(8, 8),
                Size = new System.Drawing.Size(135, 31),
                TabStop = false
            };
            btnAgregarTipoProveedor.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            btnAgregarTipoProveedor.Appearance.Options.UseFont = true;
            btnAgregarTipoProveedor.ImageOptions.Image = Properties.Resources.nuevo32x32;
            btnAgregarTipoProveedor.Click += btnAgregarTipoProveedor_Click;

            gridTipoProveedor = new GridControl
            {
                Name = "gridTipoProveedor",
                Location = new System.Drawing.Point(8, 43),
                Size = new System.Drawing.Size(436, 143),
                TabIndex = 1
            };
            gvTipoProveedor = new GridView(gridTipoProveedor)
            {
                Name = "gvTipoProveedor"
            };
            gvTipoProveedor.OptionsView.ShowIndicator = false;
            gridTipoProveedor.MainView = gvTipoProveedor;
            gridTipoProveedor.ViewCollection.Add(gvTipoProveedor);

            tabTipoProveedor.Controls.Add(btnAgregarTipoProveedor);
            tabTipoProveedor.Controls.Add(gridTipoProveedor);
            xtraTabAsociaciones.TabPages.Add(tabTipoProveedor);
            InicializarGridTipoProveedor();
        }

        private void InicializarGridTipoProveedor()
        {
            _dtTipoProveedor = new DataTable();
            _dtTipoProveedor.Columns.Add("ID_ROL_TPP", typeof(int));
            _dtTipoProveedor.Columns.Add("ID_ROL", typeof(int));
            _dtTipoProveedor.Columns.Add("ID_TIPO_PROVEEDOR", typeof(int));
            _dtTipoProveedor.Columns.Add("NOMBRE_TIPO_PROVEEDOR", typeof(string));
            _dtTipoProveedor.Columns.Add("ACTIVO", typeof(bool));
            _dtTipoProveedor.Columns.Add("FECHA_ASIGNACION", typeof(DateTime));
            gridTipoProveedor.DataSource = _dtTipoProveedor;

            gvTipoProveedor.Columns.Clear();
            gvTipoProveedor.PopulateColumns();
            OcultarColumna(gvTipoProveedor, "ID_ROL_TPP");
            OcultarColumna(gvTipoProveedor, "ID_ROL");
            OcultarColumna(gvTipoProveedor, "ID_TIPO_PROVEEDOR");

            var colTipo = gvTipoProveedor.Columns["NOMBRE_TIPO_PROVEEDOR"];
            colTipo.Caption = "Tipo Proveedor";
            colTipo.Width = 220;
            colTipo.VisibleIndex = 0;
            var repoTexto = new RepositoryItemTextEdit();
            repoTexto.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter || ((TextEdit)s).Text?.Trim() != "*") return;
                e.Handled = true;
                AbrirBusquedaTipoProveedorGrid();
            };
            gridTipoProveedor.RepositoryItems.Add(repoTexto);
            colTipo.ColumnEdit = repoTexto;

            ConfigurarColumnaCheckBox(gridTipoProveedor, gvTipoProveedor, "ACTIVO", "Activo", 55);
            var colFecha = gvTipoProveedor.Columns["FECHA_ASIGNACION"];
            colFecha.Caption = "Fecha Asignación";
            colFecha.Width = 120;
            colFecha.VisibleIndex = 2;
            colFecha.DisplayFormat.FormatString = "dd/MM/yyyy";
            colFecha.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            var repoFecha = new RepositoryItemDateEdit();
            repoFecha.DisplayFormat.FormatString = "dd/MM/yyyy";
            repoFecha.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridTipoProveedor.RepositoryItems.Add(repoFecha);
            colFecha.ColumnEdit = repoFecha;

            var colEliminar = gvTipoProveedor.Columns.AddField("ELIMINAR");
            colEliminar.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            colEliminar.Caption = " ";
            colEliminar.Width = 36;
            colEliminar.Visible = true;
            colEliminar.VisibleIndex = 3;
            colEliminar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            var repoEliminar = new RepositoryItemButtonEdit();
            repoEliminar.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            repoEliminar.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            repoEliminar.Buttons[0].ImageOptions.Image = Properties.Resources.eliminarFila32x32;
            repoEliminar.Buttons[0].ToolTip = "Eliminar tipo de proveedor";
            repoEliminar.ButtonClick += (s, e) => EliminarTipoProveedor();
            gridTipoProveedor.RepositoryItems.Add(repoEliminar);
            colEliminar.ColumnEdit = repoEliminar;
            AplicarAparienciaGrid(gvTipoProveedor);
        }

        private void AbrirBusquedaTipoProveedorGrid()
        {
            var config = new BusquedaConfig
            {
                StoredProcedure = "[ESEGURIDAD].[SP_TIPO_PROVEEDOR]",
                Accion = "LISTAR",
                Columnas = new Dictionary<string, string>
                {
                    { "NOMBRE_TIPO_PROVEEDOR", "TIPO DE PROVEEDOR" }
                },
                Anchos = new Dictionary<string, int>
                {
                    { "NOMBRE_TIPO_PROVEEDOR", 300 }
                }
            };
            using (var frm = new frmBusquedaGenerica(config))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                if (frm.ShowDialog() == DialogResult.OK && frm.FilaSeleccionada != null)
                {
                    gvTipoProveedor.SetFocusedRowCellValue("ID_TIPO_PROVEEDOR",
                        Convert.ToInt32(frm.FilaSeleccionada["ID_TIPO_PROVEEDOR"]));
                    gvTipoProveedor.SetFocusedRowCellValue("NOMBRE_TIPO_PROVEEDOR",
                        frm.FilaSeleccionada["NOMBRE_TIPO_PROVEEDOR"].ToString());
                }
                else
                {
                    gvTipoProveedor.SetFocusedRowCellValue("NOMBRE_TIPO_PROVEEDOR", "");
                }
            }
        }

        private void CargarTipoProveedorExistentes(int idRol)
        {
            DataTable dt = _dal.EjecutarConsulta("[ESEGURIDAD].[SP_ROL_TIPO_PROVEEDOR]",
                new { ACCION = "LISTAR", ID_ROL = idRol });
            _dtTipoProveedor.Clear();
            if (dt == null) return;
            foreach (DataRow row in dt.Rows)
            {
                _dtTipoProveedor.Rows.Add(
                    row["ID_ROL_TPP"], row["ID_ROL"], row["ID_TIPO_PROVEEDOR"],
                    row["NOMBRE_TIPO_PROVEEDOR"], row["ACTIVO"], row["FECHA_ASIGNACION"]);
            }
        }

        private void btnAgregarTipoProveedor_Click(object sender, EventArgs e)
        {
            _dtTipoProveedor.Rows.Add(0, IdRol, DBNull.Value, "", true, DateTime.Today);
        }

        private void EliminarTipoProveedor()
        {
            int fila = gvTipoProveedor.FocusedRowHandle;
            if (fila < 0) return;
            if (MessageBox.Show("¿Desea eliminar este tipo de proveedor del rol?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            object valor = gvTipoProveedor.GetRowCellValue(fila, "ID_ROL_TPP");
            if (valor != null && valor != DBNull.Value && Convert.ToInt32(valor) > 0)
            {
                _dal.EjecutarSinRetorno("[ESEGURIDAD].[SP_ROL_TIPO_PROVEEDOR]",
                    new { ACCION = "ELIMINAR", ID_ROL_TPP = Convert.ToInt32(valor) });
            }
            gvTipoProveedor.DeleteRow(fila);
        }

        private void GuardarTipoProveedor()
        {
            gvTipoProveedor.CloseEditor();
            gvTipoProveedor.UpdateCurrentRow();
            foreach (DataRow fila in _dtTipoProveedor.Rows)
            {
                if (fila["ID_TIPO_PROVEEDOR"] == DBNull.Value) continue;
                _dal.EjecutarSinRetorno("[ESEGURIDAD].[SP_ROL_TIPO_PROVEEDOR]", new
                {
                    ACCION = "GUARDAR",
                    ID_ROL_TPP = fila["ID_ROL_TPP"] == DBNull.Value ? 0 : Convert.ToInt32(fila["ID_ROL_TPP"]),
                    ID_ROL = IdRol,
                    ID_TIPO_PROVEEDOR = Convert.ToInt32(fila["ID_TIPO_PROVEEDOR"]),
                    ACTIVO = fila["ACTIVO"] == DBNull.Value || Convert.ToBoolean(fila["ACTIVO"]),
                    FECHA_ASIGNACION = fila["FECHA_ASIGNACION"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(fila["FECHA_ASIGNACION"]),
                    USUARIO = Configuracion.UsuarioActual
                });
            }
            CargarTipoProveedorExistentes(IdRol);
        }
    }
}
