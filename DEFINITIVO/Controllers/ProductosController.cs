using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DEFINITIVO.Data;
using DEFINITIVO.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DEFINITIVO.Services;
using System;

namespace DEFINITIVO.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MessagesService _messagesService;
        private readonly HelperService _helperService;

        public ProductosController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, MessagesService messagesService, HelperService helperService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _messagesService = messagesService;
            _helperService = helperService;
        }

        // GET: Productos
        public async Task<IActionResult> Index2()
        {
            ViewData["Categorias"] = await _context.Categoria.ToListAsync();
            return View(await _context.Producto
                                                .Include(p => p.ProductoCategoria)
                                                    .ThenInclude(a => a.Categoria)
                                                .Include(p => p.ProductoVendedor)
                                                    .ThenInclude(a => a.Vendedor)
                                                .ToListAsync());
        }
        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            // Añadirlo a la listas de transacciones como "visto" por el usuario logueado
            if (User.IsInRole("cliente"))
            {
                Transaccion transaccion = new Transaccion();
                await GenerarTransaccion(transaccion);
                _context.Add(transaccion);
                await _context.SaveChangesAsync();
            }
            async Task<Transaccion> GenerarTransaccion(Transaccion transaccion)
            {
                Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.IdentityUserId == _userManager.GetUserId(User));
                ProductoVendedor productoParaSumar = await _context.ProductoVendedor.FirstOrDefaultAsync(x => x.ProductoId == id);
                transaccion.UsuarioId = usuario.Id;
                transaccion.ProductoId = productoParaSumar.ProductoId;
                transaccion.VendedorId = productoParaSumar.VendedorId;
                transaccion.Unidades = 0;
                transaccion.FechaTransaccion = DateTime.Today;
                return (transaccion);
            }

            //agrego a la columa "CantidadVisitas" de la tabla Producto una unidad
            Producto productoActual = await _context.Producto.FirstAsync(x => x.Id == id);
            productoActual.CantidadVisitas++;
            _context.Update(productoActual);
            await _context.SaveChangesAsync();
            return View(producto);
        }
        
        //Agregar review desde el detalle del producto mediante un modal. Retorna a la vista de detalle del producto.
        public async Task<IActionResult> CrearReview(string UsuarioId, int ProductoId, string ComentarioUsuario, string valoracionUsuario)
        {
            Usuario usuario = await _helperService.ObtenerUsuario(UsuarioId);
            Producto producto = await _helperService.ObtenerProducto(ProductoId);
            Review review = new Review
            {
                UsuarioId = usuario.IdentityUserId,
                Usuario = usuario,
                ProductoId = producto.Id,
                Producto = producto,
                Comentario = ComentarioUsuario,
                Fecha = DateTime.Now,
                Valoracion = Convert.ToInt32(valoracionUsuario),
            };
            _context.Add(review);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details","Productos",new {id = producto.Id });
        }

        // GET: Productos/Create
        [Authorize(Roles = "admin,vendedor")]
        public IActionResult Create()
        {
            ViewData["NombreCategoria"] = new SelectList(_context.Categoria, "Id", "Nombre");
            ViewData["VendedorId"] = new SelectList(_context.Vendedor, "Id", "NombreDeEmpresa");
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
            _context.Add(producto);
            await _context.SaveChangesAsync();

            ProductoCategoria prodCat = new ProductoCategoria();
            prodCat.CategoriaId = productoCategoria.CategoriaId;
            prodCat.ProductoId = producto.Id;
            ProductoVendedor productoVendedor = new ProductoVendedor();
            productoVendedor.ProductoId = productoCategoria.Producto.Id;

            int aux = -1;
            if (idVendedor == null)
            {
                string idUsuario = _userManager.GetUserId(User);
                Vendedor vendedor = await _context.Vendedor.FirstOrDefaultAsync(x => x.IdentityUserId == idUsuario);
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
                _context.Add(productoVendedor);
                await _context.SaveChangesAsync();
                _context.Add(prodCat);
                await _context.SaveChangesAsync();
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

            var producto = await _context.Producto.FindAsync(id);
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
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
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

            var producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var producto = await _context.Producto.FindAsync(id);
            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();
            _messagesService.SetShowMessage(true);
            _messagesService.SetMessage($"El producto '{producto.Titulo.ToUpper()}' ha sido eliminado!");
            return RedirectToAction(nameof(Index2));
        }

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Categoria(int id)
        {
            ViewData["Categoria"] = await _context.Categoria.FirstOrDefaultAsync(x => x.Id == id);
            ViewData["CategoriaId"] = id;
            return View();
        }

        public async Task<IActionResult> Search (string inputBuscar)
        {
            List<Producto> listaProductos = await _context.Producto.ToListAsync();
            List<Producto> listaProductosEncontrados = new List<Producto>();

            foreach (Producto producto in listaProductos)
            {
                if (producto.Titulo.ToLower().Contains(inputBuscar.ToLower()) || producto.Descripcion.ToLower().Contains(inputBuscar.ToLower()))
                {
                    listaProductosEncontrados.Add(producto);
                }
            }
            ViewData["inputBuscar"] = inputBuscar;
            return View(listaProductosEncontrados);
        }
    }
}
