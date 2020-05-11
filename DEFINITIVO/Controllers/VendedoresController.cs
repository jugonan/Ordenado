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

namespace DEFINITIVO.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly IVendedoresService _vendedoresService;
        private readonly UserManager<IdentityUser> _userManager;


        public VendedoresController(IVendedoresService vendedoresService, UserManager<IdentityUser> userManager)
        {
            _vendedoresService = vendedoresService;
            _userManager = userManager;
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
        public async Task<IActionResult> Create([Bind("Id,NombreDeEmpresa,NumeroTiendas,Direccion,Ciudad,CodigoPostal,Paginaweb,Telefono,DescripcionEmpresa,IdentityUserId")] Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {
                await _vendedoresService.CreateVendedor(vendedor);
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
            //ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", vendedor.IdentityUserId);
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
            return View(vendedor);
        }

        public async Task<IActionResult> Estadisticas()
        {
            string vendedorId = _userManager.GetUserId(User);
            Vendedor vendedor = await _vendedoresService.EstadisticasVendedor(vendedorId);
            return View(vendedor);
        }

        public async Task<IActionResult> Opiniones()
        {
            string vendedorId = _userManager.GetUserId(User);
            Vendedor vendedor = await _vendedoresService.ObtenerVendedorDesdedIdentity(vendedorId);
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
