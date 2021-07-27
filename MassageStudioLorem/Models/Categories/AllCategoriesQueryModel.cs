namespace MassageStudioLorem.Models.Categories
{
    using System.Collections.Generic;

    using static Global.GlobalConstants.Paging;

    public class AllCategoriesQueryModel
    {
        public AllCategoriesQueryModel() => this.CurrentPage = CurrentPageStart;

        public string Id { get; init; }

        public string Name { get; init; }

        public int CurrentPage { get; set; }

        public int TotalCategories { get; set; }

        public IEnumerable<MassageListingViewModel> Massages 
        { get; set; }
    }
}
