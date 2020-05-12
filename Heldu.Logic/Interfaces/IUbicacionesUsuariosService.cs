using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Heldu.Entities.Models;

namespace Heldu.Logic.Interfaces
{
    public interface IUbicacionesUsuariosService
    {
        public Task<List<UbicacionUsuario>> GetUbicacionUsuario();
        public Task<UbicacionUsuario> GetUbicacionUsuarioById(int? id);
        public Task CreateUbicacionUsuario(UbicacionUsuario ubicacion);
        public Task<UbicacionUsuario> EditUbicacionUsuarioGet(int? id);
        public Task EditUbicacionUsuarioPost(UbicacionUsuario ubicacion);
        public Task DeleteUbicacionUsuarioPost(int id);
        public bool ExistUbicacionUsuario(int id);
    }
}