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
        private readonly string _connectionString;

        public VentaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Agregar(Venta venta)
        {
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "INSERT INTO VENTA (FECHAVENTA, TOTAL, CEDULACLIENTE, NOMBRECLIENTE, TELEFONOCLIENTE) " +
                                              "VALUES (:Fecha, :Total, :Cedula, :Nombre, :Telefono) RETURNING ID INTO :Id";
                            cmd.Parameters.Add(":Fecha", OracleDbType.Date).Value = venta.FechaVenta;
                            cmd.Parameters.Add(":Total", OracleDbType.Decimal).Value = venta.Total;
                            cmd.Parameters.Add(":Cedula", OracleDbType.Varchar2).Value = venta.CedulaCliente;
                            cmd.Parameters.Add(":Nombre", OracleDbType.Varchar2).Value = venta.NombreCliente;
                            cmd.Parameters.Add(":Telefono", OracleDbType.Varchar2).Value = venta.TelefonoCliente;
                            cmd.Parameters.Add(":Id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            venta.Id = Convert.ToInt32(cmd.Parameters[":Id"].Value);
                        }

                        foreach (var detalle in venta.Detalles)
                        {
                            using (var cmd = connection.CreateCommand())
                            {
                                cmd.Transaction = transaction;
                                cmd.CommandText = "INSERT INTO DETALLEVENTA (IDVENTA, IDPRODUCTO, CANTIDAD, PRECIOUNITARIO) " +
                                                  "VALUES (:IdVenta, :IdProducto, :Cantidad, :Precio)";
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
            catch (Exception)
            {

                return false;
            }
        }

        public Venta ObtenerPorId(int id)
        {
            Venta venta = null;

            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();


                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT ID, FECHAVENTA, TOTAL, CEDULACLIENTE, NOMBRECLIENTE, TELEFONOCLIENTE FROM VENTA WHERE ID = :Id";
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
                                    Detalles = new List<DetalleVenta>()
                                };
                            }
                            else
                            {
                                return null; 
                            }
                        }
                    }

                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT IDPRODUCTO, CANTIDAD, PRECIOUNITARIO FROM DETALLEVENTA WHERE IDVENTA = :IdVenta";
                        cmd.Parameters.Add(":IdVenta", OracleDbType.Int32).Value = id;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                venta.Detalles.Add(new DetalleVenta
                                {
                                    ProductoId = reader.GetInt32(0),
                                    Cantidad = reader.GetInt32(1),
                                    PrecioUnitario = reader.GetDecimal(2)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }

            return venta;
        }

        public List<Venta> ObtenerTodos()
        {
            var ventas = new List<Venta>();

            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();

                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT ID, FECHAVENTA, TOTAL, CEDULACLIENTE, NOMBRECLIENTE, TELEFONOCLIENTE FROM VENTA";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ventas.Add(new Venta
                                {
                                    Id = reader.GetInt32(0),
                                    FechaVenta = reader.GetDateTime(1),
                                    Total = reader.GetDecimal(2),
                                    CedulaCliente = reader.GetString(3),
                                    NombreCliente = reader.GetString(4),
                                    TelefonoCliente = reader.GetString(5),
                                    Detalles = new List<DetalleVenta>() 
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            return ventas;
        }

        public bool Actualizar(Venta venta)
        {
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        // Actualizar tabla VENTA
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "UPDATE VENTA SET FECHAVENTA = :Fecha, TOTAL = :Total, CEDULACLIENTE = :Cedula, " +
                                              "NOMBRECLIENTE = :Nombre, TELEFONOCLIENTE = :Telefono WHERE ID = :Id";
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
                            cmd.CommandText = "DELETE FROM DETALLEVENTA WHERE IDVENTA = :IdVenta";
                            cmd.Parameters.Add(":IdVenta", OracleDbType.Int32).Value = venta.Id;
                            cmd.ExecuteNonQuery();
                        }

                        foreach (var detalle in venta.Detalles)
                        {
                            using (var cmd = connection.CreateCommand())
                            {
                                cmd.Transaction = transaction;
                                cmd.CommandText = "INSERT INTO DETALLEVENTA (IDVENTA, IDPRODUCTO, CANTIDAD, PRECIOUNITARIO) " +
                                                  "VALUES (:IdVenta, :IdProducto, :Cantidad, :Precio)";
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
                    using (var transaction = connection.BeginTransaction())
                    {

                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "DELETE FROM DETALLEVENTA WHERE IDVENTA = :IdVenta";
                            cmd.Parameters.Add(":IdVenta", OracleDbType.Int32).Value = id;
                            cmd.ExecuteNonQuery();
                        }


                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "DELETE FROM VENTA WHERE ID = :Id";
                            cmd.Parameters.Add(":Id", OracleDbType.Int32).Value = id;
                            int filasAfectadas = cmd.ExecuteNonQuery();

                            transaction.Commit();
                            return filasAfectadas > 0;
                        }
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