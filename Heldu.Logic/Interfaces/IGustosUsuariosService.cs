using Heldu.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heldu.Logic.Interfaces
{
    public interface IGustosUsuariosService
    {
        public Task<IEnumerable<GustoUsuario>> GetGustosUsuarios();
        public Task<GustoUsuario> GetGustoUsuarioById(int? id);
        public Task CreateGustoUsuarioPost(GustoUsuario gustoUsuario);
        public Task<GustoUsuario> EditGustoUsuarioGet(int? id);
        public Task EditGustoUsuarioPost(GustoUsuario gustoUsuario);
        public Task DeleteGustoUsuarioPost(int id);
        public bool ExistGustoUsuario(int id);
    }
}
