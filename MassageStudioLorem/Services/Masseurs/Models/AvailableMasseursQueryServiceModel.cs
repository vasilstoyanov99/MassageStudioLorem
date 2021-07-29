namespace MassageStudioLorem.Services.Masseurs.Models
{
    using System.Collections.Generic;

    using static Global.GlobalConstants.Paging;

    public class AvailableMasseursQueryServiceModel
    {
        public AvailableMasseursQueryServiceModel()
            => this.CurrentPage = CurrentPageStart;

        public int CurrentPage { get; set; }

        public string MassageId { get; set; }

        public string CategoryId { get; set; }

        public double MaxPage { get; set; }

        public IEnumerable<AvailableMasseurListingServiceModel> Masseurs
        { get; set; }
    }
}
