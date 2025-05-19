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
        static void Main()
        {
            /* ------------------------------------------------------------------
             *  1. Cadena de conexión
             *     Reemplaza USER / PASS y el alias o servicio de tu BD (XEPDB1, XE, ORCL, etc.)
             * ------------------------------------------------------------------*/
            const string connectionString =
                "User Id=jenapp;Password=jen123;Data Source=localhost:1521/XEPDB1";

            /* ------------------------------------------------------------------
             *  2. Instanciar los repositorios
             * ------------------------------------------------------------------*/
            var categoriaRepo = new CategoriaRepository(connectionString);
            var productoRepo = new ProductoRepository(connectionString);

            Console.WriteLine("== PRUEBA CRUD CATEGORÍA ==");
            /* 2.1  INSERTAR categoría */
            var cat = new Categoria { Nombre = "Bebidas" };
            bool okCat = categoriaRepo.Agregar(cat);
            Console.WriteLine($"Insert categoría OK: {okCat}  |  Nuevo Id = {cat.Id}");

            /* 2.2  LEER categoría recién insertada */
            var catDb = categoriaRepo.ObtenerPorId(cat.Id);
            Console.WriteLine($"Leído Cat => Id:{catDb?.Id}  Nombre:{catDb?.Nombre}");

            /* 2.3  ACTUALIZAR categoría */
            catDb.Nombre = "Bebidas frías";
            bool updCat = categoriaRepo.Actualizar(catDb);
            Console.WriteLine($"Update categoría OK: {updCat}");

            /* 2.4  LISTAR todas las categorías */
            foreach (var c in categoriaRepo.ObtenerTodos())
                Console.WriteLine($"- {c.Id}: {c.Nombre}");

            Console.WriteLine("\n== PRUEBA CRUD PRODUCTO ==");
            /* 3.1  INSERTAR producto ligado a la categoría */
            var prod = new Producto
            {
                Nombre = "Jugo de naranja",
                Descripcion = "Natural 500 ml",
                Precio = 3.50m,
                Stock = 25,
                IdCategoria = cat.Id
            };
            bool okProd = productoRepo.Agregar(prod);
            Console.WriteLine($"Insert producto OK: {okProd}  |  Nuevo Id = {prod.Id}");

            /* 3.2  LEER producto */
            var prodDb = productoRepo.ObtenerPorId(prod.Id);
            Console.WriteLine($"Leído Prod => {prodDb?.Id} - {prodDb?.Nombre}  ${prodDb?.Precio}");

            /* 3.3  ACTUALIZAR producto */
            prodDb.Stock += 10;
            prodDb.Precio = 3.75m;
            bool updProd = productoRepo.Actualizar(prodDb);
            Console.WriteLine($"Update producto OK: {updProd}");

            /* 3.4  LISTAR productos */
            foreach (var p in productoRepo.ObtenerTodos())
                Console.WriteLine($"- {p.Id}: {p.Nombre}  Stock:{p.Stock}  $ {p.Precio}");

            /* 4.  ELIMINAR (descomenta si quieres probar)
            // categoriaRepo.Eliminar(cat.Id);
            // productoRepo.Eliminar(prod.Id);
            */

            Console.WriteLine("\nPruebas finalizadas. Pulsa <Enter> para salir…");
            Console.ReadLine();
        }
    }
}