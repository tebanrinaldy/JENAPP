using Dal;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class VentaService : IService<Venta>
    {
        private readonly IRepository<Venta> _ventaRepo;

        public VentaService(IRepository<Venta> ventaRepo)
        {
            _ventaRepo = ventaRepo;
        }

        public void Agregar(Venta venta)
        {
            venta.FechaRegistro = DateTime.Now;
            venta.Total = venta.Detalles?.Sum(d => d.Cantidad * d.PrecioUnitario) ?? 0;
            _ventaRepo.Agregar(venta);
        }

        public void Actualizar(Venta venta)
        {
            _ventaRepo.Actualizar(venta);
        }

        public void Eliminar(int id)
        {
            _ventaRepo.Eliminar(id);
        }

        public Venta ObtenerPorId(int id)
        {
            return _ventaRepo.ObtenerPorId(id);
        }

        public List<Venta> Listar()
        {
            return _ventaRepo.ObtenerTodos();
        }
    }
}