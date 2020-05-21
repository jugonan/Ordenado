using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Heldu.Database.Data;
using Heldu.Entities.Models;

namespace DEFINITIVO.Controllers
{
    public class CondicionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CondicionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Condiciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Condicion.ToListAsync());
        }

        // GET: Condiciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condicion = await _context.Condicion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (condicion == null)
            {
                return NotFound();
            }

            return View(condicion);
        }

        // GET: Condiciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Condiciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Reserva,Horario,Entrega,Recogida")] Condicion condicion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(condicion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(condicion);
        }

        // GET: Condiciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condicion = await _context.Condicion.FindAsync(id);
            if (condicion == null)
            {
                return NotFound();
            }
            return View(condicion);
        }

        // POST: Condiciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _context.Update(condicion);
                    await _context.SaveChangesAsync();
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

        // GET: Condiciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condicion = await _context.Condicion
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var condicion = await _context.Condicion.FindAsync(id);
            _context.Condicion.Remove(condicion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CondicionExists(int id)
        {
            return _context.Condicion.Any(e => e.Id == id);
        }
    }
}
