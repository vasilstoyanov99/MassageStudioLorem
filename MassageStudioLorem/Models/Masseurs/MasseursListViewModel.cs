namespace MassageStudioLorem.Models.Masseurs
{
    using Global;
    using System.Collections.Generic;

    public class MasseursListViewModel
    {
        public MasseursListViewModel() => this.CurrentPage =
            GlobalConstants.Paging.CurrentPageStart;

        public int CurrentPage { get; set; }

        public string MassageId { get; set; }

        public string CategoryId { get; set; }

        public double MaxPage { get; set; }

        public IEnumerable<MasseurViewModel> Masseurs { get; set; }
    }
}
