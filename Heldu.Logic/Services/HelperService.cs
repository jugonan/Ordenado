﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Heldu.Database.Data;
using Heldu.Entities.Models;
using Heldu.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Heldu.Logic.Services
{
    public class HelperService : IHelperService
    {
        private readonly ApplicationDbContext _context;
        public HelperService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Genero una lista de 6 índices random entre 0 y la cantidad de productos de una categoría
        public List<int> RandomProductos(int largo)
        {
            Random random = new Random();
            List<int> RandomProd = new List<int>();
            if (largo > 5)
            {
                while (RandomProd.Count() < 6)
                {
                    bool existe = false;
                    int nuevo = random.Next(0, largo);
                    foreach (int item in RandomProd)
                    {
                        if (nuevo == item)
                        {
                            existe = true;
                        }
                    }
                    if (!existe)
                    {
                        RandomProd.Add(nuevo);
                    }
                }
                return RandomProd;
            }
            else
            {
                return RandomProd;
            }
        }

        public void EnviarEmail(string asunto, string mensaje)
        {
            {
                // Your hard-coded email values (where the email will be sent from), this could be define in a config file, etc.
                var email = "heldubbk@gmail.com";
                var password = "visualstudio";

                // Your target (you may want to ensure that you have a property within your form so that you know who to send the email to
                string address = "galo130@gmail.com";

                // Builds a message and necessary credentials
                var loginInfo = new NetworkCredential(email, password);
                var msg = new MailMessage();
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);

                // This email will be sent from you
                msg.From = new MailAddress(email);
                // Your target email address
                msg.To.Add(new MailAddress(address));
                msg.Subject = asunto;
                // Build the body of your email using the Body property of your message
                msg.Body = mensaje;

                // Wires up and send the email
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);
            }
        }

        //Devuelve una lista random de reviews hechos hacia sus productos
        public async Task<List<Review>> ObtenerReviewsVendedor(Vendedor vendedor)
        {
            List<Review> reviewsTotales = await _context.Review.ToListAsync();
            //Genero una lista vacía a devolver con los reviews que le iré añadiendo después
            List<Review> reviewsHaciaVendedor = new List<Review>();
            if (vendedor.ProductoVendedor.Count != 0)
            {
                foreach (Review review in reviewsTotales)
                {
                    //Añado los productoId que coinciden de reviews con la lista que de Productos de este vendedor.
                    //Esta lista está en vendedor.ProductoVendedor y es única
                    foreach (ProductoVendedor productoVendedor in vendedor.ProductoVendedor)
                    {
                        if (review.ProductoId == productoVendedor.ProductoId)
                        {
                            reviewsHaciaVendedor.Add(review);
                        }
                    }
                }
                //Si tiene más de 3 reviews hago un random 
                if (reviewsHaciaVendedor.Count > 3)
                {
                    Random rnd = new Random();
                    for (int j = 0; j < 3; j++)
                    {
                        int longitud = reviewsTotales.Count;
                        int random = rnd.Next(0, longitud - 1);
                        reviewsHaciaVendedor.Add(reviewsTotales[random]);
                        reviewsTotales.Remove(reviewsTotales[random]);
                    }
                }
            }
            return reviewsHaciaVendedor;
        }

        public async Task<int> ObtenerMediaReviewsParaVendedor(Vendedor vendedor)
        {
            List<Review> reviews = await _context.Review.ToListAsync();
            int media;
            List<Review> reviewsProductosVendedor = new List<Review>();
            foreach (Review review in reviews)
            {
                foreach (ProductoVendedor productoVendedor in vendedor.ProductoVendedor)
                {
                    if (productoVendedor.ProductoId == review.ProductoId)
                    {
                        reviewsProductosVendedor.Add(review);
                    }
                }
            }
            int sumaTotal = 0;
            if (reviewsProductosVendedor.Count != 0)
            {

                foreach (Review review in reviewsProductosVendedor)
                {
                    sumaTotal += review.Valoracion;
                }
                media = sumaTotal / reviewsProductosVendedor.Count;
            }
            else
            {
                media = 0;
            }
            return media;
        }

        //Devuelve la Dirección IP
        public string GetIPv6Address()
        {
            var hostName = Dns.GetHostName();
            Ping ping = new Ping();
            var replay = ping.Send(hostName);

            if (replay.Status == IPStatus.Success)
            {
                return replay.Address.ToString();
            }
            return null;
        }
        public string GetIPv4LANAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var direcciones = (from ip in host.AddressList where ip.AddressFamily == AddressFamily.InterNetwork select ip.ToString()).ToList();
            return direcciones[0];
        }
        public string GetIPv4EthernetAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var direcciones = (from ip in host.AddressList where ip.AddressFamily == AddressFamily.InterNetwork select ip.ToString()).ToList();
            if (direcciones[1] != null)
                return direcciones[1];
            else
                return null;
        }

        //Devuelve un objeto con toda la información que se puede obtener de una IP
        public async Task<object> GetIPJson()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://icanhazip.com/");
            var publicIP2 = await response.Content.ReadAsStringAsync();
            string publicIP = publicIP2.Remove(publicIP2.Length - 1);
            string urlInicial = "http://api.ipapi.com/";
            string APIKey = "?access_key=42709e692e4b6c4269941c7827ee7a01";
            string urlToFetch = $"{urlInicial}{publicIP}{APIKey}";

            var response2 = await client.GetStringAsync(urlToFetch);

            var data = JsonConvert.DeserializeObject(response2);

            return data;
        }
        //Devuelve "Ciudad, Región" de acuerdo a la IP otorgada por el ISP Provider
        public async Task<string> GetCityAndRegionFromIP()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage responseIP = await client.GetAsync("http://icanhazip.com/");
            var responseString = await responseIP.Content.ReadAsStringAsync();
            string publicIP = responseString.Remove(responseString.Length - 1);
            string urlInicial = "http://api.ipapi.com/";
            string APIKey = "?access_key=42709e692e4b6c4269941c7827ee7a01";
            string urlToFetch = $"{urlInicial}{publicIP}{APIKey}";
            var response = await client.GetStringAsync(urlToFetch);

            using var jsonDoc = JsonDocument.Parse(response);
            var root = jsonDoc.RootElement;
            var myCity = root.GetProperty("city").GetString();    // Obtiene el string de la llave "city" del JSON
            var myRegion = root.GetProperty("region_name").GetString();    // Obtiene el string de la llave "region_name" del JSON

            return $"{myCity}, {myRegion}";
        }
        public async Task<string> GetPostalCodeFromIP()
        {
            using HttpClient client = new HttpClient();
            //Obtiene la IP del usuario
            HttpResponseMessage responseIP = await client.GetAsync("http://icanhazip.com/");
            var responseString = await responseIP.Content.ReadAsStringAsync();
            string publicIP = responseString.Remove(responseString.Length - 1);

            //Obtiene los datos de ubicación de acuerdo a la IP
            string urlInicial = "http://api.ipapi.com/";
            string APIKey = "?access_key=42709e692e4b6c4269941c7827ee7a01";
            string urlToFetch = $"{urlInicial}{publicIP}{APIKey}";
            var response = await client.GetStringAsync(urlToFetch);

            using var jsonDoc = JsonDocument.Parse(response);
            var root = jsonDoc.RootElement;
            var myPostalCode = root.GetProperty("zip").GetString();    // Obtiene el string de la llave "zip" del JSON

            return myPostalCode;
        }

        //Obtener el nombre de una ciudad desde un código postal (Solo para España)
        public async Task<string> GetCityFromPostalCode(string postalCode)
        {
            string city = "N/A Ciudad";
            int cp = Convert.ToInt32(postalCode);
            if (cp > 1001 && cp < 52080)
            {
                string url = $"http://api.zippopotam.us/ES/{cp}";
                using HttpClient client = new HttpClient();

                var request = await client.GetAsync(url);
                if (request.IsSuccessStatusCode)
                {
                    var response2 = await client.GetStringAsync(url);
                    using var jsonDoc2 = JsonDocument.Parse(response2);
                    var root = jsonDoc2.RootElement;
                    var root2 = root.GetProperty("places");
                    city = root2[0].GetProperty("place name").ToString();
                }
            }

            return city;
        }
    }
}
