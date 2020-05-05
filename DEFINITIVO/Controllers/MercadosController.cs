using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DEFINITIVO.Data;
using DEFINITIVO.Models;
using Microsoft.AspNetCore.Identity;

namespace DEFINITIVO.Controllers
{
    public class MercadosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public MercadosController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Mercados
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Mercado.Include(m => m.Producto).Include(m => m.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Mercados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mercado = await _context.Mercado
                .Include(m => m.Producto)
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mercado == null)
            {
                return NotFound();
            }

            return View(mercado);
        }

        // GET: Mercados/Create
        public IActionResult Create(int? ProductoId)
        {
            //ViewData["ProductoId"] = new SelectList(_context.Set<Producto>(), "Id", "Descripcion");
            ViewData["ProductoId"] = new SelectList(_context.Producto.Where(x => x.Id == ProductoId), "Id", "Titulo");
            //ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Apellido");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario.Where(x => x.IdentityUserId == _userManager.GetUserId(User)), "Id", "NombreUsuario");
            return View();
        }

        // POST: Mercados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mercado mercado)
        {
            Random random = new Random();
            string codigo = Convert.ToString(random.Next(1001, 9999));
            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.IdentityUserId == _userManager.GetUserId(User));
            mercado.UsuarioId = usuario.Id;
            mercado.Codigo = Convert.ToString(codigo);
            if (ModelState.IsValid)
            {
                _context.Add(mercado);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Inscrito", "Usuarios");
            }
            ViewData["ProductoId"] = new SelectList(_context.Set<Producto>(), "Id", "Descripcion", mercado.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "NombreUsuario", mercado.UsuarioId);
            return RedirectToAction("Inscrito", "Usuarios");
        }

        // GET: Mercados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mercado = await _context.Mercado.FindAsync(id);
            if (mercado == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Set<Producto>(), "Id", "Descripcion", mercado.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Apellido", mercado.UsuarioId);
            return View(mercado);
        }

        // POST: Mercados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,UsuarioId,ProductoId")] Mercado mercado)
        {
            if (id != mercado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mercado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MercadoExists(mercado.Id))
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
            ViewData["ProductoId"] = new SelectList(_context.Set<Producto>(), "Id", "Descripcion", mercado.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Apellido", mercado.UsuarioId);
            return View(mercado);
        }

        // GET: Mercados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mercado = await _context.Mercado
                .Include(m => m.Producto)
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mercado == null)
            {
                return NotFound();
            }
            return View(mercado);
        }

        // POST: Mercados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mercado = await _context.Mercado.FindAsync(id);
            _context.Mercado.Remove(mercado);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Desinscrito", "Usuarios");
        }

        private bool MercadoExists(int id)
        {
            return _context.Mercado.Any(e => e.Id == id);
        }
    }
}
