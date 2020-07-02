using System;
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

        public CheckoutController(IProductosService productosService, IUsuariosService usuariosService, UserManager<IdentityUser> userManager)
        {
            _productosService = productosService;
            _usuariosService = usuariosService;
            _userManager = userManager;
        }

        [Authorize(Roles = "cliente")]
        public async Task<IActionResult> Confirmar(int? id)
        {
            Producto producto = await _productosService.GetProductoById(id);
            Usuario usuario = await _usuariosService.GetUsuarioByActiveIdentityUser(_userManager.GetUserId(User));
            var opcionProducto = "opcion";

            UsuarioProductoVM modelo = new UsuarioProductoVM
            {
                producto = producto,
                usuario = usuario,
                opcion = opcionProducto
            };
            int precioFinal = Convert.ToInt32(Convert.ToDouble(producto.PrecioFinal) * 100);
            int helduFee = Convert.ToInt32(precioFinal * 0.22);

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
                    { "UsuarioId", Convert.ToString(usuario.Id) },
                    { "Usuario", usuario.NombreUsuario },
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

        public async Task<IActionResult> Pagar(string stripeToken)
        {
            StripeConfiguration.ApiKey = "sk_test_51GvJEQL9UURBAADxXJtmn6ZmPepnp0Bkt4Hwl3y53I7rjWCQKa4wj3FSfkm2V4ZOIV67I6LQDmfvPmZ16eMh9LcE0057FViwnl";

            return View();
        }

    }
}