using Dal;
using Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class DetalleVentaService : IService<DetalleVenta>
    {
        private readonly IRepository<DetalleVenta> _detalleRepository;

        public DetalleVentaService(IRepository<DetalleVenta> detalleRepository)
        {
            _detalleRepository = detalleRepository;
        }

        public void Agregar(DetalleVenta entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));

            bool resultado = _detalleRepository.Agregar(entidad);
            if (!resultado)
                throw new Exception("No se pudo agregar el detalle de venta.");
        }

        public void Actualizar(DetalleVenta entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));

            bool resultado = _detalleRepository.Actualizar(entidad);
            if (!resultado)
                throw new Exception("No se pudo actualizar el detalle de venta.");
        }

        public void Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id inválido.");

            bool resultado = _detalleRepository.Eliminar(id);
            if (!resultado)
                throw new Exception("No se pudo eliminar el detalle de venta.");
        }

        public DetalleVenta ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id inválido.");

            return _detalleRepository.ObtenerPorId(id);
        }

        public List<DetalleVenta> Listar()
        {
            return _detalleRepository.ObtenerTodos();
        }
    }
}