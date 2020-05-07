using Heldu.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heldu.Logic.Interfaces
{
    public interface IFavoritosService
    {
        public Task<List<Favorito>> GetFavoritos();
        public Task<Favorito> DetailsFavorito(int? id);
        public Task CreateFavoritoPost(Favorito favorito);
        public Task<Favorito> EditFavoritoGet(int? id);
        public Task EditFavoritoPost(Favorito favorito);
        public Task DeleteFavoritoPost(int id);
        public bool ExistFavorito(int id);
    }
}
