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
    public class CD_DOCENTE_GRUPO
    {
        private readonly CD_CONEXION _connection;
        DataTable tabla = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public CD_DOCENTE_GRUPO(IConfiguration configuration)
        {
            _connection = new CD_CONEXION(configuration);

        }

        #region InsertarDocenteGrupo
        public void InsertarDocenteGrupo(CE_DOCENTE_GRUPO obj)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_DOCENTE_GRUPO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "INS";
            cmd.Parameters.Add("@Id_Docente", SqlDbType.Int).Value = obj.Id_Docente;
            cmd.Parameters.Add("@Id_Grupo", SqlDbType.Int).Value = obj.Id_Grupo;
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

        #endregion InsertarDocenteGrupo

        #region ActualizarDocenteGrupo

        public void ActualizarDocenteGrupo(CE_DOCENTE_GRUPO obj, out int resultado, out string mensaje)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_DOCENTE_GRUPO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "UPD";
            cmd.Parameters.Add("@Id_Docente_Grupo", SqlDbType.Int).Value = obj.Id_Docente_Grupo ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Docente", SqlDbType.Int).Value = obj.Id_Docente ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Grupo", SqlDbType.Int).Value = obj.Id_Grupo ?? (object)DBNull.Value;
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
                throw new Exception($"Error al actualizar Docente: " + ex.Message, ex);


            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }

        }

        #endregion ActualizarDocenteGrupo

        #region ListarDocenteGrupo

        public List<CE_DOCENTE_GRUPO> ListardDocenteGrupo()
        {
            List<CE_DOCENTE_GRUPO> lts_docente_grupo = new List<CE_DOCENTE_GRUPO>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_DOCENTE_GRUPO";
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
                        CE_DOCENTE_GRUPO fila = new CE_DOCENTE_GRUPO();

                        fila.Id_Docente_Grupo = dr["Id_Docente_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Docente_Grupo"]);
                        fila.Id_Docente = dr["Id_Docente"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Docente"]);
                        fila.Id_Grupo = dr["Id_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_docente_grupo.Add(fila);
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
            return lts_docente_grupo;
        }

        #endregion ListarDocenteGrupo

        #region FiltrarDocenteGrupo

        public List<CE_DOCENTE_GRUPO> FiltrarDocenteGrupo(CE_DOCENTE_GRUPO obj)
        {
            List<CE_DOCENTE_GRUPO> lts_docente_grupo = new List<CE_DOCENTE_GRUPO>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_DOCENTE_GRUPO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "FIL";
            cmd.Parameters.Add("@Id_Docente", SqlDbType.VarChar, 20).Value = obj.Id_Docente;
            da.SelectCommand = cmd;


            try
            {
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_DOCENTE_GRUPO fila = new CE_DOCENTE_GRUPO();

                        fila.Id_Docente_Grupo = dr["Id_Docente_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Docente_Grupo"]);
                        fila.Id_Docente = dr["Id_Docente"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Docente"]);
                        fila.Id_Grupo = dr["Id_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_docente_grupo.Add(fila);
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

            return lts_docente_grupo;
        }

        #endregion FiltrarDocenteGrupo

        #region FiltrarDocenteGrupoPORID

        public List<CE_DOCENTE_GRUPO> FiltrarDocenteGrupoID(CE_DOCENTE_GRUPO obj)
        {
            List<CE_DOCENTE_GRUPO> lts_docente_grupo = new List<CE_DOCENTE_GRUPO>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_DOCENTE_GRUPO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "GET";
            cmd.Parameters.Add("@Id_Docente_Grupo", SqlDbType.VarChar, 20).Value = obj.Id_Docente_Grupo;
            da.SelectCommand = cmd;


            try
            {
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_DOCENTE_GRUPO fila = new CE_DOCENTE_GRUPO();

                        fila.Id_Docente_Grupo = dr["Id_Docente_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Docente_Grupo"]);
                        fila.Id_Docente = dr["Id_Docente"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Docente"]);
                        fila.Id_Grupo = dr["Id_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_docente_grupo.Add(fila);
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

            return lts_docente_grupo;
        }

        #endregion FiltrarDocenteGrupoPORID




    }
}
