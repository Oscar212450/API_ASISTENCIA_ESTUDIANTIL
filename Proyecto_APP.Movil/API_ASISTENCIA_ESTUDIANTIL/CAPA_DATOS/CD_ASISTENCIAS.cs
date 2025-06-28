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
    public class CD_ASISTENCIAS
    {
        private readonly CD_CONEXION _connection;
        DataTable tabla = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public CD_ASISTENCIAS(IConfiguration configuration)
        {
            _connection = new CD_CONEXION(configuration);

        }

        #region InsertarAsistencias
        public void InsertarAsistencia(CE_ASISTENCIAS obj)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ASISTENCIAS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "INS";
            cmd.Parameters.Add("@Id_Estudiante_Grupo", SqlDbType.Int).Value = obj.Id_Estudiante_Grupo;
            cmd.Parameters.Add("@Id_Grupo_Asignatura", SqlDbType.Int).Value = obj.Id_Grupo_Asignatura;
            cmd.Parameters.Add("@Id_Docente_Grupo", SqlDbType.Int).Value = obj.Id_Docente_Grupo;
            cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = obj.Fecha;
            cmd.Parameters.Add("@Asistio", SqlDbType.VarChar, 20).Value = obj.Asistio;
            cmd.Parameters.Add("@Observacion", SqlDbType.VarChar, 100).Value = obj.Observacion;
            cmd.Parameters.Add("@Id_Creador", SqlDbType.Int).Value = obj.Id_Creador;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.Id_Estado;


            try
            {
                cmd.ExecuteNonQuery();
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

        }

        #endregion InsertarAsistencias

        #region ActualizarAsistencia

        public void ActualizarAsistencia(CE_ASISTENCIAS obj, out int resultado, out string mensaje)
        {
            
                

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ASISTENCIAS";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "UPD";
            cmd.Parameters.Add("@Id_Asistencia", SqlDbType.Int).Value = obj.Id_Asistencia ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Estudiante_Grupo", SqlDbType.Int).Value = obj.Id_Estudiante_Grupo ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Grupo_Asignatura", SqlDbType.Int).Value = obj.Id_Grupo_Asignatura ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Docente_Grupo", SqlDbType.Int).Value = obj.Id_Docente_Grupo ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = obj.Fecha ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Asistio", SqlDbType.VarChar, 20).Value = obj.Asistio ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Observacion", SqlDbType.VarChar, 100).Value = obj.Observacion ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Creador", SqlDbType.Int).Value = obj.Id_Creador ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.Id_Estado ?? (object)DBNull.Value;

            cmd.Parameters.Add("@O_Numero", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@O_Msg", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();

                resultado = Convert.ToInt32(cmd.Parameters["@O_Numero"].Value);
                mensaje = Convert.ToString(cmd.Parameters["@O_Msg"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la asistencia: " + ex.Message, ex);

            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }
        }

        #endregion ActualizarAsistencia

        #region ListarAsistencia

        public List<CE_ASISTENCIAS> ListardAsistencias()
        {
            List<CE_ASISTENCIAS> lts_asistencias = new List<CE_ASISTENCIAS>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ASISTENCIAS";
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
                        CE_ASISTENCIAS fila = new CE_ASISTENCIAS();

                        fila.Id_Asistencia = dr["Id_Asistencia"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Asistencia"]);
                        fila.Id_Estudiante_Grupo = dr["Id_Estudiante_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estudiante_Grupo"]);
                        fila.Id_Grupo_Asignatura = dr["Id_Grupo_Asignatura"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo_Asignatura"]);
                        fila.Id_Docente_Grupo = dr["Id_Docente_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Docente_Grupo"]);
                        fila.Fecha = dr["Fecha"] is DBNull ? String.Empty : Convert.ToString(dr["Fecha"]);
                        fila.Asistio = dr["Asistio"] is DBNull ? string.Empty : Convert.ToString(dr["Asistio"]);
                        fila.Observacion = dr["Observacion"] is DBNull ? string.Empty : Convert.ToString(dr["Observacion"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_asistencias.Add(fila);
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
            return lts_asistencias;
        }

        #endregion ListarAsistencia

        #region FiltrarAsistencia

        public List<CE_ASISTENCIAS> FiltrarAsistencia(CE_ASISTENCIAS obj)
        {
            List<CE_ASISTENCIAS> lts_asistencias = new List<CE_ASISTENCIAS>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ASISTENCIAS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "FIL";
            cmd.Parameters.Add("@Id_Estudiante_Grupo", SqlDbType.Int).Value = obj.Id_Estudiante_Grupo;
            cmd.Parameters.Add("@Id_Grupo_Asignatura", SqlDbType.Int).Value = obj.Id_Grupo_Asignatura;
            cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = obj.Fecha;
            cmd.Parameters.Add("@Asistio", SqlDbType.VarChar, 20).Value = obj.Asistio;

            da.SelectCommand = cmd;


            try
            {
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_ASISTENCIAS fila = new CE_ASISTENCIAS();

                        fila.Id_Asistencia = dr["Id_Asistencia"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Asistencia"]);
                        fila.Id_Estudiante_Grupo = dr["Id_Estudiante_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estudiante_Grupo"]);
                        fila.Id_Grupo_Asignatura = dr["Id_Grupo_Asignatura"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo_Asignatura"]);
                        fila.Id_Docente_Grupo = dr["Id_Docente_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Docente_Grupo"]);
                        fila.Fecha = dr["Fecha"] is DBNull ? String.Empty : Convert.ToString(dr["Fecha"]);
                        fila.Asistio = dr["Asistio"] is DBNull ? string.Empty : Convert.ToString(dr["Asistio"]);
                        fila.Observacion = dr["Observacion"] is DBNull ? string.Empty : Convert.ToString(dr["Observacion"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_asistencias.Add(fila);
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

            return lts_asistencias;
        }

        #endregion FiltrarAsistencia

        #region FiltrarAsistenciaPORID

        public List<CE_ASISTENCIAS> FiltrarAsistenciaID(CE_ASISTENCIAS obj)
        {
            List<CE_ASISTENCIAS> lts_asistencias = new List<CE_ASISTENCIAS>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ASISTENCIAS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "GET";
            cmd.Parameters.Add("@Id_Asistencia", SqlDbType.VarChar, 20).Value = obj.Id_Asistencia;

            da.SelectCommand = cmd;


            try
            {
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_ASISTENCIAS fila = new CE_ASISTENCIAS();

                        fila.Id_Asistencia = dr["Id_Asistencia"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Asistencia"]);
                        fila.Id_Estudiante_Grupo = dr["Id_Estudiante_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estudiante_Grupo"]);
                        fila.Id_Grupo_Asignatura = dr["Id_Grupo_Asignatura"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo_Asignatura"]);
                        fila.Id_Docente_Grupo = dr["Id_Docente_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Docente_Grupo"]);
                        fila.Fecha = dr["Fecha"] is DBNull ? String.Empty : Convert.ToString(dr["Fecha"]);
                        fila.Asistio = dr["Asistio"] is DBNull ? string.Empty : Convert.ToString(dr["Asistio"]);
                        fila.Observacion = dr["Observacion"] is DBNull ? string.Empty : Convert.ToString(dr["Observacion"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_asistencias.Add(fila);
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

            return lts_asistencias;
        }

        #endregion FiltrarAsistenciaPORID
    }
}
