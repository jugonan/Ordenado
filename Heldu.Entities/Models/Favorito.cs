using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heldu.Entities.Models
{
    public class Favorito
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public DateTime FechaMeGusta { get; set; }

    }
}
