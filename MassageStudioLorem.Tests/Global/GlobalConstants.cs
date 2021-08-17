namespace MassageStudioLorem.Tests.Global
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GlobalConstants
    {
        public const string DummyDescription =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut leo quam, eleifend sit amet faucibus non, tempus et purus. Nullam nunc leo, varius vitae mauris ut, semper bibendum sapien. Quisque ut dapibus dolor. Nulla facilisi. Vivamus nunc mauris, pharetra eu orci in, auctor hendrerit lacus. Ut eget eros sed urna aliquet vestibulum. Morbi non lacus a mauris faucibus elementum a et quam.";

        public const string TestImageUrl = "https://i.imgur.com/9NfF4Cw.png";

        public const string TestUserId = "TestId";

        public const string HomeActionName = "Index";

        public const string HomeControllerName = "Home";

        public static class TestClientData
        {
            public const string FirstName = "TestClientFirstName";
            public const string UserId = TestUserId;
            public const string Id = "TestClientId";
        }

        public static class TestMasseurData
        {
            public const string FullName = "Test Masseur";
            public const string UserId = TestUserId;
            public const string Id = "TestMasseurId";
        }

        public static class TestUserData
        {
            public const string Username = "TestUsername";
        }
    }
}
