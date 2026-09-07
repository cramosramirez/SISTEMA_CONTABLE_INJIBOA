using SistemaContable.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
namespace SistemaContable.UI.Forms.Inventario
{
    public partial class frmConsultaProducto : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtProductos;
        private DataTable _dtUnidadMedida;
        public frmConsultaProducto()
        {
            InitializeComponent();
        }
        #region === CARGA INICIAL ===
        private void frmConsultaProducto_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarDatos();
        }
        #endregion
        #region === CONFIGURACIÓN DEL GRID ===
        private void ConfigurarGrid()
        {
            gridControl1.ForceInitialize();
            gvProductos.OptionsView.ShowGroupPanel = false;
            gvProductos.OptionsView.ShowAutoFilterRow = true;
            gvProductos.OptionsBehavior.AutoExpandAllGroups = false;
            gvProductos.OptionsBehavior.Editable = true;
            gvProductos.OptionsFind.AlwaysVisible = true;
            gvProductos.OptionsFind.FindNullPrompt = "Buscar producto...";
            gvProductos.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gvProductos.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvProductos.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvProductos.Appearance.FocusedRow.BackColor = Color.FromArgb(204, 229, 255);
            gvProductos.Appearance.FocusedRow.Options.UseBackColor = true;
            gvProductos.Appearance.HideSelectionRow.BackColor = Color.FromArgb(204, 229, 255);
            gvProductos.Appearance.HideSelectionRow.Options.UseBackColor = true;
            gvProductos.Appearance.Row.ForeColor = Color.Black;
            gvProductos.Appearance.Row.Font = new Font("Segoe UI", 9f);
            gvProductos.Appearance.Row.Options.UseFont = true;
            // ── Columna oculta (clave) ─────────────────────────────
            colID_PRODUCTO.FieldName = "ID_PRODUCTO";
            colID_PRODUCTO.Visible = false;
            // ── Botón Editar ───────────────────────────────────────
            colEDITAR.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
            colEDITAR.Visible = true;
            colEDITAR.VisibleIndex = 0;
            colEDITAR.Width = 41;
            // ── Código referencia ──────────────────────────────────
            colCOD_REF.FieldName = "COD_REF";
            colCOD_REF.Caption = "Código";
            colCOD_REF.OptionsColumn.AllowEdit = false;
            colCOD_REF.Width = 110;
            colCOD_REF.VisibleIndex = 1;
            colCOD_REF.Visible = true;
            // ── Descripción ────────────────────────────────────────
            colDESCRIPCION.FieldName = "DESCRIPCION";
            colDESCRIPCION.Caption = "Descripción";
            colDESCRIPCION.OptionsColumn.AllowEdit = false;
            colDESCRIPCION.Width = 280;
            colDESCRIPCION.VisibleIndex = 2;
            colDESCRIPCION.Visible = true;
            // ── Categoría / Subcategoría: ocultas de la lista (a pedido de Roberto) ─
            colCATEGORIA.FieldName = "NOMBRE_CATEGORIA";
            colCATEGORIA.Caption = "Categoría";
            colCATEGORIA.Visible = false;
            colSUBCATEGORIA.FieldName = "NOMBRE_SUBCATEGORIA";
            colSUBCATEGORIA.Caption = "Subcategoría";
            colSUBCATEGORIA.Visible = false;
            // ── Unidad de medida (texto del catálogo, no el código) ─
            colUNIMEDIDA.FieldName = "UNIMEDIDA_TEXTO";
            colUNIMEDIDA.Caption = "Unidad de Medida";
            colUNIMEDIDA.OptionsColumn.AllowEdit = false;
            colUNIMEDIDA.Width = 130;
            colUNIMEDIDA.VisibleIndex = 3;
            colUNIMEDIDA.Visible = true;
            colUNIMEDIDA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colUNIMEDIDA.AppearanceCell.Options.UseTextOptions = true;
            // ── Precio ─────────────────────────────────────────────
            colPRECIO.FieldName = "PRECIO";
            colPRECIO.Caption = "Precio";
            colPRECIO.DisplayFormat.FormatString = "N2";
            colPRECIO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPRECIO.OptionsColumn.AllowEdit = false;
            colPRECIO.Width = 90;
            colPRECIO.VisibleIndex = 4;
            colPRECIO.Visible = true;
            colPRECIO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colPRECIO.AppearanceCell.Options.UseTextOptions = true;
            // ── Estado (checkbox ACT/INA) ──────────────────────────
            colESTADO.FieldName = "ESTADO";
            colESTADO.Caption = "Activo";
            colESTADO.OptionsColumn.AllowEdit = false;
            colESTADO.Width = 60;
            colESTADO.VisibleIndex = 5;
            colESTADO.Visible = true;
            colESTADO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colESTADO.AppearanceCell.Options.UseTextOptions = true;
            var riEstado = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            riEstado.ValueChecked = "ACT";
            riEstado.ValueUnchecked = "INA";
            riEstado.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            gridControl1.RepositoryItems.Add(riEstado);
            colESTADO.ColumnEdit = riEstado;
            // ── Navigator ─────────────────────────────────────────
            gridControl1.UseEmbeddedNavigator = true;
            var nav = gridControl1.EmbeddedNavigator;
            nav.Buttons.Append.Visible = false;
            nav.Buttons.Remove.Visible = false;
            nav.Buttons.Edit.Visible = false;
            nav.Buttons.EndEdit.Visible = false;
            nav.Buttons.CancelEdit.Visible = false;
        }
        #endregion
        #region === CARGA DE DATOS ===
        private void CargarDatos()
        {
            _dtProductos = _dal.EjecutarConsulta("[EINVENTARIO].[SP_PRODUCTO]", new
            {
                ACCION = "LISTAR"
            });
            // El SP devuelve el código de UNIMEDIDA (mismo patrón que Tributo en frmProducto);
            // se resuelve aquí a texto contra el catálogo para mostrarlo en la lista.
            _dtUnidadMedida = _dal.EjecutarConsulta("[EMH].[SP_UNIDAD_MEDIDA]", new
            {
                ACCION = "LISTAR"
            });
            AgregarColumnaUnidadMedidaTexto();
            gridControl1.DataSource = _dtProductos;
        }
        private void AgregarColumnaUnidadMedidaTexto()
        {
            if (!_dtProductos.Columns.Contains("UNIMEDIDA_TEXTO"))
                _dtProductos.Columns.Add("UNIMEDIDA_TEXTO", typeof(string));
            foreach (DataRow fila in _dtProductos.Rows)
            {
                object valorCodigo = fila["UNIMEDIDA"];
                fila["UNIMEDIDA_TEXTO"] = ObtenerDescripcionCatalogo(_dtUnidadMedida, "CODIGO", valorCodigo, "VALORES");
            }
        }
        /// <summary>
        /// Busca en un catálogo ya cargado en memoria el texto a mostrar para un
        /// código dado (mismo patrón que frmProducto.ObtenerDescripcionCatalogo).
        /// </summary>
        private static string ObtenerDescripcionCatalogo(DataTable dt, string campoClave, object valorClave, string campoDescripcion)
        {
            if (dt == null || valorClave == null || valorClave == DBNull.Value) return "";
            var fila = dt.AsEnumerable().FirstOrDefault(r =>
                r[campoClave]?.ToString() == valorClave.ToString());
            return fila == null ? "" : fila[campoDescripcion].ToString();
        }
        #endregion
        #region === HELPERS ===
        private void AbrirProducto(int idProducto)
        {
            using (var frm = new frmProducto())
            {
                frm.IdProducto = idProducto;
                frm.ShowDialog(this);
            }
            CargarDatos();
        }
        #endregion
        #region === EVENTOS ===
        private void riEditar_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            int rowHandle = gvProductos.FocusedRowHandle;
            if (rowHandle < 0) return;
            object val = gvProductos.GetRowCellValue(rowHandle, colID_PRODUCTO);
            if (val == null || val == DBNull.Value) return;
            int id = Convert.ToInt32(val);
            if (id > 0) AbrirProducto(id);
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            AbrirProducto(idProducto: 0);
        }
        #endregion
    }
}