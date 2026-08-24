using SistemaContable.DAL;
using SistemaContable.UI.Forms.Proveedores; // frmFacturaSujetoExcluido vive en Proveedores
using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace SistemaContable.UI.Forms.Planilla
{
    // Hermano de frmFacturacionInt, dedicado a Sujeto Excluido: consume
    // [EGENERALES].[SP_TIPO_DOCUMENTO] @ACCION='FACTURA_SUJETO_EXCLUIDO' (ID_TIPO_COMPROB = 10).
    public partial class frmSujectoExcInt : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtCandidatos;
        private DataTable _dtDocumentos;

        public frmSujectoExcInt()
        {
            InitializeComponent();
        }

        private void frmSujectoExcInt_Load(object sender, EventArgs e)
        {
            ConfigurarGridCandidatos();
            ConfigurarGridDocumentos();

            CargarTipoComprobante();
            CargarZafra();
            CargarTipoPlanilla();
        }

        #region === CARGA DE COMBOS ===

        private void CargarTipoComprobante()
        {
            var dt = _dal.EjecutarConsulta("[EGENERALES].[SP_TIPO_DOCUMENTO]",
                new { ACCION = "FACTURA_SUJETO_EXCLUIDO" });

            cbxTIPO_COMPROBANTE.DataSource = dt;
            cbxTIPO_COMPROBANTE.ValueMember = "ID_TIPO_COMPROB";
            cbxTIPO_COMPROBANTE.DisplayMember = "NOMBRE_TIPO";
        }

        private void CargarZafra()
        {
            var dt = _dal.EjecutarConsulta("[EGENERALES].[SP_ZAFRA]", new { ACCION = "OBTENER_CB" });

            cbxZAFRA.DataSource = dt;
            cbxZAFRA.ValueMember = "ID_ZAFRA";
            cbxZAFRA.DisplayMember = "NOMBRE_ZAFRA";
        }

        private void CargarCatorcena(int idZafra)
        {
            var dt = _dal.EjecutarConsulta("[EGENERALES].[SP_CATORCENA]",
                new { ACCION = "OBTENER", ID_ZAFRA = idZafra });

            cbxCATORCENA.DataSource = dt;
            cbxCATORCENA.ValueMember = "ID_CATORCENA";
            cbxCATORCENA.DisplayMember = "CATORCENA";
        }

        private void CargarTipoPlanilla()
        {
            var dt = _dal.EjecutarConsulta("[EGENERALES].[SP_TIPO_PLANILLA]", new { ACCION = "OBTENER" });

            cbxTIPO_PLANILLA.DataSource = dt;
            cbxTIPO_PLANILLA.ValueMember = "ID_TIPO_PLANILLA";
            cbxTIPO_PLANILLA.DisplayMember = "NOMBRE_TIPO_PLANILLA";
        }

        private void cbxZAFRA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxZAFRA.SelectedValue is int idZafra)
                CargarCatorcena(idZafra);
        }

        #endregion

        #region === GRID SUPERIOR (candidatos — SP_COMPROB_PLANILLA) ===

        private void ConfigurarGridCandidatos()
        {
            gridControl1.ForceInitialize();
            gvCandidatos.OptionsView.ShowGroupPanel = false;
            gvCandidatos.OptionsView.ShowAutoFilterRow = false;
            gvCandidatos.OptionsBehavior.Editable = false;
            gvCandidatos.OptionsFind.AlwaysVisible = true;
            gvCandidatos.OptionsFind.FindNullPrompt = "Introduzca el texto a buscar...";
            gvCandidatos.OptionsSelection.EnableAppearanceFocusedCell = false;
        }

        private bool ValidarFiltros()
        {
            if (cbxTIPO_COMPROBANTE.SelectedValue == null ||
                cbxZAFRA.SelectedValue == null ||
                cbxCATORCENA.SelectedValue == null ||
                cbxTIPO_PLANILLA.SelectedValue == null)
            {
                XtraMessageBox.Show("Debe seleccionar Tipo Comprobante, Zafra, Catorcena y Tipo Planilla.",
                    "Sujeto Excluido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (!ValidarFiltros()) return;

            _dtCandidatos = _dal.EjecutarConsulta("[ECOMPROB].[SP_COMPROB_PLANILLA]", new
            {
                ACCION = "OBTENER",
                ID_TIPO_COMPROB = cbxTIPO_COMPROBANTE.SelectedValue,
                ID_ZAFRA = cbxZAFRA.SelectedValue,
                ID_CATORCENA = cbxCATORCENA.SelectedValue,
                ID_TIPO_PLANILLA = cbxTIPO_PLANILLA.SelectedValue
            });

            gridControl1.DataSource = _dtCandidatos;
            gridControl1.Refresh();

            CargarGridDocumentos();
        }

        #endregion

        #region === GRID INFERIOR (documentos ya generados) ===

        private void ConfigurarGridDocumentos()
        {
            gridControl2.ForceInitialize();
            gvDocumentos.OptionsView.ShowGroupPanel = false;
            gvDocumentos.OptionsView.ShowAutoFilterRow = false;
            gvDocumentos.OptionsBehavior.Editable = false;
            gvDocumentos.OptionsFind.AlwaysVisible = true;
            gvDocumentos.OptionsFind.FindNullPrompt = "Introduzca el texto a buscar...";
            gvDocumentos.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvDocumentos.RowCellClick += gvDocumentos_RowCellClick;
        }

        private void CargarGridDocumentos()
        {
            // TODO: pendiente de confirmar con Roberto. Revisé el SP completo de
            // [dbo].[SP_FACTURA_SUJETO_EXC] y ninguna de sus acciones existentes lista "todas las
            // facturas de Sujeto Excluido generadas" (que es lo que necesita este grid):
            //   - OBTENER            -> una sola fila por ID_FSE
            //   - LISTAR_POR_UID     -> filtra por @UID_ENLACE_CHEQUE (enlace a un cheque puntual,
            //                           no un listado general)
            //   - LISTAR_UIDS_HUERFANOS -> agrupa por UID_ENLACE_CHEQUE huérfano y @USUARIO
            // Falta agregar una acción tipo LISTAR (igual a SP_CREDITOFISCAL_ENC) o confirmar cuál
            // usar. El grid queda vacío mientras tanto.
        }

        private int? ObtenerIdDocumentoFilaActiva()
        {
            if (gvDocumentos.FocusedRowHandle < 0) return null;
            object val = gvDocumentos.GetRowCellValue(gvDocumentos.FocusedRowHandle, "ID_FSE");
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }

        private void gvDocumentos_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            int? id = ObtenerIdDocumentoFilaActiva();
            if (!id.HasValue) return;
            AbrirDocumento(id.Value);
        }

        private void AbrirDocumento(int id)
        {
            using (var frm = new frmFacturaSujetoExcluido())
            {
                frm.IdFse = id;
                frm.ShowDialog(this);
            }

            CargarGridDocumentos();
        }

        #endregion

        #region === BOTONES ===

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (cbxTIPO_COMPROBANTE.SelectedValue == null)
            {
                XtraMessageBox.Show("Seleccione primero el Tipo Comprobante.", "Sujeto Excluido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AbrirDocumento(0);
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            // TODO: mismo comportamiento pendiente que en frmFacturacionInt (pasar la fila
            // seleccionada del grid superior al inferior y abrir frmFacturaSujetoExcluido precargado).
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            // TODO: pendiente de definir.
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
