using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CAPA_DATOS
{
    public class CD_CONEXION
    {
        private readonly string _conexionString;

        private SqlConnection _connection;

        public CD_CONEXION(IConfiguration configuration)
        {
            _conexionString = configuration.GetConnectionString("CnxSql");
            _connection = new SqlConnection(_conexionString);
        }

        public SqlConnection AbrirConexion()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            return _connection;
        }

        public SqlConnection CerrarConexion()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
            return _connection;
        }

    }
}
