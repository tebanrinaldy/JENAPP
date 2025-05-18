using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
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

            public bool Agregar(Producto entidad)
            {
                using (var conn = new OracleConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new OracleCommand("INSERT INTO producto (id, nombre, descripcion, precio, stock, id_categoria, fecha_registro) " +
                                                "VALUES (:id, :nombre, :descripcion, :precio, :stock, :id_categoria, :fecha_registro)", conn);

                    cmd.Parameters.Add(new OracleParameter("id", entidad.Id));
                    cmd.Parameters.Add(new OracleParameter("nombre", entidad.Nombre));
                    cmd.Parameters.Add(new OracleParameter("descripcion", entidad.Descripcion));
                    cmd.Parameters.Add(new OracleParameter("precio", entidad.Precio));
                    cmd.Parameters.Add(new OracleParameter("stock", entidad.Stock));
                    cmd.Parameters.Add(new OracleParameter("id_categoria", entidad.IdCategoria));
                    cmd.Parameters.Add(new OracleParameter("fecha_registro", entidad.FechaRegistro));

                    return cmd.ExecuteNonQuery() > 0;
                }
            }

            public bool Actualizar(Producto entidad)
            {
                using (var conn = new OracleConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new OracleCommand("UPDATE producto SET nombre = :nombre, descripcion = :descripcion, precio = :precio, " +
                                                "stock = :stock, id_categoria = :id_categoria WHERE id = :id", conn);

                    cmd.Parameters.Add(new OracleParameter("nombre", entidad.Nombre));
                    cmd.Parameters.Add(new OracleParameter("descripcion", entidad.Descripcion));
                    cmd.Parameters.Add(new OracleParameter("precio", entidad.Precio));
                    cmd.Parameters.Add(new OracleParameter("stock", entidad.Stock));
                    cmd.Parameters.Add(new OracleParameter("id_categoria", entidad.IdCategoria));
                    cmd.Parameters.Add(new OracleParameter("id", entidad.Id));

                    return cmd.ExecuteNonQuery() > 0;
                }
            }

            public bool Eliminar(int id)
            {
                using (var conn = new OracleConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new OracleCommand("DELETE FROM producto WHERE id = :id", conn);
                    cmd.Parameters.Add(new OracleParameter("id", id));

                    return cmd.ExecuteNonQuery() > 0;
                }
            }

            public Producto ObtenerPorId(int id)
            {
                using (var conn = new OracleConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new OracleCommand("SELECT * FROM producto WHERE id = :id", conn);
                    cmd.Parameters.Add(new OracleParameter("id", id));

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Producto
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nombre = reader["nombre"].ToString(),
                                Descripcion = reader["descripcion"].ToString(),
                                Precio = Convert.ToDecimal(reader["precio"]),
                                Stock = Convert.ToInt32(reader["stock"]),
                                IdCategoria = Convert.ToInt32(reader["id_categoria"]),
                                FechaRegistro = Convert.ToDateTime(reader["fecha_registro"])
                            };
                        }
                    }
                }
                return null;
            }

            public List<Producto> ObtenerTodos()
            {
                var lista = new List<Producto>();
                using (var conn = new OracleConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new OracleCommand("SELECT * FROM producto", conn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Producto
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nombre = reader["nombre"].ToString(),
                                Descripcion = reader["descripcion"].ToString(),
                                Precio = Convert.ToDecimal(reader["precio"]),
                                Stock = Convert.ToInt32(reader["stock"]),
                                IdCategoria = Convert.ToInt32(reader["id_categoria"]),
                                FechaRegistro = Convert.ToDateTime(reader["fecha_registro"])
                            });
                        }
                    }
                }
                return lista;
            }
        }


    }

