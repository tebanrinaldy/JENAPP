using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Entity;
namespace Bll
{
    public class ProductoService : IService<Producto>
    {
        private readonly ProductoRepository _productoRepo;
        private readonly CategoriaRepository _categoriaRepo;

        public ProductoService()
        {
            _productoRepo = new ProductoRepository();
            _categoriaRepo = new CategoriaRepository();
        }

        public void Agregar(Producto producto)
        {
            if (_categoriaRepo.ObtenerPorId(producto.IdCategoria) == null)
                throw new Exception("La categoría especificada no existe.");

            if (!_productoRepo.Agregar(producto))
                throw new Exception("No se pudo insertar el producto en la base de datos.");
        }

        public void Actualizar(Producto producto)
        {
            if (_categoriaRepo.ObtenerPorId(producto.IdCategoria) == null)
                throw new Exception("La categoría especificada no existe.");

            if (!_productoRepo.Actualizar(producto))
                throw new Exception("No se pudo actualizar el producto.");
        }

        public void Eliminar(int id)
        {
            if (!_productoRepo.Eliminar(id))
                throw new Exception("No se pudo eliminar el producto.");
        }

        public Producto ObtenerPorId(int id) => _productoRepo.ObtenerPorId(id);

        public List<Producto> Listar() => _productoRepo.ObtenerTodos();
    }
}