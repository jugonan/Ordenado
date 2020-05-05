using System;
namespace DEFINITIVO.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int Valoracion { get; set; }
        public string Comentario { get; set; }
        public DateTime Fecha { get; set; }
    }
}
