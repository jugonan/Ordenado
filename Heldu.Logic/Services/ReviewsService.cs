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
        public async Task<List<Review>> ObtenerReviews(int Id)
        {
            List<Review> reviews = await _context.Review.Where(x => x.ProductoId == Id).ToListAsync();
            return reviews;
        }
        // Función simple que recibe la lista de Reviews del modelo Producto
        public async Task<int> ObtenerTotalComentarios(List<Review> reviews)
        {
            int totalComentarios = reviews.Count();
            return totalComentarios;
        }
        // Acompaña a la anterior para sacar la valoración media
        public async Task<int> ObtenerValoracionMedia(List<Review> reviews, int totalComentarios)
        {
            int mediaValoracion;
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

        Task<Review> IReviewsService.CreateReview(Review review)
        {
            throw new NotImplementedException();
        }

        Task<Review> IReviewsService.DeleteReviewPost(int id)
        {
            throw new NotImplementedException();
        }
    }
}
