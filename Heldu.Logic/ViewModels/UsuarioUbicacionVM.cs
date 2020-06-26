using System;
namespace Heldu.Logic.ViewModels
{
    public class UsuarioUbicacionVM
    {
        // Parte correspodiente al usuario
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public byte[] Darde { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public byte[] FotoUsuario { get; set; }
        public string IdentityUserId { get; set; }
        // Parte correspondiente a la ubicación
        public string Pais { get; set; }
        public string CCAA { get; set; }
        public string Provincia { get; set; }
        public string Poblacion { get; set; }
        public string CP { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Letra { get; set; }
    }
}
