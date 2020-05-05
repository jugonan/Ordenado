using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Heldu.Entities;


namespace Heldu.Database.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Heldu.Entities.Models.Categoria> Categoria { get; set; }
        public DbSet<Heldu.Entities.Models.GustoUsuario> GustoUsuario { get; set; }
        public DbSet<Heldu.Entities.Models.Mercado> Mercado { get; set; }
        public DbSet<Heldu.Entities.Models.Producto> Producto { get; set; }
        public DbSet<Heldu.Entities.Models.ProductoCategoria> ProductoCategoria { get; set; }
        public DbSet<Heldu.Entities.Models.ProductoVendedor> ProductoVendedor { get; set; }
        public DbSet<Heldu.Entities.Models.Ubicacion> Ubicacion { get; set; }
        public DbSet<Heldu.Entities.Models.Usuario> Usuario { get; set; }
        public DbSet<Heldu.Entities.Models.Vendedor> Vendedor { get; set; }
        public DbSet<Heldu.Entities.Models.Transaccion> Transaccion { get; set; }
        public DbSet<Heldu.Entities.Models.Favorito> Favorito { get; set; }
        public DbSet<Heldu.Entities.Models.Review> Review { get; set; }
        public DbSet<Heldu.Entities.Models.OpcionProducto> OpcionProducto { get; set; }
    }
}
