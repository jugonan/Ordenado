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
using Microsoft.AspNetCore.Authorization;

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
            
            var horario = algo.GetProperty("Horario");
            int horarioLength = horario.GetArrayLength();
            string[] horarioArray = new string[horarioLength];
            for (int i = 0; i < horarioLength; i++)
            {
                horarioArray[i] = horario[i].GetString();
            }

            var reservas = algo.GetProperty("Reservas");
            int reservasLength = reservas.GetArrayLength();
            string[] reservasArray = new string[reservasLength];
            for (int i = 0; i < reservasLength; i++)
            {
                reservasArray[i] = reservas[i].GetString();
            }

            var entregas = algo.GetProperty("Entregas");
            int entregasLength = entregas.GetArrayLength();
            string[] entregasArray = new string[entregasLength];
            for (int i = 0; i < entregasLength; i++)
            {
                entregasArray[i] = entregas[i].GetString();
            }

            var recogidas = algo.GetProperty("Recogidas");
            int recogidasLength = recogidas.GetArrayLength();
            string[] recogidasArray = new string[recogidasLength];
            for (int i = 0; i < recogidasLength; i++)
            {
                recogidasArray[i] = recogidas[i].GetString();
            }

            var otros = algo.GetProperty("Otros");
            int otrosLength = otros.GetArrayLength();
            string[] otrosArray = new string[otrosLength];
            for (int i = 0; i < otrosLength; i++)
            {
                otrosArray[i] = otros[i].GetString();
            }

            ViewData["Horario"] = horarioArray;
            ViewData["Reservas"] = reservasArray;
            ViewData["Entregas"] = entregasArray;
            ViewData["Recogidas"] = recogidasArray;
            ViewData["Otros"] = otrosArray;
            //ProductoCategoriaCondicionesVM productoCategoriaCondicionesVM = await _opcionesProductosService.CrearVM(productoId);
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
                return RedirectToAction(nameof(Index));
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
