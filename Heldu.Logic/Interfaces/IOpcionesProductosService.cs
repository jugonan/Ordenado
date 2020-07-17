using Heldu.Entities.Models;
using Heldu.Logic.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heldu.Logic.Interfaces
{
    public interface IOpcionesProductosService
    {
        public Task<List<OpcionProducto>> GetOpcionesProductos();
        public Task<List<OpcionProducto>> GetOpcionProductoById(int? id);
        public Task<OpcionProducto> GetFirstOpcionProductoByProductoId(int? id);
        public Task<OpcionProducto> GetOpcionProductoByHisId(int? id);
        public Task CreateOpcionProductoPost(OpcionProducto opcionProducto);
        public Task<OpcionProducto> EditOpcionProductoGet(int? id);
        public Task EditOpcionProductoPost(OpcionProducto opcionProducto);
        public Task DeleteOpcionProductoPost(int id);
        public bool ExistOpcionProducto(int id);
        public OpcionProducto crearDesdeJson(string json, int productoId);
    }
}
