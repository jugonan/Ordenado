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
using DEFINITIVO.ViewModel;
using DEFINITIVO.Services;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace DEFINITIVO.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly Manejo_Productos _myManejadorDeProductos;
        private readonly HelperService _helperService;
        private readonly Usuario _usuario;

        public UsuariosController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, Manejo_Productos myManejadorDeProductos, HelperService helperService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _myManejadorDeProductos = myManejadorDeProductos;
            _helperService = helperService;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Usuario
                                                       .Include(u => u.IdentityUser)
                                                       .Include(u => u.Mercados)
                                                            .ThenInclude(m => m.Producto)
                                                       .Include(u => u.Categorias)
                                                            .ThenInclude(c => c.Categoria);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Miperfil()
        {
            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.IdentityUserId == _userManager.GetUserId(User));
            return View(usuario);

        }

        public IActionResult Carrito()
        {
            return View();
        }

        public async Task<IActionResult> Micuenta()
        {
            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.IdentityUserId == _userManager.GetUserId(User));
            return View(usuario);
        }

        public async Task<IActionResult> Historico()
        {
            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.IdentityUserId == _userManager.GetUserId(User));
            ViewData["ProductosVistos"] = await _context.Transaccion
                                                                    .Include(x => x.Producto)
                                                                    .Include(x => x.Vendedor)
                                                                    .Where(x => x.Unidades == 0).ToListAsync();
            return View(usuario);
        }


        public async Task<IActionResult> Rewards()
        {
            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.IdentityUserId == _userManager.GetUserId(User));
            return View(usuario);
        }

        public async Task<IActionResult> Miscursos()
        {
            Usuario usuario = await _context.Usuario
                                                        .Include(v => v.Mercados)
                                                            .ThenInclude(p => p.Producto)
                                                        .FirstOrDefaultAsync(x => x.IdentityUserId == _userManager.GetUserId(User));
            return View(usuario);
        }

        public IActionResult Desinscrito()
        {
            return View();
        }

        // Acción casi inútil que sólo devuelve la vista
        public IActionResult Inscrito()
        {
            return View();
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .Include(u => u.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Usuario usuario, List<IFormFile> FotoUsuario)
        {
            foreach (var item in FotoUsuario)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        usuario.FotoUsuario = stream.ToArray();
                    }
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            //return View(usuario);
            return RedirectToAction("Create", "GustoUsuarios");
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,NombreUsuario,Darde,FechaNacimiento,IdentityUserId,FotoPerfil")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .Include(u => u.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
