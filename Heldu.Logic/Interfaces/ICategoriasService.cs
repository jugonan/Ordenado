using Heldu.Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heldu.Logic.Interfaces
{
    public interface ICategoriasService
    {
        public Task<List<Categoria>> GetCategorias();
        public List<Categoria> GetCategoriasSync();
        public Task<Categoria> GetCategoriaById(int? id);
        public Task CreateCategoria(Categoria categoria);
        public Task<Categoria> EditCategoriaGet(int? id);
        public Task EditCategoriaPost(Categoria categoria);
        public Task DeleteCategoriaPost(int? id);
        public bool ExistCategoria(int id);
        public Task<List<Categoria>> ListaCategorias();
        public Task<List<SelectListItem>> GetSelectListCategorias();
        public Task<Categoria> GetCategoriaByProductoId(int id);

    }
}
 