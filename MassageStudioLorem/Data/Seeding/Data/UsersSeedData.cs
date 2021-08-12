namespace MassageStudioLorem.Data.Seeding.Data
{
    public class UsersSeedData
    {
        public const string DefaultPassword = "123456";

        public class MaleMasseur
        {
            public const string Email = "brayden@lorem.com";
            public const string FirstName = "Brayden";
            public const string Username = "Brayden";
            public const string PhoneNumber = "0888888888";
            public const string Password = DefaultPassword;
        }

        public class FemaleMasseur
        {
            public const string Email = "natalie@lorem.com";
            public const string FirstName = "Natalie";
            public const string Username = "Natalie";
            public const string PhoneNumber = "0999999999";
            public const string Password = DefaultPassword;
        }

        public class Client
        {
            public const string Email = "client@lorem.com";
            public const string FirstName = "Client";
            public const string Username = "Client";
            public const string PhoneNumber = "0777777777";
            public const string Password = DefaultPassword;
        }

        public class Admin
        {
            public const string Email = "admin@lorem.com";
            public const string Username = "BestAdminEver";
            public const string Password = DefaultPassword;
        }
    }
}
