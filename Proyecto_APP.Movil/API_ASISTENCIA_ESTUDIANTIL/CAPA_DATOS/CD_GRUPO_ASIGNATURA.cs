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
    public class CD_GRUPO_ASIGNATURA
    {
        private readonly CD_CONEXION _connection;

        DataTable tabla = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();

        public CD_GRUPO_ASIGNATURA(IConfiguration configuration)
        {
            _connection = new CD_CONEXION(configuration);

        }

        #region InsertarGrupoAsignatura
        public void InsertarGrupoAsignatura(CE_GRUPO_ASIGNATURA obj)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_GRUPO_ASIGNATURA";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "INS";
            cmd.Parameters.Add("Id_Grupo", SqlDbType.Int).Value = obj.Id_Grupo;
            cmd.Parameters.Add("Id_Asignatura", SqlDbType.Int).Value = obj.Id_Asignatura;
            cmd.Parameters.Add("Id_Creador", SqlDbType.Int).Value = obj.Id_Creador;
            cmd.Parameters.Add("Id_Estado", SqlDbType.Int).Value = obj.Id_Estado;

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

        #endregion InsertarGrupoAsignatura

        #region ActualizarGrupoAsignatura
        public void ActualizarGrupoAsignatura(CE_GRUPO_ASIGNATURA obj, out int resultado, out string mensaje)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_GRUPO_ASIGNATURA";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "UPD";
            cmd.Parameters.Add("@Id_Grupo_Asignatura", SqlDbType.Int).Value = obj.Id_Grupo_Asignatura ?? (object)DBNull.Value;
            cmd.Parameters.Add("Id_Grupo", SqlDbType.Int).Value = obj.Id_Grupo ?? (object)DBNull.Value;
            cmd.Parameters.Add("Id_Asignatura", SqlDbType.Int).Value = obj.Id_Asignatura ?? (object)DBNull.Value;
            cmd.Parameters.Add("Id_Modificador", SqlDbType.Int).Value = obj.Id_Modificador ?? (object)DBNull.Value;
            cmd.Parameters.Add("Id_Estado", SqlDbType.Int).Value = obj.Id_Estado ?? (object)DBNull.Value;

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
                throw new Exception($"Error al actualizar grupo asignatura: " + ex.Message, ex);
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }

        }

        #endregion ActualizarGrupoAsignatura

        #region ListarGrupoAsignatura

        public List<CE_GRUPO_ASIGNATURA> ListarGrupoAsignatura()
        {
            List<CE_GRUPO_ASIGNATURA> lts_grupo_asignatura = new List<CE_GRUPO_ASIGNATURA>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_GRUPO_ASIGNATURA";
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
                        CE_GRUPO_ASIGNATURA fila = new CE_GRUPO_ASIGNATURA();

                        fila.Id_Grupo_Asignatura = dr["Id_Grupo_Asignatura"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo_Asignatura"]);
                        fila.Id_Grupo = dr["Id_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo"]);
                        fila.Id_Asignatura = dr["Id_Asignatura"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Asignatura"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_grupo_asignatura.Add(fila);
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
            return lts_grupo_asignatura;
        }

        #endregion ListarGrupoAsignatura

        #region FiltrarGrupoAsignatura

        public List<CE_GRUPO_ASIGNATURA> FiltrarGrupoAsignatura(CE_GRUPO_ASIGNATURA obj)
        {
            List<CE_GRUPO_ASIGNATURA> lts_grupo_asignatura = new List<CE_GRUPO_ASIGNATURA>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_GRUPO_ASIGNATURA";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "FIL";
            cmd.Parameters.Add("@Id_Grupo", SqlDbType.Int).Value = obj.Id_Grupo;
            da.SelectCommand = cmd;


            try
            {
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_GRUPO_ASIGNATURA fila = new CE_GRUPO_ASIGNATURA();

                        fila.Id_Grupo_Asignatura = dr["Id_Grupo_Asignatura"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo_Asignatura"]);
                        fila.Id_Grupo = dr["Id_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo"]);
                        fila.Id_Asignatura = dr["Id_Asignatura"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Asignatura"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_grupo_asignatura.Add(fila);
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
            return lts_grupo_asignatura;
        }

        #endregion FiltrarGrupoAsignatura

        #region FiltrarGrupoAsignaturaPORID

        public List<CE_GRUPO_ASIGNATURA> FiltrarGrupoAsignaturaID(CE_GRUPO_ASIGNATURA obj)
        {
            List<CE_GRUPO_ASIGNATURA> lts_grupo_asignatura = new List<CE_GRUPO_ASIGNATURA>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_GRUPO_ASIGNATURA";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "GET";
            cmd.Parameters.Add("@Id_Grupo_Asignatura", SqlDbType.Int).Value = obj.Id_Grupo_Asignatura; 
            da.SelectCommand = cmd;


            try
            {
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_GRUPO_ASIGNATURA fila = new CE_GRUPO_ASIGNATURA();

                        fila.Id_Grupo_Asignatura = dr["Id_Grupo_Asignatura"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo_Asignatura"]);
                        fila.Id_Grupo = dr["Id_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo"]);
                        fila.Id_Asignatura = dr["Id_Asignatura"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Asignatura"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_grupo_asignatura.Add(fila);
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
            return lts_grupo_asignatura;
        }

        #endregion FiltrarGrupoAsignaturaPORID
    }
}
