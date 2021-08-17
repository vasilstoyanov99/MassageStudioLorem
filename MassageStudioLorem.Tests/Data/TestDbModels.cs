namespace MassageStudioLorem.Tests.Data
{
    using MassageStudioLorem.Data.Enums;
    using MassageStudioLorem.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using static Global.GlobalConstants;

    public class TestDbModels
    {
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
            PhoneNumber = DummyPhoneNumber,
            UserName = "MasseurUserName",
            Id = TestMasseurData.UserId
        };

        public static Client TestClient => new()
        {
            TimeZoneOffset = 180,
            FirstName = TestClientData.FirstName,
            UserId = TestClientData.UserId,
            Id = TestClientData.Id
        };

        public static Category TestCategory => new()
        {
            Id = TestCategoryData.Id, 
            Name = TestCategoryData.Name
        };

        public static Massage TestMassage => new()
        {
            Id = TestMassageData.Id,
            ImageUrl = TestImageUrl,
            Price = 60.00,
            ShortDescription = DummyDescription,
            LongDescription = DummyDescription,
            CategoryId = TestCategoryData.Id,
            Name = TestMassageData.Name
        };

        //public static IdentityRole MasseurRole() => new()
        //{
        //    Name = "Masseur"
        //};

        //public static IdentityUser TestUser => new()
        //{
        //    Id = "TestId",
        //    UserName = "TestUsername"
        //};
}
}
