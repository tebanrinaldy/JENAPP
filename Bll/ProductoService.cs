using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
namespace Bll
{
    public class ProductoService : IService<Producto>
    {
        private List<Producto> _productos = new List<Producto>();

        public void Agregar(Producto producto)
        {
            producto.Id = _productos.Count + 1;
            producto.FechaRegistro = DateTime.Now;
            _productos.Add(producto);
        }

        public void Eliminar(int id)
        {
            var producto = _productos.FirstOrDefault(p => p.Id == id);
            if (producto != null)
                _productos.Remove(producto);
        }

        public void Actualizar(Producto producto)
        {
            var existente = ObtenerPorId(producto.Id);
            if (existente != null)
            {
                existente.Nombre = producto.Nombre;
                existente.Precio = producto.Precio;
                existente.Stock = producto.Stock;
                existente.IdCategoria = producto.IdCategoria;
            }
        }

        public Producto ObtenerPorId(int id)
        {
            return _productos.FirstOrDefault(p => p.Id == id);
        }

        public List<Producto> Listar()
        {
            return _productos;
        }
    }

}
