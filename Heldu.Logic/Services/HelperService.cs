using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Heldu.Logic.Interfaces;

namespace Heldu.Logic.Services
{
    public class HelperService : IHelperService
    {
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
    }
}
