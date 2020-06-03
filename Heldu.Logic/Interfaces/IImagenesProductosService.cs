using Heldu.Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heldu.Logic.Interfaces
{
    public interface IImagenesProductosService
    {
        public Task<ImagenesProducto> GetAllImages(int id);
        public Task<byte[]> GetMainImage(int id);
        public Task<byte[]> GetSecondImage(int id);
        public Task<byte[]> GetThirdImage(int id);
        public Task<byte[]> AgregarImagenesBlob(List<IFormFile> ImagenProducto);
        public Task CreateImagenesProducto(ImagenesProducto imagenes);
        public Task EditImagenes(ImagenesProducto imagenes);
    }
}
