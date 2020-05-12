using System;
namespace Heldu.Logic.ViewModels
{
    public class VendedorUbicacionVM
    {
        // Model de vendedor
        public int VendedorId { get; set; }
        public string NombreDeEmpresa { get; set; }
        public string NumeroTiendas { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public string Paginaweb { get; set; }
        public string Telefono { get; set; }
        public string DescripcionEmpresa { get; set; }
        public string IdentityUserId { get; set; }
        // Model de ubicación
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
