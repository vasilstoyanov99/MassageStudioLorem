namespace MassageStudioLorem.Models.Appointments
{
    using Services.Appointments.Models;
    using System.Collections.Generic;

    public class AppointmentsListViewModel
    {
        public IEnumerable<UpcomingAppointmentServiceModel> UpcomingAppointments
        { get; set; }

        public IEnumerable<PastAppointmentServiceModel> PastAppointments
        { get; set; }
    }
}
