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
    public class CD_CONTACTOS
    {
        private readonly CD_CONEXION _connection;
        DataTable tabla = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();

        public CD_CONTACTOS(IConfiguration configuration)
        {
            _connection = new CD_CONEXION(configuration);
        }

        #region InsertarContactos
        public void InsertarContacos(CE_CONTACTOS obj)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_CONTACTOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "INS";
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = obj.Id_Persona;
            cmd.Parameters.Add("@Tipo_Contacto", SqlDbType.Int).Value = obj.Tipo_Contacto;
            cmd.Parameters.Add("@Contacto", SqlDbType.VarChar, 200).Value = obj.Contacto;
            cmd.Parameters.Add("@Codigo_Postal", SqlDbType.VarChar, 10).Value = obj.Codigo_Postal;
            cmd.Parameters.Add("@Pais", SqlDbType.Int).Value = obj.Pais;
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
        #endregion InsertarContactos

        #region ActualizarContactos

        public void ActualizarContactos(CE_CONTACTOS obj, out int resultado, out string mensaje)
        {
            

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_CONTACTOS";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "UPD";
            cmd.Parameters.Add("@Id_Contacto", SqlDbType.Int).Value = obj.Id_Contacto ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = obj.Id_Persona ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Tipo_Contacto", SqlDbType.Int).Value = obj.Tipo_Contacto ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Contacto", SqlDbType.VarChar, 200).Value = obj.Contacto;
            cmd.Parameters.Add("@Codigo_Postal", SqlDbType.VarChar, 10).Value = obj.Codigo_Postal;
            cmd.Parameters.Add("@Pais", SqlDbType.Int).Value = obj.Pais ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Modificador", SqlDbType.Int).Value = obj.Id_Modificador ?? (object)DBNull.Value;
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
                throw new Exception($"Error al actualizar el contacto: " + ex.Message, ex);
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }


        }

        #endregion ActualizarContactos

        #region ListarContactos
        public List<CE_CONTACTOS> ListarContactos()
        {
            List<CE_CONTACTOS> lts_contactos = new List<CE_CONTACTOS>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_CONTACTOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "LIS";
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tabla);

                if(tabla.Rows.Count > 0)
                {
                    foreach(DataRow dr in tabla.Rows)
                    {
                        CE_CONTACTOS fila = new CE_CONTACTOS();

                        fila.Id_Contacto = dr["Id_Contacto"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Contacto"]);
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Tipo_Contacto = dr["Tipo_Contacto"] is DBNull ? 0 : Convert.ToInt32(dr["Tipo_Contacto"]);
                        fila.Contacto = dr["Contacto"] is DBNull ? string.Empty : Convert.ToString(dr["Contacto"]);
                        fila.Codigo_Postal = dr["Codigo_Postal"] is DBNull ? string.Empty : Convert.ToString(dr["Codigo_Postal"]);
                        fila.Pais = dr["Pais"] is DBNull ? 0 : Convert.ToInt32(dr["Pais"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_contactos.Add(fila);


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

            return lts_contactos;
        }

        #endregion ListarContactos

        #region FiltrarContactos

        public List<CE_CONTACTOS> FiltrarContactos(CE_CONTACTOS obj)
        {
            List<CE_CONTACTOS> lts_contactos = new List<CE_CONTACTOS>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_CONTACTOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "FIL";
            cmd.Parameters.Add("@Contacto", SqlDbType.VarChar, 200).Value = obj.Contacto;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_CONTACTOS fila = new CE_CONTACTOS();

                        fila.Id_Contacto = dr["Id_Contacto"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Contacto"]);
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Tipo_Contacto = dr["Tipo_Contacto"] is DBNull ? 0 : Convert.ToInt32(dr["Tipo_Contacto"]);
                        fila.Contacto = dr["Contacto"] is DBNull ? string.Empty : Convert.ToString(dr["Contacto"]);
                        fila.Codigo_Postal = dr["Codigo_Postal"] is DBNull ? string.Empty : Convert.ToString(dr["Codigo_Postal"]);
                        fila.Pais = dr["Pais"] is DBNull ? 0 : Convert.ToInt32(dr["Pais"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_contactos.Add(fila);


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

            return lts_contactos;
        }

        #endregion FiltrarContactos

        #region FiltrarContactospID

        public List<CE_CONTACTOS> FiltrarContactosID(CE_CONTACTOS obj)
        {
            List<CE_CONTACTOS> lts_contactos = new List<CE_CONTACTOS>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_CONTACTOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "GET";
            cmd.Parameters.Add("@Id_Contacto", SqlDbType.VarChar, 200).Value = obj.Id_Contacto;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_CONTACTOS fila = new CE_CONTACTOS();

                        fila.Id_Contacto = dr["Id_Contacto"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Contacto"]);
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Tipo_Contacto = dr["Tipo_Contacto"] is DBNull ? 0 : Convert.ToInt32(dr["Tipo_Contacto"]);
                        fila.Contacto = dr["Contacto"] is DBNull ? string.Empty : Convert.ToString(dr["Contacto"]);
                        fila.Codigo_Postal = dr["Codigo_Postal"] is DBNull ? string.Empty : Convert.ToString(dr["Codigo_Postal"]);
                        fila.Pais = dr["Pais"] is DBNull ? 0 : Convert.ToInt32(dr["Pais"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_contactos.Add(fila);


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

            return lts_contactos;
        }

        #endregion FiltrarContactosID

    }
}
