using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Heldu.Logic.Services
{
    public class UbicacionesService : IUbicacionesUsuariosService
    {
        private readonly ApplicationDbContext _context;
        public UbicacionesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<UbicacionUsuario>> GetUbicacionUsuario()
        {
            return await _context.UbicacionUsuario.ToListAsync();
        }
        public async Task<UbicacionUsuario> GetUbicacionUsuarioById(int? id)
        {
            return await _context.UbicacionUsuario.FirstOrDefaultAsync(x => x.UsuarioId == id);
        }
        public async Task CreateUbicacionUsuario(UbicacionUsuario ubicacion)
        {
            _context.Add(ubicacion);
            await _context.SaveChangesAsync();
        }
        public async Task<UbicacionUsuario> EditUbicacionUsuarioGet(int? id)
        {
            return await _context.UbicacionUsuario.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task EditUbicacionUsuarioPost(UbicacionUsuario ubicacion)
        {
            _context.Update(ubicacion);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUbicacionUsuarioPost(int id)
        {
            _context.UbicacionUsuario.Remove(await _context.UbicacionUsuario.FindAsync(id));
            await _context.SaveChangesAsync();
        }
        public bool ExistUbicacionUsuario(int id)
        {
            return _context.UbicacionUsuario.Any(e => e.Id == id);
        }
    }
}
