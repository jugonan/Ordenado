using System.Collections.Generic;
using System.Threading.Tasks;
using Heldu.Entities.Models;

namespace Heldu.Logic.Interfaces
{
    public interface IUsuariosService
    {
        public Task<List<Usuario>> GetUsuario();
        public Task<Usuario> GetUsuarioById(int? id);
        public Task CreateUsuario(Usuario usuario);
        public Task<Usuario> EditUsuarioGet(int? id);
        public Task EditUsuarioPost(Usuario usuario);
        public Task DeleteUsuarioPost(int id);
        public bool ExistUsuario(int id);
        public Task<Usuario> MisCursosUsuario(string userManagerId);
        public Task<List<Usuario>> GetUsuariosListByActiveIdentityUser(string usuarioId);
        public Task<Usuario> GetUsuarioByActiveIdentityUser(string usuarioId);
        public Task<Usuario> ObtenerUsuarioDesdedIdentity(string identityId);
        public Task<List<Usuario>> GestionarUsuarios();
    }
}
