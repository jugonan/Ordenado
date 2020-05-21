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
    public class CondicionesController : Controller
    {
        private readonly ICondicionesService _condicionesService;

        public CondicionesController(ICondicionesService condicionesService)
        {
            _condicionesService = condicionesService;
        }

        // GET: Condiciones
        public async Task<IActionResult> Index()
        {
            return View(await _condicionesService.GetCondiciones());
        }

        // GET: Condiciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Condicion condicion = await _condicionesService.GetCondicionById(id);
            if (condicion == null)
            {
                return NotFound();
            }

            return View(condicion);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Reserva,Horario,Entrega,Recogida,Otros")] Condicion condicion)
        {
            if (ModelState.IsValid)
            {
                await _condicionesService.CreateCondicion(condicion);
                return RedirectToAction(nameof(Index));
            }
            return View(condicion);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Condicion condicion = await _condicionesService.EditCondicionGet(id);
            if (condicion == null)
            {
                return NotFound();
            }
            return View(condicion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Reserva,Horario,Entrega,Recogida")] Condicion condicion)
        {
            if (id != condicion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _condicionesService.EditCondicionPost(condicion);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CondicionExists(condicion.Id))
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
            return View(condicion);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Condicion condicion = await _condicionesService.EditCondicionGet(id);
            if (condicion == null)
            {
                return NotFound();
            }

            return View(condicion);
        }

        // POST: Condiciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _condicionesService.DeleteCondicionPost(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CondicionExists(int id)
        {
            return _condicionesService.ExistCondicion(id);
        }
    }
}
