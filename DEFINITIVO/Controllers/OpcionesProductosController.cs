using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;

namespace DEFINITIVO.Controllers
{
    public class OpcionesProductosController : Controller
    {
        private readonly IOpcionesProductosService _opcionesProductosService;

        public OpcionesProductosController(IOpcionesProductosService opcionesProductosService)
        {
            _opcionesProductosService = opcionesProductosService;
        }

        // GET: OpcionesProductos
        public async Task<IActionResult> Index()
        {
            return View(await _opcionesProductosService.GetOpcionesProductos());
        }

        // GET: OpcionesProductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opcionProducto = await _opcionesProductosService.DetailsOpcionProducto(id);
            if (opcionProducto == null)
            {
                return NotFound();
            }

            return View(opcionProducto);
        }

        // GET: OpcionesProductos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OpcionesProductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: OpcionesProductos/Edit/5
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

        // POST: OpcionesProductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

            OpcionProducto opcionProducto = await _opcionesProductosService.DetailsOpcionProducto(id);
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
