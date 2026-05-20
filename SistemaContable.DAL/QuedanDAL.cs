using System;
using System.Data;

namespace SistemaContable.DAL
{
    public class QuedanDAL : DALBase
    {
        private const string SP     = "SP_QUEDAN";
        private const string SP_CCF = "SP_CREDITO_FISCAL_COMPRA";

        public DataTable Buscar(string filtro = null, DateTime? desde = null,
            DateTime? hasta = null, string estado = null, int? idEntidad = null)
            => EjecutarConsulta(SP, new
            {
                ACCION      = "BUSCAR",
                FILTRO      = filtro,
                FECHA_DESDE = desde,
                FECHA_HASTA = hasta,
                ESTADO      = estado,
                ID_ENTIDAD  = idEntidad
            });

        public DataSet Obtener(int idQuedan)
            => EjecutarMultiple(SP, new { ACCION = "OBTENER", ID_QUEDAN = idQuedan });

        public DataTable Guardar(int idQuedan, DateTime fecha, int idEntidad,
            string codigoEntidad, string estado, string observacion, string usuario)
            => EjecutarConsulta(SP, new
            {
                ACCION         = "GUARDAR",
                ID_QUEDAN      = idQuedan,
                FECHA          = fecha,
                ID_ENTIDAD     = idEntidad,
                CODIGO_ENTIDAD = codigoEntidad,
                ESTADO         = estado,
                OBSERVACION    = observacion,
                USUARIO        = usuario
            });

        public void Anular(int idQuedan, string observacion, string usuario)
            => EjecutarSinRetorno(SP, new
            {
                ACCION      = "ANULAR",
                ID_QUEDAN   = idQuedan,
                OBSERVACION = observacion,
                USUARIO     = usuario
            });

        public void Eliminar(int idQuedan)
            => EjecutarSinRetorno(SP, new { ACCION = "ELIMINAR", ID_QUEDAN = idQuedan });

        // CCF del Quedan
        public DataTable ObtenerCCF(int idCCF)
            => EjecutarConsulta(SP_CCF, new { ACCION = "OBTENER", ID_CCF_COMPRA = idCCF });

        public int GuardarCCF(
            int      idCCF,         int      idQuedan,
            int      numQuedan,     int      idTipoDTE,
            string   numControl,    string   codGeneracion,
            string   selloRecibido, DateTime fechaEmision,
            string   tipoMoneda,    int      idEntidad,
            string   codigoEntidad, DateTime? fechaRecibido,
            DateTime? fechaVence,  string   orden,
            int?     idSucursal,    int?     idTipoServi,
            int?     idTipoOpera,   int?     idClasifica,
            int?     idSector,      int?     idTipoCosto,
            decimal  noSujeta,      decimal  exenta,
            decimal  gravada,       decimal  percepcion,
            decimal  iva,           decimal  fovial,
            decimal  cotrans,       decimal  total,
            decimal  cargo,         decimal  abono,
            decimal  renta,         decimal  ivar,
            decimal  saldo,         int?     idComprobanteRet,
            string   observacion,   string   usuario)
            => EjecutarEscalar(SP_CCF, new
            {
                ACCION             = "GUARDAR",
                ID_CCF_COMPRA      = idCCF,
                ID_QUEDAN          = idQuedan,
                NUM_QUEDAN         = numQuedan,
                ID_TIPO_DTE        = idTipoDTE,
                NUM_CONTROL        = numControl,
                COD_GENERACION     = codGeneracion,
                SELLO_RECIBIDO     = selloRecibido,
                FECHA_EMISION      = fechaEmision,
                TIPO_MONEDA        = tipoMoneda,
                ID_ENTIDAD         = idEntidad,
                CODIGO_ENTIDAD     = codigoEntidad,
                FECHA_RECIBIDO     = fechaRecibido,
                FECHA_VENCE        = fechaVence,
                ORDEN              = orden,
                ID_SUCURSAL        = idSucursal,
                ID_TIPO_SERVI      = idTipoServi,
                ID_TIPO_OPERA      = idTipoOpera,
                ID_CLASIFICA       = idClasifica,
                ID_SECTOR          = idSector,
                ID_TIPO_COSTO      = idTipoCosto,
                NO_SUJETA          = noSujeta,
                EXENTA             = exenta,
                GRAVADA            = gravada,
                PERCEPCION         = percepcion,
                IVA                = iva,
                FOVIAL             = fovial,
                COTRANS            = cotrans,
                TOTAL              = total,
                CARGO              = cargo,
                ABONO              = abono,
                RENTA              = renta,
                IVAR               = ivar,
                SALDO              = saldo,
                ID_COMPROBANTE_RET = idComprobanteRet,
                OBSERVACION        = observacion,
                USUARIO            = usuario
            });

        public void EliminarCCF(int idCCF)
            => EjecutarSinRetorno(SP_CCF, new { ACCION = "ELIMINAR", ID_CCF_COMPRA = idCCF });
    }
}
