namespace MassageStudioLorem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesSeedData
    {
        public CategoriesSeedData()
        {
            Categories = new List<string>()
            {
                "Energizing", 
                "For Womеn", 
                "Relax and Stress Relieve", 
                "Pain Relief"
            };
        }

        public static IEnumerable<string> Categories { get; private set; }
    }
}
