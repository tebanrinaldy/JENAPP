using Entity.Entity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class DetalleVentaRepository : IRepository<DetalleVenta>
    {
        private readonly string _connectionString;

        public DetalleVentaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Agregar(DetalleVenta detalle)
        {
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = @"
                            INSERT INTO DETALLEVENTA (IDVENTA, IDPRODUCTO, CANTIDAD, PRECIOUNITARIO) 
                            VALUES (:IdVenta, :IdProducto, :Cantidad, :Precio)";
                        cmd.Parameters.Add(":IdVenta", OracleDbType.Int32).Value = detalle.VentaId;
                        cmd.Parameters.Add(":IdProducto", OracleDbType.Int32).Value = detalle.ProductoId;
                        cmd.Parameters.Add(":Cantidad", OracleDbType.Int32).Value = detalle.Cantidad;
                        cmd.Parameters.Add(":Precio", OracleDbType.Decimal).Value = detalle.PrecioUnitario;

                        int filas = cmd.ExecuteNonQuery();
                        return filas > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DetalleVenta ObtenerPorId(int id)
        {
            DetalleVenta detalle = null;
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();

                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = @"
                            SELECT IDVENTA, IDPRODUCTO, CANTIDAD, PRECIOUNITARIO 
                            FROM DETALLEVENTA WHERE ID = :Id";
                        cmd.Parameters.Add(":Id", OracleDbType.Int32).Value = id;

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                detalle = new DetalleVenta
                                {
                                    VentaId = reader.GetInt32(0),
                                    ProductoId = reader.GetInt32(1),
                                    Cantidad = reader.GetInt32(2),
                                    PrecioUnitario = reader.GetDecimal(3)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            return detalle;
        }

        public List<DetalleVenta> ObtenerTodos()
        {
            var lista = new List<DetalleVenta>();
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();

                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT IDVENTA, IDPRODUCTO, CANTIDAD, PRECIOUNITARIO FROM DETALLEVENTA";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new DetalleVenta
                                {
                                    VentaId = reader.GetInt32(0),
                                    ProductoId = reader.GetInt32(1),
                                    Cantidad = reader.GetInt32(2),
                                    PrecioUnitario = reader.GetDecimal(3)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                
            }

            return lista;
        }

        public bool Actualizar(DetalleVenta detalle)
        {
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();

                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = @"
                            UPDATE DETALLEVENTA SET 
                                IDVENTA = :IdVenta, 
                                IDPRODUCTO = :IdProducto, 
                                CANTIDAD = :Cantidad, 
                                PRECIOUNITARIO = :Precio 
                            WHERE IDVENTA = :IdVenta AND IDPRODUCTO = :IdProducto";

                        cmd.Parameters.Add(":IdVenta", OracleDbType.Int32).Value = detalle.VentaId;
                        cmd.Parameters.Add(":IdProducto", OracleDbType.Int32).Value = detalle.ProductoId;
                        cmd.Parameters.Add(":Cantidad", OracleDbType.Int32).Value = detalle.Cantidad;
                        cmd.Parameters.Add(":Precio", OracleDbType.Decimal).Value = detalle.PrecioUnitario;

                        int filas = cmd.ExecuteNonQuery();
                        return filas > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Eliminar(int id)
        {
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();

                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM DETALLEVENTA WHERE ID = :Id";
                        cmd.Parameters.Add(":Id", OracleDbType.Int32).Value = id;

                        int filas = cmd.ExecuteNonQuery();
                        return filas > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}