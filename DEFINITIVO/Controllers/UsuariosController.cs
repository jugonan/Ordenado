using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Heldu.Entities.Models;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Microsoft.AspNetCore.Http;
using Heldu.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Heldu.Logic.ViewModels;
using System;
using Json.Net;

namespace DEFINITIVO.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuariosService _usuariosService;
        private readonly IUbicacionesUsuariosService _ubicacionesUsuarioService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IManejoProductosService _manejoProductosService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UsuariosController(IUsuariosService usuariosService,
                                  UserManager<IdentityUser> userManager,
                                  IUbicacionesUsuariosService ubicacionesUsuariosService,
                                  IManejoProductosService manejoProductosService,
                                  SignInManager<IdentityUser> signInManager)
        {
            _usuariosService = usuariosService;
            _userManager = userManager;
            _ubicacionesUsuarioService = ubicacionesUsuariosService;
            _manejoProductosService = manejoProductosService;
            _signInManager = signInManager;
        }
        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _usuariosService.GetUsuario());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Usuario usuario = await _usuariosService.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(UsuarioUbicacionVM usuarioUbicacionVM, List<IFormFile> FotoUsuario, List<IFormFile> Darde)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = new Usuario()
                {
                    Nombre = usuarioUbicacionVM.Nombre,
                    Apellido = usuarioUbicacionVM.Apellido,
                    NombreUsuario = usuarioUbicacionVM.NombreUsuario,
                    FechaNacimiento = usuarioUbicacionVM.FechaNacimiento,
                    IdentityUserId = usuarioUbicacionVM.IdentityUserId
                };
                foreach (var item in FotoUsuario)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            usuario.FotoUsuario = stream.ToArray();
                        }
                    }
                }
                foreach (var item in Darde)
                {
                    if(item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            usuario.Darde = stream.ToArray();
                        }
                    }
                }
                await _usuariosService.CreateUsuario(usuario);

                UbicacionUsuario ubicacionUsuario = new UbicacionUsuario()
                {
                    Pais = usuarioUbicacionVM.Pais,
                    CCAA = usuarioUbicacionVM.CCAA,
                    Provincia = usuarioUbicacionVM.Provincia,
                    Poblacion = usuarioUbicacionVM.Poblacion,
                    CP = usuarioUbicacionVM.CP,
                    Calle = usuarioUbicacionVM.Calle,
                    Numero = usuarioUbicacionVM.Numero,
                    Letra = usuarioUbicacionVM.Letra,
                    UsuarioId = usuario.Id
                };
                await _ubicacionesUsuarioService.CreateUbicacionUsuario(ubicacionUsuario);
            }
            return RedirectToAction("Create", "GustoUsuarios");
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Usuario usuario = await _usuariosService.EditUsuarioGet(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Usuario usuario,List<IFormFile> FotoUsuario,List<IFormFile> Darde)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            foreach (var item in FotoUsuario)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        usuario.FotoUsuario = stream.ToArray();
                    }
                }
            }
            foreach (var item in Darde)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        usuario.Darde = stream.ToArray();
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _usuariosService.EditUsuarioPost(usuario);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Usuario usuario = await _usuariosService.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _usuariosService.DeleteUsuarioPost(id);
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _usuariosService.ExistUsuario(id);
        }

        // Ningún enlace debería dirigir a esta acción porque devuelve una vista parcial.
        // Redirigir a 'Mi Cuenta'
        public async Task<IActionResult> Miperfil()
        {
            string userManagerId = _userManager.GetUserId(User);
            Usuario usuario = await _usuariosService.ObtenerUsuarioDesdedIdentity(userManagerId);
            return View(usuario);
        }

        public IActionResult Carrito()
        {
            return View();
        }

        public async Task<IActionResult> Micuenta()
        {
            string userManagerId = _userManager.GetUserId(User);
            Usuario usuario = await _usuariosService.ObtenerUsuarioConUbicacion(userManagerId);
            return View(usuario);
        }

        public async Task<IActionResult> Historico()
        {
            string userManagerId = _userManager.GetUserId(User);
            Usuario usuario = await _usuariosService.ObtenerUsuarioDesdedIdentity(userManagerId);
            ViewData["ProductosVistos"] = await _manejoProductosService.GetProductosVistosPorUsuario(usuario.Id);
            return View(usuario);
        }


        public async Task<IActionResult> Rewards()
        {
            string userManagerId = _userManager.GetUserId(User);
            Usuario usuario = await _usuariosService.ObtenerUsuarioDesdedIdentity(userManagerId);
            return View(usuario);
        }

        public async Task<IActionResult> Miscursos()
        {
            string userManagerId = _userManager.GetUserId(User);
            Usuario usuario = await _usuariosService.MisCursosUsuario(userManagerId);
            return View(usuario);
        }

        public IActionResult Desinscrito()
        {
            return View();
        }

        // Acción casi inútil que sólo devuelve la vista
        public async Task<IActionResult> Inscrito()
        {
            await _signInManager.SignOutAsync();
            return View();
        }

        public async Task<IActionResult> GestionarUsuarios()
        {
            return View(await _usuariosService.GestionarUsuarios());
        }

        public IActionResult LeerPdf()
        {
            return View();
        }

        public async Task<IActionResult> GetDarde()
        {
            string userManagerId = _userManager.GetUserId(User);
            Usuario usuario = await _usuariosService.GetUsuarioByActiveIdentityUser(userManagerId);
            byte[] darde = usuario.Darde;
            if (darde != null)
                return File(darde, "application/pdf");
            else
                return null;
        }

        // Leyendo DARDE
        private static object DetectDocument(string gcsSourceUri,
    string gcsDestinationBucketName, string gcsDestinationPrefixName)
        {
            var client = ImageAnnotatorClient.Create();

            var asyncRequest = new AsyncAnnotateFileRequest
            {
                InputConfig = new InputConfig
                {
                    GcsSource = new GcsSource
                    {
                        Uri = gcsSourceUri
                    },
                    // Supported mime_types are: 'application/pdf' and 'image/tiff'
                    MimeType = "application/pdf"
                },
                OutputConfig = new OutputConfig
                {
                    // How many pages should be grouped into each json output file.
                    BatchSize = 2,
                    GcsDestination = new GcsDestination
                    {
                        Uri = $"gs://{gcsDestinationBucketName}/{gcsDestinationPrefixName}"
                    }
                }
            };

            asyncRequest.Features.Add(new Feature
            {
                Type = Feature.Types.Type.DocumentTextDetection
            });

            List<AsyncAnnotateFileRequest> requests =
                new List<AsyncAnnotateFileRequest>();
            requests.Add(asyncRequest);

            var operation = client.AsyncBatchAnnotateFiles(requests);

            Console.WriteLine("Waiting for the operation to finish");

            operation.PollUntilCompleted();

            // Once the rquest has completed and the output has been
            // written to GCS, we can list all the output files.
            var storageClient = StorageClient.Create();

            // List objects with the given prefix.
            var blobList = storageClient.ListObjects(gcsDestinationBucketName,
                gcsDestinationPrefixName);
            Console.WriteLine("Output files:");
            foreach (var blob in blobList)
            {
                Console.WriteLine(blob.Name);
            }

            // Process the first output file from GCS.
            // Select the first JSON file from the objects in the list.
            var output = blobList.Where(x => x.Name.Contains(".json")).First();

            var jsonString = "";
            using (var stream = new MemoryStream())
            {
                storageClient.DownloadObject(output, stream);
                jsonString = System.Text.Encoding.UTF8.GetString(stream.ToArray());
            }

            var response = JsonParser.Default
                        .Parse<AnnotateFileResponse>(jsonString);

            // The actual response for the first page of the input file.
            var firstPageResponses = response.Responses[0];
            var annotation = firstPageResponses.FullTextAnnotation;

            // Here we print the full text from the first page.
            // The response contains more information:
            // annotation/pages/blocks/paragraphs/words/symbols
            // including confidence scores and bounding boxes
            Console.WriteLine($"Full text: \n {annotation.Text}");

            return 0;
        }
    }
}
