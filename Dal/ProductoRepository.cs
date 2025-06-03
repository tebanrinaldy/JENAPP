using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Oracle.ManagedDataAccess.Types;

namespace Dal

{
    public class ProductoRepository : IRepository<Producto>
    {
        public ProductoRepository()
        {
        }

        private OracleConnection GetConnection()
        {
            return Conexion.ObtenerConexion();
        }

        public bool Agregar(Producto entidad)
        {
            const string sql = @"
                INSERT INTO productos
                (nombre, descripcion, precio, stock, id_categoria)
                VALUES (:nombre, :descripcion, :precio, :stock, :id_categoria)
                RETURNING id_producto INTO :id_out";

            using (var conn = GetConnection())
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;

                cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = entidad.Nombre;
                cmd.Parameters.Add("descripcion", OracleDbType.Varchar2).Value = entidad.Descripcion;
                cmd.Parameters.Add("precio", OracleDbType.Decimal).Value = entidad.Precio;
                cmd.Parameters.Add("stock", OracleDbType.Int32).Value = entidad.Stock;
                cmd.Parameters.Add("id_categoria", OracleDbType.Int32).Value = entidad.IdCategoria;

                var pIdOut = cmd.Parameters.Add("id_out", OracleDbType.Decimal);
                pIdOut.Direction = ParameterDirection.Output;

                conn.Open();
                var filas = cmd.ExecuteNonQuery();

                entidad.Id = Convert.ToInt32(((Oracle.ManagedDataAccess.Types.OracleDecimal)pIdOut.Value).Value);

                return filas > 0;
            }
        }

        public Producto ObtenerPorId(int id)
        {
            const string sql = @"
                SELECT  p.id_producto,
                        p.nombre,
                        p.descripcion,
                        p.precio,
                        p.stock,
                        p.id_categoria,
                        c.nombre AS nom_categoria
                FROM    productos p
                JOIN    categorias c ON c.id_categoria = p.id_categoria
                WHERE   p.id_producto = :id";

            using (var conn = GetConnection())
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Map(reader);
                    }
                }
            }
            return null;
        }

        public List<Producto> ObtenerTodos()
        {
            const string sql = @"
                SELECT  p.id_producto,
                        p.nombre,
                        p.descripcion,
                        p.precio,
                        p.stock,
                        p.id_categoria,
                        c.nombre AS nom_categoria
                FROM    productos p
                JOIN    categorias c ON c.id_categoria = p.id_categoria
                ORDER   BY p.id_producto";

            var lista = new List<Producto>();

            using (var conn = GetConnection())
            using (var cmd = new OracleCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(Map(reader));
                    }
                }
            }
            return lista;
        }

        public bool Actualizar(Producto entidad)
        {
            const string sql = @"
                UPDATE productos
                SET    nombre       = :nombre,
                       descripcion  = :descripcion,
                       precio       = :precio,
                       stock        = :stock,
                       id_categoria = :id_categoria
                WHERE  id_producto  = :id_producto";

            using (var conn = GetConnection())
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;

                cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = entidad.Nombre;
                cmd.Parameters.Add("descripcion", OracleDbType.Varchar2).Value = entidad.Descripcion;
                cmd.Parameters.Add("precio", OracleDbType.Decimal).Value = entidad.Precio;
                cmd.Parameters.Add("stock", OracleDbType.Int32).Value = entidad.Stock;
                cmd.Parameters.Add("id_categoria", OracleDbType.Int32).Value = entidad.IdCategoria;
                cmd.Parameters.Add("id_producto", OracleDbType.Int32).Value = entidad.Id;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            const string sql = @"
                DELETE FROM productos
                WHERE  id_producto = :id";

            using (var conn = GetConnection())
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private static Producto Map(OracleDataReader reader)
        {
            return new Producto
            {
                Id = reader.GetInt32(reader.GetOrdinal("id_producto")),
                Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion"))
                                ? null
                                : reader.GetString(reader.GetOrdinal("descripcion")),
                Precio = reader.GetDecimal(reader.GetOrdinal("precio")),
                Stock = reader.GetInt32(reader.GetOrdinal("stock")),
                IdCategoria = reader.GetInt32(reader.GetOrdinal("id_categoria")),
                Categoria = new Categoria
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id_categoria")),
                    Nombre = reader.GetString(reader.GetOrdinal("nom_categoria"))
                }
            };
        }
    }
}