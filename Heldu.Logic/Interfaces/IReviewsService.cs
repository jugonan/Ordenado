using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Heldu.Entities.Models;

namespace Heldu.Logic.Interfaces
{
    public interface IReviewsService
    {
        public Task<List<Review>> GetReviews();
        public Task<Review> DetailsReview(int? id);
        public Task CreateReview(Review review);
        public Task<Review> EditReviewGet(int? id);
        public Task EditReviewPost(Review review);
        public Task<Review> DeleteReviewGet(int? id);
        public Task DeleteReviewPost(int id);
        public bool ExistReview(int id);
        public Task<List<Review>> ObtenerReviewsByProductoId(int Id);
        public int CantidadComentariosByReviewList(List<Review> reviews);
        public Task<int> ObtenerValoracionMediaByProductoId(int id);
        public Task CreateReviewByUsuarioAndProducto(Usuario usuario, Producto producto, string ComentarioUsuario, string valoracionUsuario);
    }
}
