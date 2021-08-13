namespace MassageStudioLorem.Models.Appointments
{
    using System.Collections.Generic;

    using Services.Appointments.Models;

    public class AppointmentsListViewModel
    {
        public IEnumerable<UpcomingAppointmentServiceModel> UpcomingAppointments
        { get; set; }

        public IEnumerable<PastAppointmentServiceModel> PastAppointments
        { get; set; }
    }
}
