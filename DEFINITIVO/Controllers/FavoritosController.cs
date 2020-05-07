using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;

namespace DEFINITIVO.Controllers
{
    public class FavoritosController : Controller
    {
        private readonly IFavoritosService _favoritosService;
        private readonly IProductosService _productosService;
        private readonly IUsuariosService _usuariosService;
        private readonly IVendedoresService _vendedoresService;

        public FavoritosController(IFavoritosService favoritosService,
                                   IProductosService productosService,
                                   IUsuariosService usuariosService,
                                   IVendedoresService vendedoresService)
        {
            _favoritosService = favoritosService;
            _productosService = productosService;
            _productosService = productosService;
            _usuariosService = usuariosService;
            _vendedoresService = vendedoresService;
        }

        // GET: Favoritos
        public async Task<IActionResult> Index()
        {
            return View(await _favoritosService.GetFavoritos());
        }

        // GET: Favoritos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Favorito favorito = await _favoritosService.DetailsFavorito(id);

            if (favorito == null)
            {
                return NotFound();
            }

            return View(favorito);
        }

        // GET: Favoritos/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ProductoId"] = new SelectList(await _productosService.GetProductos(), "Id", "Descripcion");
            ViewData["UsuarioId"] = new SelectList(await _usuariosService.GetUsuario(), "Id", "Apellido");
            ViewData["VendedorId"] = new SelectList(await _vendedoresService.GetVendedor(), "Id", "Ciudad");
            return View();
        }

        // POST: Favoritos/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,VendedorId,ProductoId,FechaMeGusta")] Favorito favorito)
        {
            if (ModelState.IsValid)
            {
                await _favoritosService.CreateFavoritoPost(favorito);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(await _productosService.GetProductos(), "Id", "Descripcion", favorito.ProductoId);
            ViewData["UsuarioId"] = new SelectList(await _usuariosService.GetUsuario(), "Id", "Apellido", favorito.UsuarioId);
            ViewData["VendedorId"] = new SelectList(await _vendedoresService.GetVendedor(), "Id", "Ciudad", favorito.VendedorId);
            return View(favorito);
        }

        // GET: Favoritos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Favorito favorito = await _favoritosService.EditFavoritoGet(id);
            if (favorito == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(await _productosService.GetProductos(), "Id", "Descripcion", favorito.ProductoId);
            ViewData["UsuarioId"] = new SelectList(await _usuariosService.GetUsuario(), "Id", "Apellido", favorito.UsuarioId);
            ViewData["VendedorId"] = new SelectList(await _vendedoresService.GetVendedor(), "Id", "Ciudad", favorito.VendedorId);
            return View(favorito);
        }

        // POST: Favoritos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,VendedorId,ProductoId,FechaMeGusta")] Favorito favorito)
        {
            if (id != favorito.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _favoritosService.EditFavoritoPost(favorito);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoritoExists(favorito.Id))
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
            ViewData["ProductoId"] = new SelectList(await _productosService.GetProductos(), "Id", "Descripcion", favorito.ProductoId);
            ViewData["UsuarioId"] = new SelectList(await _usuariosService.GetUsuario(), "Id", "Apellido", favorito.UsuarioId);
            ViewData["VendedorId"] = new SelectList(await _vendedoresService.GetVendedor(), "Id", "Ciudad", favorito.VendedorId);
            return View(favorito);
        }

        // GET: Favoritos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Favorito favorito = await _favoritosService.DetailsFavorito(id);
            if (favorito == null)
            {
                return NotFound();
            }

            return View(favorito);
        }

        // POST: Favoritos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _favoritosService.DeleteFavoritoPost(id);
            return RedirectToAction(nameof(Index));
        }

        private bool FavoritoExists(int id)
        {
            return _favoritosService.ExistFavorito(id);
        }
    }
}
