using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPA_ENTIDADES;
using CAPA_DATOS;

namespace CAPA_DATOS
{
    public class CD_CATALOGOS
    {
        private readonly CD_CONEXION _connection;
        DataTable tabla = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();

        public CD_CATALOGOS(IConfiguration configuration)
        {
            _connection = new CD_CONEXION(configuration);
        }

        #region InsertarCatalogo
        public void InsertarCatalogo(CE_CATALOGOS obj)
        {
            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_CATALOGOS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "INS";
            cmd.Parameters.Add("@Id_Tipo_Catalogo", SqlDbType.Int).Value = obj.Id_Tipo_Catalogo;
            cmd.Parameters.Add("@Catalogo", SqlDbType.VarChar, 80).Value = obj.Catalogo;
            cmd.Parameters.Add("@Id_Creador", SqlDbType.Int).Value = obj.Id_Creador;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = obj.Activo;
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
        #endregion InsertarCatalogo

        #region ActualizarCatalogo
        public void ActualizarCatalogo(CE_CATALOGOS obj, out int resultado, out string mensaje)
        {
            

            cmd.Connection = _connection.AbrirConexion();
            cmd.CommandText = "SP_CATALOGOS";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "UPD";
            cmd.Parameters.Add("@Id_Catalogo", SqlDbType.Int).Value = obj.Id_Catalogo ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Id_Tipo_Catalogo", SqlDbType.Int).Value = obj.Id_Tipo_Catalogo ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Catalogo", SqlDbType.VarChar, 80).Value = obj.Catalogo;
            cmd.Parameters.Add("@Id_Modificador", SqlDbType.Int).Value = obj.Id_Modificador ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = obj.Activo ?? (object)DBNull.Value;

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
                throw new Exception($"Error al actualizar Catalogos: " + ex.Message, ex);
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Connection = _connection.CerrarConexion();
            }

        }
        #endregion ActualizarCatalogo

        #region ListarCatalogos
        public List<CE_CATALOGOS> ListarCatalogos() //Todo metodo de tipo Lista debe constar de informacion para mostrarla
        {
            List<CE_CATALOGOS> lts_catalogos = new List<CE_CATALOGOS>(); //Creando la informacion
            cmd.Connection = _connection.AbrirConexion(); //Abrimos la conexion
            cmd.CommandText = "SP_CATALOGOS"; //Parametro nombre del procedimiento almacenado
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
                        CE_CATALOGOS fila = new CE_CATALOGOS(); //Instanciamos un objeto de la clase CE_ESTADOS para tener un codigo mas limpio
                        fila.Id_Catalogo = dr["Id_Catalogo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Catalogo"]);
                        fila.Id_Tipo_Catalogo = dr["Id_Tipo_Catalogo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Tipo_Catalogo"]);
                        fila.Catalogo = dr["Catalogo"] is DBNull ? string.Empty : Convert.ToString(dr["Catalogo"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Activo = dr["Activo"] is DBNull ? 0 : Convert.ToInt32(dr["Activo"]);

                        lts_catalogos.Add(fila);
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

            return lts_catalogos;
        }

        #endregion ListarCatalogos

        #region FiltrarCatalogos
        public List<CE_CATALOGOS> FiltrarCatalogos(CE_CATALOGOS obj) //Todo metodo de tipo Lista debe constar de informacion para mostrarla
        {
            List<CE_CATALOGOS> lts_catalogos = new List<CE_CATALOGOS>(); //Creando la informacion
            cmd.Connection = _connection.AbrirConexion(); //Abrimos la conexion
            cmd.CommandText = "SP_CATALOGOS"; //Parametro nombre del procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure; //Le decimos el tipo de comando y le decimos que es un procedimiento almacenado
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "FIL"; //La accion es la que definimos en nuestro sp CRUD
            cmd.Parameters.Add("@Catalogo", SqlDbType.VarChar, 80).Value = obj.Catalogo;
            da.SelectCommand = cmd;
            try
            {
                da.Fill(tabla); //significa rellenar el datatable que nosotros llamamos al inicio tabla va a rellenar la tabla de nuestra consulta a nivel de memoria
                if (tabla.Rows.Count > 0) //Me cuenta las filas de una tabla y si es mayor a 0 crea un bucle
                {
                    foreach (DataRow dr in tabla.Rows) //DataRow permite recorrer y reconocer multiples datos ya que recibiremos varchars, ints, dates, bits
                    {
                        CE_CATALOGOS fila = new CE_CATALOGOS(); //Instanciamos un objeto de la clase CE_ESTADOS para tener un codigo mas limpio
                        fila.Id_Catalogo = dr["Id_Catalogo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Catalogo"]);
                        fila.Id_Tipo_Catalogo = dr["Id_Tipo_Catalogo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Tipo_Catalogo"]);
                        fila.Catalogo = dr["Catalogo"] is DBNull ? string.Empty : Convert.ToString(dr["Catalogo"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Activo = dr["Activo"] is DBNull ? 0 : Convert.ToInt32(dr["Activo"]);

                        lts_catalogos.Add(fila);
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

            return lts_catalogos;
        }
        #endregion FiltrarCatalogos

        #region FiltrarCatalogosID
        public List<CE_CATALOGOS> FiltrarCatalogosID(CE_CATALOGOS obj) //Todo metodo de tipo Lista debe constar de informacion para mostrarla
        {
            List<CE_CATALOGOS> lts_catalogos = new List<CE_CATALOGOS>(); //Creando la informacion
            cmd.Connection = _connection.AbrirConexion(); //Abrimos la conexion
            cmd.CommandText = "SP_CATALOGOS"; //Parametro nombre del procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure; //Le decimos el tipo de comando y le decimos que es un procedimiento almacenado
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 3).Value = "GET"; //La accion es la que definimos en nuestro sp CRUD
            cmd.Parameters.Add("@Id_Catalogo", SqlDbType.Int).Value = obj.Id_Catalogo;
            da.SelectCommand = cmd;
            try
            {
                da.Fill(tabla); //significa rellenar el datatable que nosotros llamamos al inicio tabla va a rellenar la tabla de nuestra consulta a nivel de memoria
                if (tabla.Rows.Count > 0) //Me cuenta las filas de una tabla y si es mayor a 0 crea un bucle
                {
                    foreach (DataRow dr in tabla.Rows) //DataRow permite recorrer y reconocer multiples datos ya que recibiremos varchars, ints, dates, bits
                    {
                        CE_CATALOGOS fila = new CE_CATALOGOS(); //Instanciamos un objeto de la clase CE_ESTADOS para tener un codigo mas limpio
                        fila.Id_Catalogo = dr["Id_Catalogo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Catalogo"]);
                        fila.Id_Tipo_Catalogo = dr["Id_Tipo_Catalogo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Tipo_Catalogo"]);
                        fila.Catalogo = dr["Catalogo"] is DBNull ? string.Empty : Convert.ToString(dr["Catalogo"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Activo = dr["Activo"] is DBNull ? 0 : Convert.ToInt32(dr["Activo"]);

                        lts_catalogos.Add(fila);
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

            return lts_catalogos;
        }
        #endregion FiltrarCatalogosID

    }
}
