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
        public ImagenesProducto ImagenesProducto { get; set; }

    }
}
