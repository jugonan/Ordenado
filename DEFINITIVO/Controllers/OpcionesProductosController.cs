using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DEFINITIVO.Data;
using DEFINITIVO.Models;

namespace DEFINITIVO.Controllers
{
    public class OpcionesProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OpcionesProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OpcionesProductos
        public async Task<IActionResult> Index()
        {
            return View(await _context.OpcionProducto.ToListAsync());
        }

        // GET: OpcionesProductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opcionProducto = await _context.OpcionProducto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (opcionProducto == null)
            {
                return NotFound();
            }

            return View(opcionProducto);
        }

        // GET: OpcionesProductos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OpcionesProductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descripcion,PrecioInicial,PrecioFinal,Descuento")] OpcionProducto opcionProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opcionProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(opcionProducto);
        }

        // GET: OpcionesProductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opcionProducto = await _context.OpcionProducto.FindAsync(id);
            if (opcionProducto == null)
            {
                return NotFound();
            }
            return View(opcionProducto);
        }

        // POST: OpcionesProductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,PrecioInicial,PrecioFinal,Descuento")] OpcionProducto opcionProducto)
        {
            if (id != opcionProducto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opcionProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpcionProductoExists(opcionProducto.Id))
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
            return View(opcionProducto);
        }

        // GET: OpcionesProductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opcionProducto = await _context.OpcionProducto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (opcionProducto == null)
            {
                return NotFound();
            }

            return View(opcionProducto);
        }

        // POST: OpcionesProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opcionProducto = await _context.OpcionProducto.FindAsync(id);
            _context.OpcionProducto.Remove(opcionProducto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpcionProductoExists(int id)
        {
            return _context.OpcionProducto.Any(e => e.Id == id);
        }
    }
}
