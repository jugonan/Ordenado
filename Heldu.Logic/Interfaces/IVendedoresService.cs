using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Heldu.Entities.Models;

namespace Heldu.Logic.Interfaces
{
    public interface IVendedoresService
    {
        public Task<List<Vendedor>> GetVendedor();
        public Task<Vendedor> DetailsVendedor(int? id);
        public Task CreateVendedor(Vendedor vendedor);
        public Task<Vendedor> EditVendedorGet(int? id);
        public Task EditVendedorPost(Vendedor vendedor);
        public Task<Vendedor> DeleteVendedorGet(int? id);
        public Task DeleteVendedorPost(int id);
        public bool ExistVendedor(int id);
        public Task<Vendedor> EstadisticasVendedor(string vendedorId);
        public Task<Vendedor> ObtenerVendedorDesdeProducto(int Id);
        public Task<Vendedor> ObtenerVendedorDesdedIdentity(string identityId);
        public Task<Vendedor> GetVendedorByIdentityUserId(string id);

    }
}
