using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DEFINITIVO.Services;
using System;
using Heldu.Logic.Interfaces;
using Heldu.Logic.ViewModels;

namespace DEFINITIVO.Controllers
{
    public class ProductosController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MessagesService _messagesService;
        private readonly IProductosService _productosService;
        private readonly ITransaccionesService _transaccionesService;
        private readonly ICategoriasService _categoriasService;
        private readonly IProductoCategoriasService _productoCategoriasService;
        private readonly IVendedoresService _vendedoresService;
        private readonly IUsuariosService _usuariosService;
        private readonly IProductosVendedoresService _productosVendedoresService;
        private readonly IReviewsService _reviewsService;

        public ProductosController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            MessagesService messagesService,
            IProductosService productosService,
            ITransaccionesService transaccionesService,
            ICategoriasService categoriasService,
            IProductoCategoriasService productoCategoriasService,
            IVendedoresService vendedoresService,
            IUsuariosService usuariosService,
            IProductosVendedoresService productosVendedoresService,
            IReviewsService reviewsService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _messagesService = messagesService;
            _productosService = productosService;
            _transaccionesService = transaccionesService;
            _categoriasService = categoriasService;
            _productoCategoriasService = productoCategoriasService;
            _vendedoresService = vendedoresService;
            _usuariosService = usuariosService;
            _productosVendedoresService = productosVendedoresService;
            _reviewsService = reviewsService;
        }

        // GET: Productos
        public async Task<ProductosForIndex2VM> Index2()
        {
            //ViewData["Categorias"] = await _categoriasService.GetCategorias();
            //return View(await _productosService.GetProductos());

            List<Categoria> listaCategorias = await _categoriasService.GetCategorias();
            List<Producto> listaProductos = await _productosService.GetProductos();
            List<ProductoCategoria> listaProductosCategorias = await _productoCategoriasService.GetProductosCategorias();

            return await _productosService.GetProductosForIndex2(listaCategorias, listaProductos, listaProductosCategorias);

        }
        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Producto producto = await _productosService.GetProductoById(id);
            if (producto == null)
            {
                return NotFound();
            }

            if (User.IsInRole("cliente"))
            {
                //Genero una nueva transacción con los datos del usuario, el producto y el vendedor
                Usuario usuario = await _usuariosService.GetUsuarioByActiveIdentityUser(_userManager.GetUserId(User));
                ProductoVendedor productoVendedor = await _productosVendedoresService.ProductoVendedorByProductoId(id);
                await _transaccionesService.CreateTransaccionWithUsuarioAndProductoVendedor(usuario, productoVendedor);
            }

            //Modifico el producto actual agregando una unidad a la columa "CantidadVisitas" de la tabla
            await _productosService.AddCantidadVisitasProductoById(id);

            return View(producto);
        }

        //Agregar review desde el detalle del producto mediante un modal. Retorna a la vista de detalle del producto.
        public async Task<IActionResult> CrearReview(string UsuarioId, int ProductoId, string ComentarioUsuario, string valoracionUsuario)
        {
            Usuario usuario = await _usuariosService.GetUsuarioByActiveIdentityUser(UsuarioId);
            Producto producto = await _productosService.GetProductoById(ProductoId);
            await _reviewsService.CreateReviewByUsuarioAndProducto(usuario, producto, ComentarioUsuario, valoracionUsuario);

            return RedirectToAction("Details", "Productos", new { id = producto.Id });
        }

        // GET: Productos/Create
        [Authorize(Roles = "admin,vendedor")]
        public async Task<IActionResult> Create()
        {
            ViewData["NombreCategoria"] = new SelectList(await _categoriasService.GetCategorias(), "Id", "Nombre");
            ViewData["VendedorId"] = new SelectList(await _vendedoresService.GetVendedor(), "Id", "NombreDeEmpresa");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<IActionResult> Create(ProductoCategoria productoCategoria, int? idVendedor)
        {
            Producto producto = productoCategoria.Producto;
            await _productosService.CreateProductoPost(producto);

            ProductoCategoria newProdCat = new ProductoCategoria()
            {
                CategoriaId = productoCategoria.CategoriaId,
                ProductoId = producto.Id
            };
            ProductoVendedor productoVendedor = new ProductoVendedor();
            productoVendedor.ProductoId = productoCategoria.Producto.Id;

            int aux = -1;
            if (idVendedor == null)
            {
                string idUsuario = _userManager.GetUserId(User);
                Vendedor vendedor = await _vendedoresService.GetVendedorByIdentityUserId(idUsuario); 
                aux = vendedor.Id;
            }
            else if (idVendedor != null)
            {
                string aux2 = idVendedor.ToString();
                aux = int.Parse(aux2);
            }
            productoVendedor.VendedorId = aux;

            if (ModelState.IsValid)
            {
                await _productosVendedoresService.CreateProductoVendedor(productoVendedor);
                await _productoCategoriasService.CreateProductoCategoriaPost(newProdCat);
                return RedirectToAction(nameof(Index2));
            }
            return View(producto);
        }

        // GET: Productos/Edit/5
        [Authorize(Roles = "admin,vendedor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Producto producto = await _productosService.EditProductoGet(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,FechaValidez,ImagenProducto,ImagenProducto2,ImagenProducto3,Precio,PrecioFinal,UnidadesStock")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productosService.EditProductoPost(producto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Modificado", "Vendedores");
            }
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Producto producto = await _productosService.GetProductoById(id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productosService.DeleteProductoPost(id);
            Producto producto = await _productosService.GetProductoById(id);
            _messagesService.SetShowMessage(true);
            _messagesService.SetMessage($"El producto '{producto.Titulo.ToUpper()}' ha sido eliminado!");
            return RedirectToAction(nameof(Index2));
        }

        private bool ProductoExists(int id)
        {
            return _productosService.ExistProducto(id);
            }
        public async Task<IActionResult> Categoria(int id)
        {
            ViewData["Categoria"] = await _categoriasService.GetCategoriaById(id);
            ViewData["CategoriaId"] = id;
            return View();
        }

        public async Task<IActionResult> Search(string inputBuscar)
        {
            List<Producto> output = await _productosService.BuscarProductosPorString(inputBuscar);
            ViewData["inputBuscar"] = inputBuscar;
            return View(output);
        }
    }
}
