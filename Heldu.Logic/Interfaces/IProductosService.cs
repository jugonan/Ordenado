using Heldu.Entities.Models;
using Heldu.Logic.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heldu.Logic.Interfaces
{
    public interface IProductosService
    {
        public Task<List<Producto>> GetProductos();
        public Task<Producto> GetProductoById(int? id);
        public Task CreateProductoPost(Producto producto);
        public Task<Producto> EditProductoGet(int? id);
        public Task EditProductoPost(Producto producto);
        public Task DeleteProductoPost(int id);
        public bool ExistProducto(int id);
        public Task AddCantidadVisitasProductoById(int? id);
        public Task<List<ProductoPrimeraOpcionProductoVM>> BuscarProductosPorString(string inputBuscar);
        public Task<List<ProductoPrimeraOpcionProductoVM>> BuscarProductosPorStringYCategoria(string inputBuscar, int categoriaId);
        public Task<List<Producto>> GetProductosByCategoriaId(int categoriaId);
        public Task<ProductosForIndex2VM> GetProductosForIndex2(List<Categoria> listaCategorias, List<Producto> listaProductos, List<ProductoCategoria> listaProductosCategorias);
        public Task<byte[]> AgregarImagenesBlob(List<IFormFile> ImagenProducto);
    }
}
