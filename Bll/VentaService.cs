using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
   public  class VentaService : IService<Venta>
    {
    private List<Venta> _ventas = new List<Venta>();
    private int _contadorId = 1;

    public void Agregar(Venta venta)
    {
        venta.Id = _contadorId++;
        venta.FechaRegistro = DateTime.Now;

      
        venta.Total = venta.Detalles?.Sum(d => d.Cantidad * d.PrecioUnitario) ?? 0;

        _ventas.Add(venta);
    }

    public void Eliminar(int id)
    {
        var venta = _ventas.FirstOrDefault(v => v.Id == id);
        if (venta != null)
            _ventas.Remove(venta);
    }

    public void Actualizar(Venta venta)
    {
        var existente = ObtenerPorId(venta.Id);
        if (existente != null)
        {
            existente.IdCliente = venta.IdCliente;
            existente.Cliente = venta.Cliente;
            existente.IdUsuario = venta.IdUsuario;
            existente.Usuario = venta.Usuario;
            existente.Total = venta.Total;
            existente.Detalles = venta.Detalles;
        }
    }

    public Venta ObtenerPorId(int id) => _ventas.FirstOrDefault(v => v.Id == id);

    public List<Venta> Listar() => _ventas;
}
}
