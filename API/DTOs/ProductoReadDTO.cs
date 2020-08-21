using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ProductoReadDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaValidez { get; set; }
        public DateTime FechaAltaProducto { get; set; }
        public string LimiteProducto { get; set; }
        public bool Estado { get; set; }
        public int CantidadVisitas { get; set; }
        public string Condiciones { get; set; }

    }
}
