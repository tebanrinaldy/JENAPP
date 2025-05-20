using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class ClienteService : IService<Cliente>
    {
        private List<Cliente> _clientes = new List<Cliente>();

        public void Agregar(Cliente cliente)
        {
            cliente.Id = _clientes.Count + 1;
            _clientes.Add(cliente);
        }

        public void Eliminar(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            if (cliente != null)
                _clientes.Remove(cliente);
        }

        public void Actualizar(Cliente cliente)
        {
            var existente = ObtenerPorId(cliente.Id);
            if (existente != null)
            {
                existente.Nombre = cliente.Nombre;
                existente.Apellido = cliente.Apellido;
                existente.Documento = cliente.Documento;
                existente.Telefono = cliente.Telefono;
                existente.Direccion = cliente.Direccion;
            }
        }

        public Cliente ObtenerPorId(int id) => _clientes.FirstOrDefault(c => c.Id == id);

        public List<Cliente> Listar() => _clientes;
    }
}
