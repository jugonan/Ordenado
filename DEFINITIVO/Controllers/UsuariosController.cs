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
        private readonly IUbicacionesUsuariosService _ubicacionesUsuarioService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IManejoProductosService _manejoProductosService;


        public UsuariosController(IUsuariosService usuariosService, UserManager<IdentityUser> userManager, IUbicacionesUsuariosService ubicacionesUsuariosService, IManejoProductosService manejoProductosService)
        {
            _usuariosService = usuariosService;
            _userManager = userManager;
            _ubicacionesUsuarioService = ubicacionesUsuariosService;
            _manejoProductosService = manejoProductosService;
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

                UbicacionUsuario ubicacionUsuario = new UbicacionUsuario()
                {
                    Pais = usuarioUbicacionVM.Pais,
                    CCAA = usuarioUbicacionVM.CCAA,
                    Provincia = usuarioUbicacionVM.Provincia,
                    Poblacion = usuarioUbicacionVM.Poblacion,
                    CP = usuarioUbicacionVM.CP,
                    Calle = usuarioUbicacionVM.Calle,
                    Numero = usuarioUbicacionVM.Numero,
                    Letra = usuarioUbicacionVM.Letra,
                    UsuarioId = usuario.Id
                };
                await _ubicacionesUsuarioService.CreateUbicacionUsuario(ubicacionUsuario);
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

        // Ningún enlace debería dirigir a esta acción porque devuelve una vista parcial.
        // Redirigir a 'Mi Cuenta'
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
            ViewData["ProductosVistos"] = await _manejoProductosService.GetProductosVistosPorUsuario(usuario.Id);
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

        public async Task<IActionResult> GestionarUsuarios()
        {
            return View(await _usuariosService.GestionarUsuarios());
        }
    }
}
