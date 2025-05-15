using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Caja: EntidadBase 
    {
        public decimal MontoInicial { get; set; }
        public decimal MontoFinal { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime? FechaCierre { get; set; }

        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }
}
