using Heldu.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heldu.Logic.Interfaces
{
    public interface IOpcionesProductosService
    {
        public Task<List<OpcionProducto>> GetOpcionesProductos();
        public Task<OpcionProducto> GetOpcionProductoById(int? id);
        public Task CreateOpcionProductoPost(OpcionProducto opcionProducto);
        public Task<OpcionProducto> EditOpcionProductoGet(int? id);
        public Task EditOpcionProductoPost(OpcionProducto opcionProducto);
        public Task DeleteOpcionProductoPost(int id);
        public bool ExistOpcionProducto(int id);
    }
}
