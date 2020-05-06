using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Heldu.Logic.Interfaces;

namespace DEFINITIVO.Controllers
{
    //[Authorize(Roles = "admin")]
    public class GustoUsuariosController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IGustosUsuariosService _gustosUsuariosService;
        private readonly ICategoriasService _categoriasService;
        private readonly IUsuariosService _usuariosService;

        public GustoUsuariosController(UserManager<IdentityUser> userManager,
                                       SignInManager<IdentityUser> signInManager,
                                       IGustosUsuariosService gustosUsuariosService,
                                       ICategoriasService categoriasService,
                                       IUsuariosService usuariosService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _gustosUsuariosService = gustosUsuariosService;
            _categoriasService = categoriasService;
            _usuariosService = usuariosService;
        }

        // GET: GustoUsuarios
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _gustosUsuariosService.GetGustosUsuarios());
        }

        // GET: GustoUsuarios/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GustoUsuario gustoUsuario = await _gustosUsuariosService.DetailsGustoUsuario(id);

            if (gustoUsuario == null)
            {
                return NotFound();
            }

            return View(gustoUsuario);
        }

        // GET: GustoUsuarios/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoriaId"] = new SelectList(await _categoriasService.GetCategorias(), "Id", "Nombre");
            ViewData["UsuarioId"] = new SelectList(await _usuariosService.GetUsuariosListByActiveIdentityUser(_userManager.GetUserId(User)), "Id", "NombreUsuario");
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
                await _gustosUsuariosService.CreateGustoUsuarioPost(gustoUsuario);
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Inscrito", "Usuarios");
            }
            ViewData["CategoriaId"] = new SelectList(await _categoriasService.GetCategorias(), "Id", "Nombre", gustoUsuario.CategoriaId);
            ViewData["UsuarioId"] = new SelectList(await _usuariosService.GetUsuariosListByActiveIdentityUser(_userManager.GetUserId(User)), "Id", "NombreUsuario", gustoUsuario.UsuarioId);
            return RedirectToAction("Index2", "Productos");
        }

        // GET: GustoUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GustoUsuario gustoUsuario = await _gustosUsuariosService.EditGustoUsuarioGet(id);
            if (gustoUsuario == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(await _categoriasService.GetCategorias(), "Id", "Nombre", gustoUsuario.CategoriaId);
            ViewData["UsuarioId"] = new SelectList(await _usuariosService.GetUsuariosListByActiveIdentityUser(_userManager.GetUserId(User)), "Id", "Apellido", gustoUsuario.UsuarioId);
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
                    await _gustosUsuariosService.EditGustoUsuarioPost(gustoUsuario);
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
            ViewData["CategoriaId"] = new SelectList(await _categoriasService.GetCategorias(), "Id", "Nombre", gustoUsuario.CategoriaId);
            ViewData["UsuarioId"] = new SelectList(await _usuariosService.GetUsuariosListByActiveIdentityUser(_userManager.GetUserId(User)), "Id", "Apellido", gustoUsuario.UsuarioId);
            return View(gustoUsuario);
        }

        // GET: GustoUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GustoUsuario gustoUsuario = await _gustosUsuariosService.DeleteGustoUsuarioGet(id);
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
            await _gustosUsuariosService.DeleteGustoUsuarioPost(id);
            return RedirectToAction(nameof(Index));
        }

        private bool GustoUsuarioExists(int id)
        {
            return _gustosUsuariosService.ExistGustoUsuario(id);
        }
    }
}
