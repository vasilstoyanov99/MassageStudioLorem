namespace MassageStudioLorem.Services.Masseurs.Models
{
    using Data.Enums;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
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
