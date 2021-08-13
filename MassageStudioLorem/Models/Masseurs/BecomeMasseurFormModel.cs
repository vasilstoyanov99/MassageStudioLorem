namespace MassageStudioLorem.Models.Masseurs
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Data.Enums;
    using Services.Masseurs.Models;

    using static Global.GlobalConstants.ErrorMessages;
    using static Global.GlobalConstants.DataValidations;

    public class BecomeMasseurFormModel
    {
        [Required]
        [StringLength(FullNameMaxLength,
            ErrorMessage = FullNameLength,
            MinimumLength = FullNameMinLength)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(UrlRegex, 
            ErrorMessage = InvalidUrl)]
        [Display(Name = "Profile Image URL")]
        public string ProfileImageUrl { get; set; }

        [Required]
        [StringLength(MasseurDescriptionMaxLength,
             ErrorMessage = MasseurDescriptionLength,
             MinimumLength = MasseurDescriptionMinLength)]
        public string Description { get; set; }

        public string CategoryId { get; set; }

        public Gender Gender { get; set; }

        public IEnumerable<MassageCategoryServiceModel> Categories
        { get; set; }
    }
}
