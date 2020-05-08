using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heldu.Logic.Services
{
    public class ProductosService: IProductosService
    {
        private readonly ApplicationDbContext _context;
        public ProductosService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Producto>> GetProductos()
        {
            return await _context.Producto
                            .Include(p => p.ProductoCategoria)
                            .ThenInclude(a => a.Categoria)
                            .Include(p => p.ProductoVendedor)
                            .ThenInclude(a => a.Vendedor)
                            .ToListAsync();
        }
<<<<<<< Updated upstream
        public async Task<Producto> GetProductoById(int? id)
=======

        public async Task<List<List<Producto>>> Index2VM(List<Categoria> categorias)
        {
            List<ProductoCategoria> productoCategorias = await _context.ProductoCategoria.ToListAsync();
            List<List<Producto>> productos = new List<List<Producto>>();
            foreach (Categoria categoria in categorias)
            {
                List<Producto> productosAdd = new List<Producto>();
                foreach (ProductoCategoria item in productoCategorias)
                {
                    if (item.CategoriaId == categoria.Id)
                    {
                        productosAdd.Add(item.Producto);
                    }
                }
                productos.Add(productosAdd);
            }
            return productos;
        }

        public async Task<Producto> DetailsProducto(int? id)
>>>>>>> Stashed changes
        {
            return await _context.Producto.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task CreateProductoPost(Producto producto)
        {
            _context.Add(producto);
            await _context.SaveChangesAsync();
        }
        public async Task<Producto> EditProductoGet (int? id)
        {
            return await _context.Producto.FindAsync(id);
        }
        public async Task EditProductoPost(Producto producto)
        {
           _context.Update(producto);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProductoPost (int id)
        {
            _context.Producto.Remove(await _context.Producto.FindAsync(id));
            await _context.SaveChangesAsync();
        }
        public bool ExistProducto(int id)
        {
            return _context.Producto.Any(e => e.Id == id);
        }
        public async Task AddCantidadVisitasProductoById(int? id)
        {
            Producto productoActual = await GetProductoById(id);
            productoActual.CantidadVisitas++;
            _context.Update(productoActual);
            await _context.SaveChangesAsync();
        }
        //Método que devuelve una lista de productos que contienen un string en su título o descripción
        public async Task<List<Producto>> BuscarProductosPorString(string inputBuscar)
        {
            List<Producto> listaProductos = await GetProductos();
            List<Producto> listaProductosEncontrados = new List<Producto>();

            foreach (Producto producto in listaProductos)
            {
                if (producto.Titulo.ToLower().Contains(inputBuscar.ToLower()) || producto.Descripcion.ToLower().Contains(inputBuscar.ToLower()))
                {
                    listaProductosEncontrados.Add(producto);
                }
            }
            return listaProductosEncontrados;
        }
        //Listar productos para una categoria determinada
        public async Task<List<Producto>> GetProductosByCategoriaId(int categoriaId)
        {
            List<Producto> productos = new List<Producto>();
            List<ProductoCategoria> productoCategorias = await _context.ProductoCategoria.Where(x => x.CategoriaId == categoriaId).ToListAsync();

            foreach (ProductoCategoria productoCategoria in productoCategorias)
            {
                int idProducto = productoCategoria.ProductoId;
                Producto nuevoProducto = _context.Producto.FirstOrDefault(x => x.Id == idProducto);
                productos.Add(nuevoProducto);
            }
            return productos;
        }
    }
}
