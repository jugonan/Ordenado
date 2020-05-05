using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DEFINITIVO.Models;

namespace DEFINITIVO.Services
{
    public interface IManejo_Productos
    {
        public List<Producto> GetProductosCanjeados(int id);
        public List<Producto> GetProductosDisponibles(int id);
        public List<Producto> GetProductosComprados(int id);
    }
}
