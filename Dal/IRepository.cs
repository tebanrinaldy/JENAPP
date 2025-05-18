using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Dal
{
   public  interface IRepository<T>
    {
        bool Agregar(T entidad);
        bool Actualizar(T entidad);
        bool Eliminar(int id);
        T ObtenerPorId(int id);
        List<T> ObtenerTodos();
    }
}
