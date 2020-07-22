using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Heldu.Entities.Models
{
    public class UbicacionVendedor
    {
        public int Id { get; set; }

        [Display(Name = "País")]
        [DefaultValue("España")]
        public string Pais { get; set; }

        [Display(Name = "Comunidad Autónoma")]
        public string CCAA { get; set; }

        [Display(Name = "Provincia")]
        public string Provincia { get; set; }

        [Display(Name = "Población")]
        public string Poblacion { get; set; }

        [Display(Name = "Código Postal")]
        [Required]
        public string CP { get; set; }

        [Display(Name = "Calle")]
        [Required]
        public string Calle { get; set; }

        [Display(Name = "Número")]
        [Required]
        public string Numero { get; set; }

        [Display(Name = "Piso/Letra/Otros")]
        public string Letra { get; set; }



        public int VendedorId { get; set; }
    }
}
