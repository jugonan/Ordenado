using System;
using System.Collections.Generic;

namespace DEFINITIVO.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<GustoUsuario> GustoUsuario { get; set; }
        public List<ProductoCategoria> ProductoCategoria { get; set; }
    }
}
