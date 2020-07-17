using Heldu.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heldu.Logic.ViewModels
{
    public class ProductoDetallesVM
    {
        public Producto producto { get; set; }
        public Vendedor vendedor { get; set; }
        public List<OpcionProducto> opcionesProducto { get; set; }

        public List<Review> reviews { get; set; }
        public int totalComentarios { get; set; }
        public int valoracionMedia { get; set; }

    }
}
