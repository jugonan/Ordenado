using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Microsoft.AspNetCore.Identity;
using DEFINITIVO.Services;
using Heldu.Logic.Interfaces;

namespace DEFINITIVO.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewsService _reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            _reviewsService = reviewsService;
        }
        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            return View(await _reviewsService.GetReviews());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Review review = await _reviewsService.DetailsReview(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create(string UsuarioId, int ProductoId, string ComentarioUsuario)
        {
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,ProductoId,Comentario")] Review review)
        {
            if (ModelState.IsValid)
            {
                await _reviewsService.CreateReview(review);
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Review review = await _reviewsService.EditReviewGet(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,ProductoId,Valoracion,Comentario,Fecha")] Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _reviewsService.EditReviewPost(review);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Review review = await _reviewsService.DeleteReviewGet(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reviewsService.DeleteReviewPost(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _reviewsService.ExistReview(id);
        }
    }
}
