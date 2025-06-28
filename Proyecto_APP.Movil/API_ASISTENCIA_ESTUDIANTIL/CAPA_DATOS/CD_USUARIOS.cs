using CAPA_ENTIDADES;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_DATOS
{
    public class CD_USUARIOS
    {
        private readonly CD_CONEXION _connection;
        DataTable tabla = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();

        public CD_USUARIOS(IConfiguration configuration)
        {
            _connection = new CD_CONEXION(configuration);
        }

        #region InsertarUsuarios

        public void InsertarUsuarios(CE_USUARIOS obj, out int resultado, out string mensaje)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_USUARIOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "INS";
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = obj.Id_Persona ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = obj.Usuario ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Contrasena", SqlDbType.VarChar, 255).Value = obj.Contrasena ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Intentos_Sesion", SqlDbType.Int).Value = obj.Intentos_Sesion ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Creador", SqlDbType.Int).Value = obj.Id_Creador ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.Id_Estado ?? (object)DBNull.Value;

            cmd.Parameters.Add("@O_Numero", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@O_Msg", SqlDbType.VarChar).Direction = ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();
                resultado = Convert.ToInt32(cmd.Parameters["@O_Numero"].Value);
                mensaje = Convert.ToString(cmd.Parameters["@O_Msg"].Value);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar Usuario: " + ex.Message, ex);
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }

        }

        #endregion InsertarUsuarios

        #region ActualizarUsuarios

        public void ActualizarUsuarios(CE_USUARIOS obj)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_USUARIOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "UPD";
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = obj.Id_Usuario;
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = obj.Id_Persona;
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = obj.Usuario;
            cmd.Parameters.Add("@Contrasena", SqlDbType.VarChar, 255).Value = obj.Contrasena;
            cmd.Parameters.Add("@Ultima_Sesion", SqlDbType.Char, 14).Value = obj.Ultima_Sesion;
            cmd.Parameters.Add("@Ultima_Cambio_Credenciales", SqlDbType.Char, 14).Value = obj.Ultima_Cambio_Credenciales;
            cmd.Parameters.Add("@Intentos_Sesion", SqlDbType.Int).Value = obj.Intentos_Sesion;
            cmd.Parameters.Add("@Id_Creador", SqlDbType.Int).Value = obj.Id_Creador;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.Id_Estado;
            try
            {
                cmd.ExecuteNonQuery();
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

        }

        #endregion ActualizarUsuarios

        #region ListarUsuarios

        public List<CE_USUARIOS> ListarUsuarios()
        {
            List<CE_USUARIOS> lts_Usuarios = new List<CE_USUARIOS>();
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_USUARIOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "LIS";
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tabla);
                if (tabla.Rows.Count > 0)
                {
                    foreach(DataRow dr in tabla.Rows)
                    {
                        CE_USUARIOS fila = new CE_USUARIOS();

                        fila.Id_Usuario = dr["Id_Usuario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Usuario"]);
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Usuario = dr["Usuario"] is DBNull ? string.Empty : Convert.ToString(dr["Usuario"]);
                        fila.Contrasena = dr["Contrasena"] is DBNull ? string.Empty : Convert.ToString(dr["Contrasena"]);
                        fila.Ultima_Sesion = dr["Ultima_Sesion"] is DBNull ? string.Empty : Convert.ToString(dr["Ultima_Sesion"]);
                        fila.Ultima_Cambio_Credenciales = dr["Ultima_Cambio_Credenciales"] is DBNull ? string.Empty : Convert.ToString(dr["Ultima_Cambio_Credenciales"]);
                        fila.Intentos_Sesion = dr["Intentos_Sesion"] is DBNull ? 0 : Convert.ToInt32(dr["Intentos_Sesion"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_Usuarios.Add(fila);
                    }
                }
            }
            catch(Exception ex)
            {
                string message = ex.Message;
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }

            return lts_Usuarios;


        }

        #endregion ListarUsuarios

        #region FiltrarUsuarios

        public List<CE_USUARIOS> FiltrarUsuarios(CE_USUARIOS obj)
        {
            List<CE_USUARIOS> lts_Usuarios = new List<CE_USUARIOS>();
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_USUARIOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "FIL";
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = obj.Usuario;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tabla);
                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_USUARIOS fila = new CE_USUARIOS();

                        fila.Id_Usuario = dr["Id_Usuario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Usuario"]);
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Usuario = dr["Usuario"] is DBNull ? string.Empty : Convert.ToString(dr["Usuario"]);
                        fila.Contrasena = dr["Contrasena"] is DBNull ? string.Empty : Convert.ToString(dr["Contrasena"]);
                        fila.Ultima_Sesion = dr["Ultima_Sesion"] is DBNull ? string.Empty : Convert.ToString(dr["Ultima_Sesion"]);
                        fila.Ultima_Cambio_Credenciales = dr["Ultima_Cambio_Credenciales"] is DBNull ? string.Empty : Convert.ToString(dr["Ultima_Cambio_Credenciales"]);
                        fila.Intentos_Sesion = dr["Intentos_Sesion"] is DBNull ? 0 : Convert.ToInt32(dr["Intentos_Sesion"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_Usuarios.Add(fila);
                    }
                }
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

            return lts_Usuarios;


        }

        #endregion FiltrarUsuarios

        #region FiltrarUsuariosPORID

        public List<CE_USUARIOS> FiltrarUsuariosID(CE_USUARIOS obj)
        {
            List<CE_USUARIOS> lts_Usuarios = new List<CE_USUARIOS>();
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_USUARIOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "GET";
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = obj.Id_Usuario;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tabla);
                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_USUARIOS fila = new CE_USUARIOS();

                        fila.Id_Usuario = dr["Id_Usuario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Usuario"]);
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Usuario = dr["Usuario"] is DBNull ? string.Empty : Convert.ToString(dr["Usuario"]);
                        fila.Contrasena = dr["Contrasena"] is DBNull ? string.Empty : Convert.ToString(dr["Contrasena"]);
                        fila.Ultima_Sesion = dr["Ultima_Sesion"] is DBNull ? string.Empty : Convert.ToString(dr["Ultima_Sesion"]);
                        fila.Ultima_Cambio_Credenciales = dr["Ultima_Cambio_Credenciales"] is DBNull ? string.Empty : Convert.ToString(dr["Ultima_Cambio_Credenciales"]);
                        fila.Intentos_Sesion = dr["Intentos_Sesion"] is DBNull ? 0 : Convert.ToInt32(dr["Intentos_Sesion"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_Usuarios.Add(fila);
                    }
                }
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

            return lts_Usuarios;


        }

        #endregion FiltrarUsuariosPORID
    }

}
