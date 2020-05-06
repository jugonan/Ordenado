using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Heldu.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Heldu.Logic.Interfaces;

namespace DEFINITIVO.Controllers
{
    public class MercadosController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMercadosService _mercadosService;
        private readonly IUsuariosService _usuariosService;
        private readonly IProductosService _productosService;

        public MercadosController(SignInManager<IdentityUser> signInManager,
                                  UserManager<IdentityUser> userManager,
                                  IMercadosService mercadosService,
                                  IUsuariosService usuariosService,
                                  IProductosService productosService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mercadosService = mercadosService;
            _usuariosService = usuariosService;
            _productosService = productosService;
        }

        // GET: Mercados
        public async Task<IActionResult> Index()
        {
            return View(await _mercadosService.GetMercados());
        }

        // GET: Mercados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mercado mercado = await _mercadosService.DetailsMercado(id);
            if (mercado == null)
            {
                return NotFound();
            }

            return View(mercado);
        }

        // GET: Mercados/Create
        public async Task<IActionResult> Create(int? ProductoId)
        {
            ViewData["ProductoId"] = new SelectList(_context.Producto.Where(x => x.Id == ProductoId), "Id", "Titulo");
            ViewData["UsuarioId"] = new SelectList(await _usuariosService.GetUsuariosListByActiveIdentityUser(_userManager.GetUserId(User)), "Id", "NombreUsuario");
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
            Usuario usuario = await _usuariosService.GetUsuarioByActiveIdentityUser(_userManager.GetUserId(User));
            mercado.UsuarioId = usuario.Id;
            mercado.Codigo = Convert.ToString(codigo);
            if (ModelState.IsValid)
            {
                await _mercadosService.CreateMercadoPost(mercado);
                return RedirectToAction("Inscrito", "Usuarios");
            }
            ViewData["ProductoId"] = new SelectList(_context.Set<Producto>(), "Id", "Descripcion", mercado.ProductoId);
            ViewData["UsuarioId"] = new SelectList(await _usuariosService.GetUsuariosListByActiveIdentityUser(_userManager.GetUserId(User)), "Id", "NombreUsuario", mercado.UsuarioId);
            return RedirectToAction("Inscrito", "Usuarios");
        }

        // GET: Mercados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mercado mercado = await _mercadosService.EditMercadoGet(id);
            if (mercado == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Set<Producto>(), "Id", "Descripcion", mercado.ProductoId);
            ViewData["UsuarioId"] = new SelectList(await _usuariosService.GetUsuariosListByActiveIdentityUser(_userManager.GetUserId(User)), "Id", "Apellido", mercado.UsuarioId);
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
                    await _mercadosService.EditMercadoPost(mercado);
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
            ViewData["UsuarioId"] = new SelectList(await _usuariosService.GetUsuariosListByActiveIdentityUser(_userManager.GetUserId(User)), "Id", "Apellido", mercado.UsuarioId);
            return View(mercado);
        }

        // GET: Mercados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mercado mercado = await _mercadosService.DeleteMercadoGet(id);
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
            await _mercadosService.DeleteMercadoPost(id);
            return RedirectToAction("Desinscrito", "Usuarios");
        }

        private bool MercadoExists(int id)
        {
            return _mercadosService.ExistMercado(id);
        }
    }
}
