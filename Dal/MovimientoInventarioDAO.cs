using Entity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class MovimientoInventarioDAO
    {
        // Ya no se recibe cadena en constructor, se usa Conexion directamente

        public MovimientoInventarioDAO()
        {
        }

        public bool RegistrarMovimiento(MovimientoInventario mov)
        {
            try
            {
                using (var connection = Conexion.ObtenerConexion())
                {
                    connection.Open();
                    using (var trans = connection.BeginTransaction())
                    {
                        // Validar stock si es salida
                        if (mov.Tipo == "Salida")
                        {
                            int stockActual = ObtenerStockActual(mov.IdProducto, connection, trans);
                            if (mov.Cantidad > stockActual)
                                throw new Exception("La cantidad a retirar excede el stock disponible.");
                        }

                        // Insertar movimiento
                        string insertSql = @"INSERT INTO movimiento_inventario 
                                             (id_producto, tipo, cantidad, fecha) 
                                             VALUES (:id_producto, :tipo, :cantidad, CURRENT_TIMESTAMP)";

                        using (var cmdInsert = new OracleCommand(insertSql, connection))
                        {
                            cmdInsert.Transaction = trans;
                            cmdInsert.Parameters.Add(":id_producto", OracleDbType.Int32).Value = mov.IdProducto;
                            cmdInsert.Parameters.Add(":tipo", OracleDbType.Varchar2).Value = mov.Tipo;
                            cmdInsert.Parameters.Add(":cantidad", OracleDbType.Int32).Value = mov.Cantidad;
                            cmdInsert.ExecuteNonQuery();
                        }

                        // Actualizar stock
                        string operador = mov.Tipo == "Entrada" ? "+" : "-";
                        string updateSql = $@"UPDATE productos 
                                              SET stock = stock {operador} :cantidad 
                                              WHERE id_producto = :id_producto";

                        using (var cmdUpdate = new OracleCommand(updateSql, connection))
                        {
                            cmdUpdate.Transaction = trans;
                            cmdUpdate.Parameters.Add(":cantidad", OracleDbType.Int32).Value = mov.Cantidad;
                            cmdUpdate.Parameters.Add(":id_producto", OracleDbType.Int32).Value = mov.IdProducto;
                            cmdUpdate.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Aquí podrías loguear el error si quieres
                return false;
            }
        }

        public int ObtenerStockActual(int idProducto)
        {
            try
            {
                using (var connection = Conexion.ObtenerConexion())
                {
                    connection.Open();
                    return ObtenerStockActual(idProducto, connection, null);
                }
            }
            catch
            {
                // Manejo simple, retorna 0 si hay error
                return 0;
            }
        }

        private int ObtenerStockActual(int idProducto, OracleConnection connection, OracleTransaction trans)
        {
            string sql = "SELECT stock FROM productos WHERE id_producto = :id_producto";

            using (var cmd = new OracleCommand(sql, connection))
            {
                cmd.Transaction = trans;
                cmd.Parameters.Add(":id_producto", OracleDbType.Int32).Value = idProducto;

                object result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    throw new Exception("Producto no encontrado.");

                return Convert.ToInt32(result);
            }
        }
    }
}