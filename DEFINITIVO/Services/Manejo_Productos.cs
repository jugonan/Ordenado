using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEFINITIVO.Data;
using DEFINITIVO.Models;
using Microsoft.EntityFrameworkCore;

namespace DEFINITIVO.Services
{
    public class Manejo_Productos : IManejo_Productos
    {
        private readonly ApplicationDbContext _context;
        private readonly HelperService _helperService;

        public Manejo_Productos(ApplicationDbContext context, HelperService helperService)
        {
            _context = context;
            _helperService = helperService;

        }

        public List<Producto> GetProductosComprados(int id)
        {
            List<Transaccion> transacciones = _context.Transaccion.Include(x => x.Producto)
                .Include(x => x.Vendedor)
            .Where(x => x.Usuario.Id == id).Where(x=>x.Unidades !=0).ToList();

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

        public List<Producto> GetProductosCanjeados(int id)
        {
            throw new NotImplementedException();
        }
    }
}
