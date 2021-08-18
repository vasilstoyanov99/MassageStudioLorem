namespace MassageStudioLorem.Tests.Areas.Masseur.Data.Models
{
    using System;

    using Services.Appointments.Models;

    using static DbModels.WorkScheduleControllerDbModels;

    public class WorkScheduleControllerTestModels
    {
        public static MasseurUpcomingAppointmentServiceModel UpcomingAppointmentModel
            => new()
        {
            Date = DateTime.Parse("20.8.2100 г. 4:53:54"),
            Hour = TestUpcomingAppointmentData.Hour,
            MassageName = TestMassage.Name,
            ClientFirstName = TestClient.FirstName,
            ClientPhoneNumber = TestClientUser.PhoneNumber
        };
    }
}
