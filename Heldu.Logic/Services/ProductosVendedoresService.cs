using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Heldu.Logic.Services
{
    public class ProductosVendedoresService : IProductosVendedoresService
    {
        private readonly ApplicationDbContext _context;

        public ProductosVendedoresService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProductoVendedor>> GetProductoVendedor()
        {
            return await _context.ProductoVendedor.ToListAsync();
        }
        public async Task<ProductoVendedor> DetailsProductoVendedor(int? id)
        {
            return await _context.ProductoVendedor.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task CreateProductoVendedor(ProductoVendedor productoVendedor)
        {
            _context.Add(productoVendedor);
            await _context.SaveChangesAsync();
        }
        public async Task<ProductoVendedor> EditProductoVendedorGet(int? id)
        {
            return await _context.ProductoVendedor.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task EditProductoVendedorPost(ProductoVendedor productoVendedor)
        {
            _context.Update(productoVendedor);
            await _context.SaveChangesAsync();
        }
        public async Task<ProductoVendedor> DeleteProductoVendedorGet(int? id)
        {
            return await _context.ProductoVendedor.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task DeleteProductoVendedorPost(int id)
        {
            ProductoVendedor productoVendedor = await _context.ProductoVendedor.FindAsync(id);
            _context.ProductoVendedor.Remove(productoVendedor);
            await _context.SaveChangesAsync();
        }
        public bool ExistProductoVendedor(int id)
        {
            return _context.ProductoVendedor.Any(e => e.Id == id);
        }
    }
}

