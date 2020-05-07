using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        public async Task<Vendedor> DetailsVendedor(int? id)
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

        //Estos dos métodos hacen exactamente lo mismo que ObtenerVendedorMedianteIdentity
        //public async Task<Vendedor> OpinionesVendedor(string vendedorId)
        //{
        //    Vendedor vendedor = await _context.Vendedor.FirstOrDefaultAsync(x => x.IdentityUserId == vendedorId);
        //    return vendedor;
        //}

        //public async Task<Vendedor> MiperfilVendedor(string vendedorId)
        //{
        //    Vendedor vendedor = await _context.Vendedor.FirstOrDefaultAsync(x => x.IdentityUserId == vendedorId);
        //    return vendedor;
        //}

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
    }
}
