﻿using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Heldu.Logic.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Heldu.Logic.Services
{
    public class ProductosService : IProductosService
    {
        private readonly ApplicationDbContext _context;
        public ProductosService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Producto>> GetProductos()
        {
            return await _context.Producto
                            .Include(p => p.ProductoCategoria)
                                .ThenInclude(a => a.Categoria)
                            .Include(p => p.ProductoVendedor)
                                .ThenInclude(a => a.Vendedor)
                            .ToListAsync();
        }
        public async Task<Producto> GetProductoById(int? id)
        {
            return await _context.Producto.Include(p=>p.ProductoVendedor).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task CreateProductoPost(Producto producto)
        {
            _context.Add(producto);
            await _context.SaveChangesAsync();
        }
        public async Task<Producto> EditProductoGet(int? id)
        {
            return await _context.Producto.FindAsync(id);
        }
        public async Task EditProductoPost(Producto producto)
        {
            _context.Update(producto);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProductoPost(int id)
        {
            _context.Producto.Remove(await _context.Producto.FindAsync(id));
            await _context.SaveChangesAsync();
        }
        public bool ExistProducto(int id)
        {
            return _context.Producto.Any(e => e.Id == id);
        }
        public async Task AddCantidadVisitasProductoById(int? id)
        {
            Producto productoActual = await GetProductoById(id);
            productoActual.CantidadVisitas++;
            _context.Update(productoActual);
            await _context.SaveChangesAsync();
        }
        //Método que devuelve una lista de productos que contienen un string en su título o descripción
        public async Task<List<ProductoPrimeraOpcionProductoVM>> BuscarProductosPorString(string inputBuscar)
        {
            List<Producto> listaProductos = new List<Producto>();
            listaProductos = await GetProductos();
            List<ProductoPrimeraOpcionProductoVM> listaProductosEncontrados = new List<ProductoPrimeraOpcionProductoVM>();

            foreach (Producto producto in listaProductos)
            {
                if (producto.Titulo.ToLower().Contains(inputBuscar.ToLower()))
                {
                    ProductoPrimeraOpcionProductoVM nuevo = new ProductoPrimeraOpcionProductoVM()
                    {
                        producto = producto,
                        opcionProducto = await _context.OpcionProducto.Where(m => m.ProductoId == producto.Id).FirstOrDefaultAsync()
                    };
                    listaProductosEncontrados.Add(nuevo);
                }
                else if (producto.Descripcion != null && producto.Descripcion.ToLower().Contains(inputBuscar.ToLower()))
                {
                    ProductoPrimeraOpcionProductoVM nuevo = new ProductoPrimeraOpcionProductoVM()
                    {
                        producto = producto,
                        opcionProducto = await _context.OpcionProducto.Where(m => m.ProductoId == producto.Id).FirstOrDefaultAsync()
                    };
                    listaProductosEncontrados.Add(nuevo);
                }
            }
            return listaProductosEncontrados;
        }
        //Método que devuelve una lista de productos que contienen un string en su título o descripción para una sola categoría
        public async Task<List<ProductoPrimeraOpcionProductoVM>> BuscarProductosPorStringYCategoria(string inputBuscar, int categoriaId)
        {
            List<Producto> listaProductos = new List<Producto>();
            listaProductos = await GetProductosByCategoriaId(categoriaId);
            List<ProductoPrimeraOpcionProductoVM> listaProductosEncontrados = new List<ProductoPrimeraOpcionProductoVM>();

            foreach (Producto producto in listaProductos)
            {
                if (producto.Titulo.ToLower().Contains(inputBuscar.ToLower()) || producto.Descripcion.ToLower().Contains(inputBuscar.ToLower()))
                {
                    ProductoPrimeraOpcionProductoVM nuevo = new ProductoPrimeraOpcionProductoVM()
                    {
                        producto = producto,
                        opcionProducto = await _context.OpcionProducto.Where(m => m.ProductoId == producto.Id).FirstOrDefaultAsync()
                    };
                    listaProductosEncontrados.Add(nuevo);
                }
            }
            return listaProductosEncontrados;
        }
        //Listar productos para una categoria determinada
        public async Task<List<Producto>> GetProductosByCategoriaId(int categoriaId)
        {
            List<Producto> productos = new List<Producto>();
            List<ProductoCategoria> productoCategorias = await _context.ProductoCategoria.Where(x => x.CategoriaId == categoriaId).ToListAsync();

            foreach (ProductoCategoria productoCategoria in productoCategorias)
            {
                int idProducto = productoCategoria.ProductoId;
                Producto nuevoProducto = _context.Producto.Find(idProducto);
                if (nuevoProducto != null)
                {
                    productos.Add(nuevoProducto);
                }
                //Solo descomentar el "else" para limpiar la tabla ProductoCategoria de ProductoId "null" cuando se hayan borrardo esos productos
                //    else
                //    {
                //        _context.ProductoCategoria.Remove(productoCategoria);
                //    }
                //}
                //await _context.SaveChangesAsync();
            }
            return productos;
        }
        //Método que devuelve n listas de Productos por Categoria.
        public async Task<ProductosForIndex2VM> GetProductosForIndex2(List<Categoria> listaCategorias, List<ProductoCategoria> listaProductosCategorias)
        {
            List<OpcionProducto> opcionesProductos = await _context.OpcionProducto.ToListAsync();
            ProductosForIndex2VM listasProductosForIndex2 = new ProductosForIndex2VM();
            listasProductosForIndex2.ListasProductos = new List<List<ProductoPrimeraOpcionProductoVM>>();

            foreach (Categoria categoria in listaCategorias)
            {
                List<ProductoPrimeraOpcionProductoVM> newListaProducto = new List<ProductoPrimeraOpcionProductoVM>();
                foreach (ProductoCategoria productoCategoria in listaProductosCategorias)
                {
                    if (productoCategoria.CategoriaId == categoria.Id)
                    {
                        OpcionProducto opcion = opcionesProductos.Where(m => m.ProductoId == productoCategoria.ProductoId).FirstOrDefault();
                        ProductoPrimeraOpcionProductoVM productoConOpcion = new ProductoPrimeraOpcionProductoVM()
                        {
                            producto = productoCategoria.Producto,
                            opcionProducto = opcion
                        };
                        newListaProducto.Add(productoConOpcion);
                    }
                }
                listasProductosForIndex2.ListasProductos.Add(newListaProducto);
            }
            return listasProductosForIndex2;
        }
        public async Task<ProductosForIndex2VM> GetProductosForIndex2()
        {
            
            List<ProductoCategoria> listaProductosCategorias = await _context.ProductoCategoria.ToListAsync();
            List<Producto> listaProductos = await GetProductos();
            List<Categoria> listaCategorias = await _context.Categoria.ToListAsync();

            ProductosForIndex2VM listasProductosForIndex2 = new ProductosForIndex2VM();
            listasProductosForIndex2.ListasProductos = new List<List<ProductoPrimeraOpcionProductoVM>>();

            foreach (Categoria categoria in listaCategorias)
            {
                List<ProductoPrimeraOpcionProductoVM> newListaProducto = new List<ProductoPrimeraOpcionProductoVM>();
                foreach (ProductoCategoria productoCategoria in listaProductosCategorias)
                {
                    if (productoCategoria.CategoriaId == categoria.Id)
                    {
                        OpcionProducto opcion = await _context.OpcionProducto.Where(m => m.ProductoId == productoCategoria.ProductoId).FirstOrDefaultAsync();
                        ProductoPrimeraOpcionProductoVM productoConOpcion = new ProductoPrimeraOpcionProductoVM()
                        {
                            producto = productoCategoria.Producto,
                            opcionProducto = opcion
                        };
                        newListaProducto.Add(productoConOpcion);
                    }
                }
                listasProductosForIndex2.ListasProductos.Add(newListaProducto);
            }
            return listasProductosForIndex2;
        }

        public async Task<byte[]> AgregarImagenesBlob(List<IFormFile> ImagenProducto)
        {
            byte[] toAdd = new byte[] { };
            foreach (var item in ImagenProducto)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        toAdd = stream.ToArray();
                    }
                }
            }
            return toAdd;
        }
    }
}
