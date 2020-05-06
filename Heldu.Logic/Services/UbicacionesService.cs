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
    public class UbicacionesService : IUbicacionesService
    {
        private readonly ApplicationDbContext _context;
        public UbicacionesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ubicacion>> GetUbicacion()
        {
            return await _context.Ubicacion.ToListAsync();
        }

        public async Task<Ubicacion> DetailUbicacion(int? id)
        {
            return await _context.Ubicacion.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task CreateUbicacion(Ubicacion ubicacion)
        {
            _context.Add(ubicacion);
            await _context.SaveChangesAsync();
        }
        public async Task<Ubicacion> EditUbicacionGet(int? id)
        {
            return await _context.Ubicacion.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task EditUbicacionPost(Ubicacion ubicacion)
        {
            _context.Update(ubicacion);
            await _context.SaveChangesAsync();
        }

        public async Task<Ubicacion> DeleteUbicacionGet(int? id)
        {
            return await _context.Ubicacion.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task DeleteUbicacionPost(int id)
        {
            Ubicacion ubicacion = await _context.Ubicacion.FindAsync(id);
            _context.Ubicacion.Remove(ubicacion);
            await _context.SaveChangesAsync();
        }

        public bool ExistUbicacion(int id)
        {
            return _context.Ubicacion.Any(e => e.Id == id);
        }

        public Task<List<Ubicacion>> GetUbiacaciones()
        {
            throw new NotImplementedException();
        }

        public Task<Ubicacion> DetailsUbicacion(int? id)
        {
            throw new NotImplementedException();
        }

        Task<Ubicacion> IUbicacionesService.CreateUbicacion(Ubicacion ubicacion)
        {
            throw new NotImplementedException();
        }

        Task<Ubicacion> IUbicacionesService.DeleteUbicacionPost(int id)
        {
            throw new NotImplementedException();
        }
    }
}
