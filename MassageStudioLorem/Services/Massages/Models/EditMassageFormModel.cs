namespace MassageStudioLorem.Services.Massages.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Global.GlobalConstants.DataValidations;
    using static Global.GlobalConstants.ErrorMessages;

    public class EditMassageFormModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(MassageNameMaxLength, 
            MinimumLength = MassageNameMinLength,
            ErrorMessage = MassageNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(ShortDescriptionMaxLength,
            MinimumLength = ShortDescriptionMinLength,
            ErrorMessage = MassageDescriptionLength)]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Required]
        [StringLength(LongDescriptionMaxLength,
            MinimumLength = LongDescriptionMinLength,
            ErrorMessage = MassageDescriptionLength)]
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        [Required]
        [RegularExpression(UrlRegex,
            ErrorMessage = InvalidUrl)]
        [Display(Name = "Massage Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        [Range(MassageMinPrice,
            MassageMaxPrice, 
            ErrorMessage = PriceRange)]
        public double Price { get; set; }
    }
}
