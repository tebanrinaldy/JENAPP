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
        private readonly string _connectionString;

        public ReporteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Actualizar(Reportes reporte)
        {
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = @"
                            UPDATE DETALLEVENTA
                            SET NOMBREPRODUCTO = :NombreProducto,
                                CANTIDAD = :Cantidad,
                                PRECIOUNITARIO = :PrecioUnitario,
                                SUBTOTAL = :Subtotal
                            WHERE IDDETALLE = :IdDetalle";

                        cmd.Parameters.Add(":NombreProducto", OracleDbType.Varchar2).Value = reporte.NombreProducto;
                        cmd.Parameters.Add(":Cantidad", OracleDbType.Int32).Value = reporte.Cantidad;
                        cmd.Parameters.Add(":PrecioUnitario", OracleDbType.Decimal).Value = reporte.PrecioUnitario;
                        cmd.Parameters.Add(":Subtotal", OracleDbType.Decimal).Value = reporte.Subtotal;
                        cmd.Parameters.Add(":IdDetalle", OracleDbType.Int32).Value = reporte.IdDetalle;

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(int idDetalle)
        {
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM DETALLEVENTA WHERE IDDETALLE = :Id";
                        cmd.Parameters.Add(":Id", OracleDbType.Int32).Value = idDetalle;

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public Reportes ObtenerPorId(int idDetalle)
        {
            Reportes reporte = null;

            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = @"
                            SELECT v.FECHAVENTA, v.TOTAL, v.CEDULACLIENTE, v.NOMBRECLIENTE, v.TELEFONOCLIENTE,
                                   d.IDDETALLE, d.NOMBREPRODUCTO, d.CANTIDAD, d.PRECIOUNITARIO, d.SUBTOTAL
                            FROM VENTAS v
                            INNER JOIN DETALLEVENTA d ON v.IDVENTA = d.IDVENTA
                            WHERE d.IDDETALLE = :Id";

                        cmd.Parameters.Add(":Id", OracleDbType.Int32).Value = idDetalle;

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                reporte = new Reportes
                                {
                                    FechaVenta = reader.GetDateTime(0),
                                    Total = reader.GetDecimal(1),
                                    CedulaCliente = reader.GetString(2),
                                    NombreCliente = reader.GetString(3),
                                    TelefonoCliente = reader.GetString(4),
                                    IdDetalle = reader.GetInt32(5),
                                    NombreProducto = reader.GetString(6),
                                    Cantidad = reader.GetInt32(7),
                                    PrecioUnitario = reader.GetDecimal(8),
                                    Subtotal = reader.GetDecimal(9)
                                };
                            }
                        }
                    }
                }
            }
            catch
            {
                // manejo de error
            }

            return reporte;
        }

        public List<Reportes> ObtenerTodos()
        {
            var lista = new List<Reportes>();

            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = @"
                            SELECT v.FECHAVENTA, v.TOTAL, v.CEDULACLIENTE, v.NOMBRECLIENTE, v.TELEFONOCLIENTE,
                                   d.IDDETALLE, d.NOMBREPRODUCTO, d.CANTIDAD, d.PRECIOUNITARIO, d.SUBTOTAL
                            FROM VENTAS v
                            INNER JOIN DETALLEVENTA d ON v.IDVENTA = d.IDVENTA";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Reportes
                                {
                                    FechaVenta = reader.GetDateTime(0),
                                    Total = reader.GetDecimal(1),
                                    CedulaCliente = reader.GetString(2),
                                    NombreCliente = reader.GetString(3),
                                    TelefonoCliente = reader.GetString(4),
                                    IdDetalle = reader.GetInt32(5),
                                    NombreProducto = reader.GetString(6),
                                    Cantidad = reader.GetInt32(7),
                                    PrecioUnitario = reader.GetDecimal(8),
                                    Subtotal = reader.GetDecimal(9)
                                });
                            }
                        }
                    }
                }
            }
            catch
            {
                // manejo de error
            }

            return lista;
        }

        // No implementamos Agregar porque los reportes provienen de las ventas ya registradas.
        public bool Agregar(Reportes entidad)
        {
            throw new NotImplementedException();
        }
    }
}