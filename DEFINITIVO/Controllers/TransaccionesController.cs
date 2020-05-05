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
    public class TransaccionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransaccionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transacciones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transaccion.Include(t => t.Producto)
                                                                .ThenInclude(u => u.Id)
                                                            .Include(t => t.Usuario)
                                                                .ThenInclude(u => u.IdentityUser)
                                                            .Include(t => t.Vendedor)
                                                                .ThenInclude(u => u.IdentityUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transacciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transaccion
                .Include(t => t.Producto)
                .Include(t => t.Usuario)
                .Include(t => t.Vendedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return View(transaccion);
        }

        // GET: Transacciones/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Titulo");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NombreUsuario");
            ViewData["VendedorId"] = new SelectList(_context.Vendedor, "Id", "NombreDeEmpresa");
            return View();
        }

        // POST: Transacciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,VendedorId,ProductoId,FechaTransaccion,Unidades")] Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaccion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index2", "Productos");
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Titulo", transaccion.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NombreUsuario", transaccion.UsuarioId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedor, "Id", "NombreDeEmpresa", transaccion.VendedorId);
            return View(transaccion);
        }

        // GET: Transacciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transaccion.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion", transaccion.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Apellido", transaccion.UsuarioId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedor, "Id", "Ciudad", transaccion.VendedorId);
            return View(transaccion);
        }

        // POST: Transacciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,VendedorId,ProductoId,FechaTransaccion,Unidades")] Transaccion transaccion)
        {
            if (id != transaccion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransaccionExists(transaccion.Id))
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
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion", transaccion.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Apellido", transaccion.UsuarioId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedor, "Id", "Ciudad", transaccion.VendedorId);
            return View(transaccion);
        }

        // GET: Transacciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transaccion
                .Include(t => t.Producto)
                .Include(t => t.Usuario)
                .Include(t => t.Vendedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return View(transaccion);
        }

        // POST: Transacciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaccion = await _context.Transaccion.FindAsync(id);
            _context.Transaccion.Remove(transaccion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransaccionExists(int id)
        {
            return _context.Transaccion.Any(e => e.Id == id);
        }
    }
}
