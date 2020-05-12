using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Heldu.Logic.Services
{
    public class UbicacionesVendedoresService : IUbicacionesVendedoresService
    {
        private readonly ApplicationDbContext _context;
        public UbicacionesVendedoresService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<UbicacionVendedor>> GetUbicacionVendedor()
        {
            return await _context.UbicacionVendedor.ToListAsync();
        }
        public async Task<UbicacionVendedor> GetUbicacionVendedorById(int? id)
        {
            return await _context.UbicacionVendedor.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task CreateUbicacionVendedor(UbicacionVendedor ubicacion)
        {
            _context.Add(ubicacion);
            await _context.SaveChangesAsync();
        }
        public async Task<UbicacionVendedor> EditUbicacionVendedorGet(int? id)
        {
            return await _context.UbicacionVendedor.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task EditUbicacionVendedorPost(UbicacionVendedor ubicacion)
        {
            _context.Update(ubicacion);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUbicacionVendedorPost(int id)
        {
            _context.UbicacionVendedor.Remove(await _context.UbicacionVendedor.FindAsync(id));
            await _context.SaveChangesAsync();
        }
        public bool ExistUbicacionVendedor(int id)
        {
            return _context.UbicacionVendedor.Any(e => e.Id == id);
        }
    }
}
