using SistemaContable.DAL;
using System;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Newtonsoft.Json; // solo para deserializar respuesta de Hacienda

namespace SistemaContable.UI.Forms.Invalidacion
{
    public partial class frmInvalidacion : Form
    {
        #region Enum / Campos Privados
        private enum EstadoFormulario { Nuevo, Habilitado, Guardado, Validado }
        private readonly DALBase _dal = new DALBase();
        private const string API_URL = "http://192.168.1.76/api/Invalidacion";

        private int _tipoMov = 0;
        private int _idTabla = 0;
        private string _codGeneracion = string.Empty;
        private string _selloRecepcion = string.Empty;

        // Versión y ambiente desde DOCUMENTO_NUMERACION
        private int _version = 1;
        private string _ambiente = "00";

        // Campos del emisor/documento que no se muestran en pantalla
        private int _idEmisor = 0;
        private string _emisor_codEstableMH = string.Empty;
        private string _emisor_codEstable = string.Empty;
        private string _emisor_codPuntoVentaMH = string.Empty;
        private string _emisor_codPuntoVenta = string.Empty;
        private string _emisor_nomEstablecimiento = string.Empty;
        private string _doc_tipoDte = string.Empty;

        public int IdInvalidacion { get; set; }
        #endregion

        #region Constructor / Load
        public frmInvalidacion()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void frmInvalidacion_Load(object sender, EventArgs e)
        {
            CargarSucursal();
            CargarTiposInvalidacion();
            CargarTiposDocumentoIdentificacion();
            CargarTipoEstablecimiento();
            rbFacturacion.Checked = true;
            if (IdInvalidacion == 0)
                InicializarNuevo();
            else
                CargarExistente(IdInvalidacion);
        }
        private void InicializarNuevo()
        {
            txtSistemaId.Text = "0";
            txtIdOrigen.Text = "0";
            _tipoMov = 0;
            rbProveedores.Checked = false;
            rbFacturacion.Checked = false;
            rbExportacion.Checked = false;
            cbxTipoDocumento.DataSource = null;
            cbxDocSellados.DataSource = null;
            txtNGeneracion.Text = Guid.NewGuid().ToString().ToUpper();
            dtpFechaAnulacion.Value = DateTime.Today;
            dtpHoraAnulacion.Value = DateTime.Now;
            dtpFechaDocumento.Value = DateTime.Today;
            ConfigurarCRUD(EstadoFormulario.Nuevo);
        }
        #endregion

        #region Carga de Combos
        private void CargarSucursal()
        {
            DataTable dt = _dal.EjecutarConsulta("[dbo].[SP_SUCURSAL]",
                new { ACCION = "OBTENER_SUCURSAL" });
            cbxSucursal.DataSource = dt;
            cbxSucursal.DisplayMember = "NOMBRE";
            cbxSucursal.ValueMember = "SUCURSAL";
            cbxSucursal.Enabled = false;
        }
        private void CargarTiposInvalidacion()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_TIPO_DE_INVALIDACION]",
                new { ACCION = "BUSCAR" });
            if (dt == null) dt = new DataTable();
            cbxTipoInvalidacion.DisplayMember = "VALORES";
            cbxTipoInvalidacion.ValueMember = "CODIGO";
            cbxTipoInvalidacion.DataSource = dt;
        }
        private void CargarTipoEstablecimiento()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_TIPO_ESTABLECIMIENTO]",
                new { ACCION = "BUSCAR" });
            if (dt == null) dt = new DataTable();
            cbxTipoEstablecimiento.DisplayMember = "VALORES";
            cbxTipoEstablecimiento.ValueMember = "CODIGO";
            cbxTipoEstablecimiento.DataSource = dt;
        }
        private void CargarTiposDocumentoIdentificacion()
        {
            DataTable dt = _dal.EjecutarConsulta(
                "[EMH].[SP_TIPO_DE_DOCUMENTO_DE_IDENTIFICACION_DEL_RECEPTOR]",
                new { ACCION = "BUSCAR" });
            if (dt == null) dt = new DataTable();
            cbxTipoDocResponsable.DisplayMember = "VALORES";
            cbxTipoDocResponsable.ValueMember = "CODIGO";
            cbxTipoDocResponsable.DataSource = dt;
            DataTable dt2 = dt.Copy();
            cbxTipoDocSolicita.DisplayMember = "VALORES";
            cbxTipoDocSolicita.ValueMember = "CODIGO";
            cbxTipoDocSolicita.DataSource = dt2;
            DataTable dt3 = dt.Copy();
            cbxDocTipoDocumento.DisplayMember = "VALORES";
            cbxDocTipoDocumento.ValueMember = "CODIGO";
            cbxDocTipoDocumento.DataSource = dt3;
        }
        private void CargarTiposDocumentoPorModulo()
        {
            if (_tipoMov == 0) return;
            DataTable dt = _dal.EjecutarConsulta("[EMANTENIMIENTO].[SP_TIPO_DTE_MOV]",
                new { ACCION = "BUSCAR_TIPO_DTE", TIPO_MOV = _tipoMov });
            if (dt == null) dt = new DataTable();
            if (!dt.Columns.Contains("ID_TPDTE")) dt.Columns.Add("ID_TPDTE", typeof(int));
            if (!dt.Columns.Contains("DOCUMENTO")) dt.Columns.Add("DOCUMENTO", typeof(string));
            foreach (DataColumn col in dt.Columns) col.AllowDBNull = true;
            DataRow fila = dt.NewRow();
            fila["ID_TPDTE"] = DBNull.Value;
            fila["DOCUMENTO"] = "-- Seleccione --";
            dt.Rows.InsertAt(fila, 0);
            cbxTipoDocumento.DisplayMember = "DOCUMENTO";
            cbxTipoDocumento.ValueMember = "ID_TPDTE";
            cbxTipoDocumento.DataSource = dt;
            cbxTipoDocumento.SelectedIndex = 0;
        }
        private void CargarDocsSellados()
        {
            if (cbxTipoDocumento.SelectedValue == null
                || cbxTipoDocumento.SelectedValue == DBNull.Value) return;
            int idTpdte;
            if (!int.TryParse(cbxTipoDocumento.SelectedValue.ToString(), out idTpdte)) return;
            DataTable dt = _dal.EjecutarConsulta("[EMANTENIMIENTO].[SP_TIPO_DTE_MOV]",
                new { ACCION = "BUSCAR_DTE_SELLADOS", ID_TPDTE = idTpdte });
            if (dt == null) dt = new DataTable();
            if (!dt.Columns.Contains("ID")) dt.Columns.Add("ID", typeof(int));
            if (!dt.Columns.Contains("NUMCONTROL")) dt.Columns.Add("NUMCONTROL", typeof(string));
            if (!dt.Columns.Contains("CODGENERACION")) dt.Columns.Add("CODGENERACION", typeof(string));
            if (!dt.Columns.Contains("SELLORECEPCION")) dt.Columns.Add("SELLORECEPCION", typeof(string));
            foreach (DataColumn col in dt.Columns) col.AllowDBNull = true;
            DataRow fila = dt.NewRow();
            fila["ID"] = DBNull.Value;
            fila["NUMCONTROL"] = "-- Seleccione --";
            fila["CODGENERACION"] = DBNull.Value;
            fila["SELLORECEPCION"] = DBNull.Value;
            dt.Rows.InsertAt(fila, 0);
            cbxDocSellados.DisplayMember = "NUMCONTROL";
            cbxDocSellados.ValueMember = "ID";
            cbxDocSellados.DataSource = dt;
            cbxDocSellados.SelectedIndex = 0;
        }
        // Llamar DESPUÉS de LlenarDatosFormulario(), cuando _doc_tipoDte ya tiene el código
        // Hacienda ('01', '03', etc.) proveniente de DOCUMENTO_NUMERACION.TIPODTE.
        private void CargarVersionAmbiente()
        {
            _version = 1;
            _ambiente = "00";
            if (string.IsNullOrWhiteSpace(_doc_tipoDte)) return;
            try
            {
                DataTable dt = _dal.EjecutarConsulta("[dbo].[SP_DOCUMENTO_NUMERACION]",
                    new
                    {
                        ACCION = "OBTENER_POR_TIPODTE",
                        TIPODTE = _doc_tipoDte.Trim(),
                        ANIO = DateTime.Now.Year
                    });
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    if (r["VERSION"] != DBNull.Value)
                        _version = Convert.ToInt32(r["VERSION"]);
                    if (r["AMBIENTE"] != DBNull.Value && !string.IsNullOrWhiteSpace(r["AMBIENTE"].ToString()))
                        _ambiente = r["AMBIENTE"].ToString().Trim();
                }
            }
            catch { /* usar defaults 1 / "00" */ }
        }
        #endregion

        #region Estado del Formulario
        private void ConfigurarCRUD(EstadoFormulario estado)
        {
            switch (estado)
            {
                case EstadoFormulario.Nuevo:
                    btnHabilitarAnulacion.Enabled = true;
                    btnGuardar.Enabled = false;
                    btnValidar.Visible = false;
                    btnImprimir.Visible = false;
                    btnCorreo.Visible = false;
                    HabilitarBusqueda(true);
                    BloquearCamposFormulario(true);
                    break;
                case EstadoFormulario.Habilitado:
                    btnHabilitarAnulacion.Enabled = false;
                    btnGuardar.Enabled = true;
                    btnValidar.Visible = false;
                    btnImprimir.Visible = false;
                    btnCorreo.Visible = false;
                    HabilitarBusqueda(false);
                    BloquearCamposFormulario(true);      // bloquea todo
                    txtMotivoInterno.ReadOnly = false;   // solo Motivo Control Interno queda activo
                    txtMotivoInterno.Focus();
                    break;
                case EstadoFormulario.Guardado:
                    btnHabilitarAnulacion.Enabled = false;
                    btnGuardar.Enabled = true;                 // permite actualizar el motivo
                    btnValidar.Visible = true;
                    btnImprimir.Visible = false;
                    btnCorreo.Visible = false;
                    BloquearCamposFormulario(true);
                    txtMotivoInterno.ReadOnly = false;          // solo el campo motivo queda activo
                    txtMotivoInterno.Focus();
                    break;
                case EstadoFormulario.Validado:
                    btnHabilitarAnulacion.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnValidar.Visible = false;
                    btnImprimir.Visible = true;
                    btnCorreo.Visible = true;
                    BloquearCamposFormulario(true);
                    break;
            }
        }
        private void HabilitarBusqueda(bool habilitar)
        {
            rbProveedores.Enabled = habilitar;
            rbFacturacion.Enabled = habilitar;
            rbExportacion.Enabled = habilitar;
            rbTranslados.Enabled = habilitar;
            cbxTipoDocumento.Enabled = habilitar;
            cbxDocSellados.Enabled = habilitar;
            dtpFechaDocumento.Enabled = habilitar;
            btnConsultar.Enabled = habilitar;
        }
        private void BloquearCamposFormulario(bool bloquear)
        {
            // Siempre solo lectura
            txtSistemaId.ReadOnly = true;
            txtIdOrigen.ReadOnly = true;
            txtNGeneracion.ReadOnly = true;
            txtSelloAnulacion.ReadOnly = true;
            dtpFechaAnulacion.Enabled = false;
            dtpHoraAnulacion.Enabled = false;
            txtEmisorNIT.ReadOnly = true;
            txtEmisorNombre.ReadOnly = true;
            txtEmisorTelefono.ReadOnly = true;
            txtEmisorCorreo.ReadOnly = true;
            cbxTipoEstablecimiento.Enabled = false;
            txtDocCodGeneracion.ReadOnly = true;
            txtDocSelloRecibido.ReadOnly = true;
            txtDocNumeroControl.ReadOnly = true;
            txtDocNombreReceptor.ReadOnly = true;
            txtDocNumeroDocumento.ReadOnly = true;
            cbxDocTipoDocumento.Enabled = false;
            dtpDocFechaEmision.Enabled = false;
            // Editables solo al habilitar
            cbxTipoInvalidacion.Enabled = !bloquear;
            txtNombreResponsable.ReadOnly = bloquear;
            cbxTipoDocResponsable.Enabled = !bloquear;
            txtNumDocResponsable.ReadOnly = bloquear;
            txtNombreSolicita.ReadOnly = bloquear;
            cbxTipoDocSolicita.Enabled = !bloquear;
            txtNumDocSolicita.ReadOnly = bloquear;
            txtMotivoInterno.ReadOnly = bloquear;
            txtDocCorreo.ReadOnly = bloquear;
            txtDocCorreoCC.ReadOnly = bloquear;
            txtDocTelefono.ReadOnly = bloquear;
        }
        #endregion

        #region Radio Buttons — Módulo
        private void rbProveedores_CheckedChanged(object sender, EventArgs e)
        {
            cbxDocSellados.DataSource = null;
            if (!rbProveedores.Checked) return;
            _tipoMov = 1; LimpiarSeleccionDocumento(); CargarTiposDocumentoPorModulo();
        }
        private void rbFacturacion_CheckedChanged(object sender, EventArgs e)
        {
            cbxTipoDocumento.DataSource = null;
            cbxDocSellados.DataSource = null;
            _tipoMov = 0;
            if (!rbFacturacion.Checked) return;
            _tipoMov = 2; LimpiarSeleccionDocumento(); CargarTiposDocumentoPorModulo();
        }
        private void rbExportacion_CheckedChanged(object sender, EventArgs e)
        {
            cbxDocSellados.DataSource = null;
            if (!rbExportacion.Checked) return;
            _tipoMov = 3; LimpiarSeleccionDocumento(); CargarTiposDocumentoPorModulo();
        }
        private void rbTranslados_CheckedChanged(object sender, EventArgs e)
        {
            cbxDocSellados.DataSource = null;
            if (!rbTranslados.Checked) return;
            _tipoMov = 4; LimpiarSeleccionDocumento(); CargarTiposDocumentoPorModulo();
        }
        private void LimpiarSeleccionDocumento()
        {
            cbxDocSellados.DataSource = null;
            _idTabla = 0;
            _codGeneracion = string.Empty;
            _selloRecepcion = string.Empty;
        }
        #endregion

        #region Búsqueda / Consultar
        private void cbxTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxDocSellados.DataSource = null;
            CargarDocsSellados();
        }
        private void dtpFechaDocumento_ValueChanged(object sender, EventArgs e)
        {
            cbxDocSellados.DataSource = null;
        }
        private void cbxDocSellados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(cbxDocSellados.SelectedItem is DataRowView row)) return;
            if (row["ID"] == DBNull.Value) { _idTabla = 0; return; }
            _idTabla = Convert.ToInt32(row["ID"]);
            _codGeneracion = row["CODGENERACION"].ToString();
            _selloRecepcion = row["SELLORECEPCION"].ToString();
            txtIdOrigen.Text = _idTabla.ToString();
        }
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (_idTabla == 0)
            {
                XtraMessageBox.Show("Seleccione un documento sellado.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ConsultarDocumento();
        }
        private void ConsultarDocumento()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DataTable dt = _dal.EjecutarConsulta("[EMANTENIMIENTO].[SP_TIPO_DTE_MOV]",
                    new
                    {
                        ACCION = "OBTENER_DTE_SELLADOS",
                        ID_TPDTE = Convert.ToInt32(cbxTipoDocumento.SelectedValue),
                        ID_ORIGEN = _idTabla
                    });
                if (dt == null || dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No se encontró información del documento.",
                        "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                LlenarDatosFormulario(dt.Rows[0]);
                CargarVersionAmbiente();   // _doc_tipoDte ya está cargado aquí
                btnGuardar.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al consultar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void LlenarDatosFormulario(DataRow r)
        {
            // Emisor
            txtEmisorNIT.Text = AsStr(r["EMISOR_NIT"]);
            txtEmisorNombre.Text = AsStr(r["EMISOR_NOMBRE"]);
            TrySetCombo(cbxTipoEstablecimiento, r, "EMISOR_TIPO_ESTABLECIMIENTO");
            txtEmisorTelefono.Text = AsStr(r["EMISOR_TELEFONO"]);
            txtEmisorCorreo.Text = AsStr(r["EMISOR_CORREO"]);
            // Campos adicionales del emisor (si OBTENER_DTE_SELLADOS los devuelve)
            _idEmisor = r.Table.Columns.Contains("ID_EMISOR") && r["ID_EMISOR"] != DBNull.Value ? Convert.ToInt32(r["ID_EMISOR"]) : 0;
            _emisor_codEstableMH = AsStr(r.Table.Columns.Contains("EMISOR_COD_ESTABLE_MH") ? r["EMISOR_COD_ESTABLE_MH"] : DBNull.Value);
            _emisor_codEstable = AsStr(r.Table.Columns.Contains("EMISOR_COD_ESTABLE") ? r["EMISOR_COD_ESTABLE"] : DBNull.Value);
            _emisor_codPuntoVentaMH = AsStr(r.Table.Columns.Contains("EMISOR_COD_PUNTO_VENTA_MH") ? r["EMISOR_COD_PUNTO_VENTA_MH"] : DBNull.Value);
            _emisor_codPuntoVenta = AsStr(r.Table.Columns.Contains("EMISOR_COD_PUNTO_VENTA") ? r["EMISOR_COD_PUNTO_VENTA"] : DBNull.Value);
            _emisor_nomEstablecimiento = AsStr(r.Table.Columns.Contains("EMISOR_NOM_ESTABLECIMIENTO") ? r["EMISOR_NOM_ESTABLECIMIENTO"] : DBNull.Value);
            _doc_tipoDte = AsStr(r.Table.Columns.Contains("DOCUMENTO_TIPO_DTE") ? r["DOCUMENTO_TIPO_DTE"] : DBNull.Value);
            // Documento
            txtDocCodGeneracion.Text = AsStr(r["DOCUMENTO_CODGENERACION"]);
            txtDocSelloRecibido.Text = AsStr(r["DOCUMENTO_SELLORECEPCION"]);
            txtDocNumeroControl.Text = AsStr(r["DOCUMENTO_NUMCONTROL"]);
            TrySetDate(dtpDocFechaEmision, r, "DOCUMENTO_FECHA_EMISION");
            TrySetCombo(cbxDocTipoDocumento, r, "DOCUMENTO_TIPO");
            txtDocNumeroDocumento.Text = AsStr(r["NUMERO_DOCUMENTO"]);
            txtDocNombreReceptor.Text = AsStr(r["DOCUMENTO_NOMBRE"]);
            txtDocTelefono.Text = AsStr(r["DOCUMENTO_TELEFONO"]);
            txtDocCorreo.Text = AsStr(r["DOCUMENTO_CORREO"]);
            // Motivo
            TrySetCombo(cbxTipoInvalidacion, r, "MOTIVO_TIPO_INVALIDACION");
            txtNombreResponsable.Text = AsStr(r["MOTIVO_NOMBRE_RESPONSABLE"]);
            TrySetCombo(cbxTipoDocResponsable, r, "MOTIVO_TIPO_DOCUMENTO_RESPONSABLE");
            txtNumDocResponsable.Text = AsStr(r["MOTIVO_DOCUMENTO_RESPONSABLE"]);
            txtNombreSolicita.Text = AsStr(r["MOTIVO_NOMBRE_SOLICITANTE"]);
            TrySetCombo(cbxTipoDocSolicita, r, "MOTIVO_TIPO_DOCUMENTO_SOLICITANTE");
            txtNumDocSolicita.Text = AsStr(r["MOTIVO_DOCUMENTO_SOLICITANTE"]);
        }
        #endregion

        #region Habilitar Anulación
        private void btnHabilitarAnulacion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDocSelloRecibido.Text))
            {
                XtraMessageBox.Show("Primero consulte un documento sellado.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ConfigurarCRUD(EstadoFormulario.Habilitado);
            txtMotivoInterno.Focus();
        }
        #endregion

        #region Guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSelloAnulacion.Text))
            {
                XtraMessageBox.Show("Este documento ya fue procesado por Hacienda.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Si ya existe registro, solo actualizar el Motivo Control Interno
            if (IdInvalidacion > 0)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    _dal.EjecutarSinRetorno("[EMANTENIMIENTO].[SP_INVALIDACIONJson]",
                        new { ACCION = "ACTUALIZAR_MOTIVO", ID = IdInvalidacion, MOTIVO = txtMotivoInterno.Text.Trim() });
                    XtraMessageBox.Show("Motivo actualizado correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Error al actualizar:\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally { Cursor = Cursors.Default; }
                return;
            }

            if (!ValidarCamposObligatorios()) return;
            try
            {
                Cursor = Cursors.WaitCursor;

                DataTable dt = _dal.EjecutarConsulta("[EMANTENIMIENTO].[SP_INVALIDACIONJson]",
                    new
                    {
                        ACCION = "GUARDAR",
                        // Identificación
                        IDENTIFICACION_version = _version,
                        IDENTIFICACION_ambiente = _ambiente,
                        IDENTIFICACION_codigoGeneracion = txtNGeneracion.Text.Trim(),
                        IDENTIFICACION_fecAnula = DateTime.Now.ToString("yyyy-MM-dd"),
                        IDENTIFICACION_horAnula = DateTime.Now.ToString("HH:mm:ss"),
                        // Emisor
                        EMISOR_nit = txtEmisorNIT.Text.Trim(),
                        EMISOR_nombre = txtEmisorNombre.Text.Trim(),
                        EMISOR_tipoEstablecimiento = cbxTipoEstablecimiento.SelectedValue,
                        EMISOR_nomEstablecimiento = _emisor_nomEstablecimiento,
                        EMISOR_codEstableMH = _emisor_codEstableMH,
                        EMISOR_codEstable = _emisor_codEstable,
                        EMISOR_codPuntoVentaMH = _emisor_codPuntoVentaMH,
                        EMISOR_codPuntoVenta = _emisor_codPuntoVenta,
                        EMISOR_telefono = txtEmisorTelefono.Text.Trim(),
                        EMISOR_correo = txtEmisorCorreo.Text.Trim(),
                        // Documento
                        DOCUMENTO_tipoDte = _doc_tipoDte,
                        DOCUMENTO_codigoGeneracion = txtDocCodGeneracion.Text.Trim(),
                        DOCUMENTO_selloRecibido = txtDocSelloRecibido.Text.Trim(),
                        DOCUMENTO_numeroControl = txtDocNumeroControl.Text.Trim(),
                        DOCUMENTO_fecEmi = dtpDocFechaEmision.Value.ToString("yyyy-MM-dd"),
                        DOCUMENTO_montoIva = 0,
                        DOCUMENTO_codigoGeneracionR = (object)null,
                        DOCUMENTO_tipoDocumento = cbxDocTipoDocumento.SelectedValue,
                        DOCUMENTO_numDocumento = txtDocNumeroDocumento.Text.Trim(),
                        DOCUMENTO_nombre = txtDocNombreReceptor.Text.Trim(),
                        DOCUMENTO_telefono = txtDocTelefono.Text.Trim(),
                        DOCUMENTO_correo = txtDocCorreo.Text.Trim(),
                        DOCUMENTO_correoCC = txtDocCorreoCC.Text.Trim(),
                        // Motivo
                        MOTIVO_tipoAnulacion = cbxTipoInvalidacion.SelectedValue,
                        MOTIVO_motivoAnulacion = cbxTipoInvalidacion.Text,
                        MOTIVO_nombreResponsable = txtNombreResponsable.Text.Trim(),
                        MOTIVO_tipDocResponsable = cbxTipoDocResponsable.SelectedValue,
                        MOTIVO_numDocResponsable = txtNumDocResponsable.Text.Trim(),
                        MOTIVO_nombreSolicita = txtNombreSolicita.Text.Trim(),
                        MOTIVO_tipDocSolicita = cbxTipoDocSolicita.SelectedValue,
                        MOTIVO_numDocSolicita = txtNumDocSolicita.Text.Trim(),
                        MOTIVO = txtMotivoInterno.Text.Trim(),
                        // Referencias
                        ID_TIPO_DOCUMENTO = Convert.ToInt32(cbxTipoDocumento.SelectedValue),
                        ID_ENC = _idTabla,
                        ID_SUCURSAL = cbxSucursal.SelectedValue,
                        ID_EMISOR = _idEmisor,
                        USER_CREA = Environment.UserName,
                        // Módulo
                        PROVEEDORES = rbProveedores.Checked ? 1 : 0,
                        FACTURACION = rbFacturacion.Checked ? 1 : 0,
                        EXPORTACION = rbExportacion.Checked ? 1 : 0,
                        TRANSLADOS = rbTranslados.Checked ? 1 : 0
                    });

                if (dt == null || dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No se pudo guardar la invalidación.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string idNuevo = dt.Rows[0]["ID_GENERADO"].ToString();
                if (string.IsNullOrWhiteSpace(idNuevo) || idNuevo == "0")
                {
                    string msg = dt.Columns.Contains("MENSAJE")
                        ? dt.Rows[0]["MENSAJE"].ToString() : "Error al guardar.";
                    XtraMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                txtSistemaId.Text = idNuevo;
                IdInvalidacion = Convert.ToInt32(idNuevo);

                // Generar JSON en SQL Server, guardarlo en [JSON] y mostrarlo
                GenerarJson();

                ConfigurarCRUD(EstadoFormulario.Guardado);
                XtraMessageBox.Show("Guardado correctamente. Ya puede validar con Hacienda.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al guardar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private bool ValidarCamposObligatorios()
        {
            if (cbxTipoInvalidacion.SelectedValue == null)
            { Aviso("Seleccione el tipo de invalidación.", cbxTipoInvalidacion); return false; }
            if (string.IsNullOrWhiteSpace(txtNombreResponsable.Text))
            { Aviso("Ingrese el nombre del responsable.", txtNombreResponsable); return false; }
            if (cbxTipoDocResponsable.SelectedValue == null)
            { Aviso("Seleccione el tipo de documento del responsable.", cbxTipoDocResponsable); return false; }
            if (string.IsNullOrWhiteSpace(txtNumDocResponsable.Text))
            { Aviso("Ingrese el número de documento del responsable.", txtNumDocResponsable); return false; }
            if (string.IsNullOrWhiteSpace(txtNombreSolicita.Text))
            { Aviso("Ingrese el nombre de quien solicita.", txtNombreSolicita); return false; }
            if (cbxTipoDocSolicita.SelectedValue == null)
            { Aviso("Seleccione el tipo de documento del solicitante.", cbxTipoDocSolicita); return false; }
            if (string.IsNullOrWhiteSpace(txtNumDocSolicita.Text))
            { Aviso("Ingrese el número de documento del solicitante.", txtNumDocSolicita); return false; }
            return true;
        }
        #endregion

        #region Generación JSON  (construida en SQL Server)
        private void GenerarJson()
        {
            try
            {
                DataTable dt = _dal.EjecutarConsulta("[EMANTENIMIENTO].[SP_INVALIDACIONJson]",
                    new { ACCION = "GENERAR_JSON", ID = IdInvalidacion });
                if (dt == null || dt.Rows.Count == 0 || dt.Rows[0][0] == DBNull.Value) return;
                txtJson.Text = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al generar JSON:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Validar con Hacienda
        private void btnValidar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSelloAnulacion.Text))
            {
                XtraMessageBox.Show("Este documento ya fue validado por Hacienda.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtJson.Text))
            {
                XtraMessageBox.Show("No hay JSON generado. Guarde primero el documento.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                string respuestaRaw = EnviarAHacienda(txtJson.Text);
                var resp = JsonConvert.DeserializeObject<RespuestaHacienda>(respuestaRaw);
                if (resp == null)
                {
                    XtraMessageBox.Show("Respuesta inválida de Hacienda.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.Equals(resp.estado, "RECHAZADO", StringComparison.OrdinalIgnoreCase))
                {
                    XtraMessageBox.Show("Hacienda rechazó el documento:\n\n" + respuestaRaw,
                        "Rechazado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                GuardarSelloHacienda(resp.selloRecibido, resp.fhProcesamiento, respuestaRaw);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al validar con Hacienda:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private string EnviarAHacienda(string jsonBody)
        {
            byte[] datos = Encoding.UTF8.GetBytes(jsonBody);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(API_URL);
            req.Method = "POST";
            req.ContentType = "application/json;charset=utf-8";
            req.ContentLength = datos.Length;
            using (Stream s = req.GetRequestStream())
                s.Write(datos, 0, datos.Length);
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            using (StreamReader reader = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
                return reader.ReadToEnd();
        }
        private void GuardarSelloHacienda(string sello, string fhProcesamiento, string respuestaRaw)
        {
            _dal.EjecutarSinRetorno("[EMANTENIMIENTO].[SP_INVALIDACIONJson]",
                new
                {
                    ACCION = "ACTUALIZAR_SELLO",
                    ID = IdInvalidacion,
                    SELLORECEPCION = sello,
                    FHPROCESAMIENTO = fhProcesamiento,
                    CODGENERACION_ANULADO = txtNGeneracion.Text.Trim(),
                    RESPUESTA_JSON = respuestaRaw
                });
            txtSelloAnulacion.Text = sello;
            ConfigurarCRUD(EstadoFormulario.Validado);
            XtraMessageBox.Show("¡Documento invalidado correctamente en Hacienda!",
                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Cargar Existente
        private void CargarExistente(int id)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DataTable dt = _dal.EjecutarConsulta("[EMANTENIMIENTO].[SP_INVALIDACIONJson]",
                    new { ACCION = "CONSULTAR", ID = id });
                if (dt == null || dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No se encontró el registro.",
                        "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }
                DataRow r = dt.Rows[0];

                txtSistemaId.Text = id.ToString();
                txtNGeneracion.Text = AsStr(r["IDENTIFICACION_codigoGeneracion"]);
                txtSelloAnulacion.Text = AsStr(r["selloRecibido_anulado"]);

                // Restaurar versión y ambiente
                _version = TryInt2(r, "IDENTIFICACION_version", 1);
                _ambiente = r.Table.Columns.Contains("IDENTIFICACION_ambiente") && r["IDENTIFICACION_ambiente"] != DBNull.Value
                            ? AsStr(r["IDENTIFICACION_ambiente"]) : "00";

                // Módulo desde bits
                _idTabla = TryInt(r, "ID_ENC");
                txtIdOrigen.Text = _idTabla.ToString();
                int idTipo = TryInt(r, "ID_TIPO_DOCUMENTO");

                if (r.Table.Columns.Contains("PROVEEDORES") && r["PROVEEDORES"] != DBNull.Value && Convert.ToBoolean(r["PROVEEDORES"]))
                { _tipoMov = 1; rbProveedores.Checked = true; }
                else if (r.Table.Columns.Contains("FACTURACION") && r["FACTURACION"] != DBNull.Value && Convert.ToBoolean(r["FACTURACION"]))
                { _tipoMov = 2; rbFacturacion.Checked = true; }
                else if (r.Table.Columns.Contains("EXPORTACION") && r["EXPORTACION"] != DBNull.Value && Convert.ToBoolean(r["EXPORTACION"]))
                { _tipoMov = 3; rbExportacion.Checked = true; }
                else if (r.Table.Columns.Contains("TRANSLADOS") && r["TRANSLADOS"] != DBNull.Value && Convert.ToBoolean(r["TRANSLADOS"]))
                { _tipoMov = 4; rbTranslados.Checked = true; }

                CargarTiposDocumentoPorModulo();
                if (idTipo > 0) cbxTipoDocumento.SelectedValue = idTipo;

                // Emisor
                txtEmisorNIT.Text = AsStr(r["EMISOR_nit"]);
                txtEmisorNombre.Text = AsStr(r["EMISOR_nombre"]);
                TrySetCombo(cbxTipoEstablecimiento, r, "EMISOR_tipoEstablecimiento");
                txtEmisorTelefono.Text = AsStr(r["EMISOR_telefono"]);
                txtEmisorCorreo.Text = AsStr(r["EMISOR_correo"]);
                _idEmisor = TryInt(r, "ID_EMISOR");
                _emisor_codEstableMH = AsStr(r["EMISOR_codEstableMH"]);
                _emisor_codEstable = AsStr(r["EMISOR_codEstable"]);
                _emisor_codPuntoVentaMH = AsStr(r["EMISOR_codPuntoVentaMH"]);
                _emisor_codPuntoVenta = AsStr(r["EMISOR_codPuntoVenta"]);
                _emisor_nomEstablecimiento = AsStr(r["EMISOR_nomEstablecimiento"]);
                _doc_tipoDte = AsStr(r["DOCUMENTO_tipoDte"]);

                // Documento
                txtDocCodGeneracion.Text = AsStr(r["DOCUMENTO_codigoGeneracion"]);
                txtDocSelloRecibido.Text = AsStr(r["DOCUMENTO_selloRecibido"]);
                txtDocNumeroControl.Text = AsStr(r["DOCUMENTO_numeroControl"]);
                if (!string.IsNullOrEmpty(AsStr(r["DOCUMENTO_fecEmi"])))
                    dtpDocFechaEmision.Value = DateTime.Parse(AsStr(r["DOCUMENTO_fecEmi"]));
                TrySetCombo(cbxDocTipoDocumento, r, "DOCUMENTO_tipoDocumento");
                txtDocNumeroDocumento.Text = AsStr(r["DOCUMENTO_numDocumento"]);
                txtDocNombreReceptor.Text = AsStr(r["DOCUMENTO_nombre"]);
                txtDocTelefono.Text = AsStr(r["DOCUMENTO_telefono"]);
                txtDocCorreo.Text = AsStr(r["DOCUMENTO_correo"]);
                txtDocCorreoCC.Text = AsStr(r["DOCUMENTO_correoCC"]);

                // Motivo
                TrySetCombo(cbxTipoInvalidacion, r, "MOTIVO_tipoAnulacion");
                txtNombreResponsable.Text = AsStr(r["MOTIVO_nombreResponsable"]);
                TrySetCombo(cbxTipoDocResponsable, r, "MOTIVO_tipDocResponsable");
                txtNumDocResponsable.Text = AsStr(r["MOTIVO_numDocResponsable"]);
                txtNombreSolicita.Text = AsStr(r["MOTIVO_nombreSolicita"]);
                TrySetCombo(cbxTipoDocSolicita, r, "MOTIVO_tipDocSolicita");
                txtNumDocSolicita.Text = AsStr(r["MOTIVO_numDocSolicita"]);
                txtMotivoInterno.Text = AsStr(r["MOTIVO"]);

                // JSON guardado en tabla
                txtJson.Text = r.Table.Columns.Contains("jsonSerializado") && r["jsonSerializado"] != DBNull.Value
                               ? r["jsonSerializado"].ToString() : string.Empty;

                bool validado = !string.IsNullOrWhiteSpace(txtSelloAnulacion.Text);
                ConfigurarCRUD(validado ? EstadoFormulario.Validado : EstadoFormulario.Guardado);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Navegación
        private void btnAtras_Click(object sender, EventArgs e) => Close();
        private void btnSalir_Click(object sender, EventArgs e) => Application.Exit();
        #endregion

        #region Helpers
        private static string AsStr(object v) =>
            v == null || v == DBNull.Value ? string.Empty : v.ToString().Trim();
        private static string NullIfEmpty(object v)
        {
            string s = AsStr(v);
            return string.IsNullOrWhiteSpace(s) ? null : s;
        }
        private static int TryInt(DataRow r, string col) =>
            r.Table.Columns.Contains(col) && r[col] != DBNull.Value
                ? Convert.ToInt32(r[col]) : 0;
        private static int TryInt2(DataRow r, string col, int defVal) =>
            r.Table.Columns.Contains(col) && r[col] != DBNull.Value
                ? Convert.ToInt32(r[col]) : defVal;
        private static bool TryBool(DataRow r, string col) =>
            r.Table.Columns.Contains(col) && r[col] != DBNull.Value
                && Convert.ToBoolean(r[col]);
        private static void TrySetDate(DateTimePicker dtp, DataRow r, string col)
        {
            if (r.Table.Columns.Contains(col) && r[col] != DBNull.Value)
                dtp.Value = Convert.ToDateTime(r[col]);
        }
        private static void TrySetCombo(System.Windows.Forms.ComboBox cbx, DataRow r, string col)
        {
            if (r.Table.Columns.Contains(col) && r[col] != DBNull.Value)
                cbx.SelectedValue = r[col];
        }
        private static void Aviso(string msg, Control foco)
        {
            XtraMessageBox.Show(msg, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            foco?.Focus();
        }
        #endregion

        #region Clases JSON
        private class RespuestaHacienda
        {
            public string estado { get; set; }
            public string selloRecibido { get; set; }
            public string fhProcesamiento { get; set; }
        }
        #endregion
    }
}
