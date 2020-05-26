using System;
using System.Collections.Generic;
using Heldu.Entities.Models;
using Microsoft.AspNetCore.Http;

namespace Heldu.Logic.ViewModels
{
    public class ProductoCategoriaVM
    {
        public Producto Producto { get; set; }
        public Categoria Categoria { get; set; }
        public byte[] ImagenProducto { get; set; }
        public byte[] ImagenProducto2 { get; set; }
        public byte[] ImagenProducto3 { get; set; }

    }
}
