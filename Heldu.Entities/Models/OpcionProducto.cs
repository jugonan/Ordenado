using System.ComponentModel.DataAnnotations;

namespace Heldu.Entities.Models
{
    public class OpcionProducto
    {
        public int Id { get; set; }

        [Display(Name = "Descripción del producto")]
        public string Descripcion { get; set; }

        [Display(Name = "Precio original del producto")]
        [DataType(DataType.Currency)]
        public float PrecioInicial { get; set; }

        [Display(Name = "Precio final del producto")]
        [DataType(DataType.Currency)]
        public float PrecioFinal { get; set; }

        [DataType(DataType.Currency)]
        public string Descuento { get; set; }

        [Display(Name = "Unidades de stock inicial")]
        public int StockInicial { get; set; }

        [Display(Name = "Cantidad vendida")]
        public int CantidadVendida { get; set; }



        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

    }

}