using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Heldu.Entities.Models;

namespace Heldu.Logic.Interfaces
{
    public interface IVisitasService
    {
        public Task<List<Visita>> GetVisitas();
        public Task<Visita> GetVisitaById(int? id);
        public Task CreateVisita(Visita Visita);
        public Task<Visita> EditVisitaGet(int? id);
        public Task EditVisitaPost(Visita Visita);
        public Task DeleteVisitaPost(int id);
        public bool ExistVisita(int id);
        public Task CreateVisitaWithUsuarioAndProductoVendedor(Usuario usuario, ProductoVendedor productoVendedor);
    }
}
