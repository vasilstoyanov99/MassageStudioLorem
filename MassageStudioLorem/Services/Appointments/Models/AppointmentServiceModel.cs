namespace MassageStudioLorem.Services.Appointments.Models
{
    using System;

    public class AppointmentServiceModel
    {
        public string Id { get; set; }

        public DateTime Date { get; set; }

        public string Hour { get; set; }

        public string MasseurFullName { get; set; }

        public string MasseurPhoneNumber { get; set; }

        public string MassageName { get; set; }

        public bool? IsUserReviewedMasseur { get; set; }
    }
}
