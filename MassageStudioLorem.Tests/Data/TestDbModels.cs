namespace MassageStudioLorem.Tests.Data
{
    using Global;
    using MassageStudioLorem.Data.Enums;
    using MassageStudioLorem.Data.Models;
    using static Global.GlobalConstants;

    public class TestDbModels
    {
        public static Masseur DummyMasseur => new()
        {
            Description = DummyDescription,
            CategoryId = "TestCategoryId",
            FullName = TestMasseurData.FullName,
            Gender = Gender.Female,
            UserId = TestUserId,
            ProfileImageUrl = TestImageUrl,
            Id = TestMasseurData.Id
        };

        public static Client DummyClient => new()
        {
            TimeZoneOffset = 180,
            FirstName = TestClientData.FirstName,
            UserId = TestClientData.UserId,
            Id = TestClientData.Id
        };
    }
}
