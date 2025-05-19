using Entity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Dal
{
    public class CategoriaRepository : IRepository<Categoria>
    {
        private readonly string _connectionString;

        public CategoriaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /* ---------- CRUD ---------- */

        public bool Agregar(Categoria entidad)
        {
            const string sql = @"
                INSERT INTO categorias
                    (id_categoria, nombre)
                VALUES
                    (seq_categorias.NEXTVAL, :nombre)";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add("nombre", entidad.Nombre);

                conn.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public bool Actualizar(Categoria entidad)
        {
            const string sql = @"
                UPDATE categorias 
                   SET nombre = :nombre
                 WHERE id_categoria = :id_categoria";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add("nombre", entidad.Nombre);
                cmd.Parameters.Add("id_categoria", entidad.Id);

                conn.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public bool Eliminar(int id)
        {
            const string sql = "DELETE FROM categorias WHERE id_categoria = :id_categoria";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add("id_categoria", id);

                conn.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public Categoria ObtenerPorId(int id)
        {
            const string sql = "SELECT * FROM categorias WHERE id_categoria = :id_categoria";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add("id_categoria", id);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? Map(reader) : null;
                }
            }
        }

        public List<Categoria> ObtenerTodos()
        {
            const string sql = "SELECT * FROM categorias ORDER BY id_categoria";
            var lista = new List<Categoria>();

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

        private static Categoria Map(OracleDataReader r)
        {
            return new Categoria
            {
                Id = Convert.ToInt32(r["id_categoria"]),
                Nombre = r["nombre"].ToString()
            };
        }
    }
}