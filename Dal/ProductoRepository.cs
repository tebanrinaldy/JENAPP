using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Oracle.ManagedDataAccess.Client;

namespace Dal

{
    public class ProductoRepository : IRepository<Producto>
    {
        private string v;

        public ProductoRepository(string v)
        {
            this.v = v;
        }

        /* ---------------- CRUD ---------------- */

        public bool Agregar(Producto p)
        {
            const string sql = @"
                INSERT INTO producto
                (id, nombre, descripcion, precio, stock, id_categoria, fecha_registro)
                VALUES (SEQ_PRODUCTO.NEXTVAL, :nombre, :descripcion, :precio,
                        :stock, :id_categoria, :fecha_registro)";

            using (var cn = ConexionOracle.Obtener())
            using (var cmd = new OracleCommand(sql, cn))
            {
                AddParameters(cmd, p, includeId: false);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Actualizar(Producto p)
        {
            const string sql = @"
                UPDATE producto SET
                    nombre        = :nombre,
                    descripcion   = :descripcion,
                    precio        = :precio,
                    stock         = :stock,
                    id_categoria  = :id_categoria
                WHERE id = :id";

            using (var cn = ConexionOracle.Obtener())
            using (var cmd = new OracleCommand(sql, cn))
            {
                AddParameters(cmd, p, includeId: true);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (var cn = ConexionOracle.Obtener())
            using (var cmd = new OracleCommand("DELETE FROM producto WHERE id = :id", cn))
            {
                cmd.Parameters.Add("id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public Producto ObtenerPorId(int id)
        {
            using (var cn = ConexionOracle.Obtener())
            using (var cmd = new OracleCommand("SELECT * FROM producto WHERE id = :id", cn))
            {
                cmd.Parameters.Add("id", id);
                using (var dr = cmd.ExecuteReader())
                {
                    return dr.Read() ? Map(dr) : null;
                }
            }
        }

        public List<Producto> ObtenerTodos()
        {
            var lista = new List<Producto>();

            using (var cn = ConexionOracle.Obtener())
            using (var cmd = new OracleCommand("SELECT * FROM producto", cn))
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read()) lista.Add(Map(dr));
            }
            return lista;
        }

        /* ------------ helpers privados ------------ */

        private static void AddParameters(OracleCommand cmd, Producto p, bool includeId)
        {
            if (includeId) cmd.Parameters.Add("id", p.Id);

            cmd.Parameters.Add("nombre", p.Nombre);
            cmd.Parameters.Add("descripcion", p.Descripcion);
            cmd.Parameters.Add("precio", p.Precio);
            cmd.Parameters.Add("stock", p.Stock);
            cmd.Parameters.Add("id_categoria", p.IdCategoria);
            cmd.Parameters.Add("fecha_registro", p.FechaRegistro);
        }

        private static Producto Map(OracleDataReader dr)
        {
            return new Producto
            {
                Id = Convert.ToInt32(dr["id"]),
                Nombre = dr["nombre"].ToString(),
                Descripcion = dr["descripcion"].ToString(),
                Precio = Convert.ToDecimal(dr["precio"]),
                Stock = Convert.ToInt32(dr["stock"]),
                IdCategoria = Convert.ToInt32(dr["id_categoria"]),
                FechaRegistro = Convert.ToDateTime(dr["fecha_registro"])
            };
        }
    }
}