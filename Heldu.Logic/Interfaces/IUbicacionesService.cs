using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Heldu.Entities.Models;

namespace Heldu.Logic.Interfaces
{
    public interface IUbicacionesService
    {
        public Task<List<Ubicacion>> GetUbicacion();
        public Task<Ubicacion> GetUbicacionById(int? id);
        public Task CreateUbicacion(Ubicacion ubicacion);
        public Task<Ubicacion> EditUbicacionGet(int? id);
        public Task EditUbicacionPost(Ubicacion ubicacion);
        public Task DeleteUbicacionPost(int id);
        public bool ExistUbicacion(int id);
    }
}