using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace TestCnx
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var cn = ConexionOracle.Obtener())
                {
                    Console.WriteLine("¡Conexión exitosa a Oracle!");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error de conexión: {ex.Message}");
            }

            Console.WriteLine("Pulsa una tecla para salir…");
            Console.ReadKey();
        }
    }
}
