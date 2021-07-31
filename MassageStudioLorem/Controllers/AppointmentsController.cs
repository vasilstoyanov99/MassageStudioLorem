namespace MassageStudioLorem.Controllers
{
    using Data;
    using Global;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Appointments;
    using Services.Appointments;
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
            var appointmentFormModel =
                this._appointmentsService
                    .GetTheMasseurSchedule(query.MasseurId, query.MassageId);

            if (appointmentFormModel == null) 
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(appointmentFormModel);
        }

        [Authorize]
        [HttpPost]

        //Use IsModelStateValid!
        public IActionResult Book([FromQuery] AppointmentFormModel query)
        {
            return this.View();
        }
    }
}