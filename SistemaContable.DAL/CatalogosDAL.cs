using System.Data;

namespace SistemaContable.DAL
{
    /// <summary>
    /// DAL para catálogos simples (una sola tabla, CRUD básico).
    /// Todos usan el mismo patrón: SP multi-acción con @ACCION.
    /// </summary>
    public class CatalogosDAL : DALBase
    {
        // ── CATALOGOS | FACTURACION ──────────────────────────────

        public DataTable BuscarModeloFacturacion(string filtro = null)
            => EjecutarConsulta("SP_MODELO_FACTURACION", new { ACCION = "BUSCAR", FILTRO = filtro });

        public DataTable ObtenerModeloFacturacion(int id)
            => EjecutarConsulta("SP_MODELO_FACTURACION", new { ACCION = "OBTENER", ID_MODELO_FACT = id });

        public int GuardarModeloFacturacion(int id, string nombre)
            => EjecutarEscalar("SP_MODELO_FACTURACION", new { ACCION = "GUARDAR", ID_MODELO_FACT = id, NOMBRE = nombre });

        public void EliminarModeloFacturacion(int id)
            => EjecutarSinRetorno("SP_MODELO_FACTURACION", new { ACCION = "ELIMINAR", ID_MODELO_FACT = id });

        // ─────────────────────────────────────────────────────────

        public DataTable BuscarTipoOperacion(string filtro = null)
            => EjecutarConsulta("SP_TIPO_OPERACION", new { ACCION = "BUSCAR", FILTRO = filtro });

        public int GuardarTipoOperacion(int id, string nombre)
            => EjecutarEscalar("SP_TIPO_OPERACION", new { ACCION = "GUARDAR", ID_TIPO_OPERACION = id, NOMBRE = nombre });

        public void EliminarTipoOperacion(int id)
            => EjecutarSinRetorno("SP_TIPO_OPERACION", new { ACCION = "ELIMINAR", ID_TIPO_OPERACION = id });

        // ─────────────────────────────────────────────────────────

        public DataTable BuscarTipoContingencia(string filtro = null)
            => EjecutarConsulta("SP_TIPO_CONTINGENCIA", new { ACCION = "BUSCAR", FILTRO = filtro });

        public int GuardarTipoContingencia(int id, string nombre)
            => EjecutarEscalar("SP_TIPO_CONTINGENCIA", new { ACCION = "GUARDAR", ID_TIPO_CONTI = id, NOMBRE = nombre });

        public void EliminarTipoContingencia(int id)
            => EjecutarSinRetorno("SP_TIPO_CONTINGENCIA", new { ACCION = "ELIMINAR", ID_TIPO_CONTI = id });

        // ─────────────────────────────────────────────────────────

        public DataTable BuscarTipoDTE(string filtro = null, bool soloFiscal = false, bool soloInterno = false)
        {
            string accion = soloFiscal ? "BUSCAR_FISCAL" : soloInterno ? "BUSCAR_INTERNO" : "BUSCAR";
            return EjecutarConsulta("SP_TIPO_DTE", new { ACCION = accion, FILTRO = filtro });
        }

        public DataTable ObtenerTipoDTE(int id)
            => EjecutarConsulta("SP_TIPO_DTE", new { ACCION = "OBTENER", ID_TIPO_DTE = id });

        // ─────────────────────────────────────────────────────────

        public DataTable BuscarSucursal(string filtro = null)
            => EjecutarConsulta("SP_SUCURSAL", new { ACCION = "BUSCAR", FILTRO = filtro });

        public int GuardarSucursal(int id, string nombre)
            => EjecutarEscalar("SP_SUCURSAL", new { ACCION = "GUARDAR", ID_SUCURSAL = id, NOMBRE = nombre });

        public void EliminarSucursal(int id)
            => EjecutarSinRetorno("SP_SUCURSAL", new { ACCION = "ELIMINAR", ID_SUCURSAL = id });

        // ─────────────────────────────────────────────────────────

        public DataTable BuscarPais(string filtro = null)
            => EjecutarConsulta("SP_PAIS", new { ACCION = "BUSCAR", FILTRO = filtro });

        // ─────────────────────────────────────────────────────────

        public DataTable BuscarDepartamento(string filtro = null)
            => EjecutarConsulta("SP_DEPARTAMENTO", new { ACCION = "BUSCAR", FILTRO = filtro });

        public DataTable BuscarMunicipio(string codiDepto = null, string filtro = null)
            => EjecutarConsulta("SP_MUNICIPIO", new { ACCION = "BUSCAR", CODI_DEPTO = codiDepto, FILTRO = filtro });

        // ── CATALOGOS | ENTIDADES ────────────────────────────────

        public DataTable BuscarTipoPersona(string filtro = null)
            => EjecutarConsulta("SP_TIPO_PERSONA", new { ACCION = "BUSCAR", FILTRO = filtro });

        public DataTable BuscarTipoDocIdentidad(string filtro = null)
            => EjecutarConsulta("SP_TIPO_DOCUMENTO_IDENTIDAD", new { ACCION = "BUSCAR", FILTRO = filtro });

        public DataTable BuscarTipoContribuyente(string filtro = null)
            => EjecutarConsulta("SP_TIPO_CONTRIBUYENTE", new { ACCION = "BUSCAR", FILTRO = filtro });

        public DataTable BuscarActividadEconomica(string filtro = null)
            => EjecutarConsulta("SP_ACTIVIDAD_ECONOMICA", new { ACCION = "BUSCAR", FILTRO = filtro });

        // ── CATALOGOS | CLASIFICACION COMPRA ────────────────────

        public DataTable BuscarTipoServicio(string filtro = null)
            => EjecutarConsulta("SP_COMPRA_TIPO_SERVICIO", new { ACCION = "BUSCAR", FILTRO = filtro });

        public int GuardarTipoServicio(int id, string nombre)
            => EjecutarEscalar("SP_COMPRA_TIPO_SERVICIO", new { ACCION = "GUARDAR", ID_TIPO_SERVI = id, NOMBRE = nombre });

        public void EliminarTipoServicio(int id)
            => EjecutarSinRetorno("SP_COMPRA_TIPO_SERVICIO", new { ACCION = "ELIMINAR", ID_TIPO_SERVI = id });

        public DataTable BuscarTipoOperacionCompra(string filtro = null)
            => EjecutarConsulta("SP_COMPRA_TIPO_OPERACION", new { ACCION = "BUSCAR", FILTRO = filtro });

        public int GuardarTipoOperacionCompra(int id, string nombre)
            => EjecutarEscalar("SP_COMPRA_TIPO_OPERACION", new { ACCION = "GUARDAR", ID_TIPO_OPERA = id, NOMBRE = nombre });

        public DataTable BuscarClasificacion(string filtro = null)
            => EjecutarConsulta("SP_COMPRA_CLASIFICACION", new { ACCION = "BUSCAR", FILTRO = filtro });

        public int GuardarClasificacion(int id, string nombre)
            => EjecutarEscalar("SP_COMPRA_CLASIFICACION", new { ACCION = "GUARDAR", ID_CLASIFICA = id, NOMBRE = nombre });

        public DataTable BuscarSector(string filtro = null)
            => EjecutarConsulta("SP_COMPRA_SECTOR", new { ACCION = "BUSCAR", FILTRO = filtro });

        public int GuardarSector(int id, string nombre)
            => EjecutarEscalar("SP_COMPRA_SECTOR", new { ACCION = "GUARDAR", ID_SECTOR = id, NOMBRE = nombre });

        public DataTable BuscarTipoCosto(string filtro = null)
            => EjecutarConsulta("SP_COMPRA_TIPO_COSTO", new { ACCION = "BUSCAR", FILTRO = filtro });

        public int GuardarTipoCosto(int id, string nombre)
            => EjecutarEscalar("SP_COMPRA_TIPO_COSTO", new { ACCION = "GUARDAR", ID_TIPO_COSTO = id, NOMBRE = nombre });

        // ── CATALOGOS | CONTABILIDAD ─────────────────────────────

        public DataTable BuscarTipoPartida(string filtro = null)
            => EjecutarConsulta("SP_TIPO_PARTIDA", new { ACCION = "BUSCAR", FILTRO = filtro });

        public int SiguienteNumeroPartida(int idTipoPartida)
            => EjecutarEscalar("SP_TIPO_PARTIDA", new { ACCION = "SIGUIENTE_NUMERO", ID_TIPO_PARTIDA = idTipoPartida });

        public DataTable BuscarTipoCuenta(string filtro = null)
            => EjecutarConsulta("SP_TIPO_CUENTA", new { ACCION = "BUSCAR", FILTRO = filtro });

        public DataTable BuscarCuenta(string filtro = null, int? idTipoCta = null, bool soloDetalle = true)
        {
            string accion = soloDetalle ? "BUSCAR" : "BUSCAR_TODO";
            return EjecutarConsulta("SP_CATALOGO_CUENTA", new { ACCION = accion, FILTRO = filtro, ID_TIPO_CTA = idTipoCta });
        }

        public DataTable ObtenerArbolCuentas()
            => EjecutarConsulta("SP_CATALOGO_CUENTA", new { ACCION = "ARBOL" });

        public DataTable ObtenerCuenta(int idCuenta)
            => EjecutarConsulta("SP_CATALOGO_CUENTA", new { ACCION = "OBTENER", ID_CUENTA = idCuenta });

        public int GuardarCuenta(int idCuenta, string cuenta, string nombreCuenta,
            int idTipoCta, int? idCuentaPadre, string anterior,
            bool esDetalle, int nivel, string usuario)
            => EjecutarEscalar("SP_CATALOGO_CUENTA", new
            {
                ACCION          = "GUARDAR",
                ID_CUENTA       = idCuenta,
                CUENTA          = cuenta,
                NOMBRE_CUENTA   = nombreCuenta,
                ID_TIPO_CTA     = idTipoCta,
                ID_CUENTA_PADRE = idCuentaPadre,
                ANTERIOR        = anterior,
                ES_DETALLE      = esDetalle,
                NIVEL           = nivel,
                USUARIO         = usuario
            });

        public void EliminarCuenta(int idCuenta)
            => EjecutarSinRetorno("SP_CATALOGO_CUENTA", new { ACCION = "ELIMINAR", ID_CUENTA = idCuenta });

        // ── CATALOGOS | BANCOS ───────────────────────────────────

        public DataTable BuscarBanco(string filtro = null)
            => EjecutarConsulta("SP_BANCO", new { ACCION = "BUSCAR", FILTRO = filtro });

        public int GuardarBanco(int id, string nombre)
            => EjecutarEscalar("SP_BANCO", new { ACCION = "GUARDAR", ID_BANCO = id, NOMBRE = nombre });

        public void EliminarBanco(int id)
            => EjecutarSinRetorno("SP_BANCO", new { ACCION = "ELIMINAR", ID_BANCO = id });

        public DataTable BuscarTipoCuentaBco(string filtro = null)
            => EjecutarConsulta("SP_TIPO_CUENTA_BCO", new { ACCION = "BUSCAR", FILTRO = filtro });

        public DataTable BuscarCuentaBancaria(string filtro = null)
            => EjecutarConsulta("SP_CUENTA_BANCARIA", new { ACCION = "BUSCAR", FILTRO = filtro });

        public DataTable ObtenerCuentaBancaria(int idCtaBanco)
            => EjecutarConsulta("SP_CUENTA_BANCARIA", new { ACCION = "OBTENER", ID_CTA_BANCO = idCtaBanco });

        public int GuardarCuentaBancaria(int idCtaBanco, int idBanco, int idTipoCta,
            string numCuenta, string nombre, System.DateTime fechaApertura,
            string ctaContable, string usuario)
            => EjecutarEscalar("SP_CUENTA_BANCARIA", new
            {
                ACCION         = "GUARDAR",
                ID_CTA_BANCO   = idCtaBanco,
                ID_BANCO       = idBanco,
                ID_TIPO_CTA    = idTipoCta,
                NUM_CUENTA     = numCuenta,
                NOMBRE         = nombre,
                FECHA_APERTURA = fechaApertura,
                CTACONTABLE    = ctaContable,
                USUARIO        = usuario
            });

        public void EliminarCuentaBancaria(int id)
            => EjecutarSinRetorno("SP_CUENTA_BANCARIA", new { ACCION = "ELIMINAR", ID_CTA_BANCO = id });
    }
}
