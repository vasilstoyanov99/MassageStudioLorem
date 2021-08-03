namespace MassageStudioLorem.Models.Appointments
{
    using Services.Appointments.Models;
    using System.Collections.Generic;

    public class AppointmentsListViewModel
    {
        public ICollection<AppointmentServiceModel> Appointments { get; set; }
    }
}
