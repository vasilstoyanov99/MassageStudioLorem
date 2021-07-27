namespace MassageStudioLorem.Models.Masseurs
{
    using System.Collections.Generic;

    using static Global.GlobalConstants.Paging;

    public class AllMasseursViewModel
    {
        public AllMasseursViewModel() => this.CurrentPage = CurrentPageStart;

        public int CurrentPage { get; set; }

        public double MaxPage { get; set; }

        public IEnumerable<MasseurDetailsViewModel> Masseurs { get; set; }
    }
}
