using SistemaContable.DAL;
using SistemaContable.UI.Forms.Ventas; // frmCreditoFiscal vive en Ventas
using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace SistemaContable.UI.Forms.Planilla
{
    // Pantalla dedicada a Crédito Fiscal: consume [EGENERALES].[SP_TIPO_DOCUMENTO] @ACCION='FACTURA_CCF'
    // (ID_TIPO_COMPROB en 1, 2, 11). Sujeto Excluido y Retención tienen sus propios formularios:
    // frmSujectoExcInt y frmRetencionInt.
    public partial class frmFacturacionInt : Form
    {
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtCandidatos;
        private DataTable _dtDocumentos;

        public frmFacturacionInt()
        {
            InitializeComponent();
        }

        private void frmFacturacionInt_Load(object sender, EventArgs e)
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
            // Pantalla fija a Crédito Fiscal: solo la acción FACTURA_CCF (ID_TIPO_COMPROB 1, 2, 11).
            var dt = _dal.EjecutarConsulta("[EGENERALES].[SP_TIPO_DOCUMENTO]",
                new { ACCION = "FACTURA_CCF" });

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
                    "Facturación Interna", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        // Reutiliza EXACTAMENTE la misma consulta de frmConsultaCreditoFiscal (SP_CREDITOFISCAL_ENC LISTAR),
        // ya que esta pantalla está fija a Crédito Fiscal. El grid se puebla con PopulateColumns()
        // en vez de columnas fijas en el Designer, igual patrón reutilizable para las pantallas hermanas.

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
            // Mismo query que frmConsultaCreditoFiscal
            _dtDocumentos = _dal.EjecutarConsulta("[EDTE].[SP_CREDITOFISCAL_ENC]", new
            {
                ACCION = "LISTAR",
                ID_EMISOR = 1
            });

            gridControl2.DataSource = _dtDocumentos;
            gvDocumentos.PopulateColumns();
            AjustarColumnasDocumentos();
            gridControl2.Refresh();
        }

        // Ajusta captions/orden de las columnas que trae SP_CREDITOFISCAL_ENC @ACCION='LISTAR'.
        private void AjustarColumnasDocumentos()
        {
            void Ajustar(string campo, string caption, int? ancho = null)
            {
                var col = gvDocumentos.Columns[campo];
                if (col == null) return;
                col.Caption = caption;
                col.OptionsColumn.AllowEdit = false;
                if (ancho.HasValue) col.Width = ancho.Value;
            }

            Ajustar("ID_CCFENC", "Sistema(Id)", 80);
            Ajustar("FECHA", "Fecha", 90);
            Ajustar("NUMINTERNO", "N° Interno", 130);
            Ajustar("NOMBRE_ENTIDAD", "Cliente", 260);
            Ajustar("CODGENERACION", "Cód. Generación", 220);
            Ajustar("NUMCONTROL", "N° Control", 220);
            Ajustar("SELLORECEPCION", "Sello Recepción", 200);
            Ajustar("TOTALVENTA", "Total Venta", 100);
        }

        private int? ObtenerIdDocumentoFilaActiva()
        {
            if (gvDocumentos.FocusedRowHandle < 0) return null;
            object val = gvDocumentos.GetRowCellValue(gvDocumentos.FocusedRowHandle, "ID_CCFENC");
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
            using (var frm = new frmCreditoFiscal())
            {
                // TODO: confirmar el nombre real de la propiedad de entrada de frmCreditoFiscal (IdCCFEnc).
                frm.IdCCFEnc = id;
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
                XtraMessageBox.Show("Seleccione primero el Tipo Comprobante.", "Facturación Interna",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AbrirDocumento(0);
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            // TODO (pendiente, según lo hablado): tomar la fila seleccionada del grid superior
            // (candidato de SP_COMPROB_PLANILLA) y pasar sus datos al grid inferior, abriendo
            // frmCreditoFiscal precargado. Se deja sin implementar por ahora.
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
