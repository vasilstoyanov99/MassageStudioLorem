namespace MassageStudioLorem.Tests.Data.DbModels
{
    using MassageStudioLorem.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using static Global.GlobalConstants;

    public class MasseursControllerTestDbModels
    {
        public static Category TestCategory => new()
        {
            Id = TestCategoryData.Id,
            Name = TestCategoryData.Name
        };

        public static IdentityUser TestUser => new()
        {
            Id = TestUserId,
            UserName = "TestUserUsername"
        };

        public static Client TestClient => new()
        {
            FirstName = TestClientData.FirstName,
            UserId = TestUserId,
            Id = "TestClientId"
        };

        public static IdentityRole MasseurRole => new()
        {
            Name = "Masseur",
            NormalizedName = "MASSEUR",
            Id = "MasseurRoleId"
        };

        public static Masseur TestMasseur => new()
        {
            Description = DummyDescription,
            CategoryId = TestCategoryData.Id,
            FullName = TestMasseurData.FullName,
            Gender = TestMasseurData.Gender,
            UserId = TestMasseurData.UserId,
            ProfileImageUrl = TestImageUrl,
            Id = TestMasseurData.Id
        };

        public static IdentityUser TestMasseurUser => new()
        {
            PhoneNumber = TestMasseurData.PhoneNumber,
            UserName = "MasseurUserName",
            Id = TestMasseurData.UserId
        };

        public static Massage TestMassage => new()
        {
            Id = TestMassageData.Id,
            ImageUrl = TestImageUrl,
            Price = TestMassageData.Price,
            ShortDescription = DummyDescription,
            LongDescription = DummyDescription,
            CategoryId = TestCategoryData.Id,
            Name = TestMassageData.Name
        };
    }
}
