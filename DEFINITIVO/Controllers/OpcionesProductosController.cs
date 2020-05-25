using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Heldu.Logic.ViewModels;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Text.Json;
using Echovoice.JSON;

namespace DEFINITIVO.Controllers
{
    public class OpcionesProductosController : Controller
    {
        private readonly IOpcionesProductosService _opcionesProductosService;
        private readonly IVendedoresService _vendedoresService;
        private readonly IProductosService _productosService;

        public OpcionesProductosController(IOpcionesProductosService opcionesProductosService, IVendedoresService vendedoresService, IProductosService productosService)
        {
            _opcionesProductosService = opcionesProductosService;
            _vendedoresService = vendedoresService;
            _productosService = productosService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _opcionesProductosService.GetOpcionesProductos());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opcionProducto = await _opcionesProductosService.GetOpcionProductoById(id);
            if (opcionProducto == null)
            {
                return NotFound();
            }

            return View(opcionProducto);
        }

        public async Task<IActionResult> Create(int productoId)
        {
            ViewData["Vendedor"] = await _vendedoresService.ObtenerVendedorDesdeProducto(productoId);
            Producto producto = await _productosService.GetProductoById(productoId);

            var json = producto.Condiciones;
            var parseado = JsonDocument.Parse(json);
            var algo = parseado.RootElement;
            var horario = algo.GetProperty("Horario");
            var reservas = algo.GetProperty("Reservas");
            var entregas = algo.GetProperty("Entregas");
            var recogidas = algo.GetProperty("Recogidas");
            var otros = algo.GetProperty("Otros");
            ViewData["Horario"] = horario;
            ViewData["Reservas"] = reservas;
            ViewData["Entregas"] = entregas;
            ViewData["Recogidas"] = recogidas;
            ViewData["Otros"] = otros;
            //ProductoCategoriaCondicionesVM productoCategoriaCondicionesVM = await _opcionesProductosService.CrearVM(productoId);
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descripcion,PrecioInicial,PrecioFinal,Descuento")] OpcionProducto opcionProducto)
        {
            if (ModelState.IsValid)
            {
                await _opcionesProductosService.CreateOpcionProductoPost(opcionProducto);
                return RedirectToAction(nameof(Index));
            }
            return View(opcionProducto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OpcionProducto opcionProducto = await _opcionesProductosService.EditOpcionProductoGet(id);
            if (opcionProducto == null)
            {
                return NotFound();
            }
            return View(opcionProducto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,PrecioInicial,PrecioFinal,Descuento")] OpcionProducto opcionProducto)
        {
            if (id != opcionProducto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _opcionesProductosService.EditOpcionProductoPost(opcionProducto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpcionProductoExists(opcionProducto.Id))
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
            return View(opcionProducto);
        }

        // GET: OpcionesProductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OpcionProducto opcionProducto = await _opcionesProductosService.GetOpcionProductoById(id);
            if (opcionProducto == null)
            {
                return NotFound();
            }

            return View(opcionProducto);
        }

        // POST: OpcionesProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _opcionesProductosService.DeleteOpcionProductoPost(id);
            return RedirectToAction(nameof(Index));
        }

        private bool OpcionProductoExists(int id)
        {
            return _opcionesProductosService.ExistOpcionProducto(id);
        }
    }
}
