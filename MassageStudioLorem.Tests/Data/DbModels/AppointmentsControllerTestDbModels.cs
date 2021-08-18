namespace MassageStudioLorem.Tests.Data.DbModels
{
    using Microsoft.AspNetCore.Identity;

    using MassageStudioLorem.Data.Models;
    using System;
    using System.Collections.Generic;
    using static Global.GlobalConstants;
    using static Data.Models.AppointmentsControllerTestModels;

    public class AppointmentsControllerTestDbModels
    {
        public static Massage TestMassage => new()
        {
            Id = TestMassageData.Id,
            CategoryId = TestCategoryData.Id,
            Name = TestMassageData.Name
        };

        public static Masseur TestMasseur => new()
        {
            CategoryId = TestCategoryData.Id,
            FullName = TestMasseurData.FullName,
            UserId = TestMasseurData.UserId,
            Id = TestMasseurData.Id
        };

        public static Client TestClient => new()
        {
            Id = TestClientData.Id,
            UserId = TestUserId,
            FirstName = TestClientData.FirstName,
            TimeZoneOffset = 180
        };

        public static IdentityUser TestClientUser => new()
        {
            Id = TestUserId,
            UserName = TestClientUserData.UserName,
            PhoneNumber = TestClientUserData.PhoneNumber
        };

        public static IdentityUser TestMasseurUser => new()
        {
            Id = TestMasseurUserData.Id,
            UserName = TestMasseurUserData.UserName,
            PhoneNumber = TestMasseurUserData.PhoneNumber
        };

        public static (Appointment pastAppointment, Appointment upcomingAppointment)
            GetAppointments()
        {
            Appointment pastAppointment = new()
            {
                    Id = PastAppointment.Id,
                    Date = PastAppointment.Date,
                    Hour = PastAppointment.Hour,
                    MassageName = TestMassage.Name,
                    MasseurFullName = TestMasseur.FullName,
                    MasseurPhoneNumber = TestMasseurUser.PhoneNumber,
                    MasseurId = TestMasseur.Id,
                    MassageId = TestMassage.Id,
                    ClientFirstName = TestClient.FirstName,
                    ClientId = TestClient.Id,
                    ClientPhoneNumber = TestClientUser.PhoneNumber,
                    IsUserReviewedMasseur = false,
                };

            var upcomingAppointment = GetUpcomingAppointment();

            return (pastAppointment, upcomingAppointment);
        }

        public static Appointment GetUpcomingAppointment()
            => new()
            {
                Id = UpcomingAppointment.Id,
                Date = UpcomingAppointment.Date,
                Hour = UpcomingAppointment.Hour,
                MassageName = TestMassage.Name,
                MasseurFullName = TestMasseur.FullName,
                MasseurPhoneNumber = TestMasseurUser.PhoneNumber,
                MasseurId = TestMasseur.Id,
                MassageId = TestMassage.Id,
                ClientFirstName = TestClient.FirstName,
                ClientId = TestClient.Id,
                ClientPhoneNumber = TestClientUser.PhoneNumber,
                IsUserReviewedMasseur = false,
            };

}
}
