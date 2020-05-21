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
using Heldu.Logic.Services;
using System.Net;
using System.Net.Http;

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
        private readonly GeoLocationService _geoLocationService;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            MessagesService messagesService,
            IHelperService helperService,
            IUsuariosService usuariosService,
            GeoLocationService geoLocationService)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _messagesService = messagesService;
            _helperService = helperService;
            _usuariosService = usuariosService;
            _geoLocationService = geoLocationService;
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
                    //Le enviamos al inicio
                    return RedirectToAction("Index2", "Productos");
                }
            }
        }
        public IActionResult Privacy()
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
        public async Task<IActionResult> FAQ()
        {
            //<!-- PROBANDO EL GeoIP2 para comproabar el pais de la IP-->
            // Do the lookup
            string ip = "128.101.101.101";
            string ip99 = "188.114.110.136";

            //var myIPv6 = _helperService.GetIPv6Address();
            //var myIPv4LAN = _helperService.GetIPv4LANAddress();
            //var myIPv4Ethernet = _helperService.GetIPv4EthernetAddress();

            //var response1 = await _geoLocationService.GetCountryNameAsync(ip);
            //var response2 = await _geoLocationService.GetCountryNameAsync(myIPv4LAN);

            //HttpClient client = new HttpClient();
            //HttpResponseMessage response = await client.GetAsync("http://icanhazip.com/");
            //var publicIP2 = await response.Content.ReadAsStringAsync();
            //string publicIP = publicIP2.Remove(publicIP2.Length-1);
            //var response3 = await _geoLocationService.GetCountryNameAsync(publicIP);
            
            var remoteIp = HttpContext.Connection.RemoteIpAddress;

            var response3 = await _geoLocationService.GetCountryNameAsync(ip99);



            Console.WriteLine(response3);           // 'United States'
            //<!-- FIN DE PROBANDO EL GeoIP2 para comproabar el pais de la IP-->
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contacto(string userId, string nombre, string email, string tel, string mensaje, string actualPath)
        {
            Usuario usuario = await _usuariosService.GetUsuarioByActiveIdentityUser(userId);
            string asunto = "[No especificado] ha enviado un mensaje desde la web";
            if (!String.IsNullOrEmpty(nombre))
            {
                asunto = $"{nombre} ha enviado un mensaje desde la web";
            }

            string mensajeCompuesto = "Mensaje vacío. Revisar el código.";
            if (usuario != null)
            {
                mensajeCompuesto = $"El usuario {usuario.Nombre} {usuario.Apellido} (ID: {usuario.Id}) envía el siguiente mensaje:" +
                    $"\n\b{mensaje}" +
                    $"\n" +
                    $"\nCon los siguientes datos de contacto:" +
                    $"\n\bE-Mail: {email}   |  Teléfono: {tel}" +
                    $"\n" +
                    $"\n Mensaje enviado el {DateTime.Now} desde la URL: {actualPath}";
            }
            else
            {
                mensajeCompuesto = $"Un usuario no logueado/registrado envía el siguiente mensaje" +
                    $"\n\b{mensaje}" +
                    $"\n" +
                    $"\nCon los siguientes datos de contacto:" +
                    $"\n\bE-Mail: {email}   |  Teléfono: {tel}" +
                    $"\n" +
                    $"\n Mensaje enviado el {DateTime.Now} desde la URL: {actualPath}";
            }

            try
            {
                _helperService.EnviarEmail(asunto, mensajeCompuesto);
                _messagesService.SetShowMessage(true);
                _messagesService.SetMessage($"Su mensaje ha sido enviado, gracias por contactarnos! Le responderemos a la brevedad");
                return RedirectToAction("Index2", "Productos");
            }
            catch (Exception)
            {
                _messagesService.SetShowMessage(true);
                _messagesService.SetMessage($"No hemos podido gestionar su mensaje. Por favor contáctenos a: heldubbk@gmail.com. Gracias!");
                return RedirectToAction("Index2", "Productos");
                throw;
            }

        }
    }
}
