using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using CAPA_ENTIDADES;



namespace CAPA_DATOS
{
    public class CD_HORARIOS
    {
        private readonly CD_CONEXION _connection;
        DataTable tabla = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();

        public CD_HORARIOS(IConfiguration configuration)
        {
            _connection = new CD_CONEXION(configuration);
        }
        #region InsertarHorario
        public void InsertarHorario(CE_HORARIOS obj)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_HORARIOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "INS";
            cmd.Parameters.Add("@Id_Turno", SqlDbType.Int).Value = obj.Id_Turno;
            cmd.Parameters.Add("@Horario", SqlDbType.VarChar, 20).Value = obj.Horario;
            cmd.Parameters.Add("@Hora_Inicio", SqlDbType.Time).Value = obj.Hora_Inicio;
            cmd.Parameters.Add("@Hora_Fin", SqlDbType.Time).Value = obj.Hora_Fin;
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
        #endregion InsertarHorario

        #region ActualizarHorario
        public void ActualizarHorario(CE_HORARIOS obj, out int resultado, out string mensaje)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_ESTADOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "UPD";

            cmd.Parameters.Add("@Id_Horario", SqlDbType.Int).Value = obj.Id_Horario ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Turno", SqlDbType.Int).Value = obj.Id_Turno ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Horario", SqlDbType.VarChar, 20).Value = obj.Horario ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Hora_Inicio", SqlDbType.Time).Value = obj.Hora_Inicio ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Hora_Fin", SqlDbType.Time).Value = obj.Hora_Fin ?? (object)DBNull.Value;
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
                throw new Exception($"Error al actualizar horario: " + ex.Message, ex);
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }

        }
        #endregion ActualizarHorario

        #region ListarHorarios
        public List<CE_HORARIOS> ListarHorarios() //Todo metodo de tipo Lista debe constar de informacion para mostrarla
        {
            List<CE_HORARIOS> lts_horarios = new List<CE_HORARIOS>(); //Creando la informacion
            cmd.Connection = _connection.AbrirConexion(); //Abrimos la conexion
            cmd.CommandText = "SP_HORARIOS"; //Parametro nombre del procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure; //Le decimos el tipo de comando y le decimos que es un procedimiento almacenado
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "LIS"; //La accion es la que definimos en nuestro sp CRUD
            da.SelectCommand = cmd; 
            try
            {
                da.Fill(tabla); //significa rellenar el datatable que nosotros llamamos al inicio tabla va a rellenar la tabla de nuestra consulta a nivel de memoria
                if(tabla.Rows.Count > 0) //Me cuenta las filas de una tabla y si es mayor a 0 crea un bucle
                {
                    foreach(DataRow dr in tabla.Rows) //DataRow permite recorrer y reconocer multiples datos ya que recibiremos varchars, ints, dates, bits
                    {
                        CE_HORARIOS fila = new CE_HORARIOS(); //Instanciamos un objeto de la clase CE_ESTADOS para tener un codigo mas limpio
                        fila.Id_Horario = dr["Id_Horario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Horario"]);
                        fila.Id_Turno = dr["Id_Turno"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Turno"]);
                        fila.Horario = dr["Horario"] is DBNull ? string.Empty : Convert.ToString(dr["Horario"]);
                        fila.Hora_Inicio = dr["Hora_Inicio"] is DBNull ? string.Empty : Convert.ToString(dr["Hora_Inicio"]);
                        fila.Hora_Fin = dr["Hora_Fin"] is DBNull ? string.Empty : Convert.ToString(dr["Hora_Fin"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_horarios.Add(fila);
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

            return lts_horarios;
        }

        #endregion ListarHorarios

        #region FiltrarHorarios

        public List<CE_HORARIOS> FiltrarHorarios(CE_HORARIOS obj) //Todo metodo de tipo Lista debe constar de informacion para mostrarla
        {
            List<CE_HORARIOS> lts_horarios = new List<CE_HORARIOS>(); //Creando la informacion
            cmd.Connection = _connection.AbrirConexion(); //Abrimos la conexion
            cmd.CommandText = "SP_HORARIOS"; //Parametro nombre del procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure; //Le decimos el tipo de comando y le decimos que es un procedimiento almacenado
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "FIL"; //La accion es la que definimos en nuestro sp CRUD
            cmd.Parameters.Add("@Horario", SqlDbType.VarChar, 20).Value = obj.Horario;
            da.SelectCommand = cmd;
            try
            {
                da.Fill(tabla); //significa rellenar el datatable que nosotros llamamos al inicio tabla va a rellenar la tabla de nuestra consulta a nivel de memoria
                if (tabla.Rows.Count > 0) //Me cuenta las filas de una tabla y si es mayor a 0 crea un bucle
                {
                    foreach (DataRow dr in tabla.Rows) //DataRow permite recorrer y reconocer multiples datos ya que recibiremos varchars, ints, dates, bits
                    {
                        CE_HORARIOS fila = new CE_HORARIOS(); //Instanciamos un objeto de la clase CE_ESTADOS para tener un codigo mas limpio
                        fila.Id_Horario = dr["Id_Horario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Horario"]);
                        fila.Id_Turno = dr["Id_Turno"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Turno"]);
                        fila.Horario = dr["Horario"] is DBNull ? string.Empty : Convert.ToString(dr["Horario"]);
                        fila.Hora_Inicio = dr["Hora_Inicio"] is DBNull ? string.Empty : Convert.ToString(dr["Hora_Inicio"]);
                        fila.Hora_Fin = dr["Hora_Fin"] is DBNull ? string.Empty : Convert.ToString(dr["Hora_Fin"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_horarios.Add(fila);
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

            return lts_horarios;
        }

        #endregion FiltrarHorarios

        #region FiltrarHorariosPORID

        public List<CE_HORARIOS> FiltrarHorariosID(CE_HORARIOS obj) //Todo metodo de tipo Lista debe constar de informacion para mostrarla
        {
            List<CE_HORARIOS> lts_horarios = new List<CE_HORARIOS>(); //Creando la informacion
            cmd.Connection = _connection.AbrirConexion(); //Abrimos la conexion
            cmd.CommandText = "SP_HORARIOS"; //Parametro nombre del procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure; //Le decimos el tipo de comando y le decimos que es un procedimiento almacenado
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "GET"; //La accion es la que definimos en nuestro sp CRUD
            cmd.Parameters.Add("@Id_Horario", SqlDbType.VarChar, 20).Value = obj.Id_Horario;
            da.SelectCommand = cmd;
            try
            {
                da.Fill(tabla); //significa rellenar el datatable que nosotros llamamos al inicio tabla va a rellenar la tabla de nuestra consulta a nivel de memoria
                if (tabla.Rows.Count > 0) //Me cuenta las filas de una tabla y si es mayor a 0 crea un bucle
                {
                    foreach (DataRow dr in tabla.Rows) //DataRow permite recorrer y reconocer multiples datos ya que recibiremos varchars, ints, dates, bits
                    {
                        CE_HORARIOS fila = new CE_HORARIOS(); //Instanciamos un objeto de la clase CE_ESTADOS para tener un codigo mas limpio
                        fila.Id_Horario = dr["Id_Horario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Horario"]);
                        fila.Id_Turno = dr["Id_Turno"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Turno"]);
                        fila.Horario = dr["Horario"] is DBNull ? string.Empty : Convert.ToString(dr["Horario"]);
                        fila.Hora_Inicio = dr["Hora_Inicio"] is DBNull ? string.Empty : Convert.ToString(dr["Hora_Inicio"]);
                        fila.Hora_Fin = dr["Hora_Fin"] is DBNull ? string.Empty : Convert.ToString(dr["Hora_Fin"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_horarios.Add(fila);
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

            return lts_horarios;
        }

        #endregion FiltrarHorariosPORID
    }
}
