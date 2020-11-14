using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Heldu.Logic.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosService _productosService;
        private readonly IVendedoresService _vendedoresService;
        private readonly IOpcionesProductosService _opcionesProductosService;
        private readonly IReviewsService _reviewsService;
        private readonly ICategoriasService _categoriaService;
        private readonly IImagenesProductosService _imagenesProductosService;
        private readonly IMapper _mapper;

        public ProductosController(IProductosService productosService,
                                   IVendedoresService vendedoresService,
                                   IOpcionesProductosService opcionesProductosService,
                                   IReviewsService reviewsService,
                                   ICategoriasService categoriasService,
                                   IImagenesProductosService imagenesProductosService,
                                   IMapper mapper)
        {
            _productosService = productosService;
            _vendedoresService = vendedoresService;
            _opcionesProductosService = opcionesProductosService;
            _reviewsService = reviewsService;
            _categoriaService = categoriasService;
            _imagenesProductosService = imagenesProductosService;
            _mapper = mapper;
        }


        // URI: ...api/Productos/
        // Devuelve todos los productos con la información básica que se guarda en la tabla Productos de la BBDD
        [Route("")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoReadDTO>>> GetAllProductos()
        {
            List<Producto> listaProductos = await _productosService.GetProductos();

            return Ok(_mapper.Map<IEnumerable<ProductoReadDTO>>(listaProductos));
        }


        // URI: ...api/Productos/Producto/5
        // Devuelve un solo producto con la información básica que se guarda en la tabla Productos de la BBDD
        [Route("Producto/{id}")]
        [HttpGet("{id}", Name = "GetProductoById")]
        public async Task<ActionResult<Producto>> GetProductoById(int id)
        {
            Producto producto = await _productosService.GetProductoById(id);

            if (producto == null)
            {
                return NotFound();
            }
            var productoReadDTO = _mapper.Map<ProductoReadDTO>(producto);
            return Ok(productoReadDTO);
        }


        // URI: ...api/Productos/ProductosVM
        // Devuelve todos los productos, cada uno con una sola opción, como los usaba el Index2. El anidado lo hace poco práctico.
        [Route("ProductosVM")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductosForIndex2VM>>> GetAllProductosVM()
        {
            var listaProductosVM = await _productosService.GetProductosForIndex2();

            return Ok(listaProductosVM);
        }


        // URI: ...api/Productos/Detalles/5
        // Devuelve un producto con todas sus opciones, el vendedor con sus datos básicos y la categoría.
        [Route("Detalles/{id}")]
        [HttpGet("{id}", Name = "GetProductoDetallesById")]
        public async Task<ActionResult> GetProductoDetallesById(int id)
        {
            ProductoDetallesVM productoDetalles = new ProductoDetallesVM();

            productoDetalles.producto = await _productosService.GetProductoById(id);
            if (productoDetalles.producto == null)
            {
                return NotFound(new string[] { "No hay producto", "Consultar la documentación" });
            }

            productoDetalles.vendedor = await _vendedoresService.ObtenerVendedorDesdeProducto(id);
            productoDetalles.opcionesProducto = await _opcionesProductosService.GetOpcionProductoById(id);
            productoDetalles.reviews = await _reviewsService.ObtenerReviewsByProductoId(id);
            productoDetalles.valoracionMedia = await _reviewsService.ObtenerValoracionMediaByProductoId(id);
            productoDetalles.totalComentarios = _reviewsService.CantidadComentariosByReviewList(productoDetalles.reviews);
            var categoria = await _categoriaService.GetCategoriaByProductoId(id);

            //var productoDetallesVMReadDTO = _mapper.Map<ProductoDetallesVMReadDTO>(productoDetalles);

            var productoDTO = new {
                id = productoDetalles.producto.Id,
                titulo = productoDetalles.producto.Titulo,
                descripcion = productoDetalles.producto.Descripcion,
                fechaValidez = productoDetalles.producto.FechaValidez,
                fechaAltaProducto = productoDetalles.producto.FechaAltaProducto,
                estado = productoDetalles.producto.Estado,
                cantidadVisitas = productoDetalles.producto.CantidadVisitas,
                condiciones = productoDetalles.producto.Condiciones,
                categoria = new {id = categoria.Id, nombre = categoria.Nombre},
                opcionProducto = _mapper.Map<IEnumerable<OpcionProductoReadDTO>>(productoDetalles.opcionesProducto),
                vendedor = new {
                    id = productoDetalles.vendedor.Id,
                    nombreEmpresa = productoDetalles.vendedor.NombreDeEmpresa,
                    telefono = productoDetalles.vendedor.Telefono,
                    paginaWeb = productoDetalles.vendedor.Paginaweb,
                    cif = productoDetalles.vendedor.CIF,
                    descripcionEmpresa = productoDetalles.vendedor.DescripcionEmpresa
                }
            };

            return Ok(productoDTO);
        }

        [Route("ByCategory/{categoriaId}")]
        [HttpGet("{id}", Name = "GetProductosByCategoryId")]
        public async Task<List<Producto>> GetProductosByCategoryId(int categoriaId)
        {
            List<Producto> productos = await _productosService.GetProductosByCategoriaId(categoriaId);

            // Tengo que añadir un equivalente NOT FOUND o returnear un DTO para permitir el not found
            //if (productos == null)
            //{
            //    return NotFound();
            //}
            return productos;
        }

        // URI: ...api/Productos/Image1/5
        //Devuelve la primera imagen del producto cuyo id se pasa
        [Route("Image1/{id}")]
        [HttpGet("{id}", Name = "GetImage1")]
        public async Task<ActionResult> GetImage1(int id)
        {
            byte[] imagen = await _imagenesProductosService.GetMainImage(id);
            if (imagen != null)
                return File(imagen, "image/jpeg");
            else
                return NotFound();
        }
    }
}
