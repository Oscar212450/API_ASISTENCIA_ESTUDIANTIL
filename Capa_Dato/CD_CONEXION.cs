using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;


namespace Capa_Dato
{
    public class CD_CONEXION
    {
        private readonly string _conexionString;
        private SqlConnection _connection;

        public CD_CONEXION(IConfiguration configuration)
        {
            _conexionString = configuration.GetConnectionString("Cnxsql")!;
            _connection = new SqlConnection(_conexionString);
        }

        public SqlConnection AbrirConexion()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            return _connection;
        }

        public SqlConnection CerrarConexion()
        {
            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
            return _connection;
        }
    }
}
