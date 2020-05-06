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
    public class ProductoVendedoresController : Controller
    {
        private readonly IProductosVendedoresService _productosVendedoresService;

        public ProductoVendedoresController(IProductosVendedoresService productosVendedoresService)
        {
            _productosVendedoresService = productosVendedoresService;
        }
        // GET: ProductoVendedores
        public async Task<IActionResult> Index()
        {
            return View(await _productosVendedoresService.GetProductoVendedor());
        }

        // GET: ProductoVendedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductoVendedor productoVendedor = await _productosVendedoresService.DetailsProductoVendedor(id);
            if (productoVendedor == null)
            {
                return NotFound();
            }

            return View(productoVendedor);
        }

        // GET: ProductoVendedores/Create
        public IActionResult Create()
        {
            //ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Titulo");
            //ViewData["VendedorId"] = new SelectList(_context.Set<Vendedor>(), "Id", "NombreDeEmpresa");
            return View();
        }

        // POST: ProductoVendedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductoId,VendedorId")] ProductoVendedor productoVendedor)
        {
            if (ModelState.IsValid)
            {
                await _productosVendedoresService.CreateProductoVendedor(productoVendedor);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion", productoVendedor.ProductoId);
            //ViewData["VendedorId"] = new SelectList(_context.Set<Vendedor>(), "Id", "NombreDeEmpresa", productoVendedor.VendedorId);
            return View(productoVendedor);
        }

        // GET: ProductoVendedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductoVendedor productoVendedor = await _productosVendedoresService.EditProductoVendedorGet(id);
            if (productoVendedor == null)
            {
                return NotFound();
            }
            //ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion", productoVendedor.ProductoId);
            //ViewData["VendedorId"] = new SelectList(_context.Set<Vendedor>(), "Id", "NombreDeEmpresa", productoVendedor.VendedorId);
            return View(productoVendedor);
        }

        // POST: ProductoVendedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductoId,VendedorId")] ProductoVendedor productoVendedor)
        {
            if (id != productoVendedor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productosVendedoresService.EditProductoVendedorPost(productoVendedor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoVendedorExists(productoVendedor.Id))
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
            //ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion", productoVendedor.ProductoId);
            //ViewData["VendedorId"] = new SelectList(_context.Set<Vendedor>(), "Id", "NombreDeEmpresa", productoVendedor.VendedorId);
            return View(productoVendedor);
        }

        // GET: ProductoVendedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductoVendedor productoVendedor = await _productosVendedoresService.DeleteProductoVendedorGet(id);
            if (productoVendedor == null)
            {
                return NotFound();
            }

            return View(productoVendedor);
        }

        // POST: ProductoVendedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productosVendedoresService.DeleteProductoVendedorPost(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoVendedorExists(int id)
        {
            return _productosVendedoresService.ExistProductoVendedor(id);
        }
    }
}
