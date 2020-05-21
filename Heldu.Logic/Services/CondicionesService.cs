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
    public class CondicionesService : ICondicionesService
    {
        private readonly ApplicationDbContext _context;
        public CondicionesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Condicion>> GetCondiciones()
        {
            return await _context.Condicion.ToListAsync();
        }
        // Obtiene las condiciones por productoId
        public async Task<Condicion> GetCondicionById(int? id)
        {
            return await _context.Condicion.FirstOrDefaultAsync(m => m.ProductoId == id);
        }
        public async Task CreateCondicion(Condicion condicion)
        {
            _context.Add(condicion);
            await _context.SaveChangesAsync();
        }
        public async Task<Condicion> EditCondicionGet(int? id)
        {
            return await _context.Condicion.FindAsync(id);
        }
        public async Task EditCondicionPost(Condicion condicion)
        {
            _context.Update(condicion);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCondicionPost(int? id)
        {
            Condicion condicion = await _context.Condicion.FindAsync(id);
            _context.Condicion.Remove(condicion);
            await _context.SaveChangesAsync();
        }
        public bool ExistCondicion(int id)
        {
            return _context.Condicion.Any(e => e.Id == id);
        }

    }
}
