namespace MassageStudioLorem.Tests.Data.DbModels
{
    using MassageStudioLorem.Data.Enums;
    using Microsoft.AspNetCore.Identity;

    using MassageStudioLorem.Data.Models;

    using static Models.AppointmentsControllerTestModels;

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

        public const string TestUserId = "TestId";

        public static class TestClientData
        {
            public const string FirstName = "TestClientFirstName";
            public const string Id = "TestClientId";
        }

        public static class TestMasseurData
        {
            public const string FullName = "Test Masseur";
            public const string UserId = "TestMasseurUserId";
            public const string Id = "TestMasseurId";
            public const string PhoneNumber = "MasseurPhoneNumber";
            public const Gender Gender = MassageStudioLorem.Data.Enums.Gender.Male;
        }

        public static class TestCategoryData
        {
            public const string Id = "TestCategoryId";

            public const string Name = "TestCategory";
        }

        public static class TestMassageData
        {
            public const string Id = "TestMassageId";

            public const string Name = "TestMassage";

            public const double Price = 60.00;
        }

        public static class TestUpcomingAppointmentData
        {
            public const string Id = "TestUpcomingAppointmentId";

            public const string Hour = "14:00";
        }

        public static class TestPastAppointmentData
        {
            public const string Id = "TestPastAppointmentId";

            public const string Hour = "09:00";

            public const string Content = "Test Review Test Review Test Review Test Review";
        }

        public static class TestClientUserData
        {
            public const string UserName = "TestUserUsername";

            public const string PhoneNumber = "TestClientUserPhoneNumber";
        }

        public static class TestMasseurUserData
        {
            public const string Id = "MasseurUserId";

            public const string UserName = "TestMasseurUserUsername";

            public const string PhoneNumber = "TestMasseurUserPhoneNumber";
        }

        public static class TestReviewData
        {
            public const string Id = "TestReviewId";

            public const string Content = "Test Review Test Review Test Review Test Review";
        }
    }
}
