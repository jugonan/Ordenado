using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Heldu.Entities.Models;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Microsoft.AspNetCore.Http;
using Heldu.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Heldu.Logic.ViewModels;

namespace DEFINITIVO.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuariosService _usuariosService;
        private readonly IUbicacionesService _ubicacionesService;
        private readonly UserManager<IdentityUser> _userManager;


        public UsuariosController(IUsuariosService usuariosService, UserManager<IdentityUser> userManager, IUbicacionesService ubicacionesService)
        {
            _usuariosService = usuariosService;
            _userManager = userManager;
            _ubicacionesService = ubicacionesService;
        }
        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _usuariosService.GetUsuario());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Usuario usuario = await _usuariosService.GetUsuarioById(id);
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
        public async Task<IActionResult> Create(UsuarioUbicacionVM usuarioUbicacionVM, List<IFormFile> FotoUsuario)
        {
            if (ModelState.IsValid)
            {
                Ubicacion ubicacion = new Ubicacion()
                {
                    Pais = usuarioUbicacionVM.Pais,
                    CCAA = usuarioUbicacionVM.CCAA,
                    Provincia = usuarioUbicacionVM.Provincia,
                    Poblacion = usuarioUbicacionVM.Poblacion,
                    CP = usuarioUbicacionVM.CP,
                    Calle = usuarioUbicacionVM.Calle,
                    Numero = usuarioUbicacionVM.Numero,
                    Letra = usuarioUbicacionVM.Letra
                };
                Usuario usuario = new Usuario()
                {
                    Nombre = usuarioUbicacionVM.Nombre,
                    Apellido = usuarioUbicacionVM.Apellido,
                    NombreUsuario = usuarioUbicacionVM.NombreUsuario,
                    Darde = usuarioUbicacionVM.Darde,
                    FechaNacimiento = usuarioUbicacionVM.FechaNacimiento,
                    IdentityUserId = usuarioUbicacionVM.IdentityUserId
                };
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
                await _usuariosService.CreateUsuario(usuario);
                await _ubicacionesService.CreateUbicacion(ubicacion);
            }
            return RedirectToAction("Create", "GustoUsuarios");
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Usuario usuario = await _usuariosService.EditUsuarioGet(id);
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
                    await _usuariosService.EditUsuarioPost(usuario);
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
            Usuario usuario = await _usuariosService.GetUsuarioById(id);
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
            await _usuariosService.DeleteUsuarioPost(id);
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _usuariosService.ExistUsuario(id);
        }

        public async Task<IActionResult> Miperfil()
        {
            string userManagerId = _userManager.GetUserId(User);
            Usuario usuario = await _usuariosService.ObtenerUsuarioDesdedIdentity(userManagerId);
            return View(usuario);
        }

        public IActionResult Carrito()
        {
            return View();
        }

        public async Task<IActionResult> Micuenta()
        {
            string userManagerId = _userManager.GetUserId(User);
            Usuario usuario = await _usuariosService.ObtenerUsuarioDesdedIdentity(userManagerId);
            return View(usuario);
        }

        public async Task<IActionResult> Historico()
        {
            string userManagerId = _userManager.GetUserId(User);
            Usuario usuario = await _usuariosService.ObtenerUsuarioDesdedIdentity(userManagerId);
            //ViewData["ProductosVistos"] = await _context.MTransaccion
            //                                                        .Include(x => x.Producto)
            //                                                        .Include(x => x.Vendedor)
            //                                                        .Where(x => x.Unidades == 0).ToListAsync();
            return View(usuario);
        }


        public async Task<IActionResult> Rewards()
        {
            string userManagerId = _userManager.GetUserId(User);
            Usuario usuario = await _usuariosService.ObtenerUsuarioDesdedIdentity(userManagerId);
            return View(usuario);
        }

        public async Task<IActionResult> Miscursos()
        {
            string userManagerId = _userManager.GetUserId(User);
            Usuario usuario = await _usuariosService.MisCursosUsuario(userManagerId);
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
    }
}
