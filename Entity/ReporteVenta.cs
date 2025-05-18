using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public class ReporteVenta : EntidadBase
    {

        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Usuario { get; set; }
        public decimal Total { get; set; }
    }
}
