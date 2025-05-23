using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class MovimientoInventario
    {
        public int IdMovimiento { get; set; }
        public int IdProducto { get; set; }
        public string Tipo { get; set; }       // "Entrada" o "Salida"
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}
