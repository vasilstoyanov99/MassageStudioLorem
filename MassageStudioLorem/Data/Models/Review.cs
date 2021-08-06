namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Global.GlobalConstants.DataValidations;

    public class Review
    {
        public Review() => this.Id = Guid.NewGuid().ToString();

        public string Id { get; set; }

        [Required]
        [MaxLength(ReviewMaxLength)]
        public string Content { get; set; }

        [Required] 
        public string ClientId { get; set; }

        [Required]
        public string ClientFirstName { get; set; }

        [Required] 
        public string MasseurId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}