namespace MassageStudioLorem.Controllers
{
    using Data;
    using Global;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Appointments;
    using System;
    using System.Linq;

    public class AppointmentsController : Controller
    {
        private readonly LoremDbContext _data;

        public AppointmentsController(LoremDbContext data) => this._data = data;

        [Authorize]
        public IActionResult Book([FromQuery] AppointmentIdsQueryModel query)
        {
            if (DefaultTimeSchedule.TimeSchedule == null)
            {
                DefaultTimeSchedule.SeedTimeTable();
            }

            var massage = this._data
                .Massages
                .FirstOrDefault(m => m.Id == query.MassageId);

            if (String.IsNullOrEmpty(query.MassageId) || massage == null)
            {
                return this.RedirectToAction("All", "Categories");
            }

            var masseur = this._data
                .Masseurs
                .FirstOrDefault(m => m.UserId == query.MasseurId);

            if (String.IsNullOrEmpty(query.MasseurId) || masseur == null)
            {
                // TODO: do it better
                return this.RedirectToAction("All", "Categories");
            }

            return this.View(new AppointmentFormModel()
            {
                MassageName = massage.Name,
                MasseurFirstAndLastName = masseur.FirstName + " " + masseur.LastName,
                MassageId = query.MassageId,
                MasseurId = query.MasseurId
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Book([FromQuery] AppointmentFormModel query)
        {
            ;
            return this.View();
        }
    }
}