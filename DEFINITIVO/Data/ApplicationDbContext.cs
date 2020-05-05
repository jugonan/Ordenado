using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DEFINITIVO.Models;

namespace DEFINITIVO.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DEFINITIVO.Models.Categoria> Categoria { get; set; }
        public DbSet<DEFINITIVO.Models.GustoUsuario> GustoUsuario { get; set; }
        public DbSet<DEFINITIVO.Models.Mercado> Mercado { get; set; }
        public DbSet<DEFINITIVO.Models.Producto> Producto { get; set; }
        public DbSet<DEFINITIVO.Models.ProductoCategoria> ProductoCategoria { get; set; }
        public DbSet<DEFINITIVO.Models.ProductoVendedor> ProductoVendedor { get; set; }
        public DbSet<DEFINITIVO.Models.Ubicacion> Ubicacion { get; set; }
        public DbSet<DEFINITIVO.Models.Usuario> Usuario { get; set; }
        public DbSet<DEFINITIVO.Models.Vendedor> Vendedor { get; set; }
        public DbSet<DEFINITIVO.Models.Transaccion> Transaccion { get; set; }
        public DbSet<DEFINITIVO.Models.Favorito> Favorito { get; set; }
        public DbSet<DEFINITIVO.Models.Review> Review { get; set; }
        public DbSet<DEFINITIVO.Models.OpcionProducto> OpcionProducto { get; set; }
    }
}
