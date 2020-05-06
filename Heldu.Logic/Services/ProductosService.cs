﻿using Heldu.Database.Data;
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
        public async Task<Producto> DetailsProducto(int? id)
        {
            return await _context.Producto
                                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task CreateProductoPost(Producto producto)
        {
            _context.Add(producto);
            await _context.SaveChangesAsync();
        }
        public async Task<Producto> EditProductoGet (int? id)
        {

        }
        public async Task EditProductoPost(Producto producto)
        {

        }
        public async Task<Producto> DeleteProductoGet (int? id)
        {

        }
        public async Task DeleteProductoPost (int id)
        {

        }
        public bool ExistProducto(int id)
        {
            return _context.Producto.Any(e => e.Id == id);
        }
        public async Task<Producto> GetProductoById(int? id)
        {
            return await _context.Producto.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddCantidadVisitasProductoById(int? id)
        {
            Producto productoActual = await GetProductoById(id);
            productoActual.CantidadVisitas++;
            _context.Update(productoActual);
            await _context.SaveChangesAsync();
        }
    }
}
