using System;
using System.Collections.Generic;
using Heldu.Entities.Models;

namespace Heldu.Logic.Interfaces
{
    public interface IManejoProductosService
    {
        public List<Producto> GetProductosDisponibles(int id);
        public List<Producto> GetProductosComprados(int id);
    }
}
