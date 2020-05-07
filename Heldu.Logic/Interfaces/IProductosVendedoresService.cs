using System.Collections.Generic;
using System.Threading.Tasks;
using Heldu.Entities.Models;

namespace Heldu.Logic.Interfaces
{
    public interface IProductosVendedoresService
    {
        public Task<List<ProductoVendedor>> GetProductoVendedor();
        public Task<ProductoVendedor> GetProductoVendedorById(int? id);
        public Task CreateProductoVendedor(ProductoVendedor productoVendedor);
        public Task<ProductoVendedor> EditProductoVendedorGet(int? id);
        public Task EditProductoVendedorPost(ProductoVendedor productoVendedorreview);
        public Task DeleteProductoVendedorPost(int id);
        public bool ExistProductoVendedor(int id);
        public Task<ProductoVendedor> ProductoVendedorByProductoId(int? id);
    }
}
