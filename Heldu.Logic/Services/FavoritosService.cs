using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heldu.Logic.Services
{
    public class FavoritosService : IFavoritosService
    {
        private readonly ApplicationDbContext _context;
        public FavoritosService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Favorito>> GetFavoritos()
        {
            return await _context.Favorito.Include(f => f.Producto).Include(f => f.Usuario).Include(f => f.Vendedor).ToListAsync();
        }
        public async Task<Favorito> DetailsFavorito(int? id)
        {
            return await _context.Favorito
                        .Include(f => f.Producto)
                        .Include(f => f.Usuario)
                        .Include(f => f.Vendedor)
                        .FirstOrDefaultAsync(m => m.Id == id);
        }
        //public async CreateFavoritoGet()
        //{

        //}
        public async Task CreateFavoritoPost(Favorito favorito)
        {
            _context.Add(favorito);
            await _context.SaveChangesAsync();
        }
        public async Task<Favorito> EditFavoritoGet(int? id)
        {
            return await _context.Favorito.FindAsync(id);
        }
        public async Task EditFavoritoPost(Favorito favorito)
        {
            _context.Update(favorito);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteFavoritoPost(int id)
        {
            Favorito favorito = await _context.Favorito.FindAsync(id);
            _context.Favorito.Remove(favorito);
            await _context.SaveChangesAsync();
        }
        public bool ExistFavorito(int id)
        {
            return _context.Favorito.Any(e => e.Id == id);
        }
    }
}
