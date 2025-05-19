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
        private List<Categoria> _categorias = new List<Categoria>();

        #region bh  q1
        public void Agregar(Categoria categoria)
        {
            categoria.Id = _categorias.Count + 1;
            categoria.FechaRegistro = DateTime.Now;
            _categorias.Add(categoria);
        }

        public void Eliminar(int id)
        {
            var categoria = _categorias.FirstOrDefault(c => c.Id == id);
            if (categoria != null)
                _categorias.Remove(categoria);
        }

        public void Actualizar(Categoria categoria)
        {
            var existente = ObtenerPorId(categoria.Id);
            if (existente != null)
            {
                existente.Nombre = categoria.Nombre;
            }
        }
        
        public Categoria ObtenerPorId(int id) => _categorias.FirstOrDefault(c => c.Id == id);

        public List<Categoria> Listar() => _categorias;
    }
    #endregion bh  q1
}