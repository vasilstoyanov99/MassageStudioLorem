﻿namespace MassageStudioLorem.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using MassageStudioLorem.Models;
    using Models.Appointments;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new AppointmentInputModel()
            {
                SalonId = "1",
                ServiceId = 2
            });
        }

        [HttpPost]
        public IActionResult Index(AppointmentInputModel model)
        {
            if (model.Time == "4:00PM")
            {
                this.ModelState.AddModelError("", $"The {model.Time} hour is already booked for {model.Date}! Available hours are: ");
                return View(model);
            }

            return View(new AppointmentInputModel()
            {
                SalonId = "1",
                ServiceId = 2
            });

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