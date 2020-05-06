using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Heldu.Logic.Services
{
    public class UsuariosService : IUsuariosService
    {
        private readonly ApplicationDbContext _context;
        public UsuariosService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> GetUsuario()
        {
            return await _context.Usuario.ToListAsync();
        }

        public async Task<Usuario> DetailsUsuario(int? id)
        {
            return await _context.Usuario.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task CreateUsuario(Usuario usuario)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
        }
        public async Task<Usuario> EditUsuarioGet(int? id)
        {
            return await _context.Usuario.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task EditUsuarioPost(Usuario usuario)
        {
            _context.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> DeleteUsuarioGet(int? id)
        {
            return await _context.Usuario.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task DeleteUsuarioPost(int id)
        {
            Usuario usuario = await _context.Usuario.FindAsync(id);
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> MiPerfilUsuario(string userManagerId)
        {
           return await _context.Usuario.FirstOrDefaultAsync(u => u.IdentityUserId == userManagerId);
        }

        public async Task<Usuario> MicuentaUsuario(string userManagerId)
        {
            return await _context.Usuario.FirstOrDefaultAsync(u => u.IdentityUserId == userManagerId);
        }

        public async Task<Usuario> HistoricoUsuario(string userManagerId)
        {
            return await _context.Usuario.FirstOrDefaultAsync(u => u.IdentityUserId == userManagerId);
        }

        public async Task<Usuario> RewardsUsuario(string userManagerId)
        {
            return await _context.Usuario.FirstOrDefaultAsync(u => u.IdentityUserId == userManagerId);
        }

        public async Task<Usuario> MiscursosUsuario(string userManagerId)
        {
            return await _context.Usuario
                                                        .Include(v => v.Mercados)
                                                            .ThenInclude(p => p.Producto)
                                                        .FirstOrDefaultAsync(x => x.IdentityUserId == userManagerId);
        }

        public bool ExistUsuario(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }

        Task<Usuario> IUsuariosService.CreateUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        Task<Usuario> IUsuariosService.DeleteUsuarioPost(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<Usuario> GetUsuarioByACtiveIdentityUser(string usuarioId)
        {
            return await _context.Usuario.FirstOrDefaultAsync(x => x.IdentityUserId == usuarioId);
        }
    }
}
