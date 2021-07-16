namespace MassageStudioLorem.Controllers
{
    using Data;
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using MassageStudioLorem.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Appointments;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly LoremDbContext _data;

        public HomeController(LoremDbContext data) => this._data = data;

        public IActionResult Index()
        {
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
            var check = this._data.MasseursBookedHours
                .FirstOrDefault(x => x.MasseurId == model.MasseurId /*&& x.Date == model.Date*/ && x.Hour == model.Hour);

            if (check != null)
            {
                this.ModelState.AddModelError("", $"The {model.Hour} hour is already booked for {model.Date}! Available hours are: ");
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