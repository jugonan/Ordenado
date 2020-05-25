using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heldu.Entities.Models
{
    public class Condicion
    {
        public int Id { get; set; }
        public List<string> Reserva { get; set; }
        public List<string> Horario { get; set; }
        public List<string> Entrega { get; set; }
        public List<string> Recogida { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public List<string> Otros { get; set; }
    }
}
