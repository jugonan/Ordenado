using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Heldu.Logic.Services
{
    public class TransaccionesService : ITransaccionesService
    {
        private readonly ApplicationDbContext _context;
        public TransaccionesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Transaccion>> GetTransaccion()
        {
            return await _context.Transaccion.Include(t => t.Producto)
                                .ThenInclude(u => u.Id)
                                .Include(t => t.Usuario)
                                .ThenInclude(u => u.IdentityUser)
                                .Include(t => t.Vendedor)
                                .ThenInclude(u => u.IdentityUser)
                                .ToListAsync();
        }
        public async Task<Transaccion> DetailsTransaccion(int? id)
        {
            return await _context.Transaccion.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task CreateTransaccion(Transaccion transaccion)
        {
            _context.Add(transaccion);
            await _context.SaveChangesAsync();
        }
        public async Task<Transaccion> EditTransaccionGet(int? id)
        {
            return await _context.Transaccion.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task EditTransaccionPost(Transaccion transaccion)
        {
            _context.Update(transaccion);
            await _context.SaveChangesAsync();
        }
        public async Task<Transaccion> DeleteTransaccionGet(int? id)
        {
            return await _context.Transaccion.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task DeleteTransaccionPost(int id)
        {
            _context.Transaccion.Remove(await _context.Transaccion.FindAsync(id));
            await _context.SaveChangesAsync();
        }
        public bool ExistTransaccion(int id)
        {
            return _context.Transaccion.Any(e => e.Id == id);
        }
        public async Task CreateTransaccionWithUsuarioAndProductoVendedor(Usuario usuario, ProductoVendedor productoVendedor)
        {
            Transaccion transaccion = new Transaccion()
            {
                UsuarioId = usuario.Id,
                ProductoId = productoVendedor.ProductoId,
                VendedorId = productoVendedor.VendedorId,
                Unidades = 0,
                FechaTransaccion = DateTime.Today,
            };
            await CreateTransaccion(transaccion);
        }
    }
}
