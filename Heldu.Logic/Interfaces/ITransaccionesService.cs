using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Heldu.Entities.Models;

namespace Heldu.Logic.Interfaces
{
    public interface ITransaccionesService
    {
        public Task<List<Transaccion>> GetTransaccion();
        public Task<Transaccion> DetailsTransaccion(int? id);
        public Task CreateTransaccion(Transaccion transaccion);
        public Task<Transaccion> EditTransaccionGet(int? id);
        public Task EditTransaccionPost(Transaccion transaccion);
        public Task DeleteTransaccionPost(int id);
        public bool ExistTransaccion(int id);
        public Task CreateTransaccionWithUsuarioAndProductoVendedor(Usuario usuario, ProductoVendedor productoVendedor);
    }
}
