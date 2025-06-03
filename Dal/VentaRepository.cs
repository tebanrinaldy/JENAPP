using Entity;
using Entity.Entity;
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
        public VentaRepository()
        {
        }

        private OracleConnection GetConnection()
        {
            return Conexion.ObtenerConexion();
        }

        public bool Agregar(Venta venta)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = @"INSERT INTO VENTAS (FECHA_VENTA, TOTAL, CEDULA_CLIENTE, NOMBRE_CLIENTE, TELEFONO_CLIENTE) 
                                                VALUES (:Fecha, :Total, :Cedula, :Nombre, :Telefono) 
                                                RETURNING ID_VENTA INTO :Id";
                            cmd.Parameters.Add(":Fecha", OracleDbType.Date).Value = venta.FechaVenta;
                            cmd.Parameters.Add(":Total", OracleDbType.Decimal).Value = venta.Total;
                            cmd.Parameters.Add(":Cedula", OracleDbType.Varchar2).Value = venta.CedulaCliente;
                            cmd.Parameters.Add(":Nombre", OracleDbType.Varchar2).Value = venta.NombreCliente;
                            cmd.Parameters.Add(":Telefono", OracleDbType.Varchar2).Value = venta.TelefonoCliente;
                            cmd.Parameters.Add(":Id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            var idOracleDecimal = (Oracle.ManagedDataAccess.Types.OracleDecimal)cmd.Parameters[":Id"].Value;
                            venta.Id = idOracleDecimal.ToInt32();
                        }

                        foreach (var detalle in venta.DetalleVentas)
                        {
                            using (var cmd = connection.CreateCommand())
                            {
                                cmd.Transaction = transaction;
                                cmd.CommandText = @"INSERT INTO DETALLE_VENTAS (ID_VENTA, ID_PRODUCTO, CANTIDAD, PRECIOUNITARIO) 
                                                    VALUES (:IdVenta, :IdProducto, :Cantidad, :Precio)";
                                cmd.Parameters.Add(":IdVenta", OracleDbType.Int32).Value = venta.Id;
                                cmd.Parameters.Add(":IdProducto", OracleDbType.Int32).Value = detalle.ProductoId;
                                cmd.Parameters.Add(":Cantidad", OracleDbType.Int32).Value = detalle.Cantidad;
                                cmd.Parameters.Add(":Precio", OracleDbType.Decimal).Value = detalle.PrecioUnitario;
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al agregar la venta: {e.Message}");
                return false;
            }
        }

        public Venta ObtenerPorId(int id)
        {
            Venta venta = null;

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT ID_VENTA, FECHA_VENTA, TOTAL, CEDULA_CLIENTE, NOMBRE_CLIENTE, TELEFONO_CLIENTE 
                                        FROM VENTAS 
                                        WHERE ID_VENTA = :Id";

                    cmd.Parameters.Add(":Id", OracleDbType.Int32).Value = id;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            venta = new Venta
                            {
                                Id = reader.GetInt32(0),
                                FechaVenta = reader.GetDateTime(1),
                                Total = reader.GetDecimal(2),
                                CedulaCliente = reader.GetString(3),
                                NombreCliente = reader.GetString(4),
                                TelefonoCliente = reader.GetString(5),
                                DetalleVentas = new List<DetalleVenta>()
                            };
                        }
                    }
                }

                if (venta != null)
                {
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT D.ID_PRODUCTO, P.NOMBRE, D.CANTIDAD, D.PRECIOUNITARIO
                                            FROM DETALLE_VENTAS D
                                            JOIN PRODUCTOS P ON D.ID_PRODUCTO = P.ID_PRODUCTO
                                            WHERE D.ID_VENTA = :IdVenta";

                        cmd.Parameters.Add(":IdVenta", OracleDbType.Int32).Value = id;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                venta.DetalleVentas.Add(new DetalleVenta
                                {
                                    ProductoId = reader.GetInt32(0),
                                    NombreProducto = reader.GetString(1),
                                    Cantidad = reader.GetInt32(2),
                                    PrecioUnitario = reader.GetDecimal(3)
                                });
                            }
                        }
                    }
                }
            }

            return venta;
        }

        public List<Venta> ObtenerTodos()
        {
            var ventas = new List<Venta>();

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT ID_VENTA, FECHA_VENTA, TOTAL, 
                                           CEDULA_CLIENTE, NOMBRE_CLIENTE, TELEFONO_CLIENTE 
                                    FROM VENTAS";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ventas.Add(new Venta
                                {
                                    Id = Convert.ToInt32(reader.GetDecimal(0)),
                                    FechaVenta = reader.GetDateTime(1),
                                    Total = reader.GetDecimal(2),
                                    CedulaCliente = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                    NombreCliente = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                    TelefonoCliente = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                    Detalles = new List<DetalleVenta>()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ventas", ex);
            }

            return ventas;
        }

        public bool Actualizar(Venta venta)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = @"UPDATE VENTAS 
                                                SET FECHA_VENTA = :Fecha, TOTAL = :Total, CEDULA_CLIENTE = :Cedula, 
                                                    NOMBRE_CLIENTE = :Nombre, TELEFONO_CLIENTE = :Telefono 
                                                WHERE ID_VENTA = :Id";
                            cmd.Parameters.Add(":Fecha", OracleDbType.Date).Value = venta.FechaVenta;
                            cmd.Parameters.Add(":Total", OracleDbType.Decimal).Value = venta.Total;
                            cmd.Parameters.Add(":Cedula", OracleDbType.Varchar2).Value = venta.CedulaCliente;
                            cmd.Parameters.Add(":Nombre", OracleDbType.Varchar2).Value = venta.NombreCliente;
                            cmd.Parameters.Add(":Telefono", OracleDbType.Varchar2).Value = venta.TelefonoCliente;
                            cmd.Parameters.Add(":Id", OracleDbType.Int32).Value = venta.Id;
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = @"DELETE FROM DETALLE_VENTAS WHERE ID_VENTA = :IdVenta";
                            cmd.Parameters.Add(":IdVenta", OracleDbType.Int32).Value = venta.Id;
                            cmd.ExecuteNonQuery();
                        }

                        foreach (var detalle in venta.DetalleVentas)
                        {
                            using (var cmd = connection.CreateCommand())
                            {
                                cmd.Transaction = transaction;
                                cmd.CommandText = @"INSERT INTO DETALLE_VENTAS (ID_VENTA, ID_PRODUCTO, CANTIDAD, PRECIOUNITARIO) 
                                                    VALUES (:IdVenta, :IdProducto, :Cantidad, :Precio)";
                                cmd.Parameters.Add(":IdVenta", OracleDbType.Int32).Value = venta.Id;
                                cmd.Parameters.Add(":IdProducto", OracleDbType.Int32).Value = detalle.ProductoId;
                                cmd.Parameters.Add(":Cantidad", OracleDbType.Int32).Value = detalle.Cantidad;
                                cmd.Parameters.Add(":Precio", OracleDbType.Decimal).Value = detalle.PrecioUnitario;
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(int id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = @"DELETE FROM DETALLE_VENTAS WHERE ID_VENTA = :IdVenta";
                            cmd.Parameters.Add(":IdVenta", OracleDbType.Int32).Value = id;
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = @"DELETE FROM VENTAS WHERE ID_VENTA = :Id";
                            cmd.Parameters.Add(":Id", OracleDbType.Int32).Value = id;
                            int filasAfectadas = cmd.ExecuteNonQuery();

                            transaction.Commit();
                            return filasAfectadas > 0;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public List<Venta> ObtenerPorRangoFechas(DateTime desde, DateTime hasta)
        {
            List<Venta> lista = new List<Venta>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"SELECT ID_VENTA, FECHA_VENTA, TOTAL, CEDULA_CLIENTE, NOMBRE_CLIENTE, TELEFONO_CLIENTE 
                                 FROM VENTAS 
                                 WHERE FECHA_VENTA BETWEEN :desde AND :hasta 
                                 ORDER BY FECHA_VENTA";

                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(":desde", OracleDbType.Date).Value = desde;
                    cmd.Parameters.Add(":hasta", OracleDbType.Date).Value = hasta;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Venta
                            {
                                Id = Convert.ToInt32(reader["ID_VENTA"]),
                                FechaVenta = Convert.ToDateTime(reader["FECHA_VENTA"]),
                                Total = Convert.ToDecimal(reader["TOTAL"]),
                                CedulaCliente = reader["CEDULA_CLIENTE"].ToString(),
                                NombreCliente = reader["NOMBRE_CLIENTE"].ToString(),
                                TelefonoCliente = reader["TELEFONO_CLIENTE"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}