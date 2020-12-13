using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using website.Models;

namespace website.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Email(string name, string phone, string email, string message)
        {
            var fromAddress = new MailAddress("yasser@cosmosglobalsolutions.com", "Website Contact");
            var toAddressess = new List<MailAddress>
            {
                //new MailAddress("sean@cosmosglobalsolutions.com", "Sean Bennett"),
                //new MailAddress("bennett.sean@gmail.com", "Sean Bennett"),
                new MailAddress("yasser@cosmosglobalsolutions.com", "Yasser Hussein"),
                //new MailAddress("sean@cosomosglobalsolutions.com", "Sean Bennett"),
                //new MailAddress("to@example.com", "To Name")
            };
            const string fromPassword = "Dragon201)";
            const string subject = "New Website Inquiry";
            string body = $"{name}\n{phone}\n{email}\n{message}";

            var smtp = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            foreach(var toAddress in toAddressess)
            {
                using (var mailMessage = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    try
                    {
                        smtp.Send(mailMessage);
                    }
                    catch (Exception)
                    {
                        return StatusCode(500);
                    }
                    
                }
            }
            
            return Ok();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
