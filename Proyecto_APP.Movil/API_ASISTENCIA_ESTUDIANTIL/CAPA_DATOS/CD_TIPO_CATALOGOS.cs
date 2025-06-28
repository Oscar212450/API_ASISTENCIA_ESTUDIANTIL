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
    public class CD_TIPO_CATALOGOS
    {
        private readonly CD_CONEXION _connection;
        DataTable tabla = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();

        public CD_TIPO_CATALOGOS(IConfiguration configuration)
        {
            _connection = new CD_CONEXION(configuration);
        }

        #region InsertarTipoCatalogo
        public void InsertarTipoCatalogo(CE_TIPO_CATALOGOS obj)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_TIPO_CATALOGOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "INS";
            cmd.Parameters.Add("@Tipo_Catalogo", SqlDbType.VarChar, 80).Value = obj.Tipo_Catalogo;
            cmd.Parameters.Add("@Id_Creador", SqlDbType.Int).Value = obj.Id_Creador;
            cmd.Parameters.Add("@Id_Modificador", SqlDbType.Int).Value = obj.Id_Modificador;
            cmd.Parameters.Add("@Activo", SqlDbType.Int).Value = obj.Activo;
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
        #endregion InsertarTipoCatalogo

        #region ActualizarTipoCatalogo
        public void ActualizarTipoCatalogo(CE_TIPO_CATALOGOS obj, out int resultado, out string mensaje)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_TIPO_CATALOGOS";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "UPD";
            cmd.Parameters.Add("@Id_Tipo_Catalogo", SqlDbType.Int).Value = obj.Id_Tipo_Catalogo ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Tipo_Catalogo", SqlDbType.VarChar, 80).Value = obj.Tipo_Catalogo;
            cmd.Parameters.Add("@Id_Modificador", SqlDbType.Int).Value = obj.Id_Modificador ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Activo", SqlDbType.Int).Value = obj.Activo ?? (object)DBNull.Value;

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
        #endregion ActualizarTipoCatalogo

        #region ListarTipoCatalogo

        public List<CE_TIPO_CATALOGOS> ListarTipoCatalogos()
        {
            List<CE_TIPO_CATALOGOS> lts_Tipo_Catalogos = new List<CE_TIPO_CATALOGOS>();
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_TIPO_CATALOGOS";
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
                        CE_TIPO_CATALOGOS fila = new CE_TIPO_CATALOGOS();
                        fila.Id_Tipo_Catalogo = dr["Id_Tipo_Catalogo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Tipo_Catalogo"]);
                        fila.Tipo_Catalogo = dr["Tipo_Catalogo"] is DBNull ? string.Empty : Convert.ToString(dr["Tipo_Catalogo"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Activo = dr["Activo"] is DBNull ? 0 : Convert.ToInt32(dr["Activo"]);

                        lts_Tipo_Catalogos.Add(fila);

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

            return lts_Tipo_Catalogos;
        }

        #endregion ListarTipoCatalogo

        #region FiltrarTipoCatalogo

        public List<CE_TIPO_CATALOGOS> FiltrarTipoCatalogos(CE_TIPO_CATALOGOS obj)
        {
            List<CE_TIPO_CATALOGOS> lts_Tipo_Catalogos = new List<CE_TIPO_CATALOGOS>();
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_TIPO_CATALOGOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "FIL";
            cmd.Parameters.Add("@Tipo_Catalogo", SqlDbType.VarChar, 80).Value = obj.Tipo_Catalogo;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tabla);
                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_TIPO_CATALOGOS fila = new CE_TIPO_CATALOGOS();
                        fila.Id_Tipo_Catalogo = dr["Id_Tipo_Catalogo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Tipo_Catalogo"]);
                        fila.Tipo_Catalogo = dr["Tipo_Catalogo"] is DBNull ? string.Empty : Convert.ToString(dr["Tipo_Catalogo"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Activo = dr["Activo"] is DBNull ? 0 : Convert.ToInt32(dr["Activo"]);

                        lts_Tipo_Catalogos.Add(fila);

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

            return lts_Tipo_Catalogos;
        }

        #endregion FiltrarTipoCatalogo

        #region FiltrarTipoCatalogoPORID

        public List<CE_TIPO_CATALOGOS> FiltrarTipoCatalogosID(CE_TIPO_CATALOGOS obj)
        {
            List<CE_TIPO_CATALOGOS> lts_Tipo_Catalogos = new List<CE_TIPO_CATALOGOS>();
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_TIPO_CATALOGOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "GET";
            cmd.Parameters.Add("@Id_Tipo_Catalogo", SqlDbType.Int).Value = obj.Id_Tipo_Catalogo;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tabla);
                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_TIPO_CATALOGOS fila = new CE_TIPO_CATALOGOS();
                        fila.Id_Tipo_Catalogo = dr["Id_Tipo_Catalogo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Tipo_Catalogo"]);
                        fila.Tipo_Catalogo = dr["Tipo_Catalogo"] is DBNull ? string.Empty : Convert.ToString(dr["Tipo_Catalogo"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Activo = dr["Activo"] is DBNull ? 0 : Convert.ToInt32(dr["Activo"]);

                        lts_Tipo_Catalogos.Add(fila);

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

            return lts_Tipo_Catalogos;
        }

        #endregion FiltrarTipoCatalogoPORID
    }
}
