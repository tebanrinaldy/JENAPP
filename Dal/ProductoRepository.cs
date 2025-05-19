using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Oracle.ManagedDataAccess.Client;

namespace Dal

{
    public class ProductoRepository : IRepository<Producto>
    {
        private readonly string _connectionString;

        public ProductoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /* ---------- CRUD ---------- */

        public bool Agregar(Producto entidad)
        {
            const string sql = @"
                INSERT INTO productos
                    (id_producto, nombre, descripcion, precio, stock, id_categoria)
                VALUES
                    (seq_productos.NEXTVAL, :nombre, :descripcion, :precio, :stock, :id_categoria)";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add("nombre", entidad.Nombre);
                cmd.Parameters.Add("descripcion", entidad.Descripcion);
                cmd.Parameters.Add("precio", entidad.Precio);
                cmd.Parameters.Add("stock", entidad.Stock);
                cmd.Parameters.Add("id_categoria", entidad.IdCategoria);

                conn.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public bool Actualizar(Producto entidad)
        {
            const string sql = @"
                UPDATE productos
                   SET nombre       = :nombre,
                       descripcion  = :descripcion,
                       precio       = :precio,
                       stock        = :stock,
                       id_categoria = :id_categoria
                 WHERE id_producto  = :id_producto";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add("nombre", entidad.Nombre);
                cmd.Parameters.Add("descripcion", entidad.Descripcion);
                cmd.Parameters.Add("precio", entidad.Precio);
                cmd.Parameters.Add("stock", entidad.Stock);
                cmd.Parameters.Add("id_categoria", entidad.IdCategoria);
                cmd.Parameters.Add("id_producto", entidad.Id);

                conn.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public bool Eliminar(int id)
        {
            const string sql = "DELETE FROM productos WHERE id_producto = :id_producto";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add("id_producto", id);

                conn.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public Producto ObtenerPorId(int id)
        {
            const string sql = "SELECT * FROM productos WHERE id_producto = :id_producto";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add("id_producto", id);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? Map(reader) : null;
                }
            }
        }

        public List<Producto> ObtenerTodos()
        {
            const string sql = "SELECT * FROM productos ORDER BY id_producto";
            var lista = new List<Producto>();

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

        /* ---------- Helper ---------- */

        private static Producto Map(OracleDataReader r)
        {
            return new Producto
            {
                Id = Convert.ToInt32(r["id_producto"]),
                Nombre = r["nombre"].ToString(),
                Descripcion = r["descripcion"]?.ToString(),
                Precio = Convert.ToDecimal(r["precio"]),
                Stock = Convert.ToInt32(r["stock"]),
                IdCategoria = Convert.ToInt32(r["id_categoria"])
            };
        }
    }
}