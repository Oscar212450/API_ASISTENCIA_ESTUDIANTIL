
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
    public class CD_ESTADOS
    {
        private readonly CD_CONEXION _connection;
        DataTable tabla = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public CD_ESTADOS(IConfiguration configuration)
        {
            _connection = new CD_CONEXION(configuration);

        }

        #region InsertarEstados

        public void InsertarEstados(CE_ESTADOS obj)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ESTADOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "INS";
            cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 80).Value = obj.Estado ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Creador", SqlDbType.Int).Value = obj.Id_Creador ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.Id_Estado ?? (object)DBNull.Value;

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


        #endregion InsertarEstado

        #region ActualizarEstado

        public void ActualizarEstado(CE_ESTADOS obj, out int resultado, out string mensaje)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ESTADOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "UPD";
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.Id_Estado ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 50).Value = obj.Estado ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Creador", SqlDbType.Int).Value = obj.Id_Creador ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Activo", SqlDbType.Int).Value = obj.Activo ?? (object)DBNull.Value;

            cmd.Parameters.Add("@O_Numero", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@O_Msg", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();
                resultado = Convert.ToInt32(cmd.Parameters["@O_Numero"].Value);
                mensaje = Convert.ToString(cmd.Parameters["@O_Msg"].Value);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar Estados: " + ex.Message, ex);
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }
        }

        #endregion ActualizarEstado

        #region ListarEstado
        public List<CE_ESTADOS> Listar_Estado() //obj de topo lista
        {
            List<CE_ESTADOS> lts_estado = new List<CE_ESTADOS>(); //obj tipo lista pero esta es como la variable interna


            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ESTADOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "LIS";
            //cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 80).Value = obj.Estado;
            //cmd.Parameters.Add("@Id_Creador", SqlDbType.Int).Value = obj.Id_Creador;
            //cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.Id_Estado;
            da.SelectCommand = cmd; // asemos uso de SqlDataAdapter(da) que me seleccione el commando el
                                    // smd porque ya trae el procedimiento almacena

            try
            {
                da.Fill(tabla); //rellenamos toda la informacion que trae la consulta sql que tiene el SelectCommand
                if (tabla.Rows.Count > 0) // conta las filas si tine mas de una fila que rellene la tabla 
                {
                    foreach (DataRow dr in tabla.Rows) //usamos el datarow para no estar intanciando por va ser mixto y se ba llamar dr
                    {
                        CE_ESTADOS fila = new CE_ESTADOS();//reutilizamos datos CE_ESTADOS

                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);
                        fila.Estado = dr["Estado"] is DBNull ? string.Empty : Convert.ToString(dr["Estado"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Activo = dr["Activo"] is DBNull ? (bool?)null : Convert.ToBoolean(dr["Activo"]);



                        lts_estado.Add(fila);// rellenamos cada fila 
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

            return lts_estado;

           
        }

        #endregion ListarEstado

        #region FiltrarEstado
        public List<CE_ESTADOS> Filtrar_Estado(CE_ESTADOS obj) //obj de topo lista
        {
            List<CE_ESTADOS> lts_estado = new List<CE_ESTADOS>(); //obj tipo lista pero esta es como la variable interna


            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ESTADOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "FIL";
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.Id_Estado;
            cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 50).Value = obj.Estado;
            da.SelectCommand = cmd; // asemos uso de SqlDataAdapter(da) que me seleccione el commando el
                                    // smd porque ya trae el procedimiento almacena

            try
            {
                da.Fill(tabla); //rellenamos toda la informacion que trae la consulta sql que tiene el SelectCommand
                if (tabla.Rows.Count > 0) // conta las filas si tine mas de una fila que rellene la tabla 
                {
                    foreach (DataRow dr in tabla.Rows) //usamos el datarow para no estarintanciando por va ser mixto y se ba llamar dr
                    {
                        CE_ESTADOS fila = new CE_ESTADOS();//reutilizamos datos CE_ESTADOS

                        fila.Id_Estado = dr["Id_Estados"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estados"]);
                        fila.Estado = dr["Estado"] is DBNull ? string.Empty : Convert.ToString(dr["Estado"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);


                        lts_estado.Add(fila);// rellenamos cada fila 
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

            return lts_estado;


        }

        #endregion FiltrarEstado

        #region FiltrarEstadoPORID

        public List<CE_ESTADOS> CD_Filtrar_EstadoID(CE_ESTADOS obj)
        {
            List<CE_ESTADOS> lts_estado = new List<CE_ESTADOS>();


            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ESTADOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "GET";
            cmd.Parameters.Add("@Id_Estados", SqlDbType.Int).Value = obj.Id_Estado;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tabla);
                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow dr in tabla.Rows)
                    {
                        CE_ESTADOS fila = new CE_ESTADOS();

                        fila.Id_Estado = dr["Id_Estados"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estados"]);
                        fila.Estado = dr["Estado"] is DBNull ? string.Empty : Convert.ToString(dr["Estado"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);


                        lts_estado.Add(fila);
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

            return lts_estado;


        }

        #endregion FiltrarEstadoPORID

    }
}
