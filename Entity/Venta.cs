using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Venta : EntidadBase
    {
        public DateTime FechaRegistro { get; set; }
        public decimal Total { get; set; }

        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }

        public int IdUsuario { get; set; }
        public ICollection<DetalleVenta> Detalles { get; set; } 
    }
}
