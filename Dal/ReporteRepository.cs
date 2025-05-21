using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public interface ReporteRepository : IRepository<Reportes>
    {
        List<Reportes> ReporteCompleto();
    }
}
