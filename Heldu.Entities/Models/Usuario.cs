using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Heldu.Entities.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        public bool Darde { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime FechaNacimiento { get; set; }
        public byte[] FotoUsuario { get; set; }
        public string IdentityUserId { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public List<Mercado> Mercados { get; set; }
        public List<GustoUsuario> Categorias { get; set; }
    }
}
