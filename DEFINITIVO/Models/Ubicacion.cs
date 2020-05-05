using System;
using System.Collections.Generic;

namespace DEFINITIVO.Models
{
    public class Ubicacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public List<Vendedor> Vendedores { get; set; }
    }
}
