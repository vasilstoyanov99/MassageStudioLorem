namespace MassageStudioLorem.Tests.Data.DbModels
{
    using MassageStudioLorem.Data.Models;
    using static Global.GlobalConstants;

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
    }
}
