using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Heldu.Entities.Models;

namespace Heldu.Logic.Interfaces
{
    public interface IUbicacionesService
    {
        public Task<List<Ubicacion>> GetUbiacaciones ();
        public Task<Ubicacion> DetailsUbicacion(int? id);
        public Task<Ubicacion> CreateUbicacion(Ubicacion ubicacion);
        public Task<Ubicacion> EditUbicacionGet(int? id);
        public Task EditUbicacionPost(Ubicacion ubicacion);
        public Task<Ubicacion> DeleteUbicacionGet(int? id);
        public Task<Ubicacion> DeleteUbicacionPost(int id);
        public bool ExistUbicacion(int id);
    }
}