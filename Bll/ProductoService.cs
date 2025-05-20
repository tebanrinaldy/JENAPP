using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Entity;
namespace Bll
{
    /// <summary>
    /// Capa de lógica de negocio para la entidad Producto.
    /// – Valida las reglas de negocio.
    /// – Delega el CRUD al repositorio (DAL).
    /// </summary>
    public class ProductoService : IService<Producto>
    {
        private readonly ProductoRepository _productoRepo;
        private readonly CategoriaRepository _categoriaRepo;

        public ProductoService(string connectionString)
        {
            _productoRepo = new ProductoRepository(connectionString);
            _categoriaRepo = new CategoriaRepository(connectionString);
        }

        //Crear o Insertar
        public void Agregar(Producto producto)
        {
            //Validacion de categoria
            if (_categoriaRepo.ObtenerPorId(producto.IdCategoria) == null)
                throw new Exception("La categoría especificada no existe.");


            if (!_productoRepo.Agregar(producto))
                throw new Exception("No se pudo insertar el producto en la base de datos.");
        }

        //Modificar o Actualizar
        public void Actualizar(Producto producto)
        {
            if (_categoriaRepo.ObtenerPorId(producto.IdCategoria) == null)
                throw new Exception("La categoría especificada no existe.");

            if (!_productoRepo.Actualizar(producto))
                throw new Exception("No se pudo actualizar el producto.");
        }

        //Eliminar
        public void Eliminar(int id)
        {
            if (!_productoRepo.Eliminar(id))
                throw new Exception("No se pudo eliminar el producto.");
        }

        //Consulta por ID
        public Producto ObtenerPorId(int id) => _productoRepo.ObtenerPorId(id);

        public List<Producto> Listar() => _productoRepo.ObtenerTodos();
    }
}
