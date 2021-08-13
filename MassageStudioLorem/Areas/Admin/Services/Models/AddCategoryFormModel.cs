namespace MassageStudioLorem.Areas.Admin.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Global.GlobalConstants.DataValidations;
    using static Global.GlobalConstants.ErrorMessages;

    public class AddCategoryFormModel
    {
        [Required]
        [StringLength(CategoryNameMaxLength,
            MinimumLength = CategoryNameMinLength,
            ErrorMessage = CategoryNameLength)]
        public string Name { get; set; }
    }
}
