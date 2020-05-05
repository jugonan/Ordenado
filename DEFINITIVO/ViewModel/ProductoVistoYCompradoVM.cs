using System.Collections.Generic;
using Heldu.Entities.Models;

namespace DEFINITIVO.ViewModel
{
    public class ProductoVistoYCompradoVM
    {
        public List<Producto> Vistos { get; set; }
        public List<Producto> Comprados { get; set; }
        public Usuario Usuario;
    }
}
