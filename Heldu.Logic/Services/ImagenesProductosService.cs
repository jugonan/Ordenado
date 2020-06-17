using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Heldu.Logic.Services
{
    public class ImagenesProductosService : IImagenesProductosService
    {
        private readonly ApplicationDbContext _context;
        public ImagenesProductosService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ImagenesProducto> GetAllImages(int? productoID)
        {
            var imagenes = await _context.ImagenesProducto.FirstOrDefaultAsync(x => x.ProductoId == productoID);
            return imagenes;
        }
        public async Task<byte[]> GetMainImage(int productoID)
        {
            var imagen = await _context.ImagenesProducto.FirstOrDefaultAsync(x => x.ProductoId == productoID);
            if (imagen != null)
                return imagen.Imagen1;
            else
                return null;
        }
        public async Task<byte[]> GetSecondImage(int productoID)
        {
            var imagen = await _context.ImagenesProducto.FirstOrDefaultAsync(x => x.ProductoId == productoID);
            return imagen.Imagen2;
        }
        public async Task<byte[]> GetThirdImage(int productoID)
        {
            var imagen = await _context.ImagenesProducto.FirstOrDefaultAsync(x => x.ProductoId == productoID);
            return imagen.Imagen3;
        }
        public async Task CreateImagenesProducto(ImagenesProducto imagenes)
        {
            _context.Add(imagenes);
            await _context.SaveChangesAsync();
        }
        public async Task EditImagenes(ImagenesProducto imagenesEdit)
        {
            ImagenesProducto imagenesNew = new ImagenesProducto();
            imagenesNew.ProductoId = imagenesEdit.ProductoId;
            var imagenesOld = await GetAllImages(imagenesEdit.ProductoId);
            if (imagenesEdit.Imagen1 != null)
            {
                imagenesNew.Imagen1 = imagenesEdit.Imagen1;
            }
            else
            {
                imagenesNew.Imagen1 = imagenesOld.Imagen1;
            }
            if (imagenesEdit.Imagen2 != null)
            {
                imagenesNew.Imagen2 = imagenesEdit.Imagen2;
            }
            else
            {
                imagenesNew.Imagen2 = imagenesOld.Imagen2;
            }
            if (imagenesEdit.Imagen3 != null)
            {
                imagenesNew.Imagen3 = imagenesEdit.Imagen3;
            }
            else
            {
                imagenesNew.Imagen3 = imagenesOld.Imagen3;
            }

            if (imagenesOld != null)
            {
                _context.ImagenesProducto.Remove(imagenesOld);
            }
            _context.ImagenesProducto.Add(imagenesNew);
            await _context.SaveChangesAsync();
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
