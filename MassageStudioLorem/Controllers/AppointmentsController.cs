namespace MassageStudioLorem.Controllers
{
    using Data;
    using Data.Models;
    using Global;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Appointments;
    using Services.Appointments;
    using Services.Appointments.Models;
    using System;
    using System.Linq;

    using static Global.GlobalConstants.ErrorMessages;

    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsService _appointmentsService;

        public AppointmentsController(IAppointmentsService appointmentsService)
            => this._appointmentsService = appointmentsService;

        [Authorize]
        public IActionResult Book([FromQuery] AppointmentIdsQueryModel query)
        {
            var appointmentModel =
                this._appointmentsService
                    .GetTheMasseurSchedule(query.MasseurId, query.MassageId);

            if (appointmentModel == null) 
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(appointmentModel);
        }

        [Authorize]
        [HttpPost]

        //TODO: Move the code to methods

        public IActionResult Book(AppointmentServiceModel query)
        {
            string massageId = query.MassageId;
            string masseurId = query.MasseurId;

            if (!this.ModelState.IsValid)
                return this.RedirectToAction("Book", new
                    {massageId, masseurId});

            var date = this._appointmentsService.ParseDate(query.Date);
            var hour = query.Hour.Trim();

            if(date == DateTime.MaxValue)
                return this.RedirectToAction
                    ("Book", new { massageId, masseurId });

            var clientId = this.User.GetId();

            var exceededBookedMassagesMessage = this._appointmentsService
                .CheckIfClientBookedTooManyMassagesInTheSameDay(date, clientId);

            if (exceededBookedMassagesMessage != null)
            {
                this.ModelState.AddModelError
                    (String.Empty, exceededBookedMassagesMessage);

                return this.View(null);
            }

            var availableHoursMessage =
                this._appointmentsService.
                    CheckIfMasseurUnavailableAndGetErrorMessage
                        (date, hour, masseurId);

            if (availableHoursMessage != null)
            {
                this.ModelState.AddModelError
                    (String.Empty, availableHoursMessage);

                return this.View(query);
            }

            this._appointmentsService.AddNewAppointment
                (clientId, masseurId, massageId, date, hour);

            return this.View();
        }
    }
}