using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heldu.Entities.Models
{
    public class Condicion
    {
        public int Id { get; set; }
        public string Reserva { get; set; }
        public string Horario { get; set; }
        public string Entrega { get; set; }
        public string Recogida { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        [NotMapped]
        public List<string> Otros { get; set; }
    }
}
