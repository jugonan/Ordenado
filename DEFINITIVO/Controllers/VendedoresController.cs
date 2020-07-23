using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Heldu.Logic.Interfaces;
using Heldu.Logic.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;

namespace DEFINITIVO.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly IVendedoresService _vendedoresService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUbicacionesVendedoresService _ubicacionesVendedoresService;
        private readonly IHelperService _helperService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _memoryCache;

        public VendedoresController(IVendedoresService vendedoresService,
                                    UserManager<IdentityUser> userManager,
                                    IUbicacionesVendedoresService ubicacionesVendedoresService,
                                    IHelperService helperService,
                                    SignInManager<IdentityUser> signInManager,
                                    IMemoryCache memoryCache)
        {
            _vendedoresService = vendedoresService;
            _userManager = userManager;
            _ubicacionesVendedoresService = ubicacionesVendedoresService;
            _helperService = helperService;
            _signInManager = signInManager;
            _memoryCache = memoryCache;
        }

        // GET: Vendedores
        [OutputCache(Duration = 0)]
        public async Task<IActionResult> Index()
        {
            return View(await _vendedoresService.GetVendedor());
        }

        // GET: Vendedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vendedor vendedor = await _vendedoresService.GetVendedorById(id);
            if (vendedor == null)
            {
                return NotFound();
            }
            return View(vendedor);
        }

        // GET: Vendedores/Create
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VendedorUbicacionVM vendedorUbicacionVM)
        {
            if (ModelState.IsValid)
            {
                Vendedor vendedor = vendedorUbicacionVM.vendedor;
                await _vendedoresService.CreateVendedor(vendedor);

                UbicacionVendedor ubicacionVendedor = vendedorUbicacionVM.ubicacion;
                await _ubicacionesVendedoresService.CreateUbicacionVendedor(ubicacionVendedor);
                return RedirectToAction("Inscrito", "Vendedores");
            }
            return RedirectToAction("Inscrito", "Vendedores");

            //NOTA: Tenemos que añadir una página error a la que enviar al vendedor si se da algún problema en la creación del mismo
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Vendedor vendedor = await _vendedoresService.EditVendedorGet(id);
            if (vendedor == null)
            {
                return NotFound();
            }
            //ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", vendedor.IdentityUserId);
            return View(vendedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreDeEmpresa,CIF,IBAN,Fee,DescripcionEmpresa,PaginaWeb,Telefono,IdentityUserId")] Vendedor vendedor)
        {
            if (id != vendedor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _vendedoresService.EditVendedorPost(vendedor);
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
                return RedirectToAction("Index", "Home");
            }
            return View(vendedor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Vendedor vendedor = await _vendedoresService.GetVendedorById(id);
            if (vendedor == null)
            {
                return NotFound();
            }

            return View(vendedor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vendedoresService.DeleteVendedorPost(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VendedorExists(int id)
        {
            return _vendedoresService.ExistVendedor(id);
        }

        [OutputCache(Duration = 0)]
        public async Task<IActionResult> Miperfil()
        {
            string vendedorId = _userManager.GetUserId(User);
            Vendedor nuevoVendedor = await _vendedoresService.ObtenerVendedorDesdedIdentity(vendedorId);
            UbicacionVendedor ubicacionVendedor = await _ubicacionesVendedoresService.GetUbicacionVendedorById(nuevoVendedor.Id);
            VendedorUbicacionVM vendedorUbicacionVM = new VendedorUbicacionVM()
            {
                vendedor = nuevoVendedor,
                ubicacion = ubicacionVendedor
            };
            ViewData["VendedorUbicacionVM"] = vendedorUbicacionVM;
            return View(nuevoVendedor);
        }

        public async Task<IActionResult> Estadisticas()
        {
            string vendedorId = _userManager.GetUserId(User);
            Vendedor vendedor = await _vendedoresService.MisproductosVendedor(vendedorId);
            ViewData["VisitasPorProducto"] = await _vendedoresService.GetProductosVistosDelVendedor(vendedor);
            return View(vendedor);
        }

        [OutputCache(Duration = 0)]
        public async Task<IActionResult> Opiniones()
        {
            string vendedorId = _userManager.GetUserId(User);
            Vendedor vendedor = await _vendedoresService.MisproductosVendedor(vendedorId);
            ViewData["ReviewsDelVendedor"] = await _helperService.ObtenerReviewsVendedor(vendedor);
            ViewData["MediaReviews"] = await _helperService.ObtenerMediaReviewsParaVendedor(vendedor);
            return View(vendedor);
        }

        [OutputCache(Duration = 0)]
        public async Task<IActionResult> Misproductos()
        {
            string vendedorId = _userManager.GetUserId(User);
            Vendedor vendedor = await _vendedoresService.MisproductosVendedor(vendedorId);
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

        // Acción casi inútil que sólo devuelve la vista
        [OutputCache(Duration = 0)]
        public async Task<IActionResult> Inscrito()
        {
            await _signInManager.SignOutAsync();
            //_memoryCache.Remove("ProductosForIndex2");
            //_memoryCache.Remove("Categorias");
            return View();
        }

        public IActionResult ProbandoAPI()
        {
            return View();
        }

        // TESTEO: Acciones creadas para devolver al usuario  a las páginas ya hechas
        public async Task<IActionResult> Estadisticas2()
        {
            string vendedorId = _userManager.GetUserId(User);
            Vendedor vendedor = await _vendedoresService.MisproductosVendedor(vendedorId);
            return View(vendedor);
        }
        public async Task<IActionResult> Misproductos2()
        {
            string vendedorId = _userManager.GetUserId(User);
            Vendedor vendedor = await _vendedoresService.MisproductosVendedor(vendedorId);
            return View(vendedor);
        }
        public async Task<IActionResult> Opiniones2()
        {
            string vendedorId = _userManager.GetUserId(User);
            Vendedor vendedor = await _vendedoresService.ObtenerVendedorDesdedIdentity(vendedorId);
            return View(vendedor);
        }

        [Authorize(Roles ="admin")]
        [HttpGet]
        public async Task<IActionResult> CambiarFee(int id)
        {
            Vendedor vendedor = await _vendedoresService.GetVendedorById(id);
            return View(vendedor);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarFee(int id, int fee)
        {
            Vendedor vendedor = await _vendedoresService.GetVendedorById(id);
            vendedor.Fee = fee;
            try
            {
                await _vendedoresService.EditVendedorPost(vendedor);
                ViewData["VendedorId"] = id;
                return RedirectToAction("FeeModificado", "Vendedores");
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
        }
    }
}
