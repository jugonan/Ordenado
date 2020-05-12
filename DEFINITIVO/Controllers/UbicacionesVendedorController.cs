using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;

namespace DEFINITIVO.Controllers
{
    public class UbicacionesVendedorController : Controller
    {
        private readonly IUbicacionesVendedoresService _ubicacionesService;

        public UbicacionesVendedorController(IUbicacionesVendedoresService ubicacionesService)
        {
            _ubicacionesService = ubicacionesService;
        }

        // GET: Ubicaciones
        public async Task<IActionResult> Index()
        {
            return View(await _ubicacionesService.GetUbicacionVendedor());
        }

        // GET: Ubicaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            UbicacionVendedor ubicacion = await _ubicacionesService.GetUbicacionVendedorById(id);
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
        public async Task<IActionResult> Create([Bind("Id,Pais,CCAA,Provincia,Poblacion,CP,Calle,Numero,Letra")] UbicacionVendedor ubicacion)
        {
            if (ModelState.IsValid)
            {
                await _ubicacionesService.CreateUbicacionVendedor(ubicacion);
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
            UbicacionVendedor ubicacion = await _ubicacionesService.EditUbicacionVendedorGet(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] UbicacionVendedor ubicacion)
        {
            if (id != ubicacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ubicacionesService.EditUbicacionVendedorPost(ubicacion);
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
            UbicacionVendedor ubicacion = await _ubicacionesService.GetUbicacionVendedorById(id);
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
            await _ubicacionesService.DeleteUbicacionVendedorPost(id);
            return RedirectToAction(nameof(Index));
        }

        private bool UbicacionExists(int id)
        {
            return _ubicacionesService.ExistUbicacionVendedor(id);
        }
    }
}
