using SistemaContable.DAL;
using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace SistemaContable.UI.Forms.Planilla
{
    // Hermano de frmFacturacionInt, dedicado a Retención: consume
    // [EGENERALES].[SP_TIPO_DOCUMENTO] @ACCION='RETENCION' (ID_TIPO_COMPROB = 9).
    //
    // TODO: a diferencia de Crédito Fiscal (frmCreditoFiscal/SP_CREDITOFISCAL_ENC) y Sujeto
    // Excluido (frmFacturaSujetoExcluido/SP_FACTURA_SUJETO_EXC), no encontré un formulario ni un
    // SP existente que represente "documento de Retención ya generado" — SP_COMPROB_PLANILLA
    // (rama RETENCION) excluye candidatos ya presentes en [ERPMH].ECHEQUES.quedan, lo que sugiere
    // que podría estar relacionado con frmDocumentoCompra/frmConsultaQuedan, pero falta confirmar
    // con Roberto antes de cablear el grid inferior y el botón Nuevo.
    public partial class frmRetencionInt : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtCandidatos;
        private DataTable _dtDocumentos;

        public frmRetencionInt()
        {
            InitializeComponent();
        }

        private void frmRetencionInt_Load(object sender, EventArgs e)
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
                new { ACCION = "RETENCION" });

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
                    "Retención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        }

        private void CargarGridDocumentos()
        {
            // TODO: pendiente de confirmar con Roberto el formulario/SP de "Retención ya generada"
            // (ver nota al inicio del archivo). El grid queda vacío mientras tanto.
        }

        #endregion

        #region === BOTONES ===

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // TODO: pendiente de confirmar con Roberto qué formulario abre "Nuevo" para Retención
            // (candidato: frmDocumentoCompra, a confirmar). Placeholder por ahora.
            XtraMessageBox.Show("Pendiente de definir el formulario de Retención.", "Retención",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            // TODO: mismo comportamiento pendiente que en frmFacturacionInt (pasar la fila
            // seleccionada del grid superior al inferior y abrir el documento de Retención precargado).
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
