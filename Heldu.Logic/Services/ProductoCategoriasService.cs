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
    public class ProductoCategoriasService : IProductoCategoriasService
    {
        private readonly ApplicationDbContext _context;
        public ProductoCategoriasService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProductoCategoria>> GetProductosCategorias()
        {
            return await _context.ProductoCategoria.Include(p => p.Categoria).Include(p => p.Producto).ToListAsync();
        }
        public List<ProductoCategoria> GetProductosCategoriasSync()
        {
            return _context.ProductoCategoria.Include(p => p.Categoria).Include(p => p.Producto).ToList();
        }
        public async Task<ProductoCategoria> GetProductoCategoriaById(int? id)
        {
            return await _context.ProductoCategoria
                                .Include(p => p.Categoria)
                                .Include(p => p.Producto)
                                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task CreateProductoCategoriaPost(ProductoCategoria productoCategoria)
        {
            _context.Add(productoCategoria);
            await _context.SaveChangesAsync();
        }
        public async Task<ProductoCategoria> EditProductoCategoriaGet(int? id)
        {
            return await _context.ProductoCategoria.FindAsync(id);
        }
        public async Task EditProductoCategoriaPost(ProductoCategoria productoCategoria)
        {
            _context.Update(productoCategoria);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProductoCategoriaPost (int id)
        {
            _context.ProductoCategoria.Remove(await _context.ProductoCategoria.FindAsync(id));
            await _context.SaveChangesAsync();
        }
        public bool ExistProductoCategoria (int id)
        {
            return _context.ProductoCategoria.Any(e => e.Id == id);
        }
    }
}
