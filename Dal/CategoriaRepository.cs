using Entity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using System.Data;
using Oracle.ManagedDataAccess.Types;

namespace Dal
{
    public class CategoriaRepository : IRepository<Categoria>
    {
        /*-------------------------------------------------
         *  F I E L D S
         *------------------------------------------------*/
        private readonly string _connectionString;

        /*-------------------------------------------------
         *  C T O R
         *------------------------------------------------*/
        public CategoriaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /*-------------------------------------------------
         *  C R U D
         *------------------------------------------------*/

        // ------------ CREATE ------------
        public bool Agregar(Categoria entidad)
        {
            const string sql = @"
                INSERT INTO categorias (nombre)
                VALUES (:nombre)
                RETURNING id_categoria INTO :id_out";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;

                // IN
                cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = entidad.Nombre;

                // OUT
                var pIdOut = cmd.Parameters.Add("id_out", OracleDbType.Decimal);
                pIdOut.Direction = ParameterDirection.Output;

                conn.Open();
                var filas = cmd.ExecuteNonQuery();

                // convertir OracleDecimal → int
                entidad.Id = Convert.ToInt32(((OracleDecimal)pIdOut.Value).Value);

                return filas > 0;
            }
        }

        // ------------ READ (por id) ------------
        public Categoria ObtenerPorId(int id)
        {
            const string sql = @"
                SELECT id_categoria, nombre
                FROM   categorias
                WHERE  id_categoria = :id";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Categoria
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id_categoria")),
                            Nombre = reader.GetString(reader.GetOrdinal("nombre"))
                        };
                    }
                }
            }
            return null;
        }

        // ------------ READ (todos) ------------
        public List<Categoria> ObtenerTodos()
        {
            var lista = new List<Categoria>();

            const string sql = @"
                SELECT id_categoria, nombre
                FROM   categorias
                ORDER  BY id_categoria";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Categoria
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id_categoria")),
                            Nombre = reader.GetString(reader.GetOrdinal("nombre"))
                        });
                    }
                }
            }
            return lista;
        }

        // ------------ UPDATE ------------
        public bool Actualizar(Categoria entidad)
        {
            const string sql = @"
                UPDATE categorias
                SET    nombre = :nombre
                WHERE  id_categoria = :id_categoria";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = entidad.Nombre;
                cmd.Parameters.Add("id_categoria", OracleDbType.Int32).Value = entidad.Id;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ------------ DELETE ------------
        public bool Eliminar(int id)
        {
            const string sql = @"
                DELETE FROM categorias
                WHERE  id_categoria = :id";

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}