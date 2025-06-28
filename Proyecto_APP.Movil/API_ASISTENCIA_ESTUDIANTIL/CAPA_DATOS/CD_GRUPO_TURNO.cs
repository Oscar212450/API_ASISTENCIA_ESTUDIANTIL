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
    public class CD_GRUPO_TURNO
    {
        private readonly CD_CONEXION _connection;

        DataTable tabla = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();

        public CD_GRUPO_TURNO(IConfiguration configuration)
        {
            _connection = new CD_CONEXION(configuration);

        }

        #region InsertarGrupoTurno
        public void InsertarGrupoTurno(CE_GRUPO_TURNO obj)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_GRUPO_TURNO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "INS";
            cmd.Parameters.Add("Id_Grupo", SqlDbType.Int).Value = obj.Id_Grupo;
            cmd.Parameters.Add("Id_Horario", SqlDbType.Int).Value = obj.Id_Horario;
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

        #endregion InsertarGrupoTurno

        #region ActualizarGrupoTurno
        public void ActualizarGrupoTurno(CE_GRUPO_TURNO obj, out int resultado, out string mensaje)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_GRUPO_TURNO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "UPD";
            cmd.Parameters.Add("@Id_Grupo_Turno", SqlDbType.Int).Value = obj.Id_Grupo_Turno ?? (object)DBNull.Value;
            cmd.Parameters.Add("Id_Grupo", SqlDbType.Int).Value = obj.Id_Grupo ?? (object)DBNull.Value;
            cmd.Parameters.Add("Id_Horario", SqlDbType.Int).Value = obj.Id_Horario ?? (object)DBNull.Value;
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
                throw new Exception($"Error al actualizar grupo turno: " + ex.Message, ex);
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }

        }

        #endregion ActualizarGrupoTurno

        #region ListarGrupoTurno

        public List<CE_GRUPO_TURNO> ListarGrupoTurno()
        {
            List<CE_GRUPO_TURNO> lts_grupo_turno = new List<CE_GRUPO_TURNO>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_GRUPO_TURNO";
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
                        CE_GRUPO_TURNO fila = new CE_GRUPO_TURNO();

                        fila.Id_Grupo_Turno = dr["Id_Grupo_Turno"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo_Turno"]);
                        fila.Id_Grupo = dr["Id_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo"]);
                        fila.Id_Horario = dr["Id_Horario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Horario"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_grupo_turno.Add(fila);
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
            return lts_grupo_turno;
        }

        #endregion ListarGrupoTurno

        #region FiltrarGrupoTurno

        public List<CE_GRUPO_TURNO> FiltrarGrupoTurno(CE_GRUPO_TURNO obj)
        {
            List<CE_GRUPO_TURNO> lts_grupo_turno = new List<CE_GRUPO_TURNO>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_GRUPO_TURNO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "FIL";
            cmd.Parameters.Add("@Id_Horario", SqlDbType.Int).Value = obj.Id_Horario;
            da.SelectCommand = cmd;


            try
            {
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_GRUPO_TURNO fila = new CE_GRUPO_TURNO();

                        fila.Id_Grupo_Turno = dr["Id_Grupo_Turno"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo_Turno"]);
                        fila.Id_Grupo = dr["Id_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo"]);
                        fila.Id_Horario = dr["Id_Horario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Horario"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_grupo_turno.Add(fila);
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
            return lts_grupo_turno;
        }

        #endregion FiltrarGrupoTurno

        #region FiltrarGrupoTurnoPORID

        public List<CE_GRUPO_TURNO> FiltrarGrupoTurnoID(CE_GRUPO_TURNO obj)
        {
            List<CE_GRUPO_TURNO> lts_grupo_turno = new List<CE_GRUPO_TURNO>();

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_GRUPO_TURNO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "GET";
            cmd.Parameters.Add("@Id_Grupo_Turno", SqlDbType.Int).Value = obj.Id_Grupo_Turno;
            da.SelectCommand = cmd;


            try
            {
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_GRUPO_TURNO fila = new CE_GRUPO_TURNO();

                        fila.Id_Grupo_Turno = dr["Id_Grupo_Turno"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo_Turno"]);
                        fila.Id_Grupo = dr["Id_Grupo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Grupo"]);
                        fila.Id_Horario = dr["Id_Horario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Horario"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_grupo_turno.Add(fila);
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
            return lts_grupo_turno;
        }

        #endregion FiltrarGrupoTurnoPORID

    }
}
