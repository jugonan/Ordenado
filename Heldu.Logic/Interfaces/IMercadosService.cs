using Heldu.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heldu.Logic.Interfaces
{
    public interface IMercadosService
    {
        public Task<List<Mercado>> GetMercados();
        public Task<Mercado> DetailsMercado(int? id);
        public Task CreateMercadoPost(Mercado mercado);
        public Task<Mercado> EditMercadoGet(int? id);
        public Task EditMercadoPost(Mercado mercado);
        public Task DeleteMercadoPost(int id);
        public bool ExistMercado(int id);
    }
}
