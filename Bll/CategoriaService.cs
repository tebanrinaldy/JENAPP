using Dal;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class CategoriaService : IService<Categoria>
    {
        private readonly CategoriaRepository _categoriaRepo;

        public CategoriaService()
        {
            _categoriaRepo = new CategoriaRepository();
        }

        public void Agregar(Categoria categoria)
        {
            if (!_categoriaRepo.Agregar(categoria))
                throw new Exception("No se pudo agregar la categoría.");
        }

        public void Eliminar(int id)
        {
            if (!_categoriaRepo.Eliminar(id))
                throw new Exception("No se pudo eliminar la categoría.");
        }

        public void Actualizar(Categoria categoria)
        {
            if (!_categoriaRepo.Actualizar(categoria))
                throw new Exception("No se pudo actualizar la categoría.");
        }

        public Categoria ObtenerPorId(int id) => _categoriaRepo.ObtenerPorId(id);

        public List<Categoria> Listar() => _categoriaRepo.ObtenerTodos();
    }
}