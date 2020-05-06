using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
        public async Task<Mercado> DetailsMercado(int? id)
        {
            return await _context.Mercado
                        .Include(m => m.Producto)
                        .Include(m => m.Usuario)
                        .FirstOrDefaultAsync(m => m.Id == id);
        }

    }
}
