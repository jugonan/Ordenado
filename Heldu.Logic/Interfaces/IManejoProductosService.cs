using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Heldu.Entities.Models;

namespace Heldu.Logic.Interfaces
{
    public interface IManejoProductosService
    {
        public List<Producto> GetProductosDisponibles(int id);
        public List<Producto> GetProductosComprados(int id);
        public Task<List<Transaccion>> GetProductosVistosPorUsuario(int id);
        public Task<List<Transaccion>> GetProductosVistosDelVendedor(int id);
    }
}
