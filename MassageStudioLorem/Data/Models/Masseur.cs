namespace MassageStudioLorem.Data.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Global.GlobalConstants.DataValidations;

    public class Masseur
    {
        public Masseur()
        {
            this.Id = Guid.NewGuid().ToString();
            this.WorkSchedule = new HashSet<Appointment>();
            this.Reviews = new HashSet<Review>();
        }

        public string Id { get; set; }

        [Required] 
        [MaxLength(FullNameMaxLength)] 
        public string FullName { get; set; }

        public Gender Gender { get; set; }

        [Required]
        [RegularExpression(UrlRegex)]
        public string ProfileImageUrl { get; set; }

        [Required]
        [MaxLength(MasseurDescriptionMaxLength)]
        public string Description { get; set; }

        [Required] 
        public string UserId { get; set; }

        public string CategoryId { get; set; }

        public ICollection<Appointment> WorkSchedule { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}