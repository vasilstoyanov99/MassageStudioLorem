namespace MassageStudioLorem.Data.Seeding
{
    using System.Linq;

    public class CategoryIdGetter
    {
        public string GetEnergizingCategoryId(LoremDbContext data)
            => GetId(data, "Energizing");

        public string GetForWomеnCategoryId(LoremDbContext data)
            => GetId(data, "For Womеn");

        public string GetRelaxAndStressReliefCategoryId(LoremDbContext data)
            => GetId(data, "Relax and Stress Relief");

        public string GetPainReliefCategoryId(LoremDbContext data)
            => GetId(data, "Pain Relief");

        private static string GetId(LoremDbContext data, string categoryName)
            => data.Categories
                .FirstOrDefault(c => c.Name == categoryName)?.Id;
    }
}
