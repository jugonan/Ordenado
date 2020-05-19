using System;
namespace Heldu.Entities.Models
{
    public class Visita
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public DateTime FechaVisita { get; set; }
        public int Unidades { get; set; }
    }
}
