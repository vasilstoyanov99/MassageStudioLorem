namespace MassageStudioLorem.Models.Categories
{
    using System.Collections.Generic;

    public class AllCategoriesViewModel
    {
        public string Name { get; init; }

        public ICollection<MassageListingViewModel> Massages 
        { get; set; }
    }
}
