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
    public class VentaRepository : IRepository<Venta>
    {
        private readonly string _connectionString;

        public VentaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Agregar(Venta entidad)
        {
            const string sql = @"
                INSERT INTO ventas (fecha, total, id_cliente)
                VALUES (SYSDATE, :total, :id_cliente)
                RETURNING id_venta INTO :id_out";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("total", OracleDbType.Decimal).Value = entidad.Total;
                cmd.Parameters.Add("id_cliente", OracleDbType.Int32).Value = entidad.IdCliente;

                var pIdOut = cmd.Parameters.Add("id_out", OracleDbType.Decimal);
                pIdOut.Direction = ParameterDirection.Output;

                conn.Open();
                var filas = cmd.ExecuteNonQuery();
                entidad.Id = Convert.ToInt32(((OracleDecimal)pIdOut.Value).Value);
                entidad.FechaRegistro = DateTime.Now;

                return filas > 0;
            }
        }

        public Venta ObtenerPorId(int id)
        {
            const string sql = @"
                SELECT v.id_venta, v.fecha, v.total,
                       v.id_cliente, c.nombre AS nombre_cliente
                FROM ventas v
                JOIN clientes c ON c.id_cliente = v.id_cliente
                WHERE v.id_venta = :id";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return Map(reader);
                }
            }

            return null;
        }

        public List<Venta> ObtenerTodos()
        {
            const string sql = @"
                SELECT v.id_venta, v.fecha, v.total,
                       v.id_cliente, c.nombre AS nombre_cliente
                FROM ventas v
                JOIN clientes c ON c.id_cliente = v.id_cliente
                ORDER BY v.id_venta DESC";

            var lista = new List<Venta>();

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        lista.Add(Map(reader));
                }
            }

            return lista;
        }

        public bool Actualizar(Venta entidad)
        {
            const string sql = @"
                UPDATE ventas
                SET total = :total,
                    id_cliente = :id_cliente
                WHERE id_venta = :id_venta";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;

                cmd.Parameters.Add("total", OracleDbType.Decimal).Value = entidad.Total;
                cmd.Parameters.Add("id_cliente", OracleDbType.Int32).Value = entidad.IdCliente;
                cmd.Parameters.Add("id_venta", OracleDbType.Int32).Value = entidad.Id;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            const string sql = @"DELETE FROM ventas WHERE id_venta = :id";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private Venta Map(OracleDataReader reader)
        {
            return new Venta
            {
                Id = reader.GetInt32(reader.GetOrdinal("id_venta")),
                FechaRegistro = reader.GetDateTime(reader.GetOrdinal("fecha")),
                Total = reader.GetDecimal(reader.GetOrdinal("total")),
                IdCliente = reader.GetInt32(reader.GetOrdinal("id_cliente")),
                Cliente = new Cliente
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id_cliente")),
                    Nombre = reader.GetString(reader.GetOrdinal("nombre_cliente"))
                }
            };
        }
    }
}