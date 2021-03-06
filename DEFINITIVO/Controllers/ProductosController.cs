using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Heldu.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DEFINITIVO.Services;
using Heldu.Logic.Interfaces;
using Heldu.Logic.ViewModels;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace DEFINITIVO.Controllers
{
    public class ProductosController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MessagesService _messagesService;
        private readonly IProductosService _productosService;
        private readonly IOpcionesProductosService _opcionesProductosService;
        private readonly IVisitasService _VisitasService;
        private readonly ICategoriasService _categoriasService;
        private readonly IProductoCategoriasService _productoCategoriasService;
        private readonly IVendedoresService _vendedoresService;
        private readonly IUsuariosService _usuariosService;
        private readonly IProductosVendedoresService _productosVendedoresService;
        private readonly IReviewsService _reviewsService;
        private readonly IHelperService _helperService;
        private readonly IImagenesProductosService _imagenesProductosService;
        private readonly IMemoryCache _memoryCache;

        public ProductosController(
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
            IHelperService helperService,
            IOpcionesProductosService opcionesProductosService,
            IImagenesProductosService imagenesProductosService,
            IMemoryCache memoryCache)

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
            _opcionesProductosService = opcionesProductosService;
            _imagenesProductosService = imagenesProductosService;
            _memoryCache = memoryCache;
        }

        //[OutputCache(Duration = 6000, VaryByParam ="postalCode", VaryByHeader = "User-Agent;From")]

        [ResponseCache(Duration = 2592000, Location = ResponseCacheLocation.Any, NoStore = false)]
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


            List<Categoria> listaCategorias;
            List<ProductoCategoria> listaProductosCategorias;
            ProductosForIndex2VM listasListaProductos;


            if (!_memoryCache.TryGetValue("ProductosForIndex2", out listasListaProductos))
            {
                listaCategorias = await _categoriasService.GetCategorias();
                listaProductosCategorias = await _productoCategoriasService.GetProductosCategorias();
                listasListaProductos = await _productosService.GetProductosForIndex2(listaCategorias, listaProductosCategorias);
                _memoryCache.Set("Categorias", listaCategorias);
                _memoryCache.Set("ProductosForIndex2", listasListaProductos);
            }
            else
            {
                listasListaProductos = _memoryCache.Get("ProductosForIndex2") as ProductosForIndex2VM;
            }

            if (!_memoryCache.TryGetValue("Categorias", out listaCategorias))
            {
                listaCategorias = await _categoriasService.GetCategorias();
                _memoryCache.Set("Categorias", listaCategorias);
            }
            else
            {
                listaCategorias = _memoryCache.Get("Categorias") as List<Categoria>;
            }

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
            string idUsuario = _userManager.GetUserId(User);
            ViewData["VendedorActivo"] = await _vendedoresService.GetVendedorByIdentityUserId(idUsuario);
            ViewData["NombreCategoria"] = new SelectList(await _categoriasService.GetCategorias(), "Id", "Nombre");
            return View();
            //Nuevo
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<IActionResult> Create(ProductoCategoriaVM productoCategoriaVM, int? idVendedor, List<IFormFile> Imagen1, List<IFormFile> Imagen2, List<IFormFile> Imagen3)
        {
            Producto producto = new Producto()
            {
                Titulo = productoCategoriaVM.Producto.Titulo,
                Descripcion = productoCategoriaVM.Producto.Descripcion,
                Condiciones = productoCategoriaVM.Producto.Condiciones,
                FechaValidez = productoCategoriaVM.Producto.FechaValidez
            };
            await _productosService.CreateProductoPost(producto);

            var img1 = await _imagenesProductosService.AgregarImagenesBlob(Imagen1);
            var img2 = await _imagenesProductosService.AgregarImagenesBlob(Imagen2);
            var img3 = await _imagenesProductosService.AgregarImagenesBlob(Imagen3);

            ImagenesProducto imagenes = new ImagenesProducto()
            {
                Imagen1 = img1,
                Imagen2 = img2,
                Imagen3 = img3,
                ProductoId = producto.Id
            };

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
                await _imagenesProductosService.CreateImagenesProducto(imagenes);
                await _productosVendedoresService.CreateProductoVendedor(productoVendedor);
                await _productoCategoriasService.CreateProductoCategoriaPost(newProdCat);
                return RedirectToAction("Create", "OpcionesProductos", new { productoId = producto.Id });
            }

            _memoryCache.Remove("ProductosForIndex2");
            return View(producto);
        }

        [Authorize(Roles = "admin,vendedor")]
        [HttpGet]
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
        public async Task<IActionResult> Edit(int id, Producto producto, List<IFormFile> Imagen1, List<IFormFile> Imagen2, List<IFormFile> Imagen3)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var imgBlob1 = await _imagenesProductosService.AgregarImagenesBlob(Imagen1);
                    var imgBlob2 = await _imagenesProductosService.AgregarImagenesBlob(Imagen2);
                    var imgBlob3 = await _imagenesProductosService.AgregarImagenesBlob(Imagen3);
                    ImagenesProducto imagenesEdit = new ImagenesProducto()
                    {
                        Imagen1 = imgBlob1,
                        Imagen2 = imgBlob2,
                        Imagen3 = imgBlob3,
                        ProductoId = id
                    };

                    await _imagenesProductosService.EditImagenes(imagenesEdit);
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
            _memoryCache.Remove("ProductosForIndex2");
            return View(producto);
        }

        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productosService.DeleteProductoPost(id);
            Producto producto = await _productosService.GetProductoById(id);
            _messagesService.SetShowMessage(true);
            _messagesService.SetMessage($"El producto '{producto.Titulo.ToUpper()}' ha sido eliminado!");
            _memoryCache.Remove("ProductosForIndex2");
            return RedirectToAction(nameof(Index2));
        }

        private bool ProductoExists(int id)
        {
            return _productosService.ExistProducto(id);
        }

        [ResponseCache(Duration = 2592000, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> Categoria(int id)
        {
            List<ProductoPrimeraOpcionProductoVM> listaProductosOpcion = new List<ProductoPrimeraOpcionProductoVM>();
            List<Producto> nuevaLista = await _productosService.GetProductosByCategoriaId(id);
            List<OpcionProducto> opcionesProductos = await _opcionesProductosService.GetOpcionesProductos();
            int i = 0;
            foreach (Producto item in nuevaLista)
            {
                ProductoPrimeraOpcionProductoVM nuevo = new ProductoPrimeraOpcionProductoVM()
                {
                    producto = item,
                    opcionProducto = opcionesProductos.Where(x => x.ProductoId == item.Id).FirstOrDefault()
                };
                listaProductosOpcion.Add(nuevo);
                i++;
            }
            ViewData["Categoria"] = await _categoriasService.GetCategoriaById(id);
            return View(listaProductosOpcion);
        }

        public async Task<IActionResult> Search(string inputBuscar, string catSelected)
        {
            List<ProductoPrimeraOpcionProductoVM> output = new List<ProductoPrimeraOpcionProductoVM>();

            ViewData["inputBuscar"] = inputBuscar;
            if (catSelected == "-1")
            {
                output = await _productosService.BuscarProductosPorString(inputBuscar);
                return View(output);
            }
            else
            {
                int categoriaId = Convert.ToInt32(catSelected);
                output = await _productosService.BuscarProductosPorStringYCategoria(inputBuscar, categoriaId);
                return View(output);
            }
        }

        public IActionResult LocateVisitor(string inputPostalCode)
        {
            return RedirectToAction("Index2", "Productos", new { postalCode = inputPostalCode });
        }

        //[OutputCache(Duration = 6000, VaryByParam = "Id")]
        [ResponseCache(Duration = 2592000, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> GetImage1(int id)
        {
            byte[] imagen = await _imagenesProductosService.GetMainImage(id);
            if (imagen != null)
                return File(imagen, "image/jpeg");
            else
                return null;
        }
        public async Task<IActionResult> GetImage2(int id)
        {
            byte[] imagen = await _imagenesProductosService.GetSecondImage(id);
            if (imagen != null)
                return File(imagen, "image/jpeg");
            else
                return null;
        }
        public async Task<IActionResult> GetImage3(int id)
        {
            byte[] imagen = await _imagenesProductosService.GetThirdImage(id);
            if (imagen != null)
                return File(imagen, "image/jpeg");
            else
                return null;
        }
        public async Task<IActionResult> Detalles(int? id)
        {
            ProductoDetallesVM productoDetalles = new ProductoDetallesVM();

            if (id == null)
            {
                return NotFound();
            }

            productoDetalles.producto = await _productosService.GetProductoById(id);
            if (productoDetalles.producto == null)
            {
                return NotFound();
            }

            if (User.IsInRole("cliente"))
            {
                //Genero una nueva transacción con los datos del usuario, el producto y el vendedor
                Usuario usuario = await _usuariosService.GetUsuarioByActiveIdentityUser(_userManager.GetUserId(User));
                ProductoVendedor productoVendedor = await _productosVendedoresService.ProductoVendedorByProductoId(id);
                await _VisitasService.CreateVisitaWithUsuarioAndProductoVendedor(usuario, productoVendedor);
                ViewData["Usuario"] = usuario;
            }

            //Modifico el producto actual agregando una unidad a la columa "CantidadVisitas" de la tabla
            await _productosService.AddCantidadVisitasProductoById(id);

            //ViewModel a pasar a la vista
            productoDetalles.vendedor = await _vendedoresService.ObtenerVendedorDesdeProducto(id);
            productoDetalles.opcionesProducto = await _opcionesProductosService.GetOpcionProductoById(id);
            productoDetalles.reviews = await _reviewsService.ObtenerReviewsByProductoId(id);
            productoDetalles.valoracionMedia = await _reviewsService.ObtenerValoracionMediaByProductoId(id);
            productoDetalles.totalComentarios = _reviewsService.CantidadComentariosByReviewList(productoDetalles.reviews);

            return View(productoDetalles);
        }
        public IActionResult Checkout()
        {
            return View();
        }

    }

}
