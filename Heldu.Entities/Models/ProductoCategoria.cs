﻿using System;
namespace Heldu.Entities.Models
{
    public class ProductoCategoria
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
