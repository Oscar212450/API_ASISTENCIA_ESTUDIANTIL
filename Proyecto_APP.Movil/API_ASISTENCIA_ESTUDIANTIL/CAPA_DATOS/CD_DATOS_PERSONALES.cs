using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPA_ENTIDADES;

namespace CAPA_DATOS
{
    public class CD_DATOS_PERSONALES
    {
        private readonly CD_CONEXION _connection;
        DataTable tabla = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();

        public CD_DATOS_PERSONALES(IConfiguration configuration)
        {
            _connection = new CD_CONEXION(configuration);
        }

        #region InsertarDatosPersonales
        public void InsertarDatosPersonales(CE_DATOS_PERSONALES obj)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_DATOS_PERSONALES";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "INS";
            cmd.Parameters.Add("@Primer_Nombre", SqlDbType.VarChar, 80).Value = obj.Primer_Nombre;
            cmd.Parameters.Add("@Segundo_Nombre", SqlDbType.VarChar, 80).Value = obj.Segundo_Nombre;
            cmd.Parameters.Add("@Primer_Apellido", SqlDbType.VarChar, 80).Value = obj.Primer_Apellido;
            cmd.Parameters.Add("@Segundo_Apellido", SqlDbType.VarChar, 80).Value = obj.Segundo_Apellido;
            cmd.Parameters.Add("@Edad", SqlDbType.Char, 2).Value = obj.Id_Creador;
            cmd.Parameters.Add("@Tipo_Cargo", SqlDbType.Int).Value = obj.Tipo_Cargo;
            cmd.Parameters.Add("@Tipo_DNI", SqlDbType.Int).Value = obj.Tipo_DNI;
            cmd.Parameters.Add("@DNI", SqlDbType.Int).Value = obj.DNI;
            cmd.Parameters.Add("@Genero", SqlDbType.Int).Value = obj.Genero;
            cmd.Parameters.Add("@Nacionalidad", SqlDbType.Int).Value = obj.Nacionalidad;
            cmd.Parameters.Add("@Departamento", SqlDbType.Int).Value = obj.Departamento;
            cmd.Parameters.Add("@Estado_Civil", SqlDbType.Int).Value = obj.Estado_Civil;
            cmd.Parameters.Add("@Id_Creador", SqlDbType.Int).Value = obj.Id_Creador;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Bit).Value = obj.Id_Estado;
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

        #endregion InsertarDatosPersonales

        #region ActualizarDatosPersonales

        public void ActualizarDatosPersonales(CE_DATOS_PERSONALES obj, out int resultado, out string mensaje)
        {
           
            

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_DATOS_PERSONALES";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "UPD";
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = obj.Id_Persona ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Primer_Nombre", SqlDbType.VarChar, 80).Value = obj.Primer_Nombre ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Segundo_Nombre", SqlDbType.VarChar, 80).Value = obj.Segundo_Nombre ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Primer_Apellido", SqlDbType.VarChar, 80).Value = obj.Primer_Apellido ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Segundo_Apellido", SqlDbType.VarChar, 80).Value = obj.Segundo_Apellido ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Edad", SqlDbType.Char, 2).Value = obj.Edad ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Tipo_Cargo", SqlDbType.Int).Value = obj.Tipo_Cargo ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Tipo_DNI", SqlDbType.Int).Value = obj.Tipo_DNI ?? (object)DBNull.Value;
            cmd.Parameters.Add("@DNI", SqlDbType.Int).Value = obj.DNI ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Genero", SqlDbType.Int).Value = obj.Genero ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Nacionalidad", SqlDbType.Int).Value = obj.Nacionalidad ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Departamento", SqlDbType.Int).Value = obj.Departamento ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Estado_Civil", SqlDbType.Int).Value = obj.Estado_Civil ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Modificador", SqlDbType.Int).Value = obj.Id_Modificador ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Bit).Value = obj.Id_Estado ?? (object)DBNull.Value;

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
                throw new Exception($"Error al actualizar persona: " + ex.Message, ex);

            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }
        }

        #endregion ActualizarDatosPersonales

        #region ListarDatosPersonales

        public List<CE_DATOS_PERSONALES> ListarDatosPersonales() //Todo metodo de tipo Lista debe constar de informacion para mostrarla
        {
            List<CE_DATOS_PERSONALES> lts_datos_personales = new List<CE_DATOS_PERSONALES>(); //Creando la informacion
            cmd.Connection = _connection.AbrirConexion(); //Abrimos la conexion
            cmd.CommandText = "SP_DATOS_PERSONALES"; //Parametro nombre del procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure; //Le decimos el tipo de comando y le decimos que es un procedimiento almacenado
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "LIS"; //La accion es la que definimos en nuestro sp CRUD
            da.SelectCommand = cmd;
            try
            {
                da.Fill(tabla); //significa rellenar el datatable que nosotros llamamos al inicio tabla va a rellenar la tabla de nuestra consulta a nivel de memoria
                if (tabla.Rows.Count > 0) //Me cuenta las filas de una tabla y si es mayor a 0 crea un bucle
                {
                    foreach (DataRow dr in tabla.Rows) //DataRow permite recorrer y reconocer multiples datos ya que recibiremos varchars, ints, dates, bits
                    {
                        CE_DATOS_PERSONALES fila = new CE_DATOS_PERSONALES(); //Instanciamos un objeto de la clase CE_ESTADOS para tener un codigo mas limpio
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Primer_Nombre = dr["Primer_Nombre"] is DBNull ? string.Empty : Convert.ToString(dr["Primer_Nombre"]);
                        fila.Segundo_Nombre = dr["Segundo_Nombre"] is DBNull ? string.Empty : Convert.ToString(dr["Segundo_Nombre"]);
                        fila.Primer_Apellido = dr["Primer_Apellido"] is DBNull ? string.Empty : Convert.ToString(dr["Primer_Apellido"]);
                        fila.Segundo_Apellido = dr["Segundo_Apellido"] is DBNull ? string.Empty : Convert.ToString(dr["Segundo_Apellido"]);
                        fila.Edad = dr["Edad"] is DBNull ? string.Empty : Convert.ToString(dr["Edad"]);
                        fila.Tipo_Cargo = dr["Tipo_Cargo"] is DBNull ? 0 : Convert.ToInt32(dr["Tipo_Cargo"]);
                        fila.Tipo_DNI = dr["Tipo_DNI"] is DBNull ? 0 : Convert.ToInt32(dr["Tipo_DNI"]);
                        fila.DNI = dr["DNI"] is DBNull ? string.Empty : Convert.ToString(dr["DNI"]);
                        fila.Genero = dr["Genero"] is DBNull ? 0 : Convert.ToInt32(dr["Genero"]);
                        fila.Nacionalidad = dr["Nacionalidad"] is DBNull ? 0 : Convert.ToInt32(dr["Nacionalidad"]);
                        fila.Departamento = dr["Departamento"] is DBNull ? 0 : Convert.ToInt32(dr["Departamento"]);
                        fila.Estado_Civil = dr["Estado_Civil"] is DBNull ? 0 : Convert.ToInt32(dr["Estado_Civil"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_datos_personales.Add(fila);
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

            return lts_datos_personales;
        }

        #endregion ListarDatosPersonales

        #region FiltrarDatosPersonales

         public List<CE_DATOS_PERSONALES> FiltrarDatosPersonales(CE_DATOS_PERSONALES obj) //Todo metodo de tipo Lista debe constar de informacion para mostrarla
        {
            List<CE_DATOS_PERSONALES> lts_datos_personales = new List<CE_DATOS_PERSONALES>(); //Creando la informacion
            cmd.Connection = _connection.AbrirConexion(); //Abrimos la conexion
            cmd.CommandText = "SP_DATOS_PERSONALES"; //Parametro nombre del procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure; //Le decimos el tipo de comando y le decimos que es un procedimiento almacenado
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "FIL"; //La accion es la que definimos en nuestro sp CRUD
            cmd.Parameters.Add("@DNI", SqlDbType.VarChar, 80).Value = obj.DNI;
            cmd.Parameters.Add("@Primer_Apellido", SqlDbType.VarChar, 80).Value = obj.Primer_Apellido;
            da.SelectCommand = cmd;
            try
            {
                da.Fill(tabla); //significa rellenar el datatable que nosotros llamamos al inicio tabla va a rellenar la tabla de nuestra consulta a nivel de memoria
                if (tabla.Rows.Count > 0) //Me cuenta las filas de una tabla y si es mayor a 0 crea un bucle
                {
                    foreach (DataRow dr in tabla.Rows) //DataRow permite recorrer y reconocer multiples datos ya que recibiremos varchars, ints, dates, bits
                    {
                        CE_DATOS_PERSONALES fila = new CE_DATOS_PERSONALES(); //Instanciamos un objeto de la clase CE_ESTADOS para tener un codigo mas limpio
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Primer_Nombre = dr["Primer_Nombre"] is DBNull ? string.Empty : Convert.ToString(dr["Primer_Nombre"]);
                        fila.Segundo_Nombre = dr["Segundo_Nombre"] is DBNull ? string.Empty : Convert.ToString(dr["Segundo_Nombre"]);
                        fila.Primer_Apellido = dr["Primer_Apellido"] is DBNull ? string.Empty : Convert.ToString(dr["Primer_Apellido"]);
                        fila.Segundo_Apellido = dr["Segundo_Apellido"] is DBNull ? string.Empty : Convert.ToString(dr["Segundo_Apellido"]);
                        fila.Edad = dr["Edad"] is DBNull ? string.Empty : Convert.ToString(dr["Edad"]);
                        fila.Tipo_Cargo = dr["Tipo_Cargo"] is DBNull ? 0 : Convert.ToInt32(dr["Tipo_Cargo"]);
                        fila.Tipo_DNI = dr["Tipo_DNI"] is DBNull ? 0 : Convert.ToInt32(dr["Tipo_DNI"]);
                        fila.DNI = dr["DNI"] is DBNull ? string.Empty : Convert.ToString(dr["DNI"]);
                        fila.Genero = dr["Genero"] is DBNull ? 0 : Convert.ToInt32(dr["Genero"]);
                        fila.Nacionalidad = dr["Nacionalidad"] is DBNull ? 0 : Convert.ToInt32(dr["Nacionalidad"]);
                        fila.Departamento = dr["Departamento"] is DBNull ? 0 : Convert.ToInt32(dr["Departamento"]);
                        fila.Estado_Civil = dr["Estado_Civil"] is DBNull ? 0 : Convert.ToInt32(dr["Estado_Civil"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_datos_personales.Add(fila);
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

            return lts_datos_personales;
        }

        #endregion FiltrarDatosPersonales
        #region FiltrarDatosPersonalesID

        public List<CE_DATOS_PERSONALES> FiltrarDatosPersonalesID(CE_DATOS_PERSONALES obj) //Todo metodo de tipo Lista debe constar de informacion para mostrarla
        {
            List<CE_DATOS_PERSONALES> lts_datos_personales = new List<CE_DATOS_PERSONALES>(); //Creando la informacion
            cmd.Connection = _connection.AbrirConexion(); //Abrimos la conexion
            cmd.CommandText = "SP_DATOS_PERSONALES"; //Parametro nombre del procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure; //Le decimos el tipo de comando y le decimos que es un procedimiento almacenado
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "GET"; //La accion es la que definimos en nuestro sp CRUD
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = obj.Id_Persona ?? (object)DBNull.Value;
            da.SelectCommand = cmd;
            try
            {
                da.Fill(tabla); //significa rellenar el datatable que nosotros llamamos al inicio tabla va a rellenar la tabla de nuestra consulta a nivel de memoria
                if (tabla.Rows.Count > 0) //Me cuenta las filas de una tabla y si es mayor a 0 crea un bucle
                {
                    foreach (DataRow dr in tabla.Rows) //DataRow permite recorrer y reconocer multiples datos ya que recibiremos varchars, ints, dates, bits
                    {
                        CE_DATOS_PERSONALES fila = new CE_DATOS_PERSONALES(); //Instanciamos un objeto de la clase CE_ESTADOS para tener un codigo mas limpio
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Primer_Nombre = dr["Primer_Nombre"] is DBNull ? string.Empty : Convert.ToString(dr["Primer_Nombre"]);
                        fila.Segundo_Nombre = dr["Segundo_Nombre"] is DBNull ? string.Empty : Convert.ToString(dr["Segundo_Nombre"]);
                        fila.Primer_Apellido = dr["Primer_Apellido"] is DBNull ? string.Empty : Convert.ToString(dr["Primer_Apellido"]);
                        fila.Segundo_Apellido = dr["Segundo_Apellido"] is DBNull ? string.Empty : Convert.ToString(dr["Segundo_Apellido"]);
                        fila.Edad = dr["Edad"] is DBNull ? string.Empty : Convert.ToString(dr["Edad"]);
                        fila.Tipo_Cargo = dr["Tipo_Cargo"] is DBNull ? 0 : Convert.ToInt32(dr["Tipo_Cargo"]);
                        fila.Tipo_DNI = dr["Tipo_DNI"] is DBNull ? 0 : Convert.ToInt32(dr["Tipo_DNI"]);
                        fila.DNI = dr["DNI"] is DBNull ? string.Empty : Convert.ToString(dr["DNI"]);
                        fila.Genero = dr["Genero"] is DBNull ? 0 : Convert.ToInt32(dr["Genero"]);
                        fila.Nacionalidad = dr["Nacionalidad"] is DBNull ? 0 : Convert.ToInt32(dr["Nacionalidad"]);
                        fila.Departamento = dr["Departamento"] is DBNull ? 0 : Convert.ToInt32(dr["Departamento"]);
                        fila.Estado_Civil = dr["Estado_Civil"] is DBNull ? 0 : Convert.ToInt32(dr["Estado_Civil"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);

                        lts_datos_personales.Add(fila);
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

            return lts_datos_personales;
        }

        #endregion FiltrarDatosPersonales


    }
}
