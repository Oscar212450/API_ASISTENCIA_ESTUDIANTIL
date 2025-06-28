
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
    public class CD_ASIGNATURAS
    {
        private readonly CD_CONEXION _connection;
        DataTable tabla = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public CD_ASIGNATURAS(IConfiguration configuration)
        {
            _connection = new CD_CONEXION(configuration);

        }

        #region InsertarAsignaturas

        public void InsertarAsignaturas(CE_ASIGNATURAS obj)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ASIGNATURAS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "INS";
            cmd.Parameters.Add("@Nombre_Asignatura", SqlDbType.VarChar, 50).Value = obj.Nombre_Asignatura;
            cmd.Parameters.Add("@Id_Creador", SqlDbType.Int).Value = obj.Id_Creador;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.Id_Estado;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }
        }


        #endregion InsertarAsignaturas

        #region ActualizarAsignaturas

        public void ActualizarAsignaturas(CE_ASIGNATURAS obj, out int resultado, out string mensaje)
        {
           
                

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ASIGNATURAS";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "UPD";
            cmd.Parameters.Add("@Id_Asignatura", SqlDbType.Int).Value = obj.Id_Asignatura ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Nombre_Asignatura", SqlDbType.VarChar, 50).Value = obj.Nombre_Asignatura ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Modificador", SqlDbType.Int).Value = obj.Id_Modificador ?? (object)DBNull.Value;
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
                throw new Exception($"Error al actualizar asignatura: " + ex.Message, ex);
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }
        }


        #endregion ActualizarAsignaturas

        #region ListarAsignaturas
        public List<CE_ASIGNATURAS> ListarAsignaturas()
        {
            List<CE_ASIGNATURAS> lts_asignaturas = new List<CE_ASIGNATURAS>();


            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ASIGNATURAS";
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
                        CE_ASIGNATURAS fila = new CE_ASIGNATURAS();

                        fila.Id_Asignatura = dr["Id_Asignatura"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Asignatura"]);
                        fila.Nombre_Asignatura = dr["Nombre_Asignatura"] is DBNull ? string.Empty : Convert.ToString(dr["Nombre_Asignatura"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_asignaturas.Add(fila);
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
                cmd.Connection = _connection.CerrarConexion();
            }

            return lts_asignaturas;

           
        }

        #endregion ListarAsignaturas

        #region FiltrarAsignaturas

        public List<CE_ASIGNATURAS> FiltrarAsignaturas(CE_ASIGNATURAS obj)
        {
            List<CE_ASIGNATURAS> lts_asignaturas = new List<CE_ASIGNATURAS>();


            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ASIGNATURAS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "FIL";
            cmd.Parameters.Add("@Nombre_Asignatura", SqlDbType.VarChar, 50).Value = obj.Nombre_Asignatura;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tabla);
                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_ASIGNATURAS fila = new CE_ASIGNATURAS();

                        fila.Id_Asignatura = dr["Id_Asignatura"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Asignatura"]);
                        fila.Nombre_Asignatura = dr["Nombre_Asignatura"] is DBNull ? string.Empty : Convert.ToString(dr["Nombre_Asignatura"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_asignaturas.Add(fila);
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
                cmd.Connection = _connection.CerrarConexion();
            }

            return lts_asignaturas;


        }

        #endregion FiltrarAsignaturas

        #region FiltrarAsignaturasID

        public List<CE_ASIGNATURAS> FiltrarAsignaturasID(CE_ASIGNATURAS obj)
        {
            List<CE_ASIGNATURAS> lts_asignaturas = new List<CE_ASIGNATURAS>();


            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ASIGNATURAS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "GET";
            cmd.Parameters.Add("@Id_Asignatura", SqlDbType.Int).Value = obj.Id_Asignatura;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tabla);
                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_ASIGNATURAS fila = new CE_ASIGNATURAS();

                        fila.Id_Asignatura = dr["Id_Asignatura"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Asignatura"]);
                        fila.Nombre_Asignatura = dr["Nombre_Asignatura"] is DBNull ? string.Empty : Convert.ToString(dr["Nombre_Asignatura"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_asignaturas.Add(fila);
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
                cmd.Connection = _connection.CerrarConexion();
            }

            return lts_asignaturas;


        }

        #endregion FiltrarAsignaturasID
    }
}
