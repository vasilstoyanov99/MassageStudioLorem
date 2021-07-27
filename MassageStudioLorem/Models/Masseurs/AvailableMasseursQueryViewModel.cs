namespace MassageStudioLorem.Models.Masseurs
{
    using static Global.GlobalConstants.Paging;

    using System.Collections.Generic;

    public class AvailableMasseursListQueryViewModel
    {
        public AvailableMasseursListQueryViewModel() => this.CurrentPage = 
            CurrentPageStart;

        public int CurrentPage { get; set; }

        public string MassageId { get; set; }

        public string CategoryId { get; set; }

        public double MaxPage { get; set; }

        public IEnumerable<AvailableMasseurListingViewModel> Masseurs
        { get; set; }
    }
}
