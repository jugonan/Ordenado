using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
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

        public async Task<Categoria> GetCategoriaById(int? id)
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

        //Devuelve el listado de categorias existentes (para usarlo en el navbar del Layout)
        public async Task<List<Categoria>> ListaCategorias()
        {
            List<Categoria> listaCategorias = await _context.Categoria.ToListAsync();

            return listaCategorias;
        }

        //Devuelve un Select List con las categorias, incluyendo un "Todas" al principio. Se usa en el "Buscar" del Layout.cshtml
        public async Task<List<SelectListItem>> GetSelectListCategorias()
        {
            List<SelectListItem> selectListCategorias = new List<SelectListItem>();
            selectListCategorias.Add(new SelectListItem { Text = "Todas las categorias", Value = "-1", Selected = true });

            List<Categoria> categorias = await GetCategorias();
            foreach (Categoria item in categorias)
            {
                selectListCategorias.Add(new SelectListItem { Text = item.Nombre, Value = item.Id.ToString()});
            }

            return selectListCategorias;
        }
    }
}

