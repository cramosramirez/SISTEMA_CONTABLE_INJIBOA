using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
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
        private bool _permisoCuentasProveedor = true;
        private bool _permisoTipoProveedor = true;
        private bool _permisoCodigosProveedor = true;
        private bool _codigosProveedorCompactados;
        private List<Control> _controlesDebajoCodigosProveedor;
        private DataTable _dtTiposProveedor;

        // ============================================================
        // Constantes de catálogos (IDs fijos en BD)
        // ============================================================
        private const int ORIGEN_LOCAL = 1;
        private const int ORIGEN_EXTERIOR = 2;
        private const int TIPO_PERSONA_JURIDICA = 2;
        private const int TIPO_CONTRIB_NO_CONTRIBUY = 0;
        private const int PAIS_EL_SALVADOR = 61;
        private const int DESPLAZAMIENTO_COMPACTO_CODIGOS = 70;
        private const int TIPO_PROVEEDOR_PREDETERMINADO = 4;
        public int IdEntidad { get; set; } = 0;

        public frmProveedor()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            ConfigurarContenedorTiposProveedor();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            CenterToScreen();
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
            {
                AplicarReglasOrigen();   // estado inicial según origen seleccionado
                CargarTipoProveedorPredeterminado();
            }

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
            RegistrarBusquedasCodigosRelacionados();
            CargarNombresCodigosRelacionados();
            AplicarPermisosRolProveedor();
            _cargandoFormulario = false;
            AplicarReglasTipoPersona();
            AplicarReglasTipoContribuyente();
        }

        #region === PERMISOS DEL ROL ===
        private void AplicarPermisosRolProveedor()
        {
            if (string.Equals(Configuracion.NombreRolActual, "Administrador",
                StringComparison.OrdinalIgnoreCase))
            {
                _permisoCuentasProveedor = true;
                _permisoTipoProveedor = true;
                _permisoCodigosProveedor = true;
                AplicarEstadoPermisosProveedor();
                return;
            }

            DataTable dt = _dal.EjecutarConsulta("[ESEGURIDAD].[SP_ROL_PERMISOS]", new
            {
                ACCION = "OBTENER",
                ID_ROL = Configuracion.IdRolActual
            });

            DataRow permiso = dt != null && dt.Rows.Count > 0 ? dt.Rows[0] : null;
            _permisoCuentasProveedor = TienePermiso(
                permiso, "PERMISO_EDICION_CTAS_PROVEEDOR");
            _permisoTipoProveedor = TienePermiso(
                permiso, "PERMISO_EDICION_TIPO_PROVEEDOR");
            _permisoCodigosProveedor = TienePermiso(
                permiso, "PERMISO_EDICION_COD_PROVEEDOR_SIGESTA");

            AplicarEstadoPermisosProveedor();
        }

        private static bool TienePermiso(DataRow permiso, string nombreColumna)
        {
            return permiso != null
                && permiso.Table.Columns.Contains(nombreColumna)
                && permiso[nombreColumna] != DBNull.Value
                && Convert.ToBoolean(permiso[nombreColumna]);
        }

        private void AplicarEstadoPermisosProveedor()
        {
            gclCuentasProveedor.Visible = _permisoCuentasProveedor;
            tabCodigosRelacionados.Visible = _permisoCodigosProveedor;
            grpTiposProveedor.Visible = _permisoTipoProveedor;

            bool compactarCodigos = !_permisoCodigosProveedor;
            if (compactarCodigos != _codigosProveedorCompactados)
            {
                int desplazamiento = compactarCodigos
                    ? -DESPLAZAMIENTO_COMPACTO_CODIGOS
                    : DESPLAZAMIENTO_COMPACTO_CODIGOS;
                if (_controlesDebajoCodigosProveedor == null)
                {
                    _controlesDebajoCodigosProveedor = gclDatosProveedor.Controls
                        .Cast<Control>()
                        .Where(control => control != tabCodigosRelacionados
                            && control != txtCODIGO_ENTIDAD
                            && control != label1
                            && control.Top > tabCodigosRelacionados.Bottom)
                        .ToList();
                }

                foreach (Control control in _controlesDebajoCodigosProveedor)
                    control.Top += desplazamiento;

                gclDatosProveedor.Height += desplazamiento;
                _codigosProveedorCompactados = compactarCodigos;
            }

            int desplazamientoCodigos = compactarCodigos
                ? -DESPLAZAMIENTO_COMPACTO_CODIGOS
                : 0;
            txtDIAS_PLAZO.Top = 630 + desplazamientoCodigos;
            label28.Top = 629 + desplazamientoCodigos;
            chkRETENER_RENTA.Top = 630 + desplazamientoCodigos;
            txtPORC_RENTA.Top = 630 + desplazamientoCodigos;
            label29.Top = 629 + desplazamientoCodigos;
            txtENCARGADO.Top = 664 + desplazamientoCodigos;
            label30.Top = 663 + desplazamientoCodigos;
            gclCuentasProveedor.Location = new Point(10, 697 + desplazamientoCodigos);

            grpTiposProveedor.Location = new Point(10,
                (_permisoCuentasProveedor ? gclCuentasProveedor.Bottom + 5 : gclCuentasProveedor.Top));

            chkRETENER_RENTA.Visible = false;
            label29.Visible = false;
            txtPORC_RENTA.Visible = false;
            chkRETENER_RENTA.Top = txtENCARGADO.Bottom + 10;
            label29.Top = txtENCARGADO.Bottom + 10;
            txtPORC_RENTA.Top = txtENCARGADO.Bottom + 10;

            int altoFormulario = 800 + desplazamientoCodigos;
            if (!_permisoCuentasProveedor)
                altoFormulario -= gclCuentasProveedor.Height + 5;
            if (_permisoTipoProveedor)
                altoFormulario = grpTiposProveedor.Bottom + 20;
            ClientSize = new Size(1322, altoFormulario);

            if (Visible)
                CenterToScreen();

            cbxORIGEN_ENTIDAD.Enabled = _permisoTipoProveedor;
            cbxTIPO_PERSONA.Enabled = _permisoTipoProveedor;
            cbxTIPO_CONTRIBUYENTE.Enabled = _permisoTipoProveedor
                && ComboHelper.ObtenerInt(cbxORIGEN_ENTIDAD) != ORIGEN_EXTERIOR;

            AjustarAnchosDatosPersonales();
        }

        private void AjustarAnchosDatosPersonales()
        {
            label1.Left = label11.Left;
            label1.Width = label11.Width;
            txtCODIGO_ENTIDAD.Left = cbxORIGEN_ENTIDAD.Left;

            Control[] controlesAlBordeDerecho =
            {
                txtDOCUMENTO, txtNRC, txtNOMBRE_COMERCIAL, txtACTIVIDAD_EXT,
                txtCASA, txtOTROS_DATOS, txtCORREO, cbxPAIS,
                cbxDISTRITO, txtPROFESION
            };

            // El combo central no debe extenderse debajo de la etiqueta Municipio.
            cbxMUNICIPIO.Width = 258;

            const int margenDerecho = 15;
            foreach (Control control in controlesAlBordeDerecho)
            {
                int anchoDisponible = gclDatosProveedor.ClientSize.Width
                    - control.Left - margenDerecho;
                control.Width = Math.Max(80, anchoDisponible);
            }
        }
        #endregion

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
                AplicarReglasTipoContribuyente();
            };

            cbxTIPO_CONTRIBUYENTE.SelectedIndexChanged += (s, e) =>
            {
                if (_cargandoFormulario) return;
                AplicarReglasTipoContribuyente();
                SugerirCodigo();
            };

            // Refuerzo: SelectedValue de un combo enlazado a DataSource puede estar
            // desactualizado durante SelectedIndexChanged/TextChanged. Se re-aplican las
            // reglas de forma diferida (cuando el valor ya cambió) para que la dirección
            // y demás campos respondan al cambiar Tipo de persona / Clasificación / Origen.
            Action reaplicarReglasDiferido = () =>
            {
                if (_cargandoFormulario || IsDisposed || !IsHandleCreated) return;
                BeginInvoke(new Action(() =>
                {
                    if (_cargandoFormulario || IsDisposed) return;
                    AplicarReglasTipoPersona();
                    AplicarReglasTipoContribuyente();
                }));
            };
            EventHandler reaplicarHandler = (s, e) => reaplicarReglasDiferido();
            foreach (System.Windows.Forms.ComboBox cbx in new[] { cbxORIGEN_ENTIDAD, cbxTIPO_PERSONA, cbxTIPO_CONTRIBUYENTE })
            {
                cbx.SelectedValueChanged += reaplicarHandler;
                cbx.SelectionChangeCommitted += reaplicarHandler;
                cbx.TextChanged += reaplicarHandler;
            }

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

            txtCODIGO_ENTIDAD.Leave += (s, e) => IntentarImportarCliente(txtCODIGO_ENTIDAD, "CODIGO_ENTIDAD");
            txtDUI.Leave += (s, e) => ValidarDuplicadoOImportarCliente(txtDUI, "DUI");
            txtNIT.Leave += (s, e) => ValidarDuplicadoOImportarCliente(txtNIT, "NIT");
            txtNRC.Leave += (s, e) => ValidarDuplicadoOImportarCliente(txtNRC, "NRC");
            txtCODIGO_ENTIDAD.KeyDown += (s, e) => ValidarImportacionConEnter(e, txtCODIGO_ENTIDAD, "CODIGO_ENTIDAD");
            txtDUI.KeyDown += (s, e) => ValidarImportacionConEnter(e, txtDUI, "DUI");
            txtNIT.KeyDown += (s, e) => ValidarImportacionConEnter(e, txtNIT, "NIT");
            txtNRC.KeyDown += (s, e) => ValidarImportacionConEnter(e, txtNRC, "NRC");
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

            // Dirección: campos detallados solo si NATURAL + NO CONTRIBUYENTE; si no, solo "Dirección"
            AplicarReglasDireccion();

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
                txtPROFESION.Clear(); 

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
            AplicarEstadoPermisosProveedor();
        }

        // ============================================================
        // Dirección: si TIPO PERSONA = NATURAL y CLASIFICACIÓN = NO CONTRIBUYENTE
        // (y origen LOCAL) se habilitan Calle, Número de casa, Apartamento/Local
        // y Colonia/Barrio, y se deshabilita "Dirección". En cualquier otro caso
        // se deshabilitan esos cuatro campos y se habilita únicamente "Dirección".
        // No borra valores existentes.
        // ============================================================
        private void AplicarReglasDireccion()
        {
            int? idOrigen = ComboHelper.ObtenerInt(cbxORIGEN_ENTIDAD);
            int? idTipoPersona = ComboHelper.ObtenerInt(cbxTIPO_PERSONA);
            int? idTipoContrib = ComboHelper.ObtenerInt(cbxTIPO_CONTRIBUYENTE);

            bool esExterior = idOrigen == ORIGEN_EXTERIOR;
            bool esNatural = idTipoPersona.HasValue
                && idTipoPersona.Value != TIPO_PERSONA_JURIDICA;
            bool esNoContribuyente = idTipoContrib == TIPO_CONTRIB_NO_CONTRIBUY;
            bool usarDireccionDetallada = !esExterior && esNatural && esNoContribuyente;

            txtCALLE.Enabled = usarDireccionDetallada;
            txtCASA.Enabled = usarDireccionDetallada;
            txtAPTO_LOCAL.Enabled = usarDireccionDetallada;
            txtCOLONIA.Enabled = usarDireccionDetallada;
            txtCOMPLEMENTO.Enabled = !usarDireccionDetallada;
            txtOTROS_DATOS.Enabled = !esExterior;
        }

        private void AplicarReglasTipoContribuyente()
        {
            int? idOrigen = ComboHelper.ObtenerInt(cbxORIGEN_ENTIDAD);
            int? idTipoContrib = ComboHelper.ObtenerInt(cbxTIPO_CONTRIBUYENTE);
            int? idTipoPersona = ComboHelper.ObtenerInt(cbxTIPO_PERSONA);

            bool esExterior = idOrigen == ORIGEN_EXTERIOR;
            bool esNoContribuyente = idTipoContrib == TIPO_CONTRIB_NO_CONTRIBUY;
            bool esNatural = idTipoPersona.HasValue
                && idTipoPersona.Value != TIPO_PERSONA_JURIDICA;
            AplicarReglasDireccion();

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
                var ds = _dal.EjecutarMultiple("[EDTE].[SP_ENTIDAD]", new
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
                txtNOMBRE_COMERCIAL.Text = SafeStr(r["NOMBRE_COMERCIAL"]);
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
                txtPROFESION.Text = SafeStr(r["PROFESION"]);
                txtACTIVIDAD_EXT.Text = r.Table.Columns.Contains("ACTIVIDAD_EXT")
                    ? SafeStr(r["ACTIVIDAD_EXT"]) : "";

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
                txtID_PROVEEDOR_ROZA.Text = r.Table.Columns.Contains("ID_PROVEEDOR_ROZA")
                    ? SafeStr(r["ID_PROVEEDOR_ROZA"]) : "";
                txtID_PROVEE_QQ.Text = r.Table.Columns.Contains("ID_PROVEE_QQ")
                    ? SafeStr(r["ID_PROVEE_QQ"]) : "";
                txtOTROS_DATOS.Text = SafeStr(r["OTROS_DATOS"]);

                // Actividades económicas
                CargarActividadEditar(r, "ID_ACTIVIDAD_1", txtCODI_ACTI1, txtACTIVIDAD_ECONOMICA1);
                CargarActividadEditar(r, "ID_ACTIVIDAD_2", txtCODI_ACTI2, txtACTIVIDAD_ECONOMICA2);
                CargarActividadEditar(r, "ID_ACTIVIDAD_3", txtCODI_ACTI3, txtACTIVIDAD_ECONOMICA3);

                // Aplicar reglas según los valores cargados
                AplicarReglasOrigen();
                CargarTiposProveedorExistentes(IdEntidad);
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
            int? idTipoPersona = ComboHelper.ObtenerInt(cbxTIPO_PERSONA);
            int? idTipoContrib = ComboHelper.ObtenerInt(cbxTIPO_CONTRIBUYENTE);
            bool esJuridica = idTipoPersona == TIPO_PERSONA_JURIDICA;
            bool esNoContribuyente = idTipoContrib == TIPO_CONTRIB_NO_CONTRIBUY;
            bool esNaturalNoContribuyente = !esJuridica
                && idTipoPersona.HasValue
                && esNoContribuyente;

            Control[] controlesValidables =
            {
                txtCODIGO_ENTIDAD, cbxORIGEN_ENTIDAD, cbxTIPO_PERSONA,
                cbxTIPO_CONTRIBUYENTE, cbxTIPO_DOCUMENTO_IDENTIDAD, txtDOCUMENTO,
                txtDUI, txtNIT, txtNRC, txtNOMBRE, txtNOMBRE_COMERCIAL,
                txtCOMPLEMENTO, txtCALLE, txtCASA, txtCOLONIA, cbxPAIS,
                cbxDEPARTAMENTO, cbxMUNICIPIO, cbxDISTRITO, txtTELEFONO,
                txtCELULAR, txtCORREO, txtCORREO_CC, txtCODI_ACTI1
            };
            foreach (Control control in controlesValidables)
                errorProviderValidacion.SetError(control, "");

            bool esValido = true;
            Control primerControlError = null;
            Action<Control, string> marcarError = (control, mensaje) =>
            {
                errorProviderValidacion.SetError(control, mensaje);
                esValido = false;
                if (primerControlError == null) primerControlError = control;
            };

            if (string.IsNullOrWhiteSpace(txtCODIGO_ENTIDAD.Text))
                marcarError(txtCODIGO_ENTIDAD, "El código de la entidad es obligatorio.");
            if (idOrigen == null)
                marcarError(cbxORIGEN_ENTIDAD, "El origen es obligatorio.");
            if (idTipoPersona == null)
                marcarError(cbxTIPO_PERSONA, "El tipo de persona es obligatorio.");
            if (!esExterior && idTipoContrib == null)
                marcarError(cbxTIPO_CONTRIBUYENTE, "El tipo de contribuyente es obligatorio.");
            if (string.IsNullOrWhiteSpace(txtNOMBRE.Text))
                marcarError(txtNOMBRE, "El nombre de la entidad es obligatorio.");

            if (esExterior)
            {
                if (ComboHelper.ObtenerInt(cbxTIPO_DOCUMENTO_IDENTIDAD) == null)
                    marcarError(cbxTIPO_DOCUMENTO_IDENTIDAD, "El tipo de documento es obligatorio para origen EXTERIOR.");
                if (string.IsNullOrWhiteSpace(txtDOCUMENTO.Text))
                    marcarError(txtDOCUMENTO, "El número de documento es obligatorio para origen EXTERIOR.");
                if (string.IsNullOrWhiteSpace(txtCOMPLEMENTO.Text))
                    marcarError(txtCOMPLEMENTO, "La dirección es obligatoria para origen EXTERIOR.");
                if (ComboHelper.ObtenerInt(cbxPAIS) == null)
                    marcarError(cbxPAIS, "El país es obligatorio para origen EXTERIOR.");
            }
            else if (idOrigen != null)
            {
                if (!esJuridica && string.IsNullOrWhiteSpace(txtDUI.Text))
                    marcarError(txtDUI, "El DUI es obligatorio para personas naturales.");
                if (string.IsNullOrWhiteSpace(txtNIT.Text))
                    marcarError(txtNIT, "El NIT es obligatorio para origen LOCAL.");
                if (!esNoContribuyente && string.IsNullOrWhiteSpace(txtNRC.Text))
                    marcarError(txtNRC, "El NRC es obligatorio para este tipo de contribuyente.");
                if (txtCALLE.Enabled && string.IsNullOrWhiteSpace(txtCALLE.Text))
                    marcarError(txtCALLE, "La calle es obligatoria para origen LOCAL.");
                if (txtCASA.Enabled && string.IsNullOrWhiteSpace(txtCASA.Text))
                    marcarError(txtCASA, "El número de casa es obligatorio para origen LOCAL.");
                if (txtCOLONIA.Enabled && string.IsNullOrWhiteSpace(txtCOLONIA.Text))
                    marcarError(txtCOLONIA, "La colonia o barrio es obligatoria para origen LOCAL.");
                if (txtCOMPLEMENTO.Enabled && string.IsNullOrWhiteSpace(txtCOMPLEMENTO.Text))
                    marcarError(txtCOMPLEMENTO, "La dirección es obligatoria.");
                if (!string.Equals(cbxPAIS.Text.Trim(), "EL SALVADOR", StringComparison.OrdinalIgnoreCase))
                    marcarError(cbxPAIS, "Si el origen es LOCAL, el país debe ser EL SALVADOR.");
                if (ComboHelper.ObtenerString(cbxDEPARTAMENTO) == null)
                    marcarError(cbxDEPARTAMENTO, "El departamento es obligatorio para origen LOCAL.");
                if (ComboHelper.ObtenerString(cbxMUNICIPIO) == null)
                    marcarError(cbxMUNICIPIO, "El municipio es obligatorio para origen LOCAL.");
                if (ComboHelper.ObtenerString(cbxDISTRITO) == null)
                    marcarError(cbxDISTRITO, "El distrito es obligatorio para origen LOCAL.");
                if (string.IsNullOrWhiteSpace(txtTELEFONO.Text))
                    marcarError(txtTELEFONO, "El teléfono es obligatorio para origen LOCAL.");
                if (string.IsNullOrWhiteSpace(txtCELULAR.Text))
                    marcarError(txtCELULAR, "El celular es obligatorio para origen LOCAL.");
                if (txtCODI_ACTI1.Enabled && string.IsNullOrWhiteSpace(SafeStr(txtCODI_ACTI1.Tag)))
                    marcarError(txtCODI_ACTI1, "La actividad económica primaria es obligatoria.");
            }

            if (string.IsNullOrWhiteSpace(txtCORREO.Text))
                marcarError(txtCORREO, "El correo es obligatorio.");
            else if (!FormHelper.EsCorreoValido(txtCORREO.Text))
                marcarError(txtCORREO, "Ingrese un correo válido.");
            if (!string.IsNullOrWhiteSpace(txtCORREO_CC.Text) && !FormHelper.EsCorreoValido(txtCORREO_CC.Text))
                marcarError(txtCORREO_CC, "Ingrese un correo CC válido.");

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
                    marcarError(txtCODI_ACTI1, "Debe seleccionar al menos la actividad económica primaria.");

                // No repetir actividades (solo comparar pares donde ambos tienen valor)  
                bool repetida =
                    (tiene1 && tiene2 && id1 == id2) ||
                    (tiene1 && tiene3 && id1 == id3) ||
                    (tiene2 && tiene3 && id2 == id3);

                if (repetida)
                    marcarError(txtCODI_ACTI1, "Las actividades económicas no deben repetirse.");
            }

            if (!esValido)
            {
                XtraMessageBox.Show("Hay campos obligatorios sin completar o con datos inválidos. Revise los campos marcados en rojo.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                primerControlError?.Focus();
                return false;
            }

            return ValidarCodigosRelacionadosAntesGuardar();
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
                    NOMBRE_COMERCIAL = NullIfEmpty(txtNOMBRE_COMERCIAL.Text),
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
                    ID_PROVEEDOR_ROZA = ParseIntNullable(txtID_PROVEEDOR_ROZA.Text),
                    ID_PROVEE_QQ = ParseIntNullable(txtID_PROVEE_QQ.Text),
                    RETENER_RENTA = chkRETENER_RENTA.Checked,
                    PORC_RENTA = ParseDecimalNullable(txtPORC_RENTA.Text),
                    USUARIO_CREA = Configuracion.UsuarioActual,
                    USUARIO_ACT = Configuracion.UsuarioActual,
                    OTROS_DATOS = NullIfEmpty(txtOTROS_DATOS.Text),
                    PROFESION = NullIfEmpty(txtPROFESION.Text),
                    ACTIVIDAD_EXT = NullIfEmpty(txtACTIVIDAD_EXT.Text),
                    ROL = "PRO"
                };

                int idGenerado = _dal.EjecutarEscalar("[EDTE].[SP_ENTIDAD]", parametros);

                if (idGenerado == 0)
                    throw new Exception("El SP no devolvió el ID generado.");

                IdEntidad = idGenerado;
                GuardarTiposProveedor();

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


        private void ConfigurarContenedorTiposProveedor()
        {
            btnAgregarTipoProveedor.Click += (s, e) => AgregarTipoProveedor();
            InicializarGridTiposProveedor();
        }

        private void InicializarGridTiposProveedor()
        {
            _dtTiposProveedor = new DataTable();
            _dtTiposProveedor.Columns.Add("ID_ENTIDAD_TPP", typeof(int));
            _dtTiposProveedor.Columns.Add("ID_ENTIDAD", typeof(int));
            _dtTiposProveedor.Columns.Add("ID_TIPO_PROVEEDOR", typeof(int));
            _dtTiposProveedor.Columns.Add("NOMBRE_TIPO_PROVEEDOR", typeof(string));
            _dtTiposProveedor.Columns.Add("ACTIVO", typeof(bool));
            _dtTiposProveedor.Columns.Add("FECHA_ASIGNACION", typeof(DateTime));
            gridTiposProveedor.DataSource = _dtTiposProveedor;
            gvTiposProveedor.PopulateColumns();

            foreach (string campo in new[] { "ID_ENTIDAD_TPP", "ID_ENTIDAD", "ID_TIPO_PROVEEDOR" })
                if (gvTiposProveedor.Columns[campo] != null) gvTiposProveedor.Columns[campo].Visible = false;

            var colTipo = gvTiposProveedor.Columns["NOMBRE_TIPO_PROVEEDOR"];
            colTipo.Caption = "Tipo Proveedor";
            colTipo.VisibleIndex = 0;
            colTipo.Width = 700;
            var repoTipo = new RepositoryItemTextEdit();
            repoTipo.KeyDown += (s, e) =>
            {
                var editor = s as TextEdit;
                if (e.KeyCode == Keys.Enter && editor != null && editor.Text.Trim() == "*")
                {
                    e.Handled = true;
                    AbrirBusquedaTipoProveedor();
                }
            };
            gridTiposProveedor.RepositoryItems.Add(repoTipo);
            colTipo.ColumnEdit = repoTipo;

            var colActivo = gvTiposProveedor.Columns["ACTIVO"];
            colActivo.Caption = "Activo";
            colActivo.VisibleIndex = 1;
            colActivo.Width = 70;
            var repoActivo = new RepositoryItemCheckEdit();
            gridTiposProveedor.RepositoryItems.Add(repoActivo);
            colActivo.ColumnEdit = repoActivo;

            var colFecha = gvTiposProveedor.Columns["FECHA_ASIGNACION"];
            colFecha.Caption = "Fecha Asignación";
            colFecha.VisibleIndex = 2;
            colFecha.Width = 140;
            colFecha.DisplayFormat.FormatString = "dd/MM/yyyy";
            colFecha.DisplayFormat.FormatType = FormatType.DateTime;

            var colEliminar = gvTiposProveedor.Columns.AddField("ELIMINAR");
            colEliminar.Caption = " ";
            colEliminar.Visible = true;
            colEliminar.VisibleIndex = 3;
            colEliminar.Width = 40;
            colEliminar.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            var repoEliminar = new RepositoryItemButtonEdit();
            repoEliminar.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            repoEliminar.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            repoEliminar.Buttons[0].ImageOptions.Image = Properties.Resources.eliminarFila32x32;
            repoEliminar.Buttons[0].ToolTip = "Eliminar tipo de proveedor";
            repoEliminar.ButtonClick += (s, e) => EliminarTipoProveedor();
            gridTiposProveedor.RepositoryItems.Add(repoEliminar);
            colEliminar.ColumnEdit = repoEliminar;

            gvTiposProveedor.OptionsView.ShowGroupPanel = false;
            gvTiposProveedor.OptionsView.ShowIndicator = false;
            gvTiposProveedor.OptionsBehavior.Editable = true;
        }

        private void AbrirBusquedaTipoProveedor()
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
                    { "NOMBRE_TIPO_PROVEEDOR", 350 }
                }
            };
            using (var frm = new frmBusquedaGenerica(config))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                if (frm.ShowDialog() != DialogResult.OK || frm.FilaSeleccionada == null) return;
                gvTiposProveedor.SetFocusedRowCellValue("ID_TIPO_PROVEEDOR",
                    Convert.ToInt32(frm.FilaSeleccionada["ID_TIPO_PROVEEDOR"]));
                gvTiposProveedor.SetFocusedRowCellValue("NOMBRE_TIPO_PROVEEDOR",
                    frm.FilaSeleccionada["NOMBRE_TIPO_PROVEEDOR"].ToString());
            }
        }

        private void AgregarTipoProveedor()
        {
            _dtTiposProveedor.Rows.Add(0, IdEntidad, DBNull.Value, "", true, DateTime.Today);
        }

        private void CargarTipoProveedorPredeterminado()
        {
            if (_dtTiposProveedor == null)
                return;

            bool yaExiste = _dtTiposProveedor.AsEnumerable().Any(fila =>
                fila.RowState != DataRowState.Deleted
                && fila["ID_TIPO_PROVEEDOR"] != DBNull.Value
                && Convert.ToInt32(fila["ID_TIPO_PROVEEDOR"]) == TIPO_PROVEEDOR_PREDETERMINADO);

            if (yaExiste)
                return;

            DataTable tiposProveedor = _dal.EjecutarConsulta(
                "[ESEGURIDAD].[SP_TIPO_PROVEEDOR]",
                new { ACCION = "LISTAR" });

            DataRow tipoPredeterminado = tiposProveedor?.AsEnumerable().FirstOrDefault(fila =>
                fila["ID_TIPO_PROVEEDOR"] != DBNull.Value
                && Convert.ToInt32(fila["ID_TIPO_PROVEEDOR"]) == TIPO_PROVEEDOR_PREDETERMINADO);

            if (tipoPredeterminado == null)
                throw new InvalidOperationException(
                    $"No existe o no está activo el tipo de proveedor predeterminado {TIPO_PROVEEDOR_PREDETERMINADO}.");

            _dtTiposProveedor.Rows.Add(
                0,
                0,
                TIPO_PROVEEDOR_PREDETERMINADO,
                tipoPredeterminado["NOMBRE_TIPO_PROVEEDOR"].ToString(),
                true,
                DateTime.Today);
        }

        private void CargarTiposProveedorExistentes(int idEntidad)
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_ENTIDAD_TIPO_PROVEEDOR]",
                new { ACCION = "LISTAR", ID_ENTIDAD = idEntidad });
            _dtTiposProveedor.Clear();
            if (dt == null) return;
            foreach (DataRow row in dt.Rows)
                _dtTiposProveedor.Rows.Add(row["ID_ENTIDAD_TPP"], row["ID_ENTIDAD"],
                    row["ID_TIPO_PROVEEDOR"], row["NOMBRE_TIPO_PROVEEDOR"],
                    row["ACTIVO"], row["FECHA_ASIGNACION"]);
        }

        private void GuardarTiposProveedor()
        {
            gvTiposProveedor.CloseEditor();
            gvTiposProveedor.UpdateCurrentRow();
            foreach (DataRow fila in _dtTiposProveedor.Rows)
            {
                if (fila.RowState == DataRowState.Deleted || fila["ID_TIPO_PROVEEDOR"] == DBNull.Value) continue;
                _dal.EjecutarSinRetorno("[EMH].[SP_ENTIDAD_TIPO_PROVEEDOR]", new
                {
                    ACCION = "GUARDAR",
                    ID_ENTIDAD_TPP = Convert.ToInt32(fila["ID_ENTIDAD_TPP"]),
                    ID_ENTIDAD = IdEntidad,
                    ID_TIPO_PROVEEDOR = Convert.ToInt32(fila["ID_TIPO_PROVEEDOR"]),
                    ACTIVO = Convert.ToBoolean(fila["ACTIVO"]),
                    FECHA_ASIGNACION = Convert.ToDateTime(fila["FECHA_ASIGNACION"]),
                    USUARIO = Configuracion.UsuarioActual
                });
            }
            CargarTiposProveedorExistentes(IdEntidad);
        }

        private void EliminarTipoProveedor()
        {
            int fila = gvTiposProveedor.FocusedRowHandle;
            if (fila < 0) return;
            if (MessageBox.Show("¿Desea eliminar este tipo de proveedor de la entidad?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            object id = gvTiposProveedor.GetRowCellValue(fila, "ID_ENTIDAD_TPP");
            if (id != null && id != DBNull.Value && Convert.ToInt32(id) > 0)
                _dal.EjecutarSinRetorno("[EMH].[SP_ENTIDAD_TIPO_PROVEEDOR]",
                    new { ACCION = "ELIMINAR", ID_ENTIDAD_TPP = Convert.ToInt32(id) });
            gvTiposProveedor.DeleteRow(fila);
        }

        private void ValidarDuplicadoOImportarCliente(TextBox campo, string nombreCampo)
        {
            if (!IntentarImportarCliente(campo, nombreCampo))
                ValidarDuplicado(campo, nombreCampo);
        }

        private void ValidarImportacionConEnter(KeyEventArgs e, TextBox campo, string nombreCampo)
        {
            if (e.KeyCode != Keys.Enter) return;

            e.Handled = true;
            e.SuppressKeyPress = true;
            if (nombreCampo == "CODIGO_ENTIDAD")
                IntentarImportarCliente(campo, nombreCampo);
            else
                ValidarDuplicadoOImportarCliente(campo, nombreCampo);
        }

        private bool IntentarImportarCliente(TextBox campo, string nombreCampo)
        {
            if (IdEntidad > 0) return false;

            string valor = campo.Text.Trim();
            if (string.IsNullOrWhiteSpace(valor)) return false;

            int idCliente = BuscarEntidadPorCampoConRol(nombreCampo, valor, "CLI");
            if (idCliente <= 0) return false;

            DialogResult respuesta = XtraMessageBox.Show(
                "Se encontró el proveedor como cliente, ¿desea importar la información?",
                "Importar información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes) return false;

            IdEntidad = idCliente;
            CargarEntidadParaEditar();
            return true;
        }

        private int BuscarEntidadPorCampoConRol(string nombreCampo, string valor, string rol)
        {
            try
            {
                DataTable dt = _dal.EjecutarConsulta("[EDTE].[SP_ENTIDAD]", new
                {
                    ACCION = "BUSCAR",
                    FILTRO = valor,
                    ROL = rol
                });

                if (dt == null || !dt.Columns.Contains(nombreCampo)) return 0;

                string buscado = NormalizarIdentificador(valor, nombreCampo);
                foreach (DataRow fila in dt.Rows)
                {
                    string encontrado = NormalizarIdentificador(SafeStr(fila[nombreCampo]), nombreCampo);
                    if (!string.Equals(encontrado, buscado, StringComparison.OrdinalIgnoreCase)) continue;

                    return Convert.ToInt32(fila["ID_ENTIDAD"]);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error buscando entidad para importar: {ex.Message}");
            }
            return 0;
        }

        private static string NormalizarIdentificador(string valor, string nombreCampo)
        {
            string texto = (valor ?? "").Trim();
            return nombreCampo == "CODIGO_ENTIDAD"
                ? texto
                : new string(texto.Where(char.IsLetterOrDigit).ToArray());
        }


        private void RegistrarBusquedasCodigosRelacionados()
        {
            RegistrarBusquedaCodigo(txtCODIPROVEEDOR, txtNOMBRE_PRODUCTOR_INTEGRACION, txtNIT_PRODUCTOR_INTEGRACION, "PRODUCTOR");
            RegistrarBusquedaCodigo(txtCODTRANSPORT, txtNOMBRE_TRANSPORTISTA_INTEGRACION, txtNIT_TRANSPORTISTA_INTEGRACION, "TRASPORTISTA");
            RegistrarBusquedaCodigo(txtID_CARGADORA, txtNOMBRE_CARGADORA_INTEGRACION, txtNIT_CARGADORA_INTEGRACION, "CARGADORA");
            RegistrarBusquedaCodigo(txtID_PROVEEDOR_ROZA, txtNOMBRE_ROZA_INTEGRACION, txtNIT_ROZA_INTEGRACION, "ROZA");
            RegistrarBusquedaCodigo(txtID_PROVEE_QQ, txtNOMBRE_QUERQUEO_INTEGRACION, txtNIT_QUERQUEO_INTEGRACION, "QUERQUEO");
        }

        private void RegistrarBusquedaCodigo(TextBox codigo, TextBox nombre, TextBox nit, string accion)
        {
            FormHelper.RegistrarBusqueda(codigo, new BusquedaConfig
            {
                StoredProcedure = "[EGENERALES].[SP_PROVEEDOR_INTEGRACION]",
                Accion = accion,
                NombreParametroAccion = "ACTION",
                Columnas = new Dictionary<string, string> { { "CODIGO", "CÓDIGO" }, { "NOMBRE", "NOMBRE" }, { "NIT", "NIT" } },
                Anchos = new Dictionary<string, int> { { "CODIGO", 100 }, { "NOMBRE", 350 }, { "NIT", 140 } }
            }, fila => AsignarCodigoRelacionado(fila, codigo, nombre, nit));
            codigo.Leave += (s, e) => ResolverCodigoRelacionado(codigo, nombre, nit, accion);
        }

        private void AsignarCodigoRelacionado(DataRow fila, TextBox codigo, TextBox nombre, TextBox nit)
        {
            string nitRelacionado = fila.Table.Columns.Contains("NIT") ? fila["NIT"].ToString() : "";
            if (!NitCoincide(txtNIT.Text, nitRelacionado))
            {
                XtraMessageBox.Show("El NIT del registro relacionado debe coincidir con el NIT del proveedor.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                codigo.Clear(); nombre.Clear(); nit.Clear();
                return;
            }
            codigo.Text = fila.Table.Columns.Contains("CODIGO") ? fila["CODIGO"].ToString() : "";
            nombre.Text = fila.Table.Columns.Contains("NOMBRE") ? fila["NOMBRE"].ToString() : "";
            nit.Text = nitRelacionado;
        }

        private static bool NitCoincide(string nitEntidad, string nitRelacionado)
        {
            string Normalizar(string valor) => new string((valor ?? "")
                .Where(char.IsLetterOrDigit).ToArray()).ToUpperInvariant();
            string entidad = Normalizar(nitEntidad);
            string relacionado = Normalizar(nitRelacionado);
            return entidad.Length > 0 && relacionado.Length > 0 && entidad == relacionado;
        }

        private void ResolverCodigoRelacionado(TextBox codigo, TextBox nombre, TextBox nit, string accion)
        {
            string valor = codigo.Text.Trim();
            if (string.IsNullOrWhiteSpace(valor) || valor == "*") return;
            DataTable dt = _dal.EjecutarConsulta("[EGENERALES].[SP_PROVEEDOR_INTEGRACION]", new { ACTION = accion, FILTRO = valor });
            DataRow fila = dt.AsEnumerable().FirstOrDefault(r => string.Equals(r["CODIGO"].ToString(), valor, StringComparison.OrdinalIgnoreCase));
            if (fila == null) { nombre.Clear(); nit.Clear(); return; }
            AsignarCodigoRelacionado(fila, codigo, nombre, nit);
        }

        private void CargarNombresCodigosRelacionados()
        {
            ResolverCodigoRelacionado(txtCODIPROVEEDOR, txtNOMBRE_PRODUCTOR_INTEGRACION, txtNIT_PRODUCTOR_INTEGRACION, "PRODUCTOR");
            ResolverCodigoRelacionado(txtCODTRANSPORT, txtNOMBRE_TRANSPORTISTA_INTEGRACION, txtNIT_TRANSPORTISTA_INTEGRACION, "TRASPORTISTA");
            ResolverCodigoRelacionado(txtID_CARGADORA, txtNOMBRE_CARGADORA_INTEGRACION, txtNIT_CARGADORA_INTEGRACION, "CARGADORA");
            ResolverCodigoRelacionado(txtID_PROVEEDOR_ROZA, txtNOMBRE_ROZA_INTEGRACION, txtNIT_ROZA_INTEGRACION, "ROZA");
            ResolverCodigoRelacionado(txtID_PROVEE_QQ, txtNOMBRE_QUERQUEO_INTEGRACION, txtNIT_QUERQUEO_INTEGRACION, "QUERQUEO");
        }

        private bool ValidarCodigosRelacionadosAntesGuardar()
        {
            TextBox[] codigos =
            {
                txtCODIPROVEEDOR, txtCODTRANSPORT, txtID_CARGADORA,
                txtID_PROVEEDOR_ROZA, txtID_PROVEE_QQ
            };
            TextBox[] nits =
            {
                txtNIT_PRODUCTOR_INTEGRACION, txtNIT_TRANSPORTISTA_INTEGRACION,
                txtNIT_CARGADORA_INTEGRACION, txtNIT_ROZA_INTEGRACION,
                txtNIT_QUERQUEO_INTEGRACION
            };
            string[] nombres = { "Productor", "Transportista", "Cargadora", "Roza", "Querqueo" };

            for (int indice = 0; indice < codigos.Length; indice++)
            {
                if (string.IsNullOrWhiteSpace(codigos[indice].Text)) continue;
                if (NitCoincide(txtNIT.Text, nits[indice].Text)) continue;

                tabCodigosRelacionados.SelectedTabPageIndex = indice;
                XtraMessageBox.Show(
                    $"El NIT de {nombres[indice]} debe coincidir con el NIT del proveedor.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                codigos[indice].Focus();
                return false;
            }
            return true;
        }
    }
}


