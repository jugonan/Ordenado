using Heldu.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heldu.Logic.ViewModels
{
    public class UsuarioProductoVM
    {
        public Producto producto { get; set; }
        public Usuario usuario { get; set; }
        public string opcion { get; set; }
    }
}
