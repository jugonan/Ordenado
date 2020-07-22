using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Heldu.Entities.Models;

namespace Heldu.Logic.Interfaces
{
    public interface IManejoProductosService
    {
        public List<Producto> GetProductosDisponibles(List<Producto> comprados);
        public List<Producto> GetProductosComprados(int id);
        public Task<List<Visita>> GetProductosVistosPorUsuario(int id);
        public Task<List<Visita>> GetProductosVistosDelVendedor(int id);
    }
}
