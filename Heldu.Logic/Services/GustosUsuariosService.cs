using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heldu.Logic.Services
{
    public class GustosUsuariosService : IGustosUsuariosService
    {
        private readonly ApplicationDbContext _context;
        public GustosUsuariosService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<GustoUsuario>> GetGustosUsuarios()
        {
            return await _context.GustoUsuario.Include(g => g.Categoria).Include(g => g.Usuario).ToListAsync();

        }
        public async Task<GustoUsuario> DetailsGustoUsuario(int? id)
        {
            return await _context.GustoUsuario
                            .Include(g => g.Categoria)
                            .Include(g => g.Usuario)
                            .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task CreateGustoUsuarioPost(GustoUsuario gustoUsuario)
        {
            _context.Add(gustoUsuario);
            await _context.SaveChangesAsync();
        }
        public async Task<GustoUsuario> EditGustoUsuarioGet(int? id)
        {
            return await _context.GustoUsuario.FindAsync(id);

        }
        public async Task EditGustoUsuarioPost(GustoUsuario gustoUsuario)
        {
            _context.Update(gustoUsuario);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteGustoUsuarioPost(int id)
        {
            GustoUsuario gustoUsuario = await _context.GustoUsuario.FindAsync(id);
            _context.GustoUsuario.Remove(gustoUsuario);
            await _context.SaveChangesAsync();
        }
        public bool ExistGustoUsuario(int id)
        {
            return _context.GustoUsuario.Any(e => e.Id == id);
        }
    }
}
