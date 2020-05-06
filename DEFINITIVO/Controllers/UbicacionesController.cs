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
    public class UbicacionesController : Controller
    {
        private readonly IUbicacionesService _ubicaciones;

        public UbicacionesController(IUbicacionesService ubicaciones)
        {
            _ubicaciones = ubicaciones;
        }

        // GET: Ubicaciones
        public async Task<IActionResult> Index()
        {
            return View(await _ubicaciones.GetUbiacaciones());
        }

        // GET: Ubicaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Ubicacion ubicacion = await _ubicaciones.DetailsUbicacion(id);
            if (ubicacion == null)
            {
                return NotFound();
            }
            return View(ubicacion);
        }

        // GET: Ubicaciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ubicaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Ubicacion ubicacion)
        {
            if (ModelState.IsValid)
            {
                await _ubicaciones.CreateUbicacion(ubicacion);
                return RedirectToAction(nameof(Index));
            }
            return View(ubicacion);
        }

        // GET: Ubicaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Ubicacion ubicacion = await _ubicaciones.EditUbicacionGet(id);
            if (ubicacion == null)
            {
                return NotFound();
            }
            return View(ubicacion);
        }

        // POST: Ubicaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Ubicacion ubicacion)
        {
            if (id != ubicacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ubicaciones.EditUbicacionPost(ubicacion);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UbicacionExists(ubicacion.Id))
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
            return View(ubicacion);
        }

        // GET: Ubicaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Ubicacion ubicacion = await _ubicaciones.DeleteUbicacionGet(id);
            if (ubicacion == null)
            {
                return NotFound();
            }

            return View(ubicacion);
        }

        // POST: Ubicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ubicaciones.DeleteUbicacionPost(id);
            return RedirectToAction(nameof(Index));
        }

        private bool UbicacionExists(int id)
        {
            return _ubicaciones.ExistUbicacion(id);
        }
    }
}
