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
    public class VisitasService : IVisitasService
    {
        private readonly ApplicationDbContext _context;
        public VisitasService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Visita>> GetVisitas()
        {
            return await _context.Visita.Include(t => t.Producto)
                                .ThenInclude(u => u.Id)
                                .Include(t => t.Usuario)
                                .ThenInclude(u => u.IdentityUser)
                                .Include(t => t.Vendedor)
                                .ThenInclude(u => u.IdentityUser)
                                .ToListAsync();
        }
        public async Task<Visita> GetVisitaById(int? id)
        {
            return await _context.Visita.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task CreateVisita(Visita Visita)
        {
            _context.Add(Visita);
            await _context.SaveChangesAsync();
        }
        public async Task<Visita> EditVisitaGet(int? id)
        {
            return await _context.Visita.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task EditVisitaPost(Visita Visita)
        {
            _context.Update(Visita);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteVisitaPost(int id)
        {
            _context.Visita.Remove(await _context.Visita.FindAsync(id));
            await _context.SaveChangesAsync();
        }
        public bool ExistVisita(int id)
        {
            return _context.Visita.Any(e => e.Id == id);
        }
        public async Task CreateVisitaWithUsuarioAndProductoVendedor(Usuario usuario, ProductoVendedor productoVendedor)
        {
            Visita Visita = new Visita()
            {
                UsuarioId = usuario.Id,
                ProductoId = productoVendedor.ProductoId,
                VendedorId = productoVendedor.VendedorId,
                Unidades = 0,
                FechaVisita = DateTime.Today,
            };
            await CreateVisita(Visita);
        }
    }
}
