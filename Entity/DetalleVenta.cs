using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    namespace Entity
    {
        public class DetalleVenta : EntidadBase
        {
            public int VentaId { get; set; }
            public Venta Venta { get; set; }

            public int ProductoId { get; set; }
            public string NombreProducto { get; set; }
            public Producto Producto { get; set; }

            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }

            public decimal Subtotal { get; set; }
             

        }
    }
}