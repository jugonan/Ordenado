using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Heldu.Entities.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del producto")]
        public string Titulo { get; set; }

        [Display(Name = "Descripción del producto")]
        public string Descripcion { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        [Display(Name = "Fecha de validez de la oferta")]
        public DateTime FechaValidez { get; set; }

        [Required]
        public byte[] ImagenProducto { get; set; }

        public byte[] ImagenProducto2 { get; set; }
            
        public byte[] ImagenProducto3 { get; set; }

        [Display(Name = "Precio original del producto")]
        [DataType(DataType.Currency)]
        public double Precio { get; set; }

        [Display(Name = "Precio final del producto")]
        [DataType(DataType.Currency)]
        public string PrecioFinal { get; set; }

        [DataType(DataType.Currency)]
        public string Descuento { get; set; }

        //Cuantos cupones pueden comprar, está en string porque puede ser una frase
        public string LimiteProducto { get; set; }

        //Dónde y cómo hacerlas
        public string Reservas { get; set; }

        //Opcional
        public string InformaciónAdicional { get; set; }

        public int OpcionProductoId { get; set; }

        public List<OpcionProducto> OpcionProducto { get; set; }

        public List<Mercado> Mercados { get; set; }
        public List<ProductoCategoria> ProductoCategoria { get; set; }
        public List<ProductoVendedor> ProductoVendedor { get; set; }

        // En desuso a fecha de 29/04
        // Añadidos el 9/04
        [Display(Name = "Estado")]
        public bool Estado { get; set; }


        [Display(Name = "¿Cuántas unidades quieres anunciar?")]
        public int UnidadesStock { get; set; }

        public int CantidadVisitas { get; set; }

        // CONDICIONES DEL PRODUCTO QUE SE GESTIONARÁN EN UN STRING (JSON)
        //public List<string> Reserva { get; set; }
        //public List<string> Horario { get; set; }
        //public List<string> Entrega { get; set; }
        //public List<string> Recogida { get; set; }
        //public List<string> Otros { get; set; }
        public string Condiciones { get; set; }
    }
}
