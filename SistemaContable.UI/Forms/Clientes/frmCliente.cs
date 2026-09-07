using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System.Linq;
namespace SistemaContable.UI.Forms.Clientes
{
    public partial class frmCliente : Form
    {
        #region === CAMPOS PRIVADOS ===
        private readonly DALBase _dal = new DALBase();
        private DataTable _dtRoles;
        private DataTable _dtAllMunicipios;
        private bool _cargando = false;
        // IDs seleccionados por búsqueda de actividad económica
        private int _idActividad1 = 0;
        private int _idActividad2 = 0;
        private int _idActividad3 = 0;
        // ID seleccionado desde la búsqueda genérica de Tipo de Precio
        private int? _idTipoPrecioSeleccionado;
        #endregion
        public int IdEntidad { get; set; } = 0;
        public frmCliente()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        #region === CARGA INICIAL ===
        private void frmEntidad_Load(object sender, EventArgs e)
        {
            FormHelper.Inicializar(this);
            CargarTipoPersona();
            CargarTipoContribuyente();
            CargarTipoDocIdentidad();
            CargarPaises();
            CargarDepartamentos();
            ConfigurarToolTips();
            RegistrarBusquedaActividades();
            RegistrarBusquedaTipoPrecio();
            RegistrarBusquedaCuenta();
            RegistrarBusquedaProveedorIntegracion();
            RegistrarBusquedaTransportistaIntegracion();
            RegistrarBusquedaCargadoraIntegracion();
            RegistrarBusquedaRozaIntegracion();
            RegistrarBusquedaQuerqueoIntegracion();
            CargarOrigen();
            InicializarGridRoles();
            if (IdEntidad == 0)
            {
                LimpiarFormulario();
                ConfigurarBotones(esNuevo: true);
                txtCODIGO_ENTIDAD.Focus();
            }
            else
            {
                CargarEntidadExistente(IdEntidad);
            }
        }
        #endregion
        #region === COMBOS ===
        private static void AgregarFilaVacia(DataTable dt, string displayMember = null)
        {
            foreach (DataColumn col in dt.Columns)
                col.AllowDBNull = true;
            DataRow fila = dt.NewRow();
            foreach (DataColumn col in dt.Columns)
            {
                try
                {
                    if (displayMember != null && col.ColumnName == displayMember)
                        fila[col] = "-- Seleccionar --";
                    else if (col.DataType == typeof(int) || col.DataType == typeof(long) ||
                             col.DataType == typeof(short) || col.DataType == typeof(byte) ||
                             col.DataType == typeof(decimal) || col.DataType == typeof(double) ||
                             col.DataType == typeof(float))
                        fila[col] = 0;
                    else if (col.DataType == typeof(bool))
                        fila[col] = false;
                    else
                        fila[col] = DBNull.Value;
                }
                catch { fila[col] = DBNull.Value; }
            }
            dt.Rows.InsertAt(fila, 0);
        }
        private void CargarTipoPersona()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_TIPO_PERSONA]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "NOMBRE");
            cbxTIPO_ENTIDAD.DataSource = dt;
            cbxTIPO_ENTIDAD.ValueMember = "ID_TIPO_PERSONA";
            cbxTIPO_ENTIDAD.DisplayMember = "NOMBRE";
            cbxTIPO_ENTIDAD.SelectedIndex = 0;
        }
        private void CargarTipoContribuyente()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_TIPO_CONTRIBUYENTE]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "NOMBRE");
            cbxTIPO_CONTRIB.DataSource = dt;
            cbxTIPO_CONTRIB.ValueMember = "ID_TIPO_CONTRIB";
            cbxTIPO_CONTRIB.DisplayMember = "NOMBRE";
            cbxTIPO_CONTRIB.SelectedIndex = 0;
        }
        private void CargarTipoDocIdentidad()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_TIPO_DOCUMENTO_IDENTIDAD]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "NOMBRE");
            cbxTIPO_DOC_IDEN.DataSource = dt;
            cbxTIPO_DOC_IDEN.ValueMember = "ID_TIPO_DOC_INDEN";
            cbxTIPO_DOC_IDEN.DisplayMember = "NOMBRE";
            cbxTIPO_DOC_IDEN.SelectedIndex = 0;
        }
        private void CargarPaises()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_PAIS]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "VALORES");
            cbxPAIS.DataSource = dt;
            cbxPAIS.ValueMember = "ID_PAIS";
            cbxPAIS.DisplayMember = "VALORES";
            cbxPAIS.SelectedIndex = 0;
        }
        private void CargarDepartamentos()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_DEPARTAMENTO]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "VALORES");
            cbxDEPTO.DataSource = dt;
            cbxDEPTO.ValueMember = "CODI_DEPTO";
            cbxDEPTO.DisplayMember = "VALORES";
            cbxDEPTO.SelectedIndex = 0;
        }
        private void CargarMunicipios(string codiDepto)
        {
            _dtAllMunicipios = _dal.EjecutarConsulta("[EMH].[SP_MUNICIPIO]",
                new { ACCION = "LISTAR", CODI_DEPTO = codiDepto });
            DataTable dtMuni = (_dtAllMunicipios != null && _dtAllMunicipios.Rows.Count > 0)
                ? _dtAllMunicipios.Copy()
                : new DataTable();
            if (!dtMuni.Columns.Contains("CODI_MUNI"))
                dtMuni.Columns.Add("CODI_MUNI", typeof(string));
            if (!dtMuni.Columns.Contains("NOMBRE_MUNICIPIO"))
                dtMuni.Columns.Add("NOMBRE_MUNICIPIO", typeof(string));
            AgregarFilaVacia(dtMuni, "NOMBRE_MUNICIPIO");
            cbxMUNI.DataSource = dtMuni;
            cbxMUNI.ValueMember = "CODI_MUNI";
            cbxMUNI.DisplayMember = "NOMBRE_MUNICIPIO";
            cbxMUNI.SelectedIndex = 0;
            cbxDIST.DataSource = null;
            cbxDIST.Items.Clear();
        }
        private void SetDistritoByMunicipio(string codiMuni)
        {
            if (_dtAllMunicipios == null || string.IsNullOrEmpty(codiMuni)) return;
            var fila = _dtAllMunicipios.AsEnumerable()
                .FirstOrDefault(r => r["CODI_MUNI"].ToString() == codiMuni);
            if (fila == null || fila["CODI_DIST"] == DBNull.Value)
            {
                cbxDIST.DataSource = null;
                cbxDIST.Items.Clear();
                return;
            }
            DataTable dtDist = new DataTable();
            dtDist.Columns.Add("CODI_DIST", typeof(string));
            dtDist.Columns.Add("NOMBRE_DISTRITO", typeof(string));
            DataRow nr = dtDist.NewRow();
            nr["CODI_DIST"] = fila["CODI_DIST"];
            nr["NOMBRE_DISTRITO"] = fila["NOMBRE_DISTRITO"] != DBNull.Value
                                    ? fila["NOMBRE_DISTRITO"]
                                    : string.Empty;
            dtDist.Rows.Add(nr);
            cbxDIST.DataSource = dtDist;
            cbxDIST.ValueMember = "CODI_DIST";
            cbxDIST.DisplayMember = "NOMBRE_DISTRITO";
            cbxDIST.SelectedIndex = 0;
        }
        /// <summary>
        /// ToolTips de búsqueda genérica ("*" + Enter), igual que en NotaRemision\frmDespacho.cs,
        /// para todos los campos de código que usan FormHelper.RegistrarBusqueda en este formulario:
        /// Actividad Económica 1/2/3, Tipo de Precio, Cuenta x Cobrar y las integraciones
        /// (Productor, Transportista, Cargadora, Roza, Querqueo) - 2026-08-25.
        /// </summary>
        private void ConfigurarToolTips()
        {
            TooltipHelper.Configurar(
                            (txtCODI_ACTIVIDAD1, "Ingrese * y presione Enter para mostrar todas las actividades económicas."),
                            (txtCODI_ACTIVIDAD2, "Ingrese * y presione Enter para mostrar todas las actividades económicas."),
                            (txtCODI_ACTIVIDAD3, "Ingrese * y presione Enter para mostrar todas las actividades económicas."),
                            (txtID_TIPO_PRECIO, "Ingrese * y presione Enter para mostrar todos los tipos de precio."),
                            (txtCUENTA_X_COBRAR, "Ingrese * y presione Enter para mostrar todas las cuentas contables."),
                            (txtCODIPROVEEDOR, "Ingrese * y presione Enter para mostrar todos los productores."),
                            (txtCODTRANSPORT, "Ingrese * y presione Enter para mostrar todos los transportistas."),
                            (txtID_CARGADORA, "Ingrese * y presione Enter para mostrar todas las cargadoras."),
                            (txtID_PROVEEDOR_ROZA, "Ingrese * y presione Enter para mostrar todos los proveedores de roza."),
                            (txtID_PROVEE_QQ, "Ingrese * y presione Enter para mostrar todos los proveedores de querqueo.")
                                 );
        }
        private void RegistrarBusquedaActividades()
        {
            var config1 = new BusquedaConfig
            {
                StoredProcedure = "[EMH].[SP_ACTIVIDAD_ECONOMICA]",
                Accion = "BUSCAR",
                Columnas = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "CODI_MH", "Código" },
                    { "VALORES", "Descripción" }
                },
                Anchos = new System.Collections.Generic.Dictionary<string, int>
                {
                    { "CODI_MH",  80 },
                    { "VALORES", 600 }
                }
            };
            FormHelper.RegistrarBusqueda(txtCODI_ACTIVIDAD1, config1, fila =>
            {
                _idActividad1 = Convert.ToInt32(fila["ID_ACTIVIDAD"]);
                txtCODI_ACTIVIDAD1.Text = fila["CODI_MH"].ToString();
                txtACTIVIDAD_1.Text = fila["VALORES"].ToString();
            });
            var config2 = new BusquedaConfig
            {
                StoredProcedure = "[EMH].[SP_ACTIVIDAD_ECONOMICA]",
                Accion = "BUSCAR",
                Columnas = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "CODI_MH", "Código" },
                    { "VALORES", "Descripción" }
                },
                Anchos = new System.Collections.Generic.Dictionary<string, int>
                {
                    { "CODI_MH",  80 },
                    { "VALORES", 600 }
                }
            };
            FormHelper.RegistrarBusqueda(txtCODI_ACTIVIDAD2, config2, fila =>
            {
                _idActividad2 = Convert.ToInt32(fila["ID_ACTIVIDAD"]);
                txtCODI_ACTIVIDAD2.Text = fila["CODI_MH"].ToString();
                txtACTIVIDAD_2.Text = fila["VALORES"].ToString();
            });
            var config3 = new BusquedaConfig
            {
                StoredProcedure = "[EMH].[SP_ACTIVIDAD_ECONOMICA]",
                Accion = "BUSCAR",
                Columnas = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "CODI_MH", "Código" },
                    { "VALORES", "Descripción" }
                },
                Anchos = new System.Collections.Generic.Dictionary<string, int>
                {
                    { "CODI_MH",  80 },
                    { "VALORES", 600 }
                }
            };
            FormHelper.RegistrarBusqueda(txtCODI_ACTIVIDAD3, config3, fila =>
            {
                _idActividad3 = Convert.ToInt32(fila["ID_ACTIVIDAD"]);
                txtCODI_ACTIVIDAD3.Text = fila["CODI_MH"].ToString();
                txtACTIVIDAD_3.Text = fila["VALORES"].ToString();
            });
            // Búsqueda por código escrito directamente (perder foco), igual que en frmProveedor
            txtCODI_ACTIVIDAD1.Leave += (s, e) => BuscarActividadPorCodigo(txtCODI_ACTIVIDAD1, txtACTIVIDAD_1, v => _idActividad1 = v);
            txtCODI_ACTIVIDAD2.Leave += (s, e) => BuscarActividadPorCodigo(txtCODI_ACTIVIDAD2, txtACTIVIDAD_2, v => _idActividad2 = v);
            txtCODI_ACTIVIDAD3.Leave += (s, e) => BuscarActividadPorCodigo(txtCODI_ACTIVIDAD3, txtACTIVIDAD_3, v => _idActividad3 = v);
        }
        /// <summary>
        /// Busca la actividad económica por su código (CODI_MH) escrito directamente
        /// en el campo, al perder el foco, usando [EMH].[SP_ACTIVIDAD_ECONOMICA].
        /// Se usa BUSCAR + FILTRO (no filtra por CODI_MH exacto) y se filtra la
        /// coincidencia exacta en C#.
        /// </summary>
        private void BuscarActividadPorCodigo(TextBox txtCodigo, TextBox txtDescripcion, Action<int> asignarId)
        {
            string codigo = txtCodigo.Text.Trim();
            if (string.IsNullOrEmpty(codigo))
            {
                asignarId(0);
                txtDescripcion.Clear();
                return;
            }
            try
            {
                DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_ACTIVIDAD_ECONOMICA]",
                    new { ACCION = "BUSCAR", FILTRO = codigo });
                DataRow fila = null;
                if (dt != null)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        if (string.Equals(r["CODI_MH"]?.ToString()?.Trim(), codigo, StringComparison.OrdinalIgnoreCase))
                        {
                            fila = r;
                            break;
                        }
                    }
                }
                if (fila == null)
                {
                    MostrarValidacion($"No se encontró la actividad económica con código '{codigo}'.");
                    asignarId(0);
                    txtCodigo.Clear();
                    txtDescripcion.Clear();
                    txtCodigo.Focus();
                    return;
                }
                asignarId(Convert.ToInt32(fila["ID_ACTIVIDAD"]));
                txtCodigo.Text = fila["CODI_MH"].ToString();
                txtDescripcion.Text = fila["VALORES"].ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error buscando actividad: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Carga código + descripción de una actividad económica ya asignada
        /// (por ID), usando ACCION="CONSULTAR" de [EMH].[SP_ACTIVIDAD_ECONOMICA].
        /// </summary>
        private void CargarActividadPorId(int idActividad, TextBox txtCodigo, TextBox txtDescripcion)
        {
            if (idActividad <= 0)
            {
                txtCodigo.Clear();
                txtDescripcion.Clear();
                return;
            }
            try
            {
                DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_ACTIVIDAD_ECONOMICA]",
                    new { ACCION = "CONSULTAR", ID_ACTIVIDAD = idActividad });
                if (dt == null || dt.Rows.Count == 0)
                {
                    txtCodigo.Clear();
                    txtDescripcion.Clear();
                    return;
                }
                var fila = dt.Rows[0];
                txtCodigo.Text = fila["CODI_MH"]?.ToString() ?? "";
                txtDescripcion.Text = fila["VALORES"]?.ToString() ?? "";
            }
            catch
            {
                // Silenciar
            }
        }
        /// <summary>
        /// Registra la búsqueda genérica con "*" para el Tipo de Precio del cliente
        /// (mismo catálogo [EINVENTARIO].[TIPO_PRECIO] usado en frmProducto).
        /// </summary>
        private void RegistrarBusquedaTipoPrecio()
        {
            var config = new BusquedaConfig
            {
                StoredProcedure = "[EINVENTARIO].[SP_TIPO_PRECIO]",
                Accion = "BUSCAR",
                Columnas = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "DESCRIPCION", "TIPO DE PRECIO" }
                },
                Anchos = new System.Collections.Generic.Dictionary<string, int>
                {
                    { "DESCRIPCION", 300 }
                }
            };
            FormHelper.RegistrarBusqueda(txtID_TIPO_PRECIO, config, fila =>
            {
                _idTipoPrecioSeleccionado = Convert.ToInt32(fila["ID_TIPO_PRECIO"]);
                txtID_TIPO_PRECIO.Text = fila["DESCRIPCION"].ToString();
            });
        }
        private string ObtenerDescripcionTipoPrecio(int? idTipoPrecio)
        {
            if (idTipoPrecio == null || idTipoPrecio <= 0) return "";
            try
            {
                DataTable dt = _dal.EjecutarConsulta("[EINVENTARIO].[SP_TIPO_PRECIO]",
                    new { ACCION = "OBTENER", ID_TIPO_PRECIO = idTipoPrecio });
                if (dt != null && dt.Rows.Count > 0)
                    return dt.Rows[0]["DESCRIPCION"]?.ToString() ?? "";
            }
            catch { }
            return "";
        }
        /// <summary>
        /// Registra la búsqueda genérica con "*" para la Cuenta x Cobrar, igual que
        /// txtCUENTA_X_PAGAR en frmProveedor: SP_CATALOGO_CUENTA ya existe en la base
        /// de datos (no se crea aquí), filtrando solo cuentas de detalle (ES_DETALLE = true).
        /// </summary>
        private void RegistrarBusquedaCuenta()
        {
            var config = new BusquedaConfig
            {
                StoredProcedure = "SP_CATALOGO_CUENTA",
                Columnas = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "CUENTA",        "CODIGO" },
                    { "NOMBRE_CUENTA", "NOMBRE" }
                },
                Anchos = new System.Collections.Generic.Dictionary<string, int>
                {
                    { "CUENTA",        100 },
                    { "NOMBRE_CUENTA", 300 }
                },
                ParametrosExtra = new { ES_DETALLE = true }
            };
            FormHelper.RegistrarBusqueda(txtCUENTA_X_COBRAR, config, fila =>
            {
                txtCUENTA_X_COBRAR.Text = fila["CUENTA"].ToString();
                txtNOMBRE_CUENTA_X_COBRAR.Text = fila["NOMBRE_CUENTA"].ToString();
            });
            txtCUENTA_X_COBRAR.Leave += (s, e) => BuscarCuentaContablePorCodigo(txtCUENTA_X_COBRAR, txtNOMBRE_CUENTA_X_COBRAR);
        }

        /// <summary>
        /// Registra la búsqueda genérica de proveedores de INJIBOA.
        /// Escribir * y presionar Enter abre el selector; también se admite
        /// escribir un código exacto y salir del campo.
        /// </summary>
        private void RegistrarBusquedaProveedorIntegracion()
        {
            var config = new BusquedaConfig
            {
                StoredProcedure = "[EGENERALES].[SP_PROVEEDOR_INTEGRACION]",
                Accion = "PRODUCTOR",
                NombreParametroAccion = "ACTION",
                Columnas = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "CODIGO", "CÓDIGO" },
                    { "NOMBRE", "NOMBRE" },
                    { "NIT", "NIT" }
                },
                Anchos = new System.Collections.Generic.Dictionary<string, int>
                {
                    { "CODIGO", 70 },
                    { "NOMBRE", 420 },
                    { "NIT", 140 }
                }
            };

            FormHelper.RegistrarBusqueda(txtCODIPROVEEDOR, config, fila =>
            {
                string nitRelacionado = ObtenerNITIntegracion(fila);
                if (!ValidarNitRelacionado("Productor", nitRelacionado, txtCODIPROVEEDOR))
                {
                    LimpiarProductorRelacionado();
                    return;
                }

                txtCODIPROVEEDOR.Text = fila["CODIGO"].ToString();
                txtNOMBRE_PROVEEDOR_INTEGRACION.Text = fila["NOMBRE"].ToString();
                txtNIT_PROVEEDOR_INTEGRACION.Text = nitRelacionado;
            });

            txtCODIPROVEEDOR.Leave += (s, e) => BuscarProveedorIntegracionPorCodigo();
        }

        private void BuscarProveedorIntegracionPorCodigo(bool validarNit = true)
        {
            string codigo = txtCODIPROVEEDOR.Text.Trim();
            if (string.IsNullOrEmpty(codigo) || codigo == "*")
            {
                if (string.IsNullOrEmpty(codigo))
                {
                    txtNOMBRE_PROVEEDOR_INTEGRACION.Clear();
                    txtNIT_PROVEEDOR_INTEGRACION.Clear();
                }
                return;
            }

            try
            {
                DataTable dt = _dal.EjecutarConsulta(
                    "[EGENERALES].[SP_PROVEEDOR_INTEGRACION]",
                    new { ACTION = "PRODUCTOR", FILTRO = codigo });

                DataRow fila = dt?.AsEnumerable().FirstOrDefault(r =>
                    string.Equals(r["CODIGO"]?.ToString()?.Trim(), codigo,
                        StringComparison.OrdinalIgnoreCase));

                if (fila == null)
                {
                    txtNOMBRE_PROVEEDOR_INTEGRACION.Clear();
                    txtNIT_PROVEEDOR_INTEGRACION.Clear();
                    MostrarValidacion($"No se encontró el proveedor con código '{codigo}'.");
                    txtCODIPROVEEDOR.Focus();
                    txtCODIPROVEEDOR.SelectAll();
                    return;
                }

                string nitRelacionado = ObtenerNITIntegracion(fila);
                if (validarNit && !ValidarNitRelacionado("Productor", nitRelacionado, txtCODIPROVEEDOR))
                {
                    LimpiarProductorRelacionado();
                    return;
                }

                txtCODIPROVEEDOR.Text = fila["CODIGO"].ToString();
                txtNOMBRE_PROVEEDOR_INTEGRACION.Text = fila["NOMBRE"].ToString();
                txtNIT_PROVEEDOR_INTEGRACION.Text = nitRelacionado;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error buscando proveedor: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Registra la búsqueda genérica de transportistas de INJIBOA.
        /// El procedimiento utiliza la acción TRASPORTISTA por contrato.
        /// </summary>
        private void RegistrarBusquedaTransportistaIntegracion()
        {
            var config = new BusquedaConfig
            {
                StoredProcedure = "[EGENERALES].[SP_PROVEEDOR_INTEGRACION]",
                Accion = "TRASPORTISTA",
                NombreParametroAccion = "ACTION",
                Columnas = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "CODIGO", "CÓDIGO" },
                    { "NOMBRE", "NOMBRE" },
                    { "NIT", "NIT" }
                },
                Anchos = new System.Collections.Generic.Dictionary<string, int>
                {
                    { "CODIGO", 70 },
                    { "NOMBRE", 420 },
                    { "NIT", 140 }
                }
            };

            FormHelper.RegistrarBusqueda(txtCODTRANSPORT, config, fila =>
            {
                string nitRelacionado = ObtenerNITIntegracion(fila);
                if (!ValidarNitRelacionado("Transportista", nitRelacionado, txtCODTRANSPORT))
                {
                    LimpiarTransportistaRelacionado();
                    return;
                }

                txtCODTRANSPORT.Text = fila["CODIGO"].ToString();
                txtNOMBRE_TRANSPORTISTA_INTEGRACION.Text = fila["NOMBRE"].ToString();
                txtNIT_TRANSPORTISTA_INTEGRACION.Text = nitRelacionado;
            });

            txtCODTRANSPORT.Leave += (s, e) => BuscarTransportistaIntegracionPorCodigo();
        }

        private void BuscarTransportistaIntegracionPorCodigo(bool validarNit = true)
        {
            string codigo = txtCODTRANSPORT.Text.Trim();
            if (string.IsNullOrEmpty(codigo) || codigo == "*")
            {
                if (string.IsNullOrEmpty(codigo))
                {
                    txtNOMBRE_TRANSPORTISTA_INTEGRACION.Clear();
                    txtNIT_TRANSPORTISTA_INTEGRACION.Clear();
                }
                return;
            }

            try
            {
                DataTable dt = _dal.EjecutarConsulta(
                    "[EGENERALES].[SP_PROVEEDOR_INTEGRACION]",
                    new { ACTION = "TRASPORTISTA", FILTRO = codigo });

                DataRow fila = dt?.AsEnumerable().FirstOrDefault(r =>
                    string.Equals(r["CODIGO"]?.ToString()?.Trim(), codigo,
                        StringComparison.OrdinalIgnoreCase));

                if (fila == null)
                {
                    txtNOMBRE_TRANSPORTISTA_INTEGRACION.Clear();
                    txtNIT_TRANSPORTISTA_INTEGRACION.Clear();
                    MostrarValidacion($"No se encontró el transportista con código '{codigo}'.");
                    txtCODTRANSPORT.Focus();
                    txtCODTRANSPORT.SelectAll();
                    return;
                }

                string nitRelacionado = ObtenerNITIntegracion(fila);
                if (validarNit && !ValidarNitRelacionado("Transportista", nitRelacionado, txtCODTRANSPORT))
                {
                    LimpiarTransportistaRelacionado();
                    return;
                }

                txtCODTRANSPORT.Text = fila["CODIGO"].ToString();
                txtNOMBRE_TRANSPORTISTA_INTEGRACION.Text = fila["NOMBRE"].ToString();
                txtNIT_TRANSPORTISTA_INTEGRACION.Text = nitRelacionado;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error buscando transportista: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Registra la búsqueda genérica de proveedores de carga de INJIBOA.
        /// </summary>
        private void RegistrarBusquedaCargadoraIntegracion()
        {
            var config = new BusquedaConfig
            {
                StoredProcedure = "[EGENERALES].[SP_PROVEEDOR_INTEGRACION]",
                Accion = "CARGADORA",
                NombreParametroAccion = "ACTION",
                Columnas = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "CODIGO", "CÓDIGO" },
                    { "NOMBRE", "NOMBRE" },
                    { "NIT", "NIT" }
                },
                Anchos = new System.Collections.Generic.Dictionary<string, int>
                {
                    { "CODIGO", 140 },
                    { "NOMBRE", 420 },
                    { "NIT", 140 }
                }
            };

            FormHelper.RegistrarBusqueda(txtID_CARGADORA, config, fila =>
            {
                string nitRelacionado = ObtenerNITIntegracion(fila);
                if (!ValidarNitRelacionado("Cargadora", nitRelacionado, txtID_CARGADORA))
                {
                    LimpiarCargadoraRelacionada();
                    return;
                }

                txtID_CARGADORA.Text = fila["CODIGO"].ToString();
                txtNOMBRE_CARGADORA_INTEGRACION.Text = fila["NOMBRE"].ToString();
                txtNIT_CARGADORA_INTEGRACION.Text = nitRelacionado;
            });

            txtID_CARGADORA.Leave += (s, e) => BuscarCargadoraIntegracionPorCodigo();
        }

        private void BuscarCargadoraIntegracionPorCodigo(bool validarNit = true)
        {
            string codigo = txtID_CARGADORA.Text.Trim();
            if (string.IsNullOrEmpty(codigo) || codigo == "*")
            {
                if (string.IsNullOrEmpty(codigo))
                {
                    txtNOMBRE_CARGADORA_INTEGRACION.Clear();
                    txtNIT_CARGADORA_INTEGRACION.Clear();
                }
                return;
            }

            try
            {
                DataTable dt = _dal.EjecutarConsulta(
                    "[EGENERALES].[SP_PROVEEDOR_INTEGRACION]",
                    new { ACTION = "CARGADORA", FILTRO = codigo });

                DataRow fila = dt?.AsEnumerable().FirstOrDefault(r =>
                    string.Equals(r["CODIGO"]?.ToString()?.Trim(), codigo,
                        StringComparison.OrdinalIgnoreCase));

                if (fila == null)
                {
                    txtNOMBRE_CARGADORA_INTEGRACION.Clear();
                    txtNIT_CARGADORA_INTEGRACION.Clear();
                    MostrarValidacion($"No se encontró la cargadora con código '{codigo}'.");
                    txtID_CARGADORA.Focus();
                    txtID_CARGADORA.SelectAll();
                    return;
                }

                string nitRelacionado = ObtenerNITIntegracion(fila);
                if (validarNit && !ValidarNitRelacionado("Cargadora", nitRelacionado, txtID_CARGADORA))
                {
                    LimpiarCargadoraRelacionada();
                    return;
                }

                txtID_CARGADORA.Text = fila["CODIGO"].ToString();
                txtNOMBRE_CARGADORA_INTEGRACION.Text = fila["NOMBRE"].ToString();
                txtNIT_CARGADORA_INTEGRACION.Text = nitRelacionado;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error buscando cargadora: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegistrarBusquedaRozaIntegracion()
        {
            RegistrarBusquedaRelacionada(
                txtID_PROVEEDOR_ROZA,
                txtNOMBRE_ROZA_INTEGRACION,
                txtNIT_ROZA_INTEGRACION,
                "ROZA",
                "Roza",
                LimpiarRozaRelacionada);
        }

        private void RegistrarBusquedaQuerqueoIntegracion()
        {
            RegistrarBusquedaRelacionada(
                txtID_PROVEE_QQ,
                txtNOMBRE_QUERQUEO_INTEGRACION,
                txtNIT_QUERQUEO_INTEGRACION,
                "QUERQUEO",
                "Querqueo",
                LimpiarQuerqueoRelacionado);
        }

        private void RegistrarBusquedaRelacionada(
            TextBox campoCodigo,
            TextBox campoNombre,
            TextBox campoNit,
            string accion,
            string tipoRelacionado,
            Action limpiarCampos)
        {
            var config = new BusquedaConfig
            {
                StoredProcedure = "[EGENERALES].[SP_PROVEEDOR_INTEGRACION]",
                Accion = accion,
                NombreParametroAccion = "ACTION",
                Columnas = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "CODIGO", "CÓDIGO" },
                    { "NOMBRE", "NOMBRE" },
                    { "NIT", "NIT" }
                },
                Anchos = new System.Collections.Generic.Dictionary<string, int>
                {
                    { "CODIGO", 70 },
                    { "NOMBRE", 420 },
                    { "NIT", 140 }
                }
            };

            FormHelper.RegistrarBusqueda(campoCodigo, config, fila =>
            {
                string nitRelacionado = ObtenerNITIntegracion(fila);
                if (!ValidarNitRelacionado(tipoRelacionado, nitRelacionado, campoCodigo))
                {
                    limpiarCampos();
                    return;
                }

                campoCodigo.Text = fila["CODIGO"].ToString();
                campoNombre.Text = fila["NOMBRE"].ToString();
                campoNit.Text = nitRelacionado;
            });

            campoCodigo.Leave += (s, e) => BuscarIntegracionRelacionadaPorCodigo(
                campoCodigo,
                campoNombre,
                campoNit,
                accion,
                tipoRelacionado,
                limpiarCampos);
        }

        private void BuscarIntegracionRelacionadaPorCodigo(
            TextBox campoCodigo,
            TextBox campoNombre,
            TextBox campoNit,
            string accion,
            string tipoRelacionado,
            Action limpiarCampos,
            bool validarNit = true)
        {
            string codigo = campoCodigo.Text.Trim();
            if (string.IsNullOrEmpty(codigo) || codigo == "*")
            {
                if (string.IsNullOrEmpty(codigo))
                {
                    campoNombre.Clear();
                    campoNit.Clear();
                }
                return;
            }

            try
            {
                DataTable dt = _dal.EjecutarConsulta(
                    "[EGENERALES].[SP_PROVEEDOR_INTEGRACION]",
                    new { ACTION = accion, FILTRO = codigo });

                DataRow fila = dt?.AsEnumerable().FirstOrDefault(r =>
                    string.Equals(r["CODIGO"]?.ToString()?.Trim(), codigo,
                        StringComparison.OrdinalIgnoreCase));

                if (fila == null)
                {
                    campoNombre.Clear();
                    campoNit.Clear();
                    MostrarValidacion($"No se encontró el registro de {tipoRelacionado} con código '{codigo}'.");
                    campoCodigo.Focus();
                    campoCodigo.SelectAll();
                    return;
                }

                string nitRelacionado = ObtenerNITIntegracion(fila);
                if (validarNit && !ValidarNitRelacionado(tipoRelacionado, nitRelacionado, campoCodigo))
                {
                    limpiarCampos();
                    return;
                }

                campoCodigo.Text = fila["CODIGO"].ToString();
                campoNombre.Text = fila["NOMBRE"].ToString();
                campoNit.Text = nitRelacionado;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error buscando {tipoRelacionado}: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string ObtenerNITIntegracion(DataRow fila)
        {
            if (fila == null || !fila.Table.Columns.Contains("NIT") || fila["NIT"] == DBNull.Value)
                return "";

            return fila["NIT"].ToString().Trim();
        }

        private bool ValidarNitRelacionado(string tipoRelacionado, string nitRelacionado, TextBox campoCodigo)
        {
            string nitClienteNormalizado = NormalizarNit(txtNIT.Text);
            string nitRelacionadoNormalizado = NormalizarNit(nitRelacionado);

            if (string.IsNullOrEmpty(nitClienteNormalizado))
            {
                XtraMessageBox.Show(
                    $"Debe ingresar el NIT del cliente antes de seleccionar {tipoRelacionado}.",
                    "Validación de NIT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNIT.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(nitRelacionadoNormalizado))
            {
                XtraMessageBox.Show(
                    $"El registro de {tipoRelacionado} seleccionado no tiene NIT y no puede asociarse al cliente.",
                    "Validación de NIT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                campoCodigo.Focus();
                return false;
            }

            if (!string.Equals(nitClienteNormalizado, nitRelacionadoNormalizado,
                    StringComparison.OrdinalIgnoreCase))
            {
                XtraMessageBox.Show(
                    $"El NIT de {tipoRelacionado} ({nitRelacionado}) no coincide con el NIT del cliente ({txtNIT.Text.Trim()}).",
                    "Validación de NIT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                campoCodigo.Focus();
                return false;
            }

            return true;
        }

        private static string NormalizarNit(string nit)
        {
            if (string.IsNullOrWhiteSpace(nit))
                return "";

            return new string(nit.Where(char.IsLetterOrDigit).ToArray()).ToUpperInvariant();
        }

        private void LimpiarProductorRelacionado()
        {
            txtCODIPROVEEDOR.Clear();
            txtNOMBRE_PROVEEDOR_INTEGRACION.Clear();
            txtNIT_PROVEEDOR_INTEGRACION.Clear();
        }

        private void LimpiarTransportistaRelacionado()
        {
            txtCODTRANSPORT.Clear();
            txtNOMBRE_TRANSPORTISTA_INTEGRACION.Clear();
            txtNIT_TRANSPORTISTA_INTEGRACION.Clear();
        }

        private void LimpiarCargadoraRelacionada()
        {
            txtID_CARGADORA.Clear();
            txtNOMBRE_CARGADORA_INTEGRACION.Clear();
            txtNIT_CARGADORA_INTEGRACION.Clear();
        }

        private void LimpiarRozaRelacionada()
        {
            txtID_PROVEEDOR_ROZA.Clear();
            txtNOMBRE_ROZA_INTEGRACION.Clear();
            txtNIT_ROZA_INTEGRACION.Clear();
        }

        private void LimpiarQuerqueoRelacionado()
        {
            txtID_PROVEE_QQ.Clear();
            txtNOMBRE_QUERQUEO_INTEGRACION.Clear();
            txtNIT_QUERQUEO_INTEGRACION.Clear();
        }
        /// <summary>
        /// Busca la cuenta contable por su código (CUENTA) al perder el foco.
        /// Mismo comportamiento que BuscarCuentaContablePorCodigo en frmProveedor.
        /// </summary>
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
                DataTable dt = _dal.EjecutarConsulta("SP_CATALOGO_CUENTA",
                    new { ACCION = "OBTENER", CUENTA = codigo });
                if (dt == null || dt.Rows.Count == 0)
                {
                    MostrarValidacion($"La cuenta contable '{codigo}' no existe.");
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
        /// <summary>
        /// Resuelve el nombre de una cuenta contable ya guardada (carga silenciosa,
        /// sin validaciones), para mostrarla en el campo de solo lectura al editar.
        /// </summary>
        private string ObtenerNombreCuenta(string cuenta)
        {
            if (string.IsNullOrWhiteSpace(cuenta)) return "";
            try
            {
                DataTable dt = _dal.EjecutarConsulta("SP_CATALOGO_CUENTA",
                    new { ACCION = "OBTENER", CUENTA = cuenta });
                if (dt != null && dt.Rows.Count > 0)
                    return dt.Rows[0]["NOMBRE_CUENTA"]?.ToString() ?? "";
            }
            catch { }
            return "";
        }
        private void CargarOrigen()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_ORIGEN_ENTIDAD]",
                new { ACCION = "LISTAR" });
            AgregarFilaVacia(dt, "NOMBRE_ORIGEN");
            cbxORIGEN.DataSource = dt;
            cbxORIGEN.ValueMember = "ID_ORIGEN";
            cbxORIGEN.DisplayMember = "NOMBRE_ORIGEN";
            cbxORIGEN.SelectedIndex = 0;
        }
        private void cbxDEPTO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando) return;
            string codiDepto = ObtenerCodigoCombo(cbxDEPTO);
            if (!string.IsNullOrEmpty(codiDepto))
                CargarMunicipios(codiDepto);
            else
            {
                cbxMUNI.DataSource = null;
                cbxMUNI.Items.Clear();
                cbxDIST.DataSource = null;
                cbxDIST.Items.Clear();
            }
        }
        private void cbxMUNI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando) return;
            string codiMuni = ObtenerCodigoCombo(cbxMUNI);
            SetDistritoByMunicipio(codiMuni);
        }
        #endregion
        #region === GRID ROLES ===
        private void InicializarGridRoles()
        {
            _dtRoles = new DataTable();
            _dtRoles.Columns.Add("ID_ENTIDAD_TPC", typeof(int));
            _dtRoles.Columns.Add("ID_ENTIDAD", typeof(int));
            _dtRoles.Columns.Add("ID_TIPO_CLIENTE", typeof(int));
            _dtRoles.Columns.Add("NOMBRE_TIPO_CLIENTE", typeof(string));
            _dtRoles.Columns.Add("ACTIVO", typeof(bool));
            _dtRoles.Columns.Add("FECHA_ASIGNACION", typeof(DateTime));
            gridRoles.DataSource = _dtRoles;
            var view = gridRoles.MainView as GridView;
            if (view == null) return;
            view.Columns.Clear();
            view.PopulateColumns();
            // Ocultar claves
            OcultarColumna(view, "ID_ENTIDAD_TPC");
            OcultarColumna(view, "ID_ENTIDAD");
            OcultarColumna(view, "ID_TIPO_CLIENTE");
            // Columna: Tipo Cliente (búsqueda genérica con "*")
            var colRol = view.Columns["NOMBRE_TIPO_CLIENTE"];
            if (colRol != null)
            {
                colRol.Caption = "Tipo Cliente";
                colRol.Width = 250;
                colRol.Visible = true;
                colRol.VisibleIndex = 0;
                colRol.OptionsColumn.AllowEdit = true;
                var repoTexto = new RepositoryItemTextEdit();
                repoTexto.KeyDown += (s, ev) =>
                {
                    if (ev.KeyCode != Keys.Enter) return;
                    var editor = s as TextEdit;
                    if (editor == null) return;
                    if (editor.Text?.Trim() != "*") return;
                    ev.Handled = true;
                    AbrirBusquedaTipoClienteGrid(view);
                };
                gridRoles.RepositoryItems.Add(repoTexto);
                colRol.ColumnEdit = repoTexto;
            }
            // Columna: Activo
            ConfigurarColumnaCheckBox(view, "ACTIVO", "Activo", 55);
            // Columna: Fecha asignación
            var colFecha = view.Columns["FECHA_ASIGNACION"];
            if (colFecha != null)
            {
                colFecha.Caption = "Fecha Asignación";
                colFecha.Width = 130;
                colFecha.Visible = true;
                colFecha.VisibleIndex = 2;
                colFecha.OptionsColumn.AllowEdit = true;
                colFecha.DisplayFormat.FormatString = "dd/MM/yyyy";
                colFecha.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                var repoFecha = new RepositoryItemDateEdit();
                repoFecha.DisplayFormat.FormatString = "dd/MM/yyyy";
                repoFecha.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridRoles.RepositoryItems.Add(repoFecha);
                colFecha.ColumnEdit = repoFecha;
            }
            // Columna: Eliminar
            var colEliminar = view.Columns.AddField("ELIMINAR");
            colEliminar.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            colEliminar.Caption = " ";
            colEliminar.Width = 36;
            colEliminar.Visible = true;
            colEliminar.VisibleIndex = 3;
            colEliminar.OptionsColumn.AllowEdit = true;
            colEliminar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            var repoEliminar = new RepositoryItemButtonEdit();
            repoEliminar.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            repoEliminar.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            repoEliminar.Buttons[0].ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.eliminarFila32x32;
            repoEliminar.Buttons[0].Caption = "";
            repoEliminar.Buttons[0].ToolTip = "Eliminar tipo de cliente";
            repoEliminar.ButtonClick += (s, ev) => EliminarRol();
            gridRoles.RepositoryItems.Add(repoEliminar);
            colEliminar.ColumnEdit = repoEliminar;
            // Apariencia
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsBehavior.Editable = true;
            view.OptionsBehavior.AutoSelectAllInEditor = true;
            view.OptionsNavigation.EnterMoveNextColumn = true;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.OptionsSelection.EnableAppearanceFocusedRow = true;
            view.Appearance.FocusedRow.BackColor = Color.FromArgb(204, 229, 255);
            view.Appearance.FocusedRow.Options.UseBackColor = true;
            view.Appearance.HideSelectionRow.BackColor = Color.FromArgb(204, 229, 255);
            view.Appearance.HideSelectionRow.Options.UseBackColor = true;
            view.Appearance.Row.ForeColor = Color.Black;
            view.Appearance.Row.Font = new Font("Segoe UI", 9f);
            view.Appearance.Row.Options.UseFont = true;
            view.Appearance.HeaderPanel.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            view.Appearance.HeaderPanel.Options.UseFont = true;
        }
        /// <summary>
        /// Abre el formulario de búsqueda genérica de Tipo de Cliente para la
        /// celda enfocada del grid. SP_TIPO_CLIENTE ya filtra por @FILTRO
        /// dentro de la propia acción LISTAR.
        /// </summary>
        private void AbrirBusquedaTipoClienteGrid(GridView view)
        {
            var config = new BusquedaConfig
            {
                StoredProcedure = "[EMH].[SP_TIPO_CLIENTE]",
                Accion = "LISTAR",
                Columnas = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "NOMBRE_TIPO_CLIENTE", "TIPO DE CLIENTE" }
                },
                Anchos = new System.Collections.Generic.Dictionary<string, int>
                {
                    { "NOMBRE_TIPO_CLIENTE", 300 }
                }
            };
            using (var frm = new frmBusquedaGenerica(config))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                if (frm.ShowDialog() == DialogResult.OK && frm.FilaSeleccionada != null)
                {
                    view.SetFocusedRowCellValue("ID_TIPO_CLIENTE",
                        Convert.ToInt32(frm.FilaSeleccionada["ID_TIPO_CLIENTE"]));
                    view.SetFocusedRowCellValue("NOMBRE_TIPO_CLIENTE", frm.FilaSeleccionada["NOMBRE_TIPO_CLIENTE"].ToString());
                }
                else
                {
                    view.SetFocusedRowCellValue("NOMBRE_TIPO_CLIENTE", "");
                }
            }
        }
        private void ConfigurarColumnaCheckBox(GridView view, string field, string caption, int width)
        {
            var col = view.Columns.ColumnByFieldName(field);
            if (col == null) return;
            col.Caption = caption;
            col.Width = width;
            col.Visible = true;
            col.OptionsColumn.AllowEdit = true;
            var repo = new RepositoryItemCheckEdit();
            repo.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard;
            gridRoles.RepositoryItems.Add(repo);
            col.ColumnEdit = repo;
        }
        private void OcultarColumna(GridView view, string field)
        {
            var col = view.Columns.ColumnByFieldName(field);
            if (col != null) col.Visible = false;
        }
        private void CargarEntidadClienteExistente(int idEntidad)
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_ENTIDAD_CLIENTE]",
                new { ACCION = "CONSULTAR", ID_ENTIDAD = idEntidad });
            if (dt == null || dt.Rows.Count == 0) return;
            DataRow r = dt.Rows[0];
            txtDIAS_PLAZO.Text = r["DIAS_PLAZO"] == DBNull.Value ? "" : r["DIAS_PLAZO"].ToString();
            txtCUENTA_X_COBRAR.Text = r["CUENTA_X_COBRAR"] == DBNull.Value ? "" : r["CUENTA_X_COBRAR"].ToString();
            txtNOMBRE_CUENTA_X_COBRAR.Text = ObtenerNombreCuenta(txtCUENTA_X_COBRAR.Text);
            _idTipoPrecioSeleccionado = r["ID_TIPO_PRECIO"] == DBNull.Value ? (int?)null : Convert.ToInt32(r["ID_TIPO_PRECIO"]);
            txtID_TIPO_PRECIO.Text = ObtenerDescripcionTipoPrecio(_idTipoPrecioSeleccionado);
        }
        private void CargarRolesExistentes(int idEntidad)
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_ENTIDAD_TIPO_CLIENTE]",
                new { ACCION = "LISTAR", ID_ENTIDAD = idEntidad });
            _dtRoles.Clear();
            if (dt == null) return;
            foreach (DataRow row in dt.Rows)
            {
                var f = _dtRoles.NewRow();
                f["ID_ENTIDAD_TPC"] = row["ID_ENTIDAD_TPC"];
                f["ID_ENTIDAD"] = row["ID_ENTIDAD"];
                f["ID_TIPO_CLIENTE"] = row["ID_TIPO_CLIENTE"] == DBNull.Value
                                     ? (object)DBNull.Value
                                     : Convert.ToInt32(row["ID_TIPO_CLIENTE"]);
                f["NOMBRE_TIPO_CLIENTE"] = row["NOMBRE_TIPO_CLIENTE"] == DBNull.Value
                                     ? ""
                                     : row["NOMBRE_TIPO_CLIENTE"].ToString();
                f["ACTIVO"] = row["ACTIVO"] == DBNull.Value
                                         ? true
                                         : Convert.ToBoolean(row["ACTIVO"]);
                f["FECHA_ASIGNACION"] = row["FECHA_ASIGNACION"] == DBNull.Value
                                         ? (object)DBNull.Value
                                         : Convert.ToDateTime(row["FECHA_ASIGNACION"]);
                _dtRoles.Rows.Add(f);
            }
        }
        private void AgregarRol()
        {
            var fila = _dtRoles.NewRow();
            fila["ID_ENTIDAD_TPC"] = 0;
            fila["ID_ENTIDAD"] = IdEntidad;
            fila["ID_TIPO_CLIENTE"] = DBNull.Value;
            fila["NOMBRE_TIPO_CLIENTE"] = "";
            fila["ACTIVO"] = true;
            fila["FECHA_ASIGNACION"] = DateTime.Today;
            _dtRoles.Rows.Add(fila);
        }
        private void EliminarRol()
        {
            var view = gridRoles.MainView as GridView;
            if (view == null) return;
            int fila = view.FocusedRowHandle;
            if (fila < 0) return;
            if (MessageBox.Show("¿Desea eliminar este tipo de cliente de la entidad?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            object valId = view.GetRowCellValue(fila, "ID_ENTIDAD_TPC");
            if (valId != null && valId != DBNull.Value)
            {
                int idRol = Convert.ToInt32(valId);
                if (idRol > 0)
                {
                    _dal.EjecutarSinRetorno("[EMH].[SP_ENTIDAD_TIPO_CLIENTE]", new
                    {
                        ACCION = "ELIMINAR",
                        ID_ENTIDAD_TPC = idRol
                    });
                }
            }
            view.DeleteRow(fila);
        }
        #endregion
        #region === CARGAR ENTIDAD EXISTENTE ===
        private void CargarEntidadExistente(int idEntidad)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DataTable dtEntidad = _dal.EjecutarConsulta("[EMH].[SP_ENTIDAD]",
                    new { ACCION = "CONSULTAR", ID_ENTIDAD = idEntidad });
                if (dtEntidad == null || dtEntidad.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No se encontró la entidad solicitada.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    return;
                }
                DataRow r = dtEntidad.Rows[0];
                IdEntidad = Convert.ToInt32(r["ID_ENTIDAD"]);
                txtCODIGO_ENTIDAD.Text = AsString(r["CODIGO_ENTIDAD"]);
                txtNOMBRE.Text = AsString(r["NOMBRE"]);
                txtNOMBRE_COMERCIAL.Text = AsString(r["NOMBRE_COMERCIAL"]);
                txtDUI.Text = AsString(r["DUI"]);
                txtPROFESION.Text = AsString(r["PROFESION"]);
                txtNIT.Text = AsString(r["NIT"]);
                txtDOCUMENTO.Text = AsString(r["DOCUMENTO"]);
                txtCORREO.Text = AsString(r["CORREO"]);
                txtCORREO_CC.Text = AsString(r["CORREO_CC"]);
                txtCELULAR.Text = AsString(r["CELULAR"]);
                txtTELEFONO.Text = AsString(r["TELEFONO"]);
                txtDIAS_PLAZO.Text = r["DIAS_PLAZO"] == DBNull.Value ? "" : r["DIAS_PLAZO"].ToString();
                txtCOMPLEMENTO.Text = AsString(r["COMPLEMENTO"]);
                txtACTIVIDAD_EXT.Text = AsString(r["ACTIVIDAD_EXT"]);
                txtCODIPROVEEDOR.Text = AsString(r["CODIPROVEEDOR"]);
                BuscarProveedorIntegracionPorCodigo(validarNit: false);
                txtCODTRANSPORT.Text = r["CODTRANSPORT"] == DBNull.Value ? "" : r["CODTRANSPORT"].ToString();
                BuscarTransportistaIntegracionPorCodigo(validarNit: false);
                txtID_CARGADORA.Text = r["ID_CARGADORA"] == DBNull.Value ? "" : r["ID_CARGADORA"].ToString();
                BuscarCargadoraIntegracionPorCodigo(validarNit: false);
                CargarCodigosIntegracion(idEntidad);
                // Combos
                SetComboById(cbxTIPO_ENTIDAD, AsInt(r["ID_TIPO_ENTIDAD"]));
                SetComboById(cbxTIPO_CONTRIB, AsInt(r["ID_TIPO_CONTRIB"]));
                SetComboById(cbxTIPO_DOC_IDEN, AsInt(r["ID_TIPO_DOC_INDEN"]));
                SetComboById(cbxPAIS, AsInt(r["ID_PAIS"]));
                _idActividad1 = AsInt(r["ID_ACTIVIDAD_1"]) ?? 0;
                _idActividad2 = AsInt(r["ID_ACTIVIDAD_2"]) ?? 0;
                _idActividad3 = AsInt(r["ID_ACTIVIDAD_3"]) ?? 0;
                CargarActividadPorId(_idActividad1, txtCODI_ACTIVIDAD1, txtACTIVIDAD_1);
                CargarActividadPorId(_idActividad2, txtCODI_ACTIVIDAD2, txtACTIVIDAD_2);
                CargarActividadPorId(_idActividad3, txtCODI_ACTIVIDAD3, txtACTIVIDAD_3);
                SetComboById(cbxORIGEN, AsInt(r["ID_ORIGEN"]));
                // El NRC se restaura después de fijar Origen y Tipo Contribuyente: ambos combos
                // disparan ActualizarEstadoNRC() (que limpia txtNRC si aún no coinciden entre sí),
                // por lo que asignarlo antes de que ambos queden fijados se perdía durante la carga.
                txtNRC.Text = AsString(r["NRC"]);
                // Departamento → Municipio → Distrito
                string codiDepto = AsString(r["CODI_DEPTO"]);
                string codiMuni = AsString(r["CODI_MUNI"]);
                if (!string.IsNullOrEmpty(codiDepto))
                {
                    _cargando = true;
                    cbxDEPTO.SelectedValue = codiDepto;
                    CargarMunicipios(codiDepto);
                    if (!string.IsNullOrEmpty(codiMuni))
                    {
                        cbxMUNI.SelectedValue = codiMuni;
                        SetDistritoByMunicipio(codiMuni);
                    }
                    _cargando = false;
                }
                CargarEntidadClienteExistente(idEntidad);
                CargarRolesExistentes(idEntidad);
                ConfigurarBotones(esNuevo: false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al cargar la entidad:\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion
        #region === GUARDAR ===
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;
            try
            {
                Cursor = Cursors.WaitCursor;
                var dtResult = _dal.EjecutarConsulta("[EMH].[SP_ENTIDAD]", new
                {
                    ACCION = "GUARDAR",
                    ID_ENTIDAD = IdEntidad,
                    CODIGO_ENTIDAD = NullIfEmpty(txtCODIGO_ENTIDAD.Text),
                    NOMBRE = txtNOMBRE.Text.Trim(),
                    NOMBRE_COMERCIAL = NullIfEmpty(txtNOMBRE_COMERCIAL.Text),
                    ID_TIPO_ENTIDAD = ObtenerIdCombo(cbxTIPO_ENTIDAD),
                    ID_TIPO_CONTRIB = ObtenerIdCombo(cbxTIPO_CONTRIB),
                    ID_TIPO_DOC_INDEN = ObtenerIdCombo(cbxTIPO_DOC_IDEN),
                    NRC = NullIfEmpty(txtNRC.Text),
                    PROFESION = NullIfEmpty(txtPROFESION.Text),
                    DUI = NullIfEmpty(txtDUI.Text),
                    NIT = NullIfEmpty(txtNIT.Text),
                    DOCUMENTO = NullIfEmpty(txtDOCUMENTO.Text),
                    CORREO = NullIfEmpty(txtCORREO.Text),
                    CORREO_CC = NullIfEmpty(txtCORREO_CC.Text),
                    CELULAR = NullIfEmpty(txtCELULAR.Text),
                    TELEFONO = NullIfEmpty(txtTELEFONO.Text),
                    DIAS_PLAZO = ParseInt(txtDIAS_PLAZO.Text),
                    COMPLEMENTO = NullIfEmpty(txtCOMPLEMENTO.Text),
                    ID_PAIS = ObtenerIdCombo(cbxPAIS),
                    CODI_DEPTO = ObtenerCodigoCombo(cbxDEPTO),
                    CODI_MUNI = ObtenerCodigoCombo(cbxMUNI),
                    ID_ACTIVIDAD_1 = _idActividad1 > 0 ? _idActividad1 : (int?)null,
                    ID_ACTIVIDAD_2 = _idActividad2 > 0 ? _idActividad2 : (int?)null,
                    ID_ACTIVIDAD_3 = _idActividad3 > 0 ? _idActividad3 : (int?)null,
                    ID_ORIGEN = ObtenerIdCombo(cbxORIGEN),
                    CODIPROVEEDOR = NullIfEmpty(txtCODIPROVEEDOR.Text),
                    CODTRANSPORT = ParseIntNull(txtCODTRANSPORT.Text),
                    ID_CARGADORA = ParseIntNull(txtID_CARGADORA.Text),
                    ACTIVIDAD_EXT = NullIfEmpty(txtACTIVIDAD_EXT.Text),
                    USER = Configuracion.UsuarioActual
                });
                if (dtResult == null || dtResult.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Error al guardar la entidad.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                IdEntidad = Convert.ToInt32(dtResult.Rows[0]["ID_GENERADO"]);
                GuardarCodigosIntegracion(IdEntidad);
                GuardarRolCliente(IdEntidad);
                GuardarEntidadCliente();
                GuardarRoles();
                XtraMessageBox.Show("Entidad guardada correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConfigurarBotones(esNuevo: false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error al guardar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void CargarCodigosIntegracion(int idEntidad)
        {
            DataTable dt = _dal.EjecutarConsulta("[EDTE].[SP_ENTIDAD]", new
            {
                ACCION = "OBTENER_INTEGRACION",
                ID_ENTIDAD = idEntidad
            });

            if (dt == null || dt.Rows.Count == 0)
            {
                LimpiarRozaRelacionada();
                LimpiarQuerqueoRelacionado();
                return;
            }

            DataRow fila = dt.Rows[0];
            txtID_PROVEEDOR_ROZA.Text = fila["ID_PROVEEDOR_ROZA"] == DBNull.Value
                ? ""
                : fila["ID_PROVEEDOR_ROZA"].ToString();
            BuscarIntegracionRelacionadaPorCodigo(
                txtID_PROVEEDOR_ROZA,
                txtNOMBRE_ROZA_INTEGRACION,
                txtNIT_ROZA_INTEGRACION,
                "ROZA",
                "Roza",
                LimpiarRozaRelacionada,
                validarNit: false);

            txtID_PROVEE_QQ.Text = fila["ID_PROVEE_QQ"] == DBNull.Value
                ? ""
                : fila["ID_PROVEE_QQ"].ToString();
            BuscarIntegracionRelacionadaPorCodigo(
                txtID_PROVEE_QQ,
                txtNOMBRE_QUERQUEO_INTEGRACION,
                txtNIT_QUERQUEO_INTEGRACION,
                "QUERQUEO",
                "Querqueo",
                LimpiarQuerqueoRelacionado,
                validarNit: false);
        }

        private void GuardarCodigosIntegracion(int idEntidad)
        {
            _dal.EjecutarSinRetorno("[EDTE].[SP_ENTIDAD]", new
            {
                ACCION = "GUARDAR_INTEGRACION",
                ID_ENTIDAD = idEntidad,
                ID_PROVEEDOR_ROZA = ParseIntNull(txtID_PROVEEDOR_ROZA.Text),
                ID_PROVEE_QQ = ParseIntNull(txtID_PROVEE_QQ.Text),
                USUARIO_ACT = Configuracion.UsuarioActual
            });
        }
        /// <summary>
        /// Da de alta (o reactiva) el rol 'CLI' para la entidad en dbo.ENTIDAD_ROL.
        /// SP_ENTIDAD.GUARDAR no maneja roles; el alta/activación del rol se hace
        /// aparte contra [EMH].[SP_ENTIDAD_ROL], que ya evita duplicar el mismo ROL.
        /// </summary>
        private void GuardarRolCliente(int idEntidad)
        {
            _dal.EjecutarSinRetorno("[EMH].[SP_ENTIDAD_ROL]", new
            {
                ACCION = "GUARDAR",
                ID_ENTIDAD_ROL = 0,
                ID_ENTIDAD = idEntidad,
                ROL = "CLI",
                ACTIVO = true,
                USUARIO = Configuracion.UsuarioActual
            });
        }
        private void GuardarEntidadCliente()
        {
            _dal.EjecutarSinRetorno("[EMH].[SP_ENTIDAD_CLIENTE]", new
            {
                ACCION = "GUARDAR",
                ID_ENTIDAD = IdEntidad,
                DIAS_PLAZO = ParseInt(txtDIAS_PLAZO.Text),
                CUENTA_X_COBRAR = NullIfEmpty(txtCUENTA_X_COBRAR.Text),
                ID_TIPO_PRECIO = _idTipoPrecioSeleccionado,
                USER = Configuracion.UsuarioActual
            });
        }
        private void GuardarRoles()
        {
            var view = gridRoles.MainView as GridView;
            view?.CloseEditor();
            view?.UpdateCurrentRow();
            foreach (DataRow fila in _dtRoles.Rows)
            {
                if (fila["ID_TIPO_CLIENTE"] == DBNull.Value) continue;
                _dal.EjecutarSinRetorno("[EMH].[SP_ENTIDAD_TIPO_CLIENTE]", new
                {
                    ACCION = "GUARDAR",
                    ID_ENTIDAD_TPC = fila["ID_ENTIDAD_TPC"] == DBNull.Value ? 0 : Convert.ToInt32(fila["ID_ENTIDAD_TPC"]),
                    ID_ENTIDAD = IdEntidad,
                    ID_TIPO_CLIENTE = Convert.ToInt32(fila["ID_TIPO_CLIENTE"]),
                    ACTIVO = fila["ACTIVO"] == DBNull.Value ? true : Convert.ToBoolean(fila["ACTIVO"]),
                    FECHA_ASIGNACION = fila["FECHA_ASIGNACION"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(fila["FECHA_ASIGNACION"]),
                    USUARIO = Configuracion.UsuarioActual
                });
            }
            CargarRolesExistentes(IdEntidad);
        }
        #endregion
        #region === ELIMINAR ENTIDAD ===
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (IdEntidad == 0)
            {
                LimpiarFormulario();
                return;
            }
            if (MessageBox.Show("¿Desea eliminar esta entidad?\nSe eliminarán también todos sus roles.",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            try
            {
                EliminarRolCliente(IdEntidad);
                if (!TieneAlgunOtroRolActivo(IdEntidad))
                {
                    // La entidad ya no tiene ningún rol activo (ni Proveedor, ni Cliente):
                    // se elimina por completo. Si aún queda otro rol (p.ej. PRO), se conserva
                    // la entidad y solo se dio de baja el rol de Cliente.
                    _dal.EjecutarSinRetorno("[EMH].[SP_ENTIDAD]", new
                    {
                        ACCION = "ELIMINAR",
                        ID_ENTIDAD = IdEntidad
                    });
                }
                XtraMessageBox.Show("Entidad eliminada correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error al eliminar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region === REGLAS DE CONTROLES ===
        private void cbxORIGEN_SelectedIndexChanged(object sender, EventArgs e)
        {
            string origen = cbxORIGEN.Text.Trim().ToUpper();
            bool esExterior = origen == "EXTERIOR";
            cbxTIPO_DOC_IDEN.Enabled = esExterior;
            txtDOCUMENTO.Enabled = esExterior;
            txtACTIVIDAD_EXT.Enabled = esExterior;
            // Actividad 1/2/3 (código + descripción) solo aplican para origen LOCAL
            bool esLocal = !esExterior;
            txtCODI_ACTIVIDAD1.Enabled = esLocal;
            txtACTIVIDAD_1.Enabled = esLocal;
            txtCODI_ACTIVIDAD2.Enabled = esLocal;
            txtACTIVIDAD_2.Enabled = esLocal;
            txtCODI_ACTIVIDAD3.Enabled = esLocal;
            txtACTIVIDAD_3.Enabled = esLocal;
            if (!esExterior)
            {
                cbxTIPO_DOC_IDEN.SelectedIndex = 0;
                txtDOCUMENTO.Clear();
                txtACTIVIDAD_EXT.Clear();
            }
            else
            {
                _idActividad1 = 0;
                _idActividad2 = 0;
                _idActividad3 = 0;
                txtCODI_ACTIVIDAD1.Text = "";
                txtACTIVIDAD_1.Text = "";
                txtCODI_ACTIVIDAD2.Text = "";
                txtACTIVIDAD_2.Text = "";
                txtCODI_ACTIVIDAD3.Text = "";
                txtACTIVIDAD_3.Text = "";
            }
            // Origen LOCAL implica País = EL SALVADOR (se autoselecciona)
            if (origen == "LOCAL")
            {
                SeleccionarPaisElSalvador();
            }
            // Origen EXTERIOR: si quedó EL SALVADOR seleccionado, se limpia
            else if (esExterior && cbxPAIS.Text.Trim().ToUpper() == "EL SALVADOR")
            {
                cbxPAIS.SelectedIndex = 0;
            }
            // NRC y NIT solo se habilitan para origen LOCAL
            txtNIT.Enabled = esLocal;
            if (!esLocal)
                txtNIT.Clear();
            ActualizarEstadoNRC();
        }
        /// <summary>
        /// El NRC solo se habilita cuando Origen = LOCAL y el tipo de contribuyente no es "No Contribuyente".
        /// Se invoca desde cbxORIGEN_SelectedIndexChanged y cbxTIPO_CONTRIB_SelectedIndexChanged
        /// para mantener ambas reglas sincronizadas sobre el mismo control.
        /// </summary>
        private void ActualizarEstadoNRC()
        {
            string origen = cbxORIGEN.Text.Trim().ToUpper();
            string contrib = cbxTIPO_CONTRIB.Text.Trim().ToUpper();
            bool esNoContribuyente = contrib.Contains("NO CONTRIBUYENTE");
            bool habilitar = origen == "LOCAL" && !esNoContribuyente;
            txtNRC.Enabled = habilitar;
            if (!habilitar)
                txtNRC.Clear();
        }
        private void cbxTIPO_ENTIDAD_SelectedIndexChanged(object sender, EventArgs e)
        {
            // El DUI es un documento de persona natural; se deshabilita para JURIDICA.
            string tipoPersona = cbxTIPO_ENTIDAD.Text.Trim().ToUpper();
            txtDUI.Enabled = tipoPersona != "JURIDICA";
        }
        /// <summary>
        /// Busca "EL SALVADOR" en el catálogo cargado de cbxPAIS y lo selecciona.
        /// Usado cuando Origen = LOCAL, ya que ese origen exige país El Salvador.
        /// </summary>
        private void SeleccionarPaisElSalvador()
        {
            if (cbxPAIS.Text.Trim().ToUpper() == "EL SALVADOR") return;
            if (cbxPAIS.DataSource is DataTable dtPaises)
            {
                DataRow[] filas = dtPaises.Select("VALORES = 'EL SALVADOR'");
                if (filas.Length > 0)
                {
                    cbxPAIS.SelectedValue = filas[0]["ID_PAIS"];
                }
            }
        }
        private void cbxTIPO_CONTRIB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string contrib = cbxTIPO_CONTRIB.Text.Trim().ToUpper();
            bool esNoContribuyente = contrib.Contains("NO CONTRIBUYENTE");
            string tipoPersona = cbxTIPO_ENTIDAD.Text.Trim().ToUpper();
            if (esNoContribuyente && tipoPersona == "JURIDICA")
            {
                XtraMessageBox.Show("No se puede seleccionar No Contribuyente cuando el tipo persona es 'JURIDICO'.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxTIPO_CONTRIB.SelectedIndex = 0;
                return;
            }
            ActualizarEstadoNRC();
        }
        #endregion
        #region === VALIDAR ===
        private bool ValidarCampos()
        {
            // Limpia errores previos de una validación anterior
            Control[] controlesValidables =
            {
                txtNOMBRE, cbxTIPO_CONTRIB, txtDUI, txtNIT, txtNRC,
                cbxTIPO_DOC_IDEN, txtDOCUMENTO, cbxPAIS, txtNOMBRE_COMERCIAL,
                txtTELEFONO, txtCELULAR, txtCORREO, txtCOMPLEMENTO,
                cbxDEPTO, cbxMUNI, cbxDIST, txtCODI_ACTIVIDAD1
            };
            foreach (Control c in controlesValidables)
                errorProvider1.SetError(c, "");

            bool esValido = true;
            Control primerControlError = null;
            void MarcarError(Control control, string mensaje)
            {
                errorProvider1.SetError(control, mensaje);
                esValido = false;
                if (primerControlError == null) primerControlError = control;
            }

            if (string.IsNullOrWhiteSpace(txtNOMBRE.Text))
                MarcarError(txtNOMBRE, "El nombre de la entidad es obligatorio.");

            // Validación por Tipo Persona
            string tipoPersona = cbxTIPO_ENTIDAD.Text.Trim().ToUpper();
            string tipoContrib = cbxTIPO_CONTRIB.Text.Trim().ToUpper();
            bool esNoContrib = tipoContrib.Contains("NO CONTRIBUYENTE");
            string origen = cbxORIGEN.Text.Trim().ToUpper();
            if (tipoPersona == "JURIDICA" && esNoContrib)
                MarcarError(cbxTIPO_CONTRIB, "Se debe seleccionar un tipo de contribuyente diferente.");

            if (tipoPersona == "NATURAL")
            {
                if (string.IsNullOrWhiteSpace(txtDUI.Text))
                    MarcarError(txtDUI, "El DUI es obligatorio para personas naturales.");
                // NIT solo aplica (y solo está habilitado) para origen LOCAL
                if (origen == "LOCAL" && string.IsNullOrWhiteSpace(txtNIT.Text))
                    MarcarError(txtNIT, "El NIT es obligatorio para personas naturales.");
            }
            else if (tipoPersona == "JURIDICA" && !esNoContrib)
            {
                // NRC solo aplica (y solo está habilitado) para origen LOCAL
                if (origen == "LOCAL" && string.IsNullOrWhiteSpace(txtNRC.Text))
                    MarcarError(txtNRC, "El NRC es obligatorio para personas jurídicas.");
            }

            // Validación por Origen (EXTERIOR)
            if (origen == "EXTERIOR")
            {
                if (ObtenerIdCombo(cbxTIPO_DOC_IDEN) == null)
                    MarcarError(cbxTIPO_DOC_IDEN, "El Tipo de Documento es obligatorio para origen EXTERIOR.");
                if (string.IsNullOrWhiteSpace(txtDOCUMENTO.Text))
                    MarcarError(txtDOCUMENTO, "El Documento es obligatorio para origen EXTERIOR.");
            }

            // Validación por Origen (LOCAL): país + campos mínimos requeridos
            if (origen == "LOCAL")
            {
                string pais = cbxPAIS.Text.Trim().ToUpper();
                if (pais != "EL SALVADOR")
                    MarcarError(cbxPAIS, "Si el Origen es LOCAL, el País debe ser EL SALVADOR.");
                if (string.IsNullOrWhiteSpace(txtNOMBRE_COMERCIAL.Text))
                    MarcarError(txtNOMBRE_COMERCIAL, "El Nombre Comercial es obligatorio para origen LOCAL.");
                if (string.IsNullOrWhiteSpace(txtNIT.Text))
                    MarcarError(txtNIT, "El NIT es obligatorio para origen LOCAL.");
                if (string.IsNullOrWhiteSpace(txtTELEFONO.Text))
                    MarcarError(txtTELEFONO, "El Teléfono es obligatorio para origen LOCAL.");
                if (string.IsNullOrWhiteSpace(txtCELULAR.Text))
                    MarcarError(txtCELULAR, "El Celular es obligatorio para origen LOCAL.");
                if (string.IsNullOrWhiteSpace(txtCORREO.Text))
                    MarcarError(txtCORREO, "El Correo es obligatorio para origen LOCAL.");
                if (string.IsNullOrWhiteSpace(txtCOMPLEMENTO.Text))
                    MarcarError(txtCOMPLEMENTO, "La Dirección es obligatoria para origen LOCAL.");
                if (ObtenerCodigoCombo(cbxDEPTO) == null)
                    MarcarError(cbxDEPTO, "El Departamento es obligatorio para origen LOCAL.");
                if (ObtenerCodigoCombo(cbxMUNI) == null)
                    MarcarError(cbxMUNI, "El Municipio es obligatorio para origen LOCAL.");
                if (ObtenerCodigoCombo(cbxDIST) == null)
                    MarcarError(cbxDIST, "El Distrito es obligatorio para origen LOCAL.");
                if (_idActividad1 == 0)
                    MarcarError(txtCODI_ACTIVIDAD1, "La Actividad 1 es obligatoria para origen LOCAL.");
            }

            // Validación por Tipo Contribuyente (NRC solo aplica para origen LOCAL)
            bool requiereNRC = origen == "LOCAL" && !esNoContrib && (tipoContrib == "GRANDE" || tipoContrib == "MEDIANO" ||
                               tipoContrib.StartsWith("PEQUEÑO"));
            if (requiereNRC && string.IsNullOrWhiteSpace(txtNRC.Text))
                MarcarError(txtNRC, "El NRC es obligatorio para este tipo de contribuyente.");
            if (requiereNRC && _idActividad1 == 0)
                MarcarError(txtCODI_ACTIVIDAD1, "Debe seleccionar al menos la Actividad Económica 1.");

            if (!esValido)
            {
                XtraMessageBox.Show("Hay campos obligatorios sin completar. Revise los campos marcados en rojo.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                primerControlError?.Focus();
                return false;
            }

            if (!ValidarNitsCodigosRelacionados())
                return false;

            return true;
        }

        private bool ValidarNitsCodigosRelacionados()
        {
            if (!string.IsNullOrWhiteSpace(txtCODIPROVEEDOR.Text))
            {
                tabCodigosRelacionados.SelectedTabPage = tabProveedor;
                if (!ValidarNitRelacionado("Productor", txtNIT_PROVEEDOR_INTEGRACION.Text,
                        txtCODIPROVEEDOR))
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(txtCODTRANSPORT.Text))
            {
                tabCodigosRelacionados.SelectedTabPage = tabTransportista;
                if (!ValidarNitRelacionado("Transportista", txtNIT_TRANSPORTISTA_INTEGRACION.Text,
                        txtCODTRANSPORT))
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(txtID_CARGADORA.Text))
            {
                tabCodigosRelacionados.SelectedTabPage = tabCargadora;
                if (!ValidarNitRelacionado("Cargadora", txtNIT_CARGADORA_INTEGRACION.Text,
                        txtID_CARGADORA))
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(txtID_PROVEEDOR_ROZA.Text))
            {
                tabCodigosRelacionados.SelectedTabPage = tabRoza;
                if (!ValidarNitRelacionado("Roza", txtNIT_ROZA_INTEGRACION.Text,
                        txtID_PROVEEDOR_ROZA))
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(txtID_PROVEE_QQ.Text))
            {
                tabCodigosRelacionados.SelectedTabPage = tabQuerqueo;
                if (!ValidarNitRelacionado("Querqueo", txtNIT_QUERQUEO_INTEGRACION.Text,
                        txtID_PROVEE_QQ))
                    return false;
            }

            return true;
        }
        #endregion
        #region === LIMPIAR ===
        private void LimpiarFormulario()
        {
            IdEntidad = 0;
            txtCODIGO_ENTIDAD.Text = "";
            lblValidacionCodigo.Text = "";
            btnGuardar.Enabled = true;
            txtNOMBRE.Text = "";
            txtNOMBRE_COMERCIAL.Text = "";
            txtNRC.Text = "";
            txtPROFESION.Text = "";
            txtDUI.Text = "";
            txtNIT.Text = "";
            txtDOCUMENTO.Text = "";
            txtCORREO.Text = "";
            txtCORREO_CC.Text = "";
            txtCELULAR.Text = "";
            txtTELEFONO.Text = "";
            txtDIAS_PLAZO.Text = "";
            txtCUENTA_X_COBRAR.Text = "";
            txtNOMBRE_CUENTA_X_COBRAR.Text = "";
            _idTipoPrecioSeleccionado = null;
            txtID_TIPO_PRECIO.Text = "";
            txtCOMPLEMENTO.Text = "";
            txtCODIPROVEEDOR.Text = "";
            txtNOMBRE_PROVEEDOR_INTEGRACION.Text = "";
            txtNIT_PROVEEDOR_INTEGRACION.Text = "";
            txtCODTRANSPORT.Text = "";
            txtNOMBRE_TRANSPORTISTA_INTEGRACION.Text = "";
            txtNIT_TRANSPORTISTA_INTEGRACION.Text = "";
            txtID_CARGADORA.Text = "";
            txtNOMBRE_CARGADORA_INTEGRACION.Text = "";
            txtNIT_CARGADORA_INTEGRACION.Text = "";
            txtID_PROVEEDOR_ROZA.Text = "";
            txtNOMBRE_ROZA_INTEGRACION.Text = "";
            txtNIT_ROZA_INTEGRACION.Text = "";
            txtID_PROVEE_QQ.Text = "";
            txtNOMBRE_QUERQUEO_INTEGRACION.Text = "";
            txtNIT_QUERQUEO_INTEGRACION.Text = "";
            cbxTIPO_ENTIDAD.SelectedIndex = 0;
            cbxTIPO_CONTRIB.SelectedIndex = 0;
            cbxTIPO_DOC_IDEN.SelectedIndex = 0;
            cbxPAIS.SelectedIndex = 0;
            cbxDEPTO.SelectedIndex = 0;
            cbxDIST.DataSource = null;
            cbxDIST.Items.Clear();
            cbxMUNI.DataSource = null;
            cbxMUNI.Items.Clear();
            _idActividad1 = 0;
            _idActividad2 = 0;
            _idActividad3 = 0;
            txtCODI_ACTIVIDAD1.Text = "";
            txtACTIVIDAD_1.Text = "";
            txtCODI_ACTIVIDAD2.Text = "";
            txtACTIVIDAD_2.Text = "";
            txtCODI_ACTIVIDAD3.Text = "";
            txtACTIVIDAD_3.Text = "";
            cbxORIGEN.SelectedIndex = 0;
            _dtRoles?.Clear();
        }
        #endregion
        #region === BOTONES ===
        private void ConfigurarBotones(bool esNuevo)
        {
            btnEliminar.Enabled = !esNuevo && IdEntidad > 0;
            btnAgregarRol.Enabled = !esNuevo && IdEntidad > 0;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            ConfigurarBotones(esNuevo: true);
            txtCODIGO_ENTIDAD.Focus();
        }
        private void txtCODIGO_ENTIDAD_Leave(object sender, EventArgs e)
        {
            ValidarCodigoExistencia();
        }
        private void btnAgregarRol_Click(object sender, EventArgs e)
        {
            AgregarRol();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #region === VALIDACIÓN CÓDIGO ===
        private void ValidarCodigoExistencia()
        {
            string codigo = txtCODIGO_ENTIDAD.Text.Trim();
            if (string.IsNullOrWhiteSpace(codigo))
            {
                lblValidacionCodigo.Text = "";
                btnGuardar.Enabled = true;
                return;
            }
            try
            {
                DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_ENTIDAD]", new
                {
                    ACCION = "BUSCAR",
                    FILTRO = codigo
                });
                int idEncontrado = 0;
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string codBD = row["CODIGO_ENTIDAD"]?.ToString()?.Trim() ?? "";
                        int idBD = row["ID_ENTIDAD"] != DBNull.Value ? Convert.ToInt32(row["ID_ENTIDAD"]) : 0;
                        if (string.Equals(codBD, codigo, StringComparison.OrdinalIgnoreCase))
                        {
                            idEncontrado = idBD;
                            break;
                        }
                    }
                }
                bool existeOtraEntidad = idEncontrado > 0 && idEncontrado != IdEntidad;
                if (existeOtraEntidad && IdEntidad == 0 && TieneRol(idEncontrado, "PRO"))
                {
                    // El código ya existe como Proveedor: se carga esa misma entidad
                    // para completarla/guardarla también como Cliente (ROL = 'CLI').
                    CargarEntidadExistente(idEncontrado);
                    lblValidacionCodigo.Text = "✔";
                    lblValidacionCodigo.ForeColor = Color.Blue;
                    lblValidacionCodigo.Tag = "EXISTE_PROVEEDOR";
                    btnGuardar.Enabled = true;
                    XtraMessageBox.Show(
                        "Este código ya existe como Proveedor. Se cargaron sus datos; " +
                        "al guardar se registrará también como Cliente.",
                        "Entidad existente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (existeOtraEntidad)
                {
                    lblValidacionCodigo.Text = "✘";
                    lblValidacionCodigo.ForeColor = Color.Red;
                    lblValidacionCodigo.Tag = "DUPLICADO";
                    btnGuardar.Enabled = false;
                }
                else
                {
                    lblValidacionCodigo.Text = "✔";
                    lblValidacionCodigo.ForeColor = Color.Green;
                    lblValidacionCodigo.Tag = "OK";
                    btnGuardar.Enabled = true;
                }
            }
            catch
            {
                lblValidacionCodigo.Text = "";
            }
        }
        /// <summary>
        /// Verifica si una entidad ya tiene un rol específico registrado en
        /// dbo.ENTIDAD_ROL (p.ej. "PRO" = Proveedor, "CLI" = Cliente).
        /// </summary>
        private bool TieneRol(int idEntidad, string rol)
        {
            try
            {
                DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_ENTIDAD_ROL]", new
                {
                    ACCION = "LISTAR",
                    ID_ENTIDAD = idEntidad
                });
                if (dt == null) return false;
                foreach (DataRow r in dt.Rows)
                {
                    bool activo = r["ACTIVO"] != DBNull.Value && Convert.ToBoolean(r["ACTIVO"]);
                    string rolBD = r["ROL"]?.ToString()?.Trim();
                    if (activo && string.Equals(rolBD, rol, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Da de baja (ELIMINAR) únicamente el rol 'CLI' de la entidad en
        /// dbo.ENTIDAD_ROL, sin afectar otros roles (p.ej. 'PRO') que comparta.
        /// </summary>
        private void EliminarRolCliente(int idEntidad)
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_ENTIDAD_ROL]", new
            {
                ACCION = "CONSULTAR",
                ID_ENTIDAD = idEntidad,
                ROL = "CLI"
            });
            if (dt == null || dt.Rows.Count == 0) return;
            int idEntidadRol = Convert.ToInt32(dt.Rows[0]["ID_ENTIDAD_ROL"]);
            _dal.EjecutarSinRetorno("[EMH].[SP_ENTIDAD_ROL]", new
            {
                ACCION = "ELIMINAR",
                ID_ENTIDAD_ROL = idEntidadRol
            });
        }
        /// <summary>
        /// Indica si a la entidad le queda algún rol activo distinto (p.ej. Proveedor)
        /// después de haber dado de baja el rol de Cliente.
        /// </summary>
        private bool TieneAlgunOtroRolActivo(int idEntidad)
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_ENTIDAD_ROL]", new
            {
                ACCION = "LISTAR",
                ID_ENTIDAD = idEntidad
            });
            if (dt == null) return false;
            foreach (DataRow r in dt.Rows)
            {
                bool activo = r["ACTIVO"] != DBNull.Value && Convert.ToBoolean(r["ACTIVO"]);
                if (activo) return true;
            }
            return false;
        }
        #endregion
        #region === HELPERS ===
        private static void SetComboById(System.Windows.Forms.ComboBox cbx, int? value)
        {
            if (value != null && value != 0)
                cbx.SelectedValue = value;
            else
                cbx.SelectedIndex = 0;
        }
        private static string AsString(object val)
            => val == null || val == DBNull.Value ? "" : val.ToString();
        private static int? AsInt(object val)
        {
            if (val == null || val == DBNull.Value) return null;
            int i = Convert.ToInt32(val);
            return i == 0 ? (int?)null : i;
        }
        private static decimal ParseDecimal(string texto)
            => decimal.TryParse(texto.Replace(",", ""), out decimal d) ? d : 0m;
        private static int? ParseInt(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return null;
            return int.TryParse(texto.Trim(), out int i) ? i : (int?)null;
        }
        private static int? ParseIntNull(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return null;
            return int.TryParse(texto.Trim(), out int i) ? i : (int?)null;
        }
        private static string NullIfEmpty(string texto)
            => string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
        private static string ObtenerCodigoCombo(System.Windows.Forms.ComboBox cbx)
        {
            if (cbx.SelectedValue == null || cbx.SelectedValue == DBNull.Value) return null;
            string val = cbx.SelectedValue.ToString();
            return string.IsNullOrWhiteSpace(val) ? null : val;
        }
        private static int? ObtenerIdCombo(System.Windows.Forms.ComboBox cbx)
        {
            if (cbx.SelectedValue == null || cbx.SelectedValue == DBNull.Value) return null;
            if (cbx.SelectedValue is DataRowView) return null;
            int val = Convert.ToInt32(cbx.SelectedValue);
            return val == 0 ? (int?)null : val;
        }
        private void MostrarValidacion(string mensaje)
        {
            XtraMessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        #endregion
        private void lblTIPO_CONTRIB_Click(object sender, EventArgs e)
        {
        }
    }
}
