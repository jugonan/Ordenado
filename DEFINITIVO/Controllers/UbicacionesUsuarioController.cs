using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;

namespace DEFINITIVO.Controllers
{
    public class UbicacionesUsuarioController : Controller
    {
        private readonly IUbicacionesUsuariosService _ubicacionesUsuariosService;

        public UbicacionesUsuarioController(IUbicacionesUsuariosService ubicacionesUsuariosService)
        {
            _ubicacionesUsuariosService = ubicacionesUsuariosService;
        }

        // GET: UbicacionesUsuario
        public async Task<IActionResult> Index()
        {
            return View(await _ubicacionesUsuariosService.GetUbicacionUsuario());
        }

        // GET: UbicacionesUsuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ubicacionUsuario = await _ubicacionesUsuariosService.GetUbicacionUsuarioById(id);
            if (ubicacionUsuario == null)
            {
                return NotFound();
            }

            return View(ubicacionUsuario);
        }

        // GET: UbicacionesUsuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UbicacionesUsuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Pais,CCAA,Provincia,Poblacion,CP,Calle,Numero,Letra,UsuarioId")] UbicacionUsuario ubicacionUsuario)
        {
            if (ModelState.IsValid)
            {
                await _ubicacionesUsuariosService.CreateUbicacionUsuario(ubicacionUsuario);
                return RedirectToAction(nameof(Index));
            }
            return View(ubicacionUsuario);
        }

        // GET: UbicacionesUsuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ubicacionUsuario = await _ubicacionesUsuariosService.GetUbicacionUsuarioById(id);
            if (ubicacionUsuario == null)
            {
                return NotFound();
            }
            return View(ubicacionUsuario);
        }

        // POST: UbicacionesUsuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Pais,CCAA,Provincia,Poblacion,CP,Calle,Numero,Letra,UsuarioId")] UbicacionUsuario ubicacionUsuario)
        {
            if (id != ubicacionUsuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ubicacionesUsuariosService.EditUbicacionUsuarioPost(ubicacionUsuario);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UbicacionUsuarioExists(ubicacionUsuario.Id))
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
            return View(ubicacionUsuario);
        }

        // GET: UbicacionesUsuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ubicacionUsuario = await _ubicacionesUsuariosService.GetUbicacionUsuarioById(id);
            if (ubicacionUsuario == null)
            {
                return NotFound();
            }

            return View(ubicacionUsuario);
        }

        // POST: UbicacionesUsuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ubicacionesUsuariosService.DeleteUbicacionUsuarioPost(id);
            return RedirectToAction(nameof(Index));
        }

        private bool UbicacionUsuarioExists(int id)
        {
            return _ubicacionesUsuariosService.ExistUbicacionUsuario(id);
        }
    }
}
