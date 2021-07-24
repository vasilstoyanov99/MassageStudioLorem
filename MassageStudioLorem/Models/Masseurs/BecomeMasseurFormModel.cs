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
        [StringLength(NameMaxLength,
            ErrorMessage = NameError,
            MinimumLength = NameMinLength)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(NameMaxLength,
            ErrorMessage = NameError,
            MinimumLength = NameMinLength)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(UrlRegex, 
            ErrorMessage = InvalidUrl)]
        [Display(Name = "Profile Image URL")]
        public string ProfileImageUrl { get; set; }

        [Required]
        [StringLength(MasseurDescriptionMaxLength,
             ErrorMessage = MasseurDescription,
             MinimumLength = MasseurDescriptionMinLength)]
        public string Description { get; set; }

        public string CategoryId { get; set; }

        public Gender Gender { get; set; }

        public IEnumerable<MassageCategoryViewModel> Categories
        { get; set; }

        //public ICollection<int> SelectedCategoriesIds { get; set; }
    }
}
