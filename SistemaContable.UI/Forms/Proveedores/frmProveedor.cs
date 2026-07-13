using DevExpress.Utils;
using DevExpress.XtraEditors;
using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Forms.Proveedores
{
    public partial class frmProveedor : Form
    {
        private readonly DALBase _dal = new DALBase();
        private bool _cargandoFormulario = true;
        private string _ultimoCodigoSugerido = "";

        // ============================================================
        // Constantes de catálogos (IDs fijos en BD)
        // ============================================================
        private const int ORIGEN_LOCAL = 1;
        private const int ORIGEN_EXTERIOR = 2;
        private const int TIPO_PERSONA_JURIDICA = 2;
        private const int TIPO_CONTRIB_NO_CONTRIBUY = 0;
        private const int PAIS_EL_SALVADOR = 61;
        public int IdEntidad { get; set; } = 0;

        public frmProveedor()
        {
            InitializeComponent();
        }

        private void frmProveedor_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);
            _cargandoFormulario = true;
            CargarCombos();
            SuscribirEventos();

            if (IdEntidad > 0)
                CargarEntidadParaEditar();
            else
                AplicarReglasOrigen();   // estado inicial según origen seleccionado

            FormHelper.RegistrarBusqueda(
                txtCUENTA_X_PAGAR,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_CATALOGO_CUENTA",
                    Columnas = new Dictionary<string, string>
                    {
                        { "CUENTA", "CODIGO" },
                        { "NOMBRE_CUENTA", "NOMBRE" }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "CUENTA",         100 },
                        { "NOMBRE_CUENTA",  300 }
                    },
                    ParametrosExtra = new { ES_DETALLE = true }
                },
                fila =>
                {
                    txtCUENTA_X_PAGAR.Text = fila["CUENTA"].ToString();
                    txtNOMBRE_CUENTA_X_PAGAR.Text = fila["NOMBRE_CUENTA"].ToString();
                }
            );

            FormHelper.RegistrarBusqueda(
                txtCUENTA_GASTO,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_CATALOGO_CUENTA",
                    Columnas = new Dictionary<string, string>
                    {
                        { "CUENTA", "CODIGO" },
                        { "NOMBRE_CUENTA", "NOMBRE" }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "CUENTA",         100 },
                        { "NOMBRE_CUENTA",  300 }
                    },
                    ParametrosExtra = new { ES_DETALLE = true }
                },
                fila =>
                {
                    txtCUENTA_GASTO.Text = fila["CUENTA"].ToString();
                    txtNOMBRE_CUENTA_GASTO.Text = fila["NOMBRE_CUENTA"].ToString();
                }
            );

            FormHelper.RegistrarBusqueda(
                txtCODI_ACTI1,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_ACTIVIDAD_ECONOMICA",
                    Columnas = new Dictionary<string, string>
                    {
                        { "CODI_MH", "CODIGO" },
                        { "VALORES", "NOMBRE" }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "CODI_MH",  80 },
                        { "VALORES",  400 }
                    }
                },
                fila =>
                {
                    txtCODI_ACTI1.Tag = fila["ID_ACTIVIDAD"].ToString();
                    txtCODI_ACTI1.Text = fila["CODI_MH"].ToString();
                    txtACTIVIDAD_ECONOMICA1.Text = fila["VALORES"].ToString();
                }
            );

            FormHelper.RegistrarBusqueda(
                txtCODI_ACTI2,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_ACTIVIDAD_ECONOMICA",
                    Columnas = new Dictionary<string, string>
                    {
                        { "CODI_MH", "CODIGO" },
                        { "VALORES", "NOMBRE" }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "CODI_MH",  80 },
                        { "VALORES",  400 }
                    }
                },
                fila =>
                {
                    txtCODI_ACTI2.Tag = fila["ID_ACTIVIDAD"].ToString();
                    txtCODI_ACTI2.Text = fila["CODI_MH"].ToString();
                    txtACTIVIDAD_ECONOMICA2.Text = fila["VALORES"].ToString();
                }
            );

            FormHelper.RegistrarBusqueda(
                txtCODI_ACTI3,
                new BusquedaConfig
                {
                    StoredProcedure = "SP_ACTIVIDAD_ECONOMICA",
                    Columnas = new Dictionary<string, string>
                    {
                        { "CODI_MH", "CODIGO" },
                        { "VALORES", "NOMBRE" }
                    },
                    Anchos = new Dictionary<string, int>
                    {
                        { "CODI_MH",  80 },
                        { "VALORES",  400 }
                    }
                },
                fila =>
                {
                    txtCODI_ACTI3.Tag = fila["ID_ACTIVIDAD"].ToString();
                    txtCODI_ACTI3.Text = fila["CODI_MH"].ToString();
                    txtACTIVIDAD_ECONOMICA3.Text = fila["VALORES"].ToString();
                }
            );

            FormHelper.ResaltarCombosEnFoco(this);
            _cargandoFormulario = false;
        }

        private void CargarCombos()
        {
            CargarCombo("SP_ORIGEN_ENTIDAD", "BUSCAR", "ID_ORIGEN", "NOMBRE_ORIGEN", cbxORIGEN_ENTIDAD);
            CargarCombo("SP_TIPO_PERSONA", "BUSCAR", "ID_TIPO_PERSONA", "NOMBRE", cbxTIPO_PERSONA);
            CargarCombo("SP_TIPO_CONTRIBUYENTE", "BUSCAR", "ID_TIPO_CONTRIB", "NOMBRE", cbxTIPO_CONTRIBUYENTE);
            CargarCombo("SP_TIPO_DOCUMENTO_IDENTIDAD", "BUSCAR", "ID_TIPO_DOC_INDEN", "NOMBRE", cbxTIPO_DOCUMENTO_IDENTIDAD);
            CargarCombo("SP_PAIS", "BUSCAR", "ID_PAIS", "VALORES", cbxPAIS);
            CargarCombo("SP_DEPARTAMENTO", "BUSCAR", "CODI_DEPTO", "VALORES", cbxDEPARTAMENTO);

            // Municipio y Distrito quedan vacíos hasta que se elija depto/muni
            cbxMUNICIPIO.DataSource = null;
            cbxDISTRITO.DataSource = null;
        }

        private void CargarCombo(string sp, string accion, string valueMember,
           string displayMember, System.Windows.Forms.ComboBox combo)
        {
            try
            {
                var dt = _dal.EjecutarConsulta(sp, new { ACCION = accion });
                // ✅ Relajar restricciones para permitir la fila "-- Seleccione --"
                foreach (DataColumn col in dt.Columns)
                    col.AllowDBNull = true;
                var filaVacia = dt.NewRow();
                filaVacia[valueMember] = -1;
                filaVacia[displayMember] = "-- Seleccione --";
                dt.Rows.InsertAt(filaVacia, 0);

                combo.DataSource = dt;
                combo.ValueMember = valueMember;
                combo.DisplayMember = displayMember;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error cargando combo {sp}: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // Suscripción de eventos (cascading y reglas)
        // ============================================================
        private void SuscribirEventos()
        {
            cbxORIGEN_ENTIDAD.SelectedIndexChanged += (s, e) =>
            {
                if (_cargandoFormulario) return;
                AplicarReglasOrigen();
                SugerirCodigo();
            };

            cbxTIPO_PERSONA.SelectedIndexChanged += (s, e) =>
            {
                if (_cargandoFormulario) return;
                AplicarReglasTipoPersona();
            };

            cbxTIPO_CONTRIBUYENTE.SelectedIndexChanged += (s, e) =>
            {
                if (_cargandoFormulario) return;
                AplicarReglasTipoContribuyente();
                SugerirCodigo();
            };

            cbxDEPARTAMENTO.SelectedIndexChanged += (s, e) =>
            {
                if (_cargandoFormulario) return;
                CargarMunicipiosDelDepto(null);
                cbxDISTRITO.DataSource = null;
            };

            cbxMUNICIPIO.SelectedIndexChanged += (s, e) =>
            {
                if (_cargandoFormulario) return;
                CargarDistritoDelMunicipio();
            };
            // Búsqueda de actividad al perder foco
            txtCODI_ACTI1.Leave += (s, e) => BuscarActividadPorCodigo(txtCODI_ACTI1, txtACTIVIDAD_ECONOMICA1);
            txtCODI_ACTI2.Leave += (s, e) => BuscarActividadPorCodigo(txtCODI_ACTI2, txtACTIVIDAD_ECONOMICA2);
            txtCODI_ACTI3.Leave += (s, e) => BuscarActividadPorCodigo(txtCODI_ACTI3, txtACTIVIDAD_ECONOMICA3);

            txtCELULAR.KeyPress += SoloNumeros_KeyPress;
            txtTELEFONO.KeyPress += SoloNumeros_KeyPress;
            txtNIT.KeyPress += SoloNumeros_KeyPress;
            txtNIT.TextChanged += SoloNumeros_TextChanged;

            txtDUI.Leave += (s, e) => ValidarDuplicado(txtDUI, "DUI");
            txtNIT.Leave += (s, e) => ValidarDuplicado(txtNIT, "NIT");
            txtNRC.Leave += (s, e) => ValidarDuplicado(txtNRC, "NRC");
            txtDOCUMENTO.Leave += (s, e) => ValidarDuplicado(txtDOCUMENTO, "DOCUMENTO");
            txtDUI.Leave += (s, e) => SugerirCodigo();
            txtNIT.Leave += (s, e) => SugerirCodigo();   // por si afecta lógica futura
            txtNRC.Leave += (s, e) => SugerirCodigo();
            txtDOCUMENTO.Leave += (s, e) => SugerirCodigo();

            txtCORREO.Leave += (s, e) => ValidarCorreoLeave(txtCORREO);
            txtCORREO_CC.Leave += (s, e) => ValidarCorreoLeave(txtCORREO_CC);

            btnGuardar.Click += btnGuardar_Click;
            btnFinalizar.Click += btnFinalizar_Click;

            txtCUENTA_X_PAGAR.Leave += (s, e) => BuscarCuentaContablePorCodigo(txtCUENTA_X_PAGAR, txtNOMBRE_CUENTA_X_PAGAR);
            txtCUENTA_GASTO.Leave += (s, e) => BuscarCuentaContablePorCodigo(txtCUENTA_GASTO, txtNOMBRE_CUENTA_GASTO);
        }



        // ============================================================
        // Cascading: cargar municipios del depto seleccionado
        // ============================================================
        private void CargarMunicipiosDelDepto(string codiMuniSeleccionado)
        {
            string codiDepto = ComboHelper.ObtenerString(cbxDEPARTAMENTO);
            if (string.IsNullOrEmpty(codiDepto))
            {
                cbxMUNICIPIO.DataSource = null;
                return;
            }

            try
            {
                var dt = _dal.EjecutarConsulta("SP_MUNICIPIO", new
                {
                    ACCION = "BUSCAR",
                    CODI_DEPTO = codiDepto
                });
                foreach (DataColumn col in dt.Columns)
                    col.AllowDBNull = true;
                var filaVacia = dt.NewRow();
                filaVacia["CODI_DEPTO"] = DBNull.Value;
                filaVacia["CODI_MUNI"] = DBNull.Value;
                filaVacia["NOMBRE_MUNICIPIO"] = "-- Seleccione --";
                dt.Rows.InsertAt(filaVacia, 0);

                cbxMUNICIPIO.DataSource = dt;
                cbxMUNICIPIO.ValueMember = "CODI_MUNI";
                cbxMUNICIPIO.DisplayMember = "NOMBRE_MUNICIPIO";

                if (!string.IsNullOrEmpty(codiMuniSeleccionado))
                    cbxMUNICIPIO.SelectedValue = codiMuniSeleccionado;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error cargando municipios: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // Cascading: cargar el distrito del municipio elegido
        // (siempre un único item según estructura de MUNICIPIO)
        // ============================================================
        private void CargarDistritoDelMunicipio()
        {
            string codiDepto = ComboHelper.ObtenerString(cbxDEPARTAMENTO);
            string codiMuni = ComboHelper.ObtenerString(cbxMUNICIPIO);

            if (string.IsNullOrEmpty(codiDepto) || string.IsNullOrEmpty(codiMuni))
            {
                cbxDISTRITO.DataSource = null;
                return;
            }

            try
            {
                // Reusar BUSCAR con CODI_DEPTO y filtrar por CODI_MUNI
                var dt = _dal.EjecutarConsulta("SP_MUNICIPIO", new
                {
                    ACCION = "BUSCAR",
                    CODI_DEPTO = codiDepto
                });

                // Filtrar el municipio específico para obtener su distrito
                var filtradas = dt.Select($"CODI_MUNI = '{codiMuni}'");
                if (filtradas.Length == 0)
                {
                    cbxDISTRITO.DataSource = null;
                    return;
                }

                var dtDistrito = filtradas.CopyToDataTable();

                cbxDISTRITO.DataSource = dtDistrito;
                cbxDISTRITO.ValueMember = "CODI_MUNI";
                cbxDISTRITO.DisplayMember = "NOMBRE_DISTRITO";

                if (dtDistrito.Rows.Count > 0)
                    cbxDISTRITO.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error cargando distrito: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // Reglas según ORIGEN (Local / Exterior)
        // ============================================================
        private void AplicarReglasOrigen()
        {
            int? idOrigen = ComboHelper.ObtenerInt(cbxORIGEN_ENTIDAD);
            bool esExterior = idOrigen == ORIGEN_EXTERIOR;

            // Documento extranjero (solo Exterior)
            cbxTIPO_DOCUMENTO_IDENTIDAD.Enabled = esExterior;
            txtDOCUMENTO.Enabled = esExterior;

            // Documentos locales (solo Local)
            txtDUI.Enabled = !esExterior;
            txtNIT.Enabled = !esExterior;
            txtNRC.Enabled = !esExterior;

            // Tipo de contribuyente (solo Local)
            cbxTIPO_CONTRIBUYENTE.Enabled = !esExterior;

            // Dirección detallada local (solo Local)
            txtCOMPLEMENTO.Enabled = esExterior;
            txtCALLE.Enabled = !esExterior;
            txtCASA.Enabled = !esExterior;
            txtAPTO_LOCAL.Enabled = !esExterior;
            txtCOLONIA.Enabled = !esExterior;
            txtOTROS_DATOS.Enabled = !esExterior;

            // Códigos auxiliares (solo Local)
            txtCODIPROVEEDOR.Enabled = !esExterior;
            txtCODTRANSPORT.Enabled = !esExterior;
            txtID_CARGADORA.Enabled = !esExterior;
            cbxPAIS.Enabled = esExterior;
            cbxDEPARTAMENTO.Enabled = !esExterior;
            cbxMUNICIPIO.Enabled = !esExterior;
            cbxDISTRITO.Enabled = !esExterior;

            // Actividades económicas (solo Local)           
            txtACTIVIDAD_ECONOMICA1.Enabled = !esExterior;
            txtACTIVIDAD_ECONOMICA2.Enabled = !esExterior;
            txtACTIVIDAD_ECONOMICA3.Enabled = !esExterior;

            // Limpiar valores deshabilitados
            if (esExterior)
            {
                // ============ EXTERIOR ============
                txtDUI.Clear();
                txtNIT.Clear();
                txtNRC.Clear();
                cbxTIPO_CONTRIBUYENTE.SelectedValue = TIPO_CONTRIB_NO_CONTRIBUY;

                txtCALLE.Clear();
                txtCASA.Clear();
                txtAPTO_LOCAL.Clear();
                txtCOLONIA.Clear();
                txtCOMPLEMENTO.Clear();
                txtOTROS_DATOS.Clear();

                txtCODIPROVEEDOR.Clear();
                txtCODTRANSPORT.Clear();
                txtID_CARGADORA.Clear();

                cbxPAIS.SelectedIndex = 0;

                // Regla 3: forzar Depto/Muni/Distrito = "00" (EXTERIOR)
                cbxPAIS.SelectedIndex = 0;   // que elija el país manualmente

                try
                {
                    cbxDEPARTAMENTO.SelectedValue = "00";
                    // Al cambiar el departamento se dispara su evento, pero como
                    // _cargandoFormulario controla el cascading lo manejamos manual:
                    CargarMunicipiosDelDepto("00");
                    CargarDistritoDelMunicipio();
                }
                catch
                {
                    // Si no existe el registro "00" en DEPARTAMENTO/MUNICIPIO, silenciar
                }
            }
            else
            {
                // ============ LOCAL ============
                cbxTIPO_DOCUMENTO_IDENTIDAD.SelectedIndex = 0;
                txtDOCUMENTO.Clear();

                // País = El Salvador y bloqueado
                cbxPAIS.SelectedValue = PAIS_EL_SALVADOR;

                // Si veníamos de Exterior con "00", limpiar para que el usuario elija
                if (ComboHelper.ObtenerString(cbxDEPARTAMENTO) == "00")
                {
                    cbxDEPARTAMENTO.SelectedIndex = 0;
                    cbxMUNICIPIO.DataSource = null;
                    cbxDISTRITO.DataSource = null;
                }
            }

            // Reaplicar regla de tipo persona (puede haberse deshabilitado DUI)
            AplicarReglasTipoPersona();
            AplicarReglasTipoContribuyente();
        }

        private void AplicarReglasTipoContribuyente()
        {
            int? idOrigen = ComboHelper.ObtenerInt(cbxORIGEN_ENTIDAD);
            int? idTipoContrib = ComboHelper.ObtenerInt(cbxTIPO_CONTRIBUYENTE);

            bool esExterior = idOrigen == ORIGEN_EXTERIOR;
            bool esNoContribuyente = idTipoContrib == TIPO_CONTRIB_NO_CONTRIBUY;

            // NRC: habilitado solo si NO es Exterior Y NO es No Contribuyente
            txtNRC.Enabled = !esExterior && !esNoContribuyente;
            if (!txtNRC.Enabled)
                txtNRC.Clear();

            // Actividades económicas: deshabilitar si es Exterior O No Contribuyente
            bool habilitarActividades = !esExterior && !esNoContribuyente;

            txtCODI_ACTI1.Enabled = habilitarActividades;
            txtCODI_ACTI2.Enabled = habilitarActividades;
            txtCODI_ACTI3.Enabled = habilitarActividades;

            txtACTIVIDAD_ECONOMICA1.Enabled = habilitarActividades;
            txtACTIVIDAD_ECONOMICA2.Enabled = habilitarActividades;
            txtACTIVIDAD_ECONOMICA3.Enabled = habilitarActividades;

            // Si se deshabilitan, limpiar valores y Tags
            if (!habilitarActividades)
            {
                txtCODI_ACTI1.Clear();
                txtCODI_ACTI1.Tag = null;
                txtACTIVIDAD_ECONOMICA1.Clear();

                txtCODI_ACTI2.Clear();
                txtCODI_ACTI2.Tag = null;
                txtACTIVIDAD_ECONOMICA2.Clear();

                txtCODI_ACTI3.Clear();
                txtCODI_ACTI3.Tag = null;
                txtACTIVIDAD_ECONOMICA3.Clear();
            }
        }


        // ============================================================
        // Reglas según TIPO PERSONA (Jurídica deshabilita DUI)
        // ============================================================
        private void AplicarReglasTipoPersona()
        {
            int? idOrigen = ComboHelper.ObtenerInt(cbxORIGEN_ENTIDAD);
            int? idTipoPersona = ComboHelper.ObtenerInt(cbxTIPO_PERSONA);

            bool esExterior = idOrigen == ORIGEN_EXTERIOR;
            bool esJuridica = idTipoPersona == TIPO_PERSONA_JURIDICA;

            // DUI: solo si NO es Exterior y NO es Jurídica
            txtDUI.Enabled = !esExterior && !esJuridica;

            if (!txtDUI.Enabled)
                txtDUI.Clear();
        }


        // ============================================================
        // Sugiere el CODIGO_ENTIDAD según las reglas:
        //   * Local + Contribuyente   → NRC
        //   * Local + No Contribuyente → DUI
        //   * Exterior                → DOCUMENTO
        // Solo sugiere si el código está vacío o coincide con el
        // sugerido anterior (para no pisar valores manuales del usuario)
        // ============================================================
        private void SugerirCodigo()
        {
            if (IdEntidad > 0) return;   // no sugerir en edición

            int? idOrigen = ComboHelper.ObtenerInt(cbxORIGEN_ENTIDAD);
            int? idTipoContrib = ComboHelper.ObtenerInt(cbxTIPO_CONTRIBUYENTE);

            bool esExterior = idOrigen == ORIGEN_EXTERIOR;
            bool esNoContribuyente = idTipoContrib == TIPO_CONTRIB_NO_CONTRIBUY;

            string sugerencia;

            if (esExterior)
                sugerencia = txtDOCUMENTO.Text.Trim();
            else if (esNoContribuyente)
                sugerencia = txtDUI.Text.Trim();
            else
                sugerencia = txtNRC.Text.Trim();

            // No pisar si el usuario digitó algo manualmente distinto
            string codigoActual = txtCODIGO_ENTIDAD.Text.Trim();
            bool codigoVacio = string.IsNullOrEmpty(codigoActual);
            bool esSugerencia = codigoActual == _ultimoCodigoSugerido;

            if (codigoVacio || esSugerencia)
            {
                txtCODIGO_ENTIDAD.Text = sugerencia;
                _ultimoCodigoSugerido = sugerencia;
            }
        }

        // ============================================================
        // Buscar actividad económica por código (CODI_MH) y setear
        // descripción + ID_ACTIVIDAD en el Tag del campo código.
        // Se usa al perder foco / Enter en txtCODI_ACTI1/2/3.
        // ============================================================
        private void BuscarActividadPorCodigo(TextBox txtCodigo, TextBox txtDescripcion)
        {
            string codigo = txtCodigo.Text.Trim();

            // Si está vacío, limpiar descripción y Tag
            if (string.IsNullOrEmpty(codigo))
            {
                txtCodigo.Tag = null;
                txtDescripcion.Clear();
                return;
            }

            try
            {
                var dt = _dal.EjecutarConsulta("SP_ACTIVIDAD_ECONOMICA", new
                {
                    ACCION = "BUSCAR",
                    FILTRO = codigo,
                    CODI_MH = codigo   // por si el SP filtra por esta col específica
                });

                // Buscar coincidencia EXACTA por CODI_MH
                DataRow fila = null;
                foreach (DataRow r in dt.Rows)
                {
                    if (string.Equals(
                            SafeStr(r["CODI_MH"]).Trim(),
                            codigo,
                            StringComparison.OrdinalIgnoreCase))
                    {
                        fila = r;
                        break;
                    }
                }

                if (fila == null)
                {
                    MostrarValidacion($"No se encontró la actividad económica con código '{codigo}'.");
                    txtCodigo.Tag = null;
                    txtCodigo.Clear();
                    txtDescripcion.Clear();
                    txtCodigo.Focus();
                    return;
                }

                // Asignar valores
                txtCodigo.Tag = fila["ID_ACTIVIDAD"].ToString();
                txtCodigo.Text = fila["CODI_MH"].ToString();
                txtDescripcion.Text = fila["VALORES"].ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error buscando actividad: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // Modo edición: cargar datos de la entidad
        // ============================================================
        private void CargarEntidadParaEditar()
        {
            try
            {
                // OBTENER devuelve DataSet: encabezado + roles
                var ds = _dal.EjecutarMultiple("SP_ENTIDAD", new
                {
                    ACCION = "OBTENER",
                    ID_ENTIDAD = IdEntidad
                });

                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    XtraMessageBox.Show($"No se encontró la entidad con ID {IdEntidad}.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var r = ds.Tables[0].Rows[0];

                // Datos generales
                txtCODIGO_ENTIDAD.Text = SafeStr(r["CODIGO_ENTIDAD"]);
                cbxORIGEN_ENTIDAD.SelectedValue = r["ID_ORIGEN"] ?? DBNull.Value;
                cbxTIPO_PERSONA.SelectedValue = r["ID_TIPO_ENTIDAD"] ?? DBNull.Value;
                cbxTIPO_CONTRIBUYENTE.SelectedValue = r["ID_TIPO_CONTRIB"] ?? DBNull.Value;
                cbxTIPO_DOCUMENTO_IDENTIDAD.SelectedValue = r["ID_TIPO_DOC_INDEN"] ?? DBNull.Value;
                txtDOCUMENTO.Text = SafeStr(r["DOCUMENTO"]);
                txtDUI.Text = SafeStr(r["DUI"]);
                txtNIT.Text = SafeStr(r["NIT"]);
                txtNRC.Text = SafeStr(r["NRC"]);
                txtNOMBRE.Text = SafeStr(r["NOMBRE"]);
                txtCOMPLEMENTO.Text = SafeStr(r["COMPLEMENTO"]);

                // Dirección
                txtCALLE.Text = SafeStr(r["CALLE"]);
                txtCASA.Text = SafeStr(r["CASA"]);
                txtAPTO_LOCAL.Text = SafeStr(r["APTO_LOCAL"]);
                txtOTROS_DATOS.Text = "";   // si reusás complemento
                txtCOLONIA.Text = SafeStr(r["COLONIA"]);

                // Contacto
                txtCORREO.Text = SafeStr(r["CORREO"]);
                txtCORREO_CC.Text = SafeStr(r["CORREO_CC"]);
                txtCELULAR.Text = SafeStr(r["CELULAR"]);
                txtTELEFONO.Text = SafeStr(r["TELEFONO"]);

                // País y ubicación
                cbxPAIS.SelectedValue = r["ID_PAIS"] ?? DBNull.Value;

                string codiDepto = SafeStr(r["CODI_DEPTO"]);
                string codiMuni = SafeStr(r["CODI_MUNI"]);

                if (!string.IsNullOrEmpty(codiDepto))
                {
                    cbxDEPARTAMENTO.SelectedValue = codiDepto;
                    CargarMunicipiosDelDepto(codiMuni);
                    CargarDistritoDelMunicipio();
                }

                // Datos comerciales
                txtENCARGADO.Text = SafeStr(r["ENCARGADO"]);
                txtDIAS_PLAZO.Text = SafeStr(r["DIAS_PLAZO"]);
                chkRETENER_RENTA.Checked = r["RETENER_RENTA"] != DBNull.Value
                                          && Convert.ToBoolean(r["RETENER_RENTA"]);
                txtPORC_RENTA.Text = SafeStr(r["PORC_RENTA"]);
                txtCUENTA_X_PAGAR.Text = SafeStr(r["CUENTA_X_PAGAR"]);
                txtNOMBRE_CUENTA_X_PAGAR.Text = SafeStr(r["NOMBRE_CUENTA_X_PAGAR"]);
                txtCUENTA_GASTO.Text = SafeStr(r["CUENTA_GASTO"]);
                txtNOMBRE_CUENTA_GASTO.Text = SafeStr(r["NOMBRE_CUENTA_GASTO"]);

                // Códigos auxiliares
                txtCODIPROVEEDOR.Text = SafeStr(r["CODIPROVEEDOR"]);
                txtCODTRANSPORT.Text = SafeStr(r["CODTRANSPORT"]);
                txtID_CARGADORA.Text = SafeStr(r["ID_CARGADORA"]);
                txtOTROS_DATOS.Text = SafeStr(r["OTROS_DATOS"]);

                // Actividades económicas
                CargarActividadEditar(r, "ID_ACTIVIDAD_1", txtCODI_ACTI1, txtACTIVIDAD_ECONOMICA1);
                CargarActividadEditar(r, "ID_ACTIVIDAD_2", txtCODI_ACTI2, txtACTIVIDAD_ECONOMICA2);
                CargarActividadEditar(r, "ID_ACTIVIDAD_3", txtCODI_ACTI3, txtACTIVIDAD_ECONOMICA3);

                // Aplicar reglas según los valores cargados
                AplicarReglasOrigen();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error cargando entidad: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // Validaciones antes de guardar
        // ============================================================
        private bool ValidarCampos()
        {
            int? idOrigen = ComboHelper.ObtenerInt(cbxORIGEN_ENTIDAD);
            bool esExterior = idOrigen == ORIGEN_EXTERIOR;

            if (string.IsNullOrWhiteSpace(txtCODIGO_ENTIDAD.Text))
            {
                MostrarValidacion("Debe ingresar el código de la entidad.");
                txtCODIGO_ENTIDAD.Focus();
                return false;
            }

            if (idOrigen == null)
            {
                MostrarValidacion("Debe seleccionar el origen.");
                cbxORIGEN_ENTIDAD.Focus();
                return false;
            }

            if (ComboHelper.ObtenerInt(cbxTIPO_PERSONA) == null)
            {
                MostrarValidacion("Debe seleccionar el tipo de persona.");
                cbxTIPO_PERSONA.Focus();
                return false;
            }

            if (!esExterior && ComboHelper.ObtenerInt(cbxTIPO_CONTRIBUYENTE) == null)
            {
                MostrarValidacion("Debe seleccionar el tipo de contribuyente.");
                cbxTIPO_CONTRIBUYENTE.Focus();
                return false;
            }

            // ============================================================
            // Validación de documentos según Origen / Tipo Persona / Contribuyente
            // ============================================================
            if (esExterior)
            {
                // EXTERIOR: exigir tipo de documento y número
                if (ComboHelper.ObtenerInt(cbxTIPO_DOCUMENTO_IDENTIDAD) == null)
                {
                    MostrarValidacion("Debe seleccionar el tipo de documento.");
                    cbxTIPO_DOCUMENTO_IDENTIDAD.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtDOCUMENTO.Text))
                {
                    MostrarValidacion("Debe ingresar el número de documento.");
                    txtDOCUMENTO.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtCOMPLEMENTO.Text))
                {
                    MostrarValidacion("Debe ingresar la dirección.");
                    txtCOMPLEMENTO.Focus();
                    return false;
                }
            }
            else
            {
                // LOCAL
                int? idTipoPersona = ComboHelper.ObtenerInt(cbxTIPO_PERSONA);
                int? idTipoContrib = ComboHelper.ObtenerInt(cbxTIPO_CONTRIBUYENTE);

                bool esJuridica = idTipoPersona == TIPO_PERSONA_JURIDICA;
                bool esNoContribuyente = idTipoContrib == TIPO_CONTRIB_NO_CONTRIBUY;

                // DUI: requerido si NO es Jurídica (las jurídicas no tienen DUI)
                if (!esJuridica && string.IsNullOrWhiteSpace(txtDUI.Text))
                {
                    MostrarValidacion("Debe ingresar el DUI.");
                    txtDUI.Focus();
                    return false;
                }

                // NIT: siempre requerido en Local
                if (string.IsNullOrWhiteSpace(txtNIT.Text))
                {
                    MostrarValidacion("Debe ingresar el NIT.");
                    txtNIT.Focus();
                    return false;
                }

                // NRC: requerido si NO es No Contribuyente
                if (!esNoContribuyente && string.IsNullOrWhiteSpace(txtNRC.Text))
                {
                    MostrarValidacion("Debe ingresar el NRC.");
                    txtNRC.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtCALLE.Text))
                {
                    MostrarValidacion("Debe ingresar calle.");
                    txtCALLE.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtCASA.Text))
                {
                    MostrarValidacion("Debe ingresar número de casa.");
                    txtCASA.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtCOLONIA.Text))
                {
                    MostrarValidacion("Debe ingresar Colonia/Barrio.");
                    txtCOLONIA.Focus();
                    return false;
                }

                if (ComboHelper.ObtenerString(cbxDEPARTAMENTO) == null)
                {
                    MostrarValidacion("Debe seleccionar el departamento.");
                    cbxDEPARTAMENTO.Focus();
                    return false;
                }

                if (ComboHelper.ObtenerString(cbxMUNICIPIO) == null)
                {
                    MostrarValidacion("Debe seleccionar el municipio.");
                    cbxMUNICIPIO.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtTELEFONO.Text))
                {
                    MostrarValidacion("Debe ingresar el telefono.");
                    txtTELEFONO.Focus();
                    return false;
                }

            }

            if (string.IsNullOrWhiteSpace(txtNOMBRE.Text))
            {
                MostrarValidacion("Debe ingresar el nombre.");
                txtNOMBRE.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCORREO.Text))
            {
                MostrarValidacion("Debe ingresar el correo.");
                txtCORREO.Focus();
                return false;
            }

            if (!FormHelper.EsCorreoValido(txtCORREO.Text))
            {
                MostrarValidacion("Debe ingresar un correo valido.");
                txtCORREO.Focus();
                return false;
            }

            if (!FormHelper.EsCorreoValido(txtCORREO_CC.Text))
            {
                MostrarValidacion("Debe ingresar un correo cc valido.");
                txtCORREO_CC.Focus();
                return false;
            }

            // Validación de actividades no repetidas (solo Local)
            // Validación de actividades no repetidas (solo Local + Contribuyente)
            // Validación de actividades económicas (solo Local + Contribuyente)
            if (!esExterior && txtCODI_ACTI1.Enabled)
            {
                string id1 = SafeStr(txtCODI_ACTI1.Tag);
                string id2 = SafeStr(txtCODI_ACTI2.Tag);
                string id3 = SafeStr(txtCODI_ACTI3.Tag);

                bool tiene1 = !string.IsNullOrEmpty(id1);
                bool tiene2 = !string.IsNullOrEmpty(id2);
                bool tiene3 = !string.IsNullOrEmpty(id3);

                // Actividad primaria es obligatoria si el campo está habilitado
                if (!tiene1)
                {
                    MostrarValidacion("Debe ingresar al menos la actividad económica primaria.");
                    txtCODI_ACTI1.Focus();
                    return false;
                }

                // No repetir actividades (solo comparar pares donde ambos tienen valor)  
                bool repetida =
                    (tiene1 && tiene2 && id1 == id2) ||
                    (tiene1 && tiene3 && id1 == id3) ||
                    (tiene2 && tiene3 && id2 == id3);

                if (repetida)
                {
                    MostrarValidacion("Las actividades económicas no deben repetirse.");
                    return false;
                }
            }
            return true;
        }

        // ============================================================
        // Valida si ya existe otra entidad con el mismo DUI/NIT/NRC
        // y que tenga el ROL = "PRO" (PROVEEDOR)
        // Solo aplica en modo nuevo (IdEntidad == 0)
        // ============================================================
        private void ValidarDuplicado(TextBox txt, string nombreCampo)
        {
            // Solo validar en alta nueva
            if (IdEntidad > 0) return;

            string valor = txt.Text.Trim();
            if (string.IsNullOrEmpty(valor)) return;

            try
            {
                var dt = _dal.EjecutarConsulta("SP_ENTIDAD", new
                {
                    ACCION = "BUSCAR_DUPLICADOS",
                    ROL = "PRO",
                    FILTRO = valor
                });

                // Buscar coincidencia exacta en la columna correspondiente
                DataRow encontrada = null;
                foreach (DataRow r in dt.Rows)
                {
                    if (string.Equals(SafeStr(r[nombreCampo]).Trim(), valor,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        encontrada = r;
                        break;
                    }
                }

                if (encontrada != null)
                {
                    string nombreExistente = SafeStr(encontrada["NOMBRE"]);
                    MostrarValidacion(
                        $"El {nombreCampo} <b>{valor}</b> ya está registrado para:<br/>" +
                        $"<b>{nombreExistente}</b>");

                    txt.Clear();
                    txt.Focus();
                }
            }
            catch (Exception ex)
            {
                // Silenciar errores de red/BD, no bloquear el flujo del usuario
                System.Diagnostics.Debug.WriteLine($"Error validando duplicado: {ex.Message}");
            }
        }

        private void MostrarValidacion(string mensaje)
        {
            MostrarValidacionHtml($"<b>{mensaje}</b>");
        }
        private void MostrarValidacionHtml(string mensaje)
        {
            var args = new XtraMessageBoxArgs
            {
                Caption = "Validación",
                Text = mensaje,    // ya viene con HTML armado
                Buttons = new[] { DialogResult.OK },
                Icon = SystemIcons.Warning,
                AllowHtmlText = DefaultBoolean.True
            };
            XtraMessageBox.Show(args);
        }

        // ============================================================
        // Carga una actividad en sus campos (código + descripción)
        // usando el ID que viene del registro de la entidad
        // ============================================================
        private void CargarActividadEditar(DataRow r, string columnaId,
            TextBox txtCodigo, TextBox txtDescripcion)
        {
            if (r[columnaId] == DBNull.Value)
            {
                txtCodigo.Tag = null;
                txtCodigo.Clear();
                txtDescripcion.Clear();
                return;
            }

            int idActividad = Convert.ToInt32(r[columnaId]);

            try
            {
                // Buscar la actividad por su ID
                var dt = _dal.EjecutarConsulta("SP_ACTIVIDAD_ECONOMICA", new
                {
                    ACCION = "OBTENER",
                    ID_ACTIVIDAD = idActividad
                });

                if (dt.Rows.Count == 0) return;

                var fila = dt.Rows[0];
                txtCodigo.Tag = idActividad.ToString();
                txtCodigo.Text = SafeStr(fila["CODI_MH"]);
                txtDescripcion.Text = SafeStr(fila["VALORES"]);
            }
            catch
            {
                // Silenciar
            }
        }

        // ============================================================
        // Guardar
        // ============================================================
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                Cursor = Cursors.WaitCursor;

                var parametros = new
                {
                    ACCION = "GUARDAR",
                    ID_ENTIDAD = IdEntidad,
                    CODIGO_ENTIDAD = NullIfEmpty(txtCODIGO_ENTIDAD.Text),
                    ID_TIPO_ENTIDAD = ComboHelper.ObtenerInt(cbxTIPO_PERSONA),
                    ID_TIPO_CONTRIB = ComboHelper.ObtenerInt(cbxTIPO_CONTRIBUYENTE),
                    ID_TIPO_DOC_INDEN = ComboHelper.ObtenerInt(cbxTIPO_DOCUMENTO_IDENTIDAD),
                    DOCUMENTO = NullIfEmpty(txtDOCUMENTO.Text),
                    NRC = NullIfEmpty(txtNRC.Text),
                    DUI = NullIfEmpty(txtDUI.Text),
                    NIT = NullIfEmpty(txtNIT.Text),
                    NOMBRE = NullIfEmpty(txtNOMBRE.Text),
                    NOMBRE_COMERCIAL = (string)null,    // no hay textbox en pantalla
                    COMPLEMENTO = NullIfEmpty(txtCOMPLEMENTO.Text),
                    CALLE = NullIfEmpty(txtCALLE.Text),
                    CASA = NullIfEmpty(txtCASA.Text),
                    APTO_LOCAL = NullIfEmpty(txtAPTO_LOCAL.Text),
                    COLONIA = NullIfEmpty(txtCOLONIA.Text),
                    CORREO = NullIfEmpty(txtCORREO.Text),
                    CORREO_CC = NullIfEmpty(txtCORREO_CC.Text),
                    CELULAR = NullIfEmpty(txtCELULAR.Text),
                    TELEFONO = NullIfEmpty(txtTELEFONO.Text),
                    ENCARGADO = NullIfEmpty(txtENCARGADO.Text),
                    DIAS_PLAZO = ParseIntNullable(txtDIAS_PLAZO.Text),
                    ID_PAIS = ComboHelper.ObtenerInt(cbxPAIS),
                    CODI_DEPTO = ComboHelper.ObtenerString(cbxDEPARTAMENTO),
                    CODI_MUNI = ComboHelper.ObtenerString(cbxMUNICIPIO),
                    ID_ACTIVIDAD_1 = ParseIntNullable(SafeStr(txtCODI_ACTI1.Tag)),
                    ID_ACTIVIDAD_2 = ParseIntNullable(SafeStr(txtCODI_ACTI2.Tag)),
                    ID_ACTIVIDAD_3 = ParseIntNullable(SafeStr(txtCODI_ACTI3.Tag)),
                    CUENTA_X_PAGAR = NullIfEmpty(txtCUENTA_X_PAGAR.Text),
                    CUENTA_GASTO = NullIfEmpty(txtCUENTA_GASTO.Text),
                    ID_ORIGEN = ComboHelper.ObtenerInt(cbxORIGEN_ENTIDAD),
                    CODIPROVEEDOR = NullIfEmpty(txtCODIPROVEEDOR.Text),
                    CODTRANSPORT = ParseIntNullable(txtCODTRANSPORT.Text),
                    ID_CARGADORA = ParseIntNullable(txtID_CARGADORA.Text),
                    RETENER_RENTA = chkRETENER_RENTA.Checked,
                    PORC_RENTA = ParseDecimalNullable(txtPORC_RENTA.Text),
                    USUARIO_CREA = Configuracion.UsuarioActual,
                    USUARIO_ACT = Configuracion.UsuarioActual,
                    OTROS_DATOS = NullIfEmpty(txtOTROS_DATOS.Text),
                    ROL = "PRO"
                };

                int idGenerado = _dal.EjecutarEscalar("SP_ENTIDAD", parametros);

                if (idGenerado == 0)
                    throw new Exception("El SP no devolvió el ID generado.");

                IdEntidad = idGenerado;

                var args = new XtraMessageBoxArgs
                {
                    Caption = "Guardado",
                    Text = $"Proveedor <b>{txtCODIGO_ENTIDAD.Text.Trim()}  {txtNOMBRE.Text.Trim()}</b> guardado correctamente.",
                    Buttons = new[] { DialogResult.OK },
                    Icon = SystemIcons.Information,
                    AllowHtmlText = DefaultBoolean.True
                };
                XtraMessageBox.Show(args);
            }
            catch (Exception ex)
            {
                var args = new XtraMessageBoxArgs
                {
                    Caption = "Validación",
                    Text = $"<b>{ex.Message}</b>",
                    Buttons = new[] { DialogResult.OK },
                    Icon = SystemIcons.Warning,
                    AllowHtmlText = DefaultBoolean.True
                };
                XtraMessageBox.Show(args);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // ============================================================
        // Finalizar (cerrar)
        // ============================================================
        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ============================================================
        // Helpers internos
        // ============================================================
        private static string SafeStr(object v)
            => v == DBNull.Value || v == null ? "" : v.ToString();

        private static string NullIfEmpty(string s)
            => string.IsNullOrWhiteSpace(s) ? null : s.Trim();

        private static int? ParseIntNullable(string s)
            => int.TryParse(s, out int v) ? v : (int?)null;

        private static decimal? ParseDecimalNullable(string s)
            => decimal.TryParse(s, out decimal v) ? v : (decimal?)null;

        // ============================================================
        // Permite solo dígitos (0-9) en el TextBox
        // ============================================================
        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir dígitos y teclas de control (backspace, etc.)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
        private void SoloNumeros_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            string limpio = new string(tb.Text.Where(char.IsDigit).ToArray());

            if (tb.Text != limpio)
            {
                int pos = tb.SelectionStart - (tb.Text.Length - limpio.Length);
                tb.Text = limpio;
                tb.SelectionStart = Math.Max(0, pos);
            }
        }

        private void ValidarCorreoLeave(TextBox txt)
        {
            string correo = txt.Text.Trim();

            if (string.IsNullOrEmpty(correo)) return;   // permitir vacío

            if (!FormHelper.EsCorreoValido(correo))
            {
                MostrarValidacion($"El correo <b>{correo}</b> no tiene un formato válido.");
                txt.Focus();
                txt.SelectAll();
            }
        }

        // ============================================================
        // Busca la cuenta contable por su código (CUENTA) y despliega
        // el nombre en el label/textbox destino. Si no existe, avisa.
        // ============================================================
        private void BuscarCuentaContablePorCodigo(TextBox txtCodigo, TextBox txtNombreDestino)
        {
            string codigo = txtCodigo.Text.Trim();
            if (string.IsNullOrEmpty(codigo))
            {
                txtNombreDestino.Clear();
                return;
            }
            try
            {
                var dt = _dal.EjecutarConsulta("SP_CATALOGO_CUENTA", new
                {
                    ACCION = "OBTENER",
                    CUENTA = codigo
                });

                if (dt.Rows.Count == 0)
                {
                    MostrarValidacion($"La cuenta contable '<b>{codigo}</b>' no existe.");
                    txtNombreDestino.Clear();
                    txtCodigo.Focus();
                    txtCodigo.SelectAll();
                    return;
                }

                var fila = dt.Rows[0];
                bool esDetalle = fila["ES_DETALLE"] != DBNull.Value && Convert.ToBoolean(fila["ES_DETALLE"]);

                if (!esDetalle)
                {
                    MostrarValidacion("No se puede asignar una cuenta acumulativa.");
                    txtNombreDestino.Clear();
                    txtCodigo.Focus();
                    txtCodigo.SelectAll();
                    return;
                }

                txtNombreDestino.Text = fila["NOMBRE_CUENTA"].ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error buscando cuenta contable: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


