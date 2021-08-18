namespace MassageStudioLorem.Tests.Data.Models
{
    using System;

    using MassageStudioLorem.Data.Models;
    using Services.Appointments.Models;
    using System.Collections.Generic;
    using static Global.GlobalConstants;
    using static DbModels.AppointmentsControllerTestDbModels;

    public class AppointmentsControllerTestModels
    {
        public static UpcomingAppointmentServiceModel UpcomingAppointment => new()
        {
            Id = TestUpcomingAppointmentData.Id,
            Date = DateTime.Parse("20.8.2100 г. 4:53:54"),
            Hour = TestUpcomingAppointmentData.Hour,
            MassageName = TestMassage.Name,
            MasseurFullName = TestMasseur.FullName,
            MasseurPhoneNumber = TestMasseurUser.PhoneNumber
        };

        public static PastAppointmentServiceModel PastAppointment => new()
        {
            Id = TestPastAppointmentData.Id,
            Date = DateTime.Parse("16.8.2021 г. 4:53:54"),
            Hour = TestPastAppointmentData.Hour,
            ClientId = TestClient.Id,
            IsUserReviewedMasseur = false,
            MassageName = TestMassage.Name,
            MasseurId = TestMasseur.Id,
            MasseurFullName = TestMasseur.FullName,
            MasseurPhoneNumber = TestMasseurUser.PhoneNumber
        };

        public static BookAppointmentServiceModel BookAppointmentData => new()
        {
            MasseurId = TestMasseur.Id,
            MasseurFullName = TestMasseur.FullName,
            MassageId = TestMassage.Id,
            MassageName = TestMassage.Name,
            ClientTimeZoneOffset = -TestClient.TimeZoneOffset,
            Date = "19-08-2100",
            Hour = "17:00",
            WorkHours = new List<string>()
        };

        public static Appointment ExpectedBookedAppointment => new()
        {
            MasseurId = TestMasseur.Id,
            MasseurFullName = TestMasseur.FullName,
            MasseurPhoneNumber = TestMasseurUser.PhoneNumber,
            MassageId = TestMassage.Id,
            MassageName = TestMassage.Name,
            ClientFirstName = TestClient.FirstName,
            ClientId = TestClient.Id,
            ClientPhoneNumber = TestClientUser.PhoneNumber,
            IsUserReviewedMasseur = false,
            Date = DateTime.Parse("19-08-2100 17:00"),
            Hour = ExpectedBookedAppointment.Hour
        };
    }
}
