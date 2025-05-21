using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public static class Conexion
    {
        private static readonly string connectionString = "User Id=jenapp;Password=jen123;Data Source=192.168.221.244:1521/XEPDB1";
        public static string ConnectionString => connectionString;

        public static OracleConnection ObtenerConexion()
        {
            return new OracleConnection(connectionString);
        }
    }
}