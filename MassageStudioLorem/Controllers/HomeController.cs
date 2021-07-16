namespace MassageStudioLorem.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using MassageStudioLorem.Models;
    using Models.Appointments;
    using Data;
    using Global;
    using System;
    using System.Text;

    public class HomeController : Controller
    {
        private readonly LoremDbContext _data;

        public HomeController(LoremDbContext data) => this._data = data;

        public IActionResult Index()
        {
            if (DefaultTimeSchedule.TimeSchedule is null)
            {
                DefaultTimeSchedule.SeedTimeTable();
            }

            var masseur = this._data.Masseurs.FirstOrDefault(x => x.FirstName == "test");

            return View(new AppointmentInputModel()
            {
                MasseurId = masseur.Id,
                MassageId = 1
            });
        }

        [HttpPost]
        public IActionResult Index(AppointmentInputModel model)
        {
            var check = this._data.MasseursAvailableHours
                .FirstOrDefault(x => x.MasseurId == model.MasseurId /*&& x.Date == model.Date*/ && x.Hour == model.Hour);

            if (check == null)
            {
                var availableHours = this._data
                    .MasseursAvailableHours
                    .Where(x => x.MasseurId == model.MasseurId /*&&
                                x.Date == model.Date*/)
                    .Select(x => new {x.Hour})
                    .ToList();

                var stringBuilder = new StringBuilder();

                foreach (var hour in availableHours)
                {
                    stringBuilder.Append($"{hour.Hour} ");
                }

                this.ModelState.AddModelError("", $"The {model.Hour} hour is already booked for {model.Date}! Available hours are: {stringBuilder.ToString().TrimEnd()}");

                return View(model);
            }

            var masseur = this._data.Masseurs.FirstOrDefault(x => x.FirstName == "test");

            return View(new AppointmentInputModel()
            {
                MasseurId = masseur.Id,
                MassageId = 1
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