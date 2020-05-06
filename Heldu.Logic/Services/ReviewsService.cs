using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Heldu.Logic.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly ApplicationDbContext _context;
        public ReviewsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Review>> GetReviews()
        {
            return await _context.Review.ToListAsync();
        }
        public async Task<Review> DetailsReview(int? id)
        {
            return await _context.Review.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task CreateReview(Review review)
        {
            _context.Add(review);
            await _context.SaveChangesAsync();
        }
        public async Task<Review> EditReviewGet(int? id)
        {
            return await _context.Review.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task EditReviewPost(Review review)
        {
            _context.Update(review);
            await _context.SaveChangesAsync();
        }
        public async Task<Review> DeleteReviewGet(int? id)
        {
            return await _context.Review.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task DeleteReviewPost(int id)
        {
            Review review = await _context.Review.FindAsync(id);
            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
        }
        public bool ExistReview(int id)
        {
            return _context.Review.Any(e => e.Id == id);
        }

        // Obtenemos Lista de reviews enviándole un Producto ID
        public async Task<List<Review>> ObtenerReviewsByProductoId(int Id)
        {
            return await _context.Review.Where(x => x.ProductoId == Id).ToListAsync();
        }
        // Función simple que recibe la lista de Reviews del modelo Producto
        public int CantidadComentariosByReviewList(List<Review> reviews)
        {
            return reviews.Count();
        }
        // Acompaña a la anterior para sacar la valoración media
        public async Task<int> ObtenerValoracionMediaByProductoId(int id)
        {
            List<Review> reviews = await ObtenerReviewsByProductoId(id);
            int mediaValoracion;
            int totalComentarios = CantidadComentariosByReviewList(reviews);
            if (totalComentarios == 0)
            {
                mediaValoracion = 0;
            }
            else
            {
                int totalValoracion = 0;
                foreach (Review review in reviews)
                {
                    totalValoracion += review.Valoracion;
                }
                mediaValoracion = totalValoracion / totalComentarios;
            }
            return mediaValoracion;
        }
        public async Task CreateReviewByUsuarioAndProducto(Usuario usuario, Producto producto, string ComentarioUsuario, string valoracionUsuario)
        {
            Review review = new Review
            {
                UsuarioId = usuario.IdentityUserId,
                Usuario = usuario,
                ProductoId = producto.Id,
                Producto = producto,
                Comentario = ComentarioUsuario,
                Fecha = DateTime.Now,
                Valoracion = Convert.ToInt32(valoracionUsuario),
            };
            _context.Add(review);
            await _context.SaveChangesAsync();
        }
    }
}
