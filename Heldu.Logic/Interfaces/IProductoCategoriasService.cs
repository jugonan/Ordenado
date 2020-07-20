using Heldu.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heldu.Logic.Interfaces
{
    public interface IProductoCategoriasService
    {
        public Task<List<ProductoCategoria>> GetProductosCategorias();
        public List<ProductoCategoria> GetProductosCategoriasSync();
        public Task<ProductoCategoria> GetProductoCategoriaById(int? id);
        public Task CreateProductoCategoriaPost(ProductoCategoria productoCategoria);
        public Task<ProductoCategoria> EditProductoCategoriaGet(int? id);
        public Task EditProductoCategoriaPost(ProductoCategoria productoCategoria);
        public Task DeleteProductoCategoriaPost(int id);
        public bool ExistProductoCategoria(int id);
    }
}
