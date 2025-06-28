using CAPA_ENTIDADES;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_DATOS
{
    public class CD_ESTUDIANTES
    {
        private readonly CD_CONEXION _connection;

        DataTable tabla = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public CD_ESTUDIANTES(IConfiguration configuration)
        {
            _connection = new CD_CONEXION(configuration);
        }

        #region InsertarEstudiantes

        public void InsertarEstudiantes(CE_ESTUDIANTES obj)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ESTUDIANTES";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "INS";
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = obj.Id_Persona;
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

        #endregion InsertarEstudiantes

        #region ActualizarEstudiantes

        public void ActualizarEstudiantes(CE_ESTUDIANTES obj, out int resultado, out string mensaje)
        {
            if (obj.Id_Estudiante == null || obj.Id_Estudiante == 0)
                throw new ArgumentException("EL ID DEL ESTUDIANTE NO PUEDE SER NULO NI IGUAL A CERO");

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_DATOS_PERSONALES";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "UPD";
            cmd.Parameters.Add("@Id_Estudiante", SqlDbType.Int).Value = obj.Id_Estudiante ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = obj.Id_Persona ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Creador", SqlDbType.Int).Value = obj.Id_Creador ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.Id_Estado ?? (object)DBNull.Value;

            cmd.Parameters.Add("@O_Numero", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@O_Msg", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();

                resultado = Convert.ToInt32(cmd.Parameters["@O_Numero"].Value);
                mensaje = Convert.ToString(cmd.Parameters["@O_Msg"].Value);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar estudiante: " + ex.Message, ex);
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }

        }

        #endregion ActualizarEstudiantes

        #region ListarEstudiantes

        public List<CE_ESTUDIANTES> ListarEstudiantes()
        {
            List<CE_ESTUDIANTES> lts_estudiantes = new List<CE_ESTUDIANTES>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ESTUDIANTES";
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
                        CE_ESTUDIANTES fila = new CE_ESTUDIANTES();

                        fila.Id_Estudiante = dr["Id_Estudiante"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estudiante"]);
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_estudiantes.Add(fila);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return lts_estudiantes;
        }

        #endregion ListarEstudiantes

        #region FiltrarEstudiantes

        public List<CE_ESTUDIANTES> FiltrarEstudiantes(CE_ESTUDIANTES obj)
        {
            List<CE_ESTUDIANTES> lts_estudiantes = new List<CE_ESTUDIANTES>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ESTUDIANTES";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "FIL";
            cmd.Parameters.Add("@Id_Estudiante", SqlDbType.Int).Value = obj.Id_Estudiante;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_ESTUDIANTES fila = new CE_ESTUDIANTES();

                        fila.Id_Estudiante = dr["Id_Estudiante"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estudiante"]);
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_estudiantes.Add(fila);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return lts_estudiantes;
        }

        #endregion FiltrarEstudiantes

        #region FiltrarEstudiantesPORID

        public List<CE_ESTUDIANTES> FiltrarEstudiantesID(CE_ESTUDIANTES obj)
        {
            List<CE_ESTUDIANTES> lts_estudiantes = new List<CE_ESTUDIANTES>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ESTUDIANTES";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "GET";
            cmd.Parameters.Add("@Id_Estudiante", SqlDbType.Int).Value = obj.Id_Estudiante;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_ESTUDIANTES fila = new CE_ESTUDIANTES();

                        fila.Id_Estudiante = dr["Id_Estudiante"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estudiante"]);
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_estudiantes.Add(fila);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return lts_estudiantes;
        }

        #endregion FiltrarEstudiantes
    }
}
