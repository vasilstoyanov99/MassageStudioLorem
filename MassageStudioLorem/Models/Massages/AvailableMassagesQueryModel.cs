namespace MassageStudioLorem.Models.Massages
{
    using Categories;
    using System.Collections.Generic;

    using static Global.GlobalConstants.Paging;

    public class AvailableMassagesQueryModel
    {
        public AvailableMassagesQueryModel() => this.CurrentPage = CurrentPageStart;

        public int CurrentPage { get; set; }

        public double MaxPage { get; set; }

        public string CategoryId { get; init; }

        public string MasseurId { get; init; }

        public IEnumerable<MassageListingViewModel> Massages
        { get; set; }
    }
}
