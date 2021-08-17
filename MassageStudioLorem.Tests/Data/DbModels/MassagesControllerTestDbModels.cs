namespace MassageStudioLorem.Tests.Data.DbModels
{
    using MassageStudioLorem.Data.Models;
    using static Global.GlobalConstants;

    public class MassagesControllerTestDbModels
    {
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

        public static Category TestCategory => new()
        {
            Id = TestCategoryData.Id,
            Name = TestCategoryData.Name
        };

        public static Masseur TestMasseur => new()
        {
            Id = TestMasseurData.Id,
            CategoryId = TestCategoryData.Id
        };
    }
}
