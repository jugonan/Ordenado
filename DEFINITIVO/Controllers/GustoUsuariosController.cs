using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DEFINITIVO.Controllers
{
    //[Authorize(Roles = "admin")]
    public class GustoUsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public GustoUsuariosController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: GustoUsuarios
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GustoUsuario.Include(g => g.Categoria).Include(g => g.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GustoUsuarios/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gustoUsuario = await _context.GustoUsuario
                .Include(g => g.Categoria)
                .Include(g => g.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gustoUsuario == null)
            {
                return NotFound();
            }

            return View(gustoUsuario);
        }

        // GET: GustoUsuarios/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Nombre");
            //ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "NombreUsuario");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario.Where(x => x.IdentityUserId == _userManager.GetUserId(User)), "Id", "NombreUsuario");
            return View();
        }

        // POST: GustoUsuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GustoUsuario gustoUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gustoUsuario);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Inscrito", "Usuarios");
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Nombre", gustoUsuario.CategoriaId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "NombreUsuario", gustoUsuario.UsuarioId);
            return RedirectToAction("Index2", "Productos");
        }

        // GET: GustoUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gustoUsuario = await _context.GustoUsuario.FindAsync(id);
            if (gustoUsuario == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Id", gustoUsuario.CategoriaId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Apellido", gustoUsuario.UsuarioId);
            return View(gustoUsuario);
        }

        // POST: GustoUsuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,CategoriaId")] GustoUsuario gustoUsuario)
        {
            if (id != gustoUsuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gustoUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GustoUsuarioExists(gustoUsuario.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Id", gustoUsuario.CategoriaId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Apellido", gustoUsuario.UsuarioId);
            return View(gustoUsuario);
        }

        // GET: GustoUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gustoUsuario = await _context.GustoUsuario
                .Include(g => g.Categoria)
                .Include(g => g.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gustoUsuario == null)
            {
                return NotFound();
            }

            return View(gustoUsuario);
        }

        // POST: GustoUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gustoUsuario = await _context.GustoUsuario.FindAsync(id);
            _context.GustoUsuario.Remove(gustoUsuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GustoUsuarioExists(int id)
        {
            return _context.GustoUsuario.Any(e => e.Id == id);
        }
    }
}
