using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heldu.Logic.Services
{
    public class CategoriasService : ICategoriasService
    {
        private readonly ApplicationDbContext _context;
        public CategoriasService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Categoria>> GetCategorias()
        {
            return await _context.Categoria.ToListAsync();
        }

        public async Task<Categoria> DetailsCategoria(int? id)
        {
            return await _context.Categoria.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task CreateCategoria(Categoria categoria)
        {
            _context.Add(categoria);
            await _context.SaveChangesAsync();
        }
        public async Task<Categoria> EditCategoriaGet(int? id)
        {
            return await _context.Categoria.FindAsync(id);
        }
        public async Task EditCategoriaPost(Categoria categoria)
        {
            _context.Update(categoria);
            await _context.SaveChangesAsync();
        }
        public async Task<Categoria> DeleteCategoriaGet(int? id)
        {
            return await _context.Categoria.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task DeleteCategoriaPost(int? id)
        {
            Categoria categoria = await _context.Categoria.FindAsync(id);
            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();
        }
        public bool ExistCategoria (int id)
        {
            return _context.Categoria.Any(e => e.Id == id);
        }
    }
}

