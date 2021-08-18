namespace MassageStudioLorem.Tests.Areas.Admin.Controllers.Data.DbModels
{
    using Microsoft.AspNetCore.Identity;

    public class HomeControllerTestDbModels
    {
        public static IdentityUser TestAdminUser => new()
        {
            Id = TestIdentityUserData.Id,
            UserName = TestIdentityUserData.Username
        };

        public static class TestIdentityUserData
        {
            public const string Id = "TestId";
            public const string Username = "TestUsername";
        }
    }
}
