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
    public class VendedoresService : IVendedoresService
    {
        private readonly ApplicationDbContext _context;
        public VendedoresService(ApplicationDbContext context)
        {
            _context = context;
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

        public async Task<Vendedor> DeleteVendedorGet(int? id)
        {
            return await _context.Vendedor.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task DeleteVendedorPost(int id)
        {
            Vendedor vendedor = await _context.Vendedor.FindAsync(id);
            _context.Vendedor.Remove(vendedor);
            await _context.SaveChangesAsync();
        }

        public async Task<Vendedor> MiperfilVendedor(string vendedorId)
        {
            Vendedor vendedor = await _context.Vendedor.FirstOrDefaultAsync(x => x.IdentityUserId == vendedorId);
            return vendedor;
        }

        public async Task<Vendedor> EstadisticasVendedor(string vendedorId)
        {
            Vendedor vendedor = await _context.Vendedor.Include(v => v.ProductoVendedor).ThenInclude(a => a.Producto).FirstOrDefaultAsync(x => x.IdentityUserId == vendedorId);
            return vendedor;
        }

        public async Task<Vendedor> OpinionesVendedor(string vendedorId)
        {
            Vendedor vendedor = await _context.Vendedor.FirstOrDefaultAsync(x => x.IdentityUserId == vendedorId);
            return vendedor;
        }

        public async Task<Vendedor> MisproductosVendedor(string vendedorId)
        {
            Vendedor vendedor = await _context.Vendedor
                                                        .Include(v => v.ProductoVendedor)
                                                            .ThenInclude(p => p.Producto)
                                                        .FirstOrDefaultAsync(x => x.IdentityUserId == vendedorId);
            return vendedor;
        }

        Task<Vendedor> IVendedoresService.CreateVendedor(Vendedor vendedor)
        {
            throw new NotImplementedException();
        }

        Task<Vendedor> IVendedoresService.DeleteVendedorPost(int id)
        {
            throw new NotImplementedException();
        }

        public bool ExistVendedor(int id)
        {
            throw new NotImplementedException();
        }
    }
}
