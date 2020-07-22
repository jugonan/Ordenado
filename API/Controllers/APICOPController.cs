using System.Collections.Generic;
using System.Threading.Tasks;
using Heldu.Entities.Models;
using Heldu.Database.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Heldu.Logic.ViewModels;
using Heldu.Logic.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APICOPController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoriasService _categoriasService;
        private readonly IProductosService _productosService;
        private readonly IProductoCategoriasService _productoCategoriasService;
        private readonly IMemoryCache _memoryCache;


        public APICOPController(
            ApplicationDbContext context,
            ICategoriasService categoriasService,
            IProductosService productosService,
            IProductoCategoriasService productoCategoriasService,
            IMemoryCache memoryCache)
        {
            _context = context;
            _categoriasService = categoriasService;
            _productosService = productosService;
            _productoCategoriasService = productoCategoriasService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<ProductosForIndex2VM> GetProducto()
        {
            List<ProductoCategoria> listaProductosCategorias = await _productoCategoriasService.GetProductosCategorias();
            List<Categoria> listaCategorias = await _categoriasService.GetCategorias();
            ProductosForIndex2VM productosForIndex2VMs = await _productosService.GetProductosForIndex2(listaCategorias, listaProductosCategorias);
            return productosForIndex2VMs;
        }
    }
}