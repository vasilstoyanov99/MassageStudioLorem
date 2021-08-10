namespace MassageStudioLorem.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Appointments;
    using Services.Appointments;
    using Services.Appointments.Models;
    using System;
    using static Global.GlobalConstants.ErrorMessages;
    using static Areas.Client.ClientConstants;

    [Authorize(Roles = ClientRoleName)]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsService _appointmentsService;

        public AppointmentsController(IAppointmentsService appointmentsService)
            => this._appointmentsService = appointmentsService;

        public IActionResult Index()
        {
            var userId = this.User.GetId();

            var upcomingAppointmentsModels = 
                this._appointmentsService.GetUpcomingAppointments(userId);

            var pastAppointments = this._appointmentsService
                .GetPastAppointments(userId);

            return this.View(new AppointmentsListViewModel()
            {
                UpcomingAppointments = upcomingAppointmentsModels,
                PastAppointments = pastAppointments
            });
        }

        public IActionResult CancelAppointment(string appointmentId)
        {
            var cancelAppointmentModel = this._appointmentsService
                .GetAppointment(appointmentId);

            if (cancelAppointmentModel == null)
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(cancelAppointmentModel);
        }

        public IActionResult DeleteAppointment(string appointmentId)
        {
            if (!this._appointmentsService
                .CheckIfAppointmentIsDeletedSuccessfully(appointmentId))
            {
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);
                return this.View("CancelAppointment", null);
            }

            return this.RedirectToAction("Index");
        }

        public IActionResult Book([FromQuery] AppointmentIdsQueryModel query)
        {
            var appointmentModel =
                this._appointmentsService
                    .GetTheMasseurSchedule(query.MasseurId, query.MassageId);

            if (appointmentModel == null) 
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(appointmentModel);
        }

        [HttpPost]
        public IActionResult Book(BookAppointmentServiceModel query)
        {
            //TODO: Move the code to methods

            var massageId = query.MassageId;
            var masseurId = query.MasseurId;
            var userId = this.User.GetId();

            if (!this.ModelState.IsValid)
                return this.RedirectToAction("Book", new
                    {massageId, masseurId});

            var date = this._appointmentsService.ParseDate(query.Date);
            var hour = query.Hour.Trim();

            if(date == DateTime.MaxValue)
                return this.RedirectToAction
                    ("Book", new { massageId, masseurId });

            var exceededBookedMassagesMessage = this._appointmentsService
                .CheckIfClientBookedTooManyMassagesInTheSameDay(date, userId);

            if (exceededBookedMassagesMessage != null)
            {
                this.ModelState.AddModelError
                    (String.Empty, exceededBookedMassagesMessage);

                return this.View(null);
            }

            if (this._appointmentsService.CheckIfClientTryingToBookAPastTime
                (date, hour))
            {
                this.ModelState.AddModelError(String.Empty, CannotBookInThePast);

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
                (userId, masseurId, massageId, date, hour);

            return this.RedirectToAction("Index");
        }

        //TODO: Check if it needs to be used!
        //private bool CheckIfNull(object obj)
        //    => obj == null;
    }
}