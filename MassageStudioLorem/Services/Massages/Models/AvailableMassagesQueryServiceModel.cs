namespace MassageStudioLorem.Services.Massages.Models
{
    using System.Collections.Generic;

    using static Global.GlobalConstants.Paging;

    public class AvailableMassagesQueryServiceModel
    {
        public AvailableMassagesQueryServiceModel()
            => this.CurrentPage = CurrentPageStart;

        public int CurrentPage { get; set; }

        public double MaxPage { get; set; }

        public string CategoryId { get; set; }

        public string MasseurId { get; set; }

        public IEnumerable<MassageListingServiceModel> Massages { get; init; }
    }
}