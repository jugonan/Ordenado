using Heldu.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heldu.Logic.Interfaces
{
    public interface IProductosService
    {
        public Task<List<Producto>> GetProductos();
        public Task<Producto> DetailsProducto(int? id);
        public Task CreateProductoPost(Producto producto);
        public Task<Producto> EditProductoGet(int? id);
        public Task EditProductoPost(Producto producto);
        public Task<Producto> DeleteProductoGet(int? id);
        public Task DeleteProductoPost(int id);
        public bool ExistProducto(int id);
        public Task<Producto> GetProductoById(int? id);
        public Task AddCantidadVisitasProductoById(int? id);
        public Task<List<Producto>> BuscarProductosPorString(string inputBuscar);
        public Task<List<Producto>> GetProductosByCategoriaId(int categoriaId);

    }
}
