namespace MassageStudioLorem.Tests.Areas.Masseur.Data.DbModels
{
    using Microsoft.AspNetCore.Identity;

    using MassageStudioLorem.Data.Models;

    public class HomeControllerTestDbModels
    {
        public static Masseur TestMasseur => new()
        {
            FullName = TestMasseurData.FullName,    
            UserId = TestMasseurData.UserId,
            Id = TestMasseurData.Id
        };

        public static class TestMasseurData
        {
            public const string FullName = "Test Masseur";
            public const string UserId = TestMasseurUserData.Id;
            public const string Id = "TestMasseurId";
        }

        public static IdentityUser TestMasseurUser => new()
        {
            Id = TestMasseurData.UserId
        };

        public static class TestMasseurUserData
        {
            public const string Id = "TestId";
        }
    }
}
