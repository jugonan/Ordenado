﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Heldu.Entities.Models;

namespace Heldu.Logic.Interfaces
{
    public interface IHelperService
    {
        public List<int> RandomProductos(int largo);
        public List<int> RandomList(int cantidadValores, int limiteSuperior);
        public void EnviarEmail(string asunto, string mensaje);
        public Task<List<Review>> ObtenerReviewsVendedor(Vendedor vendedor);
        public Task<int> ObtenerMediaReviewsParaVendedor(Vendedor vendedor);
        public string GetIPv6Address();
        public string GetIPv4LANAddress();
        public string GetIPv4EthernetAddress();
        public Task<object> GetIPJson();
        public Task<string> GetCityAndRegionFromIP();
        public Task<string> GetPostalCodeFromIP();
        public Task<string> GetCityFromPostalCode(string postalCode);
        public Task LoadProductsTask();
    }
}
