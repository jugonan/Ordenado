using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Heldu.Entities.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del producto")]
        public string Titulo { get; set; }

        [Display(Name = "Descripción del producto")]
        public string Descripcion { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de validez de la oferta")]
        public DateTime FechaValidez { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaAltaProducto { get; set; }



        public string LimiteProducto { get; set; }

        [Display(Name = "Estado")]
        public bool Estado { get; set; }
        public int CantidadVisitas { get; set; }
        public string Condiciones { get; set; }



        public ImagenesProducto ImagenesProducto { get; set; }
        public List<OpcionProducto> OpcionProducto { get; set; }
        public List<ProductoCategoria> ProductoCategoria { get; set; }
        public List<ProductoVendedor> ProductoVendedor { get; set; }
    }
}
