﻿using Heldu.Database.Data;
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
        public async Task<OpcionProducto> GetOpcionProductoById(int? id)
        {
            return await  _context.OpcionProducto
                                .FirstOrDefaultAsync(m => m.Id == id);
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
            string precioInicialVM = precioInicial.ToString();

            var precioFinal = algo.GetProperty("precioFin");
            string precioFinalVM = precioFinal.ToString();

            var descuento = algo.GetProperty("descuento");
            string descuentoVM = descuento.ToString();

            var unidades = algo.GetProperty("unidades");
            string unidadesVM = unidades.ToString();

            OpcionProducto opcionProducto = new OpcionProducto()
            {
                ProductoId = productoId,
                Descripcion = descripcionVM,
                PrecioInicial = Convert.ToDecimal(precioInicialVM),
                PrecioFinal = Convert.ToDecimal(precioFinalVM),
                Descuento = descuentoVM,
            };
            return opcionProducto;
        }
    }
}
