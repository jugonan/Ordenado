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

namespace DEFINITIVO.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly IVendedoresService _vendedoresService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUbicacionesVendedoresService _ubicacionesVendedoresService;
        private readonly IHelperService _helperService;


        public VendedoresController(IVendedoresService vendedoresService, UserManager<IdentityUser> userManager, IUbicacionesVendedoresService ubicacionesVendedoresService, IHelperService helperService)
        {
            _vendedoresService = vendedoresService;
            _userManager = userManager;
            _ubicacionesVendedoresService = ubicacionesVendedoresService;
            _helperService = helperService;
        }

        // GET: Vendedores
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
            //ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Nombre");
            return View();

        }

        // POST: Vendedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VendedorUbicacionVM vendedorUbicacionVM)
        {
            if (ModelState.IsValid)
            {
                Vendedor vendedor = new Vendedor()
                {
                    NombreDeEmpresa = vendedorUbicacionVM.NombreDeEmpresa,
                    NumeroTiendas = vendedorUbicacionVM.NumeroTiendas,
                    Paginaweb = vendedorUbicacionVM.Paginaweb,
                    Telefono = vendedorUbicacionVM.Telefono,
                    DescripcionEmpresa = vendedorUbicacionVM.DescripcionEmpresa,
                    IdentityUserId = vendedorUbicacionVM.IdentityUserId
                };
                await _vendedoresService.CreateVendedor(vendedor);
                UbicacionVendedor ubicacionVendedor = new UbicacionVendedor()
                {
                    Pais = "España",
                    CCAA = vendedorUbicacionVM.CCAA,
                    Provincia = "Pendiente",
                    Poblacion = vendedorUbicacionVM.Poblacion,
                    CP = vendedorUbicacionVM.CP,
                    Calle = vendedorUbicacionVM.Calle,
                    Numero = vendedorUbicacionVM.Numero,
                    Letra = vendedorUbicacionVM.Letra,
                    VendedorId = vendedor.Id,
                };
                await _ubicacionesVendedoresService.CreateUbicacionVendedor(ubicacionVendedor);
                return RedirectToAction("Inscrito", "Vendedores");
            }
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
            Vendedor vendedor = await _vendedoresService.EditVendedorGet(id);
            if (vendedor == null)
            {
                return NotFound();
            }
            //ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", vendedor.IdentityUserId);
            return View(vendedor);
        }

        // POST: Vendedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreDeEmpresa,Direccion,Ciudad,CodigoPostal,PaginaWeb,NumeroTiendas,Telefono,IdentityUserId")] Vendedor vendedor)
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

        // GET: Vendedores/Delete/5
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

        // POST: Vendedores/Delete/5
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

        public async Task<IActionResult> Miperfil()
        {
            string vendedorId = _userManager.GetUserId(User);
            Vendedor vendedor = await _vendedoresService.ObtenerVendedorDesdedIdentity(vendedorId);
            UbicacionVendedor ubicacionVendedor = await _ubicacionesVendedoresService.GetUbicacionVendedorById(vendedor.Id);
            ViewData["VendedorUbicacionVM"] = _ubicacionesVendedoresService.CrearVendedorUbicacionVM(vendedor, ubicacionVendedor);
            return View(vendedor);
        }

        public async Task<IActionResult> Estadisticas()
        {
            string vendedorId = _userManager.GetUserId(User);
            Vendedor vendedor = await _vendedoresService.MisproductosVendedor(vendedorId);
            // Obtengo una lista de VM en la que cada objeto tiene el ID de producto y cantidad de visitas
            ViewData["VisitasPorProducto"] = await _vendedoresService.GetProductosVistosDelVendedor(vendedor);
            return View(vendedor);
        }

        public async Task<IActionResult> Opiniones()
        {
            string vendedorId = _userManager.GetUserId(User);
            Vendedor vendedor = await _vendedoresService.MisproductosVendedor(vendedorId);
            ViewData["ReviewsDelVendedor"] = await _helperService.ObtenerReviewsVendedor(vendedor);
            ViewData["MediaReviews"] = await _helperService.ObtenerMediaReviewsParaVendedor(vendedor);
            return View(vendedor);
        }

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
        public IActionResult Inscrito()
        {
            return View();
        }

        public IActionResult ProbandoAPI()
        {
            return View();
        }

    }
}
