﻿using MechanicCompany.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace MechanicCompany.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender, IConfiguration configuration)
        {
            _logger = logger;
            _emailSender = emailSender;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            return View();
        }

        public IActionResult Contact()
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            return View();
        }

        [HttpPost]
        public IActionResult SendEmail()
        {
            var CompanyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = CompanyMail;
            var ClientAdress = HttpContext.Request.Form["YourAddress"].FirstOrDefault();
            var Subject = HttpContext.Request.Form["Subject"].FirstOrDefault();
            var Body = "<div style='width: 70%; float: center'><center>" +
                "<img src='https://i.imgur.com/JgvLADt.png' alt='Mechanic Company' height='99' width='300'/><hr>" +
                "<p></p><p></p><p>" + ClientAdress + " send email to you: </p>" +
                "<p></p><p></p><p>" + HttpContext.Request.Form["Body"].FirstOrDefault() + "</p><hr>" +
                "<p>Mechanic Company</p><p>Siewna 28, 42-201 Częstochowa</p><p>(48) 869 268 456</p><p>mikolaj.otreba@o2.pl</p>" +
                "</center></div>";
            _emailSender.SendEmailAsync(CompanyMail, Subject, Body);
            return RedirectToAction("EmailSend");
        }

        public IActionResult EmailSend()
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
