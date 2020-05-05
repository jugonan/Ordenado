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
    public class ProductoCategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoCategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductoCategorias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductoCategoria.Include(p => p.Categoria).Include(p => p.Producto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductoCategorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoCategoria = await _context.ProductoCategoria
                .Include(p => p.Categoria)
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productoCategoria == null)
            {
                return NotFound();
            }

            return View(productoCategoria);
        }

        // GET: ProductoCategorias/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Nombre");
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion");
            return View();
        }

        // POST: ProductoCategorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductoId,CategoriaId")] ProductoCategoria productoCategoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productoCategoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Id", productoCategoria.CategoriaId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion", productoCategoria.ProductoId);
            return View(productoCategoria);
        }

        // GET: ProductoCategorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoCategoria = await _context.ProductoCategoria.FindAsync(id);
            if (productoCategoria == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Id", productoCategoria.CategoriaId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion", productoCategoria.ProductoId);
            return View(productoCategoria);
        }

        // POST: ProductoCategorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductoId,CategoriaId")] ProductoCategoria productoCategoria)
        {
            if (id != productoCategoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productoCategoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoCategoriaExists(productoCategoria.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Id", productoCategoria.CategoriaId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion", productoCategoria.ProductoId);
            return View(productoCategoria);
        }

        // GET: ProductoCategorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoCategoria = await _context.ProductoCategoria
                .Include(p => p.Categoria)
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productoCategoria == null)
            {
                return NotFound();
            }

            return View(productoCategoria);
        }

        // POST: ProductoCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productoCategoria = await _context.ProductoCategoria.FindAsync(id);
            _context.ProductoCategoria.Remove(productoCategoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoCategoriaExists(int id)
        {
            return _context.ProductoCategoria.Any(e => e.Id == id);
        }
    }
}
