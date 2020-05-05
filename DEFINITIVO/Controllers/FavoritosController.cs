using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Heldu.Database.Data;
using Heldu.Entities.Models;

namespace DEFINITIVO.Controllers
{
    public class FavoritosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavoritosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Favoritos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Favorito.Include(f => f.Producto).Include(f => f.Usuario).Include(f => f.Vendedor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Favoritos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favorito = await _context.Favorito
                .Include(f => f.Producto)
                .Include(f => f.Usuario)
                .Include(f => f.Vendedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favorito == null)
            {
                return NotFound();
            }

            return View(favorito);
        }

        // GET: Favoritos/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Apellido");
            ViewData["VendedorId"] = new SelectList(_context.Vendedor, "Id", "Ciudad");
            return View();
        }

        // POST: Favoritos/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,VendedorId,ProductoId,FechaMeGusta")] Favorito favorito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favorito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion", favorito.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Apellido", favorito.UsuarioId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedor, "Id", "Ciudad", favorito.VendedorId);
            return View(favorito);
        }

        // GET: Favoritos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favorito = await _context.Favorito.FindAsync(id);
            if (favorito == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion", favorito.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Apellido", favorito.UsuarioId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedor, "Id", "Ciudad", favorito.VendedorId);
            return View(favorito);
        }

        // POST: Favoritos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,VendedorId,ProductoId,FechaMeGusta")] Favorito favorito)
        {
            if (id != favorito.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favorito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoritoExists(favorito.Id))
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
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion", favorito.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Apellido", favorito.UsuarioId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedor, "Id", "Ciudad", favorito.VendedorId);
            return View(favorito);
        }

        // GET: Favoritos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favorito = await _context.Favorito
                .Include(f => f.Producto)
                .Include(f => f.Usuario)
                .Include(f => f.Vendedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favorito == null)
            {
                return NotFound();
            }

            return View(favorito);
        }

        // POST: Favoritos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favorito = await _context.Favorito.FindAsync(id);
            _context.Favorito.Remove(favorito);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavoritoExists(int id)
        {
            return _context.Favorito.Any(e => e.Id == id);
        }
    }
}
