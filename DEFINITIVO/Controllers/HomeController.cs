using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Microsoft.AspNetCore.Http;
using DEFINITIVO.Services;
using Heldu.Logic.Interfaces;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Threading;

namespace DEFINITIVO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MessagesService _messagesService;
        private readonly IHelperService _helperService;
        private readonly IUsuariosService _usuariosService;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            MessagesService messagesService,            IHelperService helperService,
            IUsuariosService usuariosService
            )
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _messagesService = messagesService;
            _helperService = helperService;
            _usuariosService = usuariosService;
        }

        public async Task<IActionResult> Index()
        {
            string id = _userManager.GetUserId(User);
            if (id == null)
            {
                return RedirectToAction("Index2", "Productos");
            }
            else
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == id);

                if (user != null)
                {
                    //Comprobar si es usuario o vendedor o administrador
                    if (User.IsInRole("admin") || User.IsInRole("vendedor") || User.IsInRole("cliente"))
                    {
                        return RedirectToAction("Index2", "Productos");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "cliente");
                        return RedirectToAction("Create", "Usuarios", new { userId = user.Id });
                    }
                }
                else
                {
                    return RedirectToPage("~/Identity/Account/Register");
                }
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Cookies()
        {
            return View();
        }
        public IActionResult Funcionamiento()
        {
            return View();
        }

        public IActionResult Heldu()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult FAQ()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contacto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contacto(string nombreContacto, string telefonoContacto, string emailContacto, string asuntoContacto, string mensajeContacto)
        {
            string mensajeCompuesto = $"Un usuario no logueado/registrado envía el siguiente mensaje" +
                $"\n\bMensaje: {mensajeContacto}" +
                $"\n" +
                $"\nCon los siguientes datos de contacto:" +
                $"\n\bE-Mail: {emailContacto}   |  Teléfono: {telefonoContacto}" +
                $"\n\bNombre: {nombreContacto}" +
                $"\n" +
                $"\n Mensaje enviado el {DateTime.Now} desde";

            try
            {
                _helperService.EnviarEmail(asuntoContacto, mensajeCompuesto);
                _messagesService.SetShowMessage(true);
                _messagesService.SetMessage($"Su mensaje ha sido enviado, gracias por contactarnos! Le responderemos a la brevedad");
                return RedirectToAction("Index2", "Productos");
            }
            catch (Exception)
            {
                _messagesService.SetShowMessage(true);
                _messagesService.SetMessage($"No hemos podido gestionar su mensaje. Por favor contáctenos a:  jon@heldu.eus | Gracias!");
                return RedirectToAction("Index2", "Productos");
                throw;
            }
        }
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Contacto(string userId, string nombre, string email, string tel, string mensaje, string actualPath)
        //{
        //    Usuario usuario = await _usuariosService.GetUsuarioByActiveIdentityUser(userId);
        //    string asunto = "[No especificado] ha enviado un mensaje desde la web";
        //    if (!String.IsNullOrEmpty(nombre))
        //    {
        //        asunto = $"{nombre} ha enviado un mensaje desde la web";
        //    }

        //    string mensajeCompuesto = "Mensaje vacío. Revisar el código.";
        //    if (usuario != null)
        //    {
        //        mensajeCompuesto = $"El usuario {usuario.Nombre} {usuario.Apellido} (ID: {usuario.Id}) envía el siguiente mensaje:" +
        //            $"\n\b{mensaje}" +
        //            $"\n" +
        //            $"\nCon los siguientes datos de contacto:" +
        //            $"\n\bE-Mail: {email}   |  Teléfono: {tel}" +
        //            $"\n" +
        //            $"\n Mensaje enviado el {DateTime.Now} desde la URL: {actualPath}";
        //    }
        //    else
        //    {
        //        mensajeCompuesto = $"Un usuario no logueado/registrado envía el siguiente mensaje" +
        //            $"\n\b{mensaje}" +
        //            $"\n" +
        //            $"\nCon los siguientes datos de contacto:" +
        //            $"\n\bE-Mail: {email}   |  Teléfono: {tel}" +
        //            $"\n" +
        //            $"\n Mensaje enviado el {DateTime.Now} desde la URL: {actualPath}";
        //    }

        //    try
        //    {
        //        _helperService.EnviarEmail(asunto, mensajeCompuesto);
        //        _messagesService.SetShowMessage(true);
        //        _messagesService.SetMessage($"Su mensaje ha sido enviado, gracias por contactarnos! Le responderemos a la brevedad");
        //        return RedirectToAction("Index2", "Productos");
        //    }
        //    catch (Exception)
        //    {
        //        _messagesService.SetShowMessage(true);
        //        _messagesService.SetMessage($"No hemos podido gestionar su mensaje. Por favor contáctenos a:  jon@heldu.eus | Gracias!");
        //        return RedirectToAction("Index2", "Productos");
        //        throw;
        //    }

        //}
        public IActionResult LandingBeta()
        {
            //var backgroundProductsLoading = new Thread(_helperService.LoadProductsTask);
            //backgroundProductsLoading.IsBackground = true;
            //backgroundProductsLoading.Priority = ThreadPriority.Lowest;
            //backgroundProductsLoading.Start();

            return View();
        }
    }
}
