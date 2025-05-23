using Dal;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class InventarioLogica
    {
        private MovimientoInventarioDAO dao = new MovimientoInventarioDAO();

        public void ProcesarMovimiento(MovimientoInventario mov)
        {
            if (mov.Cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a cero.");

            if (mov.Tipo != "Entrada" && mov.Tipo != "Salida")
                throw new Exception("El tipo de movimiento debe ser 'Entrada' o 'Salida'.");

            dao.RegistrarMovimiento(mov);
        }
    }
}
