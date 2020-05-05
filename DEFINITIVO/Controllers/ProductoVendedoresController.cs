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
    public class ProductoVendedoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoVendedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductoVendedores
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductoVendedor.Include(p => p.Producto).Include(p => p.Vendedor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductoVendedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoVendedor = await _context.ProductoVendedor
                .Include(p => p.Producto)
                .Include(p => p.Vendedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productoVendedor == null)
            {
                return NotFound();
            }

            return View(productoVendedor);
        }

        // GET: ProductoVendedores/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Titulo");
            ViewData["VendedorId"] = new SelectList(_context.Set<Vendedor>(), "Id", "NombreDeEmpresa");
            return View();
        }

        // POST: ProductoVendedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductoId,VendedorId")] ProductoVendedor productoVendedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productoVendedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion", productoVendedor.ProductoId);
            ViewData["VendedorId"] = new SelectList(_context.Set<Vendedor>(), "Id", "NombreDeEmpresa", productoVendedor.VendedorId);
            return View(productoVendedor);
        }

        // GET: ProductoVendedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoVendedor = await _context.ProductoVendedor.FindAsync(id);
            if (productoVendedor == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion", productoVendedor.ProductoId);
            ViewData["VendedorId"] = new SelectList(_context.Set<Vendedor>(), "Id", "NombreDeEmpresa", productoVendedor.VendedorId);
            return View(productoVendedor);
        }

        // POST: ProductoVendedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductoId,VendedorId")] ProductoVendedor productoVendedor)
        {
            if (id != productoVendedor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productoVendedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoVendedorExists(productoVendedor.Id))
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
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion", productoVendedor.ProductoId);
            ViewData["VendedorId"] = new SelectList(_context.Set<Vendedor>(), "Id", "NombreDeEmpresa", productoVendedor.VendedorId);
            return View(productoVendedor);
        }

        // GET: ProductoVendedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoVendedor = await _context.ProductoVendedor
                .Include(p => p.Producto)
                .Include(p => p.Vendedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productoVendedor == null)
            {
                return NotFound();
            }

            return View(productoVendedor);
        }

        // POST: ProductoVendedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productoVendedor = await _context.ProductoVendedor.FindAsync(id);
            _context.ProductoVendedor.Remove(productoVendedor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoVendedorExists(int id)
        {
            return _context.ProductoVendedor.Any(e => e.Id == id);
        }
    }
}
