using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Bll;
using Entity;
using Visual;
using Entity.Entity;
namespace tester
{
    class Program
    {
        static void Main()
        {
            string connectionString = Conexion.ConnectionString;
            var repo = new VentaRepository(connectionString);

            while (true)
            {
                Console.WriteLine("\n--- MENÚ CRUD VENTAS ---");
                Console.WriteLine("1. Agregar venta");
                Console.WriteLine("2. Consultar venta por ID");
                Console.WriteLine("3. Eliminar venta");
                Console.WriteLine("4. Listar todas las ventas");
                Console.WriteLine("0. Salir");
                Console.Write("Selecciona una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarVenta(repo);
                        break;
                    case "2":
                        ConsultarVenta(repo);
                        break;
                    case "3":
                        EliminarVenta(repo);
                        break;
                    case "4":
                        ListarVentas(repo);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }

        static void AgregarVenta(VentaRepository repo)
        {
            var venta = new Venta();

            Console.Write("Fecha de venta (yyyy-MM-dd): ");
            venta.FechaVenta = DateTime.Parse(Console.ReadLine());

            Console.Write("Total: ");
            venta.Total = decimal.Parse(Console.ReadLine());

            Console.Write("Cédula cliente: ");
            venta.CedulaCliente = Console.ReadLine();

            Console.Write("Nombre cliente: ");
            venta.NombreCliente = Console.ReadLine();

            Console.Write("Teléfono cliente: ");
            venta.TelefonoCliente = Console.ReadLine();

            venta.Detalles = new List<DetalleVenta>();

            Console.Write("¿Cuántos productos tiene la venta?: ");
            int cantidadDetalles = int.Parse(Console.ReadLine());

            for (int i = 0; i < cantidadDetalles; i++)
            {
                var detalle = new DetalleVenta();

                Console.Write($"ID del producto {i + 1}: ");
                detalle.ProductoId = int.Parse(Console.ReadLine());

                Console.Write($"Cantidad del producto {i + 1}: ");
                detalle.Cantidad = int.Parse(Console.ReadLine());

                venta.Detalles.Add(detalle);
            }

            if (repo.Agregar(venta))
                Console.WriteLine("✅ Venta registrada correctamente.");
            else
                Console.WriteLine("❌ Error al registrar la venta.");
        }

        static void ConsultarVenta(VentaRepository repo)
        {
            Console.Write("Ingresa el ID de la venta: ");
            int id = int.Parse(Console.ReadLine());

            var venta = repo.ObtenerPorId(id);

            if (venta != null)
            {
                Console.WriteLine($"\n🧾 Venta ID: {venta.Id}");
                Console.WriteLine($"Fecha: {venta.FechaVenta}");
                Console.WriteLine($"Total: {venta.Total}");
                Console.WriteLine($"Cliente: {venta.NombreCliente} ({venta.CedulaCliente}) - {venta.TelefonoCliente}");
                Console.WriteLine("Detalles:");
                foreach (var detalle in venta.Detalles)
                {
                    Console.WriteLine($" - Producto ID: {detalle.ProductoId}, Cantidad: {detalle.Cantidad}");
                }
            }
            else
            {
                Console.WriteLine("❌ Venta no encontrada.");
            }
        }

        static void EliminarVenta(VentaRepository repo)
        {
            Console.Write("Ingresa el ID de la venta a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            if (repo.Eliminar(id))
                Console.WriteLine("✅ Venta eliminada.");
            else
                Console.WriteLine("❌ No se pudo eliminar la venta.");
        }

        static void ListarVentas(VentaRepository repo)
        {
            var ventas = repo.ObtenerTodos();

            foreach (var venta in ventas)
            {
                Console.WriteLine($"\n🧾 Venta ID: {venta.Id}");
                Console.WriteLine($"Fecha: {venta.FechaVenta}");
                Console.WriteLine($"Total: {venta.Total}");
                Console.WriteLine($"Cliente: {venta.NombreCliente} ({venta.CedulaCliente}) - {venta.TelefonoCliente}");
            }

            if (ventas.Count == 0)
                Console.WriteLine("No hay ventas registradas.");
        }
    }
}