using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriasController : Controller
    {
        private readonly ICategoriasService _categoriasService;

        public CategoriasController(ICategoriasService categoriasService)
        {
            _categoriasService = categoriasService;
        }

        // GET: /<controller>/
        [Route("")]
        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> GetAllCategorias()
        {
            List<Categoria> listaCategorias = await _categoriasService.GetCategorias();

            return Ok(listaCategorias);
        }

        [Route("Categoria/{id}")]
        [HttpGet("{id}", Name = "GetCategoriaById")]
        public async Task<ActionResult<Categoria>> GetCategoriaById(int id)
        {
            Categoria categoria = await _categoriasService.GetCategoriaById(id);

            if (categoria == null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }
    }
}
