using System;
using System.Data;

namespace SistemaContable.DAL
{
    public class ChequeDAL : DALBase
    {
        private const string SP     = "SP_CHEQUE";
        private const string SP_PAR = "SP_CHEQUE_PARTIDA";

        public DataTable Buscar(string filtro = null, DateTime? desde = null,
            DateTime? hasta = null, int? idCtaBanco = null)
            => EjecutarConsulta(SP, new
            {
                ACCION       = "BUSCAR",
                FILTRO       = filtro,
                FECHA_DESDE  = desde,
                FECHA_HASTA  = hasta,
                ID_CTA_BANCO = idCtaBanco
            });

        public DataSet Obtener(int idCheque)
            => EjecutarMultiple(SP, new { ACCION = "OBTENER", ID_CHEQUE = idCheque });

        public int Guardar(int idCheque, int idCtaBanco, int numCheque,
            DateTime fechaCheque, decimal monto, string nombreCheque,
            int? numPartida, string concepto, int? idEntidad,
            string codigoEntidad, string usuario)
            => EjecutarEscalar(SP, new
            {
                ACCION         = "GUARDAR",
                ID_CHEQUE      = idCheque,
                ID_CTA_BANCO   = idCtaBanco,
                NUM_CHEQUE     = numCheque,
                FECHA_CHEQUE   = fechaCheque,
                MONTO          = monto,
                NOMBRE_CHEQUE  = nombreCheque,
                NUM_PARTIDA    = numPartida,
                CONCEPTO       = concepto,
                ID_ENTIDAD     = idEntidad,
                CODIGO_ENTIDAD = codigoEntidad,
                USUARIO        = usuario
            });

        public void Eliminar(int idCheque)
            => EjecutarSinRetorno(SP, new { ACCION = "ELIMINAR", ID_CHEQUE = idCheque });

        // Partida contable del cheque
        public int GuardarPartida(int idChequePar, int idCheque,
            string ctaContable, string detalle, decimal cargo, decimal abono, string usuario)
            => EjecutarEscalar(SP_PAR, new
            {
                ACCION        = "GUARDAR",
                ID_CHEQUE_PAR = idChequePar,
                ID_CHEQUE     = idCheque,
                CTACONTABLE   = ctaContable,
                DETALLE       = detalle,
                CARGO         = cargo,
                ABONO         = abono,
                USUARIO       = usuario
            });

        public void EliminarPartida(int idChequePar)
            => EjecutarSinRetorno(SP_PAR, new { ACCION = "ELIMINAR", ID_CHEQUE_PAR = idChequePar });

        public void EliminarPartidasPorCheque(int idCheque)
            => EjecutarSinRetorno(SP_PAR, new { ACCION = "ELIMINAR_POR_CHEQUE", ID_CHEQUE = idCheque });

        public DataTable VerificarCuadre(int idCheque)
            => EjecutarConsulta(SP_PAR, new { ACCION = "VERIFICAR_CUADRE", ID_CHEQUE = idCheque });
    }
}
