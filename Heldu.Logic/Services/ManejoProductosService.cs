using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Heldu.Logic.Services
{
    public class ManejoProductosService : IManejoProductosService
    {
        private readonly ApplicationDbContext _context;

        public ManejoProductosService(ApplicationDbContext context)
        {
            _context = context;

        }

        public List<Producto> GetProductosComprados(int id)
        {
            List<Transaccion> transacciones = _context.Transaccion.Include(x => x.Producto)
                                                                  .Include(x => x.Vendedor)
                                                                  .Where(x => x.Usuario.Id == id)
                                                                  .Where(x => x.Unidades != 0)
                                                                  .ToList();

            List<Producto> productos = new List<Producto>();
            foreach (Transaccion item in transacciones)
            {
                productos.Add(item.Producto);
            }
            return productos;
        }

        public List<Producto> GetProductosDisponibles(int id)
        {
            DateTime FechaDeHoy = DateTime.Today;
            List<Producto> comprados = GetProductosComprados(id);
            List<Producto> disponibles = new List<Producto>();
            foreach (Producto comprado in comprados)
            {
                if (comprado.FechaValidez >= FechaDeHoy)
                {
                    disponibles.Add(comprado);
                }
            }
            return disponibles;
        }
        // Obtiene una lista con los productos vistos por EL COMPRADOR pasándole el UsuarioId
        public async Task<List<Transaccion>> GetProductosVistosPorUsuario(int id)
        {
            List<Transaccion> vistos = await _context.Transaccion
                                                                    .Include(x => x.Producto)
                                                                    .Include(x => x.Vendedor)
                                                                    .Where(x => x.Unidades == 0 && x.UsuarioId == id).ToListAsync();
            return vistos;
        }
        // Obtiene una lista con los productos vistos de EL VENDEDOR pasándole el VendedorId
        public async Task<List<Transaccion>> GetProductosVistosDelVendedor(int id)
        {
            List<Transaccion> vistos = await _context.Transaccion
                                                                    .Include(x => x.Producto)
                                                                    .Include(x => x.Vendedor)
                                                                    .Where(x => x.VendedorId == id).ToListAsync();
            return vistos;
        }
    }
}
