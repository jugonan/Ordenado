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
    public class HelperService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        // Al poner la herencia de Controller las el _signInManager y _messagesService se han puesto en gris inactivo.
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MessagesService _messagesService;

        public HelperService(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, MessagesService messagesService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _messagesService = messagesService;
        }

        public async Task<List<Producto>> ProductosDesdeCategoria(int idCategoria)
        {
            List<Producto> productos = new List<Producto>();
            List<ProductoCategoria> productoCategorias = await _context.ProductoCategoria.Where(x => x.CategoriaId == idCategoria).ToListAsync();

            foreach (ProductoCategoria productoCategoria in productoCategorias)
            {
                int idProducto = productoCategoria.ProductoId;
                Producto nuevoProducto = _context.Producto.FirstOrDefault(x => x.Id == idProducto);
                productos.Add(nuevoProducto);
            }
            return (productos);
        }

        public List<int> RandomProductos(int largo) //Genero una lista de 6 índices random entre 0 y la cantidad de productos de una categoría
        {
            Random random = new Random();
            List<int> RandomProd = new List<int>();
            if (largo > 5)
            {
                while (RandomProd.Count() < 6)
                {
                    bool existe = false;
                    int nuevo = random.Next(0, largo);
                    foreach (int item in RandomProd)
                    {
                        if (nuevo == item)
                        {
                            existe = true;
                        }
                    }
                    if (!existe)
                    {
                        RandomProd.Add(nuevo);
                    }
                }
                return RandomProd;
            }
            else
            {
                return RandomProd;
            }
        }
        //Obtener usuario con el ID de IdentityUser
        public async Task<Usuario> ObtenerUsuario(string Id)
        {
            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.IdentityUserId == Id);
            return usuario;
        }
        //Obtener vendedor con el ID de Proudcto
        public async Task<Vendedor> ObtenerVendedorDesdeProducto(int Id)
        {
            ProductoVendedor productoVendedor = await _context.ProductoVendedor
                                                                          .Include(v => v.Vendedor)
                                                                          .FirstOrDefaultAsync(x => x.ProductoId == Id);
            Vendedor vendedor = productoVendedor.Vendedor;
            return vendedor;
        }

        public async Task<Vendedor> ObtenerVendedor (string id)
        {
            Vendedor vendedor = await _context.Vendedor.FirstOrDefaultAsync(x => x.IdentityUserId == id);
            return vendedor;
        }

        public async Task<Producto> ObtenerProducto(int Id)
        {
            Producto producto = await _context.Producto.FirstOrDefaultAsync(x => x.Id == Id);
            return producto;
        }
        // Obtenemos Lista de reviews enviándole un Producto ID
        public async Task<List<Review>> ObtenerReviews(int Id)
        {
            List<Review> reviews = await _context.Review.Where(x => x.ProductoId == Id).ToListAsync();
            return reviews;
        }
        //Devuelve el listado de categorias existentes (para usarlo en el navbar del Layout)
        public async Task<List<Categoria>> ListaCategorias()
        {
            List<Categoria> listaCategorias = await _context.Categoria.ToListAsync();

            return listaCategorias;
        }
        // Función simple que recibe la lista de Reviews del modelo Producto
        public async Task<int> ObtenerTotalComentarios(List<Review> reviews)
        {
            int totalComentarios = reviews.Count();
            return totalComentarios;
        }
        // Acompaña a la anterior para sacar la valoración media
        public async Task<int> ObtenerValoracionMedia(List<Review> reviews, int totalComentarios)
        {
            int mediaValoracion;
            if (totalComentarios == 0)
            {
                mediaValoracion = 0;
            }
            else
            {
                int totalValoracion = 0;
                foreach (Review review in reviews)
                {
                    totalValoracion += review.Valoracion;
                }
                mediaValoracion = totalValoracion / totalComentarios;
            }
            return mediaValoracion;
        }
        public void EnviarEmail (string asunto, string mensaje)
        {
            {
                // Your hard-coded email values (where the email will be sent from), this could be define in a config file, etc.
                var email = "heldubbk@gmail.com";
                var password = "visualstudio";

                // Your target (you may want to ensure that you have a property within your form so that you know who to send the email to
                string address = "galo130@gmail.com";

                // Builds a message and necessary credentials
                var loginInfo = new NetworkCredential(email, password);
                var msg = new MailMessage();
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);

                // This email will be sent from you
                msg.From = new MailAddress(email);
                // Your target email address
                msg.To.Add(new MailAddress(address));
                msg.Subject = asunto;
                // Build the body of your email using the Body property of your message
                msg.Body = mensaje;

                // Wires up and send the email
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);
            }
        }
    }
}
