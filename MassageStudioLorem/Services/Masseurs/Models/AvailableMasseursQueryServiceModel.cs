namespace MassageStudioLorem.Services.Masseurs.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Data.Enums;

    using static Global.GlobalConstants.Paging;

    public class AvailableMasseursQueryServiceModel
    {
        public AvailableMasseursQueryServiceModel()
        {
            this.CurrentPage = CurrentPageStart;
            this.Sorting = Gender.Both;
        }

        public int CurrentPage { get; set; }

        public string MassageId { get; set; }

        public string CategoryId { get; set; }

        public double MaxPage { get; set; }

        [Display(Name = "Biological Gender")]
        public Gender Sorting { get; set; }

        public IEnumerable<AvailableMasseurListingServiceModel> Masseurs
        { get; set; }
    }
}
