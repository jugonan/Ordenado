using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class OpcionProductoReadDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public float PrecioInicial { get; set; }
        public float PrecioFinal { get; set; }
        public string Descuento { get; set; }
        public int StockInicial { get; set; }
        public int CantidadVendida { get; set; }
    }
}
