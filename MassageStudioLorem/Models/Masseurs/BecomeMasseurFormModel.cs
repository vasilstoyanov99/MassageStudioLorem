namespace MassageStudioLorem.Models.Masseurs
{
    using Data.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Global.GlobalConstants.ErrorMessages;
    using static Global.GlobalConstants.DataValidations;

    public class BecomeMasseurFormModel
    {
        [Required]
        [RegularExpression(UrlRegex, 
            ErrorMessage = InvalidUrl)]
        public string ProfileImageUrl { get; set; }

        [Required]
        [StringLength(MasseurDescriptionMaxLength,
             ErrorMessage = MasseurDescription),
             MinLength(MasseurDescriptionMinLength)]
        public string Description { get; set; }

        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public Gender Gender { get; set; }

        public IEnumerable<MassageCategoryViewModel> Categories
        { get; set; }

        //public ICollection<int> SelectedCategoriesIds { get; set; }
    }
}
