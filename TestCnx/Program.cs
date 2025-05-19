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
            var repo = new ProductoRepository("");   // cadena vacía usa la de ConexionOracle
            var lista = repo.ObtenerTodos();
            Console.WriteLine($"Total productos: {lista.Count}");

        }
    }
}
