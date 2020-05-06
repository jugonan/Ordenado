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
        public Task<Transaccion> CreateTransaccion(Transaccion transaccion);
        public Task<Transaccion> EditTransaccionGet(int? id);
        public Task EditTransaccionPost(Transaccion transaccion);
        public Task<Transaccion> DeleteTransaccionGet(int? id);
        public Task<Transaccion> DeleteTransaccionPost(int id);
        public bool ExistTransaccion(int id);
    }
}
