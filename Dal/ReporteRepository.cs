using Entity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class ReporteRepository : IRepository<Reportes>
    {
        private readonly string _conexion;

        public ReporteRepository(string conexion)
        {
            _conexion = conexion;
        }

        public bool Agregar(Reportes entidad)
        {
            using (var conexion = new OracleConnection(_conexion))
            {
                conexion.Open();
                string sql = @"INSERT INTO reportes (
                                id_reporte,
                                fecha_venta,
                                total,
                                cedula_cliente,
                                nombre_cliente,
                                telefono_cliente
                            ) VALUES (
                                :id_reporte,
                                :fecha_venta,
                                :total,
                                :cedula_cliente,
                                :nombre_cliente,
                                :telefono_cliente
                            )";

                using (var comando = new OracleCommand(sql, conexion))
                {
                    comando.Parameters.Add(":id_reporte", entidad.Id);
                    comando.Parameters.Add(":fecha_venta", entidad.FechaVenta);
                    comando.Parameters.Add(":total", entidad.Total);
                    comando.Parameters.Add(":cedula_cliente", entidad.CedulaCliente);
                    comando.Parameters.Add(":nombre_cliente", entidad.NombreCliente);
                    comando.Parameters.Add(":telefono_cliente", entidad.TelefonoCliente);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        public bool Actualizar(Reportes entidad)
        {
            using (var conexion = new OracleConnection(_conexion))
            {
                conexion.Open();
                string sql = @"UPDATE reportes SET
                                fecha_venta = :fecha_venta,
                                total = :total,
                                cedula_cliente = :cedula_cliente,
                                nombre_cliente = :nombre_cliente,
                                telefono_cliente = :telefono_cliente
                            WHERE id_reporte = :id_reporte";

                using (var comando = new OracleCommand(sql, conexion))
                {
                    comando.Parameters.Add(":fecha_venta", entidad.FechaVenta);
                    comando.Parameters.Add(":total", entidad.Total);
                    comando.Parameters.Add(":cedula_cliente", entidad.CedulaCliente);
                    comando.Parameters.Add(":nombre_cliente", entidad.NombreCliente);
                    comando.Parameters.Add(":telefono_cliente", entidad.TelefonoCliente);
                    comando.Parameters.Add(":id_reporte", entidad.Id);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        public bool Eliminar(int id)
        {
            using (var conexion = new OracleConnection(_conexion))
            {
                conexion.Open();
                string sql = "DELETE FROM reportes WHERE id_reporte = :id_reporte";

                using (var comando = new OracleCommand(sql, conexion))
                {
                    comando.Parameters.Add(":id_reporte", id);
                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        public Reportes ObtenerPorId(int id)
        {
            using (var conexion = new OracleConnection(_conexion))
            {
                conexion.Open();
                string sql = "SELECT * FROM reportes WHERE id_reporte = :id_reporte";

                using (var comando = new OracleCommand(sql, conexion))
                {
                    comando.Parameters.Add(":id_reporte", id);
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            return new Reportes
                            {
                                Id = Convert.ToInt32(lector["id_reporte"]),
                                FechaVenta = Convert.ToDateTime(lector["fecha_venta"]),
                                Total = Convert.ToDecimal(lector["total"]),
                                CedulaCliente = lector["cedula_cliente"].ToString(),
                                NombreCliente = lector["nombre_cliente"].ToString(),
                                TelefonoCliente = lector["telefono_cliente"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<Reportes> ObtenerTodos()
        {
            var lista = new List<Reportes>();

            using (var conexion = new OracleConnection(_conexion))
            {
                conexion.Open();
                string sql = "SELECT * FROM reportes";

                using (var comando = new OracleCommand(sql, conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            lista.Add(new Reportes
                            {
                                Id = Convert.ToInt32(lector["id_reporte"]),
                                FechaVenta = Convert.ToDateTime(lector["fecha_venta"]),
                                Total = Convert.ToDecimal(lector["total"]),
                                CedulaCliente = lector["cedula_cliente"].ToString(),
                                NombreCliente = lector["nombre_cliente"].ToString(),
                                TelefonoCliente = lector["telefono_cliente"].ToString()
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}