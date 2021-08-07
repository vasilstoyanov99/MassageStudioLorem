﻿namespace MassageStudioLorem.Services.Masseurs.Models
{
    using Data.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Global.GlobalConstants.Paging;

    public class AllMasseursQueryServiceModel
    {
        public AllMasseursQueryServiceModel()
        {
            this.CurrentPage = CurrentPageStart;
            this.Sorting = Gender.Both;
        }

        public int CurrentPage { get; set; }

        public double MaxPage { get; set; }

        [Display(Name = "Biological Gender")] 
        public Gender Sorting { get; set; }

        public IEnumerable<AvailableMasseurDetailsServiceModel> Masseurs
        { get; set; }
    }
}
