﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Reportes : EntidadBase
    {
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
        public string CedulaCliente { get; set; }
        public string NombreCliente { get; set; }
        public string TelefonoCliente { get; set; }

        public int IdVenta { get; set; }

    }
}