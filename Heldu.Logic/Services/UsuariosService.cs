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
        public async Task<Usuario> ObtenerUsuarioDesdedIdentity(string identityId)
        {
            return await _context.Usuario.Include(x => x.IdentityUser).FirstOrDefaultAsync(x => x.IdentityUserId == identityId);
        }
        public async Task<List<Usuario>> GetUsuario()
        {
            return await _context.Usuario.ToListAsync();
        }
        public async Task<Usuario> GetUsuarioById(int? id)
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
        public async Task DeleteUsuarioPost(int id)
        {
            _context.Usuario.Remove(await _context.Usuario.FindAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> MisCursosUsuario(string userManagerId)
        {
            return await _context.Usuario
                                .FirstOrDefaultAsync(x => x.IdentityUserId == userManagerId);
        }
        public bool ExistUsuario(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
        public async Task<List<Usuario>> GetUsuariosListByActiveIdentityUser(string usuarioId)
        {
            return await _context.Usuario.Where(x => x.IdentityUserId == usuarioId).ToListAsync();
        }
        public async Task<Usuario> GetUsuarioByActiveIdentityUser(string usuarioId)
        {
            return await _context.Usuario.FirstOrDefaultAsync(x => x.IdentityUserId == usuarioId);
        }

        public async Task<List<Usuario>> GestionarUsuarios()
        {
            List<Usuario> usuarios = await _context.Usuario.Include(x=>x.Categorias)
                                                                .ThenInclude(y=>y.Categoria)
                                                           .ToListAsync();
            return usuarios;
        }

        public async Task<Usuario> ObtenerUsuarioConUbicacion(string identityId)
        {
            Usuario usuario = await _context.Usuario.Include(x => x.IdentityUser).FirstOrDefaultAsync(x => x.IdentityUserId == identityId);
            UbicacionUsuario ubicacionUsuario = await _context.UbicacionUsuario.FirstOrDefaultAsync(x => x.UsuarioId == usuario.Id);
            usuario.UbicacionUsuario = ubicacionUsuario;
            return usuario;
        }
    }
}
