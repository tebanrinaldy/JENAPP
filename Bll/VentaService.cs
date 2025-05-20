using Dal;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    using Entity;
    using Dal;
    using System;
    using System.Collections.Generic;

    namespace Bll
    {
        public class VentaService : IService<Venta>
        {
            private readonly IRepository<Venta> _ventaRepository;

            public VentaService(IRepository<Venta> ventaRepository)
            {
                _ventaRepository = ventaRepository;
            }

            public void Agregar(Venta entidad)
            {
                if (entidad == null)
                    throw new ArgumentNullException(nameof(entidad));

                if (entidad.Detalles == null || entidad.Detalles.Count == 0)
                    throw new ArgumentException("La venta debe tener al menos un detalle.");

                bool resultado = _ventaRepository.Agregar(entidad);
                if (!resultado)
                    throw new Exception("No se pudo agregar la venta.");
            }

            public void Actualizar(Venta entidad)
            {
                if (entidad == null)
                    throw new ArgumentNullException(nameof(entidad));
                if (entidad.Id <= 0)
                    throw new ArgumentException("El Id de la venta no es válido.");

                bool resultado = _ventaRepository.Actualizar(entidad);
                if (!resultado)
                    throw new Exception("No se pudo actualizar la venta.");
            }

            public void Eliminar(int id)
            {
                if (id <= 0)
                    throw new ArgumentException("El Id no es válido.");

                bool resultado = _ventaRepository.Eliminar(id);
                if (!resultado)
                    throw new Exception("No se pudo eliminar la venta.");
            }

            public Venta ObtenerPorId(int id)
            {
                if (id <= 0)
                    throw new ArgumentException("El Id no es válido.");

                return _ventaRepository.ObtenerPorId(id);
            }

            public List<Venta> Listar()
            {
                return _ventaRepository.ObtenerTodos();
            }
        }
    }
}