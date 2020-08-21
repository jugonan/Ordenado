using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Heldu.Logic.ViewModels;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

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
            OpcionProductoCreateVM opcionProductoCreateVM = new OpcionProductoCreateVM()
            {
                Producto = await _productosService.GetProductoById(productoId)
            };
            Producto producto = opcionProductoCreateVM.Producto;

            var json = producto.Condiciones;
            var parseado = JsonDocument.Parse(json);
            var algo = parseado.RootElement;
            List<string>  listaCondiciones = JsonConvert.DeserializeObject<List<string>>(json);
            ViewData["Condiciones"] = listaCondiciones;
            return View(opcionProductoCreateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OpcionProductoCreateVM opcionProductoCreateVM)
        {
            if (ModelState.IsValid)
            {
                if (opcionProductoCreateVM.OpcionProducto1 != null)
                {
                    OpcionProducto opcionProducto =  _opcionesProductosService.crearDesdeJson(opcionProductoCreateVM.OpcionProducto1, opcionProductoCreateVM.Producto.Id);
                    await _opcionesProductosService.CreateOpcionProductoPost(opcionProducto);
                }
                if (opcionProductoCreateVM.OpcionProducto2 != null)
                {
                    OpcionProducto opcionProducto = _opcionesProductosService.crearDesdeJson(opcionProductoCreateVM.OpcionProducto2, opcionProductoCreateVM.Producto.Id);
                    await _opcionesProductosService.CreateOpcionProductoPost(opcionProducto);

                }
                if (opcionProductoCreateVM.OpcionProducto3 != null && opcionProductoCreateVM.OpcionProducto3 != "undefined")
                {
                    OpcionProducto opcionProducto = _opcionesProductosService.crearDesdeJson(opcionProductoCreateVM.OpcionProducto3, opcionProductoCreateVM.Producto.Id);
                    await _opcionesProductosService.CreateOpcionProductoPost(opcionProducto);
                }
                return RedirectToAction("Detalles", "Productos", new { id = opcionProductoCreateVM.Producto.Id });
            }
            return View(opcionProductoCreateVM);
        }

        [Authorize(Roles ="admin")]
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
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Descripcion,PrecioInicial,PrecioFinal,Descuento,StockInicial,CantidadVendida")] OpcionProducto opcionProducto)
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OpcionProducto opcionProducto = await _opcionesProductosService.GetOpcionProductoByHisId(id);

            if (opcionProducto == null)
            {
                return NotFound();
            }

            return View(opcionProducto);
        }

        // POST: OpcionesProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
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
