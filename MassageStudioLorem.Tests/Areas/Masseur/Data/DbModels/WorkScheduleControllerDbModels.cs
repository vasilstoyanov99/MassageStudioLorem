namespace MassageStudioLorem.Tests.Areas.Masseur.Data.DbModels
{
    using Microsoft.AspNetCore.Identity;

    using MassageStudioLorem.Data.Models;

    using static Models.WorkScheduleControllerTestModels;

    public class WorkScheduleControllerDbModels
    {
        public static Massage TestMassage => new()
        {
            Id = TestMassageData.Id,
            Name = TestMassageData.Name
        };

        public static Masseur TestMasseur => new()
        {
            FullName = TestMasseurData.FullName,
            UserId = TestMasseurData.UserId,
            Id = TestMasseurData.Id
        };

        public static Client TestClient => new()
        {
            Id = TestClientData.Id,
            UserId = TestClientData.UserId,
            FirstName = TestClientData.FirstName,
            TimeZoneOffset = 180
        };

        public static IdentityUser TestClientUser => new()
        {
            Id = TestClientData.UserId,
            UserName = TestClientUserData.UserName,
            PhoneNumber = TestClientUserData.PhoneNumber
        };

        public static IdentityUser TestMasseurUser => new()
        {
            Id = TestMasseurUserData.Id,
            UserName = TestMasseurUserData.UserName,
            PhoneNumber = TestMasseurUserData.PhoneNumber
        };

        public static Appointment TestUpcomingAppointment
            => new()
            {
                Date = UpcomingAppointmentModel.Date,
                Hour = UpcomingAppointmentModel.Hour,
                MassageName = TestMassage.Name,
                MasseurId = TestMasseur.Id,
                MassageId = TestMassage.Id,
                ClientFirstName = TestClient.FirstName,
                ClientId = TestClient.Id,
                ClientPhoneNumber = TestClientUser.PhoneNumber
            };

        public const string TestUserId = "TestId";

        public static class TestClientData
        {
            public const string FirstName = "TestClientFirstName";

            public const string Id = "TestClientId";

            public const string UserId = "TestClientUserId";
        }

        public static class TestMasseurData
        {
            public const string FullName = "TestMasseurFullName";

            public const string UserId = TestUserId;

            public const string Id = "TestMasseurId";
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

        public static class TestClientUserData
        {
            public const string UserName = "TestUserUsername";

            public const string PhoneNumber = "TestClientUserPhoneNumber";
        }

        public static class TestMasseurUserData
        {
            public const string Id = TestUserId;

            public const string UserName = "TestMasseurUserUsername";

            public const string PhoneNumber = "TestMasseurUserPhoneNumber";
        }
    }
}
