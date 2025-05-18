using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
namespace DAL_TXT
{
    public class ClienteRepositoryTxt
    {
        private string filePath = "clientes.txt";

        public void GuardarCliente(Cliente cliente)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{cliente.Id};{cliente.Nombre};{cliente.Apellido}");
            }
        }

        public List<Cliente> ObtenerClientes()
        {
            var clientes = new List<Cliente>();

            if (!File.Exists(filePath))
                return clientes;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var partes = line.Split(';');
                if (partes.Length >= 4)
                {
                    clientes.Add(new Cliente
                    {
                        Id = int.Parse(partes[0]),
                        Nombre = partes[1],
                        Apellido = partes[2],
                      
                    });
                }
            }

            return clientes;
        }
    }
}

