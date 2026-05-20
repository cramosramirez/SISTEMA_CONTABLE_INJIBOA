using System;
using System.Data;

namespace SistemaContable.DAL
{
    public class EntidadDAL : DALBase
    {
        private const string SP = "SP_ENTIDAD";
        private const string SP_ROL = "SP_ENTIDAD_ROL";

        public DataTable Buscar(string filtro = null, string rol = null)
            => EjecutarConsulta(SP, new { ACCION = "BUSCAR", FILTRO = filtro, ROL = rol });

        public DataSet Obtener(int idEntidad)
            => EjecutarMultiple(SP, new { ACCION = "OBTENER", ID_ENTIDAD = idEntidad });

        public DataTable ObtenerPorNRC(string nrc)
            => EjecutarConsulta(SP, new { ACCION = "OBTENER_POR_NRC", NRC = nrc });

        public int Guardar(
            int     idEntidad,      string codigoEntidad,
            int     idTipoEntidad,  int    idTipoContrib,
            int?    idTipoDocInden, string documento,
            string  nrc,            string dui,
            string  nit,            string nombre,
            string  complemento,    string calle,
            string  casa,           string aptoLocal,
            string  colonia,        string correo,
            string  correoCC,       string celular,
            string  telefono,       string encargado,
            int?    diasPlazo,      int?   idPais,
            string  codiDepto,      string codiMuni,
            int?    idActividad1,   int?   idActividad2,
            int?    idActividad3,   string cuentaXPagar,
            string  usuario)
            => EjecutarEscalar(SP, new
            {
                ACCION            = "GUARDAR",
                ID_ENTIDAD        = idEntidad,
                CODIGO_ENTIDAD    = codigoEntidad,
                ID_TIPO_ENTIDAD   = idTipoEntidad,
                ID_TIPO_CONTRIB   = idTipoContrib,
                ID_TIPO_DOC_INDEN = idTipoDocInden,
                DOCUMENTO         = documento,
                NRC               = nrc,
                DUI               = dui,
                NIT               = nit,
                NOMBRE            = nombre,
                COMPLEMENTO       = complemento,
                CALLE             = calle,
                CASA              = casa,
                APTO_LOCAL        = aptoLocal,
                COLONIA           = colonia,
                CORREO            = correo,
                CORREO_CC         = correoCC,
                CELULAR           = celular,
                TELEFONO          = telefono,
                ENCARGADO         = encargado,
                DIAS_PLAZO        = diasPlazo,
                ID_PAIS           = idPais,
                CODI_DEPTO        = codiDepto,
                CODI_MUNI         = codiMuni,
                ID_ACTIVIDAD_1    = idActividad1,
                ID_ACTIVIDAD_2    = idActividad2,
                ID_ACTIVIDAD_3    = idActividad3,
                CUENTA_X_PAGAR    = cuentaXPagar,
                USUARIO           = usuario
            });

        public void Eliminar(int idEntidad, string rol = null)
            => EjecutarSinRetorno(SP, new { ACCION = "ELIMINAR", ID_ENTIDAD = idEntidad, ROL = rol });

        public int AsignarRol(int idEntidad, string rol)
            => EjecutarEscalar(SP_ROL, new { ACCION = "GUARDAR", ID_ENTIDAD = idEntidad, ROL = rol });

        public void DesactivarRol(int idEntidadRol)
            => EjecutarSinRetorno(SP_ROL, new { ACCION = "ELIMINAR", ID_ENTIDAD_ROL = idEntidadRol });

        public DataTable ListarRoles(int idEntidad)
            => EjecutarConsulta(SP_ROL, new { ACCION = "LISTAR", ID_ENTIDAD = idEntidad });
    }
}
