using System;

namespace Heldu.Entities.Models
{
    public class Mercado
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
