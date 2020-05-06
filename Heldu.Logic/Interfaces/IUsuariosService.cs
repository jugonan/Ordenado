using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Heldu.Entities.Models;

namespace Heldu.Logic.Interfaces
{
    public interface IUsuariosService
    {
        public Task<List<Usuario>> GetUsuario();
        public Task<Usuario> DetailsUsuario(int? id);
        public Task<Usuario> CreateUsuario(Usuario usuario);
        public Task<Usuario> EditUsuarioGet(int? id);
        public Task EditUsuarioPost(Usuario usuario);
        public Task<Usuario> DeleteUsuarioGet(int? id);
        public Task<Usuario> DeleteUsuarioPost(int id);
        public bool ExistUsuario(int id);
        public Task<Usuario> MiPerfilUsuario(string userManagerId);
        public Task<Usuario> MicuentaUsuario(string userManagerId);
        public Task<Usuario> HistoricoUsuario(string userManagerId);
        public Task<Usuario> RewardsUsuario(string userManagerId);
        public Task<Usuario> MiscursosUsuario(string userManagerId);
    }
}
