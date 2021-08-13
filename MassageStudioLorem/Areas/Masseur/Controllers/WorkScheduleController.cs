namespace MassageStudioLorem.Areas.Masseur.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure;
    using Services.Appointments;

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

            if (CheckIfNull(upcomingAppointmentsModels))
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(upcomingAppointmentsModels);
        }

        private static bool CheckIfNull(object obj)
            => obj == null;
    }
}
