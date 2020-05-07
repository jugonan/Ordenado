using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heldu.Logic.Services
{
    public class MercadosService : IMercadosService
    {
        private readonly ApplicationDbContext _context;
        public MercadosService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Mercado>> GetMercados()
        {
            return await _context.Mercado.Include(m => m.Producto).Include(m => m.Usuario).ToListAsync();
        }
        public async Task<Mercado> GetMercadoById(int? id)
        {
            return await _context.Mercado
                        .Include(m => m.Producto)
                        .Include(m => m.Usuario)
                        .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task CreateMercadoPost(Mercado mercado)
        {
            _context.Add(mercado);
            await _context.SaveChangesAsync();
        }
        public async Task<Mercado> EditMercadoGet(int? id)
        {
            return await _context.Mercado.FindAsync(id);
        }
        public async Task EditMercadoPost(Mercado mercado)
        {
            _context.Update(mercado);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteMercadoPost(int id)
        { 
            _context.Mercado.Remove(await _context.Mercado.FindAsync(id));
            await _context.SaveChangesAsync();
        }
        public bool ExistMercado (int id)
        {
            return _context.Mercado.Any(e => e.Id == id);
        }
    }
}
