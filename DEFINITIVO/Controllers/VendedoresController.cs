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
    public class VendedoresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public VendedoresController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Vendedores
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vendedor
                                                        .Include(v => v.IdentityUser)
                                                        .Include(v => v.ProductoVendedor)
                                                            .ThenInclude(a => a.Producto);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Miperfil()
        {
            Vendedor vendedor = await _context.Vendedor.FirstOrDefaultAsync(x => x.IdentityUserId == _userManager.GetUserId(User));
            return View(vendedor);
        }

        public async Task<IActionResult> Estadisticas()
        {

            Vendedor vendedor = await _context.Vendedor.Include(v=>v.ProductoVendedor).ThenInclude(a=>a.Producto).FirstOrDefaultAsync(x => x.IdentityUserId == _userManager.GetUserId(User));
            return View(vendedor);
        }

        public async Task<IActionResult> Opiniones()
        {
            Vendedor vendedor = await _context.Vendedor.FirstOrDefaultAsync(x => x.IdentityUserId == _userManager.GetUserId(User));
            return View(vendedor);
        }

        public async Task<IActionResult> Misproductos()
        {
            Vendedor vendedor = await _context.Vendedor
                                                        .Include(v => v.ProductoVendedor)
                                                            .ThenInclude(p => p.Producto)
                                                        .FirstOrDefaultAsync(x => x.IdentityUserId == _userManager.GetUserId(User));
            return View(vendedor);
        }

        public IActionResult Modificado()
        {
            return View();
        }

        public IActionResult Eliminado()
        {
            return View();
        }

        // GET: Vendedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendedor = await _context.Vendedor
                .Include(v => v.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendedor == null)
            {
                return NotFound();
            }

            return View(vendedor);
        }

        // GET: Vendedores/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Nombre");
            return View();

        }

        // POST: Vendedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreDeEmpresa,NumeroTiendas,Direccion,Ciudad,CodigoPostal,Paginaweb,Telefono,DescripcionEmpresa,IdentityUserId")] Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendedor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Inscrito", "Vendedores");
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Nombre");
            return RedirectToAction("Inscrito", "Vendedores");

            //NOTA: Tenemos que añadir una página error a la que enviar al vendedor si se da algún problema en la creación del mismo

        }

        // GET: Vendedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendedor = await _context.Vendedor.FindAsync(id);
            if (vendedor == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", vendedor.IdentityUserId);
            return View(vendedor);
        }

        // POST: Vendedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreDeEmpresa,TipoDeEmpresa,NumeroRegistro,IdentityUserId")] Vendedor vendedor)
        {
            if (id != vendedor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendedorExists(vendedor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Home");
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", vendedor.IdentityUserId);
            return View(vendedor);
        }

        // GET: Vendedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendedor = await _context.Vendedor
                .Include(v => v.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendedor == null)
            {
                return NotFound();
            }

            return View(vendedor);
        }

        // POST: Vendedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendedor = await _context.Vendedor.FindAsync(id);
            _context.Vendedor.Remove(vendedor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendedorExists(int id)
        {
            return _context.Vendedor.Any(e => e.Id == id);
        }

        // Acción casi inútil que sólo devuelve la vista
        public IActionResult Inscrito()
        {
            return View();
        }

    }
}
