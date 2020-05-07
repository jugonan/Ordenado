using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;

namespace DEFINITIVO.Controllers
{
    public class ProductoCategoriasController : Controller
    {
        private readonly IProductoCategoriasService _productoCategoriasService;
        private readonly ICategoriasService _categoriasService;
        private readonly IProductosService _productosService;

        public ProductoCategoriasController(IProductoCategoriasService productoCategoriasService, ICategoriasService categoriasService, IProductosService productosService)
        {
            _productoCategoriasService = productoCategoriasService;
            _categoriasService = categoriasService;
            _productosService = productosService;
        }

        // GET: ProductoCategorias
        public async Task<IActionResult> Index()
        {
            return View(await _productoCategoriasService.GetProductosCategorias());
        }

        // GET: ProductoCategorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductoCategoria productoCategoria = await _productoCategoriasService.DetailsProductoCategoria(id);
            if (productoCategoria == null)
            {
                return NotFound();
            }

            return View(productoCategoria);
        }

        // GET: ProductoCategorias/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoriaId"] = new SelectList(await _categoriasService.GetCategorias(), "Id", "Nombre");
            ViewData["ProductoId"] = new SelectList(await _productosService.GetProductos(), "Id", "Descripcion");
            return View();
        }

        // POST: ProductoCategorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductoId,CategoriaId")] ProductoCategoria productoCategoria)
        {
            if (ModelState.IsValid)
            {
                await _productoCategoriasService.CreateProductoCategoriaPost(productoCategoria);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(await _categoriasService.GetCategorias(), "Id", "Id", productoCategoria.CategoriaId);
            ViewData["ProductoId"] = new SelectList(await _productosService.GetProductos(), "Id", "Descripcion", productoCategoria.ProductoId);
            return View(productoCategoria);
        }

        // GET: ProductoCategorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductoCategoria productoCategoria = await _productoCategoriasService.EditProductoCategoriaGet(id);
            if (productoCategoria == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(await _categoriasService.GetCategorias(), "Id", "Id", productoCategoria.CategoriaId);
            ViewData["ProductoId"] = new SelectList(await _productosService.GetProductos(), "Id", "Descripcion", productoCategoria.ProductoId);
            return View(productoCategoria);
        }

        // POST: ProductoCategorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductoId,CategoriaId")] ProductoCategoria productoCategoria)
        {
            if (id != productoCategoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productoCategoriasService.EditProductoCategoriaPost(productoCategoria);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoCategoriaExists(productoCategoria.Id))
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
            ViewData["CategoriaId"] = new SelectList(await _categoriasService.GetCategorias(), "Id", "Id", productoCategoria.CategoriaId);
            ViewData["ProductoId"] = new SelectList(await _productosService.GetProductos(), "Id", "Descripcion", productoCategoria.ProductoId);
            return View(productoCategoria);
        }

        // GET: ProductoCategorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductoCategoria productoCategoria = await _productoCategoriasService.DetailsProductoCategoria(id);
            if (productoCategoria == null)
            {
                return NotFound();
            }

            return View(productoCategoria);
        }

        // POST: ProductoCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productoCategoriasService.DeleteProductoCategoriaPost(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoCategoriaExists(int id)
        {
            return _productoCategoriasService.ExistProductoCategoria(id);
        }
    }
}
