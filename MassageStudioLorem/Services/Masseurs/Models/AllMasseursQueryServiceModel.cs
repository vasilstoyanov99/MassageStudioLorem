namespace MassageStudioLorem.Services.Masseurs.Models
{
    using System.Collections.Generic;

    using static Global.GlobalConstants.Paging;

    public class AllMasseursQueryServiceModel
    {
        public AllMasseursQueryServiceModel()
            => this.CurrentPage = CurrentPageStart;

        public int CurrentPage { get; set; }

        public double MaxPage { get; set; }

        public IEnumerable<AvailableMasseurDetailsServiceModel> Masseurs { get; set; }
    }
}
