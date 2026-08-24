using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace SistemaContable.DAL
{
    public class DALBase
    {
        protected string CadenaConexion
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SistemaContable"].ConnectionString;
            }
        }

        // CASO 1: Retorna DataTable — BUSCAR, CONSULTAR, LISTAR
        public DataTable EjecutarConsulta(string sp, object parametros = null)
        {
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    using (var reader = cn.ExecuteReader(sp, parametros,
                                            commandType: CommandType.StoredProcedure))
                    {
                        var dt = new DataTable();
                        dt.Load(reader);   // Load() respeta los tipos del reader (DateTime, decimal, int, etc.)
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex,
                    modulo: "DALBase.EjecutarConsulta",
                    spNombre: sp,
                    parametros: parametros);
                throw new Exception(ex.Message, ex);
            }
        }

        public DynamicParameters EjecutarConSalida(string sp, DynamicParameters parametros)
        {
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Execute(sp, parametros, commandType: CommandType.StoredProcedure);
                }
                return parametros; // trae los valores OUTPUT ya poblados
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex,
                    modulo: "DALBase.EjecutarConSalida",
                    spNombre: sp,
                    parametros: parametros);
                throw new Exception(ex.Message, ex);
            }
        }

        // CASO 2: Retorna entero — ID generado en INSERT
        public int EjecutarEscalar(string sp, object parametros = null)
        {
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    return cn.QueryFirstOrDefault<int>(sp, parametros,
                             commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex,
                    modulo: "DALBase.EjecutarEscalar",
                    spNombre: sp,
                    parametros: parametros);
                throw new Exception(ex.Message, ex);
            }
        }

        // CASO 2.1: Retorna cualquier tipo de dato — Listo para JSON
        public T EjecutarEscalar<T>(string sp, object parametros = null)
        {
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    return cn.QueryFirstOrDefault<T>(sp, parametros,
                             commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex,
                    modulo: "DALBase.EjecutarEscalar<T>",
                    spNombre: sp,
                    parametros: parametros);
                throw new Exception(ex.Message, ex);
            }
        }

        // CASO 3: Sin retorno — UPDATE, DELETE, ANULAR
        public void EjecutarSinRetorno(string sp, object parametros = null)
        {
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Execute(sp, parametros,
                               commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex,
                    modulo: "DALBase.EjecutarSinRetorno",
                    spNombre: sp,
                    parametros: parametros);
                throw new Exception(ex.Message, ex);
            }
        }

        // CASO 4: Retorna DataSet — documentos con detalle (encabezado + líneas)
        public DataSet EjecutarMultiple(string sp, object parametros = null)
        {
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sp, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (parametros != null)
                        {
                            foreach (var prop in parametros.GetType().GetProperties())
                            {
                                cmd.Parameters.AddWithValue(
                                    "@" + prop.Name,
                                    prop.GetValue(parametros) ?? DBNull.Value);
                            }
                        }

                        var ds = new DataSet();
                        using (var da = new SqlDataAdapter(cmd))
                            da.Fill(ds);

                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex,
                    modulo: "DALBase.EjecutarMultiple",
                    spNombre: sp,
                    parametros: parametros);

                throw new Exception(ex.Message, ex);
            }
        }

        // CASO 4: TVP sin retorno — UPDATE, DELETE, ANULAR
        public int EjecutarConsultaConTVP(
                 string sp,
                 string nombreParametroTVP,   // nombre del parámetro
                 string tipoTVP,              // nombre del TYPE
                 DataTable tvp,
                 object parametrosAdicionales = null)
        {
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                using (var cmd = new SqlCommand(sp, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@" + nombreParametroTVP, SqlDbType.Structured)
                    {
                        TypeName = "dbo." + tipoTVP,
                        Value = tvp
                    });
                    if (parametrosAdicionales != null)
                    {
                        foreach (var prop in parametrosAdicionales.GetType().GetProperties())
                            cmd.Parameters.AddWithValue("@" + prop.Name,
                                prop.GetValue(parametrosAdicionales) ?? DBNull.Value);
                    }
                    cn.Open();
                    var resultado = cmd.ExecuteScalar();
                    return resultado == null || resultado == DBNull.Value ? 0 : Convert.ToInt32(resultado);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex,
                    modulo: "DALBase.EjecutarConsultaConTVP",
                    spNombre: sp,
                    parametros: parametrosAdicionales);

                throw new Exception(ex.Message, ex);
            }
        }


        public int EjecutarConsultaConTVPs(
                string sp,
                object parametrosAdicionales,
                params (string Nombre, string TipoTVP, DataTable Tabla)[] tvps)
        {
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                using (var cmd = new SqlCommand(sp, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar todos los TVPs
                    foreach (var tvp in tvps)
                    {
                        cmd.Parameters.Add(new SqlParameter("@" + tvp.Nombre, SqlDbType.Structured)
                        {
                            TypeName = "dbo." + tvp.TipoTVP,
                            Value = tvp.Tabla
                        });
                    }
                    // Parámetros normales
                    if (parametrosAdicionales != null)
                    {
                        foreach (var prop in parametrosAdicionales.GetType().GetProperties())
                            cmd.Parameters.AddWithValue("@" + prop.Name,
                                prop.GetValue(parametrosAdicionales) ?? DBNull.Value);
                    }

                    cn.Open();
                    var resultado = cmd.ExecuteScalar();
                    return resultado == null || resultado == DBNull.Value
                        ? 0
                        : Convert.ToInt32(resultado);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex,
                    modulo: "DALBase.EjecutarConsultaConTVPs",
                    spNombre: sp,
                    parametros: parametrosAdicionales);

                throw new Exception(ex.Message, ex);
            }
        }
        public DataTable EjecutarConsultaConTVPDataTable(
            string storedProcedure,
            string nombreParametroTVP,
            string tipoTVP,
            DataTable tvp,
            object parametrosAdicionales = null)
            {
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                using (var cmd = new SqlCommand(storedProcedure, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@" + nombreParametroTVP, SqlDbType.Structured)
                    {
                        TypeName = "dbo." + tipoTVP,
                        Value = tvp
                    });
                    if (parametrosAdicionales != null)
                    {
                        foreach (var prop in parametrosAdicionales.GetType().GetProperties())
                            cmd.Parameters.AddWithValue("@" + prop.Name,
                                prop.GetValue(parametrosAdicionales) ?? DBNull.Value);
                    }
                    cn.Open();
                    var dt = new DataTable();
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex,
                    modulo: "DALBase.EjecutarConsultaConTVPDataTable",
                    spNombre: storedProcedure,
                    parametros: parametrosAdicionales);

                throw new Exception(ex.Message, ex);
            }                      
        }
              


        #region === NUMERACIÓN DE DOCUMENTOS ===

        /// <summary>
        /// Datos del siguiente correlativo (sin incrementar) para un tipo de documento.
        /// </summary>
        public class NumeracionPrevia
        {
            public int IdNume { get; set; }
            public int UltNumAsignado { get; set; }
            public int SiguienteNumero { get; set; }
            public int Ceros { get; set; }
            public int Anio { get; set; }
            public int IdTipoDte { get; set; }
            public string Abreviatura { get; set; }
            public string NombreDte { get; set; }

            /// <summary>Devuelve el siguiente número formateado con CEROS a la izquierda.</summary>
            public string SiguienteNumeroFormateado()
                => Ceros > 0
                    ? SiguienteNumero.ToString().PadLeft(Ceros, '0')
                    : SiguienteNumero.ToString();
        }

        /// <summary>
        /// Obtiene el siguiente correlativo (sin incrementar) para mostrarlo en el form.
        /// </summary>
        /// <param name="abreviatura">Abreviatura del documento en TIPO_DTE (ej: "QD", "FE", "CCF").</param>
        /// <param name="anio">
        ///   Año del correlativo. Pasa NULL para documentos con numeración CORRIDA (sin reinicio anual).
        ///   Pasa el año específico para documentos que sí lo distinguen.
        /// </param>
        /// <returns>Datos del preview, o null si no existe numeración configurada.</returns>
        public NumeracionPrevia ObtenerNumeracionPrevia(string abreviatura, int? anio = null)
        {
            DataTable dt = EjecutarConsulta("SP_DOCUMENTO_NUMERACION",
                new
                {
                    ACCION = "PREVIEW_POR_ABREV",
                    ABREVIATURA = abreviatura,
                    ANIO = anio   // si es null Dapper manda DBNull -> el SP lo trata como "no filtrar"
                });

            if (dt.Rows.Count == 0) return null;

            DataRow r = dt.Rows[0];
            return new NumeracionPrevia
            {
                IdNume = Convert.ToInt32(r["ID_NUME"]),
                UltNumAsignado = Convert.ToInt32(r["ULT_NUM_ASIGNADO"]),
                SiguienteNumero = Convert.ToInt32(r["SIGUIENTE_NUMERO"]),
                Ceros = Convert.ToInt32(r["CEROS"]),
                Anio = Convert.ToInt32(r["ANIO"]),
                IdTipoDte = Convert.ToInt32(r["ID_TIPO_DTE"]),
                Abreviatura = r["ABREVIATURA"].ToString(),
                NombreDte = r["NOMBRE_DTE"].ToString()
            };
        }

        public static string NuevoGUID()
        {            
            return Guid.NewGuid().ToString().ToUpper();
        }      

        #endregion
    }
}
