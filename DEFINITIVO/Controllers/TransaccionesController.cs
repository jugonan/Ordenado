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
    public class VisitasController : Controller
    {
        private readonly IVisitasService _VisitasService;

        public VisitasController(IVisitasService VisitasService)
        {
            _VisitasService = VisitasService;
        }

        // GET: Visitas
        public async Task<IActionResult> Index()
        {
            return View(await _VisitasService.GetVisitas());
        }

        // GET: Visitas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Visita Visita = await _VisitasService.GetVisitaById(id);
            if (Visita == null)
            {
                return NotFound();
            }

            return View(Visita);
        }

        // GET: Visitas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Visitas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,VendedorId,ProductoId,FechaVisita,Unidades")] Visita Visita)
        {
            if (ModelState.IsValid)
            {
                await _VisitasService.CreateVisita(Visita);
                return RedirectToAction("Index2", "Productos");
            }
            return View(Visita);
        }

        // GET: Visitas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Visita Visita = await _VisitasService.EditVisitaGet(id);
            if (Visita == null)
            {
                return NotFound();
            }
            return View(Visita);
        }

        // POST: Visitas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,VendedorId,ProductoId,FechaVisita,Unidades")] Visita Visita)
        {
            if (id != Visita.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _VisitasService.EditVisitaPost(Visita);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitaExists(Visita.Id))
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
            return View(Visita);
        }

        // GET: Visitas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Visita Visita = await _VisitasService.GetVisitaById(id);
            if (Visita == null)
            {
                return NotFound();
            }

            return View(Visita);
        }

        // POST: Visitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _VisitasService.DeleteVisitaPost(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VisitaExists(int id)
        {
            return _VisitasService.ExistVisita(id);
        }
    }
}
