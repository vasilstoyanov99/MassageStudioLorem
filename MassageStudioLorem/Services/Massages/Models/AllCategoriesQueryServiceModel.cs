namespace MassageStudioLorem.Services.Massages.Models
{
    using System.Collections.Generic;

    using static Global.GlobalConstants.Paging;

    public class AllCategoriesQueryServiceModel
    {
        public AllCategoriesQueryServiceModel()
            => this.CurrentPage = CurrentPageStart;

        public string Id { get; init; }

        public string Name { get; init; }

        public int CurrentPage { get; set; }

        public int TotalCategories { get; set; }

        public IEnumerable<MassageListingServiceModel> Massages
        { get; init; }
    }
}
