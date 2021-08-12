namespace MassageStudioLorem.Data.Seeding.Data
{
    using System.Collections.Generic;

    public class CategoriesSeedData
    {
        public CategoriesSeedData()
        {
            Categories = new List<string>()
            {
                "Energizing", 
                "For Womеn",
                "Relax and Stress Relief", 
                "Pain Relief"
            };
        }

        public static IEnumerable<string> Categories { get; private set; }
    }
}
