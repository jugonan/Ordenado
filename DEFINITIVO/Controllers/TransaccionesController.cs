using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;

namespace DEFINITIVO.Controllers
{
    public class TransaccionesController : Controller
    {
        private readonly ITransaccionesService _transaccionesService;

        public TransaccionesController(ITransaccionesService transaccionesService)
        {
            _transaccionesService = transaccionesService;
        }

        // GET: Transacciones
        public async Task<IActionResult> Index()
        {
            return View(await _transaccionesService.GetTransaccion());
        }

        // GET: Transacciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Transaccion transaccion = await _transaccionesService.DetailsTransaccion(id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return View(transaccion);
        }

        // GET: Transacciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transacciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,VendedorId,ProductoId,FechaTransaccion,Unidades")] Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                await _transaccionesService.CreateTransaccion(transaccion);
                return RedirectToAction("Index2", "Productos");
            }
            return View(transaccion);
        }

        // GET: Transacciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Transaccion transaccion = await _transaccionesService.EditTransaccionGet(id);
            if (transaccion == null)
            {
                return NotFound();
            }
            return View(transaccion);
        }

        // POST: Transacciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,VendedorId,ProductoId,FechaTransaccion,Unidades")] Transaccion transaccion)
        {
            if (id != transaccion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _transaccionesService.EditTransaccionPost(transaccion);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransaccionExists(transaccion.Id))
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
            return View(transaccion);
        }

        // GET: Transacciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Transaccion transaccion = await _transaccionesService.DetailsTransaccion(id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return View(transaccion);
        }

        // POST: Transacciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _transaccionesService.DeleteTransaccionPost(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TransaccionExists(int id)
        {
            return _transaccionesService.ExistTransaccion(id);
        }
    }
}
