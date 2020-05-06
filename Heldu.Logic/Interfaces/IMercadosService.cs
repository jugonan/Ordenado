using Heldu.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heldu.Logic.Interfaces
{
    public interface IMercadosService
    {
        public Task<List<Mercado>> GetMercados();

    }
}
