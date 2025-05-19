using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    /// <summary>
    /// Encapsula la obtención de la conexión a Oracle XE 18c.
    /// </summary>
    public static class ConexionOracle
    {
        // ⚠️ Reemplaza estos valores por los de tu entorno.
        // Ejemplo típico para XE 18c con PDB XEPDB1 en localhost.
        private const string Cadena =
            "User Id=jenapp;Password=jen123;Data Source=localhost/XEPDB1;";

        /// <summary>
        /// Devuelve una conexión abierta. Lanza excepción si falla.
        /// </summary>
        public static OracleConnection Obtener()
        {
            var cn = new OracleConnection(Cadena);
            cn.Open();            // comprobar al instante
            return cn;            // la capa que la use deberá cerrarla/disponerla
        }
    }
}