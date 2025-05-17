using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
   public class ReporteVentaService
    {
        private readonly List<Venta> _ventas;

        public ReporteVentaService(List<Venta> ventas)
        {
            _ventas = ventas;
        }

        public List<ReporteVenta> GenerarReportePorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            return _ventas
                .Where(v => v.FechaRegistro.Date >= fechaInicio.Date && v.FechaRegistro.Date <= fechaFin.Date)
                .Select(v => new ReporteVenta
                {
                    Fecha = v.FechaRegistro,
                    Usuario = $"{v.Usuario.Nombre} {v.Usuario.Apellido}",
                    Cliente = $"{v.Cliente.Nombre} {v.Cliente.Apellido}",
                    Total = v.Total
                })
                .ToList();
        }


    }
}
