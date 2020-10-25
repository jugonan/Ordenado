using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using Heldu.Entities.Models;



namespace API.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            //Source --> Target
            CreateMap<Categoria, CategoriaReadDTO>();
        }
    }
}
