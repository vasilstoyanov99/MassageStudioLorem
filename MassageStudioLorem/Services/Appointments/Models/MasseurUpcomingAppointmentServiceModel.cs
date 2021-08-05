namespace MassageStudioLorem.Services.Appointments.Models
{
    using System;

    public class MasseurUpcomingAppointmentServiceModel
    {
        public DateTime Date { get; set; }

        public string Hour { get; set; }

        public string MassageName { get; set; }

        public string ClientPhoneNumber { get; set; }
    }
}
