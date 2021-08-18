namespace MassageStudioLorem.Tests.Global
{
    using MassageStudioLorem.Data.Enums;

    public class GlobalConstants
    {
        public const string DummyDescription =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut leo quam, eleifend sit amet faucibus non, tempus et purus. Nullam nunc leo, varius vitae mauris ut, semper bibendum sapien. Quisque ut dapibus dolor. Nulla facilisi. Vivamus nunc mauris, pharetra eu orci in, auctor hendrerit lacus. Ut eget eros sed urna aliquet vestibulum. Morbi non lacus a mauris faucibus elementum a et quam.";

        public const string TestImageUrl = "https://i.imgur.com/9NfF4Cw.png";

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
    }
}
