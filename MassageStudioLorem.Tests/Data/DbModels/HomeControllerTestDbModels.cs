namespace MassageStudioLorem.Tests.Data.DbModels
{
    using MassageStudioLorem.Data.Enums;
    using MassageStudioLorem.Data.Models;

    public class HomeControllerTestDbModels
    {
        public static Client TestClient => new()
        {
            FirstName = TestClientData.FirstName,
            UserId = TestUserId,
            Id = TestClientData.Id
        };

        public static Masseur TestMasseur => new()
        {
            Id = TestMasseurData.Id,
            UserId = TestUserId
        };

        public const string HomeActionName = "Index";

        public const string HomeControllerName = "Home";

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
        }

        public static class TestMasseurUserData
        {
            public const string Id = "MasseurUserId";

            public const string UserName = "TestMasseurUserUsername";
        }

        public static class TestReviewData
        {
            public const string Id = "TestReviewId";

            public const string Content = "Test Review Test Review Test Review Test Review";
        }
    }
}
