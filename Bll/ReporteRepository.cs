using Dal;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class ReporteService : IService<Reportes>
    {
        private readonly IRepository<Reportes> _repository;

        public ReporteService(IRepository<Reportes> repository)
        {
            _repository = repository;
        }

        public void Agregar(Reportes entidad)
        {
            // No se implementa porque los reportes no se agregan manualmente
        }

        public void Actualizar(Reportes entidad)
        {
            _repository.Actualizar(entidad);
        }

        public void Eliminar(int id)
        {
            _repository.Eliminar(id);
        }

        public Reportes ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        public List<Reportes> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }

        public List<Reportes> Listar()
        {
            return _repository.ObtenerTodos();
        }
    }
}