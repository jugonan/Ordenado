using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Heldu.Entities.Models
{
    public class Vendedor
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Nombre de la empresa (*)")]
        public string NombreDeEmpresa { get; set; }

        [Required]
        [Display(Name = "Nº de tiendas (*)")]
        public string NumeroTiendas { get; set; }

        [Required]
        [Display(Name = "Calle en la que se ubica (*)")]
        public string Direccion { get; set; }

        [Required]
        [Display(Name = "Ciudad (*)")]
        public string Ciudad { get; set; }

        [Required]
        [Display(Name = "C.P. (*)")]
        public string CodigoPostal { get; set; }

        [Required]
        [Display(Name = "Pagina web (*)")]
        [DataType(DataType.Url)]
        public string Paginaweb { get; set; }

        [Required]
        [Display(Name = "Telefono de contacto (*)")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        public string DescripcionEmpresa { get; set; }

        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        public int UbicacionId { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public List<ProductoVendedor> ProductoVendedor { get; set; }
    }
}
