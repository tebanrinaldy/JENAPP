using Entity;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class ClienteRepository : IRepository<Cliente>
    {
        private readonly string _connectionString;

        public ClienteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Agregar(Cliente entidad)
        {
            const string sql = @"
                INSERT INTO clientes (nombre, telefono, direccion)
                VALUES (:nombre, :telefono, :direccion)
                RETURNING id_cliente INTO :id_out";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;

                cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = entidad.Nombre;
                cmd.Parameters.Add("telefono", OracleDbType.Varchar2).Value = entidad.Telefono;
                cmd.Parameters.Add("direccion", OracleDbType.Varchar2).Value = entidad.Direccion;

                var pIdOut = cmd.Parameters.Add("id_out", OracleDbType.Decimal);
                pIdOut.Direction = ParameterDirection.Output;

                conn.Open();
                var filas = cmd.ExecuteNonQuery();

                entidad.Id = Convert.ToInt32(((OracleDecimal)pIdOut.Value).Value);

                return filas > 0;
            }
        }

        public Cliente ObtenerPorId(int id)
        {
            const string sql = @"SELECT * FROM clientes WHERE id_cliente = :id";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Cliente
                        {
                            Id = Convert.ToInt32(reader["id_cliente"]),
                            Nombre = reader["nombre"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            Direccion = reader["direccion"].ToString()
                        };
                    }
                }
            }

            return null;
        }

        public List<Cliente> ObtenerTodos()
        {
            var lista = new List<Cliente>();
            const string sql = @"SELECT * FROM clientes ORDER BY id_cliente";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Cliente
                        {
                            Id = Convert.ToInt32(reader["id_cliente"]),
                            Nombre = reader["nombre"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            Direccion = reader["direccion"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        public bool Actualizar(Cliente entidad)
        {
            const string sql = @"
                UPDATE clientes
                SET nombre = :nombre,
                    telefono = :telefono,
                    direccion = :direccion
                WHERE id_cliente = :id";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = entidad.Nombre;
                cmd.Parameters.Add("telefono", OracleDbType.Varchar2).Value = entidad.Telefono;
                cmd.Parameters.Add("direccion", OracleDbType.Varchar2).Value = entidad.Direccion;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = entidad.Id;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            const string sql = @"DELETE FROM clientes WHERE id_cliente = :id";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}