﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public static class Conexion
    {
        private static readonly string connectionString = "User Id=jenapp;Password=jen123;Data Source=localhost:1521/XEPDB1";

        public static OracleConnection ObtenerConexion()
        {
            return new OracleConnection(connectionString);
        }
    }
}