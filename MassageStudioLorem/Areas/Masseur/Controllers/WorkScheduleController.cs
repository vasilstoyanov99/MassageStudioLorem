namespace MassageStudioLorem.Areas.Masseur.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Services.Appointments;
    using System;
    using static Global.GlobalConstants.ErrorMessages;

    public class WorkScheduleController : MasseurController
    {
        private readonly IAppointmentsService _appointmentsService;

        public WorkScheduleController(IAppointmentsService appointmentsService)
            => this._appointmentsService = appointmentsService;

        public IActionResult Index()
        {
            var userId = this.User.GetId();

            var upcomingAppointmentsModels = this._appointmentsService
                .GetMasseurUpcomingAppointments(userId);

            if (upcomingAppointmentsModels == null)
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(upcomingAppointmentsModels);
        }
    }
}
