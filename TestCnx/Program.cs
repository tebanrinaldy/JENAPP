using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Entity;

namespace TestCnx
{
    internal class Program
    {
        private const string connectionString =
            "User Id=jenapp;Password=jen123;Data Source=192.168.1.38:1521/XEPDB1;";

        private static CategoriaRepository categoriaRepo = new CategoriaRepository(connectionString);
        private static ProductoRepository productoRepo = new ProductoRepository(connectionString);

        private static void Main()
        {
            Console.Title = "CRUD JENAPP";

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== MENÚ PRINCIPAL =====");
                Console.WriteLine("1. CRUD Categorías");
                Console.WriteLine("2. CRUD Productos");
                Console.WriteLine("0. Salir");
                Console.Write("Opción: ");
                switch (Console.ReadLine())
                {
                    case "1": CrudCategorias(); break;
                    case "2": CrudProductos(); break;
                    case "0": return;
                    default: break;
                }
            }
        }

        #region === Categorías ===
        private static void CrudCategorias()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-- Categorías --");
                ListarCategorias();
                Console.WriteLine("\nA) Agregar   E) Editar   B) Borrar   0) Volver");
                Console.Write("Opción: ");
                var op = Console.ReadLine()?.ToUpper();
                switch (op)
                {
                    case "A": AgregarCategoria(); break;
                    case "E": EditarCategoria(); break;
                    case "B": BorrarCategoria(); break;
                    case "0": return;
                    default: break;
                }
            }
        }

        private static void ListarCategorias()
        {
            var list = categoriaRepo.ObtenerTodos();
            foreach (var c in list)
                Console.WriteLine($"{c.Id,3} - {c.Nombre}");
        }

        private static void AgregarCategoria()
        {
            Console.Write("Nombre nueva categoría: ");
            var nombre = Console.ReadLine();
            var cat = new Categoria { Nombre = nombre };
            categoriaRepo.Agregar(cat);
        }

        private static void EditarCategoria()
        {
            Console.Write("Id categoría a editar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var cat = categoriaRepo.ObtenerPorId(id);
                if (cat == null) return;

                Console.Write($"Nombre [{cat.Nombre}]: ");
                var nombre = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nombre))
                    cat.Nombre = nombre;

                categoriaRepo.Actualizar(cat);
            }
        }

        private static void BorrarCategoria()
        {
            Console.Write("Id categoría a borrar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
                categoriaRepo.Eliminar(id);
        }
        #endregion

        #region === Productos ===
        private static void CrudProductos()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-- Productos --");
                ListarProductos();
                Console.WriteLine("\nA) Agregar   E) Editar   B) Borrar   0) Volver");
                Console.Write("Opción: ");
                var op = Console.ReadLine()?.ToUpper();
                switch (op)
                {
                    case "A": AgregarProducto(); break;
                    case "E": EditarProducto(); break;
                    case "B": BorrarProducto(); break;
                    case "0": return;
                    default: break;
                }
            }
        }

        private static void ListarProductos()
        {
            var list = productoRepo.ObtenerTodos();
            foreach (var p in list)
                Console.WriteLine($"{p.Id,3} - {p.Nombre,-25} Stock:{p.Stock,3}  ${p.Precio}");
        }

        private static void AgregarProducto()
        {
            Console.Write("Nombre: ");
            var nombre = Console.ReadLine();

            Console.Write("Descripción: ");
            var desc = Console.ReadLine();

            Console.Write("Precio: ");
            decimal.TryParse(Console.ReadLine(), out decimal precio);

            Console.Write("Stock: ");
            int.TryParse(Console.ReadLine(), out int stock);

            Console.Write("Id Categoría: ");
            int.TryParse(Console.ReadLine(), out int idCat);

            var prod = new Producto
            {
                Nombre = nombre,
                Descripcion = desc,
                Precio = precio,
                Stock = stock,
                IdCategoria = idCat
            };
            productoRepo.Agregar(prod);
        }

        private static void EditarProducto()
        {
            Console.Write("Id producto a editar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var p = productoRepo.ObtenerPorId(id);
                if (p == null) return;

                Console.Write($"Nombre [{p.Nombre}]: ");
                var nombre = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nombre)) p.Nombre = nombre;

                Console.Write($"Descripción [{p.Descripcion}]: ");
                var desc = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(desc)) p.Descripcion = desc;

                Console.Write($"Precio [{p.Precio}]: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal precio)) p.Precio = precio;

                Console.Write($"Stock [{p.Stock}]: ");
                if (int.TryParse(Console.ReadLine(), out int stock)) p.Stock = stock;

                Console.Write($"Id Categoría [{p.IdCategoria}]: ");
                if (int.TryParse(Console.ReadLine(), out int idCat)) p.IdCategoria = idCat;

                productoRepo.Actualizar(p);
            }
        }

        private static void BorrarProducto()
        {
            Console.Write("Id producto a borrar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
                productoRepo.Eliminar(id);
        }
        #endregion
    }
}