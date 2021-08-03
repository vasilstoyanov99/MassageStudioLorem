namespace MassageStudioLorem.Models.Appointments
{
    using Services.Appointments.Models;
    using System.Collections.Generic;

    public class AppointmentsListViewModel
    {
        public IEnumerable<AppointmentServiceModel> Appointments { get; set; }
    }
}
