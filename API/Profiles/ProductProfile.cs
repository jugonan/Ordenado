using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using Heldu.Entities.Models;
using Heldu.Logic.ViewModels;

namespace API.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            //Source --> Target
            CreateMap<Producto, ProductoReadDTO>();
            CreateMap<OpcionProducto, OpcionProductoReadDTO>();
        }

    }
}
