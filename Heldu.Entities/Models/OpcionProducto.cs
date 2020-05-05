namespace Heldu.Entities.Models
{
    public class OpcionProducto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioInicial { get; set; }
        public decimal PrecioFinal { get; set; }
        public string Descuento { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

    }

}