﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DEFINITIVO.Services;

namespace DEFINITIVO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly HelperService _helperService;
        private readonly MessagesService _messagesService;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            HelperService helperService,
            MessagesService messagesService)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _helperService = helperService;
            _messagesService = messagesService;
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
        public IActionResult FAQ()
        {
            return View();
        }
        //[HttpGet]
        //public IActionResult Contacto()
        //{
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contacto(string userId, string nombre, string email, string tel, string mensaje)
        {
            Usuario usuario = await _helperService.ObtenerUsuario(userId);
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
                    $"\n Mensaje enviado el {DateTime.Now}";
            }
            else
            {
                mensajeCompuesto = $"Un usuario no logueado/registrado envía el siguiente mensaje" +
                    $"\n\b{mensaje}" +
                    $"\n" +
                    $"\nCon los siguientes datos de contacto:" +
                    $"\n\bE-Mail: {email}   |  Teléfono: {tel}" +
                    $"\n" +
                    $"\n Mensaje enviado el {DateTime.Now}";
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
                return View();
                throw;
            }

        }
    }
}