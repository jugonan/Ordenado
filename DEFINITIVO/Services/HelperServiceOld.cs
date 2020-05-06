using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DEFINITIVO.Services;
using System;
using System.Net;
using System.Net.Mail;

namespace DEFINITIVO.Services
{
    //public class HelperService
    //{
    //    private readonly ApplicationDbContext _context;
    //    private readonly UserManager<IdentityUser> _userManager;
    //    // Al poner la herencia de Controller las el _signInManager y _messagesService se han puesto en gris inactivo.
    //    private readonly SignInManager<IdentityUser> _signInManager;
    //    private readonly MessagesService _messagesService;

    //    public HelperService(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, MessagesService messagesService)
    //    {
    //        _context = context;
    //        _userManager = userManager;
    //        _signInManager = signInManager;
    //        _messagesService = messagesService;
    //    }
    //    //public async Task<List<Producto>> ProductosDesdeCategoria(int idCategoria)
    //    //{
    //    //    List<Producto> productos = new List<Producto>();
    //    //    List<ProductoCategoria> productoCategorias = await _context.ProductoCategoria.Where(x => x.CategoriaId == idCategoria).ToListAsync();

    //    //    foreach (ProductoCategoria productoCategoria in productoCategorias)
    //    //    {
    //    //        int idProducto = productoCategoria.ProductoId;
    //    //        Producto nuevoProducto = _context.Producto.FirstOrDefault(x => x.Id == idProducto);
    //    //        productos.Add(nuevoProducto);
    //    //    }
    //    //    return (productos);
    //    //}

    //    //public async Task<Producto> ObtenerProducto(int Id)
    //    //{
    //    //    Producto producto = await _context.Producto.FirstOrDefaultAsync(x => x.Id == Id);
    //    //    return producto;
    //    //}
    //}
}
