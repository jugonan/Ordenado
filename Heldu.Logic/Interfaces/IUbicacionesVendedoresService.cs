using System.Collections.Generic;
using System.Threading.Tasks;
using Heldu.Entities.Models;
using Heldu.Logic.ViewModels;

namespace Heldu.Logic.Interfaces
{
    public interface IUbicacionesVendedoresService
    {
        public Task<List<UbicacionVendedor>> GetUbicacionVendedor();
        public Task<UbicacionVendedor> GetUbicacionVendedorById(int? id);
        public Task CreateUbicacionVendedor(UbicacionVendedor ubicacion);
        public Task<UbicacionVendedor> EditUbicacionVendedorGet(int? id);
        public Task EditUbicacionVendedorPost(UbicacionVendedor ubicacion);
        public Task DeleteUbicacionVendedorPost(int id);
        public bool ExistUbicacionVendedor(int id);
    }
}