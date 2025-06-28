using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Capa_Entidades;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Capa_Dato
{
    public class CD_Autor
    {
        private readonly CD_CONEXION _conexion;
        DataTable tabla = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();

        public CD_Autor(IConfiguration configuration) 
        { 
            _conexion = new CD_CONEXION(configuration);
        }

        #region Insert_Autor
        public void Insertar_Autor(CE_Autor Obj)
        {
            using (SqlCommand cmd = new SqlCommand("SP_AUTORES", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "INS";
                cmd.Parameters.Add("@Id_autor", SqlDbType.Int).Value = Obj.Id_autor ?? (object)DBNull.Value;
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 150).Value = Obj.Nombre ?? (object)DBNull.Value;
                cmd.Parameters.Add("@Biografia", SqlDbType.VarChar).Value = Obj.Biografia ?? (object)DBNull.Value;
                cmd.Parameters.Add("@Sitio_web", SqlDbType.VarChar, 255).Value = Obj.Sitio_web ?? (object)DBNull.Value;
                cmd.Parameters.Add("@Fecha_creacion", SqlDbType.Date).Value = DBNull.Value;
                cmd.Parameters.Add("@O_Numero", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@O_Msg", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                try
                {
                    cmd.ExecuteNonQuery();
                    int resultado = Convert.ToInt32(cmd.Parameters["@O_Numero"].Value);
                    string mensaje = cmd.Parameters["@O_Msg"].Value.ToString();

                    if (resultado != 0)
                    {
                        throw new Exception("SP Error: " + mensaje);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    _conexion.CerrarConexion();
                }
            }
        }


        #endregion Insert_Autor

        #region Actualizart_Autor
        public void Actualizar_Autor(CE_Autor Obj)
        {
            cmd.Connection = _conexion.AbrirConexion();
            cmd.CommandText = "SP_AUTORES";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "UPD";
            cmd.Parameters.Add("@Id_autor", SqlDbType.Int).Value = Obj.Id_autor;
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 150).Value = Obj.Nombre;
            cmd.Parameters.Add("@Biografia", SqlDbType.VarChar, 150).Value = Obj.Biografia;
            cmd.Parameters.Add("@Sitio_web", SqlDbType.VarChar, 150).Value = Obj.Sitio_web;
            
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
                cmd.Connection = _conexion.CerrarConexion();
            }
        }

        #endregion Actualizart_Autor

        #region Listar_Autores
        public List<CE_Autor> Listar_Autores()
        {
            List<CE_Autor> listaAutores = new List<CE_Autor>();
            cmd.Connection = _conexion.AbrirConexion();
            cmd.CommandText = "SP_AUTORES";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "LIS";
            da.SelectCommand = cmd;

            try
            {
                tabla.Clear(); // Asegúrate de limpiar la tabla antes de llenar
                da.Fill(tabla);
                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_Autor fila = new CE_Autor();
                        fila.Id_autor = dr["Id_autor"] is DBNull ? 0 : Convert.ToInt32(dr["Id_autor"]);
                        fila.Nombre = dr["Nombre"] is DBNull ? string.Empty : dr["Nombre"].ToString();
                        fila.Biografia = dr["Biografia"] is DBNull ? string.Empty : dr["Biografia"].ToString();
                        fila.Sitio_web = dr["Sitio_web"] is DBNull ? string.Empty : dr["Sitio_web"].ToString();


                        listaAutores.Add(fila);
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
                cmd.Connection = _conexion.CerrarConexion();
            }

            return listaAutores;
        }

        #endregion Listar_Autor

        #region Filtrar_Nombre
        public List<CE_Autor> Filtrar_Nombre(CE_Autor obj)
        {
            List<CE_Autor> listaAutores = new List<CE_Autor>();
            cmd.Connection = _conexion.AbrirConexion();
            cmd.CommandText = "SP_AUTORES"; 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "FIL";
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 150).Value = obj.Nombre ?? string.Empty;
            da.SelectCommand = cmd;

            try
            {
                tabla.Clear(); // Limpiar la tabla antes de llenar
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_Autor fila = new CE_Autor();
                        fila.Id_autor = dr["Id_autor"] is DBNull ? 0 : Convert.ToInt32(dr["Id_autor"]);
                        fila.Nombre = dr["Nombre"] is DBNull ? string.Empty : dr["Nombre"].ToString();
                        fila.Biografia = dr["Biografia"] is DBNull ? string.Empty : dr["Biografia"].ToString();
                        fila.Sitio_web = dr["Sitio_web"] is DBNull ? string.Empty : dr["Sitio_web"].ToString();


                        listaAutores.Add(fila);
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
                cmd.Connection = _conexion.CerrarConexion();
            }

            return listaAutores;
        }

        #endregion Filtrar_Nombre

        #region Filtrar_Por_ID
        public CE_Autor Obtener_Por_Id(int idAutor)
        {
            CE_Autor autor = null;
            cmd.Connection = _conexion.AbrirConexion();
            cmd.CommandText = "SP_AUTORES";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "GET";
            cmd.Parameters.Add("@Id_autor", SqlDbType.Int).Value = idAutor;
            da.SelectCommand = cmd;

            try
            {
                tabla.Clear();
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    DataRow dr = tabla.Rows[0];
                    autor = new CE_Autor
                    {
                        Id_autor = dr["Id_autor"] is DBNull ? 0 : Convert.ToInt32(dr["Id_autor"]),
                        Nombre = dr["Nombre"] is DBNull ? string.Empty : dr["Nombre"].ToString(),
                        Biografia = dr["Biografia"] is DBNull ? string.Empty : dr["Biografia"].ToString(),
                        Sitio_web = dr["Sitio_web"] is DBNull ? string.Empty : dr["Sitio_web"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _conexion.CerrarConexion();
            }

            return autor;
        }

        #endregion Filtrar_Por_ID
    }

}
   

