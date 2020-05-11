using System;
using System.Collections.Generic;

namespace Heldu.Entities.Models
{
    public class Ubicacion
    {
        public int Id { get; set; }
        public string Pais { get; set; }
        public string CCAA { get; set; }
        public string Provincia { get; set; }
        public string Poblacion { get; set; }
        public string CP { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Letra { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public List<Vendedor> Vendedores { get; set; }
    }
}
