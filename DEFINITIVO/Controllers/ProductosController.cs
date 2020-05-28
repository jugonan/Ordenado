using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DEFINITIVO.Services;
using Heldu.Logic.Interfaces;
using Heldu.Logic.ViewModels;
using System;
using Heldu.Logic.Services;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace DEFINITIVO.Controllers
{
    public class ProductosController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MessagesService _messagesService;
        private readonly IProductosService _productosService;
        private readonly IVisitasService _VisitasService;
        private readonly ICategoriasService _categoriasService;
        private readonly IProductoCategoriasService _productoCategoriasService;
        private readonly IVendedoresService _vendedoresService;
        private readonly IUsuariosService _usuariosService;
        private readonly IProductosVendedoresService _productosVendedoresService;
        private readonly IReviewsService _reviewsService;
        private readonly IHelperService _helperService;

        public ProductosController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            MessagesService messagesService,
            IProductosService productosService,
            IVisitasService VisitasService,
            ICategoriasService categoriasService,
            IProductoCategoriasService productoCategoriasService,
            IVendedoresService vendedoresService,
            IUsuariosService usuariosService,
            IProductosVendedoresService productosVendedoresService,
            IReviewsService reviewsService,
            IHelperService helperService)

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _messagesService = messagesService;
            _productosService = productosService;
            _VisitasService = VisitasService;
            _categoriasService = categoriasService;
            _productoCategoriasService = productoCategoriasService;
            _vendedoresService = vendedoresService;
            _usuariosService = usuariosService;
            _productosVendedoresService = productosVendedoresService;
            _reviewsService = reviewsService;
            _helperService = helperService;
        }

        public async Task<IActionResult> Index2(string postalCode)
        {
            if (string.IsNullOrEmpty(postalCode))
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("sessionPostalCode")))
                {
                    string postalCodeFromIP = await _helperService.GetPostalCodeFromIP();
                    string cityAndRegionFromIP = await _helperService.GetCityAndRegionFromIP();
                    HttpContext.Session.SetString("sessionPostalCode", postalCodeFromIP);
                    HttpContext.Session.SetString("sessionCity", cityAndRegionFromIP);
                }
            }
            else
            {
                HttpContext.Session.SetString("sessionPostalCode", postalCode);
                string cityFromPostalCode = await _helperService.GetCityFromPostalCode(postalCode);
                HttpContext.Session.SetString("sessionCity", cityFromPostalCode);
            }

            string cp = HttpContext.Session.GetString("sessionPostalCode");
            string city = HttpContext.Session.GetString("sessionCity");

            ViewData["PostalCode"] = cp;
            ViewData["City"] = city;


            List<Categoria> listaCategorias = await _categoriasService.GetCategorias();
            List<Producto> listaProductos = await _productosService.GetProductos();
            List<ProductoCategoria> listaProductosCategorias = await _productoCategoriasService.GetProductosCategorias();
            ProductosForIndex2VM listasListaProductos = _productosService.GetProductosForIndex2(listaCategorias, listaProductos, listaProductosCategorias);

            //ViewData["PostalCodeByIP"] = PostalCodeFromIP;
            ViewData["Categorias"] = listaCategorias;
            return View(listasListaProductos);
        }
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
                await _VisitasService.CreateVisitaWithUsuarioAndProductoVendedor(usuario, productoVendedor);
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

        [Authorize(Roles = "admin,vendedor")]
        public async Task<IActionResult> Create()
        {
            ViewData["NombreCategoria"] = new SelectList(await _categoriasService.GetCategorias(), "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<IActionResult> Create(ProductoCategoriaVM productoCategoriaVM, int? idVendedor, List<IFormFile> ImagenProducto, List<IFormFile> ImagenProducto2, List<IFormFile> ImagenProducto3)
        {
            Producto producto = productoCategoriaVM.Producto;
            producto.ImagenProducto = await _productosService.AgregarImagenesBlob(ImagenProducto);
            producto.ImagenProducto2 = await _productosService.AgregarImagenesBlob(ImagenProducto2);
            producto.ImagenProducto3 = await _productosService.AgregarImagenesBlob(ImagenProducto3);

            await _productosService.CreateProductoPost(producto);

            ProductoCategoria newProdCat = new ProductoCategoria()
            {
                CategoriaId = productoCategoriaVM.Categoria.Id,
                ProductoId = producto.Id
            };
            ProductoVendedor productoVendedor = new ProductoVendedor();
            productoVendedor.ProductoId = producto.Id;

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
                return RedirectToAction("Create", "OpcionesProductos", new { productoId = producto.Id });
            }
            return View(producto);
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<IActionResult> Edit(int id, Producto producto, List<IFormFile> ImagenProducto, List<IFormFile> ImagenProducto2, List<IFormFile> ImagenProducto3)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    producto.ImagenProducto = await _productosService.AgregarImagenesBlob(ImagenProducto);
                    producto.ImagenProducto2 = await _productosService.AgregarImagenesBlob(ImagenProducto2);
                    producto.ImagenProducto3 = await _productosService.AgregarImagenesBlob(ImagenProducto3);
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
                return RedirectToAction("Modificado", "Vendedores");
            }
            return View(producto);
        }

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
            List<Producto> nuevaLista = await _productosService.GetProductosByCategoriaId(id);
            ViewData["Categoria"] = await _categoriasService.GetCategoriaById(id);
            return View(nuevaLista);
        }

        public async Task<IActionResult> Search(string inputBuscar, string catSelected)
        {
            ViewData["inputBuscar"] = inputBuscar;
            if (catSelected == "-1")
            {
                List<Producto> output = await _productosService.BuscarProductosPorString(inputBuscar);
                return View(output);
            }
            else
            {
                int categoriaId = Convert.ToInt32(catSelected);
                List<Producto> output = await _productosService.BuscarProductosPorStringYCategoria(inputBuscar, categoriaId);
                return View(output);
            }
        }

        public IActionResult LocateVisitor(string inputPostalCode)
        {
            return RedirectToAction("Index2", "Productos", new { postalCode = inputPostalCode });
        }
    }
}
