using Heldu.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heldu.Logic.Interfaces
{
    public interface ICategoriasService
    {
        public Task<List<Categoria>> GetCategorias();
        public Task<Categoria> DetailsCategoria(int? id);
        public Task CreateCategoria(Categoria categoria);
        public Task<Categoria> EditCategoriaGet(int? id);
        public Task EditCategoriaPost(Categoria categoria);
        public Task<Categoria> DeleteCategoriaGet(int? id);
        public Task DeleteCategoriaPost(int? id);
        public bool ExistCategoria(int id);
        public Task<List<Categoria>> ListaCategorias();
    }
}
 