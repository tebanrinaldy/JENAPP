using Entity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace Dal
{
    public class MovimientoInventarioDAO
    {
        private string connectionString = "User Id=jenapp;Password=jen123;Data Source=localhost:1521/XEPDB1";

        public void RegistrarMovimiento(MovimientoInventario mov)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();
                OracleTransaction trans = con.BeginTransaction();

                try
                {
                    // Validar stock si es salida
                    if (mov.Tipo == "Salida")
                    {
                        int stockActual = ObtenerStockActual(mov.IdProducto, con, trans);
                        if (mov.Cantidad > stockActual)
                            throw new Exception("La cantidad a retirar excede el stock disponible.");
                    }

                    // Insertar el movimiento
                    string insertSql = @"INSERT INTO movimiento_inventario 
                                         (id_producto, tipo, cantidad, fecha) 
                                         VALUES (:id_producto, :tipo, :cantidad, CURRENT_TIMESTAMP)";

                    using (OracleCommand cmdInsert = new OracleCommand(insertSql, con))
                    {
                        cmdInsert.Transaction = trans;
                        cmdInsert.Parameters.Add(":id_producto", OracleDbType.Int32).Value = mov.IdProducto;
                        cmdInsert.Parameters.Add(":tipo", OracleDbType.Varchar2).Value = mov.Tipo;
                        cmdInsert.Parameters.Add(":cantidad", OracleDbType.Int32).Value = mov.Cantidad;
                        cmdInsert.ExecuteNonQuery();
                    }

                    // Actualizar el stock
                    string operador = mov.Tipo == "Entrada" ? "+" : "-";
                    string updateSql = $@"UPDATE productos 
                                          SET stock = stock {operador} :cantidad 
                                          WHERE id_producto = :id_producto";

                    using (OracleCommand cmdUpdate = new OracleCommand(updateSql, con))
                    {
                        cmdUpdate.Transaction = trans;
                        cmdUpdate.Parameters.Add(":cantidad", OracleDbType.Int32).Value = mov.Cantidad;
                        cmdUpdate.Parameters.Add(":id_producto", OracleDbType.Int32).Value = mov.IdProducto;
                        cmdUpdate.ExecuteNonQuery();
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("Error al registrar movimiento: " + ex.Message);
                }
            }
        }

        // ✅ Método auxiliar para obtener el stock actual de un producto
        public int ObtenerStockActual(int idProducto)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();
                return ObtenerStockActual(idProducto, con, null);
            }
        }

        // Interno para uso dentro de una transacción
        private int ObtenerStockActual(int idProducto, OracleConnection con, OracleTransaction trans)
        {
            string sql = "SELECT stock FROM productos WHERE id_producto = :id_producto";

            using (OracleCommand cmd = new OracleCommand(sql, con))
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