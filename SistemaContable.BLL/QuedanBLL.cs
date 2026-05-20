using System;
using System.Data;
using SistemaContable.DAL;

namespace SistemaContable.BLL
{
    /// <summary>
    /// Reglas de negocio para el módulo de Quedan.
    /// Valida antes de enviar al DAL.
    /// </summary>
    public class QuedanBLL
    {
        private readonly QuedanDAL _dal = new QuedanDAL();

        public DataTable Buscar(string filtro = null, DateTime? desde = null,
            DateTime? hasta = null, string estado = null, int? idEntidad = null)
            => _dal.Buscar(filtro, desde, hasta, estado, idEntidad);

        public DataSet Obtener(int idQuedan)
            => _dal.Obtener(idQuedan);

        public DataTable Guardar(int idQuedan, DateTime fecha, int idEntidad,
            string codigoEntidad, string observacion, string usuario)
        {
            // Validaciones de negocio
            if (idEntidad <= 0)
                throw new Exception("Debe seleccionar un proveedor válido.");

            if (fecha > DateTime.Today)
                throw new Exception("La fecha del quedan no puede ser mayor a hoy.");

            string estado = idQuedan == 0 ? "ACT" : null; // solo aplica en nuevo

            return _dal.Guardar(idQuedan, fecha, idEntidad,
                codigoEntidad, estado, observacion, usuario);
        }

        public void Anular(int idQuedan, string usuario)
        {
            if (idQuedan <= 0)
                throw new Exception("Quedan no válido para anular.");

            _dal.Anular(idQuedan, "Anulado por el usuario", usuario);
        }

        public void Eliminar(int idQuedan)
        {
            if (idQuedan <= 0)
                throw new Exception("Quedan no válido para eliminar.");

            _dal.Eliminar(idQuedan);
        }

        // CCF
        public int GuardarCCF(
            int idCCF, int idQuedan, int numQuedan, int idTipoDTE,
            string numControl, string codGeneracion, string selloRecibido,
            DateTime fechaEmision, string tipoMoneda, int idEntidad,
            string codigoEntidad, DateTime? fechaRecibido, DateTime? fechaVence,
            string orden, int? idSucursal, int? idTipoServi, int? idTipoOpera,
            int? idClasifica, int? idSector, int? idTipoCosto,
            decimal noSujeta, decimal exenta, decimal gravada, decimal percepcion,
            decimal iva, decimal fovial, decimal cotrans, decimal total,
            decimal cargo, decimal abono, decimal renta, decimal ivar, decimal saldo,
            int? idComprobanteRet, string observacion, string usuario)
        {
            // Validar que el total cuadre
            decimal totalCalculado = noSujeta + exenta + gravada + iva
                                   + percepcion + fovial + cotrans;

            if (Math.Abs(totalCalculado - total) > 0.01m)
                throw new Exception($"El total del documento ({total:N2}) " +
                                    $"no coincide con la suma de valores ({totalCalculado:N2}).");

            if (string.IsNullOrWhiteSpace(numControl))
                throw new Exception("El número de control del DTE es requerido.");

            if (string.IsNullOrWhiteSpace(codGeneracion))
                throw new Exception("El código de generación del DTE es requerido.");

            return _dal.GuardarCCF(idCCF, idQuedan, numQuedan, idTipoDTE,
                numControl, codGeneracion, selloRecibido, fechaEmision,
                tipoMoneda, idEntidad, codigoEntidad, fechaRecibido, fechaVence,
                orden, idSucursal, idTipoServi, idTipoOpera, idClasifica,
                idSector, idTipoCosto, noSujeta, exenta, gravada, percepcion,
                iva, fovial, cotrans, total, cargo, abono, renta, ivar, saldo,
                idComprobanteRet, observacion, usuario);
        }

        public void EliminarCCF(int idCCF)
            => _dal.EliminarCCF(idCCF);
    }
}
