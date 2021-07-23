namespace MassageStudioLorem.Models.Categories
{
    using System.Collections.Generic;

    using Global;

    public class AllCategoriesQueryModel
    {
        public AllCategoriesQueryModel() => this.CurrentPage = 
            GlobalConstants.Paging.CurrentPageStart;

        public string Name { get; init; }

        public int CurrentPage { get; set; }

        public int TotalCategories { get; set; }

        public IEnumerable<MassageListingViewModel> Massages 
        { get; set; }
    }
}
