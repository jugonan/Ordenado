using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Heldu.Logic.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Heldu.Logic.Services
{
    public class OpcionesProductosService : IOpcionesProductosService
    {
        private readonly ApplicationDbContext _context;
        public OpcionesProductosService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<OpcionProducto>> GetOpcionesProductos()
        {
            return await _context.OpcionProducto.ToListAsync();
        }
        // Recibe todas las opcionesProducto desde el id de Producto
        public async Task<List<OpcionProducto>> GetOpcionProductoById(int? id)
        {
            return await _context.OpcionProducto.Where(m => m.ProductoId == id).ToListAsync();
        }
        // Devuelve una sola opcionProducto para un determinado ProductoId
        public async Task<OpcionProducto> GetFirstOpcionProductoByProductoId(int? id)
        {
            return await _context.OpcionProducto.Where(m => m.ProductoId == id).FirstOrDefaultAsync();
        }
        // Recibe una única opción de producto por el Id de estea
        public async Task<OpcionProducto> GetOpcionProductoByHisId(int? id)
        {
            return await _context.OpcionProducto.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateOpcionProductoPost(OpcionProducto opcionProducto)
        {
            _context.Add(opcionProducto);
            await _context.SaveChangesAsync();
        }
        public async Task<OpcionProducto> EditOpcionProductoGet(int? id)
        {
            return await _context.OpcionProducto.FindAsync(id);
        }
        public async Task EditOpcionProductoPost(OpcionProducto opcionProducto)
        {
            _context.Update(opcionProducto);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteOpcionProductoPost (int id)
        {
            _context.OpcionProducto.Remove(await _context.OpcionProducto.FindAsync(id));
            await _context.SaveChangesAsync();
        }
        public bool ExistOpcionProducto(int id)
        {
            return _context.OpcionProducto.Any(e => e.Id == id);
        }
        public OpcionProducto crearDesdeJson(string json, int productoId)
        {
            var parseado = JsonDocument.Parse(json);
            var algo = parseado.RootElement;

            var descripcion = algo.GetProperty("descripcion");
            string descripcionVM = descripcion.ToString();

            var precioInicial = algo.GetProperty("precioInicio");
            float precioInicialVM = float.Parse(precioInicial.ToString());

            var precioFinal = algo.GetProperty("precioFin");
            float precioFinalVM = float.Parse(precioFinal.ToString());

            var descuento = algo.GetProperty("descuento");
            string descuentoVM = descuento.ToString();

            var unidades = algo.GetProperty("stockInicial");
            string unidadesVM = unidades.ToString();

            OpcionProducto opcionProducto = new OpcionProducto()
            {
                ProductoId = productoId,
                Descripcion = descripcionVM,
                PrecioInicial = precioInicialVM,
                PrecioFinal = precioFinalVM,
                Descuento = descuentoVM,
                StockInicial = Convert.ToInt32(unidadesVM),
                CantidadVendida = 0,
            };            
            return opcionProducto;
        }
    }
}
