using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
namespace Bll
{
   public class CajaService : IService<Caja>
    {
        private List<Caja> _cajas = new List<Caja>();
        private int _contadorId = 1;

        public void Agregar(Caja caja)
        {
            caja.Id = _contadorId++;
            caja.FechaCierre = DateTime.Now;
            caja.FechaApertura = DateTime.Now;
            _cajas.Add(caja);
        }

        public void Eliminar(int id)
        {
            var caja = _cajas.FirstOrDefault(c => c.Id == id);
            if (caja != null)
                _cajas.Remove(caja);
        }

        public void Actualizar(Caja caja)
        {
            var existente = ObtenerPorId(caja.Id);
            if (existente != null)
            {
                existente.MontoInicial = caja.MontoInicial;
                existente.MontoFinal = caja.MontoFinal;
                existente.FechaApertura = caja.FechaApertura;
                existente.FechaCierre = caja.FechaCierre;
                existente.IdUsuario = caja.IdUsuario;
                existente.Usuario = caja.Usuario;
            }
        }

        public Caja ObtenerPorId(int id) => _cajas.FirstOrDefault(c => c.Id == id);

        public List<Caja> Listar() => _cajas;

    }
}
