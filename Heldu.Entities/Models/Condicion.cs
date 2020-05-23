using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heldu.Entities.Models
{
    public class Condicion
    {
        public int Id { get; set; }
        [NotMapped]
        public List<string> Reserva { get; set; }
        [NotMapped]
        public List<string> Horario { get; set; }
        [NotMapped]
        public List<string> Entrega { get; set; }
        [NotMapped]
        public List<string> Recogida { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        [NotMapped]
        public List<string> Otros { get; set; }
    }
}
