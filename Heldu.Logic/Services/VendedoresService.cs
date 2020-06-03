using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Heldu.Logic.ViewModels;

namespace Heldu.Logic.Services
{
    public class VendedoresService : IVendedoresService
    {
        private readonly ApplicationDbContext _context;
        public VendedoresService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Vendedor> ObtenerVendedorDesdedIdentity(string identityId)
        {
            Vendedor vendedor = await _context.Vendedor.FirstOrDefaultAsync(x => x.IdentityUserId == identityId);
            return vendedor;
        }
        public async Task<List<Vendedor>> GetVendedor()
        {
            return await _context.Vendedor.ToListAsync();
        }
        public async Task<Vendedor> GetVendedorById(int? id)
        {
            return await _context.Vendedor.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task CreateVendedor(Vendedor vendedor)
        {
            _context.Add(vendedor);
            await _context.SaveChangesAsync();
        }
        public async Task<Vendedor> EditVendedorGet(int? id)
        {
            return await _context.Vendedor.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task EditVendedorPost(Vendedor vendedor)
        {
            _context.Update(vendedor);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteVendedorPost(int id)
        {
            _context.Vendedor.Remove(await _context.Vendedor.FindAsync(id));
            await _context.SaveChangesAsync();
        }
        public bool ExistVendedor(int id)
        {
            return _context.Vendedor.Any(e => e.Id == id);
        }
        public async Task<Vendedor> EstadisticasVendedor(string vendedorId)
        {
            return await _context.Vendedor.Include(v => v.ProductoVendedor).ThenInclude(a => a.Producto).FirstOrDefaultAsync(x => x.IdentityUserId == vendedorId);
        }

        public async Task<Vendedor> MisproductosVendedor(string vendedorId)
        {
            return await _context.Vendedor
                                .Include(v => v.ProductoVendedor)
                                .ThenInclude(p => p.Producto)
                                .FirstOrDefaultAsync(x => x.IdentityUserId == vendedorId);
        }

        // Devuelve el objeto vendedor pasándole el Id del producto
        public async Task<Vendedor> ObtenerVendedorDesdeProducto(int Id)
        {
            ProductoVendedor productoVendedor = await _context.ProductoVendedor
                                                                          .Include(v => v.Vendedor)
                                                                          .FirstOrDefaultAsync(x => x.ProductoId == Id);
            return productoVendedor.Vendedor;
        }

        public async Task<Vendedor> GetVendedorByIdentityUserId(string id)
        {
            return await _context.Vendedor.FirstOrDefaultAsync(x => x.IdentityUserId == id);
        }

        // Devuelve una lista de VM con la el ID de cada producto y la cantidad de visitas en cada objeto VM de la lista
        public async Task<List<CantidadVisitasProductoVM>> GetProductosVistosDelVendedor(Vendedor vendedor)
        {
            List<Visita> vistos = await _context.Visita
                                                                    .Include(x => x.Producto)
                                                                    .Include(x => x.Vendedor)
                                                                    .Where(x => x.VendedorId == vendedor.Id).ToListAsync();
            //Creo una lista vacía para llenar de VM con parámetros de ProudctoId y Cantidad visitas
            List<CantidadVisitasProductoVM> objetosLista = new List<CantidadVisitasProductoVM>();
            // Aprovecho que el ProductoVendedor ya tiene una lista de productos únicos para recorrerla
            foreach (ProductoVendedor productoDiferente in vendedor.ProductoVendedor)
            {
                // Al ser una lista de productos únicos, lleno la Lista de VM con ellos
                CantidadVisitasProductoVM objeto = new CantidadVisitasProductoVM()
                {
                    ProductoId = productoDiferente.ProductoId,
                    cantidadVisitas = 0
                };
                objetosLista.Add(objeto);
            }
            // Luego recorro la lista total de Visitas y comparo el Id de cada producto con la el ProductoId
            // de cada objeto único en la lista de VM. Si es el mismo, hago +1 y así obtengo el total de visitas por
            // cada producto.
            int aux = 0;
            foreach (Visita Visita in vistos)
            {
                foreach (CantidadVisitasProductoVM objeto in objetosLista)
                {
                    if (Visita.ProductoId == objeto.ProductoId)
                    {
                        objeto.cantidadVisitas += 1;
                        aux++;
                    }
                }
            }
            if (aux != 0)
            {
                return objetosLista;
            }
            else
            {
                List<CantidadVisitasProductoVM> objetosLista2 = new List<CantidadVisitasProductoVM>();
                return objetosLista2;

            }
        }
    }
}
