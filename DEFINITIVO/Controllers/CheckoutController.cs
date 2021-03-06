﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Heldu.Logic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace DEFINITIVO.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IProductosService _productosService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUsuariosService _usuariosService;
        private readonly IVendedoresService _vendedoresService;
        private readonly IOpcionesProductosService _opcionesProductosService;
        private readonly IVisitasService _visitasService;

        public CheckoutController(IProductosService productosService,
                                  IUsuariosService usuariosService,
                                  UserManager<IdentityUser> userManager,
                                  IVendedoresService vendedoresService,
                                  IOpcionesProductosService opcionesProductosService,
                                  IVisitasService visitasService)
        {
            _productosService = productosService;
            _usuariosService = usuariosService;
            _userManager = userManager;
            _vendedoresService = vendedoresService;
            _opcionesProductosService = opcionesProductosService;
            _visitasService = visitasService;
        }


        [Authorize(Roles = "cliente")]
        public async Task<IActionResult> Confirmar(int? id, int opcionElegida)
        {
            Usuario usuario = await _usuariosService.GetUsuarioByActiveIdentityUser(_userManager.GetUserId(User));
            Producto producto = await _productosService.GetProductoById(id);
            List<OpcionProducto> opciones = await _opcionesProductosService.GetOpcionProductoById(id);
            OpcionProducto opcion = opciones[opcionElegida - 1];
            ProductoVendedor productoVendedor = producto.ProductoVendedor[0];
            Vendedor vendedor = await _vendedoresService.GetVendedorById(productoVendedor.VendedorId);

            UsuarioProductoVM modelo = new UsuarioProductoVM
            {
                producto = producto,
                usuario = usuario,
                opcion = opcion
            };
            int precioFinal = Convert.ToInt32(opcion.PrecioFinal * 100);
            float fee = vendedor.Fee;
            int helduFee = Convert.ToInt32(precioFinal * (fee / 100));

            StripeConfiguration.ApiKey = "sk_test_51GvJEQL9UURBAADxXJtmn6ZmPepnp0Bkt4Hwl3y53I7rjWCQKa4wj3FSfkm2V4ZOIV67I6LQDmfvPmZ16eMh9LcE0057FViwnl";

            var service = new PaymentIntentService();
            var createOptions = new PaymentIntentCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                Amount = precioFinal,
                Currency = "eur",
                ApplicationFeeAmount = helduFee,
                ReceiptEmail = "galo130@gmail.com",

                Metadata = new Dictionary<string, string>
                {
                    { "ProductoId", Convert.ToString(producto.Id)},
                    { "Producto", producto.Titulo },
                    { "OpcionId", opcion.Id.ToString() },
                    { "Opcion", opcion.Descripcion},
                    { "PrecioOriginal", opcion.PrecioInicial.ToString() },
                    { "UsuarioId", Convert.ToString(usuario.Id) },
                    { "Usuario", usuario.NombreUsuario },
                    { "VendedorId", vendedor.Id.ToString() },
                    { "Vendedor", vendedor.NombreDeEmpresa }
                },
                TransferData = new PaymentIntentTransferDataOptions() { Destination = "acct_1H08zjLhTBm3kv5q" },
                //TransferData = new PaymentIntentTransferDataOptions
                //{
                //     Destination = "acct_1H08zjLhTBm3kv5q",
                //},
            };
            var intent = service.Create(createOptions);
            ViewData["ClientSecret"] = intent.ClientSecret;

            return View(modelo);
        }

        public IActionResult Pagar(string stripeToken)
        {
            StripeConfiguration.ApiKey = "sk_test_51GvJEQL9UURBAADxXJtmn6ZmPepnp0Bkt4Hwl3y53I7rjWCQKa4wj3FSfkm2V4ZOIV67I6LQDmfvPmZ16eMh9LcE0057FViwnl";

            return View();
        }

        //Cuando el checkout es exitoso (se procesa el pago), se guarda la transacción en la tabla Visita con Cantidad=1
        public async Task<IActionResult> Comprado(int? productoId, int opcionId, int usuarioId)
        {
            Vendedor vendedor = await _vendedoresService.ObtenerVendedorDesdeProducto(productoId);
            Visita compra = new Visita()
            {
                FechaVisita = DateTime.Now,
                OpcionProductoId = opcionId,
                ProductoId = Convert.ToInt32(productoId),
                UsuarioId = usuarioId,
                VendedorId = vendedor.Id,
                Unidades = 1
            };
            await _visitasService.CreateVisita(compra);

            return RedirectToAction("MiCuenta", "Usuarios");
        }
    }
}