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
        [Display(Name = "Telefono de contacto (*)")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        [Required]
        [Display(Name = "Pagina web (*)")]
        [DataType(DataType.Url)]
        public string Paginaweb { get; set; }

        [Required]
        [Display(Name = "CIF (*)")]
        public string CIF { get; set; }
        
        [Required]
        [Display(Name ="IBAN (*)")]
        public string IBAN { get; set; }

        [Required]
        [Display(Name ="Fee Heldu")]
        [MaxLength(2)]
        public int Fee { get; set; }
        public string DescripcionEmpresa { get; set; }


        public UbicacionVendedor UbicacionVendedor { get; set; }

        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public List<ProductoVendedor> ProductoVendedor { get; set; }
    }
}
