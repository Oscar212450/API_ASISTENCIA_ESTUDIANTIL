using CAPA_ENTIDADES;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_DATOS
{
    public class CD_ESTUDIANTE_GRUPO
    {
        private readonly CD_CONEXION _connection;
        DataTable tabla = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public CD_ESTUDIANTE_GRUPO(IConfiguration configuration)
        {
            _connection = new CD_CONEXION(configuration);

        }

        #region InsertarEstudianteGrupo
        public void InsertarEstudianteGrupo(CE_ESTUDIANTE_GRUPO obj, out int resultado, out string mensaje)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ESTUDIANTE_GRUPO";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "INS";
            cmd.Parameters.Add("@Id_Estudiante", SqlDbType.Int).Value = obj.Id_Estudiante ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Grupo", SqlDbType.Int).Value = obj.Id_Grupo ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Creador", SqlDbType.Int).Value = obj.Id_Creador ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.Id_Estado ?? (object)DBNull.Value;

            cmd.Parameters.Add("@O_Numero", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@O_Msg", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();
                resultado = Convert.ToInt32(cmd.Parameters["@O_Numero"].Value);
                mensaje = Convert.ToString(cmd.Parameters["@O_Msg"].Value);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en actualizar Estudiante Grupo" + ex.Message,ex);
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }

        }

        #endregion InsertarEstudianteGrupo

        #region ActualizarEstudianteGrupo

        public void ActualizarEstudianteGrupo(CE_ESTUDIANTE_GRUPO obj, out int resultado, out string mensaje)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ESTUDIANTE_GRUPO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "UPD";
            cmd.Parameters.Add("@Id_Estudiante_Grupo", SqlDbType.Int).Value = obj.Id_Estudiante_Grupo;
            cmd.Parameters.Add("@Id_Estudiante", SqlDbType.Int).Value = obj.Id_Estudiante;
            cmd.Parameters.Add("@Id_Grupo", SqlDbType.Int).Value = obj.Id_Grupo;
            cmd.Parameters.Add("@Id_Modificador", SqlDbType.Int).Value = obj.Id_Modificador;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.Id_Estado;

            cmd.Parameters.Add("@O_Numero",SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@O_Msg",SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();
                resultado = Convert.ToInt32(cmd.Parameters["@O_Numero"].Value);
                mensaje = Convert.ToString(cmd.Parameters["@O_Msg"].Value);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar Grupo Estudiante: " + ex.Message, ex);
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }

        }

        #endregion ActualizarEstudianteGrupo

        #region ListarEstudianteGrupo

        public List<CE_ESTUDIANTE_GRUPO> ListardEstudianteGrupo()
        {
            List<CE_ESTUDIANTE_GRUPO> lts_estudiante_grupo = new List<CE_ESTUDIANTE_GRUPO>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ESTUDIANTE_GRUPO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "LIS";
            da.SelectCommand = cmd;


            try
            {
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_ESTUDIANTE_GRUPO fila = new CE_ESTUDIANTE_GRUPO();

                        fila.Id_Estudiante_Grupo = dr["Id_Estudiante_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estudiante_Grupo"]);
                        fila.Id_Estudiante = dr["Id_Estudiante"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estudiante"]);
                        fila.Id_Grupo = dr["Id_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_estudiante_grupo.Add(fila);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }
            return lts_estudiante_grupo;
        }

        #endregion ListarEstudianteGrupo

        #region FiltrarEstudianteGrupo

        public List<CE_ESTUDIANTE_GRUPO> FiltrarEstudianteGrupo(CE_ESTUDIANTE_GRUPO obj)
        {
            List<CE_ESTUDIANTE_GRUPO> lts_estudiante_grupo = new List<CE_ESTUDIANTE_GRUPO>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ESTUDIANTE_GRUPO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "FIL";
            cmd.Parameters.Add("@Id_Estudiante", SqlDbType.VarChar, 20).Value = obj.Id_Estudiante;
            da.SelectCommand = cmd;


            try
            {
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_ESTUDIANTE_GRUPO fila = new CE_ESTUDIANTE_GRUPO();

                        fila.Id_Estudiante_Grupo = dr["Id_Estudiante_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estudiante_Grupo"]);
                        fila.Id_Estudiante = dr["Id_Estudiante"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estudiante"]);
                        fila.Id_Grupo = dr["Id_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_estudiante_grupo.Add(fila);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }

            return lts_estudiante_grupo;
        }

        #endregion FiltrarEstudianteGrupo

        #region FiltrarEstudianteGrupoPORID

        public List<CE_ESTUDIANTE_GRUPO> FiltrarEstudianteGrupoID(CE_ESTUDIANTE_GRUPO obj)
        {
            List<CE_ESTUDIANTE_GRUPO> lts_estudiante_grupo = new List<CE_ESTUDIANTE_GRUPO>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ESTUDIANTE_GRUPO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "GET";
            cmd.Parameters.Add("@Id_Estudiante_Grupo", SqlDbType.Int).Value = obj.Id_Estudiante_Grupo;
            da.SelectCommand = cmd;


            try
            {
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_ESTUDIANTE_GRUPO fila = new CE_ESTUDIANTE_GRUPO();

                        fila.Id_Estudiante_Grupo = dr["Id_Estudiante_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estudiante_Grupo"]);
                        fila.Id_Estudiante = dr["Id_Estudiante"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estudiante"]);
                        fila.Id_Grupo = dr["Id_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_estudiante_grupo.Add(fila);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }

            return lts_estudiante_grupo;
        }

        #endregion FiltrarEstudianteGrupoPORID

    }
}
