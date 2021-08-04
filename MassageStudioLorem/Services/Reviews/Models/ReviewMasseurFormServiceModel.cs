namespace MassageStudioLorem.Services.Reviews.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Global.GlobalConstants.DataValidations;
    using static Global.GlobalConstants.ErrorMessages;

    public class ReviewMasseurFormServiceModel
    {
        //TODO: Check I more validation attributes need to be added in the other view models!

        [Required]
        [StringLength(ReviewMaxLength, 
            MinimumLength = ReviewMinLength,
            ErrorMessage = ReviewLength)]
        public string Content { get; set; }

        [Required] 
        public string ClientId { get; set; }

        [Required]
        public string MasseurId { get; set; }

        public string MasseurFullName { get; set; }

        public string AppointmentId { get; set; }
    }
}
