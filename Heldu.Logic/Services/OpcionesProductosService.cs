using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Heldu.Logic.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heldu.Logic.Services
{
    public class OpcionesProductosService : IOpcionesProductosService
    {
        private readonly ApplicationDbContext _context;
        public OpcionesProductosService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<OpcionProducto>> GetOpcionesProductos()
        {
            return await _context.OpcionProducto.ToListAsync();
        }
        public async Task<OpcionProducto> GetOpcionProductoById(int? id)
        {
            return await  _context.OpcionProducto
                                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task CreateOpcionProductoPost(OpcionProducto opcionProducto)
        {
            _context.Add(opcionProducto);
            await _context.SaveChangesAsync();
        }
        public async Task<OpcionProducto> EditOpcionProductoGet(int? id)
        {
            return await _context.OpcionProducto.FindAsync(id);
        }
        public async Task EditOpcionProductoPost(OpcionProducto opcionProducto)
        {
            _context.Update(opcionProducto);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteOpcionProductoPost (int id)
        {
            _context.OpcionProducto.Remove(await _context.OpcionProducto.FindAsync(id));
            await _context.SaveChangesAsync();
        }
        public bool ExistOpcionProducto(int id)
        {
            return _context.OpcionProducto.Any(e => e.Id == id);
        }
        public async Task<ProductoCategoriaCondicionesVM> CrearVM(int productoId)
        {
            Producto producto = await _context.Producto.FirstOrDefaultAsync(x => x.Id == productoId);
            Condicion condicion = await _context.Condicion.FirstOrDefaultAsync(x => x.ProductoId == productoId);
            ProductoCategoriaCondicionesVM productoCategoriaCondicionesVM = new ProductoCategoriaCondicionesVM()
            {
                Producto = producto,
                Condicion = condicion
            };
            return productoCategoriaCondicionesVM;
        }
    }
}
