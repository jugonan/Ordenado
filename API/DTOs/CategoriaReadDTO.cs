using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace API.DTOs
{
    public class CategoriaReadDTO : Controller
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
