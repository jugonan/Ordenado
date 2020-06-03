using System;
using System.Collections.Generic;
using System.Text;

namespace Heldu.Entities.Models
{
    public class ImagenesProducto
    {
        public int Id { get; set; }
        public byte[] Imagen1 { get; set; }
        public byte[] Imagen2 { get; set; }
        public byte[] Imagen3 { get; set; }
        public int ProductoId { get; set; }
    }
}
